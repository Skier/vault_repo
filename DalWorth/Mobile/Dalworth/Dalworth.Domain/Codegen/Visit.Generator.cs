
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Data;
    using Dalworth.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Domain
      {


      public partial class Visit : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Visit] ( " +
      
        " ID, " +
      
        " VisitStatusId, " +
      
        " CreateDate, " +
      
        " ServiceDate, " +
      
        " PreferedTimeFrom, " +
      
        " PreferedTimeTo, " +
      
        " CustomerId, " +
      
        " ServiceAddressId, " +
      
        " Notes " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @VisitStatusId, " +
      
        " @CreateDate, " +
      
        " @ServiceDate, " +
      
        " @PreferedTimeFrom, " +
      
        " @PreferedTimeTo, " +
      
        " @CustomerId, " +
      
        " @ServiceAddressId, " +
      
        " @Notes " +
      
      ")";

      public static void Insert(Visit visit, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", visit.ID);
      
        Database.PutParameter(dbCommand,"@VisitStatusId", visit.VisitStatusId);
      
        Database.PutParameter(dbCommand,"@CreateDate", visit.CreateDate);
      
        Database.PutParameter(dbCommand,"@ServiceDate", visit.ServiceDate);
      
        Database.PutParameter(dbCommand,"@PreferedTimeFrom", visit.PreferedTimeFrom);
      
        Database.PutParameter(dbCommand,"@PreferedTimeTo", visit.PreferedTimeTo);
      
        Database.PutParameter(dbCommand,"@CustomerId", visit.CustomerId);
      
        Database.PutParameter(dbCommand,"@ServiceAddressId", visit.ServiceAddressId);
      
        Database.PutParameter(dbCommand,"@Notes", visit.Notes);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(Visit visit)
      {
        Insert(visit, null);
      }

      public static void Insert(List<Visit>  visitList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(Visit visit in  visitList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", visit.ID);
      
        Database.PutParameter(dbCommand,"@VisitStatusId", visit.VisitStatusId);
      
        Database.PutParameter(dbCommand,"@CreateDate", visit.CreateDate);
      
        Database.PutParameter(dbCommand,"@ServiceDate", visit.ServiceDate);
      
        Database.PutParameter(dbCommand,"@PreferedTimeFrom", visit.PreferedTimeFrom);
      
        Database.PutParameter(dbCommand,"@PreferedTimeTo", visit.PreferedTimeTo);
      
        Database.PutParameter(dbCommand,"@CustomerId", visit.CustomerId);
      
        Database.PutParameter(dbCommand,"@ServiceAddressId", visit.ServiceAddressId);
      
        Database.PutParameter(dbCommand,"@Notes", visit.Notes);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",visit.ID);
      
        Database.UpdateParameter(dbCommand,"@VisitStatusId",visit.VisitStatusId);
      
        Database.UpdateParameter(dbCommand,"@CreateDate",visit.CreateDate);
      
        Database.UpdateParameter(dbCommand,"@ServiceDate",visit.ServiceDate);
      
        Database.UpdateParameter(dbCommand,"@PreferedTimeFrom",visit.PreferedTimeFrom);
      
        Database.UpdateParameter(dbCommand,"@PreferedTimeTo",visit.PreferedTimeTo);
      
        Database.UpdateParameter(dbCommand,"@CustomerId",visit.CustomerId);
      
        Database.UpdateParameter(dbCommand,"@ServiceAddressId",visit.ServiceAddressId);
      
        Database.UpdateParameter(dbCommand,"@Notes",visit.Notes);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<Visit>  visitList)
      {
      Insert(visitList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Visit] Set "
      
        + " VisitStatusId = @VisitStatusId, "
      
        + " CreateDate = @CreateDate, "
      
        + " ServiceDate = @ServiceDate, "
      
        + " PreferedTimeFrom = @PreferedTimeFrom, "
      
        + " PreferedTimeTo = @PreferedTimeTo, "
      
        + " CustomerId = @CustomerId, "
      
        + " ServiceAddressId = @ServiceAddressId, "
      
        + " Notes = @Notes "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(Visit visit, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", visit.ID);
      
        Database.PutParameter(dbCommand,"@VisitStatusId", visit.VisitStatusId);
      
        Database.PutParameter(dbCommand,"@CreateDate", visit.CreateDate);
      
        Database.PutParameter(dbCommand,"@ServiceDate", visit.ServiceDate);
      
        Database.PutParameter(dbCommand,"@PreferedTimeFrom", visit.PreferedTimeFrom);
      
        Database.PutParameter(dbCommand,"@PreferedTimeTo", visit.PreferedTimeTo);
      
        Database.PutParameter(dbCommand,"@CustomerId", visit.CustomerId);
      
        Database.PutParameter(dbCommand,"@ServiceAddressId", visit.ServiceAddressId);
      
        Database.PutParameter(dbCommand,"@Notes", visit.Notes);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Visit visit)
      {
      Update(visit, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " VisitStatusId, "
      
        + " CreateDate, "
      
        + " ServiceDate, "
      
        + " PreferedTimeFrom, "
      
        + " PreferedTimeTo, "
      
        + " CustomerId, "
      
        + " ServiceAddressId, "
      
        + " Notes "
      

      + " From [Visit] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static Visit FindByPrimaryKey(
      int iD, IDbTransaction transaction
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", iD);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Visit not found, search by primary key");

      }

      public static Visit FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(Visit visit, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",visit.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Visit visit)
      {
      return Exists(visit, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from Visit";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, transaction))
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

      public static Visit Load(IDataReader dataReader)
      {
      Visit visit = new Visit();

      visit.ID = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            visit.VisitStatusId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            visit.CreateDate = dataReader.GetDateTime(2);
          
            if(!dataReader.IsDBNull(3))
            visit.ServiceDate = dataReader.GetDateTime(3);
          
            if(!dataReader.IsDBNull(4))
            visit.PreferedTimeFrom = dataReader.GetDateTime(4);
          
            if(!dataReader.IsDBNull(5))
            visit.PreferedTimeTo = dataReader.GetDateTime(5);
          
            if(!dataReader.IsDBNull(6))
            visit.CustomerId = dataReader.GetInt32(6);
          
            if(!dataReader.IsDBNull(7))
            visit.ServiceAddressId = dataReader.GetInt32(7);
          
            if(!dataReader.IsDBNull(8))
            visit.Notes = dataReader.GetString(8);
          

      return visit;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Visit] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(Visit visit, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", visit.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Visit visit)
      {
      Delete(visit, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Visit] ";

      public static void Clear(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, transaction))
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
      
        + " VisitStatusId, "
      
        + " CreateDate, "
      
        + " ServiceDate, "
      
        + " PreferedTimeFrom, "
      
        + " PreferedTimeTo, "
      
        + " CustomerId, "
      
        + " ServiceAddressId, "
      
        + " Notes "
      

      + " From [Visit] ";
      public static List<Visit> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<Visit> rv = new List<Visit>();

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

      public static List<Visit> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Visit> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Visit> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Visit));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Visit item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Visit>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Visit));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Visit> itemsList
      = new List<Visit>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Visit)
      itemsList.Add(deserializedObject as Visit);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int? m_visitStatusId;
      
        protected DateTime? m_createDate;
      
        protected DateTime? m_serviceDate;
      
        protected DateTime? m_preferedTimeFrom;
      
        protected DateTime? m_preferedTimeTo;
      
        protected int? m_customerId;
      
        protected int? m_serviceAddressId;
      
        protected String m_notes;
      
      #endregion

      #region Constructors
      public Visit(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public Visit(
        int 
          iD,int? 
          visitStatusId,DateTime? 
          createDate,DateTime? 
          serviceDate,DateTime? 
          preferedTimeFrom,DateTime? 
          preferedTimeTo,int? 
          customerId,int? 
          serviceAddressId,String 
          notes
        )
        {
        
          m_iD = iD;
        
          m_visitStatusId = visitStatusId;
        
          m_createDate = createDate;
        
          m_serviceDate = serviceDate;
        
          m_preferedTimeFrom = preferedTimeFrom;
        
          m_preferedTimeTo = preferedTimeTo;
        
          m_customerId = customerId;
        
          m_serviceAddressId = serviceAddressId;
        
          m_notes = notes;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int? VisitStatusId
        {
        get { return m_visitStatusId;}
        set { m_visitStatusId = value; }
        }
      
        [XmlElement]
        public DateTime? CreateDate
        {
        get { return m_createDate;}
        set { m_createDate = value; }
        }
      
        [XmlElement]
        public DateTime? ServiceDate
        {
        get { return m_serviceDate;}
        set { m_serviceDate = value; }
        }
      
        [XmlElement]
        public DateTime? PreferedTimeFrom
        {
        get { return m_preferedTimeFrom;}
        set { m_preferedTimeFrom = value; }
        }
      
        [XmlElement]
        public DateTime? PreferedTimeTo
        {
        get { return m_preferedTimeTo;}
        set { m_preferedTimeTo = value; }
        }
      
        [XmlElement]
        public int? CustomerId
        {
        get { return m_customerId;}
        set { m_customerId = value; }
        }
      
        [XmlElement]
        public int? ServiceAddressId
        {
        get { return m_serviceAddressId;}
        set { m_serviceAddressId = value; }
        }
      
        [XmlElement]
        public String Notes
        {
        get { return m_notes;}
        set { m_notes = value; }
        }
      
      }
      #endregion
      }

    