
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


      public partial class WorkDetailStatus : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [WorkDetailStatus] ( " +
      
        " ID, " +
      
        " Status, " +
      
        " Description " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @Status, " +
      
        " @Description " +
      
      ")";

      public static void Insert(WorkDetailStatus workDetailStatus, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workDetailStatus.ID);
      
        Database.PutParameter(dbCommand,"@Status", workDetailStatus.Status);
      
        Database.PutParameter(dbCommand,"@Description", workDetailStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(WorkDetailStatus workDetailStatus)
      {
        Insert(workDetailStatus, null);
      }

      public static void Insert(List<WorkDetailStatus>  workDetailStatusList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(WorkDetailStatus workDetailStatus in  workDetailStatusList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", workDetailStatus.ID);
      
        Database.PutParameter(dbCommand,"@Status", workDetailStatus.Status);
      
        Database.PutParameter(dbCommand,"@Description", workDetailStatus.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",workDetailStatus.ID);
      
        Database.UpdateParameter(dbCommand,"@Status",workDetailStatus.Status);
      
        Database.UpdateParameter(dbCommand,"@Description",workDetailStatus.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<WorkDetailStatus>  workDetailStatusList)
      {
      Insert(workDetailStatusList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [WorkDetailStatus] Set "
      
        + " Status = @Status, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(WorkDetailStatus workDetailStatus, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workDetailStatus.ID);
      
        Database.PutParameter(dbCommand,"@Status", workDetailStatus.Status);
      
        Database.PutParameter(dbCommand,"@Description", workDetailStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkDetailStatus workDetailStatus)
      {
      Update(workDetailStatus, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Status, "
      
        + " Description "
      

      + " From [WorkDetailStatus] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static WorkDetailStatus FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkDetailStatus not found, search by primary key");

      }

      public static WorkDetailStatus FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(WorkDetailStatus workDetailStatus, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",workDetailStatus.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkDetailStatus workDetailStatus)
      {
      return Exists(workDetailStatus, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from WorkDetailStatus";

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

      public static WorkDetailStatus Load(IDataReader dataReader)
      {
      WorkDetailStatus workDetailStatus = new WorkDetailStatus();

      workDetailStatus.ID = dataReader.GetInt32(0);
          workDetailStatus.Status = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            workDetailStatus.Description = dataReader.GetString(2);
          

      return workDetailStatus;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [WorkDetailStatus] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(WorkDetailStatus workDetailStatus, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", workDetailStatus.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkDetailStatus workDetailStatus)
      {
      Delete(workDetailStatus, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [WorkDetailStatus] ";

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
      
        + " Status, "
      
        + " Description "
      

      + " From [WorkDetailStatus] ";
      public static List<WorkDetailStatus> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<WorkDetailStatus> rv = new List<WorkDetailStatus>();

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

      public static List<WorkDetailStatus> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkDetailStatus> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<WorkDetailStatus> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkDetailStatus));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkDetailStatus item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkDetailStatus>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkDetailStatus));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkDetailStatus> itemsList
      = new List<WorkDetailStatus>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkDetailStatus)
      itemsList.Add(deserializedObject as WorkDetailStatus);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_status;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public WorkDetailStatus(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public WorkDetailStatus(
        int 
          iD,String 
          status,String 
          description
        )
        {
        
          m_iD = iD;
        
          m_status = status;
        
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
        public String Status
        {
        get { return m_status;}
        set { m_status = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
      }
      #endregion
      }

    