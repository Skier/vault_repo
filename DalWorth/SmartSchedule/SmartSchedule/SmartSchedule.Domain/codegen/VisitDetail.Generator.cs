
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using SmartSchedule.Data;
    using SmartSchedule.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace SmartSchedule.Domain
      {

      [DataContract]
      public partial class VisitDetail : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into VisitDetail ( " +
      
        " VisitId, " +
      
        " ServiceId, " +
      
        " ItemSequence, " +
      
        " Note, " +
      
        " Amount " +
      
      ") Values (" +
      
        " ?VisitId, " +
      
        " ?ServiceId, " +
      
        " ?ItemSequence, " +
      
        " ?Note, " +
      
        " ?Amount " +
      
      ")";

      public static void Insert(VisitDetail visitDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?VisitId", visitDetail.VisitId);
      
        Database.PutParameter(dbCommand,"?ServiceId", visitDetail.ServiceId);
      
        Database.PutParameter(dbCommand,"?ItemSequence", visitDetail.ItemSequence);
      
        Database.PutParameter(dbCommand,"?Note", visitDetail.Note);
      
        Database.PutParameter(dbCommand,"?Amount", visitDetail.Amount);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        visitDetail.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(VisitDetail visitDetail)
      {
        Insert(visitDetail, null);
      }


      public static void Insert(List<VisitDetail>  visitDetailList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(VisitDetail visitDetail in  visitDetailList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?VisitId", visitDetail.VisitId);
      
        Database.PutParameter(dbCommand,"?ServiceId", visitDetail.ServiceId);
      
        Database.PutParameter(dbCommand,"?ItemSequence", visitDetail.ItemSequence);
      
        Database.PutParameter(dbCommand,"?Note", visitDetail.Note);
      
        Database.PutParameter(dbCommand,"?Amount", visitDetail.Amount);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?VisitId",visitDetail.VisitId);
      
        Database.UpdateParameter(dbCommand,"?ServiceId",visitDetail.ServiceId);
      
        Database.UpdateParameter(dbCommand,"?ItemSequence",visitDetail.ItemSequence);
      
        Database.UpdateParameter(dbCommand,"?Note",visitDetail.Note);
      
        Database.UpdateParameter(dbCommand,"?Amount",visitDetail.Amount);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        visitDetail.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<VisitDetail>  visitDetailList)
      {
        Insert(visitDetailList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update VisitDetail Set "
      
        + " VisitId = ?VisitId, "
      
        + " ServiceId = ?ServiceId, "
      
        + " ItemSequence = ?ItemSequence, "
      
        + " Note = ?Note, "
      
        + " Amount = ?Amount "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(VisitDetail visitDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", visitDetail.ID);
      
        Database.PutParameter(dbCommand,"?VisitId", visitDetail.VisitId);
      
        Database.PutParameter(dbCommand,"?ServiceId", visitDetail.ServiceId);
      
        Database.PutParameter(dbCommand,"?ItemSequence", visitDetail.ItemSequence);
      
        Database.PutParameter(dbCommand,"?Note", visitDetail.Note);
      
        Database.PutParameter(dbCommand,"?Amount", visitDetail.Amount);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(VisitDetail visitDetail)
      {
        Update(visitDetail, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " VisitId, "
      
        + " ServiceId, "
      
        + " ItemSequence, "
      
        + " Note, "
      
        + " Amount "
      

      + " From VisitDetail "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static VisitDetail FindByPrimaryKey(
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
      throw new DataNotFoundException("VisitDetail not found, search by primary key");

      }

      public static VisitDetail FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(VisitDetail visitDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",visitDetail.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(VisitDetail visitDetail)
      {
      return Exists(visitDetail, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from VisitDetail limit 1";

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

      public static VisitDetail Load(IDataReader dataReader, int offset)
      {
      VisitDetail visitDetail = new VisitDetail();

      visitDetail.ID = dataReader.GetInt32(0 + offset);
          visitDetail.VisitId = dataReader.GetInt32(1 + offset);
          visitDetail.ServiceId = dataReader.GetInt32(2 + offset);
          visitDetail.ItemSequence = dataReader.GetInt32(3 + offset);
          visitDetail.Note = dataReader.GetString(4 + offset);
          visitDetail.Amount = dataReader.GetDecimal(5 + offset);
          

      return visitDetail;
      }

      public static VisitDetail Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From VisitDetail "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(VisitDetail visitDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", visitDetail.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(VisitDetail visitDetail)
      {
        Delete(visitDetail, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From VisitDetail ";

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
      
        + " VisitId, "
      
        + " ServiceId, "
      
        + " ItemSequence, "
      
        + " Note, "
      
        + " Amount "
      

      + " From VisitDetail ";
      public static List<VisitDetail> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<VisitDetail> rv = new List<VisitDetail>();

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

      public static List<VisitDetail> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<VisitDetail> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<VisitDetail> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(VisitDetail));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(VisitDetail item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<VisitDetail>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(VisitDetail));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<VisitDetail> itemsList
      = new List<VisitDetail>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is VisitDetail)
      itemsList.Add(deserializedObject as VisitDetail);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_visitId;
      
        protected int m_serviceId;
      
        protected int m_itemSequence;
      
        protected String m_note;
      
        protected decimal m_amount;
      
      #endregion

      #region Constructors
      public VisitDetail(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public VisitDetail(
        int 
          iD,int 
          visitId,int 
          serviceId,int 
          itemSequence,String 
          note,decimal 
          amount
        ) : this()
        {
        
          m_iD = iD;
        
          m_visitId = visitId;
        
          m_serviceId = serviceId;
        
          m_itemSequence = itemSequence;
        
          m_note = note;
        
          m_amount = amount;
        
        }


      
      #endregion

      
        [DataMember]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [DataMember]
        public int VisitId
        {
        get { return m_visitId;}
        set { m_visitId = value; }
        }
      
        [DataMember]
        public int ServiceId
        {
        get { return m_serviceId;}
        set { m_serviceId = value; }
        }
      
        [DataMember]
        public int ItemSequence
        {
        get { return m_itemSequence;}
        set { m_itemSequence = value; }
        }
      
        [DataMember]
        public String Note
        {
        get { return m_note;}
        set { m_note = value; }
        }
      
        [DataMember]
        public decimal Amount
        {
        get { return m_amount;}
        set { m_amount = value; }
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

    