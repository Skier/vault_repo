using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors.Controls;

namespace Dalworth.Server.MainForm.MonitoringReadingEdit
{
    public class MonitoringReadingEditController : Controller<MonitoringReadingEditModel, MonitoringReadingEditView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region AffectedReading

        private MonitoringReading m_affectedReading;
        public MonitoringReading AffectedReading
        {
            get { return m_affectedReading; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data != null && data.Length > 0 && data[0] != null)
                Model.ExistingReading = (MonitoringReading) data[0];

            if (data != null && data.Length > 1 && data[1] != null)
                Model.IsEditable = (bool)data[1];
            else
                Model.IsEditable = true;

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick; 
           
            View.m_cmbReadingType.SelectedIndexChanged += OnReadingTypeChanged;
            View.m_cmbReadingType.Validating += OnReadingTypeValidating;
            View.m_txtReading.Validating += OnReadingValidating;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            if (Model.ExistingReading == null)
            {
                View.Text = "Add Reading";
                View.m_cmbReadingType.Properties.Items.RemoveAt(0);
                View.m_cmbReadingType.Properties.Items.RemoveAt(0);
                View.m_cmbReadingType.Properties.Items.RemoveAt(0);
                View.m_cmbReadingType.SelectedIndex = 0;

            } else
            {
                View.Text = "Edit Reading";
                View.m_cmbReadingType.EditValue = Model.ExistingReading.MonitoringReadingTypeId;

                if (Model.ExistingReading.MonitoringReadingType == MonitoringReadingTypeEnum.Inside
                    || Model.ExistingReading.MonitoringReadingType == MonitoringReadingTypeEnum.Outside
                    || Model.ExistingReading.MonitoringReadingType == MonitoringReadingTypeEnum.Unaffected)
                {
                    View.m_cmbReadingType.Properties.ReadOnly = true;
                } else
                {
                    View.m_cmbReadingType.Properties.Items.RemoveAt(0);
                    View.m_cmbReadingType.Properties.Items.RemoveAt(0);
                    View.m_cmbReadingType.Properties.Items.RemoveAt(0);
                }

                View.m_txtReading.EditValue = Model.ExistingReading.ReadingText;
                View.m_txtBtu.EditValue = Model.ExistingReading.BtuTonnage == decimal.Zero ? (decimal?)null : Model.ExistingReading.BtuTonnage;
                View.m_txtSerialNumber.Text = Model.ExistingReading.EquipmentSerialNumber;
                View.m_txtNotes.Text = Model.ExistingReading.Notes;
            }
            
            if (!Model.IsEditable)
            {
                View.m_cmbReadingType.Properties.ReadOnly = true;
                View.m_txtReading.Properties.ReadOnly = true;
                View.m_txtBtu.Properties.ReadOnly = true;
                View.m_txtSerialNumber.Properties.ReadOnly = true;
                View.m_txtNotes.Properties.ReadOnly = true;
                View.m_btnOk.Enabled = false;
            }

            if (View.m_cmbReadingType.Properties.ReadOnly)
                View.m_txtReading.Select();

        }

        #endregion

        #region Should be enabled

        private bool ShouldBtuBeEnabled()
        {
            return (int)View.m_cmbReadingType.EditValue == (int)MonitoringReadingTypeEnum.ACUnit;
        }

        private bool ShouldSerialNumberBeEnabled()
        {
            return (int) View.m_cmbReadingType.EditValue == (int) MonitoringReadingTypeEnum.ACUnit
                   || (int) View.m_cmbReadingType.EditValue == (int) MonitoringReadingTypeEnum.Dehumidifier;
        }

        #endregion

        #region Validation

        private void OnReadingTypeValidating(object sender, CancelEventArgs e)
        {
            if (!Model.IsEditable)
                return;

            if (View.m_cmbReadingType.SelectedIndex < 0)
                View.m_errorProvider.SetError(View.m_cmbReadingType, "Please select reading type");
            else
                View.m_errorProvider.SetError(View.m_cmbReadingType, string.Empty);
        }

        private void OnReadingValidating(object sender, CancelEventArgs e)
        {
            if (View.m_txtReading.Text == string.Empty)
                View.m_errorProvider.SetError(View.m_txtReading, "Please enter reading");
            else
                View.m_errorProvider.SetError(View.m_txtReading, string.Empty);
        }

        #endregion

        #region OnReadingTypeChanged

        private void OnReadingTypeChanged(object sender, EventArgs e)
        {
            View.m_txtBtu.Properties.ReadOnly = !ShouldBtuBeEnabled();
            View.m_txtSerialNumber.Properties.ReadOnly = !ShouldSerialNumberBeEnabled();
        }

        #endregion
               
        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            View.ValidateChildren();
            if (View.m_errorProvider.HasErrors)
                return;

            if (Model.ExistingReading != null)
                m_affectedReading = Model.ExistingReading;
            else
            {
                m_affectedReading = new MonitoringReading();
                m_affectedReading.IsRemoveAllowed = true;
            }                

            m_affectedReading.MonitoringReadingTypeId = (int)View.m_cmbReadingType.EditValue;
            m_affectedReading.ReadingText = (string)View.m_txtReading.EditValue;
            m_affectedReading.Notes = View.m_txtNotes.Text;

            if (ShouldBtuBeEnabled())
            {
                if (View.m_txtBtu.EditValue == null)
                    m_affectedReading.BtuTonnage = decimal.Zero;
                else
                    m_affectedReading.BtuTonnage = (decimal)View.m_txtBtu.EditValue;
            }                
            else
                m_affectedReading.BtuTonnage = decimal.Zero;

            if (ShouldSerialNumberBeEnabled())
                m_affectedReading.EquipmentSerialNumber = View.m_txtSerialNumber.Text;
            else
                m_affectedReading.EquipmentSerialNumber = string.Empty;
            

            View.Destroy();
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion
    }
}
