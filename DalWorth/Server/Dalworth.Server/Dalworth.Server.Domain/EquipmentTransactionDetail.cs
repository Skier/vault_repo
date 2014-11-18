using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Domain
{
    public partial class EquipmentTransactionDetail
    {
        public EquipmentTransactionDetail() { }

        #region EquipmentTransaction

        private EquipmentTransaction m_equipmentTransaction;
        public EquipmentTransaction EquipmentTransaction
        {
            get { return m_equipmentTransaction; }
            set { m_equipmentTransaction = value; }
        }

        #endregion

        #region WorkTransaction

        private WorkTransaction m_workTransaction;
        public WorkTransaction WorkTransaction
        {
            get { return m_workTransaction; }
            set { m_workTransaction = value; }
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

        #region Customer

        private Customer m_customer;
        public Customer Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        #endregion

        #region FindByTransaction

        private const string SqlFindByTransaction2 =
            @"SELECT * FROM EquipmentTransactionDetail
                where EquipmentTransactionId = ?EquipmentTransactionId
              order by VanId desc";

        public static List<EquipmentTransactionDetail> FindByTransaction(EquipmentTransaction transaction)
        {
            List<EquipmentTransactionDetail> result = new List<EquipmentTransactionDetail>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTransaction2))
            {
                Database.PutParameter(dbCommand, "?EquipmentTransactionId", transaction.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region IsTransactionExist

        private const string SqlIsTransactionExist =
            @"SELECT * FROM EquipmentTransactionDetail etd
                inner join EquipmentTransaction et on et.ID = etd.EquipmentTransactionId
                where SequenceDate = ?SequenceDate and {0}
                limit 1";

        public static bool IsTransactionExist(DateTime sequenceDate, int? vanId, int? addressId)
        {
            string query = string.Format(SqlIsTransactionExist,
                vanId.HasValue ? "VanId = " + vanId.Value : "AddressId = " + addressId.Value);

            using (IDbCommand dbCommand = Database.PrepareCommand(query))
            {
                Database.PutParameter(dbCommand, "?SequenceDate", sequenceDate);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    return dataReader.Read();
                }
            }
        }

        #endregion

        #region DeleteByTransaction

        private const string SqlDeleteByTransaction =
            @"DELETE FROM EquipmentTransactionDetail
                where EquipmentTransactionId = ?EquipmentTransactionId";

        public static void DeleteByTransaction(EquipmentTransaction transaction)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByTransaction))
            {
                Database.PutParameter(dbCommand, "?EquipmentTransactionId", transaction.ID);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region FindOnDate

        private const string SqlFindOnDate =
            @"SELECT etd.* FROM EquipmentTransactionDetail etd
                inner join EquipmentTransaction et on et.ID = etd.EquipmentTransactionId
                where et.SequenceDate <= ?SequenceDate and {0}
              order by et.SequenceDate desc, etd.EquipmentTransactionId desc, EquipmentTypeId
                limit 3";

        public static List<EquipmentTransactionDetail> FindOnDate(DateTime date, int? vanId, int? addressId,
            int? ignoreEquipmentTransactionId)
        {
            List<EquipmentTransactionDetail> result = new List<EquipmentTransactionDetail>();

            Debug.Assert(!vanId.HasValue || !addressId.HasValue, "VanId and AddressId cannot be specified simultaneously");
            string queryString = string.Format(SqlFindOnDate,
                (vanId.HasValue ? "VanId = " + vanId.Value : "AddressId = " + addressId.Value)
                + (ignoreEquipmentTransactionId.HasValue ? " and et.ID != " + ignoreEquipmentTransactionId.Value : string.Empty));

            using (IDbCommand dbCommand = Database.PrepareCommand(queryString))
            {
                Database.PutParameter(dbCommand, "?SequenceDate", date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            if (result.Count == 0)
                result.AddRange(GetEmptyDetails(vanId, addressId));
            return result;
        }

        #endregion

        #region FindVansEquipment

        private const string SqlFindVansEquipment =
            @"select etd.* from EquipmentTransaction et
                inner join EquipmentTransactionDetail etd on etd.EquipmentTransactionId = et.ID
                where Date(SequenceDate) = ?SequenceDate
                order by SequenceDate desc, VanId, EquipmentTypeId";

        //key - VanId, values - equipment quantity string
        public static Dictionary<int, string> FindVansEquipment(DateTime date)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            List<EquipmentTransactionDetail> equipmentDetails = new List<EquipmentTransactionDetail>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindVansEquipment))
            {
                Database.PutParameter(dbCommand, "?SequenceDate", date.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        equipmentDetails.Add(Load(dataReader));
                }
            }

            foreach (EquipmentTransactionDetail equipmentDetail in equipmentDetails)
            {
                if (!equipmentDetail.VanId.HasValue)
                    continue;
                if (result.ContainsKey(equipmentDetail.VanId.Value))
                    continue;

                int currentIndex = equipmentDetails.IndexOf(equipmentDetail);
                string equipmentString = string.Format("{0}/{1}/{2}", equipmentDetail.Quantity,
                    equipmentDetails[currentIndex + 1].Quantity, equipmentDetails[currentIndex + 2].Quantity);
                result.Add(equipmentDetail.VanId.Value, equipmentString);
            }

            return result;
        }

        #endregion

        #region GetEmptyDetails

        public static List<EquipmentTransactionDetail> GetEmptyDetails(int? vanId, int? addressId)
        {
            List<EquipmentTransactionDetail> result = new List<EquipmentTransactionDetail>();
            List<EquipmentType> equipmentTypes = EquipmentType.Find();
            foreach (EquipmentType equipmentType in equipmentTypes)
                result.Add(new EquipmentTransactionDetail(0, 0, equipmentType.ID, vanId, addressId, 0, 0));
            return result;
        }

        #endregion


        #region FindBy

        private const string SqlFindBy =
            @"SELECT etd.*, wt.* FROM EquipmentTransactionDetail etd
                inner join EquipmentTransaction et on etd.EquipmentTransactionId = et.ID
                inner join WorkTransaction wt on wt.ID = et.WorkTransactionId
                where SequenceDate >= ?SequenceDateStart and SequenceDate <= ?SequenceDateEnd
                    {0}
                order by SequenceDate";

        public static List<EquipmentTransactionDetail> FindBy(DateTime startDate, DateTime endDate, int? vanId, int? addressId,
            int? ignoreEquipmentTransactionId)
        {
            string queryString = string.Format(SqlFindBy, (vanId.HasValue ? " and VanId = " + vanId.Value : string.Empty)
                + (addressId.HasValue ? " and AddressId = " + addressId.Value : string.Empty)
                + (ignoreEquipmentTransactionId.HasValue ? " and et.ID != " + ignoreEquipmentTransactionId.Value : string.Empty));

            List<EquipmentTransactionDetail> result = new List<EquipmentTransactionDetail>();

            using (IDbCommand dbCommand = Database.PrepareCommand(queryString))
            {
                Database.PutParameter(dbCommand, "?SequenceDateStart", startDate);
                Database.PutParameter(dbCommand, "?SequenceDateEnd", endDate);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        EquipmentTransactionDetail detail = Load(dataReader);
                        detail.WorkTransaction = WorkTransaction.Load(dataReader, FieldsCount);
                        result.Add(detail);
                    }
                }
            }

            return result;
        }


        #endregion

        #region FindLatestOnVans

        private const string SqlFindLatestOnVans =
            @"select etd.*, Van.* from EquipmentTransaction et
                inner join EquipmentTransactionDetail etd on etd.EquipmentTransactionId = et.ID
                inner join
                (SELECT VanId, max(et2.SequenceDate) SequenceDate FROM EquipmentTransactionDetail etd2
                inner join EquipmentTransaction et2 on et2.ID = etd2.EquipmentTransactionId
                where VanId is not null
                group by VanId) SequenceLast on et.SequenceDate = SequenceLast.SequenceDate and etd.VanId = SequenceLast.VanId
                inner join Van on etd.VanId = Van.ID
                order by etd.EquipmentTransactionId, etd.EquipmentTypeId";

        public static List<EquipmentTransactionDetail> FindLatestOnVans()
        {
            List<EquipmentTransactionDetail> result = new List<EquipmentTransactionDetail>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLatestOnVans))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        EquipmentTransactionDetail detail = Load(dataReader);
                        detail.Van = Van.Load(dataReader, FieldsCount);
                        result.Add(detail);
                    }                        
                }
            }

            return result;
        }


        #endregion

        #region FindLatestOnCustomers

        private const string SqlFindLatestOnCustomers =
            @"select etd.*, c.* from EquipmentTransaction et
                inner join EquipmentTransactionDetail etd on etd.EquipmentTransactionId = et.ID
                inner join
                (SELECT AddressId, max(et2.SequenceDate) SequenceDate FROM EquipmentTransactionDetail etd2
                inner join EquipmentTransaction et2 on et2.ID = etd2.EquipmentTransactionId
                where AddressId is not null
                group by AddressId) SequenceLast on et.SequenceDate = SequenceLast.SequenceDate and etd.AddressId = SequenceLast.AddressId
                inner join Customer c on etd.AddressId = c.AddressId
                order by etd.EquipmentTransactionId, etd.EquipmentTypeId";

        public static List<EquipmentTransactionDetail> FindLatestOnCustomers()
        {
            List<EquipmentTransactionDetail> result = new List<EquipmentTransactionDetail>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLatestOnCustomers))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        EquipmentTransactionDetail detail = Load(dataReader);
                        detail.Customer = Customer.Load(dataReader, FieldsCount);
                        result.Add(detail);                        
                    }                        
                }
            }

            return result;
        }


        #endregion
    }
}
      