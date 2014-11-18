using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.Partner.Models
{
    public class Partners
    {
        public Partners() : this (false) {}

        public Partners(bool showInactive)
        {
            ShowInactive = showInactive;
            m_partners = Partner.Find(!ShowInactive);
        }

        #region PartnerList

        private List<Partner> m_partners;
        public List<Partner> PartnerList
        {
            get { return m_partners; }
        }

        #endregion

        public bool ShowInactive { get; set; }
    }

    public class Partner
    {
        #region Partner

        public Partner()
        {
        }

        public Partner(int id, string name, int usersCount, int trackingPhonesCount, bool isActive)
        {
            Id = id;
            Name = name;
            UsersCount = usersCount;
            TrackingPhonesCount = trackingPhonesCount;
            IsActive = isActive;
        }

        public Partner(int id)
        {
            Id = id;
            InitDetails();
        }

        #endregion

        #region Details

        private List<Phone> m_trackingPhones;
        public List<Phone> TrackingPhones
        {
            get { return m_trackingPhones; }
        }

        private List<Phone> m_personalPhones;
        public List<Phone> PersonalPhones
        {
            get { return m_personalPhones; }
        }

        private List<User> m_users;
        public List<User> Users
        {
            get { return m_users; }
        }

        private List<Domain.AdvertisingSource> m_advertisingSources;
        public List<Domain.AdvertisingSource> AdvertisingSources
        {
            get { return m_advertisingSources; }
        }

        #endregion


        #region Properties

        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Partner name")]
        public string Name { get; set; }

        public int UsersCount { get; set; }
        public int TrackingPhonesCount { get; set; }
        public bool IsActive { get; set; }

        public string BackLink { get; set; }

        public string UsersCountText
        {
            get
            {
                if (UsersCount == 1)
                    return "1 User";
                return UsersCount + " Users";
            }
        }

        public string TrackingPhonesCountText
        {
            get
            {
                if (TrackingPhonesCount == 1)
                    return "1 Tracking Phone";
                return TrackingPhonesCount + " Tracking Phones";                
            }
        }

        public string IsActiveText
        {
            get { return IsActive ? "Active" : "Inactive"; }
        }

        public string BackLinkText
        {
            get
            {
                if (string.IsNullOrEmpty(BackLink))
                    return "Back";
                if (BackLink.Contains("TrackingPhones"))
                    return "Back to Manage Tracking Phones";
                if (BackLink.Contains("Partners"))
                    return "Back to Manage Partners";
                return "Back";
            }
        }        

        #endregion

        #region Find

        private const String SqlFind = @"select os.ID, os.Name, count(ws.ID) as UsersCount, PhonesGroup.PhonesCount, os.Active from OrderSource os
            left join WebUser ws on ws.OrderSourceId = os.ID
            left join (select os.ID, count(tpos.TrackingPhoneId) PhonesCount from OrderSource os
              left join TrackingPhoneOrderSource tpos on tpos.OrderSourceId = os.ID
              group by os.ID) PhonesGroup on PhonesGroup.ID = os.ID
            {0}
              group by os.ID";

        public static List<Partner> Find(bool activeOnly)
        {
            List<Partner> result = new List<Partner>();

            string queryString;
            if (activeOnly)
                queryString = string.Format(SqlFind, "where os.Active = true");
            else
                queryString = string.Format(SqlFind, string.Empty);

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();

                using (IDbCommand dbCommand = Database.PrepareCommand(queryString, connection))
                {                    
                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                            result.Add(Load(dataReader));
                    }
                }
            }

            return result;
        }


        private static Partner Load(IDataReader dataReader)
        {
            return new Partner(
                dataReader.GetInt32(0),
                dataReader.GetString(1),
                dataReader.GetInt32(2),
                dataReader.GetInt32(3),
                dataReader.GetBoolean(4));
        }

        #endregion

        #region InitDetails

        public void InitDetails()
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();

                OrderSource orderSource = OrderSource.FindByPrimaryKey(Id);
                Name = orderSource.Name;
                IsActive = orderSource.Active;

                m_trackingPhones = new List<Phone>();
                foreach (Domain.TrackingPhone trackingPhone in Domain.TrackingPhone.FindByOrderSource(Id, connection))
                    m_trackingPhones.Add(new Phone(trackingPhone.ID, trackingPhone.Number));

                m_personalPhones = new List<Phone>();
                foreach (OrderSourceOwnPhone ownPhone in OrderSourceOwnPhone.FindByOrderSource(Id, connection))
                    m_personalPhones.Add(new Phone(ownPhone.ID, ownPhone.Number));

                m_users = User.Find(Id, connection);

                m_advertisingSources = Domain.AdvertisingSource.FindByPartner(Id, connection);                
            }
        }

        #endregion

        #region Save

        public void Save()
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                OrderSource orderSource = new OrderSource(0, null, Name, true);
                OrderSource.Insert(orderSource, connection);
            }
        }

        #endregion

        #region AssignTrackingPhone

        public void AssignTrackingPhone(int trackingPhoneId)
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                TrackingPhoneOrderSource orderSource = new TrackingPhoneOrderSource(trackingPhoneId, Id);
                TrackingPhoneOrderSource.Insert(orderSource, connection);
            }
        }

        #endregion

        #region UnassignTrackingPhone

        public void UnassignTrackingPhone(int trackingPhoneId)
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                TrackingPhoneOrderSource orderSource = new TrackingPhoneOrderSource(trackingPhoneId, Id);
                TrackingPhoneOrderSource.Delete(orderSource, connection);
            }
        }

        #endregion

        #region UnassignAdvertisingSource

        public void UnassignAdvertisingSource(int advertisingSourceId)
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                OrderSourceAdvertisingSource.Delete(new OrderSourceAdvertisingSource(Id, advertisingSourceId), connection);
            }
        }

        #endregion

        #region AssignAdvertisingSource

        public string AssignAdvertisingSource(AdvertisingSource advertisingSource)
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();

                if (advertisingSource.TrackingUrl == null)
                    advertisingSource.TrackingUrl = string.Empty;

                List<Domain.AdvertisingSource> sources = Domain.AdvertisingSource.FindByAcronym(
                    advertisingSource.AdvertisingSourceCode, connection);
                if (sources.Count == 0)
                    return string.Format("Advertising source with acronym {0} not found",
                        advertisingSource.AdvertisingSourceCode);
                foreach (var source in sources)
                {
                    if (source.IsActive)
                    {                        
                        if (OrderSource.FindByAdSource(source.ID, connection) != null)
                            return string.Format("Advertising source {0} is already assigned to another partner",
                                                 advertisingSource.AdvertisingSourceCode);

                        source.TrackingUrl = advertisingSource.TrackingUrl;
                        OrderSourceAdvertisingSource osas = new OrderSourceAdvertisingSource(advertisingSource.PartnerId, source.ID);
                        IDbTransaction transaction = connection.BeginTransaction();
                        try
                        {                            
                            Domain.AdvertisingSource.Update(source, connection);
                            OrderSourceAdvertisingSource.Insert(osas, connection);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                        return string.Empty;
                    }
                }

                return string.Format("Advertising source with acronym {0} is inactive",
                        advertisingSource.AdvertisingSourceCode);
            }
        }

        #endregion

        #region AddPersonalPhone

        public void AddPersonalPhone(Phone phone)
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                OrderSourceOwnPhone ownPhone = new OrderSourceOwnPhone(0, Id, 
                    string.Join(null, System.Text.RegularExpressions.Regex.Split(phone.Number, "[^\\d]")));
                OrderSourceOwnPhone.Insert(ownPhone, connection);
            }
        }

        #endregion

        #region DeletePersonalPhone

        public void DeletePersonalPhone(int personalPhoneId)
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                OrderSourceOwnPhone ownPhone = OrderSourceOwnPhone.FindByPrimaryKey(personalPhoneId, connection);
                OrderSourceOwnPhone.Delete(ownPhone, connection);
            }
        }

        #endregion

        #region SetActivityStatus

        public void SetActivityStatus(bool isActive)
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                OrderSource partner = OrderSource.FindByPrimaryKey(Id, connection);
                partner.Active = isActive;
                OrderSource.Update(partner, connection);
                IsActive = isActive;
            }            
        }

        #endregion
    }

    public class Phone
    {
        public Phone()
        {
        }

        public Phone(int id, string number)
        {
            Id = id;
            Number = number;
        }        
        
        public Phone(int id, string number, int partnerId) 
        {
            Id = id;
            Number = number;
            m_partnerId = partnerId;
        }

        private int m_partnerId;
        public int PartnerId
        {
            get { return m_partnerId; }
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Phone number")]
        [RegularExpression(@"^([0-9]( |-)?)?(\(?[0-9]{3}\)?|[0-9]{3})( |-)?([0-9]{3}( |-)?[0-9]{4}|[a-zA-Z0-9]{7})$",
            ErrorMessage = "Invalid Phone number")]
        public string Number { get; set; }

        #region NumberText

        public string NumberText
        {
            get
            {
                try
                {
                    return String.Format("{0:(###) ###-####}", Int64.Parse(Number));
                }
                catch (FormatException)
                {
                    return Number;
                }
            }
        }

        #endregion
    }
}
