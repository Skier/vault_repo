using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;
using Configuration=Dalworth.Server.SDK.Configuration;

namespace Dalworth.Server.Domain
{
    public class WorkPackage
    {
        #region Fields

        #region Work

        private Work m_work;
        public Work Work
        {
            get { return m_work; }
            set { m_work = value; }
        }

        #endregion

        #region WorkEquipments

        private List<WorkEquipment> m_workEquipments;
        public List<WorkEquipment> WorkEquipments
        {
            get { return m_workEquipments; }
            set { m_workEquipments = value; }
        }

        #endregion

        #region WorkDetails

        private List<WorkDetail> m_workDetails;
        public List<WorkDetail> WorkDetails
        {
            get { return m_workDetails; }
            set { m_workDetails = value; }
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

        #region Technician

        private Employee m_technician;
        public Employee Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
        }

        #endregion

        #region Dispatch

        private Employee m_dispatch;
        public Employee Dispatch
        {
            get { return m_dispatch; }
            set { m_dispatch = value; }
        }

        #endregion

        #endregion

        #region GetWorkPackages

        public static List<WorkPackage> GetWorkPackages(DateTime date)
        {
            List<WorkPackage> packages = new List<WorkPackage>();
            List<Work> works = Work.FindBy(date);

            foreach (Work work in works)
            {
                WorkPackage package = new WorkPackage();
                package.Work = work;
                package.WorkEquipments = WorkEquipment.FindBy(work);
                package.WorkDetails = WorkDetail.FindBy(work);
                if (work.VanId == null)
                    package.Van = null;
                else
                    package.Van = Van.FindByPrimaryKey(work.VanId.Value);
                package.Technician = Employee.FindByPrimaryKey(work.TechnicianEmployeeId);
                package.Dispatch = Employee.FindByPrimaryKey(work.DispatchEmployeeId);

                packages.Add(package);
            }

            return packages;
        }

        #endregion        

        #region CreateWork

        public static void CreateWork(WorkPackage workPackage)
        {   
            //Request validation
            if (Work.FindWorkByTechAndDate(workPackage.Technician.ID, workPackage.Work.StartDate.Value) != null)
            {
                Employee employee = Employee.FindByPrimaryKey(workPackage.Technician.ID);
                throw new Exception(string.Format("Cannot create work. Technician {0} has already work assigned for {1}", 
                    employee.DisplayName, workPackage.Work.StartDate.Value.ToShortDateString()));                
            }

            if (workPackage.Van != null 
                && Work.FindWorkByVanAndDate(workPackage.Van.ID, workPackage.Work.StartDate.Value) != null)
            {
                Van van = Van.FindByPrimaryKey(workPackage.Van.ID);
                throw new Exception(string.Format("Cannot create work. Van {0} is already assigned for work on {1}", 
                    van.LicensePlateNumber, workPackage.Work.StartDate.Value.ToShortDateString()));
            }

            foreach (WorkDetail detail in workPackage.WorkDetails)
            {
                Visit visit = Visit.FindByPrimaryKey(detail.VisitId);
                if (visit.VisitStatus != VisitStatusEnum.Pending)
                    throw new Exception(string.Format("Cannot assign visit {0}. Visit status has been changed while you were creating work.",
                        visit.ID));
            }                
            //Request validation

            workPackage.Work.DispatchEmployeeId = workPackage.Dispatch.ID;
            workPackage.Work.TechnicianEmployeeId = workPackage.Technician.ID;
            if (workPackage.Van != null)
                workPackage.Work.VanId = workPackage.Van.ID;
            workPackage.Work.CreateDate = DateTime.Now;
            Work.Insert(workPackage.Work);

            foreach (WorkEquipment equipment in workPackage.WorkEquipments)
            {
                equipment.WorkId = workPackage.Work.ID;
            }
            WorkEquipment.Insert(workPackage.WorkEquipments);

            foreach (WorkDetail detail in workPackage.WorkDetails)
            {
                Visit visit = Visit.FindByPrimaryKey(detail.VisitId);
                visit.VisitStatus = VisitStatusEnum.Assigned;
                Visit.Update(visit);
                detail.WorkId = workPackage.Work.ID;
            }

            WorkDetail.InsertAndLog(workPackage.WorkDetails);
        }

        #endregion

        #region MoveWorkToReadyForStartDayDefault

        public static void MoveWorkToReadyForStartDayDefault(Work work)
        {
            WorkPackage workPackage = new WorkPackage();
            workPackage.Work = work;

            Employee technician = Employee.FindByPrimaryKey(work.TechnicianEmployeeId);

            if (technician.DefaultVanId.HasValue)
            {
                Van defaultVan = new Van(technician.DefaultVanId.Value);
                if (Van.IsVanAvailable(defaultVan, work.StartDate.Value.Date))
                    workPackage.Van = Van.FindByPrimaryKey(defaultVan.ID);
            }
            
            if (workPackage.Van == null)
            {
                List<Van> availableVans = Van.FindAvailableVans(work.StartDate.Value.Date);
                if (availableVans.Count > 0)
                    workPackage.Van = availableVans[0];
                else
                    throw new DalworthException("No vans available");
            }

            workPackage.WorkEquipments = new List<WorkEquipment>();
            MoveWorkToReadyForStartDay(workPackage);            
        }

        #endregion

        #region MoveWorkToReadyForStartDay

        public static void MoveWorkToReadyForStartDay(WorkPackage workPackage)
        {
            int? previousVan = null;
        
            if (workPackage.Van != null)
            {
                previousVan = workPackage.Work.VanId;
                workPackage.Work.VanId = workPackage.Van.ID;
            }
                
            if (workPackage.Work.WorkStatus == WorkStatusEnum.Pending)
            {
                workPackage.Work.CreateDate = DateTime.Now;
                workPackage.Work.WorkStatus = WorkStatusEnum.ReadyForStartDay;                
            }

            if (workPackage.Work.ID == 0)
                Work.Insert(workPackage.Work);
            else
                Work.Update(workPackage.Work);

            foreach (WorkEquipment equipment in workPackage.WorkEquipments)
            {
                if (equipment.ID > 0)
                    WorkEquipment.Delete(equipment);

                if (equipment.Quantity > 0)
                {
                    equipment.WorkId = workPackage.Work.ID;
                    WorkEquipment.Insert(equipment);
                }
            }            
        }

        #endregion

        #region UndoMoveWorkToReadyForStartDay

        public static void UndoMoveWorkToReadyForStartDay(Work work)
        {
            WorkEquipment.DeleteByWork(work); 

            work.WorkStatus = WorkStatusEnum.Pending;
            work.VanId = null;
            work.StartMessage = string.Empty;
            work.EndMessage = string.Empty;
            work.EquipmentNotes = string.Empty;
            Work.Update(work);

            if (WorkDetail.FindBy(work).Count == 0)
                Work.Delete(work);
        }

        #endregion
    }
}
