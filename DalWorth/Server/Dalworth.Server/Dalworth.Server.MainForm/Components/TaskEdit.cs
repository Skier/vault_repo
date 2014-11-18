using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.Components
{
    public partial class TaskEdit : BaseControl
    {
        #region Task

        protected Task m_task;
        public Task Task
        {
            get
            {
                GetDataFromUI();
                return m_task;
            }
            set
            {
                m_task = value;
                LoadDataToUI();
            }
        }

        #endregion

        #region Items

        private List<Item> m_items;
        public List<Item> Items
        {
            get
            {
                if (m_items == null)
                    return null;
                return new List<Item>(m_items);                
            }
            set
            {
                m_items = value;
            }
        }

        #endregion

        #region IsEditable

        private bool m_isEditable;
        public bool IsEditable
        {
            get { return m_isEditable; }
            set
            {
                m_isEditable = value;
                ChengeEditableState();
            }
        }

        #endregion

        #region OriginalMessage

        private string m_originalMessage;
        public string OriginalMessage
        {
            get { return m_originalMessage; }
            set { m_originalMessage = value; }
        }

        #endregion

        #region IsClosedAmountUnknownVisible

        public bool IsClosedAmountUnknownVisible
        {
            get { return m_chkAmountNotKnown.Visible; }
            set { m_chkAmountNotKnown.Visible = value; }
        }

        #endregion

        #region Constructor

        public TaskEdit()
        {
            InitializeComponent();  
            m_txtClosedAmount.Validating += OnClosedAmountValidating;
            m_chkAmountNotKnown.CheckedChanged += OnAmountNotKnownChanged;
        }

        #endregion

        #region SetFocusToNotes

        public void SetFocusToNotes()
        {
            m_txtNotes.Focus();
        }

        #endregion

        #region SetFocusToRugsGrid

        public void SetFocusToRugsGrid()
        {
            if (m_ctlItems != null)
                m_ctlItems.SetFocusToRugsGrid();
        }

        #endregion

        #region SetFocusToReadingsTable

        public void SetFocusToReadingsTable()
        {
            if (m_ctlMonitoring != null)
                m_ctlMonitoring.SetFocusToReadingsTable();
        }

        #endregion


        #region LoadDataToUI

        protected virtual void LoadDataToUI()
        {
            if (m_task == null)
                return;


            if (m_task.TaskType == TaskTypeEnum.Monitoring || m_task.TaskType == TaskTypeEnum.RugDelivery)
            {
                m_layoutNotes.RowStyles[0].Height = 50;
                m_layoutNotes.RowStyles[1].Height = 50;
                m_task.InitPreviousTaskNotes();
                m_txtNotesPrevious.Text = m_task.PreviousNotes;                
                m_txtNotesPrevious.Visible = true;
            } else
            {
                m_layoutNotes.RowStyles[0].Height = 0;
                m_layoutNotes.RowStyles[1].Height = 100;
                m_txtNotesPrevious.Visible = false;                
            }

            if ((m_task.TaskType == TaskTypeEnum.RugPickup || m_task.TaskType == TaskTypeEnum.RugDelivery)
                && m_task.Project.ProjectType != ProjectTypeEnum.Deflood)
            {
                m_chkAmountAutoCalculated.Visible = true;
                m_lblDiscount.Visible = true;
                m_spinDiscount.Visible = true;                
                
            } else
            {
                m_chkAmountAutoCalculated.Visible = false;                
                m_lblDiscount.Visible = false;
                m_spinDiscount.Visible = false;                
            }

            m_chkAmountAutoCalculated.Enabled = false;
            m_spinDiscount.Enabled = false;

            m_chkAmountAutoCalculated.Checked = m_task.IsClosedAmountAutoCalculated;
            m_spinDiscount.Value = m_task.DiscountPercentage;                


            if (m_task.TaskType == TaskTypeEnum.RugPickup
                || m_task.TaskType == TaskTypeEnum.RugDelivery)
            {                
                m_ctlItems.IsPartOfFlood = m_task.Project.ProjectType == ProjectTypeEnum.Deflood;
                LoadItemsToUI();
                m_ctlItems.Visible = true;
            } else
            {
                m_ctlItems.Visible = false;
            }

            if (m_task.TaskType == TaskTypeEnum.Deflood)
            {
                m_ctlDeflood.Visible = m_task.TaskStatus == TaskStatusEnum.InProcess;
                m_ctlDeflood.DefloodDetail = m_task.DefloodDetail;
            }                
            else
                m_ctlDeflood.Visible = false;

            if (m_task.TaskType == TaskTypeEnum.Monitoring)
            {
                m_ctlMonitoring.Visible = m_task.TaskStatus == TaskStatusEnum.Completed;
                m_ctlMonitoring.MonitoringDetail = m_task.MonitoringDetail;
                m_ctlMonitoring.MonitoringReadings = m_task.MonitoringReadings;
            }                
            else
                m_ctlMonitoring.Visible = false;


            ShowHideFailSection(m_task.TaskFailTypeId.HasValue);

            m_lblNumber.Text = m_task.Number == string.Empty ? "Unknown" : m_task.Number;
            m_lblType.Text = m_task.TaskTypeText;
            m_lblTaskStatus.Text = m_task.TaskStatusText;
            m_lblCreated.Text = m_task.CreateDate == null ? "Unknown" : m_task.CreateDate.Value.ToShortDateString();
            m_chkReady.Checked = m_task.IsReady;
            m_txtNotes.Text = m_task.Notes;

            if (m_task.TaskType == TaskTypeEnum.RugDelivery)
                m_chkReady.Visible = true;
            else
                m_chkReady.Visible = false;

            if (m_task.TaskFailTypeId.HasValue)
                m_cmbFailType.EditValue = m_task.TaskFailTypeId.Value;
            else
                m_cmbFailType.EditValue = null;
            m_txtFailReason.Text = m_task.FailReason;


            m_lblEstimateClosedAmountLabel.Visible = false;
            m_lblEstimateClosedAmountValue.Visible = false;
            m_lblClosedAmount.Text = "&Closed Amt";            
            m_txtClosedAmount.Properties.ReadOnly = true;

            if (m_task.TaskType == TaskTypeEnum.RugPickup 
                && m_task.Project.ProjectType == ProjectTypeEnum.RugCleaning)
            {
                m_lblClosedAmount.Text = "Est &Closed Amt";
                m_txtClosedAmount.EditValue = m_task.EstimatedClosedAmount;                
            }
            else if (m_task.TaskType == TaskTypeEnum.RugDelivery
                && m_task.Project.ProjectType == ProjectTypeEnum.RugCleaning)
            {
                m_lblEstimateClosedAmountLabel.Visible = true;
                m_lblEstimateClosedAmountValue.Visible = true;
                m_lblEstimateClosedAmountValue.Text = m_task.EstimatedClosedAmount.ToString("C");
                m_txtClosedAmount.EditValue = m_task.ClosedAmount;
            }
            else
                m_txtClosedAmount.EditValue = m_task.ClosedAmount;


            m_chkAmountNotKnown.Checked = m_task.IsAmountNotKnown;


            if (m_task.TaskType == TaskTypeEnum.Miscellaneous && m_task.Project.ProjectType == ProjectTypeEnum.Miscellaneous)
                m_chkIsRugDepartment.Visible = true;
            else
                m_chkIsRugDepartment.Visible = false;
            m_chkIsRugDepartment.Checked = m_task.IsRugCleaningDepartment;
            
            m_cmbFailType.Properties.ReadOnly = true;

            if (m_task.TaskFailTypeId.HasValue && m_task.TaskFailType == TaskFailTypeEnum.Cancel)
                m_txtFailReason.Properties.ReadOnly = false;
            else
                m_txtFailReason.Properties.ReadOnly = true;
        }

        #endregion

        #region GetDataFromUI

        protected virtual void GetDataFromUI()
        {
            if (m_task == null)
                return;

            m_task.IsReady = m_chkReady.Checked;            
            m_task.Notes = m_txtNotes.Text;

            if (m_cmbFailType.EditValue == null)
                m_task.TaskFailTypeId = null;
            else
                m_task.TaskFailTypeId = (int)m_cmbFailType.EditValue;
                
            m_task.FailReason = m_txtFailReason.Text;

            m_task.IsClosedAmountAutoCalculated = m_chkAmountAutoCalculated.Checked;
            m_task.DiscountPercentage = (int)m_spinDiscount.Value;

            if (m_task.TaskType == TaskTypeEnum.RugPickup
                && m_task.Project.ProjectType == ProjectTypeEnum.RugCleaning)
            {
                m_task.EstimatedClosedAmount = (decimal)m_txtClosedAmount.EditValue;
            }                
            else
                m_task.ClosedAmount = (decimal)m_txtClosedAmount.EditValue;

            if (m_task.TaskType == TaskTypeEnum.Deflood)
                m_task.DefloodDetail = m_ctlDeflood.DefloodDetail;

            if (m_task.TaskType == TaskTypeEnum.Monitoring)
            {
                m_task.MonitoringDetail = m_ctlMonitoring.MonitoringDetail;
                m_task.MonitoringReadings = m_ctlMonitoring.MonitoringReadings;
            }

            m_task.IsAmountNotKnown = m_chkAmountNotKnown.Checked;            
            m_task.IsRugCleaningDepartment = m_chkIsRugDepartment.Checked;            
        }

        #endregion

        #region LoadItemsToUI

        private void LoadItemsToUI()
        {
            if (m_items == null)
                m_items = new List<Item>();

            m_ctlItems.Items = new BindingList<Item>(m_items);
        }

        #endregion

        #region ChengeEditableState

        protected virtual void ChengeEditableState()
        {
            m_chkReady.Enabled = m_isEditable;
            m_txtNotes.Properties.ReadOnly = !m_isEditable;
            m_chkIsRugDepartment.Enabled = m_isEditable;

            if (m_isEditable && m_task.TaskType == TaskTypeEnum.RugPickup)
                m_ctlItems.IsEditable = true;
            else
                m_ctlItems.IsEditable = false;

            if (m_task != null && m_task.TaskType == TaskTypeEnum.Deflood)
                m_ctlDeflood.IsEditable = m_isEditable;

            if (m_task != null && m_task.TaskType == TaskTypeEnum.Monitoring)
                m_ctlMonitoring.IsEditable = m_isEditable;

            if (!m_isEditable)
            {
                m_txtClosedAmount.Properties.ReadOnly = true;
                m_chkAmountAutoCalculated.Enabled = false;
                m_spinDiscount.Enabled = false;
            }
        }

        #endregion

        #region ShowHideFailSection

        private void ShowHideFailSection(bool isShow)
        {
            if (isShow)
            {
                m_groupFailSection.Visible = true;
                m_layoutFail.ColumnStyles[1].Width = 50;
                m_layoutFail.ColumnStyles[0].Width = 50;
            }
            else
            {
                m_groupFailSection.Visible = false;
                m_layoutFail.ColumnStyles[1].Width = 0;
                m_layoutFail.ColumnStyles[0].Width = 100;
            }            
        }

        #endregion

        #region OnAmountNotKnownChanged

        private void OnAmountNotKnownChanged(object sender, EventArgs e)
        {
            m_txtClosedAmount.Properties.ReadOnly = m_chkAmountNotKnown.Checked;
            m_task.IsAmountNotKnown = m_chkAmountNotKnown.Checked;            
            if (m_txtClosedAmount.Properties.ReadOnly)
            {
                m_txtClosedAmount.EditValue = decimal.Zero;
                ClearClosedAmountError();
            }                
        }

        #endregion


        #region OnClosedAmountValidating

        private void OnClosedAmountValidating(object sender, CancelEventArgs e)
        {
            if ((decimal)m_txtClosedAmount.EditValue != decimal.Zero)
                ClearClosedAmountError();
        }

        #endregion

        #region Set & Clear ClosedAmount error

        public void SetClosedAmountError()
        {
            if (m_task.TaskType == TaskTypeEnum.RugPickup)
                m_errorProvider.SetError(m_txtClosedAmount, "Please enter Estimated Closed Amount");
            else
                m_errorProvider.SetError(m_txtClosedAmount, "Please enter Closed Amount");
        }

        public void ClearClosedAmountError()
        {
            m_errorProvider.SetError(m_txtClosedAmount, string.Empty);
        }

        #endregion
    }
}
