using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Dalworth.Windows.StartDay.Message;

namespace Dalworth.Windows.StartDay.WorkSummary
{
    public class WorkSummaryController : StartDayBaseController<WorkSummaryModel, WorkSummaryView>
    {
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.StartDayModel = (StartDayModel)data[0];
            base.OnModelInitialize(data);            
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_menuBack.Click += OnBackClick;
            View.m_menuCancel.Click += OnCancelClick;
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            View.m_lblTechnicianName.Text = Model.StartDayModel.Technician.DisplayName;
            View.m_lblVanNumber.Text = Model.StartDayModel.Van.LicensePlateNumber;
        }

        #endregion

        #region OnViewActivated

        public override void OnViewActivated()
        {
            if (Model.StartDayModel.IsStartDayDone() || Model.StartDayModel.IsStartDayCancelled)
                View.Destroy();
        }

        #endregion

        #region Back Next

        public override bool IsLeftActionExist
        {
            get { return true; }            
        }

        public override string LeftActionName
        {
            get { return "Menu"; }            
        }

        public override bool IsRightActionExist
        {
            get { return true; }
        }

        public override string RightActionName
        {
            get { return "Next"; }
        }

        public override void OnRightAction()
        {
            Next();
        }

        private void OnBackClick(object sender, EventArgs e)
        {
            View.Destroy();
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            if (MessageDialog.Show(MessageDialogType.Question, "Do you realy want to cancel Start Day wizard?")
                == DialogResult.Yes)
            {
                Model.StartDayModel.IsStartDayCancelled = true;
                View.Destroy();
            }
        }

        #endregion
    }
}
