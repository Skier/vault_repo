
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


      public partial class VisitStatus : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [VisitStatus] ( " +
      
        " ID, " +
      
        " Status, " +
      
        " Description " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @Status, " +
      
        " @Description " +
      
      ")";

      public static void Insert(VisitStatus visitStatus, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", visitStatus.ID);
      
        Database.PutParameter(dbCommand,"@Status", visitStatus.Status);
      
        Database.PutParameter(dbCommand,"@Description", visitStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(VisitStatus visitStatus)
      {
        Insert(visitStatus, null);
      }

      public static void Insert(List<VisitStatus>  visitStatusList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(VisitStatus visitStatus in  visitStatusList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", visitStatus.ID);
      
        Database.PutParameter(dbCommand,"@Status", visitStatus.Status);
      
        Database.PutParameter(dbCommand,"@Description", visitStatus.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",visitStatus.ID);
      
        Database.UpdateParameter(dbCommand,"@Status",visitStatus.Status);
      
        Database.UpdateParameter(dbCommand,"@Description",visitStatus.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<VisitStatus>  visitStatusList)
      {
      Insert(visitStatusList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [VisitStatus] Set "
      
        + " Status = @Status, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(VisitStatus visitStatus, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", visitStatus.ID);
      
        Database.PutParameter(dbCommand,"@Status", visitStatus.Status);
      
        Database.PutParameter(dbCommand,"@Description", visitStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(VisitStatus visitStatus)
      {
      Update(visitStatus, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Status, "
      
        + " Description "
      

      + " From [VisitStatus] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static VisitStatus FindByPrimaryKey(
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
      throw new DataNotFoundException("VisitStatus not found, search by primary key");

      }

      public static VisitStatus FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(VisitStatus visitStatus, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",visitStatus.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(VisitStatus visitStatus)
      {
      return Exists(visitStatus, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from VisitStatus";

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

      public static VisitStatus Load(IDataReader dataReader)
      {
      VisitStatus visitStatus = new VisitStatus();

      visitStatus.ID = dataReader.GetInt32(0);
          visitStatus.Status = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            visitStatus.Description = dataReader.GetString(2);
          

      return visitStatus;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [VisitStatus] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(VisitStatus visitStatus, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", visitStatus.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(VisitStatus visitStatus)
      {
      Delete(visitStatus, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [VisitStatus] ";

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
      

      + " From [VisitStatus] ";
      public static List<VisitStatus> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<VisitStatus> rv = new List<VisitStatus>();

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

      public static List<VisitStatus> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<VisitStatus> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<VisitStatus> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(VisitStatus));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(VisitStatus item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<VisitStatus>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(VisitStatus));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<VisitStatus> itemsList
      = new List<VisitStatus>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is VisitStatus)
      itemsList.Add(deserializedObject as VisitStatus);
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
      public VisitStatus(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public VisitStatus(
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

    