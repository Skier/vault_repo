using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.MainForm;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;

namespace Dalworth.Server.MainForm.ReportPreview
{
    public class ReportPreviewController : Controller<ReportPreviewModel, ReportPreviewView>
    {        
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.ReportDocument = (ReportClass) data[0];
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_reportViewer.ReportSource = Model.ReportDocument;            
        }

        #endregion
    }
}
