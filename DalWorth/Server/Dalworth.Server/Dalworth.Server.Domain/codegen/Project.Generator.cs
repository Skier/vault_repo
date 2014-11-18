
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


      public partial class Project : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Project ( " +
      
        " ParentProdjectId, " +
      
        " ProjectTypeId, " +
      
        " CustomerId, " +
      
        " ServiceAddressId, " +
      
        " ProjectStatusId, " +
      
        " LeadId, " +
      
        " Description, " +
      
        " InsuranceCompany, " +
      
        " InsuranceAgency, " +
      
        " InsuranceAgencyPhone, " +
      
        " InsuranceAgent, " +
      
        " InsuranceAdjustor, " +
      
        " InsuranceAdjustorPhone, " +
      
        " ClaimNumber, " +
      
        " PolicyNumber, " +
      
        " DeductibleAmount, " +
      
        " AdvertisingSourceId, " +
      
        " AdvertisingTechnicianId, " +
      
        " DumpedProjectId, " +
      
        " DumpWorkTransactionId, " +
      
        " ClosedAmount, " +
      
        " PaidAmount, " +
      
        " CreateDate, " +
      
        " QbCustomerTypeListId, " +
      
        " QbSalesRepListId " +
      
      ") Values (" +
      
        " ?ParentProdjectId, " +
      
        " ?ProjectTypeId, " +
      
        " ?CustomerId, " +
      
        " ?ServiceAddressId, " +
      
        " ?ProjectStatusId, " +
      
        " ?LeadId, " +
      
        " ?Description, " +
      
        " ?InsuranceCompany, " +
      
        " ?InsuranceAgency, " +
      
        " ?InsuranceAgencyPhone, " +
      
        " ?InsuranceAgent, " +
      
        " ?InsuranceAdjustor, " +
      
        " ?InsuranceAdjustorPhone, " +
      
        " ?ClaimNumber, " +
      
        " ?PolicyNumber, " +
      
        " ?DeductibleAmount, " +
      
        " ?AdvertisingSourceId, " +
      
        " ?AdvertisingTechnicianId, " +
      
        " ?DumpedProjectId, " +
      
        " ?DumpWorkTransactionId, " +
      
        " ?ClosedAmount, " +
      
        " ?PaidAmount, " +
      
        " ?CreateDate, " +
      
        " ?QbCustomerTypeListId, " +
      
        " ?QbSalesRepListId " +
      
      ")";

      public static void Insert(Project project, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ParentProdjectId", project.ParentProdjectId);
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", project.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?CustomerId", project.CustomerId);
      
        Database.PutParameter(dbCommand,"?ServiceAddressId", project.ServiceAddressId);
      
        Database.PutParameter(dbCommand,"?ProjectStatusId", project.ProjectStatusId);
      
        Database.PutParameter(dbCommand,"?LeadId", project.LeadId);
      
        Database.PutParameter(dbCommand,"?Description", project.Description);
      
        Database.PutParameter(dbCommand,"?InsuranceCompany", project.InsuranceCompany);
      
        Database.PutParameter(dbCommand,"?InsuranceAgency", project.InsuranceAgency);
      
        Database.PutParameter(dbCommand,"?InsuranceAgencyPhone", project.InsuranceAgencyPhone);
      
        Database.PutParameter(dbCommand,"?InsuranceAgent", project.InsuranceAgent);
      
        Database.PutParameter(dbCommand,"?InsuranceAdjustor", project.InsuranceAdjustor);
      
        Database.PutParameter(dbCommand,"?InsuranceAdjustorPhone", project.InsuranceAdjustorPhone);
      
        Database.PutParameter(dbCommand,"?ClaimNumber", project.ClaimNumber);
      
        Database.PutParameter(dbCommand,"?PolicyNumber", project.PolicyNumber);
      
        Database.PutParameter(dbCommand,"?DeductibleAmount", project.DeductibleAmount);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceId", project.AdvertisingSourceId);
      
        Database.PutParameter(dbCommand,"?AdvertisingTechnicianId", project.AdvertisingTechnicianId);
      
        Database.PutParameter(dbCommand,"?DumpedProjectId", project.DumpedProjectId);
      
        Database.PutParameter(dbCommand,"?DumpWorkTransactionId", project.DumpWorkTransactionId);
      
        Database.PutParameter(dbCommand,"?ClosedAmount", project.ClosedAmount);
      
        Database.PutParameter(dbCommand,"?PaidAmount", project.PaidAmount);
      
        Database.PutParameter(dbCommand,"?CreateDate", project.CreateDate);
      
        Database.PutParameter(dbCommand,"?QbCustomerTypeListId", project.QbCustomerTypeListId);
      
        Database.PutParameter(dbCommand,"?QbSalesRepListId", project.QbSalesRepListId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        project.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Project project)
      {
        Insert(project, null);
      }


      public static void Insert(List<Project>  projectList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Project project in  projectList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ParentProdjectId", project.ParentProdjectId);
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", project.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?CustomerId", project.CustomerId);
      
        Database.PutParameter(dbCommand,"?ServiceAddressId", project.ServiceAddressId);
      
        Database.PutParameter(dbCommand,"?ProjectStatusId", project.ProjectStatusId);
      
        Database.PutParameter(dbCommand,"?LeadId", project.LeadId);
      
        Database.PutParameter(dbCommand,"?Description", project.Description);
      
        Database.PutParameter(dbCommand,"?InsuranceCompany", project.InsuranceCompany);
      
        Database.PutParameter(dbCommand,"?InsuranceAgency", project.InsuranceAgency);
      
        Database.PutParameter(dbCommand,"?InsuranceAgencyPhone", project.InsuranceAgencyPhone);
      
        Database.PutParameter(dbCommand,"?InsuranceAgent", project.InsuranceAgent);
      
        Database.PutParameter(dbCommand,"?InsuranceAdjustor", project.InsuranceAdjustor);
      
        Database.PutParameter(dbCommand,"?InsuranceAdjustorPhone", project.InsuranceAdjustorPhone);
      
        Database.PutParameter(dbCommand,"?ClaimNumber", project.ClaimNumber);
      
        Database.PutParameter(dbCommand,"?PolicyNumber", project.PolicyNumber);
      
        Database.PutParameter(dbCommand,"?DeductibleAmount", project.DeductibleAmount);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceId", project.AdvertisingSourceId);
      
        Database.PutParameter(dbCommand,"?AdvertisingTechnicianId", project.AdvertisingTechnicianId);
      
        Database.PutParameter(dbCommand,"?DumpedProjectId", project.DumpedProjectId);
      
        Database.PutParameter(dbCommand,"?DumpWorkTransactionId", project.DumpWorkTransactionId);
      
        Database.PutParameter(dbCommand,"?ClosedAmount", project.ClosedAmount);
      
        Database.PutParameter(dbCommand,"?PaidAmount", project.PaidAmount);
      
        Database.PutParameter(dbCommand,"?CreateDate", project.CreateDate);
      
        Database.PutParameter(dbCommand,"?QbCustomerTypeListId", project.QbCustomerTypeListId);
      
        Database.PutParameter(dbCommand,"?QbSalesRepListId", project.QbSalesRepListId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ParentProdjectId",project.ParentProdjectId);
      
        Database.UpdateParameter(dbCommand,"?ProjectTypeId",project.ProjectTypeId);
      
        Database.UpdateParameter(dbCommand,"?CustomerId",project.CustomerId);
      
        Database.UpdateParameter(dbCommand,"?ServiceAddressId",project.ServiceAddressId);
      
        Database.UpdateParameter(dbCommand,"?ProjectStatusId",project.ProjectStatusId);
      
        Database.UpdateParameter(dbCommand,"?LeadId",project.LeadId);
      
        Database.UpdateParameter(dbCommand,"?Description",project.Description);
      
        Database.UpdateParameter(dbCommand,"?InsuranceCompany",project.InsuranceCompany);
      
        Database.UpdateParameter(dbCommand,"?InsuranceAgency",project.InsuranceAgency);
      
        Database.UpdateParameter(dbCommand,"?InsuranceAgencyPhone",project.InsuranceAgencyPhone);
      
        Database.UpdateParameter(dbCommand,"?InsuranceAgent",project.InsuranceAgent);
      
        Database.UpdateParameter(dbCommand,"?InsuranceAdjustor",project.InsuranceAdjustor);
      
        Database.UpdateParameter(dbCommand,"?InsuranceAdjustorPhone",project.InsuranceAdjustorPhone);
      
        Database.UpdateParameter(dbCommand,"?ClaimNumber",project.ClaimNumber);
      
        Database.UpdateParameter(dbCommand,"?PolicyNumber",project.PolicyNumber);
      
        Database.UpdateParameter(dbCommand,"?DeductibleAmount",project.DeductibleAmount);
      
        Database.UpdateParameter(dbCommand,"?AdvertisingSourceId",project.AdvertisingSourceId);
      
        Database.UpdateParameter(dbCommand,"?AdvertisingTechnicianId",project.AdvertisingTechnicianId);
      
        Database.UpdateParameter(dbCommand,"?DumpedProjectId",project.DumpedProjectId);
      
        Database.UpdateParameter(dbCommand,"?DumpWorkTransactionId",project.DumpWorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"?ClosedAmount",project.ClosedAmount);
      
        Database.UpdateParameter(dbCommand,"?PaidAmount",project.PaidAmount);
      
        Database.UpdateParameter(dbCommand,"?CreateDate",project.CreateDate);
      
        Database.UpdateParameter(dbCommand,"?QbCustomerTypeListId",project.QbCustomerTypeListId);
      
        Database.UpdateParameter(dbCommand,"?QbSalesRepListId",project.QbSalesRepListId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        project.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Project>  projectList)
      {
        Insert(projectList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Project Set "
      
        + " ParentProdjectId = ?ParentProdjectId, "
      
        + " ProjectTypeId = ?ProjectTypeId, "
      
        + " CustomerId = ?CustomerId, "
      
        + " ServiceAddressId = ?ServiceAddressId, "
      
        + " ProjectStatusId = ?ProjectStatusId, "
      
        + " LeadId = ?LeadId, "
      
        + " Description = ?Description, "
      
        + " InsuranceCompany = ?InsuranceCompany, "
      
        + " InsuranceAgency = ?InsuranceAgency, "
      
        + " InsuranceAgencyPhone = ?InsuranceAgencyPhone, "
      
        + " InsuranceAgent = ?InsuranceAgent, "
      
        + " InsuranceAdjustor = ?InsuranceAdjustor, "
      
        + " InsuranceAdjustorPhone = ?InsuranceAdjustorPhone, "
      
        + " ClaimNumber = ?ClaimNumber, "
      
        + " PolicyNumber = ?PolicyNumber, "
      
        + " DeductibleAmount = ?DeductibleAmount, "
      
        + " AdvertisingSourceId = ?AdvertisingSourceId, "
      
        + " AdvertisingTechnicianId = ?AdvertisingTechnicianId, "
      
        + " DumpedProjectId = ?DumpedProjectId, "
      
        + " DumpWorkTransactionId = ?DumpWorkTransactionId, "
      
        + " ClosedAmount = ?ClosedAmount, "
      
        + " PaidAmount = ?PaidAmount, "
      
        + " CreateDate = ?CreateDate, "
      
        + " QbCustomerTypeListId = ?QbCustomerTypeListId, "
      
        + " QbSalesRepListId = ?QbSalesRepListId "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Project project, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", project.ID);
      
        Database.PutParameter(dbCommand,"?ParentProdjectId", project.ParentProdjectId);
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", project.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?CustomerId", project.CustomerId);
      
        Database.PutParameter(dbCommand,"?ServiceAddressId", project.ServiceAddressId);
      
        Database.PutParameter(dbCommand,"?ProjectStatusId", project.ProjectStatusId);
      
        Database.PutParameter(dbCommand,"?LeadId", project.LeadId);
      
        Database.PutParameter(dbCommand,"?Description", project.Description);
      
        Database.PutParameter(dbCommand,"?InsuranceCompany", project.InsuranceCompany);
      
        Database.PutParameter(dbCommand,"?InsuranceAgency", project.InsuranceAgency);
      
        Database.PutParameter(dbCommand,"?InsuranceAgencyPhone", project.InsuranceAgencyPhone);
      
        Database.PutParameter(dbCommand,"?InsuranceAgent", project.InsuranceAgent);
      
        Database.PutParameter(dbCommand,"?InsuranceAdjustor", project.InsuranceAdjustor);
      
        Database.PutParameter(dbCommand,"?InsuranceAdjustorPhone", project.InsuranceAdjustorPhone);
      
        Database.PutParameter(dbCommand,"?ClaimNumber", project.ClaimNumber);
      
        Database.PutParameter(dbCommand,"?PolicyNumber", project.PolicyNumber);
      
        Database.PutParameter(dbCommand,"?DeductibleAmount", project.DeductibleAmount);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceId", project.AdvertisingSourceId);
      
        Database.PutParameter(dbCommand,"?AdvertisingTechnicianId", project.AdvertisingTechnicianId);
      
        Database.PutParameter(dbCommand,"?DumpedProjectId", project.DumpedProjectId);
      
        Database.PutParameter(dbCommand,"?DumpWorkTransactionId", project.DumpWorkTransactionId);
      
        Database.PutParameter(dbCommand,"?ClosedAmount", project.ClosedAmount);
      
        Database.PutParameter(dbCommand,"?PaidAmount", project.PaidAmount);
      
        Database.PutParameter(dbCommand,"?CreateDate", project.CreateDate);
      
        Database.PutParameter(dbCommand,"?QbCustomerTypeListId", project.QbCustomerTypeListId);
      
        Database.PutParameter(dbCommand,"?QbSalesRepListId", project.QbSalesRepListId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Project project)
      {
        Update(project, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " ParentProdjectId, "
      
        + " ProjectTypeId, "
      
        + " CustomerId, "
      
        + " ServiceAddressId, "
      
        + " ProjectStatusId, "
      
        + " LeadId, "
      
        + " Description, "
      
        + " InsuranceCompany, "
      
        + " InsuranceAgency, "
      
        + " InsuranceAgencyPhone, "
      
        + " InsuranceAgent, "
      
        + " InsuranceAdjustor, "
      
        + " InsuranceAdjustorPhone, "
      
        + " ClaimNumber, "
      
        + " PolicyNumber, "
      
        + " DeductibleAmount, "
      
        + " AdvertisingSourceId, "
      
        + " AdvertisingTechnicianId, "
      
        + " DumpedProjectId, "
      
        + " DumpWorkTransactionId, "
      
        + " ClosedAmount, "
      
        + " PaidAmount, "
      
        + " CreateDate, "
      
        + " QbCustomerTypeListId, "
      
        + " QbSalesRepListId "
      

      + " From Project "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Project FindByPrimaryKey(
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
      throw new DataNotFoundException("Project not found, search by primary key");

      }

      public static Project FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Project project, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",project.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Project project)
      {
      return Exists(project, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Project limit 1";

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

      public static Project Load(IDataReader dataReader, int offset)
      {
      Project project = new Project();

      project.ID = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            project.ParentProdjectId = dataReader.GetInt32(1 + offset);
          project.ProjectTypeId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            project.CustomerId = dataReader.GetInt32(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            project.ServiceAddressId = dataReader.GetInt32(4 + offset);
          project.ProjectStatusId = dataReader.GetInt32(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            project.LeadId = dataReader.GetInt32(6 + offset);
          project.Description = dataReader.GetString(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            project.InsuranceCompany = dataReader.GetString(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            project.InsuranceAgency = dataReader.GetString(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            project.InsuranceAgencyPhone = dataReader.GetString(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            project.InsuranceAgent = dataReader.GetString(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            project.InsuranceAdjustor = dataReader.GetString(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            project.InsuranceAdjustorPhone = dataReader.GetString(13 + offset);
          
            if(!dataReader.IsDBNull(14 + offset))
            project.ClaimNumber = dataReader.GetString(14 + offset);
          
            if(!dataReader.IsDBNull(15 + offset))
            project.PolicyNumber = dataReader.GetString(15 + offset);
          
            if(!dataReader.IsDBNull(16 + offset))
            project.DeductibleAmount = dataReader.GetDecimal(16 + offset);
          
            if(!dataReader.IsDBNull(17 + offset))
            project.AdvertisingSourceId = dataReader.GetInt32(17 + offset);
          
            if(!dataReader.IsDBNull(18 + offset))
            project.AdvertisingTechnicianId = dataReader.GetInt32(18 + offset);
          
            if(!dataReader.IsDBNull(19 + offset))
            project.DumpedProjectId = dataReader.GetInt32(19 + offset);
          
            if(!dataReader.IsDBNull(20 + offset))
            project.DumpWorkTransactionId = dataReader.GetInt32(20 + offset);
          project.ClosedAmount = dataReader.GetDecimal(21 + offset);
          project.PaidAmount = dataReader.GetDecimal(22 + offset);
          project.CreateDate = dataReader.GetDateTime(23 + offset);
          
            if(!dataReader.IsDBNull(24 + offset))
            project.QbCustomerTypeListId = dataReader.GetString(24 + offset);
          
            if(!dataReader.IsDBNull(25 + offset))
            project.QbSalesRepListId = dataReader.GetString(25 + offset);
          

      return project;
      }

      public static Project Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Project "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Project project, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", project.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Project project)
      {
        Delete(project, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Project ";

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
      
        + " ParentProdjectId, "
      
        + " ProjectTypeId, "
      
        + " CustomerId, "
      
        + " ServiceAddressId, "
      
        + " ProjectStatusId, "
      
        + " LeadId, "
      
        + " Description, "
      
        + " InsuranceCompany, "
      
        + " InsuranceAgency, "
      
        + " InsuranceAgencyPhone, "
      
        + " InsuranceAgent, "
      
        + " InsuranceAdjustor, "
      
        + " InsuranceAdjustorPhone, "
      
        + " ClaimNumber, "
      
        + " PolicyNumber, "
      
        + " DeductibleAmount, "
      
        + " AdvertisingSourceId, "
      
        + " AdvertisingTechnicianId, "
      
        + " DumpedProjectId, "
      
        + " DumpWorkTransactionId, "
      
        + " ClosedAmount, "
      
        + " PaidAmount, "
      
        + " CreateDate, "
      
        + " QbCustomerTypeListId, "
      
        + " QbSalesRepListId "
      

      + " From Project ";
      public static List<Project> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Project> rv = new List<Project>();

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

      public static List<Project> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Project> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Project obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && ParentProdjectId == obj.ParentProdjectId && ProjectTypeId == obj.ProjectTypeId && CustomerId == obj.CustomerId && ServiceAddressId == obj.ServiceAddressId && ProjectStatusId == obj.ProjectStatusId && LeadId == obj.LeadId && Description == obj.Description && InsuranceCompany == obj.InsuranceCompany && InsuranceAgency == obj.InsuranceAgency && InsuranceAgencyPhone == obj.InsuranceAgencyPhone && InsuranceAgent == obj.InsuranceAgent && InsuranceAdjustor == obj.InsuranceAdjustor && InsuranceAdjustorPhone == obj.InsuranceAdjustorPhone && ClaimNumber == obj.ClaimNumber && PolicyNumber == obj.PolicyNumber && DeductibleAmount == obj.DeductibleAmount && AdvertisingSourceId == obj.AdvertisingSourceId && AdvertisingTechnicianId == obj.AdvertisingTechnicianId && DumpedProjectId == obj.DumpedProjectId && DumpWorkTransactionId == obj.DumpWorkTransactionId && ClosedAmount == obj.ClosedAmount && PaidAmount == obj.PaidAmount && CreateDate == obj.CreateDate && QbCustomerTypeListId == obj.QbCustomerTypeListId && QbSalesRepListId == obj.QbSalesRepListId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Project> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Project));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Project item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Project>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Project));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Project> itemsList
      = new List<Project>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Project)
      itemsList.Add(deserializedObject as Project);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int? m_parentProdjectId;
      
        protected int m_projectTypeId;
      
        protected int? m_customerId;
      
        protected int? m_serviceAddressId;
      
        protected int m_projectStatusId;
      
        protected int? m_leadId;
      
        protected String m_description;
      
        protected String m_insuranceCompany;
      
        protected String m_insuranceAgency;
      
        protected String m_insuranceAgencyPhone;
      
        protected String m_insuranceAgent;
      
        protected String m_insuranceAdjustor;
      
        protected String m_insuranceAdjustorPhone;
      
        protected String m_claimNumber;
      
        protected String m_policyNumber;
      
        protected decimal m_deductibleAmount;
      
        protected int? m_advertisingSourceId;
      
        protected int? m_advertisingTechnicianId;
      
        protected int? m_dumpedProjectId;
      
        protected int? m_dumpWorkTransactionId;
      
        protected decimal m_closedAmount;
      
        protected decimal m_paidAmount;
      
        protected DateTime m_createDate;
      
        protected String m_qbCustomerTypeListId;
      
        protected String m_qbSalesRepListId;
      
      #endregion

      #region Constructors
      public Project(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Project(
        int 
          iD,int? 
          parentProdjectId,int 
          projectTypeId,int? 
          customerId,int? 
          serviceAddressId,int 
          projectStatusId,int? 
          leadId,String 
          description,String 
          insuranceCompany,String 
          insuranceAgency,String 
          insuranceAgencyPhone,String 
          insuranceAgent,String 
          insuranceAdjustor,String 
          insuranceAdjustorPhone,String 
          claimNumber,String 
          policyNumber,decimal 
          deductibleAmount,int? 
          advertisingSourceId,int? 
          advertisingTechnicianId,int? 
          dumpedProjectId,int? 
          dumpWorkTransactionId,decimal 
          closedAmount,decimal 
          paidAmount,DateTime 
          createDate,String 
          qbCustomerTypeListId,String 
          qbSalesRepListId
        ) : this()
        {
        
          m_iD = iD;
        
          m_parentProdjectId = parentProdjectId;
        
          m_projectTypeId = projectTypeId;
        
          m_customerId = customerId;
        
          m_serviceAddressId = serviceAddressId;
        
          m_projectStatusId = projectStatusId;
        
          m_leadId = leadId;
        
          m_description = description;
        
          m_insuranceCompany = insuranceCompany;
        
          m_insuranceAgency = insuranceAgency;
        
          m_insuranceAgencyPhone = insuranceAgencyPhone;
        
          m_insuranceAgent = insuranceAgent;
        
          m_insuranceAdjustor = insuranceAdjustor;
        
          m_insuranceAdjustorPhone = insuranceAdjustorPhone;
        
          m_claimNumber = claimNumber;
        
          m_policyNumber = policyNumber;
        
          m_deductibleAmount = deductibleAmount;
        
          m_advertisingSourceId = advertisingSourceId;
        
          m_advertisingTechnicianId = advertisingTechnicianId;
        
          m_dumpedProjectId = dumpedProjectId;
        
          m_dumpWorkTransactionId = dumpWorkTransactionId;
        
          m_closedAmount = closedAmount;
        
          m_paidAmount = paidAmount;
        
          m_createDate = createDate;
        
          m_qbCustomerTypeListId = qbCustomerTypeListId;
        
          m_qbSalesRepListId = qbSalesRepListId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int? ParentProdjectId
        {
        get { return m_parentProdjectId;}
        set { m_parentProdjectId = value; }
        }
      
        [XmlElement]
        public int ProjectTypeId
        {
        get { return m_projectTypeId;}
        set { m_projectTypeId = value; }
        }
      
        [XmlElement]
        public int? CustomerId
        {
        get { return m_customerId;}
        set { m_customerId = value; }
        }
      
        [XmlElement]
        public int? ServiceAddressId
        {
        get { return m_serviceAddressId;}
        set { m_serviceAddressId = value; }
        }
      
        [XmlElement]
        public int ProjectStatusId
        {
        get { return m_projectStatusId;}
        set { m_projectStatusId = value; }
        }
      
        [XmlElement]
        public int? LeadId
        {
        get { return m_leadId;}
        set { m_leadId = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
        [XmlElement]
        public String InsuranceCompany
        {
        get { return m_insuranceCompany;}
        set { m_insuranceCompany = value; }
        }
      
        [XmlElement]
        public String InsuranceAgency
        {
        get { return m_insuranceAgency;}
        set { m_insuranceAgency = value; }
        }
      
        [XmlElement]
        public String InsuranceAgencyPhone
        {
        get { return m_insuranceAgencyPhone;}
        set { m_insuranceAgencyPhone = value; }
        }
      
        [XmlElement]
        public String InsuranceAgent
        {
        get { return m_insuranceAgent;}
        set { m_insuranceAgent = value; }
        }
      
        [XmlElement]
        public String InsuranceAdjustor
        {
        get { return m_insuranceAdjustor;}
        set { m_insuranceAdjustor = value; }
        }
      
        [XmlElement]
        public String InsuranceAdjustorPhone
        {
        get { return m_insuranceAdjustorPhone;}
        set { m_insuranceAdjustorPhone = value; }
        }
      
        [XmlElement]
        public String ClaimNumber
        {
        get { return m_claimNumber;}
        set { m_claimNumber = value; }
        }
      
        [XmlElement]
        public String PolicyNumber
        {
        get { return m_policyNumber;}
        set { m_policyNumber = value; }
        }
      
        [XmlElement]
        public decimal DeductibleAmount
        {
        get { return m_deductibleAmount;}
        set { m_deductibleAmount = value; }
        }
      
        [XmlElement]
        public int? AdvertisingSourceId
        {
        get { return m_advertisingSourceId;}
        set { m_advertisingSourceId = value; }
        }
      
        [XmlElement]
        public int? AdvertisingTechnicianId
        {
        get { return m_advertisingTechnicianId;}
        set { m_advertisingTechnicianId = value; }
        }
      
        [XmlElement]
        public int? DumpedProjectId
        {
        get { return m_dumpedProjectId;}
        set { m_dumpedProjectId = value; }
        }
      
        [XmlElement]
        public int? DumpWorkTransactionId
        {
        get { return m_dumpWorkTransactionId;}
        set { m_dumpWorkTransactionId = value; }
        }
      
        [XmlElement]
        public decimal ClosedAmount
        {
        get { return m_closedAmount;}
        set { m_closedAmount = value; }
        }
      
        [XmlElement]
        public decimal PaidAmount
        {
        get { return m_paidAmount;}
        set { m_paidAmount = value; }
        }
      
        [XmlElement]
        public DateTime CreateDate
        {
        get { return m_createDate;}
        set { m_createDate = value; }
        }
      
        [XmlElement]
        public String QbCustomerTypeListId
        {
        get { return m_qbCustomerTypeListId;}
        set { m_qbCustomerTypeListId = value; }
        }
      
        [XmlElement]
        public String QbSalesRepListId
        {
        get { return m_qbSalesRepListId;}
        set { m_qbSalesRepListId = value; }
        }
      

      public static int FieldsCount
      {
      get { return 26; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    