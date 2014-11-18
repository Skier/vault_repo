
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class CreditCardType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [CreditCardType] ( " +
      
        " CreditCardTypeId, " +
        " CreditCardTypeDescription " +
        ") Values (" +
      
        " @CreditCardTypeId, " +
        " @CreditCardTypeDescription " +
      ")";

      public static void Insert(CreditCardType creditCardType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@CreditCardTypeId", creditCardType.CreditCardTypeId);            
          
              Database.PutParameter(dbCommand,"@CreditCardTypeDescription", creditCardType.CreditCardTypeDescription);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<CreditCardType>  creditCardTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(CreditCardType creditCardType in  creditCardTypeList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@CreditCardTypeId", creditCardType.CreditCardTypeId);
          
            Database.PutParameter(dbCommand,"@CreditCardTypeDescription", creditCardType.CreditCardTypeDescription);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@CreditCardTypeId",creditCardType.CreditCardTypeId);
          
            Database.UpdateParameter(dbCommand,"@CreditCardTypeDescription",creditCardType.CreditCardTypeDescription);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [CreditCardType] Set "
      
        + " CreditCardTypeDescription = @CreditCardTypeDescription "
        + " Where "
        
          + " CreditCardTypeId = @CreditCardTypeId "
        
      ;

      public static void Update(CreditCardType creditCardType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@CreditCardTypeId", creditCardType.CreditCardTypeId);            
          
            Database.PutParameter(dbCommand,"@CreditCardTypeDescription", creditCardType.CreditCardTypeDescription);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " CreditCardTypeId, "
        + " CreditCardTypeDescription "
        + " From [CreditCardType] "
      
        + " Where "
        
        + " CreditCardTypeId = @CreditCardTypeId "
        
      ;

      public static CreditCardType FindByPrimaryKey(
      int creditCardTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CreditCardTypeId", creditCardTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("CreditCardType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(CreditCardType creditCardType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CreditCardTypeId",creditCardType.CreditCardTypeId);
      

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
      String sql = "select 1 from [CreditCardType]";

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

      public static CreditCardType Load(IDataReader dataReader)
      {
      CreditCardType creditCardType = new CreditCardType();

      creditCardType.CreditCardTypeId = dataReader.GetInt32(0);
          creditCardType.CreditCardTypeDescription = dataReader.GetString(1);
          

      return creditCardType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [CreditCardType] "

      
        + " Where "
        
          + " CreditCardTypeId = @CreditCardTypeId "
        
      ;
      public static void Delete(CreditCardType creditCardType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@CreditCardTypeId", creditCardType.CreditCardTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [CreditCardType] ";

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
      
        + " CreditCardTypeId, "
        + " CreditCardTypeDescription "
        + " From [CreditCardType] ";
      public static List<CreditCardType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<CreditCardType> rv = new List<CreditCardType>();

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
      List<CreditCardType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<CreditCardType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CreditCardType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(CreditCardType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CreditCardType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CreditCardType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<CreditCardType> itemsList
      = new List<CreditCardType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CreditCardType)
      itemsList.Add(deserializedObject as CreditCardType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region CreditCardTypeId
        protected int m_creditCardTypeId;

			[XmlAttribute]
			public int CreditCardTypeId
			{
			get { return m_creditCardTypeId;}
			set { m_creditCardTypeId = value; }
			}
		#endregion
		
		#region CreditCardTypeDescription
        protected String m_creditCardTypeDescription;

			[XmlAttribute]
			public String CreditCardTypeDescription
			{
			get { return m_creditCardTypeDescription;}
			set { m_creditCardTypeDescription = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public CreditCardType(
		int creditCardTypeId

		)
		{
		
			m_creditCardTypeId = creditCardTypeId;
		
        }

      


        public CreditCardType(
		  int creditCardTypeId,String creditCardTypeDescription
		  )
		  {

		  
			  m_creditCardTypeId = creditCardTypeId;
		  
			  m_creditCardTypeDescription = creditCardTypeDescription;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    