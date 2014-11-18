using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Weborb.Data.Inspector2.Common;
using MySql.Data.MySqlClient;

namespace Weborb.Data.Inspector2.MySQL
{
    public class Inspector : BaseInspector
    {
        private string hostname;
        private string userid;
        private string password;
        private string port = "3306";
        private MySqlConnection connection;

        public Inspector( string hostname, string port, string userid, string password )
        {
            this.hostname = hostname;
            this.port = port;
            this.userid = userid;
            this.password = password;
            this.connection = new MySqlConnection();
            this.connection.ConnectionString = GetConnectionString( "information_schema" );
        }

        public override String[] GetDatabases()
        {
            return GetStringCollection( "SELECT SCHEMA_NAME FROM SCHEMATA" );
        }

        public override String[] GetTables( string database )
        {
            return GetStringCollection( "SELECT TABLE_NAME FROM TABLES WHERE TABLE_SCHEMA = '" + database + "';" );
        }

        public override string Ping()
        {
            try
            {
                this.connection.Open();
                return "success";
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
            String query = String.Format( "select column_name, is_nullable, column_type, column_key, extra from columns where table_schema = '{0}' and table_name = '{1}'", database, table );
            MySqlCommand command = new MySqlCommand( query, connection );
            MySqlDataReader reader = null;

            try
            {                
                command.Connection.Open();
                reader = command.ExecuteReader();

                while( reader.Read() )
                {
                    ColumnInfo column = new ColumnInfo();
                    column.name = reader.GetString( 0 );
                    column.isNullable = reader.GetString( 1 ).ToLower().Equals( "yes" );
                    column.dataTypeInfo = ProcessDataType( reader.GetString( 2 ) );
                    column.keyType = reader.IsDBNull( 3 ) ? ColumnKeyType.NONE : ProcessKeyType( reader.GetString( 3 ) );
                    column.isAutoIncrement = reader.IsDBNull( 4 ) ? false : reader.GetString( 3 ).Equals( "auto_increment" );
                    column.foreignKey = GetForeignKey( database, table, column.name );
                    columns.Add( column );
                }
            }
            finally
            {
                if( reader != null )
                    reader.Close();

                command.Connection.Close();
            }

            return columns.ToArray();
        }

        public override QueryResult TestQuery( string database, string query )
        {
            MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = GetConnectionString( database );

            MySqlCommand command = new MySqlCommand( query, connection );
            MySqlDataReader reader = null;
            QueryResult queryResult = new QueryResult();
            List<Object[]> result = new List<Object[]>();

            try
            {
                command.Connection.Open();
                reader = command.ExecuteReader();
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
                if( reader != null )
                    reader.Close();

                command.Connection.Close();
            }
            queryResult.data = result.ToArray();
            return queryResult;
        }

        private ForeignKeyData GetForeignKey(string database, string table, string column)
        {
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
        }

        private ColumnDataTypeInfo ProcessDataType( String dataTypeStr )
        {
            ColumnDataTypeInfo typeInfo = new ColumnDataTypeInfo();
            String dataTypeTruncated = dataTypeStr;
            int index = dataTypeStr.IndexOf( '(' );

            if( index != -1 )
            {
                dataTypeTruncated = dataTypeStr.Substring( 0, index );
                int endIndex = dataTypeStr.IndexOf( ')', index );
                string content = dataTypeStr.Substring( index + 1, endIndex - index - 1 );

                if( !int.TryParse( content, out typeInfo.size ) )
                {
                    string[] values = content.Split( new char[] { ',' } );

                    for( int i = 0; i < values.Length; i++ )
                        values[ i ] = values[ i ].Replace( "'", "" );

                    typeInfo.values = values;
                    typeInfo.isEnumerable = true;
                }
            }

            typeInfo.dataType = dataTypeTruncated;
            return typeInfo;
        }

        private ColumnKeyType ProcessKeyType( String keyTypeStr )
        {
            keyTypeStr = keyTypeStr.ToLower();

            if( keyTypeStr.Equals( "pri" ) )
                return ColumnKeyType.PRIMARY;
            else if( keyTypeStr.Equals( "uni" ) )
                return ColumnKeyType.UNIQUE;
            else if( keyTypeStr.Equals( "mul" ) )
                return ColumnKeyType.FULLTEXT;
            else
                return ColumnKeyType.NONE;
        }

        /*
        public ColumnMetadata[] GetTableInfo( string database, string table )
        {
        }
         * */

        private String[] GetStringCollection( string query )
        {
            List<String> collection = new List<String>();
            MySqlCommand command = new MySqlCommand( query, connection );
            MySqlDataReader reader = null;

            try
            {                
                command.Connection.Open();
                reader = command.ExecuteReader();

                while( reader.Read() )
                    collection.Add( reader.GetString( 0 ) );
            }
            finally
            {
                if( reader != null )
                    reader.Close();

                command.Connection.Close();
            }

            return collection.ToArray();
        }

        protected override string GetConnectionString( string database )
        {
            return String.Format("server={0};port={1};user id={2};password={3};database={4};persist security info=true;", hostname, port, userid, password, database );
        }

        protected override string GetParameterName(string columnname) {
            return "@" + columnname;
        }

        protected override string GetDataAdapterName() {
            return "MySql.Data.MySqlClient.MySqlDataAdapter";
        }

        protected override string GetConnectionName() {
            return "MySql.Data.MySqlClient.MySqlConnection";
        }

        protected override string GetCommandName() {
            return "MySql.Data.MySqlClient.MySqlCommand";
        }

        protected override string[] GetReferencedAssemblies() {
            return new string[] {"MySql.Data.dll"};
        }

   }
}
