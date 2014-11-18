using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.Phone;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Vendors
{
    public class VendorsController : SingleFormController<VendorsModel, VendorsView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_table.AddColumn(new TableColumn(0, 0, new VendorRenderer(), null, new VendorHeaderRenderer()));
            View.m_table.AddColumn(new TableColumn(1, 80, new VendorRenderer(), null, new VendorHeaderRenderer()));            
            View.m_table.BindModel(Model);

            View.m_table.RowChanged += new RowHandler(OnTableRowChanged);
        }

        #endregion

        #region OnTableRowChanged

        void OnTableRowChanged(int rowIndex)
        {
            UpdateMenu(rowIndex);
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

            Vendor vendor = (Vendor)Model.GetObjectAt(rowIndex, 0);

            foreach (MenuItem item in View.m_phoneMenus.Keys)
                item.Click -= new EventHandler(OnPhoneItemClick);

            View.m_phoneMenus.Clear();
            View.m_menuCall.MenuItems.Clear();

            MenuItem currentPhoneItem;

            if (vendor.Phone != null && vendor.Phone != string.Empty)
            {
                currentPhoneItem = new MenuItem();
                currentPhoneItem.Text = "Phone: " + vendor.Phone;
                currentPhoneItem.Click += new EventHandler(OnPhoneItemClick);
                View.m_phoneMenus.Add(currentPhoneItem, vendor.Phone);
                View.m_menuCall.MenuItems.Add(currentPhoneItem);
            }

            if (vendor.Mobile != null && vendor.Mobile != string.Empty)
            {
                currentPhoneItem = new MenuItem();
                currentPhoneItem.Text = "Mobile: " + vendor.Mobile;
                currentPhoneItem.Click += new EventHandler(OnPhoneItemClick);
                View.m_phoneMenus.Add(currentPhoneItem, vendor.Mobile);
                View.m_menuCall.MenuItems.Add(currentPhoneItem);
            }

            if (vendor.Fax != null && vendor.Fax != string.Empty)
            {
                currentPhoneItem = new MenuItem();
                currentPhoneItem.Text = "Fax: " + vendor.Fax;
                currentPhoneItem.Click += new EventHandler(OnPhoneItemClick);
                View.m_phoneMenus.Add(currentPhoneItem, vendor.Fax);
                View.m_menuCall.MenuItems.Add(currentPhoneItem);
            }

            if (vendor.Pager != null && vendor.Pager != string.Empty)
            {
                currentPhoneItem = new MenuItem();
                currentPhoneItem.Text = "Pager: " + vendor.Pager;
                currentPhoneItem.Click += new EventHandler(OnPhoneItemClick);
                View.m_phoneMenus.Add(currentPhoneItem, vendor.Pager);
                View.m_menuCall.MenuItems.Add(currentPhoneItem);
            }

            if (vendor.AltPhone != null && vendor.AltPhone != string.Empty)
            {
                currentPhoneItem = new MenuItem();
                currentPhoneItem.Text = "Other: " + vendor.AltPhone;
                currentPhoneItem.Click += new EventHandler(OnPhoneItemClick);
                View.m_phoneMenus.Add(currentPhoneItem, vendor.AltPhone);
                View.m_menuCall.MenuItems.Add(currentPhoneItem);
            }


            if (View.m_phoneMenus.Count == 0)
                View.m_menuCall.Enabled = false;
            else
                View.m_menuCall.Enabled = true;

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

        private class VendorRenderer : DefaultTableCellRenderer
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
                    drawControl.StringFormat.Alignment = StringAlignment.Far;

                return drawControl;
            }
        }

        private class VendorHeaderRenderer : DefaultTableHeaderRenderer
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
