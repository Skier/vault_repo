
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


      public partial class Lead : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Lead ( " +
      
        " LeadStatusId, " +
      
        " ProjectTypeId, " +
      
        " EmployeeId, " +
      
        " CustomerWebAccountId, " +
      
        " WebLogId, " +
      
        " BusinessPartnerId, " +
      
        " Company, " +
      
        " FirstName, " +
      
        " LastName, " +
      
        " Address1, " +
      
        " Address2, " +
      
        " City, " +
      
        " State, " +
      
        " Zip, " +
      
        " Phone1, " +
      
        " Phone2, " +
      
        " Email, " +
      
        " CustomerNotes, " +
      
        " DispatchNotes, " +
      
        " PreferredServiceDate, " +
      
        " PreferredTime, " +
      
        " DateCreated, " +
      
        " DateCancelled, " +
      
        " KeywordId, " +
      
        " AdvertisingSourceAcronym, " +
      
        " ServmanAdvertisingSource, " +
      
        " ServmanTrackCode, " +
      
        " DateLateNotificationSent, " +
      
        " DateFirstSetPending, " +
      
        " DateLastSetPending, " +
      
        " FirstUpdateEmployeeId, " +
      
        " LastUpdateEmployeeId " +
      
      ") Values (" +
      
        " ?LeadStatusId, " +
      
        " ?ProjectTypeId, " +
      
        " ?EmployeeId, " +
      
        " ?CustomerWebAccountId, " +
      
        " ?WebLogId, " +
      
        " ?BusinessPartnerId, " +
      
        " ?Company, " +
      
        " ?FirstName, " +
      
        " ?LastName, " +
      
        " ?Address1, " +
      
        " ?Address2, " +
      
        " ?City, " +
      
        " ?State, " +
      
        " ?Zip, " +
      
        " ?Phone1, " +
      
        " ?Phone2, " +
      
        " ?Email, " +
      
        " ?CustomerNotes, " +
      
        " ?DispatchNotes, " +
      
        " ?PreferredServiceDate, " +
      
        " ?PreferredTime, " +
      
        " ?DateCreated, " +
      
        " ?DateCancelled, " +
      
        " ?KeywordId, " +
      
        " ?AdvertisingSourceAcronym, " +
      
        " ?ServmanAdvertisingSource, " +
      
        " ?ServmanTrackCode, " +
      
        " ?DateLateNotificationSent, " +
      
        " ?DateFirstSetPending, " +
      
        " ?DateLastSetPending, " +
      
        " ?FirstUpdateEmployeeId, " +
      
        " ?LastUpdateEmployeeId " +
      
      ")";

      public static void Insert(Lead lead, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?LeadStatusId", lead.LeadStatusId);
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", lead.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?EmployeeId", lead.EmployeeId);
      
        Database.PutParameter(dbCommand,"?CustomerWebAccountId", lead.CustomerWebAccountId);
      
        Database.PutParameter(dbCommand,"?WebLogId", lead.WebLogId);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", lead.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?Company", lead.Company);
      
        Database.PutParameter(dbCommand,"?FirstName", lead.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", lead.LastName);
      
        Database.PutParameter(dbCommand,"?Address1", lead.Address1);
      
        Database.PutParameter(dbCommand,"?Address2", lead.Address2);
      
        Database.PutParameter(dbCommand,"?City", lead.City);
      
        Database.PutParameter(dbCommand,"?State", lead.State);
      
        Database.PutParameter(dbCommand,"?Zip", lead.Zip);
      
        Database.PutParameter(dbCommand,"?Phone1", lead.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", lead.Phone2);
      
        Database.PutParameter(dbCommand,"?Email", lead.Email);
      
        Database.PutParameter(dbCommand,"?CustomerNotes", lead.CustomerNotes);
      
        Database.PutParameter(dbCommand,"?DispatchNotes", lead.DispatchNotes);
      
        Database.PutParameter(dbCommand,"?PreferredServiceDate", lead.PreferredServiceDate);
      
        Database.PutParameter(dbCommand,"?PreferredTime", lead.PreferredTime);
      
        Database.PutParameter(dbCommand,"?DateCreated", lead.DateCreated);
      
        Database.PutParameter(dbCommand,"?DateCancelled", lead.DateCancelled);
      
        Database.PutParameter(dbCommand,"?KeywordId", lead.KeywordId);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceAcronym", lead.AdvertisingSourceAcronym);
      
        Database.PutParameter(dbCommand,"?ServmanAdvertisingSource", lead.ServmanAdvertisingSource);
      
        Database.PutParameter(dbCommand,"?ServmanTrackCode", lead.ServmanTrackCode);
      
        Database.PutParameter(dbCommand,"?DateLateNotificationSent", lead.DateLateNotificationSent);
      
        Database.PutParameter(dbCommand,"?DateFirstSetPending", lead.DateFirstSetPending);
      
        Database.PutParameter(dbCommand,"?DateLastSetPending", lead.DateLastSetPending);
      
        Database.PutParameter(dbCommand,"?FirstUpdateEmployeeId", lead.FirstUpdateEmployeeId);
      
        Database.PutParameter(dbCommand,"?LastUpdateEmployeeId", lead.LastUpdateEmployeeId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        lead.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Lead lead)
      {
        Insert(lead, null);
      }


      public static void Insert(List<Lead>  leadList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Lead lead in  leadList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?LeadStatusId", lead.LeadStatusId);
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", lead.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?EmployeeId", lead.EmployeeId);
      
        Database.PutParameter(dbCommand,"?CustomerWebAccountId", lead.CustomerWebAccountId);
      
        Database.PutParameter(dbCommand,"?WebLogId", lead.WebLogId);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", lead.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?Company", lead.Company);
      
        Database.PutParameter(dbCommand,"?FirstName", lead.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", lead.LastName);
      
        Database.PutParameter(dbCommand,"?Address1", lead.Address1);
      
        Database.PutParameter(dbCommand,"?Address2", lead.Address2);
      
        Database.PutParameter(dbCommand,"?City", lead.City);
      
        Database.PutParameter(dbCommand,"?State", lead.State);
      
        Database.PutParameter(dbCommand,"?Zip", lead.Zip);
      
        Database.PutParameter(dbCommand,"?Phone1", lead.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", lead.Phone2);
      
        Database.PutParameter(dbCommand,"?Email", lead.Email);
      
        Database.PutParameter(dbCommand,"?CustomerNotes", lead.CustomerNotes);
      
        Database.PutParameter(dbCommand,"?DispatchNotes", lead.DispatchNotes);
      
        Database.PutParameter(dbCommand,"?PreferredServiceDate", lead.PreferredServiceDate);
      
        Database.PutParameter(dbCommand,"?PreferredTime", lead.PreferredTime);
      
        Database.PutParameter(dbCommand,"?DateCreated", lead.DateCreated);
      
        Database.PutParameter(dbCommand,"?DateCancelled", lead.DateCancelled);
      
        Database.PutParameter(dbCommand,"?KeywordId", lead.KeywordId);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceAcronym", lead.AdvertisingSourceAcronym);
      
        Database.PutParameter(dbCommand,"?ServmanAdvertisingSource", lead.ServmanAdvertisingSource);
      
        Database.PutParameter(dbCommand,"?ServmanTrackCode", lead.ServmanTrackCode);
      
        Database.PutParameter(dbCommand,"?DateLateNotificationSent", lead.DateLateNotificationSent);
      
        Database.PutParameter(dbCommand,"?DateFirstSetPending", lead.DateFirstSetPending);
      
        Database.PutParameter(dbCommand,"?DateLastSetPending", lead.DateLastSetPending);
      
        Database.PutParameter(dbCommand,"?FirstUpdateEmployeeId", lead.FirstUpdateEmployeeId);
      
        Database.PutParameter(dbCommand,"?LastUpdateEmployeeId", lead.LastUpdateEmployeeId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?LeadStatusId",lead.LeadStatusId);
      
        Database.UpdateParameter(dbCommand,"?ProjectTypeId",lead.ProjectTypeId);
      
        Database.UpdateParameter(dbCommand,"?EmployeeId",lead.EmployeeId);
      
        Database.UpdateParameter(dbCommand,"?CustomerWebAccountId",lead.CustomerWebAccountId);
      
        Database.UpdateParameter(dbCommand,"?WebLogId",lead.WebLogId);
      
        Database.UpdateParameter(dbCommand,"?BusinessPartnerId",lead.BusinessPartnerId);
      
        Database.UpdateParameter(dbCommand,"?Company",lead.Company);
      
        Database.UpdateParameter(dbCommand,"?FirstName",lead.FirstName);
      
        Database.UpdateParameter(dbCommand,"?LastName",lead.LastName);
      
        Database.UpdateParameter(dbCommand,"?Address1",lead.Address1);
      
        Database.UpdateParameter(dbCommand,"?Address2",lead.Address2);
      
        Database.UpdateParameter(dbCommand,"?City",lead.City);
      
        Database.UpdateParameter(dbCommand,"?State",lead.State);
      
        Database.UpdateParameter(dbCommand,"?Zip",lead.Zip);
      
        Database.UpdateParameter(dbCommand,"?Phone1",lead.Phone1);
      
        Database.UpdateParameter(dbCommand,"?Phone2",lead.Phone2);
      
        Database.UpdateParameter(dbCommand,"?Email",lead.Email);
      
        Database.UpdateParameter(dbCommand,"?CustomerNotes",lead.CustomerNotes);
      
        Database.UpdateParameter(dbCommand,"?DispatchNotes",lead.DispatchNotes);
      
        Database.UpdateParameter(dbCommand,"?PreferredServiceDate",lead.PreferredServiceDate);
      
        Database.UpdateParameter(dbCommand,"?PreferredTime",lead.PreferredTime);
      
        Database.UpdateParameter(dbCommand,"?DateCreated",lead.DateCreated);
      
        Database.UpdateParameter(dbCommand,"?DateCancelled",lead.DateCancelled);
      
        Database.UpdateParameter(dbCommand,"?KeywordId",lead.KeywordId);
      
        Database.UpdateParameter(dbCommand,"?AdvertisingSourceAcronym",lead.AdvertisingSourceAcronym);
      
        Database.UpdateParameter(dbCommand,"?ServmanAdvertisingSource",lead.ServmanAdvertisingSource);
      
        Database.UpdateParameter(dbCommand,"?ServmanTrackCode",lead.ServmanTrackCode);
      
        Database.UpdateParameter(dbCommand,"?DateLateNotificationSent",lead.DateLateNotificationSent);
      
        Database.UpdateParameter(dbCommand,"?DateFirstSetPending",lead.DateFirstSetPending);
      
        Database.UpdateParameter(dbCommand,"?DateLastSetPending",lead.DateLastSetPending);
      
        Database.UpdateParameter(dbCommand,"?FirstUpdateEmployeeId",lead.FirstUpdateEmployeeId);
      
        Database.UpdateParameter(dbCommand,"?LastUpdateEmployeeId",lead.LastUpdateEmployeeId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        lead.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Lead>  leadList)
      {
        Insert(leadList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Lead Set "
      
        + " LeadStatusId = ?LeadStatusId, "
      
        + " ProjectTypeId = ?ProjectTypeId, "
      
        + " EmployeeId = ?EmployeeId, "
      
        + " CustomerWebAccountId = ?CustomerWebAccountId, "
      
        + " WebLogId = ?WebLogId, "
      
        + " BusinessPartnerId = ?BusinessPartnerId, "
      
        + " Company = ?Company, "
      
        + " FirstName = ?FirstName, "
      
        + " LastName = ?LastName, "
      
        + " Address1 = ?Address1, "
      
        + " Address2 = ?Address2, "
      
        + " City = ?City, "
      
        + " State = ?State, "
      
        + " Zip = ?Zip, "
      
        + " Phone1 = ?Phone1, "
      
        + " Phone2 = ?Phone2, "
      
        + " Email = ?Email, "
      
        + " CustomerNotes = ?CustomerNotes, "
      
        + " DispatchNotes = ?DispatchNotes, "
      
        + " PreferredServiceDate = ?PreferredServiceDate, "
      
        + " PreferredTime = ?PreferredTime, "
      
        + " DateCreated = ?DateCreated, "
      
        + " DateCancelled = ?DateCancelled, "
      
        + " KeywordId = ?KeywordId, "
      
        + " AdvertisingSourceAcronym = ?AdvertisingSourceAcronym, "
      
        + " ServmanAdvertisingSource = ?ServmanAdvertisingSource, "
      
        + " ServmanTrackCode = ?ServmanTrackCode, "
      
        + " DateLateNotificationSent = ?DateLateNotificationSent, "
      
        + " DateFirstSetPending = ?DateFirstSetPending, "
      
        + " DateLastSetPending = ?DateLastSetPending, "
      
        + " FirstUpdateEmployeeId = ?FirstUpdateEmployeeId, "
      
        + " LastUpdateEmployeeId = ?LastUpdateEmployeeId "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Lead lead, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", lead.ID);
      
        Database.PutParameter(dbCommand,"?LeadStatusId", lead.LeadStatusId);
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", lead.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?EmployeeId", lead.EmployeeId);
      
        Database.PutParameter(dbCommand,"?CustomerWebAccountId", lead.CustomerWebAccountId);
      
        Database.PutParameter(dbCommand,"?WebLogId", lead.WebLogId);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", lead.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?Company", lead.Company);
      
        Database.PutParameter(dbCommand,"?FirstName", lead.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", lead.LastName);
      
        Database.PutParameter(dbCommand,"?Address1", lead.Address1);
      
        Database.PutParameter(dbCommand,"?Address2", lead.Address2);
      
        Database.PutParameter(dbCommand,"?City", lead.City);
      
        Database.PutParameter(dbCommand,"?State", lead.State);
      
        Database.PutParameter(dbCommand,"?Zip", lead.Zip);
      
        Database.PutParameter(dbCommand,"?Phone1", lead.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", lead.Phone2);
      
        Database.PutParameter(dbCommand,"?Email", lead.Email);
      
        Database.PutParameter(dbCommand,"?CustomerNotes", lead.CustomerNotes);
      
        Database.PutParameter(dbCommand,"?DispatchNotes", lead.DispatchNotes);
      
        Database.PutParameter(dbCommand,"?PreferredServiceDate", lead.PreferredServiceDate);
      
        Database.PutParameter(dbCommand,"?PreferredTime", lead.PreferredTime);
      
        Database.PutParameter(dbCommand,"?DateCreated", lead.DateCreated);
      
        Database.PutParameter(dbCommand,"?DateCancelled", lead.DateCancelled);
      
        Database.PutParameter(dbCommand,"?KeywordId", lead.KeywordId);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceAcronym", lead.AdvertisingSourceAcronym);
      
        Database.PutParameter(dbCommand,"?ServmanAdvertisingSource", lead.ServmanAdvertisingSource);
      
        Database.PutParameter(dbCommand,"?ServmanTrackCode", lead.ServmanTrackCode);
      
        Database.PutParameter(dbCommand,"?DateLateNotificationSent", lead.DateLateNotificationSent);
      
        Database.PutParameter(dbCommand,"?DateFirstSetPending", lead.DateFirstSetPending);
      
        Database.PutParameter(dbCommand,"?DateLastSetPending", lead.DateLastSetPending);
      
        Database.PutParameter(dbCommand,"?FirstUpdateEmployeeId", lead.FirstUpdateEmployeeId);
      
        Database.PutParameter(dbCommand,"?LastUpdateEmployeeId", lead.LastUpdateEmployeeId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Lead lead)
      {
        Update(lead, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " LeadStatusId, "
      
        + " ProjectTypeId, "
      
        + " EmployeeId, "
      
        + " CustomerWebAccountId, "
      
        + " WebLogId, "
      
        + " BusinessPartnerId, "
      
        + " Company, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Address1, "
      
        + " Address2, "
      
        + " City, "
      
        + " State, "
      
        + " Zip, "
      
        + " Phone1, "
      
        + " Phone2, "
      
        + " Email, "
      
        + " CustomerNotes, "
      
        + " DispatchNotes, "
      
        + " PreferredServiceDate, "
      
        + " PreferredTime, "
      
        + " DateCreated, "
      
        + " DateCancelled, "
      
        + " KeywordId, "
      
        + " AdvertisingSourceAcronym, "
      
        + " ServmanAdvertisingSource, "
      
        + " ServmanTrackCode, "
      
        + " DateLateNotificationSent, "
      
        + " DateFirstSetPending, "
      
        + " DateLastSetPending, "
      
        + " FirstUpdateEmployeeId, "
      
        + " LastUpdateEmployeeId "
      

      + " From Lead "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Lead FindByPrimaryKey(
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
      throw new DataNotFoundException("Lead not found, search by primary key");

      }

      public static Lead FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Lead lead, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",lead.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Lead lead)
      {
      return Exists(lead, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Lead limit 1";

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

      public static Lead Load(IDataReader dataReader, int offset)
      {
      Lead lead = new Lead();

      lead.ID = dataReader.GetInt32(0 + offset);
          lead.LeadStatusId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            lead.ProjectTypeId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            lead.EmployeeId = dataReader.GetInt32(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            lead.CustomerWebAccountId = dataReader.GetInt32(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            lead.WebLogId = dataReader.GetInt32(5 + offset);
          lead.BusinessPartnerId = dataReader.GetInt32(6 + offset);
          lead.Company = dataReader.GetString(7 + offset);
          lead.FirstName = dataReader.GetString(8 + offset);
          lead.LastName = dataReader.GetString(9 + offset);
          lead.Address1 = dataReader.GetString(10 + offset);
          lead.Address2 = dataReader.GetString(11 + offset);
          lead.City = dataReader.GetString(12 + offset);
          lead.State = dataReader.GetString(13 + offset);
          
            if(!dataReader.IsDBNull(14 + offset))
            lead.Zip = dataReader.GetInt32(14 + offset);
          lead.Phone1 = dataReader.GetString(15 + offset);
          lead.Phone2 = dataReader.GetString(16 + offset);
          lead.Email = dataReader.GetString(17 + offset);
          lead.CustomerNotes = dataReader.GetString(18 + offset);
          lead.DispatchNotes = dataReader.GetString(19 + offset);
          
            if(!dataReader.IsDBNull(20 + offset))
            lead.PreferredServiceDate = dataReader.GetDateTime(20 + offset);
          lead.PreferredTime = dataReader.GetString(21 + offset);
          lead.DateCreated = dataReader.GetDateTime(22 + offset);
          
            if(!dataReader.IsDBNull(23 + offset))
            lead.DateCancelled = dataReader.GetDateTime(23 + offset);
          
            if(!dataReader.IsDBNull(24 + offset))
            lead.KeywordId = dataReader.GetInt32(24 + offset);
          
            if(!dataReader.IsDBNull(25 + offset))
            lead.AdvertisingSourceAcronym = dataReader.GetString(25 + offset);
          
            if(!dataReader.IsDBNull(26 + offset))
            lead.ServmanAdvertisingSource = dataReader.GetString(26 + offset);
          
            if(!dataReader.IsDBNull(27 + offset))
            lead.ServmanTrackCode = dataReader.GetString(27 + offset);
          
            if(!dataReader.IsDBNull(28 + offset))
            lead.DateLateNotificationSent = dataReader.GetDateTime(28 + offset);
          
            if(!dataReader.IsDBNull(29 + offset))
            lead.DateFirstSetPending = dataReader.GetDateTime(29 + offset);
          
            if(!dataReader.IsDBNull(30 + offset))
            lead.DateLastSetPending = dataReader.GetDateTime(30 + offset);
          
            if(!dataReader.IsDBNull(31 + offset))
            lead.FirstUpdateEmployeeId = dataReader.GetInt32(31 + offset);
          
            if(!dataReader.IsDBNull(32 + offset))
            lead.LastUpdateEmployeeId = dataReader.GetInt32(32 + offset);
          

      return lead;
      }

      public static Lead Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Lead "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Lead lead, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", lead.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Lead lead)
      {
        Delete(lead, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Lead ";

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
      
        + " LeadStatusId, "
      
        + " ProjectTypeId, "
      
        + " EmployeeId, "
      
        + " CustomerWebAccountId, "
      
        + " WebLogId, "
      
        + " BusinessPartnerId, "
      
        + " Company, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Address1, "
      
        + " Address2, "
      
        + " City, "
      
        + " State, "
      
        + " Zip, "
      
        + " Phone1, "
      
        + " Phone2, "
      
        + " Email, "
      
        + " CustomerNotes, "
      
        + " DispatchNotes, "
      
        + " PreferredServiceDate, "
      
        + " PreferredTime, "
      
        + " DateCreated, "
      
        + " DateCancelled, "
      
        + " KeywordId, "
      
        + " AdvertisingSourceAcronym, "
      
        + " ServmanAdvertisingSource, "
      
        + " ServmanTrackCode, "
      
        + " DateLateNotificationSent, "
      
        + " DateFirstSetPending, "
      
        + " DateLastSetPending, "
      
        + " FirstUpdateEmployeeId, "
      
        + " LastUpdateEmployeeId "
      

      + " From Lead ";
      public static List<Lead> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Lead> rv = new List<Lead>();

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

      public static List<Lead> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Lead> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Lead obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && LeadStatusId == obj.LeadStatusId && ProjectTypeId == obj.ProjectTypeId && EmployeeId == obj.EmployeeId && CustomerWebAccountId == obj.CustomerWebAccountId && WebLogId == obj.WebLogId && BusinessPartnerId == obj.BusinessPartnerId && Company == obj.Company && FirstName == obj.FirstName && LastName == obj.LastName && Address1 == obj.Address1 && Address2 == obj.Address2 && City == obj.City && State == obj.State && Zip == obj.Zip && Phone1 == obj.Phone1 && Phone2 == obj.Phone2 && Email == obj.Email && CustomerNotes == obj.CustomerNotes && DispatchNotes == obj.DispatchNotes && PreferredServiceDate == obj.PreferredServiceDate && PreferredTime == obj.PreferredTime && DateCreated == obj.DateCreated && DateCancelled == obj.DateCancelled && KeywordId == obj.KeywordId && AdvertisingSourceAcronym == obj.AdvertisingSourceAcronym && ServmanAdvertisingSource == obj.ServmanAdvertisingSource && ServmanTrackCode == obj.ServmanTrackCode && DateLateNotificationSent == obj.DateLateNotificationSent && DateFirstSetPending == obj.DateFirstSetPending && DateLastSetPending == obj.DateLastSetPending && FirstUpdateEmployeeId == obj.FirstUpdateEmployeeId && LastUpdateEmployeeId == obj.LastUpdateEmployeeId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Lead> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Lead));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Lead item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Lead>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Lead));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Lead> itemsList
      = new List<Lead>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Lead)
      itemsList.Add(deserializedObject as Lead);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_leadStatusId;
      
        protected int? m_projectTypeId;
      
        protected int? m_employeeId;
      
        protected int? m_customerWebAccountId;
      
        protected int? m_webLogId;
      
        protected int m_businessPartnerId;
      
        protected String m_company;
      
        protected String m_firstName;
      
        protected String m_lastName;
      
        protected String m_address1;
      
        protected String m_address2;
      
        protected String m_city;
      
        protected String m_state;
      
        protected int? m_zip;
      
        protected String m_phone1;
      
        protected String m_phone2;
      
        protected String m_email;
      
        protected String m_customerNotes;
      
        protected String m_dispatchNotes;
      
        protected DateTime? m_preferredServiceDate;
      
        protected String m_preferredTime;
      
        protected DateTime m_dateCreated;
      
        protected DateTime? m_dateCancelled;
      
        protected int? m_keywordId;
      
        protected String m_advertisingSourceAcronym;
      
        protected String m_servmanAdvertisingSource;
      
        protected String m_servmanTrackCode;
      
        protected DateTime? m_dateLateNotificationSent;
      
        protected DateTime? m_dateFirstSetPending;
      
        protected DateTime? m_dateLastSetPending;
      
        protected int? m_firstUpdateEmployeeId;
      
        protected int? m_lastUpdateEmployeeId;
      
      #endregion

      #region Constructors
      public Lead(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Lead(
        int 
          iD,int 
          leadStatusId,int? 
          projectTypeId,int? 
          employeeId,int? 
          customerWebAccountId,int? 
          webLogId,int 
          businessPartnerId,String 
          company,String 
          firstName,String 
          lastName,String 
          address1,String 
          address2,String 
          city,String 
          state,int? 
          zip,String 
          phone1,String 
          phone2,String 
          email,String 
          customerNotes,String 
          dispatchNotes,DateTime? 
          preferredServiceDate,String 
          preferredTime,DateTime 
          dateCreated,DateTime? 
          dateCancelled,int? 
          keywordId,String 
          advertisingSourceAcronym,String 
          servmanAdvertisingSource,String 
          servmanTrackCode,DateTime? 
          dateLateNotificationSent,DateTime? 
          dateFirstSetPending,DateTime? 
          dateLastSetPending,int? 
          firstUpdateEmployeeId,int? 
          lastUpdateEmployeeId
        ) : this()
        {
        
          m_iD = iD;
        
          m_leadStatusId = leadStatusId;
        
          m_projectTypeId = projectTypeId;
        
          m_employeeId = employeeId;
        
          m_customerWebAccountId = customerWebAccountId;
        
          m_webLogId = webLogId;
        
          m_businessPartnerId = businessPartnerId;
        
          m_company = company;
        
          m_firstName = firstName;
        
          m_lastName = lastName;
        
          m_address1 = address1;
        
          m_address2 = address2;
        
          m_city = city;
        
          m_state = state;
        
          m_zip = zip;
        
          m_phone1 = phone1;
        
          m_phone2 = phone2;
        
          m_email = email;
        
          m_customerNotes = customerNotes;
        
          m_dispatchNotes = dispatchNotes;
        
          m_preferredServiceDate = preferredServiceDate;
        
          m_preferredTime = preferredTime;
        
          m_dateCreated = dateCreated;
        
          m_dateCancelled = dateCancelled;
        
          m_keywordId = keywordId;
        
          m_advertisingSourceAcronym = advertisingSourceAcronym;
        
          m_servmanAdvertisingSource = servmanAdvertisingSource;
        
          m_servmanTrackCode = servmanTrackCode;
        
          m_dateLateNotificationSent = dateLateNotificationSent;
        
          m_dateFirstSetPending = dateFirstSetPending;
        
          m_dateLastSetPending = dateLastSetPending;
        
          m_firstUpdateEmployeeId = firstUpdateEmployeeId;
        
          m_lastUpdateEmployeeId = lastUpdateEmployeeId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int LeadStatusId
        {
        get { return m_leadStatusId;}
        set { m_leadStatusId = value; }
        }
      
        [XmlElement]
        public int? ProjectTypeId
        {
        get { return m_projectTypeId;}
        set { m_projectTypeId = value; }
        }
      
        [XmlElement]
        public int? EmployeeId
        {
        get { return m_employeeId;}
        set { m_employeeId = value; }
        }
      
        [XmlElement]
        public int? CustomerWebAccountId
        {
        get { return m_customerWebAccountId;}
        set { m_customerWebAccountId = value; }
        }
      
        [XmlElement]
        public int? WebLogId
        {
        get { return m_webLogId;}
        set { m_webLogId = value; }
        }
      
        [XmlElement]
        public int BusinessPartnerId
        {
        get { return m_businessPartnerId;}
        set { m_businessPartnerId = value; }
        }
      
        [XmlElement]
        public String Company
        {
        get { return m_company;}
        set { m_company = value; }
        }
      
        [XmlElement]
        public String FirstName
        {
        get { return m_firstName;}
        set { m_firstName = value; }
        }
      
        [XmlElement]
        public String LastName
        {
        get { return m_lastName;}
        set { m_lastName = value; }
        }
      
        [XmlElement]
        public String Address1
        {
        get { return m_address1;}
        set { m_address1 = value; }
        }
      
        [XmlElement]
        public String Address2
        {
        get { return m_address2;}
        set { m_address2 = value; }
        }
      
        [XmlElement]
        public String City
        {
        get { return m_city;}
        set { m_city = value; }
        }
      
        [XmlElement]
        public String State
        {
        get { return m_state;}
        set { m_state = value; }
        }
      
        [XmlElement]
        public int? Zip
        {
        get { return m_zip;}
        set { m_zip = value; }
        }
      
        [XmlElement]
        public String Phone1
        {
        get { return m_phone1;}
        set { m_phone1 = value; }
        }
      
        [XmlElement]
        public String Phone2
        {
        get { return m_phone2;}
        set { m_phone2 = value; }
        }
      
        [XmlElement]
        public String Email
        {
        get { return m_email;}
        set { m_email = value; }
        }
      
        [XmlElement]
        public String CustomerNotes
        {
        get { return m_customerNotes;}
        set { m_customerNotes = value; }
        }
      
        [XmlElement]
        public String DispatchNotes
        {
        get { return m_dispatchNotes;}
        set { m_dispatchNotes = value; }
        }
      
        [XmlElement]
        public DateTime? PreferredServiceDate
        {
        get { return m_preferredServiceDate;}
        set { m_preferredServiceDate = value; }
        }
      
        [XmlElement]
        public String PreferredTime
        {
        get { return m_preferredTime;}
        set { m_preferredTime = value; }
        }
      
        [XmlElement]
        public DateTime DateCreated
        {
        get { return m_dateCreated;}
        set { m_dateCreated = value; }
        }
      
        [XmlElement]
        public DateTime? DateCancelled
        {
        get { return m_dateCancelled;}
        set { m_dateCancelled = value; }
        }
      
        [XmlElement]
        public int? KeywordId
        {
        get { return m_keywordId;}
        set { m_keywordId = value; }
        }
      
        [XmlElement]
        public String AdvertisingSourceAcronym
        {
        get { return m_advertisingSourceAcronym;}
        set { m_advertisingSourceAcronym = value; }
        }
      
        [XmlElement]
        public String ServmanAdvertisingSource
        {
        get { return m_servmanAdvertisingSource;}
        set { m_servmanAdvertisingSource = value; }
        }
      
        [XmlElement]
        public String ServmanTrackCode
        {
        get { return m_servmanTrackCode;}
        set { m_servmanTrackCode = value; }
        }
      
        [XmlElement]
        public DateTime? DateLateNotificationSent
        {
        get { return m_dateLateNotificationSent;}
        set { m_dateLateNotificationSent = value; }
        }
      
        [XmlElement]
        public DateTime? DateFirstSetPending
        {
        get { return m_dateFirstSetPending;}
        set { m_dateFirstSetPending = value; }
        }
      
        [XmlElement]
        public DateTime? DateLastSetPending
        {
        get { return m_dateLastSetPending;}
        set { m_dateLastSetPending = value; }
        }
      
        [XmlElement]
        public int? FirstUpdateEmployeeId
        {
        get { return m_firstUpdateEmployeeId;}
        set { m_firstUpdateEmployeeId = value; }
        }
      
        [XmlElement]
        public int? LastUpdateEmployeeId
        {
        get { return m_lastUpdateEmployeeId;}
        set { m_lastUpdateEmployeeId = value; }
        }
      

      public static int FieldsCount
      {
      get { return 33; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    