
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Servman.Domain
      {


      public partial class dept
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into dept ( " +
      
        " department, " +
      
        " descript, " +
      
        " dept_id " +
      
      ") Values (" +
      
        " ?, " +
      
        " ?, " +
      
        " ? " +
      
      ")";

      public static void Insert(dept dept)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@department", dept.department);
      
        Database.PutParameter(dbCommand,"@descript", dept.descript);
      
        Database.PutParameter(dbCommand,"@dept_id", dept.dept_id);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<dept>  deptList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(dept dept in  deptList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@department", dept.department);
      
        Database.PutParameter(dbCommand,"@descript", dept.descript);
      
        Database.PutParameter(dbCommand,"@dept_id", dept.dept_id);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@department",dept.department);
      
        Database.UpdateParameter(dbCommand,"@descript",dept.descript);
      
        Database.UpdateParameter(dbCommand,"@dept_id",dept.dept_id);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update dept Set "
      
        + " dept.descript = ? , "
      
        + " dept.dept_id = ?  "
      
        + " Where "
        
          + " dept.department = ?  "
        
      ;

      public static void Update(dept dept)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@descript", dept.descript);
      
        Database.PutParameter(dbCommand,"@dept_id", dept.dept_id);
      
        Database.PutParameter(dbCommand,"@department", dept.department);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " dept.department, "
      
        + " dept.descript, "
      
        + " dept.dept_id "
      

      + " From dept "

      
        + " Where "
        
          + " dept.department = ?  "
        
      ;

      public static dept FindByPrimaryKey(
      String department
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@department", department);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("dept not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(dept dept)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@department",dept.department);
      

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
      String sql = "select 1 from dept";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, ConnectionKeyEnum.Servman))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static dept Load(IDataReader dataReader)
      {
      dept dept = new dept();

      dept.department = dataReader.GetString(0);
          dept.descript = dataReader.GetString(1);
          dept.dept_id = dataReader.GetInt32(2);
          

      return dept;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [dept] "

      
        + " Where "
        
          + " department = ?  "
        
      ;
      public static void Delete(dept dept)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@department", dept.department);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [dept] ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, ConnectionKeyEnum.Servman))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " dept.department, "
      
        + " dept.descript, "
      
        + " dept.dept_id "
      

      + " From dept ";
      public static List<dept> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<dept> rv = new List<dept>();

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
      List<dept> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<dept> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(dept));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(dept item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<dept>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(dept));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<dept> itemsList
      = new List<dept>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is dept)
      itemsList.Add(deserializedObject as dept);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_department;
      
        protected String m_descript;
      
        protected int m_dept_id;
      
      #endregion

      #region Constructors
      public dept(
      String 
          department
      )
      {
      
        m_department = department;
      
      }

      


        public dept(
        String 
          department,String 
          descript,int 
          dept_id
        )
        {
        
          m_department = department;
        
          m_descript = descript;
        
          m_dept_id = dept_id;
        
        }


      
      #endregion

      
        [XmlElement]
        public String department
        {
        get { return m_department;}
        set { m_department = value; }
        }
      
        [XmlElement]
        public String descript
        {
        get { return m_descript;}
        set { m_descript = value; }
        }
      
        [XmlElement]
        public int dept_id
        {
        get { return m_dept_id;}
        set { m_dept_id = value; }
        }
      
      }
      #endregion
      }

    