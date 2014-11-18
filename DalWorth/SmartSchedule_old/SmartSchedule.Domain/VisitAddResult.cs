using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchedule.Domain
{
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
        public bool IsAddAllowed
        {
            get { return m_isAddAllowed; }
            set { m_isAddAllowed = value; }
        }

        #endregion        

        #region NewJointsCount

        private int m_newJointsCount;
        public int NewJointsCount
        {
            get { return m_newJointsCount; }
            set { m_newJointsCount = value; }
        }

        #endregion

        #region VisitToAddTimeStart

        private DateTime m_visitToAddTimeStart;
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
        public Technician Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
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
    }
}
