
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


      public partial class AssetRate
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [AssetRate] ( " +
      
        " AssetId, " +
      
        " BillItemTypeId, " +
      
        " ContractId, " +
      
        " Rate " +
      
      ") Values (" +
      
        " @AssetId, " +
      
        " @BillItemTypeId, " +
      
        " @ContractId, " +
      
        " @Rate " +
      
      ")";

      public static void Insert(AssetRate assetRate)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@AssetId", assetRate.AssetId);
      
        Database.PutParameter(dbCommand,"@BillItemTypeId", assetRate.BillItemTypeId);
      
          Database.PutParameter(dbCommand,"@ContractId", 0 != assetRate.ContractId ? assetRate.ContractId : null);
      
        Database.PutParameter(dbCommand,"@Rate", assetRate.Rate);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          assetRate.AssetRateId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<AssetRate>  assetRateList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(AssetRate assetRate in  assetRateList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@AssetId", assetRate.AssetId);
      
        Database.PutParameter(dbCommand,"@BillItemTypeId", assetRate.BillItemTypeId);
      
        Database.PutParameter(dbCommand,"@ContractId", assetRate.ContractId);
      
        Database.PutParameter(dbCommand,"@Rate", assetRate.Rate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@AssetId",assetRate.AssetId);
      
        Database.UpdateParameter(dbCommand,"@BillItemTypeId",assetRate.BillItemTypeId);
      
        Database.UpdateParameter(dbCommand,"@ContractId",assetRate.ContractId);
      
        Database.UpdateParameter(dbCommand,"@Rate",assetRate.Rate);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        assetRate.AssetRateId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [AssetRate] Set "
      
        + " AssetId = @AssetId, "
      
        + " BillItemTypeId = @BillItemTypeId, "
      
        + " ContractId = @ContractId, "
      
        + " Rate = @Rate "
      
        + " Where "
        
          + " AssetRateId = @AssetRateId "
        
      ;

      public static void Update(AssetRate assetRate)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@AssetRateId", assetRate.AssetRateId);
      
        Database.PutParameter(dbCommand,"@AssetId", assetRate.AssetId);
      
        Database.PutParameter(dbCommand,"@BillItemTypeId", assetRate.BillItemTypeId);
      
        Database.PutParameter(dbCommand,"@ContractId", 0 != assetRate.ContractId ? assetRate.ContractId : null);
      
        Database.PutParameter(dbCommand,"@Rate", assetRate.Rate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " AssetRateId, "
      
        + " AssetId, "
      
        + " BillItemTypeId, "
      
        + " ContractId, "
      
        + " Rate "
      

      + " From [AssetRate] "

      
        + " Where "
        
          + " AssetRateId = @AssetRateId "
        
      ;

      public static AssetRate FindByPrimaryKey(
      int assetRateId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@AssetRateId", assetRateId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("AssetRate not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(AssetRate assetRate)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@AssetRateId",assetRate.AssetRateId);
      

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
      String sql = "select 1 from AssetRate";

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

      public static AssetRate Load(IDataReader dataReader)
      {
      AssetRate assetRate = new AssetRate();

      assetRate.AssetRateId = dataReader.GetInt32(0);
          assetRate.AssetId = dataReader.GetInt32(1);
          assetRate.BillItemTypeId = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            assetRate.ContractId = dataReader.GetInt32(3);
          assetRate.Rate = dataReader.GetDecimal(4);
          

      return assetRate;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [AssetRate] "

      
        + " Where "
        
          + " AssetRateId = @AssetRateId "
        
      ;
      public static void Delete(AssetRate assetRate)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@AssetRateId", assetRate.AssetRateId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [AssetRate] ";

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

      
        + " AssetRateId, "
      
        + " AssetId, "
      
        + " BillItemTypeId, "
      
        + " ContractId, "
      
        + " Rate "
      

      + " From [AssetRate] ";
      public static List<AssetRate> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<AssetRate> rv = new List<AssetRate>();

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
      List<AssetRate> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<AssetRate> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(AssetRate));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(AssetRate item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<AssetRate>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(AssetRate));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<AssetRate> itemsList
      = new List<AssetRate>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is AssetRate)
      itemsList.Add(deserializedObject as AssetRate);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_assetRateId;
      
        protected int m_assetId;
      
        protected int m_billItemTypeId;
      
        protected int? m_contractId;
      
        protected decimal m_rate;
      
      #endregion

      #region Constructors
      public AssetRate(
      int 
          assetRateId
      )
      {
      
        m_assetRateId = assetRateId;
      
      }

      


        public AssetRate(
        int 
          assetRateId,int 
          assetId,int 
          billItemTypeId,int? 
          contractId,decimal 
          rate
        )
        {
        
          m_assetRateId = assetRateId;
        
          m_assetId = assetId;
        
          m_billItemTypeId = billItemTypeId;
        
          m_contractId = contractId;
        
          m_rate = rate;
        
        }


      
      #endregion

      
        [XmlElement]
        public int AssetRateId
        {
        get { return m_assetRateId;}
        set { m_assetRateId = value; }
        }
      
        [XmlElement]
        public int AssetId
        {
        get { return m_assetId;}
        set { m_assetId = value; }
        }
      
        [XmlElement]
        public int BillItemTypeId
        {
        get { return m_billItemTypeId;}
        set { m_billItemTypeId = value; }
        }
      
        [XmlElement]
        public int? ContractId
        {
        get { return m_contractId;}
        set { m_contractId = value; }
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

    