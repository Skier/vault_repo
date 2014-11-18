
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class Product
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Product ( " +
      
        " LocationId, " +
      
        " RouteNumber, " +
      
        " ItemNumber " +
      
      ") Values (" +
      
        " @LocationId, " +
      
        " @RouteNumber, " +
      
        " @ItemNumber " +
      
      ")";

      public static void Insert(Product product)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", product.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", product.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", product.ItemNumber);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Product>  productList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Product product in  productList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@LocationId", product.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", product.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", product.ItemNumber);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@LocationId",product.LocationId);
      
        Database.UpdateParameter(dbCommand,"@RouteNumber",product.RouteNumber);
      
        Database.UpdateParameter(dbCommand,"@ItemNumber",product.ItemNumber);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update Product Set "
      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " ItemNumber = @ItemNumber "
        
      ;

      public static void Update(Product product)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", product.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", product.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", product.ItemNumber);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " LocationId, "
      
        + " RouteNumber, "
      
        + " ItemNumber "
      

      + " From Product "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " ItemNumber = @ItemNumber "
        
      ;

      public static Product FindByPrimaryKey(
      int locationId,int routeNumber,String itemNumber
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", locationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", itemNumber);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Product not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Product product)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId",product.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber",product.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber",product.ItemNumber);
      

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
      String sql = "select 1 from Product";

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

      public static Product Load(IDataReader dataReader)
      {
      Product product = new Product();

      product.LocationId = dataReader.GetInt32(0);
          product.RouteNumber = dataReader.GetInt32(1);
          product.ItemNumber = dataReader.GetString(2);
          

      return product;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Product "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " ItemNumber = @ItemNumber "
        
      ;
      public static void Delete(Product product)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@LocationId", product.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", product.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", product.ItemNumber);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From Product ";

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

      
        + " LocationId, "
      
        + " RouteNumber, "
      
        + " ItemNumber "
      

      + " From Product ";
      public static List<Product> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<Product> rv = new List<Product>();

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
        List<Product> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<Product> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Product));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(Product item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Product>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Product));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<Product> itemsList
      = new List<Product>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Product)
        itemsList.Add(deserializedObject as Product);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_locationId;
        
          protected int m_routeNumber;
        
          protected String m_itemNumber;
        
        #endregion
        
        #region Constructors
        public Product(
        int 
          locationId,int 
          routeNumber,String 
          itemNumber
         )
        {
        
          m_locationId = locationId;
        
          m_routeNumber = routeNumber;
        
          m_itemNumber = itemNumber;
        
        }
        
        
      #endregion

      
        [XmlElement]
        public int LocationId
        {
          get { return m_locationId;}
          set { m_locationId = value; }
        }
      
        [XmlElement]
        public int RouteNumber
        {
          get { return m_routeNumber;}
          set { m_routeNumber = value; }
        }
      
        [XmlElement]
        public String ItemNumber
        {
          get { return m_itemNumber;}
          set { m_itemNumber = value; }
        }
      
      }
      #endregion
      }

    