
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Data;
    using Dalworth.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Domain
      {


      public partial class WorkTransaction : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [WorkTransaction] ( " +
      
        " ID, " +
      
        " WorkId, " +
      
        " VisitId, " +
      
        " WorkTransactionTypeId, " +
      
        " TransactionDate, " +
      
        " AmountCollected, " +
      
        " IsSent, " +
      
        " Notes " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @WorkId, " +
      
        " @VisitId, " +
      
        " @WorkTransactionTypeId, " +
      
        " @TransactionDate, " +
      
        " @AmountCollected, " +
      
        " @IsSent, " +
      
        " @Notes " +
      
      ")";

      public static void Insert(WorkTransaction workTransaction, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransaction.ID);
      
        Database.PutParameter(dbCommand,"@WorkId", workTransaction.WorkId);
      
        Database.PutParameter(dbCommand,"@VisitId", workTransaction.VisitId);
      
        Database.PutParameter(dbCommand,"@WorkTransactionTypeId", workTransaction.WorkTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@TransactionDate", workTransaction.TransactionDate);
      
        Database.PutParameter(dbCommand,"@AmountCollected", workTransaction.AmountCollected);
      
        Database.PutParameter(dbCommand,"@IsSent", workTransaction.IsSent);
      
        Database.PutParameter(dbCommand,"@Notes", workTransaction.Notes);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(WorkTransaction workTransaction)
      {
        Insert(workTransaction, null);
      }

      public static void Insert(List<WorkTransaction>  workTransactionList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(WorkTransaction workTransaction in  workTransactionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransaction.ID);
      
        Database.PutParameter(dbCommand,"@WorkId", workTransaction.WorkId);
      
        Database.PutParameter(dbCommand,"@VisitId", workTransaction.VisitId);
      
        Database.PutParameter(dbCommand,"@WorkTransactionTypeId", workTransaction.WorkTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@TransactionDate", workTransaction.TransactionDate);
      
        Database.PutParameter(dbCommand,"@AmountCollected", workTransaction.AmountCollected);
      
        Database.PutParameter(dbCommand,"@IsSent", workTransaction.IsSent);
      
        Database.PutParameter(dbCommand,"@Notes", workTransaction.Notes);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",workTransaction.ID);
      
        Database.UpdateParameter(dbCommand,"@WorkId",workTransaction.WorkId);
      
        Database.UpdateParameter(dbCommand,"@VisitId",workTransaction.VisitId);
      
        Database.UpdateParameter(dbCommand,"@WorkTransactionTypeId",workTransaction.WorkTransactionTypeId);
      
        Database.UpdateParameter(dbCommand,"@TransactionDate",workTransaction.TransactionDate);
      
        Database.UpdateParameter(dbCommand,"@AmountCollected",workTransaction.AmountCollected);
      
        Database.UpdateParameter(dbCommand,"@IsSent",workTransaction.IsSent);
      
        Database.UpdateParameter(dbCommand,"@Notes",workTransaction.Notes);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<WorkTransaction>  workTransactionList)
      {
      Insert(workTransactionList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [WorkTransaction] Set "
      
        + " WorkId = @WorkId, "
      
        + " VisitId = @VisitId, "
      
        + " WorkTransactionTypeId = @WorkTransactionTypeId, "
      
        + " TransactionDate = @TransactionDate, "
      
        + " AmountCollected = @AmountCollected, "
      
        + " IsSent = @IsSent, "
      
        + " Notes = @Notes "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(WorkTransaction workTransaction, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransaction.ID);
      
        Database.PutParameter(dbCommand,"@WorkId", workTransaction.WorkId);
      
        Database.PutParameter(dbCommand,"@VisitId", workTransaction.VisitId);
      
        Database.PutParameter(dbCommand,"@WorkTransactionTypeId", workTransaction.WorkTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@TransactionDate", workTransaction.TransactionDate);
      
        Database.PutParameter(dbCommand,"@AmountCollected", workTransaction.AmountCollected);
      
        Database.PutParameter(dbCommand,"@IsSent", workTransaction.IsSent);
      
        Database.PutParameter(dbCommand,"@Notes", workTransaction.Notes);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkTransaction workTransaction)
      {
      Update(workTransaction, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " WorkId, "
      
        + " VisitId, "
      
        + " WorkTransactionTypeId, "
      
        + " TransactionDate, "
      
        + " AmountCollected, "
      
        + " IsSent, "
      
        + " Notes "
      

      + " From [WorkTransaction] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static WorkTransaction FindByPrimaryKey(
      int iD, IDbTransaction transaction
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", iD);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("WorkTransaction not found, search by primary key");

      }

      public static WorkTransaction FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(WorkTransaction workTransaction, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",workTransaction.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkTransaction workTransaction)
      {
      return Exists(workTransaction, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from WorkTransaction";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, transaction))
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

      public static WorkTransaction Load(IDataReader dataReader)
      {
      WorkTransaction workTransaction = new WorkTransaction();

      workTransaction.ID = dataReader.GetInt32(0);
          workTransaction.WorkId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            workTransaction.VisitId = dataReader.GetInt32(2);
          workTransaction.WorkTransactionTypeId = dataReader.GetInt32(3);
          
            if(!dataReader.IsDBNull(4))
            workTransaction.TransactionDate = dataReader.GetDateTime(4);
          
            if(!dataReader.IsDBNull(5))
            workTransaction.AmountCollected = dataReader.GetDecimal(5);
          workTransaction.IsSent = dataReader.GetBoolean(6);
          
            if(!dataReader.IsDBNull(7))
            workTransaction.Notes = dataReader.GetString(7);
          

      return workTransaction;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [WorkTransaction] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(WorkTransaction workTransaction, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", workTransaction.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransaction workTransaction)
      {
      Delete(workTransaction, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [WorkTransaction] ";

      public static void Clear(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, transaction))
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
      
        + " WorkId, "
      
        + " VisitId, "
      
        + " WorkTransactionTypeId, "
      
        + " TransactionDate, "
      
        + " AmountCollected, "
      
        + " IsSent, "
      
        + " Notes "
      

      + " From [WorkTransaction] ";
      public static List<WorkTransaction> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<WorkTransaction> rv = new List<WorkTransaction>();

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

      public static List<WorkTransaction> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkTransaction> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<WorkTransaction> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransaction));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkTransaction item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkTransaction>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransaction));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkTransaction> itemsList
      = new List<WorkTransaction>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkTransaction)
      itemsList.Add(deserializedObject as WorkTransaction);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_workId;
      
        protected int? m_visitId;
      
        protected int m_workTransactionTypeId;
      
        protected DateTime? m_transactionDate;
      
        protected decimal m_amountCollected;
      
        protected bool m_isSent;
      
        protected String m_notes;
      
      #endregion

      #region Constructors
      public WorkTransaction(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public WorkTransaction(
        int 
          iD,int 
          workId,int? 
          visitId,int 
          workTransactionTypeId,DateTime? 
          transactionDate,decimal 
          amountCollected,bool 
          isSent,String 
          notes
        )
        {
        
          m_iD = iD;
        
          m_workId = workId;
        
          m_visitId = visitId;
        
          m_workTransactionTypeId = workTransactionTypeId;
        
          m_transactionDate = transactionDate;
        
          m_amountCollected = amountCollected;
        
          m_isSent = isSent;
        
          m_notes = notes;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int WorkId
        {
        get { return m_workId;}
        set { m_workId = value; }
        }
      
        [XmlElement]
        public int? VisitId
        {
        get { return m_visitId;}
        set { m_visitId = value; }
        }
      
        [XmlElement]
        public int WorkTransactionTypeId
        {
        get { return m_workTransactionTypeId;}
        set { m_workTransactionTypeId = value; }
        }
      
        [XmlElement]
        public DateTime? TransactionDate
        {
        get { return m_transactionDate;}
        set { m_transactionDate = value; }
        }
      
        [XmlElement]
        public decimal AmountCollected
        {
        get { return m_amountCollected;}
        set { m_amountCollected = value; }
        }
      
        [XmlElement]
        public bool IsSent
        {
        get { return m_isSent;}
        set { m_isSent = value; }
        }
      
        [XmlElement]
        public String Notes
        {
        get { return m_notes;}
        set { m_notes = value; }
        }
      
      }
      #endregion
      }

    