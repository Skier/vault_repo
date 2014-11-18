
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


      public partial class QbInvoiceLine : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbInvoiceLine ( " +
      
        " QbItemListId, " +
      
        " QbInvoiceId, " +
      
        " TxnLineID, " +
      
        " Description, " +
      
        " Quantity, " +
      
        " UnitOfMeasure, " +
      
        " Rate, " +
      
        " RatePercent, " +
      
        " QbClassListId, " +
      
        " Amount, " +
      
        " QbSalesTaxCodeListId, " +
      
        " TaskId, " +
      
        " ItemId " +
      
      ") Values (" +
      
        " ?QbItemListId, " +
      
        " ?QbInvoiceId, " +
      
        " ?TxnLineID, " +
      
        " ?Description, " +
      
        " ?Quantity, " +
      
        " ?UnitOfMeasure, " +
      
        " ?Rate, " +
      
        " ?RatePercent, " +
      
        " ?QbClassListId, " +
      
        " ?Amount, " +
      
        " ?QbSalesTaxCodeListId, " +
      
        " ?TaskId, " +
      
        " ?ItemId " +
      
      ")";

      public static void Insert(QbInvoiceLine qbInvoiceLine, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?QbItemListId", qbInvoiceLine.QbItemListId);
      
        Database.PutParameter(dbCommand,"?QbInvoiceId", qbInvoiceLine.QbInvoiceId);
      
        Database.PutParameter(dbCommand,"?TxnLineID", qbInvoiceLine.TxnLineID);
      
        Database.PutParameter(dbCommand,"?Description", qbInvoiceLine.Description);
      
        Database.PutParameter(dbCommand,"?Quantity", qbInvoiceLine.Quantity);
      
        Database.PutParameter(dbCommand,"?UnitOfMeasure", qbInvoiceLine.UnitOfMeasure);
      
        Database.PutParameter(dbCommand,"?Rate", qbInvoiceLine.Rate);
      
        Database.PutParameter(dbCommand,"?RatePercent", qbInvoiceLine.RatePercent);
      
        Database.PutParameter(dbCommand,"?QbClassListId", qbInvoiceLine.QbClassListId);
      
        Database.PutParameter(dbCommand,"?Amount", qbInvoiceLine.Amount);
      
        Database.PutParameter(dbCommand,"?QbSalesTaxCodeListId", qbInvoiceLine.QbSalesTaxCodeListId);
      
        Database.PutParameter(dbCommand,"?TaskId", qbInvoiceLine.TaskId);
      
        Database.PutParameter(dbCommand,"?ItemId", qbInvoiceLine.ItemId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        qbInvoiceLine.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(QbInvoiceLine qbInvoiceLine)
      {
        Insert(qbInvoiceLine, null);
      }


      public static void Insert(List<QbInvoiceLine>  qbInvoiceLineList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbInvoiceLine qbInvoiceLine in  qbInvoiceLineList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?QbItemListId", qbInvoiceLine.QbItemListId);
      
        Database.PutParameter(dbCommand,"?QbInvoiceId", qbInvoiceLine.QbInvoiceId);
      
        Database.PutParameter(dbCommand,"?TxnLineID", qbInvoiceLine.TxnLineID);
      
        Database.PutParameter(dbCommand,"?Description", qbInvoiceLine.Description);
      
        Database.PutParameter(dbCommand,"?Quantity", qbInvoiceLine.Quantity);
      
        Database.PutParameter(dbCommand,"?UnitOfMeasure", qbInvoiceLine.UnitOfMeasure);
      
        Database.PutParameter(dbCommand,"?Rate", qbInvoiceLine.Rate);
      
        Database.PutParameter(dbCommand,"?RatePercent", qbInvoiceLine.RatePercent);
      
        Database.PutParameter(dbCommand,"?QbClassListId", qbInvoiceLine.QbClassListId);
      
        Database.PutParameter(dbCommand,"?Amount", qbInvoiceLine.Amount);
      
        Database.PutParameter(dbCommand,"?QbSalesTaxCodeListId", qbInvoiceLine.QbSalesTaxCodeListId);
      
        Database.PutParameter(dbCommand,"?TaskId", qbInvoiceLine.TaskId);
      
        Database.PutParameter(dbCommand,"?ItemId", qbInvoiceLine.ItemId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?QbItemListId",qbInvoiceLine.QbItemListId);
      
        Database.UpdateParameter(dbCommand,"?QbInvoiceId",qbInvoiceLine.QbInvoiceId);
      
        Database.UpdateParameter(dbCommand,"?TxnLineID",qbInvoiceLine.TxnLineID);
      
        Database.UpdateParameter(dbCommand,"?Description",qbInvoiceLine.Description);
      
        Database.UpdateParameter(dbCommand,"?Quantity",qbInvoiceLine.Quantity);
      
        Database.UpdateParameter(dbCommand,"?UnitOfMeasure",qbInvoiceLine.UnitOfMeasure);
      
        Database.UpdateParameter(dbCommand,"?Rate",qbInvoiceLine.Rate);
      
        Database.UpdateParameter(dbCommand,"?RatePercent",qbInvoiceLine.RatePercent);
      
        Database.UpdateParameter(dbCommand,"?QbClassListId",qbInvoiceLine.QbClassListId);
      
        Database.UpdateParameter(dbCommand,"?Amount",qbInvoiceLine.Amount);
      
        Database.UpdateParameter(dbCommand,"?QbSalesTaxCodeListId",qbInvoiceLine.QbSalesTaxCodeListId);
      
        Database.UpdateParameter(dbCommand,"?TaskId",qbInvoiceLine.TaskId);
      
        Database.UpdateParameter(dbCommand,"?ItemId",qbInvoiceLine.ItemId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        qbInvoiceLine.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<QbInvoiceLine>  qbInvoiceLineList)
      {
        Insert(qbInvoiceLineList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbInvoiceLine Set "
      
        + " QbItemListId = ?QbItemListId, "
      
        + " QbInvoiceId = ?QbInvoiceId, "
      
        + " TxnLineID = ?TxnLineID, "
      
        + " Description = ?Description, "
      
        + " Quantity = ?Quantity, "
      
        + " UnitOfMeasure = ?UnitOfMeasure, "
      
        + " Rate = ?Rate, "
      
        + " RatePercent = ?RatePercent, "
      
        + " QbClassListId = ?QbClassListId, "
      
        + " Amount = ?Amount, "
      
        + " QbSalesTaxCodeListId = ?QbSalesTaxCodeListId, "
      
        + " TaskId = ?TaskId, "
      
        + " ItemId = ?ItemId "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(QbInvoiceLine qbInvoiceLine, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", qbInvoiceLine.ID);
      
        Database.PutParameter(dbCommand,"?QbItemListId", qbInvoiceLine.QbItemListId);
      
        Database.PutParameter(dbCommand,"?QbInvoiceId", qbInvoiceLine.QbInvoiceId);
      
        Database.PutParameter(dbCommand,"?TxnLineID", qbInvoiceLine.TxnLineID);
      
        Database.PutParameter(dbCommand,"?Description", qbInvoiceLine.Description);
      
        Database.PutParameter(dbCommand,"?Quantity", qbInvoiceLine.Quantity);
      
        Database.PutParameter(dbCommand,"?UnitOfMeasure", qbInvoiceLine.UnitOfMeasure);
      
        Database.PutParameter(dbCommand,"?Rate", qbInvoiceLine.Rate);
      
        Database.PutParameter(dbCommand,"?RatePercent", qbInvoiceLine.RatePercent);
      
        Database.PutParameter(dbCommand,"?QbClassListId", qbInvoiceLine.QbClassListId);
      
        Database.PutParameter(dbCommand,"?Amount", qbInvoiceLine.Amount);
      
        Database.PutParameter(dbCommand,"?QbSalesTaxCodeListId", qbInvoiceLine.QbSalesTaxCodeListId);
      
        Database.PutParameter(dbCommand,"?TaskId", qbInvoiceLine.TaskId);
      
        Database.PutParameter(dbCommand,"?ItemId", qbInvoiceLine.ItemId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbInvoiceLine qbInvoiceLine)
      {
        Update(qbInvoiceLine, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " QbItemListId, "
      
        + " QbInvoiceId, "
      
        + " TxnLineID, "
      
        + " Description, "
      
        + " Quantity, "
      
        + " UnitOfMeasure, "
      
        + " Rate, "
      
        + " RatePercent, "
      
        + " QbClassListId, "
      
        + " Amount, "
      
        + " QbSalesTaxCodeListId, "
      
        + " TaskId, "
      
        + " ItemId "
      

      + " From QbInvoiceLine "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static QbInvoiceLine FindByPrimaryKey(
      int iD, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", iD);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("QbInvoiceLine not found, search by primary key");

      }

      public static QbInvoiceLine FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbInvoiceLine qbInvoiceLine, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",qbInvoiceLine.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbInvoiceLine qbInvoiceLine)
      {
      return Exists(qbInvoiceLine, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbInvoiceLine limit 1";

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

      public static QbInvoiceLine Load(IDataReader dataReader, int offset)
      {
      QbInvoiceLine qbInvoiceLine = new QbInvoiceLine();

      qbInvoiceLine.ID = dataReader.GetInt32(0 + offset);
          qbInvoiceLine.QbItemListId = dataReader.GetString(1 + offset);
          qbInvoiceLine.QbInvoiceId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            qbInvoiceLine.TxnLineID = dataReader.GetString(3 + offset);
          qbInvoiceLine.Description = dataReader.GetString(4 + offset);
          qbInvoiceLine.Quantity = dataReader.GetDecimal(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            qbInvoiceLine.UnitOfMeasure = dataReader.GetString(6 + offset);
          qbInvoiceLine.Rate = dataReader.GetDecimal(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            qbInvoiceLine.RatePercent = dataReader.GetDecimal(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            qbInvoiceLine.QbClassListId = dataReader.GetString(9 + offset);
          qbInvoiceLine.Amount = dataReader.GetDecimal(10 + offset);
          qbInvoiceLine.QbSalesTaxCodeListId = dataReader.GetString(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            qbInvoiceLine.TaskId = dataReader.GetInt32(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            qbInvoiceLine.ItemId = dataReader.GetInt32(13 + offset);
          

      return qbInvoiceLine;
      }

      public static QbInvoiceLine Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbInvoiceLine "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(QbInvoiceLine qbInvoiceLine, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", qbInvoiceLine.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbInvoiceLine qbInvoiceLine)
      {
        Delete(qbInvoiceLine, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbInvoiceLine ";

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

      
        + " ID, "
      
        + " QbItemListId, "
      
        + " QbInvoiceId, "
      
        + " TxnLineID, "
      
        + " Description, "
      
        + " Quantity, "
      
        + " UnitOfMeasure, "
      
        + " Rate, "
      
        + " RatePercent, "
      
        + " QbClassListId, "
      
        + " Amount, "
      
        + " QbSalesTaxCodeListId, "
      
        + " TaskId, "
      
        + " ItemId "
      

      + " From QbInvoiceLine ";
      public static List<QbInvoiceLine> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbInvoiceLine> rv = new List<QbInvoiceLine>();

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

      public static List<QbInvoiceLine> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbInvoiceLine> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbInvoiceLine obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && QbItemListId == obj.QbItemListId && QbInvoiceId == obj.QbInvoiceId && TxnLineID == obj.TxnLineID && Description == obj.Description && Quantity == obj.Quantity && UnitOfMeasure == obj.UnitOfMeasure && Rate == obj.Rate && RatePercent == obj.RatePercent && QbClassListId == obj.QbClassListId && Amount == obj.Amount && QbSalesTaxCodeListId == obj.QbSalesTaxCodeListId && TaskId == obj.TaskId && ItemId == obj.ItemId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbInvoiceLine> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbInvoiceLine));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbInvoiceLine item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbInvoiceLine>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbInvoiceLine));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbInvoiceLine> itemsList
      = new List<QbInvoiceLine>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbInvoiceLine)
      itemsList.Add(deserializedObject as QbInvoiceLine);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_qbItemListId;
      
        protected int m_qbInvoiceId;
      
        protected String m_txnLineID;
      
        protected String m_description;
      
        protected decimal m_quantity;
      
        protected String m_unitOfMeasure;
      
        protected decimal m_rate;
      
        protected decimal m_ratePercent;
      
        protected String m_qbClassListId;
      
        protected decimal m_amount;
      
        protected String m_qbSalesTaxCodeListId;
      
        protected int? m_taskId;
      
        protected int? m_itemId;
      
      #endregion

      #region Constructors
      public QbInvoiceLine(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public QbInvoiceLine(
        int 
          iD,String 
          qbItemListId,int 
          qbInvoiceId,String 
          txnLineID,String 
          description,decimal 
          quantity,String 
          unitOfMeasure,decimal 
          rate,decimal 
          ratePercent,String 
          qbClassListId,decimal 
          amount,String 
          qbSalesTaxCodeListId,int? 
          taskId,int? 
          itemId
        ) : this()
        {
        
          m_iD = iD;
        
          m_qbItemListId = qbItemListId;
        
          m_qbInvoiceId = qbInvoiceId;
        
          m_txnLineID = txnLineID;
        
          m_description = description;
        
          m_quantity = quantity;
        
          m_unitOfMeasure = unitOfMeasure;
        
          m_rate = rate;
        
          m_ratePercent = ratePercent;
        
          m_qbClassListId = qbClassListId;
        
          m_amount = amount;
        
          m_qbSalesTaxCodeListId = qbSalesTaxCodeListId;
        
          m_taskId = taskId;
        
          m_itemId = itemId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String QbItemListId
        {
        get { return m_qbItemListId;}
        set { m_qbItemListId = value; }
        }
      
        [XmlElement]
        public int QbInvoiceId
        {
        get { return m_qbInvoiceId;}
        set { m_qbInvoiceId = value; }
        }
      
        [XmlElement]
        public String TxnLineID
        {
        get { return m_txnLineID;}
        set { m_txnLineID = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
        [XmlElement]
        public decimal Quantity
        {
        get { return m_quantity;}
        set { m_quantity = value; }
        }
      
        [XmlElement]
        public String UnitOfMeasure
        {
        get { return m_unitOfMeasure;}
        set { m_unitOfMeasure = value; }
        }
      
        [XmlElement]
        public decimal Rate
        {
        get { return m_rate;}
        set { m_rate = value; }
        }
      
        [XmlElement]
        public decimal RatePercent
        {
        get { return m_ratePercent;}
        set { m_ratePercent = value; }
        }
      
        [XmlElement]
        public String QbClassListId
        {
        get { return m_qbClassListId;}
        set { m_qbClassListId = value; }
        }
      
        [XmlElement]
        public decimal Amount
        {
        get { return m_amount;}
        set { m_amount = value; }
        }
      
        [XmlElement]
        public String QbSalesTaxCodeListId
        {
        get { return m_qbSalesTaxCodeListId;}
        set { m_qbSalesTaxCodeListId = value; }
        }
      
        [XmlElement]
        public int? TaskId
        {
        get { return m_taskId;}
        set { m_taskId = value; }
        }
      
        [XmlElement]
        public int? ItemId
        {
        get { return m_itemId;}
        set { m_itemId = value; }
        }
      

      public static int FieldsCount
      {
      get { return 14; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    