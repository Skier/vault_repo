using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Data;
using Dalworth.Domain.Package;
using Dalworth.Domain.SyncService;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public class ItemDeliveryInfo
    {
        #region VisitId

        private int m_visitId;
        public int VisitId
        {
            get { return m_visitId; }
            set { m_visitId = value; }
        }

        #endregion

        #region VisitNumber

        private string m_visitNumber;
        public string TaskNumber
        {
            get { return m_visitNumber; }
            set { m_visitNumber = value; }
        }

        #endregion

        #region FirstName

        private string m_firstName;
        public string FirstName
        {
            get { return m_firstName; }
            set { m_firstName = value; }
        }

        #endregion

        #region LastName

        private string m_lastName;
        public string LastName
        {
            get { return m_lastName; }
            set { m_lastName = value; }
        }

        #endregion

        #region ItemCount

        private int m_itemCount;
        public int ItemCount
        {
            get { return m_itemCount; }
            set { m_itemCount = value; }
        }

        #endregion

        #region DisplayName

        public string DisplayName
        {
            get { return m_firstName + ", " + m_lastName; }
        }

        #endregion
    }

    public partial class Visit : ICounterField
    {
        public Visit(){ }

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        public string CounterName
        {
            get { return "Visit"; }
        }

        #endregion        

        #region VisitStatus

        public VisitStatusEnum VisitStatus
        {
            get { return (VisitStatusEnum)m_visitStatusId; }
            set { m_visitStatusId = (int)value; }
        }

        #endregion

        #region GetItemDeliveryInformation

        private const string SqlGetItemDeliveryInformation =
            @"select v.ID as VisitId, t.Number, c.FirstName, c.LastName, count(*) as ItemCount from WorkDetail wd
		    inner join Visit v on wd.VisitId = v.ID
                    inner join Task t on t.VisitId = v.ID
                    inner join TaskItemDelivery tid on tid.TaskId = t.ID                
                    left join Customer c on c.ID = v.CustomerId
                    where t.TaskTypeId = 2 and wd.WorkId = @WorkId
                group by v.ID, t.Number, c.FirstName, c.LastName";

        public static List<ItemDeliveryInfo> GetItemDeliveryInformation(Work work)
        {
            List<ItemDeliveryInfo> itemDeliveryInfos = new List<ItemDeliveryInfo>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlGetItemDeliveryInformation))
            {
                Database.PutParameter(dbCommand, "@WorkId", work.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        ItemDeliveryInfo deliveryInfo = new ItemDeliveryInfo();

                        deliveryInfo.VisitId = dataReader.GetInt32(0);
                        deliveryInfo.TaskNumber = dataReader.GetString(1);
                        deliveryInfo.FirstName = dataReader.IsDBNull(2)? string.Empty : dataReader.GetString(2);
                        deliveryInfo.LastName = dataReader.IsDBNull(3)? string.Empty : dataReader.GetString(3);
                        deliveryInfo.ItemCount = dataReader.GetInt32(4);

                        itemDeliveryInfos.Add(deliveryInfo);
                    }
                }
            }

            return itemDeliveryInfos;
        }

        #endregion        

        #region Decline

        public static void Decline(int technicianId, int visitId, string reason)
        {
            Visit visit = FindByPrimaryKey(visitId);
            visit.VisitStatus = VisitStatusEnum.Declined;
            Update(visit);

            ApplicationPackage app = ApplicationPackage.GetApplicationPackage();

            WorkTransaction workTransaction = new WorkTransaction();
            Counter.Assign(workTransaction);
            workTransaction.WorkId = app.Work.ID;
            workTransaction.VisitId = visitId;
            workTransaction.WorkTransactionType = WorkTransactionTypeEnum.VisitDeclined;
            workTransaction.TransactionDate = DateTime.Now;
            workTransaction.Notes = reason;
            WorkTransaction.Insert(workTransaction);
        }

        #endregion

        #region SaveReceivedVisit

        public static void SaveReceivedVisit(VisitPackage package, IDbTransaction transaction)
        {            
            //Inserting Address
            Address address = new Address(package.ServiceAddress.ID,
                package.ServiceAddress.Address1,
                package.ServiceAddress.Address2,
                package.ServiceAddress.City,
                package.ServiceAddress.State,
                package.ServiceAddress.Zip,
                package.ServiceAddress.Map);

            try
            {
                Address.FindByPrimaryKey(package.ServiceAddress.ID, transaction);
                Address.Update(address, transaction);
            }
            catch (DataNotFoundException)
            {
                Counter.UpdateIfGreater(address, transaction);
                Address.Insert(address, transaction);
            }

            //Inserting Customer
            Customer customer = new Customer(package.Customer.ID,
                package.Customer.AddressId,
                package.Customer.FirstName,
                package.Customer.LastName,
                package.Customer.Phone1,
                package.Customer.Phone2);

            try
            {
                Customer.FindByPrimaryKey(package.Customer.ID, transaction);
                Customer.Update(customer, transaction);
            }
            catch (DataNotFoundException)
            {
                Counter.UpdateIfGreater(customer, transaction);
                Customer.Insert(customer, transaction);
            }
            
            //Visit
            Visit visit = new Visit(
                package.Visit.ID,
                package.Visit.VisitStatusId,
                package.Visit.CreateDate,
                package.Visit.ServiceDate,
                package.Visit.PreferedTimeFrom,
                package.Visit.PreferedTimeTo,
                package.Visit.CustomerId,
                package.Visit.ServiceAddressId,
                package.Visit.Notes);

            try
            {
                FindByPrimaryKey(package.Visit.ID, transaction);
                Update(visit, transaction);
            }
            catch (DataNotFoundException)
            {
                Counter.UpdateIfGreater(visit, transaction);
                Insert(visit, transaction);
            }      

            //Project and Task
            foreach (TaskPackage taskPackage in package.Tasks)
            {
                Project project = new Project(
                    taskPackage.Project.ID,
                    taskPackage.Project.CustomerId,
                    taskPackage.Project.ServiceAddressId,
                    taskPackage.Project.ProjectTypeId,
                    taskPackage.Project.ProjectStatusId,
                    taskPackage.Project.Description);

                try
                {
                    Project.FindByPrimaryKey(taskPackage.Project.ID, transaction);
                    Project.Update(project, transaction);
                }
                catch (DataNotFoundException)
                {
                    Counter.UpdateIfGreater(project, transaction);
                    Project.Insert(project, transaction);
                }

                //Insert Task
                Task task = new Task(
                    0,
                    taskPackage.Task.ID,
                    taskPackage.Task.ProjectId,
                    package.Visit.ID,
                    taskPackage.Task.TaskTypeId,
                    taskPackage.Task.TaskStatusId,
                    taskPackage.Task.Number,
                    taskPackage.Task.Sequence,
                    taskPackage.Task.CreateDate,
                    taskPackage.Task.ServiceDate,
                    taskPackage.Task.DurationMin,
                    taskPackage.Task.Description,
                    taskPackage.Task.Message,
                    taskPackage.Task.Notes);
                try
                {
                    Task taskLocal = Task.FindByServerId(taskPackage.Task.ID, transaction);
                    task.ID = taskLocal.ID;
                    Task.Update(task, transaction);
                }
                catch (DataNotFoundException)
                {
                    Counter.Assign(task, transaction);
                    Task.Insert(task, transaction);
                }

                //Items                
                foreach (SyncService.Item item in taskPackage.Items)
                {
                    Item localItem = new Item(
                        0,
                        item.ID,
                        item.ItemTypeId,
                        item.SerialNumber,
                        item.ItemShapeId,
                        item.Width,
                        item.Height,
                        item.Diameter,
                        item.IsProtectorApplied,
                        item.IsPaddingApplied,
                        item.IsMothRepelApplied,
                        item.IsRapApplied,
                        item.CleanCost,
                        item.ProtectorCost,
                        item.PaddingCost,
                        item.MothRepelCost,
                        item.RapCost,
                        item.OtherCost,
                        item.SubTotalCost,
                        item.TaxCost,
                        item.TotalCost);

                    try
                    {
                        Item localItemExisting = Item.FindByServerId(item.ID, transaction);
                        localItem.ID = localItemExisting.ID;
                        Item.Update(localItem, transaction);                        
                    }
                    catch (DataNotFoundException)
                    {
                        Counter.Assign(localItem, transaction);
                        Item.Insert(localItem, transaction);                        
                    }
                }

                //TaskEquipmentCaptures
                foreach (SyncService.TaskEquipmentCapture capture in taskPackage.TaskEquipmentCaptures)
                {
                    TaskEquipmentCapture localCapture = new TaskEquipmentCapture(
                        capture.ID,
                        task.ID,
                        capture.EquipmentId);

                    try
                    {
                        TaskEquipmentCapture.FindByPrimaryKey(capture.ID, transaction);
                        TaskEquipmentCapture.Update(localCapture, transaction);
                    }
                    catch (DataNotFoundException)
                    {
                        Counter.UpdateIfGreater(localCapture, transaction);
                        TaskEquipmentCapture.Insert(localCapture, transaction);
                    }
                }

                //TaskItemDeliveries
                foreach (SyncService.TaskItemDelivery delivery in taskPackage.TaskItemDeliveries)
                {
                    Item insertedItem = Item.FindByServerId(delivery.ItemId.Value, transaction);
                    TaskItemDelivery localDelivery = new TaskItemDelivery(
                        delivery.ID,
                        task.ID,
                        insertedItem.ID);

                    try
                    {
                        TaskItemDelivery.FindByPrimaryKey(delivery.ID, transaction);
                        TaskItemDelivery.Update(localDelivery, transaction);
                    }
                    catch (DataNotFoundException)
                    {
                        Counter.UpdateIfGreater(localDelivery, transaction);
                        TaskItemDelivery.Insert(localDelivery, transaction);
                    }
                }

                //TaskItemRequirements
                foreach (SyncService.TaskItemRequirement requirement in taskPackage.TaskItemRequirements)
                {
                    TaskItemRequirement localRequirement = new TaskItemRequirement(
                        requirement.ID,
                        task.ID,
                        requirement.ItemType,
                        requirement.ServiceQuantity,
                        requirement.CaptureQuantity);

                    try
                    {
                        TaskItemRequirement.FindByPrimaryKey(requirement.ID, transaction);
                        TaskItemRequirement.Update(localRequirement, transaction);
                    }
                    catch (DataNotFoundException)
                    {
                        Counter.UpdateIfGreater(localRequirement, transaction);
                        TaskItemRequirement.Insert(localRequirement, transaction);
                    }
                }
            }      
        }

        #endregion

        #region NoGo

        public static void NoGo(int technicianId, int visitId)
        {
            Visit visit = FindByPrimaryKey(visitId);
            visit.VisitStatus = VisitStatusEnum.NoGo;
            Update(visit);

            ApplicationPackage app = ApplicationPackage.GetApplicationPackage();

            WorkTransaction workTransaction = new WorkTransaction();
            Counter.Assign(workTransaction);
            workTransaction.WorkId = app.Work.ID;
            workTransaction.VisitId = visitId;
            workTransaction.WorkTransactionType = WorkTransactionTypeEnum.NoGo;
            workTransaction.TransactionDate = DateTime.Now;
            WorkTransaction.Insert(workTransaction);
        }

        #endregion

        #region Complete

        public static void Complete(int technicianId, VisitPackage visit, WorkTransactionPayment payment, bool isTransactionSent)
        {            
            Visit visitLocal = FindByPrimaryKey(visit.Visit.ID);
            visitLocal.VisitStatus = VisitStatusEnum.Completed;
            Update(visitLocal);

            //temporary
            int existingProjectId = 0;
            foreach (TaskPackage taskPackage in visit.Tasks)
            {
                if (taskPackage.Task.ID != 0)
                {
                    existingProjectId = taskPackage.Project.ID;
                    break;
                }
            }
            
            if (existingProjectId == 0)
                throw new DalworthException("Couldn't find existing project");                
            //temporary

            //Inserting work transaction
            WorkTransaction workTransaction = new WorkTransaction();
            Counter.Assign(workTransaction);
            workTransaction.WorkId = ApplicationPackage.GetApplicationPackage().Work.ID;
            workTransaction.VisitId = visit.Visit.ID;
            workTransaction.WorkTransactionType = WorkTransactionTypeEnum.VisitCompleted;
            workTransaction.TransactionDate = DateTime.Now;
            workTransaction.IsSent = isTransactionSent;
            if (payment != null)
                workTransaction.AmountCollected = payment.PaymentAmount;
            WorkTransaction.Insert(workTransaction);

            //going thru the tasks
            foreach (TaskPackage taskPackage in visit.Tasks)
            {
                if (taskPackage.Task.ID == 0) //Newly created task
                {
                    Task task = new Task(0,
                        null,
                        existingProjectId,
                        visit.Visit.ID,
                        taskPackage.Task.TaskTypeId,
                        taskPackage.Task.TaskStatusId,
                        string.Empty,
                        0,
                        DateTime.Now,
                        DateTime.Now,
                        0, string.Empty, string.Empty, string.Empty);
                    Counter.Assign(task);
                    Task.Insert(task);
                    taskPackage.Task.ID = task.ID;
                } else
                {
                    Task exisitingTask = Task.FindByServerId(taskPackage.Task.ID, null);
                    taskPackage.Task.ID = exisitingTask.ID;
                }

                WorkTransactionTask transactionTask = new WorkTransactionTask(
                    0,
                    workTransaction.ID,
                    taskPackage.Task.ID,
                    0,
                    (taskPackage.Task.TaskTypeId == (int)TaskTypeEnum.RugDelivery) ? workTransaction.AmountCollected : 0);
                transactionTask.TaskStatus = TaskStatusEnum.Completed;
                Counter.Assign(transactionTask);
                WorkTransactionTask.Insert(transactionTask);


                if (taskPackage.Task.TaskTypeId == (int)TaskTypeEnum.RugPickup)
                {
                    int counter = 0;
                    foreach (SyncService.Item item in taskPackage.Items) //inserting new items
                    {
                        item.SerialNumber = visit.Visit.ID + " - " + (counter + 1);
                        counter++;

                        Item itemLocal = new Item(
                            0,
                            null,
                            item.ItemTypeId,
                            item.SerialNumber,
                            item.ItemShapeId,
                            item.Width,
                            item.Height,
                            item.Diameter,
                            item.IsProtectorApplied,
                            item.IsPaddingApplied,
                            item.IsMothRepelApplied,
                            item.IsRapApplied,
                            item.CleanCost,
                            item.ProtectorCost,
                            item.PaddingCost,
                            item.MothRepelCost,
                            item.RapCost,
                            item.OtherCost,
                            item.SubTotalCost,
                            item.TaxCost,
                            item.TotalCost);

                        Counter.Assign(itemLocal);
                        Item.Insert(itemLocal);
                        item.ID = itemLocal.ID;

                        TaskItemDelivery delivery = new TaskItemDelivery(0,
                            taskPackage.Task.ID, item.ID);
                        Counter.Assign(delivery);
                        TaskItemDelivery.Insert(delivery);
                    }
                }

                foreach (SyncService.Item item in taskPackage.Items)
                {
                    int itemId;
                    if (taskPackage.Task.TaskTypeId == (int) TaskTypeEnum.RugDelivery)
                    {
                        itemId = Item.FindByServerId(item.ID, null).ID;
                    } else
                    {
                        itemId = item.ID;
                    }


                    WorkTransactionTaskItem workTransactionItem = new WorkTransactionTaskItem(
                        0,
                        transactionTask.ID,
                        itemId,
                        taskPackage.Task.TaskTypeId == (int) TaskTypeEnum.RugDelivery,
                        taskPackage.Task.TaskTypeId == (int) TaskTypeEnum.RugPickup);
                    Counter.Assign(workTransactionItem);
                    WorkTransactionTaskItem.Insert(workTransactionItem);                    
                }
            }
        }

        #endregion

        #region Accept

        public static void Accept(int technicianId, int visitId)
        {
            Visit visit = FindByPrimaryKey(visitId);
            visit.VisitStatus = VisitStatusEnum.Accepted;
            Update(visit);


            WorkTransaction workTransaction = new WorkTransaction();
            Counter.Assign(workTransaction);
            workTransaction.WorkId = ApplicationPackage.GetApplicationPackage().Work.ID;
            workTransaction.VisitId = visitId;
            workTransaction.WorkTransactionType = WorkTransactionTypeEnum.VisitAccepted;
            workTransaction.TransactionDate = DateTime.Now;
            WorkTransaction.Insert(workTransaction);

        }

        #endregion

        #region Etc

        public static void Etc(int technicianId, int visitId, decimal saleAmount, int? hours, int? minutes, string notes)
        {
            WorkTransaction workTransaction = new WorkTransaction();
            Counter.Assign(workTransaction);
            workTransaction.WorkId = ApplicationPackage.GetApplicationPackage().Work.ID;
            workTransaction.VisitId = visitId;
            workTransaction.WorkTransactionType = WorkTransactionTypeEnum.SubmitETC;
            workTransaction.TransactionDate = DateTime.Now;
            WorkTransaction.Insert(workTransaction);

            WorkTransactionEtc etc = new WorkTransactionEtc(workTransaction.ID, saleAmount, hours, minutes, notes);
            WorkTransactionEtc.Insert(etc);
        }

        #endregion

        #region Arrive

        public static void Arrive(int technicianId, int visitId)
        {
            Visit visit = FindByPrimaryKey(visitId);
            visit.VisitStatus = VisitStatusEnum.Arrived;
            Update(visit);

            WorkTransaction workTransaction = new WorkTransaction();
            Counter.Assign(workTransaction);
            workTransaction.WorkId = ApplicationPackage.GetApplicationPackage().Work.ID;
            workTransaction.VisitId = visitId;
            workTransaction.WorkTransactionType = WorkTransactionTypeEnum.VisitArrived;
            workTransaction.TransactionDate = DateTime.Now;
            WorkTransaction.Insert(workTransaction);
        }

        #endregion
    }
}
      