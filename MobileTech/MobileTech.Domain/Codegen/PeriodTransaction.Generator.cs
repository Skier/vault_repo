
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class PeriodTransaction
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into PeriodTransaction ( " +
      
        " SessionId, " +
      
        " BusinessTransactionId, " +
      
        " PeriodTransactionTypeId " +
      
      ") Values (" +
      
        " @SessionId, " +
      
        " @BusinessTransactionId, " +
      
        " @PeriodTransactionTypeId " +
      
      ")";

      public static void Insert(PeriodTransaction periodTransaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", periodTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", periodTransaction.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@PeriodTransactionTypeId", periodTransaction.PeriodTransactionTypeId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<PeriodTransaction>  periodTransactionList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(PeriodTransaction periodTransaction in  periodTransactionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@SessionId", periodTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", periodTransaction.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@PeriodTransactionTypeId", periodTransaction.PeriodTransactionTypeId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@SessionId",periodTransaction.SessionId);
      
        Database.UpdateParameter(dbCommand,"@BusinessTransactionId",periodTransaction.BusinessTransactionId);
      
        Database.UpdateParameter(dbCommand,"@PeriodTransactionTypeId",periodTransaction.PeriodTransactionTypeId);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update PeriodTransaction Set "
      
        + " PeriodTransactionTypeId = @PeriodTransactionTypeId "
      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId "
        
      ;

      public static void Update(PeriodTransaction periodTransaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", periodTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", periodTransaction.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@PeriodTransactionTypeId", periodTransaction.PeriodTransactionTypeId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " SessionId, "
      
        + " BusinessTransactionId, "
      
        + " PeriodTransactionTypeId "
      

      + " From PeriodTransaction "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId "
        
      ;

      public static PeriodTransaction FindByPrimaryKey(
      long sessionId,int businessTransactionId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", sessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", businessTransactionId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("PeriodTransaction not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(PeriodTransaction periodTransaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId",periodTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId",periodTransaction.BusinessTransactionId);
      

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
      String sql = "select 1 from PeriodTransaction";

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

      public static PeriodTransaction Load(IDataReader dataReader)
      {
      PeriodTransaction periodTransaction = new PeriodTransaction();

      periodTransaction.SessionId = dataReader.GetInt64(0);
          periodTransaction.BusinessTransactionId = dataReader.GetInt32(1);
          periodTransaction.PeriodTransactionTypeId = dataReader.GetInt16(2);
          

      return periodTransaction;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From PeriodTransaction "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId "
        
      ;
      public static void Delete(PeriodTransaction periodTransaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@SessionId", periodTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", periodTransaction.BusinessTransactionId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From PeriodTransaction ";

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

      
        + " SessionId, "
      
        + " BusinessTransactionId, "
      
        + " PeriodTransactionTypeId "
      

      + " From PeriodTransaction ";
      public static List<PeriodTransaction> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<PeriodTransaction> rv = new List<PeriodTransaction>();

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
        List<PeriodTransaction> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<PeriodTransaction> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(PeriodTransaction));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(PeriodTransaction item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<PeriodTransaction>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(PeriodTransaction));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<PeriodTransaction> itemsList
      = new List<PeriodTransaction>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is PeriodTransaction)
        itemsList.Add(deserializedObject as PeriodTransaction);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected long m_sessionId;
        
          protected int m_businessTransactionId;
        
          protected int m_periodTransactionTypeId;
        
        #endregion
        
        #region Constructors
        public PeriodTransaction(
        long 
          sessionId,int 
          businessTransactionId
         )
        {
        
          m_sessionId = sessionId;
        
          m_businessTransactionId = businessTransactionId;
        
        }
        
        


        public PeriodTransaction(
        long 
          sessionId,int 
          businessTransactionId,int 
          periodTransactionTypeId
        )
        {
        
          m_sessionId = sessionId;
        
          m_businessTransactionId = businessTransactionId;
        
          m_periodTransactionTypeId = periodTransactionTypeId;
        
          }


        
      #endregion

      
        [XmlElement]
        public long SessionId
        {
          get { return m_sessionId;}
          set { m_sessionId = value; }
        }
      
        [XmlElement]
        public int BusinessTransactionId
        {
          get { return m_businessTransactionId;}
          set { m_businessTransactionId = value; }
        }
      
        [XmlElement]
        public int PeriodTransactionTypeId
        {
          get { return m_periodTransactionTypeId;}
          set { m_periodTransactionTypeId = value; }
        }
      
      }
      #endregion
      }

    