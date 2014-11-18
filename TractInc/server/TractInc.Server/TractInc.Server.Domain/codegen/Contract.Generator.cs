
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


      public partial class Contract
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Contract] ( " +
      
        " ClientId, " +
      
        " CompanyId, " +
      
        " ContractStatusId, " +
      
        " ContractName, " +
      
        " StartDate, " +
      
        " EndDate " +
      
      ") Values (" +
      
        " @ClientId, " +
      
        " @CompanyId, " +
      
        " @ContractStatusId, " +
      
        " @ContractName, " +
      
        " @StartDate, " +
      
        " @EndDate " +
      
      ")";

      public static void Insert(Contract contract)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@ClientId", contract.ClientId);
      
        Database.PutParameter(dbCommand,"@CompanyId", contract.CompanyId);
      
        Database.PutParameter(dbCommand,"@ContractStatusId", contract.ContractStatusId);
      
        Database.PutParameter(dbCommand,"@ContractName", contract.ContractName);
      
        Database.PutParameter(dbCommand,"@StartDate", contract.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", contract.EndDate);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          contract.ContractId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<Contract>  contractList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Contract contract in  contractList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ClientId", contract.ClientId);
      
        Database.PutParameter(dbCommand,"@CompanyId", contract.CompanyId);
      
        Database.PutParameter(dbCommand,"@ContractStatusId", contract.ContractStatusId);
      
        Database.PutParameter(dbCommand,"@ContractName", contract.ContractName);
      
        Database.PutParameter(dbCommand,"@StartDate", contract.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", contract.EndDate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ClientId",contract.ClientId);
      
        Database.UpdateParameter(dbCommand,"@CompanyId",contract.CompanyId);
      
        Database.UpdateParameter(dbCommand,"@ContractStatusId",contract.ContractStatusId);
      
        Database.UpdateParameter(dbCommand,"@ContractName",contract.ContractName);
      
        Database.UpdateParameter(dbCommand,"@StartDate",contract.StartDate);
      
        Database.UpdateParameter(dbCommand,"@EndDate",contract.EndDate);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        contract.ContractId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Contract] Set "
      
        + " ClientId = @ClientId, "
      
        + " CompanyId = @CompanyId, "
      
        + " ContractStatusId = @ContractStatusId, "
      
        + " ContractName = @ContractName, "
      
        + " StartDate = @StartDate, "
      
        + " EndDate = @EndDate "
      
        + " Where "
        
          + " ContractId = @ContractId "
        
      ;

      public static void Update(Contract contract)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@ContractId", contract.ContractId);
      
        Database.PutParameter(dbCommand,"@ClientId", contract.ClientId);
      
        Database.PutParameter(dbCommand,"@CompanyId", contract.CompanyId);
      
        Database.PutParameter(dbCommand,"@ContractStatusId", contract.ContractStatusId);
      
        Database.PutParameter(dbCommand,"@ContractName", contract.ContractName);
      
        Database.PutParameter(dbCommand,"@StartDate", contract.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", contract.EndDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ContractId, "
      
        + " ClientId, "
      
        + " CompanyId, "
      
        + " ContractStatusId, "
      
        + " ContractName, "
      
        + " StartDate, "
      
        + " EndDate "
      

      + " From [Contract] "

      
        + " Where "
        
          + " ContractId = @ContractId "
        
      ;

      public static Contract FindByPrimaryKey(
      int contractId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ContractId", contractId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Contract not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Contract contract)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ContractId",contract.ContractId);
      

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
      String sql = "select 1 from Contract";

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

      public static Contract Load(IDataReader dataReader)
      {
      Contract contract = new Contract();

      contract.ContractId = dataReader.GetInt32(0);
          contract.ClientId = dataReader.GetInt32(1);
          contract.CompanyId = dataReader.GetInt32(2);
          contract.ContractStatusId = dataReader.GetInt32(3);
          contract.ContractName = dataReader.GetString(4);
          
            if(!dataReader.IsDBNull(5))
            contract.StartDate = dataReader.GetDateTime(5);
          
            if(!dataReader.IsDBNull(6))
            contract.EndDate = dataReader.GetDateTime(6);
          

      return contract;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Contract] "

      
        + " Where "
        
          + " ContractId = @ContractId "
        
      ;
      public static void Delete(Contract contract)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@ContractId", contract.ContractId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Contract] ";

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

      
        + " ContractId, "
      
        + " ClientId, "
      
        + " CompanyId, "
      
        + " ContractStatusId, "
      
        + " ContractName, "
      
        + " StartDate, "
      
        + " EndDate "
      

      + " From [Contract] ";
      public static List<Contract> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Contract> rv = new List<Contract>();

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
      List<Contract> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Contract> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Contract));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Contract item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Contract>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Contract));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Contract> itemsList
      = new List<Contract>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Contract)
      itemsList.Add(deserializedObject as Contract);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_contractId;
      
        protected int m_clientId;
      
        protected int m_companyId;
      
        protected int m_contractStatusId;
      
        protected String m_contractName;
      
        protected DateTime? m_startDate;
      
        protected DateTime? m_endDate;
      
      #endregion

      #region Constructors
      public Contract(
      int 
          contractId
      )
      {
      
        m_contractId = contractId;
      
      }

      


        public Contract(
        int 
          contractId,int 
          clientId,int 
          companyId,int 
          contractStatusId,String 
          contractName,DateTime? 
          startDate,DateTime? 
          endDate
        )
        {
        
          m_contractId = contractId;
        
          m_clientId = clientId;
        
          m_companyId = companyId;
        
          m_contractStatusId = contractStatusId;
        
          m_contractName = contractName;
        
          m_startDate = startDate;
        
          m_endDate = endDate;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ContractId
        {
        get { return m_contractId;}
        set { m_contractId = value; }
        }
      
        [XmlElement]
        public int ClientId
        {
        get { return m_clientId;}
        set { m_clientId = value; }
        }
      
        [XmlElement]
        public int CompanyId
        {
        get { return m_companyId;}
        set { m_companyId = value; }
        }
      
        [XmlElement]
        public int ContractStatusId
        {
        get { return m_contractStatusId;}
        set { m_contractStatusId = value; }
        }
      
        [XmlElement]
        public String ContractName
        {
        get { return m_contractName;}
        set { m_contractName = value; }
        }
      
        [XmlElement]
        public DateTime? StartDate
        {
        get { return m_startDate;}
        set { m_startDate = value; }
        }
      
        [XmlElement]
        public DateTime? EndDate
        {
        get { return m_endDate;}
        set { m_endDate = value; }
        }
      
      }
      #endregion
      }

    