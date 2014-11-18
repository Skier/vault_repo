
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Servman.Domain
      {


      public partial class CustomerCounter
      {

      #region Store


      #region Insert

          private const String SqlInsert = "Insert Into cust_id ( " +
      
        " cust_id " +
      
      ") Values (" +
      
        " ? " +
      
      ")";

      public static void Insert(CustomerCounter customerCounter)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@cust_id", customerCounter.cust_id);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<CustomerCounter>  customerCounterList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(CustomerCounter customerCounter in  customerCounterList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@cust_id", customerCounter.cust_id);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@cust_id",customerCounter.cust_id);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


          private const String SqlUpdate = "Update cust_id Set "
      
        + " Where "

          + " cust_id.cust_id = ?  "
        
      ;

      public static void Update(CustomerCounter customerCounter)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@cust_id", customerCounter.cust_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "


        + " cust_id.cust_id "


      + " From cust_id "

      
        + " Where "

          + " cust_id.cust_id = ?  "
        
      ;

      public static CustomerCounter FindByPrimaryKey(
      String cust_id
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@cust_id", cust_id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("cust_id not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(CustomerCounter customerCounter)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@cust_id",customerCounter.cust_id);
      

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
          String sql = "select 1 from cust_id";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, ConnectionKeyEnum.Servman))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static CustomerCounter Load(IDataReader dataReader)
      {
      CustomerCounter customerCounter = new CustomerCounter();

      customerCounter.cust_id = dataReader.GetString(0);
          

      return customerCounter;
      }

      #endregion

      #region Delete
          private const String SqlDelete = "Delete From [cust_id] "

      
        + " Where "
        
          + " cust_id = ?  "
        
      ;
      public static void Delete(CustomerCounter customerCounter)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@cust_id", customerCounter.cust_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

          private const String SqlDeleteAll = "Delete From [cust_id] ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, ConnectionKeyEnum.Servman))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "


        + " cust_id.cust_id "


      + " From cust_id ";
      public static List<CustomerCounter> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<CustomerCounter> rv = new List<CustomerCounter>();

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
      List<CustomerCounter> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<CustomerCounter> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerCounter));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(CustomerCounter item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CustomerCounter>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerCounter));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<CustomerCounter> itemsList
      = new List<CustomerCounter>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CustomerCounter)
      itemsList.Add(deserializedObject as CustomerCounter);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_cust_id;
      
      #endregion

      #region Constructors
      public CustomerCounter(
      String 
          cust_id
      )
      {
      
        m_cust_id = cust_id;
      
      }

      
      #endregion

      
        [XmlElement]
        public String cust_id
        {
        get { return m_cust_id;}
        set { m_cust_id = value; }
        }
      
      }
      #endregion
      }

    