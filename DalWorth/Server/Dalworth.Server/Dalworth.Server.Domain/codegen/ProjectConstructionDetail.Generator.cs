
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


      public partial class ProjectConstructionDetail : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ProjectConstructionDetail ( " +
      
        " ProjectId, " +
      
        " ProjectConstructionProgressId, " +
      
        " ProjectManagerEmployeeId, " +
      
        " AccountManagerEmployeeId, " +
      
        " ScopeDate, " +
      
        " SignUpDate, " +
      
        " DeclineDate, " +
      
        " EstimatedAmount, " +
      
        " ActualStartDate, " +
      
        " ActualCompletionDate, " +
      
        " SignOffDate, " +
      
        " LastBillingDate, " +
      
        " LastPaymentDate, " +
      
        " IsSelfGeneratedLead, " +
      
        " JobCost, " +
      
        " ConstructionDamageTypeId, " +
      
        " DamageTypeText, " +
      
        " DamageOrigin, " +
      
        " LossDate, " +
      
        " BilledAmount, " +
      
        " LastModifiedDate, " +
      
        " JobNumber " +
      
      ") Values (" +
      
        " ?ProjectId, " +
      
        " ?ProjectConstructionProgressId, " +
      
        " ?ProjectManagerEmployeeId, " +
      
        " ?AccountManagerEmployeeId, " +
      
        " ?ScopeDate, " +
      
        " ?SignUpDate, " +
      
        " ?DeclineDate, " +
      
        " ?EstimatedAmount, " +
      
        " ?ActualStartDate, " +
      
        " ?ActualCompletionDate, " +
      
        " ?SignOffDate, " +
      
        " ?LastBillingDate, " +
      
        " ?LastPaymentDate, " +
      
        " ?IsSelfGeneratedLead, " +
      
        " ?JobCost, " +
      
        " ?ConstructionDamageTypeId, " +
      
        " ?DamageTypeText, " +
      
        " ?DamageOrigin, " +
      
        " ?LossDate, " +
      
        " ?BilledAmount, " +
      
        " ?LastModifiedDate, " +
      
        " ?JobNumber " +
      
      ")";

      public static void Insert(ProjectConstructionDetail projectConstructionDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectConstructionDetail.ProjectId);
      
        Database.PutParameter(dbCommand,"?ProjectConstructionProgressId", projectConstructionDetail.ProjectConstructionProgressId);
      
        Database.PutParameter(dbCommand,"?ProjectManagerEmployeeId", projectConstructionDetail.ProjectManagerEmployeeId);
      
        Database.PutParameter(dbCommand,"?AccountManagerEmployeeId", projectConstructionDetail.AccountManagerEmployeeId);
      
        Database.PutParameter(dbCommand,"?ScopeDate", projectConstructionDetail.ScopeDate);
      
        Database.PutParameter(dbCommand,"?SignUpDate", projectConstructionDetail.SignUpDate);
      
        Database.PutParameter(dbCommand,"?DeclineDate", projectConstructionDetail.DeclineDate);
      
        Database.PutParameter(dbCommand,"?EstimatedAmount", projectConstructionDetail.EstimatedAmount);
      
        Database.PutParameter(dbCommand,"?ActualStartDate", projectConstructionDetail.ActualStartDate);
      
        Database.PutParameter(dbCommand,"?ActualCompletionDate", projectConstructionDetail.ActualCompletionDate);
      
        Database.PutParameter(dbCommand,"?SignOffDate", projectConstructionDetail.SignOffDate);
      
        Database.PutParameter(dbCommand,"?LastBillingDate", projectConstructionDetail.LastBillingDate);
      
        Database.PutParameter(dbCommand,"?LastPaymentDate", projectConstructionDetail.LastPaymentDate);
      
        Database.PutParameter(dbCommand,"?IsSelfGeneratedLead", projectConstructionDetail.IsSelfGeneratedLead);
      
        Database.PutParameter(dbCommand,"?JobCost", projectConstructionDetail.JobCost);
      
        Database.PutParameter(dbCommand,"?ConstructionDamageTypeId", projectConstructionDetail.ConstructionDamageTypeId);
      
        Database.PutParameter(dbCommand,"?DamageTypeText", projectConstructionDetail.DamageTypeText);
      
        Database.PutParameter(dbCommand,"?DamageOrigin", projectConstructionDetail.DamageOrigin);
      
        Database.PutParameter(dbCommand,"?LossDate", projectConstructionDetail.LossDate);
      
        Database.PutParameter(dbCommand,"?BilledAmount", projectConstructionDetail.BilledAmount);
      
        Database.PutParameter(dbCommand,"?LastModifiedDate", projectConstructionDetail.LastModifiedDate);
      
        Database.PutParameter(dbCommand,"?JobNumber", projectConstructionDetail.JobNumber);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ProjectConstructionDetail projectConstructionDetail)
      {
        Insert(projectConstructionDetail, null);
      }


      public static void Insert(List<ProjectConstructionDetail>  projectConstructionDetailList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ProjectConstructionDetail projectConstructionDetail in  projectConstructionDetailList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectConstructionDetail.ProjectId);
      
        Database.PutParameter(dbCommand,"?ProjectConstructionProgressId", projectConstructionDetail.ProjectConstructionProgressId);
      
        Database.PutParameter(dbCommand,"?ProjectManagerEmployeeId", projectConstructionDetail.ProjectManagerEmployeeId);
      
        Database.PutParameter(dbCommand,"?AccountManagerEmployeeId", projectConstructionDetail.AccountManagerEmployeeId);
      
        Database.PutParameter(dbCommand,"?ScopeDate", projectConstructionDetail.ScopeDate);
      
        Database.PutParameter(dbCommand,"?SignUpDate", projectConstructionDetail.SignUpDate);
      
        Database.PutParameter(dbCommand,"?DeclineDate", projectConstructionDetail.DeclineDate);
      
        Database.PutParameter(dbCommand,"?EstimatedAmount", projectConstructionDetail.EstimatedAmount);
      
        Database.PutParameter(dbCommand,"?ActualStartDate", projectConstructionDetail.ActualStartDate);
      
        Database.PutParameter(dbCommand,"?ActualCompletionDate", projectConstructionDetail.ActualCompletionDate);
      
        Database.PutParameter(dbCommand,"?SignOffDate", projectConstructionDetail.SignOffDate);
      
        Database.PutParameter(dbCommand,"?LastBillingDate", projectConstructionDetail.LastBillingDate);
      
        Database.PutParameter(dbCommand,"?LastPaymentDate", projectConstructionDetail.LastPaymentDate);
      
        Database.PutParameter(dbCommand,"?IsSelfGeneratedLead", projectConstructionDetail.IsSelfGeneratedLead);
      
        Database.PutParameter(dbCommand,"?JobCost", projectConstructionDetail.JobCost);
      
        Database.PutParameter(dbCommand,"?ConstructionDamageTypeId", projectConstructionDetail.ConstructionDamageTypeId);
      
        Database.PutParameter(dbCommand,"?DamageTypeText", projectConstructionDetail.DamageTypeText);
      
        Database.PutParameter(dbCommand,"?DamageOrigin", projectConstructionDetail.DamageOrigin);
      
        Database.PutParameter(dbCommand,"?LossDate", projectConstructionDetail.LossDate);
      
        Database.PutParameter(dbCommand,"?BilledAmount", projectConstructionDetail.BilledAmount);
      
        Database.PutParameter(dbCommand,"?LastModifiedDate", projectConstructionDetail.LastModifiedDate);
      
        Database.PutParameter(dbCommand,"?JobNumber", projectConstructionDetail.JobNumber);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ProjectId",projectConstructionDetail.ProjectId);
      
        Database.UpdateParameter(dbCommand,"?ProjectConstructionProgressId",projectConstructionDetail.ProjectConstructionProgressId);
      
        Database.UpdateParameter(dbCommand,"?ProjectManagerEmployeeId",projectConstructionDetail.ProjectManagerEmployeeId);
      
        Database.UpdateParameter(dbCommand,"?AccountManagerEmployeeId",projectConstructionDetail.AccountManagerEmployeeId);
      
        Database.UpdateParameter(dbCommand,"?ScopeDate",projectConstructionDetail.ScopeDate);
      
        Database.UpdateParameter(dbCommand,"?SignUpDate",projectConstructionDetail.SignUpDate);
      
        Database.UpdateParameter(dbCommand,"?DeclineDate",projectConstructionDetail.DeclineDate);
      
        Database.UpdateParameter(dbCommand,"?EstimatedAmount",projectConstructionDetail.EstimatedAmount);
      
        Database.UpdateParameter(dbCommand,"?ActualStartDate",projectConstructionDetail.ActualStartDate);
      
        Database.UpdateParameter(dbCommand,"?ActualCompletionDate",projectConstructionDetail.ActualCompletionDate);
      
        Database.UpdateParameter(dbCommand,"?SignOffDate",projectConstructionDetail.SignOffDate);
      
        Database.UpdateParameter(dbCommand,"?LastBillingDate",projectConstructionDetail.LastBillingDate);
      
        Database.UpdateParameter(dbCommand,"?LastPaymentDate",projectConstructionDetail.LastPaymentDate);
      
        Database.UpdateParameter(dbCommand,"?IsSelfGeneratedLead",projectConstructionDetail.IsSelfGeneratedLead);
      
        Database.UpdateParameter(dbCommand,"?JobCost",projectConstructionDetail.JobCost);
      
        Database.UpdateParameter(dbCommand,"?ConstructionDamageTypeId",projectConstructionDetail.ConstructionDamageTypeId);
      
        Database.UpdateParameter(dbCommand,"?DamageTypeText",projectConstructionDetail.DamageTypeText);
      
        Database.UpdateParameter(dbCommand,"?DamageOrigin",projectConstructionDetail.DamageOrigin);
      
        Database.UpdateParameter(dbCommand,"?LossDate",projectConstructionDetail.LossDate);
      
        Database.UpdateParameter(dbCommand,"?BilledAmount",projectConstructionDetail.BilledAmount);
      
        Database.UpdateParameter(dbCommand,"?LastModifiedDate",projectConstructionDetail.LastModifiedDate);
      
        Database.UpdateParameter(dbCommand,"?JobNumber",projectConstructionDetail.JobNumber);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ProjectConstructionDetail>  projectConstructionDetailList)
      {
        Insert(projectConstructionDetailList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ProjectConstructionDetail Set "
      
        + " ProjectConstructionProgressId = ?ProjectConstructionProgressId, "
      
        + " ProjectManagerEmployeeId = ?ProjectManagerEmployeeId, "
      
        + " AccountManagerEmployeeId = ?AccountManagerEmployeeId, "
      
        + " ScopeDate = ?ScopeDate, "
      
        + " SignUpDate = ?SignUpDate, "
      
        + " DeclineDate = ?DeclineDate, "
      
        + " EstimatedAmount = ?EstimatedAmount, "
      
        + " ActualStartDate = ?ActualStartDate, "
      
        + " ActualCompletionDate = ?ActualCompletionDate, "
      
        + " SignOffDate = ?SignOffDate, "
      
        + " LastBillingDate = ?LastBillingDate, "
      
        + " LastPaymentDate = ?LastPaymentDate, "
      
        + " IsSelfGeneratedLead = ?IsSelfGeneratedLead, "
      
        + " JobCost = ?JobCost, "
      
        + " ConstructionDamageTypeId = ?ConstructionDamageTypeId, "
      
        + " DamageTypeText = ?DamageTypeText, "
      
        + " DamageOrigin = ?DamageOrigin, "
      
        + " LossDate = ?LossDate, "
      
        + " BilledAmount = ?BilledAmount, "
      
        + " LastModifiedDate = ?LastModifiedDate, "
      
        + " JobNumber = ?JobNumber "
      
        + " Where "
        
          + " ProjectId = ?ProjectId "
        
      ;

      public static void Update(ProjectConstructionDetail projectConstructionDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectConstructionDetail.ProjectId);
      
        Database.PutParameter(dbCommand,"?ProjectConstructionProgressId", projectConstructionDetail.ProjectConstructionProgressId);
      
        Database.PutParameter(dbCommand,"?ProjectManagerEmployeeId", projectConstructionDetail.ProjectManagerEmployeeId);
      
        Database.PutParameter(dbCommand,"?AccountManagerEmployeeId", projectConstructionDetail.AccountManagerEmployeeId);
      
        Database.PutParameter(dbCommand,"?ScopeDate", projectConstructionDetail.ScopeDate);
      
        Database.PutParameter(dbCommand,"?SignUpDate", projectConstructionDetail.SignUpDate);
      
        Database.PutParameter(dbCommand,"?DeclineDate", projectConstructionDetail.DeclineDate);
      
        Database.PutParameter(dbCommand,"?EstimatedAmount", projectConstructionDetail.EstimatedAmount);
      
        Database.PutParameter(dbCommand,"?ActualStartDate", projectConstructionDetail.ActualStartDate);
      
        Database.PutParameter(dbCommand,"?ActualCompletionDate", projectConstructionDetail.ActualCompletionDate);
      
        Database.PutParameter(dbCommand,"?SignOffDate", projectConstructionDetail.SignOffDate);
      
        Database.PutParameter(dbCommand,"?LastBillingDate", projectConstructionDetail.LastBillingDate);
      
        Database.PutParameter(dbCommand,"?LastPaymentDate", projectConstructionDetail.LastPaymentDate);
      
        Database.PutParameter(dbCommand,"?IsSelfGeneratedLead", projectConstructionDetail.IsSelfGeneratedLead);
      
        Database.PutParameter(dbCommand,"?JobCost", projectConstructionDetail.JobCost);
      
        Database.PutParameter(dbCommand,"?ConstructionDamageTypeId", projectConstructionDetail.ConstructionDamageTypeId);
      
        Database.PutParameter(dbCommand,"?DamageTypeText", projectConstructionDetail.DamageTypeText);
      
        Database.PutParameter(dbCommand,"?DamageOrigin", projectConstructionDetail.DamageOrigin);
      
        Database.PutParameter(dbCommand,"?LossDate", projectConstructionDetail.LossDate);
      
        Database.PutParameter(dbCommand,"?BilledAmount", projectConstructionDetail.BilledAmount);
      
        Database.PutParameter(dbCommand,"?LastModifiedDate", projectConstructionDetail.LastModifiedDate);
      
        Database.PutParameter(dbCommand,"?JobNumber", projectConstructionDetail.JobNumber);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ProjectConstructionDetail projectConstructionDetail)
      {
        Update(projectConstructionDetail, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ProjectId, "
      
        + " ProjectConstructionProgressId, "
      
        + " ProjectManagerEmployeeId, "
      
        + " AccountManagerEmployeeId, "
      
        + " ScopeDate, "
      
        + " SignUpDate, "
      
        + " DeclineDate, "
      
        + " EstimatedAmount, "
      
        + " ActualStartDate, "
      
        + " ActualCompletionDate, "
      
        + " SignOffDate, "
      
        + " LastBillingDate, "
      
        + " LastPaymentDate, "
      
        + " IsSelfGeneratedLead, "
      
        + " JobCost, "
      
        + " ConstructionDamageTypeId, "
      
        + " DamageTypeText, "
      
        + " DamageOrigin, "
      
        + " LossDate, "
      
        + " BilledAmount, "
      
        + " LastModifiedDate, "
      
        + " JobNumber "
      

      + " From ProjectConstructionDetail "

      
        + " Where "
        
          + " ProjectId = ?ProjectId "
        
      ;

      public static ProjectConstructionDetail FindByPrimaryKey(
      int projectId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ProjectConstructionDetail not found, search by primary key");

      }

      public static ProjectConstructionDetail FindByPrimaryKey(
      int projectId
      )
      {
      return FindByPrimaryKey(
      projectId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ProjectConstructionDetail projectConstructionDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectId",projectConstructionDetail.ProjectId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ProjectConstructionDetail projectConstructionDetail)
      {
      return Exists(projectConstructionDetail, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ProjectConstructionDetail limit 1";

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

      public static ProjectConstructionDetail Load(IDataReader dataReader, int offset)
      {
      ProjectConstructionDetail projectConstructionDetail = new ProjectConstructionDetail();

      projectConstructionDetail.ProjectId = dataReader.GetInt32(0 + offset);
          projectConstructionDetail.ProjectConstructionProgressId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            projectConstructionDetail.ProjectManagerEmployeeId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            projectConstructionDetail.AccountManagerEmployeeId = dataReader.GetInt32(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            projectConstructionDetail.ScopeDate = dataReader.GetDateTime(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            projectConstructionDetail.SignUpDate = dataReader.GetDateTime(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            projectConstructionDetail.DeclineDate = dataReader.GetDateTime(6 + offset);
          projectConstructionDetail.EstimatedAmount = dataReader.GetDecimal(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            projectConstructionDetail.ActualStartDate = dataReader.GetDateTime(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            projectConstructionDetail.ActualCompletionDate = dataReader.GetDateTime(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            projectConstructionDetail.SignOffDate = dataReader.GetDateTime(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            projectConstructionDetail.LastBillingDate = dataReader.GetDateTime(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            projectConstructionDetail.LastPaymentDate = dataReader.GetDateTime(12 + offset);
          projectConstructionDetail.IsSelfGeneratedLead = dataReader.GetBoolean(13 + offset);
          projectConstructionDetail.JobCost = dataReader.GetDecimal(14 + offset);
          
            if(!dataReader.IsDBNull(15 + offset))
            projectConstructionDetail.ConstructionDamageTypeId = dataReader.GetInt32(15 + offset);
          projectConstructionDetail.DamageTypeText = dataReader.GetString(16 + offset);
          projectConstructionDetail.DamageOrigin = dataReader.GetString(17 + offset);
          
            if(!dataReader.IsDBNull(18 + offset))
            projectConstructionDetail.LossDate = dataReader.GetDateTime(18 + offset);
          
            if(!dataReader.IsDBNull(19 + offset))
            projectConstructionDetail.BilledAmount = dataReader.GetDecimal(19 + offset);
          projectConstructionDetail.LastModifiedDate = dataReader.GetDateTime(20 + offset);
          
            if(!dataReader.IsDBNull(21 + offset))
            projectConstructionDetail.JobNumber = dataReader.GetString(21 + offset);
          

      return projectConstructionDetail;
      }

      public static ProjectConstructionDetail Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ProjectConstructionDetail "

      
        + " Where "
        
          + " ProjectId = ?ProjectId "
        
      ;
      public static void Delete(ProjectConstructionDetail projectConstructionDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ProjectId", projectConstructionDetail.ProjectId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectConstructionDetail projectConstructionDetail)
      {
        Delete(projectConstructionDetail, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ProjectConstructionDetail ";

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
      
        + " ProjectConstructionProgressId, "
      
        + " ProjectManagerEmployeeId, "
      
        + " AccountManagerEmployeeId, "
      
        + " ScopeDate, "
      
        + " SignUpDate, "
      
        + " DeclineDate, "
      
        + " EstimatedAmount, "
      
        + " ActualStartDate, "
      
        + " ActualCompletionDate, "
      
        + " SignOffDate, "
      
        + " LastBillingDate, "
      
        + " LastPaymentDate, "
      
        + " IsSelfGeneratedLead, "
      
        + " JobCost, "
      
        + " ConstructionDamageTypeId, "
      
        + " DamageTypeText, "
      
        + " DamageOrigin, "
      
        + " LossDate, "
      
        + " BilledAmount, "
      
        + " LastModifiedDate, "
      
        + " JobNumber "
      

      + " From ProjectConstructionDetail ";
      public static List<ProjectConstructionDetail> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ProjectConstructionDetail> rv = new List<ProjectConstructionDetail>();

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

      public static List<ProjectConstructionDetail> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ProjectConstructionDetail> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ProjectConstructionDetail obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ProjectId == obj.ProjectId && ProjectConstructionProgressId == obj.ProjectConstructionProgressId && ProjectManagerEmployeeId == obj.ProjectManagerEmployeeId && AccountManagerEmployeeId == obj.AccountManagerEmployeeId && ScopeDate == obj.ScopeDate && SignUpDate == obj.SignUpDate && DeclineDate == obj.DeclineDate && EstimatedAmount == obj.EstimatedAmount && ActualStartDate == obj.ActualStartDate && ActualCompletionDate == obj.ActualCompletionDate && SignOffDate == obj.SignOffDate && LastBillingDate == obj.LastBillingDate && LastPaymentDate == obj.LastPaymentDate && IsSelfGeneratedLead == obj.IsSelfGeneratedLead && JobCost == obj.JobCost && ConstructionDamageTypeId == obj.ConstructionDamageTypeId && DamageTypeText == obj.DamageTypeText && DamageOrigin == obj.DamageOrigin && LossDate == obj.LossDate && BilledAmount == obj.BilledAmount && LastModifiedDate == obj.LastModifiedDate && JobNumber == obj.JobNumber;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ProjectConstructionDetail> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectConstructionDetail));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ProjectConstructionDetail item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ProjectConstructionDetail>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectConstructionDetail));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ProjectConstructionDetail> itemsList
      = new List<ProjectConstructionDetail>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ProjectConstructionDetail)
      itemsList.Add(deserializedObject as ProjectConstructionDetail);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_projectId;
      
        protected int m_projectConstructionProgressId;
      
        protected int? m_projectManagerEmployeeId;
      
        protected int? m_accountManagerEmployeeId;
      
        protected DateTime? m_scopeDate;
      
        protected DateTime? m_signUpDate;
      
        protected DateTime? m_declineDate;
      
        protected decimal m_estimatedAmount;
      
        protected DateTime? m_actualStartDate;
      
        protected DateTime? m_actualCompletionDate;
      
        protected DateTime? m_signOffDate;
      
        protected DateTime? m_lastBillingDate;
      
        protected DateTime? m_lastPaymentDate;
      
        protected bool m_isSelfGeneratedLead;
      
        protected decimal m_jobCost;
      
        protected int? m_constructionDamageTypeId;
      
        protected String m_damageTypeText;
      
        protected String m_damageOrigin;
      
        protected DateTime? m_lossDate;
      
        protected decimal m_billedAmount;
      
        protected DateTime m_lastModifiedDate;
      
        protected String m_jobNumber;
      
      #endregion

      #region Constructors
      public ProjectConstructionDetail(
      int 
          projectId
      ) : this()
      {
      
        m_projectId = projectId;
      
      }

      


        public ProjectConstructionDetail(
        int 
          projectId,int 
          projectConstructionProgressId,int? 
          projectManagerEmployeeId,int? 
          accountManagerEmployeeId,DateTime? 
          scopeDate,DateTime? 
          signUpDate,DateTime? 
          declineDate,decimal 
          estimatedAmount,DateTime? 
          actualStartDate,DateTime? 
          actualCompletionDate,DateTime? 
          signOffDate,DateTime? 
          lastBillingDate,DateTime? 
          lastPaymentDate,bool 
          isSelfGeneratedLead,decimal 
          jobCost,int? 
          constructionDamageTypeId,String 
          damageTypeText,String 
          damageOrigin,DateTime? 
          lossDate,decimal 
          billedAmount,DateTime 
          lastModifiedDate,String 
          jobNumber
        ) : this()
        {
        
          m_projectId = projectId;
        
          m_projectConstructionProgressId = projectConstructionProgressId;
        
          m_projectManagerEmployeeId = projectManagerEmployeeId;
        
          m_accountManagerEmployeeId = accountManagerEmployeeId;
        
          m_scopeDate = scopeDate;
        
          m_signUpDate = signUpDate;
        
          m_declineDate = declineDate;
        
          m_estimatedAmount = estimatedAmount;
        
          m_actualStartDate = actualStartDate;
        
          m_actualCompletionDate = actualCompletionDate;
        
          m_signOffDate = signOffDate;
        
          m_lastBillingDate = lastBillingDate;
        
          m_lastPaymentDate = lastPaymentDate;
        
          m_isSelfGeneratedLead = isSelfGeneratedLead;
        
          m_jobCost = jobCost;
        
          m_constructionDamageTypeId = constructionDamageTypeId;
        
          m_damageTypeText = damageTypeText;
        
          m_damageOrigin = damageOrigin;
        
          m_lossDate = lossDate;
        
          m_billedAmount = billedAmount;
        
          m_lastModifiedDate = lastModifiedDate;
        
          m_jobNumber = jobNumber;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ProjectId
        {
        get { return m_projectId;}
        set { m_projectId = value; }
        }
      
        [XmlElement]
        public int ProjectConstructionProgressId
        {
        get { return m_projectConstructionProgressId;}
        set { m_projectConstructionProgressId = value; }
        }
      
        [XmlElement]
        public int? ProjectManagerEmployeeId
        {
        get { return m_projectManagerEmployeeId;}
        set { m_projectManagerEmployeeId = value; }
        }
      
        [XmlElement]
        public int? AccountManagerEmployeeId
        {
        get { return m_accountManagerEmployeeId;}
        set { m_accountManagerEmployeeId = value; }
        }
      
        [XmlElement]
        public DateTime? ScopeDate
        {
        get { return m_scopeDate;}
        set { m_scopeDate = value; }
        }
      
        [XmlElement]
        public DateTime? SignUpDate
        {
        get { return m_signUpDate;}
        set { m_signUpDate = value; }
        }
      
        [XmlElement]
        public DateTime? DeclineDate
        {
        get { return m_declineDate;}
        set { m_declineDate = value; }
        }
      
        [XmlElement]
        public decimal EstimatedAmount
        {
        get { return m_estimatedAmount;}
        set { m_estimatedAmount = value; }
        }
      
        [XmlElement]
        public DateTime? ActualStartDate
        {
        get { return m_actualStartDate;}
        set { m_actualStartDate = value; }
        }
      
        [XmlElement]
        public DateTime? ActualCompletionDate
        {
        get { return m_actualCompletionDate;}
        set { m_actualCompletionDate = value; }
        }
      
        [XmlElement]
        public DateTime? SignOffDate
        {
        get { return m_signOffDate;}
        set { m_signOffDate = value; }
        }
      
        [XmlElement]
        public DateTime? LastBillingDate
        {
        get { return m_lastBillingDate;}
        set { m_lastBillingDate = value; }
        }
      
        [XmlElement]
        public DateTime? LastPaymentDate
        {
        get { return m_lastPaymentDate;}
        set { m_lastPaymentDate = value; }
        }
      
        [XmlElement]
        public bool IsSelfGeneratedLead
        {
        get { return m_isSelfGeneratedLead;}
        set { m_isSelfGeneratedLead = value; }
        }
      
        [XmlElement]
        public decimal JobCost
        {
        get { return m_jobCost;}
        set { m_jobCost = value; }
        }
      
        [XmlElement]
        public int? ConstructionDamageTypeId
        {
        get { return m_constructionDamageTypeId;}
        set { m_constructionDamageTypeId = value; }
        }
      
        [XmlElement]
        public String DamageTypeText
        {
        get { return m_damageTypeText;}
        set { m_damageTypeText = value; }
        }
      
        [XmlElement]
        public String DamageOrigin
        {
        get { return m_damageOrigin;}
        set { m_damageOrigin = value; }
        }
      
        [XmlElement]
        public DateTime? LossDate
        {
        get { return m_lossDate;}
        set { m_lossDate = value; }
        }
      
        [XmlElement]
        public decimal BilledAmount
        {
        get { return m_billedAmount;}
        set { m_billedAmount = value; }
        }
      
        [XmlElement]
        public DateTime LastModifiedDate
        {
        get { return m_lastModifiedDate;}
        set { m_lastModifiedDate = value; }
        }
      
        [XmlElement]
        public String JobNumber
        {
        get { return m_jobNumber;}
        set { m_jobNumber = value; }
        }
      

      public static int FieldsCount
      {
      get { return 22; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    