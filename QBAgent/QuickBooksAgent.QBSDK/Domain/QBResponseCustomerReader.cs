using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using System.Xml;
using System.Xml.Serialization;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBResponseCustomerReader:QBResponseReader<Customer>
    {
        const String TARGET_NODE = "CustomerRet";

        #region Convert
        protected override Customer Convert(object item)
        {
            CustomerRet customerRet = (CustomerRet)item;

            Customer customer 
                = 
                    new Customer(null,
                    0,
                    null,
                    customerRet.ListId,
                    null,
                    customerRet.EditSequence,
                    customerRet.FullName,
                    customerRet.Salutation ?? String.Empty,
                    customerRet.FirstName ?? String.Empty,
                    customerRet.MiddleName ?? String.Empty,
                    customerRet.LastName ?? String.Empty,
                    customerRet.Suffix ?? String.Empty,
                    customerRet.CompanyName,
                    customerRet.Phone,
                    customerRet.Mobile,
                    customerRet.Email,
                    customerRet.Pager,
                    customerRet.AltPhone,
                    customerRet.Fax,
                    customerRet.Balance,
                    null,
                    customerRet.ResaleNumber,
                    customerRet.DeliveryMethod
                );
            
            if (customerRet.TermsRef != null)
            {
                customer.Terms = new Terms();
                customer.Terms.QuickBooksListId = customerRet.TermsRef.ListId;
            }

            customer.PrintAs = customerRet.PrintAs;
            customer.Name = customerRet.Name;

            if (customerRet.BillAddress != null)
            {
                ((Customer) customer).BillAddr1 = customerRet.BillAddress.Addr1;
                ((Customer) customer).BillAddr2 = customerRet.BillAddress.Addr2;
                ((Customer) customer).BillAddr3 = customerRet.BillAddress.Addr3;
                ((Customer) customer).BillAddr4 = customerRet.BillAddress.Addr4;
                ((Customer)customer).BillCity = customerRet.BillAddress.City;
                ((Customer)customer).BillState = customerRet.BillAddress.State;
                ((Customer)customer).BillPostalCode = customerRet.BillAddress.PostalCode;
                ((Customer)customer).BillCountry = customerRet.BillAddress.Country;
            }

            if (customerRet.ShipAddress != null)
            {
                ((Customer)customer).ShipAddr1 = customerRet.ShipAddress.Addr1;
                ((Customer)customer).ShipAddr2 = customerRet.ShipAddress.Addr2;
                ((Customer)customer).ShipAddr3 = customerRet.ShipAddress.Addr3;
                ((Customer)customer).ShipAddr4 = customerRet.ShipAddress.Addr4;
                ((Customer)customer).ShipCity = customerRet.ShipAddress.City;
                ((Customer)customer).ShipState = customerRet.ShipAddress.State;
                ((Customer)customer).ShipPostalCode = customerRet.ShipAddress.PostalCode;
                ((Customer)customer).ShipCountry = customerRet.ShipAddress.Country;
            }

            ((Customer)customer).ShippingAddressSameAsBilling = (customerRet.ShipAddress == null);

            return customer;
        }
        #endregion

        protected override void ProcessResponse(QBAffectedObject<Customer> item)
        {                        
            #region Process added customers

            if (item.CommandType == QBCommandTypeEnum.Add)
            {
                QBAffectedObject<Customer> expectedCustomer;
                try
                {
                    expectedCustomer = new QBAffectedObject<Customer>(Customer.FindByPrimaryKey(item.RequestId), item.RequestId);
                }
                catch (DataNotFoundException)
                {
                    throw new QuickBooksAgentException("Expected DB object not found");
                }
                
                Customer customer = item.DomainObject;
                                               
                expectedCustomer.DomainObject.EditSequence = customer.EditSequence;
                expectedCustomer.DomainObject.QuickBooksListId = customer.QuickBooksListId;
                expectedCustomer.DomainObject.EntityState = EntityState.Synchronized;

                Customer.Update(expectedCustomer.DomainObject);

                QBEntity.Insert(
                    new QBEntity(QBEntityType.Customer, (int)customer.QuickBooksListId));
            }

            #endregion

            #region Process updated customers

            if (item.CommandType == QBCommandTypeEnum.Update)
            {
                QBAffectedObject<Customer> expectedCustomer;
                try
                {
                    Customer temporaryCustomer = Customer.FindByPrimaryKey(item.RequestId);
                    expectedCustomer = new QBAffectedObject<Customer>(
                        Customer.FindByPrimaryKey(temporaryCustomer.ModifiedCustomerId.Value), item.RequestId);
                    Customer.Delete(temporaryCustomer);
                }
                catch (DataNotFoundException)
                {
                    throw new QuickBooksAgentException("Expected DB object not found");
                }
                
                Customer existingCustomer = expectedCustomer.DomainObject;

                existingCustomer.QuickBooksListId = item.DomainObject.QuickBooksListId;
                existingCustomer.EntityState = EntityState.Synchronized;
                existingCustomer.Terms = Terms.FindByQuickBooksId(item.DomainObject.Terms.QuickBooksListId);
                existingCustomer.EditSequence = item.DomainObject.EditSequence;
                existingCustomer.Name = item.DomainObject.Name;
                existingCustomer.FullName = item.DomainObject.FullName;
                existingCustomer.Salutation = item.DomainObject.Salutation;
                existingCustomer.FirstName = item.DomainObject.FirstName;
                existingCustomer.MiddleName = item.DomainObject.MiddleName;
                existingCustomer.LastName = item.DomainObject.LastName;
                existingCustomer.Suffix = item.DomainObject.Suffix;
                existingCustomer.CompanyName = item.DomainObject.CompanyName;
                existingCustomer.Phone = item.DomainObject.Phone;
                existingCustomer.Mobile = item.DomainObject.Mobile;
                existingCustomer.Email = item.DomainObject.Email;
                existingCustomer.Pager = item.DomainObject.Pager;
                existingCustomer.AltPhone = item.DomainObject.AltPhone;
                existingCustomer.Fax = item.DomainObject.Fax;
                existingCustomer.BillAddr1 = item.DomainObject.BillAddr1;
                existingCustomer.BillAddr2 = item.DomainObject.BillAddr2;
                existingCustomer.BillAddr3 = item.DomainObject.BillAddr3;
                existingCustomer.BillAddr4 = item.DomainObject.BillAddr4;
                existingCustomer.BillCity = item.DomainObject.BillCity;
                existingCustomer.BillState = item.DomainObject.BillState;
                existingCustomer.BillPostalCode = item.DomainObject.BillPostalCode;
                existingCustomer.BillCountry = item.DomainObject.BillCountry;
                existingCustomer.ShipAddr1 = item.DomainObject.ShipAddr1;
                existingCustomer.ShipAddr2 = item.DomainObject.ShipAddr2;
                existingCustomer.ShipAddr3 = item.DomainObject.ShipAddr3;
                existingCustomer.ShipAddr4 = item.DomainObject.ShipAddr4;
                existingCustomer.ShipCity = item.DomainObject.ShipCity;
                existingCustomer.ShipState = item.DomainObject.ShipState;
                existingCustomer.ShipPostalCode = item.DomainObject.ShipPostalCode;
                existingCustomer.ShipCountry = item.DomainObject.ShipCountry;
                existingCustomer.PrintAs = item.DomainObject.PrintAs;
                existingCustomer.ShippingAddressSameAsBilling = item.DomainObject.ShippingAddressSameAsBilling;
                existingCustomer.Balance = item.DomainObject.Balance;
                existingCustomer.BalanceDate = item.DomainObject.BalanceDate;
                existingCustomer.ResaleNumber = item.DomainObject.ResaleNumber;
                existingCustomer.DeliveryMethod = item.DomainObject.DeliveryMethod;
                
                Customer.Update(existingCustomer);
            }

            #endregion

            #region Process queried customers

            if (item.CommandType == QBCommandTypeEnum.Query)
            {
                Customer customer = item.DomainObject;

                if (customer.Terms != null)
                    customer.Terms.TermsId =
                        Terms.FindByQuickBooksId(customer.Terms.QuickBooksListId).TermsId;
                try
                {
                    Customer existingCustomer = Customer.FindByQuickBooksId(
                        customer.QuickBooksListId.Value);

                    customer.CustomerId = existingCustomer.CustomerId;
                    customer.EntityState = EntityState.Synchronized;

                    Customer.Update(customer);
                }
                catch (DataNotFoundException)
                {
                    Counter.Assign(customer);

                    customer.EntityState = EntityState.Synchronized;

                    Customer.Insert(customer);

                    QBEntity.Insert(
                        new QBEntity(QBEntityType.Customer, (int)customer.QuickBooksListId));
                }
            }

            #endregion
            
        }

        #region TargetNodeName
        protected override string TargetNodeName
        {
            get { return TARGET_NODE; }
        }
        #endregion

        #region TargetClassType
        protected override Type TargetClassType
        {
            get { return typeof(CustomerRet) ; }
        }
        #endregion

        [XmlRoot("CustomerRet")]
        public class CustomerRet:QBResponseItem
        {
            #region FullName
            String m_fullName;
            [XmlElement("FullName")]
            public String FullName
            {
                get { return m_fullName; }
                set { m_fullName = value; }
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
            
            #region Name
            String m_name;
            [XmlElement("Name")]
            public String Name
            {
                get { return m_name; }
                set { m_name = value; }
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

            #region CompanyName
            String m_companyName;
            [XmlElement("CompanyName")]
            public String CompanyName
            {
                get { return m_companyName; }
                set { m_companyName = value; }
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

            #region Email
            String m_email;
            [XmlElement("Email")]
            public String Email
            {
                get { return m_email; }
                set { m_email = value; }
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

            #region BillAddress
            BillShipAddress m_billAddress;
            [XmlElement("BillAddress")]
            public BillShipAddress BillAddress
            {
                get { return m_billAddress; }
                set { m_billAddress = value; }
            }
            #endregion

            #region ShipAddress
            BillShipAddress m_shipAddress;
            [XmlElement("ShipAddress")]
            public BillShipAddress ShipAddress
            {
                get { return m_shipAddress; }
                set { m_shipAddress = value; }
            }
            #endregion

            #region PrintAs
            String m_printAs;
            [XmlElement("PrintAs")]
            public String PrintAs
            {
                get { return m_printAs; }
                set { m_printAs = value; }
            }
            #endregion

            #region ShippingAddressSameAsBilling
            bool m_shippingAddressSameAsBilling;
            public bool ShippingAddressSameAsBilling
            {
                get { return m_shippingAddressSameAsBilling; }
                set { m_shippingAddressSameAsBilling = value; }
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

            #region ResaleNumber
            String m_resaleNumber;
            [XmlElement("ResaleNumber")]
            public String ResaleNumber
            {
                get { return m_resaleNumber; }
                set { m_resaleNumber = value; }
            }
            #endregion

            #region TermsRef
            TermsRef m_termsRef;
            [XmlElement("TermsRef")]
            public TermsRef TermsRef
            {
                get { return m_termsRef; }
                set { m_termsRef = value; }
            }
            #endregion

            #region DeliveryMethod
            String m_deliveryMethod;
            [XmlElement("DeliveryMethod")]
            public String DeliveryMethod
            {
                get { return m_deliveryMethod; }
                set { m_deliveryMethod = value; }
            }
            #endregion            
        }

        public override bool IsRootNode(string nodeName)
        {
            return "CustomerQueryRs".Equals(nodeName)
                   || "CustomerModRs".Equals(nodeName)
                   || "CustomerAddRs".Equals(nodeName);
        }

        public class BillShipAddress
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

        public class TermsRef
        {
            #region ListId
            int m_listId;
            [XmlElement("ListID")]
            public int ListId
            {
                get { return m_listId; }
                set { m_listId = value; }
            }
            #endregion

            #region FullName
            String m_fullName;
            [XmlElement("FullName")]
            public String FullName
            {
                get { return m_fullName; }
                set { m_fullName = value; }
            }
            #endregion
        }
    }
}
