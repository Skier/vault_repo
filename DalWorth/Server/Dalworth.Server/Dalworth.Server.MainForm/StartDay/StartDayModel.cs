using System;
using System.Collections.Generic;
using System.ComponentModel;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.StartDay
{
    public class StartDayModel : IModel
    {
        #region VanEquipment

        //Key - EquipmentTypeId, Value - quantity
        private Dictionary<int, int> m_vanEquipment;
        public Dictionary<int, int> VanEquipment
        {
            get { return m_vanEquipment; }
            
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

        #region Van

        private Van m_van;
        public Van Van
        {
            get { return m_van; }
        }

        #endregion

        #region WorkItems

        private BindingList<WorkItemPackage> m_workItems;
        public BindingList<WorkItemPackage> WorkItems
        {
            get { return m_workItems; }
        }

        #endregion

        #region EquipmentSummaries

        private BindingList<EquipmentSummary> m_equipmentSummaries;
        internal BindingList<EquipmentSummary> EquipmentSummaries
        {
            get { return m_equipmentSummaries; }
        }

        #endregion


        #region Init

        public void Init()
        {
            List<EquipmentType> equipmentTypes = EquipmentType.Find();

            m_technician = Employee.FindByPrimaryKey(m_work.TechnicianEmployeeId);
            m_van = Van.FindByPrimaryKey(m_work.VanId.Value);
            m_workItems = new BindingList<WorkItemPackage>(WorkItemPackage.FindBy(m_work));

            List<WorkEquipment> workEquipments = WorkEquipment.FindBy(m_work);
            m_equipmentSummaries = new BindingList<EquipmentSummary>();
            
            foreach (EquipmentType equipmentType in equipmentTypes)
            {
                WorkEquipment workEquipment = new WorkEquipment();
                workEquipment.EquipmentType = equipmentType;
                workEquipment.Quantity = 0;
                m_equipmentSummaries.Add(new EquipmentSummary(workEquipment, 0));

                foreach (WorkEquipment workEquipmentReal in workEquipments)
                {
                    if (workEquipment.EquipmentType == workEquipmentReal.EquipmentType)
                        workEquipment.Quantity = workEquipmentReal.Quantity;
                }                   
            }

            m_vanEquipment = new Dictionary<int, int>();
            foreach (EquipmentType equipmentType in equipmentTypes)
                m_vanEquipment.Add(equipmentType.ID, 0);

            //Populate current equipment numbers
            try
            {                
                WorkTransaction workTransaction 
                    = WorkTransaction.FindBy(m_work, WorkTransactionTypeEnum.StartDayDone);
                EquipmentTransaction equipmentTransaction
                    = EquipmentTransaction.FindByWorkTransaction(workTransaction);
                List<EquipmentTransactionDetail> existingEquipmentDetails 
                    = EquipmentTransactionDetail.FindByTransaction(equipmentTransaction);

                foreach (EquipmentTransactionDetail detail in existingEquipmentDetails)
                {
                    if (detail.VanId != null)
                        m_vanEquipment[detail.EquipmentTypeId] = detail.Quantity;
                }
                UpdateSummaries(m_vanEquipment);
            }
            catch (DataNotFoundException){}                        
        }

        #endregion        

        #region IsVisitsExistsBefore

        public bool IsVisitsExistsBefore(DateTime date)
        {
            List<WorkDetail> workDetails = WorkDetail.FindBy(m_work);
            foreach (WorkDetail workDetail in workDetails)
            {
                if (workDetail.TimeBegin < date)
                    return true;
            }
            return false;
        }

        #endregion        

        #region UpdateSummaries

        public void UpdateSummaries(Dictionary<int, int> vanEquipment)
        {
            foreach (EquipmentSummary summary in m_equipmentSummaries)
                summary.VanQuantity = vanEquipment[summary.EquipmentTypeId];

            m_equipmentSummaries.ResetBindings();
        }

        #endregion

        #region GetFirstVisitStartTime

        private DateTime? GetFirstVisitStartTime()
        {
            List<WorkDetail> workDetails = WorkDetail.FindBy(m_work);
            if (workDetails.Count == 0)
                return null;

            DateTime result = workDetails[0].TimeBegin;

            foreach (WorkDetail detail in workDetails)
            {
                if (detail.TimeBegin < result)
                    result = detail.TimeBegin;
            }

            return result;
        }

        #endregion

        #region GetDefaultStartDayTime

        public DateTime GetDefaultStartDayTime()
        {
            DateTime? firstVisitTime = GetFirstVisitStartTime();

            if (m_work.StartDate.Value.Date == DateTime.Now.Date)
            {
                if (firstVisitTime.HasValue && firstVisitTime.Value < DateTime.Now)
                    return firstVisitTime.Value;
                return DateTime.Now;
            } 

            if (firstVisitTime.HasValue)
                return firstVisitTime.Value;

            return new DateTime(
                m_work.StartDate.Value.Year,
                m_work.StartDate.Value.Month,
                m_work.StartDate.Value.Day,
                0, 0, 0);
        }

        #endregion        
    }

    internal class EquipmentSummary
    {
        #region EquipmentTypeId

        public int EquipmentTypeId
        {
            get { return m_workEquipment.EquipmentTypeId; }
        }

        #endregion

        #region EquipmentType

        public EquipmentType EquipmentType
        {
            get { return m_workEquipment.EquipmentType; }
        }

        #endregion

        #region EquipmentTypeName

        public string EquipmentTypeName
        {
            get { return m_workEquipment.EquipmentType.Type; }
        }

        #endregion

        #region SuggestedQuantity

        public int SuggestedQuantity
        {
            get { return m_workEquipment.Quantity ?? 0; }
        }

        #endregion

        #region VanQuantity

        public int VanQuantity
        {
            get { return m_vanQuantity; }
            set { m_vanQuantity = value; }
        }

        #endregion

        #region DueQuantity

        public int DueQuantity
        {
            get 
            { 
                if (SuggestedQuantity - VanQuantity < 0)
                    return 0;
                return SuggestedQuantity - VanQuantity;                
            }
        }

        #endregion

        #region Constructor

        private readonly WorkEquipment m_workEquipment;
        private int m_vanQuantity;

        public EquipmentSummary(WorkEquipment workEquipment, int vanQuantity)
        {
            m_workEquipment = workEquipment;
            m_vanQuantity = vanQuantity;
        }

        #endregion
    }
}
