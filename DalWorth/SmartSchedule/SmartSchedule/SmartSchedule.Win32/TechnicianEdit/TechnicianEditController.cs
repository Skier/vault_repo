using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Calendar;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SmartSchedule.Data;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.SDK;
using SmartSchedule.Win32.Authenticate;
using SmartSchedule.Win32.Controls;
using SmartSchedule.Windows;
using Point=System.Drawing.Point;
using System.Linq;

namespace SmartSchedule.Win32.TechnicianEdit
{
    public class TechnicianEditController : Controller<TechnicianEditModel, TechnicianEditView>
    {
        private bool m_isPresetsComboChanging;

        private bool m_isDataLoading;
        private List<DateTime> m_prevSelectedDates = new List<DateTime>();

        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region SelectedTechnicians

        public List<TechnicianDetail> SelectedTechnicians
        {
            get
            {
                List<TechnicianDetail> result = new List<TechnicianDetail>();
               
                foreach (DateTime selectedDate in View.m_dateNavigator.Selection)
                {
                    if (Model.Technicians.ContainsKey(selectedDate.Date))
                        result.Add(Model.Technicians[selectedDate.Date]);
                }

                return result;
            }
        }

        #endregion


        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data[0] != null)
                Model.Technician = (Technician)data[0];
            Model.IsDefaultSettingsMode = (bool)data[1];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick;
            
            View.m_gridServicesView.FocusedColumnChanged += OnGridServicesFocusedColumnChanged;
            View.m_gridServicesView.DoubleClick += OnGridServicesDoubleClick;
            View.m_txtPrimaryZips.KeyPress += OnZipsKeyPress;
            View.m_txtSecondaryZips.KeyPress += OnZipsKeyPress;

            View.m_txtName.Validating += OnNameValidating;
            View.m_txtServmanId.Validating += OnServmanIdValidating;
            View.m_txtDepotAddress.Validating += OnDepotAddressValidating;
            View.m_txtEmail.Validating += OnEmailValidating;
            View.m_cmbDriveTime.Validating += OnDriveTimeValidating;
            View.m_txtMaxJobsCount.Validating += OnMaxJobsCountValidating;
            View.m_txtMaxNcoCount.Validating += OnMaxNcoCountValidating;

            View.m_timeWorkStart.Validating += OnWorkTimeValidating;
            View.m_timeWorkEnd.Validating += OnWorkTimeValidating;

            View.m_timePreset1Start.Validating += OnWorkTimeValidating;
            View.m_timePreset1End.Validating += OnWorkTimeValidating;

            View.m_timePreset2Start.Validating += OnWorkTimeValidating;
            View.m_timePreset2End.Validating += OnWorkTimeValidating;

            View.m_timePreset3Start.Validating += OnWorkTimeValidating;
            View.m_timePreset3End.Validating += OnWorkTimeValidating;

            View.m_txtRate.Validating += OnRateValidating;
            View.m_txtRate150to300.Validating += OnRateValidating;
            View.m_txtRateMore300.Validating += OnRateValidating;

            View.m_txtPrimaryZips.Validating += OnZipsValidating;
            View.m_txtSecondaryZips.Validating += OnZipsValidating;   
         
            View.m_cmbPresets.SelectedIndexChanged += OnPresetsSelectedIndexChanged;

            View.m_timeWorkStart.TimeChanged += OnDailyWorkTimeChanged;
            View.m_timeWorkEnd.TimeChanged += OnDailyWorkTimeChanged;
            View.m_chkIsContractor.CheckedChanged += OnIsContractorCheckedChanged;

            View.m_dateNavigator.CustomDrawDayNumberCell += OnDateNavigatorDrawDay;
            View.m_dateNavigator.EditDateModified += OnDateNavigatorDateModified;
            View.m_chkIsWorking.CheckedChanged += OnIsWorkingChanged;

