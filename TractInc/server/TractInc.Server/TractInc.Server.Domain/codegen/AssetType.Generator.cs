
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


      public partial class AssetType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [AssetType] ( " +
      
        " TypeName " +
      
      ") Values (" +
      
        " @TypeName " +
      
      ")";

      public static void Insert(AssetType assetType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@TypeName", assetType.TypeName);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          assetType.AssetTypeId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<AssetType>  assetTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(AssetType assetType in  assetTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@TypeName", assetType.TypeName);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@TypeName",assetType.TypeName);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        assetType.AssetTypeId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [AssetType] Set "
      
        + " TypeName = @TypeName "
      
        + " Where "
        
          + " AssetTypeId = @AssetTypeId "
        
      ;

      public static void Update(AssetType assetType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@AssetTypeId", assetType.AssetTypeId);
      
        Database.PutParameter(dbCommand,"@TypeName", assetType.TypeName);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " AssetTypeId, "
      
        + " TypeName "
      

      + " From [AssetType] "

      
        + " Where "
        
          + " AssetTypeId = @AssetTypeId "
        
      ;

      public static AssetType FindByPrimaryKey(
      int assetTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@AssetTypeId", assetTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("AssetType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(AssetType assetType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@AssetTypeId",assetType.AssetTypeId);
      

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
      String sql = "select 1 from AssetType";

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

      public static AssetType Load(IDataReader dataReader)
      {
      AssetType assetType = new AssetType();

      assetType.AssetTypeId = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            assetType.TypeName = dataReader.GetString(1);
          

      return assetType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [AssetType] "

      
        + " Where "
        
          + " AssetTypeId = @AssetTypeId "
        
      ;
      public static void Delete(AssetType assetType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@AssetTypeId", assetType.AssetTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [AssetType] ";

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

      
        + " AssetTypeId, "
      
        + " TypeName "
      

      + " From [AssetType] ";
      public static List<AssetType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<AssetType> rv = new List<AssetType>();

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
      List<AssetType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<AssetType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(AssetType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(AssetType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<AssetType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(AssetType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<AssetType> itemsList
      = new List<AssetType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is AssetType)
      itemsList.Add(deserializedObject as AssetType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_assetTypeId;
      
        protected String m_typeName;
      
      #endregion

      #region Constructors
      public AssetType(
      int 
          assetTypeId
      )
      {
      
        m_assetTypeId = assetTypeId;
      
      }

      


        public AssetType(
        int 
          assetTypeId,String 
          typeName
        )
        {
        
          m_assetTypeId = assetTypeId;
        
          m_typeName = typeName;
        
        }


      
      #endregion

      
        [XmlElement]
        public int AssetTypeId
        {
        get { return m_assetTypeId;}
        set { m_assetTypeId = value; }
        }
      
        [XmlElement]
        public String TypeName
        {
        get { return m_typeName;}
        set { m_typeName = value; }
        }
      
      }
      #endregion
      }

    