using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.ComponentModel;

using Dalworth.Server.Web.Common;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.Restoration
{
    public partial class SiteMapPage : DalworthPage
    {
        #region properties

        protected override ProjectTypeEnum ProjectType
        {
            get { throw new NotImplementedException(); }
        }

        protected override WebSiteEnum WebSite
        {
            get { return WebSiteEnum.Restoration; }
        }

        protected override string FirstName
        {
            get { return m_txtFirstName.Value; }
        }

        protected override string LastName
        {
            get { return m_txtLastName.Value; }
        }

        protected override string Phone1
        {
            get { return m_txtPhone1.Value; }
        }

        protected override string Email
        {
            get { return m_txtEmail.Value; }
        }

        protected override string CustomerNotes
        {
            get { return m_txtCustomerNotes.Value; }
        }

        protected override Label ErrorMessage
        {
            get { return m_lblErrorMessage; }
        }

        protected override Label ErrorFirstName
        {
            get { return m_lblErrorFirstName; }
        }

        protected override Label ErrorLastName
        {
            get { return m_lblErrorLastName; }
        }

        protected override Label ErrorPhone1
        {
            get { return m_lblErrorPhone1; }
        }

        protected override Label ErrorEmail
        {
            get { return m_lblErrorEmail; }
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.m_btn_Submit.ServerClick += OnLeadSubmitted;

            string supportURL = GetFullUrl("img/support.gif");

            string supportAlt = "Contact Dalworth Restoration Now!";

            m_img_support.Attributes.Add("src", supportURL);
            m_img_support.Attributes.Add("alt", supportAlt);

            
            m_latestArticles.ListName = "Latest Articles";

            
            m_header.MenuItems = GetHeaderMenuItems(DbConnection, 0);

            BindingList<WebSiteArticlePartWrapper> datasource = WebSiteArticle.GetWebSiteArticlePartWrapperWithBreadCrumTitle(null, 3, DbConnection);
            foreach (WebSiteArticlePartWrapper wrapper in datasource)
            {
                wrapper.Url = GetFullUrl(wrapper.Url);
            }

            m_latestArticles.DataSource = datasource;

            m_head.Title = "Dalworth Restoration Site Map";
            m_head.Description = "Site map of the dalworth restoration";
            m_head.Keywords = string.Empty;


            m_breadCrum.Text = string.Empty;

            List<WebSiteArticlePart> footerParts = WebSiteArticlePart.FindByArticleId(8, DbConnection);
            m_footer.Text = this.ReplaceUrl(footerParts[0].ContentText);

            WebSiteArticleCategory homeCategory = WebSiteArticleCategory.FindByPrimaryKey(1, DbConnection);

            homeCategory.LoadAllChildren(DbConnection);

            m_txtSiteMap.Text = "<ul><li>" + ConvertCategoryToUL(homeCategory) + "</li></ul>";
        }

        private string ConvertCategoryToUL(WebSiteArticleCategory category)
        {
            string result = "<h3>" + category.Name + "</h3>";
            result += "<ul>";
            foreach (WebSiteArticlePartWrapper articleWrapper in category.WebSiteArticles)
            {
                result += "<li><a href=\""+ GetFullUrl(articleWrapper.Url) + "\">"+ articleWrapper.Title + "</a></li>";
            }

            foreach (WebSiteArticleCategory childCategory in category.ChildWebSiteArticleCategories)
            {
                result += "<li>" + ConvertCategoryToUL(childCategory) + "</li>";
            }
            result += "</ul>";
            return result;
        }
    }
}
