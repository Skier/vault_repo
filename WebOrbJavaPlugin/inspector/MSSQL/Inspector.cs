using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Weborb.Data.Inspector2.Common;

namespace Weborb.Data.Inspector2.MSSQL
{
    public class Inspector : BaseInspector
    {
        private const string CONNECTION_STRING = "server={0};user id={1};password={2};database={3};connect timeout=30;";
        private const string MASTER_DATABASE = "MASTER";
        private const string SELECT_DATABASES = "SELECT CATALOG_NAME FROM INFORMATION_SCHEMA.SCHEMATA";
        private const string SELECT_TABLES = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";
        private const string SELECT_COLUMNS =
            "SELECT c.COLUMN_NAME, c.IS_NULLABLE, c.DATA_TYPE, tc.CONSTRAINT_TYPE"
            +"    FROM INFORMATION_SCHEMA.COLUMNS c"
            +"    LEFT OUTER JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu"
            +"        on c.TABLE_NAME = ccu.TABLE_NAME"
            +"            and c.COLUMN_NAME = ccu.COLUMN_NAME"
            +"    LEFT OUTER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc"
            +"        on ccu.TABLE_NAME = tc.TABLE_NAME"
            +"            and ccu.CONSTRAINT_NAME = tc.CONSTRAINT_NAME"
            +"            and tc.CONSTRAINT_TYPE='PRIMARY KEY'"
            +" WHERE c.TABLE_NAME='{0}'";

        private string hostname;
        private string userid;
        private string password;
        private SqlConnection connection;

        public Inspector( string hostname, string userid, string password )
        {
            this.hostname = hostname;
            this.userid = userid;
            this.password = password;
            this.connection = new SqlConnection();
            this.connection.ConnectionString = GetConnectionString(Inspector.MASTER_DATABASE);
        }

        public override String[] GetDatabases()
        {
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand(Inspector.SELECT_DATABASES, connection);
                SqlDataReader reader = command.ExecuteReader();
                return GetStringCollection(reader);
            }
            finally
            {
                connection.Close();
            }
        }

        public override String[] GetTables( string database )
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = GetConnectionString(database);
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(Inspector.SELECT_TABLES, conn);
                SqlDataReader reader = command.ExecuteReader();
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
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = GetConnectionString(database);
            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand(
                    string.Format(Inspector.SELECT_COLUMNS, table), conn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ColumnInfo column = new ColumnInfo();
                    column.name = reader.GetString(0);
                    column.isNullable = reader.GetString(1).ToLower().Equals("yes");
                    column.dataTypeInfo = ProcessDataType(reader.GetString(2));
                    column.keyType = reader.IsDBNull(3) 
                        ? ColumnKeyType.NONE 
                        : ProcessKeyType(reader.GetString(3));
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
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = GetConnectionString(database);

            try {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataReader reader = command.ExecuteReader();
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
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = GetConnectionString( "information_schema" );
            MySqlCommand command = new MySqlCommand( query, connection );
            MySqlDataReader reader = null;

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

        private ColumnKeyType ProcessKeyType( String keyTypeStr )
        {
            keyTypeStr = keyTypeStr.ToUpper();

            if( keyTypeStr.Equals( "PRIMARY KEY" ) )
                return ColumnKeyType.PRIMARY;
            else
                return ColumnKeyType.NONE;
        }

        protected override string GetConnectionString(string database)
        {
            string result = String.Format(Inspector.CONNECTION_STRING, hostname, userid, password, database );
            return result;
        }

        protected override string GetParameterName(string columnname) {
            return "@" + columnname;
        }

        protected override string GetDataAdapterName() {
            return "System.Data.SqlClient.SqlDataAdapter";
        }

        protected override string GetConnectionName() {
            return "System.Data.SqlClient.SqlConnection";
        }

        protected override string GetCommandName() {
            return "System.Data.SqlClient.SqlCommand";
        }

        protected override string[] GetReferencedAssemblies() {
            return new string[0];
        }

   }
}
