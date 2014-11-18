
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

      public partial class PartnerProjectType : ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into PartnerProjectType ( " +
      
        " BusinessPartnerId, " +
      
        " Name " +
      
      ") Values (" +
      
        " ?BusinessPartnerId, " +
      
        " ?Name " +
      
      ")";

      public static void Insert(PartnerProjectType partnerProjectType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", partnerProjectType.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?Name", partnerProjectType.Name);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        partnerProjectType.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(PartnerProjectType partnerProjectType)
      {
        Insert(partnerProjectType, null);
      }


      public static void Insert(List<PartnerProjectType>  partnerProjectTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(PartnerProjectType partnerProjectType in  partnerProjectTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", partnerProjectType.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?Name", partnerProjectType.Name);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?BusinessPartnerId",partnerProjectType.BusinessPartnerId);
      
        Database.UpdateParameter(dbCommand,"?Name",partnerProjectType.Name);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        partnerProjectType.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<PartnerProjectType>  partnerProjectTypeList)
      {
        Insert(partnerProjectTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update PartnerProjectType Set "
      
        + " BusinessPartnerId = ?BusinessPartnerId, "
      
        + " Name = ?Name "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(PartnerProjectType partnerProjectType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", partnerProjectType.Id);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", partnerProjectType.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?Name", partnerProjectType.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(PartnerProjectType partnerProjectType)
      {
        Update(partnerProjectType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Id, "
      
        + " BusinessPartnerId, "
      
        + " Name "
      

      + " From PartnerProjectType "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static PartnerProjectType FindByPrimaryKey(
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
      throw new DataNotFoundException("PartnerProjectType not found, search by primary key");

      }

      public static PartnerProjectType FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(PartnerProjectType partnerProjectType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",partnerProjectType.Id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(PartnerProjectType partnerProjectType)
      {
      return Exists(partnerProjectType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from PartnerProjectType limit 1";

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

      public static PartnerProjectType Load(IDataReader dataReader, int offset)
      {
      PartnerProjectType partnerProjectType = new PartnerProjectType();

      partnerProjectType.Id = dataReader.GetInt32(0 + offset);
          partnerProjectType.BusinessPartnerId = dataReader.GetInt32(1 + offset);
          partnerProjectType.Name = dataReader.GetString(2 + offset);
          

      return partnerProjectType;
      }

      public static PartnerProjectType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From PartnerProjectType "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;
      public static void Delete(PartnerProjectType partnerProjectType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", partnerProjectType.Id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(PartnerProjectType partnerProjectType)
      {
        Delete(partnerProjectType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From PartnerProjectType ";

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
      
        + " BusinessPartnerId, "
      
        + " Name "
      

      + " From PartnerProjectType ";
      public static List<PartnerProjectType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<PartnerProjectType> rv = new List<PartnerProjectType>();

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

      public static List<PartnerProjectType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<PartnerProjectType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<PartnerProjectType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PartnerProjectType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(PartnerProjectType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<PartnerProjectType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PartnerProjectType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<PartnerProjectType> itemsList
      = new List<PartnerProjectType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is PartnerProjectType)
      itemsList.Add(deserializedObject as PartnerProjectType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_id;
      
        protected int m_businessPartnerId;
      
        protected String m_name;
      
      #endregion

      #region Constructors
      public PartnerProjectType(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public PartnerProjectType(
        int 
          id,int 
          businessPartnerId,String 
          name
        ) : this()
        {
        
          m_id = id;
        
          m_businessPartnerId = businessPartnerId;
        
          m_name = name;
        
        }


      
      #endregion

      
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        public int BusinessPartnerId
        {
        get { return m_businessPartnerId;}
        set { m_businessPartnerId = value; }
        }
      
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      

      public static int FieldsCount
      {
      get { return 3; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    