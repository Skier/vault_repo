using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Controls;
using QuickBooksAgent.Windows.UI.ManageTime.Single;

namespace QuickBooksAgent.Windows.UI.ManageTime.Weekly
{
    public class WeeklyTimeTrackingController:SingleFormController<WeeklyTimeTrackingModel,WeeklyTimeTrackingView>
    {
        private Week m_selectedWeek;
        
        #region OnInitialize

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            InitPersonTypes();
            UpdatePersonList((PersonTypeEnum)View.m_cmbPersonType.SelectedItem);            
            
            View.m_cmbPersonType.SelectedIndexChanged += new EventHandler(OnPersonTypeSelectedIndexChanged);
            View.m_cmbPerson.SelectedIndexChanged += new EventHandler(OnWeekChanged);            
            View.m_table.RowChanged += new RowHandler(OnTableRowChanged);
            View.m_table.Enter += new CellValueHandler(OnTableEnter);
            View.m_menuAdd.Click += new EventHandler(OnAddClick);
            View.m_menuCopy.Click += new EventHandler(OnCopyClick);
            View.m_menuEdit.Click += new EventHandler(OnEditClick);
            View.m_menuDelete.Click += new EventHandler(OnDeleteClick);
            //View.m_menuUndo.Click += new EventHandler(OnUndoClick);
            
            //Week Picker
            View.m_btnWeek.Click += new EventHandler(OnWeekClick);
            
            m_selectedWeek = Week.Current;
            View.m_btnWeek.Text = m_selectedWeek.ToString();
            View.m_btnPrevWeek.Click += new EventHandler(OnPrevWeekClick);
            View.m_btnNextWeek.Click += new EventHandler(OnNextWeekClick);
            View.m_monthCalendar.DateChanged += new DateRangeEventHandler(OnMonthCalendarDateChanged);
            View.m_monthCalendar.KeyPress += new KeyPressEventHandler(OnMonthCalendarKeyPress);            
            View.m_btnWeek.TextChanged += new EventHandler(OnWeekChanged);
            
            View.GotFocus += new EventHandler(OnGotFocus);
            View.m_cmbPerson.GotFocus += new EventHandler(OnGotFocus);
            View.m_cmbPersonType.GotFocus += new EventHandler(OnGotFocus);
            View.m_table.GotFocus += new EventHandler(OnGotFocus);     
            View.m_table.DoubleClick += new EventHandler(OnTableDoubleTap);
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            base.OnViewLoad();
            View.m_cmbPersonType.Focus();
            View.m_table.BindModel(Model);
            View.m_table.GetColumn(0).Width = 10;
            View.m_table.GetColumn(2).Width = 50;
            View.m_table.GetColumn(3).Width = 60;

            object user = null;
            if (Configuration.App.UseUserIdentification)
            {
                if (Configuration.App.UserType == "Employee")
                {
                    user = new Employee(Configuration.App.UserId);
                    View.m_cmbPersonType.SelectedIndex = 0;
                }
                else if (Configuration.App.UserType == "Vendor")
                {
                    user = new Vendor(Configuration.App.UserId);
                    View.m_cmbPersonType.SelectedIndex = 1;
                }
            }

            if (user != null)
            {
                View.m_cmbPerson.SelectedItem = user;
                View.m_cmbPersonType.Enabled = false;
                View.m_cmbPerson.Enabled = false;
            }
            

            DefaultActionName = "Edit";
            IsDefaultActionExist = false;
            OnWeekChanged(this, EventArgs.Empty);
        }
        
        #endregion        

        #region OnPersonTypeSelectedIndexChanged

