using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Weborb.Data.Management;

namespace TractInc.DocCapture.Domain
{

    public partial class DocumentDataMapper
    {
        
        private String SqlGetCountByDoc = "Select count(*) From document Where 1=1 ";

        private void PrepareCommand(Document document, SqlCommand sqlCommand)
        {
            if (document.DocTypeId != null && document.DocTypeId != 0)
                sqlCommand.CommandText += " and DocTypeId = @DocTypeId ";
            if (document.State != null && document.State.Trim() != String.Empty)
                sqlCommand.CommandText += " and State = @State ";
            if (document.County != null && document.County.Trim() != String.Empty)
                sqlCommand.CommandText += " and County = @County ";
            if (document.DocumentNo != null && document.DocumentNo.Trim() != String.Empty)
            {
                sqlCommand.CommandText += " and ( DocumentNo = @DocumentNo or ( ";

                if (document.Vol != null && document.Vol.Trim() != String.Empty)
                    sqlCommand.CommandText += " Vol = @Vol and ";
                else
                    sqlCommand.CommandText += " 1!=1 and ";
                
                if (document.Pg != null && document.Pg.Trim() != String.Empty)
                    sqlCommand.CommandText += " Pg = @Pg and";
                else
                    sqlCommand.CommandText += " 1!=1 and";

                sqlCommand.CommandText += " 1=1 ";
                
                sqlCommand.CommandText += ") )";

            }
            else
            {
                if (document.Vol != null && document.Vol.Trim() != String.Empty)
                    sqlCommand.CommandText += " and Vol = @Vol ";
                if (document.Pg != null && document.Pg.Trim() != String.Empty)
                    sqlCommand.CommandText += " and Pg = @Pg ";
            }

            if (document.DocTypeId != null && document.DocTypeId != 0)
                sqlCommand.Parameters.AddWithValue("@DocTypeId", document.DocTypeId);
            if (document.State != null && document.State.Trim() != String.Empty)
                sqlCommand.Parameters.AddWithValue("@State", document.State);
            if (document.County != null && document.County.Trim() != String.Empty)
                sqlCommand.Parameters.AddWithValue("@County", document.County);
            if (document.DocumentNo != null && document.DocumentNo.Trim() != String.Empty)
                sqlCommand.Parameters.AddWithValue("@DocumentNo", document.DocumentNo);
            if (document.Vol != null && document.Vol.Trim() != String.Empty)
                sqlCommand.Parameters.AddWithValue("@Vol", document.Vol);
            if (document.Pg != null && document.Pg.Trim() != String.Empty)
                sqlCommand.Parameters.AddWithValue("@Pg", document.Pg);

        }

        public int GetCountByDoc(Document document)
        {
            int result;

            using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
            {
                using (SqlCommand sqlCommand = Database.CreateCommand(SqlGetCountByDoc))
                {
                    PrepareCommand(document, sqlCommand);

                    Object obj = sqlCommand.ExecuteScalar();

                    String str = obj.ToString();

                    result = Int32.Parse(str);

                }
            }

            return result;
        }

        private String SqlGetListByDoc = @"Select
            DocID, IsPublic, DocTypeId, Vol, Pg, DocumentNo, County, State, 
            DateFiled,DateSigned,ResearchNote,ImageLink 
            From document Where 1=1 ";

        public List<Document> GetListByDoc(Document document)
        {
            List<Document> result = new List<Document>();

            using (DatabaseConnectionMonitor monitor = new DatabaseConnectionMonitor(Database))
            {
                using (SqlCommand sqlCommand = Database.CreateCommand(SqlGetListByDoc))
                {
                    PrepareCommand(document, sqlCommand);

                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            result.Add(doLoad(dataReader));
                        }
                    }
                }
            }

            return result;
        }

    }

}