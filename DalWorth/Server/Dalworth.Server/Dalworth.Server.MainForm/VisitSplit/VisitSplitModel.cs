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
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.MainForm.CreateVisit;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using Task=Dalworth.Server.Domain.Task;

namespace Dalworth.Server.MainForm.VisitSplit
{
    public enum DialogMode
    {
        Split,
        Edit
    }

    public enum SplitResult
    {
        PrintVisit1,
        PrintVisit2,
        PrintBoth,
        PrintNone
    }

    public class VisitSplitModel : IModel
    {
        #region Visit1

        private Visit m_visit1;
        public Visit Visit1
        {
            get { return m_visit1; }
            set { m_visit1 = value; }
        }

        #endregion

        #region Visit2

        private Visit m_visit2;
        public Visit Visit2
        {
            get { return m_visit2; }
            set { m_visit2 = value; }
        }

        #endregion

        #region Visit1Tasks

        private BindingList<TaskWrapper> m_visit1Tasks;
        public BindingList<TaskWrapper> Visit1Tasks
        {
            get { return m_visit1Tasks; }
            set { m_visit1Tasks = value; }
        }

        #endregion

        #region Visit2Tasks

        private BindingList<TaskWrapper> m_visit2Tasks;
        public BindingList<TaskWrapper> Visit2Tasks
        {
            get { return m_visit2Tasks; }
            set { m_visit2Tasks = value; }
        }

        #endregion

        #region DefloodTasks

        private List<Task> m_defloodTasks;
        public List<Task> DefloodTasks
        {
            get { return m_defloodTasks; }
            set { m_defloodTasks = value; }
        }

        #endregion

        #region Mode

        private DialogMode m_mode;
        public DialogMode Mode
        {
            get { return m_mode; }
        }

        #endregion

        #region InitialSummaryVisit1

        private VisitSummaryPackage m_initialSummaryVisit1;       
        public VisitSummaryPackage InitialSummaryVisit1
        {
            get { return m_initialSummaryVisit1; }
            set { m_initialSummaryVisit1 = value; }
        }

        #endregion

        #region InitialSummaryVisit2

