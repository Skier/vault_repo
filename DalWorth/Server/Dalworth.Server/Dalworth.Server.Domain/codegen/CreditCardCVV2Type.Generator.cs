
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


      public partial class CreditCardCVV2Type : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into CreditCardCVV2Type ( " +
      
        " ID, " +
      
        " Type, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Type, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(CreditCardCVV2Type creditCardCVV2Type, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", creditCardCVV2Type.ID);
      
        Database.PutParameter(dbCommand,"?Type", creditCardCVV2Type.Type);
      
        Database.PutParameter(dbCommand,"?Description", creditCardCVV2Type.Description);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(CreditCardCVV2Type creditCardCVV2Type)
      {
        Insert(creditCardCVV2Type, null);
      }


      public static void Insert(List<CreditCardCVV2Type>  creditCardCVV2TypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(CreditCardCVV2Type creditCardCVV2Type in  creditCardCVV2TypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", creditCardCVV2Type.ID);
      
        Database.PutParameter(dbCommand,"?Type", creditCardCVV2Type.Type);
      
        Database.PutParameter(dbCommand,"?Description", creditCardCVV2Type.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",creditCardCVV2Type.ID);
      
        Database.UpdateParameter(dbCommand,"?Type",creditCardCVV2Type.Type);
      
        Database.UpdateParameter(dbCommand,"?Description",creditCardCVV2Type.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<CreditCardCVV2Type>  creditCardCVV2TypeList)
      {
        Insert(creditCardCVV2TypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update CreditCardCVV2Type Set "
      
        + " Type = ?Type, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(CreditCardCVV2Type creditCardCVV2Type, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", creditCardCVV2Type.ID);
      
        Database.PutParameter(dbCommand,"?Type", creditCardCVV2Type.Type);
      
        Database.PutParameter(dbCommand,"?Description", creditCardCVV2Type.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(CreditCardCVV2Type creditCardCVV2Type)
      {
        Update(creditCardCVV2Type, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Type, "
      
        + " Description "
      

      + " From CreditCardCVV2Type "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static CreditCardCVV2Type FindByPrimaryKey(
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
      throw new DataNotFoundException("CreditCardCVV2Type not found, search by primary key");

      }

      public static CreditCardCVV2Type FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(CreditCardCVV2Type creditCardCVV2Type, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",creditCardCVV2Type.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(CreditCardCVV2Type creditCardCVV2Type)
      {
      return Exists(creditCardCVV2Type, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from CreditCardCVV2Type limit 1";

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

      public static CreditCardCVV2Type Load(IDataReader dataReader, int offset)
      {
      CreditCardCVV2Type creditCardCVV2Type = new CreditCardCVV2Type();

      creditCardCVV2Type.ID = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            creditCardCVV2Type.Type = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            creditCardCVV2Type.Description = dataReader.GetString(2 + offset);
          

      return creditCardCVV2Type;
      }

      public static CreditCardCVV2Type Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From CreditCardCVV2Type "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(CreditCardCVV2Type creditCardCVV2Type, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", creditCardCVV2Type.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(CreditCardCVV2Type creditCardCVV2Type)
      {
        Delete(creditCardCVV2Type, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From CreditCardCVV2Type ";

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
      
        + " Type, "
      
        + " Description "
      

      + " From CreditCardCVV2Type ";
      public static List<CreditCardCVV2Type> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<CreditCardCVV2Type> rv = new List<CreditCardCVV2Type>();

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

      public static List<CreditCardCVV2Type> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<CreditCardCVV2Type> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (CreditCardCVV2Type obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Type == obj.Type && Description == obj.Description;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<CreditCardCVV2Type> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CreditCardCVV2Type));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(CreditCardCVV2Type item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CreditCardCVV2Type>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CreditCardCVV2Type));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<CreditCardCVV2Type> itemsList
      = new List<CreditCardCVV2Type>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CreditCardCVV2Type)
      itemsList.Add(deserializedObject as CreditCardCVV2Type);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_type;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public CreditCardCVV2Type(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public CreditCardCVV2Type(
        int 
          iD,String 
          type,String 
          description
        ) : this()
        {
        
          m_iD = iD;
        
          m_type = type;
        
          m_description = description;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String Type
        {
        get { return m_type;}
        set { m_type = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
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

    