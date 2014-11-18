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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SmartSchedule.Data;
using SmartSchedule.Domain;
using SmartSchedule.SDK;
using SmartSchedule.Win32.Controls;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Win32.VisitAdd;
using SmartSchedule.Windows;
using Point=SmartSchedule.Domain.Point;

namespace SmartSchedule.Win32.HistoricalOrders
{
    public class HistoricalOrdersController : Controller<HistoricalOrdersModel, HistoricalOrdersView>
    {
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.BookingEngine = (BookingEngine) data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion        

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_dtpDateSchedule.DateTimeChanged += OnDateChanged;
            View.m_btnClose.Click += OnCancelClick;
            View.m_gridViewOrders.DoubleClick += OnOrdersDoubleClick;
            View.m_btnAdd.Click += OnAddClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.Model = Model;
            View.m_gridOrders.DataSource = Model.Orders;
        }

        #endregion

        #region OnDateChanged

        private void OnDateChanged(object sender, EventArgs e)
        {
            if (View.m_dtpDateSchedule.EditValue == null)
                Model.RefreshOrders(null);
            else
                Model.RefreshOrders(View.m_dtpDateSchedule.DateTime);
        }

        #endregion

        #region OnAddClick

        private void OnAddClick(object sender, EventArgs e)
        {
            ProcessOrders();
        }

        #endregion

        #region OnOrdersDoubleClick

        private void OnOrdersDoubleClick(object sender, EventArgs e)
        {
            ProcessOrders();
        }

        #endregion

        #region ProcessOrders

        private void ProcessOrders()
        {
            if (View.m_gridViewOrders.FocusedRowHandle < 0)
                return;

            if (View.m_gridViewOrders.SelectedRowsCount == 1)
            {
                OrderHistory order = (OrderHistory)View.m_gridViewOrders.GetRow(
                    View.m_gridViewOrders.FocusedRowHandle);

                using (VisitAddController controller = Prepare<VisitAddController>(Model.BookingEngine, order))
                {
                    controller.Execute(false);
                }                                                    

            } else if (View.m_gridViewOrders.SelectedRowsCount > 1)
            {
                MainFormView mainForm = null;
                foreach (Form form in Application.OpenForms)
                {
                    if (form is MainFormView)
                    {
                        mainForm = (MainFormView)form;
                        break;
                    }
                }

                if (mainForm == null)
                    throw new DalworthException("Main form not found");

                using (new WaitCursor())
                {
                    int[] selectedRowHandles = View.m_gridViewOrders.GetSelectedRows();
                    foreach (int handle in selectedRowHandles)
                    {
                        OrderHistory order = (OrderHistory)View.m_gridViewOrders.GetRow(handle);
                        if (order.TimeFrameId == null || order.Cost == decimal.Zero)
                            continue;

                        Visit newVisit = Model.BookingEngine.GetNewVisit(order);

                        try
                        {
                            Database.Begin();
                            Model.BookingEngine.InsertVisit(newVisit);
                            Database.Commit();
                        }
                        catch (Exception)
                        {
                            Database.Rollback();
                            throw;
                        }
                    }                    
                }

                View.RefreshData();
            }            
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            View.Destroy();
        }

        #endregion   
    }
}
