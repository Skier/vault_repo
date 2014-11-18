using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Dalworth.Server.Controls
{
    public abstract class DrawControl
    {
        private Rectangle m_rect;
        private Font m_font;
        private StringFormat m_stringFormat;

        private Color m_foreColor;
        private Color m_backColor;

        private float m_borderWidth = 1;

        private Color m_borderColor = Color.Empty;



        public StringFormat StringFormat
        {
            get { return m_stringFormat; }
            set { m_stringFormat = value; }
        }

        public Font Font
        {
            get { return m_font; }
            set { m_font = value; }
        }


        public Color BackColor
        {
            get { return m_backColor; }
            set { m_backColor = value; }
        }


        public Color ForeColor
        {
            get { return m_foreColor; }
            set { m_foreColor = value; }
        }




        public float BorderWidth
        {
            get { return m_borderWidth; }
            set { m_borderWidth = value; }
        }




        public Rectangle Rect
        {
            get { return m_rect; }
            set { m_rect = value; }
        }

        public Color BorderColor
        {
            get { return m_borderColor; }
            set { m_borderColor = value; }
        }

        public abstract void Draw(Graphics graphics);
    }
}
