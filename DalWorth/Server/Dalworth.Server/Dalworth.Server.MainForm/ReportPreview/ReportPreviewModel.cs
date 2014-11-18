using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ReportPreview
{
    public class ReportPreviewModel : IModel
    {
        #region ReportDocument

        private ReportClass m_reportDocument;
        public ReportClass ReportDocument
        {
            get { return m_reportDocument; }
            set { m_reportDocument = value; }
        }

        #endregion

        #region Init

        public void Init()
        {
        }

        #endregion
    }
}
