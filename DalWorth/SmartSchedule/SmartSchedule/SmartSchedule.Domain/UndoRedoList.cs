using System;
using System.Collections.Generic;
using System.Text;
using SmartSchedule.Data;

namespace SmartSchedule.Domain
{
    public enum ActionEnum
    {
        VisitAdded,
        VisitModified,
        VisitDeleted
    }

    public class UndoRedoList
    {
        private const int HISTORY_DEPTH = 50;
        private List<List<VisitDump>> m_history;
        private int m_currentIndex;
        private BookingEngine m_bookingEngine;
        private bool m_isCommandSaving;
        private bool m_isUndoingRedoing;

        public delegate void UndoRedoAllowedChangedHandler(bool isUndoAllowed, bool isRedoAllowed);
        public event UndoRedoAllowedChangedHandler UndoRedoAllowedChanged;

        #region UndoRedoList

        public UndoRedoList(BookingEngine bookingEngine)
        {
            m_history = new List<List<VisitDump>>();
            m_bookingEngine = bookingEngine;
            m_currentIndex = -1;
        }

        #endregion

        #region IsUndoAllowed

        private bool IsUndoAllowed
        {
            get { return m_currentIndex >= 0; }
        }

        #endregion

        #region IsRedoAllowed

        private bool IsRedoAllowed
        {
            get { return m_currentIndex < m_history.Count - 1; }
        }

        #endregion

        #region BeginCommand

        public void BeginCommand()
        {
            if (m_isUndoingRedoing)
                return;

            m_isCommandSaving = true;

            if (m_currentIndex < m_history.Count - 1)
                m_history.RemoveRange(m_currentIndex + 1, m_history.Count - m_currentIndex - 1);

            if (m_history.Count >= HISTORY_DEPTH)
                m_history.RemoveAt(0);
            else
                m_currentIndex++;

            m_history.Add(new List<VisitDump>());
        }

        #endregion

        #region CommitCommand

        public void CommitCommand()
        {
            if (m_isUndoingRedoing)
                return;

            m_isCommandSaving = false;                
        
            if (UndoRedoAllowedChanged != null)
                UndoRedoAllowedChanged.Invoke(IsUndoAllowed, IsRedoAllowed);
        }

        #endregion

        #region SaveAction

        public void SaveAction(ActionEnum action, Visit visit, bool isBeforeChange)
        {
            if (m_isUndoingRedoing)
                return;

            if (!m_isCommandSaving)
                throw new Exception("Use BeginCommand before construction");

            VisitDump duplicateDump = null;
            foreach (VisitDump dump in m_history[m_currentIndex])
            {
                if (isBeforeChange && dump.OldState != null && dump.OldState.ID == visit.ID)
                {
                    duplicateDump = dump;
                    break;                    
                }

                if (!isBeforeChange && dump.NewState != null && dump.NewState.ID == visit.ID)
                {
                    duplicateDump = dump;
                    break;                    
                }
            }

            if (isBeforeChange)
            {
                if (duplicateDump == null)
                    m_history[m_currentIndex].Add(new VisitDump(action, (Visit)visit.Clone(), null));
            }                
            else
            {
                if (duplicateDump != null)
                {
                    duplicateDump.NewState = (Visit) visit.Clone();
                } else
                {
                    VisitDump existingDump = null;

                    foreach (VisitDump dump in m_history[m_currentIndex])
                    {
                        if (dump.OldState != null && dump.OldState.ID == visit.ID)
                        {
                            existingDump = dump;
                            break;
                        }
                    }

                    if (existingDump != null)
                        existingDump.NewState = (Visit)visit.Clone();
                    else
                        m_history[m_currentIndex].Add(new VisitDump(action, null, (Visit)visit.Clone()));    
                }
            }            
        }

        #endregion

        #region GetVisitIndex

        private int? GetVisitIndex(VisitDump dump)
        {
            foreach (Visit visit in m_bookingEngine.Visits)
            {
                if ((dump.OldState != null && visit.ID == dump.OldState.ID)
                    || (dump.NewState != null && visit.ID == dump.NewState.ID))
                {
                    return m_bookingEngine.Visits.IndexOf(visit);
                }
            }

            return null;
        }

        #endregion

        #region FindAllSavedVisits

        private List<Visit> FindAllSavedVisits(int visitId)
        {
            List<Visit> result = new List<Visit>();

            foreach (List<VisitDump> dumps in m_history)
            {
                foreach (VisitDump dump in dumps)
                {
                    if (dump.NewState != null && dump.NewState.ID == visitId)
                        result.Add(dump.NewState);

                    if (dump.OldState != null && dump.OldState.ID == visitId)
                        result.Add(dump.OldState);
                }
            }

            return result;
        }

        #endregion

        #region UpdateVisitIds

        private void UpdateVisitIds(List<Visit> visits, int newVisitId)
        {
            foreach (Visit visit in visits)
                visit.ID = newVisitId;
        }

        #endregion

        #region Undo

