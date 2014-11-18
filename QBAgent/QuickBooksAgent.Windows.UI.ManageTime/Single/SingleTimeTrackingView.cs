using System.Globalization;

namespace QuickBooksAgent.Windows.UI.ManageTime.Single
{
    public partial class SingleTimeTrackingView : BaseControl
    {
        public SingleTimeTrackingView()
        {
            InitializeComponent();
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);
            Text = "Single Activity Time Sheet - Q-Agent";                        
        }

        protected override void OnInit()
        {
            base.OnInit();

            // General Page
            Joystick.Add(m_cmbPersonType, m_tabs, m_cmbPerson, m_tabs, m_dtpDate);
            Joystick.Add(m_cmbPerson, m_cmbPersonType, m_dtpDate, m_tabs, m_dtpDate);
            Joystick.Add(m_dtpDate, m_cmbPerson, m_cmbCustomer, m_cmbPersonType, m_cmbCustomer);
            Joystick.Add(m_cmbCustomer, m_dtpDate, m_cmbService, m_dtpDate, m_cmbService);
            Joystick.Add(m_cmbService, m_cmbCustomer, m_chkBillable, m_cmbCustomer, m_chkBillable);
            Joystick.Add(m_chkBillable, m_cmbService, m_curRate, m_cmbService, m_tabs);
            Joystick.Add(m_curRate, m_chkBillable, m_tabs, m_cmbService, m_tabs);

            // Time Page
            
            //Time
            Joystick.Add(m_txtTimeHours, m_tabs, m_cmbTimeMins, m_tabs, m_cmbStartHours);
            Joystick.Add(m_cmbTimeMins, m_txtTimeHours, m_chkStartEnd, m_tabs, m_chkStartEnd);
            
            //Time Start
            Joystick.Add(m_cmbStartHours, m_cmbTimeMins, m_cmbStartMins, m_txtTimeHours, m_cmbEndHours);
            Joystick.Add(m_cmbStartMins, m_cmbStartHours, m_cmbStartPm, m_tabs, m_cmbEndMins);
            Joystick.Add(m_cmbStartPm, m_cmbStartMins, m_cmbEndHours, m_tabs, m_cmbEndPm);

            //Time End
            Joystick.Add(m_cmbEndHours, m_cmbStartPm, m_cmbEndMins, m_cmbStartHours, m_txtBreakHours);
            Joystick.Add(m_cmbEndMins, m_cmbEndHours, m_cmbEndPm, m_cmbStartMins, m_txtBreakHours);
            Joystick.Add(m_cmbEndPm, m_cmbEndMins, m_txtBreakHours, m_cmbStartPm, m_cmbBreakMins);
            
            //Time Break
            Joystick.Add(m_txtBreakHours, m_cmbEndPm, m_cmbBreakMins, m_cmbEndHours, m_chkStartEnd);
            Joystick.Add(m_cmbBreakMins, m_txtBreakHours, m_chkStartEnd, m_cmbEndPm, m_chkStartEnd);

            Joystick.Add(m_chkStartEnd, m_cmbBreakMins, m_tabs, m_txtBreakHours, m_tabs);            
            
//            Joystick.Add(m_txtTimeStart, m_txtTime, m_txtTimeEnd, m_txtTime, m_txtTimeEnd);
//            Joystick.Add(m_txtTimeEnd, m_txtTimeStart, m_txtBreak, m_txtTimeStart, m_txtBreak);
//            Joystick.Add(m_txtBreak, m_txtTimeEnd, m_chkStartEnd, m_txtTimeEnd, m_chkStartEnd);
//            Joystick.Add(m_chkStartEnd, m_txtBreak, m_tabs, m_txtBreak, m_tabs);

            // Description Page
            Joystick.Add(m_txtNotes, m_tabs, m_tabs, m_tabs, m_tabs);

            Joystick.Add(m_tabs, 0, m_chkBillable, m_cmbPersonType);
            Joystick.Add(m_tabs, 1, m_chkStartEnd, m_txtTimeHours);
            Joystick.Add(m_tabs, 2, m_txtNotes, m_txtNotes);
        }
    }
}
