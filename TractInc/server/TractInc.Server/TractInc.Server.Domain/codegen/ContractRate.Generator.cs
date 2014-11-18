
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


      public partial class ContractRate
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [ContractRate] ( " +
      
        " ContractId, " +
      
        " InvoiceItemTypeId, " +
      
        " Rate " +
      
      ") Values (" +
      
        " @ContractId, " +
      
        " @InvoiceItemTypeId, " +
      
        " @Rate " +
      
      ")";

      public static void Insert(ContractRate contractRate)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@ContractId", contractRate.ContractId);
      
        Database.PutParameter(dbCommand,"@InvoiceItemTypeId", contractRate.InvoiceItemTypeId);
      
        Database.PutParameter(dbCommand,"@Rate", contractRate.Rate);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          contractRate.ContractRateId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<ContractRate>  contractRateList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(ContractRate contractRate in  contractRateList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ContractId", contractRate.ContractId);
      
        Database.PutParameter(dbCommand,"@InvoiceItemTypeId", contractRate.InvoiceItemTypeId);
      
        Database.PutParameter(dbCommand,"@Rate", contractRate.Rate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ContractId",contractRate.ContractId);
      
        Database.UpdateParameter(dbCommand,"@InvoiceItemTypeId",contractRate.InvoiceItemTypeId);
      
        Database.UpdateParameter(dbCommand,"@Rate",contractRate.Rate);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        contractRate.ContractRateId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [ContractRate] Set "
      
        + " ContractId = @ContractId, "
      
        + " InvoiceItemTypeId = @InvoiceItemTypeId, "
      
        + " Rate = @Rate "
      
        + " Where "
        
          + " ContractRateId = @ContractRateId "
        
      ;

      public static void Update(ContractRate contractRate)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@ContractRateId", contractRate.ContractRateId);
      
        Database.PutParameter(dbCommand,"@ContractId", contractRate.ContractId);
      
        Database.PutParameter(dbCommand,"@InvoiceItemTypeId", contractRate.InvoiceItemTypeId);
      
        Database.PutParameter(dbCommand,"@Rate", contractRate.Rate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ContractRateId, "
      
        + " ContractId, "
      
        + " InvoiceItemTypeId, "
      
        + " Rate "
      

      + " From [ContractRate] "

      
        + " Where "
        
          + " ContractRateId = @ContractRateId "
        
      ;

      public static ContractRate FindByPrimaryKey(
      int contractRateId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ContractRateId", contractRateId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ContractRate not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(ContractRate contractRate)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ContractRateId",contractRate.ContractRateId);
      

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
      String sql = "select 1 from ContractRate";

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

      public static ContractRate Load(IDataReader dataReader)
      {
      ContractRate contractRate = new ContractRate();

      contractRate.ContractRateId = dataReader.GetInt32(0);
          contractRate.ContractId = dataReader.GetInt32(1);
          contractRate.InvoiceItemTypeId = dataReader.GetInt32(2);
          contractRate.Rate = dataReader.GetDecimal(3);
          

      return contractRate;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [ContractRate] "

      
        + " Where "
        
          + " ContractRateId = @ContractRateId "
        
      ;
      public static void Delete(ContractRate contractRate)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@ContractRateId", contractRate.ContractRateId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [ContractRate] ";

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

      
        + " ContractRateId, "
      
        + " ContractId, "
      
        + " InvoiceItemTypeId, "
      
        + " Rate "
      

      + " From [ContractRate] ";
      public static List<ContractRate> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<ContractRate> rv = new List<ContractRate>();

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
      List<ContractRate> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<ContractRate> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ContractRate));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ContractRate item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ContractRate>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ContractRate));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ContractRate> itemsList
      = new List<ContractRate>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ContractRate)
      itemsList.Add(deserializedObject as ContractRate);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_contractRateId;
      
        protected int m_contractId;
      
        protected int m_invoiceItemTypeId;
      
        protected decimal m_rate;
      
      #endregion

      #region Constructors
      public ContractRate(
      int 
          contractRateId
      )
      {
      
        m_contractRateId = contractRateId;
      
      }

      


        public ContractRate(
        int 
          contractRateId,int 
          contractId,int 
          invoiceItemTypeId,decimal 
          rate
        )
        {
        
          m_contractRateId = contractRateId;
        
          m_contractId = contractId;
        
          m_invoiceItemTypeId = invoiceItemTypeId;
        
          m_rate = rate;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ContractRateId
        {
        get { return m_contractRateId;}
        set { m_contractRateId = value; }
        }
      
        [XmlElement]
        public int ContractId
        {
        get { return m_contractId;}
        set { m_contractId = value; }
        }
      
        [XmlElement]
        public int InvoiceItemTypeId
        {
        get { return m_invoiceItemTypeId;}
        set { m_invoiceItemTypeId = value; }
        }
      
        [XmlElement]
        public decimal Rate
        {
        get { return m_rate;}
        set { m_rate = value; }
        }
      
      }
      #endregion
      }

    