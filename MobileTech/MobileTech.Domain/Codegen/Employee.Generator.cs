
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class Employee
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Employee ( " +
      
        " LocationId, " +
      
        " EmployeeId, " +
      
        " FirstName, " +
      
        " LastName " +
      
      ") Values (" +
      
        " @LocationId, " +
      
        " @EmployeeId, " +
      
        " @FirstName, " +
      
        " @LastName " +
      
      ")";

      public static void Insert(Employee employee)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", employee.LocationId);
      
        Database.PutParameter(dbCommand,"@EmployeeId", employee.EmployeeId);
      
        Database.PutParameter(dbCommand,"@FirstName", employee.FirstName);
      
        Database.PutParameter(dbCommand,"@LastName", employee.LastName);
      

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
      
        Database.PutParameter(dbCommand,"@LocationId", employee.LocationId);
      
        Database.PutParameter(dbCommand,"@EmployeeId", employee.EmployeeId);
      
        Database.PutParameter(dbCommand,"@FirstName", employee.FirstName);
      
        Database.PutParameter(dbCommand,"@LastName", employee.LastName);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@LocationId",employee.LocationId);
      
        Database.UpdateParameter(dbCommand,"@EmployeeId",employee.EmployeeId);
      
        Database.UpdateParameter(dbCommand,"@FirstName",employee.FirstName);
      
        Database.UpdateParameter(dbCommand,"@LastName",employee.LastName);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update Employee Set "
      
        + " FirstName = @FirstName, "
      
        + " LastName = @LastName "
      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " EmployeeId = @EmployeeId "
        
      ;

      public static void Update(Employee employee)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", employee.LocationId);
      
        Database.PutParameter(dbCommand,"@EmployeeId", employee.EmployeeId);
      
        Database.PutParameter(dbCommand,"@FirstName", employee.FirstName);
      
        Database.PutParameter(dbCommand,"@LastName", employee.LastName);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " LocationId, "
      
        + " EmployeeId, "
      
        + " FirstName, "
      
        + " LastName "
      

      + " From Employee "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " EmployeeId = @EmployeeId "
        
      ;

      public static Employee FindByPrimaryKey(
      int locationId,int employeeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", locationId);
      
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
      
        Database.PutParameter(dbCommand,"@LocationId",employee.LocationId);
      
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
      String sql = "select 1 from Employee";

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

      employee.LocationId = dataReader.GetInt32(0);
          employee.EmployeeId = dataReader.GetInt32(1);
          employee.FirstName = dataReader.GetString(2);
          employee.LastName = dataReader.GetString(3);
          

      return employee;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Employee "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " EmployeeId = @EmployeeId "
        
      ;
      public static void Delete(Employee employee)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@LocationId", employee.LocationId);
      
        Database.PutParameter(dbCommand,"@EmployeeId", employee.EmployeeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From Employee ";

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

      
        + " LocationId, "
      
        + " EmployeeId, "
      
        + " FirstName, "
      
        + " LastName "
      

      + " From Employee ";
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
        
          protected int m_locationId;
        
          protected int m_employeeId;
        
          protected String m_firstName;
        
          protected String m_lastName;
        
        #endregion
        
        #region Constructors
        public Employee(
        int 
          locationId,int 
          employeeId
         )
        {
        
          m_locationId = locationId;
        
          m_employeeId = employeeId;
        
        }
        
        


        public Employee(
        int 
          locationId,int 
          employeeId,String 
          firstName,String 
          lastName
        )
        {
        
          m_locationId = locationId;
        
          m_employeeId = employeeId;
        
          m_firstName = firstName;
        
          m_lastName = lastName;
        
          }


        
      #endregion

      
        [XmlElement]
        public int LocationId
        {
          get { return m_locationId;}
          set { m_locationId = value; }
        }
      
        [XmlElement]
        public int EmployeeId
        {
          get { return m_employeeId;}
          set { m_employeeId = value; }
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
      
      }
      #endregion
      }

    