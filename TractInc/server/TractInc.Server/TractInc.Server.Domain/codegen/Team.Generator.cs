
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


      public partial class Team
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Team] ( " +
      
        " CompanyId, " +
      
        " ParentTeamId, " +
      
        " TeamName " +
      
      ") Values (" +
      
        " @CompanyId, " +
      
        " @ParentTeamId, " +
      
        " @TeamName " +
      
      ")";

      public static void Insert(Team team)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@CompanyId", team.CompanyId);
      
          Database.PutParameter(dbCommand,"@ParentTeamId", 0 != team.ParentTeamId ? team.ParentTeamId : null);
      
        Database.PutParameter(dbCommand,"@TeamName", team.TeamName);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          team.TeamId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<Team>  teamList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Team team in  teamList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@CompanyId", team.CompanyId);
      
        Database.PutParameter(dbCommand,"@ParentTeamId", team.ParentTeamId);
      
        Database.PutParameter(dbCommand,"@TeamName", team.TeamName);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@CompanyId",team.CompanyId);
      
        Database.UpdateParameter(dbCommand,"@ParentTeamId",team.ParentTeamId);
      
        Database.UpdateParameter(dbCommand,"@TeamName",team.TeamName);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        team.TeamId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Team] Set "
      
        + " CompanyId = @CompanyId, "
      
        + " ParentTeamId = @ParentTeamId, "
      
        + " TeamName = @TeamName "
      
        + " Where "
        
          + " TeamId = @TeamId "
        
      ;

      public static void Update(Team team)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@TeamId", team.TeamId);
      
        Database.PutParameter(dbCommand,"@CompanyId", team.CompanyId);
      
        Database.PutParameter(dbCommand,"@ParentTeamId", 0 != team.ParentTeamId ? team.ParentTeamId : null);
      
        Database.PutParameter(dbCommand,"@TeamName", team.TeamName);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TeamId, "
      
        + " CompanyId, "
      
        + " ParentTeamId, "
      
        + " TeamName "
      

      + " From [Team] "

      
        + " Where "
        
          + " TeamId = @TeamId "
        
      ;

      public static Team FindByPrimaryKey(
      int teamId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@TeamId", teamId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Team not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Team team)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@TeamId",team.TeamId);
      

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
      String sql = "select 1 from Team";

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

      public static Team Load(IDataReader dataReader)
      {
      Team team = new Team();

      team.TeamId = dataReader.GetInt32(0);
          team.CompanyId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            team.ParentTeamId = dataReader.GetInt32(2);
          team.TeamName = dataReader.GetString(3);
          

      return team;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Team] "

      
        + " Where "
        
          + " TeamId = @TeamId "
        
      ;
      public static void Delete(Team team)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@TeamId", team.TeamId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Team] ";

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

      
        + " TeamId, "
      
        + " CompanyId, "
      
        + " ParentTeamId, "
      
        + " TeamName "
      

      + " From [Team] ";
      public static List<Team> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Team> rv = new List<Team>();

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
      List<Team> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Team> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Team));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Team item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Team>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Team));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Team> itemsList
      = new List<Team>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Team)
      itemsList.Add(deserializedObject as Team);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_teamId;
      
        protected int m_companyId;
      
        protected int? m_parentTeamId;
      
        protected String m_teamName;
      
      #endregion

      #region Constructors
      public Team(
      int 
          teamId
      )
      {
      
        m_teamId = teamId;
      
      }

      


        public Team(
        int 
          teamId,int 
          companyId,int? 
          parentTeamId,String 
          teamName
        )
        {
        
          m_teamId = teamId;
        
          m_companyId = companyId;
        
          m_parentTeamId = parentTeamId;
        
          m_teamName = teamName;
        
        }


      
      #endregion

      
        [XmlElement]
        public int TeamId
        {
        get { return m_teamId;}
        set { m_teamId = value; }
        }
      
        [XmlElement]
        public int CompanyId
        {
        get { return m_companyId;}
        set { m_companyId = value; }
        }
      
        [XmlElement]
        public int? ParentTeamId
        {
        get { return m_parentTeamId;}
        set { m_parentTeamId = value; }
        }
      
        [XmlElement]
        public String TeamName
        {
        get { return m_teamName;}
        set { m_teamName = value; }
        }
      
      }
      #endregion
      }

    