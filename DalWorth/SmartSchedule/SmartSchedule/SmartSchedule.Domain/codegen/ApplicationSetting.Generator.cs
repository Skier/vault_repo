
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
      public partial class ApplicationSetting : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ApplicationSetting ( " +
      
        " ImportDate, " +
      
        " Note, " +
      
        " LastEmailReportDate " +
      
      ") Values (" +
      
        " ?ImportDate, " +
      
        " ?Note, " +
      
        " ?LastEmailReportDate " +
      
      ")";

      public static void Insert(ApplicationSetting applicationSetting, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ImportDate", applicationSetting.ImportDate);
      
        Database.PutParameter(dbCommand,"?Note", applicationSetting.Note);
      
        Database.PutParameter(dbCommand,"?LastEmailReportDate", applicationSetting.LastEmailReportDate);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ApplicationSetting applicationSetting)
      {
        Insert(applicationSetting, null);
      }


      public static void Insert(List<ApplicationSetting>  applicationSettingList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ApplicationSetting applicationSetting in  applicationSettingList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ImportDate", applicationSetting.ImportDate);
      
        Database.PutParameter(dbCommand,"?Note", applicationSetting.Note);
      
        Database.PutParameter(dbCommand,"?LastEmailReportDate", applicationSetting.LastEmailReportDate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ImportDate",applicationSetting.ImportDate);
      
        Database.UpdateParameter(dbCommand,"?Note",applicationSetting.Note);
      
        Database.UpdateParameter(dbCommand,"?LastEmailReportDate",applicationSetting.LastEmailReportDate);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ApplicationSetting>  applicationSettingList)
      {
        Insert(applicationSettingList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ApplicationSetting Set "
      
        + " Note = ?Note, "
      
        + " LastEmailReportDate = ?LastEmailReportDate "
      
        + " Where "
        
          + " ImportDate = ?ImportDate "
        
      ;

      public static void Update(ApplicationSetting applicationSetting, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ImportDate", applicationSetting.ImportDate);
      
        Database.PutParameter(dbCommand,"?Note", applicationSetting.Note);
      
        Database.PutParameter(dbCommand,"?LastEmailReportDate", applicationSetting.LastEmailReportDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ApplicationSetting applicationSetting)
      {
        Update(applicationSetting, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ImportDate, "
      
        + " Note, "
      
        + " LastEmailReportDate "
      

      + " From ApplicationSetting "

      
        + " Where "
        
          + " ImportDate = ?ImportDate "
        
      ;

      public static ApplicationSetting FindByPrimaryKey(
      DateTime importDate, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ImportDate", importDate);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ApplicationSetting not found, search by primary key");

      }

      public static ApplicationSetting FindByPrimaryKey(
      DateTime importDate
      )
      {
      return FindByPrimaryKey(
      importDate, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ApplicationSetting applicationSetting, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ImportDate",applicationSetting.ImportDate);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ApplicationSetting applicationSetting)
      {
      return Exists(applicationSetting, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ApplicationSetting limit 1";

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

      public static ApplicationSetting Load(IDataReader dataReader, int offset)
      {
      ApplicationSetting applicationSetting = new ApplicationSetting();

      applicationSetting.ImportDate = dataReader.GetDateTime(0 + offset);
          applicationSetting.Note = dataReader.GetString(1 + offset);
          applicationSetting.LastEmailReportDate = dataReader.GetDateTime(2 + offset);
          

      return applicationSetting;
      }

      public static ApplicationSetting Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ApplicationSetting "

      
        + " Where "
        
          + " ImportDate = ?ImportDate "
        
      ;
      public static void Delete(ApplicationSetting applicationSetting, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ImportDate", applicationSetting.ImportDate);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ApplicationSetting applicationSetting)
      {
        Delete(applicationSetting, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ApplicationSetting ";

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

      
        + " ImportDate, "
      
        + " Note, "
      
        + " LastEmailReportDate "
      

      + " From ApplicationSetting ";
      public static List<ApplicationSetting> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ApplicationSetting> rv = new List<ApplicationSetting>();

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

      public static List<ApplicationSetting> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ApplicationSetting> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<ApplicationSetting> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ApplicationSetting));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ApplicationSetting item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ApplicationSetting>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ApplicationSetting));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ApplicationSetting> itemsList
      = new List<ApplicationSetting>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ApplicationSetting)
      itemsList.Add(deserializedObject as ApplicationSetting);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected DateTime m_importDate;
      
        protected String m_note;
      
        protected DateTime m_lastEmailReportDate;
      
      #endregion

      #region Constructors
      public ApplicationSetting(
      DateTime 
          importDate
      ) : this()
      {
      
        m_importDate = importDate;
      
      }

      


        public ApplicationSetting(
        DateTime 
          importDate,String 
          note,DateTime 
          lastEmailReportDate
        ) : this()
        {
        
          m_importDate = importDate;
        
          m_note = note;
        
          m_lastEmailReportDate = lastEmailReportDate;
        
        }


      
      #endregion

      
        [DataMember]
        public DateTime ImportDate
        {
        get { return m_importDate;}
        set { m_importDate = value; }
        }
      
        [DataMember]
        public String Note
        {
        get { return m_note;}
        set { m_note = value; }
        }
      
        [DataMember]
        public DateTime LastEmailReportDate
        {
        get { return m_lastEmailReportDate;}
        set { m_lastEmailReportDate = value; }
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

    