        private VisitSummaryPackage m_initialSummaryVisit2;
        public VisitSummaryPackage InitialSummaryVisit2
        {
            get { return m_initialSummaryVisit2; }
            set { m_initialSummaryVisit2 = value; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_initialSummaryVisit1 = new VisitSummaryPackage(m_visit1);

            m_defloodTasks = new List<Task>();

            if (m_visit2 != null)
            {
                m_mode = DialogMode.Edit;
                m_visit2Tasks = FindTasks(m_visit2);
                m_initialSummaryVisit2 = new VisitSummaryPackage(m_visit2);
            } else
            {
                m_visit2 = (Visit)m_visit1.Clone();
                m_visit2.ID = 0;
                m_mode = DialogMode.Split;
                m_visit2Tasks = new BindingList<TaskWrapper>();
            }
           
            m_visit1Tasks = FindTasks(m_visit1);            
        }

        #endregion

        #region FindTasks
        
        private BindingList<TaskWrapper> FindTasks(Visit visit)
        {
            BindingList<TaskWrapper> result = new BindingList<TaskWrapper>();

            List<Task> tasks = Task.FindByVisit(visit);
            foreach (Task task in tasks)
            {
                if (task.TaskType != TaskTypeEnum.Deflood)
                    result.Add(new TaskWrapper(task));
                else
                    m_defloodTasks.Add(task);

                task.Project = Project.FindByPrimaryKey(task.ProjectId);
            }

            return result;
        }

        #endregion

        #region MoveTasks

        public void MoveTasks(List<int> itemIndexesToBeMoved, bool isToSecondVisit)
        {
            BindingList<TaskWrapper> sourceList = isToSecondVisit ? m_visit1Tasks : m_visit2Tasks;
            BindingList<TaskWrapper> destinationList = !isToSecondVisit ? m_visit1Tasks : m_visit2Tasks;

            List<TaskWrapper> movedTasks = new List<TaskWrapper>();
            foreach (int index in itemIndexesToBeMoved)
                movedTasks.Add(sourceList[index]);

            foreach (TaskWrapper task in movedTasks)
            {
                sourceList.Remove(task);
                destinationList.Add(task);
            }
        }

        #endregion

        #region Save

        public SplitResult Save()
        {
            if (m_mode == DialogMode.Split)
                SplitVisit();
            else
                EditVisits();

            bool isPrintVisit1 = false;
            bool isPrintVisit2 = false;            

            try
            {
                Visit.FindByPrimaryKey(m_visit1.ID);
                isPrintVisit1 = m_visit1.IsNeedToPrint(m_initialSummaryVisit1, m_mode == DialogMode.Edit);
            }
            catch (DataNotFoundException){}

            try
            {
                Visit.FindByPrimaryKey(m_visit2.ID);
                isPrintVisit2 = m_visit2.IsNeedToPrint(m_initialSummaryVisit2, m_mode == DialogMode.Edit);
            }
            catch (DataNotFoundException){}

            if (isPrintVisit1 && isPrintVisit2)
                return SplitResult.PrintBoth;
            else if (!isPrintVisit1 && !isPrintVisit2)
                return SplitResult.PrintNone;
            else if (isPrintVisit1)
                return SplitResult.PrintVisit1;
            else
                return SplitResult.PrintVisit2;
        }

        #endregion

        #region SplitVisit

        public void SplitVisit()
        {
            Visit.Update(m_visit1);

            foreach (TaskWrapper task in m_visit2Tasks)
            {
                VisitTask.Delete(new VisitTask(m_visit1.ID, task.Task.ID));
                if (task.Task.TaskType == TaskTypeEnum.Monitoring)
                    VisitTask.Delete(new VisitTask(m_visit1.ID, FindDefloodTask(task).ID));
            }
                

            m_visit2.CreateDate = DateTime.Now;
            Visit.Insert(m_visit2);

            foreach (TaskWrapper task in m_visit2Tasks)
            {
                VisitTask.Insert(new VisitTask(m_visit2.ID, task.Task.ID));
                if (task.Task.TaskType == TaskTypeEnum.Monitoring)
                    VisitTask.Insert(new VisitTask(m_visit2.ID, FindDefloodTask(task).ID));
            }
                
        }

        #endregion

        #region EditVisits

        public void EditVisits()
        {
            VisitTask.DeleteBy(m_visit1);
            VisitTask.DeleteBy(m_visit2);

            foreach (TaskWrapper task in m_visit1Tasks)
            {
                VisitTask.Insert(new VisitTask(m_visit1.ID, task.Task.ID));
                if (task.Task.TaskType == TaskTypeEnum.Monitoring)
                    VisitTask.Insert(new VisitTask(m_visit1.ID, FindDefloodTask(task).ID));
            }                

            foreach (TaskWrapper task in m_visit2Tasks)
            {
                VisitTask.Insert(new VisitTask(m_visit2.ID, task.Task.ID));
                if (task.Task.TaskType == TaskTypeEnum.Monitoring)
                    VisitTask.Insert(new VisitTask(m_visit2.ID, FindDefloodTask(task).ID));
            }                

            if (m_visit1Tasks.Count == 0)
                Visit.Delete(m_visit1);
            else
                Visit.Update(m_visit1);

            if (m_visit2Tasks.Count == 0)
                Visit.Delete(m_visit2);
            else
                Visit.Update(m_visit2);
        }

        #endregion

        #region FindDefloodTask

        public Task FindDefloodTask(TaskWrapper monitoring)
        {            
            foreach (Task defloodCandidate in m_defloodTasks)
            {
                if (monitoring.Task.ParentTaskId.Value == defloodCandidate.ID)
                    return defloodCandidate;
            }

            throw new DataNotFoundException("Deflood task not found");
        }

        #endregion

        #region GetVisitsRequiringServiceDatePrompt        
      
        internal List<GetVisitsRequiringServiceDatePromptResult> GetVisitsRequiringServiceDatePrompt()
        {
            List<GetVisitsRequiringServiceDatePromptResult> result = new List<GetVisitsRequiringServiceDatePromptResult>();
            
            if (!Visit1.ServiceDate.HasValue)
            {
                foreach (TaskWrapper taskWrapper in Visit1Tasks)
                {
                    if (taskWrapper.Task.TaskTypeId == (int)TaskTypeEnum.Monitoring)
                    {
                        GetVisitsRequiringServiceDatePromptResult resultValue = 
                            new GetVisitsRequiringServiceDatePromptResult();
                        resultValue.IsDeflood = IsDeflood(taskWrapper.Task);
                        resultValue.VisitId = Visit1.ID;
                        result.Add(resultValue);
                        break;
                    }
                }
            }

            if (!Visit2.ServiceDate.HasValue)
            {
                foreach (TaskWrapper taskWrapper in Visit2Tasks)
                {
                    if (taskWrapper.Task.TaskTypeId == (int)TaskTypeEnum.Monitoring)
                    {
                        GetVisitsRequiringServiceDatePromptResult resultValue =
                            new GetVisitsRequiringServiceDatePromptResult();
                        resultValue.IsDeflood = IsDeflood(taskWrapper.Task);
                        resultValue.VisitId = Visit2.ID;
                        result.Add(resultValue);
                        break;
                    }
                }
            }

            return result;
        }

        #endregion

        #region IsDeflood

        // returns true if monitoring monitoring task is only one, meaning it is deflood visit.
        private bool IsDeflood(Task task)
        {
            Boolean result = true;

            if (task.ParentTaskId.HasValue)
            {
                Project project = Project.FindByPrimaryKey(task.ProjectId);
                List<Task> allTasks = Task.FindByProject(project);

                List<Task> monitorings = allTasks.FindAll(delegate(Task t) {return  t.TaskTypeId == (int)TaskTypeEnum.Monitoring; });

                if (monitorings.Count > 1)
                {
                    result = false;
                }
            }
        
            return result;
        }

        #endregion
    }

    public class TaskWrapper
    {
        private Task m_task;
        public Task Task
        {
            get { return m_task; }
        }

        public TaskWrapper(Task task)
        {
            m_task = task;
        }

        public int ID
        {
            get { return m_task.ID; }
        }

        public string TaskTypeText
        {
            get
            {
                if (m_task.TaskType == TaskTypeEnum.Monitoring)
                    return "Deflood, Monitoring";
                return m_task.TaskTypeText;
            }
        }
    }

    internal class GetVisitsRequiringServiceDatePromptResult
    {
        #region VisitId

        public int m_visitId;
        public int VisitId
        {
            get { return m_visitId; }
            set { m_visitId = value; }

        }

        #endregion

        #region VisitIdDescription

        public string VisitIdDescription
        {
            get { return m_visitId == 0 ? "Unknown" : m_visitId.ToString(); }

        }

        #endregion

        #region IsDeflood

        private bool m_isDeflood;
        public Boolean IsDeflood
        {
            get { return m_isDeflood; }
            set { m_isDeflood = value; }
        }

        #endregion
    }
}
