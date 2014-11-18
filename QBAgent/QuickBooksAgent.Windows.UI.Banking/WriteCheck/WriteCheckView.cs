using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.Banking.WriteCheck
{
    public partial class WriteCheckView : BaseControl
    {
        internal MenuItem m_menuAddExpence = new MenuItem();
        internal MenuItem m_menuEditExpence = new MenuItem();
        internal MenuItem m_menuDeleteExpence = new MenuItem();
        
        public WriteCheckView()
        {
            InitializeComponent();

            MenuItems.Add(m_menuAddExpence);
            MenuItems.Add(m_menuEditExpence);
            MenuItems.Add(m_menuDeleteExpence);

            m_menuEditExpence.Enabled = false;
            m_menuDeleteExpence.Enabled = false;
            m_menuAddExpence.Enabled = false;
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);
            Text = "Write Check - Q-Agent";
            
            m_menuAddExpence.Text = "Add Expence Line";
            m_menuEditExpence.Text = "Edit Expence Line";
            m_menuDeleteExpence.Text = "Delete Expence Line";
            
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            //General
            Joystick.Add(m_cmbBankAccount, m_tabs, m_cmbPayeeType, m_tabs, m_cmbPayee);
            Joystick.Add(m_cmbPayeeType, m_cmbBankAccount, m_cmbPayee, m_cmbBankAccount, m_txtCheckNumber);
            Joystick.Add(m_cmbPayee, m_cmbPayeeType, m_txtCheckNumber, m_cmbBankAccount, m_txtCheckNumber);
            Joystick.Add(m_txtCheckNumber, m_cmbPayee, m_dtpDate, m_cmbPayee, m_dtpDate);
            Joystick.Add(m_dtpDate, m_txtCheckNumber, m_chkToBePrinted, m_txtCheckNumber, m_curAmount);
            Joystick.Add(m_chkToBePrinted, m_dtpDate, m_curAmount, m_dtpDate, m_tabs);
            Joystick.Add(m_curAmount, m_chkToBePrinted, m_tabs, m_dtpDate, m_tabs);
            
            //Additional
            Joystick.Add(m_txtMemo, m_tabs, m_tabs, m_tabs, m_tabs);
            
            //Expences            
            Joystick.Add(m_table, m_tabs, m_tabs, m_tabs, m_tabs);
            
            //Tabs
            Joystick.Add(m_tabs, 0, m_curAmount, m_cmbBankAccount);
            Joystick.Add(m_tabs, 1, m_txtMemo, m_txtMemo);
            Joystick.Add(m_tabs, 2, m_table, m_table);

        }
    }

    public class ExpenceLineTableCellRenderer : DefaultTableCellRenderer
    {
        public override DrawControl getTableCellRendererComponent(
            Table table, object value, bool isSelected,
            bool hasFocus, int row, int column)
        {
            DrawControl drawControl = base.getTableCellRendererComponent(
                table, value, isSelected, hasFocus, row, column);

            if (column == 1)
                drawControl.StringFormat.Alignment = StringAlignment.Far;
            else
                drawControl.StringFormat.Alignment = StringAlignment.Center;

            return drawControl;
        }
    }

    public class ExpenceLineTableHeaderRenderer : DefaultTableHeaderRenderer
    {
        public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected,
                                                                  bool hasFocus, int row, int column)
        {
            DrawControl drawControl
                = base.getTableCellRendererComponent(table, value,
                    isSelected, hasFocus, row, column);

            if (column == 1)
                drawControl.StringFormat.Alignment = StringAlignment.Far;
            else 
                drawControl.StringFormat.Alignment = StringAlignment.Center;

            return drawControl;
        }
    }    
    
}
