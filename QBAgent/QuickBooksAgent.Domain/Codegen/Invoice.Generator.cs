
    using System;
    using System.Data;
    using System.Collections.Generic;
    using QuickBooksAgent.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace QuickBooksAgent.Domain
      {


      public partial class Invoice
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Invoice] ( " +
      
        " InvoiceId, " +
        " QuickBooksTxnId, " +
        " EntityStateId, " +
        " EditSequence, " +
        " TimeCreated, " +
        " TimeModified, " +
        " TxnNumber, " +
        " CustomerId, " +
        " ARAccountId, " +
        " TxnDate, " +
        " RefNumber, " +
        " BillAddr1, " +
        " BillAddr2, " +
        " BillAddr3, " +
        " BillAddr4, " +
        " BillCity, " +
        " BillState, " +
        " BillPostalCode, " +
        " BillCountry, " +
        " ShipAddr1, " +
        " ShipAddr2, " +
        " ShipAddr3, " +
        " ShipAddr4, " +
        " ShipCity, " +
        " ShipState, " +
        " ShipPostalCode, " +
        " ShipCountry, " +
        " TermsId, " +
        " DueDate, " +
        " ShipDate, " +
        " Subtotal, " +
        " SalesTaxPercentage, " +
        " SalesTaxTotal, " +
        " AppliedAmount, " +
        " BalanceRemaining, " +
        " Memo, " +
        " IsPaid, " +
        " IsToBePrinted, " +
        " DiscountLineAmount, " +
        " DiscountLineRatePercent, " +
        " DiscountLineIsTaxable, " +
        " DiscountLineAccountId, " +
        " SalesTaxLineAmount, " +
        " SalesTaxLineRatePercent, " +
        " SalesTaxLineAccountId, " +
        " ShippingLineAmount, " +
        " ShippingLineAccountId, " +
        " IsCustomerTaxable, " +
        " TaxCalculationType " +
        ") Values (" +
      
        " @InvoiceId, " +
        " @QuickBooksTxnId, " +
        " @EntityStateId, " +
        " @EditSequence, " +
        " @TimeCreated, " +
        " @TimeModified, " +
        " @TxnNumber, " +
        " @CustomerId, " +
        " @ARAccountId, " +
        " @TxnDate, " +
        " @RefNumber, " +
        " @BillAddr1, " +
        " @BillAddr2, " +
        " @BillAddr3, " +
        " @BillAddr4, " +
        " @BillCity, " +
        " @BillState, " +
        " @BillPostalCode, " +
        " @BillCountry, " +
        " @ShipAddr1, " +
        " @ShipAddr2, " +
        " @ShipAddr3, " +
        " @ShipAddr4, " +
        " @ShipCity, " +
        " @ShipState, " +
        " @ShipPostalCode, " +
        " @ShipCountry, " +
        " @TermsId, " +
        " @DueDate, " +
        " @ShipDate, " +
        " @Subtotal, " +
        " @SalesTaxPercentage, " +
        " @SalesTaxTotal, " +
        " @AppliedAmount, " +
        " @BalanceRemaining, " +
        " @Memo, " +
        " @IsPaid, " +
        " @IsToBePrinted, " +
        " @DiscountLineAmount, " +
        " @DiscountLineRatePercent, " +
        " @DiscountLineIsTaxable, " +
        " @DiscountLineAccountId, " +
        " @SalesTaxLineAmount, " +
        " @SalesTaxLineRatePercent, " +
        " @SalesTaxLineAccountId, " +
        " @ShippingLineAmount, " +
        " @ShippingLineAccountId, " +
        " @IsCustomerTaxable, " +
        " @TaxCalculationType " +
      ")";

      public static void Insert(Invoice invoice)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
              Database.PutParameter(dbCommand,"@InvoiceId", invoice.InvoiceId);            
          
              Database.PutParameter(dbCommand,"@QuickBooksTxnId", invoice.QuickBooksTxnId);            
          
              Database.PutParameter(dbCommand,"@EntityStateId", invoice
			.EntityState.EntityStateId);            
          
              Database.PutParameter(dbCommand,"@EditSequence", invoice.EditSequence);            
          
              Database.PutParameter(dbCommand,"@TimeCreated", invoice.TimeCreated);            
          
              Database.PutParameter(dbCommand,"@TimeModified", invoice.TimeModified);            
          
              Database.PutParameter(dbCommand,"@TxnNumber", invoice.TxnNumber);            
          
              Database.PutParameter(dbCommand,"@CustomerId", invoice
			.Customer.CustomerId);            
          
              Database.PutParameter(dbCommand,"@ARAccountId", invoice.ARAccountId);            
          
              Database.PutParameter(dbCommand,"@TxnDate", invoice.TxnDate);            
          
              Database.PutParameter(dbCommand,"@RefNumber", invoice.RefNumber);            
          
              Database.PutParameter(dbCommand,"@BillAddr1", invoice.BillAddr1);            
          
              Database.PutParameter(dbCommand,"@BillAddr2", invoice.BillAddr2);            
          
              Database.PutParameter(dbCommand,"@BillAddr3", invoice.BillAddr3);            
          
              Database.PutParameter(dbCommand,"@BillAddr4", invoice.BillAddr4);            
          
              Database.PutParameter(dbCommand,"@BillCity", invoice.BillCity);            
          
              Database.PutParameter(dbCommand,"@BillState", invoice.BillState);            
          
              Database.PutParameter(dbCommand,"@BillPostalCode", invoice.BillPostalCode);            
          
              Database.PutParameter(dbCommand,"@BillCountry", invoice.BillCountry);            
          
              Database.PutParameter(dbCommand,"@ShipAddr1", invoice.ShipAddr1);            
          
              Database.PutParameter(dbCommand,"@ShipAddr2", invoice.ShipAddr2);            
          
              Database.PutParameter(dbCommand,"@ShipAddr3", invoice.ShipAddr3);            
          
              Database.PutParameter(dbCommand,"@ShipAddr4", invoice.ShipAddr4);            
          
              Database.PutParameter(dbCommand,"@ShipCity", invoice.ShipCity);            
          
              Database.PutParameter(dbCommand,"@ShipState", invoice.ShipState);            
          
              Database.PutParameter(dbCommand,"@ShipPostalCode", invoice.ShipPostalCode);            
          
              Database.PutParameter(dbCommand,"@ShipCountry", invoice.ShipCountry);            
          
            if(invoice
			.Terms == null)
            {
            Database.PutParameter(dbCommand,"@TermsId", DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@TermsId", invoice
			.Terms.TermsId);
            }
          
              Database.PutParameter(dbCommand,"@DueDate", invoice.DueDate);            
          
              Database.PutParameter(dbCommand,"@ShipDate", invoice.ShipDate);            
          
              Database.PutParameter(dbCommand,"@Subtotal", invoice.Subtotal);            
          
              Database.PutParameter(dbCommand,"@SalesTaxPercentage", invoice.SalesTaxPercentage);            
          
              Database.PutParameter(dbCommand,"@SalesTaxTotal", invoice.SalesTaxTotal);            
          
              Database.PutParameter(dbCommand,"@AppliedAmount", invoice.AppliedAmount);            
          
              Database.PutParameter(dbCommand,"@BalanceRemaining", invoice.BalanceRemaining);            
          
              Database.PutParameter(dbCommand,"@Memo", invoice.Memo);            
          
              Database.PutParameter(dbCommand,"@IsPaid", invoice.IsPaid);            
          
              Database.PutParameter(dbCommand,"@IsToBePrinted", invoice.IsToBePrinted);            
          
              Database.PutParameter(dbCommand,"@DiscountLineAmount", invoice.DiscountLineAmount);            
          
              Database.PutParameter(dbCommand,"@DiscountLineRatePercent", invoice.DiscountLineRatePercent);            
          
              Database.PutParameter(dbCommand,"@DiscountLineIsTaxable", invoice.DiscountLineIsTaxable);            
          
              Database.PutParameter(dbCommand,"@DiscountLineAccountId", invoice.DiscountLineAccountId);            
          
              Database.PutParameter(dbCommand,"@SalesTaxLineAmount", invoice.SalesTaxLineAmount);            
          
              Database.PutParameter(dbCommand,"@SalesTaxLineRatePercent", invoice.SalesTaxLineRatePercent);            
          
              Database.PutParameter(dbCommand,"@SalesTaxLineAccountId", invoice.SalesTaxLineAccountId);            
          
              Database.PutParameter(dbCommand,"@ShippingLineAmount", invoice.ShippingLineAmount);            
          
              Database.PutParameter(dbCommand,"@ShippingLineAccountId", invoice.ShippingLineAccountId);            
          
              Database.PutParameter(dbCommand,"@IsCustomerTaxable", invoice.IsCustomerTaxable);            
          
              Database.PutParameter(dbCommand,"@TaxCalculationType", invoice.TaxCalculationType);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Invoice>  invoiceList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Invoice invoice in  invoiceList)
      {
      if(!parametersAdded)
      {
      
            Database.PutParameter(dbCommand,"@InvoiceId", invoice.InvoiceId);
          
            Database.PutParameter(dbCommand,"@QuickBooksTxnId", invoice.QuickBooksTxnId);
          
            Database.PutParameter(dbCommand,"@EntityStateId", invoice
			.EntityState.EntityStateId);
          
            Database.PutParameter(dbCommand,"@EditSequence", invoice.EditSequence);
          
            Database.PutParameter(dbCommand,"@TimeCreated", invoice.TimeCreated);
          
            Database.PutParameter(dbCommand,"@TimeModified", invoice.TimeModified);
          
            Database.PutParameter(dbCommand,"@TxnNumber", invoice.TxnNumber);
          
            Database.PutParameter(dbCommand,"@CustomerId", invoice
			.Customer.CustomerId);
          
            Database.PutParameter(dbCommand,"@ARAccountId", invoice.ARAccountId);
          
            Database.PutParameter(dbCommand,"@TxnDate", invoice.TxnDate);
          
            Database.PutParameter(dbCommand,"@RefNumber", invoice.RefNumber);
          
            Database.PutParameter(dbCommand,"@BillAddr1", invoice.BillAddr1);
          
            Database.PutParameter(dbCommand,"@BillAddr2", invoice.BillAddr2);
          
            Database.PutParameter(dbCommand,"@BillAddr3", invoice.BillAddr3);
          
            Database.PutParameter(dbCommand,"@BillAddr4", invoice.BillAddr4);
          
            Database.PutParameter(dbCommand,"@BillCity", invoice.BillCity);
          
            Database.PutParameter(dbCommand,"@BillState", invoice.BillState);
          
            Database.PutParameter(dbCommand,"@BillPostalCode", invoice.BillPostalCode);
          
            Database.PutParameter(dbCommand,"@BillCountry", invoice.BillCountry);
          
            Database.PutParameter(dbCommand,"@ShipAddr1", invoice.ShipAddr1);
          
            Database.PutParameter(dbCommand,"@ShipAddr2", invoice.ShipAddr2);
          
            Database.PutParameter(dbCommand,"@ShipAddr3", invoice.ShipAddr3);
          
            Database.PutParameter(dbCommand,"@ShipAddr4", invoice.ShipAddr4);
          
            Database.PutParameter(dbCommand,"@ShipCity", invoice.ShipCity);
          
            Database.PutParameter(dbCommand,"@ShipState", invoice.ShipState);
          
            Database.PutParameter(dbCommand,"@ShipPostalCode", invoice.ShipPostalCode);
          
            Database.PutParameter(dbCommand,"@ShipCountry", invoice.ShipCountry);
          
            if(invoice
			.Terms == null)
            {
              Database.PutParameter(dbCommand,"@TermsId", DbType.Int32);
            }
            else
            {
              Database.PutParameter(dbCommand,"@TermsId", invoice
			.Terms.TermsId);
            }
          
            Database.PutParameter(dbCommand,"@DueDate", invoice.DueDate);
          
            Database.PutParameter(dbCommand,"@ShipDate", invoice.ShipDate);
          
            Database.PutParameter(dbCommand,"@Subtotal", invoice.Subtotal);
          
            Database.PutParameter(dbCommand,"@SalesTaxPercentage", invoice.SalesTaxPercentage);
          
            Database.PutParameter(dbCommand,"@SalesTaxTotal", invoice.SalesTaxTotal);
          
            Database.PutParameter(dbCommand,"@AppliedAmount", invoice.AppliedAmount);
          
            Database.PutParameter(dbCommand,"@BalanceRemaining", invoice.BalanceRemaining);
          
            Database.PutParameter(dbCommand,"@Memo", invoice.Memo);
          
            Database.PutParameter(dbCommand,"@IsPaid", invoice.IsPaid);
          
            Database.PutParameter(dbCommand,"@IsToBePrinted", invoice.IsToBePrinted);
          
            Database.PutParameter(dbCommand,"@DiscountLineAmount", invoice.DiscountLineAmount);
          
            Database.PutParameter(dbCommand,"@DiscountLineRatePercent", invoice.DiscountLineRatePercent);
          
            Database.PutParameter(dbCommand,"@DiscountLineIsTaxable", invoice.DiscountLineIsTaxable);
          
            Database.PutParameter(dbCommand,"@DiscountLineAccountId", invoice.DiscountLineAccountId);
          
            Database.PutParameter(dbCommand,"@SalesTaxLineAmount", invoice.SalesTaxLineAmount);
          
            Database.PutParameter(dbCommand,"@SalesTaxLineRatePercent", invoice.SalesTaxLineRatePercent);
          
            Database.PutParameter(dbCommand,"@SalesTaxLineAccountId", invoice.SalesTaxLineAccountId);
          
            Database.PutParameter(dbCommand,"@ShippingLineAmount", invoice.ShippingLineAmount);
          
            Database.PutParameter(dbCommand,"@ShippingLineAccountId", invoice.ShippingLineAccountId);
          
            Database.PutParameter(dbCommand,"@IsCustomerTaxable", invoice.IsCustomerTaxable);
          
            Database.PutParameter(dbCommand,"@TaxCalculationType", invoice.TaxCalculationType);
          
      parametersAdded = true;
      }
      else
      {

      
            Database.UpdateParameter(dbCommand,"@InvoiceId",invoice.InvoiceId);
          
            Database.UpdateParameter(dbCommand,"@QuickBooksTxnId",invoice.QuickBooksTxnId);
          
            Database.UpdateParameter(dbCommand,"@EntityStateId",invoice
			.EntityState.EntityStateId);
          
            Database.UpdateParameter(dbCommand,"@EditSequence",invoice.EditSequence);
          
            Database.UpdateParameter(dbCommand,"@TimeCreated",invoice.TimeCreated);
          
            Database.UpdateParameter(dbCommand,"@TimeModified",invoice.TimeModified);
          
            Database.UpdateParameter(dbCommand,"@TxnNumber",invoice.TxnNumber);
          
            Database.UpdateParameter(dbCommand,"@CustomerId",invoice
			.Customer.CustomerId);
          
            Database.UpdateParameter(dbCommand,"@ARAccountId",invoice.ARAccountId);
          
            Database.UpdateParameter(dbCommand,"@TxnDate",invoice.TxnDate);
          
            Database.UpdateParameter(dbCommand,"@RefNumber",invoice.RefNumber);
          
            Database.UpdateParameter(dbCommand,"@BillAddr1",invoice.BillAddr1);
          
            Database.UpdateParameter(dbCommand,"@BillAddr2",invoice.BillAddr2);
          
            Database.UpdateParameter(dbCommand,"@BillAddr3",invoice.BillAddr3);
          
            Database.UpdateParameter(dbCommand,"@BillAddr4",invoice.BillAddr4);
          
            Database.UpdateParameter(dbCommand,"@BillCity",invoice.BillCity);
          
            Database.UpdateParameter(dbCommand,"@BillState",invoice.BillState);
          
            Database.UpdateParameter(dbCommand,"@BillPostalCode",invoice.BillPostalCode);
          
            Database.UpdateParameter(dbCommand,"@BillCountry",invoice.BillCountry);
          
            Database.UpdateParameter(dbCommand,"@ShipAddr1",invoice.ShipAddr1);
          
            Database.UpdateParameter(dbCommand,"@ShipAddr2",invoice.ShipAddr2);
          
            Database.UpdateParameter(dbCommand,"@ShipAddr3",invoice.ShipAddr3);
          
            Database.UpdateParameter(dbCommand,"@ShipAddr4",invoice.ShipAddr4);
          
            Database.UpdateParameter(dbCommand,"@ShipCity",invoice.ShipCity);
          
            Database.UpdateParameter(dbCommand,"@ShipState",invoice.ShipState);
          
            Database.UpdateParameter(dbCommand,"@ShipPostalCode",invoice.ShipPostalCode);
          
            Database.UpdateParameter(dbCommand,"@ShipCountry",invoice.ShipCountry);
          
            if(invoice
			.Terms == null)
            {
             Database.UpdateParameter(dbCommand,"@TermsId",DbType.Int32);
            }
            else
            {
            Database.UpdateParameter(dbCommand,"@TermsId",invoice
			.Terms.TermsId);
            }
          
            Database.UpdateParameter(dbCommand,"@DueDate",invoice.DueDate);
          
            Database.UpdateParameter(dbCommand,"@ShipDate",invoice.ShipDate);
          
            Database.UpdateParameter(dbCommand,"@Subtotal",invoice.Subtotal);
          
            Database.UpdateParameter(dbCommand,"@SalesTaxPercentage",invoice.SalesTaxPercentage);
          
            Database.UpdateParameter(dbCommand,"@SalesTaxTotal",invoice.SalesTaxTotal);
          
            Database.UpdateParameter(dbCommand,"@AppliedAmount",invoice.AppliedAmount);
          
            Database.UpdateParameter(dbCommand,"@BalanceRemaining",invoice.BalanceRemaining);
          
            Database.UpdateParameter(dbCommand,"@Memo",invoice.Memo);
          
            Database.UpdateParameter(dbCommand,"@IsPaid",invoice.IsPaid);
          
            Database.UpdateParameter(dbCommand,"@IsToBePrinted",invoice.IsToBePrinted);
          
            Database.UpdateParameter(dbCommand,"@DiscountLineAmount",invoice.DiscountLineAmount);
          
            Database.UpdateParameter(dbCommand,"@DiscountLineRatePercent",invoice.DiscountLineRatePercent);
          
            Database.UpdateParameter(dbCommand,"@DiscountLineIsTaxable",invoice.DiscountLineIsTaxable);
          
            Database.UpdateParameter(dbCommand,"@DiscountLineAccountId",invoice.DiscountLineAccountId);
          
            Database.UpdateParameter(dbCommand,"@SalesTaxLineAmount",invoice.SalesTaxLineAmount);
          
            Database.UpdateParameter(dbCommand,"@SalesTaxLineRatePercent",invoice.SalesTaxLineRatePercent);
          
            Database.UpdateParameter(dbCommand,"@SalesTaxLineAccountId",invoice.SalesTaxLineAccountId);
          
            Database.UpdateParameter(dbCommand,"@ShippingLineAmount",invoice.ShippingLineAmount);
          
            Database.UpdateParameter(dbCommand,"@ShippingLineAccountId",invoice.ShippingLineAccountId);
          
            Database.UpdateParameter(dbCommand,"@IsCustomerTaxable",invoice.IsCustomerTaxable);
          
            Database.UpdateParameter(dbCommand,"@TaxCalculationType",invoice.TaxCalculationType);
          
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Invoice] Set "
      
        + " QuickBooksTxnId = @QuickBooksTxnId, "
        + " EntityStateId = @EntityStateId, "
        + " EditSequence = @EditSequence, "
        + " TimeCreated = @TimeCreated, "
        + " TimeModified = @TimeModified, "
        + " TxnNumber = @TxnNumber, "
        + " CustomerId = @CustomerId, "
        + " ARAccountId = @ARAccountId, "
        + " TxnDate = @TxnDate, "
        + " RefNumber = @RefNumber, "
        + " BillAddr1 = @BillAddr1, "
        + " BillAddr2 = @BillAddr2, "
        + " BillAddr3 = @BillAddr3, "
        + " BillAddr4 = @BillAddr4, "
        + " BillCity = @BillCity, "
        + " BillState = @BillState, "
        + " BillPostalCode = @BillPostalCode, "
        + " BillCountry = @BillCountry, "
        + " ShipAddr1 = @ShipAddr1, "
        + " ShipAddr2 = @ShipAddr2, "
        + " ShipAddr3 = @ShipAddr3, "
        + " ShipAddr4 = @ShipAddr4, "
        + " ShipCity = @ShipCity, "
        + " ShipState = @ShipState, "
        + " ShipPostalCode = @ShipPostalCode, "
        + " ShipCountry = @ShipCountry, "
        + " TermsId = @TermsId, "
        + " DueDate = @DueDate, "
        + " ShipDate = @ShipDate, "
        + " Subtotal = @Subtotal, "
        + " SalesTaxPercentage = @SalesTaxPercentage, "
        + " SalesTaxTotal = @SalesTaxTotal, "
        + " AppliedAmount = @AppliedAmount, "
        + " BalanceRemaining = @BalanceRemaining, "
        + " Memo = @Memo, "
        + " IsPaid = @IsPaid, "
        + " IsToBePrinted = @IsToBePrinted, "
        + " DiscountLineAmount = @DiscountLineAmount, "
        + " DiscountLineRatePercent = @DiscountLineRatePercent, "
        + " DiscountLineIsTaxable = @DiscountLineIsTaxable, "
        + " DiscountLineAccountId = @DiscountLineAccountId, "
        + " SalesTaxLineAmount = @SalesTaxLineAmount, "
        + " SalesTaxLineRatePercent = @SalesTaxLineRatePercent, "
        + " SalesTaxLineAccountId = @SalesTaxLineAccountId, "
        + " ShippingLineAmount = @ShippingLineAmount, "
        + " ShippingLineAccountId = @ShippingLineAccountId, "
        + " IsCustomerTaxable = @IsCustomerTaxable, "
        + " TaxCalculationType = @TaxCalculationType "
        + " Where "
        
          + " InvoiceId = @InvoiceId "
        
      ;

      public static void Update(Invoice invoice)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
            Database.PutParameter(dbCommand,"@InvoiceId", invoice.InvoiceId);            
          
            Database.PutParameter(dbCommand,"@QuickBooksTxnId", invoice.QuickBooksTxnId);            
          
            Database.PutParameter(dbCommand,"@EntityStateId", invoice
			.EntityState.EntityStateId);            
          
            Database.PutParameter(dbCommand,"@EditSequence", invoice.EditSequence);            
          
            Database.PutParameter(dbCommand,"@TimeCreated", invoice.TimeCreated);            
          
            Database.PutParameter(dbCommand,"@TimeModified", invoice.TimeModified);            
          
            Database.PutParameter(dbCommand,"@TxnNumber", invoice.TxnNumber);            
          
            Database.PutParameter(dbCommand,"@CustomerId", invoice
			.Customer.CustomerId);            
          
            Database.PutParameter(dbCommand,"@ARAccountId", invoice.ARAccountId);            
          
            Database.PutParameter(dbCommand,"@TxnDate", invoice.TxnDate);            
          
            Database.PutParameter(dbCommand,"@RefNumber", invoice.RefNumber);            
          
            Database.PutParameter(dbCommand,"@BillAddr1", invoice.BillAddr1);            
          
            Database.PutParameter(dbCommand,"@BillAddr2", invoice.BillAddr2);            
          
            Database.PutParameter(dbCommand,"@BillAddr3", invoice.BillAddr3);            
          
            Database.PutParameter(dbCommand,"@BillAddr4", invoice.BillAddr4);            
          
            Database.PutParameter(dbCommand,"@BillCity", invoice.BillCity);            
          
            Database.PutParameter(dbCommand,"@BillState", invoice.BillState);            
          
            Database.PutParameter(dbCommand,"@BillPostalCode", invoice.BillPostalCode);            
          
            Database.PutParameter(dbCommand,"@BillCountry", invoice.BillCountry);            
          
            Database.PutParameter(dbCommand,"@ShipAddr1", invoice.ShipAddr1);            
          
            Database.PutParameter(dbCommand,"@ShipAddr2", invoice.ShipAddr2);            
          
            Database.PutParameter(dbCommand,"@ShipAddr3", invoice.ShipAddr3);            
          
            Database.PutParameter(dbCommand,"@ShipAddr4", invoice.ShipAddr4);            
          
            Database.PutParameter(dbCommand,"@ShipCity", invoice.ShipCity);            
          
            Database.PutParameter(dbCommand,"@ShipState", invoice.ShipState);            
          
            Database.PutParameter(dbCommand,"@ShipPostalCode", invoice.ShipPostalCode);            
          
            Database.PutParameter(dbCommand,"@ShipCountry", invoice.ShipCountry);            
          
            if(invoice
			.Terms == null)
            {
            Database.PutParameter(dbCommand,"@TermsId",DbType.Int32);
            }
            else
            {
            Database.PutParameter(dbCommand,"@TermsId",invoice
			.Terms.TermsId);
            }
          
            Database.PutParameter(dbCommand,"@DueDate", invoice.DueDate);            
          
            Database.PutParameter(dbCommand,"@ShipDate", invoice.ShipDate);            
          
            Database.PutParameter(dbCommand,"@Subtotal", invoice.Subtotal);            
          
            Database.PutParameter(dbCommand,"@SalesTaxPercentage", invoice.SalesTaxPercentage);            
          
            Database.PutParameter(dbCommand,"@SalesTaxTotal", invoice.SalesTaxTotal);            
          
            Database.PutParameter(dbCommand,"@AppliedAmount", invoice.AppliedAmount);            
          
            Database.PutParameter(dbCommand,"@BalanceRemaining", invoice.BalanceRemaining);            
          
            Database.PutParameter(dbCommand,"@Memo", invoice.Memo);            
          
            Database.PutParameter(dbCommand,"@IsPaid", invoice.IsPaid);            
          
            Database.PutParameter(dbCommand,"@IsToBePrinted", invoice.IsToBePrinted);            
          
            Database.PutParameter(dbCommand,"@DiscountLineAmount", invoice.DiscountLineAmount);            
          
            Database.PutParameter(dbCommand,"@DiscountLineRatePercent", invoice.DiscountLineRatePercent);            
          
            Database.PutParameter(dbCommand,"@DiscountLineIsTaxable", invoice.DiscountLineIsTaxable);            
          
            Database.PutParameter(dbCommand,"@DiscountLineAccountId", invoice.DiscountLineAccountId);            
          
            Database.PutParameter(dbCommand,"@SalesTaxLineAmount", invoice.SalesTaxLineAmount);            
          
            Database.PutParameter(dbCommand,"@SalesTaxLineRatePercent", invoice.SalesTaxLineRatePercent);            
          
            Database.PutParameter(dbCommand,"@SalesTaxLineAccountId", invoice.SalesTaxLineAccountId);            
          
            Database.PutParameter(dbCommand,"@ShippingLineAmount", invoice.ShippingLineAmount);            
          
            Database.PutParameter(dbCommand,"@ShippingLineAccountId", invoice.ShippingLineAccountId);            
          
            Database.PutParameter(dbCommand,"@IsCustomerTaxable", invoice.IsCustomerTaxable);            
          
            Database.PutParameter(dbCommand,"@TaxCalculationType", invoice.TaxCalculationType);            
          

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "
      
        + " InvoiceId, "
        + " QuickBooksTxnId, "
        + " EntityStateId, "
        + " EditSequence, "
        + " TimeCreated, "
        + " TimeModified, "
        + " TxnNumber, "
        + " CustomerId, "
        + " ARAccountId, "
        + " TxnDate, "
        + " RefNumber, "
        + " BillAddr1, "
        + " BillAddr2, "
        + " BillAddr3, "
        + " BillAddr4, "
        + " BillCity, "
        + " BillState, "
        + " BillPostalCode, "
        + " BillCountry, "
        + " ShipAddr1, "
        + " ShipAddr2, "
        + " ShipAddr3, "
        + " ShipAddr4, "
        + " ShipCity, "
        + " ShipState, "
        + " ShipPostalCode, "
        + " ShipCountry, "
        + " TermsId, "
        + " DueDate, "
        + " ShipDate, "
        + " Subtotal, "
        + " SalesTaxPercentage, "
        + " SalesTaxTotal, "
        + " AppliedAmount, "
        + " BalanceRemaining, "
        + " Memo, "
        + " IsPaid, "
        + " IsToBePrinted, "
        + " DiscountLineAmount, "
        + " DiscountLineRatePercent, "
        + " DiscountLineIsTaxable, "
        + " DiscountLineAccountId, "
        + " SalesTaxLineAmount, "
        + " SalesTaxLineRatePercent, "
        + " SalesTaxLineAccountId, "
        + " ShippingLineAmount, "
        + " ShippingLineAccountId, "
        + " IsCustomerTaxable, "
        + " TaxCalculationType "
        + " From [Invoice] "
      
        + " Where "
        
        + " InvoiceId = @InvoiceId "
        
      ;

      public static Invoice FindByPrimaryKey(
      int invoiceId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InvoiceId", invoiceId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Invoice not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Invoice invoice)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InvoiceId",invoice.InvoiceId);
      

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
      String sql = "select 1 from [Invoice]";

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

      public static Invoice Load(IDataReader dataReader)
      {
      Invoice invoice = new Invoice();

      invoice.InvoiceId = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
              invoice.QuickBooksTxnId = dataReader.GetInt32(1);
          invoice
			.EntityState = new EntityState();

            invoice
			.EntityState.EntityStateId = dataReader.GetInt32(2);
          invoice.EditSequence = dataReader.GetInt32(3);
          
            if(!dataReader.IsDBNull(4))
              invoice.TimeCreated = dataReader.GetDateTime(4);
          
            if(!dataReader.IsDBNull(5))
              invoice.TimeModified = dataReader.GetDateTime(5);
          
            if(!dataReader.IsDBNull(6))
              invoice.TxnNumber = dataReader.GetInt32(6);
          invoice
			.Customer = new Customer();

            invoice
			.Customer.CustomerId = dataReader.GetInt32(7);
          
            if(!dataReader.IsDBNull(8))
              invoice.ARAccountId = dataReader.GetInt32(8);
          
            if(!dataReader.IsDBNull(9))
              invoice.TxnDate = dataReader.GetDateTime(9);
          
            if(!dataReader.IsDBNull(10))
              invoice.RefNumber = dataReader.GetString(10);
          
            if(!dataReader.IsDBNull(11))
              invoice.BillAddr1 = dataReader.GetString(11);
          
            if(!dataReader.IsDBNull(12))
              invoice.BillAddr2 = dataReader.GetString(12);
          
            if(!dataReader.IsDBNull(13))
              invoice.BillAddr3 = dataReader.GetString(13);
          
            if(!dataReader.IsDBNull(14))
              invoice.BillAddr4 = dataReader.GetString(14);
          
            if(!dataReader.IsDBNull(15))
              invoice.BillCity = dataReader.GetString(15);
          
            if(!dataReader.IsDBNull(16))
              invoice.BillState = dataReader.GetString(16);
          
            if(!dataReader.IsDBNull(17))
              invoice.BillPostalCode = dataReader.GetString(17);
          
            if(!dataReader.IsDBNull(18))
              invoice.BillCountry = dataReader.GetString(18);
          
            if(!dataReader.IsDBNull(19))
              invoice.ShipAddr1 = dataReader.GetString(19);
          
            if(!dataReader.IsDBNull(20))
              invoice.ShipAddr2 = dataReader.GetString(20);
          
            if(!dataReader.IsDBNull(21))
              invoice.ShipAddr3 = dataReader.GetString(21);
          
            if(!dataReader.IsDBNull(22))
              invoice.ShipAddr4 = dataReader.GetString(22);
          
            if(!dataReader.IsDBNull(23))
              invoice.ShipCity = dataReader.GetString(23);
          
            if(!dataReader.IsDBNull(24))
              invoice.ShipState = dataReader.GetString(24);
          
            if(!dataReader.IsDBNull(25))
              invoice.ShipPostalCode = dataReader.GetString(25);
          
            if(!dataReader.IsDBNull(26))
              invoice.ShipCountry = dataReader.GetString(26);
          
            if(!dataReader.IsDBNull(27))
            {
            invoice
			.Terms = new Terms();
            
            invoice
			.Terms.TermsId = dataReader.GetInt32(27);
           }
            else
            invoice
			.Terms = null;
          
            if(!dataReader.IsDBNull(28))
              invoice.DueDate = dataReader.GetDateTime(28);
          
            if(!dataReader.IsDBNull(29))
              invoice.ShipDate = dataReader.GetDateTime(29);
          
            if(!dataReader.IsDBNull(30))
              invoice.Subtotal = dataReader.GetDecimal(30);
          
            if(!dataReader.IsDBNull(31))
              invoice.SalesTaxPercentage = dataReader.GetDecimal(31);
          
            if(!dataReader.IsDBNull(32))
              invoice.SalesTaxTotal = dataReader.GetDecimal(32);
          
            if(!dataReader.IsDBNull(33))
              invoice.AppliedAmount = dataReader.GetDecimal(33);
          
            if(!dataReader.IsDBNull(34))
              invoice.BalanceRemaining = dataReader.GetDecimal(34);
          
            if(!dataReader.IsDBNull(35))
              invoice.Memo = dataReader.GetString(35);
          
            if(!dataReader.IsDBNull(36))
              invoice.IsPaid = dataReader.GetBoolean(36);
          
            if(!dataReader.IsDBNull(37))
              invoice.IsToBePrinted = dataReader.GetBoolean(37);
          
            if(!dataReader.IsDBNull(38))
              invoice.DiscountLineAmount = dataReader.GetDecimal(38);
          
            if(!dataReader.IsDBNull(39))
              invoice.DiscountLineRatePercent = dataReader.GetDecimal(39);
          
            if(!dataReader.IsDBNull(40))
              invoice.DiscountLineIsTaxable = dataReader.GetBoolean(40);
          
            if(!dataReader.IsDBNull(41))
              invoice.DiscountLineAccountId = dataReader.GetInt32(41);
          
            if(!dataReader.IsDBNull(42))
              invoice.SalesTaxLineAmount = dataReader.GetDecimal(42);
          
            if(!dataReader.IsDBNull(43))
              invoice.SalesTaxLineRatePercent = dataReader.GetDecimal(43);
          
            if(!dataReader.IsDBNull(44))
              invoice.SalesTaxLineAccountId = dataReader.GetInt32(44);
          
            if(!dataReader.IsDBNull(45))
              invoice.ShippingLineAmount = dataReader.GetDecimal(45);
          
            if(!dataReader.IsDBNull(46))
              invoice.ShippingLineAccountId = dataReader.GetInt32(46);
          
            if(!dataReader.IsDBNull(47))
              invoice.IsCustomerTaxable = dataReader.GetBoolean(47);
          
            if(!dataReader.IsDBNull(48))
              invoice.TaxCalculationType = dataReader.GetBoolean(48);
          

      return invoice;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Invoice] "

      
        + " Where "
        
          + " InvoiceId = @InvoiceId "
        
      ;
      public static void Delete(Invoice invoice)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@InvoiceId", invoice.InvoiceId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Invoice] ";

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
      
        + " InvoiceId, "
        + " QuickBooksTxnId, "
        + " EntityStateId, "
        + " EditSequence, "
        + " TimeCreated, "
        + " TimeModified, "
        + " TxnNumber, "
        + " CustomerId, "
        + " ARAccountId, "
        + " TxnDate, "
        + " RefNumber, "
        + " BillAddr1, "
        + " BillAddr2, "
        + " BillAddr3, "
        + " BillAddr4, "
        + " BillCity, "
        + " BillState, "
        + " BillPostalCode, "
        + " BillCountry, "
        + " ShipAddr1, "
        + " ShipAddr2, "
        + " ShipAddr3, "
        + " ShipAddr4, "
        + " ShipCity, "
        + " ShipState, "
        + " ShipPostalCode, "
        + " ShipCountry, "
        + " TermsId, "
        + " DueDate, "
        + " ShipDate, "
        + " Subtotal, "
        + " SalesTaxPercentage, "
        + " SalesTaxTotal, "
        + " AppliedAmount, "
        + " BalanceRemaining, "
        + " Memo, "
        + " IsPaid, "
        + " IsToBePrinted, "
        + " DiscountLineAmount, "
        + " DiscountLineRatePercent, "
        + " DiscountLineIsTaxable, "
        + " DiscountLineAccountId, "
        + " SalesTaxLineAmount, "
        + " SalesTaxLineRatePercent, "
        + " SalesTaxLineAccountId, "
        + " ShippingLineAmount, "
        + " ShippingLineAccountId, "
        + " IsCustomerTaxable, "
        + " TaxCalculationType "
        + " From [Invoice] ";
      public static List<Invoice> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Invoice> rv = new List<Invoice>();

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
      List<Invoice> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Invoice> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Invoice));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Invoice item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Invoice>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Invoice));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Invoice> itemsList
      = new List<Invoice>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Invoice)
      itemsList.Add(deserializedObject as Invoice);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      


		#region Fields
		
		#region InvoiceId
        protected int m_invoiceId;

			[XmlAttribute]
			public int InvoiceId
			{
			get { return m_invoiceId;}
			set { m_invoiceId = value; }
			}
		#endregion
		
		#region QuickBooksTxnId
        protected int? m_quickBooksTxnId;

			[XmlAttribute]
			public int? QuickBooksTxnId
			{
			get { return m_quickBooksTxnId;}
			set { m_quickBooksTxnId = value; }
			}
		#endregion
		
		#region EditSequence
        protected int m_editSequence;

			[XmlAttribute]
			public int EditSequence
			{
			get { return m_editSequence;}
			set { m_editSequence = value; }
			}
		#endregion
		
		#region TimeCreated
        protected DateTime? m_timeCreated;

			[XmlAttribute]
			public DateTime? TimeCreated
			{
			get { return m_timeCreated;}
			set { m_timeCreated = value; }
			}
		#endregion
		
		#region TimeModified
        protected DateTime? m_timeModified;

			[XmlAttribute]
			public DateTime? TimeModified
			{
			get { return m_timeModified;}
			set { m_timeModified = value; }
			}
		#endregion
		
		#region TxnNumber
        protected int? m_txnNumber;

			[XmlAttribute]
			public int? TxnNumber
			{
			get { return m_txnNumber;}
			set { m_txnNumber = value; }
			}
		#endregion
		
		#region ARAccountId
        protected int? m_aRAccountId;

			[XmlAttribute]
			public int? ARAccountId
			{
			get { return m_aRAccountId;}
			set { m_aRAccountId = value; }
			}
		#endregion
		
		#region TxnDate
        protected DateTime? m_txnDate;

			[XmlAttribute]
			public DateTime? TxnDate
			{
			get { return m_txnDate;}
			set { m_txnDate = value; }
			}
		#endregion
		
		#region RefNumber
        protected String m_refNumber;

			[XmlAttribute]
			public String RefNumber
			{
			get { return m_refNumber;}
			set { m_refNumber = value; }
			}
		#endregion
		
		#region BillAddr1
        protected String m_billAddr1;

			[XmlAttribute]
			public String BillAddr1
			{
			get { return m_billAddr1;}
			set { m_billAddr1 = value; }
			}
		#endregion
		
		#region BillAddr2
        protected String m_billAddr2;

			[XmlAttribute]
			public String BillAddr2
			{
			get { return m_billAddr2;}
			set { m_billAddr2 = value; }
			}
		#endregion
		
		#region BillAddr3
        protected String m_billAddr3;

			[XmlAttribute]
			public String BillAddr3
			{
			get { return m_billAddr3;}
			set { m_billAddr3 = value; }
			}
		#endregion
		
		#region BillAddr4
        protected String m_billAddr4;

			[XmlAttribute]
			public String BillAddr4
			{
			get { return m_billAddr4;}
			set { m_billAddr4 = value; }
			}
		#endregion
		
		#region BillCity
        protected String m_billCity;

			[XmlAttribute]
			public String BillCity
			{
			get { return m_billCity;}
			set { m_billCity = value; }
			}
		#endregion
		
		#region BillState
        protected String m_billState;

			[XmlAttribute]
			public String BillState
			{
			get { return m_billState;}
			set { m_billState = value; }
			}
		#endregion
		
		#region BillPostalCode
        protected String m_billPostalCode;

			[XmlAttribute]
			public String BillPostalCode
			{
			get { return m_billPostalCode;}
			set { m_billPostalCode = value; }
			}
		#endregion
		
		#region BillCountry
        protected String m_billCountry;

			[XmlAttribute]
			public String BillCountry
			{
			get { return m_billCountry;}
			set { m_billCountry = value; }
			}
		#endregion
		
		#region ShipAddr1
        protected String m_shipAddr1;

			[XmlAttribute]
			public String ShipAddr1
			{
			get { return m_shipAddr1;}
			set { m_shipAddr1 = value; }
			}
		#endregion
		
		#region ShipAddr2
        protected String m_shipAddr2;

			[XmlAttribute]
			public String ShipAddr2
			{
			get { return m_shipAddr2;}
			set { m_shipAddr2 = value; }
			}
		#endregion
		
		#region ShipAddr3
        protected String m_shipAddr3;

			[XmlAttribute]
			public String ShipAddr3
			{
			get { return m_shipAddr3;}
			set { m_shipAddr3 = value; }
			}
		#endregion
		
		#region ShipAddr4
        protected String m_shipAddr4;

			[XmlAttribute]
			public String ShipAddr4
			{
			get { return m_shipAddr4;}
			set { m_shipAddr4 = value; }
			}
		#endregion
		
		#region ShipCity
        protected String m_shipCity;

			[XmlAttribute]
			public String ShipCity
			{
			get { return m_shipCity;}
			set { m_shipCity = value; }
			}
		#endregion
		
		#region ShipState
        protected String m_shipState;

			[XmlAttribute]
			public String ShipState
			{
			get { return m_shipState;}
			set { m_shipState = value; }
			}
		#endregion
		
		#region ShipPostalCode
        protected String m_shipPostalCode;

			[XmlAttribute]
			public String ShipPostalCode
			{
			get { return m_shipPostalCode;}
			set { m_shipPostalCode = value; }
			}
		#endregion
		
		#region ShipCountry
        protected String m_shipCountry;

			[XmlAttribute]
			public String ShipCountry
			{
			get { return m_shipCountry;}
			set { m_shipCountry = value; }
			}
		#endregion
		
		#region DueDate
        protected DateTime? m_dueDate;

			[XmlAttribute]
			public DateTime? DueDate
			{
			get { return m_dueDate;}
			set { m_dueDate = value; }
			}
		#endregion
		
		#region ShipDate
        protected DateTime? m_shipDate;

			[XmlAttribute]
			public DateTime? ShipDate
			{
			get { return m_shipDate;}
			set { m_shipDate = value; }
			}
		#endregion
		
		#region Subtotal
        protected decimal? m_subtotal;

			[XmlAttribute]
			public decimal? Subtotal
			{
			get { return m_subtotal;}
			set { m_subtotal = value; }
			}
		#endregion
		
		#region SalesTaxPercentage
        protected decimal? m_salesTaxPercentage;

			[XmlAttribute]
			public decimal? SalesTaxPercentage
			{
			get { return m_salesTaxPercentage;}
			set { m_salesTaxPercentage = value; }
			}
		#endregion
		
		#region SalesTaxTotal
        protected decimal? m_salesTaxTotal;

			[XmlAttribute]
			public decimal? SalesTaxTotal
			{
			get { return m_salesTaxTotal;}
			set { m_salesTaxTotal = value; }
			}
		#endregion
		
		#region AppliedAmount
        protected decimal? m_appliedAmount;

			[XmlAttribute]
			public decimal? AppliedAmount
			{
			get { return m_appliedAmount;}
			set { m_appliedAmount = value; }
			}
		#endregion
		
		#region BalanceRemaining
        protected decimal? m_balanceRemaining;

			[XmlAttribute]
			public decimal? BalanceRemaining
			{
			get { return m_balanceRemaining;}
			set { m_balanceRemaining = value; }
			}
		#endregion
		
		#region Memo
        protected String m_memo;

			[XmlAttribute]
			public String Memo
			{
			get { return m_memo;}
			set { m_memo = value; }
			}
		#endregion
		
		#region IsPaid
        protected bool m_isPaid;

			[XmlAttribute]
			public bool IsPaid
			{
			get { return m_isPaid;}
			set { m_isPaid = value; }
			}
		#endregion
		
		#region IsToBePrinted
        protected bool m_isToBePrinted;

			[XmlAttribute]
			public bool IsToBePrinted
			{
			get { return m_isToBePrinted;}
			set { m_isToBePrinted = value; }
			}
		#endregion
		
		#region DiscountLineAmount
        protected decimal? m_discountLineAmount;

			[XmlAttribute]
			public decimal? DiscountLineAmount
			{
			get { return m_discountLineAmount;}
			set { m_discountLineAmount = value; }
			}
		#endregion
		
		#region DiscountLineRatePercent
        protected decimal? m_discountLineRatePercent;

			[XmlAttribute]
			public decimal? DiscountLineRatePercent
			{
			get { return m_discountLineRatePercent;}
			set { m_discountLineRatePercent = value; }
			}
		#endregion
		
		#region DiscountLineIsTaxable
        protected bool m_discountLineIsTaxable;

			[XmlAttribute]
			public bool DiscountLineIsTaxable
			{
			get { return m_discountLineIsTaxable;}
			set { m_discountLineIsTaxable = value; }
			}
		#endregion
		
		#region DiscountLineAccountId
        protected int? m_discountLineAccountId;

			[XmlAttribute]
			public int? DiscountLineAccountId
			{
			get { return m_discountLineAccountId;}
			set { m_discountLineAccountId = value; }
			}
		#endregion
		
		#region SalesTaxLineAmount
        protected decimal? m_salesTaxLineAmount;

			[XmlAttribute]
			public decimal? SalesTaxLineAmount
			{
			get { return m_salesTaxLineAmount;}
			set { m_salesTaxLineAmount = value; }
			}
		#endregion
		
		#region SalesTaxLineRatePercent
        protected decimal? m_salesTaxLineRatePercent;

			[XmlAttribute]
			public decimal? SalesTaxLineRatePercent
			{
			get { return m_salesTaxLineRatePercent;}
			set { m_salesTaxLineRatePercent = value; }
			}
		#endregion
		
		#region SalesTaxLineAccountId
        protected int? m_salesTaxLineAccountId;

			[XmlAttribute]
			public int? SalesTaxLineAccountId
			{
			get { return m_salesTaxLineAccountId;}
			set { m_salesTaxLineAccountId = value; }
			}
		#endregion
		
		#region ShippingLineAmount
        protected decimal? m_shippingLineAmount;

			[XmlAttribute]
			public decimal? ShippingLineAmount
			{
			get { return m_shippingLineAmount;}
			set { m_shippingLineAmount = value; }
			}
		#endregion
		
		#region ShippingLineAccountId
        protected int? m_shippingLineAccountId;

			[XmlAttribute]
			public int? ShippingLineAccountId
			{
			get { return m_shippingLineAccountId;}
			set { m_shippingLineAccountId = value; }
			}
		#endregion
		
		#region IsCustomerTaxable
        protected bool m_isCustomerTaxable;

			[XmlAttribute]
			public bool IsCustomerTaxable
			{
			get { return m_isCustomerTaxable;}
			set { m_isCustomerTaxable = value; }
			}
		#endregion
		
		#region TaxCalculationType
        protected bool m_taxCalculationType;

			[XmlAttribute]
			public bool TaxCalculationType
			{
			get { return m_taxCalculationType;}
			set { m_taxCalculationType = value; }
			}
		#endregion
		
		#region Account
			protected Account m_account;

			[XmlElement]
			public Account Account
			{
			get { return m_account;}
			set { m_account = value; }
			}
		#endregion
		
		#region Customer
			protected Customer m_customer;

			[XmlElement]
			public Customer Customer
			{
			get { return m_customer;}
			set { m_customer = value; }
			}
		#endregion
		
		#region EntityState
			protected EntityState m_entityState;

			[XmlElement]
			public EntityState EntityState
			{
			get { return m_entityState;}
			set { m_entityState = value; }
			}
		#endregion
		
		#region Terms
			protected Terms m_terms;

			[XmlElement]
			public Terms Terms
			{
			get { return m_terms;}
			set { m_terms = value; }
			}
		#endregion
		
		
		#endregion

      #region Constructors
      public Invoice(
		int invoiceId

		)
		{
		
			m_invoiceId = invoiceId;
		
        }

      


        public Invoice(
		  Account account,Customer customer,EntityState entityState,Terms terms
			  ,
		  int invoiceId,int? quickBooksTxnId,int editSequence,DateTime? timeCreated,DateTime? timeModified,int? txnNumber,int? aRAccountId,DateTime? txnDate,String refNumber,String billAddr1,String billAddr2,String billAddr3,String billAddr4,String billCity,String billState,String billPostalCode,String billCountry,String shipAddr1,String shipAddr2,String shipAddr3,String shipAddr4,String shipCity,String shipState,String shipPostalCode,String shipCountry,DateTime? dueDate,DateTime? shipDate,decimal? subtotal,decimal? salesTaxPercentage,decimal? salesTaxTotal,decimal? appliedAmount,decimal? balanceRemaining,String memo,bool isPaid,bool isToBePrinted,decimal? discountLineAmount,decimal? discountLineRatePercent,bool discountLineIsTaxable,int? discountLineAccountId,decimal? salesTaxLineAmount,decimal? salesTaxLineRatePercent,int? salesTaxLineAccountId,decimal? shippingLineAmount,int? shippingLineAccountId,bool isCustomerTaxable,bool taxCalculationType
		  )
		  {

		  
			  m_account = account;
		  
			  m_customer = customer;
		  
			  m_entityState = entityState;
		  
			  m_terms = terms;
		  
			  m_invoiceId = invoiceId;
		  
			  m_quickBooksTxnId = quickBooksTxnId;
		  
			  m_editSequence = editSequence;
		  
			  m_timeCreated = timeCreated;
		  
			  m_timeModified = timeModified;
		  
			  m_txnNumber = txnNumber;
		  
			  m_aRAccountId = aRAccountId;
		  
			  m_txnDate = txnDate;
		  
			  m_refNumber = refNumber;
		  
			  m_billAddr1 = billAddr1;
		  
			  m_billAddr2 = billAddr2;
		  
			  m_billAddr3 = billAddr3;
		  
			  m_billAddr4 = billAddr4;
		  
			  m_billCity = billCity;
		  
			  m_billState = billState;
		  
			  m_billPostalCode = billPostalCode;
		  
			  m_billCountry = billCountry;
		  
			  m_shipAddr1 = shipAddr1;
		  
			  m_shipAddr2 = shipAddr2;
		  
			  m_shipAddr3 = shipAddr3;
		  
			  m_shipAddr4 = shipAddr4;
		  
			  m_shipCity = shipCity;
		  
			  m_shipState = shipState;
		  
			  m_shipPostalCode = shipPostalCode;
		  
			  m_shipCountry = shipCountry;
		  
			  m_dueDate = dueDate;
		  
			  m_shipDate = shipDate;
		  
			  m_subtotal = subtotal;
		  
			  m_salesTaxPercentage = salesTaxPercentage;
		  
			  m_salesTaxTotal = salesTaxTotal;
		  
			  m_appliedAmount = appliedAmount;
		  
			  m_balanceRemaining = balanceRemaining;
		  
			  m_memo = memo;
		  
			  m_isPaid = isPaid;
		  
			  m_isToBePrinted = isToBePrinted;
		  
			  m_discountLineAmount = discountLineAmount;
		  
			  m_discountLineRatePercent = discountLineRatePercent;
		  
			  m_discountLineIsTaxable = discountLineIsTaxable;
		  
			  m_discountLineAccountId = discountLineAccountId;
		  
			  m_salesTaxLineAmount = salesTaxLineAmount;
		  
			  m_salesTaxLineRatePercent = salesTaxLineRatePercent;
		  
			  m_salesTaxLineAccountId = salesTaxLineAccountId;
		  
			  m_shippingLineAmount = shippingLineAmount;
		  
			  m_shippingLineAccountId = shippingLineAccountId;
		  
			  m_isCustomerTaxable = isCustomerTaxable;
		  
			  m_taxCalculationType = taxCalculationType;
		  
		  }


	  
      #endregion

	
      }
      #endregion
      }

    