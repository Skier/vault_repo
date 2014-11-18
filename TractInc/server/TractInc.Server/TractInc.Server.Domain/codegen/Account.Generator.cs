
    using System;
    using System.Data;
    using System.Collections.Generic;
    using TractInc.Server.Data;
    using TractInc.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace TractInc.Server.Domain
      {


      public partial class Account
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Account] ( " +
      
        " AccountName, " +
      
        " ParentAccountId, " +
      
        " AccountTypeId, " +
      
        " CompanyId, " +
      
        " ClientId " +
      
      ") Values (" +
      
        " @AccountName, " +
      
        " @ParentAccountId, " +
      
        " @AccountTypeId, " +
      
        " @CompanyId, " +
      
        " @ClientId " +
      
      ")";

      public static void Insert(Account account)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@AccountName", account.AccountName);
      
          Database.PutParameter(dbCommand,"@ParentAccountId", (0 != account.ParentAccountId ? account.ParentAccountId : null));
      
        Database.PutParameter(dbCommand,"@AccountTypeId", account.AccountTypeId);
      
        Database.PutParameter(dbCommand,"@CompanyId", account.CompanyId);
      
        Database.PutParameter(dbCommand,"@ClientId", account.ClientId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          account.AccountId = (int)identValue;
        }        
      

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
      
        Database.PutParameter(dbCommand,"@AccountName", account.AccountName);
      
        Database.PutParameter(dbCommand,"@ParentAccountId", account.ParentAccountId);
      
        Database.PutParameter(dbCommand,"@AccountTypeId", account.AccountTypeId);
      
        Database.PutParameter(dbCommand,"@CompanyId", account.CompanyId);
      
        Database.PutParameter(dbCommand,"@ClientId", account.ClientId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@AccountName",account.AccountName);
      
        Database.UpdateParameter(dbCommand,"@ParentAccountId",account.ParentAccountId);
      
        Database.UpdateParameter(dbCommand,"@AccountTypeId",account.AccountTypeId);
      
        Database.UpdateParameter(dbCommand,"@CompanyId",account.CompanyId);
      
        Database.UpdateParameter(dbCommand,"@ClientId",account.ClientId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        account.AccountId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Account] Set "
      
        + " AccountName = @AccountName, "
      
        + " ParentAccountId = @ParentAccountId, "
      
        + " AccountTypeId = @AccountTypeId, "
      
        + " CompanyId = @CompanyId, "
      
        + " ClientId = @ClientId "
      
        + " Where "
        
          + " AccountId = @AccountId "
        
      ;

      public static void Update(Account account)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@AccountId", account.AccountId);
      
        Database.PutParameter(dbCommand,"@AccountName", account.AccountName);
      
        Database.PutParameter(dbCommand,"@ParentAccountId", (0 != account.ParentAccountId ? account.ParentAccountId : null));
      
        Database.PutParameter(dbCommand,"@AccountTypeId", account.AccountTypeId);
      
        Database.PutParameter(dbCommand,"@CompanyId", account.CompanyId);
      
        Database.PutParameter(dbCommand,"@ClientId", account.ClientId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " AccountId, "
      
        + " AccountName, "
      
        + " ParentAccountId, "
      
        + " AccountTypeId, "
      
        + " CompanyId, "
      
        + " ClientId "
      

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
      String sql = "select 1 from Account";

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
          account.AccountName = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            account.ParentAccountId = dataReader.GetInt32(2);
          account.AccountTypeId = dataReader.GetInt32(3);
          account.CompanyId = dataReader.GetInt32(4);
          account.ClientId = dataReader.GetInt32(5);
          

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
      
        + " AccountName, "
      
        + " ParentAccountId, "
      
        + " AccountTypeId, "
      
        + " CompanyId, "
      
        + " ClientId "
      

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
      
        protected int m_accountId;
      
        protected String m_accountName;
      
        protected int? m_parentAccountId;
      
        protected int m_accountTypeId;
      
        protected int m_companyId;
      
        protected int m_clientId;
      
      #endregion

      #region Constructors
      public Account(
      int 
          accountId
      )
      {
      
        m_accountId = accountId;
      
      }

      


        public Account(
        int 
          accountId,String 
          accountName,int? 
          parentAccountId,int 
          accountTypeId,int 
          companyId,int 
          clientId
        )
        {
        
          m_accountId = accountId;
        
          m_accountName = accountName;
        
          m_parentAccountId = parentAccountId;
        
          m_accountTypeId = accountTypeId;
        
          m_companyId = companyId;
        
          m_clientId = clientId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int AccountId
        {
        get { return m_accountId;}
        set { m_accountId = value; }
        }
      
        [XmlElement]
        public String AccountName
        {
        get { return m_accountName;}
        set { m_accountName = value; }
        }
      
        [XmlElement]
        public int? ParentAccountId
        {
        get { return m_parentAccountId;}
        set { m_parentAccountId = value; }
        }
      
        [XmlElement]
        public int AccountTypeId
        {
        get { return m_accountTypeId;}
        set { m_accountTypeId = value; }
        }
      
        [XmlElement]
        public int CompanyId
        {
        get { return m_companyId;}
        set { m_companyId = value; }
        }
      
        [XmlElement]
        public int ClientId
        {
        get { return m_clientId;}
        set { m_clientId = value; }
        }
      
      }
      #endregion
      }

    