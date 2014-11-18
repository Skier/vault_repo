using System;
using System.Collections.Generic;
using System.Text;


namespace MobileTech.Windows.UI.Controls
{
	public abstract class ListTableModel<T>:ITableModel
	{
		protected List<T> m_list;

		public ListTableModel()
		{
			m_list = new List<T>();
		}

		public ListTableModel(List<T> list)
		{
			m_list = list;
		}


		protected String GetColumnNameError(int columnIndex)
		{
			throw new Exception("Invalid column number " + columnIndex);
		}


		protected Type GetColumnClassError(int columnIndex)
		{
			throw new Exception("Invalid column number " + columnIndex);
		}

		public Object GetValueAtError(int rowIndex, int columnIndex)
		{
 			throw new Exception("Invalid column or row number ");
		}

		public T GetObject(int rowIndex)
		{
			return m_list[rowIndex];
		}

		#region ITableModel Members

        public virtual int GetRowCount()
        {
            return m_list.Count;
        }

        public abstract int GetColumnCount();

        public abstract string GetColumnName(int columnIndex);

        public abstract Type GetColumnClass(int columnIndex);

        public abstract Object GetValueAt(int rowIndex, int columnIndex);

		public virtual bool IsCellEditable(int rowIndex, int columnIndex)
		{
			return false;
		}

		public virtual void SetValueAt(object aValue, int rowIndex, int columnIndex)
		{
			throw new Exception("The method or operation is not implemented.");
		}

        public virtual Object GetObjectAt(int rowIndex, int columnIndex)
        {
            return GetObject(rowIndex);
        }

        public event TableModelChangeHandler Change;

        protected void FireChangeEvent()
        {
            if (Change != null)
                Change.Invoke();
        }

        #endregion

        public int Add(T item)
        {
            m_list.Insert(0, item);

            if (Change != null)
                Change.Invoke();

            return 0;
        }

        public int Add(List<T> itemsList)
        {
            m_list.AddRange(itemsList);

            if (Change != null)
                Change.Invoke();

            return 0;
        }
}
}
