
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


      public partial class EquipmentStatus : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into EquipmentStatus ( " +
      
        " ID, " +
      
        " Status, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Status, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(EquipmentStatus equipmentStatus, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", equipmentStatus.ID);
      
        Database.PutParameter(dbCommand,"?Status", equipmentStatus.Status);
      
        Database.PutParameter(dbCommand,"?Description", equipmentStatus.Description);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(EquipmentStatus equipmentStatus)
      {
        Insert(equipmentStatus, null);
      }


      public static void Insert(List<EquipmentStatus>  equipmentStatusList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(EquipmentStatus equipmentStatus in  equipmentStatusList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", equipmentStatus.ID);
      
        Database.PutParameter(dbCommand,"?Status", equipmentStatus.Status);
      
        Database.PutParameter(dbCommand,"?Description", equipmentStatus.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",equipmentStatus.ID);
      
        Database.UpdateParameter(dbCommand,"?Status",equipmentStatus.Status);
      
        Database.UpdateParameter(dbCommand,"?Description",equipmentStatus.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<EquipmentStatus>  equipmentStatusList)
      {
        Insert(equipmentStatusList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update EquipmentStatus Set "
      
        + " Status = ?Status, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(EquipmentStatus equipmentStatus, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", equipmentStatus.ID);
      
        Database.PutParameter(dbCommand,"?Status", equipmentStatus.Status);
      
        Database.PutParameter(dbCommand,"?Description", equipmentStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(EquipmentStatus equipmentStatus)
      {
        Update(equipmentStatus, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Status, "
      
        + " Description "
      

      + " From EquipmentStatus "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static EquipmentStatus FindByPrimaryKey(
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
      throw new DataNotFoundException("EquipmentStatus not found, search by primary key");

      }

      public static EquipmentStatus FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(EquipmentStatus equipmentStatus, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",equipmentStatus.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(EquipmentStatus equipmentStatus)
      {
      return Exists(equipmentStatus, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from EquipmentStatus limit 1";

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

      public static EquipmentStatus Load(IDataReader dataReader, int offset)
      {
      EquipmentStatus equipmentStatus = new EquipmentStatus();

      equipmentStatus.ID = dataReader.GetInt32(0 + offset);
          equipmentStatus.Status = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            equipmentStatus.Description = dataReader.GetString(2 + offset);
          

      return equipmentStatus;
      }

      public static EquipmentStatus Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From EquipmentStatus "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(EquipmentStatus equipmentStatus, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", equipmentStatus.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(EquipmentStatus equipmentStatus)
      {
        Delete(equipmentStatus, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From EquipmentStatus ";

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
      
        + " Status, "
      
        + " Description "
      

      + " From EquipmentStatus ";
      public static List<EquipmentStatus> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<EquipmentStatus> rv = new List<EquipmentStatus>();

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

      public static List<EquipmentStatus> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<EquipmentStatus> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (EquipmentStatus obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Status == obj.Status && Description == obj.Description;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<EquipmentStatus> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(EquipmentStatus));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(EquipmentStatus item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<EquipmentStatus>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(EquipmentStatus));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<EquipmentStatus> itemsList
      = new List<EquipmentStatus>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is EquipmentStatus)
      itemsList.Add(deserializedObject as EquipmentStatus);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_status;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public EquipmentStatus(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public EquipmentStatus(
        int 
          iD,String 
          status,String 
          description
        ) : this()
        {
        
          m_iD = iD;
        
          m_status = status;
        
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
        public String Status
        {
        get { return m_status;}
        set { m_status = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
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

    