            View.m_txtName.TextChanged += OnDataChanged;
            View.m_txtServmanId.TextChanged += OnDataChanged;
            View.m_txtDepotAddress.TextChanged += OnDataChanged;
            View.m_txtEmail.TextChanged += OnDataChanged;
            View.m_cmbDriveTime.TextChanged += OnDataChanged;
            View.m_txtMaxNcoCount.TextChanged += OnDataChanged;
            View.m_txtMaxJobsCount.TextChanged += OnDataChanged;
            View.m_txtRate.TextChanged += OnDataChanged;
            View.m_txtRate150to300.TextChanged += OnDataChanged;
            View.m_txtRateMore300.TextChanged += OnDataChanged;
            View.m_timeWorkStart.TextChanged += OnDataChanged;
            View.m_timeWorkEnd.TextChanged += OnDataChanged;
            View.m_txtPrimaryZips.TextChanged += OnDataChanged;
            View.m_txtSecondaryZips.TextChanged += OnDataChanged;   
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            if (Model.IsDefaultSettingsMode)
            {
                if (Model.IsTechnicianCreate)
                {
                    View.Text = "Add Technician";
                    View.m_lblServmanIdLabel.Visible = true;
                    View.m_txtServmanId.Visible = true;
                    View.m_txtName.Width = 92;
                }
                else
                    View.Text = "Technician Edit (Default Settings)";

                View.m_dateNavigator.DateTime = Model.BaseTechnicianDate;
                View.m_chkIsWorking.Enabled = false;
                View.m_dateNavigator.Enabled = false;

                View.m_groupWorkingHours.Visible = false;

                if (Model.WorkTimePresets.Count > 0 && Model.WorkTimePresets[0].TimeStart.HasValue)
                {
                    View.m_timePreset1Start.Time = Model.WorkTimePresets[0].TimeStart.Value;
                    View.m_timePreset1End.Time = Model.WorkTimePresets[0].TimeEnd.Value;
                }

                if (Model.WorkTimePresets.Count > 1 && Model.WorkTimePresets[1].TimeStart.HasValue)
                {
                    View.m_timePreset2Start.Time = Model.WorkTimePresets[1].TimeStart.Value;
                    View.m_timePreset2End.Time = Model.WorkTimePresets[1].TimeEnd.Value;
                }
                else
                {
                    View.m_timePreset2Start.Time = Model.Technicians[Model.BaseTechnicianDate].Technician.WorkingIntervals[0].TimeStart;
                    View.m_timePreset2End.Time = Model.Technicians[Model.BaseTechnicianDate].Technician.WorkingIntervals[0].TimeEnd;
                }

                if (Model.WorkTimePresets.Count > 2 && Model.WorkTimePresets[2].TimeStart.HasValue)
                {
                    View.m_timePreset3Start.Time = Model.WorkTimePresets[2].TimeStart.Value;
                    View.m_timePreset3End.Time = Model.WorkTimePresets[2].TimeEnd.Value;
                }
            }                
            else
            {
                View.Text = "Technician Edit (Day Settings)";

                View.m_dateNavigator.DateTime = Model.BaseTechnicianDate;                
                m_prevSelectedDates = View.m_dateNavigator.Selection.Cast<DateTime>().ToList();

                View.m_groupPresets.Visible = false;
                View.m_cmbPresets.SelectedIndex = -1;

                foreach (TechnicianWorkTimeDefaultPreset preset in Model.WorkTimePresets)
                {
                    if (preset.TimeEnd.HasValue)
                    {
                        ImageComboBoxItem comboBoxItem = new ImageComboBoxItem(
                            string.Format("Preset {0} ({1}-{2})", preset.PresetNumber,
                            Utils.FormatTime(preset.TimeStart.Value),
                            Utils.FormatTime(preset.TimeEnd.Value)), preset);

                        View.m_cmbPresets.Properties.Items.Add(comboBoxItem);
                    }
                }

                View.m_cmbPresets.Properties.Items.Add(new ImageComboBoxItem("Custom", null));

                View.m_lblEmail.Visible = false;
                View.m_txtEmail.Visible = false;
                View.m_txtDepotAddress.Size = new Size(198, 78);
            }
            
            OnDateNavigatorDateModified(null, EventArgs.Empty); 
            OnIsWorkingChanged(null, EventArgs.Empty);

