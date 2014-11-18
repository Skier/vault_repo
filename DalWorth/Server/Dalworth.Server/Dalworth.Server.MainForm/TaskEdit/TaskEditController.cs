using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using DevExpress.XtraGrid.Views.Base;

namespace Dalworth.Server.MainForm.TaskEdit
{
    public class TaskEditController : Controller<TaskEditModel, TaskEditView>
    {
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.Task = (Task) data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnOk.Click += OnOkClick;
            View.m_btnCancel.Click += OnCancelClick;

            View.m_rugsView.Items = Model.Items;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_dtpServiceDate.Properties.MinValue = DateTime.Now;

            View.m_lblNumber.Text = Model.Task.Number;
            View.m_lblType.Text = Model.Task.TaskTypeText;
            View.m_lblStatus.Text = Model.Task.TaskStatusText;
            View.m_lblCreateDate.Text = Model.Task.CreateDate.Value.ToShortDateString();
            View.m_dtpServiceDate.DateTime = Model.Task.ServiceDate.Value;
            View.m_cmbDuration.Duration = new TimeSpan(0, Model.Task.DurationMin ?? 0, 0);

            View.m_messages.Message1Text = Model.Task.Description;
            View.m_messages.Message2Text = Model.Task.Notes;
            View.m_messages.Message3Text = Model.Task.Message;

            if (Model.IsReadOnly)
            {
                View.m_dtpServiceDate.Properties.ReadOnly = true;
                View.m_cmbDuration.Properties.ReadOnly = true;

                View.m_messages.ReadOnly = true;
            }
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            if (Model.Task.TaskStatus == TaskStatusEnum.Completed
                || Model.Task.TaskStatus == TaskStatusEnum.RugDeliveryCreated)
                return;

            Model.Task.ServiceDate = View.m_dtpServiceDate.DateTime;
            Model.Task.DurationMin = (int)View.m_cmbDuration.Duration.TotalMinutes;
            Model.Task.Description = View.m_messages.Message1Text;
            Model.Task.Message = View.m_messages.Message3Text;
            Model.Task.Notes = View.m_messages.Message2Text;

            try
            {
                Database.Begin();
                Task.UpdateWithDetails(Model.Task);
                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

            View.Destroy();
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            View.Destroy();
        }

        #endregion                
    }
}
