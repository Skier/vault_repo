using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;
using MSXML2;

namespace Dalworth.Server.Domain.package
{
    public class VisitCompleteException : DalworthException
    {
        public VisitCompleteException(string message) : base(message) {}
    }

    public class VisitCompletePackage
    {
        #region TechnicianId

        private int m_technicianId;
        public int TechnicianId
        {
            get { return m_technicianId; }
            set { m_technicianId = value; }
        }

        #endregion

        #region Visit

        private Visit m_visit;
        public Visit Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

        #region Tasks

        private List<TaskProjectWrapperComplete> m_tasks;
        public List<TaskProjectWrapperComplete> Tasks
        {
            get { return m_tasks; }
            set { m_tasks = value; }
        }

        #endregion

        #region Work

        private Work m_work;
        public Work Work
        {
            get { return m_work; }
            set { m_work = value; }
        }

        #endregion

        #region CompletionTime

        private DateTime m_completionTime;
        public DateTime CompletionTime
        {
            get { return m_completionTime; }
            set { m_completionTime = value; }
        }

        #endregion

        #region Payments

        private List<WorkTransactionPayment> m_payments;
        public List<WorkTransactionPayment> Payments
        {
            get { return m_payments; }
            set { m_payments = value; }
        }

        #endregion

        #region PaidProjects

        private List<Project> m_paidProjects;
        public List<Project> PaidProjects
        {
            get { return m_paidProjects; }
            set { m_paidProjects = value; }
        }

        #endregion

        #region VanEquipmentDropOff

        private Dictionary<int, int> m_vanEquipmentDropOff;
        public Dictionary<int, int> VanEquipmentDropOff
        {
            get { return m_vanEquipmentDropOff; }
            set { m_vanEquipmentDropOff = value; }
        }

        #endregion

        #region Constructor

        public VisitCompletePackage(int technicianId, Visit visit, List<TaskProjectWrapperComplete> tasks, 
            Work work, List<WorkTransactionPayment> payments, List<Project> paidProjects,
            Dictionary<int, int> vanEquipmentDropOff)
        {
            m_technicianId = technicianId;
            m_visit = visit;
            m_tasks = tasks;
            m_work = work;
            m_payments = payments;
            m_paidProjects = paidProjects;
            m_vanEquipmentDropOff = vanEquipmentDropOff;
        }

        #endregion

        #region MaskCharsButLast4

        private static string MaskCharsButLast4(string input)
        {
            if (input.Length > 4)
            {
                string maskedPart = new string('X', input.Length - 4);
                return maskedPart + input.Substring(input.Length - 4);
            }
            return input;
        }

        #endregion

        #region TransferEquipment

        private void TransferEquipment(int employeeId, Visit visit, Work work, IDbConnection connection)
        {
            WorkTransaction workTransaction = new WorkTransaction(0, work.ID,
              employeeId,
              visit.ID, 0, DateTime.Now, 0, false);
            workTransaction.WorkTransactionType = WorkTransactionTypeEnum.VisitEquipmentTransfer;
            WorkTransaction.Insert(workTransaction, connection);
            DateTime sequenceDate = m_completionTime;

            EquipmentTransaction transaction = new EquipmentTransaction(0, workTransaction.ID,
                employeeId, sequenceDate, DateTime.Now, string.Empty);
            List<EquipmentTransactionDetail> vanEquipments = EquipmentTransactionDetail.FindOnDate(
                sequenceDate, work.VanId.Value, null, null);
            List<EquipmentTransactionDetail> customerEquipments = EquipmentTransactionDetail.FindOnDate(
                sequenceDate, null, visit.ServiceAddressId.Value, null);

            foreach (EquipmentTransactionDetail vanEquipment in vanEquipments)
            {
                vanEquipment.ID = 0;
                vanEquipment.EquipmentTransactionId = 0;
                vanEquipment.VanId = work.VanId.Value;
                vanEquipment.AddressId = null;
                vanEquipment.Quantity -= m_vanEquipmentDropOff[vanEquipment.EquipmentTypeId];
                vanEquipment.QuantityChange = -m_vanEquipmentDropOff[vanEquipment.EquipmentTypeId];
            }

            foreach (EquipmentTransactionDetail customerEquipment in customerEquipments)
            {
                customerEquipment.ID = 0;
                customerEquipment.EquipmentTransactionId = 0;
                customerEquipment.VanId = null;
                customerEquipment.AddressId = visit.ServiceAddressId.Value;
                customerEquipment.Quantity += m_vanEquipmentDropOff[customerEquipment.EquipmentTypeId];
                customerEquipment.QuantityChange = m_vanEquipmentDropOff[customerEquipment.EquipmentTypeId];
            }

            List<EquipmentTransactionDetail> newDetails = new List<EquipmentTransactionDetail>();
            newDetails.AddRange(vanEquipments);
            newDetails.AddRange(customerEquipments);

            EquipmentTransaction.InsertTransactional(transaction, newDetails, workTransaction);
        }

        #endregion

