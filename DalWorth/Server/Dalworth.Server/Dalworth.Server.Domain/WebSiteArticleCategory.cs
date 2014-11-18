using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  
namespace Dalworth.Server.Domain
{
    public partial class WebSiteArticleCategory
    {
        public WebSiteArticleCategory()
        {

        }

        private BindingList<WebSiteArticlePartWrapper> m_articles = new BindingList<WebSiteArticlePartWrapper>();
        public BindingList<WebSiteArticlePartWrapper> WebSiteArticles
        {
            get { return m_articles; }
        }

        private List<WebSiteArticleCategory> m_childCategories = new List<WebSiteArticleCategory>();
        public List<WebSiteArticleCategory> ChildWebSiteArticleCategories
        {
            get { return m_childCategories; }
        }

        public void LoadAllChildren(IDbConnection connection)
        {
            m_articles = WebSiteArticle.GetWebSiteArticlePartWrapperWithBreadCrumTitle(ID, null, connection);
            m_childCategories = WebSiteArticleCategory.FindByParentId(connection, ID);
            foreach (WebSiteArticleCategory category in m_childCategories)
            {
                category.LoadAllChildren(connection);
            }
        }

        #region FindByParentId

        public static List<WebSiteArticleCategory> FindByParentId(IDbConnection connection, int parentId)
        {
            string sql = SqlSelectAll + " where ParentWebSiteArticleCategoryId=?ParentId";

            using (IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
            {
                Database.PutParameter(dbCommand, "?ParentId", parentId);

                List<WebSiteArticleCategory> rv = new List<WebSiteArticleCategory>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        rv.Add(Load(dataReader));
                    }

                }

                return rv;
            }

        }

        #endregion
    }
}
      