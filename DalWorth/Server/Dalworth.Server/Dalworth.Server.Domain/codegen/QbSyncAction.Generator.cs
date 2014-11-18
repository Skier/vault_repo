
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


      public partial class QbSyncAction : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbSyncAction ( " +
      
        " ID, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(QbSyncAction qbSyncAction, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", qbSyncAction.ID);
      
        Database.PutParameter(dbCommand,"?Description", qbSyncAction.Description);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbSyncAction qbSyncAction)
      {
        Insert(qbSyncAction, null);
      }


      public static void Insert(List<QbSyncAction>  qbSyncActionList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbSyncAction qbSyncAction in  qbSyncActionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", qbSyncAction.ID);
      
        Database.PutParameter(dbCommand,"?Description", qbSyncAction.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",qbSyncAction.ID);
      
        Database.UpdateParameter(dbCommand,"?Description",qbSyncAction.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbSyncAction>  qbSyncActionList)
      {
        Insert(qbSyncActionList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbSyncAction Set "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(QbSyncAction qbSyncAction, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", qbSyncAction.ID);
      
        Database.PutParameter(dbCommand,"?Description", qbSyncAction.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbSyncAction qbSyncAction)
      {
        Update(qbSyncAction, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Description "
      

      + " From QbSyncAction "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static QbSyncAction FindByPrimaryKey(
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
      throw new DataNotFoundException("QbSyncAction not found, search by primary key");

      }

      public static QbSyncAction FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbSyncAction qbSyncAction, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",qbSyncAction.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbSyncAction qbSyncAction)
      {
      return Exists(qbSyncAction, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbSyncAction limit 1";

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

      public static QbSyncAction Load(IDataReader dataReader, int offset)
      {
      QbSyncAction qbSyncAction = new QbSyncAction();

      qbSyncAction.ID = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            qbSyncAction.Description = dataReader.GetString(1 + offset);
          

      return qbSyncAction;
      }

      public static QbSyncAction Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbSyncAction "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(QbSyncAction qbSyncAction, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", qbSyncAction.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbSyncAction qbSyncAction)
      {
        Delete(qbSyncAction, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbSyncAction ";

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
      
        + " Description "
      

      + " From QbSyncAction ";
      public static List<QbSyncAction> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbSyncAction> rv = new List<QbSyncAction>();

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

      public static List<QbSyncAction> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbSyncAction> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbSyncAction obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Description == obj.Description;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbSyncAction> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbSyncAction));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbSyncAction item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbSyncAction>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbSyncAction));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbSyncAction> itemsList
      = new List<QbSyncAction>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbSyncAction)
      itemsList.Add(deserializedObject as QbSyncAction);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public QbSyncAction(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public QbSyncAction(
        int 
          iD,String 
          description
        ) : this()
        {
        
          m_iD = iD;
        
          m_description = description;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
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

    