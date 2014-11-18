using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class Customer : ICounterField
    {
        #region Customer

        public Customer()
        {
            this.EntityState = new EntityState();
        }

        #endregion

        #region Customer

        public Customer(EntityState entityState,int customerId, int? modifiedCustomerId,
                        int? quickBooksListId, Terms terms, int editSequence, String fullName, String salutation, String firstName,
                        String middleName, String lastName, String suffix, String companyName, String phone, String mobile, 
                        String email, String pager, String altPhone, String fax, decimal balance,
                        DateTime? balanceDate, String resaleNumber, String deliveryMethod)
        {
            m_entityState = entityState;
            
            m_customerId = customerId;

            m_modifiedCustomerId = modifiedCustomerId;

            m_quickBooksListId = quickBooksListId;

            m_terms = terms;

            m_editSequence = editSequence;

            m_fullName = fullName;

            m_salutation = salutation;

            m_firstName = firstName;

            m_middleName = middleName;

            m_lastName = lastName;

            m_suffix = suffix;

            m_companyName = companyName;

            m_phone = phone;

            m_mobile = mobile;

            m_email = email;

            m_pager = pager;

            m_altPhone = altPhone;

            m_fax = fax;

            m_balance = balance;

            m_balanceDate = balanceDate;

            m_resaleNumber = resaleNumber;

            m_deliveryMethod = deliveryMethod;
        }

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get
            {
                return m_customerId;
            }
            set
            {
                m_customerId = value;
            }
        }

        public string CounterName
        {
            get { return "Customer"; }
        }

        #endregion

        #region Find By Entity State

        const string SqlFindByEntityState  = @"Select 
            CustomerId, 
            ModifiedCustomerId, 
            Customer.QuickBooksListId, 
            EntityStateId, 
            Customer.TermsId,
            Customer.EditSequence, 
            Customer.Name,
            Customer.FullName, 
            Salutation,
            FirstName, 
            MiddleName, 
            LastName,
 
            Suffix, 
            CompanyName,      
            Phone,      
            Mobile, 
            Email, 
            Pager, 
            AltPhone, 
            Fax, 
            BillAddr1, 
            BillAddr2, 
            BillAddr3, 
            BillAddr4, 
            BillCity, 

            BillState, 
            BillPostalCode, 
            BillCountry, 
            ShipAddr1, 
            ShipAddr2, 
            ShipAddr3, 
            ShipAddr4, 
            ShipCity, 
            ShipState, 
            ShipPostalCode, 
            ShipCountry, 
            PrintAs,
            ShippingAddressSameAsBilling,             
            Balance,

            BalanceDate,
            ResaleNumber,
            DeliveryMethod,   

	        Terms.TermsId, 
	        Terms.QuickBooksListId, 
	        Terms.Name, 
	        Terms.StdDueDays, 
	        Terms.StdDiscountDays, 
	        Terms.DiscountPct

            From Customer 
            Left Outer Join Terms on Terms.TermsId = Customer.TermsId 
            Where EntityStateId = @EntityStateId";

        public static List<Customer> FindBy(EntityState entityState)
        {
            List<Customer> customerList = new List<Customer>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByEntityState))
            {
                Database.PutParameter(dbCommand, "@EntityStateId", entityState.EntityStateId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Customer customer = Load(dataReader);

                        if (customer.Terms != null)
                        {
                            customer.Terms.TermsId = dataReader.GetInt32(42);
                            customer.Terms.QuickBooksListId = dataReader.GetInt32(43);
                            customer.Terms.Name = dataReader.GetString(44);
                            if (!dataReader.IsDBNull(45))
                                customer.Terms.StdDueDays = dataReader.GetInt32(45);
                            if (!dataReader.IsDBNull(46))
                                customer.Terms.StdDiscountDays = dataReader.GetInt32(46);
                            if (!dataReader.IsDBNull(47))
                                customer.Terms.DiscountPct = dataReader.GetDecimal(47);
                        }

                        customerList.Add(customer);
                    }
                }
            }


            return customerList;
        }
        #endregion

        #region FindByQuickBooksId

        private const String SqlSelectByQuickBooksId = @"Select  
            CustomerId, 
            ModifiedCustomerId, 
            Customer.QuickBooksListId, 
            EntityStateId, 
            Customer.TermsId,
            Customer.EditSequence, 
            Customer.Name,
            Customer.FullName, 
            Salutation,
            FirstName, 
            MiddleName, 
            LastName, 
            Suffix,
            CompanyName, 
            Phone, 
            Mobile, 
            Email, 
            Pager, 
            AltPhone, 
            Fax, 
            BillAddr1, 
            BillAddr2, 
            BillAddr3, 
            BillAddr4, 
            BillCity,  
            BillState, 
            BillPostalCode, 
            BillCountry, 
            ShipAddr1, 
            ShipAddr2, 
            ShipAddr3, 
            ShipAddr4, 
            ShipCity,  
            ShipState, 
            ShipPostalCode, 
            ShipCountry, 
            PrintAs,
            ShippingAddressSameAsBilling,             
            Balance,
            BalanceDate,
            ResaleNumber,
            DeliveryMethod,

	        Terms.TermsId, 
	        Terms.QuickBooksListId, 
	        Terms.Name, 
	        Terms.StdDueDays, 
	        Terms.StdDiscountDays, 
	        Terms.DiscountPct

            From Customer
            Left Outer Join Terms on Terms.TermsId = Customer.TermsId    
            Where Customer.QuickBooksListId = @QuickBooksListId";

        public static Customer FindByQuickBooksId(int quickBooksListId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByQuickBooksId))
            {
                Database.PutParameter(dbCommand, "@QuickBooksListId", quickBooksListId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        Customer customer = Load(dataReader);

                        if (customer.Terms != null)
                        {
                            customer.Terms.TermsId = dataReader.GetInt32(42);
                            customer.Terms.QuickBooksListId = dataReader.GetInt32(43);
                            customer.Terms.Name = dataReader.GetString(44);
                            if (!dataReader.IsDBNull(45))
                                customer.Terms.StdDueDays = dataReader.GetInt32(45);
                            if (!dataReader.IsDBNull(46))
                                customer.Terms.StdDiscountDays = dataReader.GetInt32(46);
                            if (!dataReader.IsDBNull(47))
                                customer.Terms.DiscountPct = dataReader.GetDecimal(47);
                        }

                        return customer;                     
                    }
                }
            }
            throw new DataNotFoundException("Customer not found, search by primary key");
        }

        #endregion

        #region Find By Company

        private const String SqlFindByFullName = @"Select 
        CustomerId, 
        ModifiedCustomerId, 
        QuickBooksListId, 
        EntityStateId, 
        TermsId,
        EditSequence, 
        Name,
        FullName, 
        Salutation,
        FirstName, 
        MiddleName, 

        LastName, 
        Suffix,
        CompanyName, 
        Phone, 
        Mobile, 
        Email,   
        Pager,  
        AltPhone,  
        Fax,  
        BillAddr1,  
        BillAddr2,  
        BillAddr3,  
        BillAddr4,  

        BillCity,  
        BillState,  
        BillPostalCode,  
        BillCountry,  
        ShipAddr1,  
        ShipAddr2,  
        ShipAddr3,  
        ShipAddr4,  
        ShipCity,  
        ShipState,  
        ShipPostalCode,  
        ShipCountry,  
        PrintAs,
        ShippingAddressSameAsBilling,  

        Balance,
        BalanceDate,
        ResaleNumber,        
        DeliveryMethod 
        From Customer    
        Order By Customer.FullName";

        public static List<Customer> FindByFullName()
        {
          using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByFullName))
          {
              List<Customer> rv = new List<Customer>();

              using (IDataReader dataReader = dbCommand.ExecuteReader())
              {
                  while (dataReader.Read())
                  {
                      rv.Add(Load(dataReader));
                  }
              }

              return rv;
          }
        }

        #endregion

        #region Find By ModifiedCustomerId

        private const String SqlFindByModifiedCustomerId = @"Select
      
        CustomerId, 
        ModifiedCustomerId, 
        QuickBooksListId, 
        EntityStateId, 
        TermsId, 
        EditSequence, 
        Name, 
        FullName, 
        Salutation, 
        FirstName, 
        MiddleName, 
        LastName, 
        Suffix, 
        CompanyName, 
        Phone, 
        Mobile, 
        Email, 
        Pager, 
        AltPhone, 
        Fax, 
        BillAddr1, 
        BillAddr2, 
        BillAddr3, 
        BillAddr4, 
        BillCity, 
        BillState, 
        BillPostalCode, 
        BillCountry, 
        ShipAddr1, 
        ShipAddr2, 
        ShipAddr3, 
        ShipAddr4, 
        ShipCity, 
        ShipState, 
        ShipPostalCode, 
        ShipCountry, 
        PrintAs, 
        ShippingAddressSameAsBilling, 
        Balance, 
        BalanceDate, 
        ResaleNumber, 
        DeliveryMethod 
        From [Customer] 
        where ModifiedCustomerId = @ModifiedCustomerId";

        public static Customer FindByModifiedCustomerId(int modifiedCustomerId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByModifiedCustomerId))
            {
                Database.PutParameter(dbCommand, "@ModifiedCustomerId", modifiedCustomerId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        Customer customer = Load(dataReader);
                        return customer;
                    }
                }
            }
            throw new DataNotFoundException("Customer not found");            
        }

        #endregion

        #region IsFullNameExist

        private const string SqlIsFullNameExist =
            @"SELECT 1 
            FROM Customer
                WHERE FullName = @FullName
                    AND FullName <> @FullNameExclude";

        public static bool IsFullNameExist(string fullName, string excludeFullName)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlIsFullNameExist))
            {
                Database.PutParameter(dbCommand, "@FullName", fullName);
                Database.PutParameter(dbCommand, "@FullNameExclude", excludeFullName);
                using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
                {
                    return reader.Read();
                }
            }

        }

        #endregion            

        #region ToString

        public override string ToString()
        {
            return FullName;
        }

        #endregion
    }
}
