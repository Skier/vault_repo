using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.ServmanSync;
using Dalworth.Server.Servman.Domain;
using Dalworth.Server.Windows;
using Timer=System.Threading.Timer;

namespace Dalworth.Server.Servman.Win32.MainForm
{
    public class MainFormController : Controller<MainFormModel, MainFormView>
    {
        #region Form

        public Form Form
        {
            get { return View; }
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnImport.Click += OnImportClick;
            View.m_btnExport.Click += OnExportClick;
            View.m_btnFirstTimeSync.Click += OnFirstTimeSyncClick;
            View.m_btnCustomerImport.Click += OnCustomerImportClick;
            View.m_btnImportTechnicians.Click += OnImportTechniciansClick;
            View.m_btnBackgroundJobs.Click += OnProcessBackgroundJobsClick;
        }

        #endregion

        #region OnFirstTimeSyncClick

        private void OnFirstTimeSyncClick(object sender, EventArgs e)
        {
            string filePath = @"FirstTimeSync.xml";

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Unable to find file FirstTimeSync.xml", "No XML file", MessageBoxButtons.OK, MessageBoxIcon.Error);                
                return;
            }

            FirstTimeOrder[] orders = new FirstTimeOrder[0];

            try
            {
                using (new WaitCursor())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(FirstTimeOrder[]));
                    using (StreamReader reader = new StreamReader(filePath))
                        orders = (FirstTimeOrder[])serializer.Deserialize(reader);                    
                }

            }
            catch (Exception ex)
            {
                string errorText = ex.Message;
                if (ex.InnerException != null)
                    errorText += "\r\n" + ex.InnerException.Message;

                MessageBox.Show(errorText, "Error parsing XML", MessageBoxButtons.OK, MessageBoxIcon.Error);                
                return;
            }

            
            string errors = string.Empty;
            foreach (FirstTimeOrder order in orders)
            {
                if (order.TicketNumber == string.Empty)
                {
                    errors += "XML document contains elements without ticket numbers\r\n";
                    break;
                }
            }

            foreach (FirstTimeOrder order in orders)
            {
                if (order.TicketNumber != string.Empty && order.GetError() != string.Empty)
                    errors += order.GetError() + "\r\n";
            }

            if (errors != string.Empty)
            {
                MessageBox.Show(errors, "Error parsing XML", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;                
            }

            using (new WaitCursor())
            {                
                ImportModel.ImportOrdersFirstTime(orders);
            }

            MessageBox.Show("First time import done", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region OnCustomerImportClick

        private void OnCustomerImportClick(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                ImportModel.ImportCustomers();                
            }

            MessageBox.Show("Customer import completed", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion


        #region OnExportClick

        private void OnExportClick(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                DalworthExportModel.ExportCustomers();
                DalworthExportModel.ExportOrders();
            }

            MessageBox.Show("Data export completed", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region OnImportClick

        private void OnImportClick(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                ImportModel.ImportCustomers();
                ImportModel.ImportOrders();
            }

            MessageBox.Show("Data import completed", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region OnImportTechniciansClick

        private void OnImportTechniciansClick(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                ImportModel.ImportUpdateAllTechnicians();
            }

            MessageBox.Show("Data import completed", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);            
        }

        #endregion

        #region OnProcessBackgroundJobsClick

        private void OnProcessBackgroundJobsClick(object sender, EventArgs e)
        {
            using (new WaitCursor())
            {
                BackgroundJobPendingModel.ProcessPendingJobs();
            }
        }

        #endregion
    }
}
