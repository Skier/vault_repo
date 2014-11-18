using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI;
using QuickBooksAgent.Windows.UI.Controls;
using QuickBooksAgent.Windows.UI.CustomerOperations.Invoice;

namespace QuickBooksAgent.Windows.UI.CustomerOperations.InvoiceSelection
{
    public delegate void InvoiceAffectHandler(QuickBooksAgent.Domain.Invoice customer);

    public class InvoiceSelectionModel : ITableModel
    {
        List<QuickBooksAgent.Domain.Invoice> m_list;

        #region Customer
        private Customer m_customer;

        public Customer Customer
        {
            get { return m_customer; }
        }

        #endregion

        #region ModifiedCustomer

        private Customer m_modifiedOrOriginalCustomer;
        public Customer ModifiedOrOriginalCustomer
        {
            get { return m_modifiedOrOriginalCustomer; }
        }

        #endregion       

        #region Company

        private Company m_company;
        public Company Company
        {
            get { return m_company; }
            set { m_company = value; }
        }

        #endregion

        #region Current
        private QuickBooksAgent.Domain.Invoice m_invoice;

        public QuickBooksAgent.Domain.Invoice Current
        {
            get { return m_invoice; }
            set { m_invoice = value; }
        }

        #endregion

        #region IModel Members

        public void Init(Customer customer)
        {
            m_customer = customer;

            m_list = QuickBooksAgent.Domain.Invoice.FindByCustomer(
                customer.CustomerId);
            
            List<Company> companies = Domain.Company.Find();
            
            if (companies == null || companies.Count == 0)
                throw new Exception("Company not found");

            m_company = companies[0];

            try
            {
                m_modifiedOrOriginalCustomer = Customer.FindByModifiedCustomerId(m_customer.CustomerId);
            }
            catch (Exception)
            {
                m_modifiedOrOriginalCustomer = m_customer;
            }
            
            if (Change != null)
                Change.Invoke();
        }

        #endregion

        #region ITableModel Members

        public int GetRowCount()
        {
            return m_list.Count;
        }

        public int GetColumnCount()
        {
            return 2;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return "Invoice #";

            return "Date";
        }

