
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class Location
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Location ( " +
      
        " LocationId, " +
      
        " Name " +
      
      ") Values (" +
      
        " @LocationId, " +
      
        " @Name " +
      
      ")";

      public static void Insert(Location location)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", location.LocationId);
      
        Database.PutParameter(dbCommand,"@Name", location.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Location>  locationList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Location location in  locationList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@LocationId", location.LocationId);
      
        Database.PutParameter(dbCommand,"@Name", location.Name);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@LocationId",location.LocationId);
      
        Database.UpdateParameter(dbCommand,"@Name",location.Name);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update Location Set "
      
        + " Name = @Name "
      
        + " Where "
        
          + " LocationId = @LocationId "
        
      ;

      public static void Update(Location location)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", location.LocationId);
      
        Database.PutParameter(dbCommand,"@Name", location.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " LocationId, "
      
        + " Name "
      

      + " From Location "

      
        + " Where "
        
          + " LocationId = @LocationId "
        
      ;

      public static Location FindByPrimaryKey(
      int locationId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", locationId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Location not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Location location)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId",location.LocationId);
      

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
      String sql = "select 1 from Location";

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

      public static Location Load(IDataReader dataReader)
      {
      Location location = new Location();

      location.LocationId = dataReader.GetInt32(0);
          location.Name = dataReader.GetString(1);
          

      return location;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Location "

      
        + " Where "
        
          + " LocationId = @LocationId "
        
      ;
      public static void Delete(Location location)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@LocationId", location.LocationId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From Location ";

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
      
        + " Name "
      

      + " From Location ";
      public static List<Location> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<Location> rv = new List<Location>();

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
        List<Location> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<Location> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Location));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(Location item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Location>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Location));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<Location> itemsList
      = new List<Location>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Location)
        itemsList.Add(deserializedObject as Location);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_locationId;
        
          protected String m_name;
        
        #endregion
        
        #region Constructors
        public Location(
        int 
          locationId
         )
        {
        
          m_locationId = locationId;
        
        }
        
        


        public Location(
        int 
          locationId,String 
          name
        )
        {
        
          m_locationId = locationId;
        
          m_name = name;
        
          }


        
      #endregion

      
        [XmlElement]
        public int LocationId
        {
          get { return m_locationId;}
          set { m_locationId = value; }
        }
      
        [XmlElement]
        public String Name
        {
          get { return m_name;}
          set { m_name = value; }
        }
      
      }
      #endregion
      }

    