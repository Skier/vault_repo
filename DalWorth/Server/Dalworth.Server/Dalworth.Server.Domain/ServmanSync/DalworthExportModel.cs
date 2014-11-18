using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.Servman.Domain;
using Dalworth.Server.Servman.Domain.intermediate;
using Address=Dalworth.Server.Servman.Domain.intermediate.Address;
using Customer=Dalworth.Server.Domain.Customer;
using CustomerTypeEnum=Dalworth.Server.Domain.CustomerTypeEnum;

namespace Dalworth.Server.Domain.ServmanSync
{
    public class DalworthExportModel
    {
        #region ExportCustomers

        public static void ExportCustomers()
        {
            List<CustomerAndAddress> newCustomers = Customer.FindToBeSynchronized();
            foreach (CustomerAndAddress customer in newCustomers)
            {
                Host.Trace("Customer Export", 
                    string.Format("Exporting Customer ID = {0}, ServmanId = {1}",
                    customer.Customer.ID, customer.Customer.ServmanCustId));

                ExportCustomer(customer);

                Host.Trace("Customer Export",
                    string.Format("Customer export done. Customer ID = {0}, ServmanId = {1}",
                    customer.Customer.ID, customer.Customer.ServmanCustId));
            }
        }

        #endregion

        #region Convert Customer and Address

        private static Servman.Domain.intermediate.Address Convert(Server.Domain.Address address)
        {
            Servman.Domain.intermediate.Address result = new Servman.Domain.intermediate.Address();

            if (address.AreaId != null)
                result.AreaId = Area.FindByPrimaryKey(address.AreaId.Value).ServmanId;
            result.Block = address.Block;
            result.Prefix = address.Prefix;
            result.Street = address.Street;
            result.Suffux = address.Suffix;
            result.Unit = address.Unit;
            result.Address2 = address.Address2;
            result.City = address.City;
            result.State = address.State;
            result.Zip = address.Zip.ToString();
            result.Zip4 = string.Empty;
            result.Grid = address.MapLetter;
            result.Page = address.MapPage;

            return result;
        }

        private static Servman.Domain.intermediate.Customer Convert(CustomerAndAddress customerAndAddress)
        {
            Servman.Domain.intermediate.Customer intermediateCustomer = new Servman.Domain.intermediate.Customer();

            intermediateCustomer.Address = Convert(customerAndAddress.Address);

            if (customerAndAddress.Customer.CustomerType == CustomerTypeEnum.Business)
                intermediateCustomer.CustomerType = Servman.Domain.intermediate.CustomerTypeEnum.Business;
            else if (customerAndAddress.Customer.CustomerType == CustomerTypeEnum.Residential)
                intermediateCustomer.CustomerType = Servman.Domain.intermediate.CustomerTypeEnum.Residential;
            else
                intermediateCustomer.CustomerType = null;
            intermediateCustomer.ID = customerAndAddress.Customer.ServmanCustId;
            intermediateCustomer.Name = customerAndAddress.Customer.LastName 
                + ", " + customerAndAddress.Customer.FirstName;

            if (intermediateCustomer.Name.Length > 2 && intermediateCustomer.Name.EndsWith(", "))
                intermediateCustomer.Name = intermediateCustomer.Name.Substring(0, intermediateCustomer.Name.Length - 2);

            intermediateCustomer.HomePhone = customerAndAddress.Customer.Phone1;
            intermediateCustomer.BusinessPhone = customerAndAddress.Customer.Phone2;
            intermediateCustomer.LastContact = customerAndAddress.Customer.Modified;
            intermediateCustomer.LastService = DateTime.MinValue;
            intermediateCustomer.LastAddressChange = DateTime.MinValue;
            intermediateCustomer.EmailAddress = customerAndAddress.Customer.Email;
            intermediateCustomer.HasVisitedWebsite = false;

            return intermediateCustomer;
        }

        #endregion

