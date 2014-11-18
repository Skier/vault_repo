using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Data;
using Dalworth.Domain;
using Dalworth.Domain.Sync;
using Dalworth.Domain.SyncService;
using Dalworth.SDK;
using Address=Dalworth.Domain.Address;
using Customer=Dalworth.Domain.Customer;
using Employee=Dalworth.Domain.Employee;
using Equipment=Dalworth.Domain.Equipment;
using Item=Dalworth.Domain.Item;
using Project=Dalworth.Domain.Project;
using Van=Dalworth.Domain.Van;
using Work=Dalworth.Domain.Work;
using WorkDetail=Dalworth.Domain.WorkDetail;
using WorkEquipment=Dalworth.Domain.WorkEquipment;
using SyncService = Dalworth.Domain.SyncService;
using Task=Dalworth.Domain.Task;
using TaskItemDelivery=Dalworth.Domain.TaskItemDelivery;
using Visit=Dalworth.Domain.Visit;

namespace Dalworth.Windows.StartDay.Login
{
    public class LoginModel : StartDayBaseModel, IModel
    {        
        #region Employees

        private List<Employee> m_employees;
        public List<Employee> Employees
        {
            get { return m_employees; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_employees = Employee.FindBy(EmployeeTypeEnum.Technician);
            StartDayModel = new StartDayModel();
        }

        #endregion

        #region IsPasswordCorrect

        public bool IsPasswordCorrect(Employee employee, string password)
        {
            return employee.Password == password;
        }

        #endregion

        #region IsWorkExist

        public bool IsWorkExist(Employee employee)
        {
            DalworthSyncService service = new DalworthSyncService();
            return service.IsTodayWorkExist(Configuration.ConnectionKey, employee.ID);
        }

        #endregion

        #region GetStartDayPackage

        public StartDayPackage GetStartDayPackage(Employee employee)
        {
            DalworthSyncService service = new DalworthSyncService();
            return service.GetStartDayPackage(Configuration.ConnectionKey, employee.ID);
        }

        #endregion

        #region SaveStartDayPackage

        public void SaveStartDayPackage(StartDayPackage package)
        {
            //Insert items
            foreach (SyncService.Item item in package.Items)
            {
                Item itemLocal =
                    new Item(
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
                    Item exisitngLocalItem = Item.FindByServerId(item.ID, null);
                    itemLocal.ID = exisitngLocalItem.ID;
                    Item.Update(itemLocal);
                } catch (DataNotFoundException)
                {
                    Counter.Assign(itemLocal);
                    Item.Insert(itemLocal);                    
                }

            }
            

            //Insert equipment
            foreach (SyncService.Equipment equipment in package.Equipments)
            {
                Equipment equipmentLocal = new Equipment(
                    equipment.ID,
                    equipment.EquipmentTypeId,
                    equipment.SerialNumber);

                try
                {
                    Equipment.FindByPrimaryKey(equipment.ID);
                    Equipment.Update(equipmentLocal);                    
                }
                catch (DataNotFoundException)
                {
                    Counter.UpdateIfGreater(equipmentLocal);
                    Equipment.Insert(equipmentLocal);                    
                }
            }            

            //Insert Addresses            
            foreach (SyncService.Address address in package.Addresses)
            {
                Address addressLocal =
                    new Address(
                        address.ID,
                        address.Address1,
                        address.Address2,
                        address.City,
                        address.State,
                        address.Zip,
                        address.Map);

                try
                {
                    Address.FindByPrimaryKey(address.ID);
                    Address.Update(addressLocal);
                }
                catch (DataNotFoundException)
                {
                    Counter.UpdateIfGreater(addressLocal);
                    Address.Insert(addressLocal);
                }
            }            

            //Insert Customers
            foreach (SyncService.Customer customer in package.Customers)
            {
                Customer customerLocal =
                    new Customer(
                        customer.ID,
                        customer.AddressId,
                        customer.FirstName,
                        customer.LastName,
                        customer.Phone1,
                        customer.Phone2);

                try
                {
                    Customer.FindByPrimaryKey(customer.ID);
                    Customer.Update(customerLocal);
                }
                catch (DataNotFoundException)
                {
                    Counter.UpdateIfGreater(customerLocal);
                    Customer.Insert(customerLocal);
                }
            }

            //Insert Van
            Van van = new Van(
                package.Van.ID,
                package.Van.LicensePlateNumber,
                package.Van.EngineNumber,
                package.Van.BodyNumber,
                package.Van.Color,
                package.Van.OilChangeDue);

            try
            {
                Van.FindByPrimaryKey(package.Van.ID);
                Van.Update(van);                

            } catch (DataNotFoundException)
            {
                Counter.UpdateIfGreater(van);
                Van.Insert(van);                
            }

            //Insert visits
            foreach (SyncService.Visit visit in package.Visits)
            {
                Visit visitLocal =
                    new Visit(
                        visit.ID,
                        visit.VisitStatusId,
                        visit.CreateDate,
                        visit.ServiceDate,
                        visit.PreferedTimeFrom,
                        visit.PreferedTimeTo,
                        visit.CustomerId,
                        visit.ServiceAddressId,
                        visit.Notes);

                try
                {
                    Visit.FindByPrimaryKey(visit.ID);
                    Visit.Update(visitLocal);
                } catch (DataNotFoundException)
                {
                    Counter.UpdateIfGreater(visitLocal);
                    Visit.Insert(visitLocal);                    
                }
            }

            //Insert Projects and Tasks
            foreach (TaskPackage task in package.Tasks)
            {
                Project projectLocal =
                    new Project(
                        task.Project.ID,
                        task.Project.CustomerId,
                        task.Project.ServiceAddressId,
                        task.Project.ProjectTypeId,
                        task.Project.ProjectStatusId,
                        task.Project.Description);

                try
                {
                    Project.FindByPrimaryKey(task.Project.ID);
                    Project.Update(projectLocal);
                }
                catch (DataNotFoundException)
                {
                    Counter.UpdateIfGreater(projectLocal);
                    Project.Insert(projectLocal);
                }

                //package.Visits[0].
                //task.

                Task taskLocal =
                    new Task(
                        0,
                        task.Task.ID,
                        task.Task.ProjectId,
                        task.Visit.ID,
                        task.Task.TaskTypeId,
                        task.Task.TaskStatusId,
                        task.Task.Number,
                        task.Task.Sequence,
                        task.Task.CreateDate,
                        task.Task.ServiceDate,
                        task.Task.DurationMin,
                        task.Task.Description,
                        task.Task.Message,
                        task.Task.Notes);

                try
                {
                    Task existingLocalTask = Task.FindByServerId(task.Task.ID, null);
                    taskLocal.ID = existingLocalTask.ID;
                    Task.Update(taskLocal);
                    
                } catch(DataNotFoundException)
                {
                    Counter.Assign(taskLocal);
                    Task.Insert(taskLocal);                                    
                }
            }

            //Insert TaskItemDelivery
            foreach (SyncService.TaskItemDelivery delivery in package.TaskItemDeliveries)
            {
                Task insertedTask = Task.FindByServerId(delivery.TaskId, null);
                Item insertedItem = Item.FindByServerId(delivery.ItemId.Value, null);

                TaskItemDelivery deliveryLocal =
                    new TaskItemDelivery(
                        delivery.ID,
                        insertedTask.ID,
                        insertedItem.ID);

                try
                {
                    TaskItemDelivery.FindByPrimaryKey(delivery.ID);
                    TaskItemDelivery.Update(deliveryLocal);                    
                } catch (DataNotFoundException)
                {
                    Counter.UpdateIfGreater(deliveryLocal);
                    TaskItemDelivery.Insert(deliveryLocal);                    
                }
            }            

            //Insert Work
            Work work = new Work(
                package.Work.ID,
                package.Work.DispatchEmployeeId,
                package.Work.TechnicianEmployeeId,
                package.Work.VanId.Value,
                package.Work.StartDate,
                package.Work.WorkStatusId,
                package.Work.StartMessage,
                package.Work.EndMessage,
                package.Work.EquipmentNotes);

            try
            {
                Work.FindByPrimaryKey(package.Work.ID);
                Work.Update(work);                
            } catch (DataNotFoundException)
            {
                Counter.UpdateIfGreater(work);
                Work.Insert(work);                
            }


            StartDayModel.Work = work;

            //Insert WorkDetails
            foreach (SyncService.WorkDetail detail in package.WorkDetails)
            {
                WorkDetail detailLocal =
                    new WorkDetail(
                        detail.ID,
                        detail.WorkId,
                        detail.VisitId,
                        detail.Sequence,
                        detail.WorkDetailStatusId);

                try
                {
                    WorkDetail.FindByPrimaryKey(detail.ID);
                    WorkDetail.Update(detailLocal);                    
                } catch (DataNotFoundException)
                {
                    Counter.UpdateIfGreater(detailLocal);
                    WorkDetail.Insert(detailLocal);                    
                }
            }

            //Inset WorkEquipment
            foreach (SyncService.WorkEquipment equipment in package.WorkEquipments)
            {
                WorkEquipment equipmentLocal = 
                    new WorkEquipment(
                        equipment.ID,
                        equipment.WorkId,
                        equipment.EquipmentTypeId,
                        equipment.Quantity);

                try
                {
                    WorkEquipment.FindByPrimaryKey(equipment.ID);
                    WorkEquipment.Update(equipmentLocal);                    
                } catch (DataNotFoundException)
                {
                    Counter.UpdateIfGreater(equipmentLocal);
                    WorkEquipment.Insert(equipmentLocal);                    
                }
            }
            
        }

        #endregion
    }
}
