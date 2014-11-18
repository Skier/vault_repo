
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


      public partial class OrderSource : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into OrderSource ( " +
      
        " ParentOrderSourceId, " +
      
        " Name, " +
      
        " Active " +
      
      ") Values (" +
      
        " ?ParentOrderSourceId, " +
      
        " ?Name, " +
      
        " ?Active " +
      
      ")";

      public static void Insert(OrderSource orderSource, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ParentOrderSourceId", orderSource.ParentOrderSourceId);
      
        Database.PutParameter(dbCommand,"?Name", orderSource.Name);
      
        Database.PutParameter(dbCommand,"?Active", orderSource.Active);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        orderSource.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(OrderSource orderSource)
      {
        Insert(orderSource, null);
      }


      public static void Insert(List<OrderSource>  orderSourceList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(OrderSource orderSource in  orderSourceList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ParentOrderSourceId", orderSource.ParentOrderSourceId);
      
        Database.PutParameter(dbCommand,"?Name", orderSource.Name);
      
        Database.PutParameter(dbCommand,"?Active", orderSource.Active);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ParentOrderSourceId",orderSource.ParentOrderSourceId);
      
        Database.UpdateParameter(dbCommand,"?Name",orderSource.Name);
      
        Database.UpdateParameter(dbCommand,"?Active",orderSource.Active);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        orderSource.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<OrderSource>  orderSourceList)
      {
        Insert(orderSourceList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update OrderSource Set "
      
        + " ParentOrderSourceId = ?ParentOrderSourceId, "
      
        + " Name = ?Name, "
      
        + " Active = ?Active "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(OrderSource orderSource, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", orderSource.ID);
      
        Database.PutParameter(dbCommand,"?ParentOrderSourceId", orderSource.ParentOrderSourceId);
      
        Database.PutParameter(dbCommand,"?Name", orderSource.Name);
      
        Database.PutParameter(dbCommand,"?Active", orderSource.Active);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(OrderSource orderSource)
      {
        Update(orderSource, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " ParentOrderSourceId, "
      
        + " Name, "
      
        + " Active "
      

      + " From OrderSource "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static OrderSource FindByPrimaryKey(
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
      throw new DataNotFoundException("OrderSource not found, search by primary key");

      }

      public static OrderSource FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(OrderSource orderSource, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",orderSource.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(OrderSource orderSource)
      {
      return Exists(orderSource, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from OrderSource limit 1";

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

      public static OrderSource Load(IDataReader dataReader, int offset)
      {
      OrderSource orderSource = new OrderSource();

      orderSource.ID = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            orderSource.ParentOrderSourceId = dataReader.GetInt32(1 + offset);
          orderSource.Name = dataReader.GetString(2 + offset);
          orderSource.Active = dataReader.GetBoolean(3 + offset);
          

      return orderSource;
      }

      public static OrderSource Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From OrderSource "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(OrderSource orderSource, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", orderSource.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(OrderSource orderSource)
      {
        Delete(orderSource, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From OrderSource ";

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
      
        + " ParentOrderSourceId, "
      
        + " Name, "
      
        + " Active "
      

      + " From OrderSource ";
      public static List<OrderSource> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<OrderSource> rv = new List<OrderSource>();

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

      public static List<OrderSource> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<OrderSource> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (OrderSource obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && ParentOrderSourceId == obj.ParentOrderSourceId && Name == obj.Name && Active == obj.Active;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<OrderSource> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrderSource));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(OrderSource item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<OrderSource>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrderSource));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<OrderSource> itemsList
      = new List<OrderSource>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is OrderSource)
      itemsList.Add(deserializedObject as OrderSource);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int? m_parentOrderSourceId;
      
        protected String m_name;
      
        protected bool m_active;
      
      #endregion

      #region Constructors
      public OrderSource(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public OrderSource(
        int 
          iD,int? 
          parentOrderSourceId,String 
          name,bool 
          active
        ) : this()
        {
        
          m_iD = iD;
        
          m_parentOrderSourceId = parentOrderSourceId;
        
          m_name = name;
        
          m_active = active;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int? ParentOrderSourceId
        {
        get { return m_parentOrderSourceId;}
        set { m_parentOrderSourceId = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        [XmlElement]
        public bool Active
        {
        get { return m_active;}
        set { m_active = value; }
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

    