        #region ExportCustomer

        private static void ExportCustomer(CustomerAndAddress customer)
        {
            bool isCustomerExported = false;

            if (customer.Customer.ServmanCustId == string.Empty || customer.Customer.LastSyncDate == null)
            {
                Servman.Domain.intermediate.Customer intermediateCustomer = Convert(customer);
                customer.Customer.ServmanCustId = ServmanExportModel.InsertCustomer(intermediateCustomer);
                isCustomerExported = true;

            } else if (customer.Customer.Modified > customer.Customer.LastSyncDate
                || customer.Address.Modified > customer.Customer.LastSyncDate)
            {
                custmast servmanCustomer = custmast.FindByPrimaryKey(customer.Customer.ServmanCustId);
                
                if (servmanCustomer.l_contact > customer.Customer.Modified || servmanCustomer.l_addr_chg > customer.Customer.Modified)
                {
                    //Customer was modified in servman later
                } else
                {
                    Servman.Domain.intermediate.Customer intermediateCustomer = Convert(customer);
                    ServmanExportModel.UpdateCustomer(intermediateCustomer, servmanCustomer);                    
                }                                        

                isCustomerExported = true;
            }

            if (isCustomerExported)
            {
                Customer freshCustomer = Customer.FindByPrimaryKey(customer.Customer.ID);
                freshCustomer.ServmanCustId = customer.Customer.ServmanCustId;
                freshCustomer.LastSyncDate = DateTime.Now;
                Customer.Update(freshCustomer);
                customer.Customer.LastSyncDate = DateTime.Now;                
            }
        }

        #endregion

        #region GetRugsDescription

        private static string GetRugsDescription(List<Item> items)
        {
            List<string> rugNotesList = new List<string>();

            foreach (Item item in items)
            {
                string itemDescription;
                if (item.ItemShape == ItemShapeEnum.Rectangle)
                    itemDescription = Utils.RemoveTrailingZeros(item.Width) + "X"
                                      + Utils.RemoveTrailingZeros(item.Height);
                else
                    itemDescription = "D" + Utils.RemoveTrailingZeros(item.Diameter);

                rugNotesList.Add(itemDescription);
            }

            if (rugNotesList.Count > 0)
                return Utils.JoinStrings(", ", rugNotesList.ToArray());
            return string.Empty;
        }

        #endregion

        #region RemoveDeletedTicketsFromServman

        private static void RemoveDeletedTicketsFromServman()
        {
            List<DeletedTask> deletedTasks = DeletedTask.FindTasksToDeleteInServman();
            foreach (DeletedTask deletedTask in deletedTasks)
            {
                ServmanExportModel.DeleteOrder(deletedTask.ServmanOrderNum);
                deletedTask.LastSyncDate = DateTime.Now;
                DeletedTask.Update(deletedTask);
            }
        }

        #endregion

        #region ExportOrders

