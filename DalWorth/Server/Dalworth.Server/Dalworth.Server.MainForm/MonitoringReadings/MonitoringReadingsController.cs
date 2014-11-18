using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;

namespace Dalworth.Server.MainForm.MonitoringReadings
{
    public class MonitoringReadingsController : Controller<MonitoringReadingsModel, MonitoringReadingsView>
    {
        #region IsReadingsExist

        public bool IsReadingsExist
        {
            get
            {
                return Model.Readings.Count > 0;
            }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.DefloodTask = (Task) data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnClose.Click += OnCloseClick;
            View.m_gridReadingsView.CustomColumnDisplayText += OnGridReadingsColumnDisplayText;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            ReadingHistory lastReading = Model.Readings[Model.Readings.Count - 1];

            for (int i = 0; i < lastReading.Readings.Count; i++)
            {
                GridColumn column = new GridColumn();
                column.Caption = lastReading.Readings[i].MonitoringReadingTypeText;
                column.Visible = true;
                column.VisibleIndex = i + 2;
                View.m_gridReadingsView.Columns.Add(column);                
            }

            View.m_gridReadings.DataSource = Model.Readings;
        }

        #endregion

        #region OnGridReadingsColumnDisplayText

        private void OnGridReadingsColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.Name != View.m_colTechnician.Name
                && e.Column.Name != View.m_colDate.Name)
            {
                ReadingHistory readingHeader = Model.Readings[e.ListSourceRowIndex];

                if (e.Column.VisibleIndex - 2 >= readingHeader.Readings.Count)
                    e.DisplayText = string.Empty;
                else
                    e.DisplayText = readingHeader.Readings[e.Column.VisibleIndex - 2].ReadingText;
            }
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
