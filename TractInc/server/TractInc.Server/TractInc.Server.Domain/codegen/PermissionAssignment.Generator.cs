
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


      public partial class PermissionAssignment
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [PermissionAssignment] ( " +
      
        " PermissionId, " +
      
        " RoleId " +
      
      ") Values (" +
      
        " @PermissionId, " +
      
        " @RoleId " +
      
      ")";

      public static void Insert(PermissionAssignment permissionAssignment)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@PermissionId", permissionAssignment.PermissionId);
      
        Database.PutParameter(dbCommand,"@RoleId", permissionAssignment.RoleId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          permissionAssignment.PermissionAssignmentId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<PermissionAssignment>  permissionAssignmentList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(PermissionAssignment permissionAssignment in  permissionAssignmentList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@PermissionId", permissionAssignment.PermissionId);
      
        Database.PutParameter(dbCommand,"@RoleId", permissionAssignment.RoleId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@PermissionId",permissionAssignment.PermissionId);
      
        Database.UpdateParameter(dbCommand,"@RoleId",permissionAssignment.RoleId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        permissionAssignment.PermissionAssignmentId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [PermissionAssignment] Set "
      
        + " PermissionId = @PermissionId, "
      
        + " RoleId = @RoleId "
      
        + " Where "
        
          + " PermissionAssignmentId = @PermissionAssignmentId "
        
      ;

      public static void Update(PermissionAssignment permissionAssignment)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@PermissionAssignmentId", permissionAssignment.PermissionAssignmentId);
      
        Database.PutParameter(dbCommand,"@PermissionId", permissionAssignment.PermissionId);
      
        Database.PutParameter(dbCommand,"@RoleId", permissionAssignment.RoleId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " PermissionAssignmentId, "
      
        + " PermissionId, "
      
        + " RoleId "
      

      + " From [PermissionAssignment] "

      
        + " Where "
        
          + " PermissionAssignmentId = @PermissionAssignmentId "
        
      ;

      public static PermissionAssignment FindByPrimaryKey(
      int permissionAssignmentId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@PermissionAssignmentId", permissionAssignmentId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("PermissionAssignment not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(PermissionAssignment permissionAssignment)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@PermissionAssignmentId",permissionAssignment.PermissionAssignmentId);
      

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
      String sql = "select 1 from PermissionAssignment";

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

      public static PermissionAssignment Load(IDataReader dataReader)
      {
      PermissionAssignment permissionAssignment = new PermissionAssignment();

      permissionAssignment.PermissionAssignmentId = dataReader.GetInt32(0);
          permissionAssignment.PermissionId = dataReader.GetInt32(1);
          permissionAssignment.RoleId = dataReader.GetInt32(2);
          

      return permissionAssignment;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [PermissionAssignment] "

      
        + " Where "
        
          + " PermissionAssignmentId = @PermissionAssignmentId "
        
      ;
      public static void Delete(PermissionAssignment permissionAssignment)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@PermissionAssignmentId", permissionAssignment.PermissionAssignmentId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [PermissionAssignment] ";

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

      
        + " PermissionAssignmentId, "
      
        + " PermissionId, "
      
        + " RoleId "
      

      + " From [PermissionAssignment] ";
      public static List<PermissionAssignment> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<PermissionAssignment> rv = new List<PermissionAssignment>();

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
      List<PermissionAssignment> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<PermissionAssignment> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PermissionAssignment));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(PermissionAssignment item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<PermissionAssignment>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PermissionAssignment));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<PermissionAssignment> itemsList
      = new List<PermissionAssignment>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is PermissionAssignment)
      itemsList.Add(deserializedObject as PermissionAssignment);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_permissionAssignmentId;
      
        protected int m_permissionId;
      
        protected int m_roleId;
      
      #endregion

      #region Constructors
      public PermissionAssignment(
      int 
          permissionAssignmentId
      )
      {
      
        m_permissionAssignmentId = permissionAssignmentId;
      
      }

      


        public PermissionAssignment(
        int 
          permissionAssignmentId,int 
          permissionId,int 
          roleId
        )
        {
        
          m_permissionAssignmentId = permissionAssignmentId;
        
          m_permissionId = permissionId;
        
          m_roleId = roleId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int PermissionAssignmentId
        {
        get { return m_permissionAssignmentId;}
        set { m_permissionAssignmentId = value; }
        }
      
        [XmlElement]
        public int PermissionId
        {
        get { return m_permissionId;}
        set { m_permissionId = value; }
        }
      
        [XmlElement]
        public int RoleId
        {
        get { return m_roleId;}
        set { m_roleId = value; }
        }
      
      }
      #endregion
      }

    