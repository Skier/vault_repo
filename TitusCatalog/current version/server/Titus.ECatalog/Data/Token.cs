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

    public class Token
    {

        private static Token c_Token = new Token();

        public static Token GetInstance()
        {
            return c_Token;
        }

        private Token()
        {
        }

        private const string SQL_SELECT_BY_PAGE_ID = @"
            select  [DocumentTokenId],
                    [DocumentPageId],
                    [Top],
                    [Left],
                    [Width],
                    [Height],
                    [Text]
            from    [DocumentToken]
            where   [DocumentPageId] = @DocumentPageId";

        public List<TokenDataObject> FindByPageId(SqlTransaction tran, int pageId)
        {
            List<TokenDataObject> result = new List<TokenDataObject>();

            DbParameter[] parms = new DbParameter[1] { new SqlParameter("@DocumentPageId", pageId) };

            using (IDataReader dataReader = SqlHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_PAGE_ID, parms))
            {
                while (dataReader.Read())
                {
                    TokenDataObject tokenInfo = new TokenDataObject();

                    tokenInfo.DocumentTokenId = (int)dataReader.GetValue(0);
                    tokenInfo.DocumentPageId = (int)dataReader.GetValue(1);
                    tokenInfo.Top = (int)dataReader.GetValue(2);
                    tokenInfo.Left = (int)dataReader.GetValue(3);
                    tokenInfo.Width = (int)dataReader.GetValue(4);
                    tokenInfo.Height = (int)dataReader.GetValue(5);
                    if (dataReader.IsDBNull(6))
                    {
                        tokenInfo.Text = null;
                    }
                    else
                    {
                        tokenInfo.Text = (string)dataReader.GetValue(6);
                    }

                    result.Add(tokenInfo);
                }
            }

            return result;
        }

        private const string SQL_INSERT = @"
        insert  into [DocumentToken]
              ( [DocumentPageId],
                [Top],
                [Left],
                [Width],
                [Height],
                [Text])
        values( @DocumentPageId,
                @Top,
                @Left,
                @Width,
                @Height,
                @Text);
        select  cast(scope_identity() as int)";

        public void Insert(SqlTransaction tran, TokenDataObject tokenInfo)
        {
            DbParameter[] parms = new DbParameter[6] {
                new SqlParameter("@DocumentPageId", tokenInfo.DocumentPageId),
                new SqlParameter("@Top", tokenInfo.Top),
                new SqlParameter("@Left", tokenInfo.Left),
                new SqlParameter("@Width", tokenInfo.Width),
                new SqlParameter("@Height", tokenInfo.Height),
                new SqlParameter("@Text", (null == tokenInfo.Text)? DBNull.Value: (object)tokenInfo.Text),
            };

            tokenInfo.DocumentTokenId = (int)SqlHelper.ExecuteScalar(tran, CommandType.Text, SQL_INSERT, parms);
        }

    }

}
