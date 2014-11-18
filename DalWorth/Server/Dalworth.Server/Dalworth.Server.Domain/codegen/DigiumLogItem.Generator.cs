
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


      public partial class DigiumLogItem : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into DigiumLogItem ( " +
      
        " CallId, " +
        " IsIncoming, " +
      
        " TimeCreated, " +
        " TimeCreatedOriginal, " +
      
        " TimeTalkStarted, " +
        " TimeTalkStartedOriginal, " +
      
        " DurationSec, " +
      
        " CallerIdNumber, " +
      
        " CallerName, " +
      
        " Extension, " +
      
        " ExtensionType, " +
      
        " IncomingDid, " +
      
        " IsIntermediateCall, " +
      
        " CallSourceId, " +
      
        " VoiceFileName " +
      
      ") Values (" +
      
        " ?CallId, " +
        " ?IsIncoming, " +
      
        " ?TimeCreated, " +

        " ?TimeCreatedOriginal, " +
      
        " ?TimeTalkStarted, " +

        " ?TimeTalkStartedOriginal, " +
      
        " ?DurationSec, " +
      
        " ?CallerIdNumber, " +
      
        " ?CallerName, " +
      
        " ?Extension, " +
      
        " ?ExtensionType, " +
      
        " ?IncomingDid, " +
      
        " ?IsIntermediateCall, " +
      
        " ?CallSourceId, " +
      
        " ?VoiceFileName " +
      
      ")";

      public static void Insert(DigiumLogItem digiumLogItem, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?CallId", digiumLogItem.CallId);
        Database.PutParameter(dbCommand, "?IsIncoming", digiumLogItem.IsIncoming);
      
        Database.PutParameter(dbCommand,"?TimeCreated", digiumLogItem.TimeCreated);
        Database.PutParameter(dbCommand, "?TimeCreatedOriginal", digiumLogItem.TimeCreatedOriginal);
      
        Database.PutParameter(dbCommand,"?TimeTalkStarted", digiumLogItem.TimeTalkStarted);
        Database.PutParameter(dbCommand, "?TimeTalkStartedOriginal", digiumLogItem.TimeTalkStartedOriginal);
      
        Database.PutParameter(dbCommand,"?DurationSec", digiumLogItem.DurationSec);
      
        Database.PutParameter(dbCommand,"?CallerIdNumber", digiumLogItem.CallerIdNumber);
      
        Database.PutParameter(dbCommand,"?CallerName", digiumLogItem.CallerName);
      
        Database.PutParameter(dbCommand,"?Extension", digiumLogItem.Extension);
      
        Database.PutParameter(dbCommand,"?ExtensionType", digiumLogItem.ExtensionType);
      
        Database.PutParameter(dbCommand,"?IncomingDid", digiumLogItem.IncomingDid);
      
        Database.PutParameter(dbCommand,"?IsIntermediateCall", digiumLogItem.IsIntermediateCall);
      
        Database.PutParameter(dbCommand,"?CallSourceId", digiumLogItem.CallSourceId);
      
        Database.PutParameter(dbCommand,"?VoiceFileName", digiumLogItem.VoiceFileName);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        digiumLogItem.ID = Convert.ToInt64(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(DigiumLogItem digiumLogItem)
      {
        Insert(digiumLogItem, null);
      }


      public static void Insert(List<DigiumLogItem>  digiumLogItemList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(DigiumLogItem digiumLogItem in  digiumLogItemList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?CallId", digiumLogItem.CallId);
        Database.PutParameter(dbCommand, "?IsIncoming", digiumLogItem.IsIncoming);
      
        Database.PutParameter(dbCommand,"?TimeCreated", digiumLogItem.TimeCreated);
        Database.PutParameter(dbCommand, "?TimeCreatedOriginal", digiumLogItem.TimeCreatedOriginal);
      
        Database.PutParameter(dbCommand,"?TimeTalkStarted", digiumLogItem.TimeTalkStarted);
        Database.PutParameter(dbCommand, "?TimeTalkStartedOriginal", digiumLogItem.TimeTalkStartedOriginal);
      
        Database.PutParameter(dbCommand,"?DurationSec", digiumLogItem.DurationSec);
      
        Database.PutParameter(dbCommand,"?CallerIdNumber", digiumLogItem.CallerIdNumber);
      
        Database.PutParameter(dbCommand,"?CallerName", digiumLogItem.CallerName);
      
        Database.PutParameter(dbCommand,"?Extension", digiumLogItem.Extension);
      
        Database.PutParameter(dbCommand,"?ExtensionType", digiumLogItem.ExtensionType);
      
        Database.PutParameter(dbCommand,"?IncomingDid", digiumLogItem.IncomingDid);
      
        Database.PutParameter(dbCommand,"?IsIntermediateCall", digiumLogItem.IsIntermediateCall);
      
        Database.PutParameter(dbCommand,"?CallSourceId", digiumLogItem.CallSourceId);
      
        Database.PutParameter(dbCommand,"?VoiceFileName", digiumLogItem.VoiceFileName);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?CallId",digiumLogItem.CallId);
        Database.UpdateParameter(dbCommand, "?IsIncoming", digiumLogItem.IsIncoming);
      
        Database.UpdateParameter(dbCommand,"?TimeCreated",digiumLogItem.TimeCreated);
        Database.UpdateParameter(dbCommand, "?TimeCreatedOriginal", digiumLogItem.TimeCreatedOriginal);
      
        Database.UpdateParameter(dbCommand,"?TimeTalkStarted",digiumLogItem.TimeTalkStarted);
        Database.UpdateParameter(dbCommand, "?TimeTalkStartedOriginal", digiumLogItem.TimeTalkStartedOriginal);
      
        Database.UpdateParameter(dbCommand,"?DurationSec",digiumLogItem.DurationSec);
      
        Database.UpdateParameter(dbCommand,"?CallerIdNumber",digiumLogItem.CallerIdNumber);
      
        Database.UpdateParameter(dbCommand,"?CallerName",digiumLogItem.CallerName);
      
        Database.UpdateParameter(dbCommand,"?Extension",digiumLogItem.Extension);
      
        Database.UpdateParameter(dbCommand,"?ExtensionType",digiumLogItem.ExtensionType);
      
        Database.UpdateParameter(dbCommand,"?IncomingDid",digiumLogItem.IncomingDid);
      
        Database.UpdateParameter(dbCommand,"?IsIntermediateCall",digiumLogItem.IsIntermediateCall);
      
        Database.UpdateParameter(dbCommand,"?CallSourceId",digiumLogItem.CallSourceId);
      
        Database.UpdateParameter(dbCommand,"?VoiceFileName",digiumLogItem.VoiceFileName);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        digiumLogItem.ID = Convert.ToInt64(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<DigiumLogItem>  digiumLogItemList)
      {
        Insert(digiumLogItemList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update DigiumLogItem Set "
      
        + " CallId = ?CallId, "
        + " IsIncoming = ?IsIncoming, "
      
        + " TimeCreated = ?TimeCreated, "
        + " TimeCreatedOriginal = ?TimeCreatedOriginal, "
      
        + " TimeTalkStarted = ?TimeTalkStarted, "
        + " TimeTalkStartedOriginal = ?TimeTalkStartedOriginal, "
      
        + " DurationSec = ?DurationSec, "
      
        + " CallerIdNumber = ?CallerIdNumber, "
      
        + " CallerName = ?CallerName, "
      
        + " Extension = ?Extension, "
      
        + " ExtensionType = ?ExtensionType, "
      
        + " IncomingDid = ?IncomingDid, "
      
        + " IsIntermediateCall = ?IsIntermediateCall, "
      
        + " CallSourceId = ?CallSourceId, "
      
        + " VoiceFileName = ?VoiceFileName "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(DigiumLogItem digiumLogItem, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", digiumLogItem.ID);
      
        Database.PutParameter(dbCommand,"?CallId", digiumLogItem.CallId);
        Database.PutParameter(dbCommand, "?IsIncoming", digiumLogItem.IsIncoming);
      
        Database.PutParameter(dbCommand,"?TimeCreated", digiumLogItem.TimeCreated);
        Database.PutParameter(dbCommand, "?TimeCreatedOriginal", digiumLogItem.TimeCreatedOriginal);
      
        Database.PutParameter(dbCommand,"?TimeTalkStarted", digiumLogItem.TimeTalkStarted);
        Database.PutParameter(dbCommand, "?TimeTalkStartedOriginal", digiumLogItem.TimeTalkStartedOriginal);
      
        Database.PutParameter(dbCommand,"?DurationSec", digiumLogItem.DurationSec);
      
        Database.PutParameter(dbCommand,"?CallerIdNumber", digiumLogItem.CallerIdNumber);
      
        Database.PutParameter(dbCommand,"?CallerName", digiumLogItem.CallerName);
      
        Database.PutParameter(dbCommand,"?Extension", digiumLogItem.Extension);
      
        Database.PutParameter(dbCommand,"?ExtensionType", digiumLogItem.ExtensionType);
      
        Database.PutParameter(dbCommand,"?IncomingDid", digiumLogItem.IncomingDid);
      
        Database.PutParameter(dbCommand,"?IsIntermediateCall", digiumLogItem.IsIntermediateCall);
      
        Database.PutParameter(dbCommand,"?CallSourceId", digiumLogItem.CallSourceId);
      
        Database.PutParameter(dbCommand,"?VoiceFileName", digiumLogItem.VoiceFileName);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(DigiumLogItem digiumLogItem)
      {
        Update(digiumLogItem, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " CallId, "
        + " IsIncoming, "
      
        + " TimeCreated, "
        + " TimeCreatedOriginal, "
      
        + " TimeTalkStarted, "
        + " TimeTalkStartedOriginal, "
      
        + " DurationSec, "
      
        + " CallerIdNumber, "
      
        + " CallerName, "
      
        + " Extension, "
      
        + " ExtensionType, "
      
        + " IncomingDid, "
      
        + " IsIntermediateCall, "
      
        + " CallSourceId, "
      
        + " VoiceFileName "
      

      + " From DigiumLogItem "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static DigiumLogItem FindByPrimaryKey(
      long iD, IDbConnection connection
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
      throw new DataNotFoundException("DigiumLogItem not found, search by primary key");

      }

      public static DigiumLogItem FindByPrimaryKey(
      long iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(DigiumLogItem digiumLogItem, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",digiumLogItem.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(DigiumLogItem digiumLogItem)
      {
      return Exists(digiumLogItem, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from DigiumLogItem limit 1";

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

      public static DigiumLogItem Load(IDataReader dataReader, int offset)
      {
      DigiumLogItem digiumLogItem = new DigiumLogItem();

      digiumLogItem.ID = dataReader.GetInt64(0 + offset);
          digiumLogItem.CallId = dataReader.GetString(1 + offset);
          digiumLogItem.IsIncoming = dataReader.GetBoolean(2 + offset);
          digiumLogItem.TimeCreated = dataReader.GetDateTime(3 + offset);
          digiumLogItem.TimeCreatedOriginal = dataReader.GetDateTime(4 + offset);
          digiumLogItem.TimeTalkStarted = dataReader.GetDateTime(5 + offset);
          digiumLogItem.TimeTalkStartedOriginal = dataReader.GetDateTime(6 + offset);
          digiumLogItem.DurationSec = dataReader.GetInt32(7 + offset);
          digiumLogItem.CallerIdNumber = dataReader.GetString(8 + offset);
          digiumLogItem.CallerName = dataReader.GetString(9 + offset);
          digiumLogItem.Extension = dataReader.GetString(10 + offset);
          digiumLogItem.ExtensionType = dataReader.GetString(11 + offset);
          digiumLogItem.IncomingDid = dataReader.GetString(12 + offset);
          digiumLogItem.IsIntermediateCall = dataReader.GetBoolean(13 + offset);
          
            if(!dataReader.IsDBNull(14 + offset))
            digiumLogItem.CallSourceId = dataReader.GetInt32(14 + offset);
          digiumLogItem.VoiceFileName = dataReader.GetString(15 + offset);
          

      return digiumLogItem;
      }

      public static DigiumLogItem Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From DigiumLogItem "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(DigiumLogItem digiumLogItem, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", digiumLogItem.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(DigiumLogItem digiumLogItem)
      {
        Delete(digiumLogItem, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From DigiumLogItem ";

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
      
        + " CallId, "
        + " IsIncoming, "
      
        + " TimeCreated, "
        + " TimeCreatedOriginal, "
      
        + " TimeTalkStarted, "
        + " TimeTalkStartedOriginal, "
      
        + " DurationSec, "
      
        + " CallerIdNumber, "
      
        + " CallerName, "
      
        + " Extension, "
      
        + " ExtensionType, "
      
        + " IncomingDid, "
      
        + " IsIntermediateCall, "
      
        + " CallSourceId, "
      
        + " VoiceFileName "
      

      + " From DigiumLogItem ";
      public static List<DigiumLogItem> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<DigiumLogItem> rv = new List<DigiumLogItem>();

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

      public static List<DigiumLogItem> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<DigiumLogItem> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (DigiumLogItem obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && CallId == obj.CallId && TimeCreated == obj.TimeCreated && TimeTalkStarted == obj.TimeTalkStarted && DurationSec == obj.DurationSec && CallerIdNumber == obj.CallerIdNumber && CallerName == obj.CallerName && Extension == obj.Extension && ExtensionType == obj.ExtensionType && IncomingDid == obj.IncomingDid && IsIntermediateCall == obj.IsIntermediateCall && CallSourceId == obj.CallSourceId && VoiceFileName == obj.VoiceFileName;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<DigiumLogItem> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(DigiumLogItem));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(DigiumLogItem item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<DigiumLogItem>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(DigiumLogItem));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<DigiumLogItem> itemsList
      = new List<DigiumLogItem>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is DigiumLogItem)
      itemsList.Add(deserializedObject as DigiumLogItem);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected long m_iD;
      
        protected String m_callId;
        protected bool m_isIncoming;
      
        protected DateTime m_timeCreated;
        protected DateTime m_timeCreatedOriginal;
      
        protected DateTime m_timeTalkStarted;
        protected DateTime m_timeTalkStartedOriginal;
      
        protected int m_durationSec;
      
        protected String m_callerIdNumber;
      
        protected String m_callerName;
      
        protected String m_extension;
      
        protected String m_extensionType;
      
        protected String m_incomingDid;
      
        protected bool m_isIntermediateCall;
      
        protected int? m_callSourceId;
      
        protected String m_voiceFileName;
      
      #endregion

      #region Constructors
      public DigiumLogItem(
      long 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public DigiumLogItem(
        long 
          iD,String 
          callId,
            bool isIncoming,
            DateTime timeCreated,
            DateTime timeCreatedOriginal,
            DateTime timeTalkStarted,
            DateTime timeTalkStartedOriginal,
            int 
          durationSec,String 
          callerIdNumber,String 
          callerName,String 
          extension,String 
          extensionType,String 
          incomingDid,bool 
          isIntermediateCall,int? 
          callSourceId,String 
          voiceFileName
        ) : this()
        {
        
          m_iD = iD;
        
          m_callId = callId;
          m_isIncoming = isIncoming;
        
          m_timeCreated = timeCreated;
          m_timeCreatedOriginal = timeCreatedOriginal;
        
          m_timeTalkStarted = timeTalkStarted;
          m_timeTalkStartedOriginal = timeTalkStartedOriginal;
        
          m_durationSec = durationSec;
        
          m_callerIdNumber = callerIdNumber;
        
          m_callerName = callerName;
        
          m_extension = extension;
        
          m_extensionType = extensionType;
        
          m_incomingDid = incomingDid;
        
          m_isIntermediateCall = isIntermediateCall;
        
          m_callSourceId = callSourceId;
        
          m_voiceFileName = voiceFileName;
        
        }


      
      #endregion

      
        [XmlElement]
        public long ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String CallId
        {
        get { return m_callId;}
        set { m_callId = value; }
        }

        [XmlElement]
        public bool IsIncoming
        {
        get { return m_isIncoming;}
        set { m_isIncoming = value; }
        }
      
        [XmlElement]
        public DateTime TimeCreated
        {
        get { return m_timeCreated;}
        set { m_timeCreated = value; }
        }

        [XmlElement]
        public DateTime TimeCreatedOriginal
        {
        get { return m_timeCreatedOriginal;}
        set { m_timeCreatedOriginal = value; }
        }
      
        [XmlElement]
        public DateTime TimeTalkStarted
        {
        get { return m_timeTalkStarted;}
        set { m_timeTalkStarted = value; }
        }

        [XmlElement]
        public DateTime TimeTalkStartedOriginal
        {
        get { return m_timeTalkStartedOriginal;}
        set { m_timeTalkStartedOriginal = value; }
        }
      
        [XmlElement]
        public int DurationSec
        {
        get { return m_durationSec;}
        set { m_durationSec = value; }
        }
      
        [XmlElement]
        public String CallerIdNumber
        {
        get { return m_callerIdNumber;}
        set { m_callerIdNumber = value; }
        }
      
        [XmlElement]
        public String CallerName
        {
        get { return m_callerName;}
        set { m_callerName = value; }
        }
      
        [XmlElement]
        public String Extension
        {
        get { return m_extension;}
        set { m_extension = value; }
        }
      
        [XmlElement]
        public String ExtensionType
        {
        get { return m_extensionType;}
        set { m_extensionType = value; }
        }
      
        [XmlElement]
        public String IncomingDid
        {
        get { return m_incomingDid;}
        set { m_incomingDid = value; }
        }
      
        [XmlElement]
        public bool IsIntermediateCall
        {
        get { return m_isIntermediateCall;}
        set { m_isIntermediateCall = value; }
        }
      
        [XmlElement]
        public int? CallSourceId
        {
        get { return m_callSourceId;}
        set { m_callSourceId = value; }
        }
      
        [XmlElement]
        public String VoiceFileName
        {
        get { return m_voiceFileName;}
        set { m_voiceFileName = value; }
        }
      

      public static int FieldsCount
      {
      get { return 16; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    