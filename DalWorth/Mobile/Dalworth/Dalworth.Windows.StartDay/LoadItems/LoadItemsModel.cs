using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Dalworth.Controls;
using Dalworth.Domain;

namespace Dalworth.Windows.StartDay.LoadItems
{
    public class LoadItemsModel : StartDayBaseModel, IModel, ITableModel
    {
        #region Init

        public void Init()
        {
            StartDayModel.ItemDeliveryInfoList = Visit.GetItemDeliveryInformation(StartDayModel.Work);
        }

        #endregion

        #region ITable Model

        public int GetRowCount()
        {
            return StartDayModel.ItemDeliveryInfoList.Count;
        }

        public int GetColumnCount()
        {
            return 4;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 1)
                return "Task";
            else if (columnIndex == 2)
                return "Customer";
            else if (columnIndex == 3)
                return "Rugs";
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
            if (columnIndex == 1)
                return StartDayModel.ItemDeliveryInfoList[rowIndex].TaskNumber;
            else if (columnIndex == 2)
                return StartDayModel.ItemDeliveryInfoList[rowIndex].DisplayName;
            else if (columnIndex == 3)
                return StartDayModel.ItemDeliveryInfoList[rowIndex].ItemCount;

            return string.Empty;
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            return;
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return StartDayModel.ItemDeliveryInfoList[rowIndex];
        }

        public event TableModelChangeHandler Change;

        #endregion
    }
}
