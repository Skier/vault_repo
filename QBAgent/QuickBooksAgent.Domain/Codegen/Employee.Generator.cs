
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class Employee
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Employee] ( " +
      
        " EmployeeId, " +
        " ModifiedEmployeeId, " +
        " QuickBooksListId, " +
        " EntityStateId, " +
        " EditSequence, " +
        " Name, " +
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
        " PrintAs, " +
        " Phone, " +
        " Mobile, " +
        " AltPhone, " +
        " Email, " +
        " HiredDate, " +
        " ReleasedDate " +
        ") Values (" +
      
        " @EmployeeId, " +
        " @ModifiedEmployeeId, " +
        " @QuickBooksListId, " +
        " @EntityStateId, " +
        " @EditSequence, " +
        " @Name, " +
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
        " @PrintAs, " +
        " @Phone, " +
        " @Mobile, " +
        " @AltPhone, " +
        " @Email, " +
        " @HiredDate, " +
        " @ReleasedDate " +
      ")";

      public static void Insert(Employee employee)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@EmployeeId", employee.EmployeeId);            
          
              Database.PutParameter(dbCommand,"@ModifiedEmployeeId", employee.ModifiedEmployeeId);            
          
              Database.PutParameter(dbCommand,"@QuickBooksListId", employee.QuickBooksListId);            
          
              Database.PutParameter(dbCommand,"@EntityStateId", employee
			.EntityState.EntityStateId);            
          
              Database.PutParameter(dbCommand,"@EditSequence", employee.EditSequence);            
          
              Database.PutParameter(dbCommand,"@Name", employee.Name);            
          
              Database.PutParameter(dbCommand,"@Salutation", employee.Salutation);            
          
              Database.PutParameter(dbCommand,"@FirstName", employee.FirstName);            
          
              Database.PutParameter(dbCommand,"@MiddleName", employee.MiddleName);            
          
              Database.PutParameter(dbCommand,"@LastName", employee.LastName);            
          
              Database.PutParameter(dbCommand,"@Suffix", employee.Suffix);            
          
              Database.PutParameter(dbCommand,"@Addr1", employee.Addr1);            
          
              Database.PutParameter(dbCommand,"@Addr2", employee.Addr2);            
          
              Database.PutParameter(dbCommand,"@Addr3", employee.Addr3);            
          
              Database.PutParameter(dbCommand,"@Addr4", employee.Addr4);            
          
              Database.PutParameter(dbCommand,"@City", employee.City);            
          
              Database.PutParameter(dbCommand,"@State", employee.State);            
          
              Database.PutParameter(dbCommand,"@PostalCode", employee.PostalCode);            
          
              Database.PutParameter(dbCommand,"@Country", employee.Country);            
          
              Database.PutParameter(dbCommand,"@PrintAs", employee.PrintAs);            
          
              Database.PutParameter(dbCommand,"@Phone", employee.Phone);            
          
              Database.PutParameter(dbCommand,"@Mobile", employee.Mobile);            
          
              Database.PutParameter(dbCommand,"@AltPhone", employee.AltPhone);            
          
              Database.PutParameter(dbCommand,"@Email", employee.Email);            
          
              Database.PutParameter(dbCommand,"@HiredDate", employee.HiredDate);            
          
              Database.PutParameter(dbCommand,"@ReleasedDate", employee.ReleasedDate);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Employee>  employeeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Employee employee in  employeeList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@EmployeeId", employee.EmployeeId);
          
            Database.PutParameter(dbCommand,"@ModifiedEmployeeId", employee.ModifiedEmployeeId);
          
            Database.PutParameter(dbCommand,"@QuickBooksListId", employee.QuickBooksListId);
          
            Database.PutParameter(dbCommand,"@EntityStateId", employee
			.EntityState.EntityStateId);
          
            Database.PutParameter(dbCommand,"@EditSequence", employee.EditSequence);
          
            Database.PutParameter(dbCommand,"@Name", employee.Name);
          
            Database.PutParameter(dbCommand,"@Salutation", employee.Salutation);
          
            Database.PutParameter(dbCommand,"@FirstName", employee.FirstName);
          
            Database.PutParameter(dbCommand,"@MiddleName", employee.MiddleName);
          
            Database.PutParameter(dbCommand,"@LastName", employee.LastName);
          
            Database.PutParameter(dbCommand,"@Suffix", employee.Suffix);
          
            Database.PutParameter(dbCommand,"@Addr1", employee.Addr1);
          
            Database.PutParameter(dbCommand,"@Addr2", employee.Addr2);
          
            Database.PutParameter(dbCommand,"@Addr3", employee.Addr3);
          
            Database.PutParameter(dbCommand,"@Addr4", employee.Addr4);
          
            Database.PutParameter(dbCommand,"@City", employee.City);
          
            Database.PutParameter(dbCommand,"@State", employee.State);
          
            Database.PutParameter(dbCommand,"@PostalCode", employee.PostalCode);
          
            Database.PutParameter(dbCommand,"@Country", employee.Country);
          
            Database.PutParameter(dbCommand,"@PrintAs", employee.PrintAs);
          
            Database.PutParameter(dbCommand,"@Phone", employee.Phone);
          
            Database.PutParameter(dbCommand,"@Mobile", employee.Mobile);
          
            Database.PutParameter(dbCommand,"@AltPhone", employee.AltPhone);
          
            Database.PutParameter(dbCommand,"@Email", employee.Email);
          
            Database.PutParameter(dbCommand,"@HiredDate", employee.HiredDate);
          
            Database.PutParameter(dbCommand,"@ReleasedDate", employee.ReleasedDate);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@EmployeeId",employee.EmployeeId);
          
            Database.UpdateParameter(dbCommand,"@ModifiedEmployeeId",employee.ModifiedEmployeeId);
          
            Database.UpdateParameter(dbCommand,"@QuickBooksListId",employee.QuickBooksListId);
          
            Database.UpdateParameter(dbCommand,"@EntityStateId",employee
			.EntityState.EntityStateId);
          
            Database.UpdateParameter(dbCommand,"@EditSequence",employee.EditSequence);
          
            Database.UpdateParameter(dbCommand,"@Name",employee.Name);
          
            Database.UpdateParameter(dbCommand,"@Salutation",employee.Salutation);
          
            Database.UpdateParameter(dbCommand,"@FirstName",employee.FirstName);
          
            Database.UpdateParameter(dbCommand,"@MiddleName",employee.MiddleName);
          
            Database.UpdateParameter(dbCommand,"@LastName",employee.LastName);
          
            Database.UpdateParameter(dbCommand,"@Suffix",employee.Suffix);
          
            Database.UpdateParameter(dbCommand,"@Addr1",employee.Addr1);
          
            Database.UpdateParameter(dbCommand,"@Addr2",employee.Addr2);
          
            Database.UpdateParameter(dbCommand,"@Addr3",employee.Addr3);
          
            Database.UpdateParameter(dbCommand,"@Addr4",employee.Addr4);
          
            Database.UpdateParameter(dbCommand,"@City",employee.City);
          
            Database.UpdateParameter(dbCommand,"@State",employee.State);
          
            Database.UpdateParameter(dbCommand,"@PostalCode",employee.PostalCode);
          
            Database.UpdateParameter(dbCommand,"@Country",employee.Country);
          
            Database.UpdateParameter(dbCommand,"@PrintAs",employee.PrintAs);
          
            Database.UpdateParameter(dbCommand,"@Phone",employee.Phone);
          
            Database.UpdateParameter(dbCommand,"@Mobile",employee.Mobile);
          
            Database.UpdateParameter(dbCommand,"@AltPhone",employee.AltPhone);
          
            Database.UpdateParameter(dbCommand,"@Email",employee.Email);
          
            Database.UpdateParameter(dbCommand,"@HiredDate",employee.HiredDate);
          
            Database.UpdateParameter(dbCommand,"@ReleasedDate",employee.ReleasedDate);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Employee] Set "
      
        + " ModifiedEmployeeId = @ModifiedEmployeeId, "
        + " QuickBooksListId = @QuickBooksListId, "
        + " EntityStateId = @EntityStateId, "
        + " EditSequence = @EditSequence, "
        + " Name = @Name, "
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
        + " PrintAs = @PrintAs, "
        + " Phone = @Phone, "
        + " Mobile = @Mobile, "
        + " AltPhone = @AltPhone, "
        + " Email = @Email, "
        + " HiredDate = @HiredDate, "
        + " ReleasedDate = @ReleasedDate "
        + " Where "
        
          + " EmployeeId = @EmployeeId "
        
      ;

      public static void Update(Employee employee)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@EmployeeId", employee.EmployeeId);            
          
            Database.PutParameter(dbCommand,"@ModifiedEmployeeId", employee.ModifiedEmployeeId);            
          
            Database.PutParameter(dbCommand,"@QuickBooksListId", employee.QuickBooksListId);            
          
            Database.PutParameter(dbCommand,"@EntityStateId", employee
			.EntityState.EntityStateId);            
          
            Database.PutParameter(dbCommand,"@EditSequence", employee.EditSequence);            
          
            Database.PutParameter(dbCommand,"@Name", employee.Name);            
          
            Database.PutParameter(dbCommand,"@Salutation", employee.Salutation);            
          
            Database.PutParameter(dbCommand,"@FirstName", employee.FirstName);            
          
            Database.PutParameter(dbCommand,"@MiddleName", employee.MiddleName);            
          
            Database.PutParameter(dbCommand,"@LastName", employee.LastName);            
          
            Database.PutParameter(dbCommand,"@Suffix", employee.Suffix);            
          
            Database.PutParameter(dbCommand,"@Addr1", employee.Addr1);            
          
            Database.PutParameter(dbCommand,"@Addr2", employee.Addr2);            
          
            Database.PutParameter(dbCommand,"@Addr3", employee.Addr3);            
          
            Database.PutParameter(dbCommand,"@Addr4", employee.Addr4);            
          
            Database.PutParameter(dbCommand,"@City", employee.City);            
          
            Database.PutParameter(dbCommand,"@State", employee.State);            
          
            Database.PutParameter(dbCommand,"@PostalCode", employee.PostalCode);            
          
            Database.PutParameter(dbCommand,"@Country", employee.Country);            
          
            Database.PutParameter(dbCommand,"@PrintAs", employee.PrintAs);            
          
            Database.PutParameter(dbCommand,"@Phone", employee.Phone);            
          
            Database.PutParameter(dbCommand,"@Mobile", employee.Mobile);            
          
            Database.PutParameter(dbCommand,"@AltPhone", employee.AltPhone);            
          
            Database.PutParameter(dbCommand,"@Email", employee.Email);            
          
            Database.PutParameter(dbCommand,"@HiredDate", employee.HiredDate);            
          
            Database.PutParameter(dbCommand,"@ReleasedDate", employee.ReleasedDate);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " EmployeeId, "
        + " ModifiedEmployeeId, "
        + " QuickBooksListId, "
        + " EntityStateId, "
        + " EditSequence, "
        + " Name, "
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
        + " PrintAs, "
        + " Phone, "
        + " Mobile, "
        + " AltPhone, "
        + " Email, "
        + " HiredDate, "
        + " ReleasedDate "
        + " From [Employee] "
      
        + " Where "
        
        + " EmployeeId = @EmployeeId "
        
      ;

      public static Employee FindByPrimaryKey(
      int employeeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@EmployeeId", employeeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Employee not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Employee employee)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@EmployeeId",employee.EmployeeId);
      

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
      String sql = "select 1 from [Employee]";

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

      public static Employee Load(IDataReader dataReader)
      {
      Employee employee = new Employee();

      employee.EmployeeId = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
              employee.ModifiedEmployeeId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
              employee.QuickBooksListId = dataReader.GetInt32(2);
          employee
			.EntityState = new EntityState();

            employee
			.EntityState.EntityStateId = dataReader.GetInt32(3);
          employee.EditSequence = dataReader.GetInt32(4);
          
            if(!dataReader.IsDBNull(5))
              employee.Name = dataReader.GetString(5);
          
            if(!dataReader.IsDBNull(6))
              employee.Salutation = dataReader.GetString(6);
          
            if(!dataReader.IsDBNull(7))
              employee.FirstName = dataReader.GetString(7);
          
            if(!dataReader.IsDBNull(8))
              employee.MiddleName = dataReader.GetString(8);
          
            if(!dataReader.IsDBNull(9))
              employee.LastName = dataReader.GetString(9);
          
            if(!dataReader.IsDBNull(10))
              employee.Suffix = dataReader.GetString(10);
          
            if(!dataReader.IsDBNull(11))
              employee.Addr1 = dataReader.GetString(11);
          
            if(!dataReader.IsDBNull(12))
              employee.Addr2 = dataReader.GetString(12);
          
            if(!dataReader.IsDBNull(13))
              employee.Addr3 = dataReader.GetString(13);
          
            if(!dataReader.IsDBNull(14))
              employee.Addr4 = dataReader.GetString(14);
          
            if(!dataReader.IsDBNull(15))
              employee.City = dataReader.GetString(15);
          
            if(!dataReader.IsDBNull(16))
              employee.State = dataReader.GetString(16);
          
            if(!dataReader.IsDBNull(17))
              employee.PostalCode = dataReader.GetString(17);
          
            if(!dataReader.IsDBNull(18))
              employee.Country = dataReader.GetString(18);
          
            if(!dataReader.IsDBNull(19))
              employee.PrintAs = dataReader.GetString(19);
          
            if(!dataReader.IsDBNull(20))
              employee.Phone = dataReader.GetString(20);
          
            if(!dataReader.IsDBNull(21))
              employee.Mobile = dataReader.GetString(21);
          
            if(!dataReader.IsDBNull(22))
              employee.AltPhone = dataReader.GetString(22);
          
            if(!dataReader.IsDBNull(23))
              employee.Email = dataReader.GetString(23);
          
            if(!dataReader.IsDBNull(24))
              employee.HiredDate = dataReader.GetDateTime(24);
          
            if(!dataReader.IsDBNull(25))
              employee.ReleasedDate = dataReader.GetDateTime(25);
          

      return employee;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Employee] "

      
        + " Where "
        
          + " EmployeeId = @EmployeeId "
        
      ;
      public static void Delete(Employee employee)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@EmployeeId", employee.EmployeeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Employee] ";

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
      
        + " EmployeeId, "
        + " ModifiedEmployeeId, "
        + " QuickBooksListId, "
        + " EntityStateId, "
        + " EditSequence, "
        + " Name, "
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
        + " PrintAs, "
        + " Phone, "
        + " Mobile, "
        + " AltPhone, "
        + " Email, "
        + " HiredDate, "
        + " ReleasedDate "
        + " From [Employee] ";
      public static List<Employee> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Employee> rv = new List<Employee>();

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
      List<Employee> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Employee> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Employee));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Employee item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Employee>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Employee));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Employee> itemsList
      = new List<Employee>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Employee)
      itemsList.Add(deserializedObject as Employee);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region EmployeeId
        protected int m_employeeId;

			[XmlAttribute]
			public int EmployeeId
			{
			get { return m_employeeId;}
			set { m_employeeId = value; }
			}
		#endregion
		
		#region ModifiedEmployeeId
        protected int? m_modifiedEmployeeId;

			[XmlAttribute]
			public int? ModifiedEmployeeId
			{
			get { return m_modifiedEmployeeId;}
			set { m_modifiedEmployeeId = value; }
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
		
		#region PrintAs
        protected String m_printAs;

			[XmlAttribute]
			public String PrintAs
			{
			get { return m_printAs;}
			set { m_printAs = value; }
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
		
		#region AltPhone
        protected String m_altPhone;

			[XmlAttribute]
			public String AltPhone
			{
			get { return m_altPhone;}
			set { m_altPhone = value; }
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
		
		#region HiredDate
        protected DateTime? m_hiredDate;

			[XmlAttribute]
			public DateTime? HiredDate
			{
			get { return m_hiredDate;}
			set { m_hiredDate = value; }
			}
		#endregion
		
		#region ReleasedDate
        protected DateTime? m_releasedDate;

			[XmlAttribute]
			public DateTime? ReleasedDate
			{
			get { return m_releasedDate;}
			set { m_releasedDate = value; }
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
      public Employee(
		int employeeId

		)
		{
		
			m_employeeId = employeeId;
		
        }

      


        public Employee(
		  EntityState entityState
			  ,
		  int employeeId,int? modifiedEmployeeId,int? quickBooksListId,int editSequence,String name,String salutation,String firstName,String middleName,String lastName,String suffix,String addr1,String addr2,String addr3,String addr4,String city,String state,String postalCode,String country,String printAs,String phone,String mobile,String altPhone,String email,DateTime? hiredDate,DateTime? releasedDate
		  )
		  {

		  
			  m_entityState = entityState;
		  
			  m_employeeId = employeeId;
		  
			  m_modifiedEmployeeId = modifiedEmployeeId;
		  
			  m_quickBooksListId = quickBooksListId;
		  
			  m_editSequence = editSequence;
		  
			  m_name = name;
		  
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
		  
			  m_printAs = printAs;
		  
			  m_phone = phone;
		  
			  m_mobile = mobile;
		  
			  m_altPhone = altPhone;
		  
			  m_email = email;
		  
			  m_hiredDate = hiredDate;
		  
			  m_releasedDate = releasedDate;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    