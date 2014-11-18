using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.MonitoringReadings;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using BaseControl=Dalworth.Server.Windows.BaseControl;

namespace Dalworth.Server.MainForm.Components
{
    public partial class DefloodEdit : BaseControl
    {
        #region IsEditable

        private bool m_isEditable;
        public bool IsEditable
        {
            get { return m_isEditable; }
            set
            {
                m_isEditable = value;
                EnableDisable();
            }
        }

        #endregion

        #region IsReadingsAvailable

        public bool IsReadingsAvailable
        {
            set { m_btnReadings.Enabled = value; }
        }

        #endregion

        #region DefloodDetail

        private DefloodDetail m_defloodDetail;
        public DefloodDetail DefloodDetail
        {
            get
            {
                GetDataFromUI();
                return m_defloodDetail;
            }
            set
            {
                m_defloodDetail = value;
                SetDataToUI();
            }
        }

        #endregion

        #region Constructor

        public DefloodEdit()
        {
            InitializeComponent();
            m_btnReadings.Click += OnReadingsClick;
        }

        #endregion                

        #region EnableDisable

        private void EnableDisable()
        {
            m_dtpFloodDate.Properties.ReadOnly = !m_isEditable;
            m_cmbClass.Properties.ReadOnly = !m_isEditable;
            m_txtCubicFeet.Properties.ReadOnly = !m_isEditable;            
        }

        #endregion

        #region SetDataToUI

        private void SetDataToUI()
        {
            if (m_defloodDetail == null)
                return;

            m_dtpFloodDate.EditValue = m_defloodDetail.FloodDate;
            m_cmbClass.EditValue = m_defloodDetail.FloodClass;
            if (m_defloodDetail.CubicFeet == decimal.Zero)
                m_txtCubicFeet.EditValue = null;
            else
                m_txtCubicFeet.EditValue = m_defloodDetail.CubicFeet;               
        }

        #endregion

        #region GetDataFromUI

        private void GetDataFromUI()
        {
            if (m_defloodDetail == null)
                return;

            if (m_dtpFloodDate.EditValue == null)
                m_defloodDetail.FloodDate = null;
            else
                m_defloodDetail.FloodDate = m_dtpFloodDate.DateTime;

            if (m_cmbClass.EditValue == null)
                m_defloodDetail.FloodClass = null;
            else
                m_defloodDetail.FloodClass = (int)m_cmbClass.EditValue;

            if (m_txtCubicFeet.EditValue == null)
                m_defloodDetail.CubicFeet = 0;
            else
                m_defloodDetail.CubicFeet = Convert.ToDecimal(m_txtCubicFeet.EditValue);            
        }

        #endregion

        #region OnReadingsClick

        private void OnReadingsClick(object sender, EventArgs e)
        {
            using (MonitoringReadingsController controller
                = Controller.Prepare<MonitoringReadingsController>(
                    new Task(m_defloodDetail.DefloodTaskId)))
            {
                if (!controller.IsReadingsExist)
                {
                    XtraMessageBox.Show("No readings available", "No readings", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                } else
                    controller.Execute(false);                
            }
        }

        #endregion

    }
}
