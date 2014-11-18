
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class InvoiceLine
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [InvoiceLine] ( " +
      
        " InvoiceLineId, " +
        " InvoiceId, " +
        " QuickBooksTxnLineId, " +
        " ItemId, " +
        " LineDescription, " +
        " Quantity, " +
        " Rate, " +
        " RatePercent, " +
        " Amount, " +
        " ServiceDate, " +
        " IsTaxable " +
        ") Values (" +
      
        " @InvoiceLineId, " +
        " @InvoiceId, " +
        " @QuickBooksTxnLineId, " +
        " @ItemId, " +
        " @LineDescription, " +
        " @Quantity, " +
        " @Rate, " +
        " @RatePercent, " +
        " @Amount, " +
        " @ServiceDate, " +
        " @IsTaxable " +
      ")";

      public static void Insert(InvoiceLine invoiceLine)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@InvoiceLineId", invoiceLine.InvoiceLineId);            
          
              Database.PutParameter(dbCommand,"@InvoiceId", invoiceLine
			.Invoice.InvoiceId);            
          
              Database.PutParameter(dbCommand,"@QuickBooksTxnLineId", invoiceLine.QuickBooksTxnLineId);            
          
            if(invoiceLine
			.Item == null)
            {
            Database.PutParameter(dbCommand,"@ItemId", DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@ItemId", invoiceLine
			.Item.ItemId);
            }
          
              Database.PutParameter(dbCommand,"@LineDescription", invoiceLine.LineDescription);            
          
              Database.PutParameter(dbCommand,"@Quantity", invoiceLine.Quantity);            
          
              Database.PutParameter(dbCommand,"@Rate", invoiceLine.Rate);            
          
              Database.PutParameter(dbCommand,"@RatePercent", invoiceLine.RatePercent);            
          
              Database.PutParameter(dbCommand,"@Amount", invoiceLine.Amount);            
          
              Database.PutParameter(dbCommand,"@ServiceDate", invoiceLine.ServiceDate);            
          
              Database.PutParameter(dbCommand,"@IsTaxable", invoiceLine.IsTaxable);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<InvoiceLine>  invoiceLineList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(InvoiceLine invoiceLine in  invoiceLineList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@InvoiceLineId", invoiceLine.InvoiceLineId);
          
            Database.PutParameter(dbCommand,"@InvoiceId", invoiceLine
			.Invoice.InvoiceId);
          
            Database.PutParameter(dbCommand,"@QuickBooksTxnLineId", invoiceLine.QuickBooksTxnLineId);
          
            if(invoiceLine
			.Item == null)
            {
              Database.PutParameter(dbCommand,"@ItemId", DbType.Int32);
            }
            else
            {
              Database.PutParameter(dbCommand,"@ItemId", invoiceLine
			.Item.ItemId);
            }
          
            Database.PutParameter(dbCommand,"@LineDescription", invoiceLine.LineDescription);
          
            Database.PutParameter(dbCommand,"@Quantity", invoiceLine.Quantity);
          
            Database.PutParameter(dbCommand,"@Rate", invoiceLine.Rate);
          
            Database.PutParameter(dbCommand,"@RatePercent", invoiceLine.RatePercent);
          
            Database.PutParameter(dbCommand,"@Amount", invoiceLine.Amount);
          
            Database.PutParameter(dbCommand,"@ServiceDate", invoiceLine.ServiceDate);
          
            Database.PutParameter(dbCommand,"@IsTaxable", invoiceLine.IsTaxable);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@InvoiceLineId",invoiceLine.InvoiceLineId);
          
            Database.UpdateParameter(dbCommand,"@InvoiceId",invoiceLine
			.Invoice.InvoiceId);
          
            Database.UpdateParameter(dbCommand,"@QuickBooksTxnLineId",invoiceLine.QuickBooksTxnLineId);
          
            if(invoiceLine
			.Item == null)
            {
             Database.UpdateParameter(dbCommand,"@ItemId",DbType.Int32);
            }
            else
            {
            Database.UpdateParameter(dbCommand,"@ItemId",invoiceLine
			.Item.ItemId);
            }
          
            Database.UpdateParameter(dbCommand,"@LineDescription",invoiceLine.LineDescription);
          
            Database.UpdateParameter(dbCommand,"@Quantity",invoiceLine.Quantity);
          
            Database.UpdateParameter(dbCommand,"@Rate",invoiceLine.Rate);
          
            Database.UpdateParameter(dbCommand,"@RatePercent",invoiceLine.RatePercent);
          
            Database.UpdateParameter(dbCommand,"@Amount",invoiceLine.Amount);
          
            Database.UpdateParameter(dbCommand,"@ServiceDate",invoiceLine.ServiceDate);
          
            Database.UpdateParameter(dbCommand,"@IsTaxable",invoiceLine.IsTaxable);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [InvoiceLine] Set "
      
        + " InvoiceId = @InvoiceId, "
        + " QuickBooksTxnLineId = @QuickBooksTxnLineId, "
        + " ItemId = @ItemId, "
        + " LineDescription = @LineDescription, "
        + " Quantity = @Quantity, "
        + " Rate = @Rate, "
        + " RatePercent = @RatePercent, "
        + " Amount = @Amount, "
        + " ServiceDate = @ServiceDate, "
        + " IsTaxable = @IsTaxable "
        + " Where "
        
          + " InvoiceLineId = @InvoiceLineId "
        
      ;

      public static void Update(InvoiceLine invoiceLine)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@InvoiceLineId", invoiceLine.InvoiceLineId);            
          
            Database.PutParameter(dbCommand,"@InvoiceId", invoiceLine
			.Invoice.InvoiceId);            
          
            Database.PutParameter(dbCommand,"@QuickBooksTxnLineId", invoiceLine.QuickBooksTxnLineId);            
          
            if(invoiceLine
			.Item == null)
            {
            Database.PutParameter(dbCommand,"@ItemId",DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@ItemId",invoiceLine
			.Item.ItemId);
            }
          
            Database.PutParameter(dbCommand,"@LineDescription", invoiceLine.LineDescription);            
          
            Database.PutParameter(dbCommand,"@Quantity", invoiceLine.Quantity);            
          
            Database.PutParameter(dbCommand,"@Rate", invoiceLine.Rate);            
          
            Database.PutParameter(dbCommand,"@RatePercent", invoiceLine.RatePercent);            
          
            Database.PutParameter(dbCommand,"@Amount", invoiceLine.Amount);            
          
            Database.PutParameter(dbCommand,"@ServiceDate", invoiceLine.ServiceDate);            
          
            Database.PutParameter(dbCommand,"@IsTaxable", invoiceLine.IsTaxable);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " InvoiceLineId, "
        + " InvoiceId, "
        + " QuickBooksTxnLineId, "
        + " ItemId, "
        + " LineDescription, "
        + " Quantity, "
        + " Rate, "
        + " RatePercent, "
        + " Amount, "
        + " ServiceDate, "
        + " IsTaxable "
        + " From [InvoiceLine] "
      
        + " Where "
        
        + " InvoiceLineId = @InvoiceLineId "
        
      ;

      public static InvoiceLine FindByPrimaryKey(
      int invoiceLineId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InvoiceLineId", invoiceLineId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("InvoiceLine not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(InvoiceLine invoiceLine)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InvoiceLineId",invoiceLine.InvoiceLineId);
      

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
      String sql = "select 1 from [InvoiceLine]";

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

      public static InvoiceLine Load(IDataReader dataReader)
      {
      InvoiceLine invoiceLine = new InvoiceLine();

      invoiceLine.InvoiceLineId = dataReader.GetInt32(0);
          invoiceLine
			.Invoice = new Invoice();

            invoiceLine
			.Invoice.InvoiceId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
              invoiceLine.QuickBooksTxnLineId = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            {
            invoiceLine
			.Item = new Item();
            
            invoiceLine
			.Item.ItemId = dataReader.GetInt32(3);
           }
            else
            invoiceLine
			.Item = null;
          
            if(!dataReader.IsDBNull(4))
              invoiceLine.LineDescription = dataReader.GetString(4);
          
            if(!dataReader.IsDBNull(5))
              invoiceLine.Quantity = dataReader.GetDecimal(5);
          
            if(!dataReader.IsDBNull(6))
              invoiceLine.Rate = dataReader.GetDecimal(6);
          
            if(!dataReader.IsDBNull(7))
              invoiceLine.RatePercent = dataReader.GetDecimal(7);
          
            if(!dataReader.IsDBNull(8))
              invoiceLine.Amount = dataReader.GetDecimal(8);
          
            if(!dataReader.IsDBNull(9))
              invoiceLine.ServiceDate = dataReader.GetDateTime(9);
          
            if(!dataReader.IsDBNull(10))
              invoiceLine.IsTaxable = dataReader.GetBoolean(10);
          

      return invoiceLine;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [InvoiceLine] "

      
        + " Where "
        
          + " InvoiceLineId = @InvoiceLineId "
        
      ;
      public static void Delete(InvoiceLine invoiceLine)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@InvoiceLineId", invoiceLine.InvoiceLineId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [InvoiceLine] ";

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
      
        + " InvoiceLineId, "
        + " InvoiceId, "
        + " QuickBooksTxnLineId, "
        + " ItemId, "
        + " LineDescription, "
        + " Quantity, "
        + " Rate, "
        + " RatePercent, "
        + " Amount, "
        + " ServiceDate, "
        + " IsTaxable "
        + " From [InvoiceLine] ";
      public static List<InvoiceLine> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<InvoiceLine> rv = new List<InvoiceLine>();

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
      List<InvoiceLine> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<InvoiceLine> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(InvoiceLine));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(InvoiceLine item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<InvoiceLine>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(InvoiceLine));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<InvoiceLine> itemsList
      = new List<InvoiceLine>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is InvoiceLine)
      itemsList.Add(deserializedObject as InvoiceLine);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region InvoiceLineId
        protected int m_invoiceLineId;

			[XmlAttribute]
			public int InvoiceLineId
			{
			get { return m_invoiceLineId;}
			set { m_invoiceLineId = value; }
			}
		#endregion
		
		#region QuickBooksTxnLineId
        protected int? m_quickBooksTxnLineId;

			[XmlAttribute]
			public int? QuickBooksTxnLineId
			{
			get { return m_quickBooksTxnLineId;}
			set { m_quickBooksTxnLineId = value; }
			}
		#endregion
		
		#region LineDescription
        protected String m_lineDescription;

			[XmlAttribute]
			public String LineDescription
			{
			get { return m_lineDescription;}
			set { m_lineDescription = value; }
			}
		#endregion
		
		#region Quantity
        protected decimal? m_quantity;

			[XmlAttribute]
			public decimal? Quantity
			{
			get { return m_quantity;}
			set { m_quantity = value; }
			}
		#endregion
		
		#region Rate
        protected decimal? m_rate;

			[XmlAttribute]
			public decimal? Rate
			{
			get { return m_rate;}
			set { m_rate = value; }
			}
		#endregion
		
		#region RatePercent
        protected decimal? m_ratePercent;

			[XmlAttribute]
			public decimal? RatePercent
			{
			get { return m_ratePercent;}
			set { m_ratePercent = value; }
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
		
		#region ServiceDate
        protected DateTime? m_serviceDate;

			[XmlAttribute]
			public DateTime? ServiceDate
			{
			get { return m_serviceDate;}
			set { m_serviceDate = value; }
			}
		#endregion
		
		#region IsTaxable
        protected bool m_isTaxable;

			[XmlAttribute]
			public bool IsTaxable
			{
			get { return m_isTaxable;}
			set { m_isTaxable = value; }
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
		
		#region Item
			protected Item m_item;

			[XmlElement]
			public Item Item
			{
			get { return m_item;}
			set { m_item = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public InvoiceLine(
		int invoiceLineId

		)
		{
		
			m_invoiceLineId = invoiceLineId;
		
        }

      


        public InvoiceLine(
		  Invoice invoice,Item item
			  ,
		  int invoiceLineId,int? quickBooksTxnLineId,String lineDescription,decimal? quantity,decimal? rate,decimal? ratePercent,decimal? amount,DateTime? serviceDate,bool isTaxable
		  )
		  {

		  
			  m_invoice = invoice;
		  
			  m_item = item;
		  
			  m_invoiceLineId = invoiceLineId;
		  
			  m_quickBooksTxnLineId = quickBooksTxnLineId;
		  
			  m_lineDescription = lineDescription;
		  
			  m_quantity = quantity;
		  
			  m_rate = rate;
		  
			  m_ratePercent = ratePercent;
		  
			  m_amount = amount;
		  
			  m_serviceDate = serviceDate;
		  
			  m_isTaxable = isTaxable;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    