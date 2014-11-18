
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Servman.Data;
    using Servman.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Servman.Domain
      {

      public partial class BusinessPartner : ICloneable
      {

      #region Store


      #region Save

      public static BusinessPartner Save(BusinessPartner businessPartner, IDbConnection connection)
      {
      	if (!Exists(businessPartner, connection))
      		Insert(businessPartner, connection);
      	else
      		Update(businessPartner, connection);
      	return businessPartner;
      }

      public static BusinessPartner Save(BusinessPartner businessPartner)
      {
      	if (!Exists(businessPartner))
      		Insert(businessPartner);
      	else
      		Update(businessPartner);
      	return businessPartner;
      }

      #endregion


      #region Insert

      private const String SqlInsert = "Insert Into BusinessPartner ( " +
      
        " UserId, " +
      
        " CreatedByUserId, " +
      
        " DateCreated, " +
      
        " SalesRepId, " +
      
        " ShowAs, " +
      
        " QbVendorRecordId, " +
      
        " IsActive, " +
      
        " CanLogin " +
      
      ") Values (" +
      
        " ?UserId, " +
      
        " ?CreatedByUserId, " +
      
        " ?DateCreated, " +
      
        " ?SalesRepId, " +
      
        " ?ShowAs, " +
      
        " ?QbVendorRecordId, " +
      
        " ?IsActive, " +
      
        " ?CanLogin " +
      
      ")";

      public static void Insert(BusinessPartner businessPartner, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?UserId", businessPartner.UserId);
      
        Database.PutParameter(dbCommand,"?CreatedByUserId", businessPartner.CreatedByUserId);
      
        Database.PutParameter(dbCommand,"?DateCreated", businessPartner.DateCreated);
      
        Database.PutParameter(dbCommand,"?SalesRepId", businessPartner.SalesRepId);
      
        Database.PutParameter(dbCommand,"?ShowAs", businessPartner.ShowAs);
      
        Database.PutParameter(dbCommand,"?QbVendorRecordId", businessPartner.QbVendorRecordId);
      
        Database.PutParameter(dbCommand,"?IsActive", businessPartner.IsActive);
      
        Database.PutParameter(dbCommand,"?CanLogin", businessPartner.CanLogin);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        businessPartner.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
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
      
        Database.PutParameter(dbCommand,"?UserId", businessPartner.UserId);
      
        Database.PutParameter(dbCommand,"?CreatedByUserId", businessPartner.CreatedByUserId);
      
        Database.PutParameter(dbCommand,"?DateCreated", businessPartner.DateCreated);
      
        Database.PutParameter(dbCommand,"?SalesRepId", businessPartner.SalesRepId);
      
        Database.PutParameter(dbCommand,"?ShowAs", businessPartner.ShowAs);
      
        Database.PutParameter(dbCommand,"?QbVendorRecordId", businessPartner.QbVendorRecordId);
      
        Database.PutParameter(dbCommand,"?IsActive", businessPartner.IsActive);
      
        Database.PutParameter(dbCommand,"?CanLogin", businessPartner.CanLogin);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?UserId",businessPartner.UserId);
      
        Database.UpdateParameter(dbCommand,"?CreatedByUserId",businessPartner.CreatedByUserId);
      
        Database.UpdateParameter(dbCommand,"?DateCreated",businessPartner.DateCreated);
      
        Database.UpdateParameter(dbCommand,"?SalesRepId",businessPartner.SalesRepId);
      
        Database.UpdateParameter(dbCommand,"?ShowAs",businessPartner.ShowAs);
      
        Database.UpdateParameter(dbCommand,"?QbVendorRecordId",businessPartner.QbVendorRecordId);
      
        Database.UpdateParameter(dbCommand,"?IsActive",businessPartner.IsActive);
      
        Database.UpdateParameter(dbCommand,"?CanLogin",businessPartner.CanLogin);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        businessPartner.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
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
      
        + " UserId = ?UserId, "
      
        + " CreatedByUserId = ?CreatedByUserId, "
      
        + " DateCreated = ?DateCreated, "
      
        + " SalesRepId = ?SalesRepId, "
      
        + " ShowAs = ?ShowAs, "
      
        + " QbVendorRecordId = ?QbVendorRecordId, "
      
        + " IsActive = ?IsActive, "
      
        + " CanLogin = ?CanLogin "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(BusinessPartner businessPartner, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", businessPartner.Id);
      
        Database.PutParameter(dbCommand,"?UserId", businessPartner.UserId);
      
        Database.PutParameter(dbCommand,"?CreatedByUserId", businessPartner.CreatedByUserId);
      
        Database.PutParameter(dbCommand,"?DateCreated", businessPartner.DateCreated);
      
        Database.PutParameter(dbCommand,"?SalesRepId", businessPartner.SalesRepId);
      
        Database.PutParameter(dbCommand,"?ShowAs", businessPartner.ShowAs);
      
        Database.PutParameter(dbCommand,"?QbVendorRecordId", businessPartner.QbVendorRecordId);
      
        Database.PutParameter(dbCommand,"?IsActive", businessPartner.IsActive);
      
        Database.PutParameter(dbCommand,"?CanLogin", businessPartner.CanLogin);
      

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

      
        + " Id, "
      
        + " UserId, "
      
        + " CreatedByUserId, "
      
        + " DateCreated, "
      
        + " SalesRepId, "
      
        + " ShowAs, "
      
        + " QbVendorRecordId, "
      
        + " IsActive, "
      
        + " CanLogin "
      

      + " From BusinessPartner "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static BusinessPartner FindByPrimaryKey(
      int id, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("BusinessPartner not found, search by primary key");

      }

      public static BusinessPartner FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(BusinessPartner businessPartner, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",businessPartner.Id);
      

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

      businessPartner.Id = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            businessPartner.UserId = dataReader.GetInt32(1 + offset);
          businessPartner.CreatedByUserId = dataReader.GetInt32(2 + offset);
          businessPartner.DateCreated = dataReader.GetDateTime(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            businessPartner.SalesRepId = dataReader.GetInt32(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            businessPartner.ShowAs = dataReader.GetString(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            businessPartner.QbVendorRecordId = dataReader.GetString(6 + offset);
          businessPartner.IsActive = dataReader.GetBoolean(7 + offset);
          businessPartner.CanLogin = dataReader.GetBoolean(8 + offset);
          

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
        
          + " Id = ?Id "
        
      ;
      public static void Delete(BusinessPartner businessPartner, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", businessPartner.Id);
      


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

      
        + " Id, "
      
        + " UserId, "
      
        + " CreatedByUserId, "
      
        + " DateCreated, "
      
        + " SalesRepId, "
      
        + " ShowAs, "
      
        + " QbVendorRecordId, "
      
        + " IsActive, "
      
        + " CanLogin "
      

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
      
        protected int m_id;
      
        protected int? m_userId;
      
        protected int m_createdByUserId;
      
        protected DateTime m_dateCreated;
      
        protected int? m_salesRepId;
      
        protected String m_showAs;
      
        protected String m_qbVendorRecordId;
      
        protected bool m_isActive;
      
        protected bool m_canLogin;
      
      #endregion

      #region Constructors
      public BusinessPartner(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public BusinessPartner(
        int 
          id,int? 
          userId,int 
          createdByUserId,DateTime 
          dateCreated,int? 
          salesRepId,String 
          showAs,String 
          qbVendorRecordId,bool 
          isActive,bool 
          canLogin
        ) : this()
        {
        
          m_id = id;
        
          m_userId = userId;
        
          m_createdByUserId = createdByUserId;
        
          m_dateCreated = dateCreated;
        
          m_salesRepId = salesRepId;
        
          m_showAs = showAs;
        
          m_qbVendorRecordId = qbVendorRecordId;
        
          m_isActive = isActive;
        
          m_canLogin = canLogin;
        
        }


      
      #endregion

      
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        public int? UserId
        {
        get { return m_userId;}
        set { m_userId = value; }
        }
      
        public int CreatedByUserId
        {
        get { return m_createdByUserId;}
        set { m_createdByUserId = value; }
        }
      
        public DateTime DateCreated
        {
        get { return m_dateCreated;}
        set { m_dateCreated = value; }
        }
      
        public int? SalesRepId
        {
        get { return m_salesRepId;}
        set { m_salesRepId = value; }
        }
      
        public String ShowAs
        {
        get { return m_showAs;}
        set { m_showAs = value; }
        }
      
        public String QbVendorRecordId
        {
        get { return m_qbVendorRecordId;}
        set { m_qbVendorRecordId = value; }
        }
      
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
        }
      
        public bool CanLogin
        {
        get { return m_canLogin;}
        set { m_canLogin = value; }
        }
      

      public static int FieldsCount
      {
      get { return 9; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    