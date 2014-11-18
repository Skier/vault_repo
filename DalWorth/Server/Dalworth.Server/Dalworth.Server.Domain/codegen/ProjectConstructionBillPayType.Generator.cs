
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


      public partial class ProjectConstructionBillPayType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ProjectConstructionBillPayType ( " +
      
        " ID, " +
      
        " BillPayType " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?BillPayType " +
      
      ")";

      public static void Insert(ProjectConstructionBillPayType projectConstructionBillPayType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", projectConstructionBillPayType.ID);
      
        Database.PutParameter(dbCommand,"?BillPayType", projectConstructionBillPayType.BillPayType);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ProjectConstructionBillPayType projectConstructionBillPayType)
      {
        Insert(projectConstructionBillPayType, null);
      }


      public static void Insert(List<ProjectConstructionBillPayType>  projectConstructionBillPayTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ProjectConstructionBillPayType projectConstructionBillPayType in  projectConstructionBillPayTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", projectConstructionBillPayType.ID);
      
        Database.PutParameter(dbCommand,"?BillPayType", projectConstructionBillPayType.BillPayType);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",projectConstructionBillPayType.ID);
      
        Database.UpdateParameter(dbCommand,"?BillPayType",projectConstructionBillPayType.BillPayType);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ProjectConstructionBillPayType>  projectConstructionBillPayTypeList)
      {
        Insert(projectConstructionBillPayTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ProjectConstructionBillPayType Set "
      
        + " BillPayType = ?BillPayType "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(ProjectConstructionBillPayType projectConstructionBillPayType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", projectConstructionBillPayType.ID);
      
        Database.PutParameter(dbCommand,"?BillPayType", projectConstructionBillPayType.BillPayType);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ProjectConstructionBillPayType projectConstructionBillPayType)
      {
        Update(projectConstructionBillPayType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " BillPayType "
      

      + " From ProjectConstructionBillPayType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static ProjectConstructionBillPayType FindByPrimaryKey(
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
      throw new DataNotFoundException("ProjectConstructionBillPayType not found, search by primary key");

      }

      public static ProjectConstructionBillPayType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ProjectConstructionBillPayType projectConstructionBillPayType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",projectConstructionBillPayType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ProjectConstructionBillPayType projectConstructionBillPayType)
      {
      return Exists(projectConstructionBillPayType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ProjectConstructionBillPayType limit 1";

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

      public static ProjectConstructionBillPayType Load(IDataReader dataReader, int offset)
      {
      ProjectConstructionBillPayType projectConstructionBillPayType = new ProjectConstructionBillPayType();

      projectConstructionBillPayType.ID = dataReader.GetInt32(0 + offset);
          projectConstructionBillPayType.BillPayType = dataReader.GetString(1 + offset);
          

      return projectConstructionBillPayType;
      }

      public static ProjectConstructionBillPayType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ProjectConstructionBillPayType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(ProjectConstructionBillPayType projectConstructionBillPayType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", projectConstructionBillPayType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectConstructionBillPayType projectConstructionBillPayType)
      {
        Delete(projectConstructionBillPayType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ProjectConstructionBillPayType ";

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
      
        + " BillPayType "
      

      + " From ProjectConstructionBillPayType ";
      public static List<ProjectConstructionBillPayType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ProjectConstructionBillPayType> rv = new List<ProjectConstructionBillPayType>();

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

      public static List<ProjectConstructionBillPayType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ProjectConstructionBillPayType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ProjectConstructionBillPayType obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && BillPayType == obj.BillPayType;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ProjectConstructionBillPayType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectConstructionBillPayType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ProjectConstructionBillPayType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ProjectConstructionBillPayType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectConstructionBillPayType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ProjectConstructionBillPayType> itemsList
      = new List<ProjectConstructionBillPayType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ProjectConstructionBillPayType)
      itemsList.Add(deserializedObject as ProjectConstructionBillPayType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_billPayType;
      
      #endregion

      #region Constructors
      public ProjectConstructionBillPayType(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public ProjectConstructionBillPayType(
        int 
          iD,String 
          billPayType
        ) : this()
        {
        
          m_iD = iD;
        
          m_billPayType = billPayType;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String BillPayType
        {
        get { return m_billPayType;}
        set { m_billPayType = value; }
        }
      

      public static int FieldsCount
      {
      get { return 2; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    