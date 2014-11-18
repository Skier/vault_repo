using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using Dalworth.Server;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Domain
{
    public partial class ProjectFeedback
    {
        public enum CallbackPeriodEnum
        {
            NotSelected,
            DoNotRemind,
            SixMonth,
            OneYear,
            OneYearAndHalf,
            TwoYears
        }

        public ProjectFeedback()
        {

        }

        public void Submit(int projectId, int rateId, string customerNote, ProjectFeedback.CallbackPeriodEnum callbackPeriod)
        {
            ProjectId = projectId;
            CanBePostedOnWebSite = false;
            RateId = rateId;
            CustomerNote = customerNote;
            EditedCustomerNote = customerNote;
            DispatcherNote = string.Empty;
            DateCreated = DateTime.Now;
            CallbackPeriod = callbackPeriod;

            BackgroundJobPending backgroundJob = new BackgroundJobPending();
            backgroundJob.BackgroundJobType = BackgroundJobTypeEnum.ProjectFeedbackReceived;
            backgroundJob.ProjectId = ProjectId;

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                Insert(this, connection);
                BackgroundJobPending.Insert(backgroundJob, connection);
            }
        }

        public CallbackPeriodEnum CallbackPeriod
        {
            get
            {
                if (!IsCallbackSelected)
                    return CallbackPeriodEnum.NotSelected;

                if (IsDoNotCallSelected)
                    return CallbackPeriodEnum.DoNotRemind;

                switch (CallbackDaysInterval)
                {
                    case 180:
                        return CallbackPeriodEnum.SixMonth;
                        break;
                    case 360:
                        return CallbackPeriodEnum.OneYear;
                        break;
                    case 540:
                        return CallbackPeriodEnum.OneYearAndHalf;
                        break;
                    case 720:
                        return CallbackPeriodEnum.TwoYears;
                        break;
                }

                return CallbackPeriodEnum.NotSelected;
            }

            set
            {
                switch (value)
                {
                    case CallbackPeriodEnum.NotSelected:
                        IsCallbackSelected = false;
                        break;
                    case CallbackPeriodEnum.DoNotRemind:
                        IsCallbackSelected = true;
                        IsDoNotCallSelected = true;
                        break;
                    case CallbackPeriodEnum.SixMonth:
                        IsCallbackSelected = true;
                        IsDoNotCallSelected = false;
                        CallbackDaysInterval = 180;
                        break;
                    case CallbackPeriodEnum.OneYear:
                        IsCallbackSelected = true;
                        IsDoNotCallSelected = false;
                        CallbackDaysInterval = 360;
                        break;
                    case CallbackPeriodEnum.OneYearAndHalf:
                        IsCallbackSelected = true;
                        IsDoNotCallSelected = false;
                        CallbackDaysInterval = 540;
                        break;
                    case CallbackPeriodEnum.TwoYears:
                        IsCallbackSelected = true;
                        IsDoNotCallSelected = false;
                        CallbackDaysInterval = 720;
                        break;
                }
            }
        }

        #region FindLatest

        private const string SqlFindLatest =
            @"SELECT pf.*
              FROM projectfeedback pf
              where pf.projectid =?ProjectId
              order by pf.id desc
              limit 1";

        public static ProjectFeedback FindLatest(int projectId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLatest))
            {
                Database.PutParameter(dbCommand, "?ProjectId", projectId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }
            throw new DataNotFoundException("ProjectFeedback not found, search by project Id" + projectId.ToString());
        }

        #endregion

        #region GetNewCount

        private const string SqlNewCount = @"select count(*) from projectfeedback where DateReviewed is null";

        public static int GetNewCount()
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlNewCount))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return dataReader.GetInt32(0);
                }
            }

            return 0;
        }

        #endregion

        #region FindByProjectId

        private const string SqlFindByProjectId =
            @"SELECT * FROM projectfeedback
             where projectid = ?ProjectId
             order by id desc
             limit 1";

        public static ProjectFeedback FindByProjectId(int projectId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByProjectId))
            {
                Database.PutParameter(dbCommand, "?ProjectId", projectId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }
            throw new DataNotFoundException("ProjectFeedback not found, search by project Id");
        }

        #endregion

        #region FindReminderCandidates

        private const string SqlFindReminderCandidates =
            @"select *
             from projectfeedback
             where
             date(adddate(datecreated, callbackdaysinterval)) < date(now())
             and ReminderEmailSentDate is null
             and isCallbackSelected = true
             and isdonotcallselected = false             
            ";

        public static List<ProjectFeedback> FindReminderCandidates()
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindReminderCandidates, null))
            {
                List<ProjectFeedback> rv = new List<ProjectFeedback>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        rv.Add(Load(dataReader));
                    }
                }
                return rv;
            }
        }
        #endregion
    }

    public class ProjectFeedbackWrapper
    {
        #region Properties

        public int ID
        {
            get { return m_projectFeedback.ID; }
        }

        private DateTime m_callbackDate;
        public string CallbackDate
        {
            get 
            {
                if (m_callbackDate == null || m_callbackDate == DateTime.MinValue)
                    return String.Empty;
                else
                    return string.Format("{0:MM/dd/yyyy}", m_callbackDate); 
            }
        }

        public string Phone1
        {
            get { return m_customer.Phone1Formatted; }
        }

        public string FirstLastName
        {
            get 
            {
                return Utils.FormatName(m_customer.FirstName, "") + " " + m_customer.LastName.Substring(0, 1);
            }
        }

        public string CustomerName
        {
            get
            {
                return Utils.FormatName(m_customer.FirstName, "") + " " + Utils.FormatName(m_customer.LastName, "");
            }
        }

        public string ProjectType
        {
            get
            {
                return m_project.ProjectTypeText;
            }
        }

        public string Rating
        {
            get {return m_projectFeedbackRate.Rate;}
        }

        public string City
        {
            get {

                string[] cityNameParts = m_address.City.Split(' ');

                string cityName = string.Empty;

                foreach (string str in cityNameParts)
                {
                    cityName += " " + Utils.FormatName(str, "");
                }

                return cityName;
            }
        }

        public string Zip
        {
            get { return m_address.Zip.ToString(); }
        }

        public string State
        {
            get { return m_address.State; }
        }

        public string DatePosted
        {
            get { return string.Format("{0:M/dd/yyyy}", m_projectFeedback.DateCreated); }
        }

        public string CustomerNote
        {
            get { return m_projectFeedback.CustomerNote; }
        }

        public string CustomerNoteEdited
        {
            get { return m_projectFeedback.EditedCustomerNote;}
        }

        public string Rate
        {
            get { return m_projectFeedbackRate.Rate; }
        }

        public string DateReviewed
        {
            get { return string.Format("{0:MM/dd/yyyy}", m_projectFeedback.DateReviewed); }
        }

        private Employee m_reviewedByEmployee;
        public string ReviewedByEmployeeName
        {
            get {return m_reviewedByEmployee == null?String.Empty:m_reviewedByEmployee.FirstName + " " + m_reviewedByEmployee.LastName; }
            
        }

        public Employee ReviewedByEmployee
        {
            set { m_reviewedByEmployee = value; }
        }

        public bool IsPublished
        {
            get {return m_projectFeedback.CanBePostedOnWebSite;}
        }

        private ProjectFeedback m_projectFeedback;
        public ProjectFeedback ProjectFeedback
        {
            get { return m_projectFeedback; }
            set { m_projectFeedback = value; }
        }

        private Customer m_customer;
        public Customer Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        private Address m_address;
        public Address Address
        {
            get { return m_address; }
            set { m_address = value; }
        }

        private Project m_project;
        public Project Project
        {
            get { return m_project; }
            set { m_project = value; }
        }

        private ProjectFeedbackRate m_projectFeedbackRate;
        public ProjectFeedbackRate ProjectFeedbackRate
        {
            get { return m_projectFeedbackRate; }
            set { m_projectFeedbackRate = value; }
        }

        #endregion 

        #region GetApprovedFeedbacks

        public static BindingList<ProjectFeedbackWrapper> FindApprovedFeedbacks(ProjectTypeEnum projectType, IDbConnection connection)
        {
            return FindApprovedFeedbacks(projectType, 25, connection);
        }

        private const string SqlApprovedFeedbacks =
            @"SELECT pf.*, c.*, ad.* FROM projectfeedback pf
                join project p on pf.projectid = p.id
                join customer c on p.customerid = c.id
                join address ad on ad.id = c.addressid
            where p.ProjectTypeId = ?ProjectTypeId
            and pf.RateId < 3 and length(EditedCustomerNote) > 0
            and CanBePostedOnWebSite = 1
            order by pf.id desc
            limit ?Limit";

        public static BindingList<ProjectFeedbackWrapper> FindApprovedFeedbacks(ProjectTypeEnum projectType, int limit, IDbConnection connection)
        {
            int projectTypeInt = (int)projectType;

            BindingList<ProjectFeedbackWrapper> result = new BindingList<ProjectFeedbackWrapper>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlApprovedFeedbacks, connection))
            {
                Database.PutParameter(dbCommand, "?ProjectTypeId", projectTypeInt);
                Database.PutParameter(dbCommand, "?Limit", limit);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while(dataReader.Read())
                    {
                        int offset = 0;
                        ProjectFeedbackWrapper wrapper = new ProjectFeedbackWrapper();

                        wrapper.ProjectFeedback = ProjectFeedback.Load(dataReader, offset);
                        offset += ProjectFeedback.FieldsCount;

                        wrapper.Customer = Customer.Load(dataReader, offset);
                        offset += Customer.FieldsCount;

                        wrapper.Address = Address.Load(dataReader, offset);
                        result.Add(wrapper);
                    }
                }
                
            }

            return result;
        }

        #endregion

        #region FindFeedbackWrappers

        private const string SqlGetProjectFeedbacks =
            @"SELECT pfr.*, pf.*, date_add(pf.datecreated, INTERVAL pf.callbackdaysinterval DAY) callbackdate, c.*, p.*, e.* 
                FROM projectfeedback pf
                join project p on pf.projectid = p.id
                join customer c on p.customerid = c.id
                join projectfeedbackrate pfr on pf.rateid = pfr.id
                left outer join employee e on e.id = pf.ReviewedByEmployeeId
                where 1 = 1
            ";

        public static BindingList<ProjectFeedbackWrapper> FindProjectFeedbacks(string firstName, string lastName, int? status,
            DateRange dateCreatedRange, DateRange dateCallbackRange, IDbConnection connection)
        {
           string sql = SqlGetProjectFeedbacks; 

           BindingList<ProjectFeedbackWrapper> result = new BindingList<ProjectFeedbackWrapper>();

           if (dateCallbackRange != null && (dateCallbackRange.StartDate.HasValue || dateCallbackRange.EndDate.HasValue))
           {
               if (dateCallbackRange.StartDate.HasValue || dateCallbackRange.EndDate.HasValue)
                   sql += " and pf.callbackdaysinterval is not null";

               if (dateCallbackRange.StartDate.HasValue)
                   sql += " and date_add(pf.datecreated, INTERVAL pf.callbackdaysinterval DAY) >=?DateCallbackStart";

                if (dateCallbackRange.EndDate.HasValue)
                    sql += " and date_add(pf.datecreated, INTERVAL pf.callbackdaysinterval DAY) <= ?DateCallbackEnd";
           }
           else
           {
               if (firstName != null && firstName != string.Empty)
                   sql += " and c.FirstName like ?FirstName";

               if (lastName != null && lastName != string.Empty)
                   sql += " and c.LastName like ?LastName";

               if (status.HasValue)
               {
                   if (status.Value == 0)
                       sql += " and DateReviewed is null";
                   else
                       sql += " and DateReviewed is not null";
               }

               if (dateCreatedRange != null)
               {
                   if (dateCreatedRange.StartDate.HasValue)
                       sql += " and DateCreated >= ?DateCreatedStart";

                   if (dateCreatedRange.EndDate.HasValue)
                       sql += " and DateCreated <= ?DateCreatedEnd";
               }
           }

           sql += " order by pf.id desc";

           using (IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
           {
               if (dateCallbackRange != null && (dateCallbackRange.StartDate.HasValue || dateCallbackRange.EndDate.HasValue))
               {
                   if (dateCallbackRange.StartDate.HasValue)
                       Database.PutParameter(dbCommand, "?DateCallbackStart", dateCallbackRange.StartDate);

                   if (dateCallbackRange.EndDate.HasValue)
                        Database.PutParameter(dbCommand, "?DateCallbackEnd", dateCallbackRange.EndDate);
               }
               else
               {
                   if (firstName != null && firstName != string.Empty)
                       Database.PutParameter(dbCommand, "?FirstName", firstName + "%");

                   if (lastName != null && lastName != string.Empty)
                       Database.PutParameter(dbCommand, "?LastName", lastName + "%");

                   if (dateCreatedRange != null)
                   {
                       if (dateCreatedRange.StartDate.HasValue)
                           Database.PutParameter(dbCommand, "?DateCreatedStart", dateCreatedRange.StartDate);

                       if (dateCreatedRange.EndDate.HasValue)
                           Database.PutParameter(dbCommand, "?DateCreatedEnd", dateCreatedRange.EndDate);
                   }
               }

               using (IDataReader dataReader = dbCommand.ExecuteReader())
               {
                   while (dataReader.Read())
                   {
                       int offset = 0;
                       ProjectFeedbackWrapper wrapper = new ProjectFeedbackWrapper();

                       wrapper.m_projectFeedbackRate = ProjectFeedbackRate.Load(dataReader, offset);
                       offset += ProjectFeedbackRate.FieldsCount;
 
                       wrapper.ProjectFeedback = ProjectFeedback.Load(dataReader, offset);
                       offset += ProjectFeedback.FieldsCount;

                       if (!dataReader.IsDBNull(offset))
                           wrapper.m_callbackDate = dataReader.GetDateTime(offset);
                       offset++;

                       wrapper.Customer = Customer.Load(dataReader, offset);
                       offset += Customer.FieldsCount;

                       wrapper.m_project = Project.Load(dataReader, offset);
                       offset += Project.FieldsCount;

                       if (!dataReader.IsDBNull(offset))
                           wrapper.m_reviewedByEmployee = Employee.Load(dataReader, offset);

                        offset += Employee.FieldsCount;

                       result.Add(wrapper);
                   }
               }
           }

           return result;
        }    

        #endregion
    }
}
      