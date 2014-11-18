
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using SmartSchedule.Data;
    using SmartSchedule.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace SmartSchedule.Domain
      {

      [DataContract]
      public partial class TimeFrame : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TimeFrame ( " +
      
        " TimeStart, " +
      
        " TimeEnd " +
      
      ") Values (" +
      
        " ?TimeStart, " +
      
        " ?TimeEnd " +
      
      ")";

      public static void Insert(TimeFrame timeFrame, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TimeStart", timeFrame.TimeStart);
      
        Database.PutParameter(dbCommand,"?TimeEnd", timeFrame.TimeEnd);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        timeFrame.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(TimeFrame timeFrame)
      {
        Insert(timeFrame, null);
      }


      public static void Insert(List<TimeFrame>  timeFrameList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TimeFrame timeFrame in  timeFrameList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TimeStart", timeFrame.TimeStart);
      
        Database.PutParameter(dbCommand,"?TimeEnd", timeFrame.TimeEnd);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TimeStart",timeFrame.TimeStart);
      
        Database.UpdateParameter(dbCommand,"?TimeEnd",timeFrame.TimeEnd);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        timeFrame.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<TimeFrame>  timeFrameList)
      {
        Insert(timeFrameList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TimeFrame Set "
      
        + " TimeStart = ?TimeStart, "
      
        + " TimeEnd = ?TimeEnd "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(TimeFrame timeFrame, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", timeFrame.ID);
      
        Database.PutParameter(dbCommand,"?TimeStart", timeFrame.TimeStart);
      
        Database.PutParameter(dbCommand,"?TimeEnd", timeFrame.TimeEnd);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TimeFrame timeFrame)
      {
        Update(timeFrame, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " TimeStart, "
      
        + " TimeEnd "
      

      + " From TimeFrame "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static TimeFrame FindByPrimaryKey(
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
      throw new DataNotFoundException("TimeFrame not found, search by primary key");

      }

      public static TimeFrame FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TimeFrame timeFrame, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",timeFrame.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TimeFrame timeFrame)
      {
      return Exists(timeFrame, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TimeFrame limit 1";

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

      public static TimeFrame Load(IDataReader dataReader, int offset)
      {
      TimeFrame timeFrame = new TimeFrame();

      timeFrame.ID = dataReader.GetInt32(0 + offset);
          timeFrame.TimeStart = dataReader.GetDateTime(1 + offset);
          timeFrame.TimeEnd = dataReader.GetDateTime(2 + offset);
          

      return timeFrame;
      }

      public static TimeFrame Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TimeFrame "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(TimeFrame timeFrame, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", timeFrame.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TimeFrame timeFrame)
      {
        Delete(timeFrame, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TimeFrame ";

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
      
        + " TimeStart, "
      
        + " TimeEnd "
      

      + " From TimeFrame ";
      public static List<TimeFrame> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TimeFrame> rv = new List<TimeFrame>();

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

      public static List<TimeFrame> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TimeFrame> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TimeFrame> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TimeFrame));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TimeFrame item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TimeFrame>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TimeFrame));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TimeFrame> itemsList
      = new List<TimeFrame>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TimeFrame)
      itemsList.Add(deserializedObject as TimeFrame);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected DateTime m_timeStart;
      
        protected DateTime m_timeEnd;
      
      #endregion

      #region Constructors
      public TimeFrame(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public TimeFrame(
        int 
          iD,DateTime 
          timeStart,DateTime 
          timeEnd
        ) : this()
        {
        
          m_iD = iD;
        
          m_timeStart = timeStart;
        
          m_timeEnd = timeEnd;
        
        }


      
      #endregion

      
        [DataMember]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [DataMember]
        public DateTime TimeStart
        {
        get { return m_timeStart;}
        set { m_timeStart = value; }
        }
      
        [DataMember]
        public DateTime TimeEnd
        {
        get { return m_timeEnd;}
        set { m_timeEnd = value; }
        }
      

      public static int FieldsCount
      {
      get { return 3; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    