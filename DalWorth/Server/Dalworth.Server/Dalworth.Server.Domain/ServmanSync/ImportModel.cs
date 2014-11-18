using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;
using Dalworth.Server.Domain.package;
using Dalworth.Server.SDK;
using Dalworth.Server.Servman.Domain;

namespace Dalworth.Server.Domain.ServmanSync
{
    public class ImportModel
    {
        #region ImportAdvertisingSources

        private static void ImportAdvertisingSources(SyncToolInfo syncToolInfo)
        {
            if (syncToolInfo.LastImportAdSourceDate.Date == DateTime.Now.Date)
                return;

            List<ad_src> servmanAdsources = ad_src.Find();            
            Dictionary<string, byte> areaIdMap = new Dictionary<string, byte>();
            foreach (var area in Area.Find())
                areaIdMap.Add(area.ServmanId, area.ID);

            try
            {
                Database.Begin();

                foreach (var servmanAdsource in servmanAdsources)
                {
                    int adsourceId;
                    try
                    {
                        adsourceId = int.Parse(servmanAdsource.id_code.Trim());
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    AdvertisingSource existingAdsource;
                    try
                    {
                        existingAdsource = AdvertisingSource.FindByPrimaryKey(adsourceId);
                    }
                    catch (DataNotFoundException)
                    {
                        existingAdsource = new AdvertisingSource();
                        existingAdsource.TrackingUrl = string.Empty;
                    }

                    AdvertisingSource mergedAdsource = new AdvertisingSource(adsourceId,
                        !areaIdMap.ContainsKey(servmanAdsource.area_id.Trim()) ? areaIdMap["D/FW"] : areaIdMap[servmanAdsource.area_id.Trim()],
                        servmanAdsource.descript,
                        servmanAdsource.active,
                        servmanAdsource.techrefer,
                        servmanAdsource.acronym.Trim(),
                        existingAdsource.IsRestoration,
                        existingAdsource.TrackingUrl);

                    if (existingAdsource.ID == 0)
                        AdvertisingSource.Insert(mergedAdsource);
                    else
                        AdvertisingSource.Update(mergedAdsource);
                }

                syncToolInfo.LastImportAdSourceDate = DateTime.Now;
                SyncToolInfo.Update(syncToolInfo);

                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }            
        }

        #endregion

        #region ImportTrucks

        private static void ImportTrucks(SyncToolInfo syncToolInfo)
        {
            List<truck> newTrucks = truck.FindNewTrucks(syncToolInfo.LastImportedTruckId);

            if (newTrucks.Count == 0)
                return;

            try
            {
                Database.Begin();

                foreach (truck servmanTruck in newTrucks)
                {
                    Van van = new Van(0,
                        servmanTruck.truck_id.Trim(),
                        servmanTruck.truck_num.Trim(),
                        1,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        string.Empty,
                        servmanTruck.pager_num.Trim());
                    Van.Insert(van);
                }

                syncToolInfo.LastImportedTruckId = newTrucks[newTrucks.Count - 1].truck_id.Trim();
                SyncToolInfo.Update(syncToolInfo);

                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        #endregion

        #region ImportUpdateAllTechnicians

        public static void ImportUpdateAllTechnicians()
        {
            Host.Trace("Technician Import", "Started");
            List<techmast> servmanTechnicians = techmast.Find();
            
            foreach (techmast servmanTechnician in servmanTechnicians)
            {
                Employee employee;

                try
                {
                    employee = Employee.FindByServmanTechId(servmanTechnician.tech_id.Trim());
                }
                catch (DataNotFoundException)
                {
                    employee = new Employee();
                }

                employee.EmployeeType = EmployeeTypeEnum.Technician;
                employee.ServmanTechId = servmanTechnician.tech_id.Trim();
                employee.FirstName = ServmanConversionUtil.GetFirstName(servmanTechnician.tech_name.Trim());
                employee.LastName = ServmanConversionUtil.GetLastName(servmanTechnician.tech_name.Trim());
                employee.IsActive = servmanTechnician.active;
                employee.IsRestoration = servmanTechnician.cell.Trim() == "014";

                if (employee.ID == 0)
                    Employee.Insert(employee);
                else
                    Employee.Update(employee);
            }

            Host.Trace("Technician Import", "Done");
        }

        #endregion

        #region ImportDictionaries

        private static void ImportDictionaries(SyncToolInfo syncToolInfo)
        {
            ImportAdvertisingSources(syncToolInfo);
            ImportTrucks(syncToolInfo);
        }

        #endregion

        #region ImportCustomers

        public static void ImportCustomers()
        {
            SyncToolInfo syncToolInfo = SyncToolInfo.Find()[0];

            ImportAdvertisingSources(syncToolInfo);
            ImportTrucks(syncToolInfo);

            if (syncToolInfo.LastCustomerImportDate.Year == 1799)
                ImportAllCustomers();
            else
                ImportModifiedCustomers();
        }

        private static void ImportModifiedCustomers()
        {
            SyncToolInfo syncToolInfo = SyncToolInfo.Find()[0];
            List<custmast> customers = custmast.FindCustomers(syncToolInfo.LastCustomerImportDate);

            try
            {
                Database.Begin();

                foreach (custmast customer in customers)
                    ImportCustomer(customer);

                syncToolInfo.LastCustomerImportDate = DateTime.Now.AddDays(-1);
                SyncToolInfo.Update(syncToolInfo);

                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        private static void ImportAllCustomers()
        {
            string lastImportedCustomer = string.Empty;

            while (true)
            {                
                List<custmast> customersBlock = custmast.FindCustomers(lastImportedCustomer);
                if (customersBlock.Count == 0)
                {
                    //settings last importing date means first customer sync was performed
                    SyncToolInfo syncToolInfo = SyncToolInfo.Find()[0];
                    syncToolInfo.LastCustomerImportDate = DateTime.Now.AddDays(-1);
                    SyncToolInfo.Update(syncToolInfo);
                    return;
                }

                try
                {
                    Database.Begin();
                    foreach (custmast customer in customersBlock)
                        ImportCustomer(customer);
                    Database.Commit();
                    lastImportedCustomer = customersBlock[customersBlock.Count - 1].cust_id;
                }
                catch (Exception)
                {
                    Database.Rollback();
                    throw;
                }                
            }            
        }

        #endregion

        #region ImportOrders

        public static void ImportOrders()
        {
            SyncToolInfo syncToolInfo = SyncToolInfo.Find()[0];
            ImportDictionaries(syncToolInfo);

            string lastImportedTicket = syncToolInfo.LastImportedTicketNumber;
            List<h_order> orders = h_order.FindNewOrders(lastImportedTicket);

            foreach (h_order order in orders)
                ImportOrder(syncToolInfo, order, TaskTypeEnum.RugPickup, false);

            if (orders.Count > 0)
                PendingTaskGridState.MakePendingTaskGridDirty();                
        }

        #endregion

        #region ImportOrdersFirstTime

        public static void ImportOrdersFirstTime(FirstTimeOrder[] orders)
        {
            SyncToolInfo syncToolInfo = SyncToolInfo.Find()[0];
            ImportDictionaries(syncToolInfo);

            foreach (FirstTimeOrder order in orders)
            {
                h_order horder = null;

                try
                {
                    horder = h_order.FindByPrimaryKey(order.TicketNumber);
                }
                catch (DataNotFoundException)
                {
                    Host.Trace("Order Import",
                        string.Format("Order {0} not found", order.TicketNumber));                    
                }

                if (horder != null)
                {
                    ImportOrder(syncToolInfo, horder, order.TicketType, true);

                    try
                    {
                        string maxTicketNum = Task.FindMaxImportedServmanOrderNumber();
                        syncToolInfo.LastImportedTicketNumber = maxTicketNum;
                        SyncToolInfo.Update(syncToolInfo);
                    }
                    catch (DataNotFoundException) { }
                }                    
            }

            if (orders.Length > 0)
                PendingTaskGridState.MakePendingTaskGridDirty();                
        }

        #endregion

        #region ImportOrder

        //Accepts RugPickup, RugDelivery, Deflood, Monitoring, Misc
        private static void ImportOrder(SyncToolInfo syncToolInfo, h_order order, 
            TaskTypeEnum taskType, bool isFirstTimeSync)
        {
            Host.Trace("Order Import",
                string.Format("Importing Order ServmanId = {0}", order.ticket_num));

            hdeflood hdeflood;
            try
            {
                hdeflood = hdeflood.FindByPrimaryKey(order.ticket_num);
                if (!isFirstTimeSync && hdeflood.reschd_num > 0)
                {
                    Host.Trace("Order Import",
                        string.Format("Order {0} was rescheduled. Import ignored", order.ticket_num));
                    return;
                }
            }
            catch (DataNotFoundException)
            {
                Host.Trace("Order Import", "hdeflood record not found. Order ignored");
                return;
            }

            List<ddeflood> ddefloods = ddeflood.FindByTicket(order.ticket_num);

            m_alt_ad servmanAltAddress = null;
            if (order.alt_addr)
                servmanAltAddress = m_alt_ad.FindByPrimaryKey(order.ticket_num);
            
            custmast servmanCustomer = custmast.FindByPrimaryKey(order.cust_id);

            try
            {
                Task.FindByServmanOrderNum(order.ticket_num);
                Host.Trace("Order Import",
                    string.Format("Order {0} already imported. New data ignored", order.ticket_num));
                return;
            }
            catch (DataNotFoundException) { }

            try
            {
                Database.Begin();

                int? advertisingTechnicianId = null;
                if (hdeflood.tech_refer.Trim() != string.Empty)
                    try
                    {
                        Employee technician = Employee.FindByServmanTechId(hdeflood.tech_refer.Trim());
                        advertisingTechnicianId = technician.ID;
                    }
                    catch (DataNotFoundException) { }

                //Import Customer and Additional address
                Customer customer = ImportCustomer(servmanCustomer);

                Address alternativeAddress = null;
                if (servmanAltAddress != null)
                {
                    alternativeAddress = Customer.ConvertFromServman(servmanAltAddress);
                    Address.Insert(alternativeAddress);

                    CustomerAddressAdditional caa
                        = new CustomerAddressAdditional(customer.ID, alternativeAddress.ID);
                    CustomerAddressAdditional.Insert(caa);
                }

                QbSalesRep salesRep = null;
                if (advertisingTechnicianId.HasValue)
                {
                    try
                    {
                        salesRep = QbSalesRep.FindByEmployeeId(advertisingTechnicianId.Value, null);

                        //TODO: Email notification of the need to setup salesrep
                    }
                    catch (DataNotFoundException){}
                }
                
                //Importing Project
                Project project = new Project(0, null, 0,
                      customer.ID,
                      alternativeAddress == null ? customer.AddressId.Value : alternativeAddress.ID,
                      (int)ProjectStatusEnum.Open,null,
                      string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                      string.Empty, string.Empty, string.Empty, decimal.Zero,
                      null,
                      advertisingTechnicianId, null, null, decimal.Zero, decimal.Zero, DateTime.Now,
                      "80000002-1272662058", //Dalworth Clean
                      salesRep != null?salesRep.ListId:null);

                if (taskType == TaskTypeEnum.RugPickup || taskType == TaskTypeEnum.RugDelivery)
                    project.ProjectType = ProjectTypeEnum.RugCleaning;
                else if (taskType == TaskTypeEnum.Deflood || taskType == TaskTypeEnum.Monitoring)
                    project.ProjectType = ProjectTypeEnum.Deflood;
                else if (taskType == TaskTypeEnum.Miscellaneous)
                    project.ProjectType = ProjectTypeEnum.Miscellaneous;

                try
                {
                    int servmanAdSourceId = int.Parse(hdeflood.ad_source.Trim());
                    AdvertisingSource.FindByPrimaryKey(servmanAdSourceId);
                    project.AdvertisingSourceId = servmanAdSourceId;
                }
                catch (Exception)
                {
                    project.AdvertisingSourceId = 42; //Repeat
                }

                if (project.ProjectType == ProjectTypeEnum.Deflood)
                {
                    project.InsuranceAgency = hdeflood.i_agncy.Trim();
                    project.InsuranceAgencyPhone = hdeflood.i_agncy_ph.Trim();
                    project.InsuranceAgent = hdeflood.i_agent.Trim();
                    project.InsuranceCompany = hdeflood.i_carrier.Trim();
                    project.InsuranceAdjustor = hdeflood.i_adj.Trim();
                    project.InsuranceAdjustorPhone = hdeflood.i_adj_ph.Trim();                        
                }                
                Project.InsertAndLog(project);

                //Importing Task

                if (taskType == TaskTypeEnum.RugDelivery)
                {
                    ImportVisit(order, hdeflood, ddefloods, customer,
                        project, TaskTypeEnum.RugPickup, syncToolInfo, TaskStatusEnum.Completed);                    
                } else if (taskType == TaskTypeEnum.Monitoring)
                {                                                            
                    if (hdeflood.trans_num.Trim() == string.Empty)
                    {
                        Host.Trace("Order Import", "Unable to find previous Deflood order. Monitoring order import failed");
                        Database.Rollback();
                        return;                                            
                    }

                    List<OrderPackage> previosOrders = new List<OrderPackage>();                    

                    using (IDbConnection servmanConnection = Connection.Instance.GetTemporaryDbConnection(ConnectionKeyEnum.Servman))
                    {
                        servmanConnection.Open();

                        string currentTicketNumber = hdeflood.trans_num.Trim();
                        while (currentTicketNumber != string.Empty)
                        {
                            OrderPackage historicalOrder = new OrderPackage(
                                h_order.FindByPrimaryKey(currentTicketNumber, servmanConnection),
                                hdeflood.FindByPrimaryKey(currentTicketNumber, servmanConnection),
                                ddeflood.FindByTicket(currentTicketNumber, servmanConnection));

                            previosOrders.Add(historicalOrder);

                            currentTicketNumber = historicalOrder.HDeflood.trans_num.Trim();
                        }
                    }
                    
                    previosOrders.Reverse();

                    foreach (OrderPackage historicalOrder in previosOrders)
                    {
                        bool isFirstDeflood = previosOrders.IndexOf(historicalOrder) == 0;

                        ImportVisit(historicalOrder.HOrder,
                            historicalOrder.HDeflood,
                            historicalOrder.DDefloods,
                            customer,
                            project,
                            isFirstDeflood ? TaskTypeEnum.Deflood : TaskTypeEnum.Monitoring, 
                            syncToolInfo,
                            isFirstDeflood ? TaskStatusEnum.InProcess : TaskStatusEnum.Completed);                                            
                    }
                }

                Visit visit = ImportVisit(order, hdeflood, ddefloods, customer, 
                    project, taskType, syncToolInfo, TaskStatusEnum.NotCompleted);


                Database.Commit();
                
                if (Configuration.AutomatedVisitPrint && visit.IsServiceDateTodayOrTomorrow)
                {
                    VisitSummaryPackage package = new VisitSummaryPackage(visit);

                    try
                    {
                        package.Print();
                        visit.SyncToolPrintDate = DateTime.Now;
                        Visit.Update(visit);
                    }
                    catch (Exception ex)
                    {
                        Host.Trace("Order Import",
                            string.Format("Visit {0} print failed. " + ex.Message + ex.StackTrace,
                            visit.ID));
                    }
                }
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
            
        }

        #endregion

        #region ImportCustomer

        public static Customer ImportCustomer(custmast customer)
        {
            Host.Trace("Customer Import",
                string.Format("Importing Customer ServmanId = {0}", customer.cust_id));

            try
            {
                Customer myExistingCustomer = Customer.FindBy(customer.cust_id);

                if (customer.l_contact >= myExistingCustomer.Modified || customer.l_addr_chg >= myExistingCustomer.Modified)
                {
                    CustomerAndAddress myCustomer = Customer.ConvertFromServman(customer);
                    myCustomer.Customer.ID = myExistingCustomer.ID;
                    myCustomer.Address.ID = myExistingCustomer.AddressId ?? 0;
                    myCustomer.Customer.AddressId = myExistingCustomer.AddressId;

                    if (myCustomer.Address.ID == 0)
                    {
                        Address.Insert(myCustomer.Address);
                        myCustomer.Customer.AddressId = myCustomer.Address.ID;
                    }
                    else
                    {
                        Address.Update(myCustomer.Address);
                    }

                    myCustomer.Customer.LastSyncDate = DateTime.Now;
                    Customer.Update(myCustomer.Customer);

                    Host.Trace("Customer Import",
                        string.Format("Customer updated. ServmanId = {0}", customer.cust_id));
                    return myCustomer.Customer;
                }

                Host.Trace("Customer Import",
                    string.Format("Customer changes not found, data ignored. ServmanId = {0}", customer.cust_id));
                return myExistingCustomer;
            }
            catch (DataNotFoundException)
            {
                CustomerAndAddress myCustomer = Customer.ConvertFromServman(customer);
                Address.Insert(myCustomer.Address);
                myCustomer.Customer.AddressId = myCustomer.Address.ID;
                myCustomer.Customer.LastSyncDate = DateTime.Now;
                Customer.Insert(myCustomer.Customer);

                Host.Trace("Customer Import",
                    string.Format("Customer created. ServmanId = {0}", customer.cust_id));
                return myCustomer.Customer;
            }
        }

        #endregion

        #region ImportVisit

        private static Visit ImportVisit(h_order order, hdeflood hdeflood, List<ddeflood> ddefloods, 
            Customer customer, Project project, TaskTypeEnum taskType, SyncToolInfo syncToolInfo, 
            TaskStatusEnum taskStatus)
        {
            string taskNotes = Utils.JoinStrings("\r\n", hdeflood.src_flood.Trim(), hdeflood.rooms.Trim());

            List<string> ddefloodNotes = new List<string>();
            foreach (ddeflood deflood in ddefloods)
                ddefloodNotes.Add(deflood.note.Trim());
            taskNotes += Utils.JoinStrings("\r\n", ddefloodNotes.ToArray());

            Task task = null;

            if (taskType == TaskTypeEnum.Monitoring)
            {
                List<Task> projectTasks = Task.FindByProject(project);
                foreach (Task projectTask in projectTasks)
                {
                    if (projectTask.TaskType == TaskTypeEnum.Deflood)
                    {
                        task = projectTask;
                        break;
                    }
                }

                if (task == null)
                    throw new Exception("Deflood task not found in project");

            } else
            {                
                task = new Task(0, null,
                     order.ticket_num,
                     project.ID,
                     (int)taskType,
                     (int)taskStatus,
                     null, 
                     true, 
                     string.Empty, null,
                     DateTime.Now,
                     order.date,
                     null,
                     string.Empty,
                     taskNotes,
                     string.Empty, string.Empty,
                     false,
                     taskType == TaskTypeEnum.Deflood && taskStatus != TaskStatusEnum.NotCompleted 
                        ? (decimal)hdeflood.amount : decimal.Zero,
                     false,
                     decimal.Zero, false,
                     false, DateTime.Now, DateTime.Now, null, null, 0, null, false, 0, null);
                project.ClosedAmount += task.ClosedAmount;

                if (task.TaskType == TaskTypeEnum.RugPickup)
                {
                    task.IsClosedAmountAutoCalculated = true;
                    task.IsEstimatedClosedAmountAutoCalculated = true;
                }

                if (task.TaskType == TaskTypeEnum.RugDelivery)
                {
                    if (order.date.Year == DateTime.Now.Year)
                    {
                        task.IsReady = true;
                        task.ServiceDate = order.date;
                    }
                    else
                    {
                        task.IsReady = false;
                        task.ServiceDate = null;
                    }
                }

                if (task.TaskType == TaskTypeEnum.RugPickup && taskStatus == TaskStatusEnum.Completed)
                {
                    if (order.date.Year == DateTime.Now.Year + 1)
                        task.ServiceDate = order.date.AddYears(-1);
                    else
                        task.ServiceDate = hdeflood.d_1st_call;
                }

                Task.Insert(task);
                task.Number = task.ID.ToString();
                Task.Update(task);

                if (task.TaskType == TaskTypeEnum.Deflood)
                {
                    DefloodDetail defloodDetail = new DefloodDetail(task.ID,
                        hdeflood.d_of_flood, null, decimal.Zero);
                    DefloodDetail.Insert(defloodDetail);
                }

                if (task.TaskType == TaskTypeEnum.RugDelivery)
                {
                    Task completedRugPickup = Task.FindRugPickup(task);
                    Visit visitWithRugPickup = Visit.FindByTask(completedRugPickup)[0];
                    WorkTransaction completeTransaction =
                        WorkTransaction.FindBy(visitWithRugPickup.ID, WorkTransactionTypeEnum.VisitCompleted);
                    WorkTransactionTask workTransactionTask = new WorkTransactionTask(
                        completeTransaction.ID,
                        task.ID,
                        false, true,
                        (int)WorkTransactionTaskActionEnum.Generated);
                    WorkTransactionTask.Insert(workTransactionTask);
                }
            }            

            Task monitoringTask = null;
            if (task.TaskType == TaskTypeEnum.Deflood || task.TaskType == TaskTypeEnum.Monitoring)
            {
                monitoringTask = new Task(0, 
                    task.ID,
                    order.ticket_num,
                    project.ID,
                    (int)TaskTypeEnum.Monitoring,
                    taskStatus == TaskStatusEnum.InProcess ? (int)TaskStatusEnum.Completed : (int)taskStatus,
                    null,
                    true,
                    string.Empty, null,
                    DateTime.Now,
                    order.date,
                    null,
                    string.Empty, 
                    taskType == TaskTypeEnum.Monitoring ? taskNotes : string.Empty,
                    string.Empty, string.Empty,
                    false,
                    taskType == TaskTypeEnum.Monitoring ? (decimal)hdeflood.amount : decimal.Zero,
                    false,
                    decimal.Zero, false,
                    false, DateTime.Now, DateTime.Now, null, null, 0, null, false, 0, null);                

                Task.Insert(monitoringTask);
                monitoringTask.Number = monitoringTask.ID.ToString();
                Task.Update(monitoringTask);

                project.ClosedAmount += monitoringTask.ClosedAmount;
                Project.UpdateAndLog(project);

                MonitoringDetail monitoringDetail = new MonitoringDetail(monitoringTask.ID,
                    false, false, false, false, false, false, false, 
                    string.Empty, false, false, string.Empty, string.Empty, string.Empty, false);
                MonitoringDetail.Insert(monitoringDetail);


                Task prevMonitoring = null;
                try
                {
                    prevMonitoring = Task.FindPrevMonitoring(task, monitoringTask);
                }
                catch (DataNotFoundException){}

                if (prevMonitoring != null)
                {
                    Visit visitWithPrevMonitoring = Visit.FindByTask(prevMonitoring)[0];
                    WorkTransaction completeTransaction =
                        WorkTransaction.FindBy(visitWithPrevMonitoring.ID, WorkTransactionTypeEnum.VisitCompleted);
                    WorkTransactionTask workTransactionTask = new WorkTransactionTask(
                        completeTransaction.ID,
                        monitoringTask.ID,
                        false, true,
                        (int)WorkTransactionTaskActionEnum.Generated);
                    WorkTransactionTask.Insert(workTransactionTask);                    
                }                
            }           

            //Insert items
            List<Item> items = new List<Item>();
            if (task.TaskType == TaskTypeEnum.RugPickup)
            {
                if (taskStatus == TaskStatusEnum.Completed)
                {
                    items = Item.ParseFromServman(hdeflood.note);                                        
                    if (items.Count == 0)
                        items.Add(Item.GetDefaultRug());
                }
            }
            else if (task.TaskType == TaskTypeEnum.RugDelivery)
            {
                Task rugPickupTask = Task.FindRugPickup(task);
                items = Item.FindByTask(rugPickupTask);
            }

            foreach (Item item in items)
            {
                item.TaskId = task.ID;
                Item.Insert(item);
            }
            
            Visit visit = new Visit(0,
                taskStatus == TaskStatusEnum.Completed || taskStatus == TaskStatusEnum.InProcess 
                    ? (int)VisitStatusEnum.Completed : (int)VisitStatusEnum.Pending,
                DateTime.Now,
                monitoringTask != null ? monitoringTask.ServiceDate : task.ServiceDate,
                null,
                null,
                null,
                customer.ID,
                project.ServiceAddressId,
                hdeflood.note.Trim(),
                null, null, null, false, false, false, 
                taskType == TaskTypeEnum.Monitoring ? monitoringTask.ClosedAmount : task.ClosedAmount, 
                null, false);

            Visit.Insert(visit);
            VisitTask visitTask = new VisitTask(visit.ID, task.ID);
            VisitTask.Insert(visitTask);
            if (monitoringTask != null)
            {
                VisitTask monitoringVisitTask = new VisitTask(visit.ID, monitoringTask.ID);
                VisitTask.Insert(monitoringVisitTask);
            }

            syncToolInfo.LastImportedTicketNumber = order.ticket_num;
            SyncToolInfo.Update(syncToolInfo);

            if (taskStatus == TaskStatusEnum.Completed || taskStatus == TaskStatusEnum.InProcess)
                CompleteVisit(visit, hdeflood);

            Host.Trace("Order Import",
                string.Format("Order import done. ID = {0}, ServmanId = {1}",
                task.ID, task.ServmanOrderNum));                

            return visit;
        }

        #endregion

        #region CompleteVisit

        private static void CompleteVisit(Visit visit, hdeflood hdeflood)
        {                        
            List<Task> tasks = Task.FindByVisit(visit);
            Task mainTask = tasks[0];
            Project project = Project.FindByPrimaryKey(mainTask.ProjectId);
            
            DateTime completionDate;
            if (mainTask.TaskType == TaskTypeEnum.Deflood)
            {
                if (Utils.GetDateTimeFromServman(hdeflood.d_complete, hdeflood.t_complete).HasValue)
                    completionDate = Utils.GetDateTimeFromServman(hdeflood.d_complete, hdeflood.t_complete).Value;
                else if (Utils.GetDateTimeFromServman(hdeflood.d_dispatch, hdeflood.t_dispatch).HasValue)
                    completionDate = Utils.GetDateTimeFromServman(hdeflood.d_dispatch, hdeflood.t_dispatch).Value.AddMinutes(30);
                else
                    completionDate = mainTask.ServiceDate.Value;
            } else
                completionDate = mainTask.ServiceDate.Value;

            Employee technician = null;
            if (hdeflood.tech_id.Trim() != string.Empty)
            {
                try
                {
                    technician = Employee.FindByServmanTechId(hdeflood.tech_id.Trim());
                }
                catch (DataNotFoundException){}
            }
                
            if (technician == null)
                technician = Employee.FindByServmanTechId("519"); //Shane Hobbs

            try
            {
                DashboardSharedSetting.FindByPrimaryKey(completionDate.Date, technician.ID);
            }
            catch (DataNotFoundException)
            {
                if (!DashboardSharedSetting.IsContainsSettings(completionDate.Date))
                {
                    List<Employee> unknownTechnicians = Employee.FindUnknownTechnicians();
                    for (int i = 0; i < unknownTechnicians.Count; i++)
                    {
                        DashboardSharedSetting setting = new DashboardSharedSetting(
                            completionDate.Date,
                            unknownTechnicians[i].ID,
                            unknownTechnicians[i].ID,
                            false, i);
                        DashboardSharedSetting.Insert(setting);
                    }
                }

                List<DashboardSharedSetting> dashboardSettings
                    = DashboardSharedSetting.FindSettings(completionDate.Date);

                foreach (DashboardSharedSetting setting in dashboardSettings)
                {
                    if (setting.TechnicianId == setting.UnknownTechnicianId)//means this cell is set to unknown technician
                    {
                        DashboardSharedSetting.Delete(setting);
                        setting.TechnicianId = technician.ID;
                        setting.IsVisible = true;
                        DashboardSharedSetting.Insert(setting);
                        break;
                    }
                }                
            }


            Van van = null;
            try
            {
                techschd techSchedule;

                using (IDbConnection servmanConnection = Connection.Instance.GetTemporaryDbConnection(ConnectionKeyEnum.Servman))
                {
                    servmanConnection.Open();
                    techSchedule = techschd.FindByPrimaryKey(
                        technician.ServmanTechId, completionDate.Date, servmanConnection);
                }
                
                if (techSchedule.truck_id.Trim() != string.Empty)
                {
                    try
                    {
                        van = Van.FindByServmanTruckId(techSchedule.truck_id.Trim());
                    }
                    catch (DataNotFoundException){}
                }

                if (techSchedule.truck_num.Trim() != string.Empty)
                    van = Van.FindByServmanTruckNum(techSchedule.truck_num.Trim());
            }
            catch (DataNotFoundException){}

            if (van == null)
            {
                if (technician.DefaultVanId.HasValue)
                    van = Van.FindByPrimaryKey(technician.DefaultVanId.Value);
                else
                    van = Van.FindAvailableVans(completionDate.Date)[0];
            }               

            Work work = null;
            try
            {
                work = Work.FindWorkByTechAndDate(technician.ID, completionDate.Date);
            }
            catch (DataNotFoundException){}

            if (work == null)
            {
                work = new Work(0, 78,
                    technician.ID,
                    van.ID,
                    completionDate.Date,
                    (int)WorkStatusEnum.StartDayDone,
                    string.Empty, string.Empty, string.Empty, false, DateTime.Now,
                    completionDate.Date, null, decimal.Zero);
                Work.Insert(work);

                WorkTransaction startDayTransaction = new WorkTransaction(0,
                    work.ID,
                    78,
                    null,
                    (int)WorkTransactionTypeEnum.StartDayDone,
                    DateTime.Now,
                    decimal.Zero, false);
                WorkTransaction.Insert(startDayTransaction);
            }

            bool isFirstDeflood = Visit.FindByTask(mainTask).Count == 1;

            if (mainTask.TaskType == TaskTypeEnum.Deflood)
            {                
                if (isFirstDeflood) 
                    work.ClosedDollarAmount += mainTask.ClosedAmount;
                work.ClosedDollarAmount += tasks[1].ClosedAmount; //add monitoring task closed amount
                Work.Update(work);
            }

            //Time start end etc undefined only for Rug Pickup            
            DateTime timeBegin = DateTime.MinValue;
            DateTime timeEnd = DateTime.MinValue;
            
            if (mainTask.TaskType == TaskTypeEnum.Deflood)
            {
                if (hdeflood.t_dispatch.Trim() != string.Empty)
                    timeBegin = Utils.GetDateTimeFromServman(completionDate.Date, hdeflood.t_dispatch).Value;

                if (completionDate.Hour != 0 || completionDate.Minute != 0)
                {
                    timeEnd = completionDate;
                    if (timeBegin == DateTime.MinValue)
                    {
                        timeBegin = timeEnd.AddMinutes(-30);
                        if (timeBegin.Date != completionDate.Date)
                            timeBegin = completionDate.Date; //start of day
                    }                        
                }                    
            }

            if (timeBegin == DateTime.MinValue)
                timeBegin = FindLatestWorkDetailEndTime(work);

            if (timeEnd == DateTime.MinValue || timeEnd <= timeBegin)
            {
                timeEnd = timeBegin.AddMinutes(30);
                if (timeEnd.Date != completionDate.Date)
                    timeEnd = new DateTime(completionDate.Year, completionDate.Month, 
                        completionDate.Day, 23, 59, 59);
            }                

            WorkDetail workDetail = new WorkDetail(0,
                work.ID,
                visit.ID,
                timeBegin,
                timeEnd,
                null, null, null, null, timeEnd,
                timeBegin,
                timeEnd);
            WorkDetail.InsertAndLog(workDetail);

            visit.ConfirmDateTime = completionDate.Date;
            visit.ConfirmedFrameBegin = timeBegin;
            visit.ConfirmedFrameEnd = timeBegin.AddHours(2);
            Visit.Update(visit);

            if (hdeflood.t_dispatch.Trim() != string.Empty)
            {
                workDetail.TimeDispatch = timeBegin;
                WorkDetail.UpdateAndLog(workDetail);
                WorkTransaction dispatchTransaction = new WorkTransaction(0,
                    work.ID, 78, visit.ID,
                    (int)WorkTransactionTypeEnum.VisitDispatched,
                    DateTime.Now, decimal.Zero, false);
                WorkTransaction.Insert(dispatchTransaction);
            }

            if (mainTask.TaskType == TaskTypeEnum.Deflood)
            {
                List<MonitoringReading> monitoringReadings = MonitoringReading.DefaultReadings;

                foreach (MonitoringReading reading in monitoringReadings)
                {
                    reading.MonitoringTaskId = tasks[1].ID;
                    MonitoringReading.Insert(reading);
                }                
            }

            WorkTransaction completeTransaction = new WorkTransaction(0,
                work.ID, 78, visit.ID, 
                (int)WorkTransactionTypeEnum.VisitCompleted,
                DateTime.Now, 
                decimal.Zero, false);
            WorkTransaction.Insert(completeTransaction);


            decimal currentProjectClosedAmount = project.ClosedAmount;
            if (mainTask.TaskType == TaskTypeEnum.Deflood)
            {                
                if (isFirstDeflood)
                    project.ClosedAmount -= mainTask.ClosedAmount;
                project.ClosedAmount -= tasks[1].ClosedAmount;
                Project.UpdateAndLog(project);
            }
            Project dumpedProject = Project.Dump(project, completeTransaction.ID);
            project.ClosedAmount = currentProjectClosedAmount;
            Project.UpdateAndLog(project);

            for (int i = 0; i < tasks.Count; i++)
            {
                Task taskArchiveCopy = (Task)tasks[i].Clone();

                if (tasks[i].TaskType == TaskTypeEnum.Deflood && !isFirstDeflood)
                    tasks[i].TaskStatus = TaskStatusEnum.InProcess;
                else
                {
                    tasks[i].TaskStatus = TaskStatusEnum.NotCompleted;
                    tasks[i].ClosedAmount = decimal.Zero;
                }
                Task.Update(tasks[i]);
                Task.Dump(tasks[i], completeTransaction.ID, dumpedProject.ID);
                Task.Update(taskArchiveCopy);
                tasks[i] = taskArchiveCopy;
            }

            WorkTransaction equipmentWorkTransaction = new WorkTransaction(0,
                work.ID, 78, visit.ID,
                (int)WorkTransactionTypeEnum.VisitEquipmentTransfer,
                DateTime.Now,
                decimal.Zero, false);
            WorkTransaction.Insert(equipmentWorkTransaction);

            EquipmentTransaction equipmentTransaction = new EquipmentTransaction(0,
                equipmentWorkTransaction.ID,
                78, timeEnd, DateTime.Now, string.Empty);
            EquipmentTransaction.Insert(equipmentTransaction);

            WorkTransactionProject workTransactionProject = new WorkTransactionProject(
                completeTransaction.ID,
                project.ID,
                true, false);
            WorkTransactionProject.Insert(workTransactionProject);

            foreach (Task task in tasks)
            {
                WorkTransactionTask workTransactionTask = new WorkTransactionTask(
                    completeTransaction.ID,
                    task.ID, true, false,
                    (int)WorkTransactionTaskActionEnum.Complete);

                if (task.TaskStatus == TaskStatusEnum.InProcess)
                    workTransactionTask.WorkTransactionTaskAction = WorkTransactionTaskActionEnum.InProcess;                

                WorkTransactionTask.Insert(workTransactionTask);                            
            }
        }

        private static DateTime FindLatestWorkDetailEndTime(Work work)
        {
            DateTime result = work.StartDate.Value.Date;
            List<WorkDetail> workDetails = WorkDetail.FindBy(work);

            foreach (WorkDetail workDetail in workDetails)
            {
                if (workDetail.TimeEnd > result)
                    result = workDetail.TimeEnd;
            }

            return result;
        }

        #endregion
    }

    public class FirstTimeOrder
    {
        #region Constructor

        public FirstTimeOrder() {}

        public FirstTimeOrder(string ticketNumber, TaskTypeEnum ticketType)
        {
            m_ticketNumber = ticketNumber;
            m_ticketType = ticketType;
        }

        #endregion

        #region TicketNumber

        private string m_ticketNumber;       
        public string TicketNumber
        {
            get { return m_ticketNumber; }
            set { m_ticketNumber = value; }
        }

        #endregion

        #region TicketType

        private TaskTypeEnum m_ticketType;
        public TaskTypeEnum TicketType
        {
            get { return m_ticketType; }
            set { m_ticketType = value; }
        }

        #endregion

        #region GetError

        public string GetError()
        {
            string error = string.Empty;
            
            if (m_ticketNumber.Length != 6)
                error = "Ticket number should have 6 digits. ";
            else if (
                !char.IsDigit(m_ticketNumber[0])
                || !char.IsDigit(m_ticketNumber[1])
                || !char.IsDigit(m_ticketNumber[2])
                || !char.IsDigit(m_ticketNumber[3])
                || !char.IsDigit(m_ticketNumber[4])
                || !char.IsDigit(m_ticketNumber[5]))
            {
                error = "Ticket number should have 6 digits. ";
            }

            if (m_ticketType != TaskTypeEnum.RugPickup
                && m_ticketType != TaskTypeEnum.RugDelivery 
                && m_ticketType != TaskTypeEnum.Deflood 
                && m_ticketType != TaskTypeEnum.Monitoring
                && m_ticketType != TaskTypeEnum.Miscellaneous)
            {
                error += m_ticketType + " ticket type is not allowed";
            }


            if (error != string.Empty)
                return "Ticket " + m_ticketNumber + ": " + error;
            return string.Empty;                
        }

        #endregion
    }

    public class OrderPackage
    {
        #region Constructor

        public OrderPackage(h_order hOrder, hdeflood hDeflood, List<ddeflood> dDefloods)
        {
            m_hOrder = hOrder;
            m_hDeflood = hDeflood;
            m_dDefloods = dDefloods;
        }

        #endregion

        #region HOrder

        private h_order m_hOrder;        
        public h_order HOrder
        {
            get { return m_hOrder; }
            set { m_hOrder = value; }
        }

        #endregion

        #region HDeflood

        private hdeflood m_hDeflood;
        public hdeflood HDeflood
        {
            get { return m_hDeflood; }
            set { m_hDeflood = value; }
        }

        #endregion

        #region DDefloods

        private List<ddeflood> m_dDefloods;
        public List<ddeflood> DDefloods
        {
            get { return m_dDefloods; }
            set { m_dDefloods = value; }
        }

        #endregion
    }
}
