
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


      public partial class WorkTransactionEquipment : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [WorkTransactionEquipment] ( " +
      
        " ID, " +
      
        " WorkTransactionId, " +
      
        " EquipmentId, " +
      
        " IsLeft, " +
      
        " IsCaptured " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @WorkTransactionId, " +
      
        " @EquipmentId, " +
      
        " @IsLeft, " +
      
        " @IsCaptured " +
      
      ")";

      public static void Insert(WorkTransactionEquipment workTransactionEquipment, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionEquipment.ID);
      
        Database.PutParameter(dbCommand,"@WorkTransactionId", workTransactionEquipment.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"@EquipmentId", workTransactionEquipment.EquipmentId);
      
        Database.PutParameter(dbCommand,"@IsLeft", workTransactionEquipment.IsLeft);
      
        Database.PutParameter(dbCommand,"@IsCaptured", workTransactionEquipment.IsCaptured);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(WorkTransactionEquipment workTransactionEquipment)
      {
        Insert(workTransactionEquipment, null);
      }

      public static void Insert(List<WorkTransactionEquipment>  workTransactionEquipmentList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(WorkTransactionEquipment workTransactionEquipment in  workTransactionEquipmentList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionEquipment.ID);
      
        Database.PutParameter(dbCommand,"@WorkTransactionId", workTransactionEquipment.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"@EquipmentId", workTransactionEquipment.EquipmentId);
      
        Database.PutParameter(dbCommand,"@IsLeft", workTransactionEquipment.IsLeft);
      
        Database.PutParameter(dbCommand,"@IsCaptured", workTransactionEquipment.IsCaptured);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",workTransactionEquipment.ID);
      
        Database.UpdateParameter(dbCommand,"@WorkTransactionId",workTransactionEquipment.WorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"@EquipmentId",workTransactionEquipment.EquipmentId);
      
        Database.UpdateParameter(dbCommand,"@IsLeft",workTransactionEquipment.IsLeft);
      
        Database.UpdateParameter(dbCommand,"@IsCaptured",workTransactionEquipment.IsCaptured);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<WorkTransactionEquipment>  workTransactionEquipmentList)
      {
      Insert(workTransactionEquipmentList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [WorkTransactionEquipment] Set "
      
        + " WorkTransactionId = @WorkTransactionId, "
      
        + " EquipmentId = @EquipmentId, "
      
        + " IsLeft = @IsLeft, "
      
        + " IsCaptured = @IsCaptured "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(WorkTransactionEquipment workTransactionEquipment, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionEquipment.ID);
      
        Database.PutParameter(dbCommand,"@WorkTransactionId", workTransactionEquipment.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"@EquipmentId", workTransactionEquipment.EquipmentId);
      
        Database.PutParameter(dbCommand,"@IsLeft", workTransactionEquipment.IsLeft);
      
        Database.PutParameter(dbCommand,"@IsCaptured", workTransactionEquipment.IsCaptured);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkTransactionEquipment workTransactionEquipment)
      {
      Update(workTransactionEquipment, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " WorkTransactionId, "
      
        + " EquipmentId, "
      
        + " IsLeft, "
      
        + " IsCaptured "
      

      + " From [WorkTransactionEquipment] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static WorkTransactionEquipment FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkTransactionEquipment not found, search by primary key");

      }

      public static WorkTransactionEquipment FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(WorkTransactionEquipment workTransactionEquipment, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",workTransactionEquipment.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkTransactionEquipment workTransactionEquipment)
      {
      return Exists(workTransactionEquipment, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from WorkTransactionEquipment";

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

      public static WorkTransactionEquipment Load(IDataReader dataReader)
      {
      WorkTransactionEquipment workTransactionEquipment = new WorkTransactionEquipment();

      workTransactionEquipment.ID = dataReader.GetInt32(0);
          workTransactionEquipment.WorkTransactionId = dataReader.GetInt32(1);
          workTransactionEquipment.EquipmentId = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            workTransactionEquipment.IsLeft = dataReader.GetBoolean(3);
          
            if(!dataReader.IsDBNull(4))
            workTransactionEquipment.IsCaptured = dataReader.GetBoolean(4);
          

      return workTransactionEquipment;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [WorkTransactionEquipment] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(WorkTransactionEquipment workTransactionEquipment, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", workTransactionEquipment.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransactionEquipment workTransactionEquipment)
      {
      Delete(workTransactionEquipment, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [WorkTransactionEquipment] ";

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
      
        + " EquipmentId, "
      
        + " IsLeft, "
      
        + " IsCaptured "
      

      + " From [WorkTransactionEquipment] ";
      public static List<WorkTransactionEquipment> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<WorkTransactionEquipment> rv = new List<WorkTransactionEquipment>();

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

      public static List<WorkTransactionEquipment> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkTransactionEquipment> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<WorkTransactionEquipment> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionEquipment));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkTransactionEquipment item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkTransactionEquipment>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionEquipment));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkTransactionEquipment> itemsList
      = new List<WorkTransactionEquipment>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkTransactionEquipment)
      itemsList.Add(deserializedObject as WorkTransactionEquipment);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_workTransactionId;
      
        protected int m_equipmentId;
      
        protected bool m_isLeft;
      
        protected bool m_isCaptured;
      
      #endregion

      #region Constructors
      public WorkTransactionEquipment(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public WorkTransactionEquipment(
        int 
          iD,int 
          workTransactionId,int 
          equipmentId,bool 
          isLeft,bool 
          isCaptured
        )
        {
        
          m_iD = iD;
        
          m_workTransactionId = workTransactionId;
        
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
        public int WorkTransactionId
        {
        get { return m_workTransactionId;}
        set { m_workTransactionId = value; }
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

    