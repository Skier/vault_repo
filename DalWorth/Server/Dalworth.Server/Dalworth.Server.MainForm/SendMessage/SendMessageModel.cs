using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Windows.Forms;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;

namespace Dalworth.Server.MainForm.SendMessage
{
    public class SendMessageModel : IModel
    {
        #region Work

        private Work m_work;
        public Work Work
        {
            get { return m_work; }
            set { m_work = value; }
        }

        #endregion

        #region Technician

        private Employee m_technician;
        public Employee Technician
        {
            get { return m_technician; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_technician = Employee.FindByPrimaryKey(m_work.TechnicianEmployeeId);
        }

        #endregion

        #region Send

        public void Send(string message)
        {
            Van van = Van.FindByPrimaryKey(m_work.VanId.Value);

            while (true)
            {
                try
                {
                    PagerMessage.SendViaWebService(van.ServmanTruckId, message);
                    return;
                }
                catch (Exception ex)
                {
                    Host.Trace("SendMessageModel:Send", "Error: " + ex);

                    if (XtraMessageBox.Show("Unable to send visit to pager. Try again?", "Pager send error",
                        MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }

            
        }

        #endregion
    }
}
