using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.Windows.UI.Setup.Register
{
    public class RegisterController : SingleFormController<Configuration.QuickBooksConfiguration, RegisterView>
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            View.m_txtLicense1.TextChanged += new EventHandler(OnLicenseChanged);
            View.m_txtLicense2.TextChanged += new EventHandler(OnLicenseChanged);
            View.m_txtLicense3.TextChanged += new EventHandler(OnLicenseChanged);
            View.m_txtLicense4.TextChanged += new EventHandler(OnLicenseChanged);
            
            View.m_txtLicense1.KeyPress += new KeyPressEventHandler(OnLicenseKeyPress);
            View.m_txtLicense2.KeyPress += new KeyPressEventHandler(OnLicenseKeyPress);
            View.m_txtLicense3.KeyPress += new KeyPressEventHandler(OnLicenseKeyPress);
            View.m_txtLicense4.KeyPress += new KeyPressEventHandler(OnLicenseKeyPress);                        

            IsDefaultActionExist = true;
            DefaultActionName = "Cancel";                                                
        }

        public override void OnViewLoad()
        {
            if (Configuration.App.License != string.Empty)
            {
                try
                {
                    View.m_txtLicense1.Text = Configuration.App.License.Substring(0, 5);
                    View.m_txtLicense2.Text = Configuration.App.License.Substring(6, 5);
                    View.m_txtLicense3.Text = Configuration.App.License.Substring(12, 5);
                    View.m_txtLicense4.Text = Configuration.App.License.Substring(18, 4);
                }
                catch (Exception){}
            }

            View.m_txtFirstName.Text = Configuration.App.FirstName;
            View.m_txtLastName.Text = Configuration.App.LastName;
            View.m_txtCompany.Text = Configuration.App.Company;
            
            if (Configuration.App.IsLicensed())
            {
                View.m_txtLicense1.ReadOnly =
                    View.m_txtLicense2.ReadOnly =
                    View.m_txtLicense3.ReadOnly =
                    View.m_txtLicense4.ReadOnly =
                    View.m_txtFirstName.ReadOnly =
                    View.m_txtLastName.ReadOnly =
                    View.m_txtCompany.ReadOnly = true;

                View.m_lblApplicationRegistered.Visible = true;
            } else
                View.m_lblApplicationRegistered.Visible = false;
            
            View.m_txtLicense1.Focus();                
        }

        public override void OnDefaultAction()
        {
            View.Destroy();
        }

        private void OnLicenseKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b')
            {
                TextBox textBox = (TextBox)sender;
                                                
                if (textBox.Text.Length == 0)
                {
                    if (textBox.Name == "m_txtLicense4")
                    {
                        View.m_txtLicense3.SelectAll();
                        View.m_txtLicense3.Focus();
                    }
                    else if (textBox.Name == "m_txtLicense3")
                    {
                        View.m_txtLicense2.SelectAll();
                        View.m_txtLicense2.Focus();
                    }
                    else if (textBox.Name == "m_txtLicense2")
                    {
                        View.m_txtLicense1.SelectAll();
                        View.m_txtLicense1.Focus();
                    }
                }
                
            }
        }

        private void OnLicenseChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox) sender;
            
            if (textBox.Text.Length >= 5)
            {
                if (textBox.Name == "m_txtLicense1")
                {
                    View.m_txtLicense2.SelectAll();
                    View.m_txtLicense2.Focus();
                }                    
                else if (textBox.Name == "m_txtLicense2")
                {
                    View.m_txtLicense3.SelectAll();
                    View.m_txtLicense3.Focus();                    
                }
                else if (textBox.Name == "m_txtLicense3")
                {
                    View.m_txtLicense4.SelectAll();
                    View.m_txtLicense4.Focus();                    
                }
            }
        }

        protected override bool OnSave()
        {
            if (Configuration.App.IsLicensed())
                return true;
            
            if (!IsValid())
                return false;
            
            string licenseNumber = string.Format("{0}-{1}-{2}-{3}", View.m_txtLicense1.Text,
                 View.m_txtLicense2.Text, View.m_txtLicense3.Text, View.m_txtLicense4.Text);
            
            if (Configuration.App.IsLicenseValid(licenseNumber, View.m_txtFirstName.Text, 
                View.m_txtLastName.Text, View.m_txtCompany.Text))
            {
                Configuration.App.License = licenseNumber;
                Configuration.App.FirstName = View.m_txtFirstName.Text;
                Configuration.App.LastName = View.m_txtLastName.Text;
                Configuration.App.Company = View.m_txtCompany.Text;

                try
                {
                    using (new WaitCursor())
                    {
                        Configuration.Save();
                    }

                    MessageBox.Show("Registration completed. Thank you for using Q-Agent.");
                    return true;
                }
                catch (Exception e)
                {
                    EventService.AddEvent(new QuickBooksAgentException("Unable to save config file", e));
                    return false;
                }                
                
            } else
            {
                MessageBox.Show("You have entered wrong license number. Please try again");
                View.m_txtLicense1.SelectAll();
                View.m_txtLicense1.Focus();
                return false;                
            }
        }

        private bool IsValid()
        {
            if (View.m_txtLicense1.Text.Length != 5)
            {
                MessageBox.Show("Please enter License Number");
                View.m_txtLicense1.SelectAll();
                View.m_txtLicense1.Focus();
                return false;
            } else if (View.m_txtLicense2.Text.Length != 5)
            {
                MessageBox.Show("Please enter License Number");
                View.m_txtLicense2.SelectAll();
                View.m_txtLicense2.Focus();
                return false;
                
            }else if (View.m_txtLicense3.Text.Length != 5)
            {
                MessageBox.Show("Please enter License Number");
                View.m_txtLicense3.SelectAll();
                View.m_txtLicense3.Focus();
                return false;
                
            }else if (View.m_txtLicense4.Text.Length != 4)
            {
                MessageBox.Show("Please enter License Number");
                View.m_txtLicense4.SelectAll();
                View.m_txtLicense4.Focus();
                return false;                
            }else if (View.m_txtFirstName.Text.Length == 0)
            {
                MessageBox.Show("Please enter First Name");
                View.m_txtFirstName.SelectAll();
                View.m_txtFirstName.Focus();
                return false;                
            }

            return true;
        }
    }
}
