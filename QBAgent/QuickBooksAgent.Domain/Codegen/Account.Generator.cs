
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class Account
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Account] ( " +
      
        " AccountId, " +
        " QuickBooksListId, " +
        " EntityStateId, " +
        " EditSequence, " +
        " Name, " +
        " FullName, " +
        " AccountTypeId, " +
        " DetailAccountTypeId, " +
        " AccountNumber, " +
        " LastCheckNumber, " +
        " Descriptive, " +
        " Balance, " +
        " TotalBalance " +
        ") Values (" +
      
        " @AccountId, " +
        " @QuickBooksListId, " +
        " @EntityStateId, " +
        " @EditSequence, " +
        " @Name, " +
        " @FullName, " +
        " @AccountTypeId, " +
        " @DetailAccountTypeId, " +
        " @AccountNumber, " +
        " @LastCheckNumber, " +
        " @Descriptive, " +
        " @Balance, " +
        " @TotalBalance " +
      ")";

      public static void Insert(Account account)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@AccountId", account.AccountId);            
          
              Database.PutParameter(dbCommand,"@QuickBooksListId", account.QuickBooksListId);            
          
              Database.PutParameter(dbCommand,"@EntityStateId", account
			.EntityState.EntityStateId);            
          
              Database.PutParameter(dbCommand,"@EditSequence", account.EditSequence);            
          
              Database.PutParameter(dbCommand,"@Name", account.Name);            
          
              Database.PutParameter(dbCommand,"@FullName", account.FullName);            
          
              Database.PutParameter(dbCommand,"@AccountTypeId", account
			.AccountType.AccountTypeId);            
          
            if(account
			.DetailAccountType == null)
            {
            Database.PutParameter(dbCommand,"@DetailAccountTypeId", DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@DetailAccountTypeId", account
			.DetailAccountType.DetailAccountTypeId);
            }
          
              Database.PutParameter(dbCommand,"@AccountNumber", account.AccountNumber);            
          
              Database.PutParameter(dbCommand,"@LastCheckNumber", account.LastCheckNumber);            
          
              Database.PutParameter(dbCommand,"@Descriptive", account.Descriptive);            
          
              Database.PutParameter(dbCommand,"@Balance", account.Balance);            
          
              Database.PutParameter(dbCommand,"@TotalBalance", account.TotalBalance);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Account>  accountList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Account account in  accountList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@AccountId", account.AccountId);
          
            Database.PutParameter(dbCommand,"@QuickBooksListId", account.QuickBooksListId);
          
            Database.PutParameter(dbCommand,"@EntityStateId", account
			.EntityState.EntityStateId);
          
            Database.PutParameter(dbCommand,"@EditSequence", account.EditSequence);
          
            Database.PutParameter(dbCommand,"@Name", account.Name);
          
            Database.PutParameter(dbCommand,"@FullName", account.FullName);
          
            Database.PutParameter(dbCommand,"@AccountTypeId", account
			.AccountType.AccountTypeId);
          
            if(account
			.DetailAccountType == null)
            {
              Database.PutParameter(dbCommand,"@DetailAccountTypeId", DbType.Int32);
            }
            else
            {
              Database.PutParameter(dbCommand,"@DetailAccountTypeId", account
			.DetailAccountType.DetailAccountTypeId);
            }
          
            Database.PutParameter(dbCommand,"@AccountNumber", account.AccountNumber);
          
            Database.PutParameter(dbCommand,"@LastCheckNumber", account.LastCheckNumber);
          
            Database.PutParameter(dbCommand,"@Descriptive", account.Descriptive);
          
            Database.PutParameter(dbCommand,"@Balance", account.Balance);
          
            Database.PutParameter(dbCommand,"@TotalBalance", account.TotalBalance);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@AccountId",account.AccountId);
          
            Database.UpdateParameter(dbCommand,"@QuickBooksListId",account.QuickBooksListId);
          
            Database.UpdateParameter(dbCommand,"@EntityStateId",account
			.EntityState.EntityStateId);
          
            Database.UpdateParameter(dbCommand,"@EditSequence",account.EditSequence);
          
            Database.UpdateParameter(dbCommand,"@Name",account.Name);
          
            Database.UpdateParameter(dbCommand,"@FullName",account.FullName);
          
            Database.UpdateParameter(dbCommand,"@AccountTypeId",account
			.AccountType.AccountTypeId);
          
            if(account
			.DetailAccountType == null)
            {
             Database.UpdateParameter(dbCommand,"@DetailAccountTypeId",DbType.Int32);
            }
            else
            {
            Database.UpdateParameter(dbCommand,"@DetailAccountTypeId",account
			.DetailAccountType.DetailAccountTypeId);
            }
          
            Database.UpdateParameter(dbCommand,"@AccountNumber",account.AccountNumber);
          
            Database.UpdateParameter(dbCommand,"@LastCheckNumber",account.LastCheckNumber);
          
            Database.UpdateParameter(dbCommand,"@Descriptive",account.Descriptive);
          
            Database.UpdateParameter(dbCommand,"@Balance",account.Balance);
          
            Database.UpdateParameter(dbCommand,"@TotalBalance",account.TotalBalance);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Account] Set "
      
        + " QuickBooksListId = @QuickBooksListId, "
        + " EntityStateId = @EntityStateId, "
        + " EditSequence = @EditSequence, "
        + " Name = @Name, "
        + " FullName = @FullName, "
        + " AccountTypeId = @AccountTypeId, "
        + " DetailAccountTypeId = @DetailAccountTypeId, "
        + " AccountNumber = @AccountNumber, "
        + " LastCheckNumber = @LastCheckNumber, "
        + " Descriptive = @Descriptive, "
        + " Balance = @Balance, "
        + " TotalBalance = @TotalBalance "
        + " Where "
        
          + " AccountId = @AccountId "
        
      ;

      public static void Update(Account account)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@AccountId", account.AccountId);            
          
            Database.PutParameter(dbCommand,"@QuickBooksListId", account.QuickBooksListId);            
          
            Database.PutParameter(dbCommand,"@EntityStateId", account
			.EntityState.EntityStateId);            
          
            Database.PutParameter(dbCommand,"@EditSequence", account.EditSequence);            
          
            Database.PutParameter(dbCommand,"@Name", account.Name);            
          
            Database.PutParameter(dbCommand,"@FullName", account.FullName);            
          
            Database.PutParameter(dbCommand,"@AccountTypeId", account
			.AccountType.AccountTypeId);            
          
            if(account
			.DetailAccountType == null)
            {
            Database.PutParameter(dbCommand,"@DetailAccountTypeId",DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@DetailAccountTypeId",account
			.DetailAccountType.DetailAccountTypeId);
            }
          
            Database.PutParameter(dbCommand,"@AccountNumber", account.AccountNumber);            
          
            Database.PutParameter(dbCommand,"@LastCheckNumber", account.LastCheckNumber);            
          
            Database.PutParameter(dbCommand,"@Descriptive", account.Descriptive);            
          
            Database.PutParameter(dbCommand,"@Balance", account.Balance);            
          
            Database.PutParameter(dbCommand,"@TotalBalance", account.TotalBalance);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " AccountId, "
        + " QuickBooksListId, "
        + " EntityStateId, "
        + " EditSequence, "
        + " Name, "
        + " FullName, "
        + " AccountTypeId, "
        + " DetailAccountTypeId, "
        + " AccountNumber, "
        + " LastCheckNumber, "
        + " Descriptive, "
        + " Balance, "
        + " TotalBalance "
        + " From [Account] "
      
        + " Where "
        
        + " AccountId = @AccountId "
        
      ;

      public static Account FindByPrimaryKey(
      int accountId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@AccountId", accountId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Account not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Account account)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@AccountId",account.AccountId);
      

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
      String sql = "select 1 from [Account]";

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

      public static Account Load(IDataReader dataReader)
      {
      Account account = new Account();

      account.AccountId = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
              account.QuickBooksListId = dataReader.GetInt32(1);
          account
			.EntityState = new EntityState();

            account
			.EntityState.EntityStateId = dataReader.GetInt32(2);
          account.EditSequence = dataReader.GetInt32(3);
          account.Name = dataReader.GetString(4);
          
            if(!dataReader.IsDBNull(5))
              account.FullName = dataReader.GetString(5);
          account
			.AccountType = new AccountType();

            account
			.AccountType.AccountTypeId = dataReader.GetInt32(6);
          
            if(!dataReader.IsDBNull(7))
            {
            account
			.DetailAccountType = new DetailAccountType();
            
            account
			.DetailAccountType.DetailAccountTypeId = dataReader.GetInt32(7);
           }
            else
            account
			.DetailAccountType = null;
          
            if(!dataReader.IsDBNull(8))
              account.AccountNumber = dataReader.GetString(8);
          
            if(!dataReader.IsDBNull(9))
              account.LastCheckNumber = dataReader.GetString(9);
          
            if(!dataReader.IsDBNull(10))
              account.Descriptive = dataReader.GetString(10);
          
            if(!dataReader.IsDBNull(11))
              account.Balance = dataReader.GetDecimal(11);
          
            if(!dataReader.IsDBNull(12))
              account.TotalBalance = dataReader.GetDecimal(12);
          

      return account;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Account] "

      
        + " Where "
        
          + " AccountId = @AccountId "
        
      ;
      public static void Delete(Account account)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@AccountId", account.AccountId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Account] ";

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
      
        + " AccountId, "
        + " QuickBooksListId, "
        + " EntityStateId, "
        + " EditSequence, "
        + " Name, "
        + " FullName, "
        + " AccountTypeId, "
        + " DetailAccountTypeId, "
        + " AccountNumber, "
        + " LastCheckNumber, "
        + " Descriptive, "
        + " Balance, "
        + " TotalBalance "
        + " From [Account] ";
      public static List<Account> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Account> rv = new List<Account>();

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
      List<Account> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Account> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Account));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Account item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Account>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Account));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Account> itemsList
      = new List<Account>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Account)
      itemsList.Add(deserializedObject as Account);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region AccountId
        protected int m_accountId;

			[XmlAttribute]
			public int AccountId
			{
			get { return m_accountId;}
			set { m_accountId = value; }
			}
		#endregion
		
		#region QuickBooksListId
        protected int? m_quickBooksListId;

			[XmlAttribute]
			public int? QuickBooksListId
			{
			get { return m_quickBooksListId;}
			set { m_quickBooksListId = value; }
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
		
		#region Name
        protected String m_name;

			[XmlAttribute]
			public String Name
			{
			get { return m_name;}
			set { m_name = value; }
			}
		#endregion
		
		#region FullName
        protected String m_fullName;

			[XmlAttribute]
			public String FullName
			{
			get { return m_fullName;}
			set { m_fullName = value; }
			}
		#endregion
		
		#region AccountNumber
        protected String m_accountNumber;

			[XmlAttribute]
			public String AccountNumber
			{
			get { return m_accountNumber;}
			set { m_accountNumber = value; }
			}
		#endregion
		
		#region LastCheckNumber
        protected String m_lastCheckNumber;

			[XmlAttribute]
			public String LastCheckNumber
			{
			get { return m_lastCheckNumber;}
			set { m_lastCheckNumber = value; }
			}
		#endregion
		
		#region Descriptive
        protected String m_descriptive;

			[XmlAttribute]
			public String Descriptive
			{
			get { return m_descriptive;}
			set { m_descriptive = value; }
			}
		#endregion
		
		#region Balance
        protected decimal? m_balance;

			[XmlAttribute]
			public decimal? Balance
			{
			get { return m_balance;}
			set { m_balance = value; }
			}
		#endregion
		
		#region TotalBalance
        protected decimal? m_totalBalance;

			[XmlAttribute]
			public decimal? TotalBalance
			{
			get { return m_totalBalance;}
			set { m_totalBalance = value; }
			}
		#endregion
		
		#region AccountType
			protected AccountType m_accountType;

			[XmlElement]
			public AccountType AccountType
			{
			get { return m_accountType;}
			set { m_accountType = value; }
			}
		#endregion
		
		#region DetailAccountType
			protected DetailAccountType m_detailAccountType;

			[XmlElement]
			public DetailAccountType DetailAccountType
			{
			get { return m_detailAccountType;}
			set { m_detailAccountType = value; }
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
		
		
		#endregion

      #region Constructors
      public Account(
		int accountId

		)
		{
		
			m_accountId = accountId;
		
        }

      


        public Account(
		  AccountType accountType,DetailAccountType detailAccountType,EntityState entityState
			  ,
		  int accountId,int? quickBooksListId,int editSequence,String name,String fullName,String accountNumber,String lastCheckNumber,String descriptive,decimal? balance,decimal? totalBalance
		  )
		  {

		  
			  m_accountType = accountType;
		  
			  m_detailAccountType = detailAccountType;
		  
			  m_entityState = entityState;
		  
			  m_accountId = accountId;
		  
			  m_quickBooksListId = quickBooksListId;
		  
			  m_editSequence = editSequence;
		  
			  m_name = name;
		  
			  m_fullName = fullName;
		  
			  m_accountNumber = accountNumber;
		  
			  m_lastCheckNumber = lastCheckNumber;
		  
			  m_descriptive = descriptive;
		  
			  m_balance = balance;
		  
			  m_totalBalance = totalBalance;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    