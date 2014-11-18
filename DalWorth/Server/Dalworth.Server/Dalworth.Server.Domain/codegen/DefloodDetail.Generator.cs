
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


      public partial class DefloodDetail : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into DefloodDetail ( " +
      
        " DefloodTaskId, " +
      
        " FloodDate, " +
      
        " FloodClass, " +
      
        " CubicFeet " +
      
      ") Values (" +
      
        " ?DefloodTaskId, " +
      
        " ?FloodDate, " +
      
        " ?FloodClass, " +
      
        " ?CubicFeet " +
      
      ")";

      public static void Insert(DefloodDetail defloodDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?DefloodTaskId", defloodDetail.DefloodTaskId);
      
        Database.PutParameter(dbCommand,"?FloodDate", defloodDetail.FloodDate);
      
        Database.PutParameter(dbCommand,"?FloodClass", defloodDetail.FloodClass);
      
        Database.PutParameter(dbCommand,"?CubicFeet", defloodDetail.CubicFeet);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(DefloodDetail defloodDetail)
      {
        Insert(defloodDetail, null);
      }


      public static void Insert(List<DefloodDetail>  defloodDetailList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(DefloodDetail defloodDetail in  defloodDetailList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?DefloodTaskId", defloodDetail.DefloodTaskId);
      
        Database.PutParameter(dbCommand,"?FloodDate", defloodDetail.FloodDate);
      
        Database.PutParameter(dbCommand,"?FloodClass", defloodDetail.FloodClass);
      
        Database.PutParameter(dbCommand,"?CubicFeet", defloodDetail.CubicFeet);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?DefloodTaskId",defloodDetail.DefloodTaskId);
      
        Database.UpdateParameter(dbCommand,"?FloodDate",defloodDetail.FloodDate);
      
        Database.UpdateParameter(dbCommand,"?FloodClass",defloodDetail.FloodClass);
      
        Database.UpdateParameter(dbCommand,"?CubicFeet",defloodDetail.CubicFeet);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<DefloodDetail>  defloodDetailList)
      {
        Insert(defloodDetailList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update DefloodDetail Set "
      
        + " FloodDate = ?FloodDate, "
      
        + " FloodClass = ?FloodClass, "
      
        + " CubicFeet = ?CubicFeet "
      
        + " Where "
        
          + " DefloodTaskId = ?DefloodTaskId "
        
      ;

      public static void Update(DefloodDetail defloodDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?DefloodTaskId", defloodDetail.DefloodTaskId);
      
        Database.PutParameter(dbCommand,"?FloodDate", defloodDetail.FloodDate);
      
        Database.PutParameter(dbCommand,"?FloodClass", defloodDetail.FloodClass);
      
        Database.PutParameter(dbCommand,"?CubicFeet", defloodDetail.CubicFeet);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(DefloodDetail defloodDetail)
      {
        Update(defloodDetail, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " DefloodTaskId, "
      
        + " FloodDate, "
      
        + " FloodClass, "
      
        + " CubicFeet "
      

      + " From DefloodDetail "

      
        + " Where "
        
          + " DefloodTaskId = ?DefloodTaskId "
        
      ;

      public static DefloodDetail FindByPrimaryKey(
      int defloodTaskId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?DefloodTaskId", defloodTaskId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("DefloodDetail not found, search by primary key");

      }

      public static DefloodDetail FindByPrimaryKey(
      int defloodTaskId
      )
      {
      return FindByPrimaryKey(
      defloodTaskId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(DefloodDetail defloodDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?DefloodTaskId",defloodDetail.DefloodTaskId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(DefloodDetail defloodDetail)
      {
      return Exists(defloodDetail, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from DefloodDetail limit 1";

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

      public static DefloodDetail Load(IDataReader dataReader, int offset)
      {
      DefloodDetail defloodDetail = new DefloodDetail();

      defloodDetail.DefloodTaskId = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            defloodDetail.FloodDate = dataReader.GetDateTime(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            defloodDetail.FloodClass = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            defloodDetail.CubicFeet = dataReader.GetDecimal(3 + offset);
          

      return defloodDetail;
      }

      public static DefloodDetail Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From DefloodDetail "

      
        + " Where "
        
          + " DefloodTaskId = ?DefloodTaskId "
        
      ;
      public static void Delete(DefloodDetail defloodDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?DefloodTaskId", defloodDetail.DefloodTaskId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(DefloodDetail defloodDetail)
      {
        Delete(defloodDetail, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From DefloodDetail ";

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

      
        + " DefloodTaskId, "
      
        + " FloodDate, "
      
        + " FloodClass, "
      
        + " CubicFeet "
      

      + " From DefloodDetail ";
      public static List<DefloodDetail> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<DefloodDetail> rv = new List<DefloodDetail>();

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

      public static List<DefloodDetail> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<DefloodDetail> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (DefloodDetail obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return DefloodTaskId == obj.DefloodTaskId && FloodDate == obj.FloodDate && FloodClass == obj.FloodClass && CubicFeet == obj.CubicFeet;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<DefloodDetail> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(DefloodDetail));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(DefloodDetail item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<DefloodDetail>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(DefloodDetail));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<DefloodDetail> itemsList
      = new List<DefloodDetail>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is DefloodDetail)
      itemsList.Add(deserializedObject as DefloodDetail);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_defloodTaskId;
      
        protected DateTime? m_floodDate;
      
        protected int? m_floodClass;
      
        protected decimal m_cubicFeet;
      
      #endregion

      #region Constructors
      public DefloodDetail(
      int 
          defloodTaskId
      ) : this()
      {
      
        m_defloodTaskId = defloodTaskId;
      
      }

      


        public DefloodDetail(
        int 
          defloodTaskId,DateTime? 
          floodDate,int? 
          floodClass,decimal 
          cubicFeet
        ) : this()
        {
        
          m_defloodTaskId = defloodTaskId;
        
          m_floodDate = floodDate;
        
          m_floodClass = floodClass;
        
          m_cubicFeet = cubicFeet;
        
        }


      
      #endregion

      
        [XmlElement]
        public int DefloodTaskId
        {
        get { return m_defloodTaskId;}
        set { m_defloodTaskId = value; }
        }
      
        [XmlElement]
        public DateTime? FloodDate
        {
        get { return m_floodDate;}
        set { m_floodDate = value; }
        }
      
        [XmlElement]
        public int? FloodClass
        {
        get { return m_floodClass;}
        set { m_floodClass = value; }
        }
      
        [XmlElement]
        public decimal CubicFeet
        {
        get { return m_cubicFeet;}
        set { m_cubicFeet = value; }
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

    