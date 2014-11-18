
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class CheckExpenceLine
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [CheckExpenceLine] ( " +
      
        " CheckExpenceLineId, " +
        " CheckId, " +
        " TxnLineID, " +
        " AccountId, " +
        " Amount, " +
        " Memo, " +
        " CustomerId " +
        ") Values (" +
      
        " @CheckExpenceLineId, " +
        " @CheckId, " +
        " @TxnLineID, " +
        " @AccountId, " +
        " @Amount, " +
        " @Memo, " +
        " @CustomerId " +
      ")";

      public static void Insert(CheckExpenceLine checkExpenceLine)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@CheckExpenceLineId", checkExpenceLine.CheckExpenceLineId);            
          
              Database.PutParameter(dbCommand,"@CheckId", checkExpenceLine
			.Check.CheckId);            
          
              Database.PutParameter(dbCommand,"@TxnLineID", checkExpenceLine.TxnLineID);            
          
            if(checkExpenceLine
			.Account == null)
            {
            Database.PutParameter(dbCommand,"@AccountId", DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@AccountId", checkExpenceLine
			.Account.AccountId);
            }
          
              Database.PutParameter(dbCommand,"@Amount", checkExpenceLine.Amount);            
          
              Database.PutParameter(dbCommand,"@Memo", checkExpenceLine.Memo);            
          
            if(checkExpenceLine
			.Customer == null)
            {
            Database.PutParameter(dbCommand,"@CustomerId", DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@CustomerId", checkExpenceLine
			.Customer.CustomerId);
            }
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<CheckExpenceLine>  checkExpenceLineList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(CheckExpenceLine checkExpenceLine in  checkExpenceLineList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@CheckExpenceLineId", checkExpenceLine.CheckExpenceLineId);
          
            Database.PutParameter(dbCommand,"@CheckId", checkExpenceLine
			.Check.CheckId);
          
            Database.PutParameter(dbCommand,"@TxnLineID", checkExpenceLine.TxnLineID);
          
            if(checkExpenceLine
			.Account == null)
            {
              Database.PutParameter(dbCommand,"@AccountId", DbType.Int32);
            }
            else
            {
              Database.PutParameter(dbCommand,"@AccountId", checkExpenceLine
			.Account.AccountId);
            }
          
            Database.PutParameter(dbCommand,"@Amount", checkExpenceLine.Amount);
          
            Database.PutParameter(dbCommand,"@Memo", checkExpenceLine.Memo);
          
            if(checkExpenceLine
			.Customer == null)
            {
              Database.PutParameter(dbCommand,"@CustomerId", DbType.Int32);
            }
            else
            {
              Database.PutParameter(dbCommand,"@CustomerId", checkExpenceLine
			.Customer.CustomerId);
            }
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@CheckExpenceLineId",checkExpenceLine.CheckExpenceLineId);
          
            Database.UpdateParameter(dbCommand,"@CheckId",checkExpenceLine
			.Check.CheckId);
          
            Database.UpdateParameter(dbCommand,"@TxnLineID",checkExpenceLine.TxnLineID);
          
            if(checkExpenceLine
			.Account == null)
            {
             Database.UpdateParameter(dbCommand,"@AccountId",DbType.Int32);
            }
            else
            {
            Database.UpdateParameter(dbCommand,"@AccountId",checkExpenceLine
			.Account.AccountId);
            }
          
            Database.UpdateParameter(dbCommand,"@Amount",checkExpenceLine.Amount);
          
            Database.UpdateParameter(dbCommand,"@Memo",checkExpenceLine.Memo);
          
            if(checkExpenceLine
			.Customer == null)
            {
             Database.UpdateParameter(dbCommand,"@CustomerId",DbType.Int32);
            }
            else
            {
            Database.UpdateParameter(dbCommand,"@CustomerId",checkExpenceLine
			.Customer.CustomerId);
            }
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [CheckExpenceLine] Set "
      
        + " CheckId = @CheckId, "
        + " TxnLineID = @TxnLineID, "
        + " AccountId = @AccountId, "
        + " Amount = @Amount, "
        + " Memo = @Memo, "
        + " CustomerId = @CustomerId "
        + " Where "
        
          + " CheckExpenceLineId = @CheckExpenceLineId "
        
      ;

      public static void Update(CheckExpenceLine checkExpenceLine)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@CheckExpenceLineId", checkExpenceLine.CheckExpenceLineId);            
          
            Database.PutParameter(dbCommand,"@CheckId", checkExpenceLine
			.Check.CheckId);            
          
            Database.PutParameter(dbCommand,"@TxnLineID", checkExpenceLine.TxnLineID);            
          
            if(checkExpenceLine
			.Account == null)
            {
            Database.PutParameter(dbCommand,"@AccountId",DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@AccountId",checkExpenceLine
			.Account.AccountId);
            }
          
            Database.PutParameter(dbCommand,"@Amount", checkExpenceLine.Amount);            
          
            Database.PutParameter(dbCommand,"@Memo", checkExpenceLine.Memo);            
          
            if(checkExpenceLine
			.Customer == null)
            {
            Database.PutParameter(dbCommand,"@CustomerId",DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@CustomerId",checkExpenceLine
			.Customer.CustomerId);
            }
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " CheckExpenceLineId, "
        + " CheckId, "
        + " TxnLineID, "
        + " AccountId, "
        + " Amount, "
        + " Memo, "
        + " CustomerId "
        + " From [CheckExpenceLine] "
      
        + " Where "
        
        + " CheckExpenceLineId = @CheckExpenceLineId "
        
      ;

      public static CheckExpenceLine FindByPrimaryKey(
      int checkExpenceLineId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CheckExpenceLineId", checkExpenceLineId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("CheckExpenceLine not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(CheckExpenceLine checkExpenceLine)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CheckExpenceLineId",checkExpenceLine.CheckExpenceLineId);
      

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
      String sql = "select 1 from [CheckExpenceLine]";

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

      public static CheckExpenceLine Load(IDataReader dataReader)
      {
      CheckExpenceLine checkExpenceLine = new CheckExpenceLine();

      checkExpenceLine.CheckExpenceLineId = dataReader.GetInt32(0);
          checkExpenceLine
			.Check = new Check();

            checkExpenceLine
			.Check.CheckId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
              checkExpenceLine.TxnLineID = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            {
            checkExpenceLine
			.Account = new Account();
            
            checkExpenceLine
			.Account.AccountId = dataReader.GetInt32(3);
           }
            else
            checkExpenceLine
			.Account = null;
          
            if(!dataReader.IsDBNull(4))
              checkExpenceLine.Amount = dataReader.GetDecimal(4);
          
            if(!dataReader.IsDBNull(5))
              checkExpenceLine.Memo = dataReader.GetString(5);
          
            if(!dataReader.IsDBNull(6))
            {
            checkExpenceLine
			.Customer = new Customer();
            
            checkExpenceLine
			.Customer.CustomerId = dataReader.GetInt32(6);
           }
            else
            checkExpenceLine
			.Customer = null;
          

      return checkExpenceLine;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [CheckExpenceLine] "

      
        + " Where "
        
          + " CheckExpenceLineId = @CheckExpenceLineId "
        
      ;
      public static void Delete(CheckExpenceLine checkExpenceLine)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@CheckExpenceLineId", checkExpenceLine.CheckExpenceLineId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [CheckExpenceLine] ";

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
      
        + " CheckExpenceLineId, "
        + " CheckId, "
        + " TxnLineID, "
        + " AccountId, "
        + " Amount, "
        + " Memo, "
        + " CustomerId "
        + " From [CheckExpenceLine] ";
      public static List<CheckExpenceLine> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<CheckExpenceLine> rv = new List<CheckExpenceLine>();

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
      List<CheckExpenceLine> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<CheckExpenceLine> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CheckExpenceLine));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(CheckExpenceLine item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CheckExpenceLine>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CheckExpenceLine));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<CheckExpenceLine> itemsList
      = new List<CheckExpenceLine>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CheckExpenceLine)
      itemsList.Add(deserializedObject as CheckExpenceLine);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region CheckExpenceLineId
        protected int m_checkExpenceLineId;

			[XmlAttribute]
			public int CheckExpenceLineId
			{
			get { return m_checkExpenceLineId;}
			set { m_checkExpenceLineId = value; }
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
		
		#region Check
			protected Check m_check;

			[XmlElement]
			public Check Check
			{
			get { return m_check;}
			set { m_check = value; }
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
      public CheckExpenceLine(
		int checkExpenceLineId

		)
		{
		
			m_checkExpenceLineId = checkExpenceLineId;
		
        }

      


        public CheckExpenceLine(
		  Account account,Check check,Customer customer
			  ,
		  int checkExpenceLineId,int? txnLineID,decimal? amount,String memo
		  )
		  {

		  
			  m_account = account;
		  
			  m_check = check;
		  
			  m_customer = customer;
		  
			  m_checkExpenceLineId = checkExpenceLineId;
		  
			  m_txnLineID = txnLineID;
		  
			  m_amount = amount;
		  
			  m_memo = memo;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    