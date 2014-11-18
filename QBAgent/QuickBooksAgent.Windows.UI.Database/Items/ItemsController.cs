using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.DatabaseManager.Items
{
    public class ItemsController:SingleFormController<ItemsModel,ItemsView>
    {
        public override void OnViewLoad()
        {
            base.OnViewLoad();

            View.m_table.AddColumn(new TableColumn(0, 0, new ItemRenderer(), null, new ItemHeaderRenderer()));
            View.m_table.AddColumn(new TableColumn(1, 80, new ItemRenderer(), null, new ItemHeaderRenderer()));                        
            View.m_table.BindModel(Model);

            if (Model.GetRowCount() > 0)
            {
                View.m_table.Select(0);
                View.m_table.Focus();
            }
        }

        #region Renderer Classes

        private class ItemRenderer : DefaultTableCellRenderer
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

        private class ItemHeaderRenderer : DefaultTableHeaderRenderer
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
