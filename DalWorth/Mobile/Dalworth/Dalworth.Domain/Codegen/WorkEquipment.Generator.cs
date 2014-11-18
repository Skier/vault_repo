
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


      public partial class WorkEquipment : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [WorkEquipment] ( " +
      
        " ID, " +
      
        " WorkId, " +
      
        " EquipmentTypeId, " +
      
        " Quantity " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @WorkId, " +
      
        " @EquipmentTypeId, " +
      
        " @Quantity " +
      
      ")";

      public static void Insert(WorkEquipment workEquipment, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workEquipment.ID);
      
        Database.PutParameter(dbCommand,"@WorkId", workEquipment.WorkId);
      
        Database.PutParameter(dbCommand,"@EquipmentTypeId", workEquipment.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"@Quantity", workEquipment.Quantity);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(WorkEquipment workEquipment)
      {
        Insert(workEquipment, null);
      }

      public static void Insert(List<WorkEquipment>  workEquipmentList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(WorkEquipment workEquipment in  workEquipmentList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", workEquipment.ID);
      
        Database.PutParameter(dbCommand,"@WorkId", workEquipment.WorkId);
      
        Database.PutParameter(dbCommand,"@EquipmentTypeId", workEquipment.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"@Quantity", workEquipment.Quantity);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",workEquipment.ID);
      
        Database.UpdateParameter(dbCommand,"@WorkId",workEquipment.WorkId);
      
        Database.UpdateParameter(dbCommand,"@EquipmentTypeId",workEquipment.EquipmentTypeId);
      
        Database.UpdateParameter(dbCommand,"@Quantity",workEquipment.Quantity);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<WorkEquipment>  workEquipmentList)
      {
      Insert(workEquipmentList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [WorkEquipment] Set "
      
        + " WorkId = @WorkId, "
      
        + " EquipmentTypeId = @EquipmentTypeId, "
      
        + " Quantity = @Quantity "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(WorkEquipment workEquipment, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workEquipment.ID);
      
        Database.PutParameter(dbCommand,"@WorkId", workEquipment.WorkId);
      
        Database.PutParameter(dbCommand,"@EquipmentTypeId", workEquipment.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"@Quantity", workEquipment.Quantity);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkEquipment workEquipment)
      {
      Update(workEquipment, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " WorkId, "
      
        + " EquipmentTypeId, "
      
        + " Quantity "
      

      + " From [WorkEquipment] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static WorkEquipment FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkEquipment not found, search by primary key");

      }

      public static WorkEquipment FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(WorkEquipment workEquipment, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",workEquipment.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkEquipment workEquipment)
      {
      return Exists(workEquipment, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from WorkEquipment";

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

      public static WorkEquipment Load(IDataReader dataReader)
      {
      WorkEquipment workEquipment = new WorkEquipment();

      workEquipment.ID = dataReader.GetInt32(0);
          workEquipment.WorkId = dataReader.GetInt32(1);
          workEquipment.EquipmentTypeId = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            workEquipment.Quantity = dataReader.GetInt32(3);
          

      return workEquipment;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [WorkEquipment] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(WorkEquipment workEquipment, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", workEquipment.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkEquipment workEquipment)
      {
      Delete(workEquipment, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [WorkEquipment] ";

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
      
        + " WorkId, "
      
        + " EquipmentTypeId, "
      
        + " Quantity "
      

      + " From [WorkEquipment] ";
      public static List<WorkEquipment> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<WorkEquipment> rv = new List<WorkEquipment>();

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

      public static List<WorkEquipment> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkEquipment> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<WorkEquipment> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkEquipment));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkEquipment item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkEquipment>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkEquipment));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkEquipment> itemsList
      = new List<WorkEquipment>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkEquipment)
      itemsList.Add(deserializedObject as WorkEquipment);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_workId;
      
        protected int m_equipmentTypeId;
      
        protected int? m_quantity;
      
      #endregion

      #region Constructors
      public WorkEquipment(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public WorkEquipment(
        int 
          iD,int 
          workId,int 
          equipmentTypeId,int? 
          quantity
        )
        {
        
          m_iD = iD;
        
          m_workId = workId;
        
          m_equipmentTypeId = equipmentTypeId;
        
          m_quantity = quantity;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int WorkId
        {
        get { return m_workId;}
        set { m_workId = value; }
        }
      
        [XmlElement]
        public int EquipmentTypeId
        {
        get { return m_equipmentTypeId;}
        set { m_equipmentTypeId = value; }
        }
      
        [XmlElement]
        public int? Quantity
        {
        get { return m_quantity;}
        set { m_quantity = value; }
        }
      
      }
      #endregion
      }

    