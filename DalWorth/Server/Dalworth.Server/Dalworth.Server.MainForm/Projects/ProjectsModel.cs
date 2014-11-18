using System.Collections.Generic;
using System.ComponentModel;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using Dalworth.Server.Domain.package;

namespace Dalworth.Server.MainForm.Projects
{
    public class ProjectsModel : IModel
    {
        #region Projects

        private BindingList<ProjectWrapper> m_projects;
        public BindingList<ProjectWrapper> Projects
        {
            get { return m_projects; }
        }

        #endregion

        #region ProjectManagers

        private List<Employee> m_projectManagers;
        public List<Employee> ProjectManagers
        {
            get { return m_projectManagers; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_projectManagers = Employee.FindBy(EmployeeTypeEnum.ProjectManager);

            UpdateProjects();
        }

        #endregion

        #region UpdateProjects

        public void UpdateProjects(string jobNumber = null, 
            string servmanId = null, string firstName = null, string lastName = null, string phoneNo = null,
            string city = null, string zip = null, string street = null, string block = null, 
            ProjectStatusEnum? status = null, ProjectTypeEnum? projectType = null, DateRange dateRange = null, int? projectManagerId = null)
        {
            if (!string.IsNullOrEmpty(jobNumber))
            {
                int projectId;
                if (int.TryParse(jobNumber, out projectId))
                    m_projects = Project.FindProjectWrappers(exactProjectId:(int?)projectId);
                else       
                    m_projects = Project.FindProjectWrappers(jobNumber: jobNumber);

                return;
            }

            if ((string.IsNullOrEmpty(servmanId)  || servmanId.Trim() == string.Empty)
                && (string.IsNullOrEmpty(firstName) || firstName.Trim() == string.Empty)
                && (string.IsNullOrEmpty(lastName) || lastName.Trim() == string.Empty)
                && (string.IsNullOrEmpty(phoneNo) || phoneNo.Trim() == string.Empty)
                && (string.IsNullOrEmpty(city) || city.Trim() == string.Empty)
                && (string.IsNullOrEmpty(zip) || zip.Trim() == string.Empty)
                && (string.IsNullOrEmpty(street) || street.Trim() == string.Empty)
                && (string.IsNullOrEmpty(block) || block.Trim() == string.Empty)
                && status == null
                && projectType == null
                && (dateRange == null || dateRange.IsNull)
                && projectManagerId == null)
            {
                m_projects = new BindingList<ProjectWrapper>();
            }
            else
            {
                m_projects = Project.FindProjectWrappers(firstName:firstName, 
                lastName:lastName, phoneNo:phoneNo,
                city:city, zip:zip, street:street, block:block, 
                status:status, projectType:projectType, 
                dateRange:dateRange, 
                projectManagerId:projectManagerId);
            }
        }

        #endregion

        #region FindVisit

        public Visit FindVisit(TaskWrapperOnProjectsScreen task)
        {
            if (task.ProcessDate.HasValue)
                return Visit.FindByTaskAndProcessDate(task.Task, task.ProcessDate.Value);

            List<Visit> visits = Visit.FindByTask(task.Task);
            if (visits.Count == 0)
                throw new DataNotFoundException("Visit not found");

            return Visit.FindByTask(task.Task)[0];
        }

        #endregion

        #region Print

        public void Print(List<ProjectWrapper> selectedProjects)
        {
            foreach (ProjectWrapper project in selectedProjects)
            {
                new ProjectPrint(project).Print();
            }
        }

        #endregion

        #region FindRecentProjects

        public BindingList<ProjectWrapper> FindRecentProjects(int customerId)
        {
            return Project.FindProjectWrappers(customerId:customerId, isActiveAndRecent:true);
        }

        #endregion

        #region FindRecentProjects

        public BindingList<ProjectPrint> GetPrintedTickets(List<ProjectWrapper> selectedProjects)
        {
            BindingList<ProjectPrint> result = new BindingList<ProjectPrint>();

            foreach (ProjectWrapper project in selectedProjects)
            {
                result.Add(new ProjectPrint(project));
            }

            return result;
        }

        #endregion
    }
}
