
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

      public partial class PhoneToSalesRep : ICloneable
      {

      #region Store


      #region Save

      public static PhoneToSalesRep Save(PhoneToSalesRep phoneToSalesRep, IDbConnection connection)
      {
      	if (!Exists(phoneToSalesRep, connection))
      		Insert(phoneToSalesRep, connection);
      	else
      		Update(phoneToSalesRep, connection);
      	return phoneToSalesRep;
      }

      public static PhoneToSalesRep Save(PhoneToSalesRep phoneToSalesRep)
      {
      	if (!Exists(phoneToSalesRep))
      		Insert(phoneToSalesRep);
      	else
      		Update(phoneToSalesRep);
      	return phoneToSalesRep;
      }

      #endregion


      #region Insert

      private const String SqlInsert = "Insert Into PhoneToSalesRep ( " +
      
        " PhoneId, " +
      
        " SalesRepId, " +
      
        " Notes, " +
      
        " IsIncoming " +
      
      ") Values (" +
      
        " ?PhoneId, " +
      
        " ?SalesRepId, " +
      
        " ?Notes, " +
      
        " ?IsIncoming " +
      
      ")";

      public static void Insert(PhoneToSalesRep phoneToSalesRep, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?PhoneId", phoneToSalesRep.PhoneId);
      
        Database.PutParameter(dbCommand,"?SalesRepId", phoneToSalesRep.SalesRepId);
      
        Database.PutParameter(dbCommand,"?Notes", phoneToSalesRep.Notes);
      
        Database.PutParameter(dbCommand,"?IsIncoming", phoneToSalesRep.IsIncoming);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(PhoneToSalesRep phoneToSalesRep)
      {
        Insert(phoneToSalesRep, null);
      }


      public static void Insert(List<PhoneToSalesRep>  phoneToSalesRepList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(PhoneToSalesRep phoneToSalesRep in  phoneToSalesRepList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?PhoneId", phoneToSalesRep.PhoneId);
      
        Database.PutParameter(dbCommand,"?SalesRepId", phoneToSalesRep.SalesRepId);
      
        Database.PutParameter(dbCommand,"?Notes", phoneToSalesRep.Notes);
      
        Database.PutParameter(dbCommand,"?IsIncoming", phoneToSalesRep.IsIncoming);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?PhoneId",phoneToSalesRep.PhoneId);
      
        Database.UpdateParameter(dbCommand,"?SalesRepId",phoneToSalesRep.SalesRepId);
      
        Database.UpdateParameter(dbCommand,"?Notes",phoneToSalesRep.Notes);
      
        Database.UpdateParameter(dbCommand,"?IsIncoming",phoneToSalesRep.IsIncoming);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<PhoneToSalesRep>  phoneToSalesRepList)
      {
        Insert(phoneToSalesRepList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update PhoneToSalesRep Set "
      
        + " Notes = ?Notes, "
      
        + " IsIncoming = ?IsIncoming "
      
        + " Where "
        
          + " PhoneId = ?PhoneId and  "
        
          + " SalesRepId = ?SalesRepId "
        
      ;

      public static void Update(PhoneToSalesRep phoneToSalesRep, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?PhoneId", phoneToSalesRep.PhoneId);
      
        Database.PutParameter(dbCommand,"?SalesRepId", phoneToSalesRep.SalesRepId);
      
        Database.PutParameter(dbCommand,"?Notes", phoneToSalesRep.Notes);
      
        Database.PutParameter(dbCommand,"?IsIncoming", phoneToSalesRep.IsIncoming);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(PhoneToSalesRep phoneToSalesRep)
      {
        Update(phoneToSalesRep, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " PhoneId, "
      
        + " SalesRepId, "
      
        + " Notes, "
      
        + " IsIncoming "
      

      + " From PhoneToSalesRep "

      
        + " Where "
        
          + " PhoneId = ?PhoneId and  "
        
          + " SalesRepId = ?SalesRepId "
        
      ;

      public static PhoneToSalesRep FindByPrimaryKey(
      int phoneId,int salesRepId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?PhoneId", phoneId);
      
        Database.PutParameter(dbCommand,"?SalesRepId", salesRepId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("PhoneToSalesRep not found, search by primary key");

      }

      public static PhoneToSalesRep FindByPrimaryKey(
      int phoneId,int salesRepId
      )
      {
      return FindByPrimaryKey(
      phoneId,salesRepId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(PhoneToSalesRep phoneToSalesRep, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?PhoneId",phoneToSalesRep.PhoneId);
      
        Database.PutParameter(dbCommand,"?SalesRepId",phoneToSalesRep.SalesRepId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(PhoneToSalesRep phoneToSalesRep)
      {
      return Exists(phoneToSalesRep, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from PhoneToSalesRep limit 1";

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

      public static PhoneToSalesRep Load(IDataReader dataReader, int offset)
      {
      PhoneToSalesRep phoneToSalesRep = new PhoneToSalesRep();

      phoneToSalesRep.PhoneId = dataReader.GetInt32(0 + offset);
          phoneToSalesRep.SalesRepId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            phoneToSalesRep.Notes = dataReader.GetString(2 + offset);
          phoneToSalesRep.IsIncoming = dataReader.GetBoolean(3 + offset);
          

      return phoneToSalesRep;
      }

      public static PhoneToSalesRep Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From PhoneToSalesRep "

      
        + " Where "
        
          + " PhoneId = ?PhoneId and  "
        
          + " SalesRepId = ?SalesRepId "
        
      ;
      public static void Delete(PhoneToSalesRep phoneToSalesRep, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?PhoneId", phoneToSalesRep.PhoneId);
      
        Database.PutParameter(dbCommand,"?SalesRepId", phoneToSalesRep.SalesRepId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(PhoneToSalesRep phoneToSalesRep)
      {
        Delete(phoneToSalesRep, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From PhoneToSalesRep ";

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
      
        + " SalesRepId, "
      
        + " Notes, "
      
        + " IsIncoming "
      

      + " From PhoneToSalesRep ";
      public static List<PhoneToSalesRep> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<PhoneToSalesRep> rv = new List<PhoneToSalesRep>();

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

      public static List<PhoneToSalesRep> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<PhoneToSalesRep> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<PhoneToSalesRep> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PhoneToSalesRep));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(PhoneToSalesRep item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<PhoneToSalesRep>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PhoneToSalesRep));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<PhoneToSalesRep> itemsList
      = new List<PhoneToSalesRep>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is PhoneToSalesRep)
      itemsList.Add(deserializedObject as PhoneToSalesRep);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_phoneId;
      
        protected int m_salesRepId;
      
        protected String m_notes;
      
        protected bool m_isIncoming;
      
      #endregion

      #region Constructors
      public PhoneToSalesRep(
      int 
          phoneId,int 
          salesRepId
      ) : this()
      {
      
        m_phoneId = phoneId;
      
        m_salesRepId = salesRepId;
      
      }

      


        public PhoneToSalesRep(
        int 
          phoneId,int 
          salesRepId,String 
          notes,bool 
          isIncoming
        ) : this()
        {
        
          m_phoneId = phoneId;
        
          m_salesRepId = salesRepId;
        
          m_notes = notes;
        
          m_isIncoming = isIncoming;
        
        }


      
      #endregion

      
        public int PhoneId
        {
        get { return m_phoneId;}
        set { m_phoneId = value; }
        }
      
        public int SalesRepId
        {
        get { return m_salesRepId;}
        set { m_salesRepId = value; }
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

    