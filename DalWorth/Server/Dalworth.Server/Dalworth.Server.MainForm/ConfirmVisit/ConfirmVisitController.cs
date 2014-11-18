using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.MainForm.Components;
using Dalworth.Server.MainForm.Dashboard;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraScheduler;

namespace Dalworth.Server.MainForm.ConfirmVisit
{
    public enum ConfirmVisitResultEnum
    {
        Confirm,
        Continue,
        Cancel
    }

    public class ConfirmVisitController : Controller<ConfirmVisitModel, ConfirmVisitView>
    {
        private bool m_isCustomerModified;
        private bool m_isAddressModified;

        private enum TimeFrameTemplateEnum
        {
            Custom = 1,
            InAM = 2,
            InPM = 3
        }

        #region Result

        private ConfirmVisitResultEnum m_result;
        public ConfirmVisitResultEnum Result
        {
            get { return m_result; }
        }

        #endregion        

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.Appointment = (AppointmentWrapper) data[0];
            Model.CurrentDispatch = (Employee)data[1];
            if (data.Length > 2)
                Model.DispatchTime = (DateTime?) data[2];            

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnConfirm.Click += OnConfirmClick;
            View.m_btnDispatchWithoutConfirm.Click += OnDispatchWithoutConfirmClick;
            View.m_btnCancel.Click += OnCancelClick;
            View.m_ctlCustomer.Modified += OnCustomerModified;
            View.m_ctlAddress.Modified += OnAddressModified;

            View.m_dtpPrefferedTimeFrameBegin.Validating += OnTimeFrameValidating;
            View.m_dtpPrefferedTimeFrameEnd.Validating += OnTimeFrameValidating;
            View.m_dtpConfirmTimeEnd.Validating += OnConfirmTimeEndValidating;

            View.m_cmbTimeFrameTemplate.SelectedIndexChanged += OnTimeFrameTemplateChanged;
            View.m_dtpPrefferedTimeFrameBegin.EditValueChanged += OnTimeFrameChanged;
            View.m_dtpPrefferedTimeFrameEnd.EditValueChanged += OnTimeFrameChanged;

            View.m_dtpConfirmTimeStart.TimeChanged += OnConfirmTimeStartChanged;                        

            View.m_chkLeftMessage.CheckedChanged += OnLeftMessageCheckedChanged;
            View.m_chkBusy.CheckedChanged += OnBusyCheckedChanged;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {            
            Visit visit = Model.Appointment.Visit;

            if (Model.DispatchTime.HasValue)
            {
                View.m_btnConfirm.Location = new Point(498, View.m_btnConfirm.Location.Y);
                View.m_btnDispatchWithoutConfirm.Visible = true;

                List<DispatchNotificationReasonEnum> reasons 
                    = visit.GetConfirmationReasons(Model.DispatchTime.Value);
                View.m_lblNotificationReasons.Text = string.Empty;

                foreach (DispatchNotificationReasonEnum reason in reasons)
                {
                    if (reason == DispatchNotificationReasonEnum.OutOfTimeFrame)
                        View.m_lblNotificationReasons.Text += "  * Estimated arrival time is out of confirmed time frame\n";
                    else if (reason == DispatchNotificationReasonEnum.LeftMessage)
                        View.m_lblNotificationReasons.Text += "  * Left a message on last confirmation\n";
                    else if (reason == DispatchNotificationReasonEnum.Busy)
                        View.m_lblNotificationReasons.Text += "  * Busy on last confirmation\n";
                    else if (reason == DispatchNotificationReasonEnum.CallOnTheWay)
                        View.m_lblNotificationReasons.Text += "  * Customer asked to call on the way\n";
                }

            } else
            {
                View.Size = new Size(View.Size.Width, 291);
            }

            if (visit.ServiceDate.HasValue)
                View.m_lblServiceDate.Text = visit.ServiceDate.Value.ToShortDateString();
            else
                View.m_lblServiceDate.Text = "Unknown";

            View.m_dtpConfirmTimeStart.Time = Model.ConfirmTimeFrameStart;            

            if (visit.PreferedTimeFrom.HasValue)
                View.m_dtpPrefferedTimeFrameBegin.EditValue = visit.PreferedTimeFrom.Value;
            else
                View.m_dtpPrefferedTimeFrameBegin.EditValue = null;

            if (visit.PreferedTimeTo.HasValue)
                View.m_dtpPrefferedTimeFrameEnd.EditValue = visit.PreferedTimeTo.Value;
            else
                View.m_dtpPrefferedTimeFrameEnd.EditValue = null;
            OnTimeFrameChanged(null, null);

            View.m_chkCallOnYourWay.Checked = visit.IsCallOnYourWay;           

            if (visit.IsConfirmed)
            {
                View.m_lblLastConfirmationTime.Text = visit.ConfirmDateTime.Value.ToShortTimeString();
                View.m_lblLastConfirmedTimeFrame.Text = visit.ConfirmedTimeFrameText;

                if (visit.ConfirmLeftMessage)
                    View.m_lblLastConfirmationMethod.Text = "Left Message";
                else if (visit.ConfirmBusy)
                    View.m_lblLastConfirmationMethod.Text = "Busy";
                else
                    View.m_lblLastConfirmationMethod.Text = "Conversation";
            } else
            {
                View.m_lblLastConfirmationTime.Text = "Unknown";
                View.m_lblLastConfirmedTimeFrame.Text = "Unknown";
                View.m_lblLastConfirmationMethod.Text = "Unknown";
            }

            View.m_ctlCustomer.Address = Model.CustomerAddress;
            View.m_ctlCustomer.Customer = Model.Customer;

            View.m_ctlAddress.BaseAddress = Model.CustomerAddress == null 
                ? null : (Address)Model.CustomerAddress.Clone();
            View.m_ctlAddress.BaseAddressName = "Customer Address";
            View.m_ctlAddress.Caption = "Visit Address";
            View.m_ctlAddress.CurrentAddress = Model.ServiceAddress;
            View.m_ctlAddress.Customer = (Customer) Model.Customer.Clone();
            if (Model.CustomerAddress != null && Model.ServiceAddress != null)
                View.m_ctlAddress.IsBaseAddressActive = Model.CustomerAddress.ID == Model.ServiceAddress.ID;

            View.m_txtVisitNotes.Text = visit.Notes;

            View.m_dtpConfirmTimeStart.Select();            
        }

