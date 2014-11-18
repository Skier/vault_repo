
    using System;
    using System.Data;
    using System.Collections.Generic;
    using TractInc.Server.Data;
    using TractInc.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace TractInc.Server.Domain
      {


      public partial class BillItemType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [BillItemType] ( " +
      
        " TypeName, " +
      
        " InvoiceItemTypeId " +
      
      ") Values (" +
      
        " @TypeName, " +
      
        " @InvoiceItemTypeId " +
      
      ")";

      public static void Insert(BillItemType billItemType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@TypeName", billItemType.TypeName);
      
        Database.PutParameter(dbCommand,"@InvoiceItemTypeId", billItemType.InvoiceItemTypeId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          billItemType.BillItemTypeId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<BillItemType>  billItemTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(BillItemType billItemType in  billItemTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@TypeName", billItemType.TypeName);
      
        Database.PutParameter(dbCommand,"@InvoiceItemTypeId", billItemType.InvoiceItemTypeId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@TypeName",billItemType.TypeName);
      
        Database.UpdateParameter(dbCommand,"@InvoiceItemTypeId",billItemType.InvoiceItemTypeId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        billItemType.BillItemTypeId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [BillItemType] Set "
      
        + " TypeName = @TypeName, "
      
        + " InvoiceItemTypeId = @InvoiceItemTypeId "
      
        + " Where "
        
          + " BillItemTypeId = @BillItemTypeId "
        
      ;

      public static void Update(BillItemType billItemType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@BillItemTypeId", billItemType.BillItemTypeId);
      
        Database.PutParameter(dbCommand,"@TypeName", billItemType.TypeName);
      
        Database.PutParameter(dbCommand,"@InvoiceItemTypeId", billItemType.InvoiceItemTypeId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " BillItemTypeId, "
      
        + " TypeName, "
      
        + " InvoiceItemTypeId "
      

      + " From [BillItemType] "

      
        + " Where "
        
          + " BillItemTypeId = @BillItemTypeId "
        
      ;

      public static BillItemType FindByPrimaryKey(
      int billItemTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@BillItemTypeId", billItemTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("BillItemType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(BillItemType billItemType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@BillItemTypeId",billItemType.BillItemTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData()
      {
      String sql = "select 1 from BillItemType";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static BillItemType Load(IDataReader dataReader)
      {
      BillItemType billItemType = new BillItemType();

      billItemType.BillItemTypeId = dataReader.GetInt32(0);
          billItemType.TypeName = dataReader.GetString(1);
          billItemType.InvoiceItemTypeId = dataReader.GetInt32(2);
          

      return billItemType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [BillItemType] "

      
        + " Where "
        
          + " BillItemTypeId = @BillItemTypeId "
        
      ;
      public static void Delete(BillItemType billItemType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@BillItemTypeId", billItemType.BillItemTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [BillItemType] ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " BillItemTypeId, "
      
        + " TypeName, "
      
        + " InvoiceItemTypeId "
      

      + " From [BillItemType] ";
      public static List<BillItemType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<BillItemType> rv = new List<BillItemType>();

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
      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<BillItemType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<BillItemType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(BillItemType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(BillItemType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<BillItemType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(BillItemType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<BillItemType> itemsList
      = new List<BillItemType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is BillItemType)
      itemsList.Add(deserializedObject as BillItemType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_billItemTypeId;
      
        protected String m_typeName;
      
        protected int m_invoiceItemTypeId;
      
      #endregion

      #region Constructors
      public BillItemType(
      int 
          billItemTypeId
      )
      {
      
        m_billItemTypeId = billItemTypeId;
      
      }

      


        public BillItemType(
        int 
          billItemTypeId,String 
          typeName,int 
          invoiceItemTypeId
        )
        {
        
          m_billItemTypeId = billItemTypeId;
        
          m_typeName = typeName;
        
          m_invoiceItemTypeId = invoiceItemTypeId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int BillItemTypeId
        {
        get { return m_billItemTypeId;}
        set { m_billItemTypeId = value; }
        }
      
        [XmlElement]
        public String TypeName
        {
        get { return m_typeName;}
        set { m_typeName = value; }
        }
      
        [XmlElement]
        public int InvoiceItemTypeId
        {
        get { return m_invoiceItemTypeId;}
        set { m_invoiceItemTypeId = value; }
        }
      
      }
      #endregion
      }

    