
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


      public partial class Area : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Area ( " +
      
        " ServmanId, " +
      
        " Name, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?ServmanId, " +
      
        " ?Name, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(Area area, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ServmanId", area.ServmanId);
      
        Database.PutParameter(dbCommand,"?Name", area.Name);
      
        Database.PutParameter(dbCommand,"?Description", area.Description);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        area.ID = Convert.ToByte(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Area area)
      {
        Insert(area, null);
      }


      public static void Insert(List<Area>  areaList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Area area in  areaList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ServmanId", area.ServmanId);
      
        Database.PutParameter(dbCommand,"?Name", area.Name);
      
        Database.PutParameter(dbCommand,"?Description", area.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ServmanId",area.ServmanId);
      
        Database.UpdateParameter(dbCommand,"?Name",area.Name);
      
        Database.UpdateParameter(dbCommand,"?Description",area.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        area.ID = Convert.ToByte(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Area>  areaList)
      {
        Insert(areaList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Area Set "
      
        + " ServmanId = ?ServmanId, "
      
        + " Name = ?Name, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Area area, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", area.ID);
      
        Database.PutParameter(dbCommand,"?ServmanId", area.ServmanId);
      
        Database.PutParameter(dbCommand,"?Name", area.Name);
      
        Database.PutParameter(dbCommand,"?Description", area.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Area area)
      {
        Update(area, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " ServmanId, "
      
        + " Name, "
      
        + " Description "
      

      + " From Area "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Area FindByPrimaryKey(
      byte iD, IDbConnection connection
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
      throw new DataNotFoundException("Area not found, search by primary key");

      }

      public static Area FindByPrimaryKey(
      byte iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Area area, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",area.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Area area)
      {
      return Exists(area, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Area limit 1";

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

      public static Area Load(IDataReader dataReader, int offset)
      {
      Area area = new Area();

      area.ID = dataReader.GetByte(0 + offset);
          area.ServmanId = dataReader.GetString(1 + offset);
          area.Name = dataReader.GetString(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            area.Description = dataReader.GetString(3 + offset);
          

      return area;
      }

      public static Area Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Area "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Area area, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", area.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Area area)
      {
        Delete(area, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Area ";

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
      
        + " ServmanId, "
      
        + " Name, "
      
        + " Description "
      

      + " From Area ";
      public static List<Area> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Area> rv = new List<Area>();

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

      public static List<Area> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Area> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Area obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && ServmanId == obj.ServmanId && Name == obj.Name && Description == obj.Description;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Area> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Area));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Area item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Area>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Area));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Area> itemsList
      = new List<Area>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Area)
      itemsList.Add(deserializedObject as Area);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected byte m_iD;
      
        protected String m_servmanId;
      
        protected String m_name;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public Area(
      byte 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Area(
        byte 
          iD,String 
          servmanId,String 
          name,String 
          description
        ) : this()
        {
        
          m_iD = iD;
        
          m_servmanId = servmanId;
        
          m_name = name;
        
          m_description = description;
        
        }


      
      #endregion

      
        [XmlElement]
        public byte ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String ServmanId
        {
        get { return m_servmanId;}
        set { m_servmanId = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
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

    