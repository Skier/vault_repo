using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Controls;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.CreateWork
{
    public class CreateWorkModel : IModel
    {
        #region CurrentDispatch

        private Employee m_currentDispatch;
        public Employee CurrentDispatch
        {
            get { return m_currentDispatch; }
            set { m_currentDispatch = value; }
        }

        #endregion

        #region AvailableVans

        private List<Van> m_availableVans;
        internal List<Van> AvailableVans
        {
            get { return m_availableVans; }
        }

        #endregion

        #region WorkEquipments

        private List<WorkEquipment> m_workEquipments;
        public List<WorkEquipment> WorkEquipments
        {
            get { return m_workEquipments; }
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

        #region Technician

        private Employee m_technician;
        public Employee Technician
        {
            get { return m_technician; }
        }

        #endregion

        #region Init

        public void Init()
        {            
            m_availableVans = Van.FindAvailableVans(Work.StartDate.Value);            
            m_technician = Employee.FindByPrimaryKey(Work.TechnicianEmployeeId);

            if (m_work.VanId != null)
            {
                m_availableVans.Insert(0, Van.FindByPrimaryKey(m_work.VanId.Value));
                m_workEquipments = WorkEquipment.FindBy(m_work);

                List<EquipmentType> equipmentTypes = EquipmentType.Find();
                Dictionary<int, EquipmentType> equipmentTypesMap = new Dictionary<int, EquipmentType>();
                foreach (EquipmentType type in equipmentTypes)
                    equipmentTypesMap.Add(type.ID, type);

                foreach (WorkEquipment workEquipment in m_workEquipments)
                {                    
                    if (equipmentTypesMap.ContainsKey(workEquipment.EquipmentTypeId))
                        equipmentTypesMap.Remove(workEquipment.EquipmentTypeId);
                }

                foreach (EquipmentType equipmentType in equipmentTypesMap.Values)
                    m_workEquipments.Add(new WorkEquipment(0, 0, equipmentType.ID, 0));

            } else
            {
                InitWorkEquipment();
            }                        
        }

        #endregion

        #region InitWorkEquipment

        public void InitWorkEquipment()
        {
            List<int> visitIds = new List<int>();
            List<WorkDetail> workDetails = WorkDetail.FindBy(Work);
            foreach (WorkDetail detail in workDetails)
                visitIds.Add(detail.VisitId);
            m_workEquipments = WorkEquipment.GetEstimate(visitIds);
        }

        #endregion

        #region CreateWork

        public void CreateWork(WorkPackage workPackage)
        {
            WorkPackage.MoveWorkToReadyForStartDay(workPackage);
            DashboardState.MakeDashboardDirty(workPackage.Dispatch.ID);            
        }

        #endregion       
    }
}
