
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


      public partial class QbCreditMemoLine : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbCreditMemoLine ( " +
      
        " TxnLineID, " +
      
        " QbCreditMemoTxnID, " +
      
        " QbItemListId, " +
      
        " Description, " +
      
        " Quantity, " +
      
        " Rate, " +
      
        " RatePercent, " +
      
        " Amount, " +
      
        " QbSalesTaxCodeListId " +
      
      ") Values (" +
      
        " ?TxnLineID, " +
      
        " ?QbCreditMemoTxnID, " +
      
        " ?QbItemListId, " +
      
        " ?Description, " +
      
        " ?Quantity, " +
      
        " ?Rate, " +
      
        " ?RatePercent, " +
      
        " ?Amount, " +
      
        " ?QbSalesTaxCodeListId " +
      
      ")";

      public static void Insert(QbCreditMemoLine qbCreditMemoLine, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TxnLineID", qbCreditMemoLine.TxnLineID);
      
        Database.PutParameter(dbCommand,"?QbCreditMemoTxnID", qbCreditMemoLine.QbCreditMemoTxnID);
      
        Database.PutParameter(dbCommand,"?QbItemListId", qbCreditMemoLine.QbItemListId);
      
        Database.PutParameter(dbCommand,"?Description", qbCreditMemoLine.Description);
      
        Database.PutParameter(dbCommand,"?Quantity", qbCreditMemoLine.Quantity);
      
        Database.PutParameter(dbCommand,"?Rate", qbCreditMemoLine.Rate);
      
        Database.PutParameter(dbCommand,"?RatePercent", qbCreditMemoLine.RatePercent);
      
        Database.PutParameter(dbCommand,"?Amount", qbCreditMemoLine.Amount);
      
        Database.PutParameter(dbCommand,"?QbSalesTaxCodeListId", qbCreditMemoLine.QbSalesTaxCodeListId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbCreditMemoLine qbCreditMemoLine)
      {
        Insert(qbCreditMemoLine, null);
      }


      public static void Insert(List<QbCreditMemoLine>  qbCreditMemoLineList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbCreditMemoLine qbCreditMemoLine in  qbCreditMemoLineList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TxnLineID", qbCreditMemoLine.TxnLineID);
      
        Database.PutParameter(dbCommand,"?QbCreditMemoTxnID", qbCreditMemoLine.QbCreditMemoTxnID);
      
        Database.PutParameter(dbCommand,"?QbItemListId", qbCreditMemoLine.QbItemListId);
      
        Database.PutParameter(dbCommand,"?Description", qbCreditMemoLine.Description);
      
        Database.PutParameter(dbCommand,"?Quantity", qbCreditMemoLine.Quantity);
      
        Database.PutParameter(dbCommand,"?Rate", qbCreditMemoLine.Rate);
      
        Database.PutParameter(dbCommand,"?RatePercent", qbCreditMemoLine.RatePercent);
      
        Database.PutParameter(dbCommand,"?Amount", qbCreditMemoLine.Amount);
      
        Database.PutParameter(dbCommand,"?QbSalesTaxCodeListId", qbCreditMemoLine.QbSalesTaxCodeListId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TxnLineID",qbCreditMemoLine.TxnLineID);
      
        Database.UpdateParameter(dbCommand,"?QbCreditMemoTxnID",qbCreditMemoLine.QbCreditMemoTxnID);
      
        Database.UpdateParameter(dbCommand,"?QbItemListId",qbCreditMemoLine.QbItemListId);
      
        Database.UpdateParameter(dbCommand,"?Description",qbCreditMemoLine.Description);
      
        Database.UpdateParameter(dbCommand,"?Quantity",qbCreditMemoLine.Quantity);
      
        Database.UpdateParameter(dbCommand,"?Rate",qbCreditMemoLine.Rate);
      
        Database.UpdateParameter(dbCommand,"?RatePercent",qbCreditMemoLine.RatePercent);
      
        Database.UpdateParameter(dbCommand,"?Amount",qbCreditMemoLine.Amount);
      
        Database.UpdateParameter(dbCommand,"?QbSalesTaxCodeListId",qbCreditMemoLine.QbSalesTaxCodeListId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbCreditMemoLine>  qbCreditMemoLineList)
      {
        Insert(qbCreditMemoLineList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbCreditMemoLine Set "
      
        + " QbCreditMemoTxnID = ?QbCreditMemoTxnID, "
      
        + " QbItemListId = ?QbItemListId, "
      
        + " Description = ?Description, "
      
        + " Quantity = ?Quantity, "
      
        + " Rate = ?Rate, "
      
        + " RatePercent = ?RatePercent, "
      
        + " Amount = ?Amount, "
      
        + " QbSalesTaxCodeListId = ?QbSalesTaxCodeListId "
      
        + " Where "
        
          + " TxnLineID = ?TxnLineID "
        
      ;

      public static void Update(QbCreditMemoLine qbCreditMemoLine, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?TxnLineID", qbCreditMemoLine.TxnLineID);
      
        Database.PutParameter(dbCommand,"?QbCreditMemoTxnID", qbCreditMemoLine.QbCreditMemoTxnID);
      
        Database.PutParameter(dbCommand,"?QbItemListId", qbCreditMemoLine.QbItemListId);
      
        Database.PutParameter(dbCommand,"?Description", qbCreditMemoLine.Description);
      
        Database.PutParameter(dbCommand,"?Quantity", qbCreditMemoLine.Quantity);
      
        Database.PutParameter(dbCommand,"?Rate", qbCreditMemoLine.Rate);
      
        Database.PutParameter(dbCommand,"?RatePercent", qbCreditMemoLine.RatePercent);
      
        Database.PutParameter(dbCommand,"?Amount", qbCreditMemoLine.Amount);
      
        Database.PutParameter(dbCommand,"?QbSalesTaxCodeListId", qbCreditMemoLine.QbSalesTaxCodeListId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbCreditMemoLine qbCreditMemoLine)
      {
        Update(qbCreditMemoLine, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TxnLineID, "
      
        + " QbCreditMemoTxnID, "
      
        + " QbItemListId, "
      
        + " Description, "
      
        + " Quantity, "
      
        + " Rate, "
      
        + " RatePercent, "
      
        + " Amount, "
      
        + " QbSalesTaxCodeListId "
      

      + " From QbCreditMemoLine "

      
        + " Where "
        
          + " TxnLineID = ?TxnLineID "
        
      ;

      public static QbCreditMemoLine FindByPrimaryKey(
      String txnLineID, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TxnLineID", txnLineID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("QbCreditMemoLine not found, search by primary key");

      }

      public static QbCreditMemoLine FindByPrimaryKey(
      String txnLineID
      )
      {
      return FindByPrimaryKey(
      txnLineID, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbCreditMemoLine qbCreditMemoLine, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TxnLineID",qbCreditMemoLine.TxnLineID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbCreditMemoLine qbCreditMemoLine)
      {
      return Exists(qbCreditMemoLine, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbCreditMemoLine limit 1";

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

      public static QbCreditMemoLine Load(IDataReader dataReader, int offset)
      {
      QbCreditMemoLine qbCreditMemoLine = new QbCreditMemoLine();

      qbCreditMemoLine.TxnLineID = dataReader.GetString(0 + offset);
          qbCreditMemoLine.QbCreditMemoTxnID = dataReader.GetString(1 + offset);
          qbCreditMemoLine.QbItemListId = dataReader.GetString(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            qbCreditMemoLine.Description = dataReader.GetString(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            qbCreditMemoLine.Quantity = dataReader.GetDecimal(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            qbCreditMemoLine.Rate = dataReader.GetDecimal(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            qbCreditMemoLine.RatePercent = dataReader.GetDecimal(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            qbCreditMemoLine.Amount = dataReader.GetDecimal(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            qbCreditMemoLine.QbSalesTaxCodeListId = dataReader.GetString(8 + offset);
          

      return qbCreditMemoLine;
      }

      public static QbCreditMemoLine Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbCreditMemoLine "

      
        + " Where "
        
          + " TxnLineID = ?TxnLineID "
        
      ;
      public static void Delete(QbCreditMemoLine qbCreditMemoLine, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?TxnLineID", qbCreditMemoLine.TxnLineID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbCreditMemoLine qbCreditMemoLine)
      {
        Delete(qbCreditMemoLine, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbCreditMemoLine ";

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

      
        + " TxnLineID, "
      
        + " QbCreditMemoTxnID, "
      
        + " QbItemListId, "
      
        + " Description, "
      
        + " Quantity, "
      
        + " Rate, "
      
        + " RatePercent, "
      
        + " Amount, "
      
        + " QbSalesTaxCodeListId "
      

      + " From QbCreditMemoLine ";
      public static List<QbCreditMemoLine> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbCreditMemoLine> rv = new List<QbCreditMemoLine>();

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

      public static List<QbCreditMemoLine> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbCreditMemoLine> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbCreditMemoLine obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return TxnLineID == obj.TxnLineID && QbCreditMemoTxnID == obj.QbCreditMemoTxnID && QbItemListId == obj.QbItemListId && Description == obj.Description && Quantity == obj.Quantity && Rate == obj.Rate && RatePercent == obj.RatePercent && Amount == obj.Amount && QbSalesTaxCodeListId == obj.QbSalesTaxCodeListId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbCreditMemoLine> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbCreditMemoLine));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbCreditMemoLine item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbCreditMemoLine>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbCreditMemoLine));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbCreditMemoLine> itemsList
      = new List<QbCreditMemoLine>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbCreditMemoLine)
      itemsList.Add(deserializedObject as QbCreditMemoLine);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_txnLineID;
      
        protected String m_qbCreditMemoTxnID;
      
        protected String m_qbItemListId;
      
        protected String m_description;
      
        protected decimal m_quantity;
      
        protected decimal m_rate;
      
        protected decimal m_ratePercent;
      
        protected decimal m_amount;
      
        protected String m_qbSalesTaxCodeListId;
      
      #endregion

      #region Constructors
      public QbCreditMemoLine(
      String 
          txnLineID
      ) : this()
      {
      
        m_txnLineID = txnLineID;
      
      }

      


        public QbCreditMemoLine(
        String 
          txnLineID,String 
          qbCreditMemoTxnID,String 
          qbItemListId,String 
          description,decimal 
          quantity,decimal 
          rate,decimal 
          ratePercent,decimal 
          amount,String 
          qbSalesTaxCodeListId
        ) : this()
        {
        
          m_txnLineID = txnLineID;
        
          m_qbCreditMemoTxnID = qbCreditMemoTxnID;
        
          m_qbItemListId = qbItemListId;
        
          m_description = description;
        
          m_quantity = quantity;
        
          m_rate = rate;
        
          m_ratePercent = ratePercent;
        
          m_amount = amount;
        
          m_qbSalesTaxCodeListId = qbSalesTaxCodeListId;
        
        }


      
      #endregion

      
        [XmlElement]
        public String TxnLineID
        {
        get { return m_txnLineID;}
        set { m_txnLineID = value; }
        }
      
        [XmlElement]
        public String QbCreditMemoTxnID
        {
        get { return m_qbCreditMemoTxnID;}
        set { m_qbCreditMemoTxnID = value; }
        }
      
        [XmlElement]
        public String QbItemListId
        {
        get { return m_qbItemListId;}
        set { m_qbItemListId = value; }
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
      

      public static int FieldsCount
      {
      get { return 9; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    