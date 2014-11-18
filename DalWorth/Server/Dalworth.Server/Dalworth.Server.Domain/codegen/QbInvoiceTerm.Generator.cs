
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Domain
      {


      public partial class QbInvoiceTerm : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbInvoiceTerm ( " +
      
        " ListId, " +
      
        " Name, " +
      
        " TimeCreated, " +
      
        " TimeModified, " +
      
        " EditSequence, " +
      
        " IsActive, " +
      
        " IsDateDriven, " +
      
        " StdDueDays, " +
      
        " StdDiscountDays, " +
      
        " DiscountPct, " +
      
        " DayOfMonthDue, " +
      
        " DueNextMonthDays, " +
      
        " DiscountDayOfMonth " +
      
      ") Values (" +
      
        " ?ListId, " +
      
        " ?Name, " +
      
        " ?TimeCreated, " +
      
        " ?TimeModified, " +
      
        " ?EditSequence, " +
      
        " ?IsActive, " +
      
        " ?IsDateDriven, " +
      
        " ?StdDueDays, " +
      
        " ?StdDiscountDays, " +
      
        " ?DiscountPct, " +
      
        " ?DayOfMonthDue, " +
      
        " ?DueNextMonthDays, " +
      
        " ?DiscountDayOfMonth " +
      
      ")";

      public static void Insert(QbInvoiceTerm qbInvoiceTerm, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbInvoiceTerm.ListId);
      
        Database.PutParameter(dbCommand,"?Name", qbInvoiceTerm.Name);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbInvoiceTerm.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbInvoiceTerm.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbInvoiceTerm.EditSequence);
      
        Database.PutParameter(dbCommand,"?IsActive", qbInvoiceTerm.IsActive);
      
        Database.PutParameter(dbCommand,"?IsDateDriven", qbInvoiceTerm.IsDateDriven);
      
        Database.PutParameter(dbCommand,"?StdDueDays", qbInvoiceTerm.StdDueDays);
      
        Database.PutParameter(dbCommand,"?StdDiscountDays", qbInvoiceTerm.StdDiscountDays);
      
        Database.PutParameter(dbCommand,"?DiscountPct", qbInvoiceTerm.DiscountPct);
      
        Database.PutParameter(dbCommand,"?DayOfMonthDue", qbInvoiceTerm.DayOfMonthDue);
      
        Database.PutParameter(dbCommand,"?DueNextMonthDays", qbInvoiceTerm.DueNextMonthDays);
      
        Database.PutParameter(dbCommand,"?DiscountDayOfMonth", qbInvoiceTerm.DiscountDayOfMonth);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbInvoiceTerm qbInvoiceTerm)
      {
        Insert(qbInvoiceTerm, null);
      }


      public static void Insert(List<QbInvoiceTerm>  qbInvoiceTermList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbInvoiceTerm qbInvoiceTerm in  qbInvoiceTermList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbInvoiceTerm.ListId);
      
        Database.PutParameter(dbCommand,"?Name", qbInvoiceTerm.Name);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbInvoiceTerm.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbInvoiceTerm.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbInvoiceTerm.EditSequence);
      
        Database.PutParameter(dbCommand,"?IsActive", qbInvoiceTerm.IsActive);
      
        Database.PutParameter(dbCommand,"?IsDateDriven", qbInvoiceTerm.IsDateDriven);
      
        Database.PutParameter(dbCommand,"?StdDueDays", qbInvoiceTerm.StdDueDays);
      
        Database.PutParameter(dbCommand,"?StdDiscountDays", qbInvoiceTerm.StdDiscountDays);
      
        Database.PutParameter(dbCommand,"?DiscountPct", qbInvoiceTerm.DiscountPct);
      
        Database.PutParameter(dbCommand,"?DayOfMonthDue", qbInvoiceTerm.DayOfMonthDue);
      
        Database.PutParameter(dbCommand,"?DueNextMonthDays", qbInvoiceTerm.DueNextMonthDays);
      
        Database.PutParameter(dbCommand,"?DiscountDayOfMonth", qbInvoiceTerm.DiscountDayOfMonth);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ListId",qbInvoiceTerm.ListId);
      
        Database.UpdateParameter(dbCommand,"?Name",qbInvoiceTerm.Name);
      
        Database.UpdateParameter(dbCommand,"?TimeCreated",qbInvoiceTerm.TimeCreated);
      
        Database.UpdateParameter(dbCommand,"?TimeModified",qbInvoiceTerm.TimeModified);
      
        Database.UpdateParameter(dbCommand,"?EditSequence",qbInvoiceTerm.EditSequence);
      
        Database.UpdateParameter(dbCommand,"?IsActive",qbInvoiceTerm.IsActive);
      
        Database.UpdateParameter(dbCommand,"?IsDateDriven",qbInvoiceTerm.IsDateDriven);
      
        Database.UpdateParameter(dbCommand,"?StdDueDays",qbInvoiceTerm.StdDueDays);
      
        Database.UpdateParameter(dbCommand,"?StdDiscountDays",qbInvoiceTerm.StdDiscountDays);
      
        Database.UpdateParameter(dbCommand,"?DiscountPct",qbInvoiceTerm.DiscountPct);
      
        Database.UpdateParameter(dbCommand,"?DayOfMonthDue",qbInvoiceTerm.DayOfMonthDue);
      
        Database.UpdateParameter(dbCommand,"?DueNextMonthDays",qbInvoiceTerm.DueNextMonthDays);
      
        Database.UpdateParameter(dbCommand,"?DiscountDayOfMonth",qbInvoiceTerm.DiscountDayOfMonth);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbInvoiceTerm>  qbInvoiceTermList)
      {
        Insert(qbInvoiceTermList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbInvoiceTerm Set "
      
        + " Name = ?Name, "
      
        + " TimeCreated = ?TimeCreated, "
      
        + " TimeModified = ?TimeModified, "
      
        + " EditSequence = ?EditSequence, "
      
        + " IsActive = ?IsActive, "
      
        + " IsDateDriven = ?IsDateDriven, "
      
        + " StdDueDays = ?StdDueDays, "
      
        + " StdDiscountDays = ?StdDiscountDays, "
      
        + " DiscountPct = ?DiscountPct, "
      
        + " DayOfMonthDue = ?DayOfMonthDue, "
      
        + " DueNextMonthDays = ?DueNextMonthDays, "
      
        + " DiscountDayOfMonth = ?DiscountDayOfMonth "
      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static void Update(QbInvoiceTerm qbInvoiceTerm, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbInvoiceTerm.ListId);
      
        Database.PutParameter(dbCommand,"?Name", qbInvoiceTerm.Name);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbInvoiceTerm.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbInvoiceTerm.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbInvoiceTerm.EditSequence);
      
        Database.PutParameter(dbCommand,"?IsActive", qbInvoiceTerm.IsActive);
      
        Database.PutParameter(dbCommand,"?IsDateDriven", qbInvoiceTerm.IsDateDriven);
      
        Database.PutParameter(dbCommand,"?StdDueDays", qbInvoiceTerm.StdDueDays);
      
        Database.PutParameter(dbCommand,"?StdDiscountDays", qbInvoiceTerm.StdDiscountDays);
      
        Database.PutParameter(dbCommand,"?DiscountPct", qbInvoiceTerm.DiscountPct);
      
        Database.PutParameter(dbCommand,"?DayOfMonthDue", qbInvoiceTerm.DayOfMonthDue);
      
        Database.PutParameter(dbCommand,"?DueNextMonthDays", qbInvoiceTerm.DueNextMonthDays);
      
        Database.PutParameter(dbCommand,"?DiscountDayOfMonth", qbInvoiceTerm.DiscountDayOfMonth);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbInvoiceTerm qbInvoiceTerm)
      {
        Update(qbInvoiceTerm, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ListId, "
      
        + " Name, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence, "
      
        + " IsActive, "
      
        + " IsDateDriven, "
      
        + " StdDueDays, "
      
        + " StdDiscountDays, "
      
        + " DiscountPct, "
      
        + " DayOfMonthDue, "
      
        + " DueNextMonthDays, "
      
        + " DiscountDayOfMonth "
      

      + " From QbInvoiceTerm "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static QbInvoiceTerm FindByPrimaryKey(
      String listId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", listId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("QbInvoiceTerm not found, search by primary key");

      }

      public static QbInvoiceTerm FindByPrimaryKey(
      String listId
      )
      {
      return FindByPrimaryKey(
      listId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbInvoiceTerm qbInvoiceTerm, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId",qbInvoiceTerm.ListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbInvoiceTerm qbInvoiceTerm)
      {
      return Exists(qbInvoiceTerm, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbInvoiceTerm limit 1";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      public static bool IsContainsData()
      {
      return IsContainsData(null);
      }

      #endregion

      #region Load

      public static QbInvoiceTerm Load(IDataReader dataReader, int offset)
      {
      QbInvoiceTerm qbInvoiceTerm = new QbInvoiceTerm();

      qbInvoiceTerm.ListId = dataReader.GetString(0 + offset);
          qbInvoiceTerm.Name = dataReader.GetString(1 + offset);
          qbInvoiceTerm.TimeCreated = dataReader.GetDateTime(2 + offset);
          qbInvoiceTerm.TimeModified = dataReader.GetDateTime(3 + offset);
          qbInvoiceTerm.EditSequence = dataReader.GetString(4 + offset);
          qbInvoiceTerm.IsActive = dataReader.GetBoolean(5 + offset);
          qbInvoiceTerm.IsDateDriven = dataReader.GetBoolean(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            qbInvoiceTerm.StdDueDays = dataReader.GetInt32(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            qbInvoiceTerm.StdDiscountDays = dataReader.GetInt32(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            qbInvoiceTerm.DiscountPct = dataReader.GetDecimal(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            qbInvoiceTerm.DayOfMonthDue = dataReader.GetInt32(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            qbInvoiceTerm.DueNextMonthDays = dataReader.GetInt32(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            qbInvoiceTerm.DiscountDayOfMonth = dataReader.GetInt32(12 + offset);
          

      return qbInvoiceTerm;
      }

      public static QbInvoiceTerm Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbInvoiceTerm "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;
      public static void Delete(QbInvoiceTerm qbInvoiceTerm, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ListId", qbInvoiceTerm.ListId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbInvoiceTerm qbInvoiceTerm)
      {
        Delete(qbInvoiceTerm, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbInvoiceTerm ";

      public static void Clear(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, connection))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Clear()
      {
        Clear(null);
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " ListId, "
      
        + " Name, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence, "
      
        + " IsActive, "
      
        + " IsDateDriven, "
      
        + " StdDueDays, "
      
        + " StdDiscountDays, "
      
        + " DiscountPct, "
      
        + " DayOfMonthDue, "
      
        + " DueNextMonthDays, "
      
        + " DiscountDayOfMonth "
      

      + " From QbInvoiceTerm ";
      public static List<QbInvoiceTerm> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbInvoiceTerm> rv = new List<QbInvoiceTerm>();

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

      public static List<QbInvoiceTerm> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbInvoiceTerm> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbInvoiceTerm obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ListId == obj.ListId && Name == obj.Name && TimeCreated == obj.TimeCreated && TimeModified == obj.TimeModified && EditSequence == obj.EditSequence && IsActive == obj.IsActive && IsDateDriven == obj.IsDateDriven && StdDueDays == obj.StdDueDays && StdDiscountDays == obj.StdDiscountDays && DiscountPct == obj.DiscountPct && DayOfMonthDue == obj.DayOfMonthDue && DueNextMonthDays == obj.DueNextMonthDays && DiscountDayOfMonth == obj.DiscountDayOfMonth;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbInvoiceTerm> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbInvoiceTerm));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbInvoiceTerm item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbInvoiceTerm>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbInvoiceTerm));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbInvoiceTerm> itemsList
      = new List<QbInvoiceTerm>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbInvoiceTerm)
      itemsList.Add(deserializedObject as QbInvoiceTerm);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_listId;
      
        protected String m_name;
      
        protected DateTime m_timeCreated;
      
        protected DateTime m_timeModified;
      
        protected String m_editSequence;
      
        protected bool m_isActive;
      
        protected bool m_isDateDriven;
      
        protected int? m_stdDueDays;
      
        protected int? m_stdDiscountDays;
      
        protected decimal m_discountPct;
      
        protected int? m_dayOfMonthDue;
      
        protected int? m_dueNextMonthDays;
      
        protected int? m_discountDayOfMonth;
      
      #endregion

      #region Constructors
      public QbInvoiceTerm(
      String 
          listId
      ) : this()
      {
      
        m_listId = listId;
      
      }

      


        public QbInvoiceTerm(
        String 
          listId,String 
          name,DateTime 
          timeCreated,DateTime 
          timeModified,String 
          editSequence,bool 
          isActive,bool 
          isDateDriven,int? 
          stdDueDays,int? 
          stdDiscountDays,decimal 
          discountPct,int? 
          dayOfMonthDue,int? 
          dueNextMonthDays,int? 
          discountDayOfMonth
        ) : this()
        {
        
          m_listId = listId;
        
          m_name = name;
        
          m_timeCreated = timeCreated;
        
          m_timeModified = timeModified;
        
          m_editSequence = editSequence;
        
          m_isActive = isActive;
        
          m_isDateDriven = isDateDriven;
        
          m_stdDueDays = stdDueDays;
        
          m_stdDiscountDays = stdDiscountDays;
        
          m_discountPct = discountPct;
        
          m_dayOfMonthDue = dayOfMonthDue;
        
          m_dueNextMonthDays = dueNextMonthDays;
        
          m_discountDayOfMonth = discountDayOfMonth;
        
        }


      
      #endregion

      
        [XmlElement]
        public String ListId
        {
        get { return m_listId;}
        set { m_listId = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        [XmlElement]
        public DateTime TimeCreated
        {
        get { return m_timeCreated;}
        set { m_timeCreated = value; }
        }
      
        [XmlElement]
        public DateTime TimeModified
        {
        get { return m_timeModified;}
        set { m_timeModified = value; }
        }
      
        [XmlElement]
        public String EditSequence
        {
        get { return m_editSequence;}
        set { m_editSequence = value; }
        }
      
        [XmlElement]
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
        }
      
        [XmlElement]
        public bool IsDateDriven
        {
        get { return m_isDateDriven;}
        set { m_isDateDriven = value; }
        }
      
        [XmlElement]
        public int? StdDueDays
        {
        get { return m_stdDueDays;}
        set { m_stdDueDays = value; }
        }
      
        [XmlElement]
        public int? StdDiscountDays
        {
        get { return m_stdDiscountDays;}
        set { m_stdDiscountDays = value; }
        }
      
        [XmlElement]
        public decimal DiscountPct
        {
        get { return m_discountPct;}
        set { m_discountPct = value; }
        }
      
        [XmlElement]
        public int? DayOfMonthDue
        {
        get { return m_dayOfMonthDue;}
        set { m_dayOfMonthDue = value; }
        }
      
        [XmlElement]
        public int? DueNextMonthDays
        {
        get { return m_dueNextMonthDays;}
        set { m_dueNextMonthDays = value; }
        }
      
        [XmlElement]
        public int? DiscountDayOfMonth
        {
        get { return m_discountDayOfMonth;}
        set { m_discountDayOfMonth = value; }
        }
      

      public static int FieldsCount
      {
      get { return 13; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    