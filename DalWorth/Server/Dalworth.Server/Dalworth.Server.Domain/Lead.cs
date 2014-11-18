using System;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class Lead
    {
        #region FIELD CONSTANTS

        public const string FIELD_PROJECTTYPEID = "PROJECTTYPEID";
        public const string FIELD_SERVMANTECHID = "SERVMANTECHID";
        public const string FIELD_BUSINESSPARTNERID = "BUSINESSPARTNERID";
        public const string FIELD_COMPANY = "COMPANY";
        public const string FIELD_FIRSTNAME = "FIRSTNAME";
        public const string FIELD_LASTNAME = "LASTNAME";
        public const string FIELD_ADDRESS1 = "ADDRESS1";
        public const string FIELD_ADDRESS2 = "ADDRESS2";
        public const string FIELD_CITY = "CITY";
        public const string FIELD_STATE = "STATE";
        public const string FIELD_ZIP = "ZIP";
        public const string FIELD_PHONE1 = "PHONE1";
        public const string FIELD_PHONE2 = "PHONE2";
        public const string FIELD_EMAIL = "Email";
        public const string FIELD_PREFEREED_TIME = "PREFERREDTIME";
        public const string FIELD_PREFERRED_SERVICE_DATE = "PREFERREDSERVICEDATE";
        public const string FIELD_SYSTEMERROR = "SYSTEMERROR";
        public const string FIELD_ADVERTISING_SOURECE_ACRONYM = "ADVERTISINGSOURCEACRONYM";
        public const string FIELD_SERVMAN_ADVERTISING_SOURCE = "SERVMAN_ADVERTISING_SOURCE";
        public const string FIELD_SERVMAN_TRACK_CODE = "SERVMAN_TRACK_CODE";

        #endregion

        public Lead()
        {
        }

        #region ContainsAddress

        public bool ContainsAddress
        {
            get { return Address1 != string.Empty && City != string.Empty && Zip.HasValue && Zip != 0 && State != string.Empty;}
        }

        #endregion

        #region Fields

        #region FullName

        public string FullName
        {
            get { return (FirstName + " " + LastName); }
        }

        #endregion

        #region Address

        public string Address
        {
            get
            {
                if (ContainsAddress)
                {
                    return Address1 + " " + Address2 + ", " + City + " " + Zip + " " + State;
                }
                else
                {
                    return string.Empty;
                }

            }
        }

        #endregion

        #region Phones

        public string Phones
        {
            get { return (Utils.FormatPhone(Phone1) + ((Phone2 != string.Empty) ? (", " + Utils.FormatPhone(Phone2)) : "")); }
        }

        #endregion

        #region ProjectType

        public ProjectTypeEnum ProjectType
        {
            get
            {
                if (!m_projectTypeId.HasValue)
                    return ProjectTypeEnum.NotSpecified;

                return (ProjectTypeEnum)m_projectTypeId;
            }
            
            set
            {
                if (value == ProjectTypeEnum.NotSpecified)
                    m_projectTypeId = null;

                m_projectTypeId = (int)value;
            }
        }

        public string ProjectTypeStr
        {
            get { return Domain.ProjectType.GetText(ProjectType); }
        }

        #endregion

        #region Status

        public LeadStatusEnum Status
        {
            get { return (LeadStatusEnum)m_leadStatusId; }
            set { m_leadStatusId = (int)value; }
        }

        public string StatusStr
        {
            get { return LeadStatus.GetText(Status); }
        }

        #endregion

        #endregion

        #region FindLeadWrappers

        public static BindingList<LeadWrapper> FindLeadWrappers(int exactLeadId)
        {
            BindingList<LeadWrapper> result = new BindingList<LeadWrapper>();

            Lead lead = null;
            BusinessPartner businessPartner = null;
            try
            {
                lead = FindByPrimaryKey(exactLeadId);
                if (lead.BusinessPartnerId > 0)
                {
                    businessPartner = BusinessPartner.FindByPrimaryKey(lead.BusinessPartnerId);
                }
            }
            catch (Exception) { }

            if (lead != null)
                result.Add(new LeadWrapper(lead, businessPartner));

            return result;
        }

        public static BindingList<LeadWrapper> FindLeadWrappers(
            string firstName, string lastName, string phoneNo,
            string city, string zip, string street, string block,
            int? status, DateRange dateRange)
        {
            string SqlFindLeads =
                @" select l.*, bp.* from lead l
                    inner join BusinessPartner bp on bp.ID = l.BusinessPartnerId
                    where l.projecttypeid is not null and ";

            if (firstName != null && firstName != string.Empty)
                SqlFindLeads += " l.FirstName like ?FirstName and";
            if (lastName != null && lastName != string.Empty)
                SqlFindLeads += " l.LastName like ?LastName and";
            if (phoneNo != null && phoneNo != string.Empty)
                SqlFindLeads += " (l.Phone1 = ?PhoneNumber or l.Phone2 = ?PhoneNumber) and";
            if (city != null && city != string.Empty)
                if (city == street)
                    SqlFindLeads += " (l.City like ?City or l.Address1 like ?Street)  and ";
                else
                    SqlFindLeads += " l.City like ?City and ";
            if (zip != null && zip != string.Empty)
                if (zip == block)
                    SqlFindLeads += " (l.Zip = ?Zip or l.Address1 like ?Block) and ";
                else
                    SqlFindLeads += " l.Zip = ?Zip and ";
            if (street != null && street != string.Empty && street != city)
                SqlFindLeads += " l.Address1 like ?Street and";
            if (block != null && block != string.Empty && block != zip)
                SqlFindLeads += " l.Address1 like ?Block and";
            if (status != null)
            {
                if (status == 0)
                    SqlFindLeads += " (l.LeadStatusId = ?NewLeadStatusId or l.LeadStatusId = ?PendingLeadStatusId) and ";
                else
                    SqlFindLeads += " l.LeadStatusId = ?LeadStatusId and ";
            }
            if (dateRange != null)
            {
                if (dateRange.StartDate.HasValue)
                    SqlFindLeads += " l.DateCreated >= ?DateCreatedStart and";
                if (dateRange.EndDate.HasValue)
                    SqlFindLeads += " l.DateCreated <= ?DateCreatedEnd and";
            }

            SqlFindLeads += " 1=1 order by l.LeadStatusId, l.DateCreated desc limit 200";

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLeads))
            {
                if (firstName != null && firstName != string.Empty)
                    Database.PutParameter(dbCommand, "?FirstName", firstName + "%");
                if (lastName != null && lastName != string.Empty)
                    Database.PutParameter(dbCommand, "?LastName", lastName + "%");
                if (phoneNo != null && phoneNo != string.Empty)
                    Database.PutParameter(dbCommand, "?PhoneNumber", phoneNo);
                if (city != null && city != string.Empty)
                    Database.PutParameter(dbCommand, "?City", city + "%");
                if (zip != null && zip != string.Empty)
                    Database.PutParameter(dbCommand, "?Zip", zip);
                if (street != null && street != string.Empty)
                    Database.PutParameter(dbCommand, "?Street", "%" + street + "%");
                if (block != null && block != string.Empty)
                    Database.PutParameter(dbCommand, "?Block", block + "%");
                if (status != null)
                    if (status == 0)
                    {
                        Database.PutParameter(dbCommand, "?NewLeadStatusId", (int)LeadStatusEnum.New);
                        Database.PutParameter(dbCommand, "?PendingLeadStatusId", (int)LeadStatusEnum.Pending);
                    }
                    else
                        Database.PutParameter(dbCommand, "?LeadStatusId", status);
                if (dateRange != null)
                {
                    if (dateRange.StartDate.HasValue)
                        Database.PutParameter(dbCommand, "?DateCreatedStart",
                                              new DateTime(dateRange.StartDate.Value.Year,
                                                           dateRange.StartDate.Value.Month,
                                                           dateRange.StartDate.Value.Day, 0, 0, 0));
                    if (dateRange.EndDate.HasValue)
                        Database.PutParameter(dbCommand, "?DateCreatedEnd",
                                              new DateTime(dateRange.EndDate.Value.Year,
                                                           dateRange.EndDate.Value.Month,
                                                           dateRange.EndDate.Value.Day, 23, 59, 59));
                }

                BindingList<LeadWrapper> result = new BindingList<LeadWrapper>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Lead lead = Load(dataReader);
                        BusinessPartner businessPartner = null;

                        if (!dataReader.IsDBNull(FieldsCount))
                        {
                            businessPartner = BusinessPartner.Load(dataReader, FieldsCount);
                        }

                        LeadWrapper leadWrapper = new LeadWrapper(lead, businessPartner);
                        result.Add(leadWrapper);
                    }
                }

                foreach (LeadWrapper wrapper in result)
                {
                    try
                    {
                        if (wrapper.Lead.LastUpdateEmployeeId.HasValue)
                        {
                            Employee employee = Employee.FindByPrimaryKey(wrapper.Lead.LastUpdateEmployeeId.Value);
                            wrapper.LastUpdatedByEmployee = employee.FirstName + " " + employee.LastName;
                        }

                        if (wrapper.Lead.FirstUpdateEmployeeId.HasValue)
                        {
                            Employee employee = Employee.FindByPrimaryKey(wrapper.Lead.FirstUpdateEmployeeId.Value);
                            wrapper.FirstUpdatedByEmployee = employee.FirstName + " " + employee.LastName;
                        }
                    }
                    catch (DataNotFoundException) { };
                }
                return result;
            }
        }

        public static BindingList<LeadWrapper> FindSimilarLeadWrappers(
            string firstName, string lastName, string phone1, string phone2)
        {
            string SqlFindLeads =
                @" select l.*, bp.* from lead l
                    inner join BusinessPartner bp on bp.ID = l.BusinessPartnerId
                    where (";

            bool isLastNameDefined = false;
            bool isFirstNameDefined = false;
            bool isPhone1Defined = false;
            bool isPhone2Defined = false;

            if (firstName != null && firstName != string.Empty && lastName != null && lastName != string.Empty)
            {
                SqlFindLeads += " (l.FirstName = ?FirstName and l.LastName = ?LastName) or ";
                isFirstNameDefined = true;
                isLastNameDefined = true;
            }
            if (lastName != null && lastName != string.Empty && phone1 != null && phone1 != string.Empty)
            {
                SqlFindLeads += " (l.LastName = ?LastName and l.Phone1 = ?Phone1) or ";
                isLastNameDefined = true;
                isPhone1Defined = true;
            }
            if (lastName != null && lastName != string.Empty && phone2 != null && phone2 != string.Empty)
            {
                SqlFindLeads += " (l.LastName = ?LastName and l.Phone2 = ?Phone2) or";
                isLastNameDefined = true;
                isPhone2Defined = true;
            }

            SqlFindLeads += " 0=1) and (l.LeadStatusId = 1 or l.LeadStatusId = 2) order by l.PreferredServiceDate desc, l.LeadStatusId desc limit 200";

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLeads))
            {
                if (isFirstNameDefined)
                    Database.PutParameter(dbCommand, "?FirstName", firstName);
                if (isLastNameDefined)
                    Database.PutParameter(dbCommand, "?LastName", lastName);
                if (isPhone1Defined)
                    Database.PutParameter(dbCommand, "?Phone1", phone1);
                if (isPhone2Defined)
                    Database.PutParameter(dbCommand, "?Phone2", phone2);

                BindingList<LeadWrapper> result = new BindingList<LeadWrapper>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Lead lead = Load(dataReader);
                        BusinessPartner businessPartner = null;

                        if (!dataReader.IsDBNull(FieldsCount))
                        {
                            businessPartner = BusinessPartner.Load(dataReader, FieldsCount);
                        }

                        LeadWrapper leadWrapper = new LeadWrapper(lead, businessPartner);

                        result.Add(leadWrapper);
                    }
                }
                return result;
            }
        }

        #endregion

        #region GetLeadsInfo

        public static LeadsInfo GetLeadsInfo(IDbConnection connection)
        {
            string SqlGetLeadsInfo =
                @" select
                    (select count(*) from lead where LeadStatusId = 1 and projecttypeid is not null) openedLeads,
                    (select count(*) from lead where LeadStatusId = 2 and projecttypeid is not null) pendingLeads,
                    (select count(*) from lead l where projecttypeid is not null and LeadStatusId = 2 and (DATE_ADD(l.DateCreated, INTERVAL 12 HOUR) < Now())) pendingAlertLeads";

            LeadsInfo result = new LeadsInfo();

            using (IDataReader dataReader = Database.PrepareCommand(SqlGetLeadsInfo, connection).ExecuteReader())
            {
                if (dataReader.Read())
                {
                    result.OpenedLeads = dataReader.GetInt32(0);
                    result.PendingLeads = dataReader.GetInt32(1);
                    result.PendingAlertLeads = dataReader.GetInt32(2);
                }
            }
            return result;
        }

        #endregion

        #region Save

        public static void Save(Lead lead)
        {
            if (lead.ID > 0)
                Update(lead);
            else
                Insert(lead);
        }

        #endregion

        #region Submit

        public Dictionary<string, string> Submit(int? projectTypeId, int businessPartnerId,
           string firstName, string lastName, string phone1, string email, string customerNotes, string advertisingSourceAcronym)
        {
            return Submit(false, projectTypeId, string.Empty, businessPartnerId, string.Empty, firstName, lastName,
                string.Empty, string.Empty, String.Empty, String.Empty,
                string.Empty, phone1, string.Empty, email, customerNotes, string.Empty, string.Empty, advertisingSourceAcronym);
        }

        public Dictionary<string, string> Submit(int? projectTypeId, int businessPartnerId,
           string firstName, string lastName, string phone1, string email, string customerNotes, string advertisingSourceAcronym,
            int? keywordId, int? webLogId, IDbConnection connection)
        {
            return Submit(false, projectTypeId, string.Empty, businessPartnerId, string.Empty, firstName, lastName,
                string.Empty, string.Empty, String.Empty, String.Empty,
                string.Empty, phone1, string.Empty, email, customerNotes, string.Empty, string.Empty,
                advertisingSourceAcronym, string.Empty, string.Empty, keywordId, webLogId, connection);
        }

        public Dictionary<string, string> Submit(int? projectTypeId, int businessPartnerId,
           string firstName, string lastName, string phone1, string email, string customerNotes, string advertisingSourceAcronym,
            int? keywordId, int? webLogId)
        {
            return Submit(false, projectTypeId, string.Empty, businessPartnerId, string.Empty, firstName, lastName,
                string.Empty, string.Empty, String.Empty, String.Empty,
                string.Empty, phone1, string.Empty, email, customerNotes, string.Empty, string.Empty,
                advertisingSourceAcronym, string.Empty, string.Empty, keywordId, webLogId);
        }

        public Dictionary<string, string> Submit(int? projectTypeId, string servmanTechId, int businessPartnerId,
            string firstName, string lastName, string phone1, string email, string customerNotes, string advertisingSourceAcronym,
            string servmanAdvertisingSource, string servmanTrackCode)
        {
            return Submit(false, projectTypeId, servmanTechId, businessPartnerId, string.Empty, firstName, lastName,
                string.Empty, string.Empty, String.Empty, String.Empty,
                string.Empty, phone1, string.Empty, email, customerNotes, string.Empty, string.Empty, advertisingSourceAcronym,
                servmanAdvertisingSource, servmanTrackCode, null, null);
        }

        public Dictionary<string, string> Submit(int? projectTypeId, string servmanTechId, int businessPartnerId, string company,
            string firstName, string lastName, string address1, string address2,
            string city, string state, string zip, string phone1, string phone2, string email, string customerNotes,
            string preferredServiceDate, string preferredTime, string advertisingSourceAcronym, string servmanAdvertisingSource,
            string servmanTrackCode)
        {
            return Submit(true, projectTypeId, servmanTechId, businessPartnerId, company, firstName,
                lastName, address1, address2, city, state, zip, phone1, phone2, email, customerNotes,
                preferredServiceDate, preferredTime, advertisingSourceAcronym, servmanAdvertisingSource, servmanTrackCode, null, null);
        }

        public Dictionary<string, string> Submit(int? projectTypeId, int businessPartnerId, string company,
            string firstName, string lastName, string address1, string address2,
            string city, string state, string zip, string phone1, string phone2, string email, string customerNotes,
            string preferredServiceDate, string preferredTime, string advertisingSourceAcronym)
        {
            return Submit(true, projectTypeId, string.Empty, businessPartnerId, company, firstName,
                lastName, address1, address2, city, state, zip, phone1, phone2, email, customerNotes,
                preferredServiceDate, preferredTime, advertisingSourceAcronym);
        }

        private Dictionary<string, string> Submit(bool isFullLead, int? projectTypeId, string servmanTechId,
            int businessPartnerId, string company,
            string firstName, string lastName, string address1, string address2,
            string city, string state, string zip, string phone1, string phone2, string email, string customerNotes,
            string preferredServiceDate, string preferredTime, string advertisingSourceAcronym)
        {
            return Submit(isFullLead, projectTypeId, servmanTechId, businessPartnerId, company, firstName, lastName, address1, address2,
                city, state, zip, phone1, phone2, email, customerNotes, preferredServiceDate, preferredTime, advertisingSourceAcronym, string.Empty,
                string.Empty, null, null);
        }

        private Dictionary<string, string> Submit(bool isFullLead, int? projectTypeId, string servmanTechId,
           int businessPartnerId, string company,
           string firstName, string lastName, string address1, string address2,
           string city, string state, string zip, string phone1, string phone2, string email, string customerNotes,
           string preferredServiceDate, string preferredTime, string advertisingSourceAcronym, string servmanAdvertisingSource,
           string servmanTrackCode, int? keywordId, int? weblogId, IDbConnection connection)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            Employee technician = null;
            int intZip = 0;
            DateTime serviceDate = DateTime.MinValue;

            try
            {
                if (projectTypeId.HasValue)
                {
                    try
                    {
                        Domain.ProjectType.FindByPrimaryKey(projectTypeId.Value, connection);
                    }
                    catch (DataNotFoundException)
                    {
                        result.Add(FIELD_PROJECTTYPEID, "not found.");
                    }
                }

                if (servmanTechId != null && servmanTechId.Length > 0)
                {
                    try
                    {
                        technician = Employee.FindByServmanTechId(servmanTechId, connection);
                    }
                    catch (DataNotFoundException)
                    {
                        result.Add(FIELD_SERVMANTECHID, "not found.");
                    }
                }

                try
                {
                    BusinessPartner.FindByPrimaryKey(businessPartnerId, connection);
                }
                catch (DataNotFoundException)
                {
                    result.Add(FIELD_BUSINESSPARTNERID, "not found.");
                }

                if (firstName == null || firstName.Length == 0)
                    result.Add(FIELD_FIRSTNAME, "is required.");

                if (lastName == null || lastName.Length == 0)
                    result.Add(FIELD_LASTNAME, "is required.");

                if (phone1 == null || phone1.Length == 0)
                    result.Add(FIELD_PHONE1, "is required.");
                else
                {
                    phone1 = Regex.Replace(phone1, "[^0-9]", "");

                    if (phone1.Length < 10 || phone1.Length > 11)
                        result.Add(FIELD_PHONE1, "must be 10 or 11 digits.");
                }

                if (email == null || email.Length == 0)
                    result.Add(FIELD_EMAIL, "is required.");
                else
                {
                    email = email.ToUpper();
                    Regex regex = new Regex("^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$");

                    if (!regex.IsMatch(email))
                        result.Add(FIELD_EMAIL, "is in invalid format.");
                }

                if (advertisingSourceAcronym != null && advertisingSourceAcronym.Length > 6)
                    result.Add(FIELD_ADVERTISING_SOURECE_ACRONYM, "is too long.  Must be 1 to 6 characters.");

                if (servmanAdvertisingSource != null && servmanAdvertisingSource.Length > 10)
                    result.Add(FIELD_SERVMAN_ADVERTISING_SOURCE, "is too long.  Must be 1 to 10 characters");

                if (servmanTrackCode != null && servmanTrackCode.Length > 10)
                    result.Add(FIELD_SERVMAN_TRACK_CODE, "is too long.  Must be 1 to 10 characters");

                if (isFullLead)
                {
                    if (company != null && company.Length > 50)
                        result.Add(FIELD_COMPANY, "is too long.  Must be less then 50 characters.");

                    if (address1 == null || address1.Length == 0)
                        result.Add(FIELD_ADDRESS1, "is required.");
                    else if (address1.Length > 60)
                        result.Add(FIELD_ADDRESS1, "too long.  Must be less then 60 characters.");

                    if (address2 != null && address2.Length > 40)
                        result.Add(FIELD_ADDRESS2, "too long  Must be less then 40 characters.");

                    if (city == null || city.Length == 0)
                        result.Add(FIELD_CITY, "is required.");
                    else if (city.Length > 24)
                        result.Add(FIELD_CITY, "too long  Must be less then 24 characters.");

                    if (state == null || state.Length == 0)
                        result.Add(FIELD_STATE, "is required.");
                    else if (state.Length != 2)
                        result.Add(FIELD_STATE, "invalid value.");

                    if (zip != null && zip.Length > 0)
                    {
                        if (zip.Length != 5)
                            result.Add(FIELD_ZIP, "is in incorrect format.");
                        else
                        {
                            if (!int.TryParse(zip, out intZip))
                                result.Add(FIELD_ZIP, "must be 5 digits.");
                        }
                    }

                    if (phone2 != null && phone2.Length > 0)
                    {
                        phone2 = Regex.Replace(phone2, "[^0-9]", "");

                        if (phone2.Length < 10 || phone2.Length > 11)
                            result.Add(FIELD_PHONE2, "must be 10 or 11 digits.");
                    }

                    if (preferredServiceDate != null && preferredServiceDate.Length > 0)
                    {
                        if (!DateTime.TryParse(preferredServiceDate, out serviceDate))
                            result.Add(FIELD_PREFERRED_SERVICE_DATE, "is in invalid format.  Must be yyyy/mm/dd.");
                    }

                    if (preferredTime == null || preferredTime.Length == 0)
                        preferredTime = "Any Time";

                    if (preferredTime != null && preferredTime.Length > 0 && preferredTime != "AM" && preferredTime != "PM" && preferredTime != "Any Time")
                        result.Add(FIELD_PREFEREED_TIME, "must be AM, PM or Any Time.");
                }
                else
                {
                    address1 = String.Empty;
                    address2 = string.Empty;
                    city = string.Empty;
                    state = string.Empty;
                    zip = string.Empty;
                    phone2 = string.Empty;
                    preferredTime = string.Empty;

                }

                if (result.Count > 0)
                    return result;

                ProjectTypeId = projectTypeId;
                if (technician != null)
                    EmployeeId = technician.ID;
                BusinessPartnerId = businessPartnerId;
                Company = (company != null ? company.ToUpper() : String.Empty);
                FirstName = firstName.ToUpper();
                LastName = lastName.ToUpper();
                Address1 = address1.ToUpper();
                Address2 = (address2 != null ? address2.ToUpper() : String.Empty);
                City = city.ToUpper();
                State = state.ToUpper();
                Zip = intZip;
                Phone1 = phone1;
                Phone2 = phone2;
                Email = email.ToUpper();
                PreferredServiceDate = serviceDate;
                PreferredTime = preferredTime.ToUpper();
                AdvertisingSourceAcronym = advertisingSourceAcronym;
                ServmanAdvertisingSource = servmanAdvertisingSource;
                ServmanTrackCode = servmanTrackCode;
                CustomerNotes = (customerNotes != null ? customerNotes : String.Empty);

                DispatchNotes = String.Empty;
                DateCreated = DateTime.Now;
                Status = LeadStatusEnum.New;
                KeywordId = keywordId;
                WebLogId = weblogId;

                IDbTransaction transaction = connection.BeginTransaction();

                try
                {
                    Lead.Insert(this, connection);

                    BackgroundJobPending job = new BackgroundJobPending();
                    job.BackgroundJobTypeId = (int)BackgroundJobTypeEnum.LeadRecievedEmail;
                    job.LeadId = ID;
                    BackgroundJobPending.Insert(job, connection);

                    job.BackgroundJobTypeId = (int)BackgroundJobTypeEnum.LeadRecievedPrint;
                    BackgroundJobPending.Insert(job, connection);

                    if (ProjectTypeId == (int)ProjectTypeEnum.Deflood)
                    {
                        job.BackgroundJobTypeId = (int)BackgroundJobTypeEnum.NotifiyOnCallNewLead;
                        BackgroundJobPending.Insert(job, connection);
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                result.Add(FIELD_SYSTEMERROR, ex.Message);
            }

            return result;
        }

        private Dictionary<string, string> Submit(bool isFullLead, int? projectTypeId, string servmanTechId,
            int businessPartnerId, string company,
            string firstName, string lastName, string address1, string address2,
            string city, string state, string zip, string phone1, string phone2, string email, string customerNotes,
            string preferredServiceDate, string preferredTime, string advertisingSourceAcronym, string servmanAdvertisingSource,
            string servmanTrackCode, int? keywordId, int? weblogId)
        {

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                return Submit(isFullLead, projectTypeId, servmanTechId, businessPartnerId, company, firstName, lastName, address1, address2,
                    city, state, zip, phone1, phone2, email, customerNotes, preferredServiceDate, preferredTime, advertisingSourceAcronym,
                    servmanAdvertisingSource, servmanTrackCode, keywordId, weblogId, connection);
            }
        }

        #endregion

        #region FindLateLeads

        private const String SqlSelectLateLeads = SqlSelectAll +
            @" where leadstatusid = 1
               and DateLateNotificationSent is null
               and timestampdiff(MINUTE,datecreated, current_timestamp()) >= 10";

        public static List<Lead> FindLateLeads(IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectLateLeads, connection))
            {
                List<Lead> rv = new List<Lead>();

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

    public class LeadWrapper
    {
        #region Constructor

        public LeadWrapper(Lead lead, BusinessPartner businessPartner)
        {
            m_lead = lead;
            m_businessPartner = businessPartner;
        }

        #endregion

        #region Lead

        private Lead m_lead;
        public Lead Lead
        {
            get { return m_lead; }
            set { m_lead = value; }
        }

        #region ID

        public int ID
        {
            get
            {
                if (m_lead != null)
                    return m_lead.ID;
                else
                    return 0;
            }
        }

        #endregion

        #region ProjectTypeStr

        public string ProjectTypeStr
        {
            get { return Lead.ProjectTypeStr; }
        }

        #endregion

        #region FullName

        public string FullName
        {
            get
            {
                if (m_lead != null)
                    return m_lead.FullName;
                else
                    return "[No Lead]";
            }
        }

        #endregion

        #region Address

        public string Address
        {
            get
            {
                if (m_lead != null)
                    return m_lead.Address;
                else
                    return "[No Lead]";
            }
        }

        #endregion

        #region Zip

        public string Zip
        {
            get
            {
                if (m_lead.Zip.HasValue && m_lead.Zip > 0)
                    return m_lead.Zip.ToString();
                else
                    return string.Empty;
                
            }
        }

        #endregion

        #region Phones

        public string Phones
        {
            get
            {
                if (m_lead != null)
                    return m_lead.Phones;
                else
                    return "[No Lead]";
            }
        }

        #endregion

        #region Email

        public string Email
        {
            get
            {
                if (m_lead != null)
                    return m_lead.Email;
                else
                    return "[No Lead]";
            }
        }

        #endregion

        #region DateCreated

        public DateTime? DateCreated
        {
            get
            {
                if (m_lead != null)
                    return m_lead.DateCreated;
                else
                    return null;
            }
        }

        #endregion

        #region PreferredServiceDate

        public DateTime? PreferredServiceDate
        {
            get
            {
                if (m_lead != null)
                    return m_lead.PreferredServiceDate;
                else
                    return null;
            }
        }

        #endregion

        #region PreferredTime

        public string PreferredTime
        {
            get
            {
                if (m_lead != null)
                    return m_lead.PreferredTime;
                else
                    return "[No Lead]";
            }
        }

        #endregion

        #region CustomerNotes

        public string CustomerNotes
        {
            get
            {
                if (m_lead != null)
                    return m_lead.CustomerNotes;
                else
                    return "[No Lead]";
            }
        }

        #endregion

        #region DispatchNotes

        public string DispatchNotes
        {
            get
            {
                if (m_lead != null)
                    return m_lead.DispatchNotes;
                else
                    return "[No Lead]";
            }
            set
            {
                if (m_lead != null)
                    m_lead.DispatchNotes = value;
            }
        }

        #endregion

        #region StatusStr

        public string StatusStr
        {
            get
            {
                if (m_lead != null)
                    return m_lead.StatusStr;
                else
                    return "[No Lead]";
            }
        }

        #endregion

        #region AdvertisingSourceAcronym

        public string AdvertisingSourceAcronym
        {
            get
            {
                if (m_lead != null)
                    return m_lead.AdvertisingSourceAcronym;
                else
                    return "[No Lead]";
            }
        }

        #endregion

        #endregion

        #region BusinessPartner

        private BusinessPartner m_businessPartner;
        public BusinessPartner BusinessPartner
        {
            get { return m_businessPartner; }
            set { m_businessPartner = value; }
        }

        #region BusinessPartnerName

        public string BusinessPartnerName
        {
            get
            {
                if (m_businessPartner != null)
                    return m_businessPartner.Name;
                else
                    return "[No Business Partner]";
            }
        }

        #endregion

        #region LastUpdatedByEmployee

        private string m_lastUpdatedByEmployee = null;

        public string LastUpdatedByEmployee
        {
            get 
            {
                if (m_lastUpdatedByEmployee == null)
                    return "";
                else
                    return m_lastUpdatedByEmployee;
            }

            set { m_lastUpdatedByEmployee = value; }
        }

        #endregion

        #region FirstUpdatedByEmployee

        private string m_firstUpdatedByEmployee = null;

        public string FirstUpdatedByEmployee
        {
            get
            {
                if (m_firstUpdatedByEmployee == null)
                    return "";
                else
                    return m_firstUpdatedByEmployee;
            }

            set { m_firstUpdatedByEmployee = value; }
        }

        #endregion

        #region FirstUpdatedByDate

        public DateTime? FirstUpdatedByDate
        {
            get { return Lead.DateFirstSetPending; }
        }

        #endregion

        #region LastUpdatedByDate

        public DateTime? LastUpdatedByDate
        {
            get { return Lead.DateLastSetPending; }
        }

        #endregion

        #endregion
    }

    public class LeadsInfo
    {
        public LeadsInfo()
        {
            m_openedLeads = 0;
            m_pendingLeads = 0;
            m_pendingAlertLeads = 0;
        }

        public LeadsInfo(int openedLeads, int pendingLeads, int pendingAlertLeads)
        {
            m_openedLeads = openedLeads;
            m_pendingLeads = pendingLeads;
            m_pendingAlertLeads = pendingAlertLeads;
        }

        #region OpenedLeads

        private int m_openedLeads;
        public int OpenedLeads
        {
            get { return m_openedLeads; }
            set { m_openedLeads = value; }
        }

        #endregion

        #region PendingLeads

        private int m_pendingLeads;
        public int PendingLeads
        {
            get { return m_pendingLeads; }
            set { m_pendingLeads = value; }
        }

        #endregion

        #region PendingAlertLeads

        private int m_pendingAlertLeads;
        public int PendingAlertLeads
        {
            get { return m_pendingAlertLeads; }
            set { m_pendingAlertLeads = value; }
        }

        #endregion
    }
}


      