using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Domain;
using Dalworth.Domain.Sync;
using Dalworth.Domain.SyncService;
using Dalworth.SDK;
using Item=Dalworth.Domain.Item;
using Work=Dalworth.Domain.Work;
using WorkStatusEnum=Dalworth.Domain.WorkStatusEnum;
using WorkTransaction=Dalworth.Domain.WorkTransaction;
using WorkTransactionEquipment=Dalworth.Domain.WorkTransactionEquipment;
using WorkTransactionItem=Dalworth.Domain.WorkTransactionItem;
using WorkTransactionVanCheck=Dalworth.Domain.WorkTransactionVanCheck;
using SyncService = Dalworth.Domain.SyncService;

namespace Dalworth.Windows.StartDay.VanCheck
{
    public class VanCheckModel : StartDayBaseModel, IModel
    {
        #region StepNumber

        private int m_stepNumber;
        public int StepNumber
        {
            get { return m_stepNumber; }
            set { m_stepNumber = value; }
        }

        #endregion

        #region WorkTransactionVanCheck

        private WorkTransactionVanCheck m_workTransactionVanCheck;
        public WorkTransactionVanCheck WorkTransactionVanCheck
        {
            get { return m_workTransactionVanCheck; }
            set { m_workTransactionVanCheck = value; }
        }

        #endregion

        #region Init

        public void Init()
        {
            
        }

        #endregion

        #region WriteStartDayTransaction

        public void WriteStartDayTransaction()
        {            
            WorkTransaction transaction = new WorkTransaction(
                0,
                StartDayModel.Work.ID,
                null,
                (int)Dalworth.Domain.WorkTransactionTypeEnum.StartDayDone,
                DateTime.Now,
                decimal.Zero,
                true,
                string.Empty);

            Counter.Assign(transaction);
            WorkTransaction.Insert(transaction);

            StartDayModel.Work.WorkStatus = WorkStatusEnum.StartDayDone;
            Work.Update(StartDayModel.Work);

            if (StartDayModel.CapturedEquipment == null)
                StartDayModel.CapturedEquipment = new List<WorkTransactionEquipment>();

            foreach (WorkTransactionEquipment equipment in StartDayModel.CapturedEquipment)
            {
                equipment.WorkTransactionId = transaction.ID;
                Counter.Assign(equipment);                
            }

            WorkTransactionEquipment.Insert(StartDayModel.CapturedEquipment);

            List<Item> deliveryItems = Item.GetDeliveryItems(StartDayModel.Work);
            List<WorkTransactionItem> workTransactionItems = new List<WorkTransactionItem>();
            foreach (Item item in deliveryItems)
            {
                WorkTransactionItem workTransactionItem = new WorkTransactionItem(0, transaction.ID, item.ID, false, true);
                Counter.Assign(workTransactionItem);
                workTransactionItems.Add(workTransactionItem);
            }
            WorkTransactionItem.Insert(workTransactionItems);


            WorkTransactionVanCheck.WorkTransactionId = transaction.ID;            
            Domain.WorkTransactionVanCheck.Insert(WorkTransactionVanCheck);

            Application.Clear();
            Application application = new Application(1, 0, StartDayModel.Work.ID);
            application.ApplicationState = ApplicationStateEnum.StartDayDone;
            Application.Insert(application);

            //Prepare server package            
            StartDayDonePackage package = new StartDayDonePackage();            
            package.WorkTransaction = new SyncService.WorkTransaction();
            package.WorkTransaction.AmountCollected = transaction.AmountCollected;
            package.WorkTransaction.VisitId = transaction.VisitId;
            package.WorkTransaction.TransactionDate = transaction.TransactionDate;
            package.WorkTransaction.WorkId = transaction.WorkId;
            package.WorkTransaction.WorkTransactionTypeId = transaction.WorkTransactionTypeId;
            
            List<SyncService.WorkTransactionEquipment> equipments = new List<SyncService.WorkTransactionEquipment>();
            foreach (WorkTransactionEquipment equipment in StartDayModel.CapturedEquipment)
            {
                SyncService.WorkTransactionEquipment serverEquipment = new SyncService.WorkTransactionEquipment();
                serverEquipment.EquipmentId = equipment.EquipmentId;
                serverEquipment.IsCaptured = equipment.IsCaptured;
                serverEquipment.IsLeft = equipment.IsLeft;
                equipments.Add(serverEquipment);
            }
            package.CapturedEquipments = equipments.ToArray();

            List<SyncService.WorkTransactionItem> items = new List<SyncService.WorkTransactionItem>();
            foreach (WorkTransactionItem item in workTransactionItems)
            {
                Item localItem = Item.FindByPrimaryKey(item.ItemId.Value);

                SyncService.WorkTransactionItem serverItem = new SyncService.WorkTransactionItem();
                serverItem.IsCaptured = item.IsCaptured;
                serverItem.IsLeft = item.IsLeft;
                serverItem.ItemId = localItem.ServerId;
                items.Add(serverItem);
            }
            package.CapturedItems = items.ToArray();

            package.VanCheck = new SyncService.WorkTransactionVanCheck();
            package.VanCheck.HobbsReading = WorkTransactionVanCheck.HobbsReading;
            package.VanCheck.OdometerReading = WorkTransactionVanCheck.OdometerReading;
            package.VanCheck.OilChecked = WorkTransactionVanCheck.OilChecked;
            package.VanCheck.SpecialNeeds = WorkTransactionVanCheck.SpecialNeeds;
            package.VanCheck.SuppliesStocked = WorkTransactionVanCheck.SuppliesStocked;
            package.VanCheck.UnitClean = WorkTransactionVanCheck.UnitClean;
            package.VanCheck.VanClean = WorkTransactionVanCheck.VanClean;

            DalworthSyncService service = new DalworthSyncService();
            service.SaveStartDayDone(Configuration.ConnectionKey, package);
        }

        #endregion        
    }
}
