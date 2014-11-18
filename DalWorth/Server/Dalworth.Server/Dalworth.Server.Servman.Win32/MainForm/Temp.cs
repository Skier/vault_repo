using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Servman.Domain;

namespace Dalworth.Server.Servman.Win32.MainForm
{
    class Temp
    {
//        #region GetNewOrders
//
//        public void GetNewOrders()
//        {
//            string latestImportedOrderNumber = string.Empty;
//
//            try
//            {
//                Task task = Task.GetLatestImportedTask();
//                latestImportedOrderNumber = task.ServmanOrderNum;
//            }
//            catch (DataNotFoundException) { }
//
//            List<h_order> orders = h_order.GetNewOrders(latestImportedOrderNumber);
//            Host.Trace("Order Listener", "Get new orders, count = " + orders.Count);
//
//            foreach (h_order order in orders)
//            {
//                Host.Trace("Order Listener", "Importing order #" + order.ticket_num);
//
//                custmast custmast = Domain.custmast.FindByPrimaryKey(order.cust_id);
//                hdeflood hdeflood = null;
//
//                try
//                {
//                    hdeflood = hdeflood.FindByPrimaryKey(order.ticket_num);
//                }
//                catch (DataNotFoundException) { }
//
//                List<ddeflood> ddefloods = ddeflood.FindByTicket(order.ticket_num);
//
//                //Insert new customer
//                Customer customer;
//                try
//                {
//                    customer = Customer.FindBy(custmast.cust_id);
//                }
//                catch (DataNotFoundException)
//                {
//                    customer = Customer.InsertCustomer(custmast);
//                }
//
//                //Insert project and task
//                try
//                {
//                    Task.FindByServmanOrderNum(order.ticket_num);
//                    throw new Exception("This task already exist in our DB");
//                }
//                catch (DataNotFoundException)
//                {
//                    Project.ImportProject(customer, order, hdeflood, ddefloods);
//                }
//
//                PendingTaskGridState.MakePendingTaskGridDirty();
//
//                Host.Trace("Order Listener", "Order #" + order.ticket_num + " has been imported");
//            }
//
//            Host.Trace("Order Listener", "Importing orders completed successfully, count = " + orders.Count);
//        }
//
//        #endregion
//
//        #region SendWorks
//
//        private void SendWorks()
//        {
//            List<Work> works = Work.FindNotSentWorks();
//            Host.Trace("Servman Export", "Exporting Works, Count = " + works.Count);
//            foreach (Work work in works)
//            {
//                Host.Trace("Servman Export", "Exporting Work, ID = " + work.ID);
//
//                Employee technician = Employee.FindByPrimaryKey(work.TechnicianEmployeeId);
//                Employee dispatch = Employee.FindByPrimaryKey(work.DispatchEmployeeId);
//                Van van = Van.FindByPrimaryKey(work.VanId.Value);
//
//                Debug.Assert(technician.ServmanTechId != null && technician.ServmanTechId != string.Empty,
//                    "Cannot export work ID=" + work.ID + ". Technician doesn't have ServmanTechId assigned");
//                Debug.Assert(dispatch.ServmanUserId != null && dispatch.ServmanUserId != string.Empty,
//                    "Cannot export work ID=" + work.ID + ". Dispatch doesn't have ServmanUserId assigned");
//                Debug.Assert(van.ServmanTruckId != null && van.ServmanTruckId != string.Empty,
//                    "Cannot export work ID=" + work.ID + ". Van doesn't have ServmanTruckId assigned");
//                Debug.Assert(van.ServmanTruckNum != null && van.ServmanTruckNum != string.Empty,
//                    "Cannot export work ID=" + work.ID + ". Van doesn't have ServmanTruckNum assigned");
//
//                techschd techschd = new techschd();
//                techschd.tech_id = technician.ServmanTechId;
//                techschd.date = work.StartDate ?? new DateTime();
//                techschd.truck_num = van.ServmanTruckNum;
//                techschd.disp_id = dispatch.ServmanUserId;
//                techschd.truck_id = van.ServmanTruckId;
//                techschd.note = string.Empty;
//                Domain.techschd.Insert(techschd);
//
//                work.IsSentToServman = true;
//                Work.Update(work);
//
//                Host.Trace("Servman Export", "Work has been exported successfully, ID = " + work.ID);
//            }
//        }
//
//        #endregion
//
//        #region SendTicketDispatchTransactions
//
//        private void SendTicketDispatchTransactions()
//        {
//            //            foreach (TicketTransaction transaction in transactions)
//            //            {
//            //                Visit visit = Visit.FindByPrimaryKey(transaction.TicketId);
//            //
//            //                disp_que disp_que = Domain.disp_que.FindByPrimaryKey(visit.ServmanTicketNum);
//            //                h_order h_order = Domain.h_order.FindByPrimaryKey(visit.ServmanTicketNum);
//            //
//            //                DateTime dispatchDate = transaction.TransactionDate ?? DateTime.Now;
//            //
//            //                disp_que.d_dispatch = dispatchDate.Date;
//            //                disp_que.t_dispatch = dispatchDate.Hour.ToString("00") + dispatchDate.Minute.ToString("00");
//            //                h_order.tran_stat = 3;
//            //
//            //                try
//            //                {
//            //                    Database.Begin(ConnectionKeyEnum.Servman);
//            //                    Domain.disp_que.Update(disp_que);
//            //                    Domain.h_order.Update(h_order);
//            //                    Database.Commit();
//            //                }
//            //                catch (Exception)
//            //                {
//            //                    Database.Rollback();
//            //                    throw;
//            //                }
//            //
//            //                transaction.IsSentToServman = true;
//            //                TicketTransaction.Update(transaction);
//            //
//            //            }
//        }
//
//        #endregion
//
//        #region SendWorkTransactions
//
//        private void SendWorkTransactions()
//        {
//            List<WorkTransaction> transactions = WorkTransaction.FindNotSentTransactions();
//            Host.Trace("Servman Export", "Exporting Work Transactions, Count = " + transactions.Count);
//            foreach (WorkTransaction transaction in transactions)
//            {
//                Visit visit = Visit.FindByPrimaryKey(transaction.VisitId.Value);
//                List<Task> visitTasks = Task.FindByVisit(visit);
//                Work work = Work.FindByPrimaryKey(transaction.WorkId);
//                Employee technician = Employee.FindByPrimaryKey(work.TechnicianEmployeeId);
//
//                foreach (Task task in visitTasks)
//                {
//                    if (!task.IsSentToServman)
//                        SendTaskFirstTime(task, visit, work, technician);
//                    task.IsSentToServman = true;
//                    Task.Update(task);
//                }
//
//
//                Task mainTask = Task.FindByVisit(visit)[0];
//                h_order h_order = null;
//                disp_que disp_que = null;
//                hdeflood hdeflood = null;
//                List<ddeflood> ddefloods = null;
//
//                if (mainTask.ServmanOrderNum == null || mainTask.ServmanOrderNum == string.Empty)
//                    continue;
//
//                if (transaction.WorkTransactionType == WorkTransactionTypeEnum.VisitDeclined)
//                {
//                    Host.Trace("Servman Export", "Exporting Visit Declined transaction, ID = " + transaction.ID);
//                    h_order = Domain.h_order.FindByPrimaryKey(mainTask.ServmanOrderNum);
//                    h_order.comp_type = 3;
//                }
//                else if (transaction.WorkTransactionType == WorkTransactionTypeEnum.VisitArrived)
//                {
//                    Host.Trace("Servman Export", "Exporting Visit Arrived transaction, ID = " + transaction.ID);
//                    disp_que = Domain.disp_que.FindByPrimaryKey(mainTask.ServmanOrderNum);
//                    WorkDetail detail = WorkDetail.FindByWorkAndVisit(work, visit, null);
//                    DateTime arriveDate = detail.TimeBegin;
//                    disp_que.arival = arriveDate.Hour.ToString("00") + arriveDate.Minute.ToString("00");
//                }
//                else if (transaction.WorkTransactionType == WorkTransactionTypeEnum.NoGo)
//                {
//                    Host.Trace("Servman Export", "Exporting No Go transaction, ID = " + transaction.ID);
//                    h_order = Domain.h_order.FindByPrimaryKey(mainTask.ServmanOrderNum);
//                    h_order.comp_type = 3;
//                }
//                else if (transaction.WorkTransactionType == WorkTransactionTypeEnum.SubmitETC)
//                {
//                    Host.Trace("Servman Export", "Exporting Submit ETC transaction, ID = " + transaction.ID);
//                    WorkTransactionEtc etc = WorkTransactionEtc.FindByWorkTransaction(transaction);
//                    disp_que = Domain.disp_que.FindByPrimaryKey(mainTask.ServmanOrderNum);
//                    disp_que.t_estcomp = (etc.Hours.HasValue ? etc.Hours.Value.ToString("00") : "00")
//                                         + (etc.Minutes.HasValue ? etc.Minutes.Value.ToString("00") : "00");
//                }
//                else if (transaction.WorkTransactionType == WorkTransactionTypeEnum.VisitCompleted)
//                {
//                    if (mainTask.TaskType == TaskTypeEnum.RugPickup)
//                    {
//                        Host.Trace("Servman Export",
//                                   "Exporting Rug Pickup complete transaction, ID = " + transaction.ID);
//                        h_order = Domain.h_order.FindByPrimaryKey(mainTask.ServmanOrderNum);
//                        h_order.date = h_order.date.AddYears(1);
//
//                        List<Item> items = Item.FindByWorkTransaction(transaction);
//                        ddefloods = new List<ddeflood>();
//
//                        for (int i = 0; i < items.Count; i++)
//                        {
//                            string note = string.Empty;
//                            if (items[i].ItemShape.HasValue && items[i].ItemShape.Value == ItemShapeEnum.Rectangle)
//                                note = "Rectangle rug " + items[i].Height + "x" + items[i].Width;
//
//                            if (items[i].ItemShape.HasValue && items[i].ItemShape.Value == ItemShapeEnum.Round)
//                                note = "Round rug D=" + items[i].Diameter;
//
//                            ddeflood ddeflood = new ddeflood();
//                            ddeflood.ticket_num = mainTask.ServmanOrderNum;
//                            ddeflood.serv_type = "07";
//                            ddeflood.item_num = i + 1;
//                            ddeflood.note = note;
//                            ddeflood.amount = decimal.ToSingle(items[i].TotalCost);
//
//                            ddeflood.enter_by = string.Empty;
//                            ddeflood.prc_type = string.Empty;
//                            ddeflood.user_adj = string.Empty;
//
//                            ddefloods.Add(ddeflood);
//                        }
//                    }
//                    else if (mainTask.TaskType == TaskTypeEnum.RugDelivery)
//                    {
//                        Host.Trace("Servman Export",
//                                   "Exporting Rug Delivery complete transaction, ID = " + transaction.ID);
//                        h_order = Domain.h_order.FindByPrimaryKey(mainTask.ServmanOrderNum);
//                        h_order.date = transaction.TransactionDate.Value;
//                        h_order.tran_stat = 5;
//                        h_order.comp_type = 2;
//                        h_order.tech_id = "519"; //Shane Hobbs
//
//                        WorkDetail detail = WorkDetail.FindByWorkAndVisit(work, visit, null);
//                        DateTime completeDate = detail.TimeEnd;
//
//
//                        disp_que = Domain.disp_que.FindByPrimaryKey(mainTask.ServmanOrderNum);
//                        disp_que.t_complete = completeDate.Hour.ToString("00") + completeDate.Minute.ToString("00");
//
//
//                        try
//                        {
//                            hdeflood = hdeflood.FindByPrimaryKey(mainTask.ServmanOrderNum);
//                            hdeflood.d_complete = completeDate.Date;
//                            hdeflood.t_complete = completeDate.Hour.ToString("00") +
//                                                  completeDate.Minute.ToString("00");
//                            hdeflood.tech_id = "519"; //Shane Hobbs
//                        }
//                        catch (DataNotFoundException) { }
//                    }
//                    else if (mainTask.TaskType == TaskTypeEnum.Unknown)
//                    {
//                        Host.Trace("Servman Export",
//                                   "Exporting Unknown complete transaction, ID = " + transaction.ID);
//                        WorkDetail detail = WorkDetail.FindByWorkAndVisit(work, visit, null);
//                        DateTime completeDate = detail.TimeEnd;
//
//                        disp_que = Domain.disp_que.FindByPrimaryKey(mainTask.ServmanOrderNum);
//                        disp_que.t_complete = completeDate.Hour.ToString("00") + completeDate.Minute.ToString("00");
//
//                        h_order = Domain.h_order.FindByPrimaryKey(mainTask.ServmanOrderNum);
//                        h_order.tran_stat = 5;
//                        h_order.comp_type = 2;
//
//                        try
//                        {
//                            hdeflood = hdeflood.FindByPrimaryKey(mainTask.ServmanOrderNum);
//                            hdeflood.d_complete = completeDate.Date;
//                            hdeflood.t_complete = completeDate.Hour.ToString("00") +
//                                                  completeDate.Minute.ToString("00");
//                        }
//                        catch (DataNotFoundException) { }
//                    }
//                }
//
//                try
//                {
//                    Database.Begin(ConnectionKeyEnum.Servman);
//                    if (h_order != null)
//                        Domain.h_order.Update(h_order);
//
//                    if (disp_que != null)
//                        Domain.disp_que.Update(disp_que);
//
//                    if (hdeflood != null)
//                        Domain.hdeflood.Update(hdeflood);
//
//                    if (ddefloods != null)
//                    {
//                        foreach (ddeflood ddeflood in ddefloods)
//                        {
//                            ddeflood.Insert(ddeflood);
//                        }
//                    }
//
//
//                    Database.Commit();
//                    Host.Trace("Servman Export", "Transaction exported successfully, ID = " + transaction.ID);
//                }
//                catch (Exception)
//                {
//                    Database.Rollback();
//                    throw;
//                }
//
//                transaction.IsSentToServman = true;
//                WorkTransaction.Update(transaction);
//            }
//        }
//
//        #endregion
//
//        #region CopyOrder
//
//        public string CopyOrder(string orderNumber)
//        {
//            h_order order = h_order.FindByPrimaryKey(orderNumber);
//
//            order.ticket_num = h_order.GetNextOrderNumber();
//            order.serv_type = 4;
//            order.tran_stat = 1;
//            order.tran_type = 1;
//            order.comp_type = 1;
//            order.amount = 0;
//            order.recve_amt = 0;
//            order.tech_id = string.Empty;
//
//            hdeflood deflood = null;
//
//            try
//            {
//                deflood = hdeflood.FindByPrimaryKey(orderNumber);
//                deflood.ticket_num = order.ticket_num;
//                deflood.trans_num = string.Empty;
//                deflood.d_dispatch = new DateTime(1899, 12, 30);
//                deflood.t_dispatch = string.Empty;
//                deflood.d_complete = new DateTime(1899, 12, 30);
//                deflood.t_complete = string.Empty;
//                deflood.tran_type = 1;
//                deflood.tran_stat = 1;
//                deflood.comp_type = 1;
//                deflood.canc_type = 0;
//                deflood.amount = 0;
//                deflood.tech_id = string.Empty;
//                deflood.t_arrival = string.Empty;
//            }
//            catch (DataNotFoundException) { }
//
//            List<ddeflood> ddefloods = ddeflood.FindByTicket(orderNumber);
//            foreach (ddeflood localDeflood in ddefloods)
//            {
//                localDeflood.ticket_num = order.ticket_num;
//            }
//
//            try
//            {
//                Database.Begin(ConnectionKeyEnum.Servman);
//
//                h_order.Insert(order);
//                if (deflood != null)
//                    hdeflood.Insert(deflood);
//                ddeflood.Insert(ddefloods);
//                Database.Commit();
//                return order.ticket_num;
//            }
//            catch (Exception)
//            {
//                Database.Rollback();
//                throw;
//            }
//        }
//
//        #endregion
//
//        #region SendTaskFirstTime
//
//        private void SendTaskFirstTime(Task task, Visit visit, Work work, Employee technician)
//        {
//            if (task.ServmanOrderNum != null && task.ServmanOrderNum != string.Empty)
//            {
//                Customer customer = Customer.FindByPrimaryKey(visit.CustomerId.Value);
//
//                disp_que disp_que = new disp_que();
//                disp_que.ticket_num = task.ServmanOrderNum;
//                disp_que.customer = customer.DisplayName;
//                disp_que.d_dispatch = work.CreateDate;
//                disp_que.tech_id = technician.ServmanTechId;
//                disp_que.serv_type = 4;
//                disp_que.phone = customer.Phone1;
//                disp_que.t_dispatch = string.Empty;
//                disp_que.t_estcomp = string.Empty;
//                disp_que.t_complete = string.Empty;
//                disp_que.arival = string.Empty;
//                disp_que.span = string.Empty;
//                disp_que.time_stat = string.Empty;
//                disp_que.note = string.Empty;
//                disp_que.order = string.Empty;
//                disp_que.grid = string.Empty;
//                disp_que.auto_time = string.Empty;
//
//                h_order h_order = h_order.FindByPrimaryKey(task.ServmanOrderNum);
//                h_order.tech_id = technician.ServmanTechId;
//                h_order.tran_stat = 2;
//
//                hdeflood hdeflood = null;
//                try
//                {
//                    hdeflood = hdeflood.FindByPrimaryKey(task.ServmanOrderNum);
//                    hdeflood.tech_id = technician.ServmanTechId;
//                }
//                catch (DataNotFoundException) { }
//
//                try
//                {
//                    Database.Begin(ConnectionKeyEnum.Servman);
//
//                    disp_que.Insert(disp_que);
//                    h_order.Update(h_order);
//                    if (hdeflood != null)
//                        hdeflood.Update(hdeflood);
//
//                    Database.Commit();
//                }
//                catch (Exception)
//                {
//                    Database.Rollback();
//                    throw;
//                }
//            }
//            else // If there is no servman number
//            {
//                if (task.TaskType == TaskTypeEnum.RugPickup) //Create new orders in servman only for rug pickup tickets
//                {
//                    Host.Trace("Servman Export", "Creating Rug Pickup Task");
//
//                    Customer customer = Customer.FindByPrimaryKey(visit.CustomerId.Value);
//
//                    Project project = Project.FindByPrimaryKey(task.ProjectId);
//                    Address address;
//                    if (project.ServiceAddressId == null)
//                    {
//                        address = Address.FindByPrimaryKey(customer.AddressId.Value);
//                    }
//                    else
//                    {
//                        address = Address.FindByPrimaryKey(project.ServiceAddressId.Value);
//                    }
//
//                    disp_que disp_que = new disp_que();
//                    disp_que.ticket_num = h_order.GetNextOrderNumber();
//                    disp_que.customer = customer.DisplayName;
//                    disp_que.d_dispatch = work.CreateDate;
//                    disp_que.tech_id = technician.ServmanTechId;
//                    disp_que.serv_type = 4;
//                    disp_que.phone = customer.Phone1;
//                    disp_que.t_dispatch = string.Empty;
//                    disp_que.t_estcomp = string.Empty;
//                    disp_que.t_complete = string.Empty;
//                    disp_que.arival = string.Empty;
//                    disp_que.span = string.Empty;
//                    disp_que.time_stat = string.Empty;
//                    disp_que.note = string.Empty;
//                    disp_que.order = string.Empty;
//                    disp_que.grid = string.Empty;
//                    disp_que.auto_time = string.Empty;
//
//                    h_order h_order2 = new h_order();
//                    h_order2.ticket_num = disp_que.ticket_num;
//                    h_order2.cust_id = customer.ServmanCustId;
//                    h_order2.alt_addr = false;
//                    h_order2.contact = string.Empty;
//                    h_order2.date = task.ServiceDate.Value.Date;
//                    h_order2.time = task.ServiceDate.Value.TimeOfDay.Hours.ToString("00")
//                                    + task.ServiceDate.Value.TimeOfDay.Minutes.ToString("00");
//                    h_order2.page = address.Map;
//                    h_order2.grid = "A";
//                    h_order2.area_id = "D/FW";
//                    h_order2.serv_type = 1;
//                    h_order2.tran_type = 1;
//                    h_order2.comp_type = 1;
//                    h_order2.company = "273";
//                    h_order2.tech_id = technician.ServmanTechId;
//                    h_order2.amount = 0;
//                    h_order2.tran_stat = 2;
//                    h_order2.closer_id = "148";
//                    h_order2.recve_amt = 0;
//                    h_order2.pr_date = DateTime.Now;
//                    h_order2.zip = address.Zip;
//                    h_order2.bookby = string.Empty;
//                    h_order2.mapbook = "75";
//                    h_order2.companyid = 1;
//                    h_order2.order_conf = true;
//                    h_order2.cleancnum = string.Empty;
//
//                    try
//                    {
//                        Database.Begin(ConnectionKeyEnum.Servman);
//                        disp_que.Insert(disp_que);
//                        h_order.Insert(h_order2);
//                        Database.Commit();
//                    }
//                    catch (Exception)
//                    {
//                        Database.Rollback();
//                        throw;
//                    }
//
//                    //updating our db
//                    try
//                    {
//                        Database.Begin();
//
//                        if (task.TaskStatus == TaskStatusEnum.RugDeliveryCreated) // Find Rug Delivery and update numbers
//                        {
//                            Task rugDelivery = Task.FindByNumber(task.Number + "D");
//                            rugDelivery.ServmanOrderNum = h_order2.ticket_num;
//                            rugDelivery.Number = h_order2.ticket_num + "D";
//                            Task.Update(rugDelivery);
//                        }
//
//                        task.ServmanOrderNum = h_order2.ticket_num;
//                        task.Number = h_order2.ticket_num;
//                        Task.Update(task);
//
//
//                        Database.Commit();
//                    }
//                    catch (Exception)
//                    {
//                        Database.Rollback();
//                        throw;
//                    }
//
//
//                    Host.Trace("Servman Export", "Rug Pickup Task has been successfully created, ticket_num = " + h_order2.ticket_num);
//                }
//            }
//        }
//
//        #endregion
    }
}
