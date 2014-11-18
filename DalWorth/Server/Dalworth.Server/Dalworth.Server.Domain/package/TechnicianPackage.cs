using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain.package
{
    public class TechnicianPackage
    {
        #region Fields

        #region Work

        private Work m_work;
        public Work Work
        {
            get { return m_work; }
            set { m_work = value; }
        }

        #endregion
        
        #region WorkDetails

        private List<WorkDetail> m_workDetails;
        public List<WorkDetail> WorkDetails
        {
            get { return m_workDetails; }
            set { m_workDetails = value; }
        }

        #endregion

        #region Visits

        private List<VisitPackage> m_visits;
        public List<VisitPackage> Visits
        {
            get { return m_visits; }
            set { m_visits = value; }
        }

        #endregion

        #region Technician

        private Employee m_technician;
        public Employee Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
        }

        #endregion

        #region Van

        private Van m_van;
        public Van Van
        {
            get { return m_van; }
            set { m_van = value; }
        }

        #endregion        

        #endregion

        #region FindTechnicianPackages

        private const String SqlFindTechnicianPackages =
                @"select e.*, w.*, vn.*, wd.*, v.*, c.*, a.*, vt.*, t.*, p.* from Employee e
                    left join Work w on w.TechnicianEmployeeId = e.ID AND Date(w.StartDate) = ?StartDate
                    left join Van vn on vn.ID = w.VanId

                    left join WorkDetail wd on wd.WorkId = w.ID
                    left join Visit v on v.ID = wd.VisitId

                    left join Customer c on c.ID = v.CustomerId
                    left join Address a on v.ServiceAddressId = a.ID
                    left join VisitTask vt on v.ID = vt.VisitId
                    left join Task t on t.ID = vt.TaskId
                    left join Project p on p.ID = t.ProjectId

                    where e.EmployeeTypeId = 1 and e.IsActive and e.IsRestoration and IsUnknown
                    order by e.ID, v.ID";

        private const String SqlFindTechnicianPackagesWithSettings =
                @"select e.*, w.*, vn.*, wd.*, v.*, c.*, a.*, vt.*, t.*, p.* from DashboardSharedSetting dss
                    inner join Employee e on dss.TechnicianId = e.ID

                    left join Work w on w.TechnicianEmployeeId = e.ID AND Date(w.StartDate) = ?StartDate
                    left join Van vn on vn.ID = w.VanId

                    left join WorkDetail wd on wd.WorkId = w.ID
                    left join Visit v on v.ID = wd.VisitId

                    left join Customer c on c.ID = v.CustomerId
                    left join Address a on v.ServiceAddressId = a.ID
                    left join VisitTask vt on v.ID = vt.VisitId
                    left join Task t on t.ID = vt.TaskId
                    left join Project p on p.ID = t.ProjectId

                    where Date(dss.DashboardDate) = ?StartDate and
                    e.EmployeeTypeId = 1 and e.IsActive and dss.IsVisible
                    order by dss.Sequence, e.ID, v.ID";

        public static List<TechnicianPackage> FindTechnicianPackages(DateTime date, int dispatchId, IDbConnection connection)
        {
            List<TechnicianPackage> packages = new List<TechnicianPackage>();

            bool isContainsSettings = DashboardSharedSetting.IsContainsSettings(date);

            using (IDbCommand dbCommand = Database.PrepareCommand(
                isContainsSettings ? SqlFindTechnicianPackagesWithSettings : SqlFindTechnicianPackages, connection))
            {
                Database.PutParameter(dbCommand, "?StartDate", date.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    int prevEmployeeId = 0;
                    int prevVisitId = 0;

                    while (dataReader.Read())
                    {
                        TechnicianPackage package;

                        if (dataReader.GetInt32(0) == prevEmployeeId)
                        {
                            package = packages[packages.Count - 1];
                            Visit visit =
                                Visit.Load(dataReader, Employee.FieldsCount + Domain.Work.FieldsCount + Domain.Van.FieldsCount + WorkDetail.FieldsCount);
                            if (visit.ID == prevVisitId)
                            {
                                TaskPackage taskPackage = new TaskPackage();
                                taskPackage.Task =
                                    Task.Load(dataReader,
                                              Employee.FieldsCount + Domain.Work.FieldsCount + Domain.Van.FieldsCount + WorkDetail.FieldsCount +
                                              Visit.FieldsCount + Customer.FieldsCount + Address.FieldsCount +
                                              VisitTask.FieldsCount);
                                taskPackage.Task.Project =
                                    Project.Load(dataReader,
                                                 Employee.FieldsCount + Domain.Work.FieldsCount + Domain.Van.FieldsCount +
                                                 WorkDetail.FieldsCount +
                                                 Visit.FieldsCount + Customer.FieldsCount + Address.FieldsCount +
                                                 VisitTask.FieldsCount + Task.FieldsCount);
                                package.Visits[package.Visits.Count - 1].Tasks.Add(taskPackage);                                
                                continue;
                            }
                        }                            
                        else
                        {
                            package = new TechnicianPackage();
                            package.WorkDetails = new List<WorkDetail>();
                            package.Visits = new List<VisitPackage>();
                            packages.Add(package);

                            package.Technician = Employee.Load(dataReader);
                            prevEmployeeId = package.Technician.ID;
                            if (!dataReader.IsDBNull(Employee.FieldsCount))
                            {
                                package.Work = Work.Load(dataReader, Employee.FieldsCount);
                                if (package.Work.VanId != null)
                                    package.Van = Van.Load(dataReader, Employee.FieldsCount + Work.FieldsCount);
                            }
                                
                        }

                        if (package.Work != null)
                        {
                            if (dataReader.IsDBNull(Employee.FieldsCount + Domain.Work.FieldsCount + Van.FieldsCount)) //No work details
                                continue;

                            package.WorkDetails.Add(
                                WorkDetail.Load(dataReader,
                                Employee.FieldsCount + Domain.Work.FieldsCount + Domain.Van.FieldsCount));

                            VisitPackage visitPackage = new VisitPackage(
                                Visit.Load(dataReader, Employee.FieldsCount + Domain.Work.FieldsCount + Domain.Van.FieldsCount + WorkDetail.FieldsCount),
                                null, null, null);

                            if (!dataReader.IsDBNull(Employee.FieldsCount + Domain.Work.FieldsCount + Domain.Van.FieldsCount + WorkDetail.FieldsCount + Visit.FieldsCount)) //Customer Exist
                                visitPackage.Customer = Customer.Load(dataReader, Employee.FieldsCount + Domain.Work.FieldsCount + Domain.Van.FieldsCount + WorkDetail.FieldsCount + Visit.FieldsCount);

                            if (!dataReader.IsDBNull(Employee.FieldsCount + Domain.Work.FieldsCount + Domain.Van.FieldsCount + WorkDetail.FieldsCount + Visit.FieldsCount + Customer.FieldsCount)) //Address Exist
                                visitPackage.ServiceAddress = Address.Load(dataReader, Employee.FieldsCount + Domain.Work.FieldsCount + Domain.Van.FieldsCount + WorkDetail.FieldsCount + Visit.FieldsCount + Customer.FieldsCount);

                            prevVisitId = visitPackage.Visit.ID;
                            visitPackage.Tasks = new List<TaskPackage>();

                            TaskPackage taskPackage = new TaskPackage();
                            taskPackage.Task =
                                Task.Load(dataReader,
                                          Employee.FieldsCount + Domain.Work.FieldsCount + Domain.Van.FieldsCount + WorkDetail.FieldsCount +
                                          Visit.FieldsCount + Customer.FieldsCount + Address.FieldsCount +
                                          VisitTask.FieldsCount);
                            taskPackage.Task.Project =
                                Project.Load(dataReader,
                                         Employee.FieldsCount + Domain.Work.FieldsCount + Domain.Van.FieldsCount + WorkDetail.FieldsCount +
                                         Visit.FieldsCount + Customer.FieldsCount + Address.FieldsCount +
                                         VisitTask.FieldsCount + Task.FieldsCount);

                            visitPackage.Tasks.Add(taskPackage);
                            package.Visits.Add(visitPackage);
                        }
                    }
                }
            }

            return packages;           
        }

        #endregion        
    }
}
