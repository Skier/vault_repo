
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class InvoiceTransactionLineDetail
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [InvoiceTransactionLineDetail] ( " +
      
        " InvoiceTransactionLineDetailId, " +
        " InvoiceTransactionId, " +
        " QuickBooksTxnLineID, " +
        " Amount " +
        ") Values (" +
      
        " @InvoiceTransactionLineDetailId, " +
        " @InvoiceTransactionId, " +
        " @QuickBooksTxnLineID, " +
        " @Amount " +
      ")";

      public static void Insert(InvoiceTransactionLineDetail invoiceTransactionLineDetail)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@InvoiceTransactionLineDetailId", invoiceTransactionLineDetail.InvoiceTransactionLineDetailId);            
          
              Database.PutParameter(dbCommand,"@InvoiceTransactionId", invoiceTransactionLineDetail
			.InvoiceTransaction.InvoiceTransactionId);            
          
              Database.PutParameter(dbCommand,"@QuickBooksTxnLineID", invoiceTransactionLineDetail.QuickBooksTxnLineID);            
          
              Database.PutParameter(dbCommand,"@Amount", invoiceTransactionLineDetail.Amount);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<InvoiceTransactionLineDetail>  invoiceTransactionLineDetailList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(InvoiceTransactionLineDetail invoiceTransactionLineDetail in  invoiceTransactionLineDetailList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@InvoiceTransactionLineDetailId", invoiceTransactionLineDetail.InvoiceTransactionLineDetailId);
          
            Database.PutParameter(dbCommand,"@InvoiceTransactionId", invoiceTransactionLineDetail
			.InvoiceTransaction.InvoiceTransactionId);
          
            Database.PutParameter(dbCommand,"@QuickBooksTxnLineID", invoiceTransactionLineDetail.QuickBooksTxnLineID);
          
            Database.PutParameter(dbCommand,"@Amount", invoiceTransactionLineDetail.Amount);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@InvoiceTransactionLineDetailId",invoiceTransactionLineDetail.InvoiceTransactionLineDetailId);
          
            Database.UpdateParameter(dbCommand,"@InvoiceTransactionId",invoiceTransactionLineDetail
			.InvoiceTransaction.InvoiceTransactionId);
          
            Database.UpdateParameter(dbCommand,"@QuickBooksTxnLineID",invoiceTransactionLineDetail.QuickBooksTxnLineID);
          
            Database.UpdateParameter(dbCommand,"@Amount",invoiceTransactionLineDetail.Amount);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [InvoiceTransactionLineDetail] Set "
      
        + " InvoiceTransactionId = @InvoiceTransactionId, "
        + " QuickBooksTxnLineID = @QuickBooksTxnLineID, "
        + " Amount = @Amount "
        + " Where "
        
          + " InvoiceTransactionLineDetailId = @InvoiceTransactionLineDetailId "
        
      ;

      public static void Update(InvoiceTransactionLineDetail invoiceTransactionLineDetail)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@InvoiceTransactionLineDetailId", invoiceTransactionLineDetail.InvoiceTransactionLineDetailId);            
          
            Database.PutParameter(dbCommand,"@InvoiceTransactionId", invoiceTransactionLineDetail
			.InvoiceTransaction.InvoiceTransactionId);            
          
            Database.PutParameter(dbCommand,"@QuickBooksTxnLineID", invoiceTransactionLineDetail.QuickBooksTxnLineID);            
          
            Database.PutParameter(dbCommand,"@Amount", invoiceTransactionLineDetail.Amount);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " InvoiceTransactionLineDetailId, "
        + " InvoiceTransactionId, "
        + " QuickBooksTxnLineID, "
        + " Amount "
        + " From [InvoiceTransactionLineDetail] "
      
        + " Where "
        
        + " InvoiceTransactionLineDetailId = @InvoiceTransactionLineDetailId "
        
      ;

      public static InvoiceTransactionLineDetail FindByPrimaryKey(
      int invoiceTransactionLineDetailId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InvoiceTransactionLineDetailId", invoiceTransactionLineDetailId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("InvoiceTransactionLineDetail not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(InvoiceTransactionLineDetail invoiceTransactionLineDetail)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InvoiceTransactionLineDetailId",invoiceTransactionLineDetail.InvoiceTransactionLineDetailId);
      

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
      String sql = "select 1 from [InvoiceTransactionLineDetail]";

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

      public static InvoiceTransactionLineDetail Load(IDataReader dataReader)
      {
      InvoiceTransactionLineDetail invoiceTransactionLineDetail = new InvoiceTransactionLineDetail();

      invoiceTransactionLineDetail.InvoiceTransactionLineDetailId = dataReader.GetInt32(0);
          invoiceTransactionLineDetail
			.InvoiceTransaction = new InvoiceTransaction();

            invoiceTransactionLineDetail
			.InvoiceTransaction.InvoiceTransactionId = dataReader.GetInt32(1);
          invoiceTransactionLineDetail.QuickBooksTxnLineID = dataReader.GetInt32(2);
          invoiceTransactionLineDetail.Amount = dataReader.GetDecimal(3);
          

      return invoiceTransactionLineDetail;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [InvoiceTransactionLineDetail] "

      
        + " Where "
        
          + " InvoiceTransactionLineDetailId = @InvoiceTransactionLineDetailId "
        
      ;
      public static void Delete(InvoiceTransactionLineDetail invoiceTransactionLineDetail)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@InvoiceTransactionLineDetailId", invoiceTransactionLineDetail.InvoiceTransactionLineDetailId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [InvoiceTransactionLineDetail] ";

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
      
        + " InvoiceTransactionLineDetailId, "
        + " InvoiceTransactionId, "
        + " QuickBooksTxnLineID, "
        + " Amount "
        + " From [InvoiceTransactionLineDetail] ";
      public static List<InvoiceTransactionLineDetail> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<InvoiceTransactionLineDetail> rv = new List<InvoiceTransactionLineDetail>();

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
      List<InvoiceTransactionLineDetail> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<InvoiceTransactionLineDetail> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(InvoiceTransactionLineDetail));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(InvoiceTransactionLineDetail item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<InvoiceTransactionLineDetail>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(InvoiceTransactionLineDetail));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<InvoiceTransactionLineDetail> itemsList
      = new List<InvoiceTransactionLineDetail>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is InvoiceTransactionLineDetail)
      itemsList.Add(deserializedObject as InvoiceTransactionLineDetail);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region InvoiceTransactionLineDetailId
        protected int m_invoiceTransactionLineDetailId;

			[XmlAttribute]
			public int InvoiceTransactionLineDetailId
			{
			get { return m_invoiceTransactionLineDetailId;}
			set { m_invoiceTransactionLineDetailId = value; }
			}
		#endregion
		
		#region QuickBooksTxnLineID
        protected int m_quickBooksTxnLineID;

			[XmlAttribute]
			public int QuickBooksTxnLineID
			{
			get { return m_quickBooksTxnLineID;}
			set { m_quickBooksTxnLineID = value; }
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
		
		#region InvoiceTransaction
			protected InvoiceTransaction m_invoiceTransaction;

			[XmlElement]
			public InvoiceTransaction InvoiceTransaction
			{
			get { return m_invoiceTransaction;}
			set { m_invoiceTransaction = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public InvoiceTransactionLineDetail(
		int invoiceTransactionLineDetailId

		)
		{
		
			m_invoiceTransactionLineDetailId = invoiceTransactionLineDetailId;
		
        }

      


        public InvoiceTransactionLineDetail(
		  InvoiceTransaction invoiceTransaction
			  ,
		  int invoiceTransactionLineDetailId,int quickBooksTxnLineID,decimal amount
		  )
		  {

		  
			  m_invoiceTransaction = invoiceTransaction;
		  
			  m_invoiceTransactionLineDetailId = invoiceTransactionLineDetailId;
		  
			  m_quickBooksTxnLineID = quickBooksTxnLineID;
		  
			  m_amount = amount;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    