using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace Servman.Codegen
{
    public class DatabaseMeta
    {

        const String SqlRelatedTables =
            "SELECT  sysobjects.name AS RelatedTable " +
            "FROM    sysobjects " +
            "INNER JOIN  syscolumns ON sysobjects.id = syscolumns.id " +
            "INNER JOIN  sysforeignkeys ON syscolumns.id = sysforeignkeys.fkeyid " +
               "AND  syscolumns.colid = sysforeignkeys.fkey " +
            "INNER JOIN  syscolumns syscolumns2 " +
               "ON sysforeignkeys.rkeyid = syscolumns2.id " +
               "AND  sysforeignkeys.rkey = syscolumns2.colid " +
            "INNER JOIN  sysobjects sysobjects2 ON syscolumns2.id = sysobjects2.id " +
            "WHERE   sysobjects2.name = @TableName " +
            "group by " +
            "sysobjects.name ";

        const String SqlDependsTables =
            "SELECT  sysobjects.name AS DependsTable " +
            "FROM    sysobjects " +
            "INNER JOIN  syscolumns ON sysobjects.id = syscolumns.id " +
            "INNER JOIN  sysforeignkeys ON syscolumns.id = sysforeignkeys.rkeyid " +
               "AND  syscolumns.colid = sysforeignkeys.rkey " +
            "INNER JOIN  syscolumns syscolumns2  " +
               "ON sysforeignkeys.fkeyid = syscolumns2.id " +
               "AND  sysforeignkeys.fkey = syscolumns2.colid " +
            "INNER JOIN  sysobjects sysobjects2 ON syscolumns2.id = sysobjects2.id " +
            "WHERE   sysobjects2.name = @TableName " +
            "group by  " +
            "sysobjects.name";


        public List<TableMeta> Tables = new List<TableMeta>();

        public void Sort()
        {

           /*TableMeta[] tables = Tables.ToArray();

            for (int i = 0; i < tables.Length; i++)
            {
                for (int j = i+1; j < tables.Length; j++)
                {
                    if (tables[i].IsDepends(tables[j].Name))
                    {
                        TableMeta temp = tables[j];
                        tables[j] = tables[i];
                        tables[i] = temp;
                    }
                }
            }

            Tables = new List<TableMeta>(tables);*/


            for (int i = 0; i < Tables.Count; i++)
            {
                for (int j = i + 1; j < Tables.Count; j++)
                {
                    if (Tables[i].IsDepends(Tables[j].Name))
                    {
                        TableMeta temp = Tables[j];
                        Tables[j] = Tables[i];
                        Tables[i] = temp;
                    }
                }
            }

        }


        public TableMeta LoadTableMeta(String connectionString, String tableName)
        {

            foreach (TableMeta item in Tables)
                if (item.Name.Equals(tableName))
                    return item;


            TableMeta tableMeta = new TableMeta();

            tableMeta.Name = tableName;

            Tables.Add(tableMeta);


            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();


                using (SqlCommand sqlCommand = new SqlCommand(SqlDependsTables, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@TableName", SqlDbType.NVarChar).Value = tableName;

                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            if (dataReader.GetString(0) != tableName)
                            {
                                tableMeta.DependsTables.Add(dataReader.GetString(0),
                                    LoadTableMeta(connectionString, dataReader.GetString(0)));
                            }
                        }
                    }
                }

                using (SqlCommand sqlCommand = new SqlCommand(SqlRelatedTables, sqlConnection))
                {
                    sqlCommand.Parameters.Add("@TableName", SqlDbType.NVarChar).Value = tableName;


                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            if (dataReader.GetString(0) != tableName)
                            {
                                tableMeta.RelatedTables.Add(dataReader.GetString(0),
                                    LoadTableMeta(connectionString, dataReader.GetString(0)));
                            }
                        }
                    }
                }


                sqlConnection.Close();
            }

            return tableMeta;
        }
    }
}
