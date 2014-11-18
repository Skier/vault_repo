
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


      public partial class WorkTransactionType : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [WorkTransactionType] ( " +
      
        " ID, " +
      
        " Type, " +
      
        " Description " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @Type, " +
      
        " @Description " +
      
      ")";

      public static void Insert(WorkTransactionType workTransactionType, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionType.ID);
      
        Database.PutParameter(dbCommand,"@Type", workTransactionType.Type);
      
        Database.PutParameter(dbCommand,"@Description", workTransactionType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(WorkTransactionType workTransactionType)
      {
        Insert(workTransactionType, null);
      }

      public static void Insert(List<WorkTransactionType>  workTransactionTypeList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(WorkTransactionType workTransactionType in  workTransactionTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionType.ID);
      
        Database.PutParameter(dbCommand,"@Type", workTransactionType.Type);
      
        Database.PutParameter(dbCommand,"@Description", workTransactionType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",workTransactionType.ID);
      
        Database.UpdateParameter(dbCommand,"@Type",workTransactionType.Type);
      
        Database.UpdateParameter(dbCommand,"@Description",workTransactionType.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<WorkTransactionType>  workTransactionTypeList)
      {
      Insert(workTransactionTypeList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [WorkTransactionType] Set "
      
        + " Type = @Type, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(WorkTransactionType workTransactionType, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionType.ID);
      
        Database.PutParameter(dbCommand,"@Type", workTransactionType.Type);
      
        Database.PutParameter(dbCommand,"@Description", workTransactionType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkTransactionType workTransactionType)
      {
      Update(workTransactionType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Type, "
      
        + " Description "
      

      + " From [WorkTransactionType] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static WorkTransactionType FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkTransactionType not found, search by primary key");

      }

      public static WorkTransactionType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(WorkTransactionType workTransactionType, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",workTransactionType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkTransactionType workTransactionType)
      {
      return Exists(workTransactionType, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from WorkTransactionType";

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

      public static WorkTransactionType Load(IDataReader dataReader)
      {
      WorkTransactionType workTransactionType = new WorkTransactionType();

      workTransactionType.ID = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            workTransactionType.Type = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            workTransactionType.Description = dataReader.GetString(2);
          

      return workTransactionType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [WorkTransactionType] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(WorkTransactionType workTransactionType, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", workTransactionType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransactionType workTransactionType)
      {
      Delete(workTransactionType, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [WorkTransactionType] ";

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
      
        + " Type, "
      
        + " Description "
      

      + " From [WorkTransactionType] ";
      public static List<WorkTransactionType> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<WorkTransactionType> rv = new List<WorkTransactionType>();

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

      public static List<WorkTransactionType> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkTransactionType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<WorkTransactionType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkTransactionType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkTransactionType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkTransactionType> itemsList
      = new List<WorkTransactionType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkTransactionType)
      itemsList.Add(deserializedObject as WorkTransactionType);
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
      public WorkTransactionType(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public WorkTransactionType(
        int 
          iD,String 
          type,String 
          description
        )
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
      
      }
      #endregion
      }

    