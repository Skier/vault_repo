
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


      public partial class VanDetail : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [VanDetail] ( " +
      
        " ID, " +
      
        " VanId, " +
      
        " DateCreated, " +
      
        " OilChangeDue " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @VanId, " +
      
        " @DateCreated, " +
      
        " @OilChangeDue " +
      
      ")";

      public static void Insert(VanDetail vanDetail, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", vanDetail.ID);
      
        Database.PutParameter(dbCommand,"@VanId", vanDetail.VanId);
      
        Database.PutParameter(dbCommand,"@DateCreated", vanDetail.DateCreated);
      
        Database.PutParameter(dbCommand,"@OilChangeDue", vanDetail.OilChangeDue);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(VanDetail vanDetail)
      {
        Insert(vanDetail, null);
      }

      public static void Insert(List<VanDetail>  vanDetailList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(VanDetail vanDetail in  vanDetailList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", vanDetail.ID);
      
        Database.PutParameter(dbCommand,"@VanId", vanDetail.VanId);
      
        Database.PutParameter(dbCommand,"@DateCreated", vanDetail.DateCreated);
      
        Database.PutParameter(dbCommand,"@OilChangeDue", vanDetail.OilChangeDue);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",vanDetail.ID);
      
        Database.UpdateParameter(dbCommand,"@VanId",vanDetail.VanId);
      
        Database.UpdateParameter(dbCommand,"@DateCreated",vanDetail.DateCreated);
      
        Database.UpdateParameter(dbCommand,"@OilChangeDue",vanDetail.OilChangeDue);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<VanDetail>  vanDetailList)
      {
      Insert(vanDetailList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [VanDetail] Set "
      
        + " VanId = @VanId, "
      
        + " DateCreated = @DateCreated, "
      
        + " OilChangeDue = @OilChangeDue "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(VanDetail vanDetail, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", vanDetail.ID);
      
        Database.PutParameter(dbCommand,"@VanId", vanDetail.VanId);
      
        Database.PutParameter(dbCommand,"@DateCreated", vanDetail.DateCreated);
      
        Database.PutParameter(dbCommand,"@OilChangeDue", vanDetail.OilChangeDue);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(VanDetail vanDetail)
      {
      Update(vanDetail, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " VanId, "
      
        + " DateCreated, "
      
        + " OilChangeDue "
      

      + " From [VanDetail] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static VanDetail FindByPrimaryKey(
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
      throw new DataNotFoundException("VanDetail not found, search by primary key");

      }

      public static VanDetail FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(VanDetail vanDetail, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",vanDetail.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(VanDetail vanDetail)
      {
      return Exists(vanDetail, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from VanDetail";

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

      public static VanDetail Load(IDataReader dataReader)
      {
      VanDetail vanDetail = new VanDetail();

      vanDetail.ID = dataReader.GetInt32(0);
          vanDetail.VanId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            vanDetail.DateCreated = dataReader.GetDateTime(2);
          
            if(!dataReader.IsDBNull(3))
            vanDetail.OilChangeDue = dataReader.GetDecimal(3);
          

      return vanDetail;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [VanDetail] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(VanDetail vanDetail, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", vanDetail.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(VanDetail vanDetail)
      {
      Delete(vanDetail, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [VanDetail] ";

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
      
        + " VanId, "
      
        + " DateCreated, "
      
        + " OilChangeDue "
      

      + " From [VanDetail] ";
      public static List<VanDetail> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<VanDetail> rv = new List<VanDetail>();

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

      public static List<VanDetail> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<VanDetail> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<VanDetail> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(VanDetail));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(VanDetail item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<VanDetail>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(VanDetail));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<VanDetail> itemsList
      = new List<VanDetail>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is VanDetail)
      itemsList.Add(deserializedObject as VanDetail);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_vanId;
      
        protected DateTime? m_dateCreated;
      
        protected decimal m_oilChangeDue;
      
      #endregion

      #region Constructors
      public VanDetail(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public VanDetail(
        int 
          iD,int 
          vanId,DateTime? 
          dateCreated,decimal 
          oilChangeDue
        )
        {
        
          m_iD = iD;
        
          m_vanId = vanId;
        
          m_dateCreated = dateCreated;
        
          m_oilChangeDue = oilChangeDue;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int VanId
        {
        get { return m_vanId;}
        set { m_vanId = value; }
        }
      
        [XmlElement]
        public DateTime? DateCreated
        {
        get { return m_dateCreated;}
        set { m_dateCreated = value; }
        }
      
        [XmlElement]
        public decimal OilChangeDue
        {
        get { return m_oilChangeDue;}
        set { m_oilChangeDue = value; }
        }
      
      }
      #endregion
      }

    