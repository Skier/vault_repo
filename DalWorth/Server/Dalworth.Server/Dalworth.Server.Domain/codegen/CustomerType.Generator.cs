
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


      public partial class CustomerType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into CustomerType ( " +
      
        " ID, " +
      
        " Type, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Type, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(CustomerType customerType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", customerType.ID);
      
        Database.PutParameter(dbCommand,"?Type", customerType.Type);
      
        Database.PutParameter(dbCommand,"?Description", customerType.Description);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(CustomerType customerType)
      {
        Insert(customerType, null);
      }


      public static void Insert(List<CustomerType>  customerTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(CustomerType customerType in  customerTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", customerType.ID);
      
        Database.PutParameter(dbCommand,"?Type", customerType.Type);
      
        Database.PutParameter(dbCommand,"?Description", customerType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",customerType.ID);
      
        Database.UpdateParameter(dbCommand,"?Type",customerType.Type);
      
        Database.UpdateParameter(dbCommand,"?Description",customerType.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<CustomerType>  customerTypeList)
      {
        Insert(customerTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update CustomerType Set "
      
        + " Type = ?Type, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(CustomerType customerType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", customerType.ID);
      
        Database.PutParameter(dbCommand,"?Type", customerType.Type);
      
        Database.PutParameter(dbCommand,"?Description", customerType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(CustomerType customerType)
      {
        Update(customerType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Type, "
      
        + " Description "
      

      + " From CustomerType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static CustomerType FindByPrimaryKey(
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
      throw new DataNotFoundException("CustomerType not found, search by primary key");

      }

      public static CustomerType FindByPrimaryKey(
      byte iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(CustomerType customerType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",customerType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(CustomerType customerType)
      {
      return Exists(customerType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from CustomerType limit 1";

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

      public static CustomerType Load(IDataReader dataReader, int offset)
      {
      CustomerType customerType = new CustomerType();

      customerType.ID = dataReader.GetByte(0 + offset);
          customerType.Type = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            customerType.Description = dataReader.GetString(2 + offset);
          

      return customerType;
      }

      public static CustomerType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From CustomerType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(CustomerType customerType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", customerType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(CustomerType customerType)
      {
        Delete(customerType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From CustomerType ";

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
      

      + " From CustomerType ";
      public static List<CustomerType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<CustomerType> rv = new List<CustomerType>();

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

      public static List<CustomerType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<CustomerType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (CustomerType obj)
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

      List<CustomerType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(CustomerType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CustomerType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<CustomerType> itemsList
      = new List<CustomerType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CustomerType)
      itemsList.Add(deserializedObject as CustomerType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected byte m_iD;
      
        protected String m_type;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public CustomerType(
      byte 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public CustomerType(
        byte 
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
        public byte ID
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

    