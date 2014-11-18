using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.Banking.ManageCreditCard
{
    public partial class ManageCreditCardView : BaseControl
    {
        internal MenuItem m_menuAddCard = new MenuItem();
        internal MenuItem m_menuViewEditCard = new MenuItem();
        internal MenuItem m_menuDeleteCard = new MenuItem();

        public ManageCreditCardView()
        {
            InitializeComponent();
            
            m_menuAddCard.Text = "Add";
            m_menuViewEditCard.Text = "View";
            m_menuDeleteCard.Text = "Delete";
            
            MenuItems.Add(m_menuAddCard);
            MenuItems.Add(m_menuViewEditCard);
            MenuItems.Add(m_menuDeleteCard);
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);
            Text = "Manage CC Charges - Q-Agent";
        }
    }

    #region Renderer Classes

    internal class CardTableCellRenderer : DefaultTableCellRenderer
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

    internal class CardTableHeaderCellRenderer : DefaultTableHeaderRenderer
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