        public static void ExportOrders()
        {
            RemoveDeletedTicketsFromServman();

            List<Task> tasksToBeExported = Task.FindTasksToBeExported();
            List<int> taskIdsToIgnore = new List<int>();

            foreach (Task taskToBeExported in tasksToBeExported)
            {
                if (taskIdsToIgnore.Contains(taskToBeExported.ID))
                    continue;

                ServmanExportPackage package 
                    = ServmanExportPackage.FindPackageToBeExported(taskToBeExported);

                ServmanExportPackage rugOnDefloodPackage = null;
                
                if ((package.Task.TaskType == TaskTypeEnum.RugPickup
                    || package.Task.TaskType == TaskTypeEnum.RugDelivery)
                    && package.Project.ProjectType == ProjectTypeEnum.Deflood)
                {
                    rugOnDefloodPackage = package;

                    package = null;

                    List<Task> visitTasks = Task.FindByVisit(rugOnDefloodPackage.Visit);
                    foreach (Task task in visitTasks)
                    {
                        if (task.TaskType == TaskTypeEnum.Monitoring && 
                            task.ProjectId == rugOnDefloodPackage.Project.ID)
                        {
                            ServmanExportPackage monitoringCandidatePackage 
                                = ServmanExportPackage.FindPackageToBeExported(task);

                            if (monitoringCandidatePackage.Visit.ID == rugOnDefloodPackage.Visit.ID)
                            {
                                package = monitoringCandidatePackage;
                                taskIdsToIgnore.Add(monitoringCandidatePackage.Task.ID);
                                break;
                            }                            
                        }
                    }

                    if (package == null)
                    {   
                        Task dummyMonitoringTask = new Task(0, null, string.Empty, 
                            rugOnDefloodPackage.Project.ID,
                            (int)TaskTypeEnum.Monitoring,
                            rugOnDefloodPackage.Task.TaskStatusId,
                            rugOnDefloodPackage.Task.TaskFailTypeId,
                            true, string.Empty, 0, DateTime.Now, 
                            rugOnDefloodPackage.Task.ServiceDate,
                            0, 
                            rugOnDefloodPackage.Task.Description, 
                            rugOnDefloodPackage.Task.Message, 
                            rugOnDefloodPackage.Task.Notes,
                            rugOnDefloodPackage.Task.FailReason,
                            false, decimal.Zero, false, decimal.Zero, false,
                            false, DateTime.Now, null, null, null, 0, null, false, 0, null);                        

                        package = new ServmanExportPackage(rugOnDefloodPackage.Customer,
                            rugOnDefloodPackage.CustomerAddress,
                            rugOnDefloodPackage.AdditionalAddress,
                            dummyMonitoringTask,
                            rugOnDefloodPackage.Project,
                            rugOnDefloodPackage.Visit,
                            rugOnDefloodPackage.Work,
                            rugOnDefloodPackage.WorkDetail,
                            new List<Item>(),
                            null,
                            rugOnDefloodPackage.DefloodDetail,
                            rugOnDefloodPackage.Technician,
                            rugOnDefloodPackage.Van,
                            rugOnDefloodPackage.AdvertisingTechnician,
                            rugOnDefloodPackage.PerformedByUser);                        
                    }
                }

                if (rugOnDefloodPackage != null)
                {
                    if (!string.IsNullOrEmpty(package.Task.ServmanOrderNum))
                        rugOnDefloodPackage.Task.ServmanOrderNum = package.Task.ServmanOrderNum;
                    else if (!string.IsNullOrEmpty(rugOnDefloodPackage.Task.ServmanOrderNum))
                        package.Task.ServmanOrderNum = rugOnDefloodPackage.Task.ServmanOrderNum;
                }

                ServmanExportPackage firstMonitoringPackage = null;
                ServmanExportPackage pendingRugDelivery = null;

                if (package.Task.TaskType == TaskTypeEnum.Monitoring)
                {
                    if (Task.IsFirstMonitoring(package.Task))
                    {
                        firstMonitoringPackage = package;
                        Task defloodTask = Task.FindByPrimaryKey(package.Task.ParentTaskId.Value);
                        package = ServmanExportPackage.FindPackageToBeExported(defloodTask);
                        if (package.Task.TaskStatus == TaskStatusEnum.InProcess)
                            package.Task.TaskStatus = TaskStatusEnum.Completed;
                    }

                } else if (package.Task.TaskType == TaskTypeEnum.RugDelivery 
                    && package.Project.ProjectType == ProjectTypeEnum.RugCleaning)
                {   
                    if (package.Task.ServmanOrderNum == null || package.Task.ServmanOrderNum == string.Empty)
                    {
                        Task rugPickup = Task.FindRugPickup(package.Task);
                        package.Task.ServmanOrderNum = rugPickup.ServmanOrderNum;
                    }
                
                    if (package.Visit.VisitStatus == VisitStatusEnum.Pending)
                    {
                        pendingRugDelivery = package;
                        Task rugPickup = Task.FindRugPickup(package.Task);
                        package = ServmanExportPackage.FindPackageToBeExported(rugPickup);
                    }
                }

                Host.Trace("Order Export",
                    string.Format("Exporting Order ID = {0}, ServmanId = {1}",
                    package.Task.ID, package.Task.ServmanOrderNum));


                package.Customer = Customer.FindByPrimaryKey(package.Customer.ID);
                package.CustomerAddress = Address.FindByPrimaryKey(package.Customer.AddressId.Value);

                ExportCustomer(new CustomerAndAddress(package.Customer, package.CustomerAddress));

                Servman.Domain.intermediate.Order order = new Servman.Domain.intermediate.Order();

                order.Customer = Convert(new CustomerAndAddress(package.Customer, package.CustomerAddress));

                if (package.AdditionalAddress != null)
                    order.AlternativeAddress = Convert(package.AdditionalAddress);

                order.DefloodInfo = new Deflood();
                order.DefloodInfo.SourceOfFlood = string.Empty;
                if (package.DefloodDetail != null && package.DefloodDetail.FloodDate.HasValue)
                    order.DefloodInfo.DateOfFlood = package.DefloodDetail.FloodDate.Value;
                order.DefloodInfo.InsuranceAgency = package.Project.InsuranceAgency;
                order.DefloodInfo.InsuranceAgenyPhone = package.Project.InsuranceAgencyPhone;
                order.DefloodInfo.InsuranceAgent = package.Project.InsuranceAgent;
                order.DefloodInfo.InsuranceCarrier = package.Project.InsuranceCompany;
                order.DefloodInfo.InsuranceAdjustor = package.Project.InsuranceAdjustor;
                order.DefloodInfo.InsuranceAdjustorPhone = package.Project.InsuranceAdjustorPhone;

                if (package.Task.TaskType == TaskTypeEnum.RugPickup)
                    order.OrderType = OrderTypeEnum.RugPickup;
                else if (package.Task.TaskType == TaskTypeEnum.RugDelivery)
                    order.OrderType = OrderTypeEnum.RugDelivery;
                else if (package.Task.TaskType == TaskTypeEnum.Deflood)
                    order.OrderType = OrderTypeEnum.Deflood;
                else if (package.Task.TaskType == TaskTypeEnum.Monitoring)
                    order.OrderType = OrderTypeEnum.Monitoring;
                else if (package.Task.TaskType == TaskTypeEnum.Miscellaneous)
                    order.OrderType = OrderTypeEnum.Deflood;
                else if (package.Task.TaskType == TaskTypeEnum.Help)
                    order.OrderType = OrderTypeEnum.Monitoring;

                if (package.Task.TaskType == TaskTypeEnum.Miscellaneous 
                    && (package.Task.IsRugCleaningDepartment || package.Project.ProjectType == ProjectTypeEnum.RugCleaning))
                {
                    order.OrderType = OrderTypeEnum.RugDelivery;
                }                    

                if (package.Visit.VisitStatus == VisitStatusEnum.Pending)
                    order.OrderStatus = OrderStatusEnum.Pending;
                else if (package.Visit.VisitStatus == VisitStatusEnum.Assigned)
                    order.OrderStatus = OrderStatusEnum.Assigned;
                else if (package.Visit.VisitStatus == VisitStatusEnum.AssignedForExecution)
                    order.OrderStatus = OrderStatusEnum.Dispatched;
                else if (package.Visit.VisitStatus == VisitStatusEnum.Completed)
                    order.OrderStatus = OrderStatusEnum.Completed;

                    
                if (package.Task.TaskFailType == TaskFailTypeEnum.Cancel)
                    order.OrderStatus = OrderStatusEnum.Cancelled;

                if (package.Visit.VisitStatus == VisitStatusEnum.Arrived
                    || package.Visit.VisitStatus == VisitStatusEnum.Completed)
                {
                    try
                    {
                        WorkTransactionEtc etc = WorkTransactionEtc.FindLastETC(package.Visit);
                        order.TimeEstimateComplete = new DateTime(
                            DateTime.Now.Year,
                            DateTime.Now.Month,
                            DateTime.Now.Day,
                            etc.Hours.Value, etc.Minutes.Value, 0);
                    }
                    catch (DataNotFoundException){}
                }

                order.TicketNumber = package.Task.ServmanOrderNum;
                order.ContactName = string.Empty;

                if (package.Work != null && package.Work.StartDate.HasValue)
                    order.ServiceDate = package.Work.StartDate.Value;
                else
                    order.ServiceDate = package.Visit.ServiceDate ?? DateTime.MinValue;

                DateTime? timeFrom;
                DateTime? timeTo;
                if (package.WorkDetail != null)
                {
                    timeFrom = package.WorkDetail.TimeBegin;
                    timeTo = package.WorkDetail.TimeEnd;
                } else
                {
                    timeFrom = package.Visit.PreferedTimeFrom;
                    timeTo = package.Visit.PreferedTimeTo;                    
                }

                order.TimeSchedule = ServmanConversionUtil.GetServmanTimeFrame(timeFrom, timeTo);
                if (package.Technician != null)
                    order.TechnicianId = package.Technician.ServmanTechId;
                    
                if (package.Van != null)
                    order.TruckId = package.Van.ServmanTruckId;

                order.ClosedAmount = package.Task.ClosedAmount;
                if (firstMonitoringPackage != null)
                    order.ClosedAmount += firstMonitoringPackage.Task.ClosedAmount;
                if (rugOnDefloodPackage != null)
                    order.ClosedAmount += rugOnDefloodPackage.Task.ClosedAmount;

//                if ((package.Task.TaskType == TaskTypeEnum.RugPickup
//                    || package.Task.TaskType == TaskTypeEnum.RugDelivery)
//                    && package.Task.ClosedAmount == decimal.Zero)
//                {
//                    foreach (Item item in package.Items)
//                        order.ClosedAmount += item.SubTotalCost;
//                }

                if (package.Project.AdvertisingSourceId != null)
                    order.AdvertisingSourceId = package.Project.AdvertisingSourceId.Value.ToString("000000");
                if (package.AdvertisingTechnician != null)
                    order.AdvertisingTechnicianReferenceId = package.AdvertisingTechnician.ServmanTechId;
                order.DateTimeFirstCall = package.Task.CreateDate ?? DateTime.MinValue;

                if (package.Work != null && package.Work.StartDate.HasValue)
                    order.DateSchedule = package.Work.StartDate.Value;
                else
                    order.DateSchedule = package.Visit.ServiceDate ?? DateTime.MinValue;

                if (package.WorkDetail != null)
                {
                    order.DateTimeDispatch = package.WorkDetail.TimeDispatch ?? DateTime.MinValue;
                    order.DateTimeArrived = package.WorkDetail.TimeArrive ?? DateTime.MinValue;
                    order.DateTimeCompleted = package.WorkDetail.TimeComplete ?? DateTime.MinValue;
                }                

                if (package.WorkDetail != null)
                    order.IsConfirmed = package.Visit.IsConfirmed;

                if (package.Task.TaskType == TaskTypeEnum.Help)
                    order.PrintedNote = "PART 2\r\n";

                string rugsNotes = GetRugsDescription(package.Items);

                if (rugsNotes != string.Empty && package.Project.ProjectType == ProjectTypeEnum.RugCleaning)
                {
                    rugsNotes += " " + package.Task.EstimatedClosedAmount.ToString("C");
                    order.PrintedNote += Utils.JoinStrings("\r\n", package.Visit.Notes, 
                        package.Task.Notes, rugsNotes);
                } else
                    order.PrintedNote += Utils.JoinStrings("\r\n", package.Visit.Notes, package.Task.Notes);                


                if (order.OrderStatus == OrderStatusEnum.Cancelled)
                    order.PrintedNote = Utils.JoinStrings("\r\n", order.PrintedNote, package.Task.FailReason);                

                order.DispatchNote = rugsNotes;
    
                if (rugOnDefloodPackage != null)
                {
                    string rugOnDefloodNotes;

                    if (rugOnDefloodPackage.Task.TaskType == TaskTypeEnum.RugPickup)
                        rugOnDefloodNotes = "Rug Pickup ";
                    else
                        rugOnDefloodNotes = "Rug Delivery ";
                   
                    rugOnDefloodNotes += GetRugsDescription(rugOnDefloodPackage.Items);

                    if (rugOnDefloodPackage.Task.ClosedAmount != decimal.Zero)
                        rugOnDefloodNotes += " " + rugOnDefloodPackage.Task.ClosedAmount.ToString("C");

                    if (string.IsNullOrEmpty(order.PrintedNote))
                        order.PrintedNote = rugOnDefloodNotes;
                    else
                        order.PrintedNote += "\r\n" + rugOnDefloodNotes;
                    
                    order.DispatchNote = rugOnDefloodNotes;
                }               

                if (package.PerformedByUser != null)
                    order.PerformedByUser = package.PerformedByUser.ServmanUserId;

                if (firstMonitoringPackage == null && package.ParentDefloodTask != null)
                {
                    try
                    {
                        Task prevMonitoring = Task.FindPrevMonitoring(package.ParentDefloodTask, package.Task);
                        order.OriginateTicketNumber = prevMonitoring.ServmanOrderNum;
                    }
                    catch (DataNotFoundException){}                    
                }

                //Save data
                if (package.Task.ServmanOrderNum == null || package.Task.ServmanOrderNum == string.Empty)
                {
                    package.Task.ServmanOrderNum = ServmanExportModel.InsertOrder(order);
                } else
                {
                    ServmanExportModel.UpdateOrder(order);
                }


                if (package.Task.ID != 0)
                {
                    Task freshTask = Task.FindByPrimaryKey(package.Task.ID);
                    freshTask.ServmanOrderNum = package.Task.ServmanOrderNum;
                    freshTask.LastSyncDate = DateTime.Now;
                    Task.Update(freshTask);                    
                }

                if (firstMonitoringPackage != null)
                {
                    Task freshFirstMonitoringTask = Task.FindByPrimaryKey(firstMonitoringPackage.Task.ID);
                    freshFirstMonitoringTask.ServmanOrderNum = package.Task.ServmanOrderNum;
                    freshFirstMonitoringTask.LastSyncDate = DateTime.Now;
                    Task.Update(freshFirstMonitoringTask);
                }

                if (pendingRugDelivery != null)
                {
                    Task freshPendingDelivery = Task.FindByPrimaryKey(pendingRugDelivery.Task.ID);
                    freshPendingDelivery.ServmanOrderNum = package.Task.ServmanOrderNum;
                    freshPendingDelivery.LastSyncDate = DateTime.Now;
                    Task.Update(freshPendingDelivery);                    
                }

                if (rugOnDefloodPackage != null)
                {
                    Task freshRugOnDefloodTask = Task.FindByPrimaryKey(rugOnDefloodPackage.Task.ID);
                    freshRugOnDefloodTask.LastSyncDate = DateTime.Now;
                    freshRugOnDefloodTask.ServmanOrderNum = package.Task.ServmanOrderNum;
                    Task.Update(freshRugOnDefloodTask);                    
                }

                Host.Trace("Order Export",
                    string.Format("Order export done. Order ID = {0}, ServmanId = {1}",
                    package.Task.ID, package.Task.ServmanOrderNum));
            }
        }

        #endregion
    }
}
