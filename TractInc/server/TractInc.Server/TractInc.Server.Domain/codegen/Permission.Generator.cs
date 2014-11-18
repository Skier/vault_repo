
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


      public partial class Permission
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Permission] ( " +
      
        " ModuleId, " +
      
        " Description, " +
      
        " Code " +
      
      ") Values (" +
      
        " @ModuleId, " +
      
        " @Description, " +
      
        " @Code " +
      
      ")";

      public static void Insert(Permission permission)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@ModuleId", permission.ModuleId);
      
        Database.PutParameter(dbCommand,"@Description", permission.Description);
      
        Database.PutParameter(dbCommand,"@Code", permission.Code);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          permission.PermissionId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<Permission>  permissionList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Permission permission in  permissionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ModuleId", permission.ModuleId);
      
        Database.PutParameter(dbCommand,"@Description", permission.Description);
      
        Database.PutParameter(dbCommand,"@Code", permission.Code);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ModuleId",permission.ModuleId);
      
        Database.UpdateParameter(dbCommand,"@Description",permission.Description);
      
        Database.UpdateParameter(dbCommand,"@Code",permission.Code);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        permission.PermissionId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Permission] Set "
      
        + " ModuleId = @ModuleId, "
      
        + " Description = @Description, "
      
        + " Code = @Code "
      
        + " Where "
        
          + " PermissionId = @PermissionId "
        
      ;

      public static void Update(Permission permission)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@PermissionId", permission.PermissionId);
      
        Database.PutParameter(dbCommand,"@ModuleId", permission.ModuleId);
      
        Database.PutParameter(dbCommand,"@Description", permission.Description);
      
        Database.PutParameter(dbCommand,"@Code", permission.Code);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " PermissionId, "
      
        + " ModuleId, "
      
        + " Description, "
      
        + " Code "
      

      + " From [Permission] "

      
        + " Where "
        
          + " PermissionId = @PermissionId "
        
      ;

      public static Permission FindByPrimaryKey(
      int permissionId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@PermissionId", permissionId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Permission not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Permission permission)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@PermissionId",permission.PermissionId);
      

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
      String sql = "select 1 from Permission";

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

      public static Permission Load(IDataReader dataReader)
      {
      Permission permission = new Permission();

      permission.PermissionId = dataReader.GetInt32(0);
          permission.ModuleId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            permission.Description = dataReader.GetString(2);
          permission.Code = dataReader.GetString(3);
          

      return permission;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Permission] "

      
        + " Where "
        
          + " PermissionId = @PermissionId "
        
      ;
      public static void Delete(Permission permission)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@PermissionId", permission.PermissionId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Permission] ";

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

      
        + " PermissionId, "
      
        + " ModuleId, "
      
        + " Description, "
      
        + " Code "
      

      + " From [Permission] ";
      public static List<Permission> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Permission> rv = new List<Permission>();

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
      List<Permission> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Permission> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Permission));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Permission item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Permission>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Permission));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Permission> itemsList
      = new List<Permission>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Permission)
      itemsList.Add(deserializedObject as Permission);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_permissionId;
      
        protected int m_moduleId;
      
        protected String m_description;
      
        protected String m_code;
      
      #endregion

      #region Constructors
      public Permission(
      int 
          permissionId
      )
      {
      
        m_permissionId = permissionId;
      
      }

      


        public Permission(
        int 
          permissionId,int 
          moduleId,String 
          description,String 
          code
        )
        {
        
          m_permissionId = permissionId;
        
          m_moduleId = moduleId;
        
          m_description = description;
        
          m_code = code;
        
        }


      
      #endregion

      
        [XmlElement]
        public int PermissionId
        {
        get { return m_permissionId;}
        set { m_permissionId = value; }
        }
      
        [XmlElement]
        public int ModuleId
        {
        get { return m_moduleId;}
        set { m_moduleId = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
        [XmlElement]
        public String Code
        {
        get { return m_code;}
        set { m_code = value; }
        }
      
      }
      #endregion
      }

    