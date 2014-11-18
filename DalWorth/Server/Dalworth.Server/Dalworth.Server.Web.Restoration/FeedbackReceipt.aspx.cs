using System;
using System.Collections.Generic;

using Dalworth.Server.Web.Common;
using Dalworth.Server.Domain;


namespace Dalworth.Server.Web.Restoration
{
    public partial class FeedbackReceipt : DalworthPage
    {
       #region properties

        protected override WebSiteEnum WebSite
        {
            get { return WebSiteEnum.Restoration; }
        }

        protected override ProjectTypeEnum ProjectType
        {
            get { throw new NotImplementedException(); }
        }

        #endregion 

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            m_header.SelectedMenu = 0;
            m_header.MenuItems = GetHeaderMenuItems(DbConnection, 0);

            List<WebSiteArticlePart> footerParts = WebSiteArticlePart.FindByArticleId(8, DbConnection);
            m_footer.Text = this.ReplaceUrl(footerParts[0].ContentText);

            string key = Request.Params.Get("id");

            ProjectFeedback projectFeedBack = ProjectFeedback.FindByPrimaryKey(int.Parse(key));
            Project project = Project.FindByPrimaryKey(projectFeedBack.ProjectId, DbConnection);
            Customer customer = Customer.FindByPrimaryKey(project.CustomerId.Value, DbConnection);

            List<WebSiteArticlePart> parts = WebSiteArticlePart.FindByArticleId(1, DbConnection);
            foreach (WebSiteArticlePart part in parts)
            {
                if (part.WebSiteArticlePartTypeId == 7)
                {
                    m_txtServices.Text = ReplaceUrl(part.ContentText);
                    break;
                }
            }

            m_divNegativeFeedback.Visible = false;
            m_divPositiveFeedback.Visible = false;

            string serviceRate = "No Rated";
             
            switch (projectFeedBack.RateId)
            {
                case 0:
                    serviceRate = "No Rated";
                    break;
                case 1:
                    serviceRate = "Excellent";
                    break;
                case 2:
                    serviceRate= "Good";
                    break;
                case 3:
                    serviceRate = "Acceptable";
                    break;
                case 4: 
                    serviceRate = "Needs improvement";
                    break;
                case 5:
                    serviceRate = "Not satisfied";
                    break;
            }

            if (projectFeedBack.RateId <= 2)
            {
                m_divPositiveFeedback.Visible = true;

                if (projectFeedBack.RateId == 0)
                {
                    m_divReferalSites.Visible = false;
                }

                m_txtName.Text = customer.FirstName + " " + customer.LastName;
                m_txtComment.Text = projectFeedBack.CustomerNote;
                m_txtServiceRate.Text = serviceRate;
                return;
            }

            m_divNegativeFeedback.Visible = true;
            m_txtName1.Text = customer.FirstName + " " + customer.LastName;
            m_txtComment1.Text = projectFeedBack.CustomerNote;
            m_txtServiceRate1.Text = serviceRate;
            m_txtServiceRate2.Text = serviceRate;
        }
    }
}
