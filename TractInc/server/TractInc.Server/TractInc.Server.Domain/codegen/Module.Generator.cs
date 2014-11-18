
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


      public partial class Module
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Module] ( " +
      
        " ShortName, " +
      
        " Description, " +
      
        " Url, " +
      
        " ModuleTypeId " +
      
      ") Values (" +
      
        " @ShortName, " +
      
        " @Description, " +
      
        " @Url, " +
      
        " @ModuleTypeId " +
      
      ")";

      public static void Insert(Module module)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@ShortName", module.ShortName);
      
        Database.PutParameter(dbCommand,"@Description", module.Description);
      
        Database.PutParameter(dbCommand,"@Url", module.Url);
      
        Database.PutParameter(dbCommand,"@ModuleTypeId", module.ModuleTypeId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          module.ModuleId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<Module>  moduleList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Module module in  moduleList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ShortName", module.ShortName);
      
        Database.PutParameter(dbCommand,"@Description", module.Description);
      
        Database.PutParameter(dbCommand,"@Url", module.Url);
      
        Database.PutParameter(dbCommand,"@ModuleTypeId", module.ModuleTypeId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ShortName",module.ShortName);
      
        Database.UpdateParameter(dbCommand,"@Description",module.Description);
      
        Database.UpdateParameter(dbCommand,"@Url",module.Url);
      
        Database.UpdateParameter(dbCommand,"@ModuleTypeId",module.ModuleTypeId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        module.ModuleId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Module] Set "
      
        + " ShortName = @ShortName, "
      
        + " Description = @Description, "
      
        + " Url = @Url, "
      
        + " ModuleTypeId = @ModuleTypeId "
      
        + " Where "
        
          + " ModuleId = @ModuleId "
        
      ;

      public static void Update(Module module)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@ModuleId", module.ModuleId);
      
        Database.PutParameter(dbCommand,"@ShortName", module.ShortName);
      
        Database.PutParameter(dbCommand,"@Description", module.Description);
      
        Database.PutParameter(dbCommand,"@Url", module.Url);
      
        Database.PutParameter(dbCommand,"@ModuleTypeId", module.ModuleTypeId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ModuleId, "
      
        + " ShortName, "
      
        + " Description, "
      
        + " Url, "
      
        + " ModuleTypeId "
      

      + " From [Module] "

      
        + " Where "
        
          + " ModuleId = @ModuleId "
        
      ;

      public static Module FindByPrimaryKey(
      int moduleId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ModuleId", moduleId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Module not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Module module)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ModuleId",module.ModuleId);
      

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
      String sql = "select 1 from Module";

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

      public static Module Load(IDataReader dataReader)
      {
      Module module = new Module();

      module.ModuleId = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            module.ShortName = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            module.Description = dataReader.GetString(2);
          module.Url = dataReader.GetString(3);
          module.ModuleTypeId = dataReader.GetInt32(4);
          

      return module;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Module] "

      
        + " Where "
        
          + " ModuleId = @ModuleId "
        
      ;
      public static void Delete(Module module)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@ModuleId", module.ModuleId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Module] ";

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

      
        + " ModuleId, "
      
        + " ShortName, "
      
        + " Description, "
      
        + " Url, "
      
        + " ModuleTypeId "
      

      + " From [Module] ";
      public static List<Module> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Module> rv = new List<Module>();

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
      List<Module> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Module> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Module));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Module item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Module>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Module));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Module> itemsList
      = new List<Module>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Module)
      itemsList.Add(deserializedObject as Module);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_moduleId;
      
        protected String m_shortName;
      
        protected String m_description;
      
        protected String m_url;
      
        protected int m_moduleTypeId;
      
      #endregion

      #region Constructors
      public Module(
      int 
          moduleId
      )
      {
      
        m_moduleId = moduleId;
      
      }

      


        public Module(
        int 
          moduleId,String 
          shortName,String 
          description,String 
          url,int 
          moduleTypeId
        )
        {
        
          m_moduleId = moduleId;
        
          m_shortName = shortName;
        
          m_description = description;
        
          m_url = url;
        
          m_moduleTypeId = moduleTypeId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ModuleId
        {
        get { return m_moduleId;}
        set { m_moduleId = value; }
        }
      
        [XmlElement]
        public String ShortName
        {
        get { return m_shortName;}
        set { m_shortName = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
        [XmlElement]
        public String Url
        {
        get { return m_url;}
        set { m_url = value; }
        }
      
        [XmlElement]
        public int ModuleTypeId
        {
        get { return m_moduleTypeId;}
        set { m_moduleTypeId = value; }
        }
      
      }
      #endregion
      }

    