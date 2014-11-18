using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace QuickBooksAgent.Windows.UI.Controls
{
    public class DragAndDrop
    {
        Point m_start, m_end;

        public DragAndDrop(int x, int y)
        {
            m_start.X = x;
            m_start.Y = y;
            m_end.X = x;
            m_end.Y = y;
        }

        public Point End
        {
            get { return m_end; }
        }

        public Point Start
        {
            get { return m_start; }
        }

        public int X
        {
            get
            {
                return m_start.X - End.X;
            }
        }

        public int Y
        {
            get
            {
                return m_start.Y - End.Y;
            }
        }

        public void SetEnd(int x, int y)
        {
            m_end.X = x;
            m_end.Y = y;

        }
    }
}
