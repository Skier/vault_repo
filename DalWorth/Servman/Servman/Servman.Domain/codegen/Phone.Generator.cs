
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

      public partial class Phone : ICloneable
      {

      #region Store


      #region Save

      public static Phone Save(Phone phone, IDbConnection connection)
      {
      	if (!Exists(phone, connection))
      		Insert(phone, connection);
      	else
      		Update(phone, connection);
      	return phone;
      }

      public static Phone Save(Phone phone)
      {
      	if (!Exists(phone))
      		Insert(phone);
      	else
      		Update(phone);
      	return phone;
      }

      #endregion


      #region Insert

      private const String SqlInsert = "Insert Into Phone ( " +
      
        " Number, " +
      
        " TwilioId, " +
      
        " IncomingCallUrl, " +
      
        " IncomingSmsUrl, " +
      
        " Description, " +
      
        " IsTollFree, " +
      
        " IsSuspended, " +
      
        " IsRemoved " +
      
      ") Values (" +
      
        " ?Number, " +
      
        " ?TwilioId, " +
      
        " ?IncomingCallUrl, " +
      
        " ?IncomingSmsUrl, " +
      
        " ?Description, " +
      
        " ?IsTollFree, " +
      
        " ?IsSuspended, " +
      
        " ?IsRemoved " +
      
      ")";

      public static void Insert(Phone phone, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?Number", phone.Number);
      
        Database.PutParameter(dbCommand,"?TwilioId", phone.TwilioId);
      
        Database.PutParameter(dbCommand,"?IncomingCallUrl", phone.IncomingCallUrl);
      
        Database.PutParameter(dbCommand,"?IncomingSmsUrl", phone.IncomingSmsUrl);
      
        Database.PutParameter(dbCommand,"?Description", phone.Description);
      
        Database.PutParameter(dbCommand,"?IsTollFree", phone.IsTollFree);
      
        Database.PutParameter(dbCommand,"?IsSuspended", phone.IsSuspended);
      
        Database.PutParameter(dbCommand,"?IsRemoved", phone.IsRemoved);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        phone.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Phone phone)
      {
        Insert(phone, null);
      }


      public static void Insert(List<Phone>  phoneList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Phone phone in  phoneList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?Number", phone.Number);
      
        Database.PutParameter(dbCommand,"?TwilioId", phone.TwilioId);
      
        Database.PutParameter(dbCommand,"?IncomingCallUrl", phone.IncomingCallUrl);
      
        Database.PutParameter(dbCommand,"?IncomingSmsUrl", phone.IncomingSmsUrl);
      
        Database.PutParameter(dbCommand,"?Description", phone.Description);
      
        Database.PutParameter(dbCommand,"?IsTollFree", phone.IsTollFree);
      
        Database.PutParameter(dbCommand,"?IsSuspended", phone.IsSuspended);
      
        Database.PutParameter(dbCommand,"?IsRemoved", phone.IsRemoved);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?Number",phone.Number);
      
        Database.UpdateParameter(dbCommand,"?TwilioId",phone.TwilioId);
      
        Database.UpdateParameter(dbCommand,"?IncomingCallUrl",phone.IncomingCallUrl);
      
        Database.UpdateParameter(dbCommand,"?IncomingSmsUrl",phone.IncomingSmsUrl);
      
        Database.UpdateParameter(dbCommand,"?Description",phone.Description);
      
        Database.UpdateParameter(dbCommand,"?IsTollFree",phone.IsTollFree);
      
        Database.UpdateParameter(dbCommand,"?IsSuspended",phone.IsSuspended);
      
        Database.UpdateParameter(dbCommand,"?IsRemoved",phone.IsRemoved);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        phone.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Phone>  phoneList)
      {
        Insert(phoneList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Phone Set "
      
        + " Number = ?Number, "
      
        + " TwilioId = ?TwilioId, "
      
        + " IncomingCallUrl = ?IncomingCallUrl, "
      
        + " IncomingSmsUrl = ?IncomingSmsUrl, "
      
        + " Description = ?Description, "
      
        + " IsTollFree = ?IsTollFree, "
      
        + " IsSuspended = ?IsSuspended, "
      
        + " IsRemoved = ?IsRemoved "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(Phone phone, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", phone.Id);
      
        Database.PutParameter(dbCommand,"?Number", phone.Number);
      
        Database.PutParameter(dbCommand,"?TwilioId", phone.TwilioId);
      
        Database.PutParameter(dbCommand,"?IncomingCallUrl", phone.IncomingCallUrl);
      
        Database.PutParameter(dbCommand,"?IncomingSmsUrl", phone.IncomingSmsUrl);
      
        Database.PutParameter(dbCommand,"?Description", phone.Description);
      
        Database.PutParameter(dbCommand,"?IsTollFree", phone.IsTollFree);
      
        Database.PutParameter(dbCommand,"?IsSuspended", phone.IsSuspended);
      
        Database.PutParameter(dbCommand,"?IsRemoved", phone.IsRemoved);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Phone phone)
      {
        Update(phone, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Id, "
      
        + " Number, "
      
        + " TwilioId, "
      
        + " IncomingCallUrl, "
      
        + " IncomingSmsUrl, "
      
        + " Description, "
      
        + " IsTollFree, "
      
        + " IsSuspended, "
      
        + " IsRemoved "
      

      + " From Phone "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static Phone FindByPrimaryKey(
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
      throw new DataNotFoundException("Phone not found, search by primary key");

      }

      public static Phone FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Phone phone, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",phone.Id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Phone phone)
      {
      return Exists(phone, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Phone limit 1";

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

      public static Phone Load(IDataReader dataReader, int offset)
      {
      Phone phone = new Phone();

      phone.Id = dataReader.GetInt32(0 + offset);
          phone.Number = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            phone.TwilioId = dataReader.GetString(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            phone.IncomingCallUrl = dataReader.GetString(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            phone.IncomingSmsUrl = dataReader.GetString(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            phone.Description = dataReader.GetString(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            phone.IsTollFree = dataReader.GetBoolean(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            phone.IsSuspended = dataReader.GetBoolean(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            phone.IsRemoved = dataReader.GetBoolean(8 + offset);
          

      return phone;
      }

      public static Phone Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Phone "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;
      public static void Delete(Phone phone, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", phone.Id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Phone phone)
      {
        Delete(phone, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Phone ";

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
      
        + " Number, "
      
        + " TwilioId, "
      
        + " IncomingCallUrl, "
      
        + " IncomingSmsUrl, "
      
        + " Description, "
      
        + " IsTollFree, "
      
        + " IsSuspended, "
      
        + " IsRemoved "
      

      + " From Phone ";
      public static List<Phone> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Phone> rv = new List<Phone>();

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

      public static List<Phone> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Phone> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Phone> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Phone));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Phone item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Phone>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Phone));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Phone> itemsList
      = new List<Phone>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Phone)
      itemsList.Add(deserializedObject as Phone);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_id;
      
        protected String m_number;
      
        protected String m_twilioId;
      
        protected String m_incomingCallUrl;
      
        protected String m_incomingSmsUrl;
      
        protected String m_description;
      
        protected bool m_isTollFree;
      
        protected bool m_isSuspended;
      
        protected bool m_isRemoved;
      
      #endregion

      #region Constructors
      public Phone(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public Phone(
        int 
          id,String 
          number,String 
          twilioId,String 
          incomingCallUrl,String 
          incomingSmsUrl,String 
          description,bool 
          isTollFree,bool 
          isSuspended,bool 
          isRemoved
        ) : this()
        {
        
          m_id = id;
        
          m_number = number;
        
          m_twilioId = twilioId;
        
          m_incomingCallUrl = incomingCallUrl;
        
          m_incomingSmsUrl = incomingSmsUrl;
        
          m_description = description;
        
          m_isTollFree = isTollFree;
        
          m_isSuspended = isSuspended;
        
          m_isRemoved = isRemoved;
        
        }


      
      #endregion

      
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        public String Number
        {
        get { return m_number;}
        set { m_number = value; }
        }
      
        public String TwilioId
        {
        get { return m_twilioId;}
        set { m_twilioId = value; }
        }
      
        public String IncomingCallUrl
        {
        get { return m_incomingCallUrl;}
        set { m_incomingCallUrl = value; }
        }
      
        public String IncomingSmsUrl
        {
        get { return m_incomingSmsUrl;}
        set { m_incomingSmsUrl = value; }
        }
      
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
        public bool IsTollFree
        {
        get { return m_isTollFree;}
        set { m_isTollFree = value; }
        }
      
        public bool IsSuspended
        {
        get { return m_isSuspended;}
        set { m_isSuspended = value; }
        }
      
        public bool IsRemoved
        {
        get { return m_isRemoved;}
        set { m_isRemoved = value; }
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

    