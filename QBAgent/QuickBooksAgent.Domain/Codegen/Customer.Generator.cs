
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class Customer
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Customer] ( " +
      
        " CustomerId, " +
        " ModifiedCustomerId, " +
        " QuickBooksListId, " +
        " EntityStateId, " +
        " TermsId, " +
        " EditSequence, " +
        " Name, " +
        " FullName, " +
        " Salutation, " +
        " FirstName, " +
        " MiddleName, " +
        " LastName, " +
        " Suffix, " +
        " CompanyName, " +
        " Phone, " +
        " Mobile, " +
        " Email, " +
        " Pager, " +
        " AltPhone, " +
        " Fax, " +
        " BillAddr1, " +
        " BillAddr2, " +
        " BillAddr3, " +
        " BillAddr4, " +
        " BillCity, " +
        " BillState, " +
        " BillPostalCode, " +
        " BillCountry, " +
        " ShipAddr1, " +
        " ShipAddr2, " +
        " ShipAddr3, " +
        " ShipAddr4, " +
        " ShipCity, " +
        " ShipState, " +
        " ShipPostalCode, " +
        " ShipCountry, " +
        " PrintAs, " +
        " ShippingAddressSameAsBilling, " +
        " Balance, " +
        " BalanceDate, " +
        " ResaleNumber, " +
        " DeliveryMethod " +
        ") Values (" +
      
        " @CustomerId, " +
        " @ModifiedCustomerId, " +
        " @QuickBooksListId, " +
        " @EntityStateId, " +
        " @TermsId, " +
        " @EditSequence, " +
        " @Name, " +
        " @FullName, " +
        " @Salutation, " +
        " @FirstName, " +
        " @MiddleName, " +
        " @LastName, " +
        " @Suffix, " +
        " @CompanyName, " +
        " @Phone, " +
        " @Mobile, " +
        " @Email, " +
        " @Pager, " +
        " @AltPhone, " +
        " @Fax, " +
        " @BillAddr1, " +
        " @BillAddr2, " +
        " @BillAddr3, " +
        " @BillAddr4, " +
        " @BillCity, " +
        " @BillState, " +
        " @BillPostalCode, " +
        " @BillCountry, " +
        " @ShipAddr1, " +
        " @ShipAddr2, " +
        " @ShipAddr3, " +
        " @ShipAddr4, " +
        " @ShipCity, " +
        " @ShipState, " +
        " @ShipPostalCode, " +
        " @ShipCountry, " +
        " @PrintAs, " +
        " @ShippingAddressSameAsBilling, " +
        " @Balance, " +
        " @BalanceDate, " +
        " @ResaleNumber, " +
        " @DeliveryMethod " +
      ")";

      public static void Insert(Customer customer)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@CustomerId", customer.CustomerId);            
          
              Database.PutParameter(dbCommand,"@ModifiedCustomerId", customer.ModifiedCustomerId);            
          
              Database.PutParameter(dbCommand,"@QuickBooksListId", customer.QuickBooksListId);            
          
              Database.PutParameter(dbCommand,"@EntityStateId", customer
			.EntityState.EntityStateId);            
          
            if(customer
			.Terms == null)
            {
            Database.PutParameter(dbCommand,"@TermsId", DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@TermsId", customer
			.Terms.TermsId);
            }
          
              Database.PutParameter(dbCommand,"@EditSequence", customer.EditSequence);            
          
              Database.PutParameter(dbCommand,"@Name", customer.Name);            
          
              Database.PutParameter(dbCommand,"@FullName", customer.FullName);            
          
              Database.PutParameter(dbCommand,"@Salutation", customer.Salutation);            
          
              Database.PutParameter(dbCommand,"@FirstName", customer.FirstName);            
          
              Database.PutParameter(dbCommand,"@MiddleName", customer.MiddleName);            
          
              Database.PutParameter(dbCommand,"@LastName", customer.LastName);            
          
              Database.PutParameter(dbCommand,"@Suffix", customer.Suffix);            
          
              Database.PutParameter(dbCommand,"@CompanyName", customer.CompanyName);            
          
              Database.PutParameter(dbCommand,"@Phone", customer.Phone);            
          
              Database.PutParameter(dbCommand,"@Mobile", customer.Mobile);            
          
              Database.PutParameter(dbCommand,"@Email", customer.Email);            
          
              Database.PutParameter(dbCommand,"@Pager", customer.Pager);            
          
              Database.PutParameter(dbCommand,"@AltPhone", customer.AltPhone);            
          
              Database.PutParameter(dbCommand,"@Fax", customer.Fax);            
          
              Database.PutParameter(dbCommand,"@BillAddr1", customer.BillAddr1);            
          
              Database.PutParameter(dbCommand,"@BillAddr2", customer.BillAddr2);            
          
              Database.PutParameter(dbCommand,"@BillAddr3", customer.BillAddr3);            
          
              Database.PutParameter(dbCommand,"@BillAddr4", customer.BillAddr4);            
          
              Database.PutParameter(dbCommand,"@BillCity", customer.BillCity);            
          
              Database.PutParameter(dbCommand,"@BillState", customer.BillState);            
          
              Database.PutParameter(dbCommand,"@BillPostalCode", customer.BillPostalCode);            
          
              Database.PutParameter(dbCommand,"@BillCountry", customer.BillCountry);            
          
              Database.PutParameter(dbCommand,"@ShipAddr1", customer.ShipAddr1);            
          
              Database.PutParameter(dbCommand,"@ShipAddr2", customer.ShipAddr2);            
          
              Database.PutParameter(dbCommand,"@ShipAddr3", customer.ShipAddr3);            
          
              Database.PutParameter(dbCommand,"@ShipAddr4", customer.ShipAddr4);            
          
              Database.PutParameter(dbCommand,"@ShipCity", customer.ShipCity);            
          
              Database.PutParameter(dbCommand,"@ShipState", customer.ShipState);            
          
              Database.PutParameter(dbCommand,"@ShipPostalCode", customer.ShipPostalCode);            
          
              Database.PutParameter(dbCommand,"@ShipCountry", customer.ShipCountry);            
          
              Database.PutParameter(dbCommand,"@PrintAs", customer.PrintAs);            
          
              Database.PutParameter(dbCommand,"@ShippingAddressSameAsBilling", customer.ShippingAddressSameAsBilling);            
          
              Database.PutParameter(dbCommand,"@Balance", customer.Balance);            
          
              Database.PutParameter(dbCommand,"@BalanceDate", customer.BalanceDate);            
          
              Database.PutParameter(dbCommand,"@ResaleNumber", customer.ResaleNumber);            
          
              Database.PutParameter(dbCommand,"@DeliveryMethod", customer.DeliveryMethod);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Customer>  customerList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Customer customer in  customerList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@CustomerId", customer.CustomerId);
          
            Database.PutParameter(dbCommand,"@ModifiedCustomerId", customer.ModifiedCustomerId);
          
            Database.PutParameter(dbCommand,"@QuickBooksListId", customer.QuickBooksListId);
          
            Database.PutParameter(dbCommand,"@EntityStateId", customer
			.EntityState.EntityStateId);
          
            if(customer
			.Terms == null)
            {
              Database.PutParameter(dbCommand,"@TermsId", DbType.Int32);
            }
            else
            {
              Database.PutParameter(dbCommand,"@TermsId", customer
			.Terms.TermsId);
            }
          
            Database.PutParameter(dbCommand,"@EditSequence", customer.EditSequence);
          
            Database.PutParameter(dbCommand,"@Name", customer.Name);
          
            Database.PutParameter(dbCommand,"@FullName", customer.FullName);
          
            Database.PutParameter(dbCommand,"@Salutation", customer.Salutation);
          
            Database.PutParameter(dbCommand,"@FirstName", customer.FirstName);
          
            Database.PutParameter(dbCommand,"@MiddleName", customer.MiddleName);
          
            Database.PutParameter(dbCommand,"@LastName", customer.LastName);
          
            Database.PutParameter(dbCommand,"@Suffix", customer.Suffix);
          
            Database.PutParameter(dbCommand,"@CompanyName", customer.CompanyName);
          
            Database.PutParameter(dbCommand,"@Phone", customer.Phone);
          
            Database.PutParameter(dbCommand,"@Mobile", customer.Mobile);
          
            Database.PutParameter(dbCommand,"@Email", customer.Email);
          
            Database.PutParameter(dbCommand,"@Pager", customer.Pager);
          
            Database.PutParameter(dbCommand,"@AltPhone", customer.AltPhone);
          
            Database.PutParameter(dbCommand,"@Fax", customer.Fax);
          
            Database.PutParameter(dbCommand,"@BillAddr1", customer.BillAddr1);
          
            Database.PutParameter(dbCommand,"@BillAddr2", customer.BillAddr2);
          
            Database.PutParameter(dbCommand,"@BillAddr3", customer.BillAddr3);
          
            Database.PutParameter(dbCommand,"@BillAddr4", customer.BillAddr4);
          
            Database.PutParameter(dbCommand,"@BillCity", customer.BillCity);
          
            Database.PutParameter(dbCommand,"@BillState", customer.BillState);
          
            Database.PutParameter(dbCommand,"@BillPostalCode", customer.BillPostalCode);
          
            Database.PutParameter(dbCommand,"@BillCountry", customer.BillCountry);
          
            Database.PutParameter(dbCommand,"@ShipAddr1", customer.ShipAddr1);
          
            Database.PutParameter(dbCommand,"@ShipAddr2", customer.ShipAddr2);
          
            Database.PutParameter(dbCommand,"@ShipAddr3", customer.ShipAddr3);
          
            Database.PutParameter(dbCommand,"@ShipAddr4", customer.ShipAddr4);
          
            Database.PutParameter(dbCommand,"@ShipCity", customer.ShipCity);
          
            Database.PutParameter(dbCommand,"@ShipState", customer.ShipState);
          
            Database.PutParameter(dbCommand,"@ShipPostalCode", customer.ShipPostalCode);
          
            Database.PutParameter(dbCommand,"@ShipCountry", customer.ShipCountry);
          
            Database.PutParameter(dbCommand,"@PrintAs", customer.PrintAs);
          
            Database.PutParameter(dbCommand,"@ShippingAddressSameAsBilling", customer.ShippingAddressSameAsBilling);
          
            Database.PutParameter(dbCommand,"@Balance", customer.Balance);
          
            Database.PutParameter(dbCommand,"@BalanceDate", customer.BalanceDate);
          
            Database.PutParameter(dbCommand,"@ResaleNumber", customer.ResaleNumber);
          
            Database.PutParameter(dbCommand,"@DeliveryMethod", customer.DeliveryMethod);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@CustomerId",customer.CustomerId);
          
            Database.UpdateParameter(dbCommand,"@ModifiedCustomerId",customer.ModifiedCustomerId);
          
            Database.UpdateParameter(dbCommand,"@QuickBooksListId",customer.QuickBooksListId);
          
            Database.UpdateParameter(dbCommand,"@EntityStateId",customer
			.EntityState.EntityStateId);
          
            if(customer
			.Terms == null)
            {
             Database.UpdateParameter(dbCommand,"@TermsId",DbType.Int32);
            }
            else
            {
            Database.UpdateParameter(dbCommand,"@TermsId",customer
			.Terms.TermsId);
            }
          
            Database.UpdateParameter(dbCommand,"@EditSequence",customer.EditSequence);
          
            Database.UpdateParameter(dbCommand,"@Name",customer.Name);
          
            Database.UpdateParameter(dbCommand,"@FullName",customer.FullName);
          
            Database.UpdateParameter(dbCommand,"@Salutation",customer.Salutation);
          
            Database.UpdateParameter(dbCommand,"@FirstName",customer.FirstName);
          
            Database.UpdateParameter(dbCommand,"@MiddleName",customer.MiddleName);
          
            Database.UpdateParameter(dbCommand,"@LastName",customer.LastName);
          
            Database.UpdateParameter(dbCommand,"@Suffix",customer.Suffix);
          
            Database.UpdateParameter(dbCommand,"@CompanyName",customer.CompanyName);
          
            Database.UpdateParameter(dbCommand,"@Phone",customer.Phone);
          
            Database.UpdateParameter(dbCommand,"@Mobile",customer.Mobile);
          
            Database.UpdateParameter(dbCommand,"@Email",customer.Email);
          
            Database.UpdateParameter(dbCommand,"@Pager",customer.Pager);
          
            Database.UpdateParameter(dbCommand,"@AltPhone",customer.AltPhone);
          
            Database.UpdateParameter(dbCommand,"@Fax",customer.Fax);
          
            Database.UpdateParameter(dbCommand,"@BillAddr1",customer.BillAddr1);
          
            Database.UpdateParameter(dbCommand,"@BillAddr2",customer.BillAddr2);
          
            Database.UpdateParameter(dbCommand,"@BillAddr3",customer.BillAddr3);
          
            Database.UpdateParameter(dbCommand,"@BillAddr4",customer.BillAddr4);
          
            Database.UpdateParameter(dbCommand,"@BillCity",customer.BillCity);
          
            Database.UpdateParameter(dbCommand,"@BillState",customer.BillState);
          
            Database.UpdateParameter(dbCommand,"@BillPostalCode",customer.BillPostalCode);
          
            Database.UpdateParameter(dbCommand,"@BillCountry",customer.BillCountry);
          
            Database.UpdateParameter(dbCommand,"@ShipAddr1",customer.ShipAddr1);
          
            Database.UpdateParameter(dbCommand,"@ShipAddr2",customer.ShipAddr2);
          
            Database.UpdateParameter(dbCommand,"@ShipAddr3",customer.ShipAddr3);
          
            Database.UpdateParameter(dbCommand,"@ShipAddr4",customer.ShipAddr4);
          
            Database.UpdateParameter(dbCommand,"@ShipCity",customer.ShipCity);
          
            Database.UpdateParameter(dbCommand,"@ShipState",customer.ShipState);
          
            Database.UpdateParameter(dbCommand,"@ShipPostalCode",customer.ShipPostalCode);
          
            Database.UpdateParameter(dbCommand,"@ShipCountry",customer.ShipCountry);
          
            Database.UpdateParameter(dbCommand,"@PrintAs",customer.PrintAs);
          
            Database.UpdateParameter(dbCommand,"@ShippingAddressSameAsBilling",customer.ShippingAddressSameAsBilling);
          
            Database.UpdateParameter(dbCommand,"@Balance",customer.Balance);
          
            Database.UpdateParameter(dbCommand,"@BalanceDate",customer.BalanceDate);
          
            Database.UpdateParameter(dbCommand,"@ResaleNumber",customer.ResaleNumber);
          
            Database.UpdateParameter(dbCommand,"@DeliveryMethod",customer.DeliveryMethod);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Customer] Set "
      
        + " ModifiedCustomerId = @ModifiedCustomerId, "
        + " QuickBooksListId = @QuickBooksListId, "
        + " EntityStateId = @EntityStateId, "
        + " TermsId = @TermsId, "
        + " EditSequence = @EditSequence, "
        + " Name = @Name, "
        + " FullName = @FullName, "
        + " Salutation = @Salutation, "
        + " FirstName = @FirstName, "
        + " MiddleName = @MiddleName, "
        + " LastName = @LastName, "
        + " Suffix = @Suffix, "
        + " CompanyName = @CompanyName, "
        + " Phone = @Phone, "
        + " Mobile = @Mobile, "
        + " Email = @Email, "
        + " Pager = @Pager, "
        + " AltPhone = @AltPhone, "
        + " Fax = @Fax, "
        + " BillAddr1 = @BillAddr1, "
        + " BillAddr2 = @BillAddr2, "
        + " BillAddr3 = @BillAddr3, "
        + " BillAddr4 = @BillAddr4, "
        + " BillCity = @BillCity, "
        + " BillState = @BillState, "
        + " BillPostalCode = @BillPostalCode, "
        + " BillCountry = @BillCountry, "
        + " ShipAddr1 = @ShipAddr1, "
        + " ShipAddr2 = @ShipAddr2, "
        + " ShipAddr3 = @ShipAddr3, "
        + " ShipAddr4 = @ShipAddr4, "
        + " ShipCity = @ShipCity, "
        + " ShipState = @ShipState, "
        + " ShipPostalCode = @ShipPostalCode, "
        + " ShipCountry = @ShipCountry, "
        + " PrintAs = @PrintAs, "
        + " ShippingAddressSameAsBilling = @ShippingAddressSameAsBilling, "
        + " Balance = @Balance, "
        + " BalanceDate = @BalanceDate, "
        + " ResaleNumber = @ResaleNumber, "
        + " DeliveryMethod = @DeliveryMethod "
        + " Where "
        
          + " CustomerId = @CustomerId "
        
      ;

      public static void Update(Customer customer)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@CustomerId", customer.CustomerId);            
          
            Database.PutParameter(dbCommand,"@ModifiedCustomerId", customer.ModifiedCustomerId);            
          
            Database.PutParameter(dbCommand,"@QuickBooksListId", customer.QuickBooksListId);            
          
            Database.PutParameter(dbCommand,"@EntityStateId", customer
			.EntityState.EntityStateId);            
          
            if(customer
			.Terms == null)
            {
            Database.PutParameter(dbCommand,"@TermsId",DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@TermsId",customer
			.Terms.TermsId);
            }
          
            Database.PutParameter(dbCommand,"@EditSequence", customer.EditSequence);            
          
            Database.PutParameter(dbCommand,"@Name", customer.Name);            
          
            Database.PutParameter(dbCommand,"@FullName", customer.FullName);            
          
            Database.PutParameter(dbCommand,"@Salutation", customer.Salutation);            
          
            Database.PutParameter(dbCommand,"@FirstName", customer.FirstName);            
          
            Database.PutParameter(dbCommand,"@MiddleName", customer.MiddleName);            
          
            Database.PutParameter(dbCommand,"@LastName", customer.LastName);            
          
            Database.PutParameter(dbCommand,"@Suffix", customer.Suffix);            
          
            Database.PutParameter(dbCommand,"@CompanyName", customer.CompanyName);            
          
            Database.PutParameter(dbCommand,"@Phone", customer.Phone);            
          
            Database.PutParameter(dbCommand,"@Mobile", customer.Mobile);            
          
            Database.PutParameter(dbCommand,"@Email", customer.Email);            
          
            Database.PutParameter(dbCommand,"@Pager", customer.Pager);            
          
            Database.PutParameter(dbCommand,"@AltPhone", customer.AltPhone);            
          
            Database.PutParameter(dbCommand,"@Fax", customer.Fax);            
          
            Database.PutParameter(dbCommand,"@BillAddr1", customer.BillAddr1);            
          
            Database.PutParameter(dbCommand,"@BillAddr2", customer.BillAddr2);            
          
            Database.PutParameter(dbCommand,"@BillAddr3", customer.BillAddr3);            
          
            Database.PutParameter(dbCommand,"@BillAddr4", customer.BillAddr4);            
          
            Database.PutParameter(dbCommand,"@BillCity", customer.BillCity);            
          
            Database.PutParameter(dbCommand,"@BillState", customer.BillState);            
          
            Database.PutParameter(dbCommand,"@BillPostalCode", customer.BillPostalCode);            
          
            Database.PutParameter(dbCommand,"@BillCountry", customer.BillCountry);            
          
            Database.PutParameter(dbCommand,"@ShipAddr1", customer.ShipAddr1);            
          
            Database.PutParameter(dbCommand,"@ShipAddr2", customer.ShipAddr2);            
          
            Database.PutParameter(dbCommand,"@ShipAddr3", customer.ShipAddr3);            
          
            Database.PutParameter(dbCommand,"@ShipAddr4", customer.ShipAddr4);            
          
            Database.PutParameter(dbCommand,"@ShipCity", customer.ShipCity);            
          
            Database.PutParameter(dbCommand,"@ShipState", customer.ShipState);            
          
            Database.PutParameter(dbCommand,"@ShipPostalCode", customer.ShipPostalCode);            
          
            Database.PutParameter(dbCommand,"@ShipCountry", customer.ShipCountry);            
          
            Database.PutParameter(dbCommand,"@PrintAs", customer.PrintAs);            
          
            Database.PutParameter(dbCommand,"@ShippingAddressSameAsBilling", customer.ShippingAddressSameAsBilling);            
          
            Database.PutParameter(dbCommand,"@Balance", customer.Balance);            
          
            Database.PutParameter(dbCommand,"@BalanceDate", customer.BalanceDate);            
          
            Database.PutParameter(dbCommand,"@ResaleNumber", customer.ResaleNumber);            
          
            Database.PutParameter(dbCommand,"@DeliveryMethod", customer.DeliveryMethod);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " CustomerId, "
        + " ModifiedCustomerId, "
        + " QuickBooksListId, "
        + " EntityStateId, "
        + " TermsId, "
        + " EditSequence, "
        + " Name, "
        + " FullName, "
        + " Salutation, "
        + " FirstName, "
        + " MiddleName, "
        + " LastName, "
        + " Suffix, "
        + " CompanyName, "
        + " Phone, "
        + " Mobile, "
        + " Email, "
        + " Pager, "
        + " AltPhone, "
        + " Fax, "
        + " BillAddr1, "
        + " BillAddr2, "
        + " BillAddr3, "
        + " BillAddr4, "
        + " BillCity, "
        + " BillState, "
        + " BillPostalCode, "
        + " BillCountry, "
        + " ShipAddr1, "
        + " ShipAddr2, "
        + " ShipAddr3, "
        + " ShipAddr4, "
        + " ShipCity, "
        + " ShipState, "
        + " ShipPostalCode, "
        + " ShipCountry, "
        + " PrintAs, "
        + " ShippingAddressSameAsBilling, "
        + " Balance, "
        + " BalanceDate, "
        + " ResaleNumber, "
        + " DeliveryMethod "
        + " From [Customer] "
      
        + " Where "
        
        + " CustomerId = @CustomerId "
        
      ;

      public static Customer FindByPrimaryKey(
      int customerId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CustomerId", customerId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Customer not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Customer customer)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CustomerId",customer.CustomerId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData()
      {
      String sql = "select 1 from [Customer]";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static Customer Load(IDataReader dataReader)
      {
      Customer customer = new Customer();

      customer.CustomerId = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
              customer.ModifiedCustomerId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
              customer.QuickBooksListId = dataReader.GetInt32(2);
          customer
			.EntityState = new EntityState();

            customer
			.EntityState.EntityStateId = dataReader.GetInt32(3);
          
            if(!dataReader.IsDBNull(4))
            {
            customer
			.Terms = new Terms();
            
            customer
			.Terms.TermsId = dataReader.GetInt32(4);
           }
            else
            customer
			.Terms = null;
          customer.EditSequence = dataReader.GetInt32(5);
          customer.Name = dataReader.GetString(6);
          customer.FullName = dataReader.GetString(7);
          customer.Salutation = dataReader.GetString(8);
          customer.FirstName = dataReader.GetString(9);
          customer.MiddleName = dataReader.GetString(10);
          customer.LastName = dataReader.GetString(11);
          customer.Suffix = dataReader.GetString(12);
          
            if(!dataReader.IsDBNull(13))
              customer.CompanyName = dataReader.GetString(13);
          
            if(!dataReader.IsDBNull(14))
              customer.Phone = dataReader.GetString(14);
          
            if(!dataReader.IsDBNull(15))
              customer.Mobile = dataReader.GetString(15);
          
            if(!dataReader.IsDBNull(16))
              customer.Email = dataReader.GetString(16);
          
            if(!dataReader.IsDBNull(17))
              customer.Pager = dataReader.GetString(17);
          
            if(!dataReader.IsDBNull(18))
              customer.AltPhone = dataReader.GetString(18);
          
            if(!dataReader.IsDBNull(19))
              customer.Fax = dataReader.GetString(19);
          
            if(!dataReader.IsDBNull(20))
              customer.BillAddr1 = dataReader.GetString(20);
          
            if(!dataReader.IsDBNull(21))
              customer.BillAddr2 = dataReader.GetString(21);
          
            if(!dataReader.IsDBNull(22))
              customer.BillAddr3 = dataReader.GetString(22);
          
            if(!dataReader.IsDBNull(23))
              customer.BillAddr4 = dataReader.GetString(23);
          
            if(!dataReader.IsDBNull(24))
              customer.BillCity = dataReader.GetString(24);
          
            if(!dataReader.IsDBNull(25))
              customer.BillState = dataReader.GetString(25);
          
            if(!dataReader.IsDBNull(26))
              customer.BillPostalCode = dataReader.GetString(26);
          
            if(!dataReader.IsDBNull(27))
              customer.BillCountry = dataReader.GetString(27);
          
            if(!dataReader.IsDBNull(28))
              customer.ShipAddr1 = dataReader.GetString(28);
          
            if(!dataReader.IsDBNull(29))
              customer.ShipAddr2 = dataReader.GetString(29);
          
            if(!dataReader.IsDBNull(30))
              customer.ShipAddr3 = dataReader.GetString(30);
          
            if(!dataReader.IsDBNull(31))
              customer.ShipAddr4 = dataReader.GetString(31);
          
            if(!dataReader.IsDBNull(32))
              customer.ShipCity = dataReader.GetString(32);
          
            if(!dataReader.IsDBNull(33))
              customer.ShipState = dataReader.GetString(33);
          
            if(!dataReader.IsDBNull(34))
              customer.ShipPostalCode = dataReader.GetString(34);
          
            if(!dataReader.IsDBNull(35))
              customer.ShipCountry = dataReader.GetString(35);
          
            if(!dataReader.IsDBNull(36))
              customer.PrintAs = dataReader.GetString(36);
          customer.ShippingAddressSameAsBilling = dataReader.GetBoolean(37);
          
            if(!dataReader.IsDBNull(38))
              customer.Balance = dataReader.GetDecimal(38);
          
            if(!dataReader.IsDBNull(39))
              customer.BalanceDate = dataReader.GetDateTime(39);
          
            if(!dataReader.IsDBNull(40))
              customer.ResaleNumber = dataReader.GetString(40);
          customer.DeliveryMethod = dataReader.GetString(41);
          

      return customer;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Customer] "

      
        + " Where "
        
          + " CustomerId = @CustomerId "
        
      ;
      public static void Delete(Customer customer)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@CustomerId", customer.CustomerId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Customer] ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "
      
        + " CustomerId, "
        + " ModifiedCustomerId, "
        + " QuickBooksListId, "
        + " EntityStateId, "
        + " TermsId, "
        + " EditSequence, "
        + " Name, "
        + " FullName, "
        + " Salutation, "
        + " FirstName, "
        + " MiddleName, "
        + " LastName, "
        + " Suffix, "
        + " CompanyName, "
        + " Phone, "
        + " Mobile, "
        + " Email, "
        + " Pager, "
        + " AltPhone, "
        + " Fax, "
        + " BillAddr1, "
        + " BillAddr2, "
        + " BillAddr3, "
        + " BillAddr4, "
        + " BillCity, "
        + " BillState, "
        + " BillPostalCode, "
        + " BillCountry, "
        + " ShipAddr1, "
        + " ShipAddr2, "
        + " ShipAddr3, "
        + " ShipAddr4, "
        + " ShipCity, "
        + " ShipState, "
        + " ShipPostalCode, "
        + " ShipCountry, "
        + " PrintAs, "
        + " ShippingAddressSameAsBilling, "
        + " Balance, "
        + " BalanceDate, "
        + " ResaleNumber, "
        + " DeliveryMethod "
        + " From [Customer] ";
      public static List<Customer> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Customer> rv = new List<Customer>();

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      while(dataReader.Read())
      {
      rv.Add(Load(dataReader));
      }

      }

      return rv;
      }

      }
      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Customer> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Customer> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Customer));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Customer item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Customer>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Customer));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Customer> itemsList
      = new List<Customer>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Customer)
      itemsList.Add(deserializedObject as Customer);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region CustomerId
        protected int m_customerId;

			[XmlAttribute]
			public int CustomerId
			{
			get { return m_customerId;}
			set { m_customerId = value; }
			}
		#endregion
		
		#region ModifiedCustomerId
        protected int? m_modifiedCustomerId;

			[XmlAttribute]
			public int? ModifiedCustomerId
			{
			get { return m_modifiedCustomerId;}
			set { m_modifiedCustomerId = value; }
			}
		#endregion
		
		#region QuickBooksListId
        protected int? m_quickBooksListId;

			[XmlAttribute]
			public int? QuickBooksListId
			{
			get { return m_quickBooksListId;}
			set { m_quickBooksListId = value; }
			}
		#endregion
		
		#region EditSequence
        protected int m_editSequence;

			[XmlAttribute]
			public int EditSequence
			{
			get { return m_editSequence;}
			set { m_editSequence = value; }
			}
		#endregion
		
		#region Name
        protected String m_name;

			[XmlAttribute]
			public String Name
			{
			get { return m_name;}
			set { m_name = value; }
			}
		#endregion
		
		#region FullName
        protected String m_fullName;

			[XmlAttribute]
			public String FullName
			{
			get { return m_fullName;}
			set { m_fullName = value; }
			}
		#endregion
		
		#region Salutation
        protected String m_salutation;

			[XmlAttribute]
			public String Salutation
			{
			get { return m_salutation;}
			set { m_salutation = value; }
			}
		#endregion
		
		#region FirstName
        protected String m_firstName;

			[XmlAttribute]
			public String FirstName
			{
			get { return m_firstName;}
			set { m_firstName = value; }
			}
		#endregion
		
		#region MiddleName
        protected String m_middleName;

			[XmlAttribute]
			public String MiddleName
			{
			get { return m_middleName;}
			set { m_middleName = value; }
			}
		#endregion
		
		#region LastName
        protected String m_lastName;

			[XmlAttribute]
			public String LastName
			{
			get { return m_lastName;}
			set { m_lastName = value; }
			}
		#endregion
		
		#region Suffix
        protected String m_suffix;

			[XmlAttribute]
			public String Suffix
			{
			get { return m_suffix;}
			set { m_suffix = value; }
			}
		#endregion
		
		#region CompanyName
        protected String m_companyName;

			[XmlAttribute]
			public String CompanyName
			{
			get { return m_companyName;}
			set { m_companyName = value; }
			}
		#endregion
		
		#region Phone
        protected String m_phone;

			[XmlAttribute]
			public String Phone
			{
			get { return m_phone;}
			set { m_phone = value; }
			}
		#endregion
		
		#region Mobile
        protected String m_mobile;

			[XmlAttribute]
			public String Mobile
			{
			get { return m_mobile;}
			set { m_mobile = value; }
			}
		#endregion
		
		#region Email
        protected String m_email;

			[XmlAttribute]
			public String Email
			{
			get { return m_email;}
			set { m_email = value; }
			}
		#endregion
		
		#region Pager
        protected String m_pager;

			[XmlAttribute]
			public String Pager
			{
			get { return m_pager;}
			set { m_pager = value; }
			}
		#endregion
		
		#region AltPhone
        protected String m_altPhone;

			[XmlAttribute]
			public String AltPhone
			{
			get { return m_altPhone;}
			set { m_altPhone = value; }
			}
		#endregion
		
		#region Fax
        protected String m_fax;

			[XmlAttribute]
			public String Fax
			{
			get { return m_fax;}
			set { m_fax = value; }
			}
		#endregion
		
		#region BillAddr1
        protected String m_billAddr1;

			[XmlAttribute]
			public String BillAddr1
			{
			get { return m_billAddr1;}
			set { m_billAddr1 = value; }
			}
		#endregion
		
		#region BillAddr2
        protected String m_billAddr2;

			[XmlAttribute]
			public String BillAddr2
			{
			get { return m_billAddr2;}
			set { m_billAddr2 = value; }
			}
		#endregion
		
		#region BillAddr3
        protected String m_billAddr3;

			[XmlAttribute]
			public String BillAddr3
			{
			get { return m_billAddr3;}
			set { m_billAddr3 = value; }
			}
		#endregion
		
		#region BillAddr4
        protected String m_billAddr4;

			[XmlAttribute]
			public String BillAddr4
			{
			get { return m_billAddr4;}
			set { m_billAddr4 = value; }
			}
		#endregion
		
		#region BillCity
        protected String m_billCity;

			[XmlAttribute]
			public String BillCity
			{
			get { return m_billCity;}
			set { m_billCity = value; }
			}
		#endregion
		
		#region BillState
        protected String m_billState;

			[XmlAttribute]
			public String BillState
			{
			get { return m_billState;}
			set { m_billState = value; }
			}
		#endregion
		
		#region BillPostalCode
        protected String m_billPostalCode;

			[XmlAttribute]
			public String BillPostalCode
			{
			get { return m_billPostalCode;}
			set { m_billPostalCode = value; }
			}
		#endregion
		
		#region BillCountry
        protected String m_billCountry;

			[XmlAttribute]
			public String BillCountry
			{
			get { return m_billCountry;}
			set { m_billCountry = value; }
			}
		#endregion
		
		#region ShipAddr1
        protected String m_shipAddr1;

			[XmlAttribute]
			public String ShipAddr1
			{
			get { return m_shipAddr1;}
			set { m_shipAddr1 = value; }
			}
		#endregion
		
		#region ShipAddr2
        protected String m_shipAddr2;

			[XmlAttribute]
			public String ShipAddr2
			{
			get { return m_shipAddr2;}
			set { m_shipAddr2 = value; }
			}
		#endregion
		
		#region ShipAddr3
        protected String m_shipAddr3;

			[XmlAttribute]
			public String ShipAddr3
			{
			get { return m_shipAddr3;}
			set { m_shipAddr3 = value; }
			}
		#endregion
		
		#region ShipAddr4
        protected String m_shipAddr4;

			[XmlAttribute]
			public String ShipAddr4
			{
			get { return m_shipAddr4;}
			set { m_shipAddr4 = value; }
			}
		#endregion
		
		#region ShipCity
        protected String m_shipCity;

			[XmlAttribute]
			public String ShipCity
			{
			get { return m_shipCity;}
			set { m_shipCity = value; }
			}
		#endregion
		
		#region ShipState
        protected String m_shipState;

			[XmlAttribute]
			public String ShipState
			{
			get { return m_shipState;}
			set { m_shipState = value; }
			}
		#endregion
		
		#region ShipPostalCode
        protected String m_shipPostalCode;

			[XmlAttribute]
			public String ShipPostalCode
			{
			get { return m_shipPostalCode;}
			set { m_shipPostalCode = value; }
			}
		#endregion
		
		#region ShipCountry
        protected String m_shipCountry;

			[XmlAttribute]
			public String ShipCountry
			{
			get { return m_shipCountry;}
			set { m_shipCountry = value; }
			}
		#endregion
		
		#region PrintAs
        protected String m_printAs;

			[XmlAttribute]
			public String PrintAs
			{
			get { return m_printAs;}
			set { m_printAs = value; }
			}
		#endregion
		
		#region ShippingAddressSameAsBilling
        protected bool m_shippingAddressSameAsBilling;

			[XmlAttribute]
			public bool ShippingAddressSameAsBilling
			{
			get { return m_shippingAddressSameAsBilling;}
			set { m_shippingAddressSameAsBilling = value; }
			}
		#endregion
		
		#region Balance
        protected decimal? m_balance;

			[XmlAttribute]
			public decimal? Balance
			{
			get { return m_balance;}
			set { m_balance = value; }
			}
		#endregion
		
		#region BalanceDate
        protected DateTime? m_balanceDate;

			[XmlAttribute]
			public DateTime? BalanceDate
			{
			get { return m_balanceDate;}
			set { m_balanceDate = value; }
			}
		#endregion
		
		#region ResaleNumber
        protected String m_resaleNumber;

			[XmlAttribute]
			public String ResaleNumber
			{
			get { return m_resaleNumber;}
			set { m_resaleNumber = value; }
			}
		#endregion
		
		#region DeliveryMethod
        protected String m_deliveryMethod;

			[XmlAttribute]
			public String DeliveryMethod
			{
			get { return m_deliveryMethod;}
			set { m_deliveryMethod = value; }
			}
		#endregion
		
		#region EntityState
			protected EntityState m_entityState;

			[XmlElement]
			public EntityState EntityState
			{
			get { return m_entityState;}
			set { m_entityState = value; }
			}
		#endregion
		
		#region Terms
			protected Terms m_terms;

			[XmlElement]
			public Terms Terms
			{
			get { return m_terms;}
			set { m_terms = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public Customer(
		int customerId

		)
		{
		
			m_customerId = customerId;
		
        }

      


        public Customer(
		  EntityState entityState,Terms terms
			  ,
		  int customerId,int? modifiedCustomerId,int? quickBooksListId,int editSequence,String name,String fullName,String salutation,String firstName,String middleName,String lastName,String suffix,String companyName,String phone,String mobile,String email,String pager,String altPhone,String fax,String billAddr1,String billAddr2,String billAddr3,String billAddr4,String billCity,String billState,String billPostalCode,String billCountry,String shipAddr1,String shipAddr2,String shipAddr3,String shipAddr4,String shipCity,String shipState,String shipPostalCode,String shipCountry,String printAs,bool shippingAddressSameAsBilling,decimal? balance,DateTime? balanceDate,String resaleNumber,String deliveryMethod
		  )
		  {

		  
			  m_entityState = entityState;
		  
			  m_terms = terms;
		  
			  m_customerId = customerId;
		  
			  m_modifiedCustomerId = modifiedCustomerId;
		  
			  m_quickBooksListId = quickBooksListId;
		  
			  m_editSequence = editSequence;
		  
			  m_name = name;
		  
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
		  
			  m_billAddr1 = billAddr1;
		  
			  m_billAddr2 = billAddr2;
		  
			  m_billAddr3 = billAddr3;
		  
			  m_billAddr4 = billAddr4;
		  
			  m_billCity = billCity;
		  
			  m_billState = billState;
		  
			  m_billPostalCode = billPostalCode;
		  
			  m_billCountry = billCountry;
		  
			  m_shipAddr1 = shipAddr1;
		  
			  m_shipAddr2 = shipAddr2;
		  
			  m_shipAddr3 = shipAddr3;
		  
			  m_shipAddr4 = shipAddr4;
		  
			  m_shipCity = shipCity;
		  
			  m_shipState = shipState;
		  
			  m_shipPostalCode = shipPostalCode;
		  
			  m_shipCountry = shipCountry;
		  
			  m_printAs = printAs;
		  
			  m_shippingAddressSameAsBilling = shippingAddressSameAsBilling;
		  
			  m_balance = balance;
		  
			  m_balanceDate = balanceDate;
		  
			  m_resaleNumber = resaleNumber;
		  
			  m_deliveryMethod = deliveryMethod;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    