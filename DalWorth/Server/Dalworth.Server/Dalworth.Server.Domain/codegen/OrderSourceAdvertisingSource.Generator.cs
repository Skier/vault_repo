
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


      public partial class OrderSourceAdvertisingSource : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into OrderSourceAdvertisingSource ( " +
      
        " OrderSourceId, " +
      
        " AdvertisingSourceId " +
      
      ") Values (" +
      
        " ?OrderSourceId, " +
      
        " ?AdvertisingSourceId " +
      
      ")";

      public static void Insert(OrderSourceAdvertisingSource orderSourceAdvertisingSource, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?OrderSourceId", orderSourceAdvertisingSource.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceId", orderSourceAdvertisingSource.AdvertisingSourceId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(OrderSourceAdvertisingSource orderSourceAdvertisingSource)
      {
        Insert(orderSourceAdvertisingSource, null);
      }


      public static void Insert(List<OrderSourceAdvertisingSource>  orderSourceAdvertisingSourceList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(OrderSourceAdvertisingSource orderSourceAdvertisingSource in  orderSourceAdvertisingSourceList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?OrderSourceId", orderSourceAdvertisingSource.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceId", orderSourceAdvertisingSource.AdvertisingSourceId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?OrderSourceId",orderSourceAdvertisingSource.OrderSourceId);
      
        Database.UpdateParameter(dbCommand,"?AdvertisingSourceId",orderSourceAdvertisingSource.AdvertisingSourceId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<OrderSourceAdvertisingSource>  orderSourceAdvertisingSourceList)
      {
        Insert(orderSourceAdvertisingSourceList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update OrderSourceAdvertisingSource Set "
      
        + " Where "
        
          + " OrderSourceId = ?OrderSourceId and  "
        
          + " AdvertisingSourceId = ?AdvertisingSourceId "
        
      ;

      public static void Update(OrderSourceAdvertisingSource orderSourceAdvertisingSource, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?OrderSourceId", orderSourceAdvertisingSource.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceId", orderSourceAdvertisingSource.AdvertisingSourceId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(OrderSourceAdvertisingSource orderSourceAdvertisingSource)
      {
        Update(orderSourceAdvertisingSource, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " OrderSourceId, "
      
        + " AdvertisingSourceId "
      

      + " From OrderSourceAdvertisingSource "

      
        + " Where "
        
          + " OrderSourceId = ?OrderSourceId and  "
        
          + " AdvertisingSourceId = ?AdvertisingSourceId "
        
      ;

      public static OrderSourceAdvertisingSource FindByPrimaryKey(
      int orderSourceId,int advertisingSourceId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?OrderSourceId", orderSourceId);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceId", advertisingSourceId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("OrderSourceAdvertisingSource not found, search by primary key");

      }

      public static OrderSourceAdvertisingSource FindByPrimaryKey(
      int orderSourceId,int advertisingSourceId
      )
      {
      return FindByPrimaryKey(
      orderSourceId,advertisingSourceId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(OrderSourceAdvertisingSource orderSourceAdvertisingSource, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?OrderSourceId",orderSourceAdvertisingSource.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceId",orderSourceAdvertisingSource.AdvertisingSourceId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(OrderSourceAdvertisingSource orderSourceAdvertisingSource)
      {
      return Exists(orderSourceAdvertisingSource, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from OrderSourceAdvertisingSource limit 1";

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

      public static OrderSourceAdvertisingSource Load(IDataReader dataReader, int offset)
      {
      OrderSourceAdvertisingSource orderSourceAdvertisingSource = new OrderSourceAdvertisingSource();

      orderSourceAdvertisingSource.OrderSourceId = dataReader.GetInt32(0 + offset);
          orderSourceAdvertisingSource.AdvertisingSourceId = dataReader.GetInt32(1 + offset);
          

      return orderSourceAdvertisingSource;
      }

      public static OrderSourceAdvertisingSource Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From OrderSourceAdvertisingSource "

      
        + " Where "
        
          + " OrderSourceId = ?OrderSourceId and  "
        
          + " AdvertisingSourceId = ?AdvertisingSourceId "
        
      ;
      public static void Delete(OrderSourceAdvertisingSource orderSourceAdvertisingSource, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?OrderSourceId", orderSourceAdvertisingSource.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceId", orderSourceAdvertisingSource.AdvertisingSourceId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(OrderSourceAdvertisingSource orderSourceAdvertisingSource)
      {
        Delete(orderSourceAdvertisingSource, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From OrderSourceAdvertisingSource ";

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

      
        + " OrderSourceId, "
      
        + " AdvertisingSourceId "
      

      + " From OrderSourceAdvertisingSource ";
      public static List<OrderSourceAdvertisingSource> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<OrderSourceAdvertisingSource> rv = new List<OrderSourceAdvertisingSource>();

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

      public static List<OrderSourceAdvertisingSource> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<OrderSourceAdvertisingSource> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (OrderSourceAdvertisingSource obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return OrderSourceId == obj.OrderSourceId && AdvertisingSourceId == obj.AdvertisingSourceId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<OrderSourceAdvertisingSource> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrderSourceAdvertisingSource));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(OrderSourceAdvertisingSource item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<OrderSourceAdvertisingSource>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrderSourceAdvertisingSource));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<OrderSourceAdvertisingSource> itemsList
      = new List<OrderSourceAdvertisingSource>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is OrderSourceAdvertisingSource)
      itemsList.Add(deserializedObject as OrderSourceAdvertisingSource);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_orderSourceId;
      
        protected int m_advertisingSourceId;
      
      #endregion

      #region Constructors
      public OrderSourceAdvertisingSource(
      int 
          orderSourceId,int 
          advertisingSourceId
      ) : this()
      {
      
        m_orderSourceId = orderSourceId;
      
        m_advertisingSourceId = advertisingSourceId;
      
      }

      
      #endregion

      
        [XmlElement]
        public int OrderSourceId
        {
        get { return m_orderSourceId;}
        set { m_orderSourceId = value; }
        }
      
        [XmlElement]
        public int AdvertisingSourceId
        {
        get { return m_advertisingSourceId;}
        set { m_advertisingSourceId = value; }
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

    