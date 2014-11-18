using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain
{
    public class StartDayDonePackage
    {
        #region WorkTransaction

        private WorkTransaction m_workTransaction;
        public WorkTransaction WorkTransaction
        {
            get { return m_workTransaction; }
            set { m_workTransaction = value; }
        }

        #endregion

        #region LoadAndKeepEquipment

        //key - equipment type ID, value - quantity
        private Dictionary<int, int> m_loadAndKeepEquipment;
        public Dictionary<int, int> LoadAndKeepEquipment
        {
            get { return m_loadAndKeepEquipment; }
            set { m_loadAndKeepEquipment = value; }
        }

        #endregion

        #region EmployeeExecutedId

        private int m_employeeExecutedId;
        public int EmployeeExecutedId
        {
            get { return m_employeeExecutedId; }
            set { m_employeeExecutedId = value; }
        }

        #endregion

        #region SaveStartDayDoneDefault

        public static void SaveStartDayDoneDefault(Work work)
        {
            StartDayDonePackage package = new StartDayDonePackage();
            package.WorkTransaction = new WorkTransaction(0,
                work.ID,
                Configuration.CurrentDispatchId,
                null,
                (int)WorkTransactionTypeEnum.StartDayDone,
                DateTime.Now,
                decimal.Zero,
                false);

            package.LoadAndKeepEquipment = new Dictionary<int, int>();
            foreach (EquipmentType equipmentType in EquipmentType.Find())
                package.LoadAndKeepEquipment.Add(equipmentType.ID, 0);                        

            package.EmployeeExecutedId = Configuration.CurrentDispatchId;

            DateTime startDayDate = new DateTime(
                work.StartDate.Value.Year,
                work.StartDate.Value.Month,
                work.StartDate.Value.Day,
                0, 0, 0);

            SaveStartDayDone(package, startDayDate);
        }

        #endregion

        #region SaveStartDayDone
        
        public static void SaveStartDayDone(StartDayDonePackage package, DateTime? startDayDate)
        {
            Work work = Work.FindByPrimaryKey(package.WorkTransaction.WorkId);
            if (work.WorkStatus == WorkStatusEnum.ReadyForStartDay)
                work.WorkStatus = WorkStatusEnum.StartDayDone;
            if (startDayDate == null)
                work.StartDayDate = DateTime.Now;
            else
                work.StartDayDate = startDayDate.Value;
            Work.Update(work);

            try
            {
                package.WorkTransaction =
                    WorkTransaction.FindBy(work, WorkTransactionTypeEnum.StartDayDone);
            }
            catch (DataNotFoundException){}

            if (package.WorkTransaction.ID != 0)
            {
                package.WorkTransaction.EmployeeId = package.WorkTransaction.EmployeeId;
                package.WorkTransaction.TransactionDate = package.WorkTransaction.TransactionDate;
                WorkTransaction.Update(package.WorkTransaction);

                EquipmentTransaction equipmentTransaction
                    = EquipmentTransaction.FindByWorkTransaction(package.WorkTransaction);
                EquipmentTransaction.DeleteTransactional(equipmentTransaction);

            } else
                WorkTransaction.Insert(package.WorkTransaction);

            EquipmentTransaction newEquipmentTransaction = new EquipmentTransaction(0,
                package.WorkTransaction.ID, package.WorkTransaction.EmployeeId,
                work.StartDayDate.Value, DateTime.Now, string.Empty);

            List<EquipmentTransactionDetail> previousDetails = EquipmentTransactionDetail.FindOnDate(
                newEquipmentTransaction.SequenceDate, work.VanId, null, null);

            List<EquipmentTransactionDetail> details = new List<EquipmentTransactionDetail>();

            foreach (EquipmentTransactionDetail previousDetail in previousDetails)
            {
                int newQuantity = package.LoadAndKeepEquipment[previousDetail.EquipmentTypeId];
                details.Add(new EquipmentTransactionDetail(0, 0, previousDetail.EquipmentTypeId,
                    work.VanId.Value, null, newQuantity, newQuantity - previousDetail.Quantity));                    
            }

            EquipmentTransaction.InsertTransactional(newEquipmentTransaction, details, package.WorkTransaction);
        }

        #endregion

        #region UndoSaveStartDayDone

        public static void UndoSaveStartDayDone(Work work)
        {
            WorkTransaction startDayWorkTransaction
                = WorkTransaction.FindBy(work, WorkTransactionTypeEnum.StartDayDone);

            EquipmentTransaction equipmentTransaction
                = EquipmentTransaction.FindByWorkTransaction(startDayWorkTransaction);
            EquipmentTransaction.DeleteTransactional(equipmentTransaction);

            WorkTransaction.Delete(startDayWorkTransaction);

            work.WorkStatus = WorkStatusEnum.ReadyForStartDay;
            work.StartDayDate = null;
            Work.Update(work);
        }

        #endregion
    }
}
