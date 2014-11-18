
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class Terms
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Terms] ( " +
      
        " TermsId, " +
        " QuickBooksListId, " +
        " Name, " +
        " TimeCreated, " +
        " TimeModified, " +
        " EditSequence, " +
        " StdDueDays, " +
        " StdDiscountDays, " +
        " DiscountPct " +
        ") Values (" +
      
        " @TermsId, " +
        " @QuickBooksListId, " +
        " @Name, " +
        " @TimeCreated, " +
        " @TimeModified, " +
        " @EditSequence, " +
        " @StdDueDays, " +
        " @StdDiscountDays, " +
        " @DiscountPct " +
      ")";

      public static void Insert(Terms terms)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@TermsId", terms.TermsId);            
          
              Database.PutParameter(dbCommand,"@QuickBooksListId", terms.QuickBooksListId);            
          
              Database.PutParameter(dbCommand,"@Name", terms.Name);            
          
              Database.PutParameter(dbCommand,"@TimeCreated", terms.TimeCreated);            
          
              Database.PutParameter(dbCommand,"@TimeModified", terms.TimeModified);            
          
              Database.PutParameter(dbCommand,"@EditSequence", terms.EditSequence);            
          
              Database.PutParameter(dbCommand,"@StdDueDays", terms.StdDueDays);            
          
              Database.PutParameter(dbCommand,"@StdDiscountDays", terms.StdDiscountDays);            
          
              Database.PutParameter(dbCommand,"@DiscountPct", terms.DiscountPct);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Terms>  termsList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Terms terms in  termsList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@TermsId", terms.TermsId);
          
            Database.PutParameter(dbCommand,"@QuickBooksListId", terms.QuickBooksListId);
          
            Database.PutParameter(dbCommand,"@Name", terms.Name);
          
            Database.PutParameter(dbCommand,"@TimeCreated", terms.TimeCreated);
          
            Database.PutParameter(dbCommand,"@TimeModified", terms.TimeModified);
          
            Database.PutParameter(dbCommand,"@EditSequence", terms.EditSequence);
          
            Database.PutParameter(dbCommand,"@StdDueDays", terms.StdDueDays);
          
            Database.PutParameter(dbCommand,"@StdDiscountDays", terms.StdDiscountDays);
          
            Database.PutParameter(dbCommand,"@DiscountPct", terms.DiscountPct);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@TermsId",terms.TermsId);
          
            Database.UpdateParameter(dbCommand,"@QuickBooksListId",terms.QuickBooksListId);
          
            Database.UpdateParameter(dbCommand,"@Name",terms.Name);
          
            Database.UpdateParameter(dbCommand,"@TimeCreated",terms.TimeCreated);
          
            Database.UpdateParameter(dbCommand,"@TimeModified",terms.TimeModified);
          
            Database.UpdateParameter(dbCommand,"@EditSequence",terms.EditSequence);
          
            Database.UpdateParameter(dbCommand,"@StdDueDays",terms.StdDueDays);
          
            Database.UpdateParameter(dbCommand,"@StdDiscountDays",terms.StdDiscountDays);
          
            Database.UpdateParameter(dbCommand,"@DiscountPct",terms.DiscountPct);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Terms] Set "
      
        + " QuickBooksListId = @QuickBooksListId, "
        + " Name = @Name, "
        + " TimeCreated = @TimeCreated, "
        + " TimeModified = @TimeModified, "
        + " EditSequence = @EditSequence, "
        + " StdDueDays = @StdDueDays, "
        + " StdDiscountDays = @StdDiscountDays, "
        + " DiscountPct = @DiscountPct "
        + " Where "
        
          + " TermsId = @TermsId "
        
      ;

      public static void Update(Terms terms)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@TermsId", terms.TermsId);            
          
            Database.PutParameter(dbCommand,"@QuickBooksListId", terms.QuickBooksListId);            
          
            Database.PutParameter(dbCommand,"@Name", terms.Name);            
          
            Database.PutParameter(dbCommand,"@TimeCreated", terms.TimeCreated);            
          
            Database.PutParameter(dbCommand,"@TimeModified", terms.TimeModified);            
          
            Database.PutParameter(dbCommand,"@EditSequence", terms.EditSequence);            
          
            Database.PutParameter(dbCommand,"@StdDueDays", terms.StdDueDays);            
          
            Database.PutParameter(dbCommand,"@StdDiscountDays", terms.StdDiscountDays);            
          
            Database.PutParameter(dbCommand,"@DiscountPct", terms.DiscountPct);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " TermsId, "
        + " QuickBooksListId, "
        + " Name, "
        + " TimeCreated, "
        + " TimeModified, "
        + " EditSequence, "
        + " StdDueDays, "
        + " StdDiscountDays, "
        + " DiscountPct "
        + " From [Terms] "
      
        + " Where "
        
        + " TermsId = @TermsId "
        
      ;

      public static Terms FindByPrimaryKey(
      int termsId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@TermsId", termsId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Terms not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Terms terms)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@TermsId",terms.TermsId);
      

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
      String sql = "select 1 from [Terms]";

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

      public static Terms Load(IDataReader dataReader)
      {
      Terms terms = new Terms();

      terms.TermsId = dataReader.GetInt32(0);
          terms.QuickBooksListId = dataReader.GetInt32(1);
          terms.Name = dataReader.GetString(2);
          
            if(!dataReader.IsDBNull(3))
              terms.TimeCreated = dataReader.GetDateTime(3);
          
            if(!dataReader.IsDBNull(4))
              terms.TimeModified = dataReader.GetDateTime(4);
          terms.EditSequence = dataReader.GetInt32(5);
          
            if(!dataReader.IsDBNull(6))
              terms.StdDueDays = dataReader.GetInt32(6);
          
            if(!dataReader.IsDBNull(7))
              terms.StdDiscountDays = dataReader.GetInt32(7);
          
            if(!dataReader.IsDBNull(8))
              terms.DiscountPct = dataReader.GetDecimal(8);
          

      return terms;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Terms] "

      
        + " Where "
        
          + " TermsId = @TermsId "
        
      ;
      public static void Delete(Terms terms)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@TermsId", terms.TermsId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Terms] ";

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
      
        + " TermsId, "
        + " QuickBooksListId, "
        + " Name, "
        + " TimeCreated, "
        + " TimeModified, "
        + " EditSequence, "
        + " StdDueDays, "
        + " StdDiscountDays, "
        + " DiscountPct "
        + " From [Terms] ";
      public static List<Terms> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Terms> rv = new List<Terms>();

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
      List<Terms> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Terms> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Terms));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Terms item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Terms>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Terms));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Terms> itemsList
      = new List<Terms>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Terms)
      itemsList.Add(deserializedObject as Terms);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region TermsId
        protected int m_termsId;

			[XmlAttribute]
			public int TermsId
			{
			get { return m_termsId;}
			set { m_termsId = value; }
			}
		#endregion
		
		#region QuickBooksListId
        protected int m_quickBooksListId;

			[XmlAttribute]
			public int QuickBooksListId
			{
			get { return m_quickBooksListId;}
			set { m_quickBooksListId = value; }
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
		
		#region EditSequence
        protected int m_editSequence;

			[XmlAttribute]
			public int EditSequence
			{
			get { return m_editSequence;}
			set { m_editSequence = value; }
			}
		#endregion
		
		#region StdDueDays
        protected int? m_stdDueDays;

			[XmlAttribute]
			public int? StdDueDays
			{
			get { return m_stdDueDays;}
			set { m_stdDueDays = value; }
			}
		#endregion
		
		#region StdDiscountDays
        protected int? m_stdDiscountDays;

			[XmlAttribute]
			public int? StdDiscountDays
			{
			get { return m_stdDiscountDays;}
			set { m_stdDiscountDays = value; }
			}
		#endregion
		
		#region DiscountPct
        protected decimal? m_discountPct;

			[XmlAttribute]
			public decimal? DiscountPct
			{
			get { return m_discountPct;}
			set { m_discountPct = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public Terms(
		int termsId

		)
		{
		
			m_termsId = termsId;
		
        }

      


        public Terms(
		  int termsId,int quickBooksListId,String name,DateTime? timeCreated,DateTime? timeModified,int editSequence,int? stdDueDays,int? stdDiscountDays,decimal? discountPct
		  )
		  {

		  
			  m_termsId = termsId;
		  
			  m_quickBooksListId = quickBooksListId;
		  
			  m_name = name;
		  
			  m_timeCreated = timeCreated;
		  
			  m_timeModified = timeModified;
		  
			  m_editSequence = editSequence;
		  
			  m_stdDueDays = stdDueDays;
		  
			  m_stdDiscountDays = stdDiscountDays;
		  
			  m_discountPct = discountPct;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    