        public Type GetColumnClass(int columnIndex)
        {
            if (columnIndex == 0)
                return typeof(String);

            return typeof(DateTime);
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
                return m_list[rowIndex].RefNumber ?? "(No number)";
            else
                if (m_list[rowIndex].TxnDate == null)
                    return string.Empty;
                else
                    return m_list[rowIndex].TxnDate.Value.ToString("yyyy-MM-dd");
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {

        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_list[rowIndex];
        }

        public event TableModelChangeHandler Change;

        #endregion

        #region Update

        internal int Update(QuickBooksAgent.Domain.Invoice invoice)
        {
            for (int i = 0; i <= m_list.Count - 1; i++)
            {
                if (m_list[i].InvoiceId == invoice.InvoiceId)
                {
                    m_list[i] = invoice;
                    return i;
                }
            }

            m_list.Add(invoice);

            if (Change != null)
                Change.Invoke();

            return m_list.Count - 1;
        }

        #endregion

        #region DeleteInvoice

        public void DeleteInvoice()
        {
            if (Current == null) return;

            try
            {
                Database.Begin();

                InvoiceLine.DeleteByInvoice(Current.InvoiceId);
                Domain.Invoice.Delete(Current);

                m_list.Remove(Current);

                this.Current = null;

                Database.Commit();
            }
            catch (Exception e)
            {
                Database.Rollback();

                throw e;
            }

            if (Change != null)
                Change.Invoke();
        }

        #endregion        

        #region GetInvoiceEmail
        
        public string GetInvoiceEmailBody()
        {            
            return string.Format(@"Dear {0},

Your invoice is attached. Please remit payment at your earliest convenience.
Thank you for your business - we appreciate it very much.

Sincerely,
{1}", Customer.PrintAs, Company.CompanyName);
        }

        public string GetInvoiceEmailAttachment()
        {            
            ///////////Format company address////////////////////
            List<string> companyAddress = new List<string>();
            if (Company.ForCustomerAddr1 != null && Company.ForCustomerAddr1 != string.Empty)
                companyAddress.Add(Company.ForCustomerAddr1);
            if (Company.ForCustomerAddr2 != null && Company.ForCustomerAddr2 != string.Empty)
                companyAddress.Add(Company.ForCustomerAddr2);
            if (Company.ForCustomerAddr3 != null && Company.ForCustomerAddr3 != string.Empty)
                companyAddress.Add(Company.ForCustomerAddr3);
            if (Company.ForCustomerAddr4 != null && Company.ForCustomerAddr4 != string.Empty)
                companyAddress.Add(Company.ForCustomerAddr4);

            string cityStateZip = string.Empty;
            cityStateZip += Company.ForCustomerCity;

            if (Company.ForCustomerState != null && Company.ForCustomerState != string.Empty)
            {
                if (cityStateZip == string.Empty)
                    cityStateZip += Company.ForCustomerState;
                else
                    cityStateZip += ",&nbsp;" + Company.ForCustomerState;
            }

            if (Company.ForCustomerPostalCode != null && Company.ForCustomerPostalCode != string.Empty)
            {
                if (cityStateZip == string.Empty)
                    cityStateZip += Company.ForCustomerPostalCode;
                else
                    cityStateZip += "&nbsp;" + Company.ForCustomerPostalCode;
            }          
            
            companyAddress.Add(cityStateZip);
            companyAddress.Add(Company.CompanyEmailForCustomer);

            string companyAddressString = string.Empty;
            foreach (string s in companyAddress)
            {
                companyAddressString += s + "<br>";
            }
            ///////////Format company address////////////////////                        
            string termsName = "(Unavailable)";
            if (Current.Terms != null)
            {
                if (Current.Terms.Name == null || Current.Terms.Name == string.Empty)
                    termsName = Terms.FindByPrimaryKey(Current.Terms.TermsId).Name;
                 else
                    termsName = Current.Terms.Name;
            }

            
            ////////////BILL TO//////////////////
            List<string> billToAddress = new List<string>();
            billToAddress.Add(Customer.Name);
            
            if (Customer.CompanyName != null && Customer.CompanyName != string.Empty)
                billToAddress.Add(Customer.CompanyName);            
            
            if (Current.BillAddr1 != null && Current.BillAddr1 != string.Empty)
                billToAddress.Add(Current.BillAddr1);
            if (Current.BillAddr2 != null && Current.BillAddr2 != string.Empty)
                billToAddress.Add(Current.BillAddr2);
            if (Current.BillAddr3 != null && Current.BillAddr3 != string.Empty)
                billToAddress.Add(Current.BillAddr3);
            if (Current.BillAddr4 != null && Current.BillAddr4 != string.Empty)
                billToAddress.Add(Current.BillAddr4);

            cityStateZip = string.Empty;
            cityStateZip += Current.BillCity;

            if (Current.BillState != null && Current.BillState != string.Empty)
            {
                if (cityStateZip == string.Empty)
                    cityStateZip += Current.BillState;
                else
                    cityStateZip += ",&nbsp;" + Current.BillState;
            }

            if (Current.BillPostalCode != null && Current.BillPostalCode != string.Empty)
            {
                if (cityStateZip == string.Empty)
                    cityStateZip += Current.BillPostalCode;
                else
                    cityStateZip += "&nbsp;" + Current.BillPostalCode;
            }          
            
            billToAddress.Add(cityStateZip);

            string billToString = string.Empty;
            foreach (string s in billToAddress)
            {
                billToString += s + "<br>";
            }            
            ////////////BILL TO//////////////////
            
            //Invoice Lines
            string invoiceLinesString = string.Empty;
            List<InvoiceLine> invoiceLines = InvoiceLine.FindBy(Current.InvoiceId);
            foreach (InvoiceLine line in invoiceLines)
            {                
                invoiceLinesString += "<tr>";
                invoiceLinesString += string.Format("<td width='14%'><font face='Courier New' size='2'>{0}</font></td>",
                    line.ServiceDate == null ? string.Empty : line.ServiceDate.Value.ToString("MM/dd/yyyy"));
                
                string activity = string.Empty;
                if (line.Item != null)
                {
                    activity = line.Item.Name;
                }

                if (line.LineDescription != null && line.LineDescription != string.Empty)
                {
                    if (activity == string.Empty)
                        activity += line.LineDescription;
                    else
                        activity += " - " + line.LineDescription;
                }
                
                invoiceLinesString += string.Format("<td width='38%'><font face='Courier New' size='2'>{0}</font></td>",
                    activity);
                invoiceLinesString +=
                    string.Format("<td width='17%'><p align='right'><font face='Courier New' size='2'>{0}</font></td>",
                                  line.Quantity == null ? string.Empty : QBDataType.RoundTripFormat(line.Quantity));
                
                string rateString = string.Empty;
                if (line.Rate != null)
                    rateString = QBDataType.RoundTripFormat(line.Rate);
                else if (line.RatePercent != null)
                    rateString = QBDataType.RoundTripFormat(line.RatePercent) + "%";
                                
                invoiceLinesString +=
                    string.Format("<td width='14%'><p align='right'><font face='Courier New' size='2'>{0}</font></td>",
                                  rateString);
                
                
                invoiceLinesString +=
                    string.Format("<td width='14%'><p align='right'><font face='Courier New' size='2'>{0}</font></td>",
                                  line.Amount == null ? 0.ToString("C") : line.Amount.Value.ToString("C"));
                
                invoiceLinesString += "</tr>";
            }

            List<InvoiceLine> serviceLines = InvoiceLine.FindServiceLinesBy(Current.InvoiceId);

            decimal subtotal = decimal.Zero;
            foreach (InvoiceLine line in invoiceLines)
            {
                subtotal += line.Amount ?? 0;
            }
            
            //////////////Discounts/////////////////
            string discountPercentString = "0%";
            decimal discountAmount = 0;
            if (Current.EntityState == EntityState.Synchronized)
            {
                InvoiceLine discount = null;
                foreach (InvoiceLine line in serviceLines)
                {
                    if (line.LineDescription == "Discount")
                    {
                        discount = line;
                        break;
                    }                        
                }
                                 
                if (discount != null)
                {
                    if (discount.Rate != null)
                    {
                        decimal rate;
                        if (discount.Rate < 0)
                            rate = -100*discount.Rate.Value;
                        else
                            rate = 100*discount.Rate.Value;

                        if (decimal.Truncate(rate) == rate)
                            discountPercentString = decimal.ToInt32(rate).ToString();                                                        
                        else
                            discountPercentString = QBDataType.RoundTripFormat(rate);

                        if (discountPercentString != string.Empty)
                            discountPercentString += "%";
                    }
                    discountAmount = discount.Amount ?? 0;
                }                
            } else
            {
                discountPercentString = QBDataType.RoundTripFormat(Current.DiscountLineRatePercent);
                if (discountPercentString != string.Empty)
                    discountPercentString += "%";
                
                discountAmount = Current.DiscountLineAmount ?? 0;
            }
            //////////////Discounts/////////////////

            
            /////////////Tax////////////////
            string taxPercent = "0%";
            decimal taxAmount = 0;
            if (Current.EntityState == EntityState.Synchronized)
            {
                InvoiceLine tax = null;
                foreach (InvoiceLine line in serviceLines)
                {
                    if (line.LineDescription == "Tax")
                    {
                        tax = line;
                        break;
                    }                        
                }                
                
                if (tax != null)
                {                    
                    taxPercent = QBDataType.RoundTripFormat(tax.RatePercent);

                    if (taxPercent != string.Empty)
                        taxPercent += "%";
                    
                    taxAmount = tax.Amount ?? 0;
                }

            } else
            {            
                taxPercent = QBDataType.RoundTripFormat(Current.SalesTaxLineRatePercent);
                if (taxPercent != string.Empty)
                    taxPercent += "%";
                
                taxAmount = Current.SalesTaxLineAmount ?? 0;
            }                        
            /////////////Tax////////////////

            /////////////Shipping////////////////
            decimal shippingAmount = 0;
            if (Current.EntityState == EntityState.Synchronized)
            {
                InvoiceLine shipping = null;
                foreach (InvoiceLine line in serviceLines)
                {
                    if (line.LineDescription == "Shipping")
                    {
                        shipping = line;
                        break;
                    }                        
                }
                
                if (shipping != null)
                {
                    shippingAmount = shipping.Amount ?? 0;
                }

            } else
            {
                shippingAmount = Current.ShippingLineAmount ?? 0;
            }                        
            /////////////Shipping////////////////

            decimal amountDue = decimal.Zero;
            if (Current.EntityState == EntityState.Synchronized)
                amountDue = Current.BalanceRemaining ?? 0;
            else
                amountDue = subtotal + discountAmount + taxAmount + shippingAmount;

            
            string output =
                string.Format(
                    @"<html>

<head>
<meta http-equiv='Content-Language' content='en-us'>
<meta http-equiv='Content-Type' content='text/html; charset=windows-1252'>
<title>Invoice</title>
</head>

<body>

<table border='0' width='630' id='table1'>{0}
	<tr>
		<td colspan='4'>
		<p align='center'><font face='Courier New' 
size='2'>---------------------------------- Invoice ---------------------------------</font></td>
	</tr>
	<tr>
		<td width='114'>&nbsp;</td>
		<td width='506' colspan='3'>&nbsp;</td>
	</tr>
	<tr>
		<td colspan='4'>
		<p align='center'><font face='Courier New' size='2'>{1}</font></td>
	</tr>
	<tr>
		<td width='114'>&nbsp;</td>
		<td width='506' colspan='3'>&nbsp;</td>
	</tr>
	<tr>
		<td colspan='4'><font face='Courier New' size='2'>{2}</font></td>
	</tr>
	<tr>
		<td width='114'>&nbsp;</td>
		<td width='506' colspan='3'>&nbsp;</td>
	</tr>
	<tr>
		<td width='114'><font face='Courier New' 
size='2'>INVOICE&nbsp;#:</font></td>
		<td width='506' colspan='3'><font face='Courier New' 
size='2'>{3}</font></td>
	</tr>
	<tr>
		<td width='114'><font face='Courier New' size='2'>DATE:</font></td>
		<td width='506' colspan='3'><font face='Courier New' 
size='2'>{4}</font></td>
	</tr>
	<tr>
		<td width='114'><font face='Courier New' 
size='2'>DUE&nbsp;DATE:</font></td>
		<td width='506' colspan='3'><font face='Courier New' 
size='2'>{5}</font></td>
	</tr>
	<tr>
		<td width='114'><font face='Courier New' size='2'>TERMS:</font></td>
		<td width='506' colspan='3'><font face='Courier New' 
size='2'>{6}</font></td>
	</tr>
	<tr>
		<td width='114'><font face='Courier New' 
size='2'>AMOUNT&nbsp;DUE:</font></td>
		<td width='506' colspan='3'><font face='Courier New' 
size='2'>{7}</font></td>
	</tr>
	<tr>
		<td width='114'>&nbsp;</td>
		<td width='506' colspan='3'>&nbsp;</td>
	</tr>
	<tr>
		<td width='620' colspan='4'>
		<p align='center'><font face='Courier New' 
size='2'>-----------------------------------------------------------------------------</
font></td>
	</tr>
	<tr>
		<td width='114'><font face='Courier New' size='2'>BILL TO:</font></td>
		<td width='506' colspan='3'>&nbsp;</td>
	</tr>
	<tr>
		<td width='114'>&nbsp;</td>
		<td width='506' colspan='3'>&nbsp;</td>
	</tr>
	<tr>
		<td width='620' colspan='4'><font face='Courier New' size='2'>
		{8}
        </font></td>
	</tr>
	<tr>
		<td width='620' colspan='4'><font face='Courier New' 
size='2'>-----------------------------------------------------------------------------</font></td>
	</tr>
	<tr>
		<td width='114'>&nbsp;</td>
		<td width='506' colspan='3'>&nbsp;</td>
	</tr>
	<tr>
		<td width='620' colspan='4'>
		<table border='0' width='100%' id='table2'>
			<tr>
				<td colspan='5' nowrap><font face='Courier New' 
size='2'><u>&nbsp; 
				
Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp; 
				
Activity&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
				Quantity&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
Rate&nbsp;&nbsp;&nbsp;&nbsp; 
				Amount&nbsp; </u></font></td>
			</tr>
			{9}
			<tr>
				<td width='14%'>&nbsp;</td>
				<td width='38%'>&nbsp;</td>
				<td width='17%'>&nbsp;</td>
				<td width='14%'>&nbsp;</td>
				<td width='14%'>&nbsp;</td>
			</tr>
		</table>
		</td>
	</tr>
	<tr>
		<td width='114'>&nbsp;</td>
		<td width='206'>&nbsp;</td>
		<td width='190' align='right'><font face='Courier New' size='2'>
		SUBTOTAL:</font></td>
		<td width='104' align='right'><font face='Courier New' 
size='2'>{10}</font></td>
	</tr>
	<tr>
		<td width='114'>&nbsp;</td>
		<td width='206'>&nbsp;</td>
		<td width='190' align='right'><font face='Courier New' size='2'>
		DISCOUNT{11}:</font></td>
		<td width='104' align='right'><font face='Courier New' 
size='2'>{12}</font></td>
	</tr>
	<tr>
		<td width='114'>&nbsp;</td>
		<td width='206'>&nbsp;</td>
		<td width='190' align='right'><font face='Courier New' size='2'>
		TAX{13}:</font></td>
		<td width='104' align='right'><font face='Courier New' 
size='2'>{14}</font></td>
	</tr>
	<tr>
		<td width='114'>&nbsp;</td>
		<td width='206'>&nbsp;</td>
		<td width='190' align='right'><font face='Courier New' size='2'>
		SHIPPING:</font></td>
		<td width='104' align='right'><font face='Courier New' 
size='2'>{15}</font></td>
	</tr>
	<tr>
		<td width='114'>&nbsp;</td>
		<td width='206'>&nbsp;</td>
		<td width='296' colspan='2' align='right'>
		<font face='Courier New' 
size='2'>-------------------------------------</font></td>
	</tr>
	<tr>
		<td width='114'>&nbsp;</td>
		<td width='206'>&nbsp;</td>
		<td width='190' align='right'>
		<p align='right'><font face='Courier New' size='2'>TOTAL:</font></td>
		<td width='104' align='right'><font face='Courier New' 
size='2'>{16}</font></td>
	</tr>
	<tr>
		<td width='620' colspan='4'>&nbsp;</td>
	</tr>
	<tr>
		<td width='620' colspan='4'><font face='Courier New' size='2'>{17}</font></td>
	</tr>
</table>

</body>

</html>",
                    string.Empty, Company.CompanyName,
                    companyAddressString,
                    string.IsNullOrEmpty(Current.RefNumber) ? "(Unavailable)" : Current.RefNumber,
                    Current.TxnDate == null ? "(Unavailable)" : Current.TxnDate.Value.ToString("MM/dd/yyyy"),
                    Current.DueDate == null ? "(Unavailable)" : Current.DueDate.Value.ToString("MM/dd/yyyy"),
                    termsName,
                    amountDue.ToString("C"),
                    billToString,
                    invoiceLinesString,
                    subtotal.ToString("C"),
                    discountPercentString == string.Empty ? string.Empty : string.Format("&nbsp;({0})", discountPercentString),
                    discountAmount.ToString("C"),
                    taxPercent == string.Empty ? string.Empty : string.Format("&nbsp;({0})", taxPercent),
                    taxAmount.ToString("C"),
                    shippingAmount.ToString("C"),
                    (subtotal + discountAmount + taxAmount + shippingAmount).ToString("C"),
                    Current.Memo);
            
            return output;
        }

        #endregion
    }
}
