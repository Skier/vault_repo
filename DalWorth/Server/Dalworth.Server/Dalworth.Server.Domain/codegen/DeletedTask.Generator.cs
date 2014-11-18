
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


      public partial class DeletedTask : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into DeletedTask ( " +
      
        " ServmanOrderNum, " +
      
        " LastSyncDate " +
      
      ") Values (" +
      
        " ?ServmanOrderNum, " +
      
        " ?LastSyncDate " +
      
      ")";

      public static void Insert(DeletedTask deletedTask, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ServmanOrderNum", deletedTask.ServmanOrderNum);
      
        Database.PutParameter(dbCommand,"?LastSyncDate", deletedTask.LastSyncDate);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(DeletedTask deletedTask)
      {
        Insert(deletedTask, null);
      }


      public static void Insert(List<DeletedTask>  deletedTaskList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(DeletedTask deletedTask in  deletedTaskList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ServmanOrderNum", deletedTask.ServmanOrderNum);
      
        Database.PutParameter(dbCommand,"?LastSyncDate", deletedTask.LastSyncDate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ServmanOrderNum",deletedTask.ServmanOrderNum);
      
        Database.UpdateParameter(dbCommand,"?LastSyncDate",deletedTask.LastSyncDate);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<DeletedTask>  deletedTaskList)
      {
        Insert(deletedTaskList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update DeletedTask Set "
      
        + " LastSyncDate = ?LastSyncDate "
      
        + " Where "
        
          + " ServmanOrderNum = ?ServmanOrderNum "
        
      ;

      public static void Update(DeletedTask deletedTask, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ServmanOrderNum", deletedTask.ServmanOrderNum);
      
        Database.PutParameter(dbCommand,"?LastSyncDate", deletedTask.LastSyncDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(DeletedTask deletedTask)
      {
        Update(deletedTask, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ServmanOrderNum, "
      
        + " LastSyncDate "
      

      + " From DeletedTask "

      
        + " Where "
        
          + " ServmanOrderNum = ?ServmanOrderNum "
        
      ;

      public static DeletedTask FindByPrimaryKey(
      String servmanOrderNum, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ServmanOrderNum", servmanOrderNum);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("DeletedTask not found, search by primary key");

      }

      public static DeletedTask FindByPrimaryKey(
      String servmanOrderNum
      )
      {
      return FindByPrimaryKey(
      servmanOrderNum, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(DeletedTask deletedTask, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ServmanOrderNum",deletedTask.ServmanOrderNum);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(DeletedTask deletedTask)
      {
      return Exists(deletedTask, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from DeletedTask limit 1";

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

      public static DeletedTask Load(IDataReader dataReader, int offset)
      {
      DeletedTask deletedTask = new DeletedTask();

      deletedTask.ServmanOrderNum = dataReader.GetString(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            deletedTask.LastSyncDate = dataReader.GetDateTime(1 + offset);
          

      return deletedTask;
      }

      public static DeletedTask Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From DeletedTask "

      
        + " Where "
        
          + " ServmanOrderNum = ?ServmanOrderNum "
        
      ;
      public static void Delete(DeletedTask deletedTask, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ServmanOrderNum", deletedTask.ServmanOrderNum);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(DeletedTask deletedTask)
      {
        Delete(deletedTask, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From DeletedTask ";

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

      
        + " ServmanOrderNum, "
      
        + " LastSyncDate "
      

      + " From DeletedTask ";
      public static List<DeletedTask> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<DeletedTask> rv = new List<DeletedTask>();

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

      public static List<DeletedTask> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<DeletedTask> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (DeletedTask obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ServmanOrderNum == obj.ServmanOrderNum && LastSyncDate == obj.LastSyncDate;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<DeletedTask> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(DeletedTask));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(DeletedTask item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<DeletedTask>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(DeletedTask));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<DeletedTask> itemsList
      = new List<DeletedTask>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is DeletedTask)
      itemsList.Add(deserializedObject as DeletedTask);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_servmanOrderNum;
      
        protected DateTime? m_lastSyncDate;
      
      #endregion

      #region Constructors
      public DeletedTask(
      String 
          servmanOrderNum
      ) : this()
      {
      
        m_servmanOrderNum = servmanOrderNum;
      
      }

      


        public DeletedTask(
        String 
          servmanOrderNum,DateTime? 
          lastSyncDate
        ) : this()
        {
        
          m_servmanOrderNum = servmanOrderNum;
        
          m_lastSyncDate = lastSyncDate;
        
        }


      
      #endregion

      
        [XmlElement]
        public String ServmanOrderNum
        {
        get { return m_servmanOrderNum;}
        set { m_servmanOrderNum = value; }
        }
      
        [XmlElement]
        public DateTime? LastSyncDate
        {
        get { return m_lastSyncDate;}
        set { m_lastSyncDate = value; }
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

    