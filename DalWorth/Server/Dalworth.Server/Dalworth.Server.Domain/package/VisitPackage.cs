using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using Dalworth.Server.Data;
using Dalworth.Server.Domain.package;

namespace Dalworth.Server.Domain
{
    public enum PendingVisitTypeEnum
    {
        Current = 1, //Taday and past
        Tomorrow = 2,
        DayAfterTomorrow = 3,
        Future = 4, //Future excluding tomorrow and day after tomorrow
        Unscheduled = 5,
        All = 6
    }

    public class VisitPackage
    {
        public VisitPackage() {}

        public VisitPackage(Visit visit, Customer customer, Address serviceAddress, List<TaskPackage> tasks)
        {
            m_visit = visit;
            m_customer = customer;
            m_serviceAddress = serviceAddress;
            m_tasks = tasks;
        }

        #region Fields

        #region Visit

        private Visit m_visit;
        public Visit Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

        #region Customer

        private Customer m_customer;
        public Customer Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        #endregion

        #region ServiceAddress

        private Address m_serviceAddress;
        public Address ServiceAddress
        {
            get { return m_serviceAddress; }
            set { m_serviceAddress = value; }
        }

        #endregion        

        #region Tasks

        private List<TaskPackage> m_tasks;
        public List<TaskPackage> Tasks
        {
            get { return m_tasks; }
            set { m_tasks = value; }
        }

        #endregion

        #region Simple Type Properties

        #region VisitNumber

        public string VisitNumber
        {
            get { return m_visit.ID.ToString(); }
        }

        #endregion

        #region TaskTypes

        public string TaskTypes
        {
            get
            {
                List<Task> tasks = new List<Task>();
                foreach (TaskPackage task in m_tasks)
                    tasks.Add(task.Task);

                return m_visit.GetTaskTypes(tasks);
            }
        }

        #endregion

        #region IsReady

        public bool IsReady
        {
            get
            {
                foreach (TaskPackage task in m_tasks)
                    if (!task.Task.IsReady)
                        return false;
                return true;
            }
        }

        #endregion

        #region Message

        public string Message
        {
            get { return m_tasks[0].Task.Message; }
        }

        #endregion

        #region Notes

        public string Notes
        {
            get { return m_visit.Notes; }
        }

        #endregion

        #region CustomerName

        public string CustomerName
        {
            get
            {
                if (Customer == null)
                    return "[No Customer]";
                return Customer.DisplayName;
            }
        }

        #endregion

        #region Map

        public string Map
        {
            get
            {
                if (ServiceAddress == null)
                    return string.Empty;
                return ServiceAddress.Map ?? string.Empty;
            }
        }

        #endregion

        #region ServiceAddressString

        public string ServiceAddressString
        {
            get 
            {
                if (ServiceAddress == null)
                    return string.Empty;
                return ServiceAddress.AddressSingleLine;
            }
        }

        #endregion        

        #region ServiceDate

        public DateTime? ServiceDate
        {
            get
            {
                if (Visit.ServiceDate.HasValue)
                    return Visit.ServiceDate.Value.Date;
                return null;                
            }
        }

        #endregion

        #region TimeFrame

        public string TimeFrame
        {
            get
            {
                return Visit.TimeFrameText;
            }
        }

        #endregion

        #region PrintLabel

        public string PrintLabel
        {
            get { return "More"; }
        }

        #endregion

        #endregion

        #endregion

        #region GetPendingVisits

