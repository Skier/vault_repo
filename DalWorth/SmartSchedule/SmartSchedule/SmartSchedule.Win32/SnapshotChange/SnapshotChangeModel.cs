using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.SnapshotChange
{
    public class SnapshotChangeModel : IModel
    {
        #region Changes

        private List<VisitChangeItem> m_changes;
        public List<VisitChangeItem> Changes
        {
            get { return m_changes; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_changes = WcfClient.WcfClient.Instance.GetSnapshotChanges();
        }

        #endregion        
    }
}