            View.AlwaysAllowedControls.Add(View.m_btnCancel);
            View.AlwaysAllowedControls.Add(View.m_dateNavigator);
            View.MinRequiredUserRole = UserRoleEnum.Supervisor;
        }

        #endregion

        #region OnDateNavigatorDateModified

        private void OnDateNavigatorDateModified(object sender, EventArgs args)
        {
            if (m_prevSelectedDates.Count > 0)
            {
                if (View.m_errorProvider.HasErrors
                    || (Configuration.IsRealtimeMode 
                    && View.m_dateNavigator.Selection.Cast<DateTime>().Any(date => date.Date < DateTime.Today)))
                {
                    View.m_dateNavigator.Selection.Clear();
                    foreach (DateTime date in m_prevSelectedDates)
                        View.m_dateNavigator.Selection.Add(date);
                    return;
                }

                m_prevSelectedDates = View.m_dateNavigator.Selection.Cast<DateTime>().ToList();
            }

            m_isDataLoading = true;

            TechnicianDetail technician = null;
            if (SelectedTechnicians.Count > 0)
                technician = SelectedTechnicians[0];

            if (technician == null)
            {
                View.m_chkIsWorking.Checked = false;
                technician = Model.TechnicianDefault;
            } else
            {
                bool isAllSelectedWorking = true;
                foreach (DateTime selectedDate in View.m_dateNavigator.Selection)
                {
                    if (!Model.Technicians.ContainsKey(selectedDate.Date))
                    {
                        isAllSelectedWorking = false;
                        break;
                    }
                }

                bool isAllSelectedNotWorking = true;
                foreach (DateTime selectedDate in View.m_dateNavigator.Selection)
                {
                    if (Model.Technicians.ContainsKey(selectedDate.Date))
                    {
                        isAllSelectedNotWorking = false;
                        break;
                    }
                }

                if (isAllSelectedWorking)
                    View.m_chkIsWorking.Checked = true;
                else if (isAllSelectedNotWorking)
                    View.m_chkIsWorking.Checked = false;
                else
                    View.m_chkIsWorking.EditValue = null;
            }
            OnIsWorkingChanged(null, EventArgs.Empty);

            View.m_chkIsContractor.Checked = technician.Technician.IsContractor;

            string name = technician.Technician.Name;
            string depotAddress = technician.Technician.DepotAddress;
            int? driveTimeMinutes = technician.Technician.DriveTimeMinutes;
            int? maxVisitsCount = technician.Technician.MaxVisitsCount;
            int? maxNcoCount = technician.Technician.MaxNonExclusiveVisitsCount;
            TechnicianWorkTime workingInterval = technician.Technician.WorkingIntervals[0];
            decimal? rate = technician.Technician.HourlyRate;
            decimal? rate150to300 = technician.Technician.HourlyRate150to300;
            decimal? rateMore300 = technician.Technician.HourlyRateMore300;
            string primaryZipCodes = technician.PrimaryZipCodesText;
            string secondaryZipCodes = technician.SecondaryZipCodesText;
            BindingList<TechnicianService> services = Model.IsDefaultSettingsMode ? 
                new BindingList<TechnicianService>(Model.TechnicianDefault.Services.ToList()) 
                : new BindingList<TechnicianService>(technician.Services.ToList());

            foreach (TechnicianDetail technicianDetail in SelectedTechnicians)
            {                
                if (technicianDetail.Technician.Name != name)
                    name = null;
                if (technicianDetail.Technician.DepotAddress != depotAddress)
                    depotAddress = null;
                if (technicianDetail.Technician.DriveTimeMinutes != driveTimeMinutes)
                    driveTimeMinutes = null;
                if (technicianDetail.Technician.MaxVisitsCount != maxVisitsCount)
                    maxVisitsCount = null;
                if (technicianDetail.Technician.MaxNonExclusiveVisitsCount != maxNcoCount)
                    maxNcoCount = null;
                if (workingInterval != null
                    && (technicianDetail.Technician.WorkingIntervals[0].TimeStart.TimeOfDay != workingInterval.TimeStart.TimeOfDay
                    || technicianDetail.Technician.WorkingIntervals[0].TimeEnd.TimeOfDay != workingInterval.TimeEnd.TimeOfDay))
                {
                    workingInterval = null;
                }
                if (technicianDetail.Technician.HourlyRate != rate)
                    rate = null;
                if (technicianDetail.Technician.HourlyRate150to300 != rate150to300)
                    rate150to300 = null;
                if (technicianDetail.Technician.HourlyRateMore300 != rateMore300)
                    rateMore300 = null;
                if (technicianDetail.PrimaryZipCodesText != primaryZipCodes)
                    primaryZipCodes = null;
                if (technicianDetail.SecondaryZipCodesText != secondaryZipCodes)
                    secondaryZipCodes = null;

                if (services.Count == 0)
                    continue;

                if (technicianDetail.Services.Count() != services.Count)
                    services = new BindingList<TechnicianService>();
                else
                {
                    for (int i = 0; i < services.Count; i++)
                    {
                        if (services[i].Name != technicianDetail.Services.ElementAt(i).Name
                            || services[i].ServiceAllowance != technicianDetail.Services.ElementAt(i).ServiceAllowance)
                        {
                            services = new BindingList<TechnicianService>();
                        }
                    }
                }
            }


            View.m_txtName.EditValue = name;
            View.m_txtDepotAddress.EditValue = depotAddress;
            if (driveTimeMinutes == null)
                View.m_cmbDriveTime.EditValue = null;
            else
                View.m_cmbDriveTime.Duration = new TimeSpan(0, driveTimeMinutes.Value, 0);
            View.m_txtMaxJobsCount.EditValue = maxVisitsCount;
            View.m_txtMaxNcoCount.EditValue = maxNcoCount;

            if (workingInterval != null)
            {
                View.m_timeWorkStart.Time = workingInterval.TimeStart;
                View.m_timeWorkEnd.Time = workingInterval.TimeEnd;                
            }
            else
            {
                View.m_timeWorkStart.EditValue = null;
                View.m_timeWorkEnd.EditValue = null;                                
            }

            View.m_txtRate.EditValue = rate;
            View.m_txtRate150to300.EditValue = rate150to300;
            View.m_txtRateMore300.EditValue = rateMore300;

            View.m_txtPrimaryZips.EditValue = primaryZipCodes;
            View.m_txtSecondaryZips.EditValue = secondaryZipCodes;

            View.m_gridServices.DataSource = services;            
            View.m_gridServices.RefreshDataSource();

            if (!Model.IsDefaultSettingsMode)
            {
                foreach (ImageComboBoxItem item in View.m_cmbPresets.Properties.Items)
                {
                    TechnicianWorkTimeDefaultPreset preset = (TechnicianWorkTimeDefaultPreset) item.Value;

                    if (preset == null)
                        continue;

                    if (!View.m_chkIsWorking.Checked)
                    {
                        if (preset.PresetNumber == 2)
                        {
                            View.m_cmbPresets.SelectedIndex
                                = View.m_cmbPresets.Properties.Items.IndexOf(item);
                            break;                                
                        }
                    }
                    else
                    {
                        if (View.m_timeWorkStart.Time.TimeOfDay == preset.TimeStart.Value.TimeOfDay
                            && View.m_timeWorkEnd.Time.TimeOfDay == preset.TimeEnd.Value.TimeOfDay)
                        {
                            View.m_cmbPresets.SelectedIndex
                                = View.m_cmbPresets.Properties.Items.IndexOf(item);
                            break;
                        }                                                
                    }
                }

                if (View.m_cmbPresets.SelectedIndex == -1)
                    View.m_cmbPresets.SelectedIndex = View.m_cmbPresets.Properties.Items.Count - 1;
            }
            else
            {
                View.m_txtEmail.Text = Model.TechnicianDefault.Email;
            }

            m_isDataLoading = false;
        }        

        #endregion

        #region OnIsWorkingChanged

        private void OnIsWorkingChanged(object sender, EventArgs args)
        {
            if (View.m_chkIsWorking.EditValue == null)
                EnableDisableControls(false);
            else if (View.m_chkIsWorking.Checked)
            {
                EnableDisableControls(true);

                foreach (DateTime selectedDate in View.m_dateNavigator.Selection)
                {
                    if (!Model.Technicians.ContainsKey(selectedDate.Date))
                    {
                        TechnicianDetail technician = Model.TechnicianDefault.GetTechnicianDefaultClone(selectedDate);
                        technician.IsDirty = true;
                        Model.Technicians.Add(selectedDate.Date, technician);
                    }                        
                }

                OnDataChanged(null, EventArgs.Empty);
            }                
            else
            {
                EnableDisableControls(false);

                foreach (DateTime selectedDate in View.m_dateNavigator.Selection)
                {
                    if (Model.Technicians.ContainsKey(selectedDate.Date))
                        Model.Technicians.Remove(selectedDate.Date);
                }
            } 
            
            View.m_dateNavigator.Refresh();
        }

        private void EnableDisableControls(bool isEnable)
        {
            View.m_cmbPresets.Enabled = isEnable;
            View.m_timeWorkStart.Enabled = isEnable;
            View.m_timeWorkEnd.Enabled = isEnable;

            View.m_txtName.Enabled = isEnable;
            View.m_txtServmanId.Enabled = isEnable;
            View.m_txtDepotAddress.Enabled = isEnable;
            View.m_txtEmail.Enabled = isEnable;
            View.m_cmbDriveTime.Enabled = isEnable;
            View.m_txtMaxJobsCount.Enabled = isEnable;
            View.m_txtMaxNcoCount.Enabled = isEnable;

            View.m_txtRate.Enabled = isEnable;
            View.m_txtRate150to300.Enabled = isEnable;
            View.m_txtRateMore300.Enabled = isEnable;

            View.m_gridServices.Enabled = isEnable;
            View.m_chkIsContractor.Enabled = Model.IsDefaultSettingsMode;

            if (!View.m_chkIsContractor.Checked)
            {
                View.m_txtPrimaryZips.Enabled = isEnable;
                View.m_txtSecondaryZips.Enabled = isEnable;
            }
        }

        #endregion


        #region OnPresetsSelectedIndexChanged

        private void OnPresetsSelectedIndexChanged(object sender, EventArgs args)
        {
            ImageComboBoxItem selectedItem = (ImageComboBoxItem) View.m_cmbPresets.SelectedItem;
            if (selectedItem.Value != null)
            {
                TechnicianWorkTimeDefaultPreset preset = (TechnicianWorkTimeDefaultPreset) selectedItem.Value;
                m_isPresetsComboChanging = true;
                View.m_timeWorkStart.Time = preset.TimeStart.Value;
                View.m_timeWorkEnd.Time = preset.TimeEnd.Value;

                OnWorkTimeValidating(View.m_timeWorkStart, null);
                OnWorkTimeValidating(View.m_timeWorkEnd, null);

                m_isPresetsComboChanging = false;
            }
        }

        private void OnDailyWorkTimeChanged(object sender, EventArgs args)
        {
            if (m_isPresetsComboChanging)
                return;            

            //Set to custom
            View.m_cmbPresets.SelectedIndex = View.m_cmbPresets.Properties.Items.Count - 1;
        }

        #endregion


        #region Validation

        private string GetErrorText(TechnicianDetailValidationError.ErrorFieldEnum errorField)
        {
            List<TechnicianDetail> details = SelectedTechnicians;
            if (details.Count == 0)
                return string.Empty;

            List<TechnicianDetailValidationError> errors = TechnicianDetailValidationError.GetErrors(
                details[0], Model.IsDefaultSettingsMode);
            foreach (TechnicianDetailValidationError error in errors)
            {
                if (error.ErrorField == errorField)
                    return error.ErrorText;
            }

            return string.Empty;
        }

        private void OnNameValidating(object sender, CancelEventArgs args)
        {
            View.m_errorProvider.SetError(View.m_txtName, GetErrorText(TechnicianDetailValidationError.ErrorFieldEnum.Name));
        }

        private void OnServmanIdValidating(object sender, CancelEventArgs cancelEventArgs)
        {
            View.m_errorProvider.SetError(View.m_txtServmanId, GetErrorText(TechnicianDetailValidationError.ErrorFieldEnum.ServmanId));
        }

        private void OnDepotAddressValidating(object sender, CancelEventArgs args)
        {
            View.m_errorProvider.SetError(View.m_txtDepotAddress, GetErrorText(TechnicianDetailValidationError.ErrorFieldEnum.DepotAddress));
        }

        private void OnEmailValidating(object sender, CancelEventArgs cancelEventArgs)
        {
            View.m_errorProvider.SetError(View.m_txtEmail, GetErrorText(TechnicianDetailValidationError.ErrorFieldEnum.Email));
        }

        private void OnDriveTimeValidating(object sender, CancelEventArgs args)
        {
            View.m_errorProvider.SetError(View.m_cmbDriveTime, GetErrorText(TechnicianDetailValidationError.ErrorFieldEnum.DriveTimeMinutes));
        }

        private void OnMaxJobsCountValidating(object sender, CancelEventArgs args)
        {
            string errorText = GetErrorText(TechnicianDetailValidationError.ErrorFieldEnum.MaxVisitsCount);
            View.m_errorProvider.SetError(View.m_txtMaxJobsCount, errorText);
            if (errorText == string.Empty && View.m_errorProvider.GetError(View.m_txtMaxNcoCount) != string.Empty)
                OnMaxNcoCountValidating(View.m_txtMaxNcoCount, args);
        }

        private void OnMaxNcoCountValidating(object sender, CancelEventArgs args)
        {
            string errorText = GetErrorText(TechnicianDetailValidationError.ErrorFieldEnum.MaxNonExclusiveVisitsCount);
            View.m_errorProvider.SetError(View.m_txtMaxNcoCount, errorText);
            if (errorText == string.Empty && View.m_errorProvider.GetError(View.m_txtMaxJobsCount) != string.Empty)
                OnMaxJobsCountValidating(View.m_txtMaxJobsCount, args);
        }

        private void OnWorkTimeValidating(object sender, CancelEventArgs args)
        {
            TimeEditEx control = (TimeEditEx)sender;
            TimeEditEx associatedControl;

            if (control == View.m_timeWorkStart)
                associatedControl = View.m_timeWorkEnd;
            else if (control == View.m_timeWorkEnd)
                associatedControl = View.m_timeWorkStart;
            else if (control == View.m_timePreset1Start)
                associatedControl = View.m_timePreset1End;
            else if (control == View.m_timePreset1End)
                associatedControl = View.m_timePreset1Start;
            else if (control == View.m_timePreset2Start)
                associatedControl = View.m_timePreset2End;
            else if (control == View.m_timePreset2End)
                associatedControl = View.m_timePreset2Start;
            else if (control == View.m_timePreset3Start)
                associatedControl = View.m_timePreset3End;
            else
                associatedControl = View.m_timePreset3Start;

            bool isStartTime = control.Name.Contains("Start");

            if (Model.IsDefaultSettingsMode && control.Name.Contains("Work"))
                return;

            if (!Model.IsDefaultSettingsMode && control.Name.Contains("Preset"))
                return;

            if (control.EditValue == null && associatedControl.EditValue != null)
            {
                View.m_errorProvider.SetError(control, "Value cannot be Empty");
                return;
            }

            if (control.EditValue != null && associatedControl.EditValue == null)
            {
                View.m_errorProvider.SetError(control, string.Empty);
                return;
            }

            if (control.EditValue == null && associatedControl.EditValue == null)
            {
                View.m_errorProvider.SetError(control, string.Empty);
                View.m_errorProvider.SetError(associatedControl, string.Empty);
                return;
            }

            if (control.Time.TimeOfDay.TotalHours == 0)
                View.m_errorProvider.SetError(control, "Time should be greater than 12AM");
            else if (control.Time.Minute % 15 != 0)
                View.m_errorProvider.SetError(control, "Time minutes should be multiple of 15");
            else if (isStartTime && control.Time.TimeOfDay > associatedControl.Time.TimeOfDay)
                View.m_errorProvider.SetError(control, "Time Start should be less than Time End");
            else if (!isStartTime && control.Time.TimeOfDay < associatedControl.Time.TimeOfDay)
                View.m_errorProvider.SetError(control, "Time End should be greater than Time Start");
            else
            {
                View.m_errorProvider.SetError(control, string.Empty);

                if (View.m_errorProvider.GetError(associatedControl) != string.Empty)
                    OnWorkTimeValidating(associatedControl, null);
            }
        }

        private void OnRateValidating(object sender, CancelEventArgs args)
        {
            TextEdit control = (TextEdit)sender;

            string errorText = string.Empty;
            if (control == View.m_txtRate)
                errorText = GetErrorText(TechnicianDetailValidationError.ErrorFieldEnum.HourlyRate);
            else if (control == View.m_txtRate150to300)
                errorText = GetErrorText(TechnicianDetailValidationError.ErrorFieldEnum.HourlyRate150To300);
            else if (control == View.m_txtRateMore300)
                errorText = GetErrorText(TechnicianDetailValidationError.ErrorFieldEnum.HourlyRateMore300);

            View.m_errorProvider.SetError(control, errorText);
        }

        private void OnZipsValidating(object sender, CancelEventArgs args)
        {
            MemoEdit control = (MemoEdit)sender;

            string errorText;
            if (control == View.m_txtPrimaryZips)
                errorText = GetErrorText(TechnicianDetailValidationError.ErrorFieldEnum.PrimaryZipCodes);
            else
                errorText = GetErrorText(TechnicianDetailValidationError.ErrorFieldEnum.SecondaryZipCodes);

            View.m_errorProvider.SetError(control, errorText);

            MemoEdit anotherControl = control == View.m_txtPrimaryZips ? View.m_txtSecondaryZips : View.m_txtPrimaryZips;
            if (errorText == string.Empty && View.m_errorProvider.GetError(anotherControl) != string.Empty)
                OnZipsValidating(anotherControl, null);
        }

        #endregion

        #region OnGridServicesFocusedColumnChanged

        private void OnGridServicesFocusedColumnChanged(object sender, FocusedColumnChangedEventArgs args)
        {
            if (args.FocusedColumn.Name != View.m_colServiceName.Name)
                View.m_gridServicesView.FocusedColumn = View.m_colServiceName;            
        }

        #endregion

        #region Services

        private void OnGridServicesDoubleClick(object sender, EventArgs args)
        {
            TechnicianService service = (TechnicianService)View.m_gridServicesView.GetRow(
                View.m_gridServicesView.FocusedRowHandle);

            if (service == null)
                return;

            BindingList<TechnicianService> list = (BindingList<TechnicianService>) View.m_gridServices.DataSource;
            int index = list.IndexOf(service);
            service.SetAnotherAllowance();
            list.ResetItem(index);
            View.m_gridServicesView.RefreshData();

            foreach (var technician in SelectedTechnicians)
                technician.Services.ElementAt(index).ServiceAllowance = service.ServiceAllowance;
                            
            OnDataChanged(null, EventArgs.Empty);
        }

        #endregion


        #region Zips

        private void OnZipsKeyPress(object sender, KeyPressEventArgs args)
        {            
            if (!char.IsControl(args.KeyChar) && !char.IsDigit(args.KeyChar) 
                && !char.IsWhiteSpace(args.KeyChar) && args.KeyChar != ',')
            {
                args.Handled = true;
            }                
        }

        #endregion

        #region OnIsContractorCheckedChanged

        private void OnIsContractorCheckedChanged(object sender, EventArgs args)
        {
            View.m_txtPrimaryZips.Enabled = !View.m_chkIsContractor.Checked;
            View.m_txtSecondaryZips.Enabled = !View.m_chkIsContractor.Checked;
        }

        #endregion

        #region OnDateNavigatorDrawDay

        private void OnDateNavigatorDrawDay(object sender, CustomDrawDayNumberCellEventArgs e)
        {
            if (Model.IsDefaultSettingsMode)
                return;

            if (Model.Technicians.Keys.ToList().Contains(e.Date.Date))
            {
                e.Style.ForeColor = View.m_dateNavigator.Selection.Contains(e.Date)
                                        ? Color.Lime : Color.Black;
                e.Style.Font = e.Cache.GetFont(e.Style.Font, FontStyle.Bold);
            }
        }

        #endregion

        #region OnDataChanged

        private void OnDataChanged(object sender, EventArgs args)
        {
            if (m_isDataLoading)
                return;

            foreach (TechnicianDetail selectedTechnician in SelectedTechnicians)
            {
                selectedTechnician.IsDirty = true;

                TimeEditEx workTimeStartContol;
                TimeEditEx workTimeEndContol;

                if (Model.IsDefaultSettingsMode)
                {
                    workTimeStartContol = View.m_timePreset2Start;
                    workTimeEndContol = View.m_timePreset2End;                    
                }
                else
                {
                    workTimeStartContol = View.m_timeWorkStart;
                    workTimeEndContol = View.m_timeWorkEnd;
                }

                if (workTimeStartContol.EditValue != null && workTimeEndContol.EditValue != null)
                {
                    TechnicianWorkTime workingInterval = selectedTechnician.Technician.WorkingIntervals[0];
                    workingInterval.TimeStart = workingInterval.TimeStart.Date.Add(workTimeStartContol.Time.TimeOfDay);
                    workingInterval.TimeEnd = workingInterval.TimeEnd.Date.Add(workTimeEndContol.Time.TimeOfDay);

                    selectedTechnician.Technician.WorkingIntervals.Clear();
                    selectedTechnician.Technician.WorkingIntervals.Add(workingInterval);
                }

                if (View.m_txtName.EditValue != null)
                    selectedTechnician.Technician.Name = View.m_txtName.Text;
                if (View.m_txtServmanId.EditValue != null)
                    selectedTechnician.Technician.ServmanId = View.m_txtServmanId.Text;
                if (View.m_txtDepotAddress.EditValue != null)
                    selectedTechnician.Technician.DepotAddress = View.m_txtDepotAddress.Text;
                if (View.m_txtEmail.EditValue != null)
                    selectedTechnician.Email = View.m_txtEmail.Text;
                if (View.m_cmbDriveTime.EditValue != null)
                    selectedTechnician.Technician.DriveTimeMinutes = (int)View.m_cmbDriveTime.Duration.TotalMinutes;
                if (View.m_txtMaxJobsCount.EditValue != null)
                    selectedTechnician.Technician.MaxVisitsCount = int.Parse(View.m_txtMaxJobsCount.Text);
                if (View.m_txtMaxNcoCount.EditValue != null)
                    selectedTechnician.Technician.MaxNonExclusiveVisitsCount = int.Parse(View.m_txtMaxNcoCount.Text);

                if (View.m_txtRate.EditValue != null)
                    selectedTechnician.Technician.HourlyRate = decimal.Parse(View.m_txtRate.Text, NumberStyles.Currency);
                if (View.m_txtRate150to300.EditValue != null)
                    selectedTechnician.Technician.HourlyRate150to300 = decimal.Parse(View.m_txtRate150to300.Text, NumberStyles.Currency);
                if (View.m_txtRateMore300.EditValue != null)
                    selectedTechnician.Technician.HourlyRateMore300 = decimal.Parse(View.m_txtRateMore300.Text, NumberStyles.Currency);    

                if (!View.m_txtPrimaryZips.Enabled)
                    View.m_txtPrimaryZips.Text = string.Empty;
                else if (View.m_txtPrimaryZips.EditValue != null)
                    selectedTechnician.PrimaryZipCodesText = View.m_txtPrimaryZips.Text;

                if (!View.m_txtSecondaryZips.Enabled)
                    View.m_txtSecondaryZips.Text = string.Empty;
                else if (View.m_txtSecondaryZips.EditValue != null)
                    selectedTechnician.SecondaryZipCodesText = View.m_txtSecondaryZips.Text;
            }
        }

        #endregion        
  
        #region OnOkClick

        private void SetCurrentDate(DateTime date)
        {
            if (View.m_dateNavigator.DateTime.Date != date.Date)
                View.m_dateNavigator.DateTime = date.Date;
        }

        private void OnOkClick(object sender, EventArgs e)
        {
            View.ValidateChildren();
            if (View.m_errorProvider.HasErrors)
                return;

            User.ResetLogOutTimer();
            if (Model.IsDefaultSettingsMode)
            {
                TechnicianDetail technician = Model.Technicians[Model.BaseTechnicianDate];
                technician.Technician.IsContractor = View.m_chkIsContractor.Checked;

                List<TechnicianWorkTimeDefaultPreset> newPresets = new List<TechnicianWorkTimeDefaultPreset>();
                newPresets.Add(new TechnicianWorkTimeDefaultPreset(technician.Technician.TechnicianDefaultId, 1,
                    (DateTime?)View.m_timePreset1Start.EditValue, (DateTime?)View.m_timePreset1End.EditValue));
                newPresets.Add(new TechnicianWorkTimeDefaultPreset(technician.Technician.TechnicianDefaultId, 2,
                    (DateTime?)View.m_timePreset2Start.EditValue, (DateTime?)View.m_timePreset2End.EditValue));
                newPresets.Add(new TechnicianWorkTimeDefaultPreset(technician.Technician.TechnicianDefaultId, 3,
                    (DateTime?)View.m_timePreset3Start.EditValue, (DateTime?)View.m_timePreset3End.EditValue));
                technician.WorkingHoursPresets = newPresets;
            }
                           
            List<TechnicianDetailValidationError> errors = new List<TechnicianDetailValidationError>();
            using (new WaitCursor())
                errors = Model.Save();

            foreach (TechnicianDetailValidationError error in errors)
            {
                if (error.ErrorField == TechnicianDetailValidationError.ErrorFieldEnum.General)
                {
                    XtraMessageBox.Show(error.ErrorText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;                    
                }

                SetCurrentDate(error.ScheduleDate);

                if (error.ErrorField == TechnicianDetailValidationError.ErrorFieldEnum.ServmanId)
                {
                    View.m_errorProvider.SetError(View.m_txtServmanId, error.ErrorText);
                    return;
                }                    

                if (error.ErrorField == TechnicianDetailValidationError.ErrorFieldEnum.DepotAddress)
                {
                    View.m_errorProvider.SetError(View.m_txtDepotAddress, error.ErrorText);
                    return;
                }                    

                if (error.ErrorField == TechnicianDetailValidationError.ErrorFieldEnum.PrimaryZipCodes)
                {
                    View.m_errorProvider.SetError(View.m_txtPrimaryZips, error.ErrorText);
                    return;
                }                    

                if (error.ErrorField == TechnicianDetailValidationError.ErrorFieldEnum.SecondaryZipCodes)
                {
                    View.m_errorProvider.SetError(View.m_txtSecondaryZips, error.ErrorText);
                    return;
                }

                throw new Exception("Unexpected error: " + error.ErrorText);
            }

            View.Destroy();
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion   
    }
}
