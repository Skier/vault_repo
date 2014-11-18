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

namespace Dalworth.Windows.StartDay.LoadItems
{
    public partial class LoadItemsView : BaseControl
    {
        internal MenuItem m_menuCancel;
        internal MenuItem m_menuBack;

        public LoadItemsView()
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

            Text = "Load Rugs";
            m_menuCancel.Text = "Cancel";
            m_menuBack.Text = "Back";
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
