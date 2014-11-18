using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Dalworth.Controls
{
    public class ImageTableCellRenderer:DefaultTableCellRenderer,ITableCellRenderer
    {
        Image m_image;

        bool m_bDrawText = false;

        public bool DrawText
        {
            get { return m_bDrawText; }
            set { m_bDrawText = value; }
        }

        public Image Picture
        {
            get { return m_image; }
            set { m_image = value; }
        }

        public override void Draw(Graphics graphics)
        {

            if(m_image != null)
            {
                graphics.FillRectangle(Table.GetBrush(BackColor), Rect);

                if (BorderWidth > 0)
                {
                    graphics.DrawRectangle(Table.GetPen(BorderColor, BorderWidth),
                        Rect);
                }

                /*graphics.DrawImage(m_image,
                     Rect.X + (int)BorderWidth,
                     Rect.Y + (int)BorderWidth);*/

                graphics.DrawImage(m_image,
                    new Rectangle(Rect.X + (int)BorderWidth, 
                    Rect.Y + (int)BorderWidth,
                    m_image.Width,
                    m_image.Height),
                    new Rectangle(0,0,m_image.Width,m_image.Height),
                    GraphicsUnit.Pixel);				



                if(m_bDrawText)
                {
                    int width = Rect.Width - m_image.Width;
                    int x = Rect.X + m_image.Width + (int)BorderWidth;

                    if (Rect.X + Rect.Width > x)
                    {
                        Draw(graphics,
                            m_value.ToString(),
                            new RectangleF(x, Rect.Y, width, Rect.Height));
                    }
                }


            }else
                base.Draw(graphics);
        }
    }
}
