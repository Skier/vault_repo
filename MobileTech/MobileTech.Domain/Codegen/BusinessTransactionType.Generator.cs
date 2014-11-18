
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class BusinessTransactionType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into BusinessTransactionType ( " +
      
        " BusinessTransactionTypeId, " +
      
        " Name, " +
      
        " Description " +
      
      ") Values (" +
      
        " @BusinessTransactionTypeId, " +
      
        " @Name, " +
      
        " @Description " +
      
      ")";

      public static void Insert(BusinessTransactionType businessTransactionType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@BusinessTransactionTypeId", businessTransactionType.BusinessTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@Name", businessTransactionType.Name);
      
        Database.PutParameter(dbCommand,"@Description", businessTransactionType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<BusinessTransactionType>  businessTransactionTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(BusinessTransactionType businessTransactionType in  businessTransactionTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@BusinessTransactionTypeId", businessTransactionType.BusinessTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@Name", businessTransactionType.Name);
      
        Database.PutParameter(dbCommand,"@Description", businessTransactionType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@BusinessTransactionTypeId",businessTransactionType.BusinessTransactionTypeId);
      
        Database.UpdateParameter(dbCommand,"@Name",businessTransactionType.Name);
      
        Database.UpdateParameter(dbCommand,"@Description",businessTransactionType.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update BusinessTransactionType Set "
      
        + " Name = @Name, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " BusinessTransactionTypeId = @BusinessTransactionTypeId "
        
      ;

      public static void Update(BusinessTransactionType businessTransactionType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@BusinessTransactionTypeId", businessTransactionType.BusinessTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@Name", businessTransactionType.Name);
      
        Database.PutParameter(dbCommand,"@Description", businessTransactionType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " BusinessTransactionTypeId, "
      
        + " Name, "
      
        + " Description "
      

      + " From BusinessTransactionType "

      
        + " Where "
        
          + " BusinessTransactionTypeId = @BusinessTransactionTypeId "
        
      ;

      public static BusinessTransactionType FindByPrimaryKey(
      int businessTransactionTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@BusinessTransactionTypeId", businessTransactionTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("BusinessTransactionType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(BusinessTransactionType businessTransactionType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@BusinessTransactionTypeId",businessTransactionType.BusinessTransactionTypeId);
      

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
      String sql = "select 1 from BusinessTransactionType";

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

      public static BusinessTransactionType Load(IDataReader dataReader)
      {
      BusinessTransactionType businessTransactionType = new BusinessTransactionType();

      businessTransactionType.BusinessTransactionTypeId = dataReader.GetInt16(0);
          businessTransactionType.Name = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            businessTransactionType.Description = dataReader.GetString(2);
          

      return businessTransactionType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From BusinessTransactionType "

      
        + " Where "
        
          + " BusinessTransactionTypeId = @BusinessTransactionTypeId "
        
      ;
      public static void Delete(BusinessTransactionType businessTransactionType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@BusinessTransactionTypeId", businessTransactionType.BusinessTransactionTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From BusinessTransactionType ";

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

      
        + " BusinessTransactionTypeId, "
      
        + " Name, "
      
        + " Description "
      

      + " From BusinessTransactionType ";
      public static List<BusinessTransactionType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<BusinessTransactionType> rv = new List<BusinessTransactionType>();

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
        List<BusinessTransactionType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<BusinessTransactionType> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(BusinessTransactionType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(BusinessTransactionType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<BusinessTransactionType>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(BusinessTransactionType));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<BusinessTransactionType> itemsList
      = new List<BusinessTransactionType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is BusinessTransactionType)
        itemsList.Add(deserializedObject as BusinessTransactionType);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_businessTransactionTypeId;
        
          protected String m_name;
        
          protected String m_description;
        
        #endregion
        
        #region Constructors
        public BusinessTransactionType(
        int 
          businessTransactionTypeId
         )
        {
        
          m_businessTransactionTypeId = businessTransactionTypeId;
        
        }
        
        


        public BusinessTransactionType(
        int 
          businessTransactionTypeId,String 
          name,String 
          description
        )
        {
        
          m_businessTransactionTypeId = businessTransactionTypeId;
        
          m_name = name;
        
          m_description = description;
        
          }


        
      #endregion

      
        [XmlElement]
        public int BusinessTransactionTypeId
        {
          get { return m_businessTransactionTypeId;}
          set { m_businessTransactionTypeId = value; }
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

    