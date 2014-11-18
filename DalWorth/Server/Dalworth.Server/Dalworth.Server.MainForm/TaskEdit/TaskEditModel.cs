using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.TaskEdit
{
    public class TaskEditModel : IModel
    {
        #region Task

        private Task m_task;
        public Task Task
        {
            get { return m_task; }
            set { m_task = value; }
        }

        #endregion

        #region Items

        private BindingList<Item> m_items;
        public BindingList<Item> Items
        {
            get { return m_items; }
            set { m_items = value; }
        }

        #endregion

        #region IsReadOnly

        private bool m_isReadOnly;
        public bool IsReadOnly
        {
            get { return m_isReadOnly; }
        }

        #endregion

        #region Init

        public void Init()
        {
            Items = new BindingList<Item>(Item.FindByTask(m_task));

            m_isReadOnly = true;

            if (m_task.TaskStatus == TaskStatusEnum.NotCompleted)
            {
                Visit visit = Visit.FindLatestVisitByTask(m_task);
                if (visit.VisitStatus == VisitStatusEnum.Pending
                    || visit.VisitStatus == VisitStatusEnum.Assigned)
                {
                    m_isReadOnly = false;
                }                                    
            }

        }

        #endregion
    }
}
