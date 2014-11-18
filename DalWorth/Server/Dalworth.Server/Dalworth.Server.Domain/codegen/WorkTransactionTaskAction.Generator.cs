
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


      public partial class WorkTransactionTaskAction : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WorkTransactionTaskAction ( " +
      
        " ID, " +
      
        " TaskAction " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?TaskAction " +
      
      ")";

      public static void Insert(WorkTransactionTaskAction workTransactionTaskAction, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", workTransactionTaskAction.ID);
      
        Database.PutParameter(dbCommand,"?TaskAction", workTransactionTaskAction.TaskAction);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(WorkTransactionTaskAction workTransactionTaskAction)
      {
        Insert(workTransactionTaskAction, null);
      }


      public static void Insert(List<WorkTransactionTaskAction>  workTransactionTaskActionList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WorkTransactionTaskAction workTransactionTaskAction in  workTransactionTaskActionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", workTransactionTaskAction.ID);
      
        Database.PutParameter(dbCommand,"?TaskAction", workTransactionTaskAction.TaskAction);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",workTransactionTaskAction.ID);
      
        Database.UpdateParameter(dbCommand,"?TaskAction",workTransactionTaskAction.TaskAction);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<WorkTransactionTaskAction>  workTransactionTaskActionList)
      {
        Insert(workTransactionTaskActionList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WorkTransactionTaskAction Set "
      
        + " TaskAction = ?TaskAction "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(WorkTransactionTaskAction workTransactionTaskAction, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", workTransactionTaskAction.ID);
      
        Database.PutParameter(dbCommand,"?TaskAction", workTransactionTaskAction.TaskAction);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkTransactionTaskAction workTransactionTaskAction)
      {
        Update(workTransactionTaskAction, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " TaskAction "
      

      + " From WorkTransactionTaskAction "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static WorkTransactionTaskAction FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkTransactionTaskAction not found, search by primary key");

      }

      public static WorkTransactionTaskAction FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WorkTransactionTaskAction workTransactionTaskAction, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",workTransactionTaskAction.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkTransactionTaskAction workTransactionTaskAction)
      {
      return Exists(workTransactionTaskAction, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WorkTransactionTaskAction limit 1";

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

      public static WorkTransactionTaskAction Load(IDataReader dataReader, int offset)
      {
      WorkTransactionTaskAction workTransactionTaskAction = new WorkTransactionTaskAction();

      workTransactionTaskAction.ID = dataReader.GetInt32(0 + offset);
          workTransactionTaskAction.TaskAction = dataReader.GetString(1 + offset);
          

      return workTransactionTaskAction;
      }

      public static WorkTransactionTaskAction Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WorkTransactionTaskAction "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(WorkTransactionTaskAction workTransactionTaskAction, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", workTransactionTaskAction.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransactionTaskAction workTransactionTaskAction)
      {
        Delete(workTransactionTaskAction, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WorkTransactionTaskAction ";

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
      
        + " TaskAction "
      

      + " From WorkTransactionTaskAction ";
      public static List<WorkTransactionTaskAction> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WorkTransactionTaskAction> rv = new List<WorkTransactionTaskAction>();

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

      public static List<WorkTransactionTaskAction> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkTransactionTaskAction> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WorkTransactionTaskAction obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && TaskAction == obj.TaskAction;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WorkTransactionTaskAction> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionTaskAction));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkTransactionTaskAction item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkTransactionTaskAction>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionTaskAction));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkTransactionTaskAction> itemsList
      = new List<WorkTransactionTaskAction>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkTransactionTaskAction)
      itemsList.Add(deserializedObject as WorkTransactionTaskAction);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_taskAction;
      
      #endregion

      #region Constructors
      public WorkTransactionTaskAction(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public WorkTransactionTaskAction(
        int 
          iD,String 
          taskAction
        ) : this()
        {
        
          m_iD = iD;
        
          m_taskAction = taskAction;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String TaskAction
        {
        get { return m_taskAction;}
        set { m_taskAction = value; }
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

    