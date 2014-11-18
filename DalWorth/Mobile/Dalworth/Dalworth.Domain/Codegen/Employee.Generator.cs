
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Data;
    using Dalworth.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Domain
      {


      public partial class Employee : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Employee] ( " +
      
        " ID, " +
      
        " EmployeeTypeId, " +
      
        " AddressId, " +
      
        " FirstName, " +
      
        " LastName, " +
      
        " HireDate, " +
      
        " Phone1, " +
      
        " Phone2, " +
      
        " Password " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @EmployeeTypeId, " +
      
        " @AddressId, " +
      
        " @FirstName, " +
      
        " @LastName, " +
      
        " @HireDate, " +
      
        " @Phone1, " +
      
        " @Phone2, " +
      
        " @Password " +
      
      ")";

      public static void Insert(Employee employee, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", employee.ID);
      
        Database.PutParameter(dbCommand,"@EmployeeTypeId", employee.EmployeeTypeId);
      
        Database.PutParameter(dbCommand,"@AddressId", employee.AddressId);
      
        Database.PutParameter(dbCommand,"@FirstName", employee.FirstName);
      
        Database.PutParameter(dbCommand,"@LastName", employee.LastName);
      
        Database.PutParameter(dbCommand,"@HireDate", employee.HireDate);
      
        Database.PutParameter(dbCommand,"@Phone1", employee.Phone1);
      
        Database.PutParameter(dbCommand,"@Phone2", employee.Phone2);
      
        Database.PutParameter(dbCommand,"@Password", employee.Password);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(Employee employee)
      {
        Insert(employee, null);
      }

      public static void Insert(List<Employee>  employeeList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(Employee employee in  employeeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", employee.ID);
      
        Database.PutParameter(dbCommand,"@EmployeeTypeId", employee.EmployeeTypeId);
      
        Database.PutParameter(dbCommand,"@AddressId", employee.AddressId);
      
        Database.PutParameter(dbCommand,"@FirstName", employee.FirstName);
      
        Database.PutParameter(dbCommand,"@LastName", employee.LastName);
      
        Database.PutParameter(dbCommand,"@HireDate", employee.HireDate);
      
        Database.PutParameter(dbCommand,"@Phone1", employee.Phone1);
      
        Database.PutParameter(dbCommand,"@Phone2", employee.Phone2);
      
        Database.PutParameter(dbCommand,"@Password", employee.Password);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",employee.ID);
      
        Database.UpdateParameter(dbCommand,"@EmployeeTypeId",employee.EmployeeTypeId);
      
        Database.UpdateParameter(dbCommand,"@AddressId",employee.AddressId);
      
        Database.UpdateParameter(dbCommand,"@FirstName",employee.FirstName);
      
        Database.UpdateParameter(dbCommand,"@LastName",employee.LastName);
      
        Database.UpdateParameter(dbCommand,"@HireDate",employee.HireDate);
      
        Database.UpdateParameter(dbCommand,"@Phone1",employee.Phone1);
      
        Database.UpdateParameter(dbCommand,"@Phone2",employee.Phone2);
      
        Database.UpdateParameter(dbCommand,"@Password",employee.Password);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<Employee>  employeeList)
      {
      Insert(employeeList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Employee] Set "
      
        + " EmployeeTypeId = @EmployeeTypeId, "
      
        + " AddressId = @AddressId, "
      
        + " FirstName = @FirstName, "
      
        + " LastName = @LastName, "
      
        + " HireDate = @HireDate, "
      
        + " Phone1 = @Phone1, "
      
        + " Phone2 = @Phone2, "
      
        + " Password = @Password "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(Employee employee, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", employee.ID);
      
        Database.PutParameter(dbCommand,"@EmployeeTypeId", employee.EmployeeTypeId);
      
        Database.PutParameter(dbCommand,"@AddressId", employee.AddressId);
      
        Database.PutParameter(dbCommand,"@FirstName", employee.FirstName);
      
        Database.PutParameter(dbCommand,"@LastName", employee.LastName);
      
        Database.PutParameter(dbCommand,"@HireDate", employee.HireDate);
      
        Database.PutParameter(dbCommand,"@Phone1", employee.Phone1);
      
        Database.PutParameter(dbCommand,"@Phone2", employee.Phone2);
      
        Database.PutParameter(dbCommand,"@Password", employee.Password);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Employee employee)
      {
      Update(employee, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " EmployeeTypeId, "
      
        + " AddressId, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " HireDate, "
      
        + " Phone1, "
      
        + " Phone2, "
      
        + " Password "
      

      + " From [Employee] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static Employee FindByPrimaryKey(
      int iD, IDbTransaction transaction
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", iD);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Employee not found, search by primary key");

      }

      public static Employee FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(Employee employee, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",employee.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Employee employee)
      {
      return Exists(employee, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from Employee";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, transaction))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      public static bool IsContainsData()
      {
      return IsContainsData(null);
      }

      #endregion

      #region Load

      public static Employee Load(IDataReader dataReader)
      {
      Employee employee = new Employee();

      employee.ID = dataReader.GetInt32(0);
          employee.EmployeeTypeId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            employee.AddressId = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            employee.FirstName = dataReader.GetString(3);
          
            if(!dataReader.IsDBNull(4))
            employee.LastName = dataReader.GetString(4);
          
            if(!dataReader.IsDBNull(5))
            employee.HireDate = dataReader.GetDateTime(5);
          
            if(!dataReader.IsDBNull(6))
            employee.Phone1 = dataReader.GetString(6);
          
            if(!dataReader.IsDBNull(7))
            employee.Phone2 = dataReader.GetString(7);
          
            if(!dataReader.IsDBNull(8))
            employee.Password = dataReader.GetString(8);
          

      return employee;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Employee] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(Employee employee, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", employee.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Employee employee)
      {
      Delete(employee, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Employee] ";

      public static void Clear(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, transaction))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Clear()
      {
      Clear(null);
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " ID, "
      
        + " EmployeeTypeId, "
      
        + " AddressId, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " HireDate, "
      
        + " Phone1, "
      
        + " Phone2, "
      
        + " Password "
      

      + " From [Employee] ";
      public static List<Employee> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
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

      public static List<Employee> Find()
      {
        return Find(null);
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
      
        protected int m_iD;
      
        protected int m_employeeTypeId;
      
        protected int? m_addressId;
      
        protected String m_firstName;
      
        protected String m_lastName;
      
        protected DateTime? m_hireDate;
      
        protected String m_phone1;
      
        protected String m_phone2;
      
        protected String m_password;
      
      #endregion

      #region Constructors
      public Employee(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public Employee(
        int 
          iD,int 
          employeeTypeId,int? 
          addressId,String 
          firstName,String 
          lastName,DateTime? 
          hireDate,String 
          phone1,String 
          phone2,String 
          password
        )
        {
        
          m_iD = iD;
        
          m_employeeTypeId = employeeTypeId;
        
          m_addressId = addressId;
        
          m_firstName = firstName;
        
          m_lastName = lastName;
        
          m_hireDate = hireDate;
        
          m_phone1 = phone1;
        
          m_phone2 = phone2;
        
          m_password = password;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int EmployeeTypeId
        {
        get { return m_employeeTypeId;}
        set { m_employeeTypeId = value; }
        }
      
        [XmlElement]
        public int? AddressId
        {
        get { return m_addressId;}
        set { m_addressId = value; }
        }
      
        [XmlElement]
        public String FirstName
        {
        get { return m_firstName;}
        set { m_firstName = value; }
        }
      
        [XmlElement]
        public String LastName
        {
        get { return m_lastName;}
        set { m_lastName = value; }
        }
      
        [XmlElement]
        public DateTime? HireDate
        {
        get { return m_hireDate;}
        set { m_hireDate = value; }
        }
      
        [XmlElement]
        public String Phone1
        {
        get { return m_phone1;}
        set { m_phone1 = value; }
        }
      
        [XmlElement]
        public String Phone2
        {
        get { return m_phone2;}
        set { m_phone2 = value; }
        }
      
        [XmlElement]
        public String Password
        {
        get { return m_password;}
        set { m_password = value; }
        }
      
      }
      #endregion
      }

    