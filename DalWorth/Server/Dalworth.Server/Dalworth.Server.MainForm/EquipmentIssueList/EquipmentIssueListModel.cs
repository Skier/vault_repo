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
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors.DXErrorProvider;

namespace Dalworth.Server.MainForm.EquipmentIssueList
{
    public class EquipmentIssueListModel
    {
        #region EquipmentIssues

        private BindingList<EquipmentIssue> m_equipmentIssues;
        public BindingList<EquipmentIssue> EquipmentIssues
        {
            get { return m_equipmentIssues; }
            set { m_equipmentIssues = value; }
        }

        #endregion
    }
}
