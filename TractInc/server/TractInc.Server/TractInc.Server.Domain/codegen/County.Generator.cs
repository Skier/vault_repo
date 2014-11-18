
    using System;
    using System.Data;
    using System.Collections.Generic;
    using TractInc.Server.Data;
    using TractInc.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace TractInc.Server.Domain
      {


      public partial class County
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [County] ( " +
      
        " Name, " +
      
        " StateId, " +
      
        " StateName, " +
      
        " StateFips, " +
      
        " CountyFips, " +
      
        " Fips " +
      
      ") Values (" +
      
        " @Name, " +
      
        " @StateId, " +
      
        " @StateName, " +
      
        " @StateFips, " +
      
        " @CountyFips, " +
      
        " @Fips " +
      
      ")";

      public static void Insert(County county)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@Name", county.Name);
      
        Database.PutParameter(dbCommand,"@StateId", county.StateId);
      
        Database.PutParameter(dbCommand,"@StateName", county.StateName);
      
        Database.PutParameter(dbCommand,"@StateFips", county.StateFips);
      
        Database.PutParameter(dbCommand,"@CountyFips", county.CountyFips);
      
        Database.PutParameter(dbCommand,"@Fips", county.Fips);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          county.CountyId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<County>  countyList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(County county in  countyList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@Name", county.Name);
      
        Database.PutParameter(dbCommand,"@StateId", county.StateId);
      
        Database.PutParameter(dbCommand,"@StateName", county.StateName);
      
        Database.PutParameter(dbCommand,"@StateFips", county.StateFips);
      
        Database.PutParameter(dbCommand,"@CountyFips", county.CountyFips);
      
        Database.PutParameter(dbCommand,"@Fips", county.Fips);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@Name",county.Name);
      
        Database.UpdateParameter(dbCommand,"@StateId",county.StateId);
      
        Database.UpdateParameter(dbCommand,"@StateName",county.StateName);
      
        Database.UpdateParameter(dbCommand,"@StateFips",county.StateFips);
      
        Database.UpdateParameter(dbCommand,"@CountyFips",county.CountyFips);
      
        Database.UpdateParameter(dbCommand,"@Fips",county.Fips);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        county.CountyId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [County] Set "
      
        + " Name = @Name, "
      
        + " StateId = @StateId, "
      
        + " StateName = @StateName, "
      
        + " StateFips = @StateFips, "
      
        + " CountyFips = @CountyFips, "
      
        + " Fips = @Fips "
      
        + " Where "
        
          + " CountyId = @CountyId "
        
      ;

      public static void Update(County county)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@CountyId", county.CountyId);
      
        Database.PutParameter(dbCommand,"@Name", county.Name);
      
        Database.PutParameter(dbCommand,"@StateId", county.StateId);
      
        Database.PutParameter(dbCommand,"@StateName", county.StateName);
      
        Database.PutParameter(dbCommand,"@StateFips", county.StateFips);
      
        Database.PutParameter(dbCommand,"@CountyFips", county.CountyFips);
      
        Database.PutParameter(dbCommand,"@Fips", county.Fips);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " CountyId, "
      
        + " Name, "
      
        + " StateId, "
      
        + " StateName, "
      
        + " StateFips, "
      
        + " CountyFips, "
      
        + " Fips "
      

      + " From [County] "

      
        + " Where "
        
          + " CountyId = @CountyId "
        
      ;

      public static County FindByPrimaryKey(
      int countyId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CountyId", countyId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("County not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(County county)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CountyId",county.CountyId);
      

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
      String sql = "select 1 from County";

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

      public static County Load(IDataReader dataReader)
      {
      County county = new County();

      county.CountyId = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            county.Name = dataReader.GetString(1);
          county.StateId = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            county.StateName = dataReader.GetString(3);
          
            if(!dataReader.IsDBNull(4))
            county.StateFips = dataReader.GetString(4);
          
            if(!dataReader.IsDBNull(5))
            county.CountyFips = dataReader.GetString(5);
          
            if(!dataReader.IsDBNull(6))
            county.Fips = dataReader.GetString(6);
          

      return county;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [County] "

      
        + " Where "
        
          + " CountyId = @CountyId "
        
      ;
      public static void Delete(County county)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@CountyId", county.CountyId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [County] ";

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

      
        + " CountyId, "
      
        + " Name, "
      
        + " StateId, "
      
        + " StateName, "
      
        + " StateFips, "
      
        + " CountyFips, "
      
        + " Fips "
      

      + " From [County] ";
      public static List<County> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<County> rv = new List<County>();

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
      List<County> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<County> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(County));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(County item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<County>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(County));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<County> itemsList
      = new List<County>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is County)
      itemsList.Add(deserializedObject as County);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_countyId;
      
        protected String m_name;
      
        protected int m_stateId;
      
        protected String m_stateName;
      
        protected String m_stateFips;
      
        protected String m_countyFips;
      
        protected String m_fips;
      
      #endregion

      #region Constructors
      public County(
      int 
          countyId
      )
      {
      
        m_countyId = countyId;
      
      }

      


        public County(
        int 
          countyId,String 
          name,int 
          stateId,String 
          stateName,String 
          stateFips,String 
          countyFips,String 
          fips
        )
        {
        
          m_countyId = countyId;
        
          m_name = name;
        
          m_stateId = stateId;
        
          m_stateName = stateName;
        
          m_stateFips = stateFips;
        
          m_countyFips = countyFips;
        
          m_fips = fips;
        
        }


      
      #endregion

      
        [XmlElement]
        public int CountyId
        {
        get { return m_countyId;}
        set { m_countyId = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        [XmlElement]
        public int StateId
        {
        get { return m_stateId;}
        set { m_stateId = value; }
        }
      
        [XmlElement]
        public String StateName
        {
        get { return m_stateName;}
        set { m_stateName = value; }
        }
      
        [XmlElement]
        public String StateFips
        {
        get { return m_stateFips;}
        set { m_stateFips = value; }
        }
      
        [XmlElement]
        public String CountyFips
        {
        get { return m_countyFips;}
        set { m_countyFips = value; }
        }
      
        [XmlElement]
        public String Fips
        {
        get { return m_fips;}
        set { m_fips = value; }
        }
      
      }
      #endregion
      }

    