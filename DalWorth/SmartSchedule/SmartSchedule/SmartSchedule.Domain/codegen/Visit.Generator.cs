
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using SmartSchedule.Data;
    using SmartSchedule.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace SmartSchedule.Domain
      {

      [DataContract]
      public partial class Visit : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Visit ( " +
      
        " TechnicianId, " +
      
        " TimeFrameId, " +
      
        " TimeStart, " +
      
        " TimeEnd, " +
      
        " Latitude, " +
      
        " Longitude, " +
      
        " Zip, " +
      
        " Cost, " +
      
        " ExclusiveCompanyId, " +
      
        " ExclusiveTechnicianDefaultId, " +
      
        " IsTempIgnoreExclusivity, " +
      
        " TempExclusiveTechnicianId, " +
      
        " ForbiddenTechnicianDefaultId, " +
      
        " TicketNumber, " +
      
        " CustomerName, " +
      
        " Street, " +
      
        " Address2, " +
      
        " City, " +
      
        " State, " +
      
        " HomePhone, " +
      
        " BusinessPhone, " +
      
        " IsEstimate, " +
      
        " IsEstimateAndDo, " +
      
        " IsRework, " +
      
        " CallDateTime, " +
      
        " Mapsco, " +
      
        " IsCalledCustomer, " +
      
        " IsFixed, " +
      
        " Area, " +
      
        " ServType, " +
      
        " AdsourceAcronym, " +
      
        " CustomerRank, " +
      
        " OriginatedTechnicianDefaultId, " +
      
        " OriginatedCompleteDate, " +
      
        " OriginatedTicketNumber, " +
      
        " CustomerExclusiveTechnicianDefaultId, " +
      
        " Note, " +
      
        " ExpCred, " +
      
        " SpecName, " +
      
        " ServmanBaseTimeFrameId, " +
      
        " SdPercent, " +
      
        " TaxPercent, " +
      
        " DurationCost, " +
      
        " SnapshotDate, " +
      
        " IsBlockout, " +
      
        " TechnicianEmailSentDate, " +
      
        " IsSecondaryEmailSent " +
      
      ") Values (" +
      
        " ?TechnicianId, " +
      
        " ?TimeFrameId, " +
      
        " ?TimeStart, " +
      
        " ?TimeEnd, " +
      
        " ?Latitude, " +
      
        " ?Longitude, " +
      
        " ?Zip, " +
      
        " ?Cost, " +
      
        " ?ExclusiveCompanyId, " +
      
        " ?ExclusiveTechnicianDefaultId, " +
      
        " ?IsTempIgnoreExclusivity, " +
      
        " ?TempExclusiveTechnicianId, " +
      
        " ?ForbiddenTechnicianDefaultId, " +
      
        " ?TicketNumber, " +
      
        " ?CustomerName, " +
      
        " ?Street, " +
      
        " ?Address2, " +
      
        " ?City, " +
      
        " ?State, " +
      
        " ?HomePhone, " +
      
        " ?BusinessPhone, " +
      
        " ?IsEstimate, " +
      
        " ?IsEstimateAndDo, " +
      
        " ?IsRework, " +
      
        " ?CallDateTime, " +
      
        " ?Mapsco, " +
      
        " ?IsCalledCustomer, " +
      
        " ?IsFixed, " +
      
        " ?Area, " +
      
        " ?ServType, " +
      
        " ?AdsourceAcronym, " +
      
        " ?CustomerRank, " +
      
        " ?OriginatedTechnicianDefaultId, " +
      
        " ?OriginatedCompleteDate, " +
      
        " ?OriginatedTicketNumber, " +
      
        " ?CustomerExclusiveTechnicianDefaultId, " +
      
        " ?Note, " +
      
        " ?ExpCred, " +
      
        " ?SpecName, " +
      
        " ?ServmanBaseTimeFrameId, " +
      
        " ?SdPercent, " +
      
        " ?TaxPercent, " +
      
        " ?DurationCost, " +
      
        " ?SnapshotDate, " +
      
        " ?IsBlockout, " +
      
        " ?TechnicianEmailSentDate, " +
      
        " ?IsSecondaryEmailSent " +
      
      ")";

      public static void Insert(Visit visit, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", visit.TechnicianId);
      
        Database.PutParameter(dbCommand,"?TimeFrameId", visit.TimeFrameId);
      
        Database.PutParameter(dbCommand,"?TimeStart", visit.TimeStart);
      
        Database.PutParameter(dbCommand,"?TimeEnd", visit.TimeEnd);
      
        Database.PutParameter(dbCommand,"?Latitude", visit.Latitude);
      
        Database.PutParameter(dbCommand,"?Longitude", visit.Longitude);
      
        Database.PutParameter(dbCommand,"?Zip", visit.Zip);
      
        Database.PutParameter(dbCommand,"?Cost", visit.Cost);
      
        Database.PutParameter(dbCommand,"?ExclusiveCompanyId", visit.ExclusiveCompanyId);
      
        Database.PutParameter(dbCommand,"?ExclusiveTechnicianDefaultId", visit.ExclusiveTechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?IsTempIgnoreExclusivity", visit.IsTempIgnoreExclusivity);
      
        Database.PutParameter(dbCommand,"?TempExclusiveTechnicianId", visit.TempExclusiveTechnicianId);
      
        Database.PutParameter(dbCommand,"?ForbiddenTechnicianDefaultId", visit.ForbiddenTechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?TicketNumber", visit.TicketNumber);
      
        Database.PutParameter(dbCommand,"?CustomerName", visit.CustomerName);
      
        Database.PutParameter(dbCommand,"?Street", visit.Street);
      
        Database.PutParameter(dbCommand,"?Address2", visit.Address2);
      
        Database.PutParameter(dbCommand,"?City", visit.City);
      
        Database.PutParameter(dbCommand,"?State", visit.State);
      
        Database.PutParameter(dbCommand,"?HomePhone", visit.HomePhone);
      
        Database.PutParameter(dbCommand,"?BusinessPhone", visit.BusinessPhone);
      
        Database.PutParameter(dbCommand,"?IsEstimate", visit.IsEstimate);
      
        Database.PutParameter(dbCommand,"?IsEstimateAndDo", visit.IsEstimateAndDo);
      
        Database.PutParameter(dbCommand,"?IsRework", visit.IsRework);
      
        Database.PutParameter(dbCommand,"?CallDateTime", visit.CallDateTime);
      
        Database.PutParameter(dbCommand,"?Mapsco", visit.Mapsco);
      
        Database.PutParameter(dbCommand,"?IsCalledCustomer", visit.IsCalledCustomer);
      
        Database.PutParameter(dbCommand,"?IsFixed", visit.IsFixed);
      
        Database.PutParameter(dbCommand,"?Area", visit.Area);
      
        Database.PutParameter(dbCommand,"?ServType", visit.ServType);
      
        Database.PutParameter(dbCommand,"?AdsourceAcronym", visit.AdsourceAcronym);
      
        Database.PutParameter(dbCommand,"?CustomerRank", visit.CustomerRank);
      
        Database.PutParameter(dbCommand,"?OriginatedTechnicianDefaultId", visit.OriginatedTechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?OriginatedCompleteDate", visit.OriginatedCompleteDate);
      
        Database.PutParameter(dbCommand,"?OriginatedTicketNumber", visit.OriginatedTicketNumber);
      
        Database.PutParameter(dbCommand,"?CustomerExclusiveTechnicianDefaultId", visit.CustomerExclusiveTechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?Note", visit.Note);
      
        Database.PutParameter(dbCommand,"?ExpCred", visit.ExpCred);
      
        Database.PutParameter(dbCommand,"?SpecName", visit.SpecName);
      
        Database.PutParameter(dbCommand,"?ServmanBaseTimeFrameId", visit.ServmanBaseTimeFrameId);
      
        Database.PutParameter(dbCommand,"?SdPercent", visit.SdPercent);
      
        Database.PutParameter(dbCommand,"?TaxPercent", visit.TaxPercent);
      
        Database.PutParameter(dbCommand,"?DurationCost", visit.DurationCost);
      
        Database.PutParameter(dbCommand,"?SnapshotDate", visit.SnapshotDate);
      
        Database.PutParameter(dbCommand,"?IsBlockout", visit.IsBlockout);
      
        Database.PutParameter(dbCommand,"?TechnicianEmailSentDate", visit.TechnicianEmailSentDate);
      
        Database.PutParameter(dbCommand,"?IsSecondaryEmailSent", visit.IsSecondaryEmailSent);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        visit.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Visit visit)
      {
        Insert(visit, null);
      }


      public static void Insert(List<Visit>  visitList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Visit visit in  visitList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", visit.TechnicianId);
      
        Database.PutParameter(dbCommand,"?TimeFrameId", visit.TimeFrameId);
      
        Database.PutParameter(dbCommand,"?TimeStart", visit.TimeStart);
      
        Database.PutParameter(dbCommand,"?TimeEnd", visit.TimeEnd);
      
        Database.PutParameter(dbCommand,"?Latitude", visit.Latitude);
      
        Database.PutParameter(dbCommand,"?Longitude", visit.Longitude);
      
        Database.PutParameter(dbCommand,"?Zip", visit.Zip);
      
        Database.PutParameter(dbCommand,"?Cost", visit.Cost);
      
        Database.PutParameter(dbCommand,"?ExclusiveCompanyId", visit.ExclusiveCompanyId);
      
        Database.PutParameter(dbCommand,"?ExclusiveTechnicianDefaultId", visit.ExclusiveTechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?IsTempIgnoreExclusivity", visit.IsTempIgnoreExclusivity);
      
        Database.PutParameter(dbCommand,"?TempExclusiveTechnicianId", visit.TempExclusiveTechnicianId);
      
        Database.PutParameter(dbCommand,"?ForbiddenTechnicianDefaultId", visit.ForbiddenTechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?TicketNumber", visit.TicketNumber);
      
        Database.PutParameter(dbCommand,"?CustomerName", visit.CustomerName);
      
        Database.PutParameter(dbCommand,"?Street", visit.Street);
      
        Database.PutParameter(dbCommand,"?Address2", visit.Address2);
      
        Database.PutParameter(dbCommand,"?City", visit.City);
      
        Database.PutParameter(dbCommand,"?State", visit.State);
      
        Database.PutParameter(dbCommand,"?HomePhone", visit.HomePhone);
      
        Database.PutParameter(dbCommand,"?BusinessPhone", visit.BusinessPhone);
      
        Database.PutParameter(dbCommand,"?IsEstimate", visit.IsEstimate);
      
        Database.PutParameter(dbCommand,"?IsEstimateAndDo", visit.IsEstimateAndDo);
      
        Database.PutParameter(dbCommand,"?IsRework", visit.IsRework);
      
        Database.PutParameter(dbCommand,"?CallDateTime", visit.CallDateTime);
      
        Database.PutParameter(dbCommand,"?Mapsco", visit.Mapsco);
      
        Database.PutParameter(dbCommand,"?IsCalledCustomer", visit.IsCalledCustomer);
      
        Database.PutParameter(dbCommand,"?IsFixed", visit.IsFixed);
      
        Database.PutParameter(dbCommand,"?Area", visit.Area);
      
        Database.PutParameter(dbCommand,"?ServType", visit.ServType);
      
        Database.PutParameter(dbCommand,"?AdsourceAcronym", visit.AdsourceAcronym);
      
        Database.PutParameter(dbCommand,"?CustomerRank", visit.CustomerRank);
      
        Database.PutParameter(dbCommand,"?OriginatedTechnicianDefaultId", visit.OriginatedTechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?OriginatedCompleteDate", visit.OriginatedCompleteDate);
      
        Database.PutParameter(dbCommand,"?OriginatedTicketNumber", visit.OriginatedTicketNumber);
      
        Database.PutParameter(dbCommand,"?CustomerExclusiveTechnicianDefaultId", visit.CustomerExclusiveTechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?Note", visit.Note);
      
        Database.PutParameter(dbCommand,"?ExpCred", visit.ExpCred);
      
        Database.PutParameter(dbCommand,"?SpecName", visit.SpecName);
      
        Database.PutParameter(dbCommand,"?ServmanBaseTimeFrameId", visit.ServmanBaseTimeFrameId);
      
        Database.PutParameter(dbCommand,"?SdPercent", visit.SdPercent);
      
        Database.PutParameter(dbCommand,"?TaxPercent", visit.TaxPercent);
      
        Database.PutParameter(dbCommand,"?DurationCost", visit.DurationCost);
      
        Database.PutParameter(dbCommand,"?SnapshotDate", visit.SnapshotDate);
      
        Database.PutParameter(dbCommand,"?IsBlockout", visit.IsBlockout);
      
        Database.PutParameter(dbCommand,"?TechnicianEmailSentDate", visit.TechnicianEmailSentDate);
      
        Database.PutParameter(dbCommand,"?IsSecondaryEmailSent", visit.IsSecondaryEmailSent);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TechnicianId",visit.TechnicianId);
      
        Database.UpdateParameter(dbCommand,"?TimeFrameId",visit.TimeFrameId);
      
        Database.UpdateParameter(dbCommand,"?TimeStart",visit.TimeStart);
      
        Database.UpdateParameter(dbCommand,"?TimeEnd",visit.TimeEnd);
      
        Database.UpdateParameter(dbCommand,"?Latitude",visit.Latitude);
      
        Database.UpdateParameter(dbCommand,"?Longitude",visit.Longitude);
      
        Database.UpdateParameter(dbCommand,"?Zip",visit.Zip);
      
        Database.UpdateParameter(dbCommand,"?Cost",visit.Cost);
      
        Database.UpdateParameter(dbCommand,"?ExclusiveCompanyId",visit.ExclusiveCompanyId);
      
        Database.UpdateParameter(dbCommand,"?ExclusiveTechnicianDefaultId",visit.ExclusiveTechnicianDefaultId);
      
        Database.UpdateParameter(dbCommand,"?IsTempIgnoreExclusivity",visit.IsTempIgnoreExclusivity);
      
        Database.UpdateParameter(dbCommand,"?TempExclusiveTechnicianId",visit.TempExclusiveTechnicianId);
      
        Database.UpdateParameter(dbCommand,"?ForbiddenTechnicianDefaultId",visit.ForbiddenTechnicianDefaultId);
      
        Database.UpdateParameter(dbCommand,"?TicketNumber",visit.TicketNumber);
      
        Database.UpdateParameter(dbCommand,"?CustomerName",visit.CustomerName);
      
        Database.UpdateParameter(dbCommand,"?Street",visit.Street);
      
        Database.UpdateParameter(dbCommand,"?Address2",visit.Address2);
      
        Database.UpdateParameter(dbCommand,"?City",visit.City);
      
        Database.UpdateParameter(dbCommand,"?State",visit.State);
      
        Database.UpdateParameter(dbCommand,"?HomePhone",visit.HomePhone);
      
        Database.UpdateParameter(dbCommand,"?BusinessPhone",visit.BusinessPhone);
      
        Database.UpdateParameter(dbCommand,"?IsEstimate",visit.IsEstimate);
      
        Database.UpdateParameter(dbCommand,"?IsEstimateAndDo",visit.IsEstimateAndDo);
      
        Database.UpdateParameter(dbCommand,"?IsRework",visit.IsRework);
      
        Database.UpdateParameter(dbCommand,"?CallDateTime",visit.CallDateTime);
      
        Database.UpdateParameter(dbCommand,"?Mapsco",visit.Mapsco);
      
        Database.UpdateParameter(dbCommand,"?IsCalledCustomer",visit.IsCalledCustomer);
      
        Database.UpdateParameter(dbCommand,"?IsFixed",visit.IsFixed);
      
        Database.UpdateParameter(dbCommand,"?Area",visit.Area);
      
        Database.UpdateParameter(dbCommand,"?ServType",visit.ServType);
      
        Database.UpdateParameter(dbCommand,"?AdsourceAcronym",visit.AdsourceAcronym);
      
        Database.UpdateParameter(dbCommand,"?CustomerRank",visit.CustomerRank);
      
        Database.UpdateParameter(dbCommand,"?OriginatedTechnicianDefaultId",visit.OriginatedTechnicianDefaultId);
      
        Database.UpdateParameter(dbCommand,"?OriginatedCompleteDate",visit.OriginatedCompleteDate);
      
        Database.UpdateParameter(dbCommand,"?OriginatedTicketNumber",visit.OriginatedTicketNumber);
      
        Database.UpdateParameter(dbCommand,"?CustomerExclusiveTechnicianDefaultId",visit.CustomerExclusiveTechnicianDefaultId);
      
        Database.UpdateParameter(dbCommand,"?Note",visit.Note);
      
        Database.UpdateParameter(dbCommand,"?ExpCred",visit.ExpCred);
      
        Database.UpdateParameter(dbCommand,"?SpecName",visit.SpecName);
      
        Database.UpdateParameter(dbCommand,"?ServmanBaseTimeFrameId",visit.ServmanBaseTimeFrameId);
      
        Database.UpdateParameter(dbCommand,"?SdPercent",visit.SdPercent);
      
        Database.UpdateParameter(dbCommand,"?TaxPercent",visit.TaxPercent);
      
        Database.UpdateParameter(dbCommand,"?DurationCost",visit.DurationCost);
      
        Database.UpdateParameter(dbCommand,"?SnapshotDate",visit.SnapshotDate);
      
        Database.UpdateParameter(dbCommand,"?IsBlockout",visit.IsBlockout);
      
        Database.UpdateParameter(dbCommand,"?TechnicianEmailSentDate",visit.TechnicianEmailSentDate);
      
        Database.UpdateParameter(dbCommand,"?IsSecondaryEmailSent",visit.IsSecondaryEmailSent);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        visit.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Visit>  visitList)
      {
        Insert(visitList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Visit Set "
      
        + " TechnicianId = ?TechnicianId, "
      
        + " TimeFrameId = ?TimeFrameId, "
      
        + " TimeStart = ?TimeStart, "
      
        + " TimeEnd = ?TimeEnd, "
      
        + " Latitude = ?Latitude, "
      
        + " Longitude = ?Longitude, "
      
        + " Zip = ?Zip, "
      
        + " Cost = ?Cost, "
      
        + " ExclusiveCompanyId = ?ExclusiveCompanyId, "
      
        + " ExclusiveTechnicianDefaultId = ?ExclusiveTechnicianDefaultId, "
      
        + " IsTempIgnoreExclusivity = ?IsTempIgnoreExclusivity, "
      
        + " TempExclusiveTechnicianId = ?TempExclusiveTechnicianId, "
      
        + " ForbiddenTechnicianDefaultId = ?ForbiddenTechnicianDefaultId, "
      
        + " TicketNumber = ?TicketNumber, "
      
        + " CustomerName = ?CustomerName, "
      
        + " Street = ?Street, "
      
        + " Address2 = ?Address2, "
      
        + " City = ?City, "
      
        + " State = ?State, "
      
        + " HomePhone = ?HomePhone, "
      
        + " BusinessPhone = ?BusinessPhone, "
      
        + " IsEstimate = ?IsEstimate, "
      
        + " IsEstimateAndDo = ?IsEstimateAndDo, "
      
        + " IsRework = ?IsRework, "
      
        + " CallDateTime = ?CallDateTime, "
      
        + " Mapsco = ?Mapsco, "
      
        + " IsCalledCustomer = ?IsCalledCustomer, "
      
        + " IsFixed = ?IsFixed, "
      
        + " Area = ?Area, "
      
        + " ServType = ?ServType, "
      
        + " AdsourceAcronym = ?AdsourceAcronym, "
      
        + " CustomerRank = ?CustomerRank, "
      
        + " OriginatedTechnicianDefaultId = ?OriginatedTechnicianDefaultId, "
      
        + " OriginatedCompleteDate = ?OriginatedCompleteDate, "
      
        + " OriginatedTicketNumber = ?OriginatedTicketNumber, "
      
        + " CustomerExclusiveTechnicianDefaultId = ?CustomerExclusiveTechnicianDefaultId, "
      
        + " Note = ?Note, "
      
        + " ExpCred = ?ExpCred, "
      
        + " SpecName = ?SpecName, "
      
        + " ServmanBaseTimeFrameId = ?ServmanBaseTimeFrameId, "
      
        + " SdPercent = ?SdPercent, "
      
        + " TaxPercent = ?TaxPercent, "
      
        + " DurationCost = ?DurationCost, "
      
        + " SnapshotDate = ?SnapshotDate, "
      
        + " IsBlockout = ?IsBlockout, "
      
        + " TechnicianEmailSentDate = ?TechnicianEmailSentDate, "
      
        + " IsSecondaryEmailSent = ?IsSecondaryEmailSent "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Visit visit, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", visit.ID);
      
        Database.PutParameter(dbCommand,"?TechnicianId", visit.TechnicianId);
      
        Database.PutParameter(dbCommand,"?TimeFrameId", visit.TimeFrameId);
      
        Database.PutParameter(dbCommand,"?TimeStart", visit.TimeStart);
      
        Database.PutParameter(dbCommand,"?TimeEnd", visit.TimeEnd);
      
        Database.PutParameter(dbCommand,"?Latitude", visit.Latitude);
      
        Database.PutParameter(dbCommand,"?Longitude", visit.Longitude);
      
        Database.PutParameter(dbCommand,"?Zip", visit.Zip);
      
        Database.PutParameter(dbCommand,"?Cost", visit.Cost);
      
        Database.PutParameter(dbCommand,"?ExclusiveCompanyId", visit.ExclusiveCompanyId);
      
        Database.PutParameter(dbCommand,"?ExclusiveTechnicianDefaultId", visit.ExclusiveTechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?IsTempIgnoreExclusivity", visit.IsTempIgnoreExclusivity);
      
        Database.PutParameter(dbCommand,"?TempExclusiveTechnicianId", visit.TempExclusiveTechnicianId);
      
        Database.PutParameter(dbCommand,"?ForbiddenTechnicianDefaultId", visit.ForbiddenTechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?TicketNumber", visit.TicketNumber);
      
        Database.PutParameter(dbCommand,"?CustomerName", visit.CustomerName);
      
        Database.PutParameter(dbCommand,"?Street", visit.Street);
      
        Database.PutParameter(dbCommand,"?Address2", visit.Address2);
      
        Database.PutParameter(dbCommand,"?City", visit.City);
      
        Database.PutParameter(dbCommand,"?State", visit.State);
      
        Database.PutParameter(dbCommand,"?HomePhone", visit.HomePhone);
      
        Database.PutParameter(dbCommand,"?BusinessPhone", visit.BusinessPhone);
      
        Database.PutParameter(dbCommand,"?IsEstimate", visit.IsEstimate);
      
        Database.PutParameter(dbCommand,"?IsEstimateAndDo", visit.IsEstimateAndDo);
      
        Database.PutParameter(dbCommand,"?IsRework", visit.IsRework);
      
        Database.PutParameter(dbCommand,"?CallDateTime", visit.CallDateTime);
      
        Database.PutParameter(dbCommand,"?Mapsco", visit.Mapsco);
      
        Database.PutParameter(dbCommand,"?IsCalledCustomer", visit.IsCalledCustomer);
      
        Database.PutParameter(dbCommand,"?IsFixed", visit.IsFixed);
      
        Database.PutParameter(dbCommand,"?Area", visit.Area);
      
        Database.PutParameter(dbCommand,"?ServType", visit.ServType);
      
        Database.PutParameter(dbCommand,"?AdsourceAcronym", visit.AdsourceAcronym);
      
        Database.PutParameter(dbCommand,"?CustomerRank", visit.CustomerRank);
      
        Database.PutParameter(dbCommand,"?OriginatedTechnicianDefaultId", visit.OriginatedTechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?OriginatedCompleteDate", visit.OriginatedCompleteDate);
      
        Database.PutParameter(dbCommand,"?OriginatedTicketNumber", visit.OriginatedTicketNumber);
      
        Database.PutParameter(dbCommand,"?CustomerExclusiveTechnicianDefaultId", visit.CustomerExclusiveTechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?Note", visit.Note);
      
        Database.PutParameter(dbCommand,"?ExpCred", visit.ExpCred);
      
        Database.PutParameter(dbCommand,"?SpecName", visit.SpecName);
      
        Database.PutParameter(dbCommand,"?ServmanBaseTimeFrameId", visit.ServmanBaseTimeFrameId);
      
        Database.PutParameter(dbCommand,"?SdPercent", visit.SdPercent);
      
        Database.PutParameter(dbCommand,"?TaxPercent", visit.TaxPercent);
      
        Database.PutParameter(dbCommand,"?DurationCost", visit.DurationCost);
      
        Database.PutParameter(dbCommand,"?SnapshotDate", visit.SnapshotDate);
      
        Database.PutParameter(dbCommand,"?IsBlockout", visit.IsBlockout);
      
        Database.PutParameter(dbCommand,"?TechnicianEmailSentDate", visit.TechnicianEmailSentDate);
      
        Database.PutParameter(dbCommand,"?IsSecondaryEmailSent", visit.IsSecondaryEmailSent);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Visit visit)
      {
        Update(visit, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " TechnicianId, "
      
        + " TimeFrameId, "
      
        + " TimeStart, "
      
        + " TimeEnd, "
      
        + " Latitude, "
      
        + " Longitude, "
      
        + " Zip, "
      
        + " Cost, "
      
        + " ExclusiveCompanyId, "
      
        + " ExclusiveTechnicianDefaultId, "
      
        + " IsTempIgnoreExclusivity, "
      
        + " TempExclusiveTechnicianId, "
      
        + " ForbiddenTechnicianDefaultId, "
      
        + " TicketNumber, "
      
        + " CustomerName, "
      
        + " Street, "
      
        + " Address2, "
      
        + " City, "
      
        + " State, "
      
        + " HomePhone, "
      
        + " BusinessPhone, "
      
        + " IsEstimate, "
      
        + " IsEstimateAndDo, "
      
        + " IsRework, "
      
        + " CallDateTime, "
      
        + " Mapsco, "
      
        + " IsCalledCustomer, "
      
        + " IsFixed, "
      
        + " Area, "
      
        + " ServType, "
      
        + " AdsourceAcronym, "
      
        + " CustomerRank, "
      
        + " OriginatedTechnicianDefaultId, "
      
        + " OriginatedCompleteDate, "
      
        + " OriginatedTicketNumber, "
      
        + " CustomerExclusiveTechnicianDefaultId, "
      
        + " Note, "
      
        + " ExpCred, "
      
        + " SpecName, "
      
        + " ServmanBaseTimeFrameId, "
      
        + " SdPercent, "
      
        + " TaxPercent, "
      
        + " DurationCost, "
      
        + " SnapshotDate, "
      
        + " IsBlockout, "
      
        + " TechnicianEmailSentDate, "
      
        + " IsSecondaryEmailSent "
      

      + " From Visit "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Visit FindByPrimaryKey(
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
      throw new DataNotFoundException("Visit not found, search by primary key");

      }

      public static Visit FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Visit visit, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",visit.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Visit visit)
      {
      return Exists(visit, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Visit limit 1";

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

      public static Visit Load(IDataReader dataReader, int offset)
      {
      Visit visit = new Visit();

      visit.ID = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            visit.TechnicianId = dataReader.GetInt32(1 + offset);
          visit.TimeFrameId = dataReader.GetInt32(2 + offset);
          visit.TimeStart = dataReader.GetDateTime(3 + offset);
          visit.TimeEnd = dataReader.GetDateTime(4 + offset);
          visit.Latitude = dataReader.GetFloat(5 + offset);
          visit.Longitude = dataReader.GetFloat(6 + offset);
          visit.Zip = dataReader.GetString(7 + offset);
          visit.Cost = dataReader.GetDecimal(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            visit.ExclusiveCompanyId = dataReader.GetInt32(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            visit.ExclusiveTechnicianDefaultId = dataReader.GetInt32(10 + offset);
          visit.IsTempIgnoreExclusivity = dataReader.GetBoolean(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            visit.TempExclusiveTechnicianId = dataReader.GetInt32(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            visit.ForbiddenTechnicianDefaultId = dataReader.GetInt32(13 + offset);
          visit.TicketNumber = dataReader.GetString(14 + offset);
          visit.CustomerName = dataReader.GetString(15 + offset);
          visit.Street = dataReader.GetString(16 + offset);
          visit.Address2 = dataReader.GetString(17 + offset);
          visit.City = dataReader.GetString(18 + offset);
          visit.State = dataReader.GetString(19 + offset);
          visit.HomePhone = dataReader.GetString(20 + offset);
          visit.BusinessPhone = dataReader.GetString(21 + offset);
          visit.IsEstimate = dataReader.GetBoolean(22 + offset);
          visit.IsEstimateAndDo = dataReader.GetBoolean(23 + offset);
          visit.IsRework = dataReader.GetBoolean(24 + offset);
          
            if(!dataReader.IsDBNull(25 + offset))
            visit.CallDateTime = dataReader.GetDateTime(25 + offset);
          visit.Mapsco = dataReader.GetString(26 + offset);
          visit.IsCalledCustomer = dataReader.GetBoolean(27 + offset);
          visit.IsFixed = dataReader.GetBoolean(28 + offset);
          visit.Area = dataReader.GetString(29 + offset);
          visit.ServType = dataReader.GetInt32(30 + offset);
          visit.AdsourceAcronym = dataReader.GetString(31 + offset);
          visit.CustomerRank = dataReader.GetInt32(32 + offset);
          
            if(!dataReader.IsDBNull(33 + offset))
            visit.OriginatedTechnicianDefaultId = dataReader.GetInt32(33 + offset);
          
            if(!dataReader.IsDBNull(34 + offset))
            visit.OriginatedCompleteDate = dataReader.GetDateTime(34 + offset);
          visit.OriginatedTicketNumber = dataReader.GetString(35 + offset);
          
            if(!dataReader.IsDBNull(36 + offset))
            visit.CustomerExclusiveTechnicianDefaultId = dataReader.GetInt32(36 + offset);
          visit.Note = dataReader.GetString(37 + offset);
          visit.ExpCred = dataReader.GetBoolean(38 + offset);
          visit.SpecName = dataReader.GetString(39 + offset);
          
            if(!dataReader.IsDBNull(40 + offset))
            visit.ServmanBaseTimeFrameId = dataReader.GetInt32(40 + offset);
          
            if(!dataReader.IsDBNull(41 + offset))
            visit.SdPercent = dataReader.GetDecimal(41 + offset);
          
            if(!dataReader.IsDBNull(42 + offset))
            visit.TaxPercent = dataReader.GetDecimal(42 + offset);
          visit.DurationCost = dataReader.GetDecimal(43 + offset);
          
            if(!dataReader.IsDBNull(44 + offset))
            visit.SnapshotDate = dataReader.GetDateTime(44 + offset);
          visit.IsBlockout = dataReader.GetBoolean(45 + offset);
          
            if(!dataReader.IsDBNull(46 + offset))
            visit.TechnicianEmailSentDate = dataReader.GetDateTime(46 + offset);
          visit.IsSecondaryEmailSent = dataReader.GetBoolean(47 + offset);
          

      return visit;
      }

      public static Visit Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Visit "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Visit visit, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", visit.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Visit visit)
      {
        Delete(visit, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Visit ";

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
      
        + " TechnicianId, "
      
        + " TimeFrameId, "
      
        + " TimeStart, "
      
        + " TimeEnd, "
      
        + " Latitude, "
      
        + " Longitude, "
      
        + " Zip, "
      
        + " Cost, "
      
        + " ExclusiveCompanyId, "
      
        + " ExclusiveTechnicianDefaultId, "
      
        + " IsTempIgnoreExclusivity, "
      
        + " TempExclusiveTechnicianId, "
      
        + " ForbiddenTechnicianDefaultId, "
      
        + " TicketNumber, "
      
        + " CustomerName, "
      
        + " Street, "
      
        + " Address2, "
      
        + " City, "
      
        + " State, "
      
        + " HomePhone, "
      
        + " BusinessPhone, "
      
        + " IsEstimate, "
      
        + " IsEstimateAndDo, "
      
        + " IsRework, "
      
        + " CallDateTime, "
      
        + " Mapsco, "
      
        + " IsCalledCustomer, "
      
        + " IsFixed, "
      
        + " Area, "
      
        + " ServType, "
      
        + " AdsourceAcronym, "
      
        + " CustomerRank, "
      
        + " OriginatedTechnicianDefaultId, "
      
        + " OriginatedCompleteDate, "
      
        + " OriginatedTicketNumber, "
      
        + " CustomerExclusiveTechnicianDefaultId, "
      
        + " Note, "
      
        + " ExpCred, "
      
        + " SpecName, "
      
        + " ServmanBaseTimeFrameId, "
      
        + " SdPercent, "
      
        + " TaxPercent, "
      
        + " DurationCost, "
      
        + " SnapshotDate, "
      
        + " IsBlockout, "
      
        + " TechnicianEmailSentDate, "
      
        + " IsSecondaryEmailSent "
      

      + " From Visit ";
      public static List<Visit> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Visit> rv = new List<Visit>();

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

      public static List<Visit> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Visit> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Visit> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Visit));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Visit item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Visit>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Visit));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Visit> itemsList
      = new List<Visit>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Visit)
      itemsList.Add(deserializedObject as Visit);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int? m_technicianId;
      
        protected int m_timeFrameId;
      
        protected DateTime m_timeStart;
      
        protected DateTime m_timeEnd;
      
        protected float m_latitude;
      
        protected float m_longitude;
      
        protected String m_zip;
      
        protected decimal m_cost;
      
        protected int? m_exclusiveCompanyId;
      
        protected int? m_exclusiveTechnicianDefaultId;
      
        protected bool m_isTempIgnoreExclusivity;
      
        protected int? m_tempExclusiveTechnicianId;
      
        protected int? m_forbiddenTechnicianDefaultId;
      
        protected String m_ticketNumber;
      
        protected String m_customerName;
      
        protected String m_street;
      
        protected String m_address2;
      
        protected String m_city;
      
        protected String m_state;
      
        protected String m_homePhone;
      
        protected String m_businessPhone;
      
        protected bool m_isEstimate;
      
        protected bool m_isEstimateAndDo;
      
        protected bool m_isRework;
      
        protected DateTime? m_callDateTime;
      
        protected String m_mapsco;
      
        protected bool m_isCalledCustomer;
      
        protected bool m_isFixed;
      
        protected String m_area;
      
        protected int m_servType;
      
        protected String m_adsourceAcronym;
      
        protected int m_customerRank;
      
        protected int? m_originatedTechnicianDefaultId;
      
        protected DateTime? m_originatedCompleteDate;
      
        protected String m_originatedTicketNumber;
      
        protected int? m_customerExclusiveTechnicianDefaultId;
      
        protected String m_note;
      
        protected bool m_expCred;
      
        protected String m_specName;
      
        protected int? m_servmanBaseTimeFrameId;
      
        protected decimal m_sdPercent;
      
        protected decimal m_taxPercent;
      
        protected decimal m_durationCost;
      
        protected DateTime? m_snapshotDate;
      
        protected bool m_isBlockout;
      
        protected DateTime? m_technicianEmailSentDate;
      
        protected bool m_isSecondaryEmailSent;
      
      #endregion

      #region Constructors
      public Visit(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Visit(
        int 
          iD,int? 
          technicianId,int 
          timeFrameId,DateTime 
          timeStart,DateTime 
          timeEnd,float 
          latitude,float 
          longitude,String 
          zip,decimal 
          cost,int? 
          exclusiveCompanyId,int? 
          exclusiveTechnicianDefaultId,bool 
          isTempIgnoreExclusivity,int? 
          tempExclusiveTechnicianId,int? 
          forbiddenTechnicianDefaultId,String 
          ticketNumber,String 
          customerName,String 
          street,String 
          address2,String 
          city,String 
          state,String 
          homePhone,String 
          businessPhone,bool 
          isEstimate,bool 
          isEstimateAndDo,bool 
          isRework,DateTime? 
          callDateTime,String 
          mapsco,bool 
          isCalledCustomer,bool 
          isFixed,String 
          area,int 
          servType,String 
          adsourceAcronym,int 
          customerRank,int? 
          originatedTechnicianDefaultId,DateTime? 
          originatedCompleteDate,String 
          originatedTicketNumber,int? 
          customerExclusiveTechnicianDefaultId,String 
          note,bool 
          expCred,String 
          specName,int? 
          servmanBaseTimeFrameId,decimal 
          sdPercent,decimal 
          taxPercent,decimal 
          durationCost,DateTime? 
          snapshotDate,bool 
          isBlockout,DateTime? 
          technicianEmailSentDate,bool 
          isSecondaryEmailSent
        ) : this()
        {
        
          m_iD = iD;
        
          m_technicianId = technicianId;
        
          m_timeFrameId = timeFrameId;
        
          m_timeStart = timeStart;
        
          m_timeEnd = timeEnd;
        
          m_latitude = latitude;
        
          m_longitude = longitude;
        
          m_zip = zip;
        
          m_cost = cost;
        
          m_exclusiveCompanyId = exclusiveCompanyId;
        
          m_exclusiveTechnicianDefaultId = exclusiveTechnicianDefaultId;
        
          m_isTempIgnoreExclusivity = isTempIgnoreExclusivity;
        
          m_tempExclusiveTechnicianId = tempExclusiveTechnicianId;
        
          m_forbiddenTechnicianDefaultId = forbiddenTechnicianDefaultId;
        
          m_ticketNumber = ticketNumber;
        
          m_customerName = customerName;
        
          m_street = street;
        
          m_address2 = address2;
        
          m_city = city;
        
          m_state = state;
        
          m_homePhone = homePhone;
        
          m_businessPhone = businessPhone;
        
          m_isEstimate = isEstimate;
        
          m_isEstimateAndDo = isEstimateAndDo;
        
          m_isRework = isRework;
        
          m_callDateTime = callDateTime;
        
          m_mapsco = mapsco;
        
          m_isCalledCustomer = isCalledCustomer;
        
          m_isFixed = isFixed;
        
          m_area = area;
        
          m_servType = servType;
        
          m_adsourceAcronym = adsourceAcronym;
        
          m_customerRank = customerRank;
        
          m_originatedTechnicianDefaultId = originatedTechnicianDefaultId;
        
          m_originatedCompleteDate = originatedCompleteDate;
        
          m_originatedTicketNumber = originatedTicketNumber;
        
          m_customerExclusiveTechnicianDefaultId = customerExclusiveTechnicianDefaultId;
        
          m_note = note;
        
          m_expCred = expCred;
        
          m_specName = specName;
        
          m_servmanBaseTimeFrameId = servmanBaseTimeFrameId;
        
          m_sdPercent = sdPercent;
        
          m_taxPercent = taxPercent;
        
          m_durationCost = durationCost;
        
          m_snapshotDate = snapshotDate;
        
          m_isBlockout = isBlockout;
        
          m_technicianEmailSentDate = technicianEmailSentDate;
        
          m_isSecondaryEmailSent = isSecondaryEmailSent;
        
        }


      
      #endregion

      
        [DataMember]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
            
        [DataMember]
        public int TimeFrameId
        {
        get { return m_timeFrameId;}
        set { m_timeFrameId = value; }
        }
            
        [DataMember]
        public DateTime TimeEnd
        {
        get { return m_timeEnd;}
        set { m_timeEnd = value; }
        }
      
        [DataMember]
        public float Latitude
        {
        get { return m_latitude;}
        set { m_latitude = value; }
        }
      
        [DataMember]
        public float Longitude
        {
        get { return m_longitude;}
        set { m_longitude = value; }
        }
      
        [DataMember]
        public String Zip
        {
        get { return m_zip;}
        set { m_zip = value; }
        }
      
        [DataMember]
        public decimal Cost
        {
        get { return m_cost;}
        set { m_cost = value; }
        }
      
        [DataMember]
        public int? ExclusiveCompanyId
        {
        get { return m_exclusiveCompanyId;}
        set { m_exclusiveCompanyId = value; }
        }
      
        [DataMember]
        public int? ExclusiveTechnicianDefaultId
        {
        get { return m_exclusiveTechnicianDefaultId;}
        set { m_exclusiveTechnicianDefaultId = value; }
        }
      
        [DataMember]
        public bool IsTempIgnoreExclusivity
        {
        get { return m_isTempIgnoreExclusivity;}
        set { m_isTempIgnoreExclusivity = value; }
        }
      
        [DataMember]
        public int? TempExclusiveTechnicianId
        {
        get { return m_tempExclusiveTechnicianId;}
        set { m_tempExclusiveTechnicianId = value; }
        }
      
        [DataMember]
        public int? ForbiddenTechnicianDefaultId
        {
        get { return m_forbiddenTechnicianDefaultId;}
        set { m_forbiddenTechnicianDefaultId = value; }
        }
      
        [DataMember]
        public String TicketNumber
        {
        get { return m_ticketNumber;}
        set { m_ticketNumber = value; }
        }
      
        [DataMember]
        public String CustomerName
        {
        get { return m_customerName;}
        set { m_customerName = value; }
        }
      
        [DataMember]
        public String Street
        {
        get { return m_street;}
        set { m_street = value; }
        }
      
        [DataMember]
        public String Address2
        {
        get { return m_address2;}
        set { m_address2 = value; }
        }
      
        [DataMember]
        public String City
        {
        get { return m_city;}
        set { m_city = value; }
        }
      
        [DataMember]
        public String State
        {
        get { return m_state;}
        set { m_state = value; }
        }
      
        [DataMember]
        public String HomePhone
        {
        get { return m_homePhone;}
        set { m_homePhone = value; }
        }
      
        [DataMember]
        public String BusinessPhone
        {
        get { return m_businessPhone;}
        set { m_businessPhone = value; }
        }
      
        [DataMember]
        public bool IsEstimate
        {
        get { return m_isEstimate;}
        set { m_isEstimate = value; }
        }
      
        [DataMember]
        public bool IsEstimateAndDo
        {
        get { return m_isEstimateAndDo;}
        set { m_isEstimateAndDo = value; }
        }
      
        [DataMember]
        public bool IsRework
        {
        get { return m_isRework;}
        set { m_isRework = value; }
        }
      
        [DataMember]
        public DateTime? CallDateTime
        {
        get { return m_callDateTime;}
        set { m_callDateTime = value; }
        }
      
        [DataMember]
        public String Mapsco
        {
        get { return m_mapsco;}
        set { m_mapsco = value; }
        }
      
        [DataMember]
        public bool IsCalledCustomer
        {
        get { return m_isCalledCustomer;}
        set { m_isCalledCustomer = value; }
        }
      
        [DataMember]
        public bool IsFixed
        {
        get { return m_isFixed;}
        set { m_isFixed = value; }
        }
      
        [DataMember]
        public String Area
        {
        get { return m_area;}
        set { m_area = value; }
        }
      
        [DataMember]
        public int ServType
        {
        get { return m_servType;}
        set { m_servType = value; }
        }
      
        [DataMember]
        public String AdsourceAcronym
        {
        get { return m_adsourceAcronym;}
        set { m_adsourceAcronym = value; }
        }
      
        [DataMember]
        public int CustomerRank
        {
        get { return m_customerRank;}
        set { m_customerRank = value; }
        }
      
        [DataMember]
        public int? OriginatedTechnicianDefaultId
        {
        get { return m_originatedTechnicianDefaultId;}
        set { m_originatedTechnicianDefaultId = value; }
        }
      
        [DataMember]
        public DateTime? OriginatedCompleteDate
        {
        get { return m_originatedCompleteDate;}
        set { m_originatedCompleteDate = value; }
        }
      
        [DataMember]
        public String OriginatedTicketNumber
        {
        get { return m_originatedTicketNumber;}
        set { m_originatedTicketNumber = value; }
        }
      
        [DataMember]
        public int? CustomerExclusiveTechnicianDefaultId
        {
        get { return m_customerExclusiveTechnicianDefaultId;}
        set { m_customerExclusiveTechnicianDefaultId = value; }
        }
      
        [DataMember]
        public String Note
        {
        get { return m_note;}
        set { m_note = value; }
        }
      
        [DataMember]
        public bool ExpCred
        {
        get { return m_expCred;}
        set { m_expCred = value; }
        }
      
        [DataMember]
        public String SpecName
        {
        get { return m_specName;}
        set { m_specName = value; }
        }
      
        [DataMember]
        public int? ServmanBaseTimeFrameId
        {
        get { return m_servmanBaseTimeFrameId;}
        set { m_servmanBaseTimeFrameId = value; }
        }
      
        [DataMember]
        public decimal SdPercent
        {
        get { return m_sdPercent;}
        set { m_sdPercent = value; }
        }
      
        [DataMember]
        public decimal TaxPercent
        {
        get { return m_taxPercent;}
        set { m_taxPercent = value; }
        }
      
        [DataMember]
        public decimal DurationCost
        {
        get { return m_durationCost;}
        set { m_durationCost = value; }
        }
      
        [DataMember]
        public DateTime? SnapshotDate
        {
        get { return m_snapshotDate;}
        set { m_snapshotDate = value; }
        }
      
        [DataMember]
        public bool IsBlockout
        {
        get { return m_isBlockout;}
        set { m_isBlockout = value; }
        }
      
        [DataMember]
        public DateTime? TechnicianEmailSentDate
        {
        get { return m_technicianEmailSentDate;}
        set { m_technicianEmailSentDate = value; }
        }
      
        [DataMember]
        public bool IsSecondaryEmailSent
        {
        get { return m_isSecondaryEmailSent;}
        set { m_isSecondaryEmailSent = value; }
        }
      

      public static int FieldsCount
      {
      get { return 48; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    