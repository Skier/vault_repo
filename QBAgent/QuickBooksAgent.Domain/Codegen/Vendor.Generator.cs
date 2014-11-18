
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class Vendor
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Vendor] ( " +
      
        " VendorId, " +
        " ModifiedVendorId, " +
        " QuickBooksListId, " +
        " EntityStateId, " +
        " EditSequence, " +
        " Name, " +
        " CompanyName, " +
        " Salutation, " +
        " FirstName, " +
        " MiddleName, " +
        " LastName, " +
        " Suffix, " +
        " Addr1, " +
        " Addr2, " +
        " Addr3, " +
        " Addr4, " +
        " City, " +
        " State, " +
        " PostalCode, " +
        " Country, " +
        " Phone, " +
        " Mobile, " +
        " Pager, " +
        " AltPhone, " +
        " Fax, " +
        " Email, " +
        " NameOnCheck, " +
        " VendorTaxIdent, " +
        " IsVendorEligibleFor1099, " +
        " Balance " +
        ") Values (" +
      
        " @VendorId, " +
        " @ModifiedVendorId, " +
        " @QuickBooksListId, " +
        " @EntityStateId, " +
        " @EditSequence, " +
        " @Name, " +
        " @CompanyName, " +
        " @Salutation, " +
        " @FirstName, " +
        " @MiddleName, " +
        " @LastName, " +
        " @Suffix, " +
        " @Addr1, " +
        " @Addr2, " +
        " @Addr3, " +
        " @Addr4, " +
        " @City, " +
        " @State, " +
        " @PostalCode, " +
        " @Country, " +
        " @Phone, " +
        " @Mobile, " +
        " @Pager, " +
        " @AltPhone, " +
        " @Fax, " +
        " @Email, " +
        " @NameOnCheck, " +
        " @VendorTaxIdent, " +
        " @IsVendorEligibleFor1099, " +
        " @Balance " +
      ")";

      public static void Insert(Vendor vendor)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@VendorId", vendor.VendorId);            
          
              Database.PutParameter(dbCommand,"@ModifiedVendorId", vendor.ModifiedVendorId);            
          
              Database.PutParameter(dbCommand,"@QuickBooksListId", vendor.QuickBooksListId);            
          
              Database.PutParameter(dbCommand,"@EntityStateId", vendor
			.EntityState.EntityStateId);            
          
              Database.PutParameter(dbCommand,"@EditSequence", vendor.EditSequence);            
          
              Database.PutParameter(dbCommand,"@Name", vendor.Name);            
          
              Database.PutParameter(dbCommand,"@CompanyName", vendor.CompanyName);            
          
              Database.PutParameter(dbCommand,"@Salutation", vendor.Salutation);            
          
              Database.PutParameter(dbCommand,"@FirstName", vendor.FirstName);            
          
              Database.PutParameter(dbCommand,"@MiddleName", vendor.MiddleName);            
          
              Database.PutParameter(dbCommand,"@LastName", vendor.LastName);            
          
              Database.PutParameter(dbCommand,"@Suffix", vendor.Suffix);            
          
              Database.PutParameter(dbCommand,"@Addr1", vendor.Addr1);            
          
              Database.PutParameter(dbCommand,"@Addr2", vendor.Addr2);            
          
              Database.PutParameter(dbCommand,"@Addr3", vendor.Addr3);            
          
              Database.PutParameter(dbCommand,"@Addr4", vendor.Addr4);            
          
              Database.PutParameter(dbCommand,"@City", vendor.City);            
          
              Database.PutParameter(dbCommand,"@State", vendor.State);            
          
              Database.PutParameter(dbCommand,"@PostalCode", vendor.PostalCode);            
          
              Database.PutParameter(dbCommand,"@Country", vendor.Country);            
          
              Database.PutParameter(dbCommand,"@Phone", vendor.Phone);            
          
              Database.PutParameter(dbCommand,"@Mobile", vendor.Mobile);            
          
              Database.PutParameter(dbCommand,"@Pager", vendor.Pager);            
          
              Database.PutParameter(dbCommand,"@AltPhone", vendor.AltPhone);            
          
              Database.PutParameter(dbCommand,"@Fax", vendor.Fax);            
          
              Database.PutParameter(dbCommand,"@Email", vendor.Email);            
          
              Database.PutParameter(dbCommand,"@NameOnCheck", vendor.NameOnCheck);            
          
              Database.PutParameter(dbCommand,"@VendorTaxIdent", vendor.VendorTaxIdent);            
          
              Database.PutParameter(dbCommand,"@IsVendorEligibleFor1099", vendor.IsVendorEligibleFor1099);            
          
              Database.PutParameter(dbCommand,"@Balance", vendor.Balance);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Vendor>  vendorList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Vendor vendor in  vendorList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@VendorId", vendor.VendorId);
          
            Database.PutParameter(dbCommand,"@ModifiedVendorId", vendor.ModifiedVendorId);
          
            Database.PutParameter(dbCommand,"@QuickBooksListId", vendor.QuickBooksListId);
          
            Database.PutParameter(dbCommand,"@EntityStateId", vendor
			.EntityState.EntityStateId);
          
            Database.PutParameter(dbCommand,"@EditSequence", vendor.EditSequence);
          
            Database.PutParameter(dbCommand,"@Name", vendor.Name);
          
            Database.PutParameter(dbCommand,"@CompanyName", vendor.CompanyName);
          
            Database.PutParameter(dbCommand,"@Salutation", vendor.Salutation);
          
            Database.PutParameter(dbCommand,"@FirstName", vendor.FirstName);
          
            Database.PutParameter(dbCommand,"@MiddleName", vendor.MiddleName);
          
            Database.PutParameter(dbCommand,"@LastName", vendor.LastName);
          
            Database.PutParameter(dbCommand,"@Suffix", vendor.Suffix);
          
            Database.PutParameter(dbCommand,"@Addr1", vendor.Addr1);
          
            Database.PutParameter(dbCommand,"@Addr2", vendor.Addr2);
          
            Database.PutParameter(dbCommand,"@Addr3", vendor.Addr3);
          
            Database.PutParameter(dbCommand,"@Addr4", vendor.Addr4);
          
            Database.PutParameter(dbCommand,"@City", vendor.City);
          
            Database.PutParameter(dbCommand,"@State", vendor.State);
          
            Database.PutParameter(dbCommand,"@PostalCode", vendor.PostalCode);
          
            Database.PutParameter(dbCommand,"@Country", vendor.Country);
          
            Database.PutParameter(dbCommand,"@Phone", vendor.Phone);
          
            Database.PutParameter(dbCommand,"@Mobile", vendor.Mobile);
          
            Database.PutParameter(dbCommand,"@Pager", vendor.Pager);
          
            Database.PutParameter(dbCommand,"@AltPhone", vendor.AltPhone);
          
            Database.PutParameter(dbCommand,"@Fax", vendor.Fax);
          
            Database.PutParameter(dbCommand,"@Email", vendor.Email);
          
            Database.PutParameter(dbCommand,"@NameOnCheck", vendor.NameOnCheck);
          
            Database.PutParameter(dbCommand,"@VendorTaxIdent", vendor.VendorTaxIdent);
          
            Database.PutParameter(dbCommand,"@IsVendorEligibleFor1099", vendor.IsVendorEligibleFor1099);
          
            Database.PutParameter(dbCommand,"@Balance", vendor.Balance);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@VendorId",vendor.VendorId);
          
            Database.UpdateParameter(dbCommand,"@ModifiedVendorId",vendor.ModifiedVendorId);
          
            Database.UpdateParameter(dbCommand,"@QuickBooksListId",vendor.QuickBooksListId);
          
            Database.UpdateParameter(dbCommand,"@EntityStateId",vendor
			.EntityState.EntityStateId);
          
            Database.UpdateParameter(dbCommand,"@EditSequence",vendor.EditSequence);
          
            Database.UpdateParameter(dbCommand,"@Name",vendor.Name);
          
            Database.UpdateParameter(dbCommand,"@CompanyName",vendor.CompanyName);
          
            Database.UpdateParameter(dbCommand,"@Salutation",vendor.Salutation);
          
            Database.UpdateParameter(dbCommand,"@FirstName",vendor.FirstName);
          
            Database.UpdateParameter(dbCommand,"@MiddleName",vendor.MiddleName);
          
            Database.UpdateParameter(dbCommand,"@LastName",vendor.LastName);
          
            Database.UpdateParameter(dbCommand,"@Suffix",vendor.Suffix);
          
            Database.UpdateParameter(dbCommand,"@Addr1",vendor.Addr1);
          
            Database.UpdateParameter(dbCommand,"@Addr2",vendor.Addr2);
          
            Database.UpdateParameter(dbCommand,"@Addr3",vendor.Addr3);
          
            Database.UpdateParameter(dbCommand,"@Addr4",vendor.Addr4);
          
            Database.UpdateParameter(dbCommand,"@City",vendor.City);
          
            Database.UpdateParameter(dbCommand,"@State",vendor.State);
          
            Database.UpdateParameter(dbCommand,"@PostalCode",vendor.PostalCode);
          
            Database.UpdateParameter(dbCommand,"@Country",vendor.Country);
          
            Database.UpdateParameter(dbCommand,"@Phone",vendor.Phone);
          
            Database.UpdateParameter(dbCommand,"@Mobile",vendor.Mobile);
          
            Database.UpdateParameter(dbCommand,"@Pager",vendor.Pager);
          
            Database.UpdateParameter(dbCommand,"@AltPhone",vendor.AltPhone);
          
            Database.UpdateParameter(dbCommand,"@Fax",vendor.Fax);
          
            Database.UpdateParameter(dbCommand,"@Email",vendor.Email);
          
            Database.UpdateParameter(dbCommand,"@NameOnCheck",vendor.NameOnCheck);
          
            Database.UpdateParameter(dbCommand,"@VendorTaxIdent",vendor.VendorTaxIdent);
          
            Database.UpdateParameter(dbCommand,"@IsVendorEligibleFor1099",vendor.IsVendorEligibleFor1099);
          
            Database.UpdateParameter(dbCommand,"@Balance",vendor.Balance);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Vendor] Set "
      
        + " ModifiedVendorId = @ModifiedVendorId, "
        + " QuickBooksListId = @QuickBooksListId, "
        + " EntityStateId = @EntityStateId, "
        + " EditSequence = @EditSequence, "
        + " Name = @Name, "
        + " CompanyName = @CompanyName, "
        + " Salutation = @Salutation, "
        + " FirstName = @FirstName, "
        + " MiddleName = @MiddleName, "
        + " LastName = @LastName, "
        + " Suffix = @Suffix, "
        + " Addr1 = @Addr1, "
        + " Addr2 = @Addr2, "
        + " Addr3 = @Addr3, "
        + " Addr4 = @Addr4, "
        + " City = @City, "
        + " State = @State, "
        + " PostalCode = @PostalCode, "
        + " Country = @Country, "
        + " Phone = @Phone, "
        + " Mobile = @Mobile, "
        + " Pager = @Pager, "
        + " AltPhone = @AltPhone, "
        + " Fax = @Fax, "
        + " Email = @Email, "
        + " NameOnCheck = @NameOnCheck, "
        + " VendorTaxIdent = @VendorTaxIdent, "
        + " IsVendorEligibleFor1099 = @IsVendorEligibleFor1099, "
        + " Balance = @Balance "
        + " Where "
        
          + " VendorId = @VendorId "
        
      ;

      public static void Update(Vendor vendor)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@VendorId", vendor.VendorId);            
          
            Database.PutParameter(dbCommand,"@ModifiedVendorId", vendor.ModifiedVendorId);            
          
            Database.PutParameter(dbCommand,"@QuickBooksListId", vendor.QuickBooksListId);            
          
            Database.PutParameter(dbCommand,"@EntityStateId", vendor
			.EntityState.EntityStateId);            
          
            Database.PutParameter(dbCommand,"@EditSequence", vendor.EditSequence);            
          
            Database.PutParameter(dbCommand,"@Name", vendor.Name);            
          
            Database.PutParameter(dbCommand,"@CompanyName", vendor.CompanyName);            
          
            Database.PutParameter(dbCommand,"@Salutation", vendor.Salutation);            
          
            Database.PutParameter(dbCommand,"@FirstName", vendor.FirstName);            
          
            Database.PutParameter(dbCommand,"@MiddleName", vendor.MiddleName);            
          
            Database.PutParameter(dbCommand,"@LastName", vendor.LastName);            
          
            Database.PutParameter(dbCommand,"@Suffix", vendor.Suffix);            
          
            Database.PutParameter(dbCommand,"@Addr1", vendor.Addr1);            
          
            Database.PutParameter(dbCommand,"@Addr2", vendor.Addr2);            
          
            Database.PutParameter(dbCommand,"@Addr3", vendor.Addr3);            
          
            Database.PutParameter(dbCommand,"@Addr4", vendor.Addr4);            
          
            Database.PutParameter(dbCommand,"@City", vendor.City);            
          
            Database.PutParameter(dbCommand,"@State", vendor.State);            
          
            Database.PutParameter(dbCommand,"@PostalCode", vendor.PostalCode);            
          
            Database.PutParameter(dbCommand,"@Country", vendor.Country);            
          
            Database.PutParameter(dbCommand,"@Phone", vendor.Phone);            
          
            Database.PutParameter(dbCommand,"@Mobile", vendor.Mobile);            
          
            Database.PutParameter(dbCommand,"@Pager", vendor.Pager);            
          
            Database.PutParameter(dbCommand,"@AltPhone", vendor.AltPhone);            
          
            Database.PutParameter(dbCommand,"@Fax", vendor.Fax);            
          
            Database.PutParameter(dbCommand,"@Email", vendor.Email);            
          
            Database.PutParameter(dbCommand,"@NameOnCheck", vendor.NameOnCheck);            
          
            Database.PutParameter(dbCommand,"@VendorTaxIdent", vendor.VendorTaxIdent);            
          
            Database.PutParameter(dbCommand,"@IsVendorEligibleFor1099", vendor.IsVendorEligibleFor1099);            
          
            Database.PutParameter(dbCommand,"@Balance", vendor.Balance);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " VendorId, "
        + " ModifiedVendorId, "
        + " QuickBooksListId, "
        + " EntityStateId, "
        + " EditSequence, "
        + " Name, "
        + " CompanyName, "
        + " Salutation, "
        + " FirstName, "
        + " MiddleName, "
        + " LastName, "
        + " Suffix, "
        + " Addr1, "
        + " Addr2, "
        + " Addr3, "
        + " Addr4, "
        + " City, "
        + " State, "
        + " PostalCode, "
        + " Country, "
        + " Phone, "
        + " Mobile, "
        + " Pager, "
        + " AltPhone, "
        + " Fax, "
        + " Email, "
        + " NameOnCheck, "
        + " VendorTaxIdent, "
        + " IsVendorEligibleFor1099, "
        + " Balance "
        + " From [Vendor] "
      
        + " Where "
        
        + " VendorId = @VendorId "
        
      ;

      public static Vendor FindByPrimaryKey(
      int vendorId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@VendorId", vendorId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Vendor not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Vendor vendor)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@VendorId",vendor.VendorId);
      

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
      String sql = "select 1 from [Vendor]";

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

      public static Vendor Load(IDataReader dataReader)
      {
      Vendor vendor = new Vendor();

      vendor.VendorId = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
              vendor.ModifiedVendorId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
              vendor.QuickBooksListId = dataReader.GetInt32(2);
          vendor
			.EntityState = new EntityState();

            vendor
			.EntityState.EntityStateId = dataReader.GetInt32(3);
          vendor.EditSequence = dataReader.GetInt32(4);
          vendor.Name = dataReader.GetString(5);
          
            if(!dataReader.IsDBNull(6))
              vendor.CompanyName = dataReader.GetString(6);
          
            if(!dataReader.IsDBNull(7))
              vendor.Salutation = dataReader.GetString(7);
          
            if(!dataReader.IsDBNull(8))
              vendor.FirstName = dataReader.GetString(8);
          
            if(!dataReader.IsDBNull(9))
              vendor.MiddleName = dataReader.GetString(9);
          
            if(!dataReader.IsDBNull(10))
              vendor.LastName = dataReader.GetString(10);
          
            if(!dataReader.IsDBNull(11))
              vendor.Suffix = dataReader.GetString(11);
          
            if(!dataReader.IsDBNull(12))
              vendor.Addr1 = dataReader.GetString(12);
          
            if(!dataReader.IsDBNull(13))
              vendor.Addr2 = dataReader.GetString(13);
          
            if(!dataReader.IsDBNull(14))
              vendor.Addr3 = dataReader.GetString(14);
          
            if(!dataReader.IsDBNull(15))
              vendor.Addr4 = dataReader.GetString(15);
          
            if(!dataReader.IsDBNull(16))
              vendor.City = dataReader.GetString(16);
          
            if(!dataReader.IsDBNull(17))
              vendor.State = dataReader.GetString(17);
          
            if(!dataReader.IsDBNull(18))
              vendor.PostalCode = dataReader.GetString(18);
          
            if(!dataReader.IsDBNull(19))
              vendor.Country = dataReader.GetString(19);
          
            if(!dataReader.IsDBNull(20))
              vendor.Phone = dataReader.GetString(20);
          
            if(!dataReader.IsDBNull(21))
              vendor.Mobile = dataReader.GetString(21);
          
            if(!dataReader.IsDBNull(22))
              vendor.Pager = dataReader.GetString(22);
          
            if(!dataReader.IsDBNull(23))
              vendor.AltPhone = dataReader.GetString(23);
          
            if(!dataReader.IsDBNull(24))
              vendor.Fax = dataReader.GetString(24);
          
            if(!dataReader.IsDBNull(25))
              vendor.Email = dataReader.GetString(25);
          
            if(!dataReader.IsDBNull(26))
              vendor.NameOnCheck = dataReader.GetString(26);
          
            if(!dataReader.IsDBNull(27))
              vendor.VendorTaxIdent = dataReader.GetString(27);
          
            if(!dataReader.IsDBNull(28))
              vendor.IsVendorEligibleFor1099 = dataReader.GetBoolean(28);
          
            if(!dataReader.IsDBNull(29))
              vendor.Balance = dataReader.GetDecimal(29);
          

      return vendor;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Vendor] "

      
        + " Where "
        
          + " VendorId = @VendorId "
        
      ;
      public static void Delete(Vendor vendor)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@VendorId", vendor.VendorId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Vendor] ";

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
      
        + " VendorId, "
        + " ModifiedVendorId, "
        + " QuickBooksListId, "
        + " EntityStateId, "
        + " EditSequence, "
        + " Name, "
        + " CompanyName, "
        + " Salutation, "
        + " FirstName, "
        + " MiddleName, "
        + " LastName, "
        + " Suffix, "
        + " Addr1, "
        + " Addr2, "
        + " Addr3, "
        + " Addr4, "
        + " City, "
        + " State, "
        + " PostalCode, "
        + " Country, "
        + " Phone, "
        + " Mobile, "
        + " Pager, "
        + " AltPhone, "
        + " Fax, "
        + " Email, "
        + " NameOnCheck, "
        + " VendorTaxIdent, "
        + " IsVendorEligibleFor1099, "
        + " Balance "
        + " From [Vendor] ";
      public static List<Vendor> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Vendor> rv = new List<Vendor>();

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
      List<Vendor> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Vendor> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Vendor));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Vendor item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Vendor>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Vendor));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Vendor> itemsList
      = new List<Vendor>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Vendor)
      itemsList.Add(deserializedObject as Vendor);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region VendorId
        protected int m_vendorId;

			[XmlAttribute]
			public int VendorId
			{
			get { return m_vendorId;}
			set { m_vendorId = value; }
			}
		#endregion
		
		#region ModifiedVendorId
        protected int? m_modifiedVendorId;

			[XmlAttribute]
			public int? ModifiedVendorId
			{
			get { return m_modifiedVendorId;}
			set { m_modifiedVendorId = value; }
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
		
		#region CompanyName
        protected String m_companyName;

			[XmlAttribute]
			public String CompanyName
			{
			get { return m_companyName;}
			set { m_companyName = value; }
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
		
		#region Email
        protected String m_email;

			[XmlAttribute]
			public String Email
			{
			get { return m_email;}
			set { m_email = value; }
			}
		#endregion
		
		#region NameOnCheck
        protected String m_nameOnCheck;

			[XmlAttribute]
			public String NameOnCheck
			{
			get { return m_nameOnCheck;}
			set { m_nameOnCheck = value; }
			}
		#endregion
		
		#region VendorTaxIdent
        protected String m_vendorTaxIdent;

			[XmlAttribute]
			public String VendorTaxIdent
			{
			get { return m_vendorTaxIdent;}
			set { m_vendorTaxIdent = value; }
			}
		#endregion
		
		#region IsVendorEligibleFor1099
        protected bool m_isVendorEligibleFor1099;

			[XmlAttribute]
			public bool IsVendorEligibleFor1099
			{
			get { return m_isVendorEligibleFor1099;}
			set { m_isVendorEligibleFor1099 = value; }
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
		
		#region EntityState
			protected EntityState m_entityState;

			[XmlElement]
			public EntityState EntityState
			{
			get { return m_entityState;}
			set { m_entityState = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public Vendor(
		int vendorId

		)
		{
		
			m_vendorId = vendorId;
		
        }

      


        public Vendor(
		  EntityState entityState
			  ,
		  int vendorId,int? modifiedVendorId,int? quickBooksListId,int editSequence,String name,String companyName,String salutation,String firstName,String middleName,String lastName,String suffix,String addr1,String addr2,String addr3,String addr4,String city,String state,String postalCode,String country,String phone,String mobile,String pager,String altPhone,String fax,String email,String nameOnCheck,String vendorTaxIdent,bool isVendorEligibleFor1099,decimal? balance
		  )
		  {

		  
			  m_entityState = entityState;
		  
			  m_vendorId = vendorId;
		  
			  m_modifiedVendorId = modifiedVendorId;
		  
			  m_quickBooksListId = quickBooksListId;
		  
			  m_editSequence = editSequence;
		  
			  m_name = name;
		  
			  m_companyName = companyName;
		  
			  m_salutation = salutation;
		  
			  m_firstName = firstName;
		  
			  m_middleName = middleName;
		  
			  m_lastName = lastName;
		  
			  m_suffix = suffix;
		  
			  m_addr1 = addr1;
		  
			  m_addr2 = addr2;
		  
			  m_addr3 = addr3;
		  
			  m_addr4 = addr4;
		  
			  m_city = city;
		  
			  m_state = state;
		  
			  m_postalCode = postalCode;
		  
			  m_country = country;
		  
			  m_phone = phone;
		  
			  m_mobile = mobile;
		  
			  m_pager = pager;
		  
			  m_altPhone = altPhone;
		  
			  m_fax = fax;
		  
			  m_email = email;
		  
			  m_nameOnCheck = nameOnCheck;
		  
			  m_vendorTaxIdent = vendorTaxIdent;
		  
			  m_isVendorEligibleFor1099 = isVendorEligibleFor1099;
		  
			  m_balance = balance;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    