        #endregion

        #region OnConfirmTimeEndValidating

        private void OnConfirmTimeEndValidating(object sender, CancelEventArgs e)
        {
            if (View.m_dtpConfirmTimeEnd.Time.Hour <= View.m_dtpConfirmTimeStart.Time.Hour)
                View.m_errorProvider.SetError(View.m_dtpConfirmTimeEnd, "Confirmed end time should be greater than start time");
            else
                View.m_errorProvider.SetError(View.m_dtpConfirmTimeEnd, string.Empty);
        }

        #endregion


        #region OnConfirmTimeStartChanged

        private void OnConfirmTimeStartChanged(object sender, EventArgs e)
        {
            View.m_dtpConfirmTimeEnd.Time = View.m_dtpConfirmTimeStart.Time.AddHours(2);
        }

        #endregion

        #region Customer or Address modified

        private void OnCustomerModified(Customer customer, Address address)
        {
            m_isCustomerModified = true;            
            View.m_ctlAddress.BaseAddress = (Address)View.m_ctlCustomer.Address.Clone();                
            if (View.m_ctlAddress.IsBaseAddressActive)
                View.m_ctlAddress.CurrentAddress = (Address)View.m_ctlCustomer.Address.Clone();                
        }

        private void OnAddressModified(Address baseAddress, Address currentAddress, bool isBaseAddressActive)
        {
            m_isAddressModified = true;
        }

        #endregion

        #region OnTimeFrameTemplateChanged

        private bool m_isTimeFrameUpdatingEachOther = false;

