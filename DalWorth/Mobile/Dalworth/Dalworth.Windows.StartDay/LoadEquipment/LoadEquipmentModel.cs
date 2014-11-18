using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Dalworth.Controls;
using Dalworth.Domain;

namespace Dalworth.Windows.StartDay.LoadEquipment
{
    public class LoadEquipmentModel : StartDayBaseModel, IModel, ITableModel
    {
        private List<WorkEquipment> m_workEquipment;

        #region Init

        public void Init()
        {
            m_workEquipment = WorkEquipment.FindBy(StartDayModel.Work);
        }

        #endregion

        #region ITable Model

        public int GetRowCount()
        {
            return m_workEquipment.Count;
        }

        public int GetColumnCount()
        {
            return 2;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return "Equipment";
            else if (columnIndex == 1)
                return "Count";
            return string.Empty;
        }

        public Type GetColumnClass(int columnIndex)
        {
            if (columnIndex == 0)
                return typeof(Image);
            else
                return typeof(string);
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
                return m_workEquipment[rowIndex].EquipmentType + "(s)";
            else if (columnIndex == 1)
                return m_workEquipment[rowIndex].Quantity;

            return string.Empty;
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            return;
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_workEquipment[rowIndex];
        }

        public event TableModelChangeHandler Change;

        #endregion
    }
}
