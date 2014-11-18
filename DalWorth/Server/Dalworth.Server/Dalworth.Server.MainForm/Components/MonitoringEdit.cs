using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.MonitoringReadingEdit;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;

namespace Dalworth.Server.MainForm.Components
{
    public partial class MonitoringEdit : UserControl
    {
        private NavigatorButton m_btnAdd;
        private NavigatorCustomButton m_btnDelete;
        private NavigatorCustomButton m_btnEdit;

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

        #region MonitoringDetail

        private MonitoringDetail m_monitoringDetail;
        public MonitoringDetail MonitoringDetail
        {
            get
            {
                GetMonitoringDetailFromUI();
                return m_monitoringDetail;
            }
            set
            {
                m_monitoringDetail = value;
                SetMonitoringDetailToUI();
            }
        }

        #endregion

        #region MonitoringReadings

        private BindingList<MonitoringReading> m_monitoringReadings;
        public List<MonitoringReading> MonitoringReadings
        {
            get
            {                
                return new List<MonitoringReading>(m_monitoringReadings);
            }
            set
            {
                m_monitoringReadings = new BindingList<MonitoringReading>(value);
                SetMonitoringReadigsToUI();
            }
        }

        #endregion

        #region Constructor

        public MonitoringEdit()
        {
            InitializeComponent();

            m_btnAdd = m_gridReadings.EmbeddedNavigator.Buttons.Append;
            m_btnDelete = m_gridReadings.EmbeddedNavigator.CustomButtons[0];
            m_btnEdit = m_gridReadings.EmbeddedNavigator.CustomButtons[1];

            m_chkBasePulled.CheckedChanged += OnBasePulledCheckedChanged;
            m_chkConstructionNeeded.CheckedChanged += OnConstructionNeededCheckedChanged;

            m_gridViewReadings.FocusedRowChanged += OnReadingsFocusedRowChanged;
            m_gridReadings.EmbeddedNavigator.ButtonClick += OnReadingsButtonClick;
            m_gridViewReadings.ValidatingEditor += OnReadingsValidatingEditor;            
            m_gridViewReadings.KeyDown += OnReadingsKeyDown;
            
            m_chkNoReadings.CheckedChanged += OnNoReadingsChanged;
        }

        #endregion

        #region SetFocusToReadingsTable

        public void SetFocusToReadingsTable()
        {            
            m_gridViewReadings.Focus();
        }

        #endregion

        #region OnReadingsFocusedRowChanged

        private void OnReadingsFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            MonitoringReading reading 
                = (MonitoringReading) m_gridViewReadings.GetRow(e.FocusedRowHandle);
            
            if (reading == null)
            {
                m_btnEdit.Enabled = false;
                return;
            } else if (!m_isEditable)
            {
                m_btnAdd.Enabled = false;                
                m_btnDelete.Enabled = false;                
                m_btnEdit.Enabled = true;
                return;
            }

            m_btnAdd.Enabled = true;                
            m_btnEdit.Enabled = true;

            m_btnDelete.Enabled = reading.IsRemoveAllowed;
        }

        #endregion

        #region OnReadingsValidatingEditor

