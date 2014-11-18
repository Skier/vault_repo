using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.Phone;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Employees
{
    public class EmployeesController : SingleFormController<EmployeesModel, EmployeesView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_table.AddColumn(new TableColumn(0, 0, new EmployeeRenderer(), null, new EmployeeHeaderRenderer()));
            View.m_table.AddColumn(new TableColumn(1, 80, new EmployeeRenderer(), null, new EmployeeHeaderRenderer()));
            View.m_table.BindModel(Model);

            View.m_table.RowChanged += new RowHandler(OnTableRowChanged);
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            base.OnViewLoad();

            if (Model.GetRowCount() > 0)
            {
                View.m_table.Select(0);
                View.m_table.Focus();
            } else
            {
                UpdateMenu(-1);
            }
        }

        #endregion

        #region UpdateMenu

        private void UpdateMenu(int rowIndex)
        {
            if (rowIndex < 0 || Model.GetRowCount() == 0)
            {
                View.m_menuCall.Enabled = false;
                return;
            }

            Employee employee = (Employee)Model.GetObjectAt(rowIndex, 0);
            
            foreach (MenuItem item in View.m_phoneMenus.Keys)
                item.Click -= new EventHandler(OnPhoneItemClick);

            View.m_phoneMenus.Clear();
            View.m_menuCall.MenuItems.Clear();

            MenuItem currentPhoneItem;

            if (employee.Phone != null && employee.Phone != string.Empty)
            {
                currentPhoneItem = new MenuItem();
                currentPhoneItem.Text = "Phone: " + employee.Phone;
                currentPhoneItem.Click += new EventHandler(OnPhoneItemClick);
                View.m_phoneMenus.Add(currentPhoneItem, employee.Phone);
                View.m_menuCall.MenuItems.Add(currentPhoneItem);
            }

            if (employee.Mobile != null && employee.Mobile != string.Empty)
            {
                currentPhoneItem = new MenuItem();
                currentPhoneItem.Text = "Mobile: " + employee.Mobile;
                currentPhoneItem.Click += new EventHandler(OnPhoneItemClick);
                View.m_phoneMenus.Add(currentPhoneItem, employee.Mobile);
                View.m_menuCall.MenuItems.Add(currentPhoneItem);
            }

            if (employee.AltPhone != null && employee.AltPhone != string.Empty)
            {
                currentPhoneItem = new MenuItem();
                currentPhoneItem.Text = "Other: " + employee.AltPhone;
                currentPhoneItem.Click += new EventHandler(OnPhoneItemClick);
                View.m_phoneMenus.Add(currentPhoneItem, employee.AltPhone);
                View.m_menuCall.MenuItems.Add(currentPhoneItem);
            }

            if (View.m_phoneMenus.Count == 0)
                View.m_menuCall.Enabled = false;
            else
                View.m_menuCall.Enabled = true;                                                    
            
        }

        #endregion

        #region OnTableRowChanged

        void OnTableRowChanged(int rowIndex)
        {
            UpdateMenu(rowIndex);
        }

        #endregion  

        #region OnPhoneItemClick

        private void OnPhoneItemClick(object sender, EventArgs e)
        {
            try
            {
                Phone.MakeCall(View.m_phoneMenus[(MenuItem)sender] + "\0", true);
            }
            catch (Exception ex)
            {
                MessageDialog.Show(MessageDialogType.Information, string.Format("Couldn't make phone call: {0}", ex.Message));
            }
        }

        #endregion        

        #region Renderer Classes

        private class EmployeeRenderer : DefaultTableCellRenderer
        {
            public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected, bool hasFocus, int row, int column)
            {
                DrawControl drawControl =
                    base.getTableCellRendererComponent(
                    table,
                    value,
                    isSelected,
                    hasFocus,
                    row,
                    column);

                if (column == 0)
                    drawControl.StringFormat.Alignment = StringAlignment.Near;
                else if (column == 1)
                    drawControl.StringFormat.Alignment = StringAlignment.Center;

                return drawControl;
            }
        }

        private class EmployeeHeaderRenderer : DefaultTableHeaderRenderer
        {
            public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected, bool hasFocus, int row, int column)
            {
                DrawControl drawControl =
                    base.getTableCellRendererComponent(
                    table,
                    value,
                    isSelected,
                    hasFocus,
                    row,
                    column);

                drawControl.StringFormat.Alignment = StringAlignment.Center;

                return drawControl;
            }
        }

        #endregion        
    }
}
