using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace Weborb.Data.Inspector2.Common
{
public abstract class BaseInspector : IInspector
{
    public const string SUCCESS_CODE = "success";

    public abstract String[] GetDatabases();

    public abstract String[] GetTables( string database );

    public abstract string Ping();

    public abstract ColumnInfo[] GetColumns( string database, string table );

    public abstract QueryResult TestQuery( string database, string query );

    public void GenerateSourceCode(string destdir, string database, string table, string query)
    {
        FileStream stream = new FileStream(
            Path.Combine(destdir, "generated\\" + table + "DAO.cs"), 
            FileMode.Create);
        StreamWriter writer = new StreamWriter(stream);

        ColumnInfo[] columns = this.GetColumns(database, table);
        String columnsForUpdate = "";
        String values = "";
        String columnsForInsert = "";
        String valuesForInsert = "";
        Boolean first = true;
        ColumnInfo primaryKey = null;
        for (int i = 0; i < columns.Length; i++)
        {
            ColumnInfo column = columns[i];
            if (!ColumnKeyType.PRIMARY.Equals(column.keyType))
            {
                if (!first)
                {
                    columnsForUpdate += ", ";
                }
                else
                {
                    first = false;
                }
                columnsForUpdate += column.name + " = " + GetParameterName(column.name);
                values += 
"                command.Parameters.Add(\"" + column.name + "\", record[\"" + column.name + "\"]);\r\n";
            } 
            else
            {
                primaryKey = column;
            }

            columnsForInsert += (0 != i) ? ", " + column.name : column.name;
            valuesForInsert += (0 != i) ? ", " + GetParameterName(column.name) : GetParameterName(column.name);
        }
        if ( null == primaryKey ) {
            throw new Exception("Primary key is not defined for table " + table);
        }

        values += 
"                command.Parameters.Add(\"" + primaryKey.name + "\", record[\"" + primaryKey.name + "\"]);\r\n";


        writer.WriteLine(
"using System;\r\n"
+ "using System.Collections;\r\n"
+ "using System.Text;\r\n"
+ "using System.Data;\r\n"
+ "\r\n"
+ "namespace WebORB.Generated\r\n"
+ "{\r\n"
+ "    public class " + table + "DAO\r\n"
+ "    {\r\n"
+ "        private static string connectionString = \"" + GetConnectionString(database) + "\";\r\n"
+ "\r\n"
+ "        public DataTable get" + table + "s()\r\n"
+ "        {\r\n"
+ "            DataTable result = new DataTable();\r\n"
+ "            " + GetDataAdapterName() + " dataAdapter = new " + GetDataAdapterName() + "(\"" + query + "\", connectionString );\r\n"
+ "            dataAdapter.Fill( result );\r\n"
+ "            return result;\r\n"
+ "        }\r\n"
+ "\r\n"
+ "        public void update" + table + "( Hashtable record )\r\n"
+ "        {\r\n"
+ "            " + GetConnectionName() + " connection = new " + GetConnectionName() + "( connectionString );\r\n"
+ "            try {\r\n"
+ "                String query = \"update " + table + " set \"\r\n"
+ "                 + \"" + columnsForUpdate + " where " + primaryKey.name + " = " + GetParameterName(primaryKey.name) + "\";\r\n"
+ "                " + GetCommandName() + " command = new " + GetCommandName() + "(query, connection);\r\n"
+ values
+ "                connection.Open();\r\n"
+ "                command.ExecuteNonQuery();\r\n"
+ "            } finally {\r\n"
+ "                connection.Close();\r\n"
+ "            }\r\n"
+ "        }\r\n"
+ "\r\n"
+ "        public void remove" + table + "( Hashtable record )\r\n"
+ "        {\r\n"
+ "            " + GetConnectionName() + " connection = new " + GetConnectionName() + "( connectionString );\r\n"
+ "            try {\r\n"
+ "                String query = \"delete from " + table + " where " + primaryKey.name + " = " + GetParameterName(primaryKey.name) + "\";\r\n"
+ "                " + GetCommandName() + " command = new " + GetCommandName() + "(query, connection);\r\n"
+ "                command.Parameters.Add(\"" + primaryKey.name + "\", record[\"" + primaryKey.name + "\"]);\r\n"
+ "                connection.Open();\r\n"
+ "                command.ExecuteNonQuery();\r\n"
+ "            } finally {\r\n"
+ "                connection.Close();\r\n"
+ "            }\r\n"
+ "        }\r\n"
+ "\r\n"
+ "        public void insert" + table + "( Hashtable record )\r\n"
+ "        {\r\n"
+ "            " + GetConnectionName() + " connection = new " + GetConnectionName() + "( connectionString );\r\n"
+ "            try {\r\n"
+ "                String query = \"insert into " + table + "\"\r\n"
+ "                 + \" (" + columnsForInsert + ") values (" + valuesForInsert + ")\";\r\n"
+ "                " + GetCommandName() + " command = new " + GetCommandName() + "(query, connection);\r\n"
+ values
+ "                connection.Open();\r\n"
+ "                command.ExecuteNonQuery();\r\n"
+ "            } finally {\r\n"
+ "                connection.Close();\r\n"
+ "            }\r\n"
+ "        }\r\n"
+ "\r\n"
+ "    }\r\n"
+ "}\r\n");
        writer.Close();

    }

    public void CompileGeneratedSources(string destdir, string table) {
        Directory.SetCurrentDirectory(Path.Combine(destdir, "bin"));
        CSharpCodeProvider codeProvider = new CSharpCodeProvider();
        ICodeCompiler icc = codeProvider.CreateCompiler();
        System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
        parameters.OutputAssembly = table + "DAO.dll";
        parameters.TempFiles = new TempFileCollection(Path.Combine(destdir, "generated"));
        foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) 
        {
            parameters.ReferencedAssemblies.Add(asm.Location);
        }
/*
        parameters.ReferencedAssemblies.Add("System.dll");
        parameters.ReferencedAssemblies.Add("System.Data.dll");
        parameters.ReferencedAssemblies.Add("System.Xml.dll");
        
        string[] assemblies = GetReferencedAssemblies();
        foreach (string assembly in assemblies) {
            parameters.ReferencedAssemblies.Add(assembly);
        }
*/
        CompilerResults results = icc.CompileAssemblyFromFile(parameters, 
            Path.Combine(destdir,  "generated\\" + table + "DAO.cs"));
        if ( results.Errors.Count > 0 ) {
            FileStream stream = new FileStream(
                Path.Combine(destdir, "generated\\" + table + "DAO.err"), 
                FileMode.Create);
            StreamWriter writer = new StreamWriter(stream);
            foreach (CompilerError err in results.Errors) {
                writer.WriteLine(err.Line + ", "
                    + err.ErrorNumber + ", "
                    + err.ErrorText);
            }
            writer.Close();
            throw new Exception("Cannot compile generated sources.");
        }
    }
/*
    private ForeignKeyData GetForeignKey( string database, string table, string column )
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

*/
    protected abstract string GetConnectionString(string database);

    protected abstract string GetParameterName(string columnname);

    protected abstract string GetDataAdapterName();

    protected abstract string GetConnectionName();

    protected abstract string GetCommandName();

    protected abstract string[] GetReferencedAssemblies();

    protected String[] GetStringCollection(IDataReader reader)
    {
        List<String> collection = new List<String>();
        try
        {
            while (reader.Read())
            {
                collection.Add(reader.GetString(0));
            }
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
        }

        return collection.ToArray();
    }
}
}
