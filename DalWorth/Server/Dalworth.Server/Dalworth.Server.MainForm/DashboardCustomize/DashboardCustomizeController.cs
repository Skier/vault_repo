using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.XtraDataLayout;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Dalworth.Server.MainForm.DashboardCustomize
{
    public class DashboardCustomizeController : Controller<DashboardCustomizeModel, DashboardCustomizeView>
    {        
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.DashboardDate = (DateTime) data[0];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_lstTechnicians.SelectedIndexChanged += OnTechniciansListSelectedIndexChanged;
            View.m_btnUp.Click += OnUpClick;
            View.m_btnDown.Click += OnDownClick;

            View.m_btnOk.Click += OnOkClick;
            View.m_btnCancel.Click += OnCancelClick;  
          
            View.m_lstTechnicians.MouseClick += OnTechniciansMouseClick;
            View.m_cmbTechnicians.Closed += OnTechniciansPopupClosed;                
            View.m_lstTechnicians.KeyDown += OnTechniciansKeyDown;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_txtVisibleCount.Properties.MaxValue = Model.Technicians.Count;
            View.m_txtVisibleCount.Value = View.m_txtVisibleCount.Properties.MaxValue;
            foreach (TechnicianCustomize technician in Model.Technicians)
            {
                if (!technician.Setting.IsVisible)
                {
                    View.m_txtVisibleCount.Value = Model.Technicians.IndexOf(technician);
                    break;
                }                    
            }

            View.m_txtVisibleCount.ValueChanged += OnVisibleCountChanged;
            View.m_lstTechnicians.DataSource = Model.Technicians;
            View.m_lstTechnicians.Focus();
        }

        #endregion

        #region OnTechniciansKeyDown

        private void OnTechniciansKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
                OpenTechnicianSelector(View.m_lstTechnicians.SelectedIndex);
            else if (e.Alt && e.KeyCode == Keys.Up && View.m_btnUp.Enabled)
                OnUpClick(null, null);
            else if (e.Alt && e.KeyCode == Keys.Down && View.m_btnUp.Enabled)
                OnDownClick(null, null);
        }

        #endregion


        #region OnUpClick

        private void OnUpClick(object sender, EventArgs e)
        {
            foreach (int index in View.m_lstTechnicians.SelectedIndices)
            {
                if (index == 0)
                    return;
            }

            int[] indexArray = new int[View.m_lstTechnicians.SelectedIndices.Count];
            View.m_lstTechnicians.SelectedIndices.CopyTo(indexArray, 0);
            List<int> indexes = new List<int>(indexArray);
            indexes.Sort();


            View.m_lstTechnicians.BeginUpdate();
            foreach (int index in indexes)
            {
                TechnicianCustomize aboveTechnician = Model.Technicians[index - 1];
                Model.Technicians[index - 1] = Model.Technicians[index];
                Model.Technicians[index] = aboveTechnician;
                Model.Technicians[index - 1].ItemIndex = index - 1;
                Model.Technicians[index].ItemIndex = index;
            }

            foreach (int index in indexes)
            {
                View.m_lstTechnicians.SetSelected(index - 1, true);
                View.m_lstTechnicians.SetSelected(index, false);                
            }
            View.m_lstTechnicians.EndUpdate();            
        }

        #endregion

        #region OnDownClick

        private void OnDownClick(object sender, EventArgs e)
        {
            foreach (int index in View.m_lstTechnicians.SelectedIndices)
            {
                if (index == Model.Technicians.Count - 1)
                    return;
            }
            
            int[] indexArray = new int[View.m_lstTechnicians.SelectedIndices.Count];
            View.m_lstTechnicians.SelectedIndices.CopyTo(indexArray, 0);
            List<int> indexes = new List<int>(indexArray);
            indexes.Sort();

            View.m_lstTechnicians.BeginUpdate();
            for (int i = indexes.Count - 1; i >= 0; i--)
            {
                TechnicianCustomize belowTechnician = Model.Technicians[indexes[i] + 1];
                Model.Technicians[indexes[i] + 1] = Model.Technicians[indexes[i]];
                Model.Technicians[indexes[i]] = belowTechnician;
                Model.Technicians[indexes[i] + 1].ItemIndex = indexes[i] + 1;
                Model.Technicians[indexes[i]].ItemIndex = indexes[i];

            }

            for (int i = indexes.Count - 1; i >= 0; i--)
            {
                View.m_lstTechnicians.SetSelected(indexes[i], false);
                View.m_lstTechnicians.SetSelected(indexes[i] + 1, true);
            }
            View.m_lstTechnicians.EndUpdate();
        }

        #endregion        

        #region HideCombo

        private void HideCombo()
        {
            View.m_cmbTechnicians.Visible = false;
            View.m_lstTechnicians.Focus();
        }

        #endregion

        #region OnTechniciansPopupClosed

        private void OnTechniciansPopupClosed(object sender, ClosedEventArgs e)
        {
            if (e.CloseMode == PopupCloseMode.Normal || e.CloseMode == PopupCloseMode.CloseUpKey)
            {
                TechnicianCustomize item = (TechnicianCustomize) View.m_lstTechnicians.GetItem(
                    View.m_lstTechnicians.SelectedIndex);
                item.Setting.Technician = (Employee) View.m_cmbTechnicians.EditValue;                
            }
            HideCombo();
        }

        #endregion

        #region OnTechniciansListSelectedIndexChanged

        private void OnTechniciansListSelectedIndexChanged(object sender, EventArgs e)
        {
            HideCombo();

            int[] indexArray = new int[View.m_lstTechnicians.SelectedIndices.Count];

            if (indexArray.Length == 0)
            {
                View.m_btnUp.Enabled = true;
                View.m_btnDown.Enabled = true;
                return;                
            }

            View.m_lstTechnicians.SelectedIndices.CopyTo(indexArray, 0);
            List<int> indexes = new List<int>(indexArray);
            indexes.Sort();

            bool up = true;
            bool down = true;

            foreach (int index in indexes)
            {
                if (!Model.Technicians[index].Setting.IsVisible)
                {
                    View.m_btnUp.Enabled = false;
                    View.m_btnDown.Enabled = false;
                    return;
                }        
        
                if (index == (int)View.m_txtVisibleCount.Value - 1)
                    down = false;
            }

            if (indexes[0] == 0)
                up = false;
            if (indexes[indexes.Count - 1] == Model.Technicians.Count - 1)
                down = false;                


            View.m_btnUp.Enabled = up;
            View.m_btnDown.Enabled = down;
        }

        #endregion

        #region OnVisibleCountChanged

        private void OnVisibleCountChanged(object sender, EventArgs e)
        {
            View.m_lstTechnicians.BeginUpdate();
            for (int i = 0; i < Model.Technicians.Count; i++)
            {
                Model.Technicians[i].Setting.IsVisible = i + 1 <= View.m_txtVisibleCount.Value;
            }
            View.m_lstTechnicians.EndUpdate();
            OnTechniciansListSelectedIndexChanged(null, null);
            View.m_txtVisibleCount.Focus();
        }

        #endregion


        #region OnTechniciansMouseClick

        private void OnTechniciansMouseClick(object sender, MouseEventArgs e)
        {
            int itemIndex = View.m_lstTechnicians.IndexFromPoint(e.Location);

            if (e.Button == MouseButtons.Right)
                OpenTechnicianSelector(itemIndex);            
        }

        private void OpenTechnicianSelector(int listItemIndex)
        {
            if (listItemIndex >= 0)
            {
                View.m_cmbTechnicians.Properties.Items.Clear();
                TechnicianCustomize currentItem
                    = (TechnicianCustomize)View.m_lstTechnicians.GetItem(listItemIndex);

                if (currentItem.Work != null && currentItem.Work.WorkStatus != WorkStatusEnum.Pending)
                    return;

                if (currentItem.Setting.Technician.IsUnknown && !currentItem.Setting.IsVisible)
                    return;

                if (currentItem.Setting.Technician.IsUnknown)
                {
                    View.m_cmbTechnicians.Properties.Items.Add(
                        new ImageComboBoxItem(currentItem.Setting.Technician.DisplayName,
                            currentItem.Setting.Technician));
                    View.m_cmbTechnicians.SelectedIndex = 0;
                }
                else
                {
                    Employee unknownTechnician
                        = Model.UnknownTechnicianMap[currentItem.Setting.UnknownTechnicianId];

                    View.m_cmbTechnicians.Properties.Items.Add(
                        new ImageComboBoxItem(unknownTechnician.DisplayName,
                            unknownTechnician));
                }

                foreach (Employee employee in Model.TechnicianChoiceList)
                {
                    View.m_cmbTechnicians.Properties.Items.Add(
                        new ImageComboBoxItem(employee.DisplayName, employee));

                    if (employee.ID == currentItem.Setting.Technician.ID)
                        View.m_cmbTechnicians.EditValue = employee;
                }

                View.m_lstTechnicians.SelectedIndex = listItemIndex;

                Rectangle itemRect = View.m_lstTechnicians.GetItemRectangle(listItemIndex);
                View.m_cmbTechnicians.Location
                    = new Point(itemRect.Location.X + 42,
                    itemRect.Location.Y + View.m_lstTechnicians.Location.Y);
                View.m_cmbTechnicians.Visible = true;
                View.m_cmbTechnicians.Focus();
                View.m_cmbTechnicians.ShowPopup();
            }            
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            if (!IsValid())
                return;

            try
            {
                Database.Begin();
                Model.Save();
                Database.Commit();
                Host.TraceUserAction("Technicians Arrangement modified");
                View.Destroy();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }            
        }

        #endregion

        #region IsValid

        private bool IsValid()
        {
            if (Model.IsEveryoneOff())
            {
                XtraMessageBox.Show("At least one technician should be turned on", "All technicians are off",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            TechnicianCustomize duplicate = Model.FindDuplicateTechnician();
            if (duplicate != null)
            {
                XtraMessageBox.Show(string.Format("Technician list shouldn't contain duplicates ({0})", 
                                                  duplicate.Setting.Technician.DisplayName), 
                                    "Duplicate Technician",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;                
            }

            TechnicianCustomize invisibleWithWork = Model.FindFirstInvisibleWithWork();
            if (invisibleWithWork != null)
            {
                XtraMessageBox.Show(string.Format("You cannot turn Technician off with Visits assigned ({0})", 
                                                  invisibleWithWork.Setting.Technician.DisplayName), 
                                    "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        #endregion     

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion

        #region OnCancel

        protected override bool OnCancel()
        {
            return true;
        }

        #endregion
    }
}
