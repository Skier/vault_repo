
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class CreditCard
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [CreditCard] ( " +
      
        " CreditCardId, " +
        " CreditCardTypeId, " +
        " QuickBooksTxnId, " +
        " EntityStateId, " +
        " EditSequence, " +
        " TimeCreated, " +
        " TimeModified, " +
        " TxnNumber, " +
        " TxnDate, " +
        " AccountId, " +
        " PayeeQBEntityId, " +
        " RefNumber, " +
        " Amount, " +
        " Memo " +
        ") Values (" +
      
        " @CreditCardId, " +
        " @CreditCardTypeId, " +
        " @QuickBooksTxnId, " +
        " @EntityStateId, " +
        " @EditSequence, " +
        " @TimeCreated, " +
        " @TimeModified, " +
        " @TxnNumber, " +
        " @TxnDate, " +
        " @AccountId, " +
        " @PayeeQBEntityId, " +
        " @RefNumber, " +
        " @Amount, " +
        " @Memo " +
      ")";

      public static void Insert(CreditCard creditCard)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@CreditCardId", creditCard.CreditCardId);            
          
              Database.PutParameter(dbCommand,"@CreditCardTypeId", creditCard
			.CreditCardType.CreditCardTypeId);            
          
              Database.PutParameter(dbCommand,"@QuickBooksTxnId", creditCard.QuickBooksTxnId);            
          
              Database.PutParameter(dbCommand,"@EntityStateId", creditCard
			.EntityState.EntityStateId);            
          
              Database.PutParameter(dbCommand,"@EditSequence", creditCard.EditSequence);            
          
              Database.PutParameter(dbCommand,"@TimeCreated", creditCard.TimeCreated);            
          
              Database.PutParameter(dbCommand,"@TimeModified", creditCard.TimeModified);            
          
              Database.PutParameter(dbCommand,"@TxnNumber", creditCard.TxnNumber);            
          
              Database.PutParameter(dbCommand,"@TxnDate", creditCard.TxnDate);            
          
              Database.PutParameter(dbCommand,"@AccountId", creditCard
			.Account.AccountId);            
          
              Database.PutParameter(dbCommand,"@PayeeQBEntityId", creditCard.PayeeQBEntityId);            
          
              Database.PutParameter(dbCommand,"@RefNumber", creditCard.RefNumber);            
          
              Database.PutParameter(dbCommand,"@Amount", creditCard.Amount);            
          
              Database.PutParameter(dbCommand,"@Memo", creditCard.Memo);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<CreditCard>  creditCardList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(CreditCard creditCard in  creditCardList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@CreditCardId", creditCard.CreditCardId);
          
            Database.PutParameter(dbCommand,"@CreditCardTypeId", creditCard
			.CreditCardType.CreditCardTypeId);
          
            Database.PutParameter(dbCommand,"@QuickBooksTxnId", creditCard.QuickBooksTxnId);
          
            Database.PutParameter(dbCommand,"@EntityStateId", creditCard
			.EntityState.EntityStateId);
          
            Database.PutParameter(dbCommand,"@EditSequence", creditCard.EditSequence);
          
            Database.PutParameter(dbCommand,"@TimeCreated", creditCard.TimeCreated);
          
            Database.PutParameter(dbCommand,"@TimeModified", creditCard.TimeModified);
          
            Database.PutParameter(dbCommand,"@TxnNumber", creditCard.TxnNumber);
          
            Database.PutParameter(dbCommand,"@TxnDate", creditCard.TxnDate);
          
            Database.PutParameter(dbCommand,"@AccountId", creditCard
			.Account.AccountId);
          
            Database.PutParameter(dbCommand,"@PayeeQBEntityId", creditCard.PayeeQBEntityId);
          
            Database.PutParameter(dbCommand,"@RefNumber", creditCard.RefNumber);
          
            Database.PutParameter(dbCommand,"@Amount", creditCard.Amount);
          
            Database.PutParameter(dbCommand,"@Memo", creditCard.Memo);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@CreditCardId",creditCard.CreditCardId);
          
            Database.UpdateParameter(dbCommand,"@CreditCardTypeId",creditCard
			.CreditCardType.CreditCardTypeId);
          
            Database.UpdateParameter(dbCommand,"@QuickBooksTxnId",creditCard.QuickBooksTxnId);
          
            Database.UpdateParameter(dbCommand,"@EntityStateId",creditCard
			.EntityState.EntityStateId);
          
            Database.UpdateParameter(dbCommand,"@EditSequence",creditCard.EditSequence);
          
            Database.UpdateParameter(dbCommand,"@TimeCreated",creditCard.TimeCreated);
          
            Database.UpdateParameter(dbCommand,"@TimeModified",creditCard.TimeModified);
          
            Database.UpdateParameter(dbCommand,"@TxnNumber",creditCard.TxnNumber);
          
            Database.UpdateParameter(dbCommand,"@TxnDate",creditCard.TxnDate);
          
            Database.UpdateParameter(dbCommand,"@AccountId",creditCard
			.Account.AccountId);
          
            Database.UpdateParameter(dbCommand,"@PayeeQBEntityId",creditCard.PayeeQBEntityId);
          
            Database.UpdateParameter(dbCommand,"@RefNumber",creditCard.RefNumber);
          
            Database.UpdateParameter(dbCommand,"@Amount",creditCard.Amount);
          
            Database.UpdateParameter(dbCommand,"@Memo",creditCard.Memo);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [CreditCard] Set "
      
        + " CreditCardTypeId = @CreditCardTypeId, "
        + " QuickBooksTxnId = @QuickBooksTxnId, "
        + " EntityStateId = @EntityStateId, "
        + " EditSequence = @EditSequence, "
        + " TimeCreated = @TimeCreated, "
        + " TimeModified = @TimeModified, "
        + " TxnNumber = @TxnNumber, "
        + " TxnDate = @TxnDate, "
        + " AccountId = @AccountId, "
        + " PayeeQBEntityId = @PayeeQBEntityId, "
        + " RefNumber = @RefNumber, "
        + " Amount = @Amount, "
        + " Memo = @Memo "
        + " Where "
        
          + " CreditCardId = @CreditCardId "
        
      ;

      public static void Update(CreditCard creditCard)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@CreditCardId", creditCard.CreditCardId);            
          
            Database.PutParameter(dbCommand,"@CreditCardTypeId", creditCard
			.CreditCardType.CreditCardTypeId);            
          
            Database.PutParameter(dbCommand,"@QuickBooksTxnId", creditCard.QuickBooksTxnId);            
          
            Database.PutParameter(dbCommand,"@EntityStateId", creditCard
			.EntityState.EntityStateId);            
          
            Database.PutParameter(dbCommand,"@EditSequence", creditCard.EditSequence);            
          
            Database.PutParameter(dbCommand,"@TimeCreated", creditCard.TimeCreated);            
          
            Database.PutParameter(dbCommand,"@TimeModified", creditCard.TimeModified);            
          
            Database.PutParameter(dbCommand,"@TxnNumber", creditCard.TxnNumber);            
          
            Database.PutParameter(dbCommand,"@TxnDate", creditCard.TxnDate);            
          
            Database.PutParameter(dbCommand,"@AccountId", creditCard
			.Account.AccountId);            
          
            Database.PutParameter(dbCommand,"@PayeeQBEntityId", creditCard.PayeeQBEntityId);            
          
            Database.PutParameter(dbCommand,"@RefNumber", creditCard.RefNumber);            
          
            Database.PutParameter(dbCommand,"@Amount", creditCard.Amount);            
          
            Database.PutParameter(dbCommand,"@Memo", creditCard.Memo);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " CreditCardId, "
        + " CreditCardTypeId, "
        + " QuickBooksTxnId, "
        + " EntityStateId, "
        + " EditSequence, "
        + " TimeCreated, "
        + " TimeModified, "
        + " TxnNumber, "
        + " TxnDate, "
        + " AccountId, "
        + " PayeeQBEntityId, "
        + " RefNumber, "
        + " Amount, "
        + " Memo "
        + " From [CreditCard] "
      
        + " Where "
        
        + " CreditCardId = @CreditCardId "
        
      ;

      public static CreditCard FindByPrimaryKey(
      int creditCardId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CreditCardId", creditCardId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("CreditCard not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(CreditCard creditCard)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CreditCardId",creditCard.CreditCardId);
      

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
      String sql = "select 1 from [CreditCard]";

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

      public static CreditCard Load(IDataReader dataReader)
      {
      CreditCard creditCard = new CreditCard();

      creditCard.CreditCardId = dataReader.GetInt32(0);
          creditCard
			.CreditCardType = new CreditCardType();

            creditCard
			.CreditCardType.CreditCardTypeId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
              creditCard.QuickBooksTxnId = dataReader.GetInt32(2);
          creditCard
			.EntityState = new EntityState();

            creditCard
			.EntityState.EntityStateId = dataReader.GetInt32(3);
          creditCard.EditSequence = dataReader.GetInt32(4);
          
            if(!dataReader.IsDBNull(5))
              creditCard.TimeCreated = dataReader.GetDateTime(5);
          
            if(!dataReader.IsDBNull(6))
              creditCard.TimeModified = dataReader.GetDateTime(6);
          
            if(!dataReader.IsDBNull(7))
              creditCard.TxnNumber = dataReader.GetInt32(7);
          
            if(!dataReader.IsDBNull(8))
              creditCard.TxnDate = dataReader.GetDateTime(8);
          creditCard
			.Account = new Account();

            creditCard
			.Account.AccountId = dataReader.GetInt32(9);
          
            if(!dataReader.IsDBNull(10))
              creditCard.PayeeQBEntityId = dataReader.GetInt32(10);
          
            if(!dataReader.IsDBNull(11))
              creditCard.RefNumber = dataReader.GetString(11);
          
            if(!dataReader.IsDBNull(12))
              creditCard.Amount = dataReader.GetDecimal(12);
          
            if(!dataReader.IsDBNull(13))
              creditCard.Memo = dataReader.GetString(13);
          

      return creditCard;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [CreditCard] "

      
        + " Where "
        
          + " CreditCardId = @CreditCardId "
        
      ;
      public static void Delete(CreditCard creditCard)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@CreditCardId", creditCard.CreditCardId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [CreditCard] ";

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
      
        + " CreditCardId, "
        + " CreditCardTypeId, "
        + " QuickBooksTxnId, "
        + " EntityStateId, "
        + " EditSequence, "
        + " TimeCreated, "
        + " TimeModified, "
        + " TxnNumber, "
        + " TxnDate, "
        + " AccountId, "
        + " PayeeQBEntityId, "
        + " RefNumber, "
        + " Amount, "
        + " Memo "
        + " From [CreditCard] ";
      public static List<CreditCard> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<CreditCard> rv = new List<CreditCard>();

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
      List<CreditCard> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<CreditCard> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CreditCard));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(CreditCard item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CreditCard>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CreditCard));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<CreditCard> itemsList
      = new List<CreditCard>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CreditCard)
      itemsList.Add(deserializedObject as CreditCard);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region CreditCardId
        protected int m_creditCardId;

			[XmlAttribute]
			public int CreditCardId
			{
			get { return m_creditCardId;}
			set { m_creditCardId = value; }
			}
		#endregion
		
		#region QuickBooksTxnId
        protected int? m_quickBooksTxnId;

			[XmlAttribute]
			public int? QuickBooksTxnId
			{
			get { return m_quickBooksTxnId;}
			set { m_quickBooksTxnId = value; }
			}
		#endregion
		
		#region EditSequence
        protected int m_editSequence;

			[XmlAttribute]
			public int EditSequence
			{
			get { return m_editSequence;}
			set { m_editSequence = value; }
			}
		#endregion
		
		#region TimeCreated
        protected DateTime? m_timeCreated;

			[XmlAttribute]
			public DateTime? TimeCreated
			{
			get { return m_timeCreated;}
			set { m_timeCreated = value; }
			}
		#endregion
		
		#region TimeModified
        protected DateTime? m_timeModified;

			[XmlAttribute]
			public DateTime? TimeModified
			{
			get { return m_timeModified;}
			set { m_timeModified = value; }
			}
		#endregion
		
		#region TxnNumber
        protected int? m_txnNumber;

			[XmlAttribute]
			public int? TxnNumber
			{
			get { return m_txnNumber;}
			set { m_txnNumber = value; }
			}
		#endregion
		
		#region TxnDate
        protected DateTime? m_txnDate;

			[XmlAttribute]
			public DateTime? TxnDate
			{
			get { return m_txnDate;}
			set { m_txnDate = value; }
			}
		#endregion
		
		#region PayeeQBEntityId
        protected int? m_payeeQBEntityId;

			[XmlAttribute]
			public int? PayeeQBEntityId
			{
			get { return m_payeeQBEntityId;}
			set { m_payeeQBEntityId = value; }
			}
		#endregion
		
		#region RefNumber
        protected String m_refNumber;

			[XmlAttribute]
			public String RefNumber
			{
			get { return m_refNumber;}
			set { m_refNumber = value; }
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
		
		#region CreditCardType
			protected CreditCardType m_creditCardType;

			[XmlElement]
			public CreditCardType CreditCardType
			{
			get { return m_creditCardType;}
			set { m_creditCardType = value; }
			}
		#endregion
		
		#region EntityState
			protected EntityState m_entityState;

			[XmlElement]
			public EntityState EntityState
			{
			get { return m_entityState;}
			set { m_entityState = value; }
			}
		#endregion
		
		#region QBEntity
			protected QBEntity m_qBEntity;

			[XmlElement]
			public QBEntity QBEntity
			{
			get { return m_qBEntity;}
			set { m_qBEntity = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public CreditCard(
		int creditCardId

		)
		{
		
			m_creditCardId = creditCardId;
		
        }

      


        public CreditCard(
		  Account account,CreditCardType creditCardType,EntityState entityState,QBEntity qBEntity
			  ,
		  int creditCardId,int? quickBooksTxnId,int editSequence,DateTime? timeCreated,DateTime? timeModified,int? txnNumber,DateTime? txnDate,int? payeeQBEntityId,String refNumber,decimal? amount,String memo
		  )
		  {

		  
			  m_account = account;
		  
			  m_creditCardType = creditCardType;
		  
			  m_entityState = entityState;
		  
			  m_qBEntity = qBEntity;
		  
			  m_creditCardId = creditCardId;
		  
			  m_quickBooksTxnId = quickBooksTxnId;
		  
			  m_editSequence = editSequence;
		  
			  m_timeCreated = timeCreated;
		  
			  m_timeModified = timeModified;
		  
			  m_txnNumber = txnNumber;
		  
			  m_txnDate = txnDate;
		  
			  m_payeeQBEntityId = payeeQBEntityId;
		  
			  m_refNumber = refNumber;
		  
			  m_amount = amount;
		  
			  m_memo = memo;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    