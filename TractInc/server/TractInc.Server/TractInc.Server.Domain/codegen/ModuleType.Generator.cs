
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


      public partial class ModuleType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [ModuleType] ( " +
      
        " TypeName " +
      
      ") Values (" +
      
        " @TypeName " +
      
      ")";

      public static void Insert(ModuleType moduleType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@TypeName", moduleType.TypeName);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          moduleType.ModuleTypeId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<ModuleType>  moduleTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(ModuleType moduleType in  moduleTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@TypeName", moduleType.TypeName);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@TypeName",moduleType.TypeName);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        moduleType.ModuleTypeId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [ModuleType] Set "
      
        + " TypeName = @TypeName "
      
        + " Where "
        
          + " ModuleTypeId = @ModuleTypeId "
        
      ;

      public static void Update(ModuleType moduleType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@ModuleTypeId", moduleType.ModuleTypeId);
      
        Database.PutParameter(dbCommand,"@TypeName", moduleType.TypeName);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ModuleTypeId, "
      
        + " TypeName "
      

      + " From [ModuleType] "

      
        + " Where "
        
          + " ModuleTypeId = @ModuleTypeId "
        
      ;

      public static ModuleType FindByPrimaryKey(
      int moduleTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ModuleTypeId", moduleTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ModuleType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(ModuleType moduleType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ModuleTypeId",moduleType.ModuleTypeId);
      

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
      String sql = "select 1 from ModuleType";

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

      public static ModuleType Load(IDataReader dataReader)
      {
      ModuleType moduleType = new ModuleType();

      moduleType.ModuleTypeId = dataReader.GetInt32(0);
          moduleType.TypeName = dataReader.GetString(1);
          

      return moduleType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [ModuleType] "

      
        + " Where "
        
          + " ModuleTypeId = @ModuleTypeId "
        
      ;
      public static void Delete(ModuleType moduleType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@ModuleTypeId", moduleType.ModuleTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [ModuleType] ";

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

      
        + " ModuleTypeId, "
      
        + " TypeName "
      

      + " From [ModuleType] ";
      public static List<ModuleType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<ModuleType> rv = new List<ModuleType>();

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
      List<ModuleType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<ModuleType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ModuleType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ModuleType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ModuleType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ModuleType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ModuleType> itemsList
      = new List<ModuleType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ModuleType)
      itemsList.Add(deserializedObject as ModuleType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_moduleTypeId;
      
        protected String m_typeName;
      
      #endregion

      #region Constructors
      public ModuleType(
      int 
          moduleTypeId
      )
      {
      
        m_moduleTypeId = moduleTypeId;
      
      }

      


        public ModuleType(
        int 
          moduleTypeId,String 
          typeName
        )
        {
        
          m_moduleTypeId = moduleTypeId;
        
          m_typeName = typeName;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ModuleTypeId
        {
        get { return m_moduleTypeId;}
        set { m_moduleTypeId = value; }
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

    