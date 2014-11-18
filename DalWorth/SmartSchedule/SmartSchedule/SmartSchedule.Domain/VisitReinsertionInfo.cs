using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchedule.Domain
{
    //Represents how the sigle visit got reinserted (to allow insertion)
    public class VisitReinsertionInfo
    {
        public VisitReinsertionInfo() : this(null, null, int.MinValue) { }

        public VisitReinsertionInfo(Technician technician, List<PathItem> path, int rankChange)
        {
            m_technician = technician;
            m_path = path;
            m_rankChange = rankChange;
        }        

        //New technician where to place reinserted visit
        private Technician m_technician;
        public Technician Technician
        {
            get { return m_technician; }
        }

        //Represents the new best possible path on 'Technician' 
        private List<PathItem> m_path;
        public List<PathItem> Path
        {
            get { return m_path; }
        }

        //Gets the value how this reinsertion has been affected (changed) good joint rank
        //Greater than zero - increased, less - decreased rank
        private int m_rankChange;
        public int RankChange
        {
            get { return m_rankChange; }
        }

        public bool IsReinsertionSucceed()
        {
            return m_path != null && m_path.Count > 0;
        }
    }
}
