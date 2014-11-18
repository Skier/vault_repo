using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Web.Partner.Models
{
    public class TrackingPhones
    {
        private int m_partnerId;

        #region TrackingPhones

        public TrackingPhones()
        {
            m_trackingPhones = TrackingPhone.Find(false);
        }

        #endregion

        #region TrackingPhones

        public TrackingPhones(int partnerId, bool unassignedOnly)
        {
            m_trackingPhones = TrackingPhone.Find(unassignedOnly);
            m_partnerId = partnerId;
        }

        #endregion

        #region Phones

        private List<TrackingPhone> m_trackingPhones;
        public List<TrackingPhone> Phones
        {
            get { return m_trackingPhones; }
        }

        #endregion

        #region DeletePhone

        public void DeletePhone(int id)
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                Domain.TrackingPhone.Delete(new Domain.TrackingPhone(id), connection);
            }
                
            m_trackingPhones = TrackingPhone.Find(false);
        }

        #endregion
    }

    public class TrackingPhone
    {
        #region TrackingPhone

        public TrackingPhone()
        {
        }

        public TrackingPhone(int id, string phoneNumber, int? orderSourceId, string orderSource)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            OrderSourceId = orderSourceId;
            OrderSource = orderSource;
        }

        #endregion

        #region Properties

        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Phone number")]
        [RegularExpression(@"^([0-9]( |-)?)?(\(?[0-9]{3}\)?|[0-9]{3})( |-)?([0-9]{3}( |-)?[0-9]{4}|[a-zA-Z0-9]{7})$",
            ErrorMessage = "Invalid Phone number")]
        public string PhoneNumber { get; set; }

        public string PhoneNumberText
        {
            get
            {
                try
                {
                    return String.Format("{0:(###) ###-####}", Int64.Parse(PhoneNumber));
                }
                catch (FormatException)
                {
                    return PhoneNumber;
                }
            }
        }

        public int? OrderSourceId { get; set; }
        public string OrderSource { get; set; }
        public string OrderSourceText 
        { 
            get
            {
                if (!OrderSourceId.HasValue)
                    return "Unassigned";
                return OrderSource;
            }
        }

        #endregion

        #region Find

        private const String SqlFind = @"SELECT tp.ID, Number, OrderSourceId, Name FROM TrackingPhone tp
            left join TrackingPhoneOrderSource tpos on tpos.TrackingPhoneId = tp.ID
            left join OrderSource os on os.ID = tpos.OrderSourceId 
        {0}
        order by os.ID";

        public static List<TrackingPhone> Find(bool unassignedOnly)
        {
            List<TrackingPhone> result = new List<TrackingPhone>();

            string queryString;
            if (unassignedOnly)
                queryString = string.Format(SqlFind, " where OrderSourceId is null");
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


        private static TrackingPhone Load(IDataReader dataReader)
        {
            return new TrackingPhone(
                dataReader.GetInt32(0),
                dataReader.GetString(1),
                dataReader.IsDBNull(2) ? (int?)null : dataReader.GetInt32(2),
                dataReader.IsDBNull(3) ? string.Empty : dataReader.GetString(3));
        }

        #endregion

        #region Save

        public string Save()
        {
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();
                if (!string.IsNullOrEmpty(PhoneNumber))
                    PhoneNumber = string.Join(null, System.Text.RegularExpressions.Regex.Split(PhoneNumber, "[^\\d]"));

                if (Domain.TrackingPhone.FindByPhone(PhoneNumber, connection) != null)
                    return string.Format("Unable to add new Tracking phone. Phone number {0} is already exist.", PhoneNumberText);
                Domain.TrackingPhone trackingPhone = new Domain.TrackingPhone(0, PhoneNumber);                
                Domain.TrackingPhone.Insert(trackingPhone, connection);
            }

            return string.Empty;
        }

        #endregion
    }
}
