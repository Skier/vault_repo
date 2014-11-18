
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


      public partial class InsuranceCompany : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into InsuranceCompany ( " +
      
        " AddressId, " +
      
        " Name, " +
      
        " ContactPerson, " +
      
        " Phone1, " +
      
        " Phone2 " +
      
      ") Values (" +
      
        " ?AddressId, " +
      
        " ?Name, " +
      
        " ?ContactPerson, " +
      
        " ?Phone1, " +
      
        " ?Phone2 " +
      
      ")";

      public static void Insert(InsuranceCompany insuranceCompany, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?AddressId", insuranceCompany.AddressId);
      
        Database.PutParameter(dbCommand,"?Name", insuranceCompany.Name);
      
        Database.PutParameter(dbCommand,"?ContactPerson", insuranceCompany.ContactPerson);
      
        Database.PutParameter(dbCommand,"?Phone1", insuranceCompany.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", insuranceCompany.Phone2);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        insuranceCompany.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(InsuranceCompany insuranceCompany)
      {
        Insert(insuranceCompany, null);
      }


      public static void Insert(List<InsuranceCompany>  insuranceCompanyList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(InsuranceCompany insuranceCompany in  insuranceCompanyList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?AddressId", insuranceCompany.AddressId);
      
        Database.PutParameter(dbCommand,"?Name", insuranceCompany.Name);
      
        Database.PutParameter(dbCommand,"?ContactPerson", insuranceCompany.ContactPerson);
      
        Database.PutParameter(dbCommand,"?Phone1", insuranceCompany.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", insuranceCompany.Phone2);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?AddressId",insuranceCompany.AddressId);
      
        Database.UpdateParameter(dbCommand,"?Name",insuranceCompany.Name);
      
        Database.UpdateParameter(dbCommand,"?ContactPerson",insuranceCompany.ContactPerson);
      
        Database.UpdateParameter(dbCommand,"?Phone1",insuranceCompany.Phone1);
      
        Database.UpdateParameter(dbCommand,"?Phone2",insuranceCompany.Phone2);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        insuranceCompany.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<InsuranceCompany>  insuranceCompanyList)
      {
        Insert(insuranceCompanyList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update InsuranceCompany Set "
      
        + " AddressId = ?AddressId, "
      
        + " Name = ?Name, "
      
        + " ContactPerson = ?ContactPerson, "
      
        + " Phone1 = ?Phone1, "
      
        + " Phone2 = ?Phone2 "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(InsuranceCompany insuranceCompany, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", insuranceCompany.ID);
      
        Database.PutParameter(dbCommand,"?AddressId", insuranceCompany.AddressId);
      
        Database.PutParameter(dbCommand,"?Name", insuranceCompany.Name);
      
        Database.PutParameter(dbCommand,"?ContactPerson", insuranceCompany.ContactPerson);
      
        Database.PutParameter(dbCommand,"?Phone1", insuranceCompany.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", insuranceCompany.Phone2);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(InsuranceCompany insuranceCompany)
      {
        Update(insuranceCompany, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " AddressId, "
      
        + " Name, "
      
        + " ContactPerson, "
      
        + " Phone1, "
      
        + " Phone2 "
      

      + " From InsuranceCompany "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static InsuranceCompany FindByPrimaryKey(
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
      throw new DataNotFoundException("InsuranceCompany not found, search by primary key");

      }

      public static InsuranceCompany FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(InsuranceCompany insuranceCompany, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",insuranceCompany.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(InsuranceCompany insuranceCompany)
      {
      return Exists(insuranceCompany, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from InsuranceCompany limit 1";

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

      public static InsuranceCompany Load(IDataReader dataReader, int offset)
      {
      InsuranceCompany insuranceCompany = new InsuranceCompany();

      insuranceCompany.ID = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            insuranceCompany.AddressId = dataReader.GetInt32(1 + offset);
          insuranceCompany.Name = dataReader.GetString(2 + offset);
          insuranceCompany.ContactPerson = dataReader.GetString(3 + offset);
          insuranceCompany.Phone1 = dataReader.GetString(4 + offset);
          insuranceCompany.Phone2 = dataReader.GetString(5 + offset);
          

      return insuranceCompany;
      }

      public static InsuranceCompany Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From InsuranceCompany "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(InsuranceCompany insuranceCompany, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", insuranceCompany.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(InsuranceCompany insuranceCompany)
      {
        Delete(insuranceCompany, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From InsuranceCompany ";

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
      
        + " AddressId, "
      
        + " Name, "
      
        + " ContactPerson, "
      
        + " Phone1, "
      
        + " Phone2 "
      

      + " From InsuranceCompany ";
      public static List<InsuranceCompany> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<InsuranceCompany> rv = new List<InsuranceCompany>();

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

      public static List<InsuranceCompany> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<InsuranceCompany> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (InsuranceCompany obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && AddressId == obj.AddressId && Name == obj.Name && ContactPerson == obj.ContactPerson && Phone1 == obj.Phone1 && Phone2 == obj.Phone2;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<InsuranceCompany> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(InsuranceCompany));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(InsuranceCompany item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<InsuranceCompany>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(InsuranceCompany));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<InsuranceCompany> itemsList
      = new List<InsuranceCompany>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is InsuranceCompany)
      itemsList.Add(deserializedObject as InsuranceCompany);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int? m_addressId;
      
        protected String m_name;
      
        protected String m_contactPerson;
      
        protected String m_phone1;
      
        protected String m_phone2;
      
      #endregion

      #region Constructors
      public InsuranceCompany(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public InsuranceCompany(
        int 
          iD,int? 
          addressId,String 
          name,String 
          contactPerson,String 
          phone1,String 
          phone2
        ) : this()
        {
        
          m_iD = iD;
        
          m_addressId = addressId;
        
          m_name = name;
        
          m_contactPerson = contactPerson;
        
          m_phone1 = phone1;
        
          m_phone2 = phone2;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int? AddressId
        {
        get { return m_addressId;}
        set { m_addressId = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        [XmlElement]
        public String ContactPerson
        {
        get { return m_contactPerson;}
        set { m_contactPerson = value; }
        }
      
        [XmlElement]
        public String Phone1
        {
        get { return m_phone1;}
        set { m_phone1 = value; }
        }
      
        [XmlElement]
        public String Phone2
        {
        get { return m_phone2;}
        set { m_phone2 = value; }
        }
      

      public static int FieldsCount
      {
      get { return 6; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    