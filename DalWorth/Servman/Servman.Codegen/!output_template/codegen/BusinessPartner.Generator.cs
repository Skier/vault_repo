
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


      #region Insert

      private const String SqlInsert = "Insert Into BusinessPartner ( " +
      
        " QbUserId, " +
      
        " Name, " +
      
        " Address, " +
      
        " Email, " +
      
        " Phone, " +
      
        " FirstName, " +
      
        " LastName, " +
      
        " CanLogin " +
      
      ") Values (" +
      
        " ?QbUserId, " +
      
        " ?Name, " +
      
        " ?Address, " +
      
        " ?Email, " +
      
        " ?Phone, " +
      
        " ?FirstName, " +
      
        " ?LastName, " +
      
        " ?CanLogin " +
      
      ")";

      public static void Insert(BusinessPartner businessPartner, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?QbUserId", businessPartner.QbUserId);
      
        Database.PutParameter(dbCommand,"?Name", businessPartner.Name);
      
        Database.PutParameter(dbCommand,"?Address", businessPartner.Address);
      
        Database.PutParameter(dbCommand,"?Email", businessPartner.Email);
      
        Database.PutParameter(dbCommand,"?Phone", businessPartner.Phone);
      
        Database.PutParameter(dbCommand,"?FirstName", businessPartner.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", businessPartner.LastName);
      
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
      
        Database.PutParameter(dbCommand,"?QbUserId", businessPartner.QbUserId);
      
        Database.PutParameter(dbCommand,"?Name", businessPartner.Name);
      
        Database.PutParameter(dbCommand,"?Address", businessPartner.Address);
      
        Database.PutParameter(dbCommand,"?Email", businessPartner.Email);
      
        Database.PutParameter(dbCommand,"?Phone", businessPartner.Phone);
      
        Database.PutParameter(dbCommand,"?FirstName", businessPartner.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", businessPartner.LastName);
      
        Database.PutParameter(dbCommand,"?CanLogin", businessPartner.CanLogin);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?QbUserId",businessPartner.QbUserId);
      
        Database.UpdateParameter(dbCommand,"?Name",businessPartner.Name);
      
        Database.UpdateParameter(dbCommand,"?Address",businessPartner.Address);
      
        Database.UpdateParameter(dbCommand,"?Email",businessPartner.Email);
      
        Database.UpdateParameter(dbCommand,"?Phone",businessPartner.Phone);
      
        Database.UpdateParameter(dbCommand,"?FirstName",businessPartner.FirstName);
      
        Database.UpdateParameter(dbCommand,"?LastName",businessPartner.LastName);
      
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
      
        + " QbUserId = ?QbUserId, "
      
        + " Name = ?Name, "
      
        + " Address = ?Address, "
      
        + " Email = ?Email, "
      
        + " Phone = ?Phone, "
      
        + " FirstName = ?FirstName, "
      
        + " LastName = ?LastName, "
      
        + " CanLogin = ?CanLogin "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(BusinessPartner businessPartner, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", businessPartner.Id);
      
        Database.PutParameter(dbCommand,"?QbUserId", businessPartner.QbUserId);
      
        Database.PutParameter(dbCommand,"?Name", businessPartner.Name);
      
        Database.PutParameter(dbCommand,"?Address", businessPartner.Address);
      
        Database.PutParameter(dbCommand,"?Email", businessPartner.Email);
      
        Database.PutParameter(dbCommand,"?Phone", businessPartner.Phone);
      
        Database.PutParameter(dbCommand,"?FirstName", businessPartner.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", businessPartner.LastName);
      
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
      
        + " QbUserId, "
      
        + " Name, "
      
        + " Address, "
      
        + " Email, "
      
        + " Phone, "
      
        + " FirstName, "
      
        + " LastName, "
      
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
            businessPartner.QbUserId = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            businessPartner.Name = dataReader.GetString(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            businessPartner.Address = dataReader.GetString(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            businessPartner.Email = dataReader.GetString(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            businessPartner.Phone = dataReader.GetString(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            businessPartner.FirstName = dataReader.GetString(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            businessPartner.LastName = dataReader.GetString(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
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
      
        + " QbUserId, "
      
        + " Name, "
      
        + " Address, "
      
        + " Email, "
      
        + " Phone, "
      
        + " FirstName, "
      
        + " LastName, "
      
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
      
        protected String m_qbUserId;
      
        protected String m_name;
      
        protected String m_address;
      
        protected String m_email;
      
        protected String m_phone;
      
        protected String m_firstName;
      
        protected String m_lastName;
      
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
          id,String 
          qbUserId,String 
          name,String 
          address,String 
          email,String 
          phone,String 
          firstName,String 
          lastName,bool 
          canLogin
        ) : this()
        {
        
          m_id = id;
        
          m_qbUserId = qbUserId;
        
          m_name = name;
        
          m_address = address;
        
          m_email = email;
        
          m_phone = phone;
        
          m_firstName = firstName;
        
          m_lastName = lastName;
        
          m_canLogin = canLogin;
        
        }


      
      #endregion

      
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        public String QbUserId
        {
        get { return m_qbUserId;}
        set { m_qbUserId = value; }
        }
      
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        public String Address
        {
        get { return m_address;}
        set { m_address = value; }
        }
      
        public String Email
        {
        get { return m_email;}
        set { m_email = value; }
        }
      
        public String Phone
        {
        get { return m_phone;}
        set { m_phone = value; }
        }
      
        public String FirstName
        {
        get { return m_firstName;}
        set { m_firstName = value; }
        }
      
        public String LastName
        {
        get { return m_lastName;}
        set { m_lastName = value; }
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

    