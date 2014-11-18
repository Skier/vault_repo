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

    public class Page
    {

        private static Page c_Page = new Page();

        public static Page GetInstance()
        {
            return c_Page;
        }

        private Page()
        {
        }

        private const string SQL_SELECT_BY_DOCUMENT_ID = @"
            select  [DocumentPageId],
                    [DocumentId],
                    [PageNumber],
                    [ScreenshotURL],
                    [Width],
                    [Height]
            from    [DocumentPage]
            where   ([DocumentId] = @DocumentId
                    or @DocumentId = 0)
            order   by [PageNumber]";

        public List<PageDataObject> FindByDocumentId(SqlTransaction tran, int documentId)
        {
            List<PageDataObject> result = new List<PageDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@DocumentId", documentId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_DOCUMENT_ID, parms))
            {
                while (dataReader.Read())
                {
                    PageDataObject pageInfo = new PageDataObject();

                    pageInfo.DocumentPageId = (int)dataReader.GetValue(0);
                    pageInfo.DocumentId = (int)dataReader.GetValue(1);
                    pageInfo.PageNumber = (int)dataReader.GetValue(2);
                    pageInfo.ScreenshotURL = (string)dataReader.GetValue(3);
                    pageInfo.Width = (int)dataReader.GetValue(4);
                    pageInfo.Height = (int)dataReader.GetValue(5);

                    result.Add(pageInfo);
                }
            }

            return result;
        }

        public List<PageDataObject> FindPages(SqlTransaction tran)
        {
            return FindByDocumentId(tran, 0);
        }

        private const string SQL_SELECT_BY_MODEL_ID = @"
            select  dp.[DocumentPageId],
                    dp.[DocumentId],
                    dp.[PageNumber],
                    dp.[ScreenshotURL],
                    dp.[Width],
                    dp.[Height]
            from    [DocumentPage] dp
                    inner join [DocumentPageModel] dpm
                            on dpm.[DocumentPageId] = dp.[DocumentPageId]
            where   dpm.[ModelId] = @ModelId";

        public PageDataObject FindByModelId(SqlTransaction tran, int modelId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@ModelId", modelId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_MODEL_ID, parms))
            {
                if (dataReader.Read())
                {
                    PageDataObject pageInfo = new PageDataObject();

                    pageInfo.DocumentPageId = (int)dataReader.GetValue(0);
                    pageInfo.DocumentId = (int)dataReader.GetValue(1);
                    pageInfo.PageNumber = (int)dataReader.GetValue(2);
                    pageInfo.ScreenshotURL = (string)dataReader.GetValue(3);
                    pageInfo.Width = (int)dataReader.GetValue(4);
                    pageInfo.Height = (int)dataReader.GetValue(5);

                    return pageInfo;
                }
            }

            return null;
        }

        private const string SQL_SELECT_BY_MODEL_ITEM_ID = @"
            select  dp.[DocumentPageId],
                    dp.[DocumentId],
                    dp.[PageNumber],
                    dp.[ScreenshotURL],
                    dp.[Width],
                    dp.[Height]
            from    [DocumentPage] dp
                    inner join [DocumentPageModelItem] dpmi
                            on dpmi.[DocumentPageId] = dp.[DocumentPageId]
            where   dpmi.[ModelItemId] = @ModelItemId";

        public PageDataObject FindByModelItemId(SqlTransaction tran, int modelItemId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@ModelItemId", modelItemId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_MODEL_ITEM_ID, parms))
            {
                if (dataReader.Read())
                {
                    PageDataObject pageInfo = new PageDataObject();

                    pageInfo.DocumentPageId = (int)dataReader.GetValue(0);
                    pageInfo.DocumentId = (int)dataReader.GetValue(1);
                    pageInfo.PageNumber = (int)dataReader.GetValue(2);
                    pageInfo.ScreenshotURL = (string)dataReader.GetValue(3);
                    pageInfo.Width = (int)dataReader.GetValue(4);
                    pageInfo.Height = (int)dataReader.GetValue(5);

                    return pageInfo;
                }
            }

            return null;
        }

        private const string SQL_SELECT_BY_CODE = @"
            select  dp.[DocumentPageId],
                    dp.[DocumentId],
                    dp.[PageNumber],
                    dp.[ScreenshotURL],
                    dp.[Width],
                    dp.[Height]
            from    [DocumentPage] dp
                    inner join [Document] d
                            on d.[DocumentId] = dp.[DocumentId]
            where   d.[Code] = @Code
            and     dp.[PageNumber] = @PageNumber";

        public PageDataObject FindByCode(SqlTransaction tran, string code, int pageNumber)
        {
            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@Code", code),
                new SqlParameter("@PageNumber", pageNumber)
            };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_CODE, parms))
            {
                if (dataReader.Read())
                {
                    PageDataObject pageInfo = new PageDataObject();

                    pageInfo.DocumentPageId = (int)dataReader.GetValue(0);
                    pageInfo.DocumentId = (int)dataReader.GetValue(1);
                    pageInfo.PageNumber = (int)dataReader.GetValue(2);
                    pageInfo.ScreenshotURL = (string)dataReader.GetValue(3);
                    pageInfo.Width = (int)dataReader.GetValue(4);
                    pageInfo.Height = (int)dataReader.GetValue(5);

                    return pageInfo;
                }
            }

            return null;
        }

        private const string SQL_SELECT_PAGES_COUNT = @"
            select  count(*)
            from    [DocumentPage]";

        public int GetPagesCount(SqlTransaction tran)
        {
            using (IDataReader dataReader = SqlHelper.ExecuteReader(
                    tran, CommandType.Text, SQL_SELECT_PAGES_COUNT, new DbParameter[0] { }))
            {
                if (dataReader.Read())
                {
                    PageDataObject pageInfo = new PageDataObject();

                    return (int)dataReader.GetValue(0);
                }
                else
                {
                    return 0;
                }
            }
        }

        public List<PageDataObject> FindPages(SqlTransaction tran)
        {
            return FindByDocumentId(tran, 0);
        }

        private const string SQL_SELECT_BY_MODEL_ID = @"
            select  dp.[DocumentPageId],
                    dp.[DocumentId],
                    dp.[PageNumber],
                    dp.[ScreenshotURL],
                    dp.[Width],
                    dp.[Height]
            from    [DocumentPage] dp
                    inner join [DocumentPageModel] dpm
                            on dpm.[DocumentPageId] = dp.[DocumentPageId]
            where   dpm.[ModelId] = @ModelId";

        public PageDataObject FindByModelId(SqlTransaction tran, int modelId)
        {
            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@ModelId", modelId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_MODEL_ID, parms))
            {
                if (dataReader.Read())
                {
                    PageDataObject pageInfo = new PageDataObject();

                    pageInfo.DocumentPageId = (int)dataReader.GetValue(0);
                    pageInfo.DocumentId = (int)dataReader.GetValue(1);
                    pageInfo.PageNumber = (int)dataReader.GetValue(2);
                    pageInfo.ScreenshotURL = (string)dataReader.GetValue(3);
                    pageInfo.Width = (int)dataReader.GetValue(4);
                    pageInfo.Height = (int)dataReader.GetValue(5);

                    return pageInfo;
                }
            }

            return null;
        }

        private const string SQL_SELECT_PAGES_COUNT = @"
            select  count(*)
            from    [DocumentPage]";

        public int GetPagesCount(SqlTransaction tran)
        {
            using (IDataReader dataReader = SqlHelper.ExecuteReader(
                    tran, CommandType.Text, SQL_SELECT_PAGES_COUNT, new DbParameter[0] { }))
            {
                if (dataReader.Read())
                {
                    PageDataObject pageInfo = new PageDataObject();

                    return (int)dataReader.GetValue(0);
                }
                else
                {
                    return 0;
                }
            }
        }

        private const string SQL_INSERT = @"
        insert  into [DocumentPage]
              ( [DocumentId],
                [PageNumber],
                [ScreenshotURL],
                [Width],
                [Height] )
        values( @DocumentId,
                @PageNumber,
                @ScreenshotURL,
                @Width,
                @Height );
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, PageDataObject pageInfo)
        {
            DbParameter[] parms = new DbParameter[5] {
                new SqlParameter("@DocumentId", pageInfo.DocumentId),
                new SqlParameter("@PageNumber", pageInfo.PageNumber),
                new SqlParameter("@ScreenshotURL", pageInfo.ScreenshotURL),
                new SqlParameter("@Width", pageInfo.Width),
                new SqlParameter("@Height", pageInfo.Height)
            };

            pageInfo.DocumentPageId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_UPDATE = @"
        update  [DocumentPage]
        set     [ScreenshotURL] = @ScreenshotURL";

        public void Update(SqlTransaction tran, PageDataObject pageInfo)
        {
            DbParameter[] parms = new DbParameter[1] {
                new SqlParameter("@ScreenshotURL", pageInfo.ScreenshotURL)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

    }

}
