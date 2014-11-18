using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using Microsoft.WindowsMobile.PocketOutlook;
using QuickBooksAgent.Domain;
using Account=Microsoft.WindowsMobile.PocketOutlook.Account;

namespace QuickBooksAgent.Windows.UI.Setup.Application
{
    public class ApplicationController : SingleFormController<ApplicationModel ,ApplicationView>
    {
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            View.m_cmbUserType.SelectedIndexChanged += new EventHandler(OnUserTypeChanged);
            View.m_cmbSettingsType.SelectedIndexChanged += new EventHandler(OnSettingsTypeChanged);            
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            base.OnViewLoad();


            View.m_chbTrace.Checked = Model.Configuration.Trace;

            if (Model.IsUserIdentificationAllowed)
            {
                if (Model.Configuration.UserType != null
                    && Model.Configuration.UserType != string.Empty)
                {
                    if (Model.Configuration.UserType == "Employee")
                        View.m_cmbUserType.SelectedIndex = 0;
                    else if (Model.Configuration.UserType == "Vendor")
                        View.m_cmbUserType.SelectedIndex = 1;
                }

                if (Model.Configuration.UserId != 0)
                {
                    if (View.m_cmbUserType.SelectedIndex == 0)
                        View.m_cmbUser.SelectedItem = new Employee(Model.Configuration.UserId);
                    else if (View.m_cmbUserType.SelectedIndex == 1)
                        View.m_cmbUser.SelectedItem = new Vendor(Model.Configuration.UserId);
                }

                View.m_chkUseUserIdentification.Checked = Model.Configuration.UseUserIdentification;
            }
            else
            {
                View.m_cmbUserType.Enabled = false;
                View.m_cmbUser.Enabled = false;
                View.m_chkUseUserIdentification.Enabled = false;
            }

            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(null, "All"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(14, "2 Weeks"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(30, "1 Month"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(60, "2 Months"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(90, "3 Months"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(180, "6 Months"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(365, "1 Year"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(730, "2 Years"));
            View.m_cmbTransactionHistory.Items.Add(new DatePeriod(1095, "3 Years"));
            
            View.m_cmbTransactionHistory.SelectedItem = new DatePeriod(Configuration.QuickBooks.TransactionFreshnessDays);
            if (View.m_cmbTransactionHistory.SelectedIndex == -1)
                View.m_cmbTransactionHistory.SelectedIndex = 0;
            
            
            //E-Mail            
            OutlookSession session = new OutlookSession();            
            foreach (EmailAccount account in session.EmailAccounts)
            {
                View.m_cmbOutlookAccount.Items.Add(account.Name);
            }
            session.Dispose();

            View.m_cmbOutlookAccount.Enabled = false;
            View.m_lblOutlookAccount.Enabled = false;
            View.m_txtSmtpServer.Enabled = false;
            View.m_lblSmtpServer.Enabled = false;
            View.m_txtSmtpPort.Enabled = false;
            View.m_lblSmtpPort.Enabled = false;
            View.m_txtEmailFrom.Enabled = false;
            View.m_lblEmailFrom.Enabled = false;

            View.m_cmbSettingsType.SelectedIndex = 0;

            if (Model.Configuration.EmailSettingsType != null)
                View.m_cmbSettingsType.SelectedIndex = Model.Configuration.EmailSettingsType.Value;

            View.m_cmbOutlookAccount.SelectedItem = Model.Configuration.OutlookAccount;
            View.m_txtSmtpServer.Text = Model.Configuration.SmtpServer;
            View.m_txtSmtpPort.Text 
                = Model.Configuration.SmtpPort.HasValue ? 
                  Model.Configuration.SmtpPort.ToString() : string.Empty;
            View.m_txtEmailFrom.Text = Model.Configuration.EmailFrom;
            

            InitDefaultAction("Cancel", true);            
            View.m_tabs.Focus();
        }

        #endregion

        #region OnDefaultAction

        public override void OnDefaultAction()
        {
            View.Destroy();
        }

        #endregion

        #region OnSave

        protected override bool OnSave()
        {
            Model.Configuration.Trace = View.m_chbTrace.Checked;
            Configuration.QuickBooks.TransactionFreshnessDays =
                ((DatePeriod)View.m_cmbTransactionHistory.SelectedItem).DaysCount;


            if (Model.IsUserIdentificationAllowed)
            {
                if (View.m_chkUseUserIdentification.Checked && View.m_cmbUser.SelectedIndex == -1)
                {
                    MessageDialog.Show(MessageDialogType.Information, "Please select Employee or Vendor");
                    View.m_cmbUser.Focus();
                    return false;
                }
                        
                if (View.m_cmbUser.SelectedIndex == -1)
                {
                    Model.Configuration.UserType = string.Empty;
                    Model.Configuration.UserId = 0;
                    Model.Configuration.UseUserIdentification = false;
                
                } else
                {
                    Model.Configuration.UseUserIdentification = View.m_chkUseUserIdentification.Checked;

                    if (Model.Configuration.UseUserIdentification)
                    {
                        Model.Configuration.UserType = View.m_cmbUserType.SelectedItem.ToString();

                        if (Model.Configuration.UserType == "Employee")
                            Model.Configuration.UserId = ((Employee)View.m_cmbUser.SelectedItem).EmployeeId;
                        else if (Model.Configuration.UserType == "Vendor")
                            Model.Configuration.UserId = ((Vendor)View.m_cmbUser.SelectedItem).VendorId;
                        
                    } else
                    {
                        if (Model.Configuration.UserId == 0) //Previos User wasn't set
                        {
                            if (DialogResult.Yes == MessageDialog.Show(MessageDialogType.Question, string.Format("Do you want to identify yourself as {0}?", View.m_cmbUser.SelectedItem.ToString())))
                            {
                                Model.Configuration.UserType = View.m_cmbUserType.SelectedItem.ToString();

                                if (Model.Configuration.UserType == "Employee")
                                    Model.Configuration.UserId = ((Employee)View.m_cmbUser.SelectedItem).EmployeeId;
                                else if (Model.Configuration.UserType == "Vendor")
                                    Model.Configuration.UserId = ((Vendor)View.m_cmbUser.SelectedItem).VendorId;

                                Model.Configuration.UseUserIdentification = true;                                
                            } else
                            {
                                Model.Configuration.UserType = string.Empty;
                                Model.Configuration.UserId = 0;                                
                            }
                        } else
                        {
                            Model.Configuration.UserType = string.Empty;
                            Model.Configuration.UserId = 0;                                                                                        
                        }
                        
                    }                                        
                }
            } 
            
            //Email settings validation
            if (View.m_cmbSettingsType.SelectedIndex < 1)
                Model.Configuration.EmailSettingsType = null;
            else if (View.m_cmbSettingsType.SelectedIndex == 1) //Outlook
            {
                if (View.m_cmbOutlookAccount.SelectedIndex < 0)
                {
                    if (View.m_cmbOutlookAccount.Items.Count > 0)
                    {
                        MessageDialog.Show(MessageDialogType.Information,
                                           "Please select one of Pocket Outlook accounts for sending mail");
                        View.m_cmbOutlookAccount.Focus();
                        return false;
                    } else
                    {
                        MessageDialog.Show(MessageDialogType.Information,
                                           "Please create an email account in Pocket Outlook for sending mail");
                        return false;                        
                    }
                    
                } else
                {
                    Model.Configuration.OutlookAccount = View.m_cmbOutlookAccount.SelectedItem.ToString();
                }
            }
            else if (View.m_cmbSettingsType.SelectedIndex == 2) //Custom
            {
                if (View.m_txtSmtpServer.Text == string.Empty)
                {
                    MessageDialog.Show(MessageDialogType.Information,
                                       "Please enter SMTP server address for outgoing mail");
                    View.m_txtSmtpServer.Focus();
                    return false;                                            
                } else
                {
                    Model.Configuration.SmtpServer = View.m_txtSmtpServer.Text;
                }
                
                if (View.m_txtSmtpPort.Text == string.Empty)
                {
                    MessageDialog.Show(MessageDialogType.Information,
                                       "Please enter SMTP server port for outgoing mail");
                    View.m_txtSmtpPort.Focus();
                    return false;                                            
                } else
                {
                    int port = -1;
                    try
                    {
                        port = int.Parse(View.m_txtSmtpPort.Text);
                    }
                    catch (Exception)
                    {
                        MessageDialog.Show(MessageDialogType.Information,
                                           "Please enter valid SMTP server port for outgoing mail");
                        View.m_txtSmtpPort.SelectAll();
                        View.m_txtSmtpPort.Focus();
                        return false;                                                                    
                    }

                    if (port < 0 || port > 65535)
                    {
                        MessageDialog.Show(MessageDialogType.Information,
                                           "Please enter valid SMTP server port for outgoing mail");
                        View.m_txtSmtpPort.SelectAll();
                        View.m_txtSmtpPort.Focus();
                        return false;                                                                                            
                    }

                    Model.Configuration.SmtpPort = port;                                        
                }
                
                if (View.m_txtEmailFrom.Text == string.Empty)
                {
                    MessageDialog.Show(MessageDialogType.Information,
                                       "Please enter Email From for outgoing mail to let recipients identify email sender");
                    View.m_txtEmailFrom.Focus();
                    return false;                                                                
                } else
                {
                    Regex regex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                    if (!regex.IsMatch(View.m_txtEmailFrom.Text))
                    {
                        MessageDialog.Show(MessageDialogType.Information,
                                           "Please enter valid Email From for outgoing mail to let recipients identify email sender");
                        View.m_txtEmailFrom.SelectAll();
                        View.m_txtEmailFrom.Focus();
                        return false;                                                                                        
                    }

                    Model.Configuration.EmailFrom = View.m_txtEmailFrom.Text;
                }
            }

            Model.Configuration.EmailSettingsType = View.m_cmbSettingsType.SelectedIndex;
            
                try
                {
                    using (WaitCursor waitCursor = new WaitCursor())
                    {
                        Configuration.Save();
                    }
                }
                catch (Exception e)
                {
                    EventService.AddEvent(new QuickBooksAgentException("Unable to save config file", e));

                    return false;
                }

            return true;
        }

        #endregion

        #region OnUserTypeChanged

        private void OnUserTypeChanged(object sender, EventArgs e)
        {
            View.m_cmbUser.Items.Clear();
            
            if (View.m_cmbUserType.SelectedIndex == 0) //Employee
            {
                foreach (Employee employee in Model.Employees)
                    View.m_cmbUser.Items.Add(employee);
                
            } else if (View.m_cmbUserType.SelectedIndex == 1) // Vendor
            {
                foreach (Vendor vendor in Model.Vendors)
                    View.m_cmbUser.Items.Add(vendor);
                
            } 
                
        }

        #endregion

        #region OnSettingsTypeChanged

        private void OnSettingsTypeChanged(object sender, EventArgs e)
        {
            if (View.m_cmbSettingsType.SelectedIndex == 1)
            {
                View.m_cmbOutlookAccount.Enabled = true;                
                View.m_lblOutlookAccount.Enabled = true;                
                View.m_txtSmtpServer.Enabled = false;
                View.m_lblSmtpServer.Enabled = false;
                View.m_txtSmtpPort.Enabled = false;
                View.m_lblSmtpPort.Enabled = false;
                View.m_txtEmailFrom.Enabled = false;
                View.m_lblEmailFrom.Enabled = false;                
                
            } else if (View.m_cmbSettingsType.SelectedIndex == 2)
            {
                View.m_cmbOutlookAccount.Enabled = false;
                View.m_lblOutlookAccount.Enabled = false;
                View.m_txtSmtpServer.Enabled = true;
                View.m_lblSmtpServer.Enabled = true;
                View.m_txtSmtpPort.Enabled = true;
                View.m_lblSmtpPort.Enabled = true;
                View.m_txtEmailFrom.Enabled = true;
                View.m_lblEmailFrom.Enabled = true;                                
            } else
            {
                View.m_cmbOutlookAccount.Enabled = false;
                View.m_lblOutlookAccount.Enabled = false;
                View.m_txtSmtpServer.Enabled = false;
                View.m_lblSmtpServer.Enabled = false;
                View.m_txtSmtpPort.Enabled = false;
                View.m_lblSmtpPort.Enabled = false;
                View.m_txtEmailFrom.Enabled = false;
                View.m_lblEmailFrom.Enabled = false;
            }
                
        }

        #endregion
    }
}
