
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

      public partial class QbCustomer : ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbCustomer ( " +
      
        " RecordId, " +
      
        " ShowAs " +
      
      ") Values (" +
      
        " ?RecordId, " +
      
        " ?ShowAs " +
      
      ")";

      public static void Insert(QbCustomer qbCustomer, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?RecordId", qbCustomer.RecordId);
      
        Database.PutParameter(dbCommand,"?ShowAs", qbCustomer.ShowAs);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbCustomer qbCustomer)
      {
        Insert(qbCustomer, null);
      }


      public static void Insert(List<QbCustomer>  qbCustomerList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbCustomer qbCustomer in  qbCustomerList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?RecordId", qbCustomer.RecordId);
      
        Database.PutParameter(dbCommand,"?ShowAs", qbCustomer.ShowAs);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?RecordId",qbCustomer.RecordId);
      
        Database.UpdateParameter(dbCommand,"?ShowAs",qbCustomer.ShowAs);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbCustomer>  qbCustomerList)
      {
        Insert(qbCustomerList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbCustomer Set "
      
        + " ShowAs = ?ShowAs "
      
        + " Where "
        
          + " RecordId = ?RecordId "
        
      ;

      public static void Update(QbCustomer qbCustomer, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?RecordId", qbCustomer.RecordId);
      
        Database.PutParameter(dbCommand,"?ShowAs", qbCustomer.ShowAs);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbCustomer qbCustomer)
      {
        Update(qbCustomer, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " RecordId, "
      
        + " ShowAs "
      

      + " From QbCustomer "

      
        + " Where "
        
          + " RecordId = ?RecordId "
        
      ;

      public static QbCustomer FindByPrimaryKey(
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
      throw new DataNotFoundException("QbCustomer not found, search by primary key");

      }

      public static QbCustomer FindByPrimaryKey(
      String recordId
      )
      {
      return FindByPrimaryKey(
      recordId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbCustomer qbCustomer, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?RecordId",qbCustomer.RecordId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbCustomer qbCustomer)
      {
      return Exists(qbCustomer, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbCustomer limit 1";

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

      public static QbCustomer Load(IDataReader dataReader, int offset)
      {
      QbCustomer qbCustomer = new QbCustomer();

      qbCustomer.RecordId = dataReader.GetString(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            qbCustomer.ShowAs = dataReader.GetString(1 + offset);
          

      return qbCustomer;
      }

      public static QbCustomer Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbCustomer "

      
        + " Where "
        
          + " RecordId = ?RecordId "
        
      ;
      public static void Delete(QbCustomer qbCustomer, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?RecordId", qbCustomer.RecordId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbCustomer qbCustomer)
      {
        Delete(qbCustomer, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbCustomer ";

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

      
        + " RecordId, "
      
        + " ShowAs "
      

      + " From QbCustomer ";
      public static List<QbCustomer> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbCustomer> rv = new List<QbCustomer>();

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

      public static List<QbCustomer> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbCustomer> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<QbCustomer> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbCustomer));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbCustomer item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbCustomer>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbCustomer));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbCustomer> itemsList
      = new List<QbCustomer>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbCustomer)
      itemsList.Add(deserializedObject as QbCustomer);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_recordId;
      
        protected String m_showAs;
      
      #endregion

      #region Constructors
      public QbCustomer(
      String 
          recordId
      ) : this()
      {
      
        m_recordId = recordId;
      
      }

      


        public QbCustomer(
        String 
          recordId,String 
          showAs
        ) : this()
        {
        
          m_recordId = recordId;
        
          m_showAs = showAs;
        
        }


      
      #endregion

      
        public String RecordId
        {
        get { return m_recordId;}
        set { m_recordId = value; }
        }
      
        public String ShowAs
        {
        get { return m_showAs;}
        set { m_showAs = value; }
        }
      

      public static int FieldsCount
      {
      get { return 2; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    