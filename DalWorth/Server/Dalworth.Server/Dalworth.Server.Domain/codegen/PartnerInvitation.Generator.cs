
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


      public partial class PartnerInvitation : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into PartnerInvitation ( " +
      
        " InvitationKey, " +
      
        " OrderSourceId, " +
      
        " WebUserId, " +
      
        " Email, " +
      
        " ExpirationDate, " +
      
        " IsInvitationSent " +
      
      ") Values (" +
      
        " ?InvitationKey, " +
      
        " ?OrderSourceId, " +
      
        " ?WebUserId, " +
      
        " ?Email, " +
      
        " ?ExpirationDate, " +
      
        " ?IsInvitationSent " +
      
      ")";

      public static void Insert(PartnerInvitation partnerInvitation, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?InvitationKey", partnerInvitation.InvitationKey);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", partnerInvitation.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?WebUserId", partnerInvitation.WebUserId);
      
        Database.PutParameter(dbCommand,"?Email", partnerInvitation.Email);
      
        Database.PutParameter(dbCommand,"?ExpirationDate", partnerInvitation.ExpirationDate);
      
        Database.PutParameter(dbCommand,"?IsInvitationSent", partnerInvitation.IsInvitationSent);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(PartnerInvitation partnerInvitation)
      {
        Insert(partnerInvitation, null);
      }


      public static void Insert(List<PartnerInvitation>  partnerInvitationList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(PartnerInvitation partnerInvitation in  partnerInvitationList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?InvitationKey", partnerInvitation.InvitationKey);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", partnerInvitation.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?WebUserId", partnerInvitation.WebUserId);
      
        Database.PutParameter(dbCommand,"?Email", partnerInvitation.Email);
      
        Database.PutParameter(dbCommand,"?ExpirationDate", partnerInvitation.ExpirationDate);
      
        Database.PutParameter(dbCommand,"?IsInvitationSent", partnerInvitation.IsInvitationSent);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?InvitationKey",partnerInvitation.InvitationKey);
      
        Database.UpdateParameter(dbCommand,"?OrderSourceId",partnerInvitation.OrderSourceId);
      
        Database.UpdateParameter(dbCommand,"?WebUserId",partnerInvitation.WebUserId);
      
        Database.UpdateParameter(dbCommand,"?Email",partnerInvitation.Email);
      
        Database.UpdateParameter(dbCommand,"?ExpirationDate",partnerInvitation.ExpirationDate);
      
        Database.UpdateParameter(dbCommand,"?IsInvitationSent",partnerInvitation.IsInvitationSent);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<PartnerInvitation>  partnerInvitationList)
      {
        Insert(partnerInvitationList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update PartnerInvitation Set "
      
        + " OrderSourceId = ?OrderSourceId, "
      
        + " WebUserId = ?WebUserId, "
      
        + " Email = ?Email, "
      
        + " ExpirationDate = ?ExpirationDate, "
      
        + " IsInvitationSent = ?IsInvitationSent "
      
        + " Where "
        
          + " InvitationKey = ?InvitationKey "
        
      ;

      public static void Update(PartnerInvitation partnerInvitation, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?InvitationKey", partnerInvitation.InvitationKey);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", partnerInvitation.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?WebUserId", partnerInvitation.WebUserId);
      
        Database.PutParameter(dbCommand,"?Email", partnerInvitation.Email);
      
        Database.PutParameter(dbCommand,"?ExpirationDate", partnerInvitation.ExpirationDate);
      
        Database.PutParameter(dbCommand,"?IsInvitationSent", partnerInvitation.IsInvitationSent);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(PartnerInvitation partnerInvitation)
      {
        Update(partnerInvitation, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " InvitationKey, "
      
        + " OrderSourceId, "
      
        + " WebUserId, "
      
        + " Email, "
      
        + " ExpirationDate, "
      
        + " IsInvitationSent "
      

      + " From PartnerInvitation "

      
        + " Where "
        
          + " InvitationKey = ?InvitationKey "
        
      ;

      public static PartnerInvitation FindByPrimaryKey(
      String invitationKey, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?InvitationKey", invitationKey);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("PartnerInvitation not found, search by primary key");

      }

      public static PartnerInvitation FindByPrimaryKey(
      String invitationKey
      )
      {
      return FindByPrimaryKey(
      invitationKey, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(PartnerInvitation partnerInvitation, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?InvitationKey",partnerInvitation.InvitationKey);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(PartnerInvitation partnerInvitation)
      {
      return Exists(partnerInvitation, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from PartnerInvitation limit 1";

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

      public static PartnerInvitation Load(IDataReader dataReader, int offset)
      {
      PartnerInvitation partnerInvitation = new PartnerInvitation();

      partnerInvitation.InvitationKey = dataReader.GetString(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            partnerInvitation.OrderSourceId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            partnerInvitation.WebUserId = dataReader.GetInt32(2 + offset);
          partnerInvitation.Email = dataReader.GetString(3 + offset);
          partnerInvitation.ExpirationDate = dataReader.GetDateTime(4 + offset);
          partnerInvitation.IsInvitationSent = dataReader.GetBoolean(5 + offset);
          

      return partnerInvitation;
      }

      public static PartnerInvitation Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From PartnerInvitation "

      
        + " Where "
        
          + " InvitationKey = ?InvitationKey "
        
      ;
      public static void Delete(PartnerInvitation partnerInvitation, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?InvitationKey", partnerInvitation.InvitationKey);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(PartnerInvitation partnerInvitation)
      {
        Delete(partnerInvitation, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From PartnerInvitation ";

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

      
        + " InvitationKey, "
      
        + " OrderSourceId, "
      
        + " WebUserId, "
      
        + " Email, "
      
        + " ExpirationDate, "
      
        + " IsInvitationSent "
      

      + " From PartnerInvitation ";
      public static List<PartnerInvitation> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<PartnerInvitation> rv = new List<PartnerInvitation>();

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

      public static List<PartnerInvitation> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<PartnerInvitation> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<PartnerInvitation> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PartnerInvitation));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(PartnerInvitation item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<PartnerInvitation>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PartnerInvitation));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<PartnerInvitation> itemsList
      = new List<PartnerInvitation>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is PartnerInvitation)
      itemsList.Add(deserializedObject as PartnerInvitation);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_invitationKey;
      
        protected int? m_orderSourceId;
      
        protected int? m_webUserId;
      
        protected String m_email;
      
        protected DateTime m_expirationDate;
      
        protected bool m_isInvitationSent;
      
      #endregion

      #region Constructors
      public PartnerInvitation(
      String 
          invitationKey
      ) : this()
      {
      
        m_invitationKey = invitationKey;
      
      }

      


        public PartnerInvitation(
        String 
          invitationKey,int? 
          orderSourceId,int? 
          webUserId,String 
          email,DateTime 
          expirationDate,bool 
          isInvitationSent
        ) : this()
        {
        
          m_invitationKey = invitationKey;
        
          m_orderSourceId = orderSourceId;
        
          m_webUserId = webUserId;
        
          m_email = email;
        
          m_expirationDate = expirationDate;
        
          m_isInvitationSent = isInvitationSent;
        
        }


      
      #endregion

      
        [XmlElement]
        public String InvitationKey
        {
        get { return m_invitationKey;}
        set { m_invitationKey = value; }
        }
      
        [XmlElement]
        public int? OrderSourceId
        {
        get { return m_orderSourceId;}
        set { m_orderSourceId = value; }
        }
      
        [XmlElement]
        public int? WebUserId
        {
        get { return m_webUserId;}
        set { m_webUserId = value; }
        }
      
        [XmlElement]
        public String Email
        {
        get { return m_email;}
        set { m_email = value; }
        }
      
        [XmlElement]
        public DateTime ExpirationDate
        {
        get { return m_expirationDate;}
        set { m_expirationDate = value; }
        }
      
        [XmlElement]
        public bool IsInvitationSent
        {
        get { return m_isInvitationSent;}
        set { m_isInvitationSent = value; }
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

    