        public static List<VisitPackage> FindVisits(int? exactVisitId, VisitStatusEnum visitStatus, 
            PendingVisitTypeEnum visitType, string customerName, IDbConnection connection)
        {
            string SqlGetVisits =
                @"SELECT v.*, c.*, a.*, t.*, p.* FROM Visit v
                    left join Customer c on c.ID = v.CustomerId
                    left join Address a on a.ID = v.ServiceAddressId
                    inner join VisitTask vt on vt.VisitId = v.ID
                    inner join Task t on t.ID = vt.TaskId
                    inner join Project p on p.ID = t.ProjectId
                WHERE 1=1 ";

            
            if (exactVisitId.HasValue)
            {
                SqlGetVisits += "AND v.ID = ?ExactVisitId ";
            } else
            {
                SqlGetVisits += "AND v.VisitStatusId = ?VisitStatusId ";
                if (customerName.Trim() != string.Empty)
                    SqlGetVisits += "AND c.LastName like ?CustomerLastName ";
                else if (visitType == PendingVisitTypeEnum.Current)
                {
                    SqlGetVisits += @"AND 
                    ((v.ServiceDate is not null AND DATE(v.ServiceDate) <= DATE(CURDATE()))
                      OR
                    (v.ServiceDate is null and v.ID in
                       (select v1.ID from VisitTask vt1 
                         inner join Visit v1 on v1.ID = vt1.VisitId
                         inner join Task t1 on vt1.TaskID = t1.ID
                     where t1.TaskTypeId = 5)))";   
                }
                else if (visitType == PendingVisitTypeEnum.Tomorrow)
                    SqlGetVisits += "AND v.ServiceDate is not null AND DATE(v.ServiceDate) = DATE(DATE_ADD(CURDATE(), INTERVAL 1 DAY)) ";
                else if (visitType == PendingVisitTypeEnum.DayAfterTomorrow)
                    SqlGetVisits += "AND v.ServiceDate is not null AND DATE(v.ServiceDate) = DATE(DATE_ADD(CURDATE(), INTERVAL 2 DAY)) ";
                else if (visitType == PendingVisitTypeEnum.Future)
                    SqlGetVisits += "AND v.ServiceDate is not null AND DATE(v.ServiceDate) > DATE(DATE_ADD(CURDATE(), INTERVAL 2 DAY)) ";
                else if (visitType == PendingVisitTypeEnum.Unscheduled)
                    SqlGetVisits += "AND v.ServiceDate is null";               
            }

            SqlGetVisits += " order by v.ID";

            List<VisitPackage> result = new List<VisitPackage>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlGetVisits, connection))
            {
                if (exactVisitId.HasValue)
                {
                    Database.PutParameter(dbCommand, "?ExactVisitId", exactVisitId.Value);
                }
                else
                {
                    Database.PutParameter(dbCommand, "?VisitStatusId", (int)visitStatus);
                    if (customerName.Trim() != string.Empty)
                        Database.PutParameter(dbCommand, "?CustomerLastName", customerName.Trim() + "%");
                }

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (!dataReader.Read())
                        return result;

                    while (true)
                    {
                        Visit visit = Visit.Load(dataReader);
                        Customer customer = null;
                        Address serviceAddress = null;

                        if (!dataReader.IsDBNull(Visit.FieldsCount))
                            customer = Customer.Load(dataReader, Visit.FieldsCount);

                        if (!dataReader.IsDBNull(Visit.FieldsCount + Domain.Customer.FieldsCount))
                            serviceAddress = Address.Load(dataReader, Visit.FieldsCount + Domain.Customer.FieldsCount);

                        List<TaskPackage> tasks = new List<TaskPackage>();
                        TaskPackage task = new TaskPackage();
                        task.Task = Task.Load(dataReader, Visit.FieldsCount 
                            + Domain.Customer.FieldsCount + Address.FieldsCount);
                        task.Task.Project = Project.Load(dataReader, Visit.FieldsCount 
                            + Domain.Customer.FieldsCount + Address.FieldsCount + Task.FieldsCount);
                        tasks.Add(task);

                        bool isNextRowExists = dataReader.Read();
                        while (isNextRowExists && dataReader.GetInt32(0) == visit.ID)
                        {
                            task = new TaskPackage();
                            task.Task = Task.Load(dataReader, Visit.FieldsCount
                                + Domain.Customer.FieldsCount + Address.FieldsCount);
                            task.Task.Project = Project.Load(dataReader, Visit.FieldsCount
                                + Domain.Customer.FieldsCount + Address.FieldsCount + Task.FieldsCount);
                            tasks.Add(task);

                            isNextRowExists = dataReader.Read();
                        }

                        result.Add(new VisitPackage(visit, customer, serviceAddress, tasks));

                        if (!isNextRowExists)
                            break;
                    }
                }
            }

            return result;
        }

        #endregion        

        #region GetVisit

        public static VisitPackage GetVisit(int visitId, IDbConnection connection)
        {
            VisitPackage package = FindVisits(visitId, VisitStatusEnum.Pending, 
                PendingVisitTypeEnum.All, string.Empty, connection)[0];

            foreach (TaskPackage task in package.Tasks)
            {
                if (task.Task.TaskType == TaskTypeEnum.RugPickup || task.Task.TaskType == TaskTypeEnum.RugDelivery)
                    task.Items = Item.FindByTask(task.Task);
            }                       

            return package;
        }

        #endregion

        #region Equals & GetHashCode

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            VisitPackage visitPackage = obj as VisitPackage;
            if (visitPackage == null) return false;
            return Equals(m_visit.ID, visitPackage.m_visit.ID);
        }

        public override int GetHashCode()
        {
            return m_visit.ID.GetHashCode();
        }

        #endregion
    }
}
