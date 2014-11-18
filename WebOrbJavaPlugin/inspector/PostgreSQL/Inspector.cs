using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Npgsql;
using Weborb.Data.Inspector2.Common;

namespace Weborb.Data.Inspector2.PostgreSQL
{
    public class Inspector : BaseInspector
    {
        private const string CONNECTION_STRING = "Server={0};Port={1};User Id={2};Password={3};Database={4};";
        private const string MASTER_DATABASE = "template1";
        private const string SELECT_DATABASES = "select datname from pg_catalog.pg_database where datname!='template0'";
        private const string SELECT_TABLES = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE table_type='BASE TABLE' AND table_schema NOT IN ('pg_catalog', 'information_schema')";
        private const string SELECT_COLUMNS = 
              "select c.column_name, c.is_nullable, c.data_type, "
	        + "    (select tc.constraint_type "
	        + "     from information_schema.table_constraints tc "
		    + "     inner join information_schema.constraint_column_usage ccu on ccu.constraint_name=tc.constraint_name "
	        + "         where tc.constraint_type='PRIMARY KEY' and c.table_name=ccu.table_name and c.column_name=ccu.column_name) "
            + "from information_schema.columns c "
            + "where c.table_name = '{0}'";

        private string hostname;
        private string port;
        private string userid;
        private string password;
        private NpgsqlConnection connection;

        public Inspector( string hostname, string port, string userid, string password )
        {
            this.hostname = hostname;
            this.port = port;
            this.userid = userid;
            this.password = password;
            this.connection = new NpgsqlConnection();
            this.connection.ConnectionString = GetConnectionString(Inspector.MASTER_DATABASE);
        }

        public override String[] GetDatabases()
        {
            try
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(Inspector.SELECT_DATABASES, connection);
                NpgsqlDataReader reader = command.ExecuteReader();
                return GetStringCollection(reader);
            }
            finally
            {
                connection.Close();
            }
        }

        public override String[] GetTables( string database )
        {
            NpgsqlConnection conn = new NpgsqlConnection();
            conn.ConnectionString = GetConnectionString(database);
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(Inspector.SELECT_TABLES, conn);
                NpgsqlDataReader reader = command.ExecuteReader();
                return GetStringCollection(reader);
            }
            finally
            {
                conn.Close();
            }
        }

        public override string Ping()
        {
            try
            {
                this.connection.Open();
                return BaseInspector.SUCCESS_CODE;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                this.connection.Close();
            }
        }

        public override ColumnInfo[] GetColumns( string database, string table )
        {
            List<ColumnInfo> columns = new List<ColumnInfo>();
            NpgsqlConnection conn = new NpgsqlConnection();
            conn.ConnectionString = GetConnectionString(database);
            try
            {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(
                    string.Format(Inspector.SELECT_COLUMNS, table), conn);
                NpgsqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ColumnInfo column = new ColumnInfo();
                    column.name = reader.GetString(0);
                    column.isNullable = reader.GetString(1).ToLower().Equals("yes");
                    column.dataTypeInfo = ProcessDataType(reader.GetString(2));
                    column.keyType = reader.IsDBNull(3) ? ColumnKeyType.NONE : ProcessKeyType(reader.GetString(3));
/*
                    column.isAutoIncrement = reader.IsDBNull(4) ? false : reader.GetString(3).Equals("auto_increment");
                    column.foreignKey = GetForeignKey(database, table, column.name);
*/
                    columns.Add(column);
                }
            }
            finally
            {
                conn.Close();
            }
            return columns.ToArray();
        }

        public override QueryResult TestQuery( string database, string query )
        {
            QueryResult queryResult = new QueryResult();
            List<Object[]> result = new List<Object[]>();
            NpgsqlConnection conn = new NpgsqlConnection();
            conn.ConnectionString = GetConnectionString(database);

            try {
                conn.Open();
                NpgsqlCommand command = new NpgsqlCommand(query, conn);
                NpgsqlDataReader reader = command.ExecuteReader();
                DataTable schemaTable = reader.GetSchemaTable();

                int count = schemaTable.Rows.Count;
                queryResult.columnTitles = new string[ count ];

                for( int i = 0; i < count; i++ )
                    queryResult.columnTitles[ i ] = (string) schemaTable.Rows[ i ][ "ColumnName" ];

                while( reader.Read() )
                {
                    Object[] row = new Object[ reader.FieldCount ];

                    for( int i = 0; i < row.Length; i++ )
                    {
                        row[ i ] = reader.GetValue( i );

                        if( row[ i ] is DBNull )
                            row[ i ] = null;
                    }

                    result.Add( row );
                }
            }
            finally
            {
                conn.Close();
            }
            queryResult.data = result.ToArray();
            return queryResult;
        }

        private ForeignKeyData GetForeignKey(string database, string table, string column)
        {
/*
            String query = String.Format( "select referenced_table_schema, referenced_table_name, referenced_column_name from key_column_usage where table_schema = '{0}' and table_name = '{1}' and column_name = '{2}' and referenced_table_schema is not null;", database, table, column );
            MyNpgsqlConnection connection = new MyNpgsqlConnection();
            connection.ConnectionString = GetConnectionString( "information_schema" );
            MyNpgsqlCommand command = new MyNpgsqlCommand( query, connection );
            MyNpgsqlDataReader reader = null;

            try
            {
                command.Connection.Open();
                reader = command.ExecuteReader();

                if( !reader.HasRows )
                    return null;

                reader.Read();
                ForeignKeyData foreignKeyData = new ForeignKeyData();
                foreignKeyData.database = reader.GetString( 0 );
                foreignKeyData.table = reader.GetString( 1 );
                foreignKeyData.column = reader.GetString( 2 );
                return foreignKeyData;
            }
            finally
            {
                if( reader != null )
                    reader.Close();

                command.Connection.Close();
            }
 */
            throw new NotImplementedException();
        }

        private ColumnDataTypeInfo ProcessDataType( String dataTypeStr )
        {
            ColumnDataTypeInfo typeInfo = new ColumnDataTypeInfo();
            typeInfo.dataType = dataTypeStr;
            return typeInfo;
        }

        protected override string GetConnectionString(string database)
        {
            return String.Format(Inspector.CONNECTION_STRING, hostname, port, userid, password, database );
        }

        protected override string GetParameterName(string columnname) {
            return ":" + columnname;
        }

        protected override string GetDataAdapterName() {
            return "Npgsql.NpgsqlDataAdapter";
        }

        protected override string GetConnectionName() {
            return "Npgsql.NpgsqlConnection";
        }

        protected override string GetCommandName() {
            return "Npgsql.NpgsqlCommand";
        }

        protected override string[] GetReferencedAssemblies() {
            return new string[] {"Npgsql.dll", "Mono.Security.dll"};
        }

        private ColumnKeyType ProcessKeyType( String keyTypeStr ) {
            keyTypeStr = keyTypeStr.ToLower();
            if( keyTypeStr.Equals( "primary key" ) )
                return ColumnKeyType.PRIMARY;
            else
                return ColumnKeyType.NONE;
        }

    }
}
