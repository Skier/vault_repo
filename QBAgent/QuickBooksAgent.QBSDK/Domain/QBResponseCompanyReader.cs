using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using System.Xml;
using System.Xml.Serialization;


namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBResponseCompanyReader : QBResponseReader<Company>
    {
        #region Convert

        protected override Company Convert(object item)
        {
            CompanyRet companyRet = (CompanyRet)item;

            if (companyRet.Address == null)
                companyRet.Address = new Address();
            
            if (companyRet.LegalAddress == null)
                companyRet.LegalAddress = new Address();
            
            if (companyRet.ForCustomerAddress == null)
                companyRet.ForCustomerAddress = new Address();

            Company company = new Company(
                0,
                companyRet.IsSampleCompany,
                companyRet.CompanyName,
                companyRet.LegalCompanyName,
                companyRet.Address.Addr1,
                companyRet.Address.Addr2,
                companyRet.Address.Addr3,
                companyRet.Address.Addr4,
                companyRet.Address.City,
                companyRet.Address.State,
                companyRet.Address.PostalCode,
                companyRet.Address.Country,
                companyRet.LegalAddress.Addr1,
                companyRet.LegalAddress.Addr2,
                companyRet.LegalAddress.Addr3,
                companyRet.LegalAddress.Addr4,
                companyRet.LegalAddress.City,
                companyRet.LegalAddress.State,
                companyRet.LegalAddress.PostalCode,
                companyRet.LegalAddress.Country,
                companyRet.ForCustomerAddress.Addr1,
                companyRet.ForCustomerAddress.Addr2,
                companyRet.ForCustomerAddress.Addr3,
                companyRet.ForCustomerAddress.Addr4,
                companyRet.ForCustomerAddress.City,
                companyRet.ForCustomerAddress.State,
                companyRet.ForCustomerAddress.PostalCode,
                companyRet.ForCustomerAddress.Country,                                
                companyRet.Phone,
                companyRet.Email,
                companyRet.CompanyEmailForCustomer,
                companyRet.FirstMonthFiscalYear,
                companyRet.FirstMonthIncomeTaxYear,
                companyRet.CompanyType);

            return company;
        }

        #endregion

        #region ProcessResponse

        protected override void ProcessResponse(QBAffectedObject<Company> item)
        {            
            #region Process queried Company

            if (item.CommandType == QBCommandTypeEnum.Query)
            {
                Company company = item.DomainObject;
                
                try
                {
                    Company existingCompany = Company.FindByPrimaryKey(0);
                    company.CompanyId = existingCompany.CompanyId;
                    Company.Update(company);
                }
                catch (DataNotFoundException)
                {
                    company.CompanyId = 0;
                    Company.Insert(company);                    
                }   
            }

            #endregion
        }

        #endregion
        
        #region TargetNodeName
        
        protected override string TargetNodeName
        {
            get { return "CompanyRet"; }
        }
        
        #endregion

        #region TargetClassType
        
        protected override Type TargetClassType
        {
            get { return typeof(CompanyRet); }
        }
        
        #endregion

        #region IsRootNode

        public override bool IsRootNode(string nodeName)
        {
            return "CompanyQueryRs".Equals(nodeName);
        }

        #endregion                
        
        #region CompanyRet

        [XmlRoot("CompanyRet")]
        public class CompanyRet : QBResponseItem
        {
            #region IsSampleCompany

            bool m_isSampleCompany;
            [XmlElement("IsSampleCompany")]
            public bool IsSampleCompany
            {
                get { return m_isSampleCompany; }
                set { m_isSampleCompany = value; }
            }

            #endregion

            #region CompanyName

            string m_companyName;
            [XmlElement("CompanyName")]
            public string CompanyName
            {
                get { return m_companyName; }
                set { m_companyName = value; }
            }

            #endregion

            #region LegalCompanyName

            string m_legalCompanyName;
            [XmlElement("LegalCompanyName")]
            public string LegalCompanyName
            {
                get { return m_legalCompanyName; }
                set { m_legalCompanyName = value; }
            }

            #endregion

            #region Phone

            string m_phone;
            [XmlElement("Phone")]
            public string Phone
            {
                get { return m_phone; }
                set { m_phone = value; }
            }

            #endregion

            #region Email

            string m_email;
            [XmlElement("Email")]
            public string Email
            {
                get { return m_email; }
                set { m_email = value; }
            }

            #endregion

            #region CompanyEmailForCustomer

            string m_companyEmailForCustomer;
            [XmlElement("CompanyEmailForCustomer")]
            public string CompanyEmailForCustomer
            {
                get { return m_companyEmailForCustomer; }
                set { m_companyEmailForCustomer = value; }
            }

            #endregion

            #region FirstMonthFiscalYear

            string m_firstMonthFiscalYear;
            [XmlElement("FirstMonthFiscalYear")]
            public string FirstMonthFiscalYear
            {
                get { return m_firstMonthFiscalYear; }
                set { m_firstMonthFiscalYear = value; }
            }

            #endregion

            #region FirstMonthIncomeTaxYear

            string m_firstMonthIncomeTaxYear;
            [XmlElement("FirstMonthIncomeTaxYear")]
            public string FirstMonthIncomeTaxYear
            {
                get { return m_firstMonthIncomeTaxYear; }
                set { m_firstMonthIncomeTaxYear = value; }
            }

            #endregion

            #region CompanyType

            string m_companyType;
            [XmlElement("CompanyType")]
            public string CompanyType
            {
                get { return m_companyType; }
                set { m_companyType = value; }
            }

            #endregion

            #region Address

            private Address m_address;
            [XmlElement("Address")]
            public Address Address
            {
                get { return m_address; }
                set { m_address = value; }
            }

            #endregion

            #region LegalAddress

            private Address m_legalAddress;
            [XmlElement("LegalAddress")]
            public Address LegalAddress
            {
                get { return m_legalAddress; }
                set { m_legalAddress = value; }
            }

            #endregion

            #region ForCustomerAddress

            private Address m_forCustomerAddress;
            [XmlElement("CompanyAddressForCustomer")]
            public Address ForCustomerAddress
            {
                get { return m_forCustomerAddress; }
                set { m_forCustomerAddress = value; }
            }

            #endregion
        }

        #endregion
        
        #region Address

        public class Address
        {
            #region Addr1
            string m_addr1;
            [XmlElement("Addr1")]
            public string Addr1
            {
                get { return m_addr1; }
                set { m_addr1 = value; }
            }
            #endregion

            #region Addr2
            string m_addr2;
            [XmlElement("Addr2")]
            public string Addr2
            {
                get { return m_addr2; }
                set { m_addr2 = value; }
            }
            #endregion

            #region Addr3
            string m_addr3;
            [XmlElement("Addr3")]
            public string Addr3
            {
                get { return m_addr3; }
                set { m_addr3 = value; }
            }
            #endregion

            #region Addr4
            string m_addr4;
            [XmlElement("Addr4")]
            public string Addr4
            {
                get { return m_addr4; }
                set { m_addr4 = value; }
            }
            #endregion

            #region City
            string m_city;
            [XmlElement("City")]
            public string City
            {
                get { return m_city; }
                set { m_city = value; }
            }
            #endregion

            #region State
            string m_state;
            [XmlElement("State")]
            public string State
            {
                get { return m_state; }
                set { m_state = value; }
            }
            #endregion

            #region PostalCode
            string m_postalCode;
            [XmlElement("PostalCode")]
            public string PostalCode
            {
                get { return m_postalCode; }
                set { m_postalCode = value; }
            }
            #endregion

            #region Country
            string m_country;
            [XmlElement("Country")]
            public string Country
            {
                get { return m_country; }
                set { m_country = value; }
            }
            #endregion
        }

        #endregion
    }
}
