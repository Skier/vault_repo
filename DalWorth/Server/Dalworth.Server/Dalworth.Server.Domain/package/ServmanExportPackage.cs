using System;
using System.Collections.Generic;
using System.Text;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain.package
{
    public class ServmanExportPackage
    {
        #region Constructor

        public ServmanExportPackage(Customer customer, Address customerAddress, Address additionalAddress, 
            Task task, Project project, Visit visit, Work work, WorkDetail workDetail, 
            List<Item> items, Task parentDefloodTask, DefloodDetail defloodDetail, 
            Employee technician, Van van, Employee advertisingTechnician, Employee performedByUser)
        {
            m_customer = customer;
            m_customerAddress = customerAddress;
            m_additionalAddress = additionalAddress;
            m_task = task;
            m_project = project;
            m_visit = visit;
            m_work = work;
            m_workDetail = workDetail;
            m_items = items;
            m_parentDefloodTask = parentDefloodTask;
            m_defloodDetail = defloodDetail;
            m_technician = technician;
            m_van = van;
            m_advertisingTechnician = advertisingTechnician;
            m_performedByUser = performedByUser;
        }

        #endregion


        #region Customer

        private Customer m_customer;
        public Customer Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        #endregion

        #region CustomerAddress

        private Address m_customerAddress;
        public Address CustomerAddress
        {
            get { return m_customerAddress; }
            set { m_customerAddress = value; }
        }

        #endregion

        #region AdditionalAddress

        private Address m_additionalAddress;
        public Address AdditionalAddress
        {
            get { return m_additionalAddress; }
            set { m_additionalAddress = value; }
        }

        #endregion

        #region Task

        private Task m_task;
        public Task Task
        {
            get { return m_task; }
            set { m_task = value; }
        }

        #endregion

        #region Project

        private Project m_project;
        public Project Project
        {
            get { return m_project; }
            set { m_project = value; }
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

        #region Work

        private Work m_work;
        public Work Work
        {
            get { return m_work; }
            set { m_work = value; }
        }

        #endregion

        #region WorkDetail

        private WorkDetail m_workDetail;
        public WorkDetail WorkDetail
        {
            get { return m_workDetail; }
            set { m_workDetail = value; }
        }

        #endregion

        #region Items

        private List<Item> m_items;
        public List<Item> Items
        {
            get { return m_items; }
            set { m_items = value; }
        }

        #endregion

        #region ParentDefloodTask

        private Task m_parentDefloodTask;
        public Task ParentDefloodTask
        {
            get { return m_parentDefloodTask; }
            set { m_parentDefloodTask = value; }
        }

        #endregion

        #region DefloodDetail

        private DefloodDetail m_defloodDetail;
        public DefloodDetail DefloodDetail
        {
            get { return m_defloodDetail; }
            set { m_defloodDetail = value; }
        }

        #endregion

        #region Technician

        private Employee m_technician;
        public Employee Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
        }

        #endregion

        #region Van

        private Van m_van;
        public Van Van
        {
            get { return m_van; }
            set { m_van = value; }
        }

        #endregion

        #region AdvertisingTechnician

        private Employee m_advertisingTechnician;
        public Employee AdvertisingTechnician
        {
            get { return m_advertisingTechnician; }
            set { m_advertisingTechnician = value; }
        }

        #endregion

        #region PerformedByUser

        private Employee m_performedByUser;
        public Employee PerformedByUser
        {
            get { return m_performedByUser; }
            set { m_performedByUser = value; }
        }

        #endregion        

        #region FindPackageToBeExported

        public static ServmanExportPackage FindPackageToBeExported(Task task)
        {
            task = Task.FindByPrimaryKey(task.ID);
            Project project = Project.FindByPrimaryKey(task.ProjectId);
            Visit visit;

            try
            {
                if (task.TaskType == TaskTypeEnum.Deflood)
                {
                    if (task.TaskStatus == TaskStatusEnum.InProcess)
                    {
                        List<WorkTransactionTask> inProcessTransactions 
                            = WorkTransactionTask.FindBy(task, WorkTransactionTaskActionEnum.InProcess);

                        WorkTransaction workTransaction = WorkTransaction.FindByPrimaryKey(inProcessTransactions[0].WorkTransactionId);
                        visit = Visit.FindByPrimaryKey(workTransaction.VisitId.Value);

                    } else
                        visit = Visit.FindByTaskLastFirst(task, true);
                }                    
                else
                    visit = Visit.FindByTaskLastFirst(task, true);
            }
            catch (DataNotFoundException)
            {
                //In case if fisit was failed w/o work

                visit = new Visit(0,
                    (int)VisitStatusEnum.Pending,
                    DateTime.Now,
                    task.LastFailDate.Value,
                    0, null, null, 
                    project.CustomerId,
                    project.ServiceAddressId,
                    string.Empty,
                    null, null, null, false, false, false, decimal.Zero, null, false);
            }

            Customer customer = Customer.FindByPrimaryKey(visit.CustomerId.Value);
            Address customerAddress = Address.FindByPrimaryKey(customer.AddressId.Value);

            Address additionalAddress = null;
            if (visit.ServiceAddressId.Value != customerAddress.ID)
                additionalAddress = Address.FindByPrimaryKey(visit.ServiceAddressId.Value);

            WorkDetail workDetail = null;
            Work work = null;

            Employee technician = null;
            Van van = null;
            Employee performedByUser = null;

            try
            {
                workDetail = WorkDetail.FindByVisit(visit);
                work = Work.FindByPrimaryKey(workDetail.WorkId);
                technician = Employee.FindByPrimaryKey(work.TechnicianEmployeeId);

                if (work.VanId.HasValue)
                    van = Van.FindByPrimaryKey(work.VanId.Value);

                try
                {
                    WorkTransaction completeTransaction = WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitCompleted);
                    performedByUser = Employee.FindByPrimaryKey(completeTransaction.EmployeeId);
                }
                catch (DataNotFoundException) { }

                try
                {
                    WorkTransaction arriveTransaction = WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitArrived);
                    if (performedByUser == null)
                        performedByUser = Employee.FindByPrimaryKey(arriveTransaction.EmployeeId);
                }
                catch (DataNotFoundException) { }

                try
                {
                    WorkTransaction dispatchTransaction = WorkTransaction.FindBy(visit.ID, WorkTransactionTypeEnum.VisitDispatched);
                    if (performedByUser == null)
                        performedByUser = Employee.FindByPrimaryKey(dispatchTransaction.EmployeeId);
                }
                catch (DataNotFoundException) { }

            }
            catch (DataNotFoundException) { }

            List<Item> items = Item.FindByTask(task);

            Task parentDefloodTask = null;
            if (task.TaskType == TaskTypeEnum.Monitoring)
                parentDefloodTask = Task.FindByPrimaryKey(task.ParentTaskId.Value);

            DefloodDetail defloodDetail = null;
            if (task.TaskType == TaskTypeEnum.Deflood)
                defloodDetail = DefloodDetail.FindByPrimaryKey(task.ID);

            Employee advertisingTechnician = null;
            if (project.AdvertisingTechnicianId != null)
                advertisingTechnician = Employee.FindByPrimaryKey(project.AdvertisingTechnicianId.Value);

            return new ServmanExportPackage(customer, customerAddress, additionalAddress, task,
                project, visit, work, workDetail, items, parentDefloodTask,  defloodDetail, 
                technician, van, advertisingTechnician, performedByUser);
            
        }

        #endregion
    }
}
