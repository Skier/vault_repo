using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Controls;
using Dalworth.Domain;
using Dalworth.Domain.Sync;
using Dalworth.Domain.SyncService;
using Dalworth.SDK;
using Item=Dalworth.Domain.SyncService.Item;
using WorkTransaction=Dalworth.Domain.WorkTransaction;
using WorkTransactionPaymentTypeEnum=Dalworth.Domain.WorkTransactionPaymentTypeEnum;

namespace Dalworth.Windows.ServiceVisit.ServiceVisit
{
    public class ServiceVisitModel : IModel, ITableModel
    {
        #region Visit

        private VisitPackage m_visit;
        public VisitPackage Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion        

        #region IsRugPickup

        public bool IsRugPickup
        {
            get { return Visit.Tasks[0].Task.TaskTypeId == (int) TaskTypeEnum.RugPickup; }
        }

        #endregion

        #region IsRugDelivery

        public bool IsRugDelivery
        {
            get { return Visit.Tasks[0].Task.TaskTypeId == (int) TaskTypeEnum.RugDelivery; }
        }

        #endregion

        #region IsUnknownTaskType

        public bool IsUnknownTaskType
        {
            get { return Visit.Tasks[0].Task.TaskTypeId == (int) TaskTypeEnum.Unknown; }
        }

        #endregion

        #region Init

        public void Init()
        {
            
        }

        #endregion

        #region GetTaskViewTotal

        public decimal GetTasksViewTotal()
        {
            decimal result = decimal.Zero;

            foreach (TaskPackage taskPackage in m_visit.Tasks)
            {
                if (taskPackage.Task.TaskTypeId == (int) TaskTypeEnum.RugPickup)
                    continue;                

                foreach (Item item in taskPackage.Items)
                {
                    result += item.TotalCost;
                }                
            }
            return result;                
        }

        #endregion

        #region GetTaskViewTotalText

        public string GetTaskViewTotalText(TaskPackage taskPackage)
        {

            decimal result = decimal.Zero;

            foreach (Item item in taskPackage.Items)
            {
                result += item.TotalCost;
            }

            if (taskPackage.Task.TaskTypeId == (int) TaskTypeEnum.RugPickup)
                return string.Format("[{0}]", result.ToString("C"));
            return result.ToString("C");
        }

        #endregion

        #region CompleteVisit

        public PaymentResult CompleteVisit(WorkTransactionPayment payment)
        {
            if (payment == null || payment.WorkTransactionPaymentTypeId == (int)WorkTransactionPaymentTypeEnum.Cash)
            {
                Domain.Visit.Complete(Configuration.CurrentTechnicianId, Visit, payment, false);                
                return new PaymentResult();
            } else
            {
                DalworthSyncService service = new DalworthSyncService();
                PaymentResult result = service.CompleteVisit(Configuration.ConnectionKey, 
                    Configuration.CurrentTechnicianId, Visit.Visit.ID, Visit.Tasks, payment);

                if (result.IsAccepted)
                    Domain.Visit.Complete(Configuration.CurrentTechnicianId, Visit, payment, true);

                return result;
            }            
        }

        #endregion

        #region ITableModel

        public int GetRowCount()
        {
            return m_visit.Tasks.Length;            
        }

        public int GetColumnCount()
        {
            return 2;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return "Task";
            return "Cost";
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
            if (columnIndex == 0)
                return TaskType.GetText((TaskTypeEnum)m_visit.Tasks[rowIndex].Task.TaskTypeId);
            else
            {
                return GetTaskViewTotalText(m_visit.Tasks[rowIndex]);
            }                
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            throw new NotImplementedException();
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_visit.Tasks[rowIndex].Task;
        }

        public event TableModelChangeHandler Change;

        #endregion
    }
}
