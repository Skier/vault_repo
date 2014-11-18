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
using System.Collections.Generic;
using Dalworth.Server;
using Dalworth.Server.Domain;
using Dalworth.Server.Web.Common;

namespace Dalworth.Server.Web.RugCleaning
{
    public partial class Feedback : RugCleaningPage
    {
        private Customer m_customer;
        private Project  m_project;
        private DateTime m_completionDate;
        private int m_rugCount;

        protected string FirstName
        {
            get { return Utils.FormatName(m_customer.FirstName, string.Empty); }
        }

        protected string LastName
        {
            get { return Utils.FormatName(m_customer.LastName, string.Empty); }
        }

        protected string DateCompleted
        {
            get { return String.Format("{0:d/M/yyyy}", m_completionDate);}
        }

        protected string RugCount
        {
            get { return m_rugCount.ToString(); }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            btnSubmit.ServerClick += OnBtnSubmitClick;

            Master.IsSlideShowRequired = false;
            Master.SlideshowType = RugCleaningMasterPage.SlideshowTypeEnum.Home;
            Master.IsBookmarkingRequired = false;
            Master.IsVideoRequired = false;
            Master.IsShortFormRequired = false;
            Master.IsLinksVisible = false;

            string key = Request.Params.Get("id");
            int projectId;

            if (!int.TryParse(key, out projectId))
            {
                Response.Redirect("~/Error.aspx");
                return;
            }

            m_project  = Project.FindByPrimaryKey(projectId);
            if (m_project.ProjectType != ProjectTypeEnum.RugCleaning && m_project.ProjectStatus != ProjectStatusEnum.Completed)
            {
                Response.Redirect("~/Error.aspx");
                return;
            }

            m_completionDate = Project.GetCompletionDate(m_project);
            if (m_completionDate == DateTime.MinValue)
            {
                Response.Redirect("~/Error.aspx");
                return;
            }

            m_customer = Customer.FindByPrimaryKey(m_project.CustomerId.Value);

            List<Task> tasks = Task.FindByProject(m_project);

            Task delivery = null;

            foreach (Task task in tasks)
            {
                if (task.TaskType == TaskTypeEnum.RugDelivery)
                    delivery = task;
            }

            if (delivery == null)
            {
                Response.Redirect("~/Error.aspx");
                return;
            }

            List<Item> taskItems = Item.FindByTask(delivery);

            if (taskItems.Count == 0)
            {
                Response.Redirect("~/Error.aspx");
                return;
            }

            m_rugCount = taskItems.Count;
        }

        protected void OnBtnSubmitClick(object sender, EventArgs e)
        {
            ProjectFeedback feedback = new ProjectFeedback();

            int remindPeriod = int.Parse(m_selRemindPeriod.Value);

            ProjectFeedback.CallbackPeriodEnum callbackPeriod = ProjectFeedback.CallbackPeriodEnum.NotSelected;

            switch (remindPeriod)
            {
                case 0:
                    callbackPeriod = ProjectFeedback.CallbackPeriodEnum.NotSelected;
                    break;
                case 1:
                    callbackPeriod = ProjectFeedback.CallbackPeriodEnum.SixMonth;
                    break;
                case 2:
                    callbackPeriod = ProjectFeedback.CallbackPeriodEnum.OneYear;
                    break;
                case 3:
                    callbackPeriod = ProjectFeedback.CallbackPeriodEnum.OneYearAndHalf;
                    break;
                case 4:
                    callbackPeriod = ProjectFeedback.CallbackPeriodEnum.TwoYears;
                    break;
                case 5:
                    callbackPeriod = ProjectFeedback.CallbackPeriodEnum.DoNotRemind;
                    break;
            }
            
            try
            {
                feedback.Submit(m_project.ID, 
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
