
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


      public partial class ProjectPayment : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ProjectPayment ( " +
      
        " ProjectId, " +
      
        " WorkTransactionId, " +
      
        " Amount " +
      
      ") Values (" +
      
        " ?ProjectId, " +
      
        " ?WorkTransactionId, " +
      
        " ?Amount " +
      
      ")";

      public static void Insert(ProjectPayment projectPayment, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectPayment.ProjectId);
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", projectPayment.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?Amount", projectPayment.Amount);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ProjectPayment projectPayment)
      {
        Insert(projectPayment, null);
      }


      public static void Insert(List<ProjectPayment>  projectPaymentList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ProjectPayment projectPayment in  projectPaymentList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectPayment.ProjectId);
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", projectPayment.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?Amount", projectPayment.Amount);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ProjectId",projectPayment.ProjectId);
      
        Database.UpdateParameter(dbCommand,"?WorkTransactionId",projectPayment.WorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"?Amount",projectPayment.Amount);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ProjectPayment>  projectPaymentList)
      {
        Insert(projectPaymentList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ProjectPayment Set "
      
        + " Amount = ?Amount "
      
        + " Where "
        
          + " ProjectId = ?ProjectId and  "
        
          + " WorkTransactionId = ?WorkTransactionId "
        
      ;

      public static void Update(ProjectPayment projectPayment, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectPayment.ProjectId);
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", projectPayment.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?Amount", projectPayment.Amount);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ProjectPayment projectPayment)
      {
        Update(projectPayment, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ProjectId, "
      
        + " WorkTransactionId, "
      
        + " Amount "
      

      + " From ProjectPayment "

      
        + " Where "
        
          + " ProjectId = ?ProjectId and  "
        
          + " WorkTransactionId = ?WorkTransactionId "
        
      ;

      public static ProjectPayment FindByPrimaryKey(
      int projectId,int workTransactionId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectId);
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ProjectPayment not found, search by primary key");

      }

      public static ProjectPayment FindByPrimaryKey(
      int projectId,int workTransactionId
      )
      {
      return FindByPrimaryKey(
      projectId,workTransactionId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ProjectPayment projectPayment, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectId",projectPayment.ProjectId);
      
        Database.PutParameter(dbCommand,"?WorkTransactionId",projectPayment.WorkTransactionId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ProjectPayment projectPayment)
      {
      return Exists(projectPayment, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ProjectPayment limit 1";

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

      public static ProjectPayment Load(IDataReader dataReader, int offset)
      {
      ProjectPayment projectPayment = new ProjectPayment();

      projectPayment.ProjectId = dataReader.GetInt32(0 + offset);
          projectPayment.WorkTransactionId = dataReader.GetInt32(1 + offset);
          projectPayment.Amount = dataReader.GetDecimal(2 + offset);
          

      return projectPayment;
      }

      public static ProjectPayment Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ProjectPayment "

      
        + " Where "
        
          + " ProjectId = ?ProjectId and  "
        
          + " WorkTransactionId = ?WorkTransactionId "
        
      ;
      public static void Delete(ProjectPayment projectPayment, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ProjectId", projectPayment.ProjectId);
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", projectPayment.WorkTransactionId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectPayment projectPayment)
      {
        Delete(projectPayment, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ProjectPayment ";

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

      
        + " ProjectId, "
      
        + " WorkTransactionId, "
      
        + " Amount "
      

      + " From ProjectPayment ";
      public static List<ProjectPayment> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ProjectPayment> rv = new List<ProjectPayment>();

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

      public static List<ProjectPayment> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ProjectPayment> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ProjectPayment obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ProjectId == obj.ProjectId && WorkTransactionId == obj.WorkTransactionId && Amount == obj.Amount;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ProjectPayment> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectPayment));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ProjectPayment item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ProjectPayment>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectPayment));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ProjectPayment> itemsList
      = new List<ProjectPayment>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ProjectPayment)
      itemsList.Add(deserializedObject as ProjectPayment);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_projectId;
      
        protected int m_workTransactionId;
      
        protected decimal m_amount;
      
      #endregion

      #region Constructors
      public ProjectPayment(
      int 
          projectId,int 
          workTransactionId
      ) : this()
      {
      
        m_projectId = projectId;
      
        m_workTransactionId = workTransactionId;
      
      }

      


        public ProjectPayment(
        int 
          projectId,int 
          workTransactionId,decimal 
          amount
        ) : this()
        {
        
          m_projectId = projectId;
        
          m_workTransactionId = workTransactionId;
        
          m_amount = amount;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ProjectId
        {
        get { return m_projectId;}
        set { m_projectId = value; }
        }
      
        [XmlElement]
        public int WorkTransactionId
        {
        get { return m_workTransactionId;}
        set { m_workTransactionId = value; }
        }
      
        [XmlElement]
        public decimal Amount
        {
        get { return m_amount;}
        set { m_amount = value; }
        }
      

      public static int FieldsCount
      {
      get { return 3; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    