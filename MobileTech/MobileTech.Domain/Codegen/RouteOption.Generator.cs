
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class RouteOption
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into RouteOption ( " +
      
        " LocationId, " +
      
        " RouteNumber, " +
      
        " OptionName, " +
      
        " OptionValue " +
      
      ") Values (" +
      
        " @LocationId, " +
      
        " @RouteNumber, " +
      
        " @OptionName, " +
      
        " @OptionValue " +
      
      ")";

      public static void Insert(RouteOption routeOption)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", routeOption.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeOption.RouteNumber);
      
        Database.PutParameter(dbCommand,"@OptionName", routeOption.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue", routeOption.OptionValue);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<RouteOption>  routeOptionList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(RouteOption routeOption in  routeOptionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@LocationId", routeOption.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeOption.RouteNumber);
      
        Database.PutParameter(dbCommand,"@OptionName", routeOption.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue", routeOption.OptionValue);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@LocationId",routeOption.LocationId);
      
        Database.UpdateParameter(dbCommand,"@RouteNumber",routeOption.RouteNumber);
      
        Database.UpdateParameter(dbCommand,"@OptionName",routeOption.OptionName);
      
        Database.UpdateParameter(dbCommand,"@OptionValue",routeOption.OptionValue);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update RouteOption Set "
      
        + " OptionValue = @OptionValue "
      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " OptionName = @OptionName "
        
      ;

      public static void Update(RouteOption routeOption)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", routeOption.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeOption.RouteNumber);
      
        Database.PutParameter(dbCommand,"@OptionName", routeOption.OptionName);
      
        Database.PutParameter(dbCommand,"@OptionValue", routeOption.OptionValue);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " LocationId, "
      
        + " RouteNumber, "
      
        + " OptionName, "
      
        + " OptionValue "
      

      + " From RouteOption "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " OptionName = @OptionName "
        
      ;

      public static RouteOption FindByPrimaryKey(
      int locationId,int routeNumber,String optionName
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", locationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeNumber);
      
        Database.PutParameter(dbCommand,"@OptionName", optionName);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("RouteOption not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(RouteOption routeOption)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId",routeOption.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber",routeOption.RouteNumber);
      
        Database.PutParameter(dbCommand,"@OptionName",routeOption.OptionName);
      

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
      String sql = "select 1 from RouteOption";

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

      public static RouteOption Load(IDataReader dataReader)
      {
      RouteOption routeOption = new RouteOption();

      routeOption.LocationId = dataReader.GetInt32(0);
          routeOption.RouteNumber = dataReader.GetInt32(1);
          routeOption.OptionName = dataReader.GetString(2);
          routeOption.OptionValue = dataReader.GetInt32(3);
          

      return routeOption;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From RouteOption "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " OptionName = @OptionName "
        
      ;
      public static void Delete(RouteOption routeOption)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@LocationId", routeOption.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeOption.RouteNumber);
      
        Database.PutParameter(dbCommand,"@OptionName", routeOption.OptionName);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From RouteOption ";

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
      
        + " OptionName, "
      
        + " OptionValue "
      

      + " From RouteOption ";
      public static List<RouteOption> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<RouteOption> rv = new List<RouteOption>();

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
        List<RouteOption> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<RouteOption> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteOption));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(RouteOption item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<RouteOption>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteOption));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<RouteOption> itemsList
      = new List<RouteOption>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is RouteOption)
        itemsList.Add(deserializedObject as RouteOption);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_locationId;
        
          protected int m_routeNumber;
        
          protected String m_optionName;
        
          protected int m_optionValue;
        
        #endregion
        
        #region Constructors
        public RouteOption(
        int 
          locationId,int 
          routeNumber,String 
          optionName
         )
        {
        
          m_locationId = locationId;
        
          m_routeNumber = routeNumber;
        
          m_optionName = optionName;
        
        }
        
        


        public RouteOption(
        int 
          locationId,int 
          routeNumber,String 
          optionName,int 
          optionValue
        )
        {
        
          m_locationId = locationId;
        
          m_routeNumber = routeNumber;
        
          m_optionName = optionName;
        
          m_optionValue = optionValue;
        
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
      
      }
      #endregion
      }

    