
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class CustomerOption
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into CustomerOption ( " +
      
        " CustomerId, " +
      
        " OptionName, " +
      
        " OptionValue " +
      
      ") Values (" +
      
        " @CustomerId, " +
      
        " @OptionName, " +
      
        " @OptionValue " +
      
      ")";

      public static void Insert(CustomerOption customerOption)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@CustomerId", customerOption.CustomerId);
      
        Database.PutParameter(dbCommand,"@OptionName", customerOption.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue", customerOption.OptionValue);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<CustomerOption>  customerOptionList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(CustomerOption customerOption in  customerOptionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@CustomerId", customerOption.CustomerId);
      
        Database.PutParameter(dbCommand,"@OptionName", customerOption.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue", customerOption.OptionValue);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@CustomerId",customerOption.CustomerId);
      
        Database.UpdateParameter(dbCommand,"@OptionName",customerOption.OptionName);
      
        Database.UpdateParameter(dbCommand,"@OptionValue",customerOption.OptionValue);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update CustomerOption Set "
      
        + " OptionValue = @OptionValue "
      
        + " Where "
        
          + " CustomerId = @CustomerId and  "
        
          + " OptionName = @OptionName "
        
      ;

      public static void Update(CustomerOption customerOption)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@CustomerId", customerOption.CustomerId);
      
        Database.PutParameter(dbCommand,"@OptionName", customerOption.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue", customerOption.OptionValue);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " CustomerId, "
      
        + " OptionName, "
      
        + " OptionValue "
      

      + " From CustomerOption "

      
        + " Where "
        
          + " CustomerId = @CustomerId and  "
        
          + " OptionName = @OptionName "
        
      ;

      public static CustomerOption FindByPrimaryKey(
      int customerId,String optionName
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CustomerId", customerId);
      
        Database.PutParameter(dbCommand,"@OptionName", optionName);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("CustomerOption not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(CustomerOption customerOption)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CustomerId",customerOption.CustomerId);
      
        Database.PutParameter(dbCommand,"@OptionName",customerOption.OptionName);
      

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
      String sql = "select 1 from CustomerOption";

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

      public static CustomerOption Load(IDataReader dataReader)
      {
      CustomerOption customerOption = new CustomerOption();

      customerOption.CustomerId = dataReader.GetInt32(0);
          customerOption.OptionName = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            customerOption.OptionValue = dataReader.GetInt32(2);
          

      return customerOption;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From CustomerOption "

      
        + " Where "
        
          + " CustomerId = @CustomerId and  "
        
          + " OptionName = @OptionName "
        
      ;
      public static void Delete(CustomerOption customerOption)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@CustomerId", customerOption.CustomerId);
      
        Database.PutParameter(dbCommand,"@OptionName", customerOption.OptionName);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From CustomerOption ";

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

      
        + " CustomerId, "
      
        + " OptionName, "
      
        + " OptionValue "
      

      + " From CustomerOption ";
      public static List<CustomerOption> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<CustomerOption> rv = new List<CustomerOption>();

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
        List<CustomerOption> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<CustomerOption> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerOption));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(CustomerOption item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CustomerOption>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerOption));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<CustomerOption> itemsList
      = new List<CustomerOption>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CustomerOption)
        itemsList.Add(deserializedObject as CustomerOption);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_customerId;
        
          protected String m_optionName;
        
          protected int? m_optionValue;
        
        #endregion
        
        #region Constructors
        public CustomerOption(
        int 
          customerId,String 
          optionName
         )
        {
        
          m_customerId = customerId;
        
          m_optionName = optionName;
        
        }
        
        


        public CustomerOption(
        int 
          customerId,String 
          optionName,int? 
          optionValue
        )
        {
        
          m_customerId = customerId;
        
          m_optionName = optionName;
        
          m_optionValue = optionValue;
        
          }


        
      #endregion

      
        [XmlElement]
        public int CustomerId
        {
          get { return m_customerId;}
          set { m_customerId = value; }
        }
      
        [XmlElement]
        public String OptionName
        {
          get { return m_optionName;}
          set { m_optionName = value; }
        }
      
        [XmlElement]
        public int? OptionValue
        {
          get { return m_optionValue;}
          set { m_optionValue = value; }
        }
      
      }
      #endregion
      }

    