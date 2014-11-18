using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Dalworth.Controls;
using Dalworth.Windows.StartDay.Resources;

namespace Dalworth.Windows.StartDay.LoadEquipment
{
    public partial class LoadEquipmentView : BaseControl
    {
        internal MenuItem m_menuCancel;
        internal MenuItem m_menuBack;

        public LoadEquipmentView()
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

            Text = "Load Equipment";
            m_menuCancel.Text = "Cancel";
            m_menuBack.Text = "Back";
        }


        protected override void OnInit()
        {
            Joystick.Add(m_table, m_txtEquipmentNotes, m_txtEquipmentNotes, m_txtEquipmentNotes, m_txtEquipmentNotes);
            Joystick.Add(m_txtEquipmentNotes, m_table, m_table, m_table, m_table);            
        }
    }

    #region SelectionRenderer

    internal class SelectionRenderer : ImageTableCellRenderer
    {
        #region Constructor

        public SelectionRenderer()
        {
            DrawText = false;
        }

        #endregion

        #region DrawControl

        public override DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected, bool hasFocus, int row, int column)
        {
            base.getTableCellRendererComponent(table, value, isSelected, hasFocus, row, column);

            if (isSelected)
                Picture = Resource.Selected;
            else
                Picture = Resource.Unselected;

            return this;
        }

        #endregion
    }

    #endregion                
}
