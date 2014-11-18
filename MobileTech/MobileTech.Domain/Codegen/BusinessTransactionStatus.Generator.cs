
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class BusinessTransactionStatus
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into BusinessTransactionStatus ( " +
      
        " BusinessTransactionStatusId, " +
      
        " Name, " +
      
        " Description " +
      
      ") Values (" +
      
        " @BusinessTransactionStatusId, " +
      
        " @Name, " +
      
        " @Description " +
      
      ")";

      public static void Insert(BusinessTransactionStatus businessTransactionStatus)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@BusinessTransactionStatusId", businessTransactionStatus.BusinessTransactionStatusId);
      
        Database.PutParameter(dbCommand,"@Name", businessTransactionStatus.Name);
      
        Database.PutParameter(dbCommand,"@Description", businessTransactionStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<BusinessTransactionStatus>  businessTransactionStatusList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(BusinessTransactionStatus businessTransactionStatus in  businessTransactionStatusList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@BusinessTransactionStatusId", businessTransactionStatus.BusinessTransactionStatusId);
      
        Database.PutParameter(dbCommand,"@Name", businessTransactionStatus.Name);
      
        Database.PutParameter(dbCommand,"@Description", businessTransactionStatus.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@BusinessTransactionStatusId",businessTransactionStatus.BusinessTransactionStatusId);
      
        Database.UpdateParameter(dbCommand,"@Name",businessTransactionStatus.Name);
      
        Database.UpdateParameter(dbCommand,"@Description",businessTransactionStatus.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update BusinessTransactionStatus Set "
      
        + " Name = @Name, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " BusinessTransactionStatusId = @BusinessTransactionStatusId "
        
      ;

      public static void Update(BusinessTransactionStatus businessTransactionStatus)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@BusinessTransactionStatusId", businessTransactionStatus.BusinessTransactionStatusId);
      
        Database.PutParameter(dbCommand,"@Name", businessTransactionStatus.Name);
      
        Database.PutParameter(dbCommand,"@Description", businessTransactionStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " BusinessTransactionStatusId, "
      
        + " Name, "
      
        + " Description "
      

      + " From BusinessTransactionStatus "

      
        + " Where "
        
          + " BusinessTransactionStatusId = @BusinessTransactionStatusId "
        
      ;

      public static BusinessTransactionStatus FindByPrimaryKey(
      int businessTransactionStatusId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@BusinessTransactionStatusId", businessTransactionStatusId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("BusinessTransactionStatus not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(BusinessTransactionStatus businessTransactionStatus)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@BusinessTransactionStatusId",businessTransactionStatus.BusinessTransactionStatusId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData()
      {
      String sql = "select 1 from BusinessTransactionStatus";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static BusinessTransactionStatus Load(IDataReader dataReader)
      {
      BusinessTransactionStatus businessTransactionStatus = new BusinessTransactionStatus();

      businessTransactionStatus.BusinessTransactionStatusId = dataReader.GetInt16(0);
          businessTransactionStatus.Name = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            businessTransactionStatus.Description = dataReader.GetString(2);
          

      return businessTransactionStatus;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From BusinessTransactionStatus "

      
        + " Where "
        
          + " BusinessTransactionStatusId = @BusinessTransactionStatusId "
        
      ;
      public static void Delete(BusinessTransactionStatus businessTransactionStatus)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@BusinessTransactionStatusId", businessTransactionStatus.BusinessTransactionStatusId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From BusinessTransactionStatus ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " BusinessTransactionStatusId, "
      
        + " Name, "
      
        + " Description "
      

      + " From BusinessTransactionStatus ";
      public static List<BusinessTransactionStatus> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<BusinessTransactionStatus> rv = new List<BusinessTransactionStatus>();

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
      #endregion

      #region Import from file
      
      public static int Import(String xmlFilePath)
      {
        List<BusinessTransactionStatus> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<BusinessTransactionStatus> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(BusinessTransactionStatus));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(BusinessTransactionStatus item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<BusinessTransactionStatus>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(BusinessTransactionStatus));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<BusinessTransactionStatus> itemsList
      = new List<BusinessTransactionStatus>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is BusinessTransactionStatus)
        itemsList.Add(deserializedObject as BusinessTransactionStatus);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_businessTransactionStatusId;
        
          protected String m_name;
        
          protected String m_description;
        
        #endregion
        
        #region Constructors
        public BusinessTransactionStatus(
        int 
          businessTransactionStatusId
         )
        {
        
          m_businessTransactionStatusId = businessTransactionStatusId;
        
        }
        
        


        public BusinessTransactionStatus(
        int 
          businessTransactionStatusId,String 
          name,String 
          description
        )
        {
        
          m_businessTransactionStatusId = businessTransactionStatusId;
        
          m_name = name;
        
          m_description = description;
        
          }


        
      #endregion

      
        [XmlElement]
        public int BusinessTransactionStatusId
        {
          get { return m_businessTransactionStatusId;}
          set { m_businessTransactionStatusId = value; }
        }
      
        [XmlElement]
        public String Name
        {
          get { return m_name;}
          set { m_name = value; }
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

    