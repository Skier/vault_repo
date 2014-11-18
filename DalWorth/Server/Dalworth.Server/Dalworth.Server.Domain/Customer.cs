
using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using Dalworth.Server.Servman.Domain;

namespace Dalworth.Server.Domain
{
    public enum SearchPreferredCriteria
    {
        Name,
        Phone,
        Address
    }

    public partial class Customer
    {
        public Customer()
        {
            m_servmanCustId = string.Empty;
            m_firstName = string.Empty;
            m_lastName = string.Empty;
            m_phone1 = string.Empty;
            m_phone2 = string.Empty;
            m_email = string.Empty;
        }

        #region Phone1Formatted

        public string Phone1Formatted
        {
            get
            {
                return Utils.FormatPhone(Phone1);
            }
        }

        #endregion

        #region Phone2Formatted

        public string Phone2Formatted
        {
            get
            {
                return Utils.FormatPhone(Phone2);
            }
        }

        #endregion

        #region PhonesText

        public string PhonesText
        {
            get
            {
                string result = string.Empty;

                if (Phone1 != string.Empty)
                    result += "HM " + Phone1Formatted + "  ";

                if (Phone2 != string.Empty)
                    result += "BS " + Phone2Formatted;

                return result.Trim();
            }
        }

        #endregion


        #region CustomerType

        [XmlIgnore]
        public CustomerTypeEnum? CustomerType
        {
            get { return (CustomerTypeEnum?)m_customerTypeId; }
            set { m_customerTypeId = (byte?)value; }
        }

        #endregion

        #region DisplayName

        public string DisplayName
        {
            get { return Utils.JoinStrings(", ", m_lastName, m_firstName); }
        }

        #endregion

        #region FindBy ServmanCustId

        private const string SqlFindByServmanCustId =
            @"SELECT *
            FROM Customer
                WHERE ServmanCustId = ?ServmanCustId";

        public static Customer FindBy(string servmanCustId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByServmanCustId))
            {
                Database.PutParameter(dbCommand, "?ServmanCustId", servmanCustId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                  if(dataReader.Read())
                    return Load(dataReader);            
                }
            }

