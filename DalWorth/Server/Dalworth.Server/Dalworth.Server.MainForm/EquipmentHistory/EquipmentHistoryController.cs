using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.MainForm;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;

namespace Dalworth.Server.MainForm.EquipmentHistory
{
    public class EquipmentHistoryController : Controller<EquipmentHistoryModel, EquipmentHistoryView>
    {
        #region SelectedEquipment

        private EquipmentHistoryWrapper SelectedEquipment
        {
            get
            {
                return (EquipmentHistoryWrapper) View.m_gridViewEquipmentHistory.GetRow(
                    View.m_gridViewEquipmentHistory.FocusedRowHandle);
            }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.SelectedEquipments = (IList<EquipmentWrapper>) data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnClose.Click += OnCloseClick;            
            View.m_cmbHistoryDepth.SelectedIndexChanged += OnHistoryDepthChanged;
            View.m_linkTransaction.Click += OnLinkTransactionClick;
            View.m_gridViewEquipmentHistory.CustomRowCellEdit += OnGridCellEdit;
            View.m_gridViewEquipmentHistory.ShowingEditor += OnGridShowingEditor;
            View.m_gridViewEquipmentHistory.KeyDown += OnEquipmentHistoryKeyDown;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            foreach (Area area in Model.Areas)
                View.m_cmbArea.Items.Add(new ImageComboBoxItem(area.Name, (object)area.ID));

            View.m_gridEquipmentHistory.DataSource = Model.EquipmentHistory;
        }

        #endregion

        #region OnEquipmentHistoryKeyDown

        private void OnEquipmentHistoryKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return
                && View.m_gridViewEquipmentHistory.FocusedColumn.Name == View.m_colTransaction.Name)
            {
                OnLinkTransactionClick(null, null);
            }
            
        }

        #endregion


        #region OnLinkTransactionClick

        private void OnLinkTransactionClick(object sender, EventArgs e)
        {
            EquipmentHistoryWrapper selectedEquipment = SelectedEquipment;
            if (selectedEquipment.WorkTransaction == null)
                return;

            MainFormController mainFormController = (MainFormController) Configuration.MainFormController;

            if (selectedEquipment.WorkTransaction.WorkTransactionType == WorkTransactionTypeEnum.StartDayDone
                || selectedEquipment.WorkTransaction.WorkTransactionType == WorkTransactionTypeEnum.Completed)
            {
                Work work = Work.FindByPrimaryKey(selectedEquipment.WorkTransaction.WorkId);
                mainFormController.ShowWorksForm(work);

            } else if (selectedEquipment.WorkTransaction.WorkTransactionType == WorkTransactionTypeEnum.VisitEquipmentTransfer)
            {
                Visit visit = Visit.FindByPrimaryKey(selectedEquipment.WorkTransaction.VisitId.Value);
                mainFormController.ShowVisitsForm(visit);
            }
        }

        #endregion

        #region OnGridShowingEditor

        private void OnGridShowingEditor(object sender, CancelEventArgs e)
        {
            if (View.m_gridViewEquipmentHistory.FocusedColumn.Name != View.m_colTransaction.Name)
                return;

            EquipmentHistoryWrapper historyWrapper =
                (EquipmentHistoryWrapper)View.m_gridViewEquipmentHistory.GetRow(View.m_gridViewEquipmentHistory.FocusedRowHandle);
            if (historyWrapper.WorkTransaction == null)
                e.Cancel = true;
        }

        #endregion

        #region OnGridCellEdit

        private void OnGridCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.Name != View.m_colTransaction.Name)
                return;

            EquipmentHistoryWrapper historyWrapper =
                (EquipmentHistoryWrapper)View.m_gridViewEquipmentHistory.GetRow(e.RowHandle);
            if (historyWrapper.WorkTransaction == null)
                e.RepositoryItem = View.m_txtTransaction;
        }

        #endregion

        #region OnHistoryDepthChanged

        private void OnHistoryDepthChanged(object sender, EventArgs e)
        {
            View.m_gridEquipmentHistory.BeginUpdate();
            DateTime? date = null;
            if ((int)View.m_cmbHistoryDepth.EditValue != 0)
                date = DateTime.Now.AddDays(-1*(int) View.m_cmbHistoryDepth.EditValue);

            Model.InitHistory(date);
            View.m_gridEquipmentHistory.DataSource = Model.EquipmentHistory;
            View.m_gridEquipmentHistory.EndUpdate();                        
        }

        #endregion

        #region OnCloseClick

        private void OnCloseClick(object sender, EventArgs e)
        {
            View.Destroy();
        }

        #endregion       
    }
}