        private void OnPersonTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePersonList((PersonTypeEnum)View.m_cmbPersonType.SelectedItem);
            OnWeekChanged(this, EventArgs.Empty);
        }

        #endregion

        #region WeekPicker

        #region OnWeekClick

        private void OnWeekClick(object sender, EventArgs e)
        {
            View.m_pnlMonthCalendar.Visible = !View.m_pnlMonthCalendar.Visible;                
            
            if (View.m_pnlMonthCalendar.Visible)
            {
                View.m_monthCalendar.Focus();
                View.m_monthCalendar.SelectionStart = m_selectedWeek.StartDay;
                View.m_monthCalendar.SelectionEnd = m_selectedWeek.EndDay;
            }
                
        }

        #endregion

        #region OnNextWeekClick

        private void OnNextWeekClick(object sender, EventArgs e)
        {
            m_selectedWeek = m_selectedWeek.AddWeeks(1);
            View.m_btnWeek.Text = m_selectedWeek.ToString();
            OnMonthCalendarDateChanged(this,
                new DateRangeEventArgs(m_selectedWeek.StartDay, m_selectedWeek.EndDay));
        }

        #endregion

        #region OnPrevWeekClick

        private void OnPrevWeekClick(object sender, EventArgs e)
        {
            m_selectedWeek = m_selectedWeek.SubstractWeeks(1);
            View.m_btnWeek.Text = m_selectedWeek.ToString();
            OnMonthCalendarDateChanged(this,
                new DateRangeEventArgs(m_selectedWeek.StartDay, m_selectedWeek.EndDay));            
        }

        #endregion

        #region OnMonthCalendarDateChanged

        private void OnMonthCalendarDateChanged(object sender, DateRangeEventArgs e)
        {            
            m_selectedWeek = new Week(e.Start);
            View.m_monthCalendar.SelectionStart = m_selectedWeek.StartDay;
            View.m_monthCalendar.SelectionEnd = m_selectedWeek.EndDay;

            View.m_btnWeek.Text = m_selectedWeek.ToString();
        }

        #endregion

        #region OnMonthCalendarKeyPress

        private void OnMonthCalendarKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                View.m_pnlMonthCalendar.Visible = false;
                View.m_btnWeek.Focus();
            }
        }

        #endregion        

        #region OnGotFocus

        private void OnGotFocus(object sender, EventArgs e)
        {
            View.m_pnlMonthCalendar.Visible = false;
        }

        #endregion

        #region OnWeekChanged

        private void OnWeekChanged(object sender, EventArgs e)
        {
            if (View.m_cmbPerson.SelectedIndex < 0)
                return;

            int qbEntityId = 0;

            if ((PersonTypeEnum)View.m_cmbPersonType.SelectedItem == PersonTypeEnum.Employee)
                qbEntityId = ((Employee)View.m_cmbPerson.SelectedItem).QuickBooksListId.Value;
            else if ((PersonTypeEnum)View.m_cmbPersonType.SelectedItem == PersonTypeEnum.Vendor)
                qbEntityId = ((Vendor)View.m_cmbPerson.SelectedItem).QuickBooksListId.Value;

            Model.UpdateWeeklyTimeSheet(m_selectedWeek, qbEntityId);
            UpdateMenu();            
        }

        #endregion
        
        #endregion

        
        #region OnTableRowChanged

        private void OnTableRowChanged(int rowIndex)
        {
            UpdateMenu();
        }

        #endregion

        #region OnTableEnter

        private void OnTableEnter(TableCell cell)
        {
            if (IsDefaultActionExist)
                OnDefaultAction();
            else
                OnAddClick(this, EventArgs.Empty);
        }

        #region OnTableDoubleTap

        private void OnTableDoubleTap(object sender, EventArgs e)
        {
            if (IsDefaultActionExist)
                OnDefaultAction();
            else
                OnAddClick(this, EventArgs.Empty);
        }

        #endregion
        

        #endregion

        #region OnAddClick

        private void OnAddClick(object sender, EventArgs e)
        {
            object person = View.m_cmbPerson.SelectedItem;
            
            DateTime? date = null;

            if (View.m_table.Model.GetRowCount() > 0 && View.m_table.CurrentRowIndex >= 0)
            {
                TimeTrackingTableElement tableElement
                    = (TimeTrackingTableElement)Model.GetObjectAt(View.m_table.CurrentRowIndex, 0);
                if (tableElement.IsDayOfWeek)
                    date = tableElement.DayOfWeekDate;                                        
                else
                    date = tableElement.TimeTracking.TxnDate;
            }
            else 
                date = (m_selectedWeek).StartDay;
            
            SingleTimeTrackingController singleTimeTrackingController
                = SingleFormController.Prepare<SingleTimeTrackingController>(
                    null, false, person, date, true);

            singleTimeTrackingController.Closed += new SingleFormClosedHandler(OnSingleTimeTrackingClosed);
            singleTimeTrackingController.Model.TimeTrackingAffected 
                += new SingleTimeTrackingModel.TimeTrackingAffectedHandler(OnTimeTrackingAffected);
            singleTimeTrackingController.Execute();                                         
        }

        #endregion

        #region OnTimeTrackingAffected

        private void OnTimeTrackingAffected(TimeTracking timeTracking)
        {
            OnWeekChanged(this, EventArgs.Empty);
            
            for (int i = 0; i < Model.GetRowCount(); i++)
            {
                TimeTrackingTableElement tableElement 
                    = (TimeTrackingTableElement) Model.GetObjectAt(i, 0);
                if (tableElement.TimeTracking != null 
                    && tableElement.TimeTracking.TimeTrackingId == timeTracking.TimeTrackingId)
                {
                    View.m_table.Select(i, 1);
                    return;
                }
            }
        }

        #endregion

        #region OnSingleTimeTrackingClosed

        private void OnSingleTimeTrackingClosed(SingleFormController controller)
        {            
            View.m_table.Focus();
            if (View.m_table.CurrentRowIndex < 0)
                View.m_table.Select(0);
        }

        #endregion        

        #region OnEditClick

        private void OnEditClick(object sender, EventArgs e)
        {
            TimeTrackingTableElement tableElement 
                = (TimeTrackingTableElement) Model.GetObjectAt(View.m_table.CurrentRowIndex, 0);
            bool isReadOnly = !tableElement.IsEditAllowed;

            object person;
            
            try
            {
                person = Employee.FindBy(tableElement.TimeTracking.QBEntity.QBEntityId);
            }
            catch (DataNotFoundException)
            {
                person = Vendor.FindBy(tableElement.TimeTracking.QBEntity.QBEntityId);
            }            
                                                            
            SingleTimeTrackingController singleTimeTrackingController
                = SingleFormController.Prepare<SingleTimeTrackingController>(
                    tableElement.TimeTracking, isReadOnly, person, null, false);

            singleTimeTrackingController.Closed += new SingleFormClosedHandler(OnSingleTimeTrackingClosed);
            singleTimeTrackingController.Model.TimeTrackingAffected
                += new SingleTimeTrackingModel.TimeTrackingAffectedHandler(OnTimeTrackingAffected);            
            singleTimeTrackingController.Execute();
        }

        #endregion

        #region OnCopyClick

        private void OnCopyClick(object sender, EventArgs e)
        {
            TimeTrackingTableElement tableElement
                = (TimeTrackingTableElement)Model.GetObjectAt(View.m_table.CurrentRowIndex, 0);

            object person;

            try
            {
                person = Employee.FindBy(tableElement.TimeTracking.QBEntity.QBEntityId);
            }
            catch (DataNotFoundException)
            {
                person = Vendor.FindBy(tableElement.TimeTracking.QBEntity.QBEntityId);
            }

            SingleTimeTrackingController singleTimeTrackingController
                = SingleFormController.Prepare<SingleTimeTrackingController>(
                    tableElement.TimeTracking, false, person, null, true);

            singleTimeTrackingController.Closed += new SingleFormClosedHandler(OnSingleTimeTrackingClosed);
            singleTimeTrackingController.Model.TimeTrackingAffected
                += new SingleTimeTrackingModel.TimeTrackingAffectedHandler(OnTimeTrackingAffected);            
            singleTimeTrackingController.Execute();            
        }

        #endregion

        #region OnDeleteClick

        private void OnDeleteClick(object sender, EventArgs e)
        {
            if (MessageDialog.Show(MessageDialogType.Question,
                "Do you really want to delete this Time Sheet?") == DialogResult.No)
                return;            
            
            TimeTrackingTableElement tableElement
                = (TimeTrackingTableElement)Model.GetObjectAt(View.m_table.CurrentRowIndex, 0);

            Database.Begin();

            try
            {
                if (tableElement.TimeTracking.EntityState == EntityState.Created)
                {
                    TimeTracking.Delete(tableElement.TimeTracking);
                }
                else if (tableElement.TimeTracking.EntityState == EntityState.Synchronized)
                {
                    tableElement.TimeTracking.EntityState = EntityState.Deleted;
                    TimeTracking.Update(tableElement.TimeTracking);
                }

                Database.Commit();
                OnWeekChanged(this, EventArgs.Empty);  
                UpdateMenu();
            }
            catch (Exception ex)
            {
                Database.Rollback();
                throw ex;
            }            
        }

        #endregion

        #region OnUndoClick

