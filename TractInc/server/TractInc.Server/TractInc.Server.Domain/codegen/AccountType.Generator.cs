
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


      public partial class AccountType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [AccountType] ( " +
      
        " TypeName " +
      
      ") Values (" +
      
        " @TypeName " +
      
      ")";

      public static void Insert(AccountType accountType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@TypeName", accountType.TypeName);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          accountType.AccountTypeId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<AccountType>  accountTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(AccountType accountType in  accountTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@TypeName", accountType.TypeName);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@TypeName",accountType.TypeName);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        accountType.AccountTypeId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [AccountType] Set "
      
        + " TypeName = @TypeName "
      
        + " Where "
        
          + " AccountTypeId = @AccountTypeId "
        
      ;

      public static void Update(AccountType accountType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@AccountTypeId", accountType.AccountTypeId);
      
        Database.PutParameter(dbCommand,"@TypeName", accountType.TypeName);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " AccountTypeId, "
      
        + " TypeName "
      

      + " From [AccountType] "

      
        + " Where "
        
          + " AccountTypeId = @AccountTypeId "
        
      ;

      public static AccountType FindByPrimaryKey(
      int accountTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@AccountTypeId", accountTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("AccountType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(AccountType accountType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@AccountTypeId",accountType.AccountTypeId);
      

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
      String sql = "select 1 from AccountType";

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

      public static AccountType Load(IDataReader dataReader)
      {
      AccountType accountType = new AccountType();

      accountType.AccountTypeId = dataReader.GetInt32(0);
          accountType.TypeName = dataReader.GetString(1);
          

      return accountType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [AccountType] "

      
        + " Where "
        
          + " AccountTypeId = @AccountTypeId "
        
      ;
      public static void Delete(AccountType accountType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@AccountTypeId", accountType.AccountTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [AccountType] ";

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

      
        + " AccountTypeId, "
      
        + " TypeName "
      

      + " From [AccountType] ";
      public static List<AccountType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<AccountType> rv = new List<AccountType>();

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
      List<AccountType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<AccountType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(AccountType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(AccountType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<AccountType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(AccountType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<AccountType> itemsList
      = new List<AccountType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is AccountType)
      itemsList.Add(deserializedObject as AccountType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_accountTypeId;
      
        protected String m_typeName;
      
      #endregion

      #region Constructors
      public AccountType(
      int 
          accountTypeId
      )
      {
      
        m_accountTypeId = accountTypeId;
      
      }

      


        public AccountType(
        int 
          accountTypeId,String 
          typeName
        )
        {
        
          m_accountTypeId = accountTypeId;
        
          m_typeName = typeName;
        
        }


      
      #endregion

      
        [XmlElement]
        public int AccountTypeId
        {
        get { return m_accountTypeId;}
        set { m_accountTypeId = value; }
        }
      
        [XmlElement]
        public String TypeName
        {
        get { return m_typeName;}
        set { m_typeName = value; }
        }
      
      }
      #endregion
      }

    