
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


      public partial class OrderSourceOwnPhone : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into OrderSourceOwnPhone ( " +
      
        " OrderSourceId, " +
      
        " Number " +
      
      ") Values (" +
      
        " ?OrderSourceId, " +
      
        " ?Number " +
      
      ")";

      public static void Insert(OrderSourceOwnPhone orderSourceOwnPhone, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?OrderSourceId", orderSourceOwnPhone.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?Number", orderSourceOwnPhone.Number);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        orderSourceOwnPhone.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(OrderSourceOwnPhone orderSourceOwnPhone)
      {
        Insert(orderSourceOwnPhone, null);
      }


      public static void Insert(List<OrderSourceOwnPhone>  orderSourceOwnPhoneList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(OrderSourceOwnPhone orderSourceOwnPhone in  orderSourceOwnPhoneList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?OrderSourceId", orderSourceOwnPhone.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?Number", orderSourceOwnPhone.Number);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?OrderSourceId",orderSourceOwnPhone.OrderSourceId);
      
        Database.UpdateParameter(dbCommand,"?Number",orderSourceOwnPhone.Number);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        orderSourceOwnPhone.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<OrderSourceOwnPhone>  orderSourceOwnPhoneList)
      {
        Insert(orderSourceOwnPhoneList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update OrderSourceOwnPhone Set "
      
        + " OrderSourceId = ?OrderSourceId, "
      
        + " Number = ?Number "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(OrderSourceOwnPhone orderSourceOwnPhone, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", orderSourceOwnPhone.ID);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", orderSourceOwnPhone.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?Number", orderSourceOwnPhone.Number);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(OrderSourceOwnPhone orderSourceOwnPhone)
      {
        Update(orderSourceOwnPhone, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " OrderSourceId, "
      
        + " Number "
      

      + " From OrderSourceOwnPhone "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static OrderSourceOwnPhone FindByPrimaryKey(
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
      throw new DataNotFoundException("OrderSourceOwnPhone not found, search by primary key");

      }

      public static OrderSourceOwnPhone FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(OrderSourceOwnPhone orderSourceOwnPhone, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",orderSourceOwnPhone.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(OrderSourceOwnPhone orderSourceOwnPhone)
      {
      return Exists(orderSourceOwnPhone, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from OrderSourceOwnPhone limit 1";

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

      public static OrderSourceOwnPhone Load(IDataReader dataReader, int offset)
      {
      OrderSourceOwnPhone orderSourceOwnPhone = new OrderSourceOwnPhone();

      orderSourceOwnPhone.ID = dataReader.GetInt32(0 + offset);
          orderSourceOwnPhone.OrderSourceId = dataReader.GetInt32(1 + offset);
          orderSourceOwnPhone.Number = dataReader.GetString(2 + offset);
          

      return orderSourceOwnPhone;
      }

      public static OrderSourceOwnPhone Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From OrderSourceOwnPhone "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(OrderSourceOwnPhone orderSourceOwnPhone, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", orderSourceOwnPhone.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(OrderSourceOwnPhone orderSourceOwnPhone)
      {
        Delete(orderSourceOwnPhone, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From OrderSourceOwnPhone ";

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
      
        + " OrderSourceId, "
      
        + " Number "
      

      + " From OrderSourceOwnPhone ";
      public static List<OrderSourceOwnPhone> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<OrderSourceOwnPhone> rv = new List<OrderSourceOwnPhone>();

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

      public static List<OrderSourceOwnPhone> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<OrderSourceOwnPhone> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (OrderSourceOwnPhone obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && OrderSourceId == obj.OrderSourceId && Number == obj.Number;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<OrderSourceOwnPhone> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrderSourceOwnPhone));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(OrderSourceOwnPhone item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<OrderSourceOwnPhone>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrderSourceOwnPhone));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<OrderSourceOwnPhone> itemsList
      = new List<OrderSourceOwnPhone>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is OrderSourceOwnPhone)
      itemsList.Add(deserializedObject as OrderSourceOwnPhone);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_orderSourceId;
      
        protected String m_number;
      
      #endregion

      #region Constructors
      public OrderSourceOwnPhone(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public OrderSourceOwnPhone(
        int 
          iD,int 
          orderSourceId,String 
          number
        ) : this()
        {
        
          m_iD = iD;
        
          m_orderSourceId = orderSourceId;
        
          m_number = number;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int OrderSourceId
        {
        get { return m_orderSourceId;}
        set { m_orderSourceId = value; }
        }
      
        [XmlElement]
        public String Number
        {
        get { return m_number;}
        set { m_number = value; }
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

    