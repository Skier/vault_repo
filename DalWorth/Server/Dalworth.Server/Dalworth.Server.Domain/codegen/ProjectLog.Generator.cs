
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


      public partial class ProjectLog : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ProjectLog ( " +
      
        " EmployeeId, " +
      
        " DateCreated, " +
      
        " Trace, " +
      
        " ProjectId, " +
      
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
      
        " ?EmployeeId, " +
      
        " ?DateCreated, " +
      
        " ?Trace, " +
      
        " ?ProjectId, " +
      
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

      public static void Insert(ProjectLog projectLog, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?EmployeeId", projectLog.EmployeeId);
      
        Database.PutParameter(dbCommand,"?DateCreated", projectLog.DateCreated);
      
        Database.PutParameter(dbCommand,"?Trace", projectLog.Trace);
      
        Database.PutParameter(dbCommand,"?ProjectId", projectLog.ProjectId);
      
        Database.PutParameter(dbCommand,"?ParentProdjectId", projectLog.ParentProdjectId);
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", projectLog.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?CustomerId", projectLog.CustomerId);
      
        Database.PutParameter(dbCommand,"?ServiceAddressId", projectLog.ServiceAddressId);
      
        Database.PutParameter(dbCommand,"?ProjectStatusId", projectLog.ProjectStatusId);
      
        Database.PutParameter(dbCommand,"?LeadId", projectLog.LeadId);
      
        Database.PutParameter(dbCommand,"?Description", projectLog.Description);
      
        Database.PutParameter(dbCommand,"?InsuranceCompany", projectLog.InsuranceCompany);
      
        Database.PutParameter(dbCommand,"?InsuranceAgency", projectLog.InsuranceAgency);
      
        Database.PutParameter(dbCommand,"?InsuranceAgencyPhone", projectLog.InsuranceAgencyPhone);
      
        Database.PutParameter(dbCommand,"?InsuranceAgent", projectLog.InsuranceAgent);
      
        Database.PutParameter(dbCommand,"?InsuranceAdjustor", projectLog.InsuranceAdjustor);
      
        Database.PutParameter(dbCommand,"?InsuranceAdjustorPhone", projectLog.InsuranceAdjustorPhone);
      
        Database.PutParameter(dbCommand,"?ClaimNumber", projectLog.ClaimNumber);
      
        Database.PutParameter(dbCommand,"?PolicyNumber", projectLog.PolicyNumber);
      
        Database.PutParameter(dbCommand,"?DeductibleAmount", projectLog.DeductibleAmount);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceId", projectLog.AdvertisingSourceId);
      
        Database.PutParameter(dbCommand,"?AdvertisingTechnicianId", projectLog.AdvertisingTechnicianId);
      
        Database.PutParameter(dbCommand,"?DumpedProjectId", projectLog.DumpedProjectId);
      
        Database.PutParameter(dbCommand,"?DumpWorkTransactionId", projectLog.DumpWorkTransactionId);
      
        Database.PutParameter(dbCommand,"?ClosedAmount", projectLog.ClosedAmount);
      
        Database.PutParameter(dbCommand,"?PaidAmount", projectLog.PaidAmount);
      
        Database.PutParameter(dbCommand,"?CreateDate", projectLog.CreateDate);
      
        Database.PutParameter(dbCommand,"?QbCustomerTypeListId", projectLog.QbCustomerTypeListId);
      
        Database.PutParameter(dbCommand,"?QbSalesRepListId", projectLog.QbSalesRepListId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        projectLog.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(ProjectLog projectLog)
      {
        Insert(projectLog, null);
      }


      public static void Insert(List<ProjectLog>  projectLogList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ProjectLog projectLog in  projectLogList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?EmployeeId", projectLog.EmployeeId);
      
        Database.PutParameter(dbCommand,"?DateCreated", projectLog.DateCreated);
      
        Database.PutParameter(dbCommand,"?Trace", projectLog.Trace);
      
        Database.PutParameter(dbCommand,"?ProjectId", projectLog.ProjectId);
      
        Database.PutParameter(dbCommand,"?ParentProdjectId", projectLog.ParentProdjectId);
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", projectLog.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?CustomerId", projectLog.CustomerId);
      
        Database.PutParameter(dbCommand,"?ServiceAddressId", projectLog.ServiceAddressId);
      
        Database.PutParameter(dbCommand,"?ProjectStatusId", projectLog.ProjectStatusId);
      
        Database.PutParameter(dbCommand,"?LeadId", projectLog.LeadId);
      
        Database.PutParameter(dbCommand,"?Description", projectLog.Description);
      
        Database.PutParameter(dbCommand,"?InsuranceCompany", projectLog.InsuranceCompany);
      
        Database.PutParameter(dbCommand,"?InsuranceAgency", projectLog.InsuranceAgency);
      
        Database.PutParameter(dbCommand,"?InsuranceAgencyPhone", projectLog.InsuranceAgencyPhone);
      
        Database.PutParameter(dbCommand,"?InsuranceAgent", projectLog.InsuranceAgent);
      
        Database.PutParameter(dbCommand,"?InsuranceAdjustor", projectLog.InsuranceAdjustor);
      
        Database.PutParameter(dbCommand,"?InsuranceAdjustorPhone", projectLog.InsuranceAdjustorPhone);
      
        Database.PutParameter(dbCommand,"?ClaimNumber", projectLog.ClaimNumber);
      
        Database.PutParameter(dbCommand,"?PolicyNumber", projectLog.PolicyNumber);
      
        Database.PutParameter(dbCommand,"?DeductibleAmount", projectLog.DeductibleAmount);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceId", projectLog.AdvertisingSourceId);
      
        Database.PutParameter(dbCommand,"?AdvertisingTechnicianId", projectLog.AdvertisingTechnicianId);
      
        Database.PutParameter(dbCommand,"?DumpedProjectId", projectLog.DumpedProjectId);
      
        Database.PutParameter(dbCommand,"?DumpWorkTransactionId", projectLog.DumpWorkTransactionId);
      
        Database.PutParameter(dbCommand,"?ClosedAmount", projectLog.ClosedAmount);
      
        Database.PutParameter(dbCommand,"?PaidAmount", projectLog.PaidAmount);
      
        Database.PutParameter(dbCommand,"?CreateDate", projectLog.CreateDate);
      
        Database.PutParameter(dbCommand,"?QbCustomerTypeListId", projectLog.QbCustomerTypeListId);
      
        Database.PutParameter(dbCommand,"?QbSalesRepListId", projectLog.QbSalesRepListId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?EmployeeId",projectLog.EmployeeId);
      
        Database.UpdateParameter(dbCommand,"?DateCreated",projectLog.DateCreated);
      
        Database.UpdateParameter(dbCommand,"?Trace",projectLog.Trace);
      
        Database.UpdateParameter(dbCommand,"?ProjectId",projectLog.ProjectId);
      
        Database.UpdateParameter(dbCommand,"?ParentProdjectId",projectLog.ParentProdjectId);
      
        Database.UpdateParameter(dbCommand,"?ProjectTypeId",projectLog.ProjectTypeId);
      
        Database.UpdateParameter(dbCommand,"?CustomerId",projectLog.CustomerId);
      
        Database.UpdateParameter(dbCommand,"?ServiceAddressId",projectLog.ServiceAddressId);
      
        Database.UpdateParameter(dbCommand,"?ProjectStatusId",projectLog.ProjectStatusId);
      
        Database.UpdateParameter(dbCommand,"?LeadId",projectLog.LeadId);
      
        Database.UpdateParameter(dbCommand,"?Description",projectLog.Description);
      
        Database.UpdateParameter(dbCommand,"?InsuranceCompany",projectLog.InsuranceCompany);
      
        Database.UpdateParameter(dbCommand,"?InsuranceAgency",projectLog.InsuranceAgency);
      
        Database.UpdateParameter(dbCommand,"?InsuranceAgencyPhone",projectLog.InsuranceAgencyPhone);
      
        Database.UpdateParameter(dbCommand,"?InsuranceAgent",projectLog.InsuranceAgent);
      
        Database.UpdateParameter(dbCommand,"?InsuranceAdjustor",projectLog.InsuranceAdjustor);
      
        Database.UpdateParameter(dbCommand,"?InsuranceAdjustorPhone",projectLog.InsuranceAdjustorPhone);
      
        Database.UpdateParameter(dbCommand,"?ClaimNumber",projectLog.ClaimNumber);
      
        Database.UpdateParameter(dbCommand,"?PolicyNumber",projectLog.PolicyNumber);
      
        Database.UpdateParameter(dbCommand,"?DeductibleAmount",projectLog.DeductibleAmount);
      
        Database.UpdateParameter(dbCommand,"?AdvertisingSourceId",projectLog.AdvertisingSourceId);
      
        Database.UpdateParameter(dbCommand,"?AdvertisingTechnicianId",projectLog.AdvertisingTechnicianId);
      
        Database.UpdateParameter(dbCommand,"?DumpedProjectId",projectLog.DumpedProjectId);
      
        Database.UpdateParameter(dbCommand,"?DumpWorkTransactionId",projectLog.DumpWorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"?ClosedAmount",projectLog.ClosedAmount);
      
        Database.UpdateParameter(dbCommand,"?PaidAmount",projectLog.PaidAmount);
      
        Database.UpdateParameter(dbCommand,"?CreateDate",projectLog.CreateDate);
      
        Database.UpdateParameter(dbCommand,"?QbCustomerTypeListId",projectLog.QbCustomerTypeListId);
      
        Database.UpdateParameter(dbCommand,"?QbSalesRepListId",projectLog.QbSalesRepListId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        projectLog.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<ProjectLog>  projectLogList)
      {
        Insert(projectLogList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ProjectLog Set "
      
        + " EmployeeId = ?EmployeeId, "
      
        + " DateCreated = ?DateCreated, "
      
        + " Trace = ?Trace, "
      
        + " ProjectId = ?ProjectId, "
      
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

      public static void Update(ProjectLog projectLog, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", projectLog.ID);
      
        Database.PutParameter(dbCommand,"?EmployeeId", projectLog.EmployeeId);
      
        Database.PutParameter(dbCommand,"?DateCreated", projectLog.DateCreated);
      
        Database.PutParameter(dbCommand,"?Trace", projectLog.Trace);
      
        Database.PutParameter(dbCommand,"?ProjectId", projectLog.ProjectId);
      
        Database.PutParameter(dbCommand,"?ParentProdjectId", projectLog.ParentProdjectId);
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", projectLog.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?CustomerId", projectLog.CustomerId);
      
        Database.PutParameter(dbCommand,"?ServiceAddressId", projectLog.ServiceAddressId);
      
        Database.PutParameter(dbCommand,"?ProjectStatusId", projectLog.ProjectStatusId);
      
        Database.PutParameter(dbCommand,"?LeadId", projectLog.LeadId);
      
        Database.PutParameter(dbCommand,"?Description", projectLog.Description);
      
        Database.PutParameter(dbCommand,"?InsuranceCompany", projectLog.InsuranceCompany);
      
        Database.PutParameter(dbCommand,"?InsuranceAgency", projectLog.InsuranceAgency);
      
        Database.PutParameter(dbCommand,"?InsuranceAgencyPhone", projectLog.InsuranceAgencyPhone);
      
        Database.PutParameter(dbCommand,"?InsuranceAgent", projectLog.InsuranceAgent);
      
        Database.PutParameter(dbCommand,"?InsuranceAdjustor", projectLog.InsuranceAdjustor);
      
        Database.PutParameter(dbCommand,"?InsuranceAdjustorPhone", projectLog.InsuranceAdjustorPhone);
      
        Database.PutParameter(dbCommand,"?ClaimNumber", projectLog.ClaimNumber);
      
        Database.PutParameter(dbCommand,"?PolicyNumber", projectLog.PolicyNumber);
      
        Database.PutParameter(dbCommand,"?DeductibleAmount", projectLog.DeductibleAmount);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceId", projectLog.AdvertisingSourceId);
      
        Database.PutParameter(dbCommand,"?AdvertisingTechnicianId", projectLog.AdvertisingTechnicianId);
      
        Database.PutParameter(dbCommand,"?DumpedProjectId", projectLog.DumpedProjectId);
      
        Database.PutParameter(dbCommand,"?DumpWorkTransactionId", projectLog.DumpWorkTransactionId);
      
        Database.PutParameter(dbCommand,"?ClosedAmount", projectLog.ClosedAmount);
      
        Database.PutParameter(dbCommand,"?PaidAmount", projectLog.PaidAmount);
      
        Database.PutParameter(dbCommand,"?CreateDate", projectLog.CreateDate);
      
        Database.PutParameter(dbCommand,"?QbCustomerTypeListId", projectLog.QbCustomerTypeListId);
      
        Database.PutParameter(dbCommand,"?QbSalesRepListId", projectLog.QbSalesRepListId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ProjectLog projectLog)
      {
        Update(projectLog, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " EmployeeId, "
      
        + " DateCreated, "
      
        + " Trace, "
      
        + " ProjectId, "
      
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
      

      + " From ProjectLog "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static ProjectLog FindByPrimaryKey(
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
      throw new DataNotFoundException("ProjectLog not found, search by primary key");

      }

      public static ProjectLog FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ProjectLog projectLog, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",projectLog.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ProjectLog projectLog)
      {
      return Exists(projectLog, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ProjectLog limit 1";

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

      public static ProjectLog Load(IDataReader dataReader, int offset)
      {
      ProjectLog projectLog = new ProjectLog();

      projectLog.ID = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            projectLog.EmployeeId = dataReader.GetInt32(1 + offset);
          projectLog.DateCreated = dataReader.GetDateTime(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            projectLog.Trace = dataReader.GetString(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            projectLog.ProjectId = dataReader.GetInt32(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            projectLog.ParentProdjectId = dataReader.GetInt32(5 + offset);
          projectLog.ProjectTypeId = dataReader.GetInt32(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            projectLog.CustomerId = dataReader.GetInt32(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            projectLog.ServiceAddressId = dataReader.GetInt32(8 + offset);
          projectLog.ProjectStatusId = dataReader.GetInt32(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            projectLog.LeadId = dataReader.GetInt32(10 + offset);
          projectLog.Description = dataReader.GetString(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            projectLog.InsuranceCompany = dataReader.GetString(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            projectLog.InsuranceAgency = dataReader.GetString(13 + offset);
          
            if(!dataReader.IsDBNull(14 + offset))
            projectLog.InsuranceAgencyPhone = dataReader.GetString(14 + offset);
          
            if(!dataReader.IsDBNull(15 + offset))
            projectLog.InsuranceAgent = dataReader.GetString(15 + offset);
          
            if(!dataReader.IsDBNull(16 + offset))
            projectLog.InsuranceAdjustor = dataReader.GetString(16 + offset);
          
            if(!dataReader.IsDBNull(17 + offset))
            projectLog.InsuranceAdjustorPhone = dataReader.GetString(17 + offset);
          
            if(!dataReader.IsDBNull(18 + offset))
            projectLog.ClaimNumber = dataReader.GetString(18 + offset);
          
            if(!dataReader.IsDBNull(19 + offset))
            projectLog.PolicyNumber = dataReader.GetString(19 + offset);
          
            if(!dataReader.IsDBNull(20 + offset))
            projectLog.DeductibleAmount = dataReader.GetDecimal(20 + offset);
          
            if(!dataReader.IsDBNull(21 + offset))
            projectLog.AdvertisingSourceId = dataReader.GetInt32(21 + offset);
          
            if(!dataReader.IsDBNull(22 + offset))
            projectLog.AdvertisingTechnicianId = dataReader.GetInt32(22 + offset);
          
            if(!dataReader.IsDBNull(23 + offset))
            projectLog.DumpedProjectId = dataReader.GetInt32(23 + offset);
          
            if(!dataReader.IsDBNull(24 + offset))
            projectLog.DumpWorkTransactionId = dataReader.GetInt32(24 + offset);
          projectLog.ClosedAmount = dataReader.GetDecimal(25 + offset);
          projectLog.PaidAmount = dataReader.GetDecimal(26 + offset);
          projectLog.CreateDate = dataReader.GetDateTime(27 + offset);
          
            if(!dataReader.IsDBNull(28 + offset))
            projectLog.QbCustomerTypeListId = dataReader.GetString(28 + offset);
          
            if(!dataReader.IsDBNull(29 + offset))
            projectLog.QbSalesRepListId = dataReader.GetString(29 + offset);
          

      return projectLog;
      }

      public static ProjectLog Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ProjectLog "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(ProjectLog projectLog, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", projectLog.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectLog projectLog)
      {
        Delete(projectLog, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ProjectLog ";

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
      
        + " EmployeeId, "
      
        + " DateCreated, "
      
        + " Trace, "
      
        + " ProjectId, "
      
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
      

      + " From ProjectLog ";
      public static List<ProjectLog> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ProjectLog> rv = new List<ProjectLog>();

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

      public static List<ProjectLog> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ProjectLog> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ProjectLog obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && EmployeeId == obj.EmployeeId && DateCreated == obj.DateCreated && Trace == obj.Trace && ProjectId == obj.ProjectId && ParentProdjectId == obj.ParentProdjectId && ProjectTypeId == obj.ProjectTypeId && CustomerId == obj.CustomerId && ServiceAddressId == obj.ServiceAddressId && ProjectStatusId == obj.ProjectStatusId && LeadId == obj.LeadId && Description == obj.Description && InsuranceCompany == obj.InsuranceCompany && InsuranceAgency == obj.InsuranceAgency && InsuranceAgencyPhone == obj.InsuranceAgencyPhone && InsuranceAgent == obj.InsuranceAgent && InsuranceAdjustor == obj.InsuranceAdjustor && InsuranceAdjustorPhone == obj.InsuranceAdjustorPhone && ClaimNumber == obj.ClaimNumber && PolicyNumber == obj.PolicyNumber && DeductibleAmount == obj.DeductibleAmount && AdvertisingSourceId == obj.AdvertisingSourceId && AdvertisingTechnicianId == obj.AdvertisingTechnicianId && DumpedProjectId == obj.DumpedProjectId && DumpWorkTransactionId == obj.DumpWorkTransactionId && ClosedAmount == obj.ClosedAmount && PaidAmount == obj.PaidAmount && CreateDate == obj.CreateDate && QbCustomerTypeListId == obj.QbCustomerTypeListId && QbSalesRepListId == obj.QbSalesRepListId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ProjectLog> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectLog));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ProjectLog item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ProjectLog>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectLog));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ProjectLog> itemsList
      = new List<ProjectLog>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ProjectLog)
      itemsList.Add(deserializedObject as ProjectLog);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int? m_employeeId;
      
        protected DateTime m_dateCreated;
      
        protected String m_trace;
      
        protected int? m_projectId;
      
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
      public ProjectLog(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public ProjectLog(
        int 
          iD,int? 
          employeeId,DateTime 
          dateCreated,String 
          trace,int? 
          projectId,int? 
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
        
          m_employeeId = employeeId;
        
          m_dateCreated = dateCreated;
        
          m_trace = trace;
        
          m_projectId = projectId;
        
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
        public int? EmployeeId
        {
        get { return m_employeeId;}
        set { m_employeeId = value; }
        }
      
        [XmlElement]
        public DateTime DateCreated
        {
        get { return m_dateCreated;}
        set { m_dateCreated = value; }
        }
      
        [XmlElement]
        public String Trace
        {
        get { return m_trace;}
        set { m_trace = value; }
        }
      
        [XmlElement]
        public int? ProjectId
        {
        get { return m_projectId;}
        set { m_projectId = value; }
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
      get { return 30; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    