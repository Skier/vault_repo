using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.Windows.UI.ManageTime.Single
{    
    public class SingleTimeTrackingController : SingleFormController<SingleTimeTrackingModel, SingleTimeTrackingView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {            
            View.m_cmbPersonType.SelectedIndexChanged += new EventHandler(OnPersonTypeChanged);
            View.m_chkBillable.CheckStateChanged += new EventHandler(OnBillableStateChanged);
            View.m_chkStartEnd.CheckStateChanged += new EventHandler(OnStartEndStateChanged);
            View.m_cmbService.SelectedIndexChanged += new EventHandler(OnServiceChanged);
            View.m_curRate.TextChanged += new EventHandler(OnRateChanged);
                       
            View.m_txtTimeHours.TextChanged += new EventHandler(OnTimeChanged);
            View.m_cmbTimeMins.SelectedIndexChanged += new EventHandler(OnTimeChanged);

            View.m_cmbStartHours.SelectedIndexChanged += new EventHandler(OnTimeChanged);
            View.m_cmbStartMins.SelectedIndexChanged += new EventHandler(OnTimeChanged);
            View.m_cmbStartPm.SelectedIndexChanged += new EventHandler(OnTimeChanged);

            View.m_cmbEndHours.SelectedIndexChanged += new EventHandler(OnTimeChanged);
            View.m_cmbEndMins.SelectedIndexChanged += new EventHandler(OnTimeChanged);
            View.m_cmbEndPm.SelectedIndexChanged += new EventHandler(OnTimeChanged);

            View.m_txtBreakHours.TextChanged += new EventHandler(OnTimeChanged);
            View.m_cmbBreakMins.SelectedIndexChanged += new EventHandler(OnTimeChanged);
        }        

        #endregion                

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data == null)
                data = new object[0];


            if (data.Length >= 1 && data[0] != null)
            {
                Debug.Assert(data[0] is TimeTracking, "First param should be TimeTracking instance");
                Model.TimeTracking = (TimeTracking)data[0];
            }                
            else
                Model.TimeTracking = null;

            if (data.Length >= 2 && data[1] != null)
            {
                Debug.Assert(data[1] is bool, "Second param should be readonly flag value instance");
                Model.IsReadOnly = (bool)data[1];
            }                
            else
                Model.IsReadOnly = false;

            if (data.Length >= 3 && data[2] != null)
            {
                Debug.Assert((data[2] is Vendor) || (data[2] is Employee), 
                    "Third param should be Vendor or Employee");

                if (data[2] is Vendor)
                    Model.CurrentVendor = (Vendor) data[2];
                else if (data[2] is Employee)
                    Model.CurrentEmployee = (Employee) data[2];                
            }                
            else
            {
                Model.CurrentVendor = null;
                Model.CurrentEmployee = null;
            }

            if (data.Length >= 4 && data[3] != null)
            {
                Debug.Assert(data[3] is DateTime?, "Fourth param should be Current Date");
                Model.CurrentDate = (DateTime?) data[3];
            }                
            else
                Model.CurrentDate = null;

            if (data.Length >= 5 && data[4] != null)
            {
                Debug.Assert(data[4] is bool?, "Fifth param should be IsCreateNew flag");
                Model.IsCreateNew = (bool)data[4];
            }
            else
                Model.IsCreateNew = true;
            
            
            base.OnModelInitialize(data);            
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {   
            base.OnViewLoad();            

            IsDefaultActionExist = true;
            DefaultActionName = "Cancel";
            View.m_txtBreakHours.Text = string.Empty;
            View.m_txtTimeHours.Text = string.Empty;

            InitUI();
            
            if (Model.CurrentDate != null)
            {
                View.m_cmbPersonType.Enabled = false;
                View.m_cmbPerson.Enabled = false;
                View.m_lblDate.Enabled = false;
                View.m_dtpDate.Enabled = false;
                View.m_cmbCustomer.Focus();
            } else
            {
                View.m_cmbPersonType.Focus();            
            }
                        
        }

        #endregion

        #region OnPersonTypeChanged

        private void OnPersonTypeChanged(object sender, EventArgs e)
        {
            UpdatePersonList((PersonTypeEnum) View.m_cmbPersonType.SelectedItem);
        }

        #endregion        

        #region OnDefaultAction

        public override void OnDefaultAction()
        {            
            View.Destroy();
        }

        #endregion

        #region OnBillableStateChanged

        private void OnBillableStateChanged(object sender, EventArgs e)
        {
            if (View.m_chkBillable.Checked)
            {
                View.m_lblRate.Enabled = true;
                View.m_curRate.Enabled = true;
            } else
            {
                View.m_lblRate.Enabled = false;
                View.m_curRate.Enabled = false;
                View.m_curRate.Value = null;
            }
        }

        #endregion

        #region OnStartEndStateChanged

        private void OnStartEndStateChanged(object sender, EventArgs e)
        {
            View.m_lblTime.Visible = !View.m_chkStartEnd.Checked;                                   
            View.m_lbStart.Visible = View.m_chkStartEnd.Checked;            
            View.m_lbEnd.Visible = View.m_chkStartEnd.Checked;            
            View.m_lbBreak.Visible = View.m_chkStartEnd.Checked;
            
            //View.m_txtTime.Visible = !View.m_chkStartEnd.Checked;
            View.m_txtTimeHours.Visible = !View.m_chkStartEnd.Checked;
            View.m_cmbTimeMins.Visible = !View.m_chkStartEnd.Checked;
            View.m_lblTimeSeparator.Visible = !View.m_chkStartEnd.Checked;                
                
            //View.m_txtTimeStart.Visible = View.m_chkStartEnd.Checked;
            View.m_cmbStartHours.Visible = View.m_chkStartEnd.Checked;
            View.m_cmbStartMins.Visible = View.m_chkStartEnd.Checked;
            View.m_cmbStartPm.Visible = View.m_chkStartEnd.Checked;
            View.m_lblStartSeparator.Visible = View.m_chkStartEnd.Checked;            
            
            //View.m_txtTimeEnd.Visible = View.m_chkStartEnd.Checked;
            View.m_cmbEndHours.Visible = View.m_chkStartEnd.Checked;
            View.m_cmbEndMins.Visible = View.m_chkStartEnd.Checked;
            View.m_cmbEndPm.Visible = View.m_chkStartEnd.Checked;
            View.m_lblEndSeparator.Visible = View.m_chkStartEnd.Checked;
            
            //View.m_txtBreak.Visible = View.m_chkStartEnd.Checked;            
            View.m_txtBreakHours.Visible = View.m_chkStartEnd.Checked;
            View.m_cmbBreakMins.Visible = View.m_chkStartEnd.Checked;
            View.m_lblBreakSeparator.Visible = View.m_chkStartEnd.Checked;                        
            
            OnTimeChanged(this, EventArgs.Empty);
        }

        #endregion

        #region OnServiceChanged

        private void OnServiceChanged(object sender, EventArgs e)
        {   
            View.m_curRate.Value = null;
            
            if (View.m_cmbService.SelectedItem != null)
            {
                Item item = (Item) View.m_cmbService.SelectedItem;                                                                
                View.m_chkBillable.Checked = item.SalesPrice != decimal.Zero;
                if (View.m_chkBillable.Checked)
                    View.m_curRate.Value = item.SalesPrice;
            } else
                View.m_chkBillable.Checked = false;
        }

        #endregion

        #region OnTimeChanged

        private void OnTimeChanged(object sender, EventArgs e)
        {
            if (sender == View.m_txtTimeHours && View.m_cmbTimeMins.SelectedIndex == -1)
                View.m_cmbTimeMins.SelectedIndex = 0;
            else if (sender == View.m_txtBreakHours && View.m_cmbBreakMins.SelectedIndex == -1)
                View.m_cmbBreakMins.SelectedIndex = 0;
            else if (sender == View.m_cmbStartHours)
            {
                if (View.m_cmbStartMins.SelectedIndex == -1)
                    View.m_cmbStartMins.SelectedIndex = 0;
                if (View.m_cmbStartPm.SelectedIndex == -1)
                    View.m_cmbStartPm.SelectedIndex = 1;
            }
            else if (sender == View.m_cmbEndHours)
            {
                if (View.m_cmbEndMins.SelectedIndex == -1)
                    View.m_cmbEndMins.SelectedIndex = 0;
                if (View.m_cmbEndPm.SelectedIndex == -1)
                    View.m_cmbEndPm.SelectedIndex = 1;                
            }

            UpdateSummaryCost();
            
            try
            {
                TimeSpan duration = GetTimeSpanFromUI(false);
                View.m_lblSummary.Text = "Summary: "
                    + GetHours(duration) + " hr " + GetMinutes(duration) + " mins";                                    
            }
            catch (Exception)
            {
                View.m_lblSummary.Text = "Summary: undefined";                
            }            
        }

        #endregion

        #region OnRateChanged

        private void OnRateChanged(object sender, EventArgs e)
        {
            UpdateSummaryCost();
        }

        #endregion


        #region InitUI

        private void InitUI()
        {
            InitPersonTypes();            
            InitCustomers();
            InitServices();

            if (Model.CurrentEmployee != null)
                View.m_cmbPersonType.SelectedItem = PersonTypeEnum.Employee;
            else if (Model.CurrentVendor != null)
                View.m_cmbPersonType.SelectedItem = PersonTypeEnum.Vendor;
                                                
            UpdatePersonList((PersonTypeEnum)View.m_cmbPersonType.SelectedItem);

            if (Model.CurrentEmployee != null)
            {
                foreach (Employee employee in View.m_cmbPerson.Items)
                {
                    if (employee.EmployeeId == Model.CurrentEmployee.EmployeeId)
                    {
                        View.m_cmbPerson.SelectedItem = employee;
                        break;
                    }                                            
                }                
            }                
            else if (Model.CurrentVendor != null)
            {
                foreach (Vendor vendor in View.m_cmbPerson.Items)
                {
                    if (vendor.VendorId == Model.CurrentVendor.VendorId)
                    {
                        View.m_cmbPerson.SelectedItem = vendor;
                        break;                        
                    }
                }
            }
            
            if (Configuration.App.UseUserIdentification)
            {
                View.m_cmbPersonType.Enabled = false;
                View.m_cmbPerson.Enabled = false;
            }
            
            if (Model.CurrentDate != null)
                View.m_dtpDate.Value = Model.CurrentDate.Value;            
            
            if (Model.TimeTracking != null)
            {
                if (Model.TimeTracking.TxnDate != null)
                    View.m_dtpDate.Value = Model.TimeTracking.TxnDate.Value;
                
                if (Model.TimeTracking.Customer != null)
                {
                    foreach (Customer customer in View.m_cmbCustomer.Items)
                    {
                        if (customer.CustomerId == Model.TimeTracking.Customer.CustomerId)
                        {
                            View.m_cmbCustomer.SelectedItem = customer;
                            break;                                                    
                        }
                    }
                }
                
                if (Model.TimeTracking.Item != null)
                {
                    foreach (Item item in View.m_cmbService.Items)
                    {
                        if (item.ItemId == Model.TimeTracking.Item.ItemId)
                        {
                            View.m_cmbService.SelectedItem = item;
                            break;                            
                        }
                    }
                }

                View.m_chkBillable.Checked = Model.TimeTracking.IsBillable;
                if (View.m_chkBillable.Checked)
                    View.m_curRate.Value = Model.TimeTracking.Rate;

                TimeSpan duration = TimeTracking.ConvertQBDuration(Model.TimeTracking.Duration);
                View.m_txtTimeHours.Text = GetHours(duration).ToString();
                View.m_cmbTimeMins.SelectedItem = GetMinutes(duration).ToString();
                OnTimeChanged(this, EventArgs.Empty);
                View.m_txtNotes.Text = Model.TimeTracking.Notes ?? string.Empty;
            }

            View.m_lblRate.Enabled = View.m_chkBillable.Checked;
            View.m_curRate.Enabled = View.m_chkBillable.Checked;
            
            if (Model.IsReadOnly)
            {
                View.m_cmbPersonType.Enabled = false;
                View.m_cmbPerson.Enabled = false;
                View.m_lblDate.Enabled = false;
                View.m_dtpDate.Enabled = false;
                View.m_lblCustomer.Enabled = false;
                View.m_cmbCustomer.Enabled = false;
                View.m_lblService.Enabled = false;
                View.m_cmbService.Enabled = false;
                View.m_chkBillable.Enabled = false;
                View.m_lblRate.Enabled = false;
                View.m_curRate.Enabled = false;
                                
                View.m_lblTime.Enabled = false;
                View.m_txtTimeHours.Enabled = false;
                View.m_cmbTimeMins.Enabled = false;
                View.m_lblTimeSeparator.Enabled = false;
                
                View.m_chkStartEnd.Enabled = false;
                View.m_lblSummary.Enabled = false;
                View.m_txtNotes.Enabled = false;

                View.m_tabs.Focus();
            }                        
        }

        #endregion
        
        #region UpdatePersons

        private void UpdatePersonList(PersonTypeEnum personType)
        {
            View.m_cmbPerson.Items.Clear();

            if (personType == PersonTypeEnum.Employee)
            {
                foreach (Employee employee in Model.Employees)
                    View.m_cmbPerson.Items.Add(employee);
            }
            else if (personType == PersonTypeEnum.Vendor)
            {
                foreach (Vendor vendor in Model.Vendors)
                    View.m_cmbPerson.Items.Add(vendor);
            }

            if (View.m_cmbPerson.Items.Count > 0)
            {
                View.m_cmbPerson.SelectedIndex = 0;
            }
                        
            
        }

        #endregion        

        #region UpdateSummaryCost

        private const string UNDEFINED_SUMMARY = "Summary, $: undefined";
        
        private void UpdateSummaryCost()
        {
            TimeSpan duration;
            
            try
            {
                duration = GetTimeSpanFromUI(false);
            }
            catch (Exception)
            {
                View.m_lblSummaryCost.Text = UNDEFINED_SUMMARY;
                return;
            }

            
            if (View.m_curRate.Value == null)
            {
                View.m_lblSummaryCost.Text = UNDEFINED_SUMMARY;
                return;                                
            }

            if (View.m_curRate.Value < 0)
            {
                View.m_lblSummaryCost.Text = UNDEFINED_SUMMARY;
                return;                                
            }

            try
            {
                View.m_lblSummaryCost.Text = "Summary, $: "
                    + ((decimal)duration.TotalHours * View.m_curRate.Value.Value).ToString("0.00");
            }
            catch (OverflowException)
            {
                View.m_lblSummaryCost.Text = UNDEFINED_SUMMARY;
            }            
            
        }

        #endregion

        #region InitPersonTypes

        private void InitPersonTypes()
        {
            View.m_cmbPersonType.Items.Clear();
            
            //if (Model.Employees.Count > 0)
                View.m_cmbPersonType.Items.Add(PersonTypeEnum.Employee);

            //if (Model.Vendors.Count > 0)
                View.m_cmbPersonType.Items.Add(PersonTypeEnum.Vendor);

            if (View.m_cmbPersonType.Items.Count > 0)
                View.m_cmbPersonType.SelectedIndex = 0;
        }

        #endregion

        #region InitCustomers

        private void InitCustomers()
        {
            View.m_cmbCustomer.Items.Clear();
            View.m_cmbCustomer.Items.Add(new Customer());
            foreach (Customer customer in Model.Customers)
                View.m_cmbCustomer.Items.Add(customer);

            View.m_cmbCustomer.SelectedIndex = 0;
        }

        #endregion

        #region InitServices

        private void InitServices()
        {
            View.m_cmbService.Items.Clear();
            View.m_cmbService.Items.Add(new Item());
            foreach (Item item in Model.Items)
                View.m_cmbService.Items.Add(item);

            View.m_cmbService.SelectedIndex = 0;
        }

        #endregion

        #region IsFormValid

        private bool IsFormValid()
        {
            if (View.m_cmbPerson.Items.Count == 0)
            {
                MessageDialog.Show(MessageDialogType.Information, "Employee or Vendor should be selected");
                View.m_tabs.SelectedIndex = 0;
                View.m_cmbPerson.Focus();
                return false;
            }
                        
            if (View.m_txtTimeHours.Text == string.Empty && !View.m_chkStartEnd.Checked)
            {
                MessageDialog.Show(MessageDialogType.Information, "Time should be specified");
                View.m_tabs.SelectedIndex = 1;
                View.m_txtTimeHours.Focus();
                View.m_txtTimeHours.SelectAll();
                return false;
            }

            if (View.m_cmbStartHours.SelectedIndex == -1
                && View.m_cmbEndHours.SelectedIndex == -1 && View.m_chkStartEnd.Checked)
            {
                MessageDialog.Show(MessageDialogType.Information, "Start and End time should be specified");                
                View.m_tabs.SelectedIndex = 1;
                View.m_cmbStartHours.Focus();                                
                return false;
            }            

            try
            {
                GetTimeSpanFromUI(true);
            }
            catch (Exception)
            {
                return false;
            }
            
            if (View.m_txtNotes.Text.Length > 4000)
            {
                MessageDialog.Show(MessageDialogType.Information, "Notes can contain up to 4000 chars only"); 
                View.m_tabs.SelectedIndex = 2;
                View.m_txtNotes.Focus();                
                View.m_txtNotes.SelectAll();                
                return false;                
            }
            
            if (View.m_chkBillable.Checked)
            {
                if (View.m_curRate.Value == null)
                {
                    MessageDialog.Show(MessageDialogType.Information, "Rate value should be set");
                    View.m_tabs.SelectedIndex = 0;
                    View.m_curRate.Focus();
                    View.m_curRate.SelectAll();                                    
                    return false;                                    
                }

                //decimal rate;
                
//                try
//                {
//                    rate = decimal.Parse(View.m_txtRate.Text);
//                }
//                catch (Exception)
//                {
//                    MessageDialog.Show(MessageDialogType.Information, "Rate contains wrong value");
//                    View.m_tabs.SelectedIndex = 0;
//                    View.m_txtRate.Focus();
//                    View.m_txtRate.SelectAll();                                                        
//                    return false;                                                        
//                }
//                
//                if (rate < 0)
//                {
//                    MessageDialog.Show(MessageDialogType.Information, "Rate should be positive");
//                    View.m_tabs.SelectedIndex = 0;
//                    View.m_txtRate.Focus();
//                    View.m_txtRate.SelectAll();
//                    return false;                                                        
//                    
//                }
                
                if (View.m_cmbCustomer.SelectedIndex == 0 
                    || View.m_cmbService.SelectedIndex == 0)
                {
                    MessageDialog.Show(MessageDialogType.Information, "Customer and Service should be specified");

                    if (View.m_cmbCustomer.SelectedIndex == 0)
                    {
                        View.m_tabs.SelectedIndex = 0;
                        View.m_cmbCustomer.Focus();
                    }
                    else
                    {
                        View.m_tabs.SelectedIndex = 0;
                        View.m_cmbService.Focus();
                    }                    
                    
                    return false;                                                                            
                }
            }
                        
            return true;                
        }

        #endregion

        #region Time                

        #region GetTimeSpan

        private TimeSpan GetTimeSpan(TextBox hours, ComboBox minutes, bool isShowErrorMessage)
        {
            int hoursValue;
            
            try
            {
                hoursValue = int.Parse(hours.Text);
            }
            catch (Exception ex)
            {
                if (isShowErrorMessage)
                {
                    MessageDialog.Show(MessageDialogType.Information, "Hours contains wrong value");
                    View.m_tabs.SelectedIndex = 1;
                    hours.Focus();
                    hours.SelectAll();                                    
                }
                throw ex;
            }
            
            if (hoursValue < 0 || hoursValue > 8760)
            {
                if (isShowErrorMessage)
                {
                    MessageDialog.Show(MessageDialogType.Information, "Hours contains wrong value");
                    View.m_tabs.SelectedIndex = 1;
                    hours.Focus();
                    hours.SelectAll();
                }
                throw new Exception();                
            }
            
            int mins = 0;
            if (minutes.SelectedIndex != -1)
                mins = int.Parse(minutes.SelectedItem.ToString());

            return new TimeSpan(hoursValue, mins, 0);
        }

        #endregion

        #region GetTime

        private DateTime GetTime(ComboBox hours, ComboBox minutes, ComboBox ampm)
        {
            if (hours.SelectedIndex == -1 || ampm.SelectedIndex == -1)
                throw new FormatException("Hours and AM/PM should be specified");

            int mins = 0;
            if (minutes.SelectedIndex != -1)
                mins = int.Parse(minutes.SelectedItem.ToString());
            
            IFormatProvider culture = new CultureInfo("en-US", true);

            return DateTime.ParseExact(hours.SelectedItem.ToString()
               + ":" + mins.ToString() + " "
               + ampm.SelectedItem.ToString(), "h:m tt", culture);                        
        }

        #endregion

        #region GetHours

        private int GetHours(TimeSpan timeSpan)
        {            
            return (int)timeSpan.TotalHours;
        }

        #endregion

        #region GetMinutes

        private int GetMinutes(TimeSpan timeSpan)
        {
            int hours = GetHours(timeSpan);            
            double fractionalHours = timeSpan.TotalHours - hours;            
            return (int)Math.Round(fractionalHours*60);
        }

        #endregion

        #region GetQBTimeSpan

        private string GetQBTimeSpan(TimeSpan timeSpan)
        {
            return "PT" + GetHours(timeSpan) + "H" + GetMinutes(timeSpan) + "M";
        }

        #endregion        

        #region GetTimeSpanFromUI

        private TimeSpan GetTimeSpanFromUI(bool isShowErrorMessages)
        {
            if (View.m_chkStartEnd.Checked) // Start, End, Break
            {
                TimeSpan breakTime = TimeSpan.Zero;

                if (View.m_txtBreakHours.Text != string.Empty)
                {
                    try
                    {
                        breakTime = GetTimeSpan(View.m_txtBreakHours, 
                                                View.m_cmbBreakMins, isShowErrorMessages);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }                    
                }

                if (View.m_cmbStartHours.SelectedIndex == -1 || View.m_cmbStartPm.SelectedIndex == -1)
                {
                    if (isShowErrorMessages)
                    {
                        MessageDialog.Show(MessageDialogType.Information, "Start time should be set");
                        if (View.m_cmbStartHours.SelectedIndex == -1)
                            View.m_cmbStartHours.Focus();
                        else
                            View.m_cmbStartPm.Focus();                        
                    }
                    throw new FormatException();                    
                }
                
                if (View.m_cmbEndHours.SelectedIndex == -1 || View.m_cmbEndPm.SelectedIndex == -1)
                {
                    if (isShowErrorMessages)
                    {
                        MessageDialog.Show(MessageDialogType.Information, "End time should be set");
                        if (View.m_cmbEndHours.SelectedIndex == -1)
                            View.m_cmbEndHours.Focus();
                        else
                            View.m_cmbEndPm.Focus();                        
                    }
                    throw new FormatException();                    
                }

                DateTime startTime
                    = GetTime(View.m_cmbStartHours, View.m_cmbStartMins, View.m_cmbStartPm);
                
                DateTime endTime
                    = GetTime(View.m_cmbEndHours, View.m_cmbEndMins, View.m_cmbEndPm);

                TimeSpan duration = endTime - startTime;

                if (duration < TimeSpan.Zero)
                    duration = endTime.AddDays(1) - startTime;
                
                if (breakTime > duration)
                {
                    if (isShowErrorMessages)
                    {
                        MessageDialog.Show(MessageDialogType.Information, "Break time cannot be longer than the elapsed time");
                        View.m_tabs.SelectedIndex = 1;
                        View.m_txtBreakHours.Focus();                        
                        View.m_txtBreakHours.SelectAll();                        
                    }
                        
                    throw new Exception();                    
                }

                return duration - breakTime;
                
            } else //Time Interval
            {
                if (View.m_txtTimeHours.Text != string.Empty)
                {
                    try
                    {
                        return GetTimeSpan(View.m_txtTimeHours, View.m_cmbTimeMins, isShowErrorMessages);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }                        
                else
                {
                    if (isShowErrorMessages)
                    {
                        MessageDialog.Show(MessageDialogType.Information, "Time should be set");
                        View.m_tabs.SelectedIndex = 1;
                        View.m_txtTimeHours.Focus();
                        View.m_txtTimeHours.SelectAll();
                    }
                    throw new Exception();                        
                }                                        
            }            
        }

        #endregion

        #endregion        

        #region Save

        protected override bool OnSave()
        {
            if (Model.IsReadOnly)
                return true;                        
            
            if (!IsFormValid())
            {
                //SetTimeControlsHandlers();
                return false;
            }

            if (Model.IsCreateNew)
            {
                Model.TimeTracking = new TimeTracking();
                Model.TimeTracking.EntityState = EntityState.Created;
            }
             
            if ((PersonTypeEnum)View.m_cmbPersonType.SelectedItem == PersonTypeEnum.Employee)
                Model.TimeTracking.QBEntity = new QBEntity((int)((Employee)View.m_cmbPerson.SelectedItem).QuickBooksListId);
            else
                Model.TimeTracking.QBEntity = new QBEntity((int)((Vendor)View.m_cmbPerson.SelectedItem).QuickBooksListId);

            if (View.m_cmbCustomer.SelectedIndex > 0)
                Model.TimeTracking.Customer = ((Customer)View.m_cmbCustomer.SelectedItem);
            else
                Model.TimeTracking.Customer = null;

            if (View.m_cmbService.SelectedIndex > 0)
                Model.TimeTracking.Item = ((Item)View.m_cmbService.SelectedItem);
            else
                Model.TimeTracking.Item = null;

            Model.TimeTracking.Duration = GetQBTimeSpan(GetTimeSpanFromUI(false));
            Model.TimeTracking.Notes = View.m_txtNotes.Text;
            Model.TimeTracking.IsBillable = View.m_chkBillable.Checked;
            Model.TimeTracking.TxnDate = View.m_dtpDate.Value;

            if (Model.TimeTracking.IsBillable)
                Model.TimeTracking.Rate = View.m_curRate.Value;

            try
            {
                Model.Save();
            }
            catch (Exception e)
            {
                EventService.AddEvent(new QuickBooksAgentException(
                    "Unable to save time tracking", e));
                
                return false;
            }

            return true;
        }
        
        #endregion                                
    }
}
