
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


      public partial class QbItem : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbItem ( " +
      
        " ListId, " +
      
        " FullName, " +
      
        " AccountRefListId, " +
      
        " SalesTaxCodeRefListId, " +
      
        " TimeCreated, " +
      
        " TimeModified, " +
      
        " EditSequence, " +
      
        " Name, " +
      
        " IsActive, " +
      
        " QbItemTypeId, " +
      
        " TaxRate, " +
      
        " Description, " +
      
        " Price " +
      
      ") Values (" +
      
        " ?ListId, " +
      
        " ?FullName, " +
      
        " ?AccountRefListId, " +
      
        " ?SalesTaxCodeRefListId, " +
      
        " ?TimeCreated, " +
      
        " ?TimeModified, " +
      
        " ?EditSequence, " +
      
        " ?Name, " +
      
        " ?IsActive, " +
      
        " ?QbItemTypeId, " +
      
        " ?TaxRate, " +
      
        " ?Description, " +
      
        " ?Price " +
      
      ")";

      public static void Insert(QbItem qbItem, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbItem.ListId);
      
        Database.PutParameter(dbCommand,"?FullName", qbItem.FullName);
      
        Database.PutParameter(dbCommand,"?AccountRefListId", qbItem.AccountRefListId);
      
        Database.PutParameter(dbCommand,"?SalesTaxCodeRefListId", qbItem.SalesTaxCodeRefListId);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbItem.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbItem.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbItem.EditSequence);
      
        Database.PutParameter(dbCommand,"?Name", qbItem.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", qbItem.IsActive);
      
        Database.PutParameter(dbCommand,"?QbItemTypeId", qbItem.QbItemTypeId);
      
        Database.PutParameter(dbCommand,"?TaxRate", qbItem.TaxRate);
      
        Database.PutParameter(dbCommand,"?Description", qbItem.Description);
      
        Database.PutParameter(dbCommand,"?Price", qbItem.Price);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbItem qbItem)
      {
        Insert(qbItem, null);
      }


      public static void Insert(List<QbItem>  qbItemList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbItem qbItem in  qbItemList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbItem.ListId);
      
        Database.PutParameter(dbCommand,"?FullName", qbItem.FullName);
      
        Database.PutParameter(dbCommand,"?AccountRefListId", qbItem.AccountRefListId);
      
        Database.PutParameter(dbCommand,"?SalesTaxCodeRefListId", qbItem.SalesTaxCodeRefListId);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbItem.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbItem.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbItem.EditSequence);
      
        Database.PutParameter(dbCommand,"?Name", qbItem.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", qbItem.IsActive);
      
        Database.PutParameter(dbCommand,"?QbItemTypeId", qbItem.QbItemTypeId);
      
        Database.PutParameter(dbCommand,"?TaxRate", qbItem.TaxRate);
      
        Database.PutParameter(dbCommand,"?Description", qbItem.Description);
      
        Database.PutParameter(dbCommand,"?Price", qbItem.Price);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ListId",qbItem.ListId);
      
        Database.UpdateParameter(dbCommand,"?FullName",qbItem.FullName);
      
        Database.UpdateParameter(dbCommand,"?AccountRefListId",qbItem.AccountRefListId);
      
        Database.UpdateParameter(dbCommand,"?SalesTaxCodeRefListId",qbItem.SalesTaxCodeRefListId);
      
        Database.UpdateParameter(dbCommand,"?TimeCreated",qbItem.TimeCreated);
      
        Database.UpdateParameter(dbCommand,"?TimeModified",qbItem.TimeModified);
      
        Database.UpdateParameter(dbCommand,"?EditSequence",qbItem.EditSequence);
      
        Database.UpdateParameter(dbCommand,"?Name",qbItem.Name);
      
        Database.UpdateParameter(dbCommand,"?IsActive",qbItem.IsActive);
      
        Database.UpdateParameter(dbCommand,"?QbItemTypeId",qbItem.QbItemTypeId);
      
        Database.UpdateParameter(dbCommand,"?TaxRate",qbItem.TaxRate);
      
        Database.UpdateParameter(dbCommand,"?Description",qbItem.Description);
      
        Database.UpdateParameter(dbCommand,"?Price",qbItem.Price);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbItem>  qbItemList)
      {
        Insert(qbItemList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbItem Set "
      
        + " FullName = ?FullName, "
      
        + " AccountRefListId = ?AccountRefListId, "
      
        + " SalesTaxCodeRefListId = ?SalesTaxCodeRefListId, "
      
        + " TimeCreated = ?TimeCreated, "
      
        + " TimeModified = ?TimeModified, "
      
        + " EditSequence = ?EditSequence, "
      
        + " Name = ?Name, "
      
        + " IsActive = ?IsActive, "
      
        + " QbItemTypeId = ?QbItemTypeId, "
      
        + " TaxRate = ?TaxRate, "
      
        + " Description = ?Description, "
      
        + " Price = ?Price "
      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static void Update(QbItem qbItem, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbItem.ListId);
      
        Database.PutParameter(dbCommand,"?FullName", qbItem.FullName);
      
        Database.PutParameter(dbCommand,"?AccountRefListId", qbItem.AccountRefListId);
      
        Database.PutParameter(dbCommand,"?SalesTaxCodeRefListId", qbItem.SalesTaxCodeRefListId);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbItem.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbItem.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbItem.EditSequence);
      
        Database.PutParameter(dbCommand,"?Name", qbItem.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", qbItem.IsActive);
      
        Database.PutParameter(dbCommand,"?QbItemTypeId", qbItem.QbItemTypeId);
      
        Database.PutParameter(dbCommand,"?TaxRate", qbItem.TaxRate);
      
        Database.PutParameter(dbCommand,"?Description", qbItem.Description);
      
        Database.PutParameter(dbCommand,"?Price", qbItem.Price);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbItem qbItem)
      {
        Update(qbItem, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ListId, "
      
        + " FullName, "
      
        + " AccountRefListId, "
      
        + " SalesTaxCodeRefListId, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence, "
      
        + " Name, "
      
        + " IsActive, "
      
        + " QbItemTypeId, "
      
        + " TaxRate, "
      
        + " Description, "
      
        + " Price "
      

      + " From QbItem "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static QbItem FindByPrimaryKey(
      String listId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", listId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("QbItem not found, search by primary key");

      }

      public static QbItem FindByPrimaryKey(
      String listId
      )
      {
      return FindByPrimaryKey(
      listId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbItem qbItem, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId",qbItem.ListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbItem qbItem)
      {
      return Exists(qbItem, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbItem limit 1";

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

      public static QbItem Load(IDataReader dataReader, int offset)
      {
      QbItem qbItem = new QbItem();

      qbItem.ListId = dataReader.GetString(0 + offset);
          qbItem.FullName = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            qbItem.AccountRefListId = dataReader.GetString(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            qbItem.SalesTaxCodeRefListId = dataReader.GetString(3 + offset);
          qbItem.TimeCreated = dataReader.GetDateTime(4 + offset);
          qbItem.TimeModified = dataReader.GetDateTime(5 + offset);
          qbItem.EditSequence = dataReader.GetString(6 + offset);
          qbItem.Name = dataReader.GetString(7 + offset);
          qbItem.IsActive = dataReader.GetBoolean(8 + offset);
          qbItem.QbItemTypeId = dataReader.GetInt32(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            qbItem.TaxRate = dataReader.GetDecimal(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            qbItem.Description = dataReader.GetString(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            qbItem.Price = dataReader.GetDecimal(12 + offset);
          

      return qbItem;
      }

      public static QbItem Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbItem "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;
      public static void Delete(QbItem qbItem, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ListId", qbItem.ListId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbItem qbItem)
      {
        Delete(qbItem, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbItem ";

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

      
        + " ListId, "
      
        + " FullName, "
      
        + " AccountRefListId, "
      
        + " SalesTaxCodeRefListId, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence, "
      
        + " Name, "
      
        + " IsActive, "
      
        + " QbItemTypeId, "
      
        + " TaxRate, "
      
        + " Description, "
      
        + " Price "
      

      + " From QbItem ";
      public static List<QbItem> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbItem> rv = new List<QbItem>();

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

      public static List<QbItem> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbItem> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbItem obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ListId == obj.ListId && FullName == obj.FullName && AccountRefListId == obj.AccountRefListId && SalesTaxCodeRefListId == obj.SalesTaxCodeRefListId && TimeCreated == obj.TimeCreated && TimeModified == obj.TimeModified && EditSequence == obj.EditSequence && Name == obj.Name && IsActive == obj.IsActive && QbItemTypeId == obj.QbItemTypeId && TaxRate == obj.TaxRate && Description == obj.Description && Price == obj.Price;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbItem> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbItem));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbItem item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbItem>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbItem));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbItem> itemsList
      = new List<QbItem>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbItem)
      itemsList.Add(deserializedObject as QbItem);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_listId;
      
        protected String m_fullName;
      
        protected String m_accountRefListId;
      
        protected String m_salesTaxCodeRefListId;
      
        protected DateTime m_timeCreated;
      
        protected DateTime m_timeModified;
      
        protected String m_editSequence;
      
        protected String m_name;
      
        protected bool m_isActive;
      
        protected int m_qbItemTypeId;
      
        protected decimal m_taxRate;
      
        protected String m_description;
      
        protected decimal m_price;
      
      #endregion

      #region Constructors
      public QbItem(
      String 
          listId
      ) : this()
      {
      
        m_listId = listId;
      
      }

      


        public QbItem(
        String 
          listId,String 
          fullName,String 
          accountRefListId,String 
          salesTaxCodeRefListId,DateTime 
          timeCreated,DateTime 
          timeModified,String 
          editSequence,String 
          name,bool 
          isActive,int 
          qbItemTypeId,decimal 
          taxRate,String 
          description,decimal 
          price
        ) : this()
        {
        
          m_listId = listId;
        
          m_fullName = fullName;
        
          m_accountRefListId = accountRefListId;
        
          m_salesTaxCodeRefListId = salesTaxCodeRefListId;
        
          m_timeCreated = timeCreated;
        
          m_timeModified = timeModified;
        
          m_editSequence = editSequence;
        
          m_name = name;
        
          m_isActive = isActive;
        
          m_qbItemTypeId = qbItemTypeId;
        
          m_taxRate = taxRate;
        
          m_description = description;
        
          m_price = price;
        
        }


      
      #endregion

      
        [XmlElement]
        public String ListId
        {
        get { return m_listId;}
        set { m_listId = value; }
        }
      
        [XmlElement]
        public String FullName
        {
        get { return m_fullName;}
        set { m_fullName = value; }
        }
      
        [XmlElement]
        public String AccountRefListId
        {
        get { return m_accountRefListId;}
        set { m_accountRefListId = value; }
        }
      
        [XmlElement]
        public String SalesTaxCodeRefListId
        {
        get { return m_salesTaxCodeRefListId;}
        set { m_salesTaxCodeRefListId = value; }
        }
      
        [XmlElement]
        public DateTime TimeCreated
        {
        get { return m_timeCreated;}
        set { m_timeCreated = value; }
        }
      
        [XmlElement]
        public DateTime TimeModified
        {
        get { return m_timeModified;}
        set { m_timeModified = value; }
        }
      
        [XmlElement]
        public String EditSequence
        {
        get { return m_editSequence;}
        set { m_editSequence = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        [XmlElement]
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
        }
      
        [XmlElement]
        public int QbItemTypeId
        {
        get { return m_qbItemTypeId;}
        set { m_qbItemTypeId = value; }
        }
      
        [XmlElement]
        public decimal TaxRate
        {
        get { return m_taxRate;}
        set { m_taxRate = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
        [XmlElement]
        public decimal Price
        {
        get { return m_price;}
        set { m_price = value; }
        }
      

      public static int FieldsCount
      {
      get { return 13; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    