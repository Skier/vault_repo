
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


      public partial class InvoiceItemType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [InvoiceItemType] ( " +
      
        " TypeName, " +
      
        " IsCountable, " +
      
        " IsPresetRate, " +
      
        " IsSingle, " +
      
        " IsAttachRequired " +
      
      ") Values (" +
      
        " @TypeName, " +
      
        " @IsCountable, " +
      
        " @IsPresetRate, " +
      
        " @IsSingle, " +
      
        " @IsAttachRequired " +
      
      ")";

      public static void Insert(InvoiceItemType invoiceItemType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@TypeName", invoiceItemType.TypeName);
      
        Database.PutParameter(dbCommand,"@IsCountable", invoiceItemType.IsCountable);
      
        Database.PutParameter(dbCommand,"@IsPresetRate", invoiceItemType.IsPresetRate);
      
        Database.PutParameter(dbCommand,"@IsSingle", invoiceItemType.IsSingle);
      
        Database.PutParameter(dbCommand,"@IsAttachRequired", invoiceItemType.IsAttachRequired);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          invoiceItemType.InvoiceItemTypeId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<InvoiceItemType>  invoiceItemTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(InvoiceItemType invoiceItemType in  invoiceItemTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@TypeName", invoiceItemType.TypeName);
      
        Database.PutParameter(dbCommand,"@IsCountable", invoiceItemType.IsCountable);
      
        Database.PutParameter(dbCommand,"@IsPresetRate", invoiceItemType.IsPresetRate);
      
        Database.PutParameter(dbCommand,"@IsSingle", invoiceItemType.IsSingle);
      
        Database.PutParameter(dbCommand,"@IsAttachRequired", invoiceItemType.IsAttachRequired);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@TypeName",invoiceItemType.TypeName);
      
        Database.UpdateParameter(dbCommand,"@IsCountable",invoiceItemType.IsCountable);
      
        Database.UpdateParameter(dbCommand,"@IsPresetRate",invoiceItemType.IsPresetRate);
      
        Database.UpdateParameter(dbCommand,"@IsSingle",invoiceItemType.IsSingle);
      
        Database.UpdateParameter(dbCommand,"@IsAttachRequired",invoiceItemType.IsAttachRequired);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        invoiceItemType.InvoiceItemTypeId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [InvoiceItemType] Set "
      
        + " TypeName = @TypeName, "
      
        + " IsCountable = @IsCountable, "
      
        + " IsPresetRate = @IsPresetRate, "
      
        + " IsSingle = @IsSingle, "
      
        + " IsAttachRequired = @IsAttachRequired "
      
        + " Where "
        
          + " InvoiceItemTypeId = @InvoiceItemTypeId "
        
      ;

      public static void Update(InvoiceItemType invoiceItemType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@InvoiceItemTypeId", invoiceItemType.InvoiceItemTypeId);
      
        Database.PutParameter(dbCommand,"@TypeName", invoiceItemType.TypeName);
      
        Database.PutParameter(dbCommand,"@IsCountable", invoiceItemType.IsCountable);
      
        Database.PutParameter(dbCommand,"@IsPresetRate", invoiceItemType.IsPresetRate);
      
        Database.PutParameter(dbCommand,"@IsSingle", invoiceItemType.IsSingle);
      
        Database.PutParameter(dbCommand,"@IsAttachRequired", invoiceItemType.IsAttachRequired);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " InvoiceItemTypeId, "
      
        + " TypeName, "
      
        + " IsCountable, "
      
        + " IsPresetRate, "
      
        + " IsSingle, "
      
        + " IsAttachRequired "
      

      + " From [InvoiceItemType] "

      
        + " Where "
        
          + " InvoiceItemTypeId = @InvoiceItemTypeId "
        
      ;

      public static InvoiceItemType FindByPrimaryKey(
      int invoiceItemTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InvoiceItemTypeId", invoiceItemTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("InvoiceItemType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(InvoiceItemType invoiceItemType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InvoiceItemTypeId",invoiceItemType.InvoiceItemTypeId);
      

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
      String sql = "select 1 from InvoiceItemType";

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

      public static InvoiceItemType Load(IDataReader dataReader)
      {
      InvoiceItemType invoiceItemType = new InvoiceItemType();

      invoiceItemType.InvoiceItemTypeId = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            invoiceItemType.TypeName = dataReader.GetString(1);
          invoiceItemType.IsCountable = dataReader.GetBoolean(2);
          invoiceItemType.IsPresetRate = dataReader.GetBoolean(3);
          invoiceItemType.IsSingle = dataReader.GetBoolean(4);
          invoiceItemType.IsAttachRequired = dataReader.GetBoolean(5);
          

      return invoiceItemType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [InvoiceItemType] "

      
        + " Where "
        
          + " InvoiceItemTypeId = @InvoiceItemTypeId "
        
      ;
      public static void Delete(InvoiceItemType invoiceItemType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@InvoiceItemTypeId", invoiceItemType.InvoiceItemTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [InvoiceItemType] ";

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

      
        + " InvoiceItemTypeId, "
      
        + " TypeName, "
      
        + " IsCountable, "
      
        + " IsPresetRate, "
      
        + " IsSingle, "
      
        + " IsAttachRequired "
      

      + " From [InvoiceItemType] ";
      public static List<InvoiceItemType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<InvoiceItemType> rv = new List<InvoiceItemType>();

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
      List<InvoiceItemType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<InvoiceItemType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(InvoiceItemType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(InvoiceItemType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<InvoiceItemType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(InvoiceItemType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<InvoiceItemType> itemsList
      = new List<InvoiceItemType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is InvoiceItemType)
      itemsList.Add(deserializedObject as InvoiceItemType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_invoiceItemTypeId;
      
        protected String m_typeName;
      
        protected bool m_isCountable;
      
        protected bool m_isPresetRate;
      
        protected bool m_isSingle;
      
        protected bool m_isAttachRequired;
      
      #endregion

      #region Constructors
      public InvoiceItemType(
      int 
          invoiceItemTypeId
      )
      {
      
        m_invoiceItemTypeId = invoiceItemTypeId;
      
      }

      


        public InvoiceItemType(
        int 
          invoiceItemTypeId,String 
          typeName,bool 
          isCountable,bool 
          isPresetRate,bool 
          isSingle,bool 
          isAttachRequired
        )
        {
        
          m_invoiceItemTypeId = invoiceItemTypeId;
        
          m_typeName = typeName;
        
          m_isCountable = isCountable;
        
          m_isPresetRate = isPresetRate;
        
          m_isSingle = isSingle;
        
          m_isAttachRequired = isAttachRequired;
        
        }


      
      #endregion

      
        [XmlElement]
        public int InvoiceItemTypeId
        {
        get { return m_invoiceItemTypeId;}
        set { m_invoiceItemTypeId = value; }
        }
      
        [XmlElement]
        public String TypeName
        {
        get { return m_typeName;}
        set { m_typeName = value; }
        }
      
        [XmlElement]
        public bool IsCountable
        {
        get { return m_isCountable;}
        set { m_isCountable = value; }
        }
      
        [XmlElement]
        public bool IsPresetRate
        {
        get { return m_isPresetRate;}
        set { m_isPresetRate = value; }
        }
      
        [XmlElement]
        public bool IsSingle
        {
        get { return m_isSingle;}
        set { m_isSingle = value; }
        }
      
        [XmlElement]
        public bool IsAttachRequired
        {
        get { return m_isAttachRequired;}
        set { m_isAttachRequired = value; }
        }
      
      }
      #endregion
      }

    