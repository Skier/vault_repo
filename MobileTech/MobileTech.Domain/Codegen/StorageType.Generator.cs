
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class StorageType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into StorageType ( " +
      
        " StorageTypeId, " +
      
        " Name " +
      
      ") Values (" +
      
        " @StorageTypeId, " +
      
        " @Name " +
      
      ")";

      public static void Insert(StorageType storageType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@StorageTypeId", storageType.StorageTypeId);
      
        Database.PutParameter(dbCommand,"@Name", storageType.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<StorageType>  storageTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(StorageType storageType in  storageTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@StorageTypeId", storageType.StorageTypeId);
      
        Database.PutParameter(dbCommand,"@Name", storageType.Name);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@StorageTypeId",storageType.StorageTypeId);
      
        Database.UpdateParameter(dbCommand,"@Name",storageType.Name);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update StorageType Set "
      
        + " Name = @Name "
      
        + " Where "
        
          + " StorageTypeId = @StorageTypeId "
        
      ;

      public static void Update(StorageType storageType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@StorageTypeId", storageType.StorageTypeId);
      
        Database.PutParameter(dbCommand,"@Name", storageType.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " StorageTypeId, "
      
        + " Name "
      

      + " From StorageType "

      
        + " Where "
        
          + " StorageTypeId = @StorageTypeId "
        
      ;

      public static StorageType FindByPrimaryKey(
      int storageTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@StorageTypeId", storageTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("StorageType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(StorageType storageType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@StorageTypeId",storageType.StorageTypeId);
      

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
      String sql = "select 1 from StorageType";

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

      public static StorageType Load(IDataReader dataReader)
      {
      StorageType storageType = new StorageType();

      storageType.StorageTypeId = dataReader.GetInt32(0);
          storageType.Name = dataReader.GetString(1);
          

      return storageType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From StorageType "

      
        + " Where "
        
          + " StorageTypeId = @StorageTypeId "
        
      ;
      public static void Delete(StorageType storageType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@StorageTypeId", storageType.StorageTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From StorageType ";

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

      
        + " StorageTypeId, "
      
        + " Name "
      

      + " From StorageType ";
      public static List<StorageType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<StorageType> rv = new List<StorageType>();

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
        List<StorageType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<StorageType> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(StorageType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(StorageType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<StorageType>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(StorageType));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<StorageType> itemsList
      = new List<StorageType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is StorageType)
        itemsList.Add(deserializedObject as StorageType);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_storageTypeId;
        
          protected String m_name;
        
        #endregion
        
        #region Constructors
        public StorageType(
        int 
          storageTypeId
         )
        {
        
          m_storageTypeId = storageTypeId;
        
        }
        
        


        public StorageType(
        int 
          storageTypeId,String 
          name
        )
        {
        
          m_storageTypeId = storageTypeId;
        
          m_name = name;
        
          }


        
      #endregion

      
        [XmlElement]
        public int StorageTypeId
        {
          get { return m_storageTypeId;}
          set { m_storageTypeId = value; }
        }
      
        [XmlElement]
        public String Name
        {
          get { return m_name;}
          set { m_name = value; }
        }
      
      }
      #endregion
      }

    