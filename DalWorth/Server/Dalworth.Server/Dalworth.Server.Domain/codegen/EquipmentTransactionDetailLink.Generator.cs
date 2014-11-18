
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


      public partial class EquipmentTransactionDetailLink : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into EquipmentTransactionDetailLink ( " +
      
        " EquipmentTransactionDetailId, " +
      
        " PrevEquipmentTransactionDetailId " +
      
      ") Values (" +
      
        " ?EquipmentTransactionDetailId, " +
      
        " ?PrevEquipmentTransactionDetailId " +
      
      ")";

      public static void Insert(EquipmentTransactionDetailLink equipmentTransactionDetailLink, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?EquipmentTransactionDetailId", equipmentTransactionDetailLink.EquipmentTransactionDetailId);
      
        Database.PutParameter(dbCommand,"?PrevEquipmentTransactionDetailId", equipmentTransactionDetailLink.PrevEquipmentTransactionDetailId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(EquipmentTransactionDetailLink equipmentTransactionDetailLink)
      {
        Insert(equipmentTransactionDetailLink, null);
      }


      public static void Insert(List<EquipmentTransactionDetailLink>  equipmentTransactionDetailLinkList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(EquipmentTransactionDetailLink equipmentTransactionDetailLink in  equipmentTransactionDetailLinkList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?EquipmentTransactionDetailId", equipmentTransactionDetailLink.EquipmentTransactionDetailId);
      
        Database.PutParameter(dbCommand,"?PrevEquipmentTransactionDetailId", equipmentTransactionDetailLink.PrevEquipmentTransactionDetailId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?EquipmentTransactionDetailId",equipmentTransactionDetailLink.EquipmentTransactionDetailId);
      
        Database.UpdateParameter(dbCommand,"?PrevEquipmentTransactionDetailId",equipmentTransactionDetailLink.PrevEquipmentTransactionDetailId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<EquipmentTransactionDetailLink>  equipmentTransactionDetailLinkList)
      {
        Insert(equipmentTransactionDetailLinkList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update EquipmentTransactionDetailLink Set "
      
        + " PrevEquipmentTransactionDetailId = ?PrevEquipmentTransactionDetailId "
      
        + " Where "
        
          + " EquipmentTransactionDetailId = ?EquipmentTransactionDetailId "
        
      ;

      public static void Update(EquipmentTransactionDetailLink equipmentTransactionDetailLink, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?EquipmentTransactionDetailId", equipmentTransactionDetailLink.EquipmentTransactionDetailId);
      
        Database.PutParameter(dbCommand,"?PrevEquipmentTransactionDetailId", equipmentTransactionDetailLink.PrevEquipmentTransactionDetailId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(EquipmentTransactionDetailLink equipmentTransactionDetailLink)
      {
        Update(equipmentTransactionDetailLink, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " EquipmentTransactionDetailId, "
      
        + " PrevEquipmentTransactionDetailId "
      

      + " From EquipmentTransactionDetailLink "

      
        + " Where "
        
          + " EquipmentTransactionDetailId = ?EquipmentTransactionDetailId "
        
      ;

      public static EquipmentTransactionDetailLink FindByPrimaryKey(
      int equipmentTransactionDetailId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?EquipmentTransactionDetailId", equipmentTransactionDetailId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("EquipmentTransactionDetailLink not found, search by primary key");

      }

      public static EquipmentTransactionDetailLink FindByPrimaryKey(
      int equipmentTransactionDetailId
      )
      {
      return FindByPrimaryKey(
      equipmentTransactionDetailId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(EquipmentTransactionDetailLink equipmentTransactionDetailLink, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?EquipmentTransactionDetailId",equipmentTransactionDetailLink.EquipmentTransactionDetailId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(EquipmentTransactionDetailLink equipmentTransactionDetailLink)
      {
      return Exists(equipmentTransactionDetailLink, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from EquipmentTransactionDetailLink limit 1";

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

      public static EquipmentTransactionDetailLink Load(IDataReader dataReader, int offset)
      {
      EquipmentTransactionDetailLink equipmentTransactionDetailLink = new EquipmentTransactionDetailLink();

      equipmentTransactionDetailLink.EquipmentTransactionDetailId = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            equipmentTransactionDetailLink.PrevEquipmentTransactionDetailId = dataReader.GetInt32(1 + offset);
          

      return equipmentTransactionDetailLink;
      }

      public static EquipmentTransactionDetailLink Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From EquipmentTransactionDetailLink "

      
        + " Where "
        
          + " EquipmentTransactionDetailId = ?EquipmentTransactionDetailId "
        
      ;
      public static void Delete(EquipmentTransactionDetailLink equipmentTransactionDetailLink, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?EquipmentTransactionDetailId", equipmentTransactionDetailLink.EquipmentTransactionDetailId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(EquipmentTransactionDetailLink equipmentTransactionDetailLink)
      {
        Delete(equipmentTransactionDetailLink, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From EquipmentTransactionDetailLink ";

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

      
        + " EquipmentTransactionDetailId, "
      
        + " PrevEquipmentTransactionDetailId "
      

      + " From EquipmentTransactionDetailLink ";
      public static List<EquipmentTransactionDetailLink> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<EquipmentTransactionDetailLink> rv = new List<EquipmentTransactionDetailLink>();

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

      public static List<EquipmentTransactionDetailLink> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<EquipmentTransactionDetailLink> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (EquipmentTransactionDetailLink obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return EquipmentTransactionDetailId == obj.EquipmentTransactionDetailId && PrevEquipmentTransactionDetailId == obj.PrevEquipmentTransactionDetailId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<EquipmentTransactionDetailLink> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(EquipmentTransactionDetailLink));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(EquipmentTransactionDetailLink item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<EquipmentTransactionDetailLink>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(EquipmentTransactionDetailLink));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<EquipmentTransactionDetailLink> itemsList
      = new List<EquipmentTransactionDetailLink>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is EquipmentTransactionDetailLink)
      itemsList.Add(deserializedObject as EquipmentTransactionDetailLink);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_equipmentTransactionDetailId;
      
        protected int? m_prevEquipmentTransactionDetailId;
      
      #endregion

      #region Constructors
      public EquipmentTransactionDetailLink(
      int 
          equipmentTransactionDetailId
      ) : this()
      {
      
        m_equipmentTransactionDetailId = equipmentTransactionDetailId;
      
      }

      


        public EquipmentTransactionDetailLink(
        int 
          equipmentTransactionDetailId,int? 
          prevEquipmentTransactionDetailId
        ) : this()
        {
        
          m_equipmentTransactionDetailId = equipmentTransactionDetailId;
        
          m_prevEquipmentTransactionDetailId = prevEquipmentTransactionDetailId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int EquipmentTransactionDetailId
        {
        get { return m_equipmentTransactionDetailId;}
        set { m_equipmentTransactionDetailId = value; }
        }
      
        [XmlElement]
        public int? PrevEquipmentTransactionDetailId
        {
        get { return m_prevEquipmentTransactionDetailId;}
        set { m_prevEquipmentTransactionDetailId = value; }
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

    