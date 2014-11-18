using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using Weborb.Data.Inspector2.Common;

namespace Weborb.Data.Inspector2.Oracle
{
    public class Inspector : BaseInspector
    {
        private const string CONNECTION_STRING = "User ID={0}; Password={1};Data Source={2};";
        private const string SELECT_TABLES = "SELECT TABLE_NAME FROM ALL_TABLES";
        private const string SELECT_COLUMNS = 
              "SELECT c.COLUMN_NAME, c.NULLABLE, c.DATA_TYPE, "
            + "    (select tc.constraint_type "
            + "     from all_constraints tc "
            + "         inner join all_cons_columns ccu on ccu.constraint_name=tc.constraint_name "
            + "     where tc.constraint_type='P' and c.table_name=ccu.table_name and c.column_name=ccu.column_name) "
            + "FROM ALL_TAB_COLS c "
            + "WHERE c.TABLE_NAME='{0}'";

        private string datasource;
        private string userid;
        private string password;

        public Inspector( string datasource, string userid, string password )
        {
            this.datasource = datasource;
            this.userid = userid;
            this.password = password;
        }

        public override String[] GetDatabases()
        {
            return new String[] {datasource};
        }

        public override String[] GetTables( string database )
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = GetConnectionString(database);
            try
            {
                conn.Open();
                OracleCommand command = new OracleCommand(Inspector.SELECT_TABLES, conn);
                OracleDataReader reader = command.ExecuteReader();
                return GetStringCollection(reader);
            }
            finally
            {
                conn.Close();
            }
        }

        public override string Ping()
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = GetConnectionString(datasource);
            try
            {
                conn.Open();
                return BaseInspector.SUCCESS_CODE;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                conn.Close();
            }
        }

        public override ColumnInfo[] GetColumns( string database, string table )
        {
            List<ColumnInfo> columns = new List<ColumnInfo>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = GetConnectionString(database);
            try
            {
                conn.Open();
                OracleCommand command = new OracleCommand(
                    string.Format(Inspector.SELECT_COLUMNS, table), conn);
                OracleDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ColumnInfo column = new ColumnInfo();
                    column.name = reader.GetString(0);
                    column.isNullable = reader.GetString(1).ToLower().Equals("y");
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
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = GetConnectionString(database);

            try {
                conn.Open();
                OracleCommand command = new OracleCommand(query, conn);
                OracleDataReader reader = command.ExecuteReader();
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

        private ForeignKeyData GetForeignKey( string database, string table, string column )
        {
/*
            String query = String.Format( "select referenced_table_schema, referenced_table_name, referenced_column_name from key_column_usage where table_schema = '{0}' and table_name = '{1}' and column_name = '{2}' and referenced_table_schema is not null;", database, table, column );
            MyOracleConnection connection = new MyOracleConnection();
            connection.ConnectionString = GetConnectionString( "information_schema" );
            MyOracleCommand command = new MyOracleCommand( query, connection );
            MyOracleDataReader reader = null;

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
            keyTypeStr = keyTypeStr.ToLower();
            if( keyTypeStr.Equals( "p" ) )
                return ColumnKeyType.PRIMARY;
            else
                return ColumnKeyType.NONE;
        }

        protected override string GetConnectionString(string database)
        {
            return String.Format(Inspector.CONNECTION_STRING, userid, password, database);
        }

        protected override string GetParameterName(string columnname) {
            return ":" + columnname;
        }

        protected override string GetDataAdapterName() {
            return "System.Data.OracleClient.OracleDataAdapter";
        }

        protected override string GetConnectionName() {
            return "System.Data.OracleClient.OracleConnection";
        }

        protected override string GetCommandName() {
            return "System.Data.OracleClient.OracleCommand";
        }

        protected override string[] GetReferencedAssemblies() {
            return new string[0];
        }

    }
}
