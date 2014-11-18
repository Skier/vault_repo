
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


      public partial class WorkDetail : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [WorkDetail] ( " +
      
        " ID, " +
      
        " WorkId, " +
      
        " VisitId, " +
      
        " Sequence, " +
      
        " WorkDetailStatusId " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @WorkId, " +
      
        " @VisitId, " +
      
        " @Sequence, " +
      
        " @WorkDetailStatusId " +
      
      ")";

      public static void Insert(WorkDetail workDetail, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workDetail.ID);
      
        Database.PutParameter(dbCommand,"@WorkId", workDetail.WorkId);
      
        Database.PutParameter(dbCommand,"@VisitId", workDetail.VisitId);
      
        Database.PutParameter(dbCommand,"@Sequence", workDetail.Sequence);
      
        Database.PutParameter(dbCommand,"@WorkDetailStatusId", workDetail.WorkDetailStatusId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(WorkDetail workDetail)
      {
        Insert(workDetail, null);
      }

      public static void Insert(List<WorkDetail>  workDetailList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(WorkDetail workDetail in  workDetailList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", workDetail.ID);
      
        Database.PutParameter(dbCommand,"@WorkId", workDetail.WorkId);
      
        Database.PutParameter(dbCommand,"@VisitId", workDetail.VisitId);
      
        Database.PutParameter(dbCommand,"@Sequence", workDetail.Sequence);
      
        Database.PutParameter(dbCommand,"@WorkDetailStatusId", workDetail.WorkDetailStatusId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",workDetail.ID);
      
        Database.UpdateParameter(dbCommand,"@WorkId",workDetail.WorkId);
      
        Database.UpdateParameter(dbCommand,"@VisitId",workDetail.VisitId);
      
        Database.UpdateParameter(dbCommand,"@Sequence",workDetail.Sequence);
      
        Database.UpdateParameter(dbCommand,"@WorkDetailStatusId",workDetail.WorkDetailStatusId);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<WorkDetail>  workDetailList)
      {
      Insert(workDetailList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [WorkDetail] Set "
      
        + " WorkId = @WorkId, "
      
        + " VisitId = @VisitId, "
      
        + " Sequence = @Sequence, "
      
        + " WorkDetailStatusId = @WorkDetailStatusId "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(WorkDetail workDetail, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workDetail.ID);
      
        Database.PutParameter(dbCommand,"@WorkId", workDetail.WorkId);
      
        Database.PutParameter(dbCommand,"@VisitId", workDetail.VisitId);
      
        Database.PutParameter(dbCommand,"@Sequence", workDetail.Sequence);
      
        Database.PutParameter(dbCommand,"@WorkDetailStatusId", workDetail.WorkDetailStatusId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkDetail workDetail)
      {
      Update(workDetail, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " WorkId, "
      
        + " VisitId, "
      
        + " Sequence, "
      
        + " WorkDetailStatusId "
      

      + " From [WorkDetail] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static WorkDetail FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkDetail not found, search by primary key");

      }

      public static WorkDetail FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(WorkDetail workDetail, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",workDetail.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkDetail workDetail)
      {
      return Exists(workDetail, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from WorkDetail";

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

      public static WorkDetail Load(IDataReader dataReader)
      {
      WorkDetail workDetail = new WorkDetail();

      workDetail.ID = dataReader.GetInt32(0);
          workDetail.WorkId = dataReader.GetInt32(1);
          workDetail.VisitId = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            workDetail.Sequence = dataReader.GetInt32(3);
          
            if(!dataReader.IsDBNull(4))
            workDetail.WorkDetailStatusId = dataReader.GetInt32(4);
          

      return workDetail;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [WorkDetail] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(WorkDetail workDetail, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", workDetail.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkDetail workDetail)
      {
      Delete(workDetail, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [WorkDetail] ";

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
      
        + " WorkId, "
      
        + " VisitId, "
      
        + " Sequence, "
      
        + " WorkDetailStatusId "
      

      + " From [WorkDetail] ";
      public static List<WorkDetail> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<WorkDetail> rv = new List<WorkDetail>();

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

      public static List<WorkDetail> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkDetail> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<WorkDetail> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkDetail));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkDetail item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkDetail>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkDetail));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkDetail> itemsList
      = new List<WorkDetail>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkDetail)
      itemsList.Add(deserializedObject as WorkDetail);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_workId;
      
        protected int m_visitId;
      
        protected int? m_sequence;
      
        protected int? m_workDetailStatusId;
      
      #endregion

      #region Constructors
      public WorkDetail(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public WorkDetail(
        int 
          iD,int 
          workId,int 
          visitId,int? 
          sequence,int? 
          workDetailStatusId
        )
        {
        
          m_iD = iD;
        
          m_workId = workId;
        
          m_visitId = visitId;
        
          m_sequence = sequence;
        
          m_workDetailStatusId = workDetailStatusId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int WorkId
        {
        get { return m_workId;}
        set { m_workId = value; }
        }
      
        [XmlElement]
        public int VisitId
        {
        get { return m_visitId;}
        set { m_visitId = value; }
        }
      
        [XmlElement]
        public int? Sequence
        {
        get { return m_sequence;}
        set { m_sequence = value; }
        }
      
        [XmlElement]
        public int? WorkDetailStatusId
        {
        get { return m_workDetailStatusId;}
        set { m_workDetailStatusId = value; }
        }
      
      }
      #endregion
      }

    