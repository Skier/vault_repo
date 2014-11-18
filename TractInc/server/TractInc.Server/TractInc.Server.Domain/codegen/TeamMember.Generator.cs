
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


      public partial class TeamMember
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [TeamMember] ( " +
      
        " TeamId, " +
      
        " AssetId, " +
      
        " StartDate, " +
      
        " EndDate " +
      
      ") Values (" +
      
        " @TeamId, " +
      
        " @AssetId, " +
      
        " @StartDate, " +
      
        " @EndDate " +
      
      ")";

      public static void Insert(TeamMember teamMember)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@TeamId", teamMember.TeamId);
      
        Database.PutParameter(dbCommand,"@AssetId", teamMember.AssetId);
      
        Database.PutParameter(dbCommand,"@StartDate", teamMember.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", teamMember.EndDate);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          teamMember.TeamMemberId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<TeamMember>  teamMemberList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(TeamMember teamMember in  teamMemberList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@TeamId", teamMember.TeamId);
      
        Database.PutParameter(dbCommand,"@AssetId", teamMember.AssetId);
      
        Database.PutParameter(dbCommand,"@StartDate", teamMember.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", teamMember.EndDate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@TeamId",teamMember.TeamId);
      
        Database.UpdateParameter(dbCommand,"@AssetId",teamMember.AssetId);
      
        Database.UpdateParameter(dbCommand,"@StartDate",teamMember.StartDate);
      
        Database.UpdateParameter(dbCommand,"@EndDate",teamMember.EndDate);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        teamMember.TeamMemberId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [TeamMember] Set "
      
        + " TeamId = @TeamId, "
      
        + " AssetId = @AssetId, "
      
        + " StartDate = @StartDate, "
      
        + " EndDate = @EndDate "
      
        + " Where "
        
          + " TeamMemberId = @TeamMemberId "
        
      ;

      public static void Update(TeamMember teamMember)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@TeamMemberId", teamMember.TeamMemberId);
      
        Database.PutParameter(dbCommand,"@TeamId", teamMember.TeamId);
      
        Database.PutParameter(dbCommand,"@AssetId", teamMember.AssetId);
      
        Database.PutParameter(dbCommand,"@StartDate", teamMember.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", teamMember.EndDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TeamMemberId, "
      
        + " TeamId, "
      
        + " AssetId, "
      
        + " StartDate, "
      
        + " EndDate "
      

      + " From [TeamMember] "

      
        + " Where "
        
          + " TeamMemberId = @TeamMemberId "
        
      ;

      public static TeamMember FindByPrimaryKey(
      int teamMemberId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@TeamMemberId", teamMemberId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("TeamMember not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(TeamMember teamMember)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@TeamMemberId",teamMember.TeamMemberId);
      

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
      String sql = "select 1 from TeamMember";

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

      public static TeamMember Load(IDataReader dataReader)
      {
      TeamMember teamMember = new TeamMember();

      teamMember.TeamMemberId = dataReader.GetInt32(0);
          teamMember.TeamId = dataReader.GetInt32(1);
          teamMember.AssetId = dataReader.GetInt32(2);
          teamMember.StartDate = dataReader.GetDateTime(3);
          
            if(!dataReader.IsDBNull(4))
            teamMember.EndDate = dataReader.GetDateTime(4);
          

      return teamMember;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [TeamMember] "

      
        + " Where "
        
          + " TeamMemberId = @TeamMemberId "
        
      ;
      public static void Delete(TeamMember teamMember)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@TeamMemberId", teamMember.TeamMemberId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [TeamMember] ";

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

      
        + " TeamMemberId, "
      
        + " TeamId, "
      
        + " AssetId, "
      
        + " StartDate, "
      
        + " EndDate "
      

      + " From [TeamMember] ";
      public static List<TeamMember> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<TeamMember> rv = new List<TeamMember>();

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
      List<TeamMember> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TeamMember> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TeamMember));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TeamMember item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TeamMember>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TeamMember));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TeamMember> itemsList
      = new List<TeamMember>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TeamMember)
      itemsList.Add(deserializedObject as TeamMember);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_teamMemberId;
      
        protected int m_teamId;
      
        protected int m_assetId;
      
        protected DateTime m_startDate;
      
        protected DateTime? m_endDate;
      
      #endregion

      #region Constructors
      public TeamMember(
      int 
          teamMemberId
      )
      {
      
        m_teamMemberId = teamMemberId;
      
      }

      


        public TeamMember(
        int 
          teamMemberId,int 
          teamId,int 
          assetId,DateTime 
          startDate,DateTime? 
          endDate
        )
        {
        
          m_teamMemberId = teamMemberId;
        
          m_teamId = teamId;
        
          m_assetId = assetId;
        
          m_startDate = startDate;
        
          m_endDate = endDate;
        
        }


      
      #endregion

      
        [XmlElement]
        public int TeamMemberId
        {
        get { return m_teamMemberId;}
        set { m_teamMemberId = value; }
        }
      
        [XmlElement]
        public int TeamId
        {
        get { return m_teamId;}
        set { m_teamId = value; }
        }
      
        [XmlElement]
        public int AssetId
        {
        get { return m_assetId;}
        set { m_assetId = value; }
        }
      
        [XmlElement]
        public DateTime StartDate
        {
        get { return m_startDate;}
        set { m_startDate = value; }
        }
      
        [XmlElement]
        public DateTime? EndDate
        {
        get { return m_endDate;}
        set { m_endDate = value; }
        }
      
      }
      #endregion
      }

    