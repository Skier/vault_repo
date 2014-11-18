
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


      public partial class QbInvoice : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbInvoice ( " +
      
        " DumptedInvoiceId, " +
      
        " DumpWorkTransactionId, " +
      
        " QbCustomerId, " +
      
        " CustomerRefListId, " +
      
        " ProcessedDate, " +
      
        " ProcessedByEmployeeId, " +
      
        " CreatedDate, " +
      
        " TxnID, " +
      
        " TimeCreatedInQb, " +
      
        " TimeModifiedInQb, " +
      
        " TxnNumber, " +
      
        " EditSequence, " +
      
        " QbClassListId, " +
      
        " QbAccountListId, " +
      
        " QbTemplateListId, " +
      
        " TxnDate, " +
      
        " RefNumber, " +
      
        " BillingAddressAddr1, " +
      
        " BillingAddressAddr2, " +
      
        " BillingAddressAddr3, " +
      
        " BillingAddressAddr4, " +
      
        " BillingAddressAddr5, " +
      
        " BillingAddressCity, " +
      
        " BillingAddresState, " +
      
        " BillingAddresPostalCode, " +
      
        " BillingAddressCountry, " +
      
        " BillingAddressNote, " +
      
        " ShipAddressAddr1, " +
      
        " ShipAddressAddr2, " +
      
        " ShipAddressAddr3, " +
      
        " ShipAddressAddr4, " +
      
        " ShipAddressAddr5, " +
      
        " ShipAddressCity, " +
      
        " ShipAddressState, " +
      
        " ShipAddressPostalCode, " +
      
        " ShipAddressCountry, " +
      
        " ShipAddressNote, " +
      
        " QbInvoiceTermListId, " +
      
        " Memo, " +
      
        " ItemSalesTaxRef, " +
      
        " QbSalesRepRefListId, " +
      
        " SubTotalAmount, " +
      
        " TaxAmount, " +
      
        " TotalAmount, " +
      
        " IsVoid, " +
      
        " IsPending " +
      
      ") Values (" +
      
        " ?DumptedInvoiceId, " +
      
        " ?DumpWorkTransactionId, " +
      
        " ?QbCustomerId, " +
      
        " ?CustomerRefListId, " +
      
        " ?ProcessedDate, " +
      
        " ?ProcessedByEmployeeId, " +
      
        " ?CreatedDate, " +
      
        " ?TxnID, " +
      
        " ?TimeCreatedInQb, " +
      
        " ?TimeModifiedInQb, " +
      
        " ?TxnNumber, " +
      
        " ?EditSequence, " +
      
        " ?QbClassListId, " +
      
        " ?QbAccountListId, " +
      
        " ?QbTemplateListId, " +
      
        " ?TxnDate, " +
      
        " ?RefNumber, " +
      
        " ?BillingAddressAddr1, " +
      
        " ?BillingAddressAddr2, " +
      
        " ?BillingAddressAddr3, " +
      
        " ?BillingAddressAddr4, " +
      
        " ?BillingAddressAddr5, " +
      
        " ?BillingAddressCity, " +
      
        " ?BillingAddresState, " +
      
        " ?BillingAddresPostalCode, " +
      
        " ?BillingAddressCountry, " +
      
        " ?BillingAddressNote, " +
      
        " ?ShipAddressAddr1, " +
      
        " ?ShipAddressAddr2, " +
      
        " ?ShipAddressAddr3, " +
      
        " ?ShipAddressAddr4, " +
      
        " ?ShipAddressAddr5, " +
      
        " ?ShipAddressCity, " +
      
        " ?ShipAddressState, " +
      
        " ?ShipAddressPostalCode, " +
      
        " ?ShipAddressCountry, " +
      
        " ?ShipAddressNote, " +
      
        " ?QbInvoiceTermListId, " +
      
        " ?Memo, " +
      
        " ?ItemSalesTaxRef, " +
      
        " ?QbSalesRepRefListId, " +
      
        " ?SubTotalAmount, " +
      
        " ?TaxAmount, " +
      
        " ?TotalAmount, " +
      
        " ?IsVoid, " +
      
        " ?IsPending " +
      
      ")";

      public static void Insert(QbInvoice qbInvoice, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?DumptedInvoiceId", qbInvoice.DumptedInvoiceId);
      
        Database.PutParameter(dbCommand,"?DumpWorkTransactionId", qbInvoice.DumpWorkTransactionId);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", qbInvoice.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?CustomerRefListId", qbInvoice.CustomerRefListId);
      
        Database.PutParameter(dbCommand,"?ProcessedDate", qbInvoice.ProcessedDate);
      
        Database.PutParameter(dbCommand,"?ProcessedByEmployeeId", qbInvoice.ProcessedByEmployeeId);
      
        Database.PutParameter(dbCommand,"?CreatedDate", qbInvoice.CreatedDate);
      
        Database.PutParameter(dbCommand,"?TxnID", qbInvoice.TxnID);
      
        Database.PutParameter(dbCommand,"?TimeCreatedInQb", qbInvoice.TimeCreatedInQb);
      
        Database.PutParameter(dbCommand,"?TimeModifiedInQb", qbInvoice.TimeModifiedInQb);
      
        Database.PutParameter(dbCommand,"?TxnNumber", qbInvoice.TxnNumber);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbInvoice.EditSequence);
      
        Database.PutParameter(dbCommand,"?QbClassListId", qbInvoice.QbClassListId);
      
        Database.PutParameter(dbCommand,"?QbAccountListId", qbInvoice.QbAccountListId);
      
        Database.PutParameter(dbCommand,"?QbTemplateListId", qbInvoice.QbTemplateListId);
      
        Database.PutParameter(dbCommand,"?TxnDate", qbInvoice.TxnDate);
      
        Database.PutParameter(dbCommand,"?RefNumber", qbInvoice.RefNumber);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr1", qbInvoice.BillingAddressAddr1);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr2", qbInvoice.BillingAddressAddr2);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr3", qbInvoice.BillingAddressAddr3);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr4", qbInvoice.BillingAddressAddr4);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr5", qbInvoice.BillingAddressAddr5);
      
        Database.PutParameter(dbCommand,"?BillingAddressCity", qbInvoice.BillingAddressCity);
      
        Database.PutParameter(dbCommand,"?BillingAddresState", qbInvoice.BillingAddresState);
      
        Database.PutParameter(dbCommand,"?BillingAddresPostalCode", qbInvoice.BillingAddresPostalCode);
      
        Database.PutParameter(dbCommand,"?BillingAddressCountry", qbInvoice.BillingAddressCountry);
      
        Database.PutParameter(dbCommand,"?BillingAddressNote", qbInvoice.BillingAddressNote);
      
        Database.PutParameter(dbCommand,"?ShipAddressAddr1", qbInvoice.ShipAddressAddr1);
      
        Database.PutParameter(dbCommand,"?ShipAddressAddr2", qbInvoice.ShipAddressAddr2);
      
        Database.PutParameter(dbCommand,"?ShipAddressAddr3", qbInvoice.ShipAddressAddr3);
      
        Database.PutParameter(dbCommand,"?ShipAddressAddr4", qbInvoice.ShipAddressAddr4);
      
        Database.PutParameter(dbCommand,"?ShipAddressAddr5", qbInvoice.ShipAddressAddr5);
      
        Database.PutParameter(dbCommand,"?ShipAddressCity", qbInvoice.ShipAddressCity);
      
        Database.PutParameter(dbCommand,"?ShipAddressState", qbInvoice.ShipAddressState);
      
        Database.PutParameter(dbCommand,"?ShipAddressPostalCode", qbInvoice.ShipAddressPostalCode);
      
        Database.PutParameter(dbCommand,"?ShipAddressCountry", qbInvoice.ShipAddressCountry);
      
        Database.PutParameter(dbCommand,"?ShipAddressNote", qbInvoice.ShipAddressNote);
      
        Database.PutParameter(dbCommand,"?QbInvoiceTermListId", qbInvoice.QbInvoiceTermListId);
      
        Database.PutParameter(dbCommand,"?Memo", qbInvoice.Memo);
      
        Database.PutParameter(dbCommand,"?ItemSalesTaxRef", qbInvoice.ItemSalesTaxRef);
      
        Database.PutParameter(dbCommand,"?QbSalesRepRefListId", qbInvoice.QbSalesRepRefListId);
      
        Database.PutParameter(dbCommand,"?SubTotalAmount", qbInvoice.SubTotalAmount);
      
        Database.PutParameter(dbCommand,"?TaxAmount", qbInvoice.TaxAmount);
      
        Database.PutParameter(dbCommand,"?TotalAmount", qbInvoice.TotalAmount);
      
        Database.PutParameter(dbCommand,"?IsVoid", qbInvoice.IsVoid);
      
        Database.PutParameter(dbCommand,"?IsPending", qbInvoice.IsPending);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        qbInvoice.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(QbInvoice qbInvoice)
      {
        Insert(qbInvoice, null);
      }


      public static void Insert(List<QbInvoice>  qbInvoiceList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbInvoice qbInvoice in  qbInvoiceList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?DumptedInvoiceId", qbInvoice.DumptedInvoiceId);
      
        Database.PutParameter(dbCommand,"?DumpWorkTransactionId", qbInvoice.DumpWorkTransactionId);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", qbInvoice.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?CustomerRefListId", qbInvoice.CustomerRefListId);
      
        Database.PutParameter(dbCommand,"?ProcessedDate", qbInvoice.ProcessedDate);
      
        Database.PutParameter(dbCommand,"?ProcessedByEmployeeId", qbInvoice.ProcessedByEmployeeId);
      
        Database.PutParameter(dbCommand,"?CreatedDate", qbInvoice.CreatedDate);
      
        Database.PutParameter(dbCommand,"?TxnID", qbInvoice.TxnID);
      
        Database.PutParameter(dbCommand,"?TimeCreatedInQb", qbInvoice.TimeCreatedInQb);
      
        Database.PutParameter(dbCommand,"?TimeModifiedInQb", qbInvoice.TimeModifiedInQb);
      
        Database.PutParameter(dbCommand,"?TxnNumber", qbInvoice.TxnNumber);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbInvoice.EditSequence);
      
        Database.PutParameter(dbCommand,"?QbClassListId", qbInvoice.QbClassListId);
      
        Database.PutParameter(dbCommand,"?QbAccountListId", qbInvoice.QbAccountListId);
      
        Database.PutParameter(dbCommand,"?QbTemplateListId", qbInvoice.QbTemplateListId);
      
        Database.PutParameter(dbCommand,"?TxnDate", qbInvoice.TxnDate);
      
        Database.PutParameter(dbCommand,"?RefNumber", qbInvoice.RefNumber);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr1", qbInvoice.BillingAddressAddr1);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr2", qbInvoice.BillingAddressAddr2);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr3", qbInvoice.BillingAddressAddr3);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr4", qbInvoice.BillingAddressAddr4);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr5", qbInvoice.BillingAddressAddr5);
      
        Database.PutParameter(dbCommand,"?BillingAddressCity", qbInvoice.BillingAddressCity);
      
        Database.PutParameter(dbCommand,"?BillingAddresState", qbInvoice.BillingAddresState);
      
        Database.PutParameter(dbCommand,"?BillingAddresPostalCode", qbInvoice.BillingAddresPostalCode);
      
        Database.PutParameter(dbCommand,"?BillingAddressCountry", qbInvoice.BillingAddressCountry);
      
        Database.PutParameter(dbCommand,"?BillingAddressNote", qbInvoice.BillingAddressNote);
      
        Database.PutParameter(dbCommand,"?ShipAddressAddr1", qbInvoice.ShipAddressAddr1);
      
        Database.PutParameter(dbCommand,"?ShipAddressAddr2", qbInvoice.ShipAddressAddr2);
      
        Database.PutParameter(dbCommand,"?ShipAddressAddr3", qbInvoice.ShipAddressAddr3);
      
        Database.PutParameter(dbCommand,"?ShipAddressAddr4", qbInvoice.ShipAddressAddr4);
      
        Database.PutParameter(dbCommand,"?ShipAddressAddr5", qbInvoice.ShipAddressAddr5);
      
        Database.PutParameter(dbCommand,"?ShipAddressCity", qbInvoice.ShipAddressCity);
      
        Database.PutParameter(dbCommand,"?ShipAddressState", qbInvoice.ShipAddressState);
      
        Database.PutParameter(dbCommand,"?ShipAddressPostalCode", qbInvoice.ShipAddressPostalCode);
      
        Database.PutParameter(dbCommand,"?ShipAddressCountry", qbInvoice.ShipAddressCountry);
      
        Database.PutParameter(dbCommand,"?ShipAddressNote", qbInvoice.ShipAddressNote);
      
        Database.PutParameter(dbCommand,"?QbInvoiceTermListId", qbInvoice.QbInvoiceTermListId);
      
        Database.PutParameter(dbCommand,"?Memo", qbInvoice.Memo);
      
        Database.PutParameter(dbCommand,"?ItemSalesTaxRef", qbInvoice.ItemSalesTaxRef);
      
        Database.PutParameter(dbCommand,"?QbSalesRepRefListId", qbInvoice.QbSalesRepRefListId);
      
        Database.PutParameter(dbCommand,"?SubTotalAmount", qbInvoice.SubTotalAmount);
      
        Database.PutParameter(dbCommand,"?TaxAmount", qbInvoice.TaxAmount);
      
        Database.PutParameter(dbCommand,"?TotalAmount", qbInvoice.TotalAmount);
      
        Database.PutParameter(dbCommand,"?IsVoid", qbInvoice.IsVoid);
      
        Database.PutParameter(dbCommand,"?IsPending", qbInvoice.IsPending);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?DumptedInvoiceId",qbInvoice.DumptedInvoiceId);
      
        Database.UpdateParameter(dbCommand,"?DumpWorkTransactionId",qbInvoice.DumpWorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"?QbCustomerId",qbInvoice.QbCustomerId);
      
        Database.UpdateParameter(dbCommand,"?CustomerRefListId",qbInvoice.CustomerRefListId);
      
        Database.UpdateParameter(dbCommand,"?ProcessedDate",qbInvoice.ProcessedDate);
      
        Database.UpdateParameter(dbCommand,"?ProcessedByEmployeeId",qbInvoice.ProcessedByEmployeeId);
      
        Database.UpdateParameter(dbCommand,"?CreatedDate",qbInvoice.CreatedDate);
      
        Database.UpdateParameter(dbCommand,"?TxnID",qbInvoice.TxnID);
      
        Database.UpdateParameter(dbCommand,"?TimeCreatedInQb",qbInvoice.TimeCreatedInQb);
      
        Database.UpdateParameter(dbCommand,"?TimeModifiedInQb",qbInvoice.TimeModifiedInQb);
      
        Database.UpdateParameter(dbCommand,"?TxnNumber",qbInvoice.TxnNumber);
      
        Database.UpdateParameter(dbCommand,"?EditSequence",qbInvoice.EditSequence);
      
        Database.UpdateParameter(dbCommand,"?QbClassListId",qbInvoice.QbClassListId);
      
        Database.UpdateParameter(dbCommand,"?QbAccountListId",qbInvoice.QbAccountListId);
      
        Database.UpdateParameter(dbCommand,"?QbTemplateListId",qbInvoice.QbTemplateListId);
      
        Database.UpdateParameter(dbCommand,"?TxnDate",qbInvoice.TxnDate);
      
        Database.UpdateParameter(dbCommand,"?RefNumber",qbInvoice.RefNumber);
      
        Database.UpdateParameter(dbCommand,"?BillingAddressAddr1",qbInvoice.BillingAddressAddr1);
      
        Database.UpdateParameter(dbCommand,"?BillingAddressAddr2",qbInvoice.BillingAddressAddr2);
      
        Database.UpdateParameter(dbCommand,"?BillingAddressAddr3",qbInvoice.BillingAddressAddr3);
      
        Database.UpdateParameter(dbCommand,"?BillingAddressAddr4",qbInvoice.BillingAddressAddr4);
      
        Database.UpdateParameter(dbCommand,"?BillingAddressAddr5",qbInvoice.BillingAddressAddr5);
      
        Database.UpdateParameter(dbCommand,"?BillingAddressCity",qbInvoice.BillingAddressCity);
      
        Database.UpdateParameter(dbCommand,"?BillingAddresState",qbInvoice.BillingAddresState);
      
        Database.UpdateParameter(dbCommand,"?BillingAddresPostalCode",qbInvoice.BillingAddresPostalCode);
      
        Database.UpdateParameter(dbCommand,"?BillingAddressCountry",qbInvoice.BillingAddressCountry);
      
        Database.UpdateParameter(dbCommand,"?BillingAddressNote",qbInvoice.BillingAddressNote);
      
        Database.UpdateParameter(dbCommand,"?ShipAddressAddr1",qbInvoice.ShipAddressAddr1);
      
        Database.UpdateParameter(dbCommand,"?ShipAddressAddr2",qbInvoice.ShipAddressAddr2);
      
        Database.UpdateParameter(dbCommand,"?ShipAddressAddr3",qbInvoice.ShipAddressAddr3);
      
        Database.UpdateParameter(dbCommand,"?ShipAddressAddr4",qbInvoice.ShipAddressAddr4);
      
        Database.UpdateParameter(dbCommand,"?ShipAddressAddr5",qbInvoice.ShipAddressAddr5);
      
        Database.UpdateParameter(dbCommand,"?ShipAddressCity",qbInvoice.ShipAddressCity);
      
        Database.UpdateParameter(dbCommand,"?ShipAddressState",qbInvoice.ShipAddressState);
      
        Database.UpdateParameter(dbCommand,"?ShipAddressPostalCode",qbInvoice.ShipAddressPostalCode);
      
        Database.UpdateParameter(dbCommand,"?ShipAddressCountry",qbInvoice.ShipAddressCountry);
      
        Database.UpdateParameter(dbCommand,"?ShipAddressNote",qbInvoice.ShipAddressNote);
      
        Database.UpdateParameter(dbCommand,"?QbInvoiceTermListId",qbInvoice.QbInvoiceTermListId);
      
        Database.UpdateParameter(dbCommand,"?Memo",qbInvoice.Memo);
      
        Database.UpdateParameter(dbCommand,"?ItemSalesTaxRef",qbInvoice.ItemSalesTaxRef);
      
        Database.UpdateParameter(dbCommand,"?QbSalesRepRefListId",qbInvoice.QbSalesRepRefListId);
      
        Database.UpdateParameter(dbCommand,"?SubTotalAmount",qbInvoice.SubTotalAmount);
      
        Database.UpdateParameter(dbCommand,"?TaxAmount",qbInvoice.TaxAmount);
      
        Database.UpdateParameter(dbCommand,"?TotalAmount",qbInvoice.TotalAmount);
      
        Database.UpdateParameter(dbCommand,"?IsVoid",qbInvoice.IsVoid);
      
        Database.UpdateParameter(dbCommand,"?IsPending",qbInvoice.IsPending);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        qbInvoice.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<QbInvoice>  qbInvoiceList)
      {
        Insert(qbInvoiceList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbInvoice Set "
      
        + " DumptedInvoiceId = ?DumptedInvoiceId, "
      
        + " DumpWorkTransactionId = ?DumpWorkTransactionId, "
      
        + " QbCustomerId = ?QbCustomerId, "
      
        + " CustomerRefListId = ?CustomerRefListId, "
      
        + " ProcessedDate = ?ProcessedDate, "
      
        + " ProcessedByEmployeeId = ?ProcessedByEmployeeId, "
      
        + " CreatedDate = ?CreatedDate, "
      
        + " TxnID = ?TxnID, "
      
        + " TimeCreatedInQb = ?TimeCreatedInQb, "
      
        + " TimeModifiedInQb = ?TimeModifiedInQb, "
      
        + " TxnNumber = ?TxnNumber, "
      
        + " EditSequence = ?EditSequence, "
      
        + " QbClassListId = ?QbClassListId, "
      
        + " QbAccountListId = ?QbAccountListId, "
      
        + " QbTemplateListId = ?QbTemplateListId, "
      
        + " TxnDate = ?TxnDate, "
      
        + " RefNumber = ?RefNumber, "
      
        + " BillingAddressAddr1 = ?BillingAddressAddr1, "
      
        + " BillingAddressAddr2 = ?BillingAddressAddr2, "
      
        + " BillingAddressAddr3 = ?BillingAddressAddr3, "
      
        + " BillingAddressAddr4 = ?BillingAddressAddr4, "
      
        + " BillingAddressAddr5 = ?BillingAddressAddr5, "
      
        + " BillingAddressCity = ?BillingAddressCity, "
      
        + " BillingAddresState = ?BillingAddresState, "
      
        + " BillingAddresPostalCode = ?BillingAddresPostalCode, "
      
        + " BillingAddressCountry = ?BillingAddressCountry, "
      
        + " BillingAddressNote = ?BillingAddressNote, "
      
        + " ShipAddressAddr1 = ?ShipAddressAddr1, "
      
        + " ShipAddressAddr2 = ?ShipAddressAddr2, "
      
        + " ShipAddressAddr3 = ?ShipAddressAddr3, "
      
        + " ShipAddressAddr4 = ?ShipAddressAddr4, "
      
        + " ShipAddressAddr5 = ?ShipAddressAddr5, "
      
        + " ShipAddressCity = ?ShipAddressCity, "
      
        + " ShipAddressState = ?ShipAddressState, "
      
        + " ShipAddressPostalCode = ?ShipAddressPostalCode, "
      
        + " ShipAddressCountry = ?ShipAddressCountry, "
      
        + " ShipAddressNote = ?ShipAddressNote, "
      
        + " QbInvoiceTermListId = ?QbInvoiceTermListId, "
      
        + " Memo = ?Memo, "
      
        + " ItemSalesTaxRef = ?ItemSalesTaxRef, "
      
        + " QbSalesRepRefListId = ?QbSalesRepRefListId, "
      
        + " SubTotalAmount = ?SubTotalAmount, "
      
        + " TaxAmount = ?TaxAmount, "
      
        + " TotalAmount = ?TotalAmount, "
      
        + " IsVoid = ?IsVoid, "
      
        + " IsPending = ?IsPending "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(QbInvoice qbInvoice, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", qbInvoice.ID);
      
        Database.PutParameter(dbCommand,"?DumptedInvoiceId", qbInvoice.DumptedInvoiceId);
      
        Database.PutParameter(dbCommand,"?DumpWorkTransactionId", qbInvoice.DumpWorkTransactionId);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", qbInvoice.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?CustomerRefListId", qbInvoice.CustomerRefListId);
      
        Database.PutParameter(dbCommand,"?ProcessedDate", qbInvoice.ProcessedDate);
      
        Database.PutParameter(dbCommand,"?ProcessedByEmployeeId", qbInvoice.ProcessedByEmployeeId);
      
        Database.PutParameter(dbCommand,"?CreatedDate", qbInvoice.CreatedDate);
      
        Database.PutParameter(dbCommand,"?TxnID", qbInvoice.TxnID);
      
        Database.PutParameter(dbCommand,"?TimeCreatedInQb", qbInvoice.TimeCreatedInQb);
      
        Database.PutParameter(dbCommand,"?TimeModifiedInQb", qbInvoice.TimeModifiedInQb);
      
        Database.PutParameter(dbCommand,"?TxnNumber", qbInvoice.TxnNumber);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbInvoice.EditSequence);
      
        Database.PutParameter(dbCommand,"?QbClassListId", qbInvoice.QbClassListId);
      
        Database.PutParameter(dbCommand,"?QbAccountListId", qbInvoice.QbAccountListId);
      
        Database.PutParameter(dbCommand,"?QbTemplateListId", qbInvoice.QbTemplateListId);
      
        Database.PutParameter(dbCommand,"?TxnDate", qbInvoice.TxnDate);
      
        Database.PutParameter(dbCommand,"?RefNumber", qbInvoice.RefNumber);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr1", qbInvoice.BillingAddressAddr1);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr2", qbInvoice.BillingAddressAddr2);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr3", qbInvoice.BillingAddressAddr3);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr4", qbInvoice.BillingAddressAddr4);
      
        Database.PutParameter(dbCommand,"?BillingAddressAddr5", qbInvoice.BillingAddressAddr5);
      
        Database.PutParameter(dbCommand,"?BillingAddressCity", qbInvoice.BillingAddressCity);
      
        Database.PutParameter(dbCommand,"?BillingAddresState", qbInvoice.BillingAddresState);
      
        Database.PutParameter(dbCommand,"?BillingAddresPostalCode", qbInvoice.BillingAddresPostalCode);
      
        Database.PutParameter(dbCommand,"?BillingAddressCountry", qbInvoice.BillingAddressCountry);
      
        Database.PutParameter(dbCommand,"?BillingAddressNote", qbInvoice.BillingAddressNote);
      
        Database.PutParameter(dbCommand,"?ShipAddressAddr1", qbInvoice.ShipAddressAddr1);
      
        Database.PutParameter(dbCommand,"?ShipAddressAddr2", qbInvoice.ShipAddressAddr2);
      
        Database.PutParameter(dbCommand,"?ShipAddressAddr3", qbInvoice.ShipAddressAddr3);
      
        Database.PutParameter(dbCommand,"?ShipAddressAddr4", qbInvoice.ShipAddressAddr4);
      
        Database.PutParameter(dbCommand,"?ShipAddressAddr5", qbInvoice.ShipAddressAddr5);
      
        Database.PutParameter(dbCommand,"?ShipAddressCity", qbInvoice.ShipAddressCity);
      
        Database.PutParameter(dbCommand,"?ShipAddressState", qbInvoice.ShipAddressState);
      
        Database.PutParameter(dbCommand,"?ShipAddressPostalCode", qbInvoice.ShipAddressPostalCode);
      
        Database.PutParameter(dbCommand,"?ShipAddressCountry", qbInvoice.ShipAddressCountry);
      
        Database.PutParameter(dbCommand,"?ShipAddressNote", qbInvoice.ShipAddressNote);
      
        Database.PutParameter(dbCommand,"?QbInvoiceTermListId", qbInvoice.QbInvoiceTermListId);
      
        Database.PutParameter(dbCommand,"?Memo", qbInvoice.Memo);
      
        Database.PutParameter(dbCommand,"?ItemSalesTaxRef", qbInvoice.ItemSalesTaxRef);
      
        Database.PutParameter(dbCommand,"?QbSalesRepRefListId", qbInvoice.QbSalesRepRefListId);
      
        Database.PutParameter(dbCommand,"?SubTotalAmount", qbInvoice.SubTotalAmount);
      
        Database.PutParameter(dbCommand,"?TaxAmount", qbInvoice.TaxAmount);
      
        Database.PutParameter(dbCommand,"?TotalAmount", qbInvoice.TotalAmount);
      
        Database.PutParameter(dbCommand,"?IsVoid", qbInvoice.IsVoid);
      
        Database.PutParameter(dbCommand,"?IsPending", qbInvoice.IsPending);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbInvoice qbInvoice)
      {
        Update(qbInvoice, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " DumptedInvoiceId, "
      
        + " DumpWorkTransactionId, "
      
        + " QbCustomerId, "
      
        + " CustomerRefListId, "
      
        + " ProcessedDate, "
      
        + " ProcessedByEmployeeId, "
      
        + " CreatedDate, "
      
        + " TxnID, "
      
        + " TimeCreatedInQb, "
      
        + " TimeModifiedInQb, "
      
        + " TxnNumber, "
      
        + " EditSequence, "
      
        + " QbClassListId, "
      
        + " QbAccountListId, "
      
        + " QbTemplateListId, "
      
        + " TxnDate, "
      
        + " RefNumber, "
      
        + " BillingAddressAddr1, "
      
        + " BillingAddressAddr2, "
      
        + " BillingAddressAddr3, "
      
        + " BillingAddressAddr4, "
      
        + " BillingAddressAddr5, "
      
        + " BillingAddressCity, "
      
        + " BillingAddresState, "
      
        + " BillingAddresPostalCode, "
      
        + " BillingAddressCountry, "
      
        + " BillingAddressNote, "
      
        + " ShipAddressAddr1, "
      
        + " ShipAddressAddr2, "
      
        + " ShipAddressAddr3, "
      
        + " ShipAddressAddr4, "
      
        + " ShipAddressAddr5, "
      
        + " ShipAddressCity, "
      
        + " ShipAddressState, "
      
        + " ShipAddressPostalCode, "
      
        + " ShipAddressCountry, "
      
        + " ShipAddressNote, "
      
        + " QbInvoiceTermListId, "
      
        + " Memo, "
      
        + " ItemSalesTaxRef, "
      
        + " QbSalesRepRefListId, "
      
        + " SubTotalAmount, "
      
        + " TaxAmount, "
      
        + " TotalAmount, "
      
        + " IsVoid, "
      
        + " IsPending "
      

      + " From QbInvoice "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static QbInvoice FindByPrimaryKey(
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
      throw new DataNotFoundException("QbInvoice not found, search by primary key");

      }

      public static QbInvoice FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbInvoice qbInvoice, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",qbInvoice.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbInvoice qbInvoice)
      {
      return Exists(qbInvoice, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbInvoice limit 1";

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

      public static QbInvoice Load(IDataReader dataReader, int offset)
      {
      QbInvoice qbInvoice = new QbInvoice();

      qbInvoice.ID = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            qbInvoice.DumptedInvoiceId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            qbInvoice.DumpWorkTransactionId = dataReader.GetInt32(2 + offset);
          qbInvoice.QbCustomerId = dataReader.GetInt32(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            qbInvoice.CustomerRefListId = dataReader.GetString(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            qbInvoice.ProcessedDate = dataReader.GetDateTime(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            qbInvoice.ProcessedByEmployeeId = dataReader.GetInt32(6 + offset);
          qbInvoice.CreatedDate = dataReader.GetDateTime(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            qbInvoice.TxnID = dataReader.GetString(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            qbInvoice.TimeCreatedInQb = dataReader.GetDateTime(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            qbInvoice.TimeModifiedInQb = dataReader.GetDateTime(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            qbInvoice.TxnNumber = dataReader.GetInt32(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            qbInvoice.EditSequence = dataReader.GetString(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            qbInvoice.QbClassListId = dataReader.GetString(13 + offset);
          
            if(!dataReader.IsDBNull(14 + offset))
            qbInvoice.QbAccountListId = dataReader.GetString(14 + offset);
          
            if(!dataReader.IsDBNull(15 + offset))
            qbInvoice.QbTemplateListId = dataReader.GetString(15 + offset);
          
            if(!dataReader.IsDBNull(16 + offset))
            qbInvoice.TxnDate = dataReader.GetDateTime(16 + offset);
          
            if(!dataReader.IsDBNull(17 + offset))
            qbInvoice.RefNumber = dataReader.GetString(17 + offset);
          
            if(!dataReader.IsDBNull(18 + offset))
            qbInvoice.BillingAddressAddr1 = dataReader.GetString(18 + offset);
          
            if(!dataReader.IsDBNull(19 + offset))
            qbInvoice.BillingAddressAddr2 = dataReader.GetString(19 + offset);
          
            if(!dataReader.IsDBNull(20 + offset))
            qbInvoice.BillingAddressAddr3 = dataReader.GetString(20 + offset);
          
            if(!dataReader.IsDBNull(21 + offset))
            qbInvoice.BillingAddressAddr4 = dataReader.GetString(21 + offset);
          
            if(!dataReader.IsDBNull(22 + offset))
            qbInvoice.BillingAddressAddr5 = dataReader.GetString(22 + offset);
          
            if(!dataReader.IsDBNull(23 + offset))
            qbInvoice.BillingAddressCity = dataReader.GetString(23 + offset);
          
            if(!dataReader.IsDBNull(24 + offset))
            qbInvoice.BillingAddresState = dataReader.GetString(24 + offset);
          
            if(!dataReader.IsDBNull(25 + offset))
            qbInvoice.BillingAddresPostalCode = dataReader.GetString(25 + offset);
          
            if(!dataReader.IsDBNull(26 + offset))
            qbInvoice.BillingAddressCountry = dataReader.GetString(26 + offset);
          
            if(!dataReader.IsDBNull(27 + offset))
            qbInvoice.BillingAddressNote = dataReader.GetString(27 + offset);
          
            if(!dataReader.IsDBNull(28 + offset))
            qbInvoice.ShipAddressAddr1 = dataReader.GetString(28 + offset);
          
            if(!dataReader.IsDBNull(29 + offset))
            qbInvoice.ShipAddressAddr2 = dataReader.GetString(29 + offset);
          
            if(!dataReader.IsDBNull(30 + offset))
            qbInvoice.ShipAddressAddr3 = dataReader.GetString(30 + offset);
          
            if(!dataReader.IsDBNull(31 + offset))
            qbInvoice.ShipAddressAddr4 = dataReader.GetString(31 + offset);
          
            if(!dataReader.IsDBNull(32 + offset))
            qbInvoice.ShipAddressAddr5 = dataReader.GetString(32 + offset);
          
            if(!dataReader.IsDBNull(33 + offset))
            qbInvoice.ShipAddressCity = dataReader.GetString(33 + offset);
          
            if(!dataReader.IsDBNull(34 + offset))
            qbInvoice.ShipAddressState = dataReader.GetString(34 + offset);
          
            if(!dataReader.IsDBNull(35 + offset))
            qbInvoice.ShipAddressPostalCode = dataReader.GetString(35 + offset);
          
            if(!dataReader.IsDBNull(36 + offset))
            qbInvoice.ShipAddressCountry = dataReader.GetString(36 + offset);
          
            if(!dataReader.IsDBNull(37 + offset))
            qbInvoice.ShipAddressNote = dataReader.GetString(37 + offset);
          
            if(!dataReader.IsDBNull(38 + offset))
            qbInvoice.QbInvoiceTermListId = dataReader.GetString(38 + offset);
          
            if(!dataReader.IsDBNull(39 + offset))
            qbInvoice.Memo = dataReader.GetString(39 + offset);
          
            if(!dataReader.IsDBNull(40 + offset))
            qbInvoice.ItemSalesTaxRef = dataReader.GetString(40 + offset);
          
            if(!dataReader.IsDBNull(41 + offset))
            qbInvoice.QbSalesRepRefListId = dataReader.GetString(41 + offset);
          qbInvoice.SubTotalAmount = dataReader.GetDecimal(42 + offset);
          qbInvoice.TaxAmount = dataReader.GetDecimal(43 + offset);
          qbInvoice.TotalAmount = dataReader.GetDecimal(44 + offset);
          
            if(!dataReader.IsDBNull(45 + offset))
            qbInvoice.IsVoid = dataReader.GetBoolean(45 + offset);
          qbInvoice.IsPending = dataReader.GetBoolean(46 + offset);
          

      return qbInvoice;
      }

      public static QbInvoice Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbInvoice "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(QbInvoice qbInvoice, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", qbInvoice.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbInvoice qbInvoice)
      {
        Delete(qbInvoice, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbInvoice ";

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
      
        + " DumptedInvoiceId, "
      
        + " DumpWorkTransactionId, "
      
        + " QbCustomerId, "
      
        + " CustomerRefListId, "
      
        + " ProcessedDate, "
      
        + " ProcessedByEmployeeId, "
      
        + " CreatedDate, "
      
        + " TxnID, "
      
        + " TimeCreatedInQb, "
      
        + " TimeModifiedInQb, "
      
        + " TxnNumber, "
      
        + " EditSequence, "
      
        + " QbClassListId, "
      
        + " QbAccountListId, "
      
        + " QbTemplateListId, "
      
        + " TxnDate, "
      
        + " RefNumber, "
      
        + " BillingAddressAddr1, "
      
        + " BillingAddressAddr2, "
      
        + " BillingAddressAddr3, "
      
        + " BillingAddressAddr4, "
      
        + " BillingAddressAddr5, "
      
        + " BillingAddressCity, "
      
        + " BillingAddresState, "
      
        + " BillingAddresPostalCode, "
      
        + " BillingAddressCountry, "
      
        + " BillingAddressNote, "
      
        + " ShipAddressAddr1, "
      
        + " ShipAddressAddr2, "
      
        + " ShipAddressAddr3, "
      
        + " ShipAddressAddr4, "
      
        + " ShipAddressAddr5, "
      
        + " ShipAddressCity, "
      
        + " ShipAddressState, "
      
        + " ShipAddressPostalCode, "
      
        + " ShipAddressCountry, "
      
        + " ShipAddressNote, "
      
        + " QbInvoiceTermListId, "
      
        + " Memo, "
      
        + " ItemSalesTaxRef, "
      
        + " QbSalesRepRefListId, "
      
        + " SubTotalAmount, "
      
        + " TaxAmount, "
      
        + " TotalAmount, "
      
        + " IsVoid, "
      
        + " IsPending "
      

      + " From QbInvoice ";
      public static List<QbInvoice> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbInvoice> rv = new List<QbInvoice>();

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

      public static List<QbInvoice> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbInvoice> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbInvoice obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && DumptedInvoiceId == obj.DumptedInvoiceId && DumpWorkTransactionId == obj.DumpWorkTransactionId && QbCustomerId == obj.QbCustomerId && CustomerRefListId == obj.CustomerRefListId && ProcessedDate == obj.ProcessedDate && ProcessedByEmployeeId == obj.ProcessedByEmployeeId && CreatedDate == obj.CreatedDate && TxnID == obj.TxnID && TimeCreatedInQb == obj.TimeCreatedInQb && TimeModifiedInQb == obj.TimeModifiedInQb && TxnNumber == obj.TxnNumber && EditSequence == obj.EditSequence && QbClassListId == obj.QbClassListId && QbAccountListId == obj.QbAccountListId && QbTemplateListId == obj.QbTemplateListId && TxnDate == obj.TxnDate && RefNumber == obj.RefNumber && BillingAddressAddr1 == obj.BillingAddressAddr1 && BillingAddressAddr2 == obj.BillingAddressAddr2 && BillingAddressAddr3 == obj.BillingAddressAddr3 && BillingAddressAddr4 == obj.BillingAddressAddr4 && BillingAddressAddr5 == obj.BillingAddressAddr5 && BillingAddressCity == obj.BillingAddressCity && BillingAddresState == obj.BillingAddresState && BillingAddresPostalCode == obj.BillingAddresPostalCode && BillingAddressCountry == obj.BillingAddressCountry && BillingAddressNote == obj.BillingAddressNote && ShipAddressAddr1 == obj.ShipAddressAddr1 && ShipAddressAddr2 == obj.ShipAddressAddr2 && ShipAddressAddr3 == obj.ShipAddressAddr3 && ShipAddressAddr4 == obj.ShipAddressAddr4 && ShipAddressAddr5 == obj.ShipAddressAddr5 && ShipAddressCity == obj.ShipAddressCity && ShipAddressState == obj.ShipAddressState && ShipAddressPostalCode == obj.ShipAddressPostalCode && ShipAddressCountry == obj.ShipAddressCountry && ShipAddressNote == obj.ShipAddressNote && QbInvoiceTermListId == obj.QbInvoiceTermListId && Memo == obj.Memo && ItemSalesTaxRef == obj.ItemSalesTaxRef && QbSalesRepRefListId == obj.QbSalesRepRefListId && SubTotalAmount == obj.SubTotalAmount && TaxAmount == obj.TaxAmount && TotalAmount == obj.TotalAmount && IsVoid == obj.IsVoid && IsPending == obj.IsPending;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbInvoice> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbInvoice));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbInvoice item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbInvoice>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbInvoice));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbInvoice> itemsList
      = new List<QbInvoice>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbInvoice)
      itemsList.Add(deserializedObject as QbInvoice);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int? m_dumptedInvoiceId;
      
        protected int? m_dumpWorkTransactionId;
      
        protected int m_qbCustomerId;
      
        protected String m_customerRefListId;
      
        protected DateTime? m_processedDate;
      
        protected int? m_processedByEmployeeId;
      
        protected DateTime m_createdDate;
      
        protected String m_txnID;
      
        protected DateTime? m_timeCreatedInQb;
      
        protected DateTime? m_timeModifiedInQb;
      
        protected int? m_txnNumber;
      
        protected String m_editSequence;
      
        protected String m_qbClassListId;
      
        protected String m_qbAccountListId;
      
        protected String m_qbTemplateListId;
      
        protected DateTime? m_txnDate;
      
        protected String m_refNumber;
      
        protected String m_billingAddressAddr1;
      
        protected String m_billingAddressAddr2;
      
        protected String m_billingAddressAddr3;
      
        protected String m_billingAddressAddr4;
      
        protected String m_billingAddressAddr5;
      
        protected String m_billingAddressCity;
      
        protected String m_billingAddresState;
      
        protected String m_billingAddresPostalCode;
      
        protected String m_billingAddressCountry;
      
        protected String m_billingAddressNote;
      
        protected String m_shipAddressAddr1;
      
        protected String m_shipAddressAddr2;
      
        protected String m_shipAddressAddr3;
      
        protected String m_shipAddressAddr4;
      
        protected String m_shipAddressAddr5;
      
        protected String m_shipAddressCity;
      
        protected String m_shipAddressState;
      
        protected String m_shipAddressPostalCode;
      
        protected String m_shipAddressCountry;
      
        protected String m_shipAddressNote;
      
        protected String m_qbInvoiceTermListId;
      
        protected String m_memo;
      
        protected String m_itemSalesTaxRef;
      
        protected String m_qbSalesRepRefListId;
      
        protected decimal m_subTotalAmount;
      
        protected decimal m_taxAmount;
      
        protected decimal m_totalAmount;
      
        protected bool m_isVoid;
      
        protected bool m_isPending;
      
      #endregion

      #region Constructors
      public QbInvoice(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public QbInvoice(
        int 
          iD,int? 
          dumptedInvoiceId,int? 
          dumpWorkTransactionId,int 
          qbCustomerId,String 
          customerRefListId,DateTime? 
          processedDate,int? 
          processedByEmployeeId,DateTime 
          createdDate,String 
          txnID,DateTime? 
          timeCreatedInQb,DateTime? 
          timeModifiedInQb,int? 
          txnNumber,String 
          editSequence,String 
          qbClassListId,String 
          qbAccountListId,String 
          qbTemplateListId,DateTime? 
          txnDate,String 
          refNumber,String 
          billingAddressAddr1,String 
          billingAddressAddr2,String 
          billingAddressAddr3,String 
          billingAddressAddr4,String 
          billingAddressAddr5,String 
          billingAddressCity,String 
          billingAddresState,String 
          billingAddresPostalCode,String 
          billingAddressCountry,String 
          billingAddressNote,String 
          shipAddressAddr1,String 
          shipAddressAddr2,String 
          shipAddressAddr3,String 
          shipAddressAddr4,String 
          shipAddressAddr5,String 
          shipAddressCity,String 
          shipAddressState,String 
          shipAddressPostalCode,String 
          shipAddressCountry,String 
          shipAddressNote,String 
          qbInvoiceTermListId,String 
          memo,String 
          itemSalesTaxRef,String 
          qbSalesRepRefListId,decimal 
          subTotalAmount,decimal 
          taxAmount,decimal 
          totalAmount,bool 
          isVoid,bool 
          isPending
        ) : this()
        {
        
          m_iD = iD;
        
          m_dumptedInvoiceId = dumptedInvoiceId;
        
          m_dumpWorkTransactionId = dumpWorkTransactionId;
        
          m_qbCustomerId = qbCustomerId;
        
          m_customerRefListId = customerRefListId;
        
          m_processedDate = processedDate;
        
          m_processedByEmployeeId = processedByEmployeeId;
        
          m_createdDate = createdDate;
        
          m_txnID = txnID;
        
          m_timeCreatedInQb = timeCreatedInQb;
        
          m_timeModifiedInQb = timeModifiedInQb;
        
          m_txnNumber = txnNumber;
        
          m_editSequence = editSequence;
        
          m_qbClassListId = qbClassListId;
        
          m_qbAccountListId = qbAccountListId;
        
          m_qbTemplateListId = qbTemplateListId;
        
          m_txnDate = txnDate;
        
          m_refNumber = refNumber;
        
          m_billingAddressAddr1 = billingAddressAddr1;
        
          m_billingAddressAddr2 = billingAddressAddr2;
        
          m_billingAddressAddr3 = billingAddressAddr3;
        
          m_billingAddressAddr4 = billingAddressAddr4;
        
          m_billingAddressAddr5 = billingAddressAddr5;
        
          m_billingAddressCity = billingAddressCity;
        
          m_billingAddresState = billingAddresState;
        
          m_billingAddresPostalCode = billingAddresPostalCode;
        
          m_billingAddressCountry = billingAddressCountry;
        
          m_billingAddressNote = billingAddressNote;
        
          m_shipAddressAddr1 = shipAddressAddr1;
        
          m_shipAddressAddr2 = shipAddressAddr2;
        
          m_shipAddressAddr3 = shipAddressAddr3;
        
          m_shipAddressAddr4 = shipAddressAddr4;
        
          m_shipAddressAddr5 = shipAddressAddr5;
        
          m_shipAddressCity = shipAddressCity;
        
          m_shipAddressState = shipAddressState;
        
          m_shipAddressPostalCode = shipAddressPostalCode;
        
          m_shipAddressCountry = shipAddressCountry;
        
          m_shipAddressNote = shipAddressNote;
        
          m_qbInvoiceTermListId = qbInvoiceTermListId;
        
          m_memo = memo;
        
          m_itemSalesTaxRef = itemSalesTaxRef;
        
          m_qbSalesRepRefListId = qbSalesRepRefListId;
        
          m_subTotalAmount = subTotalAmount;
        
          m_taxAmount = taxAmount;
        
          m_totalAmount = totalAmount;
        
          m_isVoid = isVoid;
        
          m_isPending = isPending;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int? DumptedInvoiceId
        {
        get { return m_dumptedInvoiceId;}
        set { m_dumptedInvoiceId = value; }
        }
      
        [XmlElement]
        public int? DumpWorkTransactionId
        {
        get { return m_dumpWorkTransactionId;}
        set { m_dumpWorkTransactionId = value; }
        }
      
        [XmlElement]
        public int QbCustomerId
        {
        get { return m_qbCustomerId;}
        set { m_qbCustomerId = value; }
        }
      
        [XmlElement]
        public String CustomerRefListId
        {
        get { return m_customerRefListId;}
        set { m_customerRefListId = value; }
        }
      
        [XmlElement]
        public DateTime? ProcessedDate
        {
        get { return m_processedDate;}
        set { m_processedDate = value; }
        }
      
        [XmlElement]
        public int? ProcessedByEmployeeId
        {
        get { return m_processedByEmployeeId;}
        set { m_processedByEmployeeId = value; }
        }
      
        [XmlElement]
        public DateTime CreatedDate
        {
        get { return m_createdDate;}
        set { m_createdDate = value; }
        }
      
        [XmlElement]
        public String TxnID
        {
        get { return m_txnID;}
        set { m_txnID = value; }
        }
      
        [XmlElement]
        public DateTime? TimeCreatedInQb
        {
        get { return m_timeCreatedInQb;}
        set { m_timeCreatedInQb = value; }
        }
      
        [XmlElement]
        public DateTime? TimeModifiedInQb
        {
        get { return m_timeModifiedInQb;}
        set { m_timeModifiedInQb = value; }
        }
      
        [XmlElement]
        public int? TxnNumber
        {
        get { return m_txnNumber;}
        set { m_txnNumber = value; }
        }
      
        [XmlElement]
        public String EditSequence
        {
        get { return m_editSequence;}
        set { m_editSequence = value; }
        }
      
        [XmlElement]
        public String QbClassListId
        {
        get { return m_qbClassListId;}
        set { m_qbClassListId = value; }
        }
      
        [XmlElement]
        public String QbAccountListId
        {
        get { return m_qbAccountListId;}
        set { m_qbAccountListId = value; }
        }
      
        [XmlElement]
        public String QbTemplateListId
        {
        get { return m_qbTemplateListId;}
        set { m_qbTemplateListId = value; }
        }
      
        [XmlElement]
        public DateTime? TxnDate
        {
        get { return m_txnDate;}
        set { m_txnDate = value; }
        }
      
        [XmlElement]
        public String RefNumber
        {
        get { return m_refNumber;}
        set { m_refNumber = value; }
        }
      
        [XmlElement]
        public String BillingAddressAddr1
        {
        get { return m_billingAddressAddr1;}
        set { m_billingAddressAddr1 = value; }
        }
      
        [XmlElement]
        public String BillingAddressAddr2
        {
        get { return m_billingAddressAddr2;}
        set { m_billingAddressAddr2 = value; }
        }
      
        [XmlElement]
        public String BillingAddressAddr3
        {
        get { return m_billingAddressAddr3;}
        set { m_billingAddressAddr3 = value; }
        }
      
        [XmlElement]
        public String BillingAddressAddr4
        {
        get { return m_billingAddressAddr4;}
        set { m_billingAddressAddr4 = value; }
        }
      
        [XmlElement]
        public String BillingAddressAddr5
        {
        get { return m_billingAddressAddr5;}
        set { m_billingAddressAddr5 = value; }
        }
      
        [XmlElement]
        public String BillingAddressCity
        {
        get { return m_billingAddressCity;}
        set { m_billingAddressCity = value; }
        }
      
        [XmlElement]
        public String BillingAddresState
        {
        get { return m_billingAddresState;}
        set { m_billingAddresState = value; }
        }
      
        [XmlElement]
        public String BillingAddresPostalCode
        {
        get { return m_billingAddresPostalCode;}
        set { m_billingAddresPostalCode = value; }
        }
      
        [XmlElement]
        public String BillingAddressCountry
        {
        get { return m_billingAddressCountry;}
        set { m_billingAddressCountry = value; }
        }
      
        [XmlElement]
        public String BillingAddressNote
        {
        get { return m_billingAddressNote;}
        set { m_billingAddressNote = value; }
        }
      
        [XmlElement]
        public String ShipAddressAddr1
        {
        get { return m_shipAddressAddr1;}
        set { m_shipAddressAddr1 = value; }
        }
      
        [XmlElement]
        public String ShipAddressAddr2
        {
        get { return m_shipAddressAddr2;}
        set { m_shipAddressAddr2 = value; }
        }
      
        [XmlElement]
        public String ShipAddressAddr3
        {
        get { return m_shipAddressAddr3;}
        set { m_shipAddressAddr3 = value; }
        }
      
        [XmlElement]
        public String ShipAddressAddr4
        {
        get { return m_shipAddressAddr4;}
        set { m_shipAddressAddr4 = value; }
        }
      
        [XmlElement]
        public String ShipAddressAddr5
        {
        get { return m_shipAddressAddr5;}
        set { m_shipAddressAddr5 = value; }
        }
      
        [XmlElement]
        public String ShipAddressCity
        {
        get { return m_shipAddressCity;}
        set { m_shipAddressCity = value; }
        }
      
        [XmlElement]
        public String ShipAddressState
        {
        get { return m_shipAddressState;}
        set { m_shipAddressState = value; }
        }
      
        [XmlElement]
        public String ShipAddressPostalCode
        {
        get { return m_shipAddressPostalCode;}
        set { m_shipAddressPostalCode = value; }
        }
      
        [XmlElement]
        public String ShipAddressCountry
        {
        get { return m_shipAddressCountry;}
        set { m_shipAddressCountry = value; }
        }
      
        [XmlElement]
        public String ShipAddressNote
        {
        get { return m_shipAddressNote;}
        set { m_shipAddressNote = value; }
        }
      
        [XmlElement]
        public String QbInvoiceTermListId
        {
        get { return m_qbInvoiceTermListId;}
        set { m_qbInvoiceTermListId = value; }
        }
      
        [XmlElement]
        public String Memo
        {
        get { return m_memo;}
        set { m_memo = value; }
        }
      
        [XmlElement]
        public String ItemSalesTaxRef
        {
        get { return m_itemSalesTaxRef;}
        set { m_itemSalesTaxRef = value; }
        }
      
        [XmlElement]
        public String QbSalesRepRefListId
        {
        get { return m_qbSalesRepRefListId;}
        set { m_qbSalesRepRefListId = value; }
        }
      
        [XmlElement]
        public decimal SubTotalAmount
        {
        get { return m_subTotalAmount;}
        set { m_subTotalAmount = value; }
        }
      
        [XmlElement]
        public decimal TaxAmount
        {
        get { return m_taxAmount;}
        set { m_taxAmount = value; }
        }
      
        [XmlElement]
        public decimal TotalAmount
        {
        get { return m_totalAmount;}
        set { m_totalAmount = value; }
        }
      
        [XmlElement]
        public bool IsVoid
        {
        get { return m_isVoid;}
        set { m_isVoid = value; }
        }
      
        [XmlElement]
        public bool IsPending
        {
        get { return m_isPending;}
        set { m_isPending = value; }
        }
      

      public static int FieldsCount
      {
      get { return 47; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    