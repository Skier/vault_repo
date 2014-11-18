using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Dalworth.Controls;
using Dalworth.Windows.ServiceVisit.Resources;

namespace Dalworth.Windows.ServiceVisit.ViewTask
{
    public partial class ViewTaskView : BaseControl
    {
        internal MenuItem m_menuAddRug;
        internal MenuItem m_menuEditRug;
        internal MenuItem m_menuViewRug;
        internal MenuItem m_menuDeleteRug;

        public ViewTaskView()
        {
            InitializeComponent();

            m_menuAddRug = new MenuItem();
            m_menuEditRug = new MenuItem();
            m_menuViewRug = new MenuItem();
            m_menuDeleteRug = new MenuItem();

            m_menuAddRug.Text = "Add Rug";
            m_menuEditRug.Text = "Edit Rug";
            m_menuViewRug.Text = "View Rug";
            m_menuDeleteRug.Text = "Delete Rug";

            MenuItemsRight.Add(m_menuAddRug);
            MenuItemsRight.Add(m_menuEditRug);
            MenuItemsRight.Add(m_menuViewRug);
            MenuItemsRight.Add(m_menuDeleteRug);
        }


        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "View Task";
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
