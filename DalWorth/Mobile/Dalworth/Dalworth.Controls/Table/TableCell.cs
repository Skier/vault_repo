using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Controls
{
    public class TableCell
    {
        int m_columnIndex;

        public int ColumnIndex
        {
            get { return m_columnIndex; }
            set { m_columnIndex = value; }
        }

        int m_rowIndex;

        public int RowIndex
        {
            get { return m_rowIndex; }
            set { m_rowIndex = value; }
        }

        public TableCell() { }

        public TableCell(int rowIndex, int columnIndex)
        {
            m_rowIndex = rowIndex;
            m_columnIndex = columnIndex;
        }

        public override string ToString()
        {
            return String.Format("Row:{0} Column:{1}",
                m_rowIndex, m_columnIndex);
        }
    }
}