            throw new DataNotFoundException("Customer not found");
        }

        #endregion

        #region FindBy Visit

        private const string SqlFindByVisit =
            @"SELECT *
            FROM Customer
                WHERE ID = ?CustomerId";

        public static Customer FindBy(Visit visit)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByVisit))
            {
                Database.PutParameter(dbCommand, "?CustomerId", visit.CustomerId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("Customer not found");
        }

        #endregion

        #region FindBy Area

        private const string SqlFindByArea =
            @"(SELECT c.* FROM address a
                inner join Customer c on a.ID = c.AddressId
                where a.AreaId = ?AreaId)
                union
                (SELECT c.* FROM address a
                inner join CustomerAddressAdditional caa on caa.AddressId = a.ID
                inner join Customer c on caa.CustomerId = c.ID
                where a.AreaId = ?AreaId)";

        public static List<Customer> FindBy(Area area)
        {
            List<Customer> result = new List<Customer>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByArea))
            {
                Database.PutParameter(dbCommand, "?AreaId", area.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while(dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region FindCustomerAndAddresses

        public static List<CustomerAndAddress> FindCustomerAndAddresses(SearchPreferredCriteria criteria, 
            string firstName, string lastName, string phoneHome, string phoneBusiness, string block, string street, string zip)
        {
            string SqlFindCustomerAndAddresses =
                @"SELECT * {0} FROM Customer c
                    inner join Address a on c.AddressId = a.ID
                  where 1=1 ";

            List<CustomerAndAddress> result = new List<CustomerAndAddress>();

            //Prepare matches
            string matchesSelectSection = string.Empty;

            if (firstName != string.Empty)
                matchesSelectSection += ", IF(FirstName like ?FirstName, true, false) FirstNameMatch";
            else
                matchesSelectSection += ", false FirstNameMatch";

            if (lastName != string.Empty)
                matchesSelectSection += ", IF(LastName like ?LastNameNotExact, true, false) LastNameMatch";
            else
                matchesSelectSection += ", false LastNameMatch";


            if (phoneHome != string.Empty || phoneBusiness != string.Empty)
            {
                string phoneHomeCondition = "Phone1 = ?PhoneHomeExact or Phone2 like ?PhoneHomeNotExact";
                string phoneBusinessCondition = "Phone1 = ?PhoneBusinessExact or Phone2 like ?PhoneBusinessNotExact";

                matchesSelectSection += string.Format(", IF({0}, true, false) PhoneMatch", 
                    Utils.JoinStrings(" or ",
                    phoneHome == string.Empty ? string.Empty : phoneHomeCondition,
                    phoneBusiness == string.Empty ? string.Empty : phoneBusinessCondition));

            } else
            {
                matchesSelectSection += ", false PhoneMatch";
            }

            if (zip != string.Empty)
                matchesSelectSection += ", IF(Zip = ?Zip, true, false) ZipMatch";
            else
                matchesSelectSection += ", false ZipMatch";

            if (block != string.Empty)
                matchesSelectSection += ", IF(Block = ?Block, true, false) BlockMatch";
            else
                matchesSelectSection += ", false BlockMatch";

            if (street != string.Empty)
                matchesSelectSection += ", IF(Street like ?Street, true, false) StreetMatch";
            else
                matchesSelectSection += ", false StreetMatch";

            SqlFindCustomerAndAddresses = string.Format(SqlFindCustomerAndAddresses, matchesSelectSection);

            //Prepare filters

            if (criteria == SearchPreferredCriteria.Name)
            {
                if (lastName != string.Empty)
                {
                    if (firstName == string.Empty)
                        SqlFindCustomerAndAddresses += " and (c.LastName like ?LastNameNotExact)";                    
                    else
                        SqlFindCustomerAndAddresses += " and (c.LastName = ?LastName)";                    
                }

                if (firstName != string.Empty)
                    SqlFindCustomerAndAddresses += " and (c.FirstName like ?FirstName)";

                SqlFindCustomerAndAddresses += " order by PhoneMatch desc, BlockMatch desc, ZipMatch desc, StreetMatch desc";

                if (lastName != string.Empty && firstName == string.Empty)
                    SqlFindCustomerAndAddresses += ", LastName";
                else if (firstName != string.Empty)
                    SqlFindCustomerAndAddresses += ", FirstName";

            } else if (criteria == SearchPreferredCriteria.Phone)
            {
                if (phoneHome != string.Empty || phoneBusiness != string.Empty)
                {
                    if (phoneHome != string.Empty)
                        SqlFindCustomerAndAddresses += " and  ((c.Phone1 = ?PhoneHomeExact or c.Phone2 like ?PhoneHomeNotExact)";

                    if (phoneBusiness == string.Empty)
                        SqlFindCustomerAndAddresses += ")";
                    else
                    {
                        if (phoneHome == string.Empty)
                            SqlFindCustomerAndAddresses += " and ";
                        else
                            SqlFindCustomerAndAddresses += " or ";

                        SqlFindCustomerAndAddresses += " (c.Phone1 = ?PhoneBusinessExact or c.Phone2 like ?PhoneBusinessNotExact)";

                        if (phoneHome != string.Empty)
                            SqlFindCustomerAndAddresses += ")";
                    }                    
                }

                SqlFindCustomerAndAddresses += " order by LastNameMatch desc, FirstNameMatch desc, BlockMatch desc, ZipMatch desc, StreetMatch desc";

            } else if (criteria == SearchPreferredCriteria.Address)
            {
                if (zip != string.Empty)
                    SqlFindCustomerAndAddresses += " and (a.Zip = ?Zip)";
                if (block != string.Empty)
                    SqlFindCustomerAndAddresses += " and (a.Block = ?Block)";
                if (street != string.Empty)
                    SqlFindCustomerAndAddresses += " and (a.Street like ?Street)";

                SqlFindCustomerAndAddresses += " order by LastNameMatch desc, FirstNameMatch desc, PhoneMatch desc";

                if (street != string.Empty)
                    SqlFindCustomerAndAddresses += ", Street";
            }

            //SqlFindCustomerAndAddresses += ", LastName, FirstName limit 100";
            SqlFindCustomerAndAddresses += " limit 30";

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindCustomerAndAddresses))
            {
                if (lastName != string.Empty)
                {
                    Database.PutParameter(dbCommand, "?LastName", lastName);
                    Database.PutParameter(dbCommand, "?LastNameNotExact", lastName + "%");                    
                }

                if (firstName != string.Empty)
                    Database.PutParameter(dbCommand, "?FirstName", firstName + "%" );
                if (phoneHome != string.Empty)
                {
                    Database.PutParameter(dbCommand, "?PhoneHomeExact", phoneHome);
                    Database.PutParameter(dbCommand, "?PhoneHomeNotExact", phoneHome + "%");
                }                    
                if (phoneBusiness != string.Empty)
                {
                    Database.PutParameter(dbCommand, "?PhoneBusinessExact", phoneBusiness);
                    Database.PutParameter(dbCommand, "?PhoneBusinessNotExact", phoneBusiness + "%");
                }                    
                if (zip != string.Empty)
                    Database.PutParameter(dbCommand, "?Zip", zip);
                if (block != string.Empty)
                    Database.PutParameter(dbCommand, "?Block", block);
                if (street != string.Empty)
                    Database.PutParameter(dbCommand, "?Street", street + "%");

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        CustomerAndAddress customer = new CustomerAndAddress(
                            Load(dataReader),
                            Address.Load(dataReader, FieldsCount));
                        result.Add(customer);
                    }                        
                }
            }

            return result;
        }

        #endregion

        #region ConvertFromServman

        public static CustomerAndAddress ConvertFromServman(custmast servmanCustomer)
        {
            Customer customer = new Customer(0,
                servmanCustomer.cust_id.Trim(),
                null, 0,
                ServmanConversionUtil.GetFirstName(servmanCustomer.customer),
                ServmanConversionUtil.GetLastName(servmanCustomer.customer), 
                servmanCustomer.home_phone.Trim(),
                servmanCustomer.bus_phone.Trim(),
                servmanCustomer.emailaddr.Trim(),
                servmanCustomer.l_contact > servmanCustomer.l_addr_chg ? servmanCustomer.l_contact : servmanCustomer.l_addr_chg,
                null);

            if (servmanCustomer.cust_type == "R")
                customer.CustomerType = CustomerTypeEnum.Residential;
            else if (servmanCustomer.cust_type == "C")
                customer.CustomerType = CustomerTypeEnum.Business;            

            Address address = new Address(0,
                ServmanConversionUtil.GetArea(servmanCustomer.area_id),
                servmanCustomer.block.Trim(),
                servmanCustomer.prefix.Trim(),
                servmanCustomer.street.Trim(),
                servmanCustomer.suffix.Trim(),
                servmanCustomer.unit.Trim(),
                servmanCustomer.address2.Trim(),
                servmanCustomer.city.Trim(),
                servmanCustomer.state.Trim(),
                servmanCustomer.ZipParsed,
                ServmanConversionUtil.GetMapPage(servmanCustomer.grid),
                ServmanConversionUtil.GetMapLetter(servmanCustomer.grid),
                servmanCustomer.l_contact > servmanCustomer.l_addr_chg ? servmanCustomer.l_contact : servmanCustomer.l_addr_chg);

            return new CustomerAndAddress(customer, address);
        }

        public static Address ConvertFromServman(m_alt_ad alternativeAddress)
        {
            return new Address(0,
                ServmanConversionUtil.GetArea(alternativeAddress.area_id),
                alternativeAddress.block.Trim(),
                alternativeAddress.prefix.Trim(),
                alternativeAddress.street.Trim(),
                alternativeAddress.suffix.Trim(),
                alternativeAddress.unit.Trim(),
                alternativeAddress.address2.Trim(),
                alternativeAddress.city.Trim(),
                alternativeAddress.state.Trim(),
                alternativeAddress.ZipParsed,
                ServmanConversionUtil.GetMapPage(alternativeAddress.grid),
                ServmanConversionUtil.GetMapLetter(alternativeAddress.grid),
                DateTime.Now);
        }

        #endregion

        #region FindToBeSynchronized

        private const string SqlFindToBeSynchronized =
            @"select * from Customer c
                inner join Address a on c.AddressId = a.ID
                where (c.ServmanCustId = '') or (c.LastSyncDate is null)
                      or (c.Modified > c.LastSyncDate) or (a.Modified > c.LastSyncDate)";

        public static List<CustomerAndAddress> FindToBeSynchronized()
        {
            List<CustomerAndAddress> result = new List<CustomerAndAddress>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindToBeSynchronized))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Customer customer = Load(dataReader);
                        Address address = Address.Load(dataReader, FieldsCount);
                        result.Add(new CustomerAndAddress(customer, address));
                    }
                }
            }

            return result;
        }

        #endregion 
    }

    public class CustomerAndAddress 
    {
        private Customer m_customer;
        private Address m_address;

        #region Customer

        public Customer Customer
        {
            get { return m_customer; }
        }

        #endregion

        #region Address

        public Address Address
        {
            get { return m_address; }
        }

        #endregion

        #region Constructor

        public CustomerAndAddress(Customer customer, Address address)
        {
            m_customer = customer;
            m_address = address;
        }

        #endregion

        #region Name

        public string Name
        {
            get { return m_customer.DisplayName; }
        }

        #endregion

        #region AddressText

        public string AddressText
        {
            get {
                return Utils.JoinStrings("\r\n", m_address.AddressFirstLine, m_address.AddressSecondLine);
            }
        }

        #endregion

        #region Mapsco

        public string Mapsco
        {
            get { return m_address.Map; }
        }

        #endregion

        #region Phone1

        public string Phone1
        {
            get { return m_customer.Phone1; }
        }

        #endregion

        #region Phone2

        public string Phone2
        {
            get { return m_customer.Phone2; }
        }

        #endregion

        #region Phone1Formatted

        public string Phone1Formatted
        {
            get { return m_customer.Phone1Formatted; }
        }

        #endregion

        #region Phone2Formatted

        public string Phone2Formatted
        {
            get { return m_customer.Phone2Formatted; }
        }

        #endregion

        #region PhoneFormatted

        public string PhoneFormatted
        {
            get
            {
                string homePhone = string.Empty;
                if (m_customer.Phone1 != string.Empty)
                    homePhone = "HM " + Phone1Formatted;

                string businessPhone = string.Empty;
                if (m_customer.Phone2 != string.Empty)
                    businessPhone = "BS  " + Phone2Formatted;

                return Utils.JoinStrings("\r\n", homePhone, businessPhone);
            }
        }

        #endregion

        #region SelectValue

        public string SelectValue
        {
            get { return "Select"; }
        }

        #endregion

        #region FirstName

        public string FirstName
        {
            get { return m_customer.FirstName; }
        }

        #endregion

        #region LastName

        public string LastName
        {
            get { return m_customer.LastName; }
        }

        #endregion

        #region Block

        public string Block
        {
            get { return m_address.Block; }
        }

        #endregion

        #region Street

        public string Street
        {
            get { return m_address.Street; }
        }

        #endregion

        #region Zip

        public int? Zip
        {
            get { return m_address.Zip; }
        }

        #endregion


        #region Equals

        protected bool Equals(CustomerAndAddress customerAndAddress)
        {
            if (customerAndAddress == null) return false;
            return Equals(m_customer.ID, customerAndAddress.m_customer.ID);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as CustomerAndAddress);
        }

        public override int GetHashCode()
        {
            return m_customer.ID.GetHashCode();
        }

        #endregion
    }
}
      