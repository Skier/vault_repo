using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using System.Xml;
using System.Xml.Serialization;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBResponseVendorReader : QBResponseReader<Vendor>    
    {
        #region Convert

        protected override Vendor Convert(object item)
        {
            VendorRet vendorRet = (VendorRet)item;

            if (vendorRet.VendorAddress == null)
                vendorRet.VendorAddress = new VendorAddress();


            Vendor vendor =
                new Vendor(
                    null,
                    0,
                    null,
                    vendorRet.ListId,
                    vendorRet.EditSequence,
                    vendorRet.Name,
                    vendorRet.CompanyName,
                    vendorRet.Salutation,
                    vendorRet.FirstName,
                    vendorRet.MiddleName,
                    vendorRet.LastName,
                    vendorRet.Suffix,
                    vendorRet.VendorAddress.Addr1,
                    vendorRet.VendorAddress.Addr2,
                    vendorRet.VendorAddress.Addr3,
                    vendorRet.VendorAddress.Addr4,
                    vendorRet.VendorAddress.City,
                    vendorRet.VendorAddress.State,
                    vendorRet.VendorAddress.PostalCode,
                    vendorRet.VendorAddress.Country,
                    vendorRet.Phone,
                    vendorRet.Mobile,
                    vendorRet.Pager,
                    vendorRet.AltPhone,
                    vendorRet.Fax,
                    vendorRet.Email,
                    vendorRet.NameOnCheck,
                    vendorRet.VendorTaxIdent,
                    vendorRet.IsVendorEligibleFor1099,
                    vendorRet.Balance
                );

            return vendor;
        }

        #endregion

        #region ProcessResponse

        protected override void ProcessResponse(QBAffectedObject<Vendor> item)
        {
            QBAffectedObject<Vendor> expectedVendor = null;
            if (item.CommandType == QBCommandTypeEnum.Add || item.CommandType == QBCommandTypeEnum.Update)
            {
                try
                {
                    expectedVendor = new QBAffectedObject<Vendor>(Vendor.FindByPrimaryKey(item.RequestId), item.RequestId);
                }
                catch (DataNotFoundException)
                {
                    throw new QuickBooksAgentException("Expected DB object not found");
                }
            }            

            
            #region Process added Vendor

            if (item.CommandType == QBCommandTypeEnum.Add)
            {
                Vendor vendor = item.DomainObject;

                expectedVendor.DomainObject.EditSequence = vendor.EditSequence;
                expectedVendor.DomainObject.QuickBooksListId = vendor.QuickBooksListId;
                expectedVendor.DomainObject.EntityState = EntityState.Synchronized;

                Vendor.Update(expectedVendor.DomainObject);

                QBEntity qbEntity = new QBEntity(QBEntityType.Vendor, (int)vendor.QuickBooksListId);
                QBEntity.Insert(qbEntity);
            }
            #endregion

            #region Process updated Vendor

            if (item.CommandType == QBCommandTypeEnum.Update)
            {
                Vendor vendor = item.DomainObject;

                Vendor previousVendor
                    = Vendor.FindByPrimaryKey(expectedVendor.DomainObject.ModifiedVendorId.Value);

                Vendor.Delete(expectedVendor);

                expectedVendor.DomainObject.VendorId = previousVendor.VendorId;
                expectedVendor.DomainObject.EntityState = EntityState.Synchronized;
                expectedVendor.DomainObject.EditSequence = vendor.EditSequence;

                Vendor.Update(expectedVendor.DomainObject);
            }

            #endregion

            #region Process queried Vendor

            if (item.CommandType == QBCommandTypeEnum.Query)
            {
                Vendor vendor = item.DomainObject;
                
                try
                {
                    Vendor existingVendor = Vendor.FindBy((int)vendor.QuickBooksListId);
                    vendor.VendorId = existingVendor.VendorId;
                    vendor.EntityState = EntityState.Synchronized;
                    Vendor.Update(vendor);
                }
                catch (DataNotFoundException)
                {
                    Counter.Assign(vendor);

                    vendor.EntityState = EntityState.Synchronized;
                    Vendor.Insert(vendor);

                    QBEntity qbEntity = new QBEntity(QBEntityType.Vendor, (int)vendor.QuickBooksListId);
                    QBEntity.Insert(qbEntity);
                }
            }

            #endregion            
        }

        #endregion

        #region TargetNodeName
        protected override string TargetNodeName
        {
            get { return "VendorRet"; }
        }
        #endregion

        #region TargetClassType
        protected override Type TargetClassType
        {
            get { return typeof(VendorRet); }
        }
        #endregion

        #region IsRootNode

        public override bool IsRootNode(string nodeName)
        {
            return "VendorQueryRs".Equals(nodeName)
                   || "VendorModRs".Equals(nodeName)
                   || "VendorAddRs".Equals(nodeName);
        }

        #endregion        
        
        #region VendorRet

        [XmlRoot("VendorRet")]
        public class VendorRet : QBResponseItem
        {
            #region Name

            String m_name;
            [XmlElement("Name")]
            public String Name
            {
                get { return m_name; }
                set { m_name = value; }
            }

            #endregion
            
            #region CompanyName
            String m_companyName;
            [XmlElement("CompanyName")]
            public String CompanyName
            {
                get { return m_companyName; }
                set { m_companyName = value; }
            }
            #endregion            
            
            #region Salutation

            String m_salutation;
            [XmlElement("Salutation")]
            public String Salutation
            {
                get { return m_salutation; }
                set { m_salutation = value; }
            }

            #endregion

            #region FirstName
            String m_firstName;
            [XmlElement("FirstName")]
            public String FirstName
            {
                get { return m_firstName; }
                set { m_firstName = value; }
            }
            #endregion

            #region MiddleName
            String m_middleName;
            [XmlElement("MiddleName")]
            public String MiddleName
            {
                get { return m_middleName; }
                set { m_middleName = value; }
            }
            #endregion

            #region LastName
            String m_lastName;
            [XmlElement("LastName")]
            public String LastName
            {
                get { return m_lastName; }
                set { m_lastName = value; }
            }
            #endregion

            #region Suffix
            String m_suffix;
            [XmlElement("Suffix")]
            public String Suffix
            {
                get { return m_suffix; }
                set { m_suffix = value; }
            }
            #endregion

            #region VendorAddress
            VendorAddress m_vendorAddress;
            [XmlElement("VendorAddress")]
            public VendorAddress VendorAddress
            {
                get { return m_vendorAddress; }
                set { m_vendorAddress = value; }
            }
            #endregion
            
            #region Phone
            String m_phone;
            [XmlElement("Phone")]
            public String Phone
            {
                get { return m_phone; }
                set { m_phone = value; }
            }
            #endregion

            #region Mobile
            String m_mobile;
            [XmlElement("Mobile")]
            public String Mobile
            {
                get { return m_mobile; }
                set { m_mobile = value; }
            }
            #endregion

            #region Pager
            String m_pager;
            [XmlElement("Pager")]
            public String Pager
            {
                get { return m_pager; }
                set { m_pager = value; }
            }
            #endregion 

            #region AltPhone
            String m_altPhone;
            [XmlElement("AltPhone")]
            public String AltPhone
            {
                get { return m_altPhone; }
                set { m_altPhone = value; }
            }
            #endregion

            #region Fax
            String m_fax;
            [XmlElement("Fax")]
            public String Fax
            {
                get { return m_fax; }
                set { m_fax = value; }
            }
            #endregion

            #region Email
            String m_email;
            [XmlElement("Email")]
            public String Email
            {
                get { return m_email; }
                set { m_email = value; }
            }
            #endregion

            #region NameOnCheck
            String m_nameOnCheck;
            [XmlElement("NameOnCheck")]
            public String NameOnCheck
            {
                get { return m_nameOnCheck; }
                set { m_nameOnCheck = value; }
            }
            #endregion

            #region VendorTaxIdent
            String m_vendorTaxIdent;
            [XmlElement("VendorTaxIdent")]
            public String VendorTaxIdent
            {
                get { return m_vendorTaxIdent; }
                set { m_vendorTaxIdent = value; }
            }
            #endregion

            #region IsVendorEligibleFor1099
            bool m_isVendorEligibleFor1099;
            [XmlElement("IsVendorEligibleFor1099")]
            public bool IsVendorEligibleFor1099
            {
                get { return m_isVendorEligibleFor1099; }
                set { m_isVendorEligibleFor1099 = value; }
            }
            #endregion

            #region Balance
            decimal m_balance;
            [XmlElement("Balance")]
            public decimal Balance
            {
                get { return m_balance; }
                set { m_balance = value; }
            }
            #endregion
        }

        #endregion

        #region VendorAddress

        public class VendorAddress
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
