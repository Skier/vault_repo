
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class RouteType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into RouteType ( " +
      
        " RouteTypeId, " +
      
        " Name, " +
      
        " Description " +
      
      ") Values (" +
      
        " @RouteTypeId, " +
      
        " @Name, " +
      
        " @Description " +
      
      ")";

      public static void Insert(RouteType routeType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@RouteTypeId", routeType.RouteTypeId);
      
        Database.PutParameter(dbCommand,"@Name", routeType.Name);
      
        Database.PutParameter(dbCommand,"@Description", routeType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<RouteType>  routeTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(RouteType routeType in  routeTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@RouteTypeId", routeType.RouteTypeId);
      
        Database.PutParameter(dbCommand,"@Name", routeType.Name);
      
        Database.PutParameter(dbCommand,"@Description", routeType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@RouteTypeId",routeType.RouteTypeId);
      
        Database.UpdateParameter(dbCommand,"@Name",routeType.Name);
      
        Database.UpdateParameter(dbCommand,"@Description",routeType.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update RouteType Set "
      
        + " Name = @Name, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " RouteTypeId = @RouteTypeId "
        
      ;

      public static void Update(RouteType routeType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@RouteTypeId", routeType.RouteTypeId);
      
        Database.PutParameter(dbCommand,"@Name", routeType.Name);
      
        Database.PutParameter(dbCommand,"@Description", routeType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " RouteTypeId, "
      
        + " Name, "
      
        + " Description "
      

      + " From RouteType "

      
        + " Where "
        
          + " RouteTypeId = @RouteTypeId "
        
      ;

      public static RouteType FindByPrimaryKey(
      int routeTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@RouteTypeId", routeTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("RouteType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(RouteType routeType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@RouteTypeId",routeType.RouteTypeId);
      

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
      String sql = "select 1 from RouteType";

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

      public static RouteType Load(IDataReader dataReader)
      {
      RouteType routeType = new RouteType();

      routeType.RouteTypeId = dataReader.GetInt16(0);
          routeType.Name = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            routeType.Description = dataReader.GetString(2);
          

      return routeType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From RouteType "

      
        + " Where "
        
          + " RouteTypeId = @RouteTypeId "
        
      ;
      public static void Delete(RouteType routeType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@RouteTypeId", routeType.RouteTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From RouteType ";

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

      
        + " RouteTypeId, "
      
        + " Name, "
      
        + " Description "
      

      + " From RouteType ";
      public static List<RouteType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<RouteType> rv = new List<RouteType>();

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
        List<RouteType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<RouteType> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(RouteType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<RouteType>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteType));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<RouteType> itemsList
      = new List<RouteType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is RouteType)
        itemsList.Add(deserializedObject as RouteType);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_routeTypeId;
        
          protected String m_name;
        
          protected String m_description;
        
        #endregion
        
        #region Constructors
        public RouteType(
        int 
          routeTypeId
         )
        {
        
          m_routeTypeId = routeTypeId;
        
        }
        
        


        public RouteType(
        int 
          routeTypeId,String 
          name,String 
          description
        )
        {
        
          m_routeTypeId = routeTypeId;
        
          m_name = name;
        
          m_description = description;
        
          }


        
      #endregion

      
        [XmlElement]
        public int RouteTypeId
        {
          get { return m_routeTypeId;}
          set { m_routeTypeId = value; }
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

    