
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


      public partial class WorkTransactionTaskEquipment : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [WorkTransactionTaskEquipment] ( " +
      
        " ID, " +
      
        " WorkTransactionTaskId, " +
      
        " EquipmentId, " +
      
        " IsLeft, " +
      
        " IsCaptured " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @WorkTransactionTaskId, " +
      
        " @EquipmentId, " +
      
        " @IsLeft, " +
      
        " @IsCaptured " +
      
      ")";

      public static void Insert(WorkTransactionTaskEquipment workTransactionTaskEquipment, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionTaskEquipment.ID);
      
        Database.PutParameter(dbCommand,"@WorkTransactionTaskId", workTransactionTaskEquipment.WorkTransactionTaskId);
      
        Database.PutParameter(dbCommand,"@EquipmentId", workTransactionTaskEquipment.EquipmentId);
      
        Database.PutParameter(dbCommand,"@IsLeft", workTransactionTaskEquipment.IsLeft);
      
        Database.PutParameter(dbCommand,"@IsCaptured", workTransactionTaskEquipment.IsCaptured);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(WorkTransactionTaskEquipment workTransactionTaskEquipment)
      {
        Insert(workTransactionTaskEquipment, null);
      }

      public static void Insert(List<WorkTransactionTaskEquipment>  workTransactionTaskEquipmentList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(WorkTransactionTaskEquipment workTransactionTaskEquipment in  workTransactionTaskEquipmentList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionTaskEquipment.ID);
      
        Database.PutParameter(dbCommand,"@WorkTransactionTaskId", workTransactionTaskEquipment.WorkTransactionTaskId);
      
        Database.PutParameter(dbCommand,"@EquipmentId", workTransactionTaskEquipment.EquipmentId);
      
        Database.PutParameter(dbCommand,"@IsLeft", workTransactionTaskEquipment.IsLeft);
      
        Database.PutParameter(dbCommand,"@IsCaptured", workTransactionTaskEquipment.IsCaptured);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",workTransactionTaskEquipment.ID);
      
        Database.UpdateParameter(dbCommand,"@WorkTransactionTaskId",workTransactionTaskEquipment.WorkTransactionTaskId);
      
        Database.UpdateParameter(dbCommand,"@EquipmentId",workTransactionTaskEquipment.EquipmentId);
      
        Database.UpdateParameter(dbCommand,"@IsLeft",workTransactionTaskEquipment.IsLeft);
      
        Database.UpdateParameter(dbCommand,"@IsCaptured",workTransactionTaskEquipment.IsCaptured);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<WorkTransactionTaskEquipment>  workTransactionTaskEquipmentList)
      {
      Insert(workTransactionTaskEquipmentList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [WorkTransactionTaskEquipment] Set "
      
        + " WorkTransactionTaskId = @WorkTransactionTaskId, "
      
        + " EquipmentId = @EquipmentId, "
      
        + " IsLeft = @IsLeft, "
      
        + " IsCaptured = @IsCaptured "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(WorkTransactionTaskEquipment workTransactionTaskEquipment, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionTaskEquipment.ID);
      
        Database.PutParameter(dbCommand,"@WorkTransactionTaskId", workTransactionTaskEquipment.WorkTransactionTaskId);
      
        Database.PutParameter(dbCommand,"@EquipmentId", workTransactionTaskEquipment.EquipmentId);
      
        Database.PutParameter(dbCommand,"@IsLeft", workTransactionTaskEquipment.IsLeft);
      
        Database.PutParameter(dbCommand,"@IsCaptured", workTransactionTaskEquipment.IsCaptured);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkTransactionTaskEquipment workTransactionTaskEquipment)
      {
      Update(workTransactionTaskEquipment, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " WorkTransactionTaskId, "
      
        + " EquipmentId, "
      
        + " IsLeft, "
      
        + " IsCaptured "
      

      + " From [WorkTransactionTaskEquipment] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static WorkTransactionTaskEquipment FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkTransactionTaskEquipment not found, search by primary key");

      }

      public static WorkTransactionTaskEquipment FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(WorkTransactionTaskEquipment workTransactionTaskEquipment, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",workTransactionTaskEquipment.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkTransactionTaskEquipment workTransactionTaskEquipment)
      {
      return Exists(workTransactionTaskEquipment, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from WorkTransactionTaskEquipment";

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

      public static WorkTransactionTaskEquipment Load(IDataReader dataReader)
      {
      WorkTransactionTaskEquipment workTransactionTaskEquipment = new WorkTransactionTaskEquipment();

      workTransactionTaskEquipment.ID = dataReader.GetInt32(0);
          workTransactionTaskEquipment.WorkTransactionTaskId = dataReader.GetInt32(1);
          workTransactionTaskEquipment.EquipmentId = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            workTransactionTaskEquipment.IsLeft = dataReader.GetBoolean(3);
          
            if(!dataReader.IsDBNull(4))
            workTransactionTaskEquipment.IsCaptured = dataReader.GetBoolean(4);
          

      return workTransactionTaskEquipment;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [WorkTransactionTaskEquipment] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(WorkTransactionTaskEquipment workTransactionTaskEquipment, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", workTransactionTaskEquipment.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransactionTaskEquipment workTransactionTaskEquipment)
      {
      Delete(workTransactionTaskEquipment, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [WorkTransactionTaskEquipment] ";

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
      
        + " EquipmentId, "
      
        + " IsLeft, "
      
        + " IsCaptured "
      

      + " From [WorkTransactionTaskEquipment] ";
      public static List<WorkTransactionTaskEquipment> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<WorkTransactionTaskEquipment> rv = new List<WorkTransactionTaskEquipment>();

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

      public static List<WorkTransactionTaskEquipment> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkTransactionTaskEquipment> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<WorkTransactionTaskEquipment> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionTaskEquipment));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkTransactionTaskEquipment item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkTransactionTaskEquipment>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionTaskEquipment));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkTransactionTaskEquipment> itemsList
      = new List<WorkTransactionTaskEquipment>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkTransactionTaskEquipment)
      itemsList.Add(deserializedObject as WorkTransactionTaskEquipment);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_workTransactionTaskId;
      
        protected int m_equipmentId;
      
        protected bool m_isLeft;
      
        protected bool m_isCaptured;
      
      #endregion

      #region Constructors
      public WorkTransactionTaskEquipment(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public WorkTransactionTaskEquipment(
        int 
          iD,int 
          workTransactionTaskId,int 
          equipmentId,bool 
          isLeft,bool 
          isCaptured
        )
        {
        
          m_iD = iD;
        
          m_workTransactionTaskId = workTransactionTaskId;
        
          m_equipmentId = equipmentId;
        
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
        public int EquipmentId
        {
        get { return m_equipmentId;}
        set { m_equipmentId = value; }
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

    