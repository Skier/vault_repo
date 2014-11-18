
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

      public partial class PhoneToBusinessPartner : ICloneable
      {

      #region Store


      #region Save

      public static PhoneToBusinessPartner Save(PhoneToBusinessPartner phoneToBusinessPartner, IDbConnection connection)
      {
      	if (!Exists(phoneToBusinessPartner, connection))
      		Insert(phoneToBusinessPartner, connection);
      	else
      		Update(phoneToBusinessPartner, connection);
      	return phoneToBusinessPartner;
      }

      public static PhoneToBusinessPartner Save(PhoneToBusinessPartner phoneToBusinessPartner)
      {
      	if (!Exists(phoneToBusinessPartner))
      		Insert(phoneToBusinessPartner);
      	else
      		Update(phoneToBusinessPartner);
      	return phoneToBusinessPartner;
      }

      #endregion


      #region Insert

      private const String SqlInsert = "Insert Into PhoneToBusinessPartner ( " +
      
        " PhoneId, " +
      
        " BusinessPartnerId, " +
      
        " Notes, " +
      
        " IsIncoming " +
      
      ") Values (" +
      
        " ?PhoneId, " +
      
        " ?BusinessPartnerId, " +
      
        " ?Notes, " +
      
        " ?IsIncoming " +
      
      ")";

      public static void Insert(PhoneToBusinessPartner phoneToBusinessPartner, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?PhoneId", phoneToBusinessPartner.PhoneId);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", phoneToBusinessPartner.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?Notes", phoneToBusinessPartner.Notes);
      
        Database.PutParameter(dbCommand,"?IsIncoming", phoneToBusinessPartner.IsIncoming);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(PhoneToBusinessPartner phoneToBusinessPartner)
      {
        Insert(phoneToBusinessPartner, null);
      }


      public static void Insert(List<PhoneToBusinessPartner>  phoneToBusinessPartnerList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(PhoneToBusinessPartner phoneToBusinessPartner in  phoneToBusinessPartnerList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?PhoneId", phoneToBusinessPartner.PhoneId);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", phoneToBusinessPartner.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?Notes", phoneToBusinessPartner.Notes);
      
        Database.PutParameter(dbCommand,"?IsIncoming", phoneToBusinessPartner.IsIncoming);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?PhoneId",phoneToBusinessPartner.PhoneId);
      
        Database.UpdateParameter(dbCommand,"?BusinessPartnerId",phoneToBusinessPartner.BusinessPartnerId);
      
        Database.UpdateParameter(dbCommand,"?Notes",phoneToBusinessPartner.Notes);
      
        Database.UpdateParameter(dbCommand,"?IsIncoming",phoneToBusinessPartner.IsIncoming);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<PhoneToBusinessPartner>  phoneToBusinessPartnerList)
      {
        Insert(phoneToBusinessPartnerList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update PhoneToBusinessPartner Set "
      
        + " Notes = ?Notes, "
      
        + " IsIncoming = ?IsIncoming "
      
        + " Where "
        
          + " PhoneId = ?PhoneId and  "
        
          + " BusinessPartnerId = ?BusinessPartnerId "
        
      ;

      public static void Update(PhoneToBusinessPartner phoneToBusinessPartner, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?PhoneId", phoneToBusinessPartner.PhoneId);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", phoneToBusinessPartner.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?Notes", phoneToBusinessPartner.Notes);
      
        Database.PutParameter(dbCommand,"?IsIncoming", phoneToBusinessPartner.IsIncoming);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(PhoneToBusinessPartner phoneToBusinessPartner)
      {
        Update(phoneToBusinessPartner, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " PhoneId, "
      
        + " BusinessPartnerId, "
      
        + " Notes, "
      
        + " IsIncoming "
      

      + " From PhoneToBusinessPartner "

      
        + " Where "
        
          + " PhoneId = ?PhoneId and  "
        
          + " BusinessPartnerId = ?BusinessPartnerId "
        
      ;

      public static PhoneToBusinessPartner FindByPrimaryKey(
      int phoneId,int businessPartnerId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?PhoneId", phoneId);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", businessPartnerId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("PhoneToBusinessPartner not found, search by primary key");

      }

      public static PhoneToBusinessPartner FindByPrimaryKey(
      int phoneId,int businessPartnerId
      )
      {
      return FindByPrimaryKey(
      phoneId,businessPartnerId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(PhoneToBusinessPartner phoneToBusinessPartner, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?PhoneId",phoneToBusinessPartner.PhoneId);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId",phoneToBusinessPartner.BusinessPartnerId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(PhoneToBusinessPartner phoneToBusinessPartner)
      {
      return Exists(phoneToBusinessPartner, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from PhoneToBusinessPartner limit 1";

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

      public static PhoneToBusinessPartner Load(IDataReader dataReader, int offset)
      {
      PhoneToBusinessPartner phoneToBusinessPartner = new PhoneToBusinessPartner();

      phoneToBusinessPartner.PhoneId = dataReader.GetInt32(0 + offset);
          phoneToBusinessPartner.BusinessPartnerId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            phoneToBusinessPartner.Notes = dataReader.GetString(2 + offset);
          phoneToBusinessPartner.IsIncoming = dataReader.GetBoolean(3 + offset);
          

      return phoneToBusinessPartner;
      }

      public static PhoneToBusinessPartner Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From PhoneToBusinessPartner "

      
        + " Where "
        
          + " PhoneId = ?PhoneId and  "
        
          + " BusinessPartnerId = ?BusinessPartnerId "
        
      ;
      public static void Delete(PhoneToBusinessPartner phoneToBusinessPartner, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?PhoneId", phoneToBusinessPartner.PhoneId);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", phoneToBusinessPartner.BusinessPartnerId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(PhoneToBusinessPartner phoneToBusinessPartner)
      {
        Delete(phoneToBusinessPartner, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From PhoneToBusinessPartner ";

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

      
        + " PhoneId, "
      
        + " BusinessPartnerId, "
      
        + " Notes, "
      
        + " IsIncoming "
      

      + " From PhoneToBusinessPartner ";
      public static List<PhoneToBusinessPartner> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<PhoneToBusinessPartner> rv = new List<PhoneToBusinessPartner>();

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

      public static List<PhoneToBusinessPartner> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<PhoneToBusinessPartner> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<PhoneToBusinessPartner> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PhoneToBusinessPartner));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(PhoneToBusinessPartner item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<PhoneToBusinessPartner>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PhoneToBusinessPartner));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<PhoneToBusinessPartner> itemsList
      = new List<PhoneToBusinessPartner>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is PhoneToBusinessPartner)
      itemsList.Add(deserializedObject as PhoneToBusinessPartner);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_phoneId;
      
        protected int m_businessPartnerId;
      
        protected String m_notes;
      
        protected bool m_isIncoming;
      
      #endregion

      #region Constructors
      public PhoneToBusinessPartner(
      int 
          phoneId,int 
          businessPartnerId
      ) : this()
      {
      
        m_phoneId = phoneId;
      
        m_businessPartnerId = businessPartnerId;
      
      }

      


        public PhoneToBusinessPartner(
        int 
          phoneId,int 
          businessPartnerId,String 
          notes,bool 
          isIncoming
        ) : this()
        {
        
          m_phoneId = phoneId;
        
          m_businessPartnerId = businessPartnerId;
        
          m_notes = notes;
        
          m_isIncoming = isIncoming;
        
        }


      
      #endregion

      
        public int PhoneId
        {
        get { return m_phoneId;}
        set { m_phoneId = value; }
        }
      
        public int BusinessPartnerId
        {
        get { return m_businessPartnerId;}
        set { m_businessPartnerId = value; }
        }
      
        public String Notes
        {
        get { return m_notes;}
        set { m_notes = value; }
        }
      
        public bool IsIncoming
        {
        get { return m_isIncoming;}
        set { m_isIncoming = value; }
        }
      

      public static int FieldsCount
      {
      get { return 4; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    