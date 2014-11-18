
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class CustomerTransactionType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into CustomerTransactionType ( " +
      
        " CustomerTransactionTypeId, " +
      
        " Name, " +
      
        " Description " +
      
      ") Values (" +
      
        " @CustomerTransactionTypeId, " +
      
        " @Name, " +
      
        " @Description " +
      
      ")";

      public static void Insert(CustomerTransactionType customerTransactionType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@CustomerTransactionTypeId", customerTransactionType.CustomerTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@Name", customerTransactionType.Name);
      
        Database.PutParameter(dbCommand,"@Description", customerTransactionType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<CustomerTransactionType>  customerTransactionTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(CustomerTransactionType customerTransactionType in  customerTransactionTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@CustomerTransactionTypeId", customerTransactionType.CustomerTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@Name", customerTransactionType.Name);
      
        Database.PutParameter(dbCommand,"@Description", customerTransactionType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@CustomerTransactionTypeId",customerTransactionType.CustomerTransactionTypeId);
      
        Database.UpdateParameter(dbCommand,"@Name",customerTransactionType.Name);
      
        Database.UpdateParameter(dbCommand,"@Description",customerTransactionType.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update CustomerTransactionType Set "
      
        + " Name = @Name, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " CustomerTransactionTypeId = @CustomerTransactionTypeId "
        
      ;

      public static void Update(CustomerTransactionType customerTransactionType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@CustomerTransactionTypeId", customerTransactionType.CustomerTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@Name", customerTransactionType.Name);
      
        Database.PutParameter(dbCommand,"@Description", customerTransactionType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " CustomerTransactionTypeId, "
      
        + " Name, "
      
        + " Description "
      

      + " From CustomerTransactionType "

      
        + " Where "
        
          + " CustomerTransactionTypeId = @CustomerTransactionTypeId "
        
      ;

      public static CustomerTransactionType FindByPrimaryKey(
      int customerTransactionTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CustomerTransactionTypeId", customerTransactionTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("CustomerTransactionType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(CustomerTransactionType customerTransactionType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CustomerTransactionTypeId",customerTransactionType.CustomerTransactionTypeId);
      

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
      String sql = "select 1 from CustomerTransactionType";

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

      public static CustomerTransactionType Load(IDataReader dataReader)
      {
      CustomerTransactionType customerTransactionType = new CustomerTransactionType();

      customerTransactionType.CustomerTransactionTypeId = dataReader.GetInt16(0);
          customerTransactionType.Name = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            customerTransactionType.Description = dataReader.GetString(2);
          

      return customerTransactionType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From CustomerTransactionType "

      
        + " Where "
        
          + " CustomerTransactionTypeId = @CustomerTransactionTypeId "
        
      ;
      public static void Delete(CustomerTransactionType customerTransactionType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@CustomerTransactionTypeId", customerTransactionType.CustomerTransactionTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From CustomerTransactionType ";

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

      
        + " CustomerTransactionTypeId, "
      
        + " Name, "
      
        + " Description "
      

      + " From CustomerTransactionType ";
      public static List<CustomerTransactionType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<CustomerTransactionType> rv = new List<CustomerTransactionType>();

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
        List<CustomerTransactionType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<CustomerTransactionType> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerTransactionType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(CustomerTransactionType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CustomerTransactionType>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerTransactionType));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<CustomerTransactionType> itemsList
      = new List<CustomerTransactionType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CustomerTransactionType)
        itemsList.Add(deserializedObject as CustomerTransactionType);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_customerTransactionTypeId;
        
          protected String m_name;
        
          protected String m_description;
        
        #endregion
        
        #region Constructors
        public CustomerTransactionType(
        int 
          customerTransactionTypeId
         )
        {
        
          m_customerTransactionTypeId = customerTransactionTypeId;
        
        }
        
        


        public CustomerTransactionType(
        int 
          customerTransactionTypeId,String 
          name,String 
          description
        )
        {
        
          m_customerTransactionTypeId = customerTransactionTypeId;
        
          m_name = name;
        
          m_description = description;
        
          }


        
      #endregion

      
        [XmlElement]
        public int CustomerTransactionTypeId
        {
          get { return m_customerTransactionTypeId;}
          set { m_customerTransactionTypeId = value; }
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

    