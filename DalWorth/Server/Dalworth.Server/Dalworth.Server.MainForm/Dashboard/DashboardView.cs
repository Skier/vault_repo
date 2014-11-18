using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Controls;
using DevExpress.Utils.Drawing;
using DevExpress.Utils.Menu;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.Drawing;
using BaseControl=Dalworth.Server.Windows.BaseControl;

namespace Dalworth.Server.MainForm.Dashboard
{
    public partial class DashboardView : BaseControl
    {
        internal SchedulerPopupMenu m_menuPopupAppointment;
        internal SchedulerMenuItem m_menuDispatchVisit;
        internal SchedulerMenuItem m_menuConfirmVisit;
        internal SchedulerMenuItem m_menuArriveVisit;
        internal SchedulerMenuItem m_menuSubmitEtcVisit;
        internal SchedulerMenuItem m_menuCompleteVisit;
        internal SchedulerMenuItem m_menuUndoVisit;
        internal SchedulerMenuItem m_menuVisitDetails;
        internal SchedulerMenuItem m_menuNewVisit;
        internal SchedulerMenuItem m_menuVisitPrint;
        internal SchedulerMenuItem m_menuVisitSend;
        internal SchedulerMenuItem m_menuUnassignVisit;
        internal SchedulerMenuItem m_menuRescheduleVisit;


        internal SchedulerPopupMenu m_menuPopupResource;
        internal SchedulerMenuItem m_menuCreateWork;
        internal SchedulerMenuItem m_menuStartDay;
        internal SchedulerMenuItem m_menuCompleteWork;
        internal SchedulerMenuItem m_menuUndoWork;
        internal SchedulerMenuItem m_menuWorkDetails;
        internal SchedulerMenuItem m_menuSendMessage;

        internal NavigatorCustomButton2 m_btnCustomize;

        public DashboardView()
        {
            InitializeComponent();

            m_menuPopupAppointment = new SchedulerPopupMenu();

            m_menuConfirmVisit = new SchedulerMenuItem("Confirm");
            m_menuConfirmVisit.Shortcut = Shortcut.CtrlF;
            m_menuPopupAppointment.Items.Add(m_menuConfirmVisit);            
            
            m_menuDispatchVisit = new SchedulerMenuItem("Dispatch");
            m_menuDispatchVisit.Shortcut = Shortcut.CtrlD;
            m_menuDispatchVisit.BeginGroup = true;
            m_menuPopupAppointment.Items.Add(m_menuDispatchVisit);
            m_menuArriveVisit = new SchedulerMenuItem("Arrive");
            m_menuArriveVisit.Shortcut = Shortcut.CtrlA;
            m_menuPopupAppointment.Items.Add(m_menuArriveVisit);
            m_menuSubmitEtcVisit = new SchedulerMenuItem("Submit ETC");
            m_menuSubmitEtcVisit.Shortcut = Shortcut.CtrlE;
            m_menuPopupAppointment.Items.Add(m_menuSubmitEtcVisit);
            m_menuCompleteVisit = new SchedulerMenuItem("Complete");
            m_menuCompleteVisit.Shortcut = Shortcut.CtrlC;
            m_menuPopupAppointment.Items.Add(m_menuCompleteVisit);
            
            m_menuVisitDetails = new SchedulerMenuItem("Details");
            m_menuVisitDetails.Shortcut = Shortcut.CtrlL;
            m_menuVisitDetails.BeginGroup = true;
            m_menuPopupAppointment.Items.Add(m_menuVisitDetails);
            m_menuNewVisit = new SchedulerMenuItem("New Visit");
            m_menuNewVisit.Shortcut = Shortcut.CtrlN;
            m_menuPopupAppointment.Items.Add(m_menuNewVisit);
            m_menuVisitPrint = new SchedulerMenuItem("Print");
            m_menuVisitPrint.Shortcut = Shortcut.CtrlP;
            m_menuPopupAppointment.Items.Add(m_menuVisitPrint);
            m_menuVisitSend = new SchedulerMenuItem("Send");
            m_menuVisitSend.Shortcut = Shortcut.CtrlS;
            m_menuPopupAppointment.Items.Add(m_menuVisitSend);            
            m_menuUnassignVisit = new SchedulerMenuItem("Unassign");
            m_menuUnassignVisit.BeginGroup = true;
            m_menuUnassignVisit.Shortcut = Shortcut.Del;
            m_menuPopupAppointment.Items.Add(m_menuUnassignVisit);
            m_menuRescheduleVisit = new SchedulerMenuItem("Reschedule");
            m_menuRescheduleVisit.Shortcut = Shortcut.CtrlR;
            m_menuPopupAppointment.Items.Add(m_menuRescheduleVisit);
            
            m_menuUndoVisit = new SchedulerMenuItem("Undo");
            m_menuUndoVisit.Shortcut = Shortcut.CtrlZ;
            m_menuUndoVisit.BeginGroup = true;
            m_menuPopupAppointment.Items.Add(m_menuUndoVisit);

            ///Resource context menu
            m_menuPopupResource = new SchedulerPopupMenu();

            m_menuCreateWork = new SchedulerMenuItem("Create Work");
            m_menuCreateWork.Shortcut = Shortcut.CtrlShiftC;
            m_menuCreateWork.BeginGroup = true;
            m_menuPopupResource.Items.Add(m_menuCreateWork);
            m_menuStartDay = new SchedulerMenuItem("Start Day");
            m_menuStartDay.Shortcut = Shortcut.CtrlShiftD;
            m_menuPopupResource.Items.Add(m_menuStartDay);
            m_menuCompleteWork = new SchedulerMenuItem("Complete Work");
            m_menuCompleteWork.Shortcut = Shortcut.CtrlShiftO;
            m_menuPopupResource.Items.Add(m_menuCompleteWork);

            m_menuWorkDetails = new SchedulerMenuItem("Work Details");
            m_menuWorkDetails.Shortcut = Shortcut.CtrlShiftL;
            m_menuWorkDetails.BeginGroup = true;
            m_menuPopupResource.Items.Add(m_menuWorkDetails);
            m_menuSendMessage = new SchedulerMenuItem("Send Message");
            m_menuSendMessage.Shortcut = Shortcut.CtrlShiftS;
            m_menuPopupResource.Items.Add(m_menuSendMessage);

            m_menuUndoWork = new SchedulerMenuItem("Undo");
            m_menuUndoWork.Shortcut = Shortcut.CtrlShiftZ;
            m_menuUndoWork.BeginGroup = true;
            m_menuPopupResource.Items.Add(m_menuUndoWork);


            m_btnCustomize = new NavigatorCustomButton2(0, "Arrange Technicians");
            m_dashboard.ResourceNavigator.Buttons.CustomButtons.AddRange(
                new NavigatorCustomButton[1]{m_btnCustomize});
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Dalworth - Dashboard";
        }
    }

    public class NavigatorCustomButton2 : NavigatorCustomButton
    {
        public delegate void ClickHandler();
        public event ClickHandler Click;

        public NavigatorCustomButton2(int imageIndex, string hint) : base(imageIndex, hint) {}

        public override void DoClick()
        {
            base.DoClick();
            if (Click != null)
                Click.Invoke();
        }
    }
}