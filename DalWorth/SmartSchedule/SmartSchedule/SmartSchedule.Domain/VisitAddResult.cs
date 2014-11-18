using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartSchedule.Domain
{
    [DataContract]
    public class VisitAddResult
    {
        #region VisitAddResult

        public VisitAddResult(bool isAddAllowed, DateTime visitToAddTimeStart)
        {
            m_isAddAllowed = isAddAllowed;
            m_visitToAddTimeStart = visitToAddTimeStart;
            m_changeInstructions = new List<VisitChangeInstruction>();
        }

        #endregion


        #region IsAddAllowed

        private bool m_isAddAllowed;
        [DataMember]
        public bool IsAddAllowed
        {
            get { return m_isAddAllowed; }
            set { m_isAddAllowed = value; }
        }

        #endregion        

        #region SecondaryArea

        private bool m_secondaryArea;
        [DataMember]
        public bool SecondaryArea
        {
            get { return m_secondaryArea; }
            set { m_secondaryArea = value; }
        }

        #endregion

        #region VisitToAddTimeStart

        private DateTime m_visitToAddTimeStart;
        [DataMember]
        public DateTime VisitToAddTimeStart
        {
            get { return m_visitToAddTimeStart; }
            set { m_visitToAddTimeStart = value; }
        }

        #endregion

        #region ChangeInstructions

        private List<VisitChangeInstruction> m_changeInstructions;
        public List<VisitChangeInstruction> ChangeInstructions
        {
            get { return m_changeInstructions; }
            set { m_changeInstructions = value; }
        }

        #endregion

        #region Technician

        private Technician m_technician;
        [DataMember]
        public Technician Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
        }

        #endregion


        #region OtherFrame

        private TimeFrame m_otherFrame;
        [DataMember]
        public TimeFrame OtherFrame
        {
            get { return m_otherFrame; }
            set { m_otherFrame = value; }
        }

        #endregion        

        #region IsBetterThan

        public bool IsBetterThan(VisitAddResult other)
        {
            if (!IsAddAllowed && !other.IsAddAllowed)
                return false;
            if (IsAddAllowed && !other.IsAddAllowed)
                return true;
            if (!IsAddAllowed && other.IsAddAllowed)
                return false;

            if (SecondaryArea == other.SecondaryArea)
                return CostChange < other.CostChange;

            if (!SecondaryArea && other.SecondaryArea)
                return true;
            return false;
        }

        #endregion

        #region Urgency

        private double m_urgency;
        public double Urgency
        {
            get { return m_urgency; }
            set { m_urgency = value; }
        }

        #endregion

        #region CostChange

        private double m_costChange;
        [DataMember]
        public double CostChange
        {
            get { return m_costChange; }
            set { m_costChange = value; }
        }

        #endregion



        #region NewTimeFrameText

        public string NewTimeFrameText
        {
            get
            {
                return OtherFrame.Text;
            }
        }

        #endregion

        #region SecondaryAreaText

        public string SecondaryAreaText
        {
            get
            {
                if (m_secondaryArea)
                    return "Yes";
                return string.Empty;
            }
        }

        #endregion       
    }

    public class VisitChangeInstruction
    {
        #region VisitChangeInstruction

        public VisitChangeInstruction(int visitIndex, DateTime timeStart)
        {
            m_visitIndex = visitIndex;
            m_timeStart = timeStart;
        }

        #endregion

        #region VisitIndex

        private int m_visitIndex;
        public int VisitIndex
        {
            get { return m_visitIndex; }
            set { m_visitIndex = value; }
        }

        #endregion

        #region TimeStart

        private DateTime m_timeStart;
        public DateTime TimeStart
        {
            get { return m_timeStart; }
            set { m_timeStart = value; }
        }

        #endregion

        #region NewTechnician

        private Technician m_newTechnician;
        public Technician NewTechnician
        {
            get { return m_newTechnician; }
            set { m_newTechnician = value; }
        }

        #endregion

        #region VisitSavedOldState

        private Visit m_visitSavedOldState;
        public Visit VisitSavedOldState
        {
            get { return m_visitSavedOldState; }
            set { m_visitSavedOldState = value; }
        }

        #endregion
    }

    public class InputParameters
    {
        #region Constructors

        public InputParameters(Visit visitToInsert)
        {
            m_visitToInsert = visitToInsert;
        }

        public InputParameters(Visit visitToInsert, bool provideInstructions)
            : this(visitToInsert)
        {
            m_provideInstructions = provideInstructions;
        }

        public InputParameters(Visit visitToInsert, TechnicianDefault technicianToPlaceOnOnly, bool provideInstructions)
            : this(visitToInsert, provideInstructions)
        {
            m_technicianDefaultToPlaceOnOnly = technicianToPlaceOnOnly;
        }

        #endregion


        #region VisitToInsert

        private Visit m_visitToInsert;
        public Visit VisitToInsert
        {
            get { return m_visitToInsert; }
        }

        #endregion

        #region ProvideInstructions

        private bool m_provideInstructions;
        public bool ProvideInstructions
        {
            get { return m_provideInstructions; }
            set { m_provideInstructions = value; }
        }

        #endregion

        #region IgnoreWorkingHours

        private bool m_ignoreWorkingHours;
        public bool IgnoreWorkingHours
        {
            get { return m_ignoreWorkingHours; }
            set { m_ignoreWorkingHours = value; }
        }

        #endregion

        #region DisableSecondLevelReinsert

        private bool m_disableSecondLevelReinsert;
        public bool DisableSecondLevelReinsert
        {
            get { return m_disableSecondLevelReinsert; }
            set { m_disableSecondLevelReinsert = value; }
        }

        #endregion        

        #region TechnicianToPlaceOnOnly

        //Works even if technician is off
        private TechnicianDefault m_technicianDefaultToPlaceOnOnly;
        public TechnicianDefault TechnicianDefaultToPlaceOnOnly
        {
            get { return m_technicianDefaultToPlaceOnOnly; }
        }

        public Technician TechnicianToPlaceOnOnly
        {
            get
            {
                if (m_technicianDefaultToPlaceOnOnly == null)
                    return null;

                return Technician.GetTechnician(m_visitToInsert.ScheduleDate, 
                    m_technicianDefaultToPlaceOnOnly.ID);
            }
        }

        #endregion

        #region DontWriteToDb

        private bool m_dontWriteToDb;
        public bool DontWriteToDb
        {
            get { return m_dontWriteToDb; }
            set { m_dontWriteToDb = value; }
        }

        #endregion

        #region FindFirstPossibleResult

        private bool m_findFirstPossibleResult;
        public bool FindFirstPossibleResult
        {
            get { return m_findFirstPossibleResult; }
            set { m_findFirstPossibleResult = value; }
        }

        #endregion

        #region Extensions

        private List<WorkingHoursExtensionResult> m_extensions;
        public List<WorkingHoursExtensionResult> Extensions
        {
            get
            {
                if (m_extensions == null)
                    m_extensions = new List<WorkingHoursExtensionResult>();
                return m_extensions;
            }
        }

        #endregion
    }


    public class VisitInsertResult
    {
        public VisitInsertResult(bool isInsertSucceed, Visit insertionVisit)
        {
            IsInsertSucceed = isInsertSucceed;
            InsertionVisit = insertionVisit;
            ModifiedVisits = new List<Visit>();
        }

        public bool IsInsertSucceed { get; set; }
        public Visit InsertionVisit { get; set; }
        public List<Visit> ModifiedVisits { get; set; }
        public UserAction UserAction { get; set; }

        public void AddModifiedVisit(Visit modifiedVisit)
        {
            if (!ModifiedVisits.Contains(modifiedVisit))
                ModifiedVisits.Add(modifiedVisit);
        }
    }
}
