using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Dalworth.Controls;

namespace Dalworth.Windows.StartDay.EnterEquipment
{
    public partial class EnterEquipmentView : BaseControl
    {
        internal MenuItem m_menuCancel;
        internal MenuItem m_menuBack;

        public EnterEquipmentView()
        {
            InitializeComponent();
            m_menuCancel = new MenuItem();
            m_menuBack = new MenuItem();
            MenuItemsLeft.Add(m_menuCancel);
            MenuItemsLeft.Add(m_menuBack);
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);
            m_menuCancel.Text = "Cancel";
            m_menuBack.Text = "Back";
        }

    }

    #region CellRenderer

    class CellRenderer : DefaultTableCellRenderer
    {
        public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected,
                                                                  bool hasFocus, int row, int column)
        {
            DrawControl control = base.getTableCellRendererComponent(table, value, isSelected, hasFocus, row, column);

            if (!table.Model.IsCellEditable(row, column))
            {
                control.BackColor = hasFocus ? Color.Black : Color.LightGray;
            }

            return control;
        }
    }

    #endregion

    #region CellEditor

    class CellEditor : DigitsTableCellEditor
    {
        public delegate void OnEnterPressHandler();
        public event OnEnterPressHandler OnEnterPress;

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                if (OnEnterPress != null)
                    OnEnterPress.Invoke();
            }
            else
                base.OnKeyPress(e);
        }
    }

    #endregion
}
