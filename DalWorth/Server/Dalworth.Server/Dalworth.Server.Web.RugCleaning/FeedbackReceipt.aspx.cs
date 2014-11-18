using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server;
using Dalworth.Server.Domain;
using Dalworth.Server.Web.Common;

namespace Dalworth.Server.Web.RugCleaning
{
    public partial class FeedbackReceipt : RugCleaningPage
    {
        private ProjectFeedback m_projectFeedBack;
        private Customer m_customer;
        private ProjectFeedbackRate m_rate;

        protected string FirstName
        {
            get { return Utils.FormatName(m_customer.FirstName, string.Empty); }
        }

        protected string CustomerNotes
        {
            get { return m_projectFeedBack.CustomerNote; }
        }

        protected string RemiderPeriod
        {
            
            get
            {
                string result = string.Empty;

                switch (m_projectFeedBack.CallbackPeriod)
                {
                    case ProjectFeedback.CallbackPeriodEnum.DoNotRemind:
                        result = "Please do not call";
                        break;
                    case ProjectFeedback.CallbackPeriodEnum.NotSelected:
                        result = "Not selected";
                        break;
                    case ProjectFeedback.CallbackPeriodEnum.SixMonth:
                        result = "6 month";
                        break;
                    case ProjectFeedback.CallbackPeriodEnum.OneYear:
                        result = "1 year";
                        break;
                    case ProjectFeedback.CallbackPeriodEnum.OneYearAndHalf:
                        result = "1.5 years";
                        break;
                    case ProjectFeedback.CallbackPeriodEnum.TwoYears:
                        result = "2 years";
                        break;
                }

                return result;
            }
        }

        protected string Rating
        {
            get
            {
                return m_rate.Rate;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Master.IsSlideShowRequired = false;
            Master.SlideshowType = RugCleaningMasterPage.SlideshowTypeEnum.Home;
            Master.IsBookmarkingRequired =false;
            Master.IsVideoRequired = false;
            Master.IsShortFormRequired = false;

            string key = Request.Params.Get("id");
            
            m_projectFeedBack = ProjectFeedback.FindByPrimaryKey(int.Parse(key));

            Project project = Project.FindByPrimaryKey(m_projectFeedBack.ProjectId);

            m_customer = Customer.FindByPrimaryKey(project.CustomerId.Value);

            m_rate = ProjectFeedbackRate.FindByPrimaryKey(m_projectFeedBack.RateId);

            m_divShowReviews.Visible = false;
            m_divSaySorry.Visible = false;

            if (m_projectFeedBack.RateId == 1)
            {
                m_divShowReviews.Visible = true;
            }

            if (m_projectFeedBack.RateId > 2)
            {
                m_divSaySorry.Visible = true;
            }
        }
    }
}
