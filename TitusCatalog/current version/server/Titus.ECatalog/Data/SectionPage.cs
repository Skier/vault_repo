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

    public class SectionPage
    {

        private static SectionPage c_SectionPage = new SectionPage();

        public static SectionPage GetInstance()
        {
            return c_SectionPage;
        }

        private SectionPage()
        {
        }

        private const string SQL_SELECT_BY_SECTION_ID = @"
            select  [SectionPageId],
                    [SectionId],
                    [SectionPageNumber],
                    [ScreenshotURL],
                    [Width],
                    [Height]
            from    [EC_SectionPage]
            where   ([SectionId] = @SectionId
                    or @SectionId = 0)
            order   by [SectionPageNumber]";

        public List<SectionPageDataObject> FindBySectionId(SqlTransaction tran, int sectionId)
        {
            List<SectionPageDataObject> result = new List<SectionPageDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@SectionId", sectionId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_SECTION_ID, parms))
            {
                while (dataReader.Read())
                {
                    SectionPageDataObject pageInfo = new SectionPageDataObject();

                    pageInfo.SectionPageId = (int)dataReader.GetValue(0);
                    pageInfo.SectionId = (int)dataReader.GetValue(1);
                    pageInfo.SectionPageNumber = (int)dataReader.GetValue(2);
                    pageInfo.ScreenshotURL = (string)dataReader.GetValue(3);
                    pageInfo.Width = (int)dataReader.GetValue(4);
                    pageInfo.Height = (int)dataReader.GetValue(5);

                    result.Add(pageInfo);
                }
            }

            return result;
        }

        public List<SectionPageDataObject> FindPages(SqlTransaction tran)
        {
            return FindBySectionId(tran, 0);
        }

        private const string SQL_SELECT_BY_MODEL_AND_LEVEL = @"
            select  sp.[SectionPageId],
                    sp.[SectionId],
                    sp.[SectionPageNumber],
                    sp.[ScreenshotURL],
                    sp.[Width],
                    sp.[Height]
            from    [EC_SectionPage] sp
                    inner join [EC_SectionItem] ci
                            on ci.[SectionPageId] = sp.[SectionPageId]
            where   ci.[ModelId] = @ModelId
            and     ci.[CatalogLevel] = @CatalogLevel";

        public SectionPageDataObject FindByModelAndLevel(SqlTransaction tran, int modelId, int catalogLevel)
        {
            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@ModelId", modelId),
                new SqlParameter("CatalogLevel", catalogLevel)
            };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_MODEL_AND_LEVEL, parms))
            {
                if (dataReader.Read())
                {
                    SectionPageDataObject pageInfo = new SectionPageDataObject();

                    pageInfo.SectionPageId = (int)dataReader.GetValue(0);
                    pageInfo.SectionId = (int)dataReader.GetValue(1);
                    pageInfo.SectionPageNumber = (int)dataReader.GetValue(2);
                    pageInfo.ScreenshotURL = (string)dataReader.GetValue(3);
                    pageInfo.Width = (int)dataReader.GetValue(4);
                    pageInfo.Height = (int)dataReader.GetValue(5);

                    return pageInfo;
                }
            }

            return null;
        }

        /* private const string SQL_SELECT_BY_MODEL_ITEM_ID = @"
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
                    SectionPageDataObject pageInfo = new SectionPageDataObject();

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
        } */

        private const string SQL_SELECT_BY_CODE = @"
            select  sp.[SectionPageId],
                    sp.[SectionId],
                    sp.[SectionPageNumber],
                    sp.[ScreenshotURL],
                    sp.[Width],
                    sp.[Height]
            from    [EC_SectionPage] sp
                    inner join [EC_Section] s
                            on s.[SectionId] = sp.[SectionId]
            where   s.[SectionPrefix] = @SectionPrefix
            and     sp.[SectionPageNumber] = @PageNumber";

        public SectionPageDataObject FindByCode(SqlTransaction tran, string code, int pageNumber)
        {
            DbParameter[] parms = new DbParameter[2] {
                new SqlParameter("@SectionPrefix", code),
                new SqlParameter("@PageNumber", pageNumber)
            };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_CODE, parms))
            {
                if (dataReader.Read())
                {
                    SectionPageDataObject pageInfo = new SectionPageDataObject();

                    pageInfo.SectionPageId = (int)dataReader.GetValue(0);
                    pageInfo.SectionId = (int)dataReader.GetValue(1);
                    pageInfo.SectionPageNumber = (int)dataReader.GetValue(2);
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
            from    [EC_DocumentPage]";

        public int GetPagesCount(SqlTransaction tran)
        {
            using (IDataReader dataReader = SqlHelper.ExecuteReader(
                    tran, CommandType.Text, SQL_SELECT_PAGES_COUNT, new DbParameter[0] { }))
            {
                if (dataReader.Read())
                {
                    return (int)dataReader.GetValue(0);
                }
                else
                {
                    return 0;
                }
            }
        }

        private const string SQL_INSERT = @"
        insert  into [EC_DocumentPage]
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

        public void Insert(SqlTransaction tran, SectionPageDataObject pageInfo)
        {
            DbParameter[] parms = new DbParameter[5] {
                new SqlParameter("@DocumentId", pageInfo.SectionId),
                new SqlParameter("@PageNumber", pageInfo.SectionPageNumber),
                new SqlParameter("@ScreenshotURL", pageInfo.ScreenshotURL),
                new SqlParameter("@Width", pageInfo.Width),
                new SqlParameter("@Height", pageInfo.Height)
            };

            pageInfo.SectionPageId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

        private const string SQL_UPDATE = @"
        update  [EC_DocumentPage]
        set     [ScreenshotURL] = @ScreenshotURL";

        public void Update(SqlTransaction tran, SectionPageDataObject pageInfo)
        {
            DbParameter[] parms = new DbParameter[1] {
                new SqlParameter("@ScreenshotURL", pageInfo.ScreenshotURL)
            };

            SqlHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, parms);
        }

    }

}
