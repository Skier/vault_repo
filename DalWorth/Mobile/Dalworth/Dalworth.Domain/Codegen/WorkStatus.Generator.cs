
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


      public partial class WorkStatus : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [WorkStatus] ( " +
      
        " ID, " +
      
        " Status, " +
      
        " Description " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @Status, " +
      
        " @Description " +
      
      ")";

      public static void Insert(WorkStatus workStatus, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workStatus.ID);
      
        Database.PutParameter(dbCommand,"@Status", workStatus.Status);
      
        Database.PutParameter(dbCommand,"@Description", workStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(WorkStatus workStatus)
      {
        Insert(workStatus, null);
      }

      public static void Insert(List<WorkStatus>  workStatusList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(WorkStatus workStatus in  workStatusList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", workStatus.ID);
      
        Database.PutParameter(dbCommand,"@Status", workStatus.Status);
      
        Database.PutParameter(dbCommand,"@Description", workStatus.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",workStatus.ID);
      
        Database.UpdateParameter(dbCommand,"@Status",workStatus.Status);
      
        Database.UpdateParameter(dbCommand,"@Description",workStatus.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<WorkStatus>  workStatusList)
      {
      Insert(workStatusList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [WorkStatus] Set "
      
        + " Status = @Status, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(WorkStatus workStatus, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workStatus.ID);
      
        Database.PutParameter(dbCommand,"@Status", workStatus.Status);
      
        Database.PutParameter(dbCommand,"@Description", workStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkStatus workStatus)
      {
      Update(workStatus, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Status, "
      
        + " Description "
      

      + " From [WorkStatus] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static WorkStatus FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkStatus not found, search by primary key");

      }

      public static WorkStatus FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(WorkStatus workStatus, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",workStatus.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkStatus workStatus)
      {
      return Exists(workStatus, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from WorkStatus";

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

      public static WorkStatus Load(IDataReader dataReader)
      {
      WorkStatus workStatus = new WorkStatus();

      workStatus.ID = dataReader.GetInt32(0);
          workStatus.Status = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            workStatus.Description = dataReader.GetString(2);
          

      return workStatus;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [WorkStatus] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(WorkStatus workStatus, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", workStatus.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkStatus workStatus)
      {
      Delete(workStatus, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [WorkStatus] ";

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
      

      + " From [WorkStatus] ";
      public static List<WorkStatus> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<WorkStatus> rv = new List<WorkStatus>();

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

      public static List<WorkStatus> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkStatus> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<WorkStatus> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkStatus));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkStatus item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkStatus>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkStatus));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkStatus> itemsList
      = new List<WorkStatus>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkStatus)
      itemsList.Add(deserializedObject as WorkStatus);
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
      public WorkStatus(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public WorkStatus(
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

    