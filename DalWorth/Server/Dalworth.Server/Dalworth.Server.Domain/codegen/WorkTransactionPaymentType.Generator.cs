
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


      public partial class WorkTransactionPaymentType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WorkTransactionPaymentType ( " +
      
        " ID, " +
      
        " PaymentType, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?PaymentType, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(WorkTransactionPaymentType workTransactionPaymentType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", workTransactionPaymentType.ID);
      
        Database.PutParameter(dbCommand,"?PaymentType", workTransactionPaymentType.PaymentType);
      
        Database.PutParameter(dbCommand,"?Description", workTransactionPaymentType.Description);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(WorkTransactionPaymentType workTransactionPaymentType)
      {
        Insert(workTransactionPaymentType, null);
      }


      public static void Insert(List<WorkTransactionPaymentType>  workTransactionPaymentTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WorkTransactionPaymentType workTransactionPaymentType in  workTransactionPaymentTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", workTransactionPaymentType.ID);
      
        Database.PutParameter(dbCommand,"?PaymentType", workTransactionPaymentType.PaymentType);
      
        Database.PutParameter(dbCommand,"?Description", workTransactionPaymentType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",workTransactionPaymentType.ID);
      
        Database.UpdateParameter(dbCommand,"?PaymentType",workTransactionPaymentType.PaymentType);
      
        Database.UpdateParameter(dbCommand,"?Description",workTransactionPaymentType.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<WorkTransactionPaymentType>  workTransactionPaymentTypeList)
      {
        Insert(workTransactionPaymentTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WorkTransactionPaymentType Set "
      
        + " PaymentType = ?PaymentType, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(WorkTransactionPaymentType workTransactionPaymentType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", workTransactionPaymentType.ID);
      
        Database.PutParameter(dbCommand,"?PaymentType", workTransactionPaymentType.PaymentType);
      
        Database.PutParameter(dbCommand,"?Description", workTransactionPaymentType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkTransactionPaymentType workTransactionPaymentType)
      {
        Update(workTransactionPaymentType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " PaymentType, "
      
        + " Description "
      

      + " From WorkTransactionPaymentType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static WorkTransactionPaymentType FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkTransactionPaymentType not found, search by primary key");

      }

      public static WorkTransactionPaymentType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WorkTransactionPaymentType workTransactionPaymentType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",workTransactionPaymentType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkTransactionPaymentType workTransactionPaymentType)
      {
      return Exists(workTransactionPaymentType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WorkTransactionPaymentType limit 1";

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

      public static WorkTransactionPaymentType Load(IDataReader dataReader, int offset)
      {
      WorkTransactionPaymentType workTransactionPaymentType = new WorkTransactionPaymentType();

      workTransactionPaymentType.ID = dataReader.GetInt32(0 + offset);
          workTransactionPaymentType.PaymentType = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            workTransactionPaymentType.Description = dataReader.GetString(2 + offset);
          

      return workTransactionPaymentType;
      }

      public static WorkTransactionPaymentType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WorkTransactionPaymentType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(WorkTransactionPaymentType workTransactionPaymentType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", workTransactionPaymentType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransactionPaymentType workTransactionPaymentType)
      {
        Delete(workTransactionPaymentType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WorkTransactionPaymentType ";

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
      
        + " PaymentType, "
      
        + " Description "
      

      + " From WorkTransactionPaymentType ";
      public static List<WorkTransactionPaymentType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WorkTransactionPaymentType> rv = new List<WorkTransactionPaymentType>();

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

      public static List<WorkTransactionPaymentType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkTransactionPaymentType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WorkTransactionPaymentType obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && PaymentType == obj.PaymentType && Description == obj.Description;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WorkTransactionPaymentType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionPaymentType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkTransactionPaymentType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkTransactionPaymentType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionPaymentType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkTransactionPaymentType> itemsList
      = new List<WorkTransactionPaymentType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkTransactionPaymentType)
      itemsList.Add(deserializedObject as WorkTransactionPaymentType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_paymentType;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public WorkTransactionPaymentType(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public WorkTransactionPaymentType(
        int 
          iD,String 
          paymentType,String 
          description
        ) : this()
        {
        
          m_iD = iD;
        
          m_paymentType = paymentType;
        
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
        public String PaymentType
        {
        get { return m_paymentType;}
        set { m_paymentType = value; }
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

    