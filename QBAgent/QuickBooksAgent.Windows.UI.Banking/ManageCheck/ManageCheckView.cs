using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.Banking.ManageCheck
{
    public partial class ManageCheckView : BaseControl
    {
        internal MenuItem m_menuAddCheck = new MenuItem();
        internal MenuItem m_menuViewEditCheck = new MenuItem();
        internal MenuItem m_menuDelete = new MenuItem();
        
        public ManageCheckView()
        {
            InitializeComponent();
            
            m_menuAddCheck.Text = "Add";
            m_menuViewEditCheck.Text = "View";
            m_menuDelete.Text = "Delete";
            
            MenuItems.Add(m_menuAddCheck);
            MenuItems.Add(m_menuViewEditCheck);
            MenuItems.Add(m_menuDelete);
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);
            Text = "Manage Checks - Q-Agent";
        }

        protected override void OnInit()
        {
            base.OnInit();
            
            //TODO: Joystick put here
        }        
    }

    #region Renderer Classes

    internal class CheckTableCellRenderer : DefaultTableCellRenderer
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
                drawControl.StringFormat.Alignment = StringAlignment.Center;
            else if (column == 1)
                drawControl.StringFormat.Alignment = StringAlignment.Near;
            else if (column == 2)
                drawControl.StringFormat.Alignment = StringAlignment.Far;    
                    
            return drawControl;
        }

    }

    internal class CheckTableHeaderCellRenderer : DefaultTableHeaderRenderer
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
