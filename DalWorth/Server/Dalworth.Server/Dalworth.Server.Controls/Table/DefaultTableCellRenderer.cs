using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace Dalworth.Server.Controls
{
    public class DefaultTableCellRenderer:DrawControl,ITableCellRenderer
    {
        protected Object m_value;

        public DefaultTableCellRenderer()
        {

        }

        #region ITableCellRenderer Members

        public virtual DrawControl getTableCellRendererComponent(Table table, object value, bool isSelected, bool hasFocus, int row, int column)
        {
            this.BorderColor = table.BorderColor;
            this.Font = table.Font;

            if (!hasFocus)
            {

                if (isSelected)
                {
                    ForeColor = table.SelectionForeColor;
                    BackColor = table.SelectionBackColor;
                }
                else
                {
                    BackColor = (row % 2) == 0 ? table.BackColor : table.AltBackColor;
                    ForeColor = (row % 2) == 0 ? table.ForeColor : table.AltForeColor;
                }
            }
            else
            {
                    ForeColor = table.FocusCellForeColor;
                    BackColor = table.FocusCellBackColor; 
            }

            BorderWidth = 1;


            m_value = value;

            if (this.StringFormat == null)
                this.StringFormat = table.DefaultStringFormat;


            return this;
        }

        #endregion

        public override void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Table.GetBrush(BackColor), Rect);

            if (BorderWidth > 0)
            {
                graphics.DrawRectangle(Table.GetPen(BorderColor, BorderWidth),
                    Rect);
            }

            Draw(graphics, m_value.ToString(), (RectangleF)Rect);
        }

        protected void Draw(Graphics graphics, String text, RectangleF rect)
        {
            if (StringFormat.Alignment == StringAlignment.Far)
            {
                rect.Width -= 5;
            }
            else if (StringFormat.Alignment == StringAlignment.Near)
            {
                rect.X += 5;
            }

            graphics.DrawString(m_value.ToString(), this.Font,
                Table.GetBrush(ForeColor),
                rect, 
                this.StringFormat);
        }
    }
}