        #region MakePayment

//        private PaymentResult MakePayment(WorkTransaction workTransaction, IDbConnection connection)
//        {
//            if (m_payment == null)
//                return null;
//
//            PaymentResult result;
//
//            if (m_payment.WorkTransactionPaymentType == WorkTransactionPaymentTypeEnum.Cash)
//            {
//                m_payment.WorkTransactionId = workTransaction.ID;
//                m_payment.IsAccepted = true;
//                m_payment.ServerResponse = string.Empty;
//                WorkTransactionPayment.Insert(m_payment, connection);
//
//                result = new PaymentResult();
//                result.IsAccepted = true;
//                result.Amount = m_payment.PaymentAmount;
//
//            }
//            else //Processing CC and checks
//            {
//                string postPaymentString = null;
//
//                if (m_payment.WorkTransactionPaymentType == WorkTransactionPaymentTypeEnum.CreditCard)
//                {
//                    postPaymentString =
//                        string.Format(
//                            "ePNAccount={0}&CardNo={1}&ExpMonth={2}&ExpYear={3}&Total={4}&FirstName={5}&LastName={6}&Address={7}&City={8}&State={9}&Zip={10}&CVV2Type={11}&CVV2={12}&HTML=No",
//                            ConfigurationManager.AppSettings["ProcessingNetworkAccountNumber"],
//                            m_payment.CreditCardNumber.Replace(" ", "+"),
//                            m_payment.CreditCardExpirationDate.Value.Month.ToString("00"),
//                            m_payment.CreditCardExpirationDate.Value.Year.ToString().Substring(2),
//                            m_payment.PaymentAmount.ToString("0.00"),
//                            m_payment.FirstName.Replace(" ", "+"),
//                            m_payment.LastName.Replace(" ", "+"),
//                            m_payment.Address.Replace(" ", "+"),
//                            m_payment.City.Replace(" ", "+"),
//                            m_payment.State.Replace(" ", "+"),
//                            m_payment.Zip.Replace(" ", "+"),
//                            m_payment.CreditCardCVV2TypeId,
//                            m_payment.CreditCardCVV2.Replace(" ", "+"));
//
//                }
//                else if (m_payment.WorkTransactionPaymentType == WorkTransactionPaymentTypeEnum.BankCheck)
//                {
//                    postPaymentString =
//                        string.Format(
//                            "ePNAccount={0}&PaymentType={1}&AccountType={2}&AccountClass={3}&Company={4}&FirstName={5}&LastName={6}&BankName={7}&Routing={8}&CheckAcct={9}&Check={10}&Address={11}&City={12}&State={13}&Zip={14}&Total={15}&HTML=No",
//                            ConfigurationManager.AppSettings["ProcessingNetworkAccountNumber"],
//                            "Check",
//                            (m_payment.BankCheckAccountType.Value == BankCheckAccountTypeEnum.Company) ? "C" : "P",
//                            "Checking",
//                            m_payment.BankCheckCompany.Replace(" ", "+"),
//                            m_payment.FirstName.Replace(" ", "+"),
//                            m_payment.LastName.Replace(" ", "+"),
//                            m_payment.BankCheckBankName.Replace(" ", "+"),
//                            m_payment.BankRouteNumber.Replace(" ", "+"),
//                            m_payment.BankCheckAccountNumber.Replace(" ", "+"),
//                            m_payment.BankCheckNumber.Replace(" ", "+"),
//                            m_payment.Address.Replace(" ", "+"),
//                            m_payment.City.Replace(" ", "+"),
//                            m_payment.State.Replace(" ", "+"),
//                            m_payment.Zip.Replace(" ", "+"),
//                            m_payment.PaymentAmount.ToString("0.00"));
//                }
//
//                XMLHTTP30Class xmlHttp = new XMLHTTP30Class();
//                xmlHttp.open("POST", "https://www.eProcessingNetwork.Com/cgi-bin/tdbe/transact.pl",
//                             false, null, null);
//
//                bool isExceptionOccured = false;
//                try
//                {
//                    xmlHttp.send(postPaymentString);
//                }
//                catch (Exception ex)
//                {
//                    m_payment.IsAccepted = false;
//                    m_payment.ServerResponse = "eProcessingNetwork error: " + ex.Message;
//                    Host.Trace("eProcessingNetwork error", ex.Message + ex.StackTrace);
//                    isExceptionOccured = true;
//                }
//
//                m_payment.WorkTransactionId = workTransaction.ID;
//                if (m_payment.CreditCardNumber != null)
//                {
//                    m_payment.CreditCardNumber = MaskCharsButLast4(m_payment.CreditCardNumber);
//                }
//                else if (m_payment.BankCheckNumber != null)
//                {
//                    m_payment.BankCheckNumber = MaskCharsButLast4(m_payment.BankCheckNumber);
//                }
//
//                if (!isExceptionOccured)
//                {
//                    m_payment.IsAccepted = (xmlHttp.responseText[1] == 'Y');
//                    m_payment.ServerResponse = xmlHttp.responseText;
//                }
//
//                WorkTransactionPayment.Insert(m_payment, connection);
//
//                result = new PaymentResult();
//                result.IsAccepted = m_payment.IsAccepted;
//                result.CreditCardNumber = m_payment.CreditCardNumber;
//                result.BankCheckNumber = m_payment.BankCheckNumber;
//                result.Amount = m_payment.PaymentAmount;
//                result.ServerResponse = m_payment.ServerResponse;
//
//                if (!m_payment.IsAccepted)
//                {
//                    workTransaction.WorkTransactionType = WorkTransactionTypeEnum.VisitCompleteFailed;
//                    workTransaction.AmountCollected = decimal.Zero;
//                    WorkTransaction.Update(workTransaction, connection);
//                }
//            }            
//
//            return result;
//        }

        #endregion

        #region FindExistingTransferredOrGeneratedTasks

        private Dictionary<int, Task> FindExistingTransferredOrGeneratedTasks(Visit visit)
        {
            Dictionary<int, Task> result = new Dictionary<int, Task>();

            if (visit.VisitStatus == VisitStatusEnum.Completed)
            {
                List<Task> existingTransferredTasks = Task.FindTransferredTasksOnCompletion(visit);
                List<Task> existingGeneratedTasks = Task.FindGeneratedTasksOnCompletion(visit);
                List<Task> existingBookedTasks = Task.FindBookedTasksOnCompletion(visit);

                foreach (Task task in existingTransferredTasks)
                    result.Add(task.ID, task);
                foreach (Task task in existingGeneratedTasks)
                    result.Add(task.ID, task);                
                foreach (Task task in existingBookedTasks)
                    result.Add(task.ID, task);                
            }

            return result;
        }

        #endregion

        #region CompleteVisit

