using System;
using System.Collections.Generic;
using System.Text;
using MobileTech.Windows.UI.Controls;
using MobileTech.Domain;


namespace MobileTech.Windows.UI.DatabaseManager
{
	public class EventInfoTableModel:ListTableModel<EventLog>
	{

		public EventInfoTableModel(List<EventLog> list):base(list)
		{
 
		}

		public override int GetColumnCount()
		{
			return 3;
		}

		public override string GetColumnName(int columnIndex)
		{
			switch (columnIndex)
			{
				case 0:
					return "Type";
				case 1:
					return "Message";
				case 2:
					return "Time";

			}

			throw new Exception("Invalid column");
		}

		public override Type GetColumnClass(int columnIndex)
		{
			return String.Empty.GetType();
		}

		public override object GetValueAt(int rowIndex, int columnIndex)
		{
			switch (columnIndex)
			{
				case 0:
					return m_list[rowIndex].EventType;
				case 1:
					return m_list[rowIndex].Message;
				case 2:
					return m_list[rowIndex].CreateDate.ToString("HH:mm:ss");

			}

			throw new Exception("Invalid column");
		}

		public override bool IsCellEditable(int rowIndex, int columnIndex)
		{
			return columnIndex == 1;
		}

		public override void SetValueAt(object aValue, int rowIndex, int columnIndex)
		{
			
		}
}
}
