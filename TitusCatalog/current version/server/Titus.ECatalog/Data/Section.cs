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

    public class Section
    {

        private static Section c_Section = new Section();

        public static Section GetInstance()
        {
            return c_Section;
        }

        private Section()
        {
        }

        private const string SQL_SELECT_BY_ID = @"
            select  [SectionId],
                    [CatalogId],
                    [StartPageNumber],
                    [PagesTotal],
                    [SectionPrefix],
                    [PdfPath],
                    [Sort]
            from    [EC_Section]
            where   [SectionId] = @SectionId";

        public SectionDataObject FindById(SqlTransaction tran, int sectionId)
        {
            SectionDataObject result = null;

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@SectionId", sectionId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_ID, parms))
            {
                if (dataReader.Read())
                {
                    result = new SectionDataObject();

                    result.SectionId = (int)dataReader.GetValue(0);
                    result.CatalogId = (int)dataReader.GetValue(1);
                    result.StartPageNumber = (int)dataReader.GetValue(2);
                    result.PagesTotal = (int)dataReader.GetValue(3);
                    result.SectionPrefix = (string)dataReader.GetValue(4);
                    result.PdfPath = (string)dataReader.GetValue(5);
                    result.Sort = (int)dataReader.GetValue(6);
                }
            }

            return result;
        }

        private const string SQL_SELECT_BY_SECTION = @"
            select  [SectionId],
                    [CatalogId],
                    [StartPageNumber],
                    [PagesTotal],
                    [SectionPrefix],
                    [PdfPath],
                    [Sort]
            from    [EC_Section]
            where   [SectionPrefix] = @SectionPrefix";

        public SectionDataObject FindBySection(SqlTransaction tran, string prefix)
        {
            SectionDataObject result = null;

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@SectionPrefix", prefix) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_SECTION, parms))
            {
                if (dataReader.Read())
                {
                    result = new SectionDataObject();

                    result.SectionId = (int)dataReader.GetValue(0);
                    result.CatalogId = (int)dataReader.GetValue(1);
                    result.StartPageNumber = (int)dataReader.GetValue(2);
                    result.PagesTotal = (int)dataReader.GetValue(3);
                    result.SectionPrefix = (string)dataReader.GetValue(4);
                    result.PdfPath = (string)dataReader.GetValue(5);
                    result.Sort = (int)dataReader.GetValue(6);
                }
            }

            return result;
        }

        private const string SQL_SELECT_BY_CATALOG_ID = @"
            select  [SectionId],
                    [CatalogId],
                    [StartPageNumber],
                    [PagesTotal],
                    [SectionPrefix],
                    [PdfPath],
                    [Sort]
            from    [EC_Section]
            where   [CatalogId] = @CatalogId
            order   by [Sort]";

        public List<SectionDataObject> FindByCatalogId(SqlTransaction tran, int catalogId)
        {
            List<SectionDataObject> result = new List<SectionDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@CatalogId", catalogId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_CATALOG_ID, parms))
            {
                while (dataReader.Read())
                {
                    SectionDataObject sectionInfo = new SectionDataObject();
                    sectionInfo.SectionId = (int)dataReader.GetValue(0);
                    sectionInfo.CatalogId = (int)dataReader.GetValue(1);
                    sectionInfo.StartPageNumber = (int)dataReader.GetValue(2);
                    sectionInfo.PagesTotal = (int)dataReader.GetValue(3);
                    sectionInfo.SectionPrefix = (string)dataReader.GetValue(4);
                    sectionInfo.PdfPath = (string)dataReader.GetValue(5);
                    sectionInfo.Sort = (int)dataReader.GetValue(6);

                    result.Add(sectionInfo);
                }
            }

            return result;
        }

        private const string SQL_SELECT_BY_MODEL_AND_LEVEL = @"
            select  s.[SectionId],
                    s.[CatalogId],
                    s.[StartPageNumber],
                    s.[PagesTotal],
                    s.[SectionPrefix],
                    s.[PdfPath],
                    s.[Sort]
            from    [EC_Section] s
                    inner join [EC_SectionPage] sp
                            on sp.[SectionId] = s.[SectionId]
                    inner join [EC_CatalogItem] ci
                            on ci.[SectionPageId] = sp.[DocumentPageId]
            where   ci.[ModelId] = @ModelId
            and     ci.[CatalogLevel] = @CatalogLevel";

        public SectionDataObject FindByModelAndLevel(SqlTransaction tran, int modelId, int catalogLevel)
        {
            SectionDataObject result = null;

            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@ModelId", modelId),
                new SqlParameter("@CatalogLevel", catalogLevel)
            };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_MODEL_AND_LEVEL, parms))
            {
                if (dataReader.Read())
                {
                    result = new SectionDataObject();

                    result.SectionId = (int)dataReader.GetValue(0);
                    result.CatalogId = (int)dataReader.GetValue(1);
                    result.StartPageNumber = (int)dataReader.GetValue(2);
                    result.PagesTotal = (int)dataReader.GetValue(3);
                    result.SectionPrefix = (string)dataReader.GetValue(4);
                    result.PdfPath = (string)dataReader.GetValue(5);
                    result.Sort = (int)dataReader.GetValue(6);
                    // result.PageNumber = (int)dataReader.GetValue(3);
                }
            }

            return result;
        }
/*
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
*/
    }

}