        public VisitCompleteResultPackage CompleteVisit(int? dispatchId, IDbConnection connection)
        {
            Work currentWork = Work.FindWorkByTechAndDate(m_technicianId, m_completionTime, connection);
            int employeeId = dispatchId == null ? m_technicianId : dispatchId.Value;
            Visit visit = Visit.FindByPrimaryKey(m_visit.ID, connection);
            visit.Notes = m_visit.Notes;
            bool isRecomplete = visit.VisitStatus == VisitStatusEnum.Completed;

            List<int> insertedTasks = new List<int>();
            List<int> insertedProjects = new List<int>();

            Dictionary<int, Task> existingTransferredOrGeneratedTasks
                = FindExistingTransferredOrGeneratedTasks(visit);
            Dictionary<int, Visit> existingNextVisitsToTasksMap //key - TaskId
                = new Dictionary<int, Visit>();
            List<Visit> nextVisits = new List<Visit>();
            List<int> nextVisitIds = new List<int>();
            foreach (Task task in existingTransferredOrGeneratedTasks.Values)
            {
                Visit nextVisit = Visit.FindNextVisit(task, visit);
                if (!nextVisitIds.Contains(nextVisit.ID))
                {
                    nextVisits.Add(nextVisit);
                    nextVisitIds.Add(nextVisit.ID);
                }
                existingNextVisitsToTasksMap.Add(task.ID, nextVisit);
            }

            WorkTransaction workTransaction;

            if (isRecomplete)
            {
                UndoCompleteVisit(visit, currentWork, true);
                visit = Visit.FindByPrimaryKey(m_visit.ID, connection);

                workTransaction = WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitCompleted);
            }
            else
            {
                workTransaction = new WorkTransaction(0, currentWork.ID,
                                                      0, m_visit.ID, 0, null, 0, false);
                workTransaction.WorkTransactionType = WorkTransactionTypeEnum.VisitCompleted;
            }

            workTransaction.TransactionDate = DateTime.Now;
            workTransaction.EmployeeId = employeeId;

            if (isRecomplete)
                WorkTransaction.Update(workTransaction);
            else
                WorkTransaction.Insert(workTransaction);

            TransferEquipment(employeeId, visit, currentWork, connection);

            WorkDetail detail = WorkDetail.FindByWorkAndVisit(currentWork, visit, connection);
            if (!detail.TimeBeginAssigned.HasValue)
                detail.TimeBeginAssigned = detail.TimeBegin;
            if (!detail.TimeEndAssigned.HasValue)
                detail.TimeEndAssigned = detail.TimeEnd;
            detail.TimeEnd = m_completionTime;
            detail.TimeComplete = m_completionTime;
            WorkDetail.UpdateAndLog(detail, connection);


            visit.ClosedDollarAmount = decimal.Zero;

            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (!task.IsProject && task.IsIncludedInVisit)
                {
                    if (!(task.Task.TaskType == TaskTypeEnum.Deflood && !task.IsDefloodFirstTimeService))
                        visit.ClosedDollarAmount += task.Task.ClosedAmount;
                }
            }

            visit.VisitStatus = VisitStatusEnum.Completed;
            Visit.Update(visit, connection);
            currentWork.ClosedDollarAmount += visit.ClosedDollarAmount;
            Work.Update(currentWork);

            ////            
            //Insert Update tasks, projects, items
            ////
            /// 
            //Insert all new projects    

            //key - project id, value - its dumped id
            Dictionary<int, int> dumpedProjectIdMap = new Dictionary<int, int>();

            foreach (TaskProjectWrapperComplete project in m_tasks)
            {
                if (project.IsProject)
                {
                    WorkTransactionProject workTransactionProject = new WorkTransactionProject(
                        workTransaction.ID, 0, false, false);

                    if (project.IsNewlyAdded)
                    {
                        //Find newly added tasks on this project
                        List<TaskProjectWrapperComplete> projectNewTasks = new List<TaskProjectWrapperComplete>();
                        foreach (TaskProjectWrapperComplete task in m_tasks)
                        {
                            if (!task.IsProject && task.IsNewlyAdded
                                && task.Task.ProjectId == project.Task.ProjectId
                                && !task.Task.ParentTaskId.HasValue)
                            {
                                projectNewTasks.Add(task);
                            }
                        }

                        project.Task.Project.CustomerId = visit.CustomerId;
                        project.Task.Project.ServiceAddressId = visit.ServiceAddressId;
                        project.Task.Project.ProjectStatus = ProjectStatusEnum.Open;

                        if (project.Task.Project.ID <= 0)
                        {
                            Project equalPaidProject = null;
                            foreach (Project paidProject in m_paidProjects)
                            {
                                if (paidProject.ID == project.Task.Project.ID)
                                {
                                    equalPaidProject = paidProject;
                                    break;
                                }
                            }

                            Project.InsertAndLog(project.Task.Project, connection);
                            workTransactionProject.ProjectId = project.Task.Project.ID;
                            workTransactionProject.IsCreated = true;
                            WorkTransactionProject.Insert(workTransactionProject);

                            if (equalPaidProject != null)
                                equalPaidProject.ID = project.Task.Project.ID;
                        }
                        else
                            Project.UpdateAndLog(project.Task.Project, connection);

                        foreach (TaskProjectWrapperComplete task in projectNewTasks)
                        {
                            task.Task.Project = project.Task.Project;
                            task.Task.ProjectId = project.Task.Project.ID;
                        }

                        insertedProjects.Add(project.Task.Project.ID);
                    }
                    else
                    {
                        if (!dumpedProjectIdMap.ContainsKey(project.Task.Project.ID))
                        {
                            Project dumpedProject = Project.Dump(project.Task.Project, workTransaction.ID);
                            dumpedProjectIdMap.Add(project.Task.Project.ID, dumpedProject.ID);
                            Project.UpdateAndLog(project.Task.Project, connection);

                            workTransactionProject.ProjectId = project.Task.Project.ID;
                            workTransactionProject.IsModified = true;
                            WorkTransactionProject.Insert(workTransactionProject);
                        }
                    }
                }
            }

            //Make payments
            foreach (WorkTransactionPayment payment in m_payments)
            {
                payment.WorkTransactionId = workTransaction.ID;
                payment.IsAccepted = true;
                payment.ServerResponse = string.Empty;
                WorkTransactionPayment.Insert(payment);
            }

            foreach (Project project in m_paidProjects)
            {
                Project projectToUpdate = Project.FindByPrimaryKey(project.ID);
                projectToUpdate.PaidAmount += project.PaidAmount;
                Project.UpdateAndLog(projectToUpdate);

                ProjectPayment projectPayment = new ProjectPayment(
                    project.ID, workTransaction.ID, project.PaidAmount);
                ProjectPayment.Insert(projectPayment);
            }


