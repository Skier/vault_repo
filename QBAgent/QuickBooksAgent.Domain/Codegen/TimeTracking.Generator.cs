
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class TimeTracking
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [TimeTracking] ( " +
      
        " TimeTrackingId, " +
        " QuickBooksTxnId, " +
        " EntityStateId, " +
        " EditSequence, " +
        " TimeCreated, " +
        " TimeModified, " +
        " TxnNumber, " +
        " TxnDate, " +
        " QBEntityId, " +
        " CustomerId, " +
        " ItemId, " +
        " Rate, " +
        " Duration, " +
        " Notes, " +
        " IsBillable, " +
        " IsBilled " +
        ") Values (" +
      
        " @TimeTrackingId, " +
        " @QuickBooksTxnId, " +
        " @EntityStateId, " +
        " @EditSequence, " +
        " @TimeCreated, " +
        " @TimeModified, " +
        " @TxnNumber, " +
        " @TxnDate, " +
        " @QBEntityId, " +
        " @CustomerId, " +
        " @ItemId, " +
        " @Rate, " +
        " @Duration, " +
        " @Notes, " +
        " @IsBillable, " +
        " @IsBilled " +
      ")";

      public static void Insert(TimeTracking timeTracking)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@TimeTrackingId", timeTracking.TimeTrackingId);            
          
              Database.PutParameter(dbCommand,"@QuickBooksTxnId", timeTracking.QuickBooksTxnId);            
          
              Database.PutParameter(dbCommand,"@EntityStateId", timeTracking
			.EntityState.EntityStateId);            
          
              Database.PutParameter(dbCommand,"@EditSequence", timeTracking.EditSequence);            
          
              Database.PutParameter(dbCommand,"@TimeCreated", timeTracking.TimeCreated);            
          
              Database.PutParameter(dbCommand,"@TimeModified", timeTracking.TimeModified);            
          
              Database.PutParameter(dbCommand,"@TxnNumber", timeTracking.TxnNumber);            
          
              Database.PutParameter(dbCommand,"@TxnDate", timeTracking.TxnDate);            
          
              Database.PutParameter(dbCommand,"@QBEntityId", timeTracking
			.QBEntity.QBEntityId);            
          
            if(timeTracking
			.Customer == null)
            {
            Database.PutParameter(dbCommand,"@CustomerId", DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@CustomerId", timeTracking
			.Customer.CustomerId);
            }
          
            if(timeTracking
			.Item == null)
            {
            Database.PutParameter(dbCommand,"@ItemId", DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@ItemId", timeTracking
			.Item.ItemId);
            }
          
              Database.PutParameter(dbCommand,"@Rate", timeTracking.Rate);            
          
              Database.PutParameter(dbCommand,"@Duration", timeTracking.Duration);            
          
              Database.PutParameter(dbCommand,"@Notes", timeTracking.Notes);            
          
              Database.PutParameter(dbCommand,"@IsBillable", timeTracking.IsBillable);            
          
              Database.PutParameter(dbCommand,"@IsBilled", timeTracking.IsBilled);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<TimeTracking>  timeTrackingList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(TimeTracking timeTracking in  timeTrackingList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@TimeTrackingId", timeTracking.TimeTrackingId);
          
            Database.PutParameter(dbCommand,"@QuickBooksTxnId", timeTracking.QuickBooksTxnId);
          
            Database.PutParameter(dbCommand,"@EntityStateId", timeTracking
			.EntityState.EntityStateId);
          
            Database.PutParameter(dbCommand,"@EditSequence", timeTracking.EditSequence);
          
            Database.PutParameter(dbCommand,"@TimeCreated", timeTracking.TimeCreated);
          
            Database.PutParameter(dbCommand,"@TimeModified", timeTracking.TimeModified);
          
            Database.PutParameter(dbCommand,"@TxnNumber", timeTracking.TxnNumber);
          
            Database.PutParameter(dbCommand,"@TxnDate", timeTracking.TxnDate);
          
            Database.PutParameter(dbCommand,"@QBEntityId", timeTracking
			.QBEntity.QBEntityId);
          
            if(timeTracking
			.Customer == null)
            {
              Database.PutParameter(dbCommand,"@CustomerId", DbType.Int32);
            }
            else
            {
              Database.PutParameter(dbCommand,"@CustomerId", timeTracking
			.Customer.CustomerId);
            }
          
            if(timeTracking
			.Item == null)
            {
              Database.PutParameter(dbCommand,"@ItemId", DbType.Int32);
            }
            else
            {
              Database.PutParameter(dbCommand,"@ItemId", timeTracking
			.Item.ItemId);
            }
          
            Database.PutParameter(dbCommand,"@Rate", timeTracking.Rate);
          
            Database.PutParameter(dbCommand,"@Duration", timeTracking.Duration);
          
            Database.PutParameter(dbCommand,"@Notes", timeTracking.Notes);
          
            Database.PutParameter(dbCommand,"@IsBillable", timeTracking.IsBillable);
          
            Database.PutParameter(dbCommand,"@IsBilled", timeTracking.IsBilled);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@TimeTrackingId",timeTracking.TimeTrackingId);
          
            Database.UpdateParameter(dbCommand,"@QuickBooksTxnId",timeTracking.QuickBooksTxnId);
          
            Database.UpdateParameter(dbCommand,"@EntityStateId",timeTracking
			.EntityState.EntityStateId);
          
            Database.UpdateParameter(dbCommand,"@EditSequence",timeTracking.EditSequence);
          
            Database.UpdateParameter(dbCommand,"@TimeCreated",timeTracking.TimeCreated);
          
            Database.UpdateParameter(dbCommand,"@TimeModified",timeTracking.TimeModified);
          
            Database.UpdateParameter(dbCommand,"@TxnNumber",timeTracking.TxnNumber);
          
            Database.UpdateParameter(dbCommand,"@TxnDate",timeTracking.TxnDate);
          
            Database.UpdateParameter(dbCommand,"@QBEntityId",timeTracking
			.QBEntity.QBEntityId);
          
            if(timeTracking
			.Customer == null)
            {
             Database.UpdateParameter(dbCommand,"@CustomerId",DbType.Int32);
            }
            else
            {
            Database.UpdateParameter(dbCommand,"@CustomerId",timeTracking
			.Customer.CustomerId);
            }
          
            if(timeTracking
			.Item == null)
            {
             Database.UpdateParameter(dbCommand,"@ItemId",DbType.Int32);
            }
            else
            {
            Database.UpdateParameter(dbCommand,"@ItemId",timeTracking
			.Item.ItemId);
            }
          
            Database.UpdateParameter(dbCommand,"@Rate",timeTracking.Rate);
          
            Database.UpdateParameter(dbCommand,"@Duration",timeTracking.Duration);
          
            Database.UpdateParameter(dbCommand,"@Notes",timeTracking.Notes);
          
            Database.UpdateParameter(dbCommand,"@IsBillable",timeTracking.IsBillable);
          
            Database.UpdateParameter(dbCommand,"@IsBilled",timeTracking.IsBilled);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [TimeTracking] Set "
      
        + " QuickBooksTxnId = @QuickBooksTxnId, "
        + " EntityStateId = @EntityStateId, "
        + " EditSequence = @EditSequence, "
        + " TimeCreated = @TimeCreated, "
        + " TimeModified = @TimeModified, "
        + " TxnNumber = @TxnNumber, "
        + " TxnDate = @TxnDate, "
        + " QBEntityId = @QBEntityId, "
        + " CustomerId = @CustomerId, "
        + " ItemId = @ItemId, "
        + " Rate = @Rate, "
        + " Duration = @Duration, "
        + " Notes = @Notes, "
        + " IsBillable = @IsBillable, "
        + " IsBilled = @IsBilled "
        + " Where "
        
          + " TimeTrackingId = @TimeTrackingId "
        
      ;

      public static void Update(TimeTracking timeTracking)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@TimeTrackingId", timeTracking.TimeTrackingId);            
          
            Database.PutParameter(dbCommand,"@QuickBooksTxnId", timeTracking.QuickBooksTxnId);            
          
            Database.PutParameter(dbCommand,"@EntityStateId", timeTracking
			.EntityState.EntityStateId);            
          
            Database.PutParameter(dbCommand,"@EditSequence", timeTracking.EditSequence);            
          
            Database.PutParameter(dbCommand,"@TimeCreated", timeTracking.TimeCreated);            
          
            Database.PutParameter(dbCommand,"@TimeModified", timeTracking.TimeModified);            
          
            Database.PutParameter(dbCommand,"@TxnNumber", timeTracking.TxnNumber);            
          
            Database.PutParameter(dbCommand,"@TxnDate", timeTracking.TxnDate);            
          
            Database.PutParameter(dbCommand,"@QBEntityId", timeTracking
			.QBEntity.QBEntityId);            
          
            if(timeTracking
			.Customer == null)
            {
            Database.PutParameter(dbCommand,"@CustomerId",DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@CustomerId",timeTracking
			.Customer.CustomerId);
            }
          
            if(timeTracking
			.Item == null)
            {
            Database.PutParameter(dbCommand,"@ItemId",DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@ItemId",timeTracking
			.Item.ItemId);
            }
          
            Database.PutParameter(dbCommand,"@Rate", timeTracking.Rate);            
          
            Database.PutParameter(dbCommand,"@Duration", timeTracking.Duration);            
          
            Database.PutParameter(dbCommand,"@Notes", timeTracking.Notes);            
          
            Database.PutParameter(dbCommand,"@IsBillable", timeTracking.IsBillable);            
          
            Database.PutParameter(dbCommand,"@IsBilled", timeTracking.IsBilled);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " TimeTrackingId, "
        + " QuickBooksTxnId, "
        + " EntityStateId, "
        + " EditSequence, "
        + " TimeCreated, "
        + " TimeModified, "
        + " TxnNumber, "
        + " TxnDate, "
        + " QBEntityId, "
        + " CustomerId, "
        + " ItemId, "
        + " Rate, "
        + " Duration, "
        + " Notes, "
        + " IsBillable, "
        + " IsBilled "
        + " From [TimeTracking] "
      
        + " Where "
        
        + " TimeTrackingId = @TimeTrackingId "
        
      ;

      public static TimeTracking FindByPrimaryKey(
      int timeTrackingId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@TimeTrackingId", timeTrackingId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("TimeTracking not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(TimeTracking timeTracking)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@TimeTrackingId",timeTracking.TimeTrackingId);
      

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
      String sql = "select 1 from [TimeTracking]";

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

      public static TimeTracking Load(IDataReader dataReader)
      {
      TimeTracking timeTracking = new TimeTracking();

      timeTracking.TimeTrackingId = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
              timeTracking.QuickBooksTxnId = dataReader.GetInt32(1);
          timeTracking
			.EntityState = new EntityState();

            timeTracking
			.EntityState.EntityStateId = dataReader.GetInt32(2);
          timeTracking.EditSequence = dataReader.GetInt32(3);
          
            if(!dataReader.IsDBNull(4))
              timeTracking.TimeCreated = dataReader.GetDateTime(4);
          
            if(!dataReader.IsDBNull(5))
              timeTracking.TimeModified = dataReader.GetDateTime(5);
          
            if(!dataReader.IsDBNull(6))
              timeTracking.TxnNumber = dataReader.GetInt32(6);
          
            if(!dataReader.IsDBNull(7))
              timeTracking.TxnDate = dataReader.GetDateTime(7);
          timeTracking
			.QBEntity = new QBEntity();

            timeTracking
			.QBEntity.QBEntityId = dataReader.GetInt32(8);
          
            if(!dataReader.IsDBNull(9))
            {
            timeTracking
			.Customer = new Customer();
            
            timeTracking
			.Customer.CustomerId = dataReader.GetInt32(9);
           }
            else
            timeTracking
			.Customer = null;
          
            if(!dataReader.IsDBNull(10))
            {
            timeTracking
			.Item = new Item();
            
            timeTracking
			.Item.ItemId = dataReader.GetInt32(10);
           }
            else
            timeTracking
			.Item = null;
          
            if(!dataReader.IsDBNull(11))
              timeTracking.Rate = dataReader.GetDecimal(11);
          timeTracking.Duration = dataReader.GetString(12);
          
            if(!dataReader.IsDBNull(13))
              timeTracking.Notes = dataReader.GetString(13);
          
            if(!dataReader.IsDBNull(14))
              timeTracking.IsBillable = dataReader.GetBoolean(14);
          
            if(!dataReader.IsDBNull(15))
              timeTracking.IsBilled = dataReader.GetBoolean(15);
          

      return timeTracking;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [TimeTracking] "

      
        + " Where "
        
          + " TimeTrackingId = @TimeTrackingId "
        
      ;
      public static void Delete(TimeTracking timeTracking)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@TimeTrackingId", timeTracking.TimeTrackingId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [TimeTracking] ";

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
      
        + " TimeTrackingId, "
        + " QuickBooksTxnId, "
        + " EntityStateId, "
        + " EditSequence, "
        + " TimeCreated, "
        + " TimeModified, "
        + " TxnNumber, "
        + " TxnDate, "
        + " QBEntityId, "
        + " CustomerId, "
        + " ItemId, "
        + " Rate, "
        + " Duration, "
        + " Notes, "
        + " IsBillable, "
        + " IsBilled "
        + " From [TimeTracking] ";
      public static List<TimeTracking> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<TimeTracking> rv = new List<TimeTracking>();

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
      List<TimeTracking> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TimeTracking> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TimeTracking));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TimeTracking item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TimeTracking>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TimeTracking));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TimeTracking> itemsList
      = new List<TimeTracking>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TimeTracking)
      itemsList.Add(deserializedObject as TimeTracking);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region TimeTrackingId
        protected int m_timeTrackingId;

			[XmlAttribute]
			public int TimeTrackingId
			{
			get { return m_timeTrackingId;}
			set { m_timeTrackingId = value; }
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
		
		#region Rate
        protected decimal? m_rate;

			[XmlAttribute]
			public decimal? Rate
			{
			get { return m_rate;}
			set { m_rate = value; }
			}
		#endregion
		
		#region Duration
        protected String m_duration;

			[XmlAttribute]
			public String Duration
			{
			get { return m_duration;}
			set { m_duration = value; }
			}
		#endregion
		
		#region Notes
        protected String m_notes;

			[XmlAttribute]
			public String Notes
			{
			get { return m_notes;}
			set { m_notes = value; }
			}
		#endregion
		
		#region IsBillable
        protected bool m_isBillable;

			[XmlAttribute]
			public bool IsBillable
			{
			get { return m_isBillable;}
			set { m_isBillable = value; }
			}
		#endregion
		
		#region IsBilled
        protected bool m_isBilled;

			[XmlAttribute]
			public bool IsBilled
			{
			get { return m_isBilled;}
			set { m_isBilled = value; }
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
		
		#region EntityState
			protected EntityState m_entityState;

			[XmlElement]
			public EntityState EntityState
			{
			get { return m_entityState;}
			set { m_entityState = value; }
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
      public TimeTracking(
		int timeTrackingId

		)
		{
		
			m_timeTrackingId = timeTrackingId;
		
        }

      


        public TimeTracking(
		  Customer customer,EntityState entityState,Item item,QBEntity qBEntity
			  ,
		  int timeTrackingId,int? quickBooksTxnId,int editSequence,DateTime? timeCreated,DateTime? timeModified,int? txnNumber,DateTime? txnDate,decimal? rate,String duration,String notes,bool isBillable,bool isBilled
		  )
		  {

		  
			  m_customer = customer;
		  
			  m_entityState = entityState;
		  
			  m_item = item;
		  
			  m_qBEntity = qBEntity;
		  
			  m_timeTrackingId = timeTrackingId;
		  
			  m_quickBooksTxnId = quickBooksTxnId;
		  
			  m_editSequence = editSequence;
		  
			  m_timeCreated = timeCreated;
		  
			  m_timeModified = timeModified;
		  
			  m_txnNumber = txnNumber;
		  
			  m_txnDate = txnDate;
		  
			  m_rate = rate;
		  
			  m_duration = duration;
		  
			  m_notes = notes;
		  
			  m_isBillable = isBillable;
		  
			  m_isBilled = isBilled;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    