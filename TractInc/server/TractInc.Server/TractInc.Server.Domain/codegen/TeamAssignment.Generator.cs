
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


      public partial class TeamAssignment
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [TeamAssignment] ( " +
      
        " TeamId, " +
      
        " ProjectId, " +
      
        " LeadAssetId, " +
      
        " StartDate, " +
      
        " EndDate " +
      
      ") Values (" +
      
        " @TeamId, " +
      
        " @ProjectId, " +
      
        " @LeadAssetId, " +
      
        " @StartDate, " +
      
        " @EndDate " +
      
      ")";

      public static void Insert(TeamAssignment teamAssignment)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@TeamId", teamAssignment.TeamId);
      
        Database.PutParameter(dbCommand,"@ProjectId", teamAssignment.ProjectId);
      
        Database.PutParameter(dbCommand,"@LeadAssetId", teamAssignment.LeadAssetId);
      
        Database.PutParameter(dbCommand,"@StartDate", teamAssignment.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", teamAssignment.EndDate);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          teamAssignment.TeamAssignmentId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<TeamAssignment>  teamAssignmentList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(TeamAssignment teamAssignment in  teamAssignmentList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@TeamId", teamAssignment.TeamId);
      
        Database.PutParameter(dbCommand,"@ProjectId", teamAssignment.ProjectId);
      
        Database.PutParameter(dbCommand,"@LeadAssetId", teamAssignment.LeadAssetId);
      
        Database.PutParameter(dbCommand,"@StartDate", teamAssignment.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", teamAssignment.EndDate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@TeamId",teamAssignment.TeamId);
      
        Database.UpdateParameter(dbCommand,"@ProjectId",teamAssignment.ProjectId);
      
        Database.UpdateParameter(dbCommand,"@LeadAssetId",teamAssignment.LeadAssetId);
      
        Database.UpdateParameter(dbCommand,"@StartDate",teamAssignment.StartDate);
      
        Database.UpdateParameter(dbCommand,"@EndDate",teamAssignment.EndDate);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        teamAssignment.TeamAssignmentId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [TeamAssignment] Set "
      
        + " TeamId = @TeamId, "
      
        + " ProjectId = @ProjectId, "
      
        + " LeadAssetId = @LeadAssetId, "
      
        + " StartDate = @StartDate, "
      
        + " EndDate = @EndDate "
      
        + " Where "
        
          + " TeamAssignmentId = @TeamAssignmentId "
        
      ;

      public static void Update(TeamAssignment teamAssignment)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@TeamAssignmentId", teamAssignment.TeamAssignmentId);
      
        Database.PutParameter(dbCommand,"@TeamId", teamAssignment.TeamId);
      
        Database.PutParameter(dbCommand,"@ProjectId", teamAssignment.ProjectId);
      
        Database.PutParameter(dbCommand,"@LeadAssetId", teamAssignment.LeadAssetId);
      
        Database.PutParameter(dbCommand,"@StartDate", teamAssignment.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", teamAssignment.EndDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TeamAssignmentId, "
      
        + " TeamId, "
      
        + " ProjectId, "
      
        + " LeadAssetId, "
      
        + " StartDate, "
      
        + " EndDate "
      

      + " From [TeamAssignment] "

      
        + " Where "
        
          + " TeamAssignmentId = @TeamAssignmentId "
        
      ;

      public static TeamAssignment FindByPrimaryKey(
      int teamAssignmentId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@TeamAssignmentId", teamAssignmentId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("TeamAssignment not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(TeamAssignment teamAssignment)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@TeamAssignmentId",teamAssignment.TeamAssignmentId);
      

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
      String sql = "select 1 from TeamAssignment";

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

      public static TeamAssignment Load(IDataReader dataReader)
      {
      TeamAssignment teamAssignment = new TeamAssignment();

      teamAssignment.TeamAssignmentId = dataReader.GetInt32(0);
          teamAssignment.TeamId = dataReader.GetInt32(1);
          teamAssignment.ProjectId = dataReader.GetInt32(2);
          teamAssignment.LeadAssetId = dataReader.GetInt32(3);
          teamAssignment.StartDate = dataReader.GetDateTime(4);
          
            if(!dataReader.IsDBNull(5))
            teamAssignment.EndDate = dataReader.GetDateTime(5);
          

      return teamAssignment;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [TeamAssignment] "

      
        + " Where "
        
          + " TeamAssignmentId = @TeamAssignmentId "
        
      ;
      public static void Delete(TeamAssignment teamAssignment)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@TeamAssignmentId", teamAssignment.TeamAssignmentId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [TeamAssignment] ";

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

      
        + " TeamAssignmentId, "
      
        + " TeamId, "
      
        + " ProjectId, "
      
        + " LeadAssetId, "
      
        + " StartDate, "
      
        + " EndDate "
      

      + " From [TeamAssignment] ";
      public static List<TeamAssignment> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<TeamAssignment> rv = new List<TeamAssignment>();

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
      List<TeamAssignment> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TeamAssignment> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TeamAssignment));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TeamAssignment item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TeamAssignment>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TeamAssignment));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TeamAssignment> itemsList
      = new List<TeamAssignment>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TeamAssignment)
      itemsList.Add(deserializedObject as TeamAssignment);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_teamAssignmentId;
      
        protected int m_teamId;
      
        protected int m_projectId;
      
        protected int m_leadAssetId;
      
        protected DateTime m_startDate;
      
        protected DateTime? m_endDate;
      
      #endregion

      #region Constructors
      public TeamAssignment(
      int 
          teamAssignmentId
      )
      {
      
        m_teamAssignmentId = teamAssignmentId;
      
      }

      


        public TeamAssignment(
        int 
          teamAssignmentId,int 
          teamId,int 
          projectId,int 
          leadAssetId,DateTime 
          startDate,DateTime? 
          endDate
        )
        {
        
          m_teamAssignmentId = teamAssignmentId;
        
          m_teamId = teamId;
        
          m_projectId = projectId;
        
          m_leadAssetId = leadAssetId;
        
          m_startDate = startDate;
        
          m_endDate = endDate;
        
        }


      
      #endregion

      
        [XmlElement]
        public int TeamAssignmentId
        {
        get { return m_teamAssignmentId;}
        set { m_teamAssignmentId = value; }
        }
      
        [XmlElement]
        public int TeamId
        {
        get { return m_teamId;}
        set { m_teamId = value; }
        }
      
        [XmlElement]
        public int ProjectId
        {
        get { return m_projectId;}
        set { m_projectId = value; }
        }
      
        [XmlElement]
        public int LeadAssetId
        {
        get { return m_leadAssetId;}
        set { m_leadAssetId = value; }
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

    