
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


      public partial class WorkTransactionItem : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [WorkTransactionItem] ( " +
      
        " ID, " +
      
        " WorkTransactionId, " +
      
        " ItemId, " +
      
        " IsLeft, " +
      
        " IsCaptured " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @WorkTransactionId, " +
      
        " @ItemId, " +
      
        " @IsLeft, " +
      
        " @IsCaptured " +
      
      ")";

      public static void Insert(WorkTransactionItem workTransactionItem, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionItem.ID);
      
        Database.PutParameter(dbCommand,"@WorkTransactionId", workTransactionItem.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"@ItemId", workTransactionItem.ItemId);
      
        Database.PutParameter(dbCommand,"@IsLeft", workTransactionItem.IsLeft);
      
        Database.PutParameter(dbCommand,"@IsCaptured", workTransactionItem.IsCaptured);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(WorkTransactionItem workTransactionItem)
      {
        Insert(workTransactionItem, null);
      }

      public static void Insert(List<WorkTransactionItem>  workTransactionItemList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(WorkTransactionItem workTransactionItem in  workTransactionItemList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionItem.ID);
      
        Database.PutParameter(dbCommand,"@WorkTransactionId", workTransactionItem.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"@ItemId", workTransactionItem.ItemId);
      
        Database.PutParameter(dbCommand,"@IsLeft", workTransactionItem.IsLeft);
      
        Database.PutParameter(dbCommand,"@IsCaptured", workTransactionItem.IsCaptured);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",workTransactionItem.ID);
      
        Database.UpdateParameter(dbCommand,"@WorkTransactionId",workTransactionItem.WorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"@ItemId",workTransactionItem.ItemId);
      
        Database.UpdateParameter(dbCommand,"@IsLeft",workTransactionItem.IsLeft);
      
        Database.UpdateParameter(dbCommand,"@IsCaptured",workTransactionItem.IsCaptured);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<WorkTransactionItem>  workTransactionItemList)
      {
      Insert(workTransactionItemList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [WorkTransactionItem] Set "
      
        + " WorkTransactionId = @WorkTransactionId, "
      
        + " ItemId = @ItemId, "
      
        + " IsLeft = @IsLeft, "
      
        + " IsCaptured = @IsCaptured "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(WorkTransactionItem workTransactionItem, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionItem.ID);
      
        Database.PutParameter(dbCommand,"@WorkTransactionId", workTransactionItem.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"@ItemId", workTransactionItem.ItemId);
      
        Database.PutParameter(dbCommand,"@IsLeft", workTransactionItem.IsLeft);
      
        Database.PutParameter(dbCommand,"@IsCaptured", workTransactionItem.IsCaptured);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkTransactionItem workTransactionItem)
      {
      Update(workTransactionItem, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " WorkTransactionId, "
      
        + " ItemId, "
      
        + " IsLeft, "
      
        + " IsCaptured "
      

      + " From [WorkTransactionItem] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static WorkTransactionItem FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkTransactionItem not found, search by primary key");

      }

      public static WorkTransactionItem FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(WorkTransactionItem workTransactionItem, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",workTransactionItem.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkTransactionItem workTransactionItem)
      {
      return Exists(workTransactionItem, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from WorkTransactionItem";

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

      public static WorkTransactionItem Load(IDataReader dataReader)
      {
      WorkTransactionItem workTransactionItem = new WorkTransactionItem();

      workTransactionItem.ID = dataReader.GetInt32(0);
          workTransactionItem.WorkTransactionId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            workTransactionItem.ItemId = dataReader.GetInt32(2);
          workTransactionItem.IsLeft = dataReader.GetBoolean(3);
          workTransactionItem.IsCaptured = dataReader.GetBoolean(4);
          

      return workTransactionItem;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [WorkTransactionItem] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(WorkTransactionItem workTransactionItem, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", workTransactionItem.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransactionItem workTransactionItem)
      {
      Delete(workTransactionItem, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [WorkTransactionItem] ";

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
      
        + " WorkTransactionId, "
      
        + " ItemId, "
      
        + " IsLeft, "
      
        + " IsCaptured "
      

      + " From [WorkTransactionItem] ";
      public static List<WorkTransactionItem> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<WorkTransactionItem> rv = new List<WorkTransactionItem>();

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

      public static List<WorkTransactionItem> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkTransactionItem> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<WorkTransactionItem> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionItem));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkTransactionItem item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkTransactionItem>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionItem));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkTransactionItem> itemsList
      = new List<WorkTransactionItem>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkTransactionItem)
      itemsList.Add(deserializedObject as WorkTransactionItem);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_workTransactionId;
      
        protected int? m_itemId;
      
        protected bool m_isLeft;
      
        protected bool m_isCaptured;
      
      #endregion

      #region Constructors
      public WorkTransactionItem(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public WorkTransactionItem(
        int 
          iD,int 
          workTransactionId,int? 
          itemId,bool 
          isLeft,bool 
          isCaptured
        )
        {
        
          m_iD = iD;
        
          m_workTransactionId = workTransactionId;
        
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
        public int WorkTransactionId
        {
        get { return m_workTransactionId;}
        set { m_workTransactionId = value; }
        }
      
        [XmlElement]
        public int? ItemId
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

    