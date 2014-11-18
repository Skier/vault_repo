using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Dalworth.Data;
using Dalworth.Domain.SyncService;
using Dalworth.Windows;
using Dalworth.Windows.StartDay.WorkSummary;
using Employee=Dalworth.Domain.Employee;

namespace Dalworth.Windows.StartDay.Login
{
    public class LoginController : StartDayBaseController<LoginModel, LoginView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
        }

        #endregion

        #region OnViewActivated

        public override void OnViewActivated()
        {
            if (Model.StartDayModel.IsStartDayDone() || Model.StartDayModel.IsStartDayCancelled)
                View.Destroy();
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            foreach (Employee employee in Model.Employees)
            {
                View.m_cmbTechnician.Items.Add(employee);
            }

            View.m_cmbTechnician.Focus();
        }

        #endregion

        #region OnSave

        protected override bool OnSave()
        {
            return true;
        }

        #endregion

        #region Back Next

        public override bool IsLeftActionExist
        {
            get { return true; }            
        }

        public override string LeftActionName
        {
            get { return "Cancel"; }            
        }

        public override void OnLeftAction()
        {
            View.Destroy();
        }

        public override bool IsRightActionExist
        {
            get { return true; }            
        }

        public override string RightActionName
        {
            get { return "Next"; }
        }

        public override void OnRightAction()
        {
            if (View.m_cmbTechnician.SelectedIndex < 0)
            {
                MessageDialog.Show(MessageDialogType.Information,
                                   "Please select your name from the list.");
                View.m_cmbTechnician.Focus();
                return;
            }

            if (View.m_txtPassword.Text == string.Empty)
            {
                MessageDialog.Show(MessageDialogType.Information,
                                   "Please enter your password.");
                View.m_txtPassword.Focus();
                return;
            }

            Employee employee = (Employee)View.m_cmbTechnician.SelectedItem;

            if (Model.IsPasswordCorrect(employee, View.m_txtPassword.Text))
            {
                bool isWorkExist;
                try
                {
                    using (new WaitCursor())
                    {
                        isWorkExist = Model.IsWorkExist(employee);
                    }
                }
                catch (WebException ex)
                {
                    MessageDialog.Show(MessageDialogType.Warning,
                            "Couldn't establish connection to the server. Please check your connection availability and try again.");
                    Host.Trace("LoginController::OnRightAction", ex.Message + ex.StackTrace);
                    return;
                }
                catch (Exception ex)
                {
                    MessageDialog.Show(MessageDialogType.Warning,
                            "Unknown application error. Please contact dispatch");
                    Host.Trace("LoginController::OnRightAction", ex.Message + ex.StackTrace);
                    return;
                }

                if (isWorkExist)
                {
                    StartDayPackage package;

                    try
                    {
                        using (new WaitCursor())
                        {
                            package = Model.GetStartDayPackage(employee);
                        }
                    }
                    catch (WebException ex)
                    {
                        MessageDialog.Show(MessageDialogType.Warning,
                                "Couldn't establish connection to the server. Please check your connection availability and try again.");
                        Host.Trace("LoginController::OnRightAction", ex.Message + ex.StackTrace);
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageDialog.Show(MessageDialogType.Warning,
                                "Unknown application error. Please contact dispatch");
                        Host.Trace("LoginController::OnRightAction", ex.Message + ex.StackTrace);
                        return;
                    }

                    try
                    {
                        Database.Begin();
                        Model.SaveStartDayPackage(package);
                        Database.Commit();
                    }
                    catch (Exception)
                    {
                        Database.Rollback();
                        throw;
                    }

                    Model.StartDayModel.Technician = employee;
                    Next();
                }
                else
                {
                    MessageDialog.Show(MessageDialogType.Information,
                        "There is no work scheduled for you today. Please contact dispatch.");
                    View.Destroy();
                }
            }
            else
            {
                MessageDialog.Show(MessageDialogType.Information,
                                   "You have entered incorrect password. Please try again.");
                View.m_txtPassword.Text = string.Empty;
                View.m_txtPassword.Focus();
                return;
            }                 
        }

        #endregion
    }
}
