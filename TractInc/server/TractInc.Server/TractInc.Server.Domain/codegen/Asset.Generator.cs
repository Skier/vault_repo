
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


      public partial class Asset
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Asset] ( " +
      
        " AssetTypeId, " +
      
        " CompanyId, " +
      
        " PersonId, " +
      
        " AssetName " +
      
      ") Values (" +
      
        " @AssetTypeId, " +
      
        " @CompanyId, " +
      
        " @PersonId, " +
      
        " @AssetName " +
      
      ")";

      public static void Insert(Asset asset)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@AssetTypeId", asset.AssetTypeId);
      
        Database.PutParameter(dbCommand,"@CompanyId", asset.CompanyId);
      
          Database.PutParameter(dbCommand,"@PersonId", 0 != asset.PersonId ? asset.PersonId : null);
      
        Database.PutParameter(dbCommand,"@AssetName", asset.AssetName);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          asset.AssetId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<Asset>  assetList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Asset asset in  assetList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@AssetTypeId", asset.AssetTypeId);
      
        Database.PutParameter(dbCommand,"@CompanyId", asset.CompanyId);
      
        Database.PutParameter(dbCommand,"@PersonId", asset.PersonId);
      
        Database.PutParameter(dbCommand,"@AssetName", asset.AssetName);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@AssetTypeId",asset.AssetTypeId);
      
        Database.UpdateParameter(dbCommand,"@CompanyId",asset.CompanyId);
      
        Database.UpdateParameter(dbCommand,"@PersonId",asset.PersonId);
      
        Database.UpdateParameter(dbCommand,"@AssetName",asset.AssetName);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        asset.AssetId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Asset] Set "
      
        + " AssetTypeId = @AssetTypeId, "
      
        + " CompanyId = @CompanyId, "
      
        + " PersonId = @PersonId, "
      
        + " AssetName = @AssetName "
      
        + " Where "
        
          + " AssetId = @AssetId "
        
      ;

      public static void Update(Asset asset)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@AssetId", asset.AssetId);
      
        Database.PutParameter(dbCommand,"@AssetTypeId", asset.AssetTypeId);
      
        Database.PutParameter(dbCommand,"@CompanyId", asset.CompanyId);
      
        Database.PutParameter(dbCommand,"@PersonId", 0 != asset.PersonId ? asset.PersonId : null);
      
        Database.PutParameter(dbCommand,"@AssetName", asset.AssetName);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " AssetId, "
      
        + " AssetTypeId, "
      
        + " CompanyId, "
      
        + " PersonId, "
      
        + " AssetName "
      

      + " From [Asset] "

      
        + " Where "
        
          + " AssetId = @AssetId "
        
      ;

      public static Asset FindByPrimaryKey(
      int assetId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@AssetId", assetId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Asset not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Asset asset)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@AssetId",asset.AssetId);
      

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
      String sql = "select 1 from Asset";

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

      public static Asset Load(IDataReader dataReader)
      {
      Asset asset = new Asset();

      asset.AssetId = dataReader.GetInt32(0);
          asset.AssetTypeId = dataReader.GetInt32(1);
          asset.CompanyId = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            asset.PersonId = dataReader.GetInt32(3);
          asset.AssetName = dataReader.GetString(4);
          

      return asset;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Asset] "

      
        + " Where "
        
          + " AssetId = @AssetId "
        
      ;
      public static void Delete(Asset asset)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@AssetId", asset.AssetId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Asset] ";

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

      
        + " AssetId, "
      
        + " AssetTypeId, "
      
        + " CompanyId, "
      
        + " PersonId, "
      
        + " AssetName "
      

      + " From [Asset] ";
      public static List<Asset> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Asset> rv = new List<Asset>();

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
      List<Asset> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Asset> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Asset));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Asset item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Asset>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Asset));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Asset> itemsList
      = new List<Asset>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Asset)
      itemsList.Add(deserializedObject as Asset);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_assetId;
      
        protected int m_assetTypeId;
      
        protected int m_companyId;
      
        protected int? m_personId;
      
        protected String m_assetName;
      
      #endregion

      #region Constructors
      public Asset(
      int 
          assetId
      )
      {
      
        m_assetId = assetId;
      
      }

      


        public Asset(
        int 
          assetId,int 
          assetTypeId,int 
          companyId,int? 
          personId,String 
          assetName
        )
        {
        
          m_assetId = assetId;
        
          m_assetTypeId = assetTypeId;
        
          m_companyId = companyId;
        
          m_personId = personId;
        
          m_assetName = assetName;
        
        }


      
      #endregion

      
        [XmlElement]
        public int AssetId
        {
        get { return m_assetId;}
        set { m_assetId = value; }
        }
      
        [XmlElement]
        public int AssetTypeId
        {
        get { return m_assetTypeId;}
        set { m_assetTypeId = value; }
        }
      
        [XmlElement]
        public int CompanyId
        {
        get { return m_companyId;}
        set { m_companyId = value; }
        }
      
        [XmlElement]
        public int? PersonId
        {
        get { return m_personId;}
        set { m_personId = value; }
        }
      
        [XmlElement]
        public String AssetName
        {
        get { return m_assetName;}
        set { m_assetName = value; }
        }
      
      }
      #endregion
      }

    