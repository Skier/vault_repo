using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Text;

using Dalworth.Server.Web.Common;
using Dalworth.Server.Domain;
using Dalworth.Server.Data;


namespace Dalworth.Server.Web.Restoration
{
    public partial class SiteMapXml : DalworthPage
    {
        protected override WebSiteEnum WebSite
        {
            get { return WebSiteEnum.Restoration; }
        }

        protected override ProjectTypeEnum ProjectType
        {
            get { throw new NotImplementedException(); }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Response.ContentType = "text/xml";

            WebSiteArticleCategory homeCategory = WebSiteArticleCategory.FindByPrimaryKey(1, DbConnection);
            homeCategory.LoadAllChildren(DbConnection);

            BindingList<WebSiteArticlePartWrapper>  dataSource = new BindingList<WebSiteArticlePartWrapper>();
            PrepareDataset(dataSource, homeCategory);
            
            string xmlTemplate =
            @"
              <url>
                <loc>{0}</loc>
                <priority>{1}</priority>
                <changefreq>weekly</changefreq>
                <lastmod>{2}</lastmod>
             </url>
             ";

            StringBuilder xml = new StringBuilder();
            WebSiteArticleCategory category = null;

            foreach (WebSiteArticlePartWrapper wrapper in dataSource)
            {
                xml.Append(string.Format(xmlTemplate, GetFullUrl(wrapper.Url), wrapper.Priority, String.Format("{0:yyyy-MM-dd}", wrapper.Article.DatePublished)));
            }

            m_txtText.Text = xml.ToString();
        }

        private void PrepareDataset(BindingList<WebSiteArticlePartWrapper> dataSource, WebSiteArticleCategory category)
        {

            BindingList<ProjectFeedbackWrapper> feedbacks = ProjectFeedbackWrapper.FindApprovedFeedbacks(ProjectTypeEnum.Deflood, 1, DbConnection);

            foreach (WebSiteArticlePartWrapper article in category.WebSiteArticles)
            {
                if (article.Article.ID == category.LandingPageWebSiteArticleId)
                    article.Priority = 0.9;
                else
                    article.Priority = 0.5;

                if (feedbacks.Count > 0 && (article.Article.ID == 1 || article.Article.ID == 12))
                {
                    article.Article.DatePublished = feedbacks[0].ProjectFeedback.DateCreated;
                }

                dataSource.Add(article);
            }

            foreach (WebSiteArticleCategory childCategory in category.ChildWebSiteArticleCategories)
            {
                PrepareDataset(dataSource, childCategory);
            }
        }
        
    }
}
