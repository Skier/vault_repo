
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


      public partial class QbCreditMemo : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbCreditMemo ( " +
      
        " TxnID, " +
      
        " QbCustomerId, " +
      
        " TimeCreatedInQb, " +
      
        " TimeModifiedInQb, " +
      
        " EditSequence, " +
      
        " TxnNumber, " +
      
        " QbClassListId, " +
      
        " QbAccountListId, " +
      
        " QbTemplateListId, " +
      
        " TxnDate, " +
      
        " RefNumber, " +
      
        " IsPending, " +
      
        " TermsRefListId, " +
      
        " SalesRepRefListId, " +
      
        " SubTotalAmount, " +
      
        " ItemSalesTaxRef, " +
      
        " SalesTaxPercentage, " +
      
        " TaxAmount, " +
      
        " TotalAmount, " +
      
        " CreditRemaining, " +
      
        " Memo " +
      
      ") Values (" +
      
        " ?TxnID, " +
      
        " ?QbCustomerId, " +
      
        " ?TimeCreatedInQb, " +
      
        " ?TimeModifiedInQb, " +
      
        " ?EditSequence, " +
      
        " ?TxnNumber, " +
      
        " ?QbClassListId, " +
      
        " ?QbAccountListId, " +
      
        " ?QbTemplateListId, " +
      
        " ?TxnDate, " +
      
        " ?RefNumber, " +
      
        " ?IsPending, " +
      
        " ?TermsRefListId, " +
      
        " ?SalesRepRefListId, " +
      
        " ?SubTotalAmount, " +
      
        " ?ItemSalesTaxRef, " +
      
        " ?SalesTaxPercentage, " +
      
        " ?TaxAmount, " +
      
        " ?TotalAmount, " +
      
        " ?CreditRemaining, " +
      
        " ?Memo " +
      
      ")";

      public static void Insert(QbCreditMemo qbCreditMemo, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TxnID", qbCreditMemo.TxnID);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", qbCreditMemo.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?TimeCreatedInQb", qbCreditMemo.TimeCreatedInQb);
      
        Database.PutParameter(dbCommand,"?TimeModifiedInQb", qbCreditMemo.TimeModifiedInQb);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbCreditMemo.EditSequence);
      
        Database.PutParameter(dbCommand,"?TxnNumber", qbCreditMemo.TxnNumber);
      
        Database.PutParameter(dbCommand,"?QbClassListId", qbCreditMemo.QbClassListId);
      
        Database.PutParameter(dbCommand,"?QbAccountListId", qbCreditMemo.QbAccountListId);
      
        Database.PutParameter(dbCommand,"?QbTemplateListId", qbCreditMemo.QbTemplateListId);
      
        Database.PutParameter(dbCommand,"?TxnDate", qbCreditMemo.TxnDate);
      
        Database.PutParameter(dbCommand,"?RefNumber", qbCreditMemo.RefNumber);
      
        Database.PutParameter(dbCommand,"?IsPending", qbCreditMemo.IsPending);
      
        Database.PutParameter(dbCommand,"?TermsRefListId", qbCreditMemo.TermsRefListId);
      
        Database.PutParameter(dbCommand,"?SalesRepRefListId", qbCreditMemo.SalesRepRefListId);
      
        Database.PutParameter(dbCommand,"?SubTotalAmount", qbCreditMemo.SubTotalAmount);
      
        Database.PutParameter(dbCommand,"?ItemSalesTaxRef", qbCreditMemo.ItemSalesTaxRef);
      
        Database.PutParameter(dbCommand,"?SalesTaxPercentage", qbCreditMemo.SalesTaxPercentage);
      
        Database.PutParameter(dbCommand,"?TaxAmount", qbCreditMemo.TaxAmount);
      
        Database.PutParameter(dbCommand,"?TotalAmount", qbCreditMemo.TotalAmount);
      
        Database.PutParameter(dbCommand,"?CreditRemaining", qbCreditMemo.CreditRemaining);
      
        Database.PutParameter(dbCommand,"?Memo", qbCreditMemo.Memo);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbCreditMemo qbCreditMemo)
      {
        Insert(qbCreditMemo, null);
      }


      public static void Insert(List<QbCreditMemo>  qbCreditMemoList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbCreditMemo qbCreditMemo in  qbCreditMemoList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TxnID", qbCreditMemo.TxnID);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", qbCreditMemo.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?TimeCreatedInQb", qbCreditMemo.TimeCreatedInQb);
      
        Database.PutParameter(dbCommand,"?TimeModifiedInQb", qbCreditMemo.TimeModifiedInQb);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbCreditMemo.EditSequence);
      
        Database.PutParameter(dbCommand,"?TxnNumber", qbCreditMemo.TxnNumber);
      
        Database.PutParameter(dbCommand,"?QbClassListId", qbCreditMemo.QbClassListId);
      
        Database.PutParameter(dbCommand,"?QbAccountListId", qbCreditMemo.QbAccountListId);
      
        Database.PutParameter(dbCommand,"?QbTemplateListId", qbCreditMemo.QbTemplateListId);
      
        Database.PutParameter(dbCommand,"?TxnDate", qbCreditMemo.TxnDate);
      
        Database.PutParameter(dbCommand,"?RefNumber", qbCreditMemo.RefNumber);
      
        Database.PutParameter(dbCommand,"?IsPending", qbCreditMemo.IsPending);
      
        Database.PutParameter(dbCommand,"?TermsRefListId", qbCreditMemo.TermsRefListId);
      
        Database.PutParameter(dbCommand,"?SalesRepRefListId", qbCreditMemo.SalesRepRefListId);
      
        Database.PutParameter(dbCommand,"?SubTotalAmount", qbCreditMemo.SubTotalAmount);
      
        Database.PutParameter(dbCommand,"?ItemSalesTaxRef", qbCreditMemo.ItemSalesTaxRef);
      
        Database.PutParameter(dbCommand,"?SalesTaxPercentage", qbCreditMemo.SalesTaxPercentage);
      
        Database.PutParameter(dbCommand,"?TaxAmount", qbCreditMemo.TaxAmount);
      
        Database.PutParameter(dbCommand,"?TotalAmount", qbCreditMemo.TotalAmount);
      
        Database.PutParameter(dbCommand,"?CreditRemaining", qbCreditMemo.CreditRemaining);
      
        Database.PutParameter(dbCommand,"?Memo", qbCreditMemo.Memo);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TxnID",qbCreditMemo.TxnID);
      
        Database.UpdateParameter(dbCommand,"?QbCustomerId",qbCreditMemo.QbCustomerId);
      
        Database.UpdateParameter(dbCommand,"?TimeCreatedInQb",qbCreditMemo.TimeCreatedInQb);
      
        Database.UpdateParameter(dbCommand,"?TimeModifiedInQb",qbCreditMemo.TimeModifiedInQb);
      
        Database.UpdateParameter(dbCommand,"?EditSequence",qbCreditMemo.EditSequence);
      
        Database.UpdateParameter(dbCommand,"?TxnNumber",qbCreditMemo.TxnNumber);
      
        Database.UpdateParameter(dbCommand,"?QbClassListId",qbCreditMemo.QbClassListId);
      
        Database.UpdateParameter(dbCommand,"?QbAccountListId",qbCreditMemo.QbAccountListId);
      
        Database.UpdateParameter(dbCommand,"?QbTemplateListId",qbCreditMemo.QbTemplateListId);
      
        Database.UpdateParameter(dbCommand,"?TxnDate",qbCreditMemo.TxnDate);
      
        Database.UpdateParameter(dbCommand,"?RefNumber",qbCreditMemo.RefNumber);
      
        Database.UpdateParameter(dbCommand,"?IsPending",qbCreditMemo.IsPending);
      
        Database.UpdateParameter(dbCommand,"?TermsRefListId",qbCreditMemo.TermsRefListId);
      
        Database.UpdateParameter(dbCommand,"?SalesRepRefListId",qbCreditMemo.SalesRepRefListId);
      
        Database.UpdateParameter(dbCommand,"?SubTotalAmount",qbCreditMemo.SubTotalAmount);
      
        Database.UpdateParameter(dbCommand,"?ItemSalesTaxRef",qbCreditMemo.ItemSalesTaxRef);
      
        Database.UpdateParameter(dbCommand,"?SalesTaxPercentage",qbCreditMemo.SalesTaxPercentage);
      
        Database.UpdateParameter(dbCommand,"?TaxAmount",qbCreditMemo.TaxAmount);
      
        Database.UpdateParameter(dbCommand,"?TotalAmount",qbCreditMemo.TotalAmount);
      
        Database.UpdateParameter(dbCommand,"?CreditRemaining",qbCreditMemo.CreditRemaining);
      
        Database.UpdateParameter(dbCommand,"?Memo",qbCreditMemo.Memo);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbCreditMemo>  qbCreditMemoList)
      {
        Insert(qbCreditMemoList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbCreditMemo Set "
      
        + " QbCustomerId = ?QbCustomerId, "
      
        + " TimeCreatedInQb = ?TimeCreatedInQb, "
      
        + " TimeModifiedInQb = ?TimeModifiedInQb, "
      
        + " EditSequence = ?EditSequence, "
      
        + " TxnNumber = ?TxnNumber, "
      
        + " QbClassListId = ?QbClassListId, "
      
        + " QbAccountListId = ?QbAccountListId, "
      
        + " QbTemplateListId = ?QbTemplateListId, "
      
        + " TxnDate = ?TxnDate, "
      
        + " RefNumber = ?RefNumber, "
      
        + " IsPending = ?IsPending, "
      
        + " TermsRefListId = ?TermsRefListId, "
      
        + " SalesRepRefListId = ?SalesRepRefListId, "
      
        + " SubTotalAmount = ?SubTotalAmount, "
      
        + " ItemSalesTaxRef = ?ItemSalesTaxRef, "
      
        + " SalesTaxPercentage = ?SalesTaxPercentage, "
      
        + " TaxAmount = ?TaxAmount, "
      
        + " TotalAmount = ?TotalAmount, "
      
        + " CreditRemaining = ?CreditRemaining, "
      
        + " Memo = ?Memo "
      
        + " Where "
        
          + " TxnID = ?TxnID "
        
      ;

      public static void Update(QbCreditMemo qbCreditMemo, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?TxnID", qbCreditMemo.TxnID);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", qbCreditMemo.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?TimeCreatedInQb", qbCreditMemo.TimeCreatedInQb);
      
        Database.PutParameter(dbCommand,"?TimeModifiedInQb", qbCreditMemo.TimeModifiedInQb);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbCreditMemo.EditSequence);
      
        Database.PutParameter(dbCommand,"?TxnNumber", qbCreditMemo.TxnNumber);
      
        Database.PutParameter(dbCommand,"?QbClassListId", qbCreditMemo.QbClassListId);
      
        Database.PutParameter(dbCommand,"?QbAccountListId", qbCreditMemo.QbAccountListId);
      
        Database.PutParameter(dbCommand,"?QbTemplateListId", qbCreditMemo.QbTemplateListId);
      
        Database.PutParameter(dbCommand,"?TxnDate", qbCreditMemo.TxnDate);
      
        Database.PutParameter(dbCommand,"?RefNumber", qbCreditMemo.RefNumber);
      
        Database.PutParameter(dbCommand,"?IsPending", qbCreditMemo.IsPending);
      
        Database.PutParameter(dbCommand,"?TermsRefListId", qbCreditMemo.TermsRefListId);
      
        Database.PutParameter(dbCommand,"?SalesRepRefListId", qbCreditMemo.SalesRepRefListId);
      
        Database.PutParameter(dbCommand,"?SubTotalAmount", qbCreditMemo.SubTotalAmount);
      
        Database.PutParameter(dbCommand,"?ItemSalesTaxRef", qbCreditMemo.ItemSalesTaxRef);
      
        Database.PutParameter(dbCommand,"?SalesTaxPercentage", qbCreditMemo.SalesTaxPercentage);
      
        Database.PutParameter(dbCommand,"?TaxAmount", qbCreditMemo.TaxAmount);
      
        Database.PutParameter(dbCommand,"?TotalAmount", qbCreditMemo.TotalAmount);
      
        Database.PutParameter(dbCommand,"?CreditRemaining", qbCreditMemo.CreditRemaining);
      
        Database.PutParameter(dbCommand,"?Memo", qbCreditMemo.Memo);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbCreditMemo qbCreditMemo)
      {
        Update(qbCreditMemo, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TxnID, "
      
        + " QbCustomerId, "
      
        + " TimeCreatedInQb, "
      
        + " TimeModifiedInQb, "
      
        + " EditSequence, "
      
        + " TxnNumber, "
      
        + " QbClassListId, "
      
        + " QbAccountListId, "
      
        + " QbTemplateListId, "
      
        + " TxnDate, "
      
        + " RefNumber, "
      
        + " IsPending, "
      
        + " TermsRefListId, "
      
        + " SalesRepRefListId, "
      
        + " SubTotalAmount, "
      
        + " ItemSalesTaxRef, "
      
        + " SalesTaxPercentage, "
      
        + " TaxAmount, "
      
        + " TotalAmount, "
      
        + " CreditRemaining, "
      
        + " Memo "
      

      + " From QbCreditMemo "

      
        + " Where "
        
          + " TxnID = ?TxnID "
        
      ;

      public static QbCreditMemo FindByPrimaryKey(
      String txnID, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TxnID", txnID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("QbCreditMemo not found, search by primary key");

      }

      public static QbCreditMemo FindByPrimaryKey(
      String txnID
      )
      {
      return FindByPrimaryKey(
      txnID, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbCreditMemo qbCreditMemo, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TxnID",qbCreditMemo.TxnID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbCreditMemo qbCreditMemo)
      {
      return Exists(qbCreditMemo, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbCreditMemo limit 1";

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

      public static QbCreditMemo Load(IDataReader dataReader, int offset)
      {
      QbCreditMemo qbCreditMemo = new QbCreditMemo();

      qbCreditMemo.TxnID = dataReader.GetString(0 + offset);
          qbCreditMemo.QbCustomerId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            qbCreditMemo.TimeCreatedInQb = dataReader.GetDateTime(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            qbCreditMemo.TimeModifiedInQb = dataReader.GetDateTime(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            qbCreditMemo.EditSequence = dataReader.GetString(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            qbCreditMemo.TxnNumber = dataReader.GetInt32(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            qbCreditMemo.QbClassListId = dataReader.GetString(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            qbCreditMemo.QbAccountListId = dataReader.GetString(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            qbCreditMemo.QbTemplateListId = dataReader.GetString(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            qbCreditMemo.TxnDate = dataReader.GetDateTime(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            qbCreditMemo.RefNumber = dataReader.GetString(10 + offset);
          qbCreditMemo.IsPending = dataReader.GetBoolean(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            qbCreditMemo.TermsRefListId = dataReader.GetString(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            qbCreditMemo.SalesRepRefListId = dataReader.GetString(13 + offset);
          
            if(!dataReader.IsDBNull(14 + offset))
            qbCreditMemo.SubTotalAmount = dataReader.GetDecimal(14 + offset);
          
            if(!dataReader.IsDBNull(15 + offset))
            qbCreditMemo.ItemSalesTaxRef = dataReader.GetString(15 + offset);
          
            if(!dataReader.IsDBNull(16 + offset))
            qbCreditMemo.SalesTaxPercentage = dataReader.GetString(16 + offset);
          
            if(!dataReader.IsDBNull(17 + offset))
            qbCreditMemo.TaxAmount = dataReader.GetDecimal(17 + offset);
          
            if(!dataReader.IsDBNull(18 + offset))
            qbCreditMemo.TotalAmount = dataReader.GetDecimal(18 + offset);
          
            if(!dataReader.IsDBNull(19 + offset))
            qbCreditMemo.CreditRemaining = dataReader.GetDecimal(19 + offset);
          
            if(!dataReader.IsDBNull(20 + offset))
            qbCreditMemo.Memo = dataReader.GetString(20 + offset);
          

      return qbCreditMemo;
      }

      public static QbCreditMemo Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbCreditMemo "

      
        + " Where "
        
          + " TxnID = ?TxnID "
        
      ;
      public static void Delete(QbCreditMemo qbCreditMemo, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?TxnID", qbCreditMemo.TxnID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbCreditMemo qbCreditMemo)
      {
        Delete(qbCreditMemo, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbCreditMemo ";

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

      
        + " TxnID, "
      
        + " QbCustomerId, "
      
        + " TimeCreatedInQb, "
      
        + " TimeModifiedInQb, "
      
        + " EditSequence, "
      
        + " TxnNumber, "
      
        + " QbClassListId, "
      
        + " QbAccountListId, "
      
        + " QbTemplateListId, "
      
        + " TxnDate, "
      
        + " RefNumber, "
      
        + " IsPending, "
      
        + " TermsRefListId, "
      
        + " SalesRepRefListId, "
      
        + " SubTotalAmount, "
      
        + " ItemSalesTaxRef, "
      
        + " SalesTaxPercentage, "
      
        + " TaxAmount, "
      
        + " TotalAmount, "
      
        + " CreditRemaining, "
      
        + " Memo "
      

      + " From QbCreditMemo ";
      public static List<QbCreditMemo> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbCreditMemo> rv = new List<QbCreditMemo>();

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

      public static List<QbCreditMemo> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbCreditMemo> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbCreditMemo obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return TxnID == obj.TxnID && QbCustomerId == obj.QbCustomerId && TimeCreatedInQb == obj.TimeCreatedInQb && TimeModifiedInQb == obj.TimeModifiedInQb && EditSequence == obj.EditSequence && TxnNumber == obj.TxnNumber && QbClassListId == obj.QbClassListId && QbAccountListId == obj.QbAccountListId && QbTemplateListId == obj.QbTemplateListId && TxnDate == obj.TxnDate && RefNumber == obj.RefNumber && IsPending == obj.IsPending && TermsRefListId == obj.TermsRefListId && SalesRepRefListId == obj.SalesRepRefListId && SubTotalAmount == obj.SubTotalAmount && ItemSalesTaxRef == obj.ItemSalesTaxRef && SalesTaxPercentage == obj.SalesTaxPercentage && TaxAmount == obj.TaxAmount && TotalAmount == obj.TotalAmount && CreditRemaining == obj.CreditRemaining && Memo == obj.Memo;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbCreditMemo> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbCreditMemo));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbCreditMemo item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbCreditMemo>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbCreditMemo));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbCreditMemo> itemsList
      = new List<QbCreditMemo>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbCreditMemo)
      itemsList.Add(deserializedObject as QbCreditMemo);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_txnID;
      
        protected int m_qbCustomerId;
      
        protected DateTime? m_timeCreatedInQb;
      
        protected DateTime? m_timeModifiedInQb;
      
        protected String m_editSequence;
      
        protected int? m_txnNumber;
      
        protected String m_qbClassListId;
      
        protected String m_qbAccountListId;
      
        protected String m_qbTemplateListId;
      
        protected DateTime? m_txnDate;
      
        protected String m_refNumber;
      
        protected bool m_isPending;
      
        protected String m_termsRefListId;
      
        protected String m_salesRepRefListId;
      
        protected decimal m_subTotalAmount;
      
        protected String m_itemSalesTaxRef;
      
        protected String m_salesTaxPercentage;
      
        protected decimal m_taxAmount;
      
        protected decimal m_totalAmount;
      
        protected decimal m_creditRemaining;
      
        protected String m_memo;
      
      #endregion

      #region Constructors
      public QbCreditMemo(
      String 
          txnID
      ) : this()
      {
      
        m_txnID = txnID;
      
      }

      


        public QbCreditMemo(
        String 
          txnID,int 
          qbCustomerId,DateTime? 
          timeCreatedInQb,DateTime? 
          timeModifiedInQb,String 
          editSequence,int? 
          txnNumber,String 
          qbClassListId,String 
          qbAccountListId,String 
          qbTemplateListId,DateTime? 
          txnDate,String 
          refNumber,bool 
          isPending,String 
          termsRefListId,String 
          salesRepRefListId,decimal 
          subTotalAmount,String 
          itemSalesTaxRef,String 
          salesTaxPercentage,decimal 
          taxAmount,decimal 
          totalAmount,decimal 
          creditRemaining,String 
          memo
        ) : this()
        {
        
          m_txnID = txnID;
        
          m_qbCustomerId = qbCustomerId;
        
          m_timeCreatedInQb = timeCreatedInQb;
        
          m_timeModifiedInQb = timeModifiedInQb;
        
          m_editSequence = editSequence;
        
          m_txnNumber = txnNumber;
        
          m_qbClassListId = qbClassListId;
        
          m_qbAccountListId = qbAccountListId;
        
          m_qbTemplateListId = qbTemplateListId;
        
          m_txnDate = txnDate;
        
          m_refNumber = refNumber;
        
          m_isPending = isPending;
        
          m_termsRefListId = termsRefListId;
        
          m_salesRepRefListId = salesRepRefListId;
        
          m_subTotalAmount = subTotalAmount;
        
          m_itemSalesTaxRef = itemSalesTaxRef;
        
          m_salesTaxPercentage = salesTaxPercentage;
        
          m_taxAmount = taxAmount;
        
          m_totalAmount = totalAmount;
        
          m_creditRemaining = creditRemaining;
        
          m_memo = memo;
        
        }


      
      #endregion

      
        [XmlElement]
        public String TxnID
        {
        get { return m_txnID;}
        set { m_txnID = value; }
        }
      
        [XmlElement]
        public int QbCustomerId
        {
        get { return m_qbCustomerId;}
        set { m_qbCustomerId = value; }
        }
      
        [XmlElement]
        public DateTime? TimeCreatedInQb
        {
        get { return m_timeCreatedInQb;}
        set { m_timeCreatedInQb = value; }
        }
      
        [XmlElement]
        public DateTime? TimeModifiedInQb
        {
        get { return m_timeModifiedInQb;}
        set { m_timeModifiedInQb = value; }
        }
      
        [XmlElement]
        public String EditSequence
        {
        get { return m_editSequence;}
        set { m_editSequence = value; }
        }
      
        [XmlElement]
        public int? TxnNumber
        {
        get { return m_txnNumber;}
        set { m_txnNumber = value; }
        }
      
        [XmlElement]
        public String QbClassListId
        {
        get { return m_qbClassListId;}
        set { m_qbClassListId = value; }
        }
      
        [XmlElement]
        public String QbAccountListId
        {
        get { return m_qbAccountListId;}
        set { m_qbAccountListId = value; }
        }
      
        [XmlElement]
        public String QbTemplateListId
        {
        get { return m_qbTemplateListId;}
        set { m_qbTemplateListId = value; }
        }
      
        [XmlElement]
        public DateTime? TxnDate
        {
        get { return m_txnDate;}
        set { m_txnDate = value; }
        }
      
        [XmlElement]
        public String RefNumber
        {
        get { return m_refNumber;}
        set { m_refNumber = value; }
        }
      
        [XmlElement]
        public bool IsPending
        {
        get { return m_isPending;}
        set { m_isPending = value; }
        }
      
        [XmlElement]
        public String TermsRefListId
        {
        get { return m_termsRefListId;}
        set { m_termsRefListId = value; }
        }
      
        [XmlElement]
        public String SalesRepRefListId
        {
        get { return m_salesRepRefListId;}
        set { m_salesRepRefListId = value; }
        }
      
        [XmlElement]
        public decimal SubTotalAmount
        {
        get { return m_subTotalAmount;}
        set { m_subTotalAmount = value; }
        }
      
        [XmlElement]
        public String ItemSalesTaxRef
        {
        get { return m_itemSalesTaxRef;}
        set { m_itemSalesTaxRef = value; }
        }
      
        [XmlElement]
        public String SalesTaxPercentage
        {
        get { return m_salesTaxPercentage;}
        set { m_salesTaxPercentage = value; }
        }
      
        [XmlElement]
        public decimal TaxAmount
        {
        get { return m_taxAmount;}
        set { m_taxAmount = value; }
        }
      
        [XmlElement]
        public decimal TotalAmount
        {
        get { return m_totalAmount;}
        set { m_totalAmount = value; }
        }
      
        [XmlElement]
        public decimal CreditRemaining
        {
        get { return m_creditRemaining;}
        set { m_creditRemaining = value; }
        }
      
        [XmlElement]
        public String Memo
        {
        get { return m_memo;}
        set { m_memo = value; }
        }
      

      public static int FieldsCount
      {
      get { return 21; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    