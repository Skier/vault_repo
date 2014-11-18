using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Domain;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Dalworth.Server.MainForm.Components
{
    public partial class TaskEditComplete : Dalworth.Server.MainForm.Components.TaskEdit
    {
        public delegate void FailTypeChangedHandler(Task task, TaskFailTypeEnum? failType);
        public event FailTypeChangedHandler FailTypeChanged;

        public delegate void ClosedAmountChangedHandler(decimal cost);
        public event ClosedAmountChangedHandler ClosedAmountChanged;

        private CheckEdit m_chkRecleaningRequired;

        #region Constructor

        public TaskEditComplete()
        {
            InitializeComponent();
            m_cmbFailType.SelectedIndexChanged += OnFailTypeChanged;
            m_txtClosedAmount.EditValueChanged += OnClosedAmountChanged;            
            m_txtClosedAmount.Modified += OnClosedAmountEndUserModified;
            m_ctlItems.ItemsChanged += OnItemsChanged;
            m_chkAmountAutoCalculated.CheckedChanged += OnAmountAutoCalculatedChanged;      
            m_spinDiscount.ValueChanged += OnDiscountChanged;

            m_chkRecleaningRequired = new CheckEdit();
            m_groupFailSection.Controls.Add(m_chkRecleaningRequired);
            Point location = m_lblReason.Location;
            location.Offset(53, -3);
            m_chkRecleaningRequired.Location = location;
            m_chkRecleaningRequired.Text = "Recleaning required";
            m_chkRecleaningRequired.Width = 150;

        }

        #endregion

        #region IsFailTypeChangeAllowed

        public bool IsFailTypeChangeAllowed
        {
            set
            {
                if (m_cmbFailType != null)
                    m_cmbFailType.Properties.ReadOnly = !value;
            }
        }

        #endregion

        #region IsClosedAmountEditable

        private bool m_isClosedAmountEditable;
        public bool IsClosedAmountEditable
        {
            get { return m_isClosedAmountEditable; }
            set { m_isClosedAmountEditable = value; }
        }

        #endregion

        #region OnClosedAmountEndUserModified

        private void OnClosedAmountEndUserModified(object sender, EventArgs e)
        {
            if (m_chkAmountAutoCalculated.Checked)
                m_chkAmountAutoCalculated.Checked = false;
        }

        #endregion

        #region OnClosedAmountChanged

        private void OnClosedAmountChanged(object sender, EventArgs e)
        {
            if (ClosedAmountChanged != null)
                ClosedAmountChanged.Invoke((decimal)m_txtClosedAmount.EditValue);
        }

        #endregion

        #region OnDiscountChanged

        private void OnDiscountChanged(object sender, EventArgs e)
        {
            OnItemsChanged();
        }

        #endregion


        #region OnItemsChanged

        private void OnItemsChanged()
        {
            if (m_chkAmountAutoCalculated.Checked)
            {
                if (m_isClosedAmountEditable)
                {
                    m_txtClosedAmount.EditValue = m_task.GetEstimatedClosedAmount(
                        Items, m_chkAmountAutoCalculated.Checked, m_spinDiscount.Value);
                }
                else
                    m_txtClosedAmount.EditValue = decimal.Zero;
            }                
        }

        #endregion

        #region OnAmountAutoCalculatedChanged

        private void OnAmountAutoCalculatedChanged(object sender, EventArgs e)
        {
            if (m_chkAmountAutoCalculated.Checked)
            {
                OnItemsChanged();
                m_spinDiscount.Enabled = true;
            }                
            else
            {
                m_spinDiscount.Enabled = false;
                m_spinDiscount.Value = 0;
            }
        }

        #endregion        

        #region OnFailTypeChanged

        private void OnFailTypeChanged(object sender, EventArgs e)
        {
            if (FailTypeChanged != null && m_task != null)
                FailTypeChanged.Invoke(m_task, (TaskFailTypeEnum?)(int?)m_cmbFailType.EditValue);
        }

        #endregion

        #region ChengeEditableState

        protected override void ChengeEditableState()
        {
            base.ChengeEditableState();
            
            if (m_task != null)
            {
                m_txtNotes.Text = m_task.Notes;
                m_chkRecleaningRequired.Visible = m_task.TaskType == TaskTypeEnum.RugDelivery;

                if (IsEditable && m_task.TaskType == TaskTypeEnum.RugDelivery)
                    m_ctlItems.IsEditable = true;
            }                                       

            if (IsEditable)
            {
                m_cmbFailType.Properties.ReadOnly = (m_task.TaskType == TaskTypeEnum.RugDelivery);
                m_txtFailReason.Properties.ReadOnly = false;                
            }            

            m_txtNotes.Properties.ReadOnly = !IsEditable;
        }

        #endregion

        #region UpdateClosedAmountEditableState

        private void UpdateClosedAmountEditableState()
        {
            if (m_task == null)
                return;

            if (m_task.TaskType == TaskTypeEnum.Help)
                m_txtClosedAmount.Properties.ReadOnly = true;
            else if (!m_isClosedAmountEditable)
                m_txtClosedAmount.Properties.ReadOnly = true;
            else
                m_txtClosedAmount.Properties.ReadOnly = false;    
        
            if (!m_txtClosedAmount.Properties.ReadOnly
                && (m_task.TaskType == TaskTypeEnum.RugPickup || m_task.TaskType == TaskTypeEnum.RugDelivery))
            {
                m_chkAmountAutoCalculated.Visible = true;
                m_chkAmountAutoCalculated.Enabled = !m_txtClosedAmount.Properties.ReadOnly;

                m_lblDiscount.Visible = true;
                m_spinDiscount.Visible = true;

                m_spinDiscount.Enabled = m_chkAmountAutoCalculated.Checked;
            } else
            {
                m_chkAmountAutoCalculated.Visible = false;

                m_lblDiscount.Visible = false;
                m_spinDiscount.Visible = false;
            }

            OnItemsChanged();
        }

        #endregion

        #region LoadDataToUI

        protected override void LoadDataToUI()
        {
            base.LoadDataToUI();

            UpdateClosedAmountEditableState();
            if (m_task != null)
            {                
                m_chkRecleaningRequired.Checked = !m_task.IsReady;
                m_ctlDeflood.Visible = m_task.TaskType == TaskTypeEnum.Deflood;
                m_ctlMonitoring.Visible = m_task.TaskType == TaskTypeEnum.Monitoring;
                m_chkReady.Visible = false;
            }                
            else
                m_chkRecleaningRequired.Checked = false;
        }

        #endregion

        #region GetDataFromUI

        protected override void GetDataFromUI()
        {
            base.GetDataFromUI();

            if (m_task != null && m_task.TaskType == TaskTypeEnum.RugDelivery 
                && m_task.TaskFailType != null)
            {
                m_task.IsReady = !m_chkRecleaningRequired.Checked;
            }                
        }

        #endregion
    }
}

