
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


      public partial class ItemType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ItemType ( " +
      
        " ID, " +
      
        " Type, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Type, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(ItemType itemType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", itemType.ID);
      
        Database.PutParameter(dbCommand,"?Type", itemType.Type);
      
        Database.PutParameter(dbCommand,"?Description", itemType.Description);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ItemType itemType)
      {
        Insert(itemType, null);
      }


      public static void Insert(List<ItemType>  itemTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ItemType itemType in  itemTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", itemType.ID);
      
        Database.PutParameter(dbCommand,"?Type", itemType.Type);
      
        Database.PutParameter(dbCommand,"?Description", itemType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",itemType.ID);
      
        Database.UpdateParameter(dbCommand,"?Type",itemType.Type);
      
        Database.UpdateParameter(dbCommand,"?Description",itemType.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ItemType>  itemTypeList)
      {
        Insert(itemTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ItemType Set "
      
        + " Type = ?Type, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(ItemType itemType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", itemType.ID);
      
        Database.PutParameter(dbCommand,"?Type", itemType.Type);
      
        Database.PutParameter(dbCommand,"?Description", itemType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ItemType itemType)
      {
        Update(itemType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Type, "
      
        + " Description "
      

      + " From ItemType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static ItemType FindByPrimaryKey(
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
      throw new DataNotFoundException("ItemType not found, search by primary key");

      }

      public static ItemType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ItemType itemType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",itemType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ItemType itemType)
      {
      return Exists(itemType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ItemType limit 1";

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

      public static ItemType Load(IDataReader dataReader, int offset)
      {
      ItemType itemType = new ItemType();

      itemType.ID = dataReader.GetInt32(0 + offset);
          itemType.Type = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            itemType.Description = dataReader.GetString(2 + offset);
          

      return itemType;
      }

      public static ItemType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ItemType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(ItemType itemType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", itemType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ItemType itemType)
      {
        Delete(itemType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ItemType ";

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
      
        + " Type, "
      
        + " Description "
      

      + " From ItemType ";
      public static List<ItemType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ItemType> rv = new List<ItemType>();

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

      public static List<ItemType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ItemType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ItemType obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Type == obj.Type && Description == obj.Description;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ItemType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ItemType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ItemType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ItemType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ItemType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ItemType> itemsList
      = new List<ItemType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ItemType)
      itemsList.Add(deserializedObject as ItemType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_type;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public ItemType(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public ItemType(
        int 
          iD,String 
          type,String 
          description
        ) : this()
        {
        
          m_iD = iD;
        
          m_type = type;
        
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
        public String Type
        {
        get { return m_type;}
        set { m_type = value; }
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

    