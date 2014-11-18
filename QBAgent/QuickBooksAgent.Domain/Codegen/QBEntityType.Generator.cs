
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class QBEntityType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [QBEntityType] ( " +
      
        " QBEntityTypeId, " +
        " QBEntityTypeDescription " +
        ") Values (" +
      
        " @QBEntityTypeId, " +
        " @QBEntityTypeDescription " +
      ")";

      public static void Insert(QBEntityType qBEntityType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@QBEntityTypeId", qBEntityType.QBEntityTypeId);            
          
              Database.PutParameter(dbCommand,"@QBEntityTypeDescription", qBEntityType.QBEntityTypeDescription);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<QBEntityType>  qBEntityTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(QBEntityType qBEntityType in  qBEntityTypeList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@QBEntityTypeId", qBEntityType.QBEntityTypeId);
          
            Database.PutParameter(dbCommand,"@QBEntityTypeDescription", qBEntityType.QBEntityTypeDescription);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@QBEntityTypeId",qBEntityType.QBEntityTypeId);
          
            Database.UpdateParameter(dbCommand,"@QBEntityTypeDescription",qBEntityType.QBEntityTypeDescription);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [QBEntityType] Set "
      
        + " QBEntityTypeDescription = @QBEntityTypeDescription "
        + " Where "
        
          + " QBEntityTypeId = @QBEntityTypeId "
        
      ;

      public static void Update(QBEntityType qBEntityType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@QBEntityTypeId", qBEntityType.QBEntityTypeId);            
          
            Database.PutParameter(dbCommand,"@QBEntityTypeDescription", qBEntityType.QBEntityTypeDescription);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " QBEntityTypeId, "
        + " QBEntityTypeDescription "
        + " From [QBEntityType] "
      
        + " Where "
        
        + " QBEntityTypeId = @QBEntityTypeId "
        
      ;

      public static QBEntityType FindByPrimaryKey(
      byte qBEntityTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@QBEntityTypeId", qBEntityTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("QBEntityType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(QBEntityType qBEntityType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@QBEntityTypeId",qBEntityType.QBEntityTypeId);
      

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
      String sql = "select 1 from [QBEntityType]";

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

      public static QBEntityType Load(IDataReader dataReader)
      {
      QBEntityType qBEntityType = new QBEntityType();

      qBEntityType.QBEntityTypeId = dataReader.GetByte(0);
          qBEntityType.QBEntityTypeDescription = dataReader.GetString(1);
          

      return qBEntityType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [QBEntityType] "

      
        + " Where "
        
          + " QBEntityTypeId = @QBEntityTypeId "
        
      ;
      public static void Delete(QBEntityType qBEntityType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@QBEntityTypeId", qBEntityType.QBEntityTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [QBEntityType] ";

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
      
        + " QBEntityTypeId, "
        + " QBEntityTypeDescription "
        + " From [QBEntityType] ";
      public static List<QBEntityType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<QBEntityType> rv = new List<QBEntityType>();

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
      List<QBEntityType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<QBEntityType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QBEntityType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QBEntityType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QBEntityType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QBEntityType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QBEntityType> itemsList
      = new List<QBEntityType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QBEntityType)
      itemsList.Add(deserializedObject as QBEntityType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region QBEntityTypeId
        protected byte m_qBEntityTypeId;

			[XmlAttribute]
			public byte QBEntityTypeId
			{
			get { return m_qBEntityTypeId;}
			set { m_qBEntityTypeId = value; }
			}
		#endregion
		
		#region QBEntityTypeDescription
        protected String m_qBEntityTypeDescription;

			[XmlAttribute]
			public String QBEntityTypeDescription
			{
			get { return m_qBEntityTypeDescription;}
			set { m_qBEntityTypeDescription = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public QBEntityType(
		byte qBEntityTypeId

		)
		{
		
			m_qBEntityTypeId = qBEntityTypeId;
		
        }

      


        public QBEntityType(
		  byte qBEntityTypeId,String qBEntityTypeDescription
		  )
		  {

		  
			  m_qBEntityTypeId = qBEntityTypeId;
		  
			  m_qBEntityTypeDescription = qBEntityTypeDescription;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    