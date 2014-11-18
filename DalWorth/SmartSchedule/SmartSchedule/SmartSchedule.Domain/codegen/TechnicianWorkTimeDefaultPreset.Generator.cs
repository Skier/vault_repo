
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
      public partial class TechnicianWorkTimeDefaultPreset : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TechnicianWorkTimeDefaultPreset ( " +
      
        " TechnicianId, " +
      
        " PresetNumber, " +
      
        " TimeStart, " +
      
        " TimeEnd " +
      
      ") Values (" +
      
        " ?TechnicianId, " +
      
        " ?PresetNumber, " +
      
        " ?TimeStart, " +
      
        " ?TimeEnd " +
      
      ")";

      public static void Insert(TechnicianWorkTimeDefaultPreset technicianWorkTimeDefaultPreset, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianWorkTimeDefaultPreset.TechnicianId);
      
        Database.PutParameter(dbCommand,"?PresetNumber", technicianWorkTimeDefaultPreset.PresetNumber);
      
        Database.PutParameter(dbCommand,"?TimeStart", technicianWorkTimeDefaultPreset.TimeStart);
      
        Database.PutParameter(dbCommand,"?TimeEnd", technicianWorkTimeDefaultPreset.TimeEnd);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(TechnicianWorkTimeDefaultPreset technicianWorkTimeDefaultPreset)
      {
        Insert(technicianWorkTimeDefaultPreset, null);
      }


      public static void Insert(List<TechnicianWorkTimeDefaultPreset>  technicianWorkTimeDefaultPresetList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TechnicianWorkTimeDefaultPreset technicianWorkTimeDefaultPreset in  technicianWorkTimeDefaultPresetList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianWorkTimeDefaultPreset.TechnicianId);
      
        Database.PutParameter(dbCommand,"?PresetNumber", technicianWorkTimeDefaultPreset.PresetNumber);
      
        Database.PutParameter(dbCommand,"?TimeStart", technicianWorkTimeDefaultPreset.TimeStart);
      
        Database.PutParameter(dbCommand,"?TimeEnd", technicianWorkTimeDefaultPreset.TimeEnd);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TechnicianId",technicianWorkTimeDefaultPreset.TechnicianId);
      
        Database.UpdateParameter(dbCommand,"?PresetNumber",technicianWorkTimeDefaultPreset.PresetNumber);
      
        Database.UpdateParameter(dbCommand,"?TimeStart",technicianWorkTimeDefaultPreset.TimeStart);
      
        Database.UpdateParameter(dbCommand,"?TimeEnd",technicianWorkTimeDefaultPreset.TimeEnd);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<TechnicianWorkTimeDefaultPreset>  technicianWorkTimeDefaultPresetList)
      {
        Insert(technicianWorkTimeDefaultPresetList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TechnicianWorkTimeDefaultPreset Set "
      
        + " TimeStart = ?TimeStart, "
      
        + " TimeEnd = ?TimeEnd "
      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " PresetNumber = ?PresetNumber "
        
      ;

      public static void Update(TechnicianWorkTimeDefaultPreset technicianWorkTimeDefaultPreset, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianWorkTimeDefaultPreset.TechnicianId);
      
        Database.PutParameter(dbCommand,"?PresetNumber", technicianWorkTimeDefaultPreset.PresetNumber);
      
        Database.PutParameter(dbCommand,"?TimeStart", technicianWorkTimeDefaultPreset.TimeStart);
      
        Database.PutParameter(dbCommand,"?TimeEnd", technicianWorkTimeDefaultPreset.TimeEnd);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TechnicianWorkTimeDefaultPreset technicianWorkTimeDefaultPreset)
      {
        Update(technicianWorkTimeDefaultPreset, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TechnicianId, "
      
        + " PresetNumber, "
      
        + " TimeStart, "
      
        + " TimeEnd "
      

      + " From TechnicianWorkTimeDefaultPreset "

      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " PresetNumber = ?PresetNumber "
        
      ;

      public static TechnicianWorkTimeDefaultPreset FindByPrimaryKey(
      int technicianId,int presetNumber, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianId);
      
        Database.PutParameter(dbCommand,"?PresetNumber", presetNumber);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("TechnicianWorkTimeDefaultPreset not found, search by primary key");

      }

      public static TechnicianWorkTimeDefaultPreset FindByPrimaryKey(
      int technicianId,int presetNumber
      )
      {
      return FindByPrimaryKey(
      technicianId,presetNumber, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TechnicianWorkTimeDefaultPreset technicianWorkTimeDefaultPreset, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId",technicianWorkTimeDefaultPreset.TechnicianId);
      
        Database.PutParameter(dbCommand,"?PresetNumber",technicianWorkTimeDefaultPreset.PresetNumber);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TechnicianWorkTimeDefaultPreset technicianWorkTimeDefaultPreset)
      {
      return Exists(technicianWorkTimeDefaultPreset, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TechnicianWorkTimeDefaultPreset limit 1";

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

      public static TechnicianWorkTimeDefaultPreset Load(IDataReader dataReader, int offset)
      {
      TechnicianWorkTimeDefaultPreset technicianWorkTimeDefaultPreset = new TechnicianWorkTimeDefaultPreset();

      technicianWorkTimeDefaultPreset.TechnicianId = dataReader.GetInt32(0 + offset);
          technicianWorkTimeDefaultPreset.PresetNumber = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            technicianWorkTimeDefaultPreset.TimeStart = dataReader.GetDateTime(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            technicianWorkTimeDefaultPreset.TimeEnd = dataReader.GetDateTime(3 + offset);
          

      return technicianWorkTimeDefaultPreset;
      }

      public static TechnicianWorkTimeDefaultPreset Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TechnicianWorkTimeDefaultPreset "

      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " PresetNumber = ?PresetNumber "
        
      ;
      public static void Delete(TechnicianWorkTimeDefaultPreset technicianWorkTimeDefaultPreset, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianWorkTimeDefaultPreset.TechnicianId);
      
        Database.PutParameter(dbCommand,"?PresetNumber", technicianWorkTimeDefaultPreset.PresetNumber);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TechnicianWorkTimeDefaultPreset technicianWorkTimeDefaultPreset)
      {
        Delete(technicianWorkTimeDefaultPreset, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TechnicianWorkTimeDefaultPreset ";

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

      
        + " TechnicianId, "
      
        + " PresetNumber, "
      
        + " TimeStart, "
      
        + " TimeEnd "
      

      + " From TechnicianWorkTimeDefaultPreset ";
      public static List<TechnicianWorkTimeDefaultPreset> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TechnicianWorkTimeDefaultPreset> rv = new List<TechnicianWorkTimeDefaultPreset>();

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

      public static List<TechnicianWorkTimeDefaultPreset> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TechnicianWorkTimeDefaultPreset> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TechnicianWorkTimeDefaultPreset> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianWorkTimeDefaultPreset));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TechnicianWorkTimeDefaultPreset item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TechnicianWorkTimeDefaultPreset>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianWorkTimeDefaultPreset));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TechnicianWorkTimeDefaultPreset> itemsList
      = new List<TechnicianWorkTimeDefaultPreset>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TechnicianWorkTimeDefaultPreset)
      itemsList.Add(deserializedObject as TechnicianWorkTimeDefaultPreset);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_technicianId;
      
        protected int m_presetNumber;
      
        protected DateTime? m_timeStart;
      
        protected DateTime? m_timeEnd;
      
      #endregion

      #region Constructors
      public TechnicianWorkTimeDefaultPreset(
      int 
          technicianId,int 
          presetNumber
      ) : this()
      {
      
        m_technicianId = technicianId;
      
        m_presetNumber = presetNumber;
      
      }

      


        public TechnicianWorkTimeDefaultPreset(
        int 
          technicianId,int 
          presetNumber,DateTime? 
          timeStart,DateTime? 
          timeEnd
        ) : this()
        {
        
          m_technicianId = technicianId;
        
          m_presetNumber = presetNumber;
        
          m_timeStart = timeStart;
        
          m_timeEnd = timeEnd;
        
        }


      
      #endregion

      
        [DataMember]
        public int TechnicianId
        {
        get { return m_technicianId;}
        set { m_technicianId = value; }
        }
      
        [DataMember]
        public int PresetNumber
        {
        get { return m_presetNumber;}
        set { m_presetNumber = value; }
        }
      
        [DataMember]
        public DateTime? TimeStart
        {
        get { return m_timeStart;}
        set { m_timeStart = value; }
        }
      
        [DataMember]
        public DateTime? TimeEnd
        {
        get { return m_timeEnd;}
        set { m_timeEnd = value; }
        }
      

      public static int FieldsCount
      {
      get { return 4; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    