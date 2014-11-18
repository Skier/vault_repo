using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Controls;
using Dalworth.Domain;
using Dalworth.Domain.Sync;
using Dalworth.Domain.SyncService;
using Dalworth.SDK;
using Item=Dalworth.Domain.SyncService.Item;

namespace Dalworth.Windows.ServiceVisit.ViewTask
{
    public class ViewTaskModel : IModel, ITableModel
    {
        #region Task

        private TaskPackage m_task;
        public TaskPackage Task
        {
            get { return m_task; }
            set { m_task = value; }
        }

        #endregion        

        #region IsRugPickup

        public bool IsRugPickup
        {
            get { return Task.Task.TaskTypeId == (int)TaskTypeEnum.RugPickup; }
        }

        #endregion

        #region IsRugDelivery

        public bool IsRugDelivery
        {
            get { return Task.Task.TaskTypeId == (int)TaskTypeEnum.RugDelivery; }
        }

        #endregion

        #region IsUnknownTaskType

        public bool IsUnknownTaskType
        {
            get { return Task.Task.TaskTypeId == (int) TaskTypeEnum.Unknown; }
        }

        #endregion

        #region Init

        public void Init()
        {            
        }

        #endregion

        #region ITableModel

        public int GetRowCount()
        {
            return Task.Items.Length;
        }

        public int GetColumnCount()
        {
            if (IsRugPickup)
                return 3;
            return 2;
        }

        public string GetColumnName(int columnIndex)
        {
            if ((columnIndex == 1 && IsRugPickup) || (columnIndex == 0 && !IsRugPickup))
                return "Dimension";
            else if ((columnIndex == 2 && IsRugPickup) || (columnIndex == 1 && !IsRugPickup))
                return "Cost";

            return string.Empty;
        }

        public Type GetColumnClass(int columnIndex)
        {
            return typeof(string);
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            Item item = Task.Items[rowIndex];

            if ((columnIndex == 1 && IsRugPickup) || (columnIndex == 0 && !IsRugPickup))
            {
                if (item.ItemShapeId == (int) ItemShapeEnum.Rectangle)
                {
                    return "Rect, " + item.Width
                           + "x" + item.Height
                           + ", " + (item.Width * item.Height).ToString("0.00")
                           + "SF";
                }
                else if (item.ItemShapeId == (int) ItemShapeEnum.Round)
                {
                    return "Round, D" + item.Diameter
                           + ", " + ((decimal.ToDouble(item.Diameter * item.Diameter) * Math.PI) / 4).ToString("0.00")
                           + "SF";
                }
                return string.Empty;
            }
            else if ((columnIndex == 2 && IsRugPickup) || (columnIndex == 1 && !IsRugPickup))
                return item.TotalCost.ToString("C");

            return string.Empty;
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            throw new NotImplementedException();
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            throw new NotImplementedException();
        }

        public event TableModelChangeHandler Change;

        #endregion
        
    }
}
