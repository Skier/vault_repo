
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class CreditCardExpenceLine
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [CreditCardExpenceLine] ( " +
      
        " CreditCardExpenceLineId, " +
        " CreditCardId, " +
        " TxnLineID, " +
        " AccountId, " +
        " Amount, " +
        " Memo, " +
        " CustomerId " +
        ") Values (" +
      
        " @CreditCardExpenceLineId, " +
        " @CreditCardId, " +
        " @TxnLineID, " +
        " @AccountId, " +
        " @Amount, " +
        " @Memo, " +
        " @CustomerId " +
      ")";

      public static void Insert(CreditCardExpenceLine creditCardExpenceLine)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@CreditCardExpenceLineId", creditCardExpenceLine.CreditCardExpenceLineId);            
          
              Database.PutParameter(dbCommand,"@CreditCardId", creditCardExpenceLine
			.CreditCard.CreditCardId);            
          
              Database.PutParameter(dbCommand,"@TxnLineID", creditCardExpenceLine.TxnLineID);            
          
            if(creditCardExpenceLine
			.Account == null)
            {
            Database.PutParameter(dbCommand,"@AccountId", DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@AccountId", creditCardExpenceLine
			.Account.AccountId);
            }
          
              Database.PutParameter(dbCommand,"@Amount", creditCardExpenceLine.Amount);            
          
              Database.PutParameter(dbCommand,"@Memo", creditCardExpenceLine.Memo);            
          
            if(creditCardExpenceLine
			.Customer == null)
            {
            Database.PutParameter(dbCommand,"@CustomerId", DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@CustomerId", creditCardExpenceLine
			.Customer.CustomerId);
            }
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<CreditCardExpenceLine>  creditCardExpenceLineList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(CreditCardExpenceLine creditCardExpenceLine in  creditCardExpenceLineList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@CreditCardExpenceLineId", creditCardExpenceLine.CreditCardExpenceLineId);
          
            Database.PutParameter(dbCommand,"@CreditCardId", creditCardExpenceLine
			.CreditCard.CreditCardId);
          
            Database.PutParameter(dbCommand,"@TxnLineID", creditCardExpenceLine.TxnLineID);
          
            if(creditCardExpenceLine
			.Account == null)
            {
              Database.PutParameter(dbCommand,"@AccountId", DbType.Int32);
            }
            else
            {
              Database.PutParameter(dbCommand,"@AccountId", creditCardExpenceLine
			.Account.AccountId);
            }
          
            Database.PutParameter(dbCommand,"@Amount", creditCardExpenceLine.Amount);
          
            Database.PutParameter(dbCommand,"@Memo", creditCardExpenceLine.Memo);
          
            if(creditCardExpenceLine
			.Customer == null)
            {
              Database.PutParameter(dbCommand,"@CustomerId", DbType.Int32);
            }
            else
            {
              Database.PutParameter(dbCommand,"@CustomerId", creditCardExpenceLine
			.Customer.CustomerId);
            }
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@CreditCardExpenceLineId",creditCardExpenceLine.CreditCardExpenceLineId);
          
            Database.UpdateParameter(dbCommand,"@CreditCardId",creditCardExpenceLine
			.CreditCard.CreditCardId);
          
            Database.UpdateParameter(dbCommand,"@TxnLineID",creditCardExpenceLine.TxnLineID);
          
            if(creditCardExpenceLine
			.Account == null)
            {
             Database.UpdateParameter(dbCommand,"@AccountId",DbType.Int32);
            }
            else
            {
            Database.UpdateParameter(dbCommand,"@AccountId",creditCardExpenceLine
			.Account.AccountId);
            }
          
            Database.UpdateParameter(dbCommand,"@Amount",creditCardExpenceLine.Amount);
          
            Database.UpdateParameter(dbCommand,"@Memo",creditCardExpenceLine.Memo);
          
            if(creditCardExpenceLine
			.Customer == null)
            {
             Database.UpdateParameter(dbCommand,"@CustomerId",DbType.Int32);
            }
            else
            {
            Database.UpdateParameter(dbCommand,"@CustomerId",creditCardExpenceLine
			.Customer.CustomerId);
            }
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [CreditCardExpenceLine] Set "
      
        + " CreditCardId = @CreditCardId, "
        + " TxnLineID = @TxnLineID, "
        + " AccountId = @AccountId, "
        + " Amount = @Amount, "
        + " Memo = @Memo, "
        + " CustomerId = @CustomerId "
        + " Where "
        
          + " CreditCardExpenceLineId = @CreditCardExpenceLineId "
        
      ;

      public static void Update(CreditCardExpenceLine creditCardExpenceLine)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@CreditCardExpenceLineId", creditCardExpenceLine.CreditCardExpenceLineId);            
          
            Database.PutParameter(dbCommand,"@CreditCardId", creditCardExpenceLine
			.CreditCard.CreditCardId);            
          
            Database.PutParameter(dbCommand,"@TxnLineID", creditCardExpenceLine.TxnLineID);            
          
            if(creditCardExpenceLine
			.Account == null)
            {
            Database.PutParameter(dbCommand,"@AccountId",DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@AccountId",creditCardExpenceLine
			.Account.AccountId);
            }
          
            Database.PutParameter(dbCommand,"@Amount", creditCardExpenceLine.Amount);            
          
            Database.PutParameter(dbCommand,"@Memo", creditCardExpenceLine.Memo);            
          
            if(creditCardExpenceLine
			.Customer == null)
            {
            Database.PutParameter(dbCommand,"@CustomerId",DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@CustomerId",creditCardExpenceLine
			.Customer.CustomerId);
            }
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " CreditCardExpenceLineId, "
        + " CreditCardId, "
        + " TxnLineID, "
        + " AccountId, "
        + " Amount, "
        + " Memo, "
        + " CustomerId "
        + " From [CreditCardExpenceLine] "
      
        + " Where "
        
        + " CreditCardExpenceLineId = @CreditCardExpenceLineId "
        
      ;

      public static CreditCardExpenceLine FindByPrimaryKey(
      int creditCardExpenceLineId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CreditCardExpenceLineId", creditCardExpenceLineId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("CreditCardExpenceLine not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(CreditCardExpenceLine creditCardExpenceLine)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CreditCardExpenceLineId",creditCardExpenceLine.CreditCardExpenceLineId);
      

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
      String sql = "select 1 from [CreditCardExpenceLine]";

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

      public static CreditCardExpenceLine Load(IDataReader dataReader)
      {
      CreditCardExpenceLine creditCardExpenceLine = new CreditCardExpenceLine();

      creditCardExpenceLine.CreditCardExpenceLineId = dataReader.GetInt32(0);
          creditCardExpenceLine
			.CreditCard = new CreditCard();

            creditCardExpenceLine
			.CreditCard.CreditCardId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
              creditCardExpenceLine.TxnLineID = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            {
            creditCardExpenceLine
			.Account = new Account();
            
            creditCardExpenceLine
			.Account.AccountId = dataReader.GetInt32(3);
           }
            else
            creditCardExpenceLine
			.Account = null;
          
            if(!dataReader.IsDBNull(4))
              creditCardExpenceLine.Amount = dataReader.GetDecimal(4);
          
            if(!dataReader.IsDBNull(5))
              creditCardExpenceLine.Memo = dataReader.GetString(5);
          
            if(!dataReader.IsDBNull(6))
            {
            creditCardExpenceLine
			.Customer = new Customer();
            
            creditCardExpenceLine
			.Customer.CustomerId = dataReader.GetInt32(6);
           }
            else
            creditCardExpenceLine
			.Customer = null;
          

      return creditCardExpenceLine;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [CreditCardExpenceLine] "

      
        + " Where "
        
          + " CreditCardExpenceLineId = @CreditCardExpenceLineId "
        
      ;
      public static void Delete(CreditCardExpenceLine creditCardExpenceLine)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@CreditCardExpenceLineId", creditCardExpenceLine.CreditCardExpenceLineId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [CreditCardExpenceLine] ";

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
      
        + " CreditCardExpenceLineId, "
        + " CreditCardId, "
        + " TxnLineID, "
        + " AccountId, "
        + " Amount, "
        + " Memo, "
        + " CustomerId "
        + " From [CreditCardExpenceLine] ";
      public static List<CreditCardExpenceLine> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<CreditCardExpenceLine> rv = new List<CreditCardExpenceLine>();

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
      List<CreditCardExpenceLine> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<CreditCardExpenceLine> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CreditCardExpenceLine));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(CreditCardExpenceLine item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CreditCardExpenceLine>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CreditCardExpenceLine));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<CreditCardExpenceLine> itemsList
      = new List<CreditCardExpenceLine>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CreditCardExpenceLine)
      itemsList.Add(deserializedObject as CreditCardExpenceLine);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region CreditCardExpenceLineId
        protected int m_creditCardExpenceLineId;

			[XmlAttribute]
			public int CreditCardExpenceLineId
			{
			get { return m_creditCardExpenceLineId;}
			set { m_creditCardExpenceLineId = value; }
			}
		#endregion
		
		#region TxnLineID
        protected int? m_txnLineID;

			[XmlAttribute]
			public int? TxnLineID
			{
			get { return m_txnLineID;}
			set { m_txnLineID = value; }
			}
		#endregion
		
		#region Amount
        protected decimal? m_amount;

			[XmlAttribute]
			public decimal? Amount
			{
			get { return m_amount;}
			set { m_amount = value; }
			}
		#endregion
		
		#region Memo
        protected String m_memo;

			[XmlAttribute]
			public String Memo
			{
			get { return m_memo;}
			set { m_memo = value; }
			}
		#endregion
		
		#region Account
			protected Account m_account;

			[XmlElement]
			public Account Account
			{
			get { return m_account;}
			set { m_account = value; }
			}
		#endregion
		
		#region CreditCard
			protected CreditCard m_creditCard;

			[XmlElement]
			public CreditCard CreditCard
			{
			get { return m_creditCard;}
			set { m_creditCard = value; }
			}
		#endregion
		
		#region Customer
			protected Customer m_customer;

			[XmlElement]
			public Customer Customer
			{
			get { return m_customer;}
			set { m_customer = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public CreditCardExpenceLine(
		int creditCardExpenceLineId

		)
		{
		
			m_creditCardExpenceLineId = creditCardExpenceLineId;
		
        }

      


        public CreditCardExpenceLine(
		  Account account,CreditCard creditCard,Customer customer
			  ,
		  int creditCardExpenceLineId,int? txnLineID,decimal? amount,String memo
		  )
		  {

		  
			  m_account = account;
		  
			  m_creditCard = creditCard;
		  
			  m_customer = customer;
		  
			  m_creditCardExpenceLineId = creditCardExpenceLineId;
		  
			  m_txnLineID = txnLineID;
		  
			  m_amount = amount;
		  
			  m_memo = memo;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    