
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class TransactionType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [TransactionType] ( " +
      
        " TransactionTypeId, " +
        " TransactionTypeDescription " +
        ") Values (" +
      
        " @TransactionTypeId, " +
        " @TransactionTypeDescription " +
      ")";

      public static void Insert(TransactionType transactionType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@TransactionTypeId", transactionType.TransactionTypeId);            
          
              Database.PutParameter(dbCommand,"@TransactionTypeDescription", transactionType.TransactionTypeDescription);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<TransactionType>  transactionTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(TransactionType transactionType in  transactionTypeList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@TransactionTypeId", transactionType.TransactionTypeId);
          
            Database.PutParameter(dbCommand,"@TransactionTypeDescription", transactionType.TransactionTypeDescription);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@TransactionTypeId",transactionType.TransactionTypeId);
          
            Database.UpdateParameter(dbCommand,"@TransactionTypeDescription",transactionType.TransactionTypeDescription);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [TransactionType] Set "
      
        + " TransactionTypeDescription = @TransactionTypeDescription "
        + " Where "
        
          + " TransactionTypeId = @TransactionTypeId "
        
      ;

      public static void Update(TransactionType transactionType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@TransactionTypeId", transactionType.TransactionTypeId);            
          
            Database.PutParameter(dbCommand,"@TransactionTypeDescription", transactionType.TransactionTypeDescription);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " TransactionTypeId, "
        + " TransactionTypeDescription "
        + " From [TransactionType] "
      
        + " Where "
        
        + " TransactionTypeId = @TransactionTypeId "
        
      ;

      public static TransactionType FindByPrimaryKey(
      int transactionTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@TransactionTypeId", transactionTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("TransactionType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(TransactionType transactionType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@TransactionTypeId",transactionType.TransactionTypeId);
      

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
      String sql = "select 1 from [TransactionType]";

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

      public static TransactionType Load(IDataReader dataReader)
      {
      TransactionType transactionType = new TransactionType();

      transactionType.TransactionTypeId = dataReader.GetInt32(0);
          transactionType.TransactionTypeDescription = dataReader.GetString(1);
          

      return transactionType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [TransactionType] "

      
        + " Where "
        
          + " TransactionTypeId = @TransactionTypeId "
        
      ;
      public static void Delete(TransactionType transactionType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@TransactionTypeId", transactionType.TransactionTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [TransactionType] ";

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
      
        + " TransactionTypeId, "
        + " TransactionTypeDescription "
        + " From [TransactionType] ";
      public static List<TransactionType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<TransactionType> rv = new List<TransactionType>();

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
      List<TransactionType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TransactionType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TransactionType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TransactionType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TransactionType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TransactionType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TransactionType> itemsList
      = new List<TransactionType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TransactionType)
      itemsList.Add(deserializedObject as TransactionType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region TransactionTypeId
        protected int m_transactionTypeId;

			[XmlAttribute]
			public int TransactionTypeId
			{
			get { return m_transactionTypeId;}
			set { m_transactionTypeId = value; }
			}
		#endregion
		
		#region TransactionTypeDescription
        protected String m_transactionTypeDescription;

			[XmlAttribute]
			public String TransactionTypeDescription
			{
			get { return m_transactionTypeDescription;}
			set { m_transactionTypeDescription = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public TransactionType(
		int transactionTypeId

		)
		{
		
			m_transactionTypeId = transactionTypeId;
		
        }

      


        public TransactionType(
		  int transactionTypeId,String transactionTypeDescription
		  )
		  {

		  
			  m_transactionTypeId = transactionTypeId;
		  
			  m_transactionTypeDescription = transactionTypeDescription;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    