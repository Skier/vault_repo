
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class RouteOptionDescription
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into RouteOptionDescription ( " +
      
        " OptionName, " +
      
        " OptionValue, " +
      
        " Description " +
      
      ") Values (" +
      
        " @OptionName, " +
      
        " @OptionValue, " +
      
        " @Description " +
      
      ")";

      public static void Insert(RouteOptionDescription routeOptionDescription)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@OptionName", routeOptionDescription.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue", routeOptionDescription.OptionValue);
      
        Database.PutParameter(dbCommand,"@Description", routeOptionDescription.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<RouteOptionDescription>  routeOptionDescriptionList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(RouteOptionDescription routeOptionDescription in  routeOptionDescriptionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@OptionName", routeOptionDescription.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue", routeOptionDescription.OptionValue);
      
        Database.PutParameter(dbCommand,"@Description", routeOptionDescription.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@OptionName",routeOptionDescription.OptionName);
      
        Database.UpdateParameter(dbCommand,"@OptionValue",routeOptionDescription.OptionValue);
      
        Database.UpdateParameter(dbCommand,"@Description",routeOptionDescription.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update RouteOptionDescription Set "
      
        + " Description = @Description "
      
        + " Where "
        
          + " OptionName = @OptionName and  "
        
          + " OptionValue = @OptionValue "
        
      ;

      public static void Update(RouteOptionDescription routeOptionDescription)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@OptionName", routeOptionDescription.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue", routeOptionDescription.OptionValue);
      
        Database.PutParameter(dbCommand,"@Description", routeOptionDescription.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " OptionName, "
      
        + " OptionValue, "
      
        + " Description "
      

      + " From RouteOptionDescription "

      
        + " Where "
        
          + " OptionName = @OptionName and  "
        
          + " OptionValue = @OptionValue "
        
      ;

      public static RouteOptionDescription FindByPrimaryKey(
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
      throw new DataNotFoundException("RouteOptionDescription not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(RouteOptionDescription routeOptionDescription)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@OptionName",routeOptionDescription.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue",routeOptionDescription.OptionValue);
      

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
      String sql = "select 1 from RouteOptionDescription";

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

      public static RouteOptionDescription Load(IDataReader dataReader)
      {
      RouteOptionDescription routeOptionDescription = new RouteOptionDescription();

      routeOptionDescription.OptionName = dataReader.GetString(0);
          routeOptionDescription.OptionValue = dataReader.GetInt32(1);
          routeOptionDescription.Description = dataReader.GetString(2);
          

      return routeOptionDescription;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From RouteOptionDescription "

      
        + " Where "
        
          + " OptionName = @OptionName and  "
        
          + " OptionValue = @OptionValue "
        
      ;
      public static void Delete(RouteOptionDescription routeOptionDescription)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@OptionName", routeOptionDescription.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue", routeOptionDescription.OptionValue);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From RouteOptionDescription ";

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
      

      + " From RouteOptionDescription ";
      public static List<RouteOptionDescription> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<RouteOptionDescription> rv = new List<RouteOptionDescription>();

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
        List<RouteOptionDescription> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<RouteOptionDescription> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteOptionDescription));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(RouteOptionDescription item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<RouteOptionDescription>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteOptionDescription));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<RouteOptionDescription> itemsList
      = new List<RouteOptionDescription>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is RouteOptionDescription)
        itemsList.Add(deserializedObject as RouteOptionDescription);
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
        public RouteOptionDescription(
        String 
          optionName,int 
          optionValue
         )
        {
        
          m_optionName = optionName;
        
          m_optionValue = optionValue;
        
        }
        
        


        public RouteOptionDescription(
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

    