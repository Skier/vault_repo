using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.ManageTime.Single;
using QuickBooksAgent.Windows.UI.ManageTime.Weekly;

namespace QuickBooksAgent.Windows.UI.ManageTime.Menu
{
    public class ManageTimeMenuController : SingleFormController<ManageTimeMenuModel, ManageTimeMenuView>
    {
        protected override void OnInitialize()
        {
            View.m_mbSingleTimeSheet.Enabled = Model.IsSingleTimeTrackingAllowed;
            View.m_mbWeeklyTimeSheet.Enabled = View.m_mbSingleTimeSheet.Enabled;
            View.m_mbSingleTimeSheet.Click += new EventHandler(OnSingleTimeSheetClick);
            View.m_mbWeeklyTimeSheet.Click += new EventHandler(OnWeeklyTimeSheetClick);  
            View.m_menuSingleTimeSheet.Click += new EventHandler(OnSingleTimeSheetClick);
            View.m_menuWeeklyTimeSheet.Click += new EventHandler(OnWeeklyTimeSheetClick);  
        }

        public override void OnViewLoad()
        {
            base.OnViewLoad();
            View.m_mbSingleTimeSheet.Select();

            IsDefaultActionExist = false;
            DefaultActionName = "None";
        }

        private void OnSingleTimeSheetClick(object sender, EventArgs e)
        {
            SingleTimeTrackingController singleTimeTrackingController;

            object user = null;
            if (Configuration.App.UseUserIdentification)
            {                                
                if (Configuration.App.UserType == "Employee")
                {
                    user = new Employee(Configuration.App.UserId);
                } else if (Configuration.App.UserType == "Vendor")
                {
                    user = new Vendor(Configuration.App.UserId);
                }                
            }

            if (user != null)
            {
                singleTimeTrackingController
                    = SingleFormController.Prepare<SingleTimeTrackingController>(null, false, user, null, true);
            } else
            {
                singleTimeTrackingController
                    = SingleFormController.Prepare<SingleTimeTrackingController>();                
            }
            
            singleTimeTrackingController.Closed += new SingleFormClosedHandler(OnSingleTimeTrackingClosed);
            singleTimeTrackingController.Execute();
        }

        private void OnSingleTimeTrackingClosed(SingleFormController controller)
        {
            View.m_mbSingleTimeSheet.Focus();
        }

        private void OnWeeklyTimeSheetClick(object sender, EventArgs e)
        {
            WeeklyTimeTrackingController weeklyTimeTrackingController
                = SingleFormController.Prepare<WeeklyTimeTrackingController>();

            weeklyTimeTrackingController.Closed += new SingleFormClosedHandler(OnWeeklyTimeTrackingClosed);
            weeklyTimeTrackingController.Execute();
        }

        private void OnWeeklyTimeTrackingClosed(SingleFormController controller)
        {
            View.m_mbWeeklyTimeSheet.Focus();
        }
    }
}
