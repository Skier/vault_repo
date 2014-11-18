
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class EntityState
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [EntityState] ( " +
      
        " EntityStateId, " +
        " Name " +
        ") Values (" +
      
        " @EntityStateId, " +
        " @Name " +
      ")";

      public static void Insert(EntityState entityState)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@EntityStateId", entityState.EntityStateId);            
          
              Database.PutParameter(dbCommand,"@Name", entityState.Name);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<EntityState>  entityStateList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(EntityState entityState in  entityStateList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@EntityStateId", entityState.EntityStateId);
          
            Database.PutParameter(dbCommand,"@Name", entityState.Name);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@EntityStateId",entityState.EntityStateId);
          
            Database.UpdateParameter(dbCommand,"@Name",entityState.Name);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [EntityState] Set "
      
        + " Name = @Name "
        + " Where "
        
          + " EntityStateId = @EntityStateId "
        
      ;

      public static void Update(EntityState entityState)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@EntityStateId", entityState.EntityStateId);            
          
            Database.PutParameter(dbCommand,"@Name", entityState.Name);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " EntityStateId, "
        + " Name "
        + " From [EntityState] "
      
        + " Where "
        
        + " EntityStateId = @EntityStateId "
        
      ;

      public static EntityState FindByPrimaryKey(
      int entityStateId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@EntityStateId", entityStateId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("EntityState not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(EntityState entityState)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@EntityStateId",entityState.EntityStateId);
      

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
      String sql = "select 1 from [EntityState]";

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

      public static EntityState Load(IDataReader dataReader)
      {
      EntityState entityState = new EntityState();

      entityState.EntityStateId = dataReader.GetInt32(0);
          entityState.Name = dataReader.GetString(1);
          

      return entityState;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [EntityState] "

      
        + " Where "
        
          + " EntityStateId = @EntityStateId "
        
      ;
      public static void Delete(EntityState entityState)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@EntityStateId", entityState.EntityStateId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [EntityState] ";

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
      
        + " EntityStateId, "
        + " Name "
        + " From [EntityState] ";
      public static List<EntityState> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<EntityState> rv = new List<EntityState>();

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
      List<EntityState> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<EntityState> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(EntityState));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(EntityState item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<EntityState>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(EntityState));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<EntityState> itemsList
      = new List<EntityState>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is EntityState)
      itemsList.Add(deserializedObject as EntityState);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region EntityStateId
        protected int m_entityStateId;

			[XmlAttribute]
			public int EntityStateId
			{
			get { return m_entityStateId;}
			set { m_entityStateId = value; }
			}
		#endregion
		
		#region Name
        protected String m_name;

			[XmlAttribute]
			public String Name
			{
			get { return m_name;}
			set { m_name = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public EntityState(
		int entityStateId

		)
		{
		
			m_entityStateId = entityStateId;
		
        }

      


        public EntityState(
		  int entityStateId,String name
		  )
		  {

		  
			  m_entityStateId = entityStateId;
		  
			  m_name = name;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    