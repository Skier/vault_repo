
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Domain
      {


      public partial class ProjectConstructionBillPay : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ProjectConstructionBillPay ( " +
      
        " ProjectId, " +
      
        " ProjectConstructionBillPayTypeId, " +
      
        " IssueDate, " +
      
        " Number, " +
      
        " IsVoided, " +
      
        " Notes, " +
      
        " Amount " +
      
      ") Values (" +
      
        " ?ProjectId, " +
      
        " ?ProjectConstructionBillPayTypeId, " +
      
        " ?IssueDate, " +
      
        " ?Number, " +
      
        " ?IsVoided, " +
      
        " ?Notes, " +
      
        " ?Amount " +
      
      ")";

      public static void Insert(ProjectConstructionBillPay projectConstructionBillPay, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectConstructionBillPay.ProjectId);
      
        Database.PutParameter(dbCommand,"?ProjectConstructionBillPayTypeId", projectConstructionBillPay.ProjectConstructionBillPayTypeId);
      
        Database.PutParameter(dbCommand,"?IssueDate", projectConstructionBillPay.IssueDate);
      
        Database.PutParameter(dbCommand,"?Number", projectConstructionBillPay.Number);
      
        Database.PutParameter(dbCommand,"?IsVoided", projectConstructionBillPay.IsVoided);
      
        Database.PutParameter(dbCommand,"?Notes", projectConstructionBillPay.Notes);
      
        Database.PutParameter(dbCommand,"?Amount", projectConstructionBillPay.Amount);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        projectConstructionBillPay.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(ProjectConstructionBillPay projectConstructionBillPay)
      {
        Insert(projectConstructionBillPay, null);
      }


      public static void Insert(List<ProjectConstructionBillPay>  projectConstructionBillPayList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ProjectConstructionBillPay projectConstructionBillPay in  projectConstructionBillPayList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectConstructionBillPay.ProjectId);
      
        Database.PutParameter(dbCommand,"?ProjectConstructionBillPayTypeId", projectConstructionBillPay.ProjectConstructionBillPayTypeId);
      
        Database.PutParameter(dbCommand,"?IssueDate", projectConstructionBillPay.IssueDate);
      
        Database.PutParameter(dbCommand,"?Number", projectConstructionBillPay.Number);
      
        Database.PutParameter(dbCommand,"?IsVoided", projectConstructionBillPay.IsVoided);
      
        Database.PutParameter(dbCommand,"?Notes", projectConstructionBillPay.Notes);
      
        Database.PutParameter(dbCommand,"?Amount", projectConstructionBillPay.Amount);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ProjectId",projectConstructionBillPay.ProjectId);
      
        Database.UpdateParameter(dbCommand,"?ProjectConstructionBillPayTypeId",projectConstructionBillPay.ProjectConstructionBillPayTypeId);
      
        Database.UpdateParameter(dbCommand,"?IssueDate",projectConstructionBillPay.IssueDate);
      
        Database.UpdateParameter(dbCommand,"?Number",projectConstructionBillPay.Number);
      
        Database.UpdateParameter(dbCommand,"?IsVoided",projectConstructionBillPay.IsVoided);
      
        Database.UpdateParameter(dbCommand,"?Notes",projectConstructionBillPay.Notes);
      
        Database.UpdateParameter(dbCommand,"?Amount",projectConstructionBillPay.Amount);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        projectConstructionBillPay.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<ProjectConstructionBillPay>  projectConstructionBillPayList)
      {
        Insert(projectConstructionBillPayList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ProjectConstructionBillPay Set "
      
        + " ProjectId = ?ProjectId, "
      
        + " ProjectConstructionBillPayTypeId = ?ProjectConstructionBillPayTypeId, "
      
        + " IssueDate = ?IssueDate, "
      
        + " Number = ?Number, "
      
        + " IsVoided = ?IsVoided, "
      
        + " Notes = ?Notes, "
      
        + " Amount = ?Amount "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(ProjectConstructionBillPay projectConstructionBillPay, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", projectConstructionBillPay.ID);
      
        Database.PutParameter(dbCommand,"?ProjectId", projectConstructionBillPay.ProjectId);
      
        Database.PutParameter(dbCommand,"?ProjectConstructionBillPayTypeId", projectConstructionBillPay.ProjectConstructionBillPayTypeId);
      
        Database.PutParameter(dbCommand,"?IssueDate", projectConstructionBillPay.IssueDate);
      
        Database.PutParameter(dbCommand,"?Number", projectConstructionBillPay.Number);
      
        Database.PutParameter(dbCommand,"?IsVoided", projectConstructionBillPay.IsVoided);
      
        Database.PutParameter(dbCommand,"?Notes", projectConstructionBillPay.Notes);
      
        Database.PutParameter(dbCommand,"?Amount", projectConstructionBillPay.Amount);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ProjectConstructionBillPay projectConstructionBillPay)
      {
        Update(projectConstructionBillPay, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " ProjectId, "
      
        + " ProjectConstructionBillPayTypeId, "
      
        + " IssueDate, "
      
        + " Number, "
      
        + " IsVoided, "
      
        + " Notes, "
      
        + " Amount "
      

      + " From ProjectConstructionBillPay "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static ProjectConstructionBillPay FindByPrimaryKey(
      int iD, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", iD);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ProjectConstructionBillPay not found, search by primary key");

      }

      public static ProjectConstructionBillPay FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ProjectConstructionBillPay projectConstructionBillPay, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",projectConstructionBillPay.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ProjectConstructionBillPay projectConstructionBillPay)
      {
      return Exists(projectConstructionBillPay, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ProjectConstructionBillPay limit 1";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      public static bool IsContainsData()
      {
      return IsContainsData(null);
      }

      #endregion

      #region Load

      public static ProjectConstructionBillPay Load(IDataReader dataReader, int offset)
      {
      ProjectConstructionBillPay projectConstructionBillPay = new ProjectConstructionBillPay();

      projectConstructionBillPay.ID = dataReader.GetInt32(0 + offset);
          projectConstructionBillPay.ProjectId = dataReader.GetInt32(1 + offset);
          projectConstructionBillPay.ProjectConstructionBillPayTypeId = dataReader.GetInt32(2 + offset);
          projectConstructionBillPay.IssueDate = dataReader.GetDateTime(3 + offset);
          projectConstructionBillPay.Number = dataReader.GetString(4 + offset);
          projectConstructionBillPay.IsVoided = dataReader.GetBoolean(5 + offset);
          projectConstructionBillPay.Notes = dataReader.GetString(6 + offset);
          projectConstructionBillPay.Amount = dataReader.GetDecimal(7 + offset);
          

      return projectConstructionBillPay;
      }

      public static ProjectConstructionBillPay Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ProjectConstructionBillPay "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(ProjectConstructionBillPay projectConstructionBillPay, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", projectConstructionBillPay.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectConstructionBillPay projectConstructionBillPay)
      {
        Delete(projectConstructionBillPay, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ProjectConstructionBillPay ";

      public static void Clear(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, connection))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Clear()
      {
        Clear(null);
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " ID, "
      
        + " ProjectId, "
      
        + " ProjectConstructionBillPayTypeId, "
      
        + " IssueDate, "
      
        + " Number, "
      
        + " IsVoided, "
      
        + " Notes, "
      
        + " Amount "
      

      + " From ProjectConstructionBillPay ";
      public static List<ProjectConstructionBillPay> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ProjectConstructionBillPay> rv = new List<ProjectConstructionBillPay>();

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

      public static List<ProjectConstructionBillPay> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ProjectConstructionBillPay> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ProjectConstructionBillPay obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && ProjectId == obj.ProjectId && ProjectConstructionBillPayTypeId == obj.ProjectConstructionBillPayTypeId && IssueDate == obj.IssueDate && Number == obj.Number && IsVoided == obj.IsVoided && Notes == obj.Notes && Amount == obj.Amount;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ProjectConstructionBillPay> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectConstructionBillPay));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ProjectConstructionBillPay item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ProjectConstructionBillPay>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectConstructionBillPay));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ProjectConstructionBillPay> itemsList
      = new List<ProjectConstructionBillPay>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ProjectConstructionBillPay)
      itemsList.Add(deserializedObject as ProjectConstructionBillPay);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_projectId;
      
        protected int m_projectConstructionBillPayTypeId;
      
        protected DateTime m_issueDate;
      
        protected String m_number;
      
        protected bool m_isVoided;
      
        protected String m_notes;
      
        protected decimal m_amount;
      
      #endregion

      #region Constructors
      public ProjectConstructionBillPay(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public ProjectConstructionBillPay(
        int 
          iD,int 
          projectId,int 
          projectConstructionBillPayTypeId,DateTime 
          issueDate,String 
          number,bool 
          isVoided,String 
          notes,decimal 
          amount
        ) : this()
        {
        
          m_iD = iD;
        
          m_projectId = projectId;
        
          m_projectConstructionBillPayTypeId = projectConstructionBillPayTypeId;
        
          m_issueDate = issueDate;
        
          m_number = number;
        
          m_isVoided = isVoided;
        
          m_notes = notes;
        
          m_amount = amount;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int ProjectId
        {
        get { return m_projectId;}
        set { m_projectId = value; }
        }
      
        [XmlElement]
        public int ProjectConstructionBillPayTypeId
        {
        get { return m_projectConstructionBillPayTypeId;}
        set { m_projectConstructionBillPayTypeId = value; }
        }
      
        [XmlElement]
        public DateTime IssueDate
        {
        get { return m_issueDate;}
        set { m_issueDate = value; }
        }
      
        [XmlElement]
        public String Number
        {
        get { return m_number;}
        set { m_number = value; }
        }
      
        [XmlElement]
        public bool IsVoided
        {
        get { return m_isVoided;}
        set { m_isVoided = value; }
        }
      
        [XmlElement]
        public String Notes
        {
        get { return m_notes;}
        set { m_notes = value; }
        }
      
        [XmlElement]
        public decimal Amount
        {
        get { return m_amount;}
        set { m_amount = value; }
        }
      

      public static int FieldsCount
      {
      get { return 8; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    