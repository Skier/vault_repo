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

namespace SmartSchedule.Win32.RouteAnalyze
{
    public class RouteAnalyzeController : Controller<RouteAnalyzeModel, RouteAnalyzeView>
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
            Model.TechnicianId = (int)data[0];
            Model.CurrentDrive = (double)data[1];
            base.OnModelInitialize(data);            
        }

        #endregion


        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnClose.Click += OnCloseClick;
//            View.m_btnPrint.Click += OnPrintClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_gridRouteChanges.DataSource = Model.OptimizeOptions;
            View.m_lblCurrentRpm.Text = Model.CurrentDrive.ToString("0") + " mi";
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
