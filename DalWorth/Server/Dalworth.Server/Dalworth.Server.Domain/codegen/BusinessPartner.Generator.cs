
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


      public partial class BusinessPartner : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into BusinessPartner ( " +
      
        " Name, " +
      
        " QbCustomerTypeListId " +
      
      ") Values (" +
      
        " ?Name, " +
      
        " ?QbCustomerTypeListId " +
      
      ")";

      public static void Insert(BusinessPartner businessPartner, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?Name", businessPartner.Name);
      
        Database.PutParameter(dbCommand,"?QbCustomerTypeListId", businessPartner.QbCustomerTypeListId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        businessPartner.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(BusinessPartner businessPartner)
      {
        Insert(businessPartner, null);
      }


      public static void Insert(List<BusinessPartner>  businessPartnerList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(BusinessPartner businessPartner in  businessPartnerList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?Name", businessPartner.Name);
      
        Database.PutParameter(dbCommand,"?QbCustomerTypeListId", businessPartner.QbCustomerTypeListId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?Name",businessPartner.Name);
      
        Database.UpdateParameter(dbCommand,"?QbCustomerTypeListId",businessPartner.QbCustomerTypeListId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        businessPartner.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<BusinessPartner>  businessPartnerList)
      {
        Insert(businessPartnerList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update BusinessPartner Set "
      
        + " Name = ?Name, "
      
        + " QbCustomerTypeListId = ?QbCustomerTypeListId "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(BusinessPartner businessPartner, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", businessPartner.ID);
      
        Database.PutParameter(dbCommand,"?Name", businessPartner.Name);
      
        Database.PutParameter(dbCommand,"?QbCustomerTypeListId", businessPartner.QbCustomerTypeListId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(BusinessPartner businessPartner)
      {
        Update(businessPartner, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Name, "
      
        + " QbCustomerTypeListId "
      

      + " From BusinessPartner "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static BusinessPartner FindByPrimaryKey(
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
      throw new DataNotFoundException("BusinessPartner not found, search by primary key");

      }

      public static BusinessPartner FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(BusinessPartner businessPartner, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",businessPartner.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(BusinessPartner businessPartner)
      {
      return Exists(businessPartner, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from BusinessPartner limit 1";

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

      public static BusinessPartner Load(IDataReader dataReader, int offset)
      {
      BusinessPartner businessPartner = new BusinessPartner();

      businessPartner.ID = dataReader.GetInt32(0 + offset);
          businessPartner.Name = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            businessPartner.QbCustomerTypeListId = dataReader.GetString(2 + offset);
          

      return businessPartner;
      }

      public static BusinessPartner Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From BusinessPartner "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(BusinessPartner businessPartner, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", businessPartner.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(BusinessPartner businessPartner)
      {
        Delete(businessPartner, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From BusinessPartner ";

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
      
        + " Name, "
      
        + " QbCustomerTypeListId "
      

      + " From BusinessPartner ";
      public static List<BusinessPartner> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<BusinessPartner> rv = new List<BusinessPartner>();

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

      public static List<BusinessPartner> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<BusinessPartner> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (BusinessPartner obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Name == obj.Name && QbCustomerTypeListId == obj.QbCustomerTypeListId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<BusinessPartner> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(BusinessPartner));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(BusinessPartner item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<BusinessPartner>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(BusinessPartner));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<BusinessPartner> itemsList
      = new List<BusinessPartner>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is BusinessPartner)
      itemsList.Add(deserializedObject as BusinessPartner);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_name;
      
        protected String m_qbCustomerTypeListId;
      
      #endregion

      #region Constructors
      public BusinessPartner(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public BusinessPartner(
        int 
          iD,String 
          name,String 
          qbCustomerTypeListId
        ) : this()
        {
        
          m_iD = iD;
        
          m_name = name;
        
          m_qbCustomerTypeListId = qbCustomerTypeListId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        [XmlElement]
        public String QbCustomerTypeListId
        {
        get { return m_qbCustomerTypeListId;}
        set { m_qbCustomerTypeListId = value; }
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

    