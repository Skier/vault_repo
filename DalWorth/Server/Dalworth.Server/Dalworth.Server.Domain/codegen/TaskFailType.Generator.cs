
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


      public partial class TaskFailType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TaskFailType ( " +
      
        " ID, " +
      
        " Type, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Type, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(TaskFailType taskFailType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", taskFailType.ID);
      
        Database.PutParameter(dbCommand,"?Type", taskFailType.Type);
      
        Database.PutParameter(dbCommand,"?Description", taskFailType.Description);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(TaskFailType taskFailType)
      {
        Insert(taskFailType, null);
      }


      public static void Insert(List<TaskFailType>  taskFailTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TaskFailType taskFailType in  taskFailTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", taskFailType.ID);
      
        Database.PutParameter(dbCommand,"?Type", taskFailType.Type);
      
        Database.PutParameter(dbCommand,"?Description", taskFailType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",taskFailType.ID);
      
        Database.UpdateParameter(dbCommand,"?Type",taskFailType.Type);
      
        Database.UpdateParameter(dbCommand,"?Description",taskFailType.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<TaskFailType>  taskFailTypeList)
      {
        Insert(taskFailTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TaskFailType Set "
      
        + " Type = ?Type, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(TaskFailType taskFailType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", taskFailType.ID);
      
        Database.PutParameter(dbCommand,"?Type", taskFailType.Type);
      
        Database.PutParameter(dbCommand,"?Description", taskFailType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TaskFailType taskFailType)
      {
        Update(taskFailType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Type, "
      
        + " Description "
      

      + " From TaskFailType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static TaskFailType FindByPrimaryKey(
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
      throw new DataNotFoundException("TaskFailType not found, search by primary key");

      }

      public static TaskFailType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TaskFailType taskFailType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",taskFailType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TaskFailType taskFailType)
      {
      return Exists(taskFailType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TaskFailType limit 1";

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

      public static TaskFailType Load(IDataReader dataReader, int offset)
      {
      TaskFailType taskFailType = new TaskFailType();

      taskFailType.ID = dataReader.GetInt32(0 + offset);
          taskFailType.Type = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            taskFailType.Description = dataReader.GetString(2 + offset);
          

      return taskFailType;
      }

      public static TaskFailType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TaskFailType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(TaskFailType taskFailType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", taskFailType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TaskFailType taskFailType)
      {
        Delete(taskFailType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TaskFailType ";

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
      
        + " Type, "
      
        + " Description "
      

      + " From TaskFailType ";
      public static List<TaskFailType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TaskFailType> rv = new List<TaskFailType>();

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

      public static List<TaskFailType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TaskFailType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (TaskFailType obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Type == obj.Type && Description == obj.Description;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<TaskFailType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskFailType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TaskFailType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TaskFailType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskFailType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TaskFailType> itemsList
      = new List<TaskFailType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TaskFailType)
      itemsList.Add(deserializedObject as TaskFailType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_type;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public TaskFailType(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public TaskFailType(
        int 
          iD,String 
          type,String 
          description
        ) : this()
        {
        
          m_iD = iD;
        
          m_type = type;
        
          m_description = description;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String Type
        {
        get { return m_type;}
        set { m_type = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      

      public static int FieldsCount
      {
      get { return 3; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    