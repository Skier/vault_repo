
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

      public partial class SalesRep : ICloneable
      {

      #region Store


      #region Save

      public static SalesRep Save(SalesRep salesRep, IDbConnection connection)
      {
      	if (!Exists(salesRep, connection))
      		Insert(salesRep, connection);
      	else
      		Update(salesRep, connection);
      	return salesRep;
      }

      public static SalesRep Save(SalesRep salesRep)
      {
      	if (!Exists(salesRep))
      		Insert(salesRep);
      	else
      		Update(salesRep);
      	return salesRep;
      }

      #endregion


      #region Insert

      private const String SqlInsert = "Insert Into SalesRep ( " +
      
        " UserId, " +
      
        " ShowAs, " +
      
        " QbSalesRepRecordId, " +
      
        " IsActive " +
      
      ") Values (" +
      
        " ?UserId, " +
      
        " ?ShowAs, " +
      
        " ?QbSalesRepRecordId, " +
      
        " ?IsActive " +
      
      ")";

      public static void Insert(SalesRep salesRep, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?UserId", salesRep.UserId);
      
        Database.PutParameter(dbCommand,"?ShowAs", salesRep.ShowAs);
      
        Database.PutParameter(dbCommand,"?QbSalesRepRecordId", salesRep.QbSalesRepRecordId);
      
        Database.PutParameter(dbCommand,"?IsActive", salesRep.IsActive);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        salesRep.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(SalesRep salesRep)
      {
        Insert(salesRep, null);
      }


      public static void Insert(List<SalesRep>  salesRepList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(SalesRep salesRep in  salesRepList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?UserId", salesRep.UserId);
      
        Database.PutParameter(dbCommand,"?ShowAs", salesRep.ShowAs);
      
        Database.PutParameter(dbCommand,"?QbSalesRepRecordId", salesRep.QbSalesRepRecordId);
      
        Database.PutParameter(dbCommand,"?IsActive", salesRep.IsActive);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?UserId",salesRep.UserId);
      
        Database.UpdateParameter(dbCommand,"?ShowAs",salesRep.ShowAs);
      
        Database.UpdateParameter(dbCommand,"?QbSalesRepRecordId",salesRep.QbSalesRepRecordId);
      
        Database.UpdateParameter(dbCommand,"?IsActive",salesRep.IsActive);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        salesRep.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<SalesRep>  salesRepList)
      {
        Insert(salesRepList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update SalesRep Set "
      
        + " UserId = ?UserId, "
      
        + " ShowAs = ?ShowAs, "
      
        + " QbSalesRepRecordId = ?QbSalesRepRecordId, "
      
        + " IsActive = ?IsActive "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(SalesRep salesRep, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", salesRep.Id);
      
        Database.PutParameter(dbCommand,"?UserId", salesRep.UserId);
      
        Database.PutParameter(dbCommand,"?ShowAs", salesRep.ShowAs);
      
        Database.PutParameter(dbCommand,"?QbSalesRepRecordId", salesRep.QbSalesRepRecordId);
      
        Database.PutParameter(dbCommand,"?IsActive", salesRep.IsActive);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(SalesRep salesRep)
      {
        Update(salesRep, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Id, "
      
        + " UserId, "
      
        + " ShowAs, "
      
        + " QbSalesRepRecordId, "
      
        + " IsActive "
      

      + " From SalesRep "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static SalesRep FindByPrimaryKey(
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
      throw new DataNotFoundException("SalesRep not found, search by primary key");

      }

      public static SalesRep FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(SalesRep salesRep, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",salesRep.Id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(SalesRep salesRep)
      {
      return Exists(salesRep, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from SalesRep limit 1";

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

      public static SalesRep Load(IDataReader dataReader, int offset)
      {
      SalesRep salesRep = new SalesRep();

      salesRep.Id = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            salesRep.UserId = dataReader.GetInt32(1 + offset);
          salesRep.ShowAs = dataReader.GetString(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            salesRep.QbSalesRepRecordId = dataReader.GetString(3 + offset);
          salesRep.IsActive = dataReader.GetBoolean(4 + offset);
          

      return salesRep;
      }

      public static SalesRep Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From SalesRep "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;
      public static void Delete(SalesRep salesRep, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", salesRep.Id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(SalesRep salesRep)
      {
        Delete(salesRep, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From SalesRep ";

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
      
        + " UserId, "
      
        + " ShowAs, "
      
        + " QbSalesRepRecordId, "
      
        + " IsActive "
      

      + " From SalesRep ";
      public static List<SalesRep> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<SalesRep> rv = new List<SalesRep>();

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

      public static List<SalesRep> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<SalesRep> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<SalesRep> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(SalesRep));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(SalesRep item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<SalesRep>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(SalesRep));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<SalesRep> itemsList
      = new List<SalesRep>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is SalesRep)
      itemsList.Add(deserializedObject as SalesRep);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_id;
      
        protected int? m_userId;
      
        protected String m_showAs;
      
        protected String m_qbSalesRepRecordId;
      
        protected bool m_isActive;
      
      #endregion

      #region Constructors
      public SalesRep(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public SalesRep(
        int 
          id,int? 
          userId,String 
          showAs,String 
          qbSalesRepRecordId,bool 
          isActive
        ) : this()
        {
        
          m_id = id;
        
          m_userId = userId;
        
          m_showAs = showAs;
        
          m_qbSalesRepRecordId = qbSalesRepRecordId;
        
          m_isActive = isActive;
        
        }


      
      #endregion

      
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        public int? UserId
        {
        get { return m_userId;}
        set { m_userId = value; }
        }
      
        public String ShowAs
        {
        get { return m_showAs;}
        set { m_showAs = value; }
        }
      
        public String QbSalesRepRecordId
        {
        get { return m_qbSalesRepRecordId;}
        set { m_qbSalesRepRecordId = value; }
        }
      
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
        }
      

      public static int FieldsCount
      {
      get { return 5; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    