
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class DetailAccountType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [DetailAccountType] ( " +
      
        " DetailAccountTypeId, " +
        " DetailAccountTypeDescription " +
        ") Values (" +
      
        " @DetailAccountTypeId, " +
        " @DetailAccountTypeDescription " +
      ")";

      public static void Insert(DetailAccountType detailAccountType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@DetailAccountTypeId", detailAccountType.DetailAccountTypeId);            
          
              Database.PutParameter(dbCommand,"@DetailAccountTypeDescription", detailAccountType.DetailAccountTypeDescription);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<DetailAccountType>  detailAccountTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(DetailAccountType detailAccountType in  detailAccountTypeList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@DetailAccountTypeId", detailAccountType.DetailAccountTypeId);
          
            Database.PutParameter(dbCommand,"@DetailAccountTypeDescription", detailAccountType.DetailAccountTypeDescription);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@DetailAccountTypeId",detailAccountType.DetailAccountTypeId);
          
            Database.UpdateParameter(dbCommand,"@DetailAccountTypeDescription",detailAccountType.DetailAccountTypeDescription);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [DetailAccountType] Set "
      
        + " DetailAccountTypeDescription = @DetailAccountTypeDescription "
        + " Where "
        
          + " DetailAccountTypeId = @DetailAccountTypeId "
        
      ;

      public static void Update(DetailAccountType detailAccountType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@DetailAccountTypeId", detailAccountType.DetailAccountTypeId);            
          
            Database.PutParameter(dbCommand,"@DetailAccountTypeDescription", detailAccountType.DetailAccountTypeDescription);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " DetailAccountTypeId, "
        + " DetailAccountTypeDescription "
        + " From [DetailAccountType] "
      
        + " Where "
        
        + " DetailAccountTypeId = @DetailAccountTypeId "
        
      ;

      public static DetailAccountType FindByPrimaryKey(
      int detailAccountTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@DetailAccountTypeId", detailAccountTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("DetailAccountType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(DetailAccountType detailAccountType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@DetailAccountTypeId",detailAccountType.DetailAccountTypeId);
      

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
      String sql = "select 1 from [DetailAccountType]";

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

      public static DetailAccountType Load(IDataReader dataReader)
      {
      DetailAccountType detailAccountType = new DetailAccountType();

      detailAccountType.DetailAccountTypeId = dataReader.GetInt32(0);
          detailAccountType.DetailAccountTypeDescription = dataReader.GetString(1);
          

      return detailAccountType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [DetailAccountType] "

      
        + " Where "
        
          + " DetailAccountTypeId = @DetailAccountTypeId "
        
      ;
      public static void Delete(DetailAccountType detailAccountType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@DetailAccountTypeId", detailAccountType.DetailAccountTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [DetailAccountType] ";

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
      
        + " DetailAccountTypeId, "
        + " DetailAccountTypeDescription "
        + " From [DetailAccountType] ";
      public static List<DetailAccountType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<DetailAccountType> rv = new List<DetailAccountType>();

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
      List<DetailAccountType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<DetailAccountType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(DetailAccountType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(DetailAccountType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<DetailAccountType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(DetailAccountType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<DetailAccountType> itemsList
      = new List<DetailAccountType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is DetailAccountType)
      itemsList.Add(deserializedObject as DetailAccountType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region DetailAccountTypeId
        protected int m_detailAccountTypeId;

			[XmlAttribute]
			public int DetailAccountTypeId
			{
			get { return m_detailAccountTypeId;}
			set { m_detailAccountTypeId = value; }
			}
		#endregion
		
		#region DetailAccountTypeDescription
        protected String m_detailAccountTypeDescription;

			[XmlAttribute]
			public String DetailAccountTypeDescription
			{
			get { return m_detailAccountTypeDescription;}
			set { m_detailAccountTypeDescription = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public DetailAccountType(
		int detailAccountTypeId

		)
		{
		
			m_detailAccountTypeId = detailAccountTypeId;
		
        }

      


        public DetailAccountType(
		  int detailAccountTypeId,String detailAccountTypeDescription
		  )
		  {

		  
			  m_detailAccountTypeId = detailAccountTypeId;
		  
			  m_detailAccountTypeDescription = detailAccountTypeDescription;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    