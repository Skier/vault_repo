using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Dalworth.Data;
using Dalworth.Domain;

namespace Dalworth.Windows.StartDay.VanCheck
{
    public class VanCheckController : StartDayBaseController<VanCheckModel, VanCheckView>
    {
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.StartDayModel = (StartDayModel)data[0];            
            Model.StepNumber = (int)data[1] + 1;
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_txtOdometer.TextChanged += new EventHandler(OnRequiredFieldTextChanged);
            View.m_txtHoursMeter.TextChanged += new EventHandler(OnRequiredFieldTextChanged);            
            View.m_txtSpecialNeeds.TextChanged += new EventHandler(OnRequiredFieldTextChanged);
            View.m_menuBack.Click += OnBackClick;
            View.m_menuCancel.Click += OnCancelClick;
        }

        #endregion

        #region OnRequiredFieldTextChanged

        private void OnRequiredFieldTextChanged(object sender, EventArgs e)
        {
            UpdateCompleteButton();
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {            
            UpdateCompleteButton();
            View.m_chkOilChecked.Focus();

            View.m_lblOilChangeDue.Text = Model.StartDayModel.Van.OilChangeDue;
            View.Text = string.Format("Van Checklist - step {0}/{0}", Model.StepNumber);            
            if (Model.StepNumber == 1)
                View.Text = "Van Checklist";
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

        public override string RightActionName
        {
            get { return "Done"; }
        }

        public override void OnRightAction()
        {
            OnDoneClick();
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

        #region OnDoneClick

        private void OnDoneClick()
        {
            try
            {
                decimal.Parse(View.m_txtOdometer.Text);
            }
            catch (Exception)
            {
                MessageDialog.Show(MessageDialogType.Information, "Please enter correct Odometer reading.");
                View.m_txtOdometer.SelectAll();
                View.m_txtOdometer.Focus();
                return;
            }

            try
            {
                decimal.Parse(View.m_txtHoursMeter.Text);
            }
            catch (Exception)
            {
                MessageDialog.Show(MessageDialogType.Information, "Please enter correct Hours (Hobbs) Meter reading.");
                View.m_txtHoursMeter.SelectAll();
                View.m_txtHoursMeter.Focus();
                return;
            }
            
            Model.WorkTransactionVanCheck = new WorkTransactionVanCheck(                
                0,
                View.m_chkOilChecked.Checked,
                View.m_chkUnitClean.Checked,
                View.m_chkVanClean.Checked,
                View.m_chkSuppliesStocked.Checked,
                decimal.Parse(View.m_txtOdometer.Text),
                decimal.Parse(View.m_txtHoursMeter.Text),
                View.m_txtSpecialNeeds.Text);

            try
            {
                using (WaitCursor cursor = new WaitCursor())
                {
                    Database.Begin();
                    Model.WriteStartDayTransaction();
                    Database.Commit();
                    View.Destroy();                    
                }
            }
            catch (WebException ex)
            {
                Database.Rollback();
                MessageDialog.Show(MessageDialogType.Warning,
                        "Couldn't establish connection to the server. Please check your connection availability and try again.");
                Host.Trace("VanCheckController::OnDoneClick", ex.Message + ex.StackTrace);
                return;
            }
            catch (Exception ex)
            {
                Database.Rollback();
                MessageDialog.Show(MessageDialogType.Warning,
                        "Unknown application error. Please contact dispatch");
                Host.Trace("VanCheckController::OnDoneClick", ex.Message + ex.StackTrace);
                return;
            }

        }

        #endregion

        #region UpdateCompleteButton

        private void UpdateCompleteButton()
        {
            if (View.m_txtOdometer.Text == String.Empty)
                View.m_lblOdometer.ForeColor = Color.Red;
            else
                View.m_lblOdometer.ForeColor = Color.Black;

            if (View.m_txtHoursMeter.Text == String.Empty)
                View.m_lblPumpReading.ForeColor = Color.Red;
            else
                View.m_lblPumpReading.ForeColor = Color.Black;

            if (View.m_txtSpecialNeeds.Text == String.Empty)
                View.m_lblSpecialNeeds.ForeColor = Color.Red;
            else
                View.m_lblSpecialNeeds.ForeColor = Color.Black;

            if (View.m_txtOdometer.Text == String.Empty
                || View.m_txtHoursMeter.Text == String.Empty
                || View.m_txtSpecialNeeds.Text == String.Empty)
            {
                IsRightActionExist = false;
            }
            else
                IsRightActionExist = true;
        }

        #endregion
    }
}
