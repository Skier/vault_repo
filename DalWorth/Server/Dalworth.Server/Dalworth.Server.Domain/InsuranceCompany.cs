using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class InsuranceCompany
    {
        public InsuranceCompany() { }

        #region FindInsuranceCompanyAndAddresses

        public static List<InsuranceCompanyAndAddress> FindInsuranceCompanyAndAddresses(string name, string mapsco, string address)
        {
            string SqlFindInsuranceCompanyAndAddresses =
                @"SELECT * FROM InsuranceCompany c
                    left join Address a on c.AddressId = a.ID
                  where ";

            List<InsuranceCompanyAndAddress> result = new List<InsuranceCompanyAndAddress>();

            if (name != null && name != string.Empty)
                SqlFindInsuranceCompanyAndAddresses += " (c.Name like ?Name) and";
            if (mapsco != null && mapsco != string.Empty)
                SqlFindInsuranceCompanyAndAddresses += " a.Map like ?Map and";
            if (address != null && address != string.Empty)
                SqlFindInsuranceCompanyAndAddresses += "  (a.Address1 like ?Address or a.Address2 like ?Address or a.City like ?Address or a.State like ?Address or a.Zip like ?Address) and";
            SqlFindInsuranceCompanyAndAddresses += " 1=1";

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindInsuranceCompanyAndAddresses))
            {
                if (name != null && name != string.Empty)
                    Database.PutParameter(dbCommand, "?Name", name + "%");
                if (mapsco != null && mapsco != string.Empty)
                    Database.PutParameter(dbCommand, "?Map", mapsco + "%");
                if (address != null && address != string.Empty)
                    Database.PutParameter(dbCommand, "?Address", "%" + address + "%");

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        InsuranceCompanyAndAddress customer = new InsuranceCompanyAndAddress(
                            Load(dataReader),
                            Address.Load(dataReader, FieldsCount));
                        result.Add(customer);
                    }
                }
            }

            return result;
        }

        #endregion
    }

    public class InsuranceCompanyAndAddress
    {
        private InsuranceCompany m_insuranceCompany;
        private Address m_address;

        #region InsuranceCompany

        public InsuranceCompany InsuranceCompany
        {
            get { return m_insuranceCompany; }
        }

        #endregion

        #region Address

        public Address Address
        {
            get { return m_address; }
        }

        #endregion

        #region Constructor

        public InsuranceCompanyAndAddress(InsuranceCompany company, Address address)
        {
            m_insuranceCompany = company;
            m_address = address;
        }

        #endregion

        #region Name

        public string Name
        {
            get { return m_insuranceCompany.Name; }
        }

        #endregion

        #region AddressText

        public string AddressText
        {
            get { return m_address.AddressSingleLine; }
        }

        #endregion

        #region Mapsco

        public string Mapsco
        {
            get { return m_address.Map; }
        }

        #endregion

        #region Equals

        protected bool Equals(InsuranceCompanyAndAddress companyAndAddress)
        {
            if (companyAndAddress == null)
                return false;
            return Equals(m_insuranceCompany.ID, companyAndAddress.m_insuranceCompany.ID);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            return Equals(obj as InsuranceCompanyAndAddress);
        }

        public override int GetHashCode()
        {
            return m_insuranceCompany.ID.GetHashCode();
        }

        #endregion
    }
}