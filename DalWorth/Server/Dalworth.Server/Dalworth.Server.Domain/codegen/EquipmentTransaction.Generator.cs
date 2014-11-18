
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


      public partial class EquipmentTransaction : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into EquipmentTransaction ( " +
      
        " WorkTransactionId, " +
      
        " EmployeeId, " +
      
        " SequenceDate, " +
      
        " TransactionDate, " +
      
        " Notes " +
      
      ") Values (" +
      
        " ?WorkTransactionId, " +
      
        " ?EmployeeId, " +
      
        " ?SequenceDate, " +
      
        " ?TransactionDate, " +
      
        " ?Notes " +
      
      ")";

      public static void Insert(EquipmentTransaction equipmentTransaction, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", equipmentTransaction.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?EmployeeId", equipmentTransaction.EmployeeId);
      
        Database.PutParameter(dbCommand,"?SequenceDate", equipmentTransaction.SequenceDate);
      
        Database.PutParameter(dbCommand,"?TransactionDate", equipmentTransaction.TransactionDate);
      
        Database.PutParameter(dbCommand,"?Notes", equipmentTransaction.Notes);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        equipmentTransaction.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(EquipmentTransaction equipmentTransaction)
      {
        Insert(equipmentTransaction, null);
      }


      public static void Insert(List<EquipmentTransaction>  equipmentTransactionList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(EquipmentTransaction equipmentTransaction in  equipmentTransactionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", equipmentTransaction.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?EmployeeId", equipmentTransaction.EmployeeId);
      
        Database.PutParameter(dbCommand,"?SequenceDate", equipmentTransaction.SequenceDate);
      
        Database.PutParameter(dbCommand,"?TransactionDate", equipmentTransaction.TransactionDate);
      
        Database.PutParameter(dbCommand,"?Notes", equipmentTransaction.Notes);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WorkTransactionId",equipmentTransaction.WorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"?EmployeeId",equipmentTransaction.EmployeeId);
      
        Database.UpdateParameter(dbCommand,"?SequenceDate",equipmentTransaction.SequenceDate);
      
        Database.UpdateParameter(dbCommand,"?TransactionDate",equipmentTransaction.TransactionDate);
      
        Database.UpdateParameter(dbCommand,"?Notes",equipmentTransaction.Notes);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        equipmentTransaction.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<EquipmentTransaction>  equipmentTransactionList)
      {
        Insert(equipmentTransactionList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update EquipmentTransaction Set "
      
        + " WorkTransactionId = ?WorkTransactionId, "
      
        + " EmployeeId = ?EmployeeId, "
      
        + " SequenceDate = ?SequenceDate, "
      
        + " TransactionDate = ?TransactionDate, "
      
        + " Notes = ?Notes "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(EquipmentTransaction equipmentTransaction, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", equipmentTransaction.ID);
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", equipmentTransaction.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?EmployeeId", equipmentTransaction.EmployeeId);
      
        Database.PutParameter(dbCommand,"?SequenceDate", equipmentTransaction.SequenceDate);
      
        Database.PutParameter(dbCommand,"?TransactionDate", equipmentTransaction.TransactionDate);
      
        Database.PutParameter(dbCommand,"?Notes", equipmentTransaction.Notes);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(EquipmentTransaction equipmentTransaction)
      {
        Update(equipmentTransaction, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " WorkTransactionId, "
      
        + " EmployeeId, "
      
        + " SequenceDate, "
      
        + " TransactionDate, "
      
        + " Notes "
      

      + " From EquipmentTransaction "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static EquipmentTransaction FindByPrimaryKey(
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
      throw new DataNotFoundException("EquipmentTransaction not found, search by primary key");

      }

      public static EquipmentTransaction FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(EquipmentTransaction equipmentTransaction, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",equipmentTransaction.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(EquipmentTransaction equipmentTransaction)
      {
      return Exists(equipmentTransaction, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from EquipmentTransaction limit 1";

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

      public static EquipmentTransaction Load(IDataReader dataReader, int offset)
      {
      EquipmentTransaction equipmentTransaction = new EquipmentTransaction();

      equipmentTransaction.ID = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            equipmentTransaction.WorkTransactionId = dataReader.GetInt32(1 + offset);
          equipmentTransaction.EmployeeId = dataReader.GetInt32(2 + offset);
          equipmentTransaction.SequenceDate = dataReader.GetDateTime(3 + offset);
          equipmentTransaction.TransactionDate = dataReader.GetDateTime(4 + offset);
          equipmentTransaction.Notes = dataReader.GetString(5 + offset);
          

      return equipmentTransaction;
      }

      public static EquipmentTransaction Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From EquipmentTransaction "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(EquipmentTransaction equipmentTransaction, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", equipmentTransaction.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(EquipmentTransaction equipmentTransaction)
      {
        Delete(equipmentTransaction, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From EquipmentTransaction ";

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
      
        + " WorkTransactionId, "
      
        + " EmployeeId, "
      
        + " SequenceDate, "
      
        + " TransactionDate, "
      
        + " Notes "
      

      + " From EquipmentTransaction ";
      public static List<EquipmentTransaction> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<EquipmentTransaction> rv = new List<EquipmentTransaction>();

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

      public static List<EquipmentTransaction> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<EquipmentTransaction> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<EquipmentTransaction> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(EquipmentTransaction));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(EquipmentTransaction item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<EquipmentTransaction>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(EquipmentTransaction));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<EquipmentTransaction> itemsList
      = new List<EquipmentTransaction>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is EquipmentTransaction)
      itemsList.Add(deserializedObject as EquipmentTransaction);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int? m_workTransactionId;
      
        protected int m_employeeId;
      
        protected DateTime m_sequenceDate;
      
        protected DateTime m_transactionDate;
      
        protected String m_notes;
      
      #endregion

      #region Constructors
      public EquipmentTransaction(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public EquipmentTransaction(
        int 
          iD,int? 
          workTransactionId,int 
          employeeId,DateTime 
          sequenceDate,DateTime 
          transactionDate,String 
          notes
        ) : this()
        {
        
          m_iD = iD;
        
          m_workTransactionId = workTransactionId;
        
          m_employeeId = employeeId;
        
          m_sequenceDate = sequenceDate;
        
          m_transactionDate = transactionDate;
        
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
        public int? WorkTransactionId
        {
        get { return m_workTransactionId;}
        set { m_workTransactionId = value; }
        }
      
        [XmlElement]
        public int EmployeeId
        {
        get { return m_employeeId;}
        set { m_employeeId = value; }
        }
      
        [XmlElement]
        public DateTime SequenceDate
        {
        get { return m_sequenceDate;}
        set { m_sequenceDate = value; }
        }
      
        [XmlElement]
        public DateTime TransactionDate
        {
        get { return m_transactionDate;}
        set { m_transactionDate = value; }
        }
      
        [XmlElement]
        public String Notes
        {
        get { return m_notes;}
        set { m_notes = value; }
        }
      

      public static int FieldsCount
      {
      get { return 6; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    