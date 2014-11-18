using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SmartSchedule.Data;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.Win32.Controls;
using SmartSchedule.Win32.HistoricalOrders;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;
using Point=SmartSchedule.Domain.Point;

namespace SmartSchedule.Win32.SnapshotChange
{
    public class SnapshotChangeController : Controller<SnapshotChangeModel, SnapshotChangeView>
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
            base.OnModelInitialize(data);
        }

        #endregion


        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnClose.Click += OnCloseClick;
            View.m_btnPrint.Click += OnPrintClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_gridSnapshotChanges.DataSource = Model.Changes;
            View.m_btnPrint.Enabled = Model.Changes.Count > 0;
        }

        #endregion

        #region OnPrintClick

        private void OnPrintClick(object sender, EventArgs args)
        {
            if (View.m_gridSnapshotChangesView.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("Please select tickets to print", "Nothing selected", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);                
                return;
            }

            foreach (int rowHandle in View.m_gridSnapshotChangesView.GetSelectedRows())
            {
                VisitChangeItem item = (VisitChangeItem)View.m_gridSnapshotChangesView.GetRow(rowHandle);
                item.Print();
            }
        }

        #endregion

        #region OnCloseClick

        private void OnCloseClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion   
    }
}