//        private void OnUndoClick(object sender, EventArgs e)
//        {
//            TimeTrackingTableElement tableElement
//                = (TimeTrackingTableElement)Model.GetObjectAt(View.m_table.CurrentRowIndex, 0);
//
//            Database.Begin();
//
//            try
//            {
//                tableElement.TimeTracking.EntityState = EntityState.Synchronized;
//                TimeTracking.Update(tableElement.TimeTracking);
//
//                Database.Commit();
//                OnPersonOrWeekChanged(this, EventArgs.Empty);
//                UpdateMenu();
//            }
//            catch (Exception ex)
//            {
//                Database.Rollback();
//                throw ex;
//            }            
//        }

        #endregion

        #region OnDefaultAction

        public override void OnDefaultAction()
        {
            if (DefaultActionName == "Add")
                OnAddClick(this, EventArgs.Empty);
            else
                OnEditClick(this, EventArgs.Empty);
            
            base.OnDefaultAction();
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

        #region UpdateMenu

        private void UpdateMenu()
        {
            if (View.m_table.CurrentRowIndex < 0 || Model.GetRowCount() == 0)
            {
                View.m_menuEdit.Text = "Edit";
                DefaultActionName = "Edit";
                IsDefaultActionExist = false;
                View.m_menuDelete.Enabled = false;
                View.m_menuEdit.Enabled = false;
                View.m_menuCopy.Enabled = false;
                return;
            }
            
            TimeTrackingTableElement tableElement
                = (TimeTrackingTableElement) Model.GetObjectAt(View.m_table.CurrentRowIndex, 0);

            if (tableElement.TimeTracking == null)
            {
                View.m_menuEdit.Text = "Edit";
                DefaultActionName = View.m_menuAdd.Text;                
            }                
            else
            {
                View.m_menuEdit.Text = tableElement.IsEditAllowed ? "Edit" : "View";
                DefaultActionName = View.m_menuEdit.Text;                
            }

            IsDefaultActionExist = true;
            View.m_menuEdit.Enabled = tableElement.IsEditOrViewAllowed;
            View.m_menuCopy.Enabled = tableElement.IsEditOrViewAllowed;
            View.m_menuDelete.Enabled = tableElement.IsDeleteAllowed;            
            
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
    }
}
