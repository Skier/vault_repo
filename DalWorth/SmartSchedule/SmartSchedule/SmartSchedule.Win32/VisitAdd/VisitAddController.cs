using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SmartSchedule.Data;
using SmartSchedule.Domain;
using SmartSchedule.Domain.Sync;
using SmartSchedule.Win32.Controls;
using SmartSchedule.Win32.HistoricalOrders;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;
using Point=SmartSchedule.Domain.Point;

namespace SmartSchedule.Win32.VisitAdd
{
    public class VisitAddController : Controller<VisitAddModel, VisitAddView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region SelectedItem

        private RecommendationResponseItem m_selectedItem;
        public RecommendationResponseItem SelectedItem
        {
            get { return m_selectedItem; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.Request = (RecommendationRequest)data[0];
            base.OnModelInitialize(data);
        }

        #endregion


        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnCancel.Click += OnCancelClick;
            View.m_gridRecommedationsView.DoubleClick += OnGridDoubleClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_lblTicketNumber.Text = Model.Request.TicketNumber;
            View.m_gridRecommedations.DataSource = Model.ResponseItems;
            Debug.Assert(Model.ResponseItems.Count == 65, "Predictions count is not 65");
        }

        #endregion

        #region OnGridDoubleClick

        private void OnGridDoubleClick(object sender, EventArgs args)
        {
            RecommendationResponseItem item = (RecommendationResponseItem)View.m_gridRecommedationsView.GetRow(
                View.m_gridRecommedationsView.FocusedRowHandle);
            
            m_selectedItem = item;
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
