
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


      public partial class Work : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Work] ( " +
      
        " ID, " +
      
        " DispatchEmployeeId, " +
      
        " TechnicianEmployeeId, " +
      
        " VanId, " +
      
        " StartDate, " +
      
        " WorkStatusId, " +
      
        " StartMessage, " +
      
        " EndMessage, " +
      
        " EquipmentNotes " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @DispatchEmployeeId, " +
      
        " @TechnicianEmployeeId, " +
      
        " @VanId, " +
      
        " @StartDate, " +
      
        " @WorkStatusId, " +
      
        " @StartMessage, " +
      
        " @EndMessage, " +
      
        " @EquipmentNotes " +
      
      ")";

      public static void Insert(Work work, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", work.ID);
      
        Database.PutParameter(dbCommand,"@DispatchEmployeeId", work.DispatchEmployeeId);
      
        Database.PutParameter(dbCommand,"@TechnicianEmployeeId", work.TechnicianEmployeeId);
      
        Database.PutParameter(dbCommand,"@VanId", work.VanId);
      
        Database.PutParameter(dbCommand,"@StartDate", work.StartDate);
      
        Database.PutParameter(dbCommand,"@WorkStatusId", work.WorkStatusId);
      
        Database.PutParameter(dbCommand,"@StartMessage", work.StartMessage);
      
        Database.PutParameter(dbCommand,"@EndMessage", work.EndMessage);
      
        Database.PutParameter(dbCommand,"@EquipmentNotes", work.EquipmentNotes);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(Work work)
      {
        Insert(work, null);
      }

      public static void Insert(List<Work>  workList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(Work work in  workList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", work.ID);
      
        Database.PutParameter(dbCommand,"@DispatchEmployeeId", work.DispatchEmployeeId);
      
        Database.PutParameter(dbCommand,"@TechnicianEmployeeId", work.TechnicianEmployeeId);
      
        Database.PutParameter(dbCommand,"@VanId", work.VanId);
      
        Database.PutParameter(dbCommand,"@StartDate", work.StartDate);
      
        Database.PutParameter(dbCommand,"@WorkStatusId", work.WorkStatusId);
      
        Database.PutParameter(dbCommand,"@StartMessage", work.StartMessage);
      
        Database.PutParameter(dbCommand,"@EndMessage", work.EndMessage);
      
        Database.PutParameter(dbCommand,"@EquipmentNotes", work.EquipmentNotes);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",work.ID);
      
        Database.UpdateParameter(dbCommand,"@DispatchEmployeeId",work.DispatchEmployeeId);
      
        Database.UpdateParameter(dbCommand,"@TechnicianEmployeeId",work.TechnicianEmployeeId);
      
        Database.UpdateParameter(dbCommand,"@VanId",work.VanId);
      
        Database.UpdateParameter(dbCommand,"@StartDate",work.StartDate);
      
        Database.UpdateParameter(dbCommand,"@WorkStatusId",work.WorkStatusId);
      
        Database.UpdateParameter(dbCommand,"@StartMessage",work.StartMessage);
      
        Database.UpdateParameter(dbCommand,"@EndMessage",work.EndMessage);
      
        Database.UpdateParameter(dbCommand,"@EquipmentNotes",work.EquipmentNotes);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<Work>  workList)
      {
      Insert(workList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Work] Set "
      
        + " DispatchEmployeeId = @DispatchEmployeeId, "
      
        + " TechnicianEmployeeId = @TechnicianEmployeeId, "
      
        + " VanId = @VanId, "
      
        + " StartDate = @StartDate, "
      
        + " WorkStatusId = @WorkStatusId, "
      
        + " StartMessage = @StartMessage, "
      
        + " EndMessage = @EndMessage, "
      
        + " EquipmentNotes = @EquipmentNotes "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(Work work, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", work.ID);
      
        Database.PutParameter(dbCommand,"@DispatchEmployeeId", work.DispatchEmployeeId);
      
        Database.PutParameter(dbCommand,"@TechnicianEmployeeId", work.TechnicianEmployeeId);
      
        Database.PutParameter(dbCommand,"@VanId", work.VanId);
      
        Database.PutParameter(dbCommand,"@StartDate", work.StartDate);
      
        Database.PutParameter(dbCommand,"@WorkStatusId", work.WorkStatusId);
      
        Database.PutParameter(dbCommand,"@StartMessage", work.StartMessage);
      
        Database.PutParameter(dbCommand,"@EndMessage", work.EndMessage);
      
        Database.PutParameter(dbCommand,"@EquipmentNotes", work.EquipmentNotes);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Work work)
      {
      Update(work, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " DispatchEmployeeId, "
      
        + " TechnicianEmployeeId, "
      
        + " VanId, "
      
        + " StartDate, "
      
        + " WorkStatusId, "
      
        + " StartMessage, "
      
        + " EndMessage, "
      
        + " EquipmentNotes "
      

      + " From [Work] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static Work FindByPrimaryKey(
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
      throw new DataNotFoundException("Work not found, search by primary key");

      }

      public static Work FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(Work work, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",work.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Work work)
      {
      return Exists(work, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from Work";

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

      public static Work Load(IDataReader dataReader)
      {
      Work work = new Work();

      work.ID = dataReader.GetInt32(0);
          work.DispatchEmployeeId = dataReader.GetInt32(1);
          work.TechnicianEmployeeId = dataReader.GetInt32(2);
          work.VanId = dataReader.GetInt32(3);
          
            if(!dataReader.IsDBNull(4))
            work.StartDate = dataReader.GetDateTime(4);
          
            if(!dataReader.IsDBNull(5))
            work.WorkStatusId = dataReader.GetInt32(5);
          
            if(!dataReader.IsDBNull(6))
            work.StartMessage = dataReader.GetString(6);
          
            if(!dataReader.IsDBNull(7))
            work.EndMessage = dataReader.GetString(7);
          
            if(!dataReader.IsDBNull(8))
            work.EquipmentNotes = dataReader.GetString(8);
          

      return work;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Work] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(Work work, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", work.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Work work)
      {
      Delete(work, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Work] ";

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
      
        + " DispatchEmployeeId, "
      
        + " TechnicianEmployeeId, "
      
        + " VanId, "
      
        + " StartDate, "
      
        + " WorkStatusId, "
      
        + " StartMessage, "
      
        + " EndMessage, "
      
        + " EquipmentNotes "
      

      + " From [Work] ";
      public static List<Work> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<Work> rv = new List<Work>();

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

      public static List<Work> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Work> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Work> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Work));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Work item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Work>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Work));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Work> itemsList
      = new List<Work>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Work)
      itemsList.Add(deserializedObject as Work);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_dispatchEmployeeId;
      
        protected int m_technicianEmployeeId;
      
        protected int m_vanId;
      
        protected DateTime? m_startDate;
      
        protected int? m_workStatusId;
      
        protected String m_startMessage;
      
        protected String m_endMessage;
      
        protected String m_equipmentNotes;
      
      #endregion

      #region Constructors
      public Work(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public Work(
        int 
          iD,int 
          dispatchEmployeeId,int 
          technicianEmployeeId,int 
          vanId,DateTime? 
          startDate,int? 
          workStatusId,String 
          startMessage,String 
          endMessage,String 
          equipmentNotes
        )
        {
        
          m_iD = iD;
        
          m_dispatchEmployeeId = dispatchEmployeeId;
        
          m_technicianEmployeeId = technicianEmployeeId;
        
          m_vanId = vanId;
        
          m_startDate = startDate;
        
          m_workStatusId = workStatusId;
        
          m_startMessage = startMessage;
        
          m_endMessage = endMessage;
        
          m_equipmentNotes = equipmentNotes;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int DispatchEmployeeId
        {
        get { return m_dispatchEmployeeId;}
        set { m_dispatchEmployeeId = value; }
        }
      
        [XmlElement]
        public int TechnicianEmployeeId
        {
        get { return m_technicianEmployeeId;}
        set { m_technicianEmployeeId = value; }
        }
      
        [XmlElement]
        public int VanId
        {
        get { return m_vanId;}
        set { m_vanId = value; }
        }
      
        [XmlElement]
        public DateTime? StartDate
        {
        get { return m_startDate;}
        set { m_startDate = value; }
        }
      
        [XmlElement]
        public int? WorkStatusId
        {
        get { return m_workStatusId;}
        set { m_workStatusId = value; }
        }
      
        [XmlElement]
        public String StartMessage
        {
        get { return m_startMessage;}
        set { m_startMessage = value; }
        }
      
        [XmlElement]
        public String EndMessage
        {
        get { return m_endMessage;}
        set { m_endMessage = value; }
        }
      
        [XmlElement]
        public String EquipmentNotes
        {
        get { return m_equipmentNotes;}
        set { m_equipmentNotes = value; }
        }
      
      }
      #endregion
      }

    