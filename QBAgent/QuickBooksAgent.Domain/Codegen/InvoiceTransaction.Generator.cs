
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class InvoiceTransaction
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [InvoiceTransaction] ( " +
      
        " InvoiceTransactionId, " +
        " InvoiceId, " +
        " QuickBooksTxnId, " +
        " TransactionTypeId, " +
        " TransactionDate, " +
        " RefNumber, " +
        " Amount " +
        ") Values (" +
      
        " @InvoiceTransactionId, " +
        " @InvoiceId, " +
        " @QuickBooksTxnId, " +
        " @TransactionTypeId, " +
        " @TransactionDate, " +
        " @RefNumber, " +
        " @Amount " +
      ")";

      public static void Insert(InvoiceTransaction invoiceTransaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@InvoiceTransactionId", invoiceTransaction.InvoiceTransactionId);            
          
              Database.PutParameter(dbCommand,"@InvoiceId", invoiceTransaction
			.Invoice.InvoiceId);            
          
              Database.PutParameter(dbCommand,"@QuickBooksTxnId", invoiceTransaction.QuickBooksTxnId);            
          
              Database.PutParameter(dbCommand,"@TransactionTypeId", invoiceTransaction
			.TransactionType.TransactionTypeId);            
          
              Database.PutParameter(dbCommand,"@TransactionDate", invoiceTransaction.TransactionDate);            
          
              Database.PutParameter(dbCommand,"@RefNumber", invoiceTransaction.RefNumber);            
          
              Database.PutParameter(dbCommand,"@Amount", invoiceTransaction.Amount);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<InvoiceTransaction>  invoiceTransactionList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(InvoiceTransaction invoiceTransaction in  invoiceTransactionList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@InvoiceTransactionId", invoiceTransaction.InvoiceTransactionId);
          
            Database.PutParameter(dbCommand,"@InvoiceId", invoiceTransaction
			.Invoice.InvoiceId);
          
            Database.PutParameter(dbCommand,"@QuickBooksTxnId", invoiceTransaction.QuickBooksTxnId);
          
            Database.PutParameter(dbCommand,"@TransactionTypeId", invoiceTransaction
			.TransactionType.TransactionTypeId);
          
            Database.PutParameter(dbCommand,"@TransactionDate", invoiceTransaction.TransactionDate);
          
            Database.PutParameter(dbCommand,"@RefNumber", invoiceTransaction.RefNumber);
          
            Database.PutParameter(dbCommand,"@Amount", invoiceTransaction.Amount);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@InvoiceTransactionId",invoiceTransaction.InvoiceTransactionId);
          
            Database.UpdateParameter(dbCommand,"@InvoiceId",invoiceTransaction
			.Invoice.InvoiceId);
          
            Database.UpdateParameter(dbCommand,"@QuickBooksTxnId",invoiceTransaction.QuickBooksTxnId);
          
            Database.UpdateParameter(dbCommand,"@TransactionTypeId",invoiceTransaction
			.TransactionType.TransactionTypeId);
          
            Database.UpdateParameter(dbCommand,"@TransactionDate",invoiceTransaction.TransactionDate);
          
            Database.UpdateParameter(dbCommand,"@RefNumber",invoiceTransaction.RefNumber);
          
            Database.UpdateParameter(dbCommand,"@Amount",invoiceTransaction.Amount);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [InvoiceTransaction] Set "
      
        + " InvoiceId = @InvoiceId, "
        + " QuickBooksTxnId = @QuickBooksTxnId, "
        + " TransactionTypeId = @TransactionTypeId, "
        + " TransactionDate = @TransactionDate, "
        + " RefNumber = @RefNumber, "
        + " Amount = @Amount "
        + " Where "
        
          + " InvoiceTransactionId = @InvoiceTransactionId "
        
      ;

      public static void Update(InvoiceTransaction invoiceTransaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@InvoiceTransactionId", invoiceTransaction.InvoiceTransactionId);            
          
            Database.PutParameter(dbCommand,"@InvoiceId", invoiceTransaction
			.Invoice.InvoiceId);            
          
            Database.PutParameter(dbCommand,"@QuickBooksTxnId", invoiceTransaction.QuickBooksTxnId);            
          
            Database.PutParameter(dbCommand,"@TransactionTypeId", invoiceTransaction
			.TransactionType.TransactionTypeId);            
          
            Database.PutParameter(dbCommand,"@TransactionDate", invoiceTransaction.TransactionDate);            
          
            Database.PutParameter(dbCommand,"@RefNumber", invoiceTransaction.RefNumber);            
          
            Database.PutParameter(dbCommand,"@Amount", invoiceTransaction.Amount);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " InvoiceTransactionId, "
        + " InvoiceId, "
        + " QuickBooksTxnId, "
        + " TransactionTypeId, "
        + " TransactionDate, "
        + " RefNumber, "
        + " Amount "
        + " From [InvoiceTransaction] "
      
        + " Where "
        
        + " InvoiceTransactionId = @InvoiceTransactionId "
        
      ;

      public static InvoiceTransaction FindByPrimaryKey(
      int invoiceTransactionId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InvoiceTransactionId", invoiceTransactionId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("InvoiceTransaction not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(InvoiceTransaction invoiceTransaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InvoiceTransactionId",invoiceTransaction.InvoiceTransactionId);
      

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
      String sql = "select 1 from [InvoiceTransaction]";

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

      public static InvoiceTransaction Load(IDataReader dataReader)
      {
      InvoiceTransaction invoiceTransaction = new InvoiceTransaction();

      invoiceTransaction.InvoiceTransactionId = dataReader.GetInt32(0);
          invoiceTransaction
			.Invoice = new Invoice();

            invoiceTransaction
			.Invoice.InvoiceId = dataReader.GetInt32(1);
          invoiceTransaction.QuickBooksTxnId = dataReader.GetInt32(2);
          invoiceTransaction
			.TransactionType = new TransactionType();

            invoiceTransaction
			.TransactionType.TransactionTypeId = dataReader.GetInt32(3);
          invoiceTransaction.TransactionDate = dataReader.GetDateTime(4);
          
            if(!dataReader.IsDBNull(5))
              invoiceTransaction.RefNumber = dataReader.GetString(5);
          invoiceTransaction.Amount = dataReader.GetDecimal(6);
          

      return invoiceTransaction;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [InvoiceTransaction] "

      
        + " Where "
        
          + " InvoiceTransactionId = @InvoiceTransactionId "
        
      ;
      public static void Delete(InvoiceTransaction invoiceTransaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@InvoiceTransactionId", invoiceTransaction.InvoiceTransactionId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [InvoiceTransaction] ";

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
      
        + " InvoiceTransactionId, "
        + " InvoiceId, "
        + " QuickBooksTxnId, "
        + " TransactionTypeId, "
        + " TransactionDate, "
        + " RefNumber, "
        + " Amount "
        + " From [InvoiceTransaction] ";
      public static List<InvoiceTransaction> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<InvoiceTransaction> rv = new List<InvoiceTransaction>();

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
      List<InvoiceTransaction> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<InvoiceTransaction> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(InvoiceTransaction));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(InvoiceTransaction item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<InvoiceTransaction>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(InvoiceTransaction));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<InvoiceTransaction> itemsList
      = new List<InvoiceTransaction>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is InvoiceTransaction)
      itemsList.Add(deserializedObject as InvoiceTransaction);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region InvoiceTransactionId
        protected int m_invoiceTransactionId;

			[XmlAttribute]
			public int InvoiceTransactionId
			{
			get { return m_invoiceTransactionId;}
			set { m_invoiceTransactionId = value; }
			}
		#endregion
		
		#region QuickBooksTxnId
        protected int m_quickBooksTxnId;

			[XmlAttribute]
			public int QuickBooksTxnId
			{
			get { return m_quickBooksTxnId;}
			set { m_quickBooksTxnId = value; }
			}
		#endregion
		
		#region TransactionDate
        protected DateTime m_transactionDate;

			[XmlAttribute]
			public DateTime TransactionDate
			{
			get { return m_transactionDate;}
			set { m_transactionDate = value; }
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
        protected decimal m_amount;

			[XmlAttribute]
			public decimal Amount
			{
			get { return m_amount;}
			set { m_amount = value; }
			}
		#endregion
		
		#region Invoice
			protected Invoice m_invoice;

			[XmlElement]
			public Invoice Invoice
			{
			get { return m_invoice;}
			set { m_invoice = value; }
			}
		#endregion
		
		#region TransactionType
			protected TransactionType m_transactionType;

			[XmlElement]
			public TransactionType TransactionType
			{
			get { return m_transactionType;}
			set { m_transactionType = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public InvoiceTransaction(
		int invoiceTransactionId

		)
		{
		
			m_invoiceTransactionId = invoiceTransactionId;
		
        }

      


        public InvoiceTransaction(
		  Invoice invoice,TransactionType transactionType
			  ,
		  int invoiceTransactionId,int quickBooksTxnId,DateTime transactionDate,String refNumber,decimal amount
		  )
		  {

		  
			  m_invoice = invoice;
		  
			  m_transactionType = transactionType;
		  
			  m_invoiceTransactionId = invoiceTransactionId;
		  
			  m_quickBooksTxnId = quickBooksTxnId;
		  
			  m_transactionDate = transactionDate;
		  
			  m_refNumber = refNumber;
		  
			  m_amount = amount;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    