        private void OnReadingsValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            if (e.Value == null)
            {
                e.Valid = true;
                e.Value = decimal.Zero;
            }
        }

        #endregion

        #region OnReadingsButtonClick

        private void OnReadingsButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            m_gridViewReadings.CloseEditor();
            if (e.Button.ButtonType == NavigatorButtonType.Append)
            {
                using (MonitoringReadingEditController controller = Controller.Prepare<MonitoringReadingEditController>())
                {
                    controller.Execute(false);
                    if (!controller.IsCancelled)
                    {
                        m_monitoringReadings.Add(controller.AffectedReading);
                        m_gridViewReadings.FocusedRowHandle
                            = m_gridViewReadings.GetRowHandle(m_monitoringReadings.Count - 1);
                    }
                        
                }

                e.Handled = true;
            } 
            else if (e.Button == m_btnDelete)
            {
                m_gridViewReadings.DeleteSelectedRows();
            }
            else if (e.Button == m_btnEdit)
            {
                MonitoringReading reading
                    = (MonitoringReading)m_gridViewReadings.GetRow(m_gridViewReadings.FocusedRowHandle);
                int dataSourceIndex = m_monitoringReadings.IndexOf(reading);

                if (reading == null)
                    return;

                using (MonitoringReadingEditController controller 
                    = Controller.Prepare<MonitoringReadingEditController>(reading, m_isEditable))
                {
                    controller.Execute(false);
                    if (!controller.IsCancelled)
                    {
                        m_monitoringReadings[dataSourceIndex] = controller.AffectedReading;
                        m_monitoringReadings.ResetItem(dataSourceIndex);
                    }
                }

                e.Handled = true;
            }                
        }

        #endregion

        #region OnReadingsKeyDown

        private void OnReadingsKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (char.IsDigit(Convert.ToChar(e.KeyData)) 
                    && m_gridViewReadings.FocusedColumn.Name != m_colReading.Name)
                {
                    m_gridViewReadings.FocusedColumn = m_colReading;
                }
            }
            catch (Exception){}

            if (e.Alt)
            {
                if (e.KeyCode == Keys.Insert && m_btnAdd.Enabled)
                {
                    NavigatorButtonClickEventArgs arg = new NavigatorButtonClickEventArgs(m_btnAdd);

                    OnReadingsButtonClick(null, arg);

                    e.Handled = true;
                    e.SuppressKeyPress = true;

                }
                else if (e.KeyCode == Keys.Delete && m_btnDelete.Enabled)
                {
                    NavigatorButtonClickEventArgs arg = new NavigatorButtonClickEventArgs(m_btnDelete);

                    OnReadingsButtonClick(null, arg);

                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                else if (e.KeyCode == Keys.E && m_btnEdit.Enabled)
                {
                    NavigatorButtonClickEventArgs arg = new NavigatorButtonClickEventArgs(m_btnEdit);

                    OnReadingsButtonClick(null, arg);

                    e.Handled = true;
                    e.SuppressKeyPress = true;                    
                }
            }
        }

        #endregion



        #region OnConstructionNeededCheckedChanged

        private void OnConstructionNeededCheckedChanged(object sender, EventArgs e)
        {
            m_txtConstructionNeeded.Properties.ReadOnly = !m_chkConstructionNeeded.Checked || !m_isEditable;            
        }

        #endregion

        #region OnBasePulledCheckedChanged

        private void OnBasePulledCheckedChanged(object sender, EventArgs e)
        {
            m_cmbBaseLocation.Properties.ReadOnly = !m_chkBasePulled.Checked || !m_isEditable;            
        }

        #endregion

        #region EnableDisable

        private void EnableDisable()
        {
            m_chkAllEquipmentOn.Enabled = m_isEditable;
            m_chkAreaClean.Enabled = m_isEditable;
            m_chkBasePulled.Enabled = m_isEditable;
            m_chkCarpetRaked.Enabled = m_isEditable;
            m_chkConstructionNeeded.Enabled = m_isEditable;
            m_chkFurnitureBlocked.Enabled = m_isEditable;
            m_chkOdorsPresent.Enabled = m_isEditable;
            m_chkPlacementCorrect.Enabled = m_isEditable;
            m_chkReadingFilledOut.Enabled = m_isEditable;

            m_txtCheckPadAndSubFloor.Properties.ReadOnly = !m_isEditable;
            m_txtConstructionNeeded.Properties.ReadOnly = !m_isEditable;
            m_cmbBaseLocation.Properties.ReadOnly = !m_isEditable;
            m_cmbWallSurface.Properties.ReadOnly = !m_isEditable;

            m_gridViewReadings.OptionsBehavior.Editable = m_isEditable;

            OnConstructionNeededCheckedChanged(null, null);
            OnBasePulledCheckedChanged(null, null);
            
            OnReadingsFocusedRowChanged(null, 
                new FocusedRowChangedEventArgs(0, m_gridViewReadings.FocusedRowHandle));
        }

        #endregion

        #region SetMonitoringDetailToUI

        private void SetMonitoringDetailToUI()
        {
            if (m_monitoringDetail == null)
                return;

            m_chkAllEquipmentOn.Checked = m_monitoringDetail.IsAllEquipmentOn;
            m_chkAreaClean.Checked = m_monitoringDetail.IsAreaClean;
            m_chkBasePulled.Checked = m_monitoringDetail.IsBasePulled;
            m_chkCarpetRaked.Checked = m_monitoringDetail.IsCarpetRaked;
            m_chkConstructionNeeded.Checked = m_monitoringDetail.IsConstructionNeeded;
            m_chkFurnitureBlocked.Checked = m_monitoringDetail.IsFurnitureBlocked;
            m_chkOdorsPresent.Checked = m_monitoringDetail.IsOdorsPresent;
            m_chkPlacementCorrect.Checked = m_monitoringDetail.IsPlacementCorrect;
            m_chkReadingFilledOut.Checked = m_monitoringDetail.IsReadingAndMoistureMapFilledOut;

            m_txtCheckPadAndSubFloor.Text = m_monitoringDetail.CheckPadAndSubFloor;
            m_txtConstructionNeeded.Text = m_monitoringDetail.ConstructionNeededNotes;
            m_cmbBaseLocation.Text = m_monitoringDetail.BaseLocation;
            m_cmbWallSurface.Text = m_monitoringDetail.WallSurface;            

            OnConstructionNeededCheckedChanged(null, null);
            OnBasePulledCheckedChanged(null, null);
        }

        #endregion

        #region GetMonitoringDetailFromUI

        private void GetMonitoringDetailFromUI()
        {
            if (m_monitoringDetail == null)
                return;

            m_monitoringDetail.IsAllEquipmentOn = m_chkAllEquipmentOn.Checked;
            m_monitoringDetail.IsAreaClean = m_chkAreaClean.Checked;
            m_monitoringDetail.IsBasePulled = m_chkBasePulled.Checked;
            m_monitoringDetail.IsCarpetRaked = m_chkCarpetRaked.Checked;
            m_monitoringDetail.IsConstructionNeeded = m_chkConstructionNeeded.Checked;
            m_monitoringDetail.IsFurnitureBlocked = m_chkFurnitureBlocked.Checked;
            m_monitoringDetail.IsOdorsPresent = m_chkOdorsPresent.Checked;
            m_monitoringDetail.IsPlacementCorrect = m_chkPlacementCorrect.Checked;
            m_monitoringDetail.IsReadingAndMoistureMapFilledOut = m_chkReadingFilledOut.Checked;

            m_monitoringDetail.CheckPadAndSubFloor = m_txtCheckPadAndSubFloor.Text;
            m_monitoringDetail.ConstructionNeededNotes = m_txtConstructionNeeded.Text;
            m_monitoringDetail.BaseLocation = (string)m_cmbBaseLocation.SelectedItem;
            m_monitoringDetail.WallSurface = (string)m_cmbWallSurface.SelectedItem;

            if (!m_chkBasePulled.Checked)
                m_monitoringDetail.BaseLocation = string.Empty;

            if (!m_chkConstructionNeeded.Checked)
                m_monitoringDetail.ConstructionNeededNotes = string.Empty;
        }

        #endregion

        #region SetMonitoringReadigsToUI

        private void SetMonitoringReadigsToUI()
        {
            if (m_monitoringReadings == null || m_monitoringReadings.Count == 0)
            {
                m_monitoringReadings = new BindingList<MonitoringReading>(
                    MonitoringReading.DefaultReadings);
            }

            m_colPreviousReading.Visible = IsPreviousReadingsExists();

            m_gridReadings.DataSource = m_monitoringReadings;
            OnReadingsFocusedRowChanged(null,
                new FocusedRowChangedEventArgs(0, m_gridViewReadings.FocusedRowHandle));

            if (m_monitoringDetail != null)
                m_chkNoReadings.Checked = m_monitoringDetail.IsNoReadings;
        }

        #endregion

        #region IsPreviousReadingsExists

        private bool IsPreviousReadingsExists()
        {
            foreach (MonitoringReading reading in m_monitoringReadings)
            {
                if (reading.PreviousTemperature != decimal.Zero
                    || reading.PreviousRelativeHumidity != decimal.Zero
                    || reading.PreviousGpp != decimal.Zero)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region OnNoReadingsChanged

        private void OnNoReadingsChanged(object sender, EventArgs e)
        {
            if (m_monitoringDetail != null)
                m_monitoringDetail.IsNoReadings = m_chkNoReadings.Checked;

            m_gridReadings.Enabled = !m_chkNoReadings.Checked;
            
            if (m_chkNoReadings.Checked && m_monitoringReadings != null)
            {
                foreach (MonitoringReading reading in m_monitoringReadings)
                {
                    reading.EquipmentSerialNumber = null;
                    reading.Temperature = decimal.Zero;
                    reading.RelativeHumidity = decimal.Zero;
                    reading.BtuTonnage = decimal.Zero;
                    reading.Notes = null;
                    reading.Gpp = decimal.Zero;
                }

                m_monitoringReadings.ResetBindings();
            }
        }

        #endregion

    }
}
