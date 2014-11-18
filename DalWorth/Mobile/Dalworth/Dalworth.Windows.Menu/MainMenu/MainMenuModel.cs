using System;
using System.Collections.Generic;
using Dalworth.Data;
using Dalworth.Domain;
using Dalworth.Domain.Package;
using Dalworth.Domain.Sync;
using Dalworth.Domain.SyncService;
using Dalworth.SDK;
using Dalworth.Windows.StartDay;
using Address=Dalworth.Domain.Address;
using Customer=Dalworth.Domain.Customer;
using Equipment=Dalworth.Domain.Equipment;
using Item=Dalworth.Domain.Item;
using Project=Dalworth.Domain.Project;
using Task=Dalworth.Domain.Task;
using TaskEquipmentCapture=Dalworth.Domain.TaskEquipmentCapture;
using TaskItemDelivery=Dalworth.Domain.TaskItemDelivery;
using TaskItemRequirement=Dalworth.Domain.TaskItemRequirement;
using Van=Dalworth.Domain.Van;
using Visit=Dalworth.Domain.Visit;
using Work=Dalworth.Domain.Work;
using WorkDetail=Dalworth.Domain.WorkDetail;
using WorkEquipment=Dalworth.Domain.WorkEquipment;
using WorkStatusEnum=Dalworth.Domain.WorkStatusEnum;
using WorkTransaction=Dalworth.Domain.WorkTransaction;
using WorkTransactionEquipment=Dalworth.Domain.WorkTransactionEquipment;
using WorkTransactionItem=Dalworth.Domain.WorkTransactionItem;
using WorkTransactionVanCheck=Dalworth.Domain.WorkTransactionVanCheck;

namespace Dalworth.Windows.Menu.MainMenu
{
    public class MainMenuModel
    {
        #region GetApplicationState

        public ApplicationPackage GetApplicationPackage()
        {
            ApplicationPackage package = ApplicationPackage.GetApplicationPackage();

            if (package.Application == null)
            {
                Application application = new Application(1);
                application.ApplicationState = ApplicationStateEnum.StartDay;
                Application.Insert(application);
                package.Application = application;
            }

            if (package.Work != null)
                Configuration.CurrentTechnicianId = package.Work.TechnicianEmployeeId;
            return package;
        }

        #endregion

        #region SynchronizeEmployees

        public void SynchronizeEmployees()
        {
            ClearDb();

            DalworthSyncService service = new DalworthSyncService();
            Domain.SyncService.Employee[] serverEmployees = service.GetEmployees(Configuration.ConnectionKey);

            List<Domain.Employee> localEmployees = new List<Domain.Employee>();
            foreach (Domain.SyncService.Employee serverEmployee in serverEmployees)
            {   
                Domain.Employee localEmployee = new Domain.Employee(
                    serverEmployee.ID,
                    serverEmployee.EmployeeTypeId,
                    serverEmployee.AddressId,
                    serverEmployee.FirstName,
                    serverEmployee.LastName,
                    serverEmployee.HireDate,
                    serverEmployee.Phone1,
                    serverEmployee.Phone2,
                    serverEmployee.Password
                    );
                
                localEmployees.Add(localEmployee);
            }
            
            Domain.Employee.Insert(localEmployees);

        }

        #endregion

        #region GetIncomingMessage

        public Message GetIncomingMessage()
        {            
            DalworthSyncService service = new DalworthSyncService();
            return service.GetIncomingMessage(Configuration.ConnectionKey, Configuration.CurrentTechnicianId);
        }

        #endregion

        #region NotifyMessageReceived

        public void NotifyMessageReceived(int messageId)
        {
            DalworthSyncService service = new DalworthSyncService();
            service.NotifyMessageReceived(Configuration.ConnectionKey, messageId);
        }

        #endregion

        #region GetVisit

        public VisitPackage GetVisit(int visitId)
        {
            DalworthSyncService service = new DalworthSyncService();
            return service.GetVisit(Configuration.ConnectionKey, visitId);
        }

        #endregion

        #region CompleteDay

        public void CompleteDay()
        {
            Application application = Application.Find()[0];
            application.ApplicationState = ApplicationStateEnum.StartDay;
            Application.Update(application);

            ApplicationPackage package = GetApplicationPackage();
            
            DalworthSyncService service = new DalworthSyncService();
            service.CompleteWork(Configuration.ConnectionKey, package.Work.ID);
            
        }

        #endregion

        #region ClearDb

        public void ClearDb()
        {
            WorkTransactionEquipment.Clear();
            WorkTransactionEtc.Clear();
            WorkTransactionGps.Clear();
            WorkTransactionItem.Clear();
            WorkTransactionVanCheck.Clear();
            WorkTransactionTaskEquipment.Clear();
            WorkTransactionTaskItem.Clear();
            WorkTransactionTask.Clear();
            WorkTransaction.Clear();

            WorkEquipment.Clear();            
            WorkDetail.Clear();
            Application.Clear();
            Work.Clear();

            VanDetail.Clear();
            Van.Clear();

            TaskEquipmentCapture.Clear();
            TaskEquipmentRequirement.Clear();
            TaskItemDelivery.Clear();
            TaskItemRequirement.Clear();
            Task.Clear();
            Visit.Clear();

            Item.Clear();

            Project.Clear();
            Customer.Clear();                        
            Equipment.Clear();            
            Domain.Employee.Clear();
            Address.Clear();
        }

        #endregion
    }
}
