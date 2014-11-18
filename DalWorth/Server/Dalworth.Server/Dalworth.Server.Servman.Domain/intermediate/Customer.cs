using System;
using System.Collections.Generic;
using System.Text;

namespace Dalworth.Server.Servman.Domain.intermediate
{
    public enum CustomerTypeEnum
    {
        Business,
        Residential
    }

    public class Customer
    {
        public Customer()
        {
            m_address = new Address();
        }

        #region Address

        private Address m_address;
        public Address Address
        {
            get { return m_address; }
            set { m_address = value; }
        }

        #endregion

        #region CustomerTypeId

        private string m_customerTypeId;
        public string CustomerTypeId
        {
            get { return (m_customerTypeId ?? string.Empty).ToUpper(); }
            set { m_customerTypeId = value; }
        }

        #endregion

        #region CustomerType

        public CustomerTypeEnum? CustomerType
        {
            get
            {
                if (m_customerTypeId == "R")
                    return CustomerTypeEnum.Residential;
                if (m_customerTypeId == "C")
                    return CustomerTypeEnum.Business;
                return null;
            }

            set
            {
                if (value == CustomerTypeEnum.Residential)
                    m_customerTypeId = "R";
                else if (value == CustomerTypeEnum.Business)
                    m_customerTypeId = "C";
                else
                    m_customerTypeId = string.Empty;
            }
        }

        #endregion

        #region ID

        private string m_id;
        public string ID
        {
            get { return m_id ?? string.Empty; }
            set { m_id = value; }
        }

        #endregion


        #region Name

        private string m_name;
        public string Name
        {
            get { return (m_name ?? string.Empty).ToUpper(); }
            set { m_name = value; }
        }

        #endregion

        #region HomePhone

        private string m_homePhone;
        public string HomePhone
        {
            get { return (m_homePhone ?? string.Empty).ToUpper(); }
            set { m_homePhone = value; }
        }

        #endregion

        #region BusinessPhone

        private string m_businessPhone;
        public string BusinessPhone
        {
            get { return (m_businessPhone ?? string.Empty).ToUpper(); }
            set { m_businessPhone = value; }
        }

        #endregion

        #region LastContact

        private DateTime m_lastContact;
        public DateTime LastContact
        {
            get { return m_lastContact == DateTime.MinValue ? Utils.SERVMAN_NULL_DATE : m_lastContact; }
            set { m_lastContact = value; }
        }

        #endregion

        #region LastService

        private DateTime m_lastService;
        public DateTime LastService
        {
            get { return m_lastService == DateTime.MinValue ? Utils.SERVMAN_NULL_DATE : m_lastService; }
            set { m_lastService = value; }
        }

        #endregion

        #region LastAddressChange

        private DateTime m_lastAddressChange;
        public DateTime LastAddressChange
        {
            get { return m_lastAddressChange == DateTime.MinValue ? Utils.SERVMAN_NULL_DATE : m_lastAddressChange; }
            set { m_lastAddressChange = value; }
        }

        #endregion

        #region EmailAddress

        private string m_emailAddress;
        public string EmailAddress
        {
            get { return (m_emailAddress ?? string.Empty).ToUpper(); }
            set { m_emailAddress = value; }
        }

        #endregion

        #region HasVisitedWebsite

        private bool m_hasVisitedWebsite;
        public bool HasVisitedWebsite
        {
            get { return m_hasVisitedWebsite; }
            set { m_hasVisitedWebsite = value; }
        }

        #endregion


        #region Export & Import

        public custmast Export(custmast servmanCustomer)
        {
            custmast custmast;

            if (servmanCustomer != null) //update
                custmast = servmanCustomer;
            else
                custmast = new custmast();

            custmast.cust_id = ID;
            custmast.customer = Name;
            custmast.block = Address.Block;
            custmast.prefix = Address.Prefix;
            custmast.street = Address.Street;
            custmast.suffix = Address.Suffux;
            custmast.unit = Address.Unit;
            custmast.address2 = Address.Address2;
            custmast.city = Address.City;
            custmast.state = Address.State;
            custmast.zip = Address.Zip;
            custmast.home_phone = HomePhone;
            custmast.bus_phone = BusinessPhone;
            custmast.grid = Address.Page + Address.Grid;
            custmast.area_id = Address.AreaId;
            custmast.cust_type = CustomerTypeId;
            custmast.l_contact = LastContact;
            custmast.l_service = LastService;
            custmast.l_addr_chg = LastAddressChange;
            custmast.emailaddr = EmailAddress;
            custmast.webvisit = HasVisitedWebsite;
            custmast.zip4 = Address.Zip4;

            if (servmanCustomer == null) //New customer
            {
                custmast.cust_stat = "C";
                custmast.l_kic_mail = Utils.SERVMAN_NULL_DATE;
                custmast.addr_ver = false; //always false
                custmast.commission = 0; //ignored
                custmast.custkey_1 = string.Empty; //ignored
                custmast.cleancnum = string.Empty; //ignored
                custmast.no_email = false; //ignored
                custmast.text_email = false; //ignored
                custmast.ncoa_date = Utils.SERVMAN_NULL_DATE;
                custmast.bad_mail = false; //ignored don't know what is this
                custmast.notcuraddr = false; //ignored
                custmast.rev_total = 0; //ignored
                custmast.job_count = 0;
                custmast.dt_lastjob = Utils.SERVMAN_NULL_DATE;
                custmast.rfm = string.Empty; //ignored
                custmast.rank = 0;
                custmast.excl_cont = string.Empty;
                custmast.last_eref = Utils.SERVMAN_NULL_DATE;
            }

            return custmast;
        }

        public static Customer Import(custmast custmast)
        {
            Customer customer = new Customer();

            customer.ID = custmast.cust_id;
            customer.Name = custmast.customer;
            customer.Address.Block = custmast.block;
            customer.Address.Prefix = custmast.prefix;
            customer.Address.Street = custmast.street;
            customer.Address.Suffux = custmast.suffix;
            customer.Address.Unit = custmast.unit;
            customer.Address.Address2 = custmast.address2;
            customer.Address.City = custmast.city;
            customer.Address.State = custmast.state;
            customer.Address.Zip = custmast.zip;
            customer.HomePhone = custmast.home_phone;
            customer.BusinessPhone = custmast.bus_phone;
            customer.Address.Grid = custmast.grid;
            customer.Address.AreaId = custmast.area_id;
            customer.CustomerTypeId = custmast.cust_type;
            //custmast.cust_stat ignored
            customer.LastContact = custmast.l_contact;
            customer.LastService = custmast.l_service;
            //custmast.l_kic_mail ignored
            customer.LastAddressChange = custmast.l_addr_chg;
            //custmast.addr_ver = false; //always false
            //custmast.commission = 0; //ignored
            //custmast.custkey_1 = string.Empty; //ignored
            customer.EmailAddress = custmast.emailaddr;
            customer.HasVisitedWebsite = custmast.webvisit;
            //custmast.cleancnum = string.Empty; //ignored
            //custmast.no_email = false; //ignored
            //custmast.text_email = false; //ignored
            customer.Address.Zip4 = custmast.zip4;
            //custmast.ncoa_date ignored
            //custmast.bad_mail = false; //ignored don't know what is this
            //custmast.notcuraddr = false; //ignored
            //custmast.rev_total = 0; //ignored
            //custmast.job_count ignore
            //custmast.dt_lastjob ignored
            //custmast.rfm = string.Empty; //ignored
            //custmast.rank = 0;   

            return customer;
        }

        #endregion
    }
}
