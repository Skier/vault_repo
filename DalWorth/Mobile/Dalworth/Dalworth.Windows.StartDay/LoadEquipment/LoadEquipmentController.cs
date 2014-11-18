using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Dalworth.Controls;
using Dalworth.Windows.StartDay.LoadItems;

namespace Dalworth.Windows.StartDay.LoadEquipment
{
    public class LoadEquipmentController : StartDayBaseController<LoadEquipmentModel, LoadEquipmentView>
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

        #region OnViewActivated

        public override void OnViewActivated()
        {
            if (Model.StartDayModel.IsStartDayDone() || Model.StartDayModel.IsStartDayCancelled)
                View.Destroy();
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            View.m_table.AddColumn(new TableColumn(0, 0));
            View.m_table.AddColumn(new TableColumn(1, 40));
            View.m_table.BindModel(Model);

            View.m_table.Focus();
            View.m_table.Select(0, 0);

            View.m_txtEquipmentNotes.Text = Model.StartDayModel.Work.EquipmentNotes;
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
