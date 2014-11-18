
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


      public partial class BankCheckAccountType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into BankCheckAccountType ( " +
      
        " ID, " +
      
        " Type, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Type, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(BankCheckAccountType bankCheckAccountType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", bankCheckAccountType.ID);
      
        Database.PutParameter(dbCommand,"?Type", bankCheckAccountType.Type);
      
        Database.PutParameter(dbCommand,"?Description", bankCheckAccountType.Description);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(BankCheckAccountType bankCheckAccountType)
      {
        Insert(bankCheckAccountType, null);
      }


      public static void Insert(List<BankCheckAccountType>  bankCheckAccountTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(BankCheckAccountType bankCheckAccountType in  bankCheckAccountTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", bankCheckAccountType.ID);
      
        Database.PutParameter(dbCommand,"?Type", bankCheckAccountType.Type);
      
        Database.PutParameter(dbCommand,"?Description", bankCheckAccountType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",bankCheckAccountType.ID);
      
        Database.UpdateParameter(dbCommand,"?Type",bankCheckAccountType.Type);
      
        Database.UpdateParameter(dbCommand,"?Description",bankCheckAccountType.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<BankCheckAccountType>  bankCheckAccountTypeList)
      {
        Insert(bankCheckAccountTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update BankCheckAccountType Set "
      
        + " Type = ?Type, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(BankCheckAccountType bankCheckAccountType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", bankCheckAccountType.ID);
      
        Database.PutParameter(dbCommand,"?Type", bankCheckAccountType.Type);
      
        Database.PutParameter(dbCommand,"?Description", bankCheckAccountType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(BankCheckAccountType bankCheckAccountType)
      {
        Update(bankCheckAccountType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Type, "
      
        + " Description "
      

      + " From BankCheckAccountType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static BankCheckAccountType FindByPrimaryKey(
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
      throw new DataNotFoundException("BankCheckAccountType not found, search by primary key");

      }

      public static BankCheckAccountType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(BankCheckAccountType bankCheckAccountType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",bankCheckAccountType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(BankCheckAccountType bankCheckAccountType)
      {
      return Exists(bankCheckAccountType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from BankCheckAccountType limit 1";

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

      public static BankCheckAccountType Load(IDataReader dataReader, int offset)
      {
      BankCheckAccountType bankCheckAccountType = new BankCheckAccountType();

      bankCheckAccountType.ID = dataReader.GetInt32(0 + offset);
          bankCheckAccountType.Type = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            bankCheckAccountType.Description = dataReader.GetString(2 + offset);
          

      return bankCheckAccountType;
      }

      public static BankCheckAccountType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From BankCheckAccountType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(BankCheckAccountType bankCheckAccountType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", bankCheckAccountType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(BankCheckAccountType bankCheckAccountType)
      {
        Delete(bankCheckAccountType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From BankCheckAccountType ";

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
      

      + " From BankCheckAccountType ";
      public static List<BankCheckAccountType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<BankCheckAccountType> rv = new List<BankCheckAccountType>();

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

      public static List<BankCheckAccountType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<BankCheckAccountType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (BankCheckAccountType obj)
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

      List<BankCheckAccountType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(BankCheckAccountType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(BankCheckAccountType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<BankCheckAccountType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(BankCheckAccountType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<BankCheckAccountType> itemsList
      = new List<BankCheckAccountType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is BankCheckAccountType)
      itemsList.Add(deserializedObject as BankCheckAccountType);
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
      public BankCheckAccountType(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public BankCheckAccountType(
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

    