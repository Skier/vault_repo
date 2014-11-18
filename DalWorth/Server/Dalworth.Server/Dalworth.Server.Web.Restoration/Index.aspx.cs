using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Dalworth.Server.Web.Common;
using Dalworth.Server.Domain;


namespace Dalworth.Server.Web.Restoration
{
    public partial class Index : DalworthPage
    {
        protected WebSiteArticle m_article;

        #region properties 

        protected override ProjectTypeEnum ProjectType
        {
            get { return ProjectTypeEnum.Deflood; }
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

            WebSiteArticle insuranceArticle = WebSiteArticle.FindByPrimaryKey(11, DbConnection);
            m_lnkInsurance.Attributes.Add("href", GetFullUrl(insuranceArticle.Url));

            WebSiteArticle privacyArticle = WebSiteArticle.FindByPrimaryKey(22, DbConnection);
            m_lnkPrivacyPolicy.Attributes.Add("href", GetFullUrl(privacyArticle.Url));

            BindingList<WebSiteArticlePartWrapper> news = WebSiteArticle.GetWebSiteArticlePartWrapperWithBreadCrumTitle(10, 3, DbConnection);
            if (news.Count == 0)
            {
                m_latestNews.Visible = false;
            }
            else
            {
                m_latestNews.ListName = "Latest News";
                m_latestNews.DataSource = WebSiteArticle.GetWebSiteArticlePartWrapperWithBreadCrumTitle(10, 3, DbConnection);
            }

            m_latestArticles.ListName = "Latest Articles";
            m_latestArticles.DataSource = WebSiteArticle.GetWebSiteArticlePartWrapperWithBreadCrumTitle(null, 3, DbConnection);

            m_article = WebSiteArticle.FindByPrimaryKey(ArticleId, DbConnection);

            m_header.SelectedMenu = m_article.MenuId;
            m_header.MenuItems = GetHeaderMenuItems(DbConnection, m_article.MenuId);

            List<WebSiteArticlePart> parts = WebSiteArticlePart.FindByArticleId(m_article.ID, DbConnection);

            foreach (WebSiteArticlePart part in parts)
            {
                switch (part.WebSiteArticlePartTypeId)
                {
                    case 1:
                        m_head.Title = part.ContentText;
                        break;
                    case 2:
                        m_head.Description = ReplacePhoneNumber(part.ContentText);
                        break;
                    case 4:
                        m_head.Keywords = part.ContentText;
                        break;
                    case 7:
                        m_txtServices.Text = ReplaceUrl(part.ContentText);
                        break;
                    case 8:
                        m_txtArticle.Text = ReplaceTestimonials(ReplaceUrl(part.ContentText));
                        break;
                }
            }

            List<WebSiteArticlePart> newsFlashParts = WebSiteArticlePart.FindByArticleId(36, DbConnection);
            if (newsFlashParts.Count > 0 && newsFlashParts[0].ContentText != null && newsFlashParts[0].ContentText.Trim().Length > 0)
            {
                m_newsFlash.Text = newsFlashParts[0].ContentText;
            }
            else
            {
                m_newsFlash.Visible = false;
            }
            List<WebSiteArticlePart> footerParts = WebSiteArticlePart.FindByArticleId(8, DbConnection);
            m_footer.Text = this.ReplaceUrl(footerParts[0].ContentText);
        }

        private string ReplaceTestimonials(string text)
        {
            if (!text.Contains("{CUSTOMER_TESTIMONIAL}"))
                return text;

            BindingList<ProjectFeedbackWrapper> wrappers = ProjectFeedbackWrapper.FindApprovedFeedbacks(ProjectTypeEnum.Deflood, 1, DbConnection);
            if (wrappers.Count == 0)
                return text;

            string result = text.Replace("{CUSTOMER_TESTIMONIAL}", 
                "<i>" + wrappers[0].CustomerNoteEdited + "</i><br/> <strong>" +
                wrappers[0].DatePosted + " " +wrappers[0].FirstLastName + ", " + 
                wrappers[0].City + " " + wrappers[0].State + " " + wrappers[0].Zip + "</strong>");
            return result;
        }
    }
}