        public void Undo()
        {
//            m_isUndoingRedoing = true;
//
//            foreach (VisitDump dump in m_history[m_currentIndex])
//            {
//                int? visitIndex = GetVisitIndex(dump);
//
//                if (dump.Action == ActionEnum.VisitAdded)
//                {
//                    Visit.DeleteWithDetails(m_bookingEngine.Visits[visitIndex.Value]);
//                    m_bookingEngine.Visits.RemoveAt(visitIndex.Value);                    
//                }                    
//                else if (dump.Action == ActionEnum.VisitDeleted)
//                {
//                    List<Visit> historyToModify = FindAllSavedVisits(dump.OldState.ID);
//                    Visit.InsertWithDetails(dump.OldState);
//                    UpdateVisitIds(historyToModify, dump.OldState.ID);
//                    m_bookingEngine.Visits.Add(dump.OldState);
//                }                    
//                else if (dump.Action == ActionEnum.VisitModified)
//                {
//                    Visit.Update(dump.OldState);
//                    Visit visitToUpdate = m_bookingEngine.Visits[visitIndex.Value];
//
//                    if (visitToUpdate.TechnicianId != dump.OldState.TechnicianId)
//                        m_bookingEngine.VisitTechnicianChanged(visitToUpdate,
//                                                               visitToUpdate.TechnicianId.Value, dump.OldState.TechnicianId.Value);
//
//                    visitToUpdate.TechnicianId = dump.OldState.TechnicianId;
//                    visitToUpdate.TimeEnd = dump.OldState.TimeEnd;
//                    visitToUpdate.TimeStartDashboard = dump.OldState.TimeStart;
//                    m_bookingEngine.Visits.ResetItem(visitIndex.Value);
//                }
//            }            
//
//            m_currentIndex--;
//            m_isUndoingRedoing = false;
//
//            if (UndoRedoAllowedChanged != null)
//                UndoRedoAllowedChanged.Invoke(IsUndoAllowed, IsRedoAllowed);
        }

        #endregion

        #region Redo

        public void Redo()
        {
//            m_isUndoingRedoing = true;
//            m_currentIndex++;
//
//            foreach (VisitDump dump in m_history[m_currentIndex])
//            {
//                int? visitIndex = GetVisitIndex(dump);
//
//                if (dump.Action == ActionEnum.VisitAdded)
//                {
//                    List<Visit> historyToModify = FindAllSavedVisits(dump.NewState.ID);
//                    Visit.InsertWithDetails(dump.NewState);
//                    UpdateVisitIds(historyToModify, dump.NewState.ID);
//                    m_bookingEngine.Visits.Add(dump.NewState);
//                }                    
//                else if (dump.Action == ActionEnum.VisitDeleted)
//                {
//                    Visit.DeleteWithDetails(m_bookingEngine.Visits[visitIndex.Value]);
//                    m_bookingEngine.Visits.RemoveAt(visitIndex.Value);
//                }                    
//                else if (dump.Action == ActionEnum.VisitModified)
//                {
//                    Visit.Update(dump.NewState);
//                    Visit visitToUpdate = m_bookingEngine.Visits[visitIndex.Value];
//
//                    if (visitToUpdate.TechnicianId != dump.NewState.TechnicianId)
//                        m_bookingEngine.VisitTechnicianChanged(visitToUpdate, 
//                                                               visitToUpdate.TechnicianId.Value, dump.NewState.TechnicianId.Value);
//
//                    visitToUpdate.TechnicianId = dump.NewState.TechnicianId;                    
//                    visitToUpdate.TimeEnd = dump.NewState.TimeEnd;
//                    visitToUpdate.TimeStartDashboard = dump.NewState.TimeStart;
//                    m_bookingEngine.Visits.ResetItem(visitIndex.Value);
//                }
//            }
//           
//            m_isUndoingRedoing = false;
//
//            if (UndoRedoAllowedChanged != null)
//                UndoRedoAllowedChanged.Invoke(IsUndoAllowed, IsRedoAllowed);
        }

        #endregion

        #region Reset

        public void Reset()
        {
            m_history.Clear();
            m_currentIndex = -1;

            if (UndoRedoAllowedChanged != null)
                UndoRedoAllowedChanged.Invoke(IsUndoAllowed, IsRedoAllowed);
        }

        #endregion

    }

    public class VisitDump
    {
        #region VisitDump

        public VisitDump(ActionEnum action, Visit oldState, Visit newState)
        {
            m_action = action;
            m_oldState = oldState;
            m_newState = newState;
        }

        #endregion

        #region Action

        private ActionEnum m_action;
        public ActionEnum Action
        {
            get { return m_action; }
            set { m_action = value; }
        }

        #endregion

        #region OldState

        private Visit m_oldState;
        public Visit OldState
        {
            get { return m_oldState; }
            set { m_oldState = value; }
        }

        #endregion

        #region NewState

        private Visit m_newState;
        public Visit NewState
        {
            get { return m_newState; }
            set { m_newState = value; }
        }

        #endregion
    }
}
