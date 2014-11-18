
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Servman.Data;
    using Servman.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Servman.Domain
      {

      public partial class QbJob : ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbJob ( " +
      
        " RecordId " +
      
      ") Values (" +
      
        " ?RecordId " +
      
      ")";

      public static void Insert(QbJob qbJob, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?RecordId", qbJob.RecordId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbJob qbJob)
      {
        Insert(qbJob, null);
      }


      public static void Insert(List<QbJob>  qbJobList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbJob qbJob in  qbJobList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?RecordId", qbJob.RecordId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?RecordId",qbJob.RecordId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbJob>  qbJobList)
      {
        Insert(qbJobList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbJob Set "
      
        + " Where "
        
          + " RecordId = ?RecordId "
        
      ;

      public static void Update(QbJob qbJob, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?RecordId", qbJob.RecordId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbJob qbJob)
      {
        Update(qbJob, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " RecordId "
      

      + " From QbJob "

      
        + " Where "
        
          + " RecordId = ?RecordId "
        
      ;

      public static QbJob FindByPrimaryKey(
      String recordId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?RecordId", recordId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("QbJob not found, search by primary key");

      }

      public static QbJob FindByPrimaryKey(
      String recordId
      )
      {
      return FindByPrimaryKey(
      recordId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbJob qbJob, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?RecordId",qbJob.RecordId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbJob qbJob)
      {
      return Exists(qbJob, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbJob limit 1";

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

      public static QbJob Load(IDataReader dataReader, int offset)
      {
      QbJob qbJob = new QbJob();

      qbJob.RecordId = dataReader.GetString(0 + offset);
          

      return qbJob;
      }

      public static QbJob Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbJob "

      
        + " Where "
        
          + " RecordId = ?RecordId "
        
      ;
      public static void Delete(QbJob qbJob, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?RecordId", qbJob.RecordId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbJob qbJob)
      {
        Delete(qbJob, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbJob ";

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

      
        + " RecordId "
      

      + " From QbJob ";
      public static List<QbJob> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbJob> rv = new List<QbJob>();

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

      public static List<QbJob> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbJob> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<QbJob> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbJob));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbJob item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbJob>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbJob));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbJob> itemsList
      = new List<QbJob>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbJob)
      itemsList.Add(deserializedObject as QbJob);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_recordId;
      
      #endregion

      #region Constructors
      public QbJob(
      String 
          recordId
      ) : this()
      {
      
        m_recordId = recordId;
      
      }

      
      #endregion

      
        public String RecordId
        {
        get { return m_recordId;}
        set { m_recordId = value; }
        }
      

      public static int FieldsCount
      {
      get { return 1; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    