            //Insert update tasks
            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (!task.IsProject && !task.IsNewlyAdded && task.IsIncludedInVisit)
                    Task.Dump(task.Task, workTransaction.ID, dumpedProjectIdMap[task.Task.ProjectId]);
            }

            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (!task.IsProject && task.IsIncludedInVisit)
                {
                    try
                    {
                        Task dumpedTask = Task.FindDumpedTask(task.Task, workTransaction.ID);

                        if (dumpedTask.TaskFailTypeId == null && task.Task.TaskFailTypeId != null)
                        {
                            task.Task.FailCount = dumpedTask.FailCount + 1;

                            if (task.Task.LastFailDate == null
                                || Work.StartDate.Value.Date > task.Task.LastFailDate.Value.Date)
                            {
                                task.Task.LastFailDate = Work.StartDate.Value.Date;
                            }
                        }
                        else if (task.Task.TaskFailTypeId == null)
                        {
                            task.Task.FailCount = dumpedTask.FailCount;
                            task.Task.LastFailDate = dumpedTask.LastFailDate;
                        }
                    }
                    catch (DataNotFoundException)
                    {
                    }

                    InsertUpdateTaskWithChilds(task, visit,
                                               task.TaskAction != TaskActionEnum.Book, connection);

                    try
                    {
                        WorkTransactionTask workTransactionTask
                            = WorkTransactionTask.FindBy(workTransaction, task.Task);
                        if (task.IsNewlyAdded)
                            insertedTasks.Add(task.Task.ID);
                        SetTaskAction(workTransactionTask, task);
                        WorkTransactionTask.Update(workTransactionTask);
                    }
                    catch (DataNotFoundException)
                    {
                        WorkTransactionTask workTransactionTask = new WorkTransactionTask(
                            workTransaction.ID, task.Task.ID, false, false, 0);

                        if (!task.IsNewlyAdded && task.IsIncludedInVisit)
                            workTransactionTask.IsModified = true;
                        else if (task.IsNewlyAdded)
                        {
                            workTransactionTask.IsCreated = true;
                            insertedTasks.Add(task.Task.ID);
                        }
                        SetTaskAction(workTransactionTask, task);
                        WorkTransactionTask.Insert(workTransactionTask);
                    }
                }
            }

            //Insert update items and work transaction elements
            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (task.IsProject || !task.IsIncludedInVisit)
                    continue;

                if (task.Items == null)
                    task.Items = new List<Item>();

                List<Item> existingItems = Item.FindByTask(task.Task, connection);
                foreach (Item item in existingItems)
                    Item.Delete(item);

                foreach (Item item in task.Items)
                {
                    item.ItemType = ItemTypeEnum.Rug;
                    item.TaskId = task.Task.ID;
                    Item.Insert(item);
                }
            }

            // Create Invoice 
            WorkTransactionQbInvoice workTransactionQbInvoice = null;

            if (isRecomplete)
            {
                // if recomplete and invoice already created, don't create new invoice
                try
                {
                    workTransactionQbInvoice = WorkTransactionQbInvoice.FindByWorkTransactionId(workTransaction.ID, null);
                }
                catch (DataNotFoundException)
                {
                }
            }

            if (workTransactionQbInvoice == null)
            {
                List<QbInvoice> invoices = QbInvoice.Create(visit, connection);
                foreach (QbInvoice invoice in invoices)
                {
                    workTransactionQbInvoice = new WorkTransactionQbInvoice(workTransaction.ID, invoice.ID,
                        invoice.QbCustomer != null? (int?) invoice.QbCustomer.ID: null,
                        invoice.QbProject != null? (int?) invoice.QbProject.ID: null,
                    false, true);

                    WorkTransactionQbInvoice.Insert(workTransactionQbInvoice);
                }
            }
        
        //Generate new Visit
            List<TaskProjectWrapperComplete> newVisitTasks = FindTasksToBeAddedToNewVisit();

            Visit generatedDefaultVisit = null;
            Visit generatedDeliveryVisit = null;
            GeneratedVisitActionEnum generatedDefaultVisitAction = GeneratedVisitActionEnum.Unknown;
            GeneratedVisitActionEnum generatedDeliveryVisitAction = GeneratedVisitActionEnum.Unknown;

            foreach (Task task in existingTransferredOrGeneratedTasks.Values)
            {
                if (generatedDefaultVisit != null && generatedDeliveryVisit != null)
                    break;

                if (task.TaskType == TaskTypeEnum.RugDelivery)
                    generatedDeliveryVisit = existingNextVisitsToTasksMap[task.ID];
                else
                    generatedDefaultVisit = existingNextVisitsToTasksMap[task.ID];
            }

            if (generatedDefaultVisit == null)
                generatedDefaultVisitAction = GeneratedVisitActionEnum.Created;
            else
                generatedDefaultVisitAction = GeneratedVisitActionEnum.Updated;

            if (generatedDeliveryVisit == null)
                generatedDeliveryVisitAction = GeneratedVisitActionEnum.Created;
            else
                generatedDeliveryVisitAction = GeneratedVisitActionEnum.Updated;

            List<int> transferredAndOldGeneratedTasks = new List<int>();

            if (newVisitTasks.Count > 0)
            {                
                foreach (TaskProjectWrapperComplete task in newVisitTasks)
                {
                    if (task.IsNewlyAdded)
                    {
                        if (task.Task.ID <= 0)
                        {
                            Task.InsertWithDetails(task.Task, connection);
                            task.Task.Number = task.Task.ID.ToString();
                            Task.UpdateWithDetails(task.Task, connection);

                            WorkTransactionTask workTransactionTask = new WorkTransactionTask(
                                workTransaction.ID, task.Task.ID, false, true, 0);

                            if (task.TaskAction == TaskActionEnum.Book)
                                workTransactionTask.WorkTransactionTaskAction = WorkTransactionTaskActionEnum.Booked;
                            else
                                workTransactionTask.WorkTransactionTaskAction = WorkTransactionTaskActionEnum.Generated;

                            WorkTransactionTask.Insert(workTransactionTask);
                        } else //Generated on previous completion
                        {
                            Task.UpdateWithDetails(task.Task, connection);
                            transferredAndOldGeneratedTasks.Add(task.Task.ID);
                        }                            

                        insertedTasks.Add(task.Task.ID);

                        List<Item> existingItems = Item.FindByTask(task.Task);
                        foreach (Item existingItem in existingItems)
                            Item.Delete(existingItem);

                        if (task.Items != null)
                        {
                            foreach (Item item in task.Items)
                            {
                                item.TaskId = task.Task.ID;
                                Item.Insert(item);
                            }                            
                        }
                    }
                    else //Transferred
                    {
                        Task.UpdateWithDetails(task.Task, connection); 
                        transferredAndOldGeneratedTasks.Add(task.Task.ID);
                    }
                        

                    if (existingTransferredOrGeneratedTasks.ContainsKey(task.Task.ID))
                    {
                        Task.MarkTaskModified(task.Task);
                    } 
                    else 
                    {
                        Visit generatedVisit;

                        if (task.Task.TaskType == TaskTypeEnum.RugDelivery)
                        {
                            if (generatedDeliveryVisit == null)
                                generatedDeliveryVisit = InsertGeneratedVisit(visit);
                                
                            generatedVisit = generatedDeliveryVisit;
                        } else
                        {
                            if (generatedDefaultVisit == null)
                                generatedDefaultVisit = InsertGeneratedVisit(visit);
                                
                            generatedVisit = generatedDefaultVisit;
                        }

                        VisitTask visitTask = new VisitTask(generatedVisit.ID, task.Task.ID);
                        VisitTask.Insert(visitTask);
                    }

                }

                if (generatedDefaultVisit != null)
                    Task.MarkTasksModifiedBy(generatedDefaultVisit);
                if (generatedDeliveryVisit != null)
                    Task.MarkTasksModifiedBy(generatedDeliveryVisit);

            } 

            Task.MarkTasksModifiedBy(visit);


            //Deleting newly inserted and generated tasks
            List<Task> existingInsertedTasks = Task.FindBy(
                workTransaction, false, true);
            List<Task> tasksToBeDeleted = new List<Task>();
            foreach (Task existingInsertedTask in existingInsertedTasks)
            {
                if (!insertedTasks.Contains(existingInsertedTask.ID))
                {
                    WorkTransactionTask workTransactionTask = new WorkTransactionTask(
                        workTransaction.ID, existingInsertedTask.ID);
                    WorkTransactionTask.Delete(workTransactionTask);
                    tasksToBeDeleted.Add(existingInsertedTask);
                }
            }
            Task.DeleteDeep(tasksToBeDeleted);

            //Trying to delete trasferred tasks if needed
            foreach (Task task in existingTransferredOrGeneratedTasks.Values)
            {
                if (!transferredAndOldGeneratedTasks.Contains(task.ID))
                {                    
                    VisitTask visitTask = new VisitTask(
                        existingNextVisitsToTasksMap[task.ID].ID, task.ID);
                    VisitTask.Delete(visitTask);
                }                
            }


            //Delete next visits if empty
            foreach (Visit nextVisit in nextVisits)
            {
                if (Task.FindByVisit(nextVisit).Count == 0)
                {
                    Visit.Delete(nextVisit);

                    if (generatedDefaultVisit != null && nextVisit.ID == generatedDefaultVisit.ID)
                    {
                        generatedDefaultVisit = null;
                        generatedDefaultVisitAction = GeneratedVisitActionEnum.Unknown; 
                    }

                    if (generatedDeliveryVisit != null && nextVisit.ID == generatedDeliveryVisit.ID)
                    {
                        generatedDeliveryVisit = null;
                        generatedDeliveryVisitAction = GeneratedVisitActionEnum.Unknown; 
                    } 
                }                    
            }

            //Delete inserted projects if empty
            List<Project> existingInsertedProjects = Project.FindBy(
                workTransaction, false, true);
            foreach (Project existingInsertedProject in existingInsertedProjects)
            {
                if (!insertedProjects.Contains(existingInsertedProject.ID))
                {
                    WorkTransactionProject workTransactionProject = new WorkTransactionProject(
                        workTransaction.ID, existingInsertedProject.ID);
                    WorkTransactionProject.Delete(workTransactionProject);

                    if (Task.FindByProject(existingInsertedProject).Count == 0)
                        Project.Delete(existingInsertedProject);                    
                }
            }

            List<Task> tasks = Task.FindByVisit(visit);
            foreach (Task task in tasks)
            {
                if (task.TaskFailType == TaskFailTypeEnum.MustReturn)
                {
                    task.TaskFailType = null;
                    Task.Update(task);
                }                    
            }

            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (task.IsProject)
                {
                    Project.UpdateStatus(task.Task.Project);

                    Project project = Project.FindByPrimaryKey(task.Task.Project.ID);

                    if (project.ProjectStatus == ProjectStatusEnum.Completed)
                    {
                        Customer customer = Customer.FindByPrimaryKey(task.Task.Project.CustomerId.Value);
                        if (customer.Email != null && customer.Email != string.Empty)
                        {
                            BackgroundJobPending job = new BackgroundJobPending();
                            job.BackgroundJobTypeId = (int)BackgroundJobTypeEnum.ProjectCompletedEmail;
                            job.ProjectId = task.Task.Project.ID;
                            BackgroundJobPending.Insert(job, connection);
                        }
                    }
                }
            }

            return new VisitCompleteResultPackage(new PaymentResult(), 
                generatedDefaultVisit,
                generatedDeliveryVisit,
                generatedDefaultVisitAction,
                generatedDeliveryVisitAction);            
        }

        #endregion

        #region InsertGeneratedVisit

        private Visit InsertGeneratedVisit(Visit existingVisit)
        {
            Visit generatedVisit = new Visit(
                0, (int)VisitStatusEnum.Pending, DateTime.Now,
                null, 0, null, null,
                existingVisit.CustomerId,
                existingVisit.ServiceAddressId,
                existingVisit.Notes, null, null, null, false, false, false, decimal.Zero, null, false);
            Visit.Insert(generatedVisit);
            return generatedVisit;
        }

        #endregion

        #region FindTasksToBeAddedToNewVisit

        public List<TaskProjectWrapperComplete> FindTasksToBeAddedToNewVisit()
        {
            List<TaskProjectWrapperComplete> result = new List<TaskProjectWrapperComplete>();

            WorkTransaction workTransaction = null;
            Visit visit = Visit.FindByPrimaryKey(m_visit.ID);

            if (visit.VisitStatus == VisitStatusEnum.Completed)
            {
                workTransaction = WorkTransaction.FindBy(m_visit.ID, 
                    WorkTransactionTypeEnum.VisitCompleted);                
            }            

            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (task.IsProject || !task.IsIncludedInVisit)
                    continue;

                if (task.TaskAction == TaskActionEnum.Book)
                {
                    result.Add(task);

                } else if (task.TaskAction == TaskActionEnum.Fail
                    && task.Task.TaskFailType == TaskFailTypeEnum.MustReturn
                    && task.Task.TaskType != TaskTypeEnum.Monitoring
                    && task.Task.TaskType != TaskTypeEnum.Deflood)
                {
                    result.Add(task);
                }
                else if (task.Task.TaskType == TaskTypeEnum.RugPickup
                         && task.TaskAction == TaskActionEnum.Complete)
                {
                    //Generate Rug Delivery
                    Task deliveryTask;

                    try
                    {
                        deliveryTask = Task.FindRugDelivery(task.Task);
                        if (task.Task.Project.ProjectType == ProjectTypeEnum.RugCleaning)
                        {
                            deliveryTask.EstimatedClosedAmount = task.Task.EstimatedClosedAmount;

                            if (deliveryTask.TaskStatus != TaskStatusEnum.Completed)
                            {
                                deliveryTask.DiscountPercentage = task.Task.DiscountPercentage;
                                deliveryTask.IsClosedAmountAutoCalculated = task.Task.IsClosedAmountAutoCalculated;                                
                            }
                        }                            
                    }
                    catch (DataNotFoundException)
                    {
                        deliveryTask = new Task(0, null,
                            task.Task.Project.ProjectType == ProjectTypeEnum.RugCleaning ? task.Task.ServmanOrderNum : string.Empty,
                            task.Task.ProjectId,
                            (int)TaskTypeEnum.RugDelivery,
                            (int)TaskStatusEnum.NotCompleted,
                            null,
                            false,
                            string.Empty,
                            null,
                            DateTime.Now,
                            null,
                            0,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            false,
                            decimal.Zero,
                            task.Task.IsClosedAmountAutoCalculated,
                            decimal.Zero, false,
                            false, DateTime.Now, null, null, null, 0, null, false,
                            task.Task.DiscountPercentage, null);

                        if (task.Task.Project.ProjectType == ProjectTypeEnum.RugCleaning)
                        {
                            deliveryTask.EstimatedClosedAmount = task.Task.EstimatedClosedAmount;
                            deliveryTask.IsEstimatedClosedAmountAutoCalculated = task.Task.IsEstimatedClosedAmountAutoCalculated;
                        }
                    }

                    deliveryTask.CreateDate = CompletionTime;                    

                    TaskProjectWrapperComplete deliveryWrapper = new TaskProjectWrapperComplete(
                        deliveryTask, false, false);
                    deliveryWrapper.IsNewlyAdded = true;

                    if (deliveryTask.TaskStatus != TaskStatusEnum.Completed)
                        deliveryWrapper.Items = task.Items;
                    else
                        deliveryWrapper.Items = Item.FindByTask(deliveryTask);

                    result.Add(deliveryWrapper);
                }
                else if (task.Task.TaskType == TaskTypeEnum.Monitoring
                         && (task.TaskAction == TaskActionEnum.Complete 
                         || (task.TaskAction == TaskActionEnum.Fail && task.Task.TaskFailType == TaskFailTypeEnum.MustReturn)))
                {
                    foreach (TaskProjectWrapperComplete taskInner in m_tasks)
                    {
                        if (!taskInner.IsProject
                            && taskInner.Task.ID == task.Task.ParentTaskId.Value
                            && taskInner.Task.TaskType == TaskTypeEnum.Deflood
                            && (taskInner.TaskAction == TaskActionEnum.InProcess
                                || taskInner.Task.TaskFailType != null))
                        {
                            Task monitoringTask = null;
                            TaskProjectWrapperComplete monitoring;

                            if (task.TaskAction == TaskActionEnum.Fail && task.Task.TaskFailType == TaskFailTypeEnum.MustReturn)
                            {
                                monitoringTask = task.Task;
                                monitoring = new TaskProjectWrapperComplete(monitoringTask, false, false);
                                monitoring.IsNewlyAdded = task.IsNewlyAdded;
                            }
                            else
                            {
                                if (workTransaction != null)
                                {
                                    //Trying to find already generated monitoring
                                    List<Task> allInsertedTasks = Task.FindBy(workTransaction, false, true);
                                    foreach (Task insertedTask in allInsertedTasks)
                                    {
                                        if (insertedTask.TaskType == TaskTypeEnum.Monitoring
                                            && insertedTask.ParentTaskId == taskInner.Task.ID)
                                        {
                                            //This monitoring candidate shouldn't be on current visit
                                            if (!VisitTask.Exists(new VisitTask(m_visit.ID, insertedTask.ID)))
                                            {
                                                monitoringTask = insertedTask;
                                                monitoringTask.MonitoringDetail =
                                                    MonitoringDetail.FindByPrimaryKey(monitoringTask.ID);
                                                monitoringTask.MonitoringReadings
                                                    = MonitoringReading.FindBy(monitoringTask, null);
                                                break;
                                            }

                                        }
                                    }
                                }

                                if (monitoringTask == null)
                                {
                                    monitoringTask = new Task(0,
                                          taskInner.Task.ID,
                                          string.Empty,
                                          task.Task.ProjectId,
                                          (int)TaskTypeEnum.Monitoring,
                                          (int)TaskStatusEnum.NotCompleted,
                                          null,
                                          true,
                                          string.Empty,
                                          null,
                                          DateTime.Now,
                                          null,
                                          0,
                                          string.Empty,
                                          string.Empty,
                                          string.Empty,
                                          string.Empty,
                                          false, 
                                          decimal.Zero, false,
                                          decimal.Zero, false, 
                                          false,
                                          DateTime.Now, null, null, null, 0, null, false, 0, null);
                                    monitoringTask.MonitoringDetail = new MonitoringDetail();
                                }

                                monitoringTask.CreateDate = CompletionTime;

                                monitoring = new TaskProjectWrapperComplete(monitoringTask, false, false);
                                monitoring.IsNewlyAdded = true;
                            }

                            result.Add(monitoring);
                            result.Add(taskInner); //Add deflood task
                        }
                    }
                }
            }

            return result;
        }

        #endregion

        #region UndoCompleteVisit

        public static void UndoCompleteVisit(Visit visit, Work work, bool isForRecomplete)
        {
            Work workToBeUpdated = Work.FindByPrimaryKey(work.ID);
            workToBeUpdated.ClosedDollarAmount -= visit.ClosedDollarAmount;
            work.ClosedDollarAmount = workToBeUpdated.ClosedDollarAmount;
            Work.Update(workToBeUpdated);

            //Revert visit status
            try
            {
                WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitArrived);
                visit.VisitStatus = VisitStatusEnum.Arrived;
            }
            catch (DataNotFoundException)
            {
                try
                {
                    WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitDispatched);
                    visit.VisitStatus = VisitStatusEnum.AssignedForExecution;
                }
                catch (DataNotFoundException)
                {
                    visit.VisitStatus = VisitStatusEnum.Assigned;
                }                
            }
            visit.ClosedDollarAmount = decimal.Zero;
            Visit.Update(visit);            

            //Undo equipment
            WorkTransaction equipmentWorkTransaction
                = WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitEquipmentTransfer);            
            EquipmentTransaction equipmentTransaction
                = EquipmentTransaction.FindByWorkTransaction(equipmentWorkTransaction);
            EquipmentTransaction.DeleteTransactional(equipmentTransaction);
            WorkTransaction.Delete(equipmentWorkTransaction);

            //Find complete work transaction
            WorkTransaction completeTransaction
                = WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitCompleted);

            try
            {
                WorkTransactionQbInvoice qbInvoiceWorkTransaction = WorkTransactionQbInvoice.FindByWorkTransactionId(completeTransaction.ID, null);
                QbInvoice invoice = QbInvoice.FindByPrimaryKey(qbInvoiceWorkTransaction.QbInvoiceId, null);

                if (qbInvoiceWorkTransaction.IsCreated)
                {
                    
                    if (!invoice.IsPending && !isForRecomplete)
                        throw new InvalidOperationException("Cannot undo finalized invoice");

                    if (invoice.IsPending)
                    {
                        QbInvoiceLine.ClearTaskIdInvoiceId(invoice.ID, null);
                        invoice.IsVoid = true;
                        QbInvoice.Update(invoice);
                        QbSyncRequest.Insert(new QbSyncRequest(-1, DateTime.Now, (int) QbSyncActionEnum.InvoiceVoid,
                                                               null, invoice.ID));

                        QbCustomer qbProject = QbCustomer.FindByPrimaryKey(invoice.QbCustomerId);
                        List<QbInvoice> invoices = QbInvoice.Find(qbProject, null);

                        if (invoices.FindAll(delegate(QbInvoice inv) { return inv.IsVoid == false; }).Count == 0)
                        {
                            qbProject.IsActive = false;
                            QbCustomer.Update(qbProject, null);
                        }

                        WorkTransactionQbInvoice.Delete(qbInvoiceWorkTransaction);
                    }
                }
            }
            catch (DataNotFoundException ex)
            { }

            
            //Delete payments
            List<ProjectPayment> projectPayments = ProjectPayment.FindBy(completeTransaction);
            foreach (ProjectPayment payment in projectPayments)
            {
                Project paidProject = Project.FindByPrimaryKey(payment.ProjectId);
                paidProject.PaidAmount -= payment.Amount;
                ProjectPayment.Delete(payment);
            }
            WorkTransactionPayment.DeleteBy(completeTransaction );

            //Delete created Tasks
            if (!isForRecomplete)
            {
                Dictionary<int, Visit> visitsForPossibleDeletion = new Dictionary<int, Visit>();

                //Deleting tasks created on this visit for service and tasks generated for next visit
                //Tasks generated for next visit also located in WorkTransactionTask as created
                List<Task> insertedTasks = Task.FindBy(
                    completeTransaction, false, true);
                foreach (Task insertedTask in insertedTasks)
                {
                    WorkTransactionTask workTransactionTask = new WorkTransactionTask(
                        completeTransaction.ID, insertedTask.ID);
                    WorkTransactionTask.Delete(workTransactionTask);

                    try
                    {
                        Visit nextVisit = Visit.FindNextVisit(insertedTask, visit);
                        if (!visitsForPossibleDeletion.ContainsKey(nextVisit.ID))
                            visitsForPossibleDeletion.Add(nextVisit.ID, nextVisit);
                    }
                    catch (DataNotFoundException){}
                }
                Task.DeleteDeep(insertedTasks);                

                //Delete transferred tasks
                List<Task> transferredTasks = Task.FindTransferredTasksOnCompletion(visit);
                foreach (Task task in transferredTasks)
                {
                    Visit nextVisit = Visit.FindNextVisit(task, visit);
                    if (!visitsForPossibleDeletion.ContainsKey(nextVisit.ID))
                        visitsForPossibleDeletion.Add(nextVisit.ID, nextVisit);

                    VisitTask visitTaskToDelete = new VisitTask(
                        nextVisit.ID, task.ID);
                    VisitTask.Delete(visitTaskToDelete);                    
                }

                //Deleting produced visits if empty
                foreach (Visit visitForPossibleDeletion in visitsForPossibleDeletion.Values)
                {
                    if (Task.FindByVisit(visitForPossibleDeletion).Count == 0)
                        Visit.Delete(visitForPossibleDeletion);
                }

                //Delete inserted projects if empty
                List<Project> insertedProjects = Project.FindBy(
                    completeTransaction, false, true);
                foreach (Project insertedProject in insertedProjects)
                {
                    WorkTransactionProject workTransactionProject = new WorkTransactionProject(
                        completeTransaction.ID, insertedProject.ID);
                    WorkTransactionProject.Delete(workTransactionProject);

                    if (Task.FindByProject(insertedProject).Count == 0)
                    {
                        BackgroundJobPending.DeleteByProjectId(insertedProject.ID, null);
                        Project.Delete(insertedProject);
                    }
                }
            }            

            //Restore modified tasks and delete dump
            List<Task> modifiedTasks = Task.FindBy(
                completeTransaction, true, false);
            foreach (Task task in modifiedTasks)
            {
                Task dumpedTask = Task.FindDumpedTask(task, completeTransaction.ID);
                Task.Restore(dumpedTask);
                WorkTransactionTask workTransactionTask = new WorkTransactionTask(
                    completeTransaction.ID, task.ID);
                WorkTransactionTask.Delete(workTransactionTask);
                Task.DeleteDeep(dumpedTask);
            }

            //Restore projects and delete dump
            List<Project> modifiedProjects = Project.FindBy(
                completeTransaction, true, false);
            foreach (Project project in modifiedProjects)
            {
                Project dumpedProject = Project.FindDumpedProject(project, completeTransaction.ID);
                Project.Restore(dumpedProject);
                WorkTransactionProject workTransactionProject = new WorkTransactionProject(
                    completeTransaction.ID, project.ID);
                WorkTransactionProject.Delete(workTransactionProject);
                Project.Delete(dumpedProject);
            }

            if (!isForRecomplete)
                WorkTransaction.Delete(completeTransaction);

            WorkDetail detail = WorkDetail.FindByWorkAndVisit(work, visit, null);
            detail.TimeComplete = null;
            if (detail.TimeArrive.HasValue && detail.TimeArrive > detail.TimeEndAssigned)
                detail.TimeEnd = detail.TimeArrive.Value;
            else
                detail.TimeEnd = detail.TimeEndAssigned.Value;

            if (visit.VisitStatus == VisitStatusEnum.Assigned)
            {
                detail.TimeBegin = detail.TimeBeginAssigned.Value;
                detail.TimeEnd = detail.TimeEndAssigned.Value;
                detail.TimeBeginAssigned = null;
                detail.TimeEndAssigned = null;
            }
            else if (visit.VisitStatus == VisitStatusEnum.Arrived)
            {
                DateTime startTimeOnArrive = detail.TimeDispatch.HasValue
                    ? detail.TimeDispatch.Value : detail.TimeBeginAssigned.Value;

                detail.TimeEnd = startTimeOnArrive.Add(
                    detail.TimeEndAssigned.Value - detail.TimeBeginAssigned.Value);

                if (detail.TimeEnd < detail.TimeArrive.Value)
                    detail.TimeEnd = detail.TimeArrive.Value;
            }
            else if (visit.VisitStatus == VisitStatusEnum.AssignedForExecution)
            {
                detail.TimeEnd = detail.TimeDispatch.Value.Add(
                    detail.TimeEndAssigned.Value - detail.TimeBeginAssigned.Value);                
            }
            WorkDetail.UpdateAndLog(detail);
        }

        #endregion

        #region SetTaskAction

        private static void SetTaskAction(WorkTransactionTask workTransactionTask, TaskProjectWrapperComplete task)
        {
            if (task.TaskAction == TaskActionEnum.Complete)
                workTransactionTask.WorkTransactionTaskAction = WorkTransactionTaskActionEnum.Complete;
            else if (task.TaskAction == TaskActionEnum.InProcess)
                workTransactionTask.WorkTransactionTaskAction = WorkTransactionTaskActionEnum.InProcess;
            else if (task.TaskAction == TaskActionEnum.Book)
                workTransactionTask.WorkTransactionTaskAction = WorkTransactionTaskActionEnum.Booked;
            else if (task.Task.TaskFailType == TaskFailTypeEnum.MustReturn)
                workTransactionTask.WorkTransactionTaskAction = WorkTransactionTaskActionEnum.FailMustReturn;
            else if (task.Task.TaskFailType == TaskFailTypeEnum.Cancel)
                workTransactionTask.WorkTransactionTaskAction = WorkTransactionTaskActionEnum.Cancel;
            else
                throw new DalworthException("Unable to set corresponding task action");
        }

        #endregion

        #region InsertUpdateTaskWithChilds

        private void InsertUpdateTaskWithChilds(TaskProjectWrapperComplete initialTask, Visit visit, 
            bool linkTaskToVisit, IDbConnection connection)
        {
            List<TaskProjectWrapperComplete> childTasks = new List<TaskProjectWrapperComplete>();

            foreach (TaskProjectWrapperComplete task in m_tasks)
            {
                if (task.Task.ParentTaskId == initialTask.Task.ID)
                    childTasks.Add(task);
            }

            if (initialTask.IsNewlyAdded)
            {
                if (initialTask.Task.ID <= 0)
                {
                    initialTask.Task.CreateDate = DateTime.Now;
                    initialTask.Task.ServiceDate = visit.ServiceDate;
                    Task.InsertWithDetails(initialTask.Task, connection);
                    initialTask.Task.Number = initialTask.Task.ID.ToString();
                    Task.UpdateWithDetails(initialTask.Task, connection);

                } else
                {
                    Task.UpdateWithDetails(initialTask.Task, connection);
                }                    
            }

            if (linkTaskToVisit)
            {
                try
                {
                    VisitTask.FindByPrimaryKey(visit.ID, initialTask.Task.ID, connection);
                }
                catch (DataNotFoundException)
                {
                    VisitTask visitTask = new VisitTask(visit.ID, initialTask.Task.ID);
                    VisitTask.Insert(visitTask, connection);
                }
            } else
            {
                VisitTask.Delete(new VisitTask(visit.ID, initialTask.Task.ID), connection);
            }


            if (initialTask.IsIncludedInVisit)
            {
                if (initialTask.TaskActionId == (int)TaskActionEnum.Complete)
                    initialTask.Task.TaskStatus = TaskStatusEnum.Completed;
                else if (initialTask.TaskActionId == (int)TaskActionEnum.InProcess)
                    initialTask.Task.TaskStatus = TaskStatusEnum.InProcess;
                else if (initialTask.TaskActionId == (int)TaskActionEnum.Fail)
                    initialTask.Task.TaskStatus = TaskStatusEnum.NotCompleted;
                else if (initialTask.TaskActionId == (int)TaskActionEnum.Book)
                    initialTask.Task.TaskStatus = TaskStatusEnum.NotCompleted;


                if (initialTask.TaskActionId == (int)TaskActionEnum.Complete
                    || initialTask.TaskActionId == (int)TaskActionEnum.InProcess)
                {
                    initialTask.Task.IsReady = true;
                    initialTask.Task.TaskFailType = null;
                    initialTask.Task.FailReason = string.Empty;                        
                }

                initialTask.Task.IsReincluded = false;                
                Task.UpdateWithDetails(initialTask.Task, connection);
            } 
                    

            foreach (TaskProjectWrapperComplete childTask in childTasks)
            {
                childTask.Task.ParentTaskId = initialTask.Task.ID;
                childTask.Task.ProjectId = initialTask.Task.ProjectId;
                InsertUpdateTaskWithChilds(childTask, visit, linkTaskToVisit, connection);
            }

        }

        #endregion
    }
}
