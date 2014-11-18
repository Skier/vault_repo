
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Domain
      {


      public partial class Employee : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Employee ( " +
      
        " EmployeeTypeId, " +
      
        " ServmanUserId, " +
      
        " ServmanTechId, " +
      
        " AddressId, " +
      
        " Login, " +
      
        " FirstName, " +
      
        " LastName, " +
      
        " HireDate, " +
      
        " Phone1, " +
      
        " Phone2, " +
      
        " Password, " +
      
        " IsActive, " +
      
        " IsRestoration, " +
      
        " IsUnknown, " +
      
        " DefaultVanId, " +
      
        " SecurityRoleId " +
      
      ") Values (" +
      
        " ?EmployeeTypeId, " +
      
        " ?ServmanUserId, " +
      
        " ?ServmanTechId, " +
      
        " ?AddressId, " +
      
        " ?Login, " +
      
        " ?FirstName, " +
      
        " ?LastName, " +
      
        " ?HireDate, " +
      
        " ?Phone1, " +
      
        " ?Phone2, " +
      
        " ?Password, " +
      
        " ?IsActive, " +
      
        " ?IsRestoration, " +
      
        " ?IsUnknown, " +
      
        " ?DefaultVanId, " +
      
        " ?SecurityRoleId " +
      
      ")";

      public static void Insert(Employee employee, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?EmployeeTypeId", employee.EmployeeTypeId);
      
        Database.PutParameter(dbCommand,"?ServmanUserId", employee.ServmanUserId);
      
        Database.PutParameter(dbCommand,"?ServmanTechId", employee.ServmanTechId);
      
        Database.PutParameter(dbCommand,"?AddressId", employee.AddressId);
      
        Database.PutParameter(dbCommand,"?Login", employee.Login);
      
        Database.PutParameter(dbCommand,"?FirstName", employee.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", employee.LastName);
      
        Database.PutParameter(dbCommand,"?HireDate", employee.HireDate);
      
        Database.PutParameter(dbCommand,"?Phone1", employee.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", employee.Phone2);
      
        Database.PutParameter(dbCommand,"?Password", employee.Password);
      
        Database.PutParameter(dbCommand,"?IsActive", employee.IsActive);
      
        Database.PutParameter(dbCommand,"?IsRestoration", employee.IsRestoration);
      
        Database.PutParameter(dbCommand,"?IsUnknown", employee.IsUnknown);
      
        Database.PutParameter(dbCommand,"?DefaultVanId", employee.DefaultVanId);
      
        Database.PutParameter(dbCommand,"?SecurityRoleId", employee.SecurityRoleId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        employee.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Employee employee)
      {
        Insert(employee, null);
      }


      public static void Insert(List<Employee>  employeeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Employee employee in  employeeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?EmployeeTypeId", employee.EmployeeTypeId);
      
        Database.PutParameter(dbCommand,"?ServmanUserId", employee.ServmanUserId);
      
        Database.PutParameter(dbCommand,"?ServmanTechId", employee.ServmanTechId);
      
        Database.PutParameter(dbCommand,"?AddressId", employee.AddressId);
      
        Database.PutParameter(dbCommand,"?Login", employee.Login);
      
        Database.PutParameter(dbCommand,"?FirstName", employee.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", employee.LastName);
      
        Database.PutParameter(dbCommand,"?HireDate", employee.HireDate);
      
        Database.PutParameter(dbCommand,"?Phone1", employee.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", employee.Phone2);
      
        Database.PutParameter(dbCommand,"?Password", employee.Password);
      
        Database.PutParameter(dbCommand,"?IsActive", employee.IsActive);
      
        Database.PutParameter(dbCommand,"?IsRestoration", employee.IsRestoration);
      
        Database.PutParameter(dbCommand,"?IsUnknown", employee.IsUnknown);
      
        Database.PutParameter(dbCommand,"?DefaultVanId", employee.DefaultVanId);
      
        Database.PutParameter(dbCommand,"?SecurityRoleId", employee.SecurityRoleId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?EmployeeTypeId",employee.EmployeeTypeId);
      
        Database.UpdateParameter(dbCommand,"?ServmanUserId",employee.ServmanUserId);
      
        Database.UpdateParameter(dbCommand,"?ServmanTechId",employee.ServmanTechId);
      
        Database.UpdateParameter(dbCommand,"?AddressId",employee.AddressId);
      
        Database.UpdateParameter(dbCommand,"?Login",employee.Login);
      
        Database.UpdateParameter(dbCommand,"?FirstName",employee.FirstName);
      
        Database.UpdateParameter(dbCommand,"?LastName",employee.LastName);
      
        Database.UpdateParameter(dbCommand,"?HireDate",employee.HireDate);
      
        Database.UpdateParameter(dbCommand,"?Phone1",employee.Phone1);
      
        Database.UpdateParameter(dbCommand,"?Phone2",employee.Phone2);
      
        Database.UpdateParameter(dbCommand,"?Password",employee.Password);
      
        Database.UpdateParameter(dbCommand,"?IsActive",employee.IsActive);
      
        Database.UpdateParameter(dbCommand,"?IsRestoration",employee.IsRestoration);
      
        Database.UpdateParameter(dbCommand,"?IsUnknown",employee.IsUnknown);
      
        Database.UpdateParameter(dbCommand,"?DefaultVanId",employee.DefaultVanId);
      
        Database.UpdateParameter(dbCommand,"?SecurityRoleId",employee.SecurityRoleId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        employee.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Employee>  employeeList)
      {
        Insert(employeeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Employee Set "
      
        + " EmployeeTypeId = ?EmployeeTypeId, "
      
        + " ServmanUserId = ?ServmanUserId, "
      
        + " ServmanTechId = ?ServmanTechId, "
      
        + " AddressId = ?AddressId, "
      
        + " Login = ?Login, "
      
        + " FirstName = ?FirstName, "
      
        + " LastName = ?LastName, "
      
        + " HireDate = ?HireDate, "
      
        + " Phone1 = ?Phone1, "
      
        + " Phone2 = ?Phone2, "
      
        + " Password = ?Password, "
      
        + " IsActive = ?IsActive, "
      
        + " IsRestoration = ?IsRestoration, "
      
        + " IsUnknown = ?IsUnknown, "
      
        + " DefaultVanId = ?DefaultVanId, "
      
        + " SecurityRoleId = ?SecurityRoleId "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Employee employee, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", employee.ID);
      
        Database.PutParameter(dbCommand,"?EmployeeTypeId", employee.EmployeeTypeId);
      
        Database.PutParameter(dbCommand,"?ServmanUserId", employee.ServmanUserId);
      
        Database.PutParameter(dbCommand,"?ServmanTechId", employee.ServmanTechId);
      
        Database.PutParameter(dbCommand,"?AddressId", employee.AddressId);
      
        Database.PutParameter(dbCommand,"?Login", employee.Login);
      
        Database.PutParameter(dbCommand,"?FirstName", employee.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", employee.LastName);
      
        Database.PutParameter(dbCommand,"?HireDate", employee.HireDate);
      
        Database.PutParameter(dbCommand,"?Phone1", employee.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", employee.Phone2);
      
        Database.PutParameter(dbCommand,"?Password", employee.Password);
      
        Database.PutParameter(dbCommand,"?IsActive", employee.IsActive);
      
        Database.PutParameter(dbCommand,"?IsRestoration", employee.IsRestoration);
      
        Database.PutParameter(dbCommand,"?IsUnknown", employee.IsUnknown);
      
        Database.PutParameter(dbCommand,"?DefaultVanId", employee.DefaultVanId);
      
        Database.PutParameter(dbCommand,"?SecurityRoleId", employee.SecurityRoleId);
      

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
      
        + " ServmanUserId, "
      
        + " ServmanTechId, "
      
        + " AddressId, "
      
        + " Login, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " HireDate, "
      
        + " Phone1, "
      
        + " Phone2, "
      
        + " Password, "
      
        + " IsActive, "
      
        + " IsRestoration, "
      
        + " IsUnknown, "
      
        + " DefaultVanId, "
      
        + " SecurityRoleId "
      

      + " From Employee "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Employee FindByPrimaryKey(
      int iD, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", iD);
      

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
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Employee employee, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",employee.ID);
      

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

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Employee limit 1";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
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

      public static Employee Load(IDataReader dataReader, int offset)
      {
      Employee employee = new Employee();

      employee.ID = dataReader.GetInt32(0 + offset);
          employee.EmployeeTypeId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            employee.ServmanUserId = dataReader.GetString(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            employee.ServmanTechId = dataReader.GetString(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            employee.AddressId = dataReader.GetInt32(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            employee.Login = dataReader.GetString(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            employee.FirstName = dataReader.GetString(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            employee.LastName = dataReader.GetString(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            employee.HireDate = dataReader.GetDateTime(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            employee.Phone1 = dataReader.GetString(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            employee.Phone2 = dataReader.GetString(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            employee.Password = dataReader.GetString(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            employee.IsActive = dataReader.GetBoolean(12 + offset);
          employee.IsRestoration = dataReader.GetBoolean(13 + offset);
          employee.IsUnknown = dataReader.GetBoolean(14 + offset);
          
            if(!dataReader.IsDBNull(15 + offset))
            employee.DefaultVanId = dataReader.GetInt32(15 + offset);
          
            if(!dataReader.IsDBNull(16 + offset))
            employee.SecurityRoleId = dataReader.GetInt32(16 + offset);
          

      return employee;
      }

      public static Employee Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Employee "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Employee employee, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", employee.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Employee employee)
      {
        Delete(employee, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Employee ";

      public static void Clear(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, connection))
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
      
        + " ServmanUserId, "
      
        + " ServmanTechId, "
      
        + " AddressId, "
      
        + " Login, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " HireDate, "
      
        + " Phone1, "
      
        + " Phone2, "
      
        + " Password, "
      
        + " IsActive, "
      
        + " IsRestoration, "
      
        + " IsUnknown, "
      
        + " DefaultVanId, "
      
        + " SecurityRoleId "
      

      + " From Employee ";
      public static List<Employee> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
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

      #region ValueEquals

      public bool ValueEquals (Employee obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && EmployeeTypeId == obj.EmployeeTypeId && ServmanUserId == obj.ServmanUserId && ServmanTechId == obj.ServmanTechId && AddressId == obj.AddressId && Login == obj.Login && FirstName == obj.FirstName && LastName == obj.LastName && HireDate == obj.HireDate && Phone1 == obj.Phone1 && Phone2 == obj.Phone2 && Password == obj.Password && IsActive == obj.IsActive && IsRestoration == obj.IsRestoration && IsUnknown == obj.IsUnknown && DefaultVanId == obj.DefaultVanId && SecurityRoleId == obj.SecurityRoleId;
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
      
        protected String m_servmanUserId;
      
        protected String m_servmanTechId;
      
        protected int? m_addressId;
      
        protected String m_login;
      
        protected String m_firstName;
      
        protected String m_lastName;
      
        protected DateTime? m_hireDate;
      
        protected String m_phone1;
      
        protected String m_phone2;
      
        protected String m_password;
      
        protected bool m_isActive;
      
        protected bool m_isRestoration;
      
        protected bool m_isUnknown;
      
        protected int? m_defaultVanId;
      
        protected int? m_securityRoleId;
      
      #endregion

      #region Constructors
      public Employee(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Employee(
        int 
          iD,int 
          employeeTypeId,String 
          servmanUserId,String 
          servmanTechId,int? 
          addressId,String 
          login,String 
          firstName,String 
          lastName,DateTime? 
          hireDate,String 
          phone1,String 
          phone2,String 
          password,bool 
          isActive,bool 
          isRestoration,bool 
          isUnknown,int? 
          defaultVanId,int? 
          securityRoleId
        ) : this()
        {
        
          m_iD = iD;
        
          m_employeeTypeId = employeeTypeId;
        
          m_servmanUserId = servmanUserId;
        
          m_servmanTechId = servmanTechId;
        
          m_addressId = addressId;
        
          m_login = login;
        
          m_firstName = firstName;
        
          m_lastName = lastName;
        
          m_hireDate = hireDate;
        
          m_phone1 = phone1;
        
          m_phone2 = phone2;
        
          m_password = password;
        
          m_isActive = isActive;
        
          m_isRestoration = isRestoration;
        
          m_isUnknown = isUnknown;
        
          m_defaultVanId = defaultVanId;
        
          m_securityRoleId = securityRoleId;
        
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
        public String ServmanUserId
        {
        get { return m_servmanUserId;}
        set { m_servmanUserId = value; }
        }
      
        [XmlElement]
        public String ServmanTechId
        {
        get { return m_servmanTechId;}
        set { m_servmanTechId = value; }
        }
      
        [XmlElement]
        public int? AddressId
        {
        get { return m_addressId;}
        set { m_addressId = value; }
        }
      
        [XmlElement]
        public String Login
        {
        get { return m_login;}
        set { m_login = value; }
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
      
        [XmlElement]
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
        }
      
        [XmlElement]
        public bool IsRestoration
        {
        get { return m_isRestoration;}
        set { m_isRestoration = value; }
        }
      
        [XmlElement]
        public bool IsUnknown
        {
        get { return m_isUnknown;}
        set { m_isUnknown = value; }
        }
      
        [XmlElement]
        public int? DefaultVanId
        {
        get { return m_defaultVanId;}
        set { m_defaultVanId = value; }
        }
      
        [XmlElement]
        public int? SecurityRoleId
        {
        get { return m_securityRoleId;}
        set { m_securityRoleId = value; }
        }
      

      public static int FieldsCount
      {
      get { return 17; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    