        private void OnTimeFrameTemplateChanged(object sender, EventArgs e)
        {
            if (m_isTimeFrameUpdatingEachOther)
                return;

            m_isTimeFrameUpdatingEachOther = true;

            if ((int)View.m_cmbTimeFrameTemplate.EditValue == (int)TimeFrameTemplateEnum.InAM)
            {
                View.m_dtpPrefferedTimeFrameBegin.Time = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    0, 0, 0);
                View.m_dtpPrefferedTimeFrameEnd.Time = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    12, 0, 0);
            }
            else if ((int)View.m_cmbTimeFrameTemplate.EditValue == (int)TimeFrameTemplateEnum.InPM)
            {
                View.m_dtpPrefferedTimeFrameBegin.Time = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    12, 0, 0);
                View.m_dtpPrefferedTimeFrameEnd.Time = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    23, 0, 0);
            }

            m_isTimeFrameUpdatingEachOther = false;
        }

        #endregion

        #region OnTimeFrameChanged

        private void OnTimeFrameChanged(object sender, EventArgs e)
        {
            if (m_isTimeFrameUpdatingEachOther)
                return;

            m_isTimeFrameUpdatingEachOther = true;

            if (View.m_dtpPrefferedTimeFrameBegin.Time.Hour == 0
                && View.m_dtpPrefferedTimeFrameEnd.Time.Hour == 12)
            {
                View.m_cmbTimeFrameTemplate.EditValue = (int)TimeFrameTemplateEnum.InAM;

            }
            else if (View.m_dtpPrefferedTimeFrameBegin.Time.Hour == 12
                && View.m_dtpPrefferedTimeFrameEnd.Time.Hour == 23)
            {
                View.m_cmbTimeFrameTemplate.EditValue = (int)TimeFrameTemplateEnum.InPM;
            }
            else
                View.m_cmbTimeFrameTemplate.EditValue = (int)TimeFrameTemplateEnum.Custom;

            m_isTimeFrameUpdatingEachOther = false;
        }

        #endregion

        #region OnTimeFrameValidating

        private void OnTimeFrameValidating(object sender, CancelEventArgs e)
        {
            if (View.m_dtpPrefferedTimeFrameBegin.EditValue != null
                && View.m_dtpPrefferedTimeFrameEnd.EditValue != null)
            {
                if (View.m_dtpPrefferedTimeFrameBegin.Time > View.m_dtpPrefferedTimeFrameEnd.Time)
                {
                    View.m_errorProvider.SetError((Control)sender, "Start time should be less than End time");
                    return;
                }
            }

            View.m_errorProvider.SetError(View.m_dtpPrefferedTimeFrameBegin, string.Empty);
            View.m_errorProvider.SetError(View.m_dtpPrefferedTimeFrameEnd, string.Empty);
        }

        #endregion

        #region OnBusyCheckedChanged

        private void OnBusyCheckedChanged(object sender, EventArgs e)
        {
            if (View.m_chkBusy.Checked)
                View.m_chkLeftMessage.Checked = false;
        }

        #endregion

        #region OnLeftMessageCheckedChanged

        private void OnLeftMessageCheckedChanged(object sender, EventArgs e)
        {
            if (View.m_chkLeftMessage.Checked)
                View.m_chkBusy.Checked = false;
        }

        #endregion


        #region OnConfirmClick

        private void OnConfirmClick(object sender, EventArgs e)
        {
            View.ValidateChildren();
            if (View.m_errorProvider.HasErrors)
                return;

            Visit visit = Visit.FindByPrimaryKey(Model.Appointment.Visit.ID);
            //visit.ServiceDate = Model.Appointment.Start.Date;
            visit.ConfirmDateTime = DateTime.Now;
            visit.ConfirmedFrameBegin = View.m_dtpConfirmTimeStart.Time;
            visit.ConfirmedFrameEnd = View.m_dtpConfirmTimeEnd.Time;

            if (View.m_dtpPrefferedTimeFrameBegin.EditValue != null)
                visit.PreferedTimeFrom = View.m_dtpPrefferedTimeFrameBegin.Time;
            else
                visit.PreferedTimeFrom = null;

            if (View.m_dtpPrefferedTimeFrameEnd.EditValue != null)
                visit.PreferedTimeTo = View.m_dtpPrefferedTimeFrameEnd.Time;
            else
                visit.PreferedTimeTo = null;

            visit.ConfirmLeftMessage = View.m_chkLeftMessage.Checked;
            visit.ConfirmBusy = View.m_chkBusy.Checked;
            visit.IsCallOnYourWay = View.m_chkCallOnYourWay.Checked;
            visit.IsWillCall = View.m_chkWillCall.Checked;
            visit.Notes = View.m_txtVisitNotes.Text;            

            try
            {
                Database.Begin();

                if (m_isCustomerModified)
                {
                    View.m_ctlCustomer.Customer.Modified = DateTime.Now;
                    View.m_ctlCustomer.Address.Modified = DateTime.Now;
                    Customer.Update(View.m_ctlCustomer.Customer);
                    Address.Update(View.m_ctlCustomer.Address);
                }

                if (m_isAddressModified)
                {
                    if (View.m_ctlAddress.IsBaseAddressActive)
                    {
                        visit.ServiceAddressId = View.m_ctlAddress.BaseAddress.ID;
                        Visit.Update(visit);
//                        if (View.m_ctlAddress.CurrentAddress.ID != View.m_ctlAddress.BaseAddress.ID)
//                        {
//                            CustomerAddressAdditional.Delete(
//                                new CustomerAddressAdditional(
//                                    visit.CustomerId.Value, 
//                                    View.m_ctlAddress.CurrentAddress.ID));
//                            Address.Delete(View.m_ctlAddress.CurrentAddress);
//                        }                            
                    }
                    else
                    {
                        if (visit.ServiceAddressId == View.m_ctlAddress.BaseAddress.ID)
                        {
                            View.m_ctlAddress.CurrentAddress.Modified = DateTime.Now;
                            Address.Insert(View.m_ctlAddress.CurrentAddress);
                            CustomerAddressAdditional addressAdditional
                                = new CustomerAddressAdditional(
                                    visit.CustomerId.Value,
                                    View.m_ctlAddress.CurrentAddress.ID);
                            CustomerAddressAdditional.Insert(addressAdditional);
                        }                            
                        else
                        {
                            View.m_ctlAddress.CurrentAddress.Modified = DateTime.Now;
                            Address.Update(View.m_ctlAddress.CurrentAddress);
                        }
                            

                        visit.ServiceAddressId
                            = View.m_ctlAddress.CurrentAddress.ID;

                        Visit.Update(visit);
                    }
                }


                Visit.Update(visit);
                Database.Commit(); 
                m_result = ConfirmVisitResultEnum.Confirm;  
                Host.TraceUserAction("Confirm Visit " + visit.ID);
                View.Destroy();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        #endregion

        #region OnDispatchWithoutConfirmClick

        private void OnDispatchWithoutConfirmClick(object sender, EventArgs e)
        {
            m_result = ConfirmVisitResultEnum.Continue;
            View.Destroy();
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_result = ConfirmVisitResultEnum.Cancel;
            View.Destroy();
        }

        #endregion

    }
}
