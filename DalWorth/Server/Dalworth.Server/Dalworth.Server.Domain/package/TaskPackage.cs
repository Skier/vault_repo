using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dalworth.Server.Domain.package
{
    public class TaskPackage
    {
        #region Task

        private Task m_task;
        public Task Task
        {
            get { return m_task; }
            set { m_task = value; }
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

        #region Items

        private List<Item> m_items;
        public List<Item> Items
        {
            get { return m_items; }
            set { m_items = value; }
        }

        #endregion
    }
}
