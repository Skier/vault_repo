using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class Address
    {
        public Address()
        {
            m_block = string.Empty;
            m_prefix = string.Empty;
            m_street = string.Empty;
            m_suffix = string.Empty;
            m_unit = string.Empty;
            m_address2 = string.Empty;
            m_city = string.Empty;
            m_state = string.Empty;
            m_mapPage = string.Empty;
            m_mapLetter = string.Empty;
        }

        #region Map

        public string Map
        {
            get { return MapPage + MapLetter; }
        }

        #endregion

        #region MapServmanFormat

        public string MapServmanFormat
        {
            get
            {
                if ((MapPage == null || MapPage.Trim() == string.Empty)
                    && (MapLetter == null || MapLetter.Trim() == string.Empty))
                {
                    return "9999";
                }

                return Map;
            }
        }

        #endregion

        #region Address1

        public string Address1
        {
            get { return Utils.JoinStrings(" ", Block, Prefix, Street, Suffix); }
        }

        #endregion 

        #region AddressFirstLine

        public string AddressFirstLine
        {
            get
            {
                string address1 = Utils.JoinStrings(" ", Block, Prefix, Street, Suffix);
                string address2 = Utils.JoinStrings(" ", Unit, Address2);
                return Utils.JoinStrings(", ", address1, address2);            
            }
        }

        #endregion

        #region AddressSecondLine

        public string AddressSecondLine
        {
            get
            {
                return Utils.JoinStrings(", ", City, State, Zip.ToString());
            }
        }

        #endregion

        #region AddressSingleLine

        public string AddressSingleLine
        {
            get
            {
                return Utils.JoinStrings("; ", AddressFirstLine, AddressSecondLine);
            }
            
        }

        #endregion

        #region FindBy Customer

        private const string SqlFindByCustomer =
            @"SELECT * FROM CustomerAddressAdditional caa
                inner join Address a on caa.AddressId = a.ID
                where CustomerId = ?CustomerId";

        public static List<Address> FindAdditionalBy(Customer customer)
        {
            List<Address> result = new List<Address>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByCustomer))
            {
                Database.PutParameter(dbCommand, "?CustomerId", customer.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader, CustomerAddressAdditional.FieldsCount));
                }
            }

            return result;
        }

        #endregion
    }
}
      