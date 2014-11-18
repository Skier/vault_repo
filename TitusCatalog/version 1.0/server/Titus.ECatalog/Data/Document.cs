using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Titus.ECatalog.Entity;
using Titus.ECatalog.Util;

namespace Titus.ECatalog.Data
{

    public class Document
    {

        private static Document c_Document = new Document();

        public static Document GetInstance()
        {
            return c_Document;
        }

        private Document()
        {
        }

        private const string SQL_SELECT_BY_ID = @"
            select  [DocumentId],
                    [Path],
                    [Name],
                    [Code]
            from    [Document]
            where   [DocumentId] = @DocumentId";

        public DocumentDataObject FindById(SqlTransaction tran, int documentId)
        {
            DocumentDataObject result = null;

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@DocumentId", documentId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ID, parms))
            {
                if (dataReader.Read())
                {
                    result = new DocumentDataObject();

                    result.DocumentId = (int)dataReader.GetValue(0);
                    result.Path = (string)dataReader.GetValue(1);
                    if (!dataReader.IsDBNull(2))
                    {
                        result.Name = (string)dataReader.GetValue(2);
                    }
                    else
                    {
                        result.Name = null;
                    }
                    if (!dataReader.IsDBNull(3))
                    {
                        result.Code = (string)dataReader.GetValue(3);
                    }
                    else
                    {
                        result.Code = null;
                    }
                }
            }

            return result;
        }

        private const string SQL_SELECT_BY_MODEL_ID = @"
            select  d.[DocumentId],
                    d.[Path],
                    d.[Name],
                    d.[Code],
                    dp.[PageNumber]
            from    [Document] d
                    inner join [DocumentPage] dp
                            on dp.[DocumentId] = d.[DocumentId]
                    inner join [DocumentPageModel] dpm
                            on dpm.[DocumentPageId] = dp.[DocumentPageId]
            where   [ModelId] = @ModelId";

        public DocumentDataObject FindByModelId(SqlTransaction tran, int modelId)
        {
            DocumentDataObject result = null;

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@ModelId", modelId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_MODEL_ID, parms))
            {
                if (dataReader.Read())
                {
                    result = new DocumentDataObject();

                    result.DocumentId = (int)dataReader.GetValue(0);
                    result.Path = (string)dataReader.GetValue(1);
                    if (!dataReader.IsDBNull(2))
                    {
                        result.Name = (string)dataReader.GetValue(2);
                    }
                    else
                    {
                        result.Name = null;
                    }
                    if (!dataReader.IsDBNull(3))
                    {
                        result.Code = (string)dataReader.GetValue(3);
                    }
                    else
                    {
                        result.Code = null;
                    }
                    result.PageNumber = (int)dataReader.GetValue(3);
                }
            }

            return result;
        }

        private const string SQL_SELECT_NOT_PROCESSED = @"
            select  [DocumentId],
                    [Path],
                    [Name],
                    [Code]
            from    [Document]
            where   [Name] is null";

        public List<DocumentDataObject> FindNotProcessed(SqlTransaction tran)
        {
            List<DocumentDataObject> result = new List<DocumentDataObject>();

            DbParameter[] parms = new DbParameter[0] { };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_NOT_PROCESSED, parms))
            {
                if (dataReader.Read())
                {
                    DocumentDataObject documentInfo = new DocumentDataObject();

                    documentInfo.DocumentId = (int)dataReader.GetValue(0);
                    documentInfo.Path = (string)dataReader.GetValue(1);
                    if (!dataReader.IsDBNull(2))
                    {
                        documentInfo.Name = (string)dataReader.GetValue(2);
                    }
                    else
                    {
                        documentInfo.Name = null;
                    }
                    if (!dataReader.IsDBNull(3))
                    {
                        documentInfo.Code = (string)dataReader.GetValue(3);
                    }
                    else
                    {
                        documentInfo.Code = null;
                    }

                    result.Add(documentInfo);
                }
            }

            return result;
        }

        private const string SQL_INSERT = @"
        insert  into [Document]
              ( [Path],
                [Name] )
        values( @Path,
                @Name );
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, DocumentDataObject documentInfo)
        {
            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@Path", documentInfo.Path),
                new SqlParameter("@Name", (null == documentInfo.Name)? DBNull.Value: (object)documentInfo.Name)
            };

            documentInfo.DocumentId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_UPDATE = @"
        update  [Document]
        set     [Path] = @Path,
                [Name] = @Name
        where   [DocumentId] = @DocumentId";

        public void Update(SqlTransaction tran, DocumentDataObject documentInfo)
        {
            DbParameter[] parms = new DbParameter[3] {
                new SqlParameter("@DocumentId", documentInfo.DocumentId),
                new SqlParameter("@Path", documentInfo.Path),
                new SqlParameter("@Name", (null == documentInfo.Name)? DBNull.Value: (object)documentInfo.Name)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

    }

}
