
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class Company
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Company] ( " +
      
        " CompanyId, " +
        " IsSampleCompany, " +
        " CompanyName, " +
        " LegalCompanyName, " +
        " Addr1, " +
        " Addr2, " +
        " Addr3, " +
        " Addr4, " +
        " City, " +
        " State, " +
        " PostalCode, " +
        " Country, " +
        " LegalAddr1, " +
        " LegalAddr2, " +
        " LegalAddr3, " +
        " LegalAddr4, " +
        " LegalCity, " +
        " LegalState, " +
        " LegalPostalCode, " +
        " LegalCountry, " +
        " ForCustomerAddr1, " +
        " ForCustomerAddr2, " +
        " ForCustomerAddr3, " +
        " ForCustomerAddr4, " +
        " ForCustomerCity, " +
        " ForCustomerState, " +
        " ForCustomerPostalCode, " +
        " ForCustomerCountry, " +
        " Phone, " +
        " Email, " +
        " CompanyEmailForCustomer, " +
        " FirstMonthFiscalYear, " +
        " FirstMonthIncomeTaxYear, " +
        " CompanyType " +
        ") Values (" +
      
        " @CompanyId, " +
        " @IsSampleCompany, " +
        " @CompanyName, " +
        " @LegalCompanyName, " +
        " @Addr1, " +
        " @Addr2, " +
        " @Addr3, " +
        " @Addr4, " +
        " @City, " +
        " @State, " +
        " @PostalCode, " +
        " @Country, " +
        " @LegalAddr1, " +
        " @LegalAddr2, " +
        " @LegalAddr3, " +
        " @LegalAddr4, " +
        " @LegalCity, " +
        " @LegalState, " +
        " @LegalPostalCode, " +
        " @LegalCountry, " +
        " @ForCustomerAddr1, " +
        " @ForCustomerAddr2, " +
        " @ForCustomerAddr3, " +
        " @ForCustomerAddr4, " +
        " @ForCustomerCity, " +
        " @ForCustomerState, " +
        " @ForCustomerPostalCode, " +
        " @ForCustomerCountry, " +
        " @Phone, " +
        " @Email, " +
        " @CompanyEmailForCustomer, " +
        " @FirstMonthFiscalYear, " +
        " @FirstMonthIncomeTaxYear, " +
        " @CompanyType " +
      ")";

      public static void Insert(Company company)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@CompanyId", company.CompanyId);            
          
              Database.PutParameter(dbCommand,"@IsSampleCompany", company.IsSampleCompany);            
          
              Database.PutParameter(dbCommand,"@CompanyName", company.CompanyName);            
          
              Database.PutParameter(dbCommand,"@LegalCompanyName", company.LegalCompanyName);            
          
              Database.PutParameter(dbCommand,"@Addr1", company.Addr1);            
          
              Database.PutParameter(dbCommand,"@Addr2", company.Addr2);            
          
              Database.PutParameter(dbCommand,"@Addr3", company.Addr3);            
          
              Database.PutParameter(dbCommand,"@Addr4", company.Addr4);            
          
              Database.PutParameter(dbCommand,"@City", company.City);            
          
              Database.PutParameter(dbCommand,"@State", company.State);            
          
              Database.PutParameter(dbCommand,"@PostalCode", company.PostalCode);            
          
              Database.PutParameter(dbCommand,"@Country", company.Country);            
          
              Database.PutParameter(dbCommand,"@LegalAddr1", company.LegalAddr1);            
          
              Database.PutParameter(dbCommand,"@LegalAddr2", company.LegalAddr2);            
          
              Database.PutParameter(dbCommand,"@LegalAddr3", company.LegalAddr3);            
          
              Database.PutParameter(dbCommand,"@LegalAddr4", company.LegalAddr4);            
          
              Database.PutParameter(dbCommand,"@LegalCity", company.LegalCity);            
          
              Database.PutParameter(dbCommand,"@LegalState", company.LegalState);            
          
              Database.PutParameter(dbCommand,"@LegalPostalCode", company.LegalPostalCode);            
          
              Database.PutParameter(dbCommand,"@LegalCountry", company.LegalCountry);            
          
              Database.PutParameter(dbCommand,"@ForCustomerAddr1", company.ForCustomerAddr1);            
          
              Database.PutParameter(dbCommand,"@ForCustomerAddr2", company.ForCustomerAddr2);            
          
              Database.PutParameter(dbCommand,"@ForCustomerAddr3", company.ForCustomerAddr3);            
          
              Database.PutParameter(dbCommand,"@ForCustomerAddr4", company.ForCustomerAddr4);            
          
              Database.PutParameter(dbCommand,"@ForCustomerCity", company.ForCustomerCity);            
          
              Database.PutParameter(dbCommand,"@ForCustomerState", company.ForCustomerState);            
          
              Database.PutParameter(dbCommand,"@ForCustomerPostalCode", company.ForCustomerPostalCode);            
          
              Database.PutParameter(dbCommand,"@ForCustomerCountry", company.ForCustomerCountry);            
          
              Database.PutParameter(dbCommand,"@Phone", company.Phone);            
          
              Database.PutParameter(dbCommand,"@Email", company.Email);            
          
              Database.PutParameter(dbCommand,"@CompanyEmailForCustomer", company.CompanyEmailForCustomer);            
          
              Database.PutParameter(dbCommand,"@FirstMonthFiscalYear", company.FirstMonthFiscalYear);            
          
              Database.PutParameter(dbCommand,"@FirstMonthIncomeTaxYear", company.FirstMonthIncomeTaxYear);            
          
              Database.PutParameter(dbCommand,"@CompanyType", company.CompanyType);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Company>  companyList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Company company in  companyList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@CompanyId", company.CompanyId);
          
            Database.PutParameter(dbCommand,"@IsSampleCompany", company.IsSampleCompany);
          
            Database.PutParameter(dbCommand,"@CompanyName", company.CompanyName);
          
            Database.PutParameter(dbCommand,"@LegalCompanyName", company.LegalCompanyName);
          
            Database.PutParameter(dbCommand,"@Addr1", company.Addr1);
          
            Database.PutParameter(dbCommand,"@Addr2", company.Addr2);
          
            Database.PutParameter(dbCommand,"@Addr3", company.Addr3);
          
            Database.PutParameter(dbCommand,"@Addr4", company.Addr4);
          
            Database.PutParameter(dbCommand,"@City", company.City);
          
            Database.PutParameter(dbCommand,"@State", company.State);
          
            Database.PutParameter(dbCommand,"@PostalCode", company.PostalCode);
          
            Database.PutParameter(dbCommand,"@Country", company.Country);
          
            Database.PutParameter(dbCommand,"@LegalAddr1", company.LegalAddr1);
          
            Database.PutParameter(dbCommand,"@LegalAddr2", company.LegalAddr2);
          
            Database.PutParameter(dbCommand,"@LegalAddr3", company.LegalAddr3);
          
            Database.PutParameter(dbCommand,"@LegalAddr4", company.LegalAddr4);
          
            Database.PutParameter(dbCommand,"@LegalCity", company.LegalCity);
          
            Database.PutParameter(dbCommand,"@LegalState", company.LegalState);
          
            Database.PutParameter(dbCommand,"@LegalPostalCode", company.LegalPostalCode);
          
            Database.PutParameter(dbCommand,"@LegalCountry", company.LegalCountry);
          
            Database.PutParameter(dbCommand,"@ForCustomerAddr1", company.ForCustomerAddr1);
          
            Database.PutParameter(dbCommand,"@ForCustomerAddr2", company.ForCustomerAddr2);
          
            Database.PutParameter(dbCommand,"@ForCustomerAddr3", company.ForCustomerAddr3);
          
            Database.PutParameter(dbCommand,"@ForCustomerAddr4", company.ForCustomerAddr4);
          
            Database.PutParameter(dbCommand,"@ForCustomerCity", company.ForCustomerCity);
          
            Database.PutParameter(dbCommand,"@ForCustomerState", company.ForCustomerState);
          
            Database.PutParameter(dbCommand,"@ForCustomerPostalCode", company.ForCustomerPostalCode);
          
            Database.PutParameter(dbCommand,"@ForCustomerCountry", company.ForCustomerCountry);
          
            Database.PutParameter(dbCommand,"@Phone", company.Phone);
          
            Database.PutParameter(dbCommand,"@Email", company.Email);
          
            Database.PutParameter(dbCommand,"@CompanyEmailForCustomer", company.CompanyEmailForCustomer);
          
            Database.PutParameter(dbCommand,"@FirstMonthFiscalYear", company.FirstMonthFiscalYear);
          
            Database.PutParameter(dbCommand,"@FirstMonthIncomeTaxYear", company.FirstMonthIncomeTaxYear);
          
            Database.PutParameter(dbCommand,"@CompanyType", company.CompanyType);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@CompanyId",company.CompanyId);
          
            Database.UpdateParameter(dbCommand,"@IsSampleCompany",company.IsSampleCompany);
          
            Database.UpdateParameter(dbCommand,"@CompanyName",company.CompanyName);
          
            Database.UpdateParameter(dbCommand,"@LegalCompanyName",company.LegalCompanyName);
          
            Database.UpdateParameter(dbCommand,"@Addr1",company.Addr1);
          
            Database.UpdateParameter(dbCommand,"@Addr2",company.Addr2);
          
            Database.UpdateParameter(dbCommand,"@Addr3",company.Addr3);
          
            Database.UpdateParameter(dbCommand,"@Addr4",company.Addr4);
          
            Database.UpdateParameter(dbCommand,"@City",company.City);
          
            Database.UpdateParameter(dbCommand,"@State",company.State);
          
            Database.UpdateParameter(dbCommand,"@PostalCode",company.PostalCode);
          
            Database.UpdateParameter(dbCommand,"@Country",company.Country);
          
            Database.UpdateParameter(dbCommand,"@LegalAddr1",company.LegalAddr1);
          
            Database.UpdateParameter(dbCommand,"@LegalAddr2",company.LegalAddr2);
          
            Database.UpdateParameter(dbCommand,"@LegalAddr3",company.LegalAddr3);
          
            Database.UpdateParameter(dbCommand,"@LegalAddr4",company.LegalAddr4);
          
            Database.UpdateParameter(dbCommand,"@LegalCity",company.LegalCity);
          
            Database.UpdateParameter(dbCommand,"@LegalState",company.LegalState);
          
            Database.UpdateParameter(dbCommand,"@LegalPostalCode",company.LegalPostalCode);
          
            Database.UpdateParameter(dbCommand,"@LegalCountry",company.LegalCountry);
          
            Database.UpdateParameter(dbCommand,"@ForCustomerAddr1",company.ForCustomerAddr1);
          
            Database.UpdateParameter(dbCommand,"@ForCustomerAddr2",company.ForCustomerAddr2);
          
            Database.UpdateParameter(dbCommand,"@ForCustomerAddr3",company.ForCustomerAddr3);
          
            Database.UpdateParameter(dbCommand,"@ForCustomerAddr4",company.ForCustomerAddr4);
          
            Database.UpdateParameter(dbCommand,"@ForCustomerCity",company.ForCustomerCity);
          
            Database.UpdateParameter(dbCommand,"@ForCustomerState",company.ForCustomerState);
          
            Database.UpdateParameter(dbCommand,"@ForCustomerPostalCode",company.ForCustomerPostalCode);
          
            Database.UpdateParameter(dbCommand,"@ForCustomerCountry",company.ForCustomerCountry);
          
            Database.UpdateParameter(dbCommand,"@Phone",company.Phone);
          
            Database.UpdateParameter(dbCommand,"@Email",company.Email);
          
            Database.UpdateParameter(dbCommand,"@CompanyEmailForCustomer",company.CompanyEmailForCustomer);
          
            Database.UpdateParameter(dbCommand,"@FirstMonthFiscalYear",company.FirstMonthFiscalYear);
          
            Database.UpdateParameter(dbCommand,"@FirstMonthIncomeTaxYear",company.FirstMonthIncomeTaxYear);
          
            Database.UpdateParameter(dbCommand,"@CompanyType",company.CompanyType);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Company] Set "
      
        + " IsSampleCompany = @IsSampleCompany, "
        + " CompanyName = @CompanyName, "
        + " LegalCompanyName = @LegalCompanyName, "
        + " Addr1 = @Addr1, "
        + " Addr2 = @Addr2, "
        + " Addr3 = @Addr3, "
        + " Addr4 = @Addr4, "
        + " City = @City, "
        + " State = @State, "
        + " PostalCode = @PostalCode, "
        + " Country = @Country, "
        + " LegalAddr1 = @LegalAddr1, "
        + " LegalAddr2 = @LegalAddr2, "
        + " LegalAddr3 = @LegalAddr3, "
        + " LegalAddr4 = @LegalAddr4, "
        + " LegalCity = @LegalCity, "
        + " LegalState = @LegalState, "
        + " LegalPostalCode = @LegalPostalCode, "
        + " LegalCountry = @LegalCountry, "
        + " ForCustomerAddr1 = @ForCustomerAddr1, "
        + " ForCustomerAddr2 = @ForCustomerAddr2, "
        + " ForCustomerAddr3 = @ForCustomerAddr3, "
        + " ForCustomerAddr4 = @ForCustomerAddr4, "
        + " ForCustomerCity = @ForCustomerCity, "
        + " ForCustomerState = @ForCustomerState, "
        + " ForCustomerPostalCode = @ForCustomerPostalCode, "
        + " ForCustomerCountry = @ForCustomerCountry, "
        + " Phone = @Phone, "
        + " Email = @Email, "
        + " CompanyEmailForCustomer = @CompanyEmailForCustomer, "
        + " FirstMonthFiscalYear = @FirstMonthFiscalYear, "
        + " FirstMonthIncomeTaxYear = @FirstMonthIncomeTaxYear, "
        + " CompanyType = @CompanyType "
        + " Where "
        
          + " CompanyId = @CompanyId "
        
      ;

      public static void Update(Company company)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@CompanyId", company.CompanyId);            
          
            Database.PutParameter(dbCommand,"@IsSampleCompany", company.IsSampleCompany);            
          
            Database.PutParameter(dbCommand,"@CompanyName", company.CompanyName);            
          
            Database.PutParameter(dbCommand,"@LegalCompanyName", company.LegalCompanyName);            
          
            Database.PutParameter(dbCommand,"@Addr1", company.Addr1);            
          
            Database.PutParameter(dbCommand,"@Addr2", company.Addr2);            
          
            Database.PutParameter(dbCommand,"@Addr3", company.Addr3);            
          
            Database.PutParameter(dbCommand,"@Addr4", company.Addr4);            
          
            Database.PutParameter(dbCommand,"@City", company.City);            
          
            Database.PutParameter(dbCommand,"@State", company.State);            
          
            Database.PutParameter(dbCommand,"@PostalCode", company.PostalCode);            
          
            Database.PutParameter(dbCommand,"@Country", company.Country);            
          
            Database.PutParameter(dbCommand,"@LegalAddr1", company.LegalAddr1);            
          
            Database.PutParameter(dbCommand,"@LegalAddr2", company.LegalAddr2);            
          
            Database.PutParameter(dbCommand,"@LegalAddr3", company.LegalAddr3);            
          
            Database.PutParameter(dbCommand,"@LegalAddr4", company.LegalAddr4);            
          
            Database.PutParameter(dbCommand,"@LegalCity", company.LegalCity);            
          
            Database.PutParameter(dbCommand,"@LegalState", company.LegalState);            
          
            Database.PutParameter(dbCommand,"@LegalPostalCode", company.LegalPostalCode);            
          
            Database.PutParameter(dbCommand,"@LegalCountry", company.LegalCountry);            
          
            Database.PutParameter(dbCommand,"@ForCustomerAddr1", company.ForCustomerAddr1);            
          
            Database.PutParameter(dbCommand,"@ForCustomerAddr2", company.ForCustomerAddr2);            
          
            Database.PutParameter(dbCommand,"@ForCustomerAddr3", company.ForCustomerAddr3);            
          
            Database.PutParameter(dbCommand,"@ForCustomerAddr4", company.ForCustomerAddr4);            
          
            Database.PutParameter(dbCommand,"@ForCustomerCity", company.ForCustomerCity);            
          
            Database.PutParameter(dbCommand,"@ForCustomerState", company.ForCustomerState);            
          
            Database.PutParameter(dbCommand,"@ForCustomerPostalCode", company.ForCustomerPostalCode);            
          
            Database.PutParameter(dbCommand,"@ForCustomerCountry", company.ForCustomerCountry);            
          
            Database.PutParameter(dbCommand,"@Phone", company.Phone);            
          
            Database.PutParameter(dbCommand,"@Email", company.Email);            
          
            Database.PutParameter(dbCommand,"@CompanyEmailForCustomer", company.CompanyEmailForCustomer);            
          
            Database.PutParameter(dbCommand,"@FirstMonthFiscalYear", company.FirstMonthFiscalYear);            
          
            Database.PutParameter(dbCommand,"@FirstMonthIncomeTaxYear", company.FirstMonthIncomeTaxYear);            
          
            Database.PutParameter(dbCommand,"@CompanyType", company.CompanyType);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " CompanyId, "
        + " IsSampleCompany, "
        + " CompanyName, "
        + " LegalCompanyName, "
        + " Addr1, "
        + " Addr2, "
        + " Addr3, "
        + " Addr4, "
        + " City, "
        + " State, "
        + " PostalCode, "
        + " Country, "
        + " LegalAddr1, "
        + " LegalAddr2, "
        + " LegalAddr3, "
        + " LegalAddr4, "
        + " LegalCity, "
        + " LegalState, "
        + " LegalPostalCode, "
        + " LegalCountry, "
        + " ForCustomerAddr1, "
        + " ForCustomerAddr2, "
        + " ForCustomerAddr3, "
        + " ForCustomerAddr4, "
        + " ForCustomerCity, "
        + " ForCustomerState, "
        + " ForCustomerPostalCode, "
        + " ForCustomerCountry, "
        + " Phone, "
        + " Email, "
        + " CompanyEmailForCustomer, "
        + " FirstMonthFiscalYear, "
        + " FirstMonthIncomeTaxYear, "
        + " CompanyType "
        + " From [Company] "
      
        + " Where "
        
        + " CompanyId = @CompanyId "
        
      ;

      public static Company FindByPrimaryKey(
      int companyId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CompanyId", companyId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Company not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Company company)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CompanyId",company.CompanyId);
      

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
      String sql = "select 1 from [Company]";

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

      public static Company Load(IDataReader dataReader)
      {
      Company company = new Company();

      company.CompanyId = dataReader.GetInt32(0);
          company.IsSampleCompany = dataReader.GetBoolean(1);
          
            if(!dataReader.IsDBNull(2))
              company.CompanyName = dataReader.GetString(2);
          
            if(!dataReader.IsDBNull(3))
              company.LegalCompanyName = dataReader.GetString(3);
          
            if(!dataReader.IsDBNull(4))
              company.Addr1 = dataReader.GetString(4);
          
            if(!dataReader.IsDBNull(5))
              company.Addr2 = dataReader.GetString(5);
          
            if(!dataReader.IsDBNull(6))
              company.Addr3 = dataReader.GetString(6);
          
            if(!dataReader.IsDBNull(7))
              company.Addr4 = dataReader.GetString(7);
          
            if(!dataReader.IsDBNull(8))
              company.City = dataReader.GetString(8);
          
            if(!dataReader.IsDBNull(9))
              company.State = dataReader.GetString(9);
          
            if(!dataReader.IsDBNull(10))
              company.PostalCode = dataReader.GetString(10);
          
            if(!dataReader.IsDBNull(11))
              company.Country = dataReader.GetString(11);
          
            if(!dataReader.IsDBNull(12))
              company.LegalAddr1 = dataReader.GetString(12);
          
            if(!dataReader.IsDBNull(13))
              company.LegalAddr2 = dataReader.GetString(13);
          
            if(!dataReader.IsDBNull(14))
              company.LegalAddr3 = dataReader.GetString(14);
          
            if(!dataReader.IsDBNull(15))
              company.LegalAddr4 = dataReader.GetString(15);
          
            if(!dataReader.IsDBNull(16))
              company.LegalCity = dataReader.GetString(16);
          
            if(!dataReader.IsDBNull(17))
              company.LegalState = dataReader.GetString(17);
          
            if(!dataReader.IsDBNull(18))
              company.LegalPostalCode = dataReader.GetString(18);
          
            if(!dataReader.IsDBNull(19))
              company.LegalCountry = dataReader.GetString(19);
          
            if(!dataReader.IsDBNull(20))
              company.ForCustomerAddr1 = dataReader.GetString(20);
          
            if(!dataReader.IsDBNull(21))
              company.ForCustomerAddr2 = dataReader.GetString(21);
          
            if(!dataReader.IsDBNull(22))
              company.ForCustomerAddr3 = dataReader.GetString(22);
          
            if(!dataReader.IsDBNull(23))
              company.ForCustomerAddr4 = dataReader.GetString(23);
          
            if(!dataReader.IsDBNull(24))
              company.ForCustomerCity = dataReader.GetString(24);
          
            if(!dataReader.IsDBNull(25))
              company.ForCustomerState = dataReader.GetString(25);
          
            if(!dataReader.IsDBNull(26))
              company.ForCustomerPostalCode = dataReader.GetString(26);
          
            if(!dataReader.IsDBNull(27))
              company.ForCustomerCountry = dataReader.GetString(27);
          
            if(!dataReader.IsDBNull(28))
              company.Phone = dataReader.GetString(28);
          
            if(!dataReader.IsDBNull(29))
              company.Email = dataReader.GetString(29);
          
            if(!dataReader.IsDBNull(30))
              company.CompanyEmailForCustomer = dataReader.GetString(30);
          
            if(!dataReader.IsDBNull(31))
              company.FirstMonthFiscalYear = dataReader.GetString(31);
          
            if(!dataReader.IsDBNull(32))
              company.FirstMonthIncomeTaxYear = dataReader.GetString(32);
          
            if(!dataReader.IsDBNull(33))
              company.CompanyType = dataReader.GetString(33);
          

      return company;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Company] "

      
        + " Where "
        
          + " CompanyId = @CompanyId "
        
      ;
      public static void Delete(Company company)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@CompanyId", company.CompanyId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Company] ";

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
      
        + " CompanyId, "
        + " IsSampleCompany, "
        + " CompanyName, "
        + " LegalCompanyName, "
        + " Addr1, "
        + " Addr2, "
        + " Addr3, "
        + " Addr4, "
        + " City, "
        + " State, "
        + " PostalCode, "
        + " Country, "
        + " LegalAddr1, "
        + " LegalAddr2, "
        + " LegalAddr3, "
        + " LegalAddr4, "
        + " LegalCity, "
        + " LegalState, "
        + " LegalPostalCode, "
        + " LegalCountry, "
        + " ForCustomerAddr1, "
        + " ForCustomerAddr2, "
        + " ForCustomerAddr3, "
        + " ForCustomerAddr4, "
        + " ForCustomerCity, "
        + " ForCustomerState, "
        + " ForCustomerPostalCode, "
        + " ForCustomerCountry, "
        + " Phone, "
        + " Email, "
        + " CompanyEmailForCustomer, "
        + " FirstMonthFiscalYear, "
        + " FirstMonthIncomeTaxYear, "
        + " CompanyType "
        + " From [Company] ";
      public static List<Company> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Company> rv = new List<Company>();

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
      List<Company> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Company> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Company));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Company item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Company>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Company));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Company> itemsList
      = new List<Company>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Company)
      itemsList.Add(deserializedObject as Company);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region CompanyId
        protected int m_companyId;

			[XmlAttribute]
			public int CompanyId
			{
			get { return m_companyId;}
			set { m_companyId = value; }
			}
		#endregion
		
		#region IsSampleCompany
        protected bool m_isSampleCompany;

			[XmlAttribute]
			public bool IsSampleCompany
			{
			get { return m_isSampleCompany;}
			set { m_isSampleCompany = value; }
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
		
		#region LegalCompanyName
        protected String m_legalCompanyName;

			[XmlAttribute]
			public String LegalCompanyName
			{
			get { return m_legalCompanyName;}
			set { m_legalCompanyName = value; }
			}
		#endregion
		
		#region Addr1
        protected String m_addr1;

			[XmlAttribute]
			public String Addr1
			{
			get { return m_addr1;}
			set { m_addr1 = value; }
			}
		#endregion
		
		#region Addr2
        protected String m_addr2;

			[XmlAttribute]
			public String Addr2
			{
			get { return m_addr2;}
			set { m_addr2 = value; }
			}
		#endregion
		
		#region Addr3
        protected String m_addr3;

			[XmlAttribute]
			public String Addr3
			{
			get { return m_addr3;}
			set { m_addr3 = value; }
			}
		#endregion
		
		#region Addr4
        protected String m_addr4;

			[XmlAttribute]
			public String Addr4
			{
			get { return m_addr4;}
			set { m_addr4 = value; }
			}
		#endregion
		
		#region City
        protected String m_city;

			[XmlAttribute]
			public String City
			{
			get { return m_city;}
			set { m_city = value; }
			}
		#endregion
		
		#region State
        protected String m_state;

			[XmlAttribute]
			public String State
			{
			get { return m_state;}
			set { m_state = value; }
			}
		#endregion
		
		#region PostalCode
        protected String m_postalCode;

			[XmlAttribute]
			public String PostalCode
			{
			get { return m_postalCode;}
			set { m_postalCode = value; }
			}
		#endregion
		
		#region Country
        protected String m_country;

			[XmlAttribute]
			public String Country
			{
			get { return m_country;}
			set { m_country = value; }
			}
		#endregion
		
		#region LegalAddr1
        protected String m_legalAddr1;

			[XmlAttribute]
			public String LegalAddr1
			{
			get { return m_legalAddr1;}
			set { m_legalAddr1 = value; }
			}
		#endregion
		
		#region LegalAddr2
        protected String m_legalAddr2;

			[XmlAttribute]
			public String LegalAddr2
			{
			get { return m_legalAddr2;}
			set { m_legalAddr2 = value; }
			}
		#endregion
		
		#region LegalAddr3
        protected String m_legalAddr3;

			[XmlAttribute]
			public String LegalAddr3
			{
			get { return m_legalAddr3;}
			set { m_legalAddr3 = value; }
			}
		#endregion
		
		#region LegalAddr4
        protected String m_legalAddr4;

			[XmlAttribute]
			public String LegalAddr4
			{
			get { return m_legalAddr4;}
			set { m_legalAddr4 = value; }
			}
		#endregion
		
		#region LegalCity
        protected String m_legalCity;

			[XmlAttribute]
			public String LegalCity
			{
			get { return m_legalCity;}
			set { m_legalCity = value; }
			}
		#endregion
		
		#region LegalState
        protected String m_legalState;

			[XmlAttribute]
			public String LegalState
			{
			get { return m_legalState;}
			set { m_legalState = value; }
			}
		#endregion
		
		#region LegalPostalCode
        protected String m_legalPostalCode;

			[XmlAttribute]
			public String LegalPostalCode
			{
			get { return m_legalPostalCode;}
			set { m_legalPostalCode = value; }
			}
		#endregion
		
		#region LegalCountry
        protected String m_legalCountry;

			[XmlAttribute]
			public String LegalCountry
			{
			get { return m_legalCountry;}
			set { m_legalCountry = value; }
			}
		#endregion
		
		#region ForCustomerAddr1
        protected String m_forCustomerAddr1;

			[XmlAttribute]
			public String ForCustomerAddr1
			{
			get { return m_forCustomerAddr1;}
			set { m_forCustomerAddr1 = value; }
			}
		#endregion
		
		#region ForCustomerAddr2
        protected String m_forCustomerAddr2;

			[XmlAttribute]
			public String ForCustomerAddr2
			{
			get { return m_forCustomerAddr2;}
			set { m_forCustomerAddr2 = value; }
			}
		#endregion
		
		#region ForCustomerAddr3
        protected String m_forCustomerAddr3;

			[XmlAttribute]
			public String ForCustomerAddr3
			{
			get { return m_forCustomerAddr3;}
			set { m_forCustomerAddr3 = value; }
			}
		#endregion
		
		#region ForCustomerAddr4
        protected String m_forCustomerAddr4;

			[XmlAttribute]
			public String ForCustomerAddr4
			{
			get { return m_forCustomerAddr4;}
			set { m_forCustomerAddr4 = value; }
			}
		#endregion
		
		#region ForCustomerCity
        protected String m_forCustomerCity;

			[XmlAttribute]
			public String ForCustomerCity
			{
			get { return m_forCustomerCity;}
			set { m_forCustomerCity = value; }
			}
		#endregion
		
		#region ForCustomerState
        protected String m_forCustomerState;

			[XmlAttribute]
			public String ForCustomerState
			{
			get { return m_forCustomerState;}
			set { m_forCustomerState = value; }
			}
		#endregion
		
		#region ForCustomerPostalCode
        protected String m_forCustomerPostalCode;

			[XmlAttribute]
			public String ForCustomerPostalCode
			{
			get { return m_forCustomerPostalCode;}
			set { m_forCustomerPostalCode = value; }
			}
		#endregion
		
		#region ForCustomerCountry
        protected String m_forCustomerCountry;

			[XmlAttribute]
			public String ForCustomerCountry
			{
			get { return m_forCustomerCountry;}
			set { m_forCustomerCountry = value; }
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
		
		#region Email
        protected String m_email;

			[XmlAttribute]
			public String Email
			{
			get { return m_email;}
			set { m_email = value; }
			}
		#endregion
		
		#region CompanyEmailForCustomer
        protected String m_companyEmailForCustomer;

			[XmlAttribute]
			public String CompanyEmailForCustomer
			{
			get { return m_companyEmailForCustomer;}
			set { m_companyEmailForCustomer = value; }
			}
		#endregion
		
		#region FirstMonthFiscalYear
        protected String m_firstMonthFiscalYear;

			[XmlAttribute]
			public String FirstMonthFiscalYear
			{
			get { return m_firstMonthFiscalYear;}
			set { m_firstMonthFiscalYear = value; }
			}
		#endregion
		
		#region FirstMonthIncomeTaxYear
        protected String m_firstMonthIncomeTaxYear;

			[XmlAttribute]
			public String FirstMonthIncomeTaxYear
			{
			get { return m_firstMonthIncomeTaxYear;}
			set { m_firstMonthIncomeTaxYear = value; }
			}
		#endregion
		
		#region CompanyType
        protected String m_companyType;

			[XmlAttribute]
			public String CompanyType
			{
			get { return m_companyType;}
			set { m_companyType = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public Company(
		int companyId

		)
		{
		
			m_companyId = companyId;
		
        }

      


        public Company(
		  int companyId,bool isSampleCompany,String companyName,String legalCompanyName,String addr1,String addr2,String addr3,String addr4,String city,String state,String postalCode,String country,String legalAddr1,String legalAddr2,String legalAddr3,String legalAddr4,String legalCity,String legalState,String legalPostalCode,String legalCountry,String forCustomerAddr1,String forCustomerAddr2,String forCustomerAddr3,String forCustomerAddr4,String forCustomerCity,String forCustomerState,String forCustomerPostalCode,String forCustomerCountry,String phone,String email,String companyEmailForCustomer,String firstMonthFiscalYear,String firstMonthIncomeTaxYear,String companyType
		  )
		  {

		  
			  m_companyId = companyId;
		  
			  m_isSampleCompany = isSampleCompany;
		  
			  m_companyName = companyName;
		  
			  m_legalCompanyName = legalCompanyName;
		  
			  m_addr1 = addr1;
		  
			  m_addr2 = addr2;
		  
			  m_addr3 = addr3;
		  
			  m_addr4 = addr4;
		  
			  m_city = city;
		  
			  m_state = state;
		  
			  m_postalCode = postalCode;
		  
			  m_country = country;
		  
			  m_legalAddr1 = legalAddr1;
		  
			  m_legalAddr2 = legalAddr2;
		  
			  m_legalAddr3 = legalAddr3;
		  
			  m_legalAddr4 = legalAddr4;
		  
			  m_legalCity = legalCity;
		  
			  m_legalState = legalState;
		  
			  m_legalPostalCode = legalPostalCode;
		  
			  m_legalCountry = legalCountry;
		  
			  m_forCustomerAddr1 = forCustomerAddr1;
		  
			  m_forCustomerAddr2 = forCustomerAddr2;
		  
			  m_forCustomerAddr3 = forCustomerAddr3;
		  
			  m_forCustomerAddr4 = forCustomerAddr4;
		  
			  m_forCustomerCity = forCustomerCity;
		  
			  m_forCustomerState = forCustomerState;
		  
			  m_forCustomerPostalCode = forCustomerPostalCode;
		  
			  m_forCustomerCountry = forCustomerCountry;
		  
			  m_phone = phone;
		  
			  m_email = email;
		  
			  m_companyEmailForCustomer = companyEmailForCustomer;
		  
			  m_firstMonthFiscalYear = firstMonthFiscalYear;
		  
			  m_firstMonthIncomeTaxYear = firstMonthIncomeTaxYear;
		  
			  m_companyType = companyType;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    