using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.Partner.Models
{
    public class Users
    {
        public Users()
        {
            Load();
        }

        public void Load()
        {
            m_usersList = User.Find(PageNonZero - 1, UserName, out m_totalItemCount);
        }

        #region PartnerList

        private List<User> m_usersList;
        public List<User> UserList
        {
            get { return m_usersList; }
        }

        #endregion

        #region Paging

        public int Page { get; set; }
        public int PageNonZero
        {
            get
            {
                if (Page < 1)
                    return 1;
                return Page;
            }
        }

        private int m_totalItemCount;
        public int TotalItemCount
        {
            get { return m_totalItemCount; }
            set { m_totalItemCount = value; }
        }

        #endregion

        public string UserName { get; set; }
    }

    public class User
    {
        #region User

        public User(int id, bool isOwner, string firstName, string lastName, string partnerName, bool hasPassword, bool hasActiveInvitation)
        {
            Id = id;
            IsOwner = isOwner;
            FirstName = firstName;
            LastName = lastName;
            PartnerName = partnerName;
            HasPassword = hasPassword;
            HasActiveInvitation = hasActiveInvitation;
        }

        public User(int? userId)
        {
            if (!userId.HasValue)
                return;

            Id = userId.Value;

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                WebUser user = WebUser.FindByPrimaryKey(Id, connection);                
                FirstName = user.FirstName;
                LastName = user.LastName;
                Email = user.Email;
                HasPassword = !string.IsNullOrEmpty(user.PasswordHash);
                HasActiveInvitation = PartnerInvitation.FindByWebUser(Id, connection).Count > 0;

                if (user.OrderSourceId.HasValue)
                    PartnerId = OrderSource.FindByPrimaryKey(user.OrderSourceId.Value, connection).ID;
            }                

        }

        public User()
        {
        }

        #endregion

        #region Properties

        public int Id { get; set; }        
        public bool IsOwner { get; set; }

        [Required(ErrorMessage = "Please enter First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }

        public string PartnerName { get; set; }

        public bool HasPassword { get; set; }
        private bool HasActiveInvitation { get; set; }
        public int PartnerId { get; set; }

        public bool IsEdit
        {
            get { return Id > 0; }
        }

        public string Name
        {
            get { return Utils.JoinStrings(" ", FirstName, LastName); }
        }

        public string BackLink { get; set; }

        public string BackLinkText
        {
            get
            {
                if (string.IsNullOrEmpty(BackLink))
                    return "Back";
                if (BackLink.Contains("Users"))
                    return "Back to Manage Users";
                if (BackLink.Contains("Partner"))
                    return "Back to Partner Details";
                return "Back";
            }
        }        


        public string StatusText
        {
            get
            {
                if (HasPassword)
                    return "Active";
                if (HasActiveInvitation)
                    return "Invited";
                return "Inactive";
            }
        }

        #endregion

        #region Find

        private const int ITEMS_PER_PAGE = 15;

        public static List<User> Find(int pageNumber, string userName, out int totalItemsCount)
        {
            List<string> userNameParts = new List<string>();
            if (!string.IsNullOrEmpty(userName))
                userNameParts = new List<string>(userName.Split(' ', ','));

            string nameSearchPart = string.Empty;
            if (userNameParts.Count > 0)
            {
                if (userNameParts.Count == 1)
                {
                    nameSearchPart = string.Format(" where wu.LastName like '{0}%' ",
                        userNameParts[0]);                    
                } else
                {
                    nameSearchPart = string.Format(" where wu.FirstName like '{0}%' and  wu.LastName like '{1}%' ",
                        userNameParts[0], userNameParts[1]);
                }
            }

            string queryNoLimit = string.Format(SqlFind, nameSearchPart);
            string query = queryNoLimit + 
                string.Format(" limit {0}, {1}", pageNumber * ITEMS_PER_PAGE, ITEMS_PER_PAGE);
            string queryCount = string.Format("select count(*) from ({0}) t", queryNoLimit);

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();

                List<User> result = new List<User>();
                using (IDbCommand dbCommand = Database.PrepareCommand(query, connection))
                {
                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                            result.Add(Load(dataReader));
                    }
                }

                totalItemsCount = 0;
                using (IDbCommand dbCommand = Database.PrepareCommand(queryCount, connection))
                {
                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                            totalItemsCount = dataReader.GetInt32(0);                                                           
                    }
                }
                
                return result;                
            }                
        }

        private const String SqlFind = @"select wu.ID, wu.IsOwner, wu.FirstName, wu.LastName, os.Name, 
                wu.PasswordHash != '' as HasPassword, count(pi.InvitationKey) > 0 as HasActiveInvitation from webuser wu
            left join OrderSource os on os.ID = wu.OrderSourceId
            left join PartnerInvitation pi on pi.WebUserId = wu.ID and pi.ExpirationDate > Now()
            {0}
            group by wu.ID order by wu.FirstName, wu.LastName";

        public static List<User> Find(int? orderSourceId, IDbConnection connection)
        {
            string query = string.Format(SqlFind,
                orderSourceId.HasValue ? " where os.ID = " + orderSourceId.Value : string.Empty);

            List<User> result = new List<User>();
            using (IDbCommand dbCommand = Database.PrepareCommand(query, connection))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }            
            return result;
        }


        private static User Load(IDataReader dataReader)
        {
            return new User(
                dataReader.GetInt32(0),
                dataReader.GetBoolean(1),
                dataReader.GetString(2),
                dataReader.GetString(3),
                !dataReader.IsDBNull(4) ? dataReader.GetString(4) : string.Empty,
                dataReader.GetBoolean(5),
                dataReader.GetBoolean(6));
        }

        #endregion

        #region Create

        public string Save(int? partnerId)
        {
            bool isEdit = IsEdit;

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();

                if (isEdit)
                {
                    WebUser dbUser = WebUser.FindByPrimaryKey(Id, connection);
                    if (dbUser.Email != Email)
                    {
                        if (WebUser.FindByLogin(Email, connection) != null)
                            return "Unable to create new user. User with the same Email address already exists";                        
                    }
                    dbUser.Email = Email;
                    dbUser.Login = Email;
                    dbUser.FirstName = FirstName;
                    dbUser.LastName = LastName;
                    WebUser.Update(dbUser, connection);
                }
                else
                {
                    if (WebUser.FindByLogin(Email, connection) != null)
                        return "Unable to create new user. User with the same Email address already exists";

                    WebUser user = new WebUser(0, partnerId, Email, string.Empty, null, FirstName, LastName, 
                        Email, !partnerId.HasValue);
                    WebUser.Insert(user, connection);
                    Id = user.ID;
                    if (partnerId.HasValue)
                        PartnerId = partnerId.Value;                    
                }

            }

            if (!isEdit)
                SendInvitation();

            return string.Empty;
        }

        #endregion

        #region SendInvitation

        public void SendInvitation()
        {
            DateTime invitationExpirationDate;

            if (HasPassword)
                invitationExpirationDate = DateTime.Now.AddDays(2);
            else
                invitationExpirationDate = DateTime.Now.AddYears(5);

            PartnerInvitation invitation = new PartnerInvitation(
                Guid.NewGuid().ToString().Replace("-", string.Empty),
                PartnerId > 0 ? PartnerId : (int?)null, 
                Id, Email, invitationExpirationDate, false);
            BackgroundJobPending job = new BackgroundJobPending(0,
                HasPassword ? (int)BackgroundJobTypeEnum.PartnerSitePasswordReminder : (int)BackgroundJobTypeEnum.PartnerSiteInvitation, 
                null, null, null, invitation.InvitationKey);
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    PartnerInvitation.Insert(invitation, connection);
                    BackgroundJobPending.Insert(job, connection);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        #endregion

        #region Delete

        public void Delete()
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();

                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    List<PartnerInvitation> invitations = PartnerInvitation.FindByWebUser(Id, connection);
                    foreach (PartnerInvitation partnerInvitation in invitations)
                    {
                        BackgroundJobPending.DeleteByInvitation(partnerInvitation.InvitationKey, connection);
                        PartnerInvitation.Delete(partnerInvitation, connection);
                    }
                        
                    WebUser.Delete(new WebUser(Id), connection);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        #endregion
    }
}
