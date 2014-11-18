using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.ComponentModel;

using Dalworth.Server.Web.Common;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.Restoration
{
    public partial class LandingPage : DalworthPage
    {
        #region properties

        private ProjectTypeEnum m_projectType;
        protected override ProjectTypeEnum ProjectType
        {
            get { return m_projectType; }
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

        protected bool m_isWhatToDoVisible = false;
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            m_divArticle.Attributes.Add("class", "article");
           
            m_btn_Submit.ServerClick += OnLeadSubmitted;

            WebSiteArticle article = WebSiteArticle.FindByPrimaryKey(ArticleId, DbConnection);
            List<WebSiteArticlePart> parts = WebSiteArticlePart.FindByArticleId(ArticleId, DbConnection);
            WebSiteArticleCategory category = WebSiteArticleCategory.FindByPrimaryKey(article.WebSiteArticleCategoryId, DbConnection);

            m_header.SelectedMenu = article.MenuId;
            m_header.MenuItems = GetHeaderMenuItems(DbConnection, article.MenuId);

            WebSiteArticle privacyArticle = WebSiteArticle.FindByPrimaryKey(22, DbConnection);
            m_lnkPrivacyPolicy.Attributes.Add("href", GetFullUrl(privacyArticle.Url));

            m_latestArticles.ListName = category.Name + " Articles";

            BindingList<WebSiteArticlePartWrapper> datasource = WebSiteArticle.GetWebSiteArticlePartWrapperWithBreadCrumTitle(category.ID, 3, DbConnection);
            foreach (WebSiteArticlePartWrapper wrapper in datasource)
            {
                wrapper.Url = GetFullUrl(wrapper.Url);
            }

            m_latestArticles.DataSource = datasource;

            Stack<WebSiteArticleCategory> categories = new Stack<WebSiteArticleCategory>();
            categories.Push(category);

            while (category.ParentWebSiteArticleCategoryId.HasValue)
            {
                category = WebSiteArticleCategory.FindByPrimaryKey(category.ParentWebSiteArticleCategoryId.Value, DbConnection);
                categories.Push(category);
            }

            string breadCrum = string.Empty;
            while (categories.Count > 0)
            {
                category = categories.Pop();

                if (category.LandingPageWebSiteArticleId == article.ID)
                {
                    breadCrum += "&nbsp;&raquo;&nbsp;";
                    breadCrum += category.Name;
                }
                else
                {
                    WebSiteArticle categoryLandingPageArticle = WebSiteArticle.FindByPrimaryKey(category.LandingPageWebSiteArticleId, DbConnection);

                    if (category.ParentWebSiteArticleCategoryId.HasValue)
                        breadCrum += "&nbsp;&raquo;&nbsp;";

                    breadCrum += "<a href=\"" + GetFullUrl(categoryLandingPageArticle.Url) + "\">" + category.Name + " </a>";
                }

            }

            if (article.WebSiteArticleTypeId == 2)
            {
                m_isWhatToDoVisible = true;

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
                        case 8:
                            m_txtWhatToDo.Text = part.ContentText;
                            break;
                        case 9:
                            m_txtWhatNotToDo.Text = part.ContentText;
                            break;
                        case 10:
                            m_txtArticle.Text = ReplaceUrl(part.ContentText);
                            break;
                        case 11:
                            m_txtServiceName.Text = part.ContentText;
                            break;
                        case 12:
                            if (category.LandingPageWebSiteArticleId != article.ID)
                            {
                                breadCrum += "&nbsp;&raquo;&nbsp;";
                                breadCrum += part.ContentText;
                            }
                            break;

                    }
                }
            }

            if ((article.WebSiteArticleTypeId == 3 || 
                article.WebSiteArticleTypeId == 4 || 
                article.WebSiteArticleTypeId == 5))
            {
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
                        case 3:
                            m_divArticle.Attributes.Add("class", part.ContentText);
                            break;
                        case 4:
                            m_head.Keywords = part.ContentText;
                            break;
                        case 8:
                            if (article.ID != 12)
                                m_txtArticle.Text = ReplaceUrl(part.ContentText);
                            else
                                SetProjectFeedbacks();   
                            break;
                        case 12:
                            if (category.LandingPageWebSiteArticleId != article.ID)
                            {
                                breadCrum += "&nbsp;&raquo;&nbsp;";
                                breadCrum += part.ContentText;
                            }
                            break;
                    }
                }
            }

            m_breadCrum.Text = breadCrum;

            List<WebSiteArticlePart> footerParts = WebSiteArticlePart.FindByArticleId(8, DbConnection);
            m_footer.Text = this.ReplaceUrl(footerParts[0].ContentText);

            if (article.WebSiteArticleTypeId == 5)
            {
                m_txtFormText1.Text = "Apply to become ";
                m_txtFormText2.Text = "business partner";
                m_txtCallPhone.Text = "1 877 552 8349";
                m_projectType = ProjectTypeEnum.NotSpecified;
            }
            else
            {
                m_txtFormText1.Text = "Submit Your information";
                m_txtFormText2.Text = "for immediate Response";
                m_txtCallPhone.Text = "1 800 326 7913";
                m_projectType = ProjectTypeEnum.Deflood;
            }
            
        }

        private void SetProjectFeedbacks()
        {
            m_divCustomerFeedback.Visible = true;
            m_divArticle.Visible = false;

            BindingList<ProjectFeedbackWrapper> projectFeedbacks = ProjectFeedbackWrapper.FindApprovedFeedbacks(ProjectTypeEnum.Deflood, DbConnection);
            m_repeater.DataSource = projectFeedbacks;
            m_repeater.DataBind();
        }
    }
}
