
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class CustomerOptionDescription
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into CustomerOptionDescription ( " +
      
        " OptionName, " +
      
        " OptionValue, " +
      
        " Description " +
      
      ") Values (" +
      
        " @OptionName, " +
      
        " @OptionValue, " +
      
        " @Description " +
      
      ")";

      public static void Insert(CustomerOptionDescription customerOptionDescription)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@OptionName", customerOptionDescription.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue", customerOptionDescription.OptionValue);
      
        Database.PutParameter(dbCommand,"@Description", customerOptionDescription.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<CustomerOptionDescription>  customerOptionDescriptionList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(CustomerOptionDescription customerOptionDescription in  customerOptionDescriptionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@OptionName", customerOptionDescription.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue", customerOptionDescription.OptionValue);
      
        Database.PutParameter(dbCommand,"@Description", customerOptionDescription.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@OptionName",customerOptionDescription.OptionName);
      
        Database.UpdateParameter(dbCommand,"@OptionValue",customerOptionDescription.OptionValue);
      
        Database.UpdateParameter(dbCommand,"@Description",customerOptionDescription.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update CustomerOptionDescription Set "
      
        + " Description = @Description "
      
        + " Where "
        
          + " OptionName = @OptionName and  "
        
          + " OptionValue = @OptionValue "
        
      ;

      public static void Update(CustomerOptionDescription customerOptionDescription)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@OptionName", customerOptionDescription.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue", customerOptionDescription.OptionValue);
      
        Database.PutParameter(dbCommand,"@Description", customerOptionDescription.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " OptionName, "
      
        + " OptionValue, "
      
        + " Description "
      

      + " From CustomerOptionDescription "

      
        + " Where "
        
          + " OptionName = @OptionName and  "
        
          + " OptionValue = @OptionValue "
        
      ;

      public static CustomerOptionDescription FindByPrimaryKey(
      String optionName,int optionValue
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@OptionName", optionName);
      
        Database.PutParameter(dbCommand,"@OptionValue", optionValue);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("CustomerOptionDescription not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(CustomerOptionDescription customerOptionDescription)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@OptionName",customerOptionDescription.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue",customerOptionDescription.OptionValue);
      

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
      String sql = "select 1 from CustomerOptionDescription";

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

      public static CustomerOptionDescription Load(IDataReader dataReader)
      {
      CustomerOptionDescription customerOptionDescription = new CustomerOptionDescription();

      customerOptionDescription.OptionName = dataReader.GetString(0);
          customerOptionDescription.OptionValue = dataReader.GetInt32(1);
          customerOptionDescription.Description = dataReader.GetString(2);
          

      return customerOptionDescription;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From CustomerOptionDescription "

      
        + " Where "
        
          + " OptionName = @OptionName and  "
        
          + " OptionValue = @OptionValue "
        
      ;
      public static void Delete(CustomerOptionDescription customerOptionDescription)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@OptionName", customerOptionDescription.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue", customerOptionDescription.OptionValue);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From CustomerOptionDescription ";

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

      
        + " OptionName, "
      
        + " OptionValue, "
      
        + " Description "
      

      + " From CustomerOptionDescription ";
      public static List<CustomerOptionDescription> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<CustomerOptionDescription> rv = new List<CustomerOptionDescription>();

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
        List<CustomerOptionDescription> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<CustomerOptionDescription> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerOptionDescription));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(CustomerOptionDescription item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CustomerOptionDescription>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerOptionDescription));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<CustomerOptionDescription> itemsList
      = new List<CustomerOptionDescription>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CustomerOptionDescription)
        itemsList.Add(deserializedObject as CustomerOptionDescription);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected String m_optionName;
        
          protected int m_optionValue;
        
          protected String m_description;
        
        #endregion
        
        #region Constructors
        public CustomerOptionDescription(
        String 
          optionName,int 
          optionValue
         )
        {
        
          m_optionName = optionName;
        
          m_optionValue = optionValue;
        
        }
        
        


        public CustomerOptionDescription(
        String 
          optionName,int 
          optionValue,String 
          description
        )
        {
        
          m_optionName = optionName;
        
          m_optionValue = optionValue;
        
          m_description = description;
        
          }


        
      #endregion

      
        [XmlElement]
        public String OptionName
        {
          get { return m_optionName;}
          set { m_optionName = value; }
        }
      
        [XmlElement]
        public int OptionValue
        {
          get { return m_optionValue;}
          set { m_optionValue = value; }
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

    