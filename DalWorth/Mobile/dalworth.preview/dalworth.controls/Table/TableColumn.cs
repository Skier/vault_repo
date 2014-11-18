using System;
using System.Collections.Generic;
using System.Text;

namespace MobileTech.Windows.UI.Controls
{
    public class TableColumn
    {
        private int m_index;
        private int m_width;
        private int m_minWidth;
        private int m_preferredWidth;
        private bool m_allowAutoWidth;
        private String m_name;



        private ITableCellRenderer m_headerRenderer;
        private ITableCellRenderer m_cellRenderer;
        private ITableCellEditor m_cellEditor;

        private Object m_value;


        public TableColumn() { }

        public TableColumn(int index)
        {
            Constructor(index, 0, null, null, null);
        }

        public TableColumn(int index,
            int width)
        {
            Constructor(index, width, null, null, null);
        }


        public TableColumn(int index, 
            int width,
            ITableCellRenderer cellRenderer)
        {
            Constructor(index, width, cellRenderer, null, null);
        }

        public TableColumn(int index,
            int width,
            ITableCellRenderer cellRenderer,
            ITableCellEditor cellEditor)
        {
            Constructor(index, width, cellRenderer, cellEditor, null);
        }

        public TableColumn(int index,
            int width,
            ITableCellRenderer cellRenderer, 
            ITableCellEditor cellEditor,
            ITableCellRenderer headerRenderer) 
        {
            Constructor(index, width, cellRenderer, cellEditor, headerRenderer);
        }

        private void Constructor(int index,
            int width,
            ITableCellRenderer cellRenderer,
            ITableCellEditor cellEditor,
            ITableCellRenderer headerRenderer)
        {
            m_index = index;
            m_width = width;

            m_cellRenderer = cellRenderer;
            m_cellEditor = cellEditor;
            m_headerRenderer = headerRenderer;

            m_minWidth = 15;

            if (m_width < m_minWidth)
            {
                m_allowAutoWidth = true;
                m_width = m_minWidth;
            }
        }


        public bool AllowAutoWidth
        {
            get { return m_allowAutoWidth; }
            set { m_allowAutoWidth = value; }
        }

        public int Index
        {
            get { return m_index; }
            set { m_index = value; }
        }

        public int Width
        {
            get { return m_width; }
            set 
            {
                if (value >= m_minWidth)
                    m_width = value;
                else
                    m_width = m_minWidth;

                m_allowAutoWidth = false;
            }
        }

        public int MinWidth
        {
            get { return m_minWidth; }
            set { m_minWidth = value; }
        }

        public int PreferredWidth
        {
            get { return m_preferredWidth; }
            set { m_preferredWidth = value; }
        }


        public ITableCellRenderer HeaderRenderer
        {
            get { return m_headerRenderer; }
            set { m_headerRenderer = value; }
        }

        public ITableCellRenderer CellRenderer
        {
            get { return m_cellRenderer; }
            set { m_cellRenderer = value; }
        }

        public ITableCellEditor CellEditor
        {
            get { return m_cellEditor; }
            set { m_cellEditor = value; }
        }


        public Object Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        public String Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }

    }
}
