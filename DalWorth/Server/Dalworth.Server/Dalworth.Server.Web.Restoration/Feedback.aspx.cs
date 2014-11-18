using System;
using System.Collections.Generic;

using Dalworth.Server.Web.Common;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.Restoration
{
    public partial class Feedback : DalworthPage
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

        #endregion 

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            m_btnSubmit.ServerClick += OnBtnSubmitClick;

            m_header.SelectedMenu = 0;
            m_header.MenuItems = GetHeaderMenuItems(DbConnection, 0);

            List<WebSiteArticlePart> footerParts = WebSiteArticlePart.FindByArticleId(8, DbConnection);
            m_footer.Text = this.ReplaceUrl(footerParts[0].ContentText);
            List<WebSiteArticlePart> parts = WebSiteArticlePart.FindByArticleId(1, DbConnection);

            foreach (WebSiteArticlePart part in parts)
            {
                if (part.WebSiteArticlePartTypeId == 7)
                {
                    m_txtServices.Text = ReplaceUrl(part.ContentText);
                    break;
                }
            }

            string key = Request.Params.Get("id");
            int projectId;

            if (!int.TryParse(key, out projectId))
            {
                Response.Redirect("~/ErrorPage.aspx");
                return;
            }

            Project project = Project.FindByPrimaryKey(projectId);

            if (project.ProjectType != ProjectTypeEnum.Deflood && project.ProjectStatus != ProjectStatusEnum.Completed)
            {
                Response.Redirect("~/ErrorPage.aspx");
                return;
            }

            DateTime completionDate = Project.GetCompletionDate(project);
            
            if (completionDate == DateTime.MinValue)
            {
                Response.Redirect("~/ErrorPage.aspx");
                return;
            }

            m_txtCompletionDate.Text = String.Format("{0:M/d/yyyy}", completionDate);

            Customer customer = Customer.FindByPrimaryKey(project.CustomerId.Value);

            m_txtFirstName.Text = customer.FirstName;
            m_txtLastName.Text = customer.LastName;
        }

        protected void OnBtnSubmitClick(object sender, EventArgs e)
        {
            string key = Request.Params.Get("id");
            int projectId;

            if (!int.TryParse(key, out projectId))
            {
                Response.Redirect("~/ErrorPage.aspx");
                return;
            }

            ProjectFeedback feedback = new ProjectFeedback();

            ProjectFeedback.CallbackPeriodEnum callbackPeriod = ProjectFeedback.CallbackPeriodEnum.NotSelected;

            try
            {
                feedback.Submit(projectId,
                    int.Parse(m_selServiceRate.Value), m_txtComment.Value, callbackPeriod);
            }

            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx");
                return;
            }

            Response.Redirect("~/FeedbackReceipt.aspx?id=" + feedback.ID);

        }
    }
}
