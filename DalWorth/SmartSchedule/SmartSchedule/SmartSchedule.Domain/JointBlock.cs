using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartSchedule.Domain
{
    public class JointBlock
    {
        #region Constructor

        public JointBlock(Visit newVisit, Visit visit1)
        {
            m_newVisit = newVisit;
            m_visit1 = visit1;
        }

        public JointBlock(JointBlock jointBlock1, JointBlock jointBlock2)
        {
            m_jointBlock1 = jointBlock1;
            m_jointBlock2 = jointBlock2;

            m_newVisit = jointBlock1.NewVisit;
            m_visit1 = jointBlock1.Visit1;
            m_visit2 = jointBlock2.Visit1;
        }

        #endregion

        private JointBlock m_jointBlock1;
        private JointBlock m_jointBlock2;

        #region NewVisit

        private Visit m_newVisit;
        public Visit NewVisit
        {
            get { return m_newVisit; }
        }

        #endregion

        #region Visit1

        private Visit m_visit1;        
        public Visit Visit1
        {
            get { return m_visit1; }
        }

        #endregion

        #region Visit2

        private Visit m_visit2;
        public Visit Visit2
        {
            get { return m_visit2; }
        }

        #endregion

        #region CanJoinAtTopVisit1

        private Dictionary<Technician, bool> m_canJoinAtTopVisit1Map;
        private bool CanJoinAtTopVisit1(Technician technician)
        {
            if (m_jointBlock1 != null)
                return m_jointBlock1.CanJoinAtTopVisit1(technician);

            if (m_canJoinAtTopVisit1Map == null || !m_canJoinAtTopVisit1Map.ContainsKey(technician))
            {
                if (m_canJoinAtTopVisit1Map == null)
                    m_canJoinAtTopVisit1Map = new Dictionary<Technician, bool>();

                if (m_visit1.GetMinTimeEnd(technician) <= m_newVisit.GetMaxTimeStart(technician)
                    && m_visit1.GetMaxTimeEnd(technician) >= m_newVisit.GetMinTimeStart(technician))
                {
                    m_canJoinAtTopVisit1Map.Add(technician, true);
                } else
                    m_canJoinAtTopVisit1Map.Add(technician, false);
            }

            return m_canJoinAtTopVisit1Map[technician];
        }

        #endregion

        #region CanJoinAtBottomVisit1

        private Dictionary<Technician, bool> m_canJoinAtBottomVisit1Map;
        private bool CanJoinAtBottomVisit1(Technician technician)
        {
            if (m_jointBlock1 != null)
                return m_jointBlock1.CanJoinAtBottomVisit1(technician);

            if (m_canJoinAtBottomVisit1Map == null || !m_canJoinAtBottomVisit1Map.ContainsKey(technician))
            {
                if (m_canJoinAtBottomVisit1Map == null)
                    m_canJoinAtBottomVisit1Map = new Dictionary<Technician, bool>();

                if (m_newVisit.GetMinTimeEnd(technician) <= m_visit1.GetMaxTimeStart(technician)
                    && m_newVisit.GetMaxTimeEnd(technician) >= m_visit1.GetMinTimeStart(technician))
                {
                    m_canJoinAtBottomVisit1Map.Add(technician, true);
                } else
                    m_canJoinAtBottomVisit1Map.Add(technician, false);
            }

            return m_canJoinAtBottomVisit1Map[technician];
        }

        #endregion        

        #region CanProduceGoodJoint

        private bool CanProduceGoodJoint(Technician technician)
        {
            if (m_jointBlock1 != null && m_jointBlock2 != null)
            {
                if (m_jointBlock1.CanJoinAtTopVisit1(technician) && m_jointBlock2.CanJoinAtBottomVisit1(technician)
                    && CanHaveTwoJoints(Visit1, NewVisit, Visit2, technician))
                {
                    return true;
                }

                if (m_jointBlock1.CanJoinAtBottomVisit1(technician) && m_jointBlock2.CanJoinAtTopVisit1(technician)
                    && CanHaveTwoJoints(Visit2, NewVisit, Visit1, technician))
                {
                    return true;
                }

                return false;
            }

            return CanJoinAtTopVisit1(technician) || CanJoinAtBottomVisit1(technician);            
        }

        #endregion        

        #region AllowedTechnicians

        private List<Technician> m_allowedTechnicians;
        //Returns technicinas where this block can be placed with new visit in primary area and
        //good joint possibility is checked on every technician
        public List<Technician> AllowedTechnicians
        {
            get
            {
                if (m_allowedTechnicians == null)
                    m_allowedTechnicians = GetAllowedTechnicians(false);

                return m_allowedTechnicians;
            }
            set { m_allowedTechnicians = value; }
        }

        #endregion

        #region AllowedTechniciansNewVisitInSecondary

        private List<Technician> m_allowedTechniciansNewVisitInSecondary;        
        public List<Technician> AllowedTechniciansNewVisitInSecondary
        {
            get
            {
                if (m_allowedTechniciansNewVisitInSecondary == null)
                    m_allowedTechniciansNewVisitInSecondary = GetAllowedTechnicians(true);

                return m_allowedTechniciansNewVisitInSecondary;
            }
        }

        #endregion        
        
        #region AllowedTechniciansExistingVisitChangesArea

        private List<Technician> m_allowedTechniciansExistingVisitChangesArea;
        public List<Technician> AllowedTechniciansExistingVisitChangesArea
        {
            get
            {
                if (m_allowedTechniciansExistingVisitChangesArea == null)
                    m_allowedTechniciansExistingVisitChangesArea = GetAllowedTechniciansExistingChangesArea();

                return m_allowedTechniciansExistingVisitChangesArea;
            }
            set { m_allowedTechniciansExistingVisitChangesArea = value; }
        }

        #endregion


        #region GetIntersectingTechnicians

        private List<Technician> GetIntersectingTechnicians(List<Technician> list1, List<Technician> list2)
        {
            List<Technician> result = new List<Technician>();

            foreach (Technician technician in list1)
            {
                if (list2.Contains(technician))
                    result.Add(technician);
            }

            return result;
        }

        #endregion

        #region GetAllowedTechnicians

        private List<Technician> GetAllowedTechnicians(bool placeNewVisitInSecondaryArea)
        {
            bool is2JointsBlock = m_jointBlock1 != null && m_jointBlock2 != null;

            List<Technician> possibleTechnicians;

            if (is2JointsBlock)
            {
                if (placeNewVisitInSecondaryArea)
                {
                    possibleTechnicians = GetIntersectingTechnicians(
                        m_jointBlock1.AllowedTechniciansNewVisitInSecondary, m_jointBlock2.AllowedTechniciansNewVisitInSecondary);
                } else
                {
                    possibleTechnicians = GetIntersectingTechnicians(
                        m_jointBlock1.AllowedTechnicians, m_jointBlock2.AllowedTechnicians);                                        
                }
            } else
            {
                List<Technician> existingVisitAllowedTechnicians = Visit1.PrimaryTechnicians;
                existingVisitAllowedTechnicians.AddRange(Visit1.SecondaryTechnicians);

                existingVisitAllowedTechnicians = existingVisitAllowedTechnicians
                    .OrderBy(technician => technician.Distance(Visit1)).ToList();

                possibleTechnicians = GetIntersectingTechnicians(
                    placeNewVisitInSecondaryArea ? NewVisit.SecondaryTechnicians: NewVisit.PrimaryTechnicians,
                    existingVisitAllowedTechnicians);                
            }

            List<Technician> result = new List<Technician>();
            foreach (Technician technician in possibleTechnicians)
            {
                if (CanProduceGoodJoint(technician))
                    result.Add(technician);
            }

            return result;
        }

        #endregion

        #region GetAllowedTechniciansExistingChangesArea

        private List<Technician> GetAllowedTechniciansExistingChangesArea()
        {
            List<Technician> existingVisitAllowedTechnicians;
            if (Visit1.IsInPrimaryArea)
                existingVisitAllowedTechnicians = Visit1.SecondaryTechnicians;
            else
                existingVisitAllowedTechnicians = Visit1.PrimaryTechnicians;

            return GetIntersectingTechnicians(NewVisit.PrimaryTechnicians,
                existingVisitAllowedTechnicians);
        }

        #endregion

        #region CanHaveTwoJoints

        private bool CanHaveTwoJoints(Visit topVisit, Visit middleVisit, Visit bottomVisit, Technician technician)
        {
            DateTime? topEnd = topVisit.GetMinTimeEnd(technician);
            if (topEnd == null)
                return false;

            DateTime? middleMinStart = middleVisit.GetMinTimeStart(technician);
            if (middleMinStart == null)
                return false;

            DateTime middleStart;
            if (middleMinStart > topEnd)
                middleStart = middleMinStart.Value;
            else
                middleStart = topEnd.Value;
            DateTime middleEnd = middleStart.AddMinutes(
                middleVisit.GetDurationOnTechnician(technician));            

            if (bottomVisit.GetMaxTimeStart(technician) < middleEnd)
                return false;

            DateTime bottomStart;
            DateTime bottomMinStart = bottomVisit.GetMinTimeStart(technician).Value;
            if (bottomMinStart > middleEnd)
                bottomStart = bottomMinStart;
            else
                bottomStart = middleEnd;

            if (bottomStart != middleEnd)
            {
                if (middleVisit.GetMaxTimeEnd(technician) >= bottomStart)
                {
                    middleEnd = bottomStart;
                    middleStart = middleEnd.AddMinutes(
                        -middleVisit.GetDurationOnTechnician(technician));
                }
                else
                    return false;
            }

            if (middleStart != topEnd)
            {
                if (topVisit.GetMaxTimeEnd(technician) >= middleStart)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        #endregion
    }
}
