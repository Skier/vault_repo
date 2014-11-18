
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


      public partial class PartnerSummaryReportItem : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into PartnerSummaryReportItem ( " +
      
        " GenerateDate, " +
      
        " OrderSourceId, " +
      
        " PhoneNumber, " +
      
        " AdsourceName, " +
      
        " CallCount, " +
      
        " BookCount, " +
      
        " ShopperCount, " +
      
        " NoActionCount, " +
      
        " CancelCount, " +
      
        " InProcessCount, " +
      
        " CompletedCount, " +
      
        " Amount, " +
      
        " IsSent " +
      
      ") Values (" +
      
        " ?GenerateDate, " +
      
        " ?OrderSourceId, " +
      
        " ?PhoneNumber, " +
      
        " ?AdsourceName, " +
      
        " ?CallCount, " +
      
        " ?BookCount, " +
      
        " ?ShopperCount, " +
      
        " ?NoActionCount, " +
      
        " ?CancelCount, " +
      
        " ?InProcessCount, " +
      
        " ?CompletedCount, " +
      
        " ?Amount, " +
      
        " ?IsSent " +
      
      ")";

      public static void Insert(PartnerSummaryReportItem partnerSummaryReportItem, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?GenerateDate", partnerSummaryReportItem.GenerateDate);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", partnerSummaryReportItem.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?PhoneNumber", partnerSummaryReportItem.PhoneNumber);
      
        Database.PutParameter(dbCommand,"?AdsourceName", partnerSummaryReportItem.AdsourceName);
      
        Database.PutParameter(dbCommand,"?CallCount", partnerSummaryReportItem.CallCount);
      
        Database.PutParameter(dbCommand,"?BookCount", partnerSummaryReportItem.BookCount);
      
        Database.PutParameter(dbCommand,"?ShopperCount", partnerSummaryReportItem.ShopperCount);
      
        Database.PutParameter(dbCommand,"?NoActionCount", partnerSummaryReportItem.NoActionCount);
      
        Database.PutParameter(dbCommand,"?CancelCount", partnerSummaryReportItem.CancelCount);
      
        Database.PutParameter(dbCommand,"?InProcessCount", partnerSummaryReportItem.InProcessCount);
      
        Database.PutParameter(dbCommand,"?CompletedCount", partnerSummaryReportItem.CompletedCount);
      
        Database.PutParameter(dbCommand,"?Amount", partnerSummaryReportItem.Amount);
      
        Database.PutParameter(dbCommand,"?IsSent", partnerSummaryReportItem.IsSent);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(PartnerSummaryReportItem partnerSummaryReportItem)
      {
        Insert(partnerSummaryReportItem, null);
      }


      public static void Insert(List<PartnerSummaryReportItem>  partnerSummaryReportItemList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(PartnerSummaryReportItem partnerSummaryReportItem in  partnerSummaryReportItemList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?GenerateDate", partnerSummaryReportItem.GenerateDate);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", partnerSummaryReportItem.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?PhoneNumber", partnerSummaryReportItem.PhoneNumber);
      
        Database.PutParameter(dbCommand,"?AdsourceName", partnerSummaryReportItem.AdsourceName);
      
        Database.PutParameter(dbCommand,"?CallCount", partnerSummaryReportItem.CallCount);
      
        Database.PutParameter(dbCommand,"?BookCount", partnerSummaryReportItem.BookCount);
      
        Database.PutParameter(dbCommand,"?ShopperCount", partnerSummaryReportItem.ShopperCount);
      
        Database.PutParameter(dbCommand,"?NoActionCount", partnerSummaryReportItem.NoActionCount);
      
        Database.PutParameter(dbCommand,"?CancelCount", partnerSummaryReportItem.CancelCount);
      
        Database.PutParameter(dbCommand,"?InProcessCount", partnerSummaryReportItem.InProcessCount);
      
        Database.PutParameter(dbCommand,"?CompletedCount", partnerSummaryReportItem.CompletedCount);
      
        Database.PutParameter(dbCommand,"?Amount", partnerSummaryReportItem.Amount);
      
        Database.PutParameter(dbCommand,"?IsSent", partnerSummaryReportItem.IsSent);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?GenerateDate",partnerSummaryReportItem.GenerateDate);
      
        Database.UpdateParameter(dbCommand,"?OrderSourceId",partnerSummaryReportItem.OrderSourceId);
      
        Database.UpdateParameter(dbCommand,"?PhoneNumber",partnerSummaryReportItem.PhoneNumber);
      
        Database.UpdateParameter(dbCommand,"?AdsourceName",partnerSummaryReportItem.AdsourceName);
      
        Database.UpdateParameter(dbCommand,"?CallCount",partnerSummaryReportItem.CallCount);
      
        Database.UpdateParameter(dbCommand,"?BookCount",partnerSummaryReportItem.BookCount);
      
        Database.UpdateParameter(dbCommand,"?ShopperCount",partnerSummaryReportItem.ShopperCount);
      
        Database.UpdateParameter(dbCommand,"?NoActionCount",partnerSummaryReportItem.NoActionCount);
      
        Database.UpdateParameter(dbCommand,"?CancelCount",partnerSummaryReportItem.CancelCount);
      
        Database.UpdateParameter(dbCommand,"?InProcessCount",partnerSummaryReportItem.InProcessCount);
      
        Database.UpdateParameter(dbCommand,"?CompletedCount",partnerSummaryReportItem.CompletedCount);
      
        Database.UpdateParameter(dbCommand,"?Amount",partnerSummaryReportItem.Amount);
      
        Database.UpdateParameter(dbCommand,"?IsSent",partnerSummaryReportItem.IsSent);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<PartnerSummaryReportItem>  partnerSummaryReportItemList)
      {
        Insert(partnerSummaryReportItemList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update PartnerSummaryReportItem Set "
      
        + " CallCount = ?CallCount, "
      
        + " BookCount = ?BookCount, "
      
        + " ShopperCount = ?ShopperCount, "
      
        + " NoActionCount = ?NoActionCount, "
      
        + " CancelCount = ?CancelCount, "
      
        + " InProcessCount = ?InProcessCount, "
      
        + " CompletedCount = ?CompletedCount, "
      
        + " Amount = ?Amount, "
      
        + " IsSent = ?IsSent "
      
        + " Where "
        
          + " GenerateDate = ?GenerateDate and  "
        
          + " OrderSourceId = ?OrderSourceId and  "
        
          + " PhoneNumber = ?PhoneNumber and  "
        
          + " AdsourceName = ?AdsourceName "
        
      ;

      public static void Update(PartnerSummaryReportItem partnerSummaryReportItem, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?GenerateDate", partnerSummaryReportItem.GenerateDate);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", partnerSummaryReportItem.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?PhoneNumber", partnerSummaryReportItem.PhoneNumber);
      
        Database.PutParameter(dbCommand,"?AdsourceName", partnerSummaryReportItem.AdsourceName);
      
        Database.PutParameter(dbCommand,"?CallCount", partnerSummaryReportItem.CallCount);
      
        Database.PutParameter(dbCommand,"?BookCount", partnerSummaryReportItem.BookCount);
      
        Database.PutParameter(dbCommand,"?ShopperCount", partnerSummaryReportItem.ShopperCount);
      
        Database.PutParameter(dbCommand,"?NoActionCount", partnerSummaryReportItem.NoActionCount);
      
        Database.PutParameter(dbCommand,"?CancelCount", partnerSummaryReportItem.CancelCount);
      
        Database.PutParameter(dbCommand,"?InProcessCount", partnerSummaryReportItem.InProcessCount);
      
        Database.PutParameter(dbCommand,"?CompletedCount", partnerSummaryReportItem.CompletedCount);
      
        Database.PutParameter(dbCommand,"?Amount", partnerSummaryReportItem.Amount);
      
        Database.PutParameter(dbCommand,"?IsSent", partnerSummaryReportItem.IsSent);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(PartnerSummaryReportItem partnerSummaryReportItem)
      {
        Update(partnerSummaryReportItem, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " GenerateDate, "
      
        + " OrderSourceId, "
      
        + " PhoneNumber, "
      
        + " AdsourceName, "
      
        + " CallCount, "
      
        + " BookCount, "
      
        + " ShopperCount, "
      
        + " NoActionCount, "
      
        + " CancelCount, "
      
        + " InProcessCount, "
      
        + " CompletedCount, "
      
        + " Amount, "
      
        + " IsSent "
      

      + " From PartnerSummaryReportItem "

      
        + " Where "
        
          + " GenerateDate = ?GenerateDate and  "
        
          + " OrderSourceId = ?OrderSourceId and  "
        
          + " PhoneNumber = ?PhoneNumber and  "
        
          + " AdsourceName = ?AdsourceName "
        
      ;

      public static PartnerSummaryReportItem FindByPrimaryKey(
      DateTime generateDate,int orderSourceId,String phoneNumber,String adsourceName, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?GenerateDate", generateDate);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", orderSourceId);
      
        Database.PutParameter(dbCommand,"?PhoneNumber", phoneNumber);
      
        Database.PutParameter(dbCommand,"?AdsourceName", adsourceName);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("PartnerSummaryReportItem not found, search by primary key");

      }

      public static PartnerSummaryReportItem FindByPrimaryKey(
      DateTime generateDate,int orderSourceId,String phoneNumber,String adsourceName
      )
      {
      return FindByPrimaryKey(
      generateDate,orderSourceId,phoneNumber,adsourceName, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(PartnerSummaryReportItem partnerSummaryReportItem, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?GenerateDate",partnerSummaryReportItem.GenerateDate);
      
        Database.PutParameter(dbCommand,"?OrderSourceId",partnerSummaryReportItem.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?PhoneNumber",partnerSummaryReportItem.PhoneNumber);
      
        Database.PutParameter(dbCommand,"?AdsourceName",partnerSummaryReportItem.AdsourceName);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(PartnerSummaryReportItem partnerSummaryReportItem)
      {
      return Exists(partnerSummaryReportItem, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from PartnerSummaryReportItem limit 1";

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

      public static PartnerSummaryReportItem Load(IDataReader dataReader, int offset)
      {
      PartnerSummaryReportItem partnerSummaryReportItem = new PartnerSummaryReportItem();

      partnerSummaryReportItem.GenerateDate = dataReader.GetDateTime(0 + offset);
          partnerSummaryReportItem.OrderSourceId = dataReader.GetInt32(1 + offset);
          partnerSummaryReportItem.PhoneNumber = dataReader.GetString(2 + offset);
          partnerSummaryReportItem.AdsourceName = dataReader.GetString(3 + offset);
          partnerSummaryReportItem.CallCount = dataReader.GetInt32(4 + offset);
          partnerSummaryReportItem.BookCount = dataReader.GetInt32(5 + offset);
          partnerSummaryReportItem.ShopperCount = dataReader.GetInt32(6 + offset);
          partnerSummaryReportItem.NoActionCount = dataReader.GetInt32(7 + offset);
          partnerSummaryReportItem.CancelCount = dataReader.GetInt32(8 + offset);
          partnerSummaryReportItem.InProcessCount = dataReader.GetInt32(9 + offset);
          partnerSummaryReportItem.CompletedCount = dataReader.GetInt32(10 + offset);
          partnerSummaryReportItem.Amount = dataReader.GetDecimal(11 + offset);
          partnerSummaryReportItem.IsSent = dataReader.GetBoolean(12 + offset);
          

      return partnerSummaryReportItem;
      }

      public static PartnerSummaryReportItem Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From PartnerSummaryReportItem "

      
        + " Where "
        
          + " GenerateDate = ?GenerateDate and  "
        
          + " OrderSourceId = ?OrderSourceId and  "
        
          + " PhoneNumber = ?PhoneNumber and  "
        
          + " AdsourceName = ?AdsourceName "
        
      ;
      public static void Delete(PartnerSummaryReportItem partnerSummaryReportItem, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?GenerateDate", partnerSummaryReportItem.GenerateDate);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", partnerSummaryReportItem.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?PhoneNumber", partnerSummaryReportItem.PhoneNumber);
      
        Database.PutParameter(dbCommand,"?AdsourceName", partnerSummaryReportItem.AdsourceName);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(PartnerSummaryReportItem partnerSummaryReportItem)
      {
        Delete(partnerSummaryReportItem, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From PartnerSummaryReportItem ";

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

      
        + " GenerateDate, "
      
        + " OrderSourceId, "
      
        + " PhoneNumber, "
      
        + " AdsourceName, "
      
        + " CallCount, "
      
        + " BookCount, "
      
        + " ShopperCount, "
      
        + " NoActionCount, "
      
        + " CancelCount, "
      
        + " InProcessCount, "
      
        + " CompletedCount, "
      
        + " Amount, "
      
        + " IsSent "
      

      + " From PartnerSummaryReportItem ";
      public static List<PartnerSummaryReportItem> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<PartnerSummaryReportItem> rv = new List<PartnerSummaryReportItem>();

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

      public static List<PartnerSummaryReportItem> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<PartnerSummaryReportItem> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (PartnerSummaryReportItem obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return GenerateDate == obj.GenerateDate && OrderSourceId == obj.OrderSourceId && PhoneNumber == obj.PhoneNumber && AdsourceName == obj.AdsourceName && CallCount == obj.CallCount && BookCount == obj.BookCount && ShopperCount == obj.ShopperCount && NoActionCount == obj.NoActionCount && CancelCount == obj.CancelCount && InProcessCount == obj.InProcessCount && CompletedCount == obj.CompletedCount && Amount == obj.Amount && IsSent == obj.IsSent;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<PartnerSummaryReportItem> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PartnerSummaryReportItem));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(PartnerSummaryReportItem item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<PartnerSummaryReportItem>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PartnerSummaryReportItem));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<PartnerSummaryReportItem> itemsList
      = new List<PartnerSummaryReportItem>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is PartnerSummaryReportItem)
      itemsList.Add(deserializedObject as PartnerSummaryReportItem);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected DateTime m_generateDate;
      
        protected int m_orderSourceId;
      
        protected String m_phoneNumber;
      
        protected String m_adsourceName;
      
        protected int m_callCount;
      
        protected int m_bookCount;
      
        protected int m_shopperCount;
      
        protected int m_noActionCount;
      
        protected int m_cancelCount;
      
        protected int m_inProcessCount;
      
        protected int m_completedCount;
      
        protected decimal m_amount;
      
        protected bool m_isSent;
      
      #endregion

      #region Constructors
      public PartnerSummaryReportItem(
      DateTime 
          generateDate,int 
          orderSourceId,String 
          phoneNumber,String 
          adsourceName
      ) : this()
      {
      
        m_generateDate = generateDate;
      
        m_orderSourceId = orderSourceId;
      
        m_phoneNumber = phoneNumber;
      
        m_adsourceName = adsourceName;
      
      }

      


        public PartnerSummaryReportItem(
        DateTime 
          generateDate,int 
          orderSourceId,String 
          phoneNumber,String 
          adsourceName,int 
          callCount,int 
          bookCount,int 
          shopperCount,int 
          noActionCount,int 
          cancelCount,int 
          inProcessCount,int 
          completedCount,decimal 
          amount,bool 
          isSent
        ) : this()
        {
        
          m_generateDate = generateDate;
        
          m_orderSourceId = orderSourceId;
        
          m_phoneNumber = phoneNumber;
        
          m_adsourceName = adsourceName;
        
          m_callCount = callCount;
        
          m_bookCount = bookCount;
        
          m_shopperCount = shopperCount;
        
          m_noActionCount = noActionCount;
        
          m_cancelCount = cancelCount;
        
          m_inProcessCount = inProcessCount;
        
          m_completedCount = completedCount;
        
          m_amount = amount;
        
          m_isSent = isSent;
        
        }


      
      #endregion

      
        [XmlElement]
        public DateTime GenerateDate
        {
        get { return m_generateDate;}
        set { m_generateDate = value; }
        }
      
        [XmlElement]
        public int OrderSourceId
        {
        get { return m_orderSourceId;}
        set { m_orderSourceId = value; }
        }
      
        [XmlElement]
        public String PhoneNumber
        {
        get { return m_phoneNumber;}
        set { m_phoneNumber = value; }
        }
      
        [XmlElement]
        public String AdsourceName
        {
        get { return m_adsourceName;}
        set { m_adsourceName = value; }
        }
      
        [XmlElement]
        public int CallCount
        {
        get { return m_callCount;}
        set { m_callCount = value; }
        }
      
        [XmlElement]
        public int BookCount
        {
        get { return m_bookCount;}
        set { m_bookCount = value; }
        }
      
        [XmlElement]
        public int ShopperCount
        {
        get { return m_shopperCount;}
        set { m_shopperCount = value; }
        }
      
        [XmlElement]
        public int NoActionCount
        {
        get { return m_noActionCount;}
        set { m_noActionCount = value; }
        }
      
        [XmlElement]
        public int CancelCount
        {
        get { return m_cancelCount;}
        set { m_cancelCount = value; }
        }
      
        [XmlElement]
        public int InProcessCount
        {
        get { return m_inProcessCount;}
        set { m_inProcessCount = value; }
        }
      
        [XmlElement]
        public int CompletedCount
        {
        get { return m_completedCount;}
        set { m_completedCount = value; }
        }
      
        [XmlElement]
        public decimal Amount
        {
        get { return m_amount;}
        set { m_amount = value; }
        }
      
        [XmlElement]
        public bool IsSent
        {
        get { return m_isSent;}
        set { m_isSent = value; }
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

    