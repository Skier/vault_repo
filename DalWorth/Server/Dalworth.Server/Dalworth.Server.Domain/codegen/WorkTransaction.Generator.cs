
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


      public partial class WorkTransaction : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WorkTransaction ( " +
      
        " WorkId, " +
      
        " EmployeeId, " +
      
        " VisitId, " +
      
        " WorkTransactionTypeId, " +
      
        " TransactionDate, " +
      
        " AmountCollected, " +
      
        " IsSentToServman " +
      
      ") Values (" +
      
        " ?WorkId, " +
      
        " ?EmployeeId, " +
      
        " ?VisitId, " +
      
        " ?WorkTransactionTypeId, " +
      
        " ?TransactionDate, " +
      
        " ?AmountCollected, " +
      
        " ?IsSentToServman " +
      
      ")";

      public static void Insert(WorkTransaction workTransaction, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkId", workTransaction.WorkId);
      
        Database.PutParameter(dbCommand,"?EmployeeId", workTransaction.EmployeeId);
      
        Database.PutParameter(dbCommand,"?VisitId", workTransaction.VisitId);
      
        Database.PutParameter(dbCommand,"?WorkTransactionTypeId", workTransaction.WorkTransactionTypeId);
      
        Database.PutParameter(dbCommand,"?TransactionDate", workTransaction.TransactionDate);
      
        Database.PutParameter(dbCommand,"?AmountCollected", workTransaction.AmountCollected);
      
        Database.PutParameter(dbCommand,"?IsSentToServman", workTransaction.IsSentToServman);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        workTransaction.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(WorkTransaction workTransaction)
      {
        Insert(workTransaction, null);
      }


      public static void Insert(List<WorkTransaction>  workTransactionList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WorkTransaction workTransaction in  workTransactionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WorkId", workTransaction.WorkId);
      
        Database.PutParameter(dbCommand,"?EmployeeId", workTransaction.EmployeeId);
      
        Database.PutParameter(dbCommand,"?VisitId", workTransaction.VisitId);
      
        Database.PutParameter(dbCommand,"?WorkTransactionTypeId", workTransaction.WorkTransactionTypeId);
      
        Database.PutParameter(dbCommand,"?TransactionDate", workTransaction.TransactionDate);
      
        Database.PutParameter(dbCommand,"?AmountCollected", workTransaction.AmountCollected);
      
        Database.PutParameter(dbCommand,"?IsSentToServman", workTransaction.IsSentToServman);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WorkId",workTransaction.WorkId);
      
        Database.UpdateParameter(dbCommand,"?EmployeeId",workTransaction.EmployeeId);
      
        Database.UpdateParameter(dbCommand,"?VisitId",workTransaction.VisitId);
      
        Database.UpdateParameter(dbCommand,"?WorkTransactionTypeId",workTransaction.WorkTransactionTypeId);
      
        Database.UpdateParameter(dbCommand,"?TransactionDate",workTransaction.TransactionDate);
      
        Database.UpdateParameter(dbCommand,"?AmountCollected",workTransaction.AmountCollected);
      
        Database.UpdateParameter(dbCommand,"?IsSentToServman",workTransaction.IsSentToServman);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        workTransaction.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<WorkTransaction>  workTransactionList)
      {
        Insert(workTransactionList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WorkTransaction Set "
      
        + " WorkId = ?WorkId, "
      
        + " EmployeeId = ?EmployeeId, "
      
        + " VisitId = ?VisitId, "
      
        + " WorkTransactionTypeId = ?WorkTransactionTypeId, "
      
        + " TransactionDate = ?TransactionDate, "
      
        + " AmountCollected = ?AmountCollected, "
      
        + " IsSentToServman = ?IsSentToServman "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(WorkTransaction workTransaction, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", workTransaction.ID);
      
        Database.PutParameter(dbCommand,"?WorkId", workTransaction.WorkId);
      
        Database.PutParameter(dbCommand,"?EmployeeId", workTransaction.EmployeeId);
      
        Database.PutParameter(dbCommand,"?VisitId", workTransaction.VisitId);
      
        Database.PutParameter(dbCommand,"?WorkTransactionTypeId", workTransaction.WorkTransactionTypeId);
      
        Database.PutParameter(dbCommand,"?TransactionDate", workTransaction.TransactionDate);
      
        Database.PutParameter(dbCommand,"?AmountCollected", workTransaction.AmountCollected);
      
        Database.PutParameter(dbCommand,"?IsSentToServman", workTransaction.IsSentToServman);
      

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
      
        + " EmployeeId, "
      
        + " VisitId, "
      
        + " WorkTransactionTypeId, "
      
        + " TransactionDate, "
      
        + " AmountCollected, "
      
        + " IsSentToServman "
      

      + " From WorkTransaction "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static WorkTransaction FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkTransaction not found, search by primary key");

      }

      public static WorkTransaction FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WorkTransaction workTransaction, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",workTransaction.ID);
      

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

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WorkTransaction limit 1";

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

      public static WorkTransaction Load(IDataReader dataReader, int offset)
      {
      WorkTransaction workTransaction = new WorkTransaction();

      workTransaction.ID = dataReader.GetInt32(0 + offset);
          workTransaction.WorkId = dataReader.GetInt32(1 + offset);
          workTransaction.EmployeeId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            workTransaction.VisitId = dataReader.GetInt32(3 + offset);
          workTransaction.WorkTransactionTypeId = dataReader.GetInt32(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            workTransaction.TransactionDate = dataReader.GetDateTime(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            workTransaction.AmountCollected = dataReader.GetDecimal(6 + offset);
          workTransaction.IsSentToServman = dataReader.GetBoolean(7 + offset);
          

      return workTransaction;
      }

      public static WorkTransaction Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WorkTransaction "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(WorkTransaction workTransaction, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", workTransaction.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransaction workTransaction)
      {
        Delete(workTransaction, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WorkTransaction ";

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
      
        + " WorkId, "
      
        + " EmployeeId, "
      
        + " VisitId, "
      
        + " WorkTransactionTypeId, "
      
        + " TransactionDate, "
      
        + " AmountCollected, "
      
        + " IsSentToServman "
      

      + " From WorkTransaction ";
      public static List<WorkTransaction> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
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

      #region ValueEquals

      public bool ValueEquals (WorkTransaction obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && WorkId == obj.WorkId && EmployeeId == obj.EmployeeId && VisitId == obj.VisitId && WorkTransactionTypeId == obj.WorkTransactionTypeId && TransactionDate == obj.TransactionDate && AmountCollected == obj.AmountCollected && IsSentToServman == obj.IsSentToServman;
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
      
        protected int m_employeeId;
      
        protected int? m_visitId;
      
        protected int m_workTransactionTypeId;
      
        protected DateTime? m_transactionDate;
      
        protected decimal m_amountCollected;
      
        protected bool m_isSentToServman;
      
      #endregion

      #region Constructors
      public WorkTransaction(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public WorkTransaction(
        int 
          iD,int 
          workId,int 
          employeeId,int? 
          visitId,int 
          workTransactionTypeId,DateTime? 
          transactionDate,decimal 
          amountCollected,bool 
          isSentToServman
        ) : this()
        {
        
          m_iD = iD;
        
          m_workId = workId;
        
          m_employeeId = employeeId;
        
          m_visitId = visitId;
        
          m_workTransactionTypeId = workTransactionTypeId;
        
          m_transactionDate = transactionDate;
        
          m_amountCollected = amountCollected;
        
          m_isSentToServman = isSentToServman;
        
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
        public int EmployeeId
        {
        get { return m_employeeId;}
        set { m_employeeId = value; }
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
        public bool IsSentToServman
        {
        get { return m_isSentToServman;}
        set { m_isSentToServman = value; }
        }
      

      public static int FieldsCount
      {
      get { return 8; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    