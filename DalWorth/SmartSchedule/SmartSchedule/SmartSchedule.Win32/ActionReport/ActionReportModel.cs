using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.ActionReport
{
    public class ActionReportModel : IModel
    {
        #region Init

        public void Init()
        {
        }

        #endregion

        #region GetTechnicians

        public List<Technician> GetTechnicians()
        {
            return WcfClient.WcfClient.Instance.GetTechnicians(DateTime.MinValue, true, true);
        }

        #endregion

    }
}
