
    using System;
    using System.Data;
    using System.Collections.Generic;
    using TractInc.Server.Data;
    using TractInc.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace TractInc.Server.Domain
      {


      public partial class ContractStatus
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [ContractStatus] ( " +
      
        " StatusName " +
      
      ") Values (" +
      
        " @StatusName " +
      
      ")";

      public static void Insert(ContractStatus contractStatus)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@StatusName", contractStatus.StatusName);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          contractStatus.ContractStatusId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<ContractStatus>  contractStatusList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(ContractStatus contractStatus in  contractStatusList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@StatusName", contractStatus.StatusName);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@StatusName",contractStatus.StatusName);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        contractStatus.ContractStatusId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [ContractStatus] Set "
      
        + " StatusName = @StatusName "
      
        + " Where "
        
          + " ContractStatusId = @ContractStatusId "
        
      ;

      public static void Update(ContractStatus contractStatus)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@ContractStatusId", contractStatus.ContractStatusId);
      
        Database.PutParameter(dbCommand,"@StatusName", contractStatus.StatusName);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ContractStatusId, "
      
        + " StatusName "
      

      + " From [ContractStatus] "

      
        + " Where "
        
          + " ContractStatusId = @ContractStatusId "
        
      ;

      public static ContractStatus FindByPrimaryKey(
      int contractStatusId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ContractStatusId", contractStatusId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ContractStatus not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(ContractStatus contractStatus)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ContractStatusId",contractStatus.ContractStatusId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData()
      {
      String sql = "select 1 from ContractStatus";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static ContractStatus Load(IDataReader dataReader)
      {
      ContractStatus contractStatus = new ContractStatus();

      contractStatus.ContractStatusId = dataReader.GetInt32(0);
          contractStatus.StatusName = dataReader.GetString(1);
          

      return contractStatus;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [ContractStatus] "

      
        + " Where "
        
          + " ContractStatusId = @ContractStatusId "
        
      ;
      public static void Delete(ContractStatus contractStatus)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@ContractStatusId", contractStatus.ContractStatusId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [ContractStatus] ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " ContractStatusId, "
      
        + " StatusName "
      

      + " From [ContractStatus] ";
      public static List<ContractStatus> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<ContractStatus> rv = new List<ContractStatus>();

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
      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ContractStatus> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<ContractStatus> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ContractStatus));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ContractStatus item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ContractStatus>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ContractStatus));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ContractStatus> itemsList
      = new List<ContractStatus>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ContractStatus)
      itemsList.Add(deserializedObject as ContractStatus);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_contractStatusId;
      
        protected String m_statusName;
      
      #endregion

      #region Constructors
      public ContractStatus(
      int 
          contractStatusId
      )
      {
      
        m_contractStatusId = contractStatusId;
      
      }

      


        public ContractStatus(
        int 
          contractStatusId,String 
          statusName
        )
        {
        
          m_contractStatusId = contractStatusId;
        
          m_statusName = statusName;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ContractStatusId
        {
        get { return m_contractStatusId;}
        set { m_contractStatusId = value; }
        }
      
        [XmlElement]
        public String StatusName
        {
        get { return m_statusName;}
        set { m_statusName = value; }
        }
      
      }
      #endregion
      }

    