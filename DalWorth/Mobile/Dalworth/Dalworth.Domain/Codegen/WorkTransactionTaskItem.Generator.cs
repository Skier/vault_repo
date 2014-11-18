
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


      public partial class WorkTransactionTaskItem : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [WorkTransactionTaskItem] ( " +
      
        " ID, " +
      
        " WorkTransactionTaskId, " +
      
        " ItemId, " +
      
        " IsLeft, " +
      
        " IsCaptured " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @WorkTransactionTaskId, " +
      
        " @ItemId, " +
      
        " @IsLeft, " +
      
        " @IsCaptured " +
      
      ")";

      public static void Insert(WorkTransactionTaskItem workTransactionTaskItem, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionTaskItem.ID);
      
        Database.PutParameter(dbCommand,"@WorkTransactionTaskId", workTransactionTaskItem.WorkTransactionTaskId);
      
        Database.PutParameter(dbCommand,"@ItemId", workTransactionTaskItem.ItemId);
      
        Database.PutParameter(dbCommand,"@IsLeft", workTransactionTaskItem.IsLeft);
      
        Database.PutParameter(dbCommand,"@IsCaptured", workTransactionTaskItem.IsCaptured);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(WorkTransactionTaskItem workTransactionTaskItem)
      {
        Insert(workTransactionTaskItem, null);
      }

      public static void Insert(List<WorkTransactionTaskItem>  workTransactionTaskItemList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(WorkTransactionTaskItem workTransactionTaskItem in  workTransactionTaskItemList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionTaskItem.ID);
      
        Database.PutParameter(dbCommand,"@WorkTransactionTaskId", workTransactionTaskItem.WorkTransactionTaskId);
      
        Database.PutParameter(dbCommand,"@ItemId", workTransactionTaskItem.ItemId);
      
        Database.PutParameter(dbCommand,"@IsLeft", workTransactionTaskItem.IsLeft);
      
        Database.PutParameter(dbCommand,"@IsCaptured", workTransactionTaskItem.IsCaptured);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",workTransactionTaskItem.ID);
      
        Database.UpdateParameter(dbCommand,"@WorkTransactionTaskId",workTransactionTaskItem.WorkTransactionTaskId);
      
        Database.UpdateParameter(dbCommand,"@ItemId",workTransactionTaskItem.ItemId);
      
        Database.UpdateParameter(dbCommand,"@IsLeft",workTransactionTaskItem.IsLeft);
      
        Database.UpdateParameter(dbCommand,"@IsCaptured",workTransactionTaskItem.IsCaptured);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<WorkTransactionTaskItem>  workTransactionTaskItemList)
      {
      Insert(workTransactionTaskItemList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [WorkTransactionTaskItem] Set "
      
        + " WorkTransactionTaskId = @WorkTransactionTaskId, "
      
        + " ItemId = @ItemId, "
      
        + " IsLeft = @IsLeft, "
      
        + " IsCaptured = @IsCaptured "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(WorkTransactionTaskItem workTransactionTaskItem, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionTaskItem.ID);
      
        Database.PutParameter(dbCommand,"@WorkTransactionTaskId", workTransactionTaskItem.WorkTransactionTaskId);
      
        Database.PutParameter(dbCommand,"@ItemId", workTransactionTaskItem.ItemId);
      
        Database.PutParameter(dbCommand,"@IsLeft", workTransactionTaskItem.IsLeft);
      
        Database.PutParameter(dbCommand,"@IsCaptured", workTransactionTaskItem.IsCaptured);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkTransactionTaskItem workTransactionTaskItem)
      {
      Update(workTransactionTaskItem, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " WorkTransactionTaskId, "
      
        + " ItemId, "
      
        + " IsLeft, "
      
        + " IsCaptured "
      

      + " From [WorkTransactionTaskItem] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static WorkTransactionTaskItem FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkTransactionTaskItem not found, search by primary key");

      }

      public static WorkTransactionTaskItem FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(WorkTransactionTaskItem workTransactionTaskItem, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",workTransactionTaskItem.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkTransactionTaskItem workTransactionTaskItem)
      {
      return Exists(workTransactionTaskItem, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from WorkTransactionTaskItem";

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

      public static WorkTransactionTaskItem Load(IDataReader dataReader)
      {
      WorkTransactionTaskItem workTransactionTaskItem = new WorkTransactionTaskItem();

      workTransactionTaskItem.ID = dataReader.GetInt32(0);
          workTransactionTaskItem.WorkTransactionTaskId = dataReader.GetInt32(1);
          workTransactionTaskItem.ItemId = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            workTransactionTaskItem.IsLeft = dataReader.GetBoolean(3);
          
            if(!dataReader.IsDBNull(4))
            workTransactionTaskItem.IsCaptured = dataReader.GetBoolean(4);
          

      return workTransactionTaskItem;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [WorkTransactionTaskItem] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(WorkTransactionTaskItem workTransactionTaskItem, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", workTransactionTaskItem.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransactionTaskItem workTransactionTaskItem)
      {
      Delete(workTransactionTaskItem, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [WorkTransactionTaskItem] ";

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
      
        + " WorkTransactionTaskId, "
      
        + " ItemId, "
      
        + " IsLeft, "
      
        + " IsCaptured "
      

      + " From [WorkTransactionTaskItem] ";
      public static List<WorkTransactionTaskItem> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<WorkTransactionTaskItem> rv = new List<WorkTransactionTaskItem>();

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

      public static List<WorkTransactionTaskItem> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkTransactionTaskItem> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<WorkTransactionTaskItem> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionTaskItem));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkTransactionTaskItem item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkTransactionTaskItem>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionTaskItem));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkTransactionTaskItem> itemsList
      = new List<WorkTransactionTaskItem>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkTransactionTaskItem)
      itemsList.Add(deserializedObject as WorkTransactionTaskItem);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_workTransactionTaskId;
      
        protected int m_itemId;
      
        protected bool m_isLeft;
      
        protected bool m_isCaptured;
      
      #endregion

      #region Constructors
      public WorkTransactionTaskItem(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public WorkTransactionTaskItem(
        int 
          iD,int 
          workTransactionTaskId,int 
          itemId,bool 
          isLeft,bool 
          isCaptured
        )
        {
        
          m_iD = iD;
        
          m_workTransactionTaskId = workTransactionTaskId;
        
          m_itemId = itemId;
        
          m_isLeft = isLeft;
        
          m_isCaptured = isCaptured;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int WorkTransactionTaskId
        {
        get { return m_workTransactionTaskId;}
        set { m_workTransactionTaskId = value; }
        }
      
        [XmlElement]
        public int ItemId
        {
        get { return m_itemId;}
        set { m_itemId = value; }
        }
      
        [XmlElement]
        public bool IsLeft
        {
        get { return m_isLeft;}
        set { m_isLeft = value; }
        }
      
        [XmlElement]
        public bool IsCaptured
        {
        get { return m_isCaptured;}
        set { m_isCaptured = value; }
        }
      
      }
      #endregion
      }

    