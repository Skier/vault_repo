
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class Check
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Check] ( " +
      
        " CheckId, " +
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
        " Memo, " +
        " Addr1, " +
        " Addr2, " +
        " Addr3, " +
        " Addr4, " +
        " City, " +
        " State, " +
        " PostalCode, " +
        " Country, " +
        " IsToBePrinted " +
        ") Values (" +
      
        " @CheckId, " +
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
        " @Memo, " +
        " @Addr1, " +
        " @Addr2, " +
        " @Addr3, " +
        " @Addr4, " +
        " @City, " +
        " @State, " +
        " @PostalCode, " +
        " @Country, " +
        " @IsToBePrinted " +
      ")";

      public static void Insert(Check check)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@CheckId", check.CheckId);            
          
              Database.PutParameter(dbCommand,"@QuickBooksTxnId", check.QuickBooksTxnId);            
          
              Database.PutParameter(dbCommand,"@EntityStateId", check
			.EntityState.EntityStateId);            
          
              Database.PutParameter(dbCommand,"@EditSequence", check.EditSequence);            
          
              Database.PutParameter(dbCommand,"@TimeCreated", check.TimeCreated);            
          
              Database.PutParameter(dbCommand,"@TimeModified", check.TimeModified);            
          
              Database.PutParameter(dbCommand,"@TxnNumber", check.TxnNumber);            
          
              Database.PutParameter(dbCommand,"@TxnDate", check.TxnDate);            
          
              Database.PutParameter(dbCommand,"@AccountId", check
			.Account.AccountId);            
          
              Database.PutParameter(dbCommand,"@PayeeQBEntityId", check.PayeeQBEntityId);            
          
              Database.PutParameter(dbCommand,"@RefNumber", check.RefNumber);            
          
              Database.PutParameter(dbCommand,"@Amount", check.Amount);            
          
              Database.PutParameter(dbCommand,"@Memo", check.Memo);            
          
              Database.PutParameter(dbCommand,"@Addr1", check.Addr1);            
          
              Database.PutParameter(dbCommand,"@Addr2", check.Addr2);            
          
              Database.PutParameter(dbCommand,"@Addr3", check.Addr3);            
          
              Database.PutParameter(dbCommand,"@Addr4", check.Addr4);            
          
              Database.PutParameter(dbCommand,"@City", check.City);            
          
              Database.PutParameter(dbCommand,"@State", check.State);            
          
              Database.PutParameter(dbCommand,"@PostalCode", check.PostalCode);            
          
              Database.PutParameter(dbCommand,"@Country", check.Country);            
          
              Database.PutParameter(dbCommand,"@IsToBePrinted", check.IsToBePrinted);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Check>  checkList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Check check in  checkList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@CheckId", check.CheckId);
          
            Database.PutParameter(dbCommand,"@QuickBooksTxnId", check.QuickBooksTxnId);
          
            Database.PutParameter(dbCommand,"@EntityStateId", check
			.EntityState.EntityStateId);
          
            Database.PutParameter(dbCommand,"@EditSequence", check.EditSequence);
          
            Database.PutParameter(dbCommand,"@TimeCreated", check.TimeCreated);
          
            Database.PutParameter(dbCommand,"@TimeModified", check.TimeModified);
          
            Database.PutParameter(dbCommand,"@TxnNumber", check.TxnNumber);
          
            Database.PutParameter(dbCommand,"@TxnDate", check.TxnDate);
          
            Database.PutParameter(dbCommand,"@AccountId", check
			.Account.AccountId);
          
            Database.PutParameter(dbCommand,"@PayeeQBEntityId", check.PayeeQBEntityId);
          
            Database.PutParameter(dbCommand,"@RefNumber", check.RefNumber);
          
            Database.PutParameter(dbCommand,"@Amount", check.Amount);
          
            Database.PutParameter(dbCommand,"@Memo", check.Memo);
          
            Database.PutParameter(dbCommand,"@Addr1", check.Addr1);
          
            Database.PutParameter(dbCommand,"@Addr2", check.Addr2);
          
            Database.PutParameter(dbCommand,"@Addr3", check.Addr3);
          
            Database.PutParameter(dbCommand,"@Addr4", check.Addr4);
          
            Database.PutParameter(dbCommand,"@City", check.City);
          
            Database.PutParameter(dbCommand,"@State", check.State);
          
            Database.PutParameter(dbCommand,"@PostalCode", check.PostalCode);
          
            Database.PutParameter(dbCommand,"@Country", check.Country);
          
            Database.PutParameter(dbCommand,"@IsToBePrinted", check.IsToBePrinted);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@CheckId",check.CheckId);
          
            Database.UpdateParameter(dbCommand,"@QuickBooksTxnId",check.QuickBooksTxnId);
          
            Database.UpdateParameter(dbCommand,"@EntityStateId",check
			.EntityState.EntityStateId);
          
            Database.UpdateParameter(dbCommand,"@EditSequence",check.EditSequence);
          
            Database.UpdateParameter(dbCommand,"@TimeCreated",check.TimeCreated);
          
            Database.UpdateParameter(dbCommand,"@TimeModified",check.TimeModified);
          
            Database.UpdateParameter(dbCommand,"@TxnNumber",check.TxnNumber);
          
            Database.UpdateParameter(dbCommand,"@TxnDate",check.TxnDate);
          
            Database.UpdateParameter(dbCommand,"@AccountId",check
			.Account.AccountId);
          
            Database.UpdateParameter(dbCommand,"@PayeeQBEntityId",check.PayeeQBEntityId);
          
            Database.UpdateParameter(dbCommand,"@RefNumber",check.RefNumber);
          
            Database.UpdateParameter(dbCommand,"@Amount",check.Amount);
          
            Database.UpdateParameter(dbCommand,"@Memo",check.Memo);
          
            Database.UpdateParameter(dbCommand,"@Addr1",check.Addr1);
          
            Database.UpdateParameter(dbCommand,"@Addr2",check.Addr2);
          
            Database.UpdateParameter(dbCommand,"@Addr3",check.Addr3);
          
            Database.UpdateParameter(dbCommand,"@Addr4",check.Addr4);
          
            Database.UpdateParameter(dbCommand,"@City",check.City);
          
            Database.UpdateParameter(dbCommand,"@State",check.State);
          
            Database.UpdateParameter(dbCommand,"@PostalCode",check.PostalCode);
          
            Database.UpdateParameter(dbCommand,"@Country",check.Country);
          
            Database.UpdateParameter(dbCommand,"@IsToBePrinted",check.IsToBePrinted);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Check] Set "
      
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
        + " Memo = @Memo, "
        + " Addr1 = @Addr1, "
        + " Addr2 = @Addr2, "
        + " Addr3 = @Addr3, "
        + " Addr4 = @Addr4, "
        + " City = @City, "
        + " State = @State, "
        + " PostalCode = @PostalCode, "
        + " Country = @Country, "
        + " IsToBePrinted = @IsToBePrinted "
        + " Where "
        
          + " CheckId = @CheckId "
        
      ;

      public static void Update(Check check)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@CheckId", check.CheckId);            
          
            Database.PutParameter(dbCommand,"@QuickBooksTxnId", check.QuickBooksTxnId);            
          
            Database.PutParameter(dbCommand,"@EntityStateId", check
			.EntityState.EntityStateId);            
          
            Database.PutParameter(dbCommand,"@EditSequence", check.EditSequence);            
          
            Database.PutParameter(dbCommand,"@TimeCreated", check.TimeCreated);            
          
            Database.PutParameter(dbCommand,"@TimeModified", check.TimeModified);            
          
            Database.PutParameter(dbCommand,"@TxnNumber", check.TxnNumber);            
          
            Database.PutParameter(dbCommand,"@TxnDate", check.TxnDate);            
          
            Database.PutParameter(dbCommand,"@AccountId", check
			.Account.AccountId);            
          
            Database.PutParameter(dbCommand,"@PayeeQBEntityId", check.PayeeQBEntityId);            
          
            Database.PutParameter(dbCommand,"@RefNumber", check.RefNumber);            
          
            Database.PutParameter(dbCommand,"@Amount", check.Amount);            
          
            Database.PutParameter(dbCommand,"@Memo", check.Memo);            
          
            Database.PutParameter(dbCommand,"@Addr1", check.Addr1);            
          
            Database.PutParameter(dbCommand,"@Addr2", check.Addr2);            
          
            Database.PutParameter(dbCommand,"@Addr3", check.Addr3);            
          
            Database.PutParameter(dbCommand,"@Addr4", check.Addr4);            
          
            Database.PutParameter(dbCommand,"@City", check.City);            
          
            Database.PutParameter(dbCommand,"@State", check.State);            
          
            Database.PutParameter(dbCommand,"@PostalCode", check.PostalCode);            
          
            Database.PutParameter(dbCommand,"@Country", check.Country);            
          
            Database.PutParameter(dbCommand,"@IsToBePrinted", check.IsToBePrinted);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " CheckId, "
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
        + " Memo, "
        + " Addr1, "
        + " Addr2, "
        + " Addr3, "
        + " Addr4, "
        + " City, "
        + " State, "
        + " PostalCode, "
        + " Country, "
        + " IsToBePrinted "
        + " From [Check] "
      
        + " Where "
        
        + " CheckId = @CheckId "
        
      ;

      public static Check FindByPrimaryKey(
      int checkId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CheckId", checkId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Check not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Check check)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CheckId",check.CheckId);
      

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
      String sql = "select 1 from [Check]";

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

      public static Check Load(IDataReader dataReader)
      {
      Check check = new Check();

      check.CheckId = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
              check.QuickBooksTxnId = dataReader.GetInt32(1);
          check
			.EntityState = new EntityState();

            check
			.EntityState.EntityStateId = dataReader.GetInt32(2);
          check.EditSequence = dataReader.GetInt32(3);
          
            if(!dataReader.IsDBNull(4))
              check.TimeCreated = dataReader.GetDateTime(4);
          
            if(!dataReader.IsDBNull(5))
              check.TimeModified = dataReader.GetDateTime(5);
          
            if(!dataReader.IsDBNull(6))
              check.TxnNumber = dataReader.GetInt32(6);
          
            if(!dataReader.IsDBNull(7))
              check.TxnDate = dataReader.GetDateTime(7);
          check
			.Account = new Account();

            check
			.Account.AccountId = dataReader.GetInt32(8);
          
            if(!dataReader.IsDBNull(9))
              check.PayeeQBEntityId = dataReader.GetInt32(9);
          
            if(!dataReader.IsDBNull(10))
              check.RefNumber = dataReader.GetString(10);
          
            if(!dataReader.IsDBNull(11))
              check.Amount = dataReader.GetDecimal(11);
          
            if(!dataReader.IsDBNull(12))
              check.Memo = dataReader.GetString(12);
          
            if(!dataReader.IsDBNull(13))
              check.Addr1 = dataReader.GetString(13);
          
            if(!dataReader.IsDBNull(14))
              check.Addr2 = dataReader.GetString(14);
          
            if(!dataReader.IsDBNull(15))
              check.Addr3 = dataReader.GetString(15);
          
            if(!dataReader.IsDBNull(16))
              check.Addr4 = dataReader.GetString(16);
          
            if(!dataReader.IsDBNull(17))
              check.City = dataReader.GetString(17);
          
            if(!dataReader.IsDBNull(18))
              check.State = dataReader.GetString(18);
          
            if(!dataReader.IsDBNull(19))
              check.PostalCode = dataReader.GetString(19);
          
            if(!dataReader.IsDBNull(20))
              check.Country = dataReader.GetString(20);
          
            if(!dataReader.IsDBNull(21))
              check.IsToBePrinted = dataReader.GetBoolean(21);
          

      return check;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Check] "

      
        + " Where "
        
          + " CheckId = @CheckId "
        
      ;
      public static void Delete(Check check)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@CheckId", check.CheckId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Check] ";

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
      
        + " CheckId, "
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
        + " Memo, "
        + " Addr1, "
        + " Addr2, "
        + " Addr3, "
        + " Addr4, "
        + " City, "
        + " State, "
        + " PostalCode, "
        + " Country, "
        + " IsToBePrinted "
        + " From [Check] ";
      public static List<Check> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Check> rv = new List<Check>();

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
      List<Check> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Check> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Check));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Check item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Check>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Check));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Check> itemsList
      = new List<Check>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Check)
      itemsList.Add(deserializedObject as Check);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region CheckId
        protected int m_checkId;

			[XmlAttribute]
			public int CheckId
			{
			get { return m_checkId;}
			set { m_checkId = value; }
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
		
		#region Addr1
        protected String m_addr1;

			[XmlAttribute]
			public String Addr1
			{
			get { return m_addr1;}
			set { m_addr1 = value; }
			}
		#endregion
		
		#region Addr2
        protected String m_addr2;

			[XmlAttribute]
			public String Addr2
			{
			get { return m_addr2;}
			set { m_addr2 = value; }
			}
		#endregion
		
		#region Addr3
        protected String m_addr3;

			[XmlAttribute]
			public String Addr3
			{
			get { return m_addr3;}
			set { m_addr3 = value; }
			}
		#endregion
		
		#region Addr4
        protected String m_addr4;

			[XmlAttribute]
			public String Addr4
			{
			get { return m_addr4;}
			set { m_addr4 = value; }
			}
		#endregion
		
		#region City
        protected String m_city;

			[XmlAttribute]
			public String City
			{
			get { return m_city;}
			set { m_city = value; }
			}
		#endregion
		
		#region State
        protected String m_state;

			[XmlAttribute]
			public String State
			{
			get { return m_state;}
			set { m_state = value; }
			}
		#endregion
		
		#region PostalCode
        protected String m_postalCode;

			[XmlAttribute]
			public String PostalCode
			{
			get { return m_postalCode;}
			set { m_postalCode = value; }
			}
		#endregion
		
		#region Country
        protected String m_country;

			[XmlAttribute]
			public String Country
			{
			get { return m_country;}
			set { m_country = value; }
			}
		#endregion
		
		#region IsToBePrinted
        protected bool m_isToBePrinted;

			[XmlAttribute]
			public bool IsToBePrinted
			{
			get { return m_isToBePrinted;}
			set { m_isToBePrinted = value; }
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
      public Check(
		int checkId

		)
		{
		
			m_checkId = checkId;
		
        }

      


        public Check(
		  Account account,EntityState entityState,QBEntity qBEntity
			  ,
		  int checkId,int? quickBooksTxnId,int editSequence,DateTime? timeCreated,DateTime? timeModified,int? txnNumber,DateTime? txnDate,int? payeeQBEntityId,String refNumber,decimal? amount,String memo,String addr1,String addr2,String addr3,String addr4,String city,String state,String postalCode,String country,bool isToBePrinted
		  )
		  {

		  
			  m_account = account;
		  
			  m_entityState = entityState;
		  
			  m_qBEntity = qBEntity;
		  
			  m_checkId = checkId;
		  
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
		  
			  m_addr1 = addr1;
		  
			  m_addr2 = addr2;
		  
			  m_addr3 = addr3;
		  
			  m_addr4 = addr4;
		  
			  m_city = city;
		  
			  m_state = state;
		  
			  m_postalCode = postalCode;
		  
			  m_country = country;
		  
			  m_isToBePrinted = isToBePrinted;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    