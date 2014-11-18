
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class QBEntity
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [QBEntity] ( " +
      
        " QBEntityId, " +
        " QBEntityTypeId " +
        ") Values (" +
      
        " @QBEntityId, " +
        " @QBEntityTypeId " +
      ")";

      public static void Insert(QBEntity qBEntity)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@QBEntityId", qBEntity.QBEntityId);            
          
              Database.PutParameter(dbCommand,"@QBEntityTypeId", qBEntity
			.QBEntityType.QBEntityTypeId);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<QBEntity>  qBEntityList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(QBEntity qBEntity in  qBEntityList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@QBEntityId", qBEntity.QBEntityId);
          
            Database.PutParameter(dbCommand,"@QBEntityTypeId", qBEntity
			.QBEntityType.QBEntityTypeId);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@QBEntityId",qBEntity.QBEntityId);
          
            Database.UpdateParameter(dbCommand,"@QBEntityTypeId",qBEntity
			.QBEntityType.QBEntityTypeId);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [QBEntity] Set "
      
        + " QBEntityTypeId = @QBEntityTypeId "
        + " Where "
        
          + " QBEntityId = @QBEntityId "
        
      ;

      public static void Update(QBEntity qBEntity)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@QBEntityId", qBEntity.QBEntityId);            
          
            Database.PutParameter(dbCommand,"@QBEntityTypeId", qBEntity
			.QBEntityType.QBEntityTypeId);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " QBEntityId, "
        + " QBEntityTypeId "
        + " From [QBEntity] "
      
        + " Where "
        
        + " QBEntityId = @QBEntityId "
        
      ;

      public static QBEntity FindByPrimaryKey(
      int qBEntityId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@QBEntityId", qBEntityId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("QBEntity not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(QBEntity qBEntity)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@QBEntityId",qBEntity.QBEntityId);
      

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
      String sql = "select 1 from [QBEntity]";

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

      public static QBEntity Load(IDataReader dataReader)
      {
      QBEntity qBEntity = new QBEntity();

      qBEntity.QBEntityId = dataReader.GetInt32(0);
          qBEntity
			.QBEntityType = new QBEntityType();

            qBEntity
			.QBEntityType.QBEntityTypeId = dataReader.GetByte(1);
          

      return qBEntity;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [QBEntity] "

      
        + " Where "
        
          + " QBEntityId = @QBEntityId "
        
      ;
      public static void Delete(QBEntity qBEntity)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@QBEntityId", qBEntity.QBEntityId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [QBEntity] ";

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
      
        + " QBEntityId, "
        + " QBEntityTypeId "
        + " From [QBEntity] ";
      public static List<QBEntity> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<QBEntity> rv = new List<QBEntity>();

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
      List<QBEntity> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<QBEntity> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QBEntity));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QBEntity item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QBEntity>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QBEntity));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QBEntity> itemsList
      = new List<QBEntity>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QBEntity)
      itemsList.Add(deserializedObject as QBEntity);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region QBEntityId
        protected int m_qBEntityId;

			[XmlAttribute]
			public int QBEntityId
			{
			get { return m_qBEntityId;}
			set { m_qBEntityId = value; }
			}
		#endregion
		
		#region QBEntityType
			protected QBEntityType m_qBEntityType;

			[XmlElement]
			public QBEntityType QBEntityType
			{
			get { return m_qBEntityType;}
			set { m_qBEntityType = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public QBEntity(
		int qBEntityId

		)
		{
		
			m_qBEntityId = qBEntityId;
		
        }

      


        public QBEntity(
		  QBEntityType qBEntityType
			  ,
		  int qBEntityId
		  )
		  {

		  
			  m_qBEntityType = qBEntityType;
		  
			  m_qBEntityId = qBEntityId;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    