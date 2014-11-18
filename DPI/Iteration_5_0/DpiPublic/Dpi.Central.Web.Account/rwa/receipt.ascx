<%@ Control Language="c#" AutoEventWireup="false" Codebehind="receipt.ascx.cs" Inherits="Dpi.Central.Web.Account.Wireless.Processes.Rwa.ReceiptControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div class="report_row"><span class="report_title">Account Information</span></div>
<div class="report_row"><SPAN class="report_label">Phone Number</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblPhoneNumber" runat="server">5642337744</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">Account Name</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblAcctName" runat="server">Nick Casey</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">PIN Number</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblPinNumber" runat="server">123412341234</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">Control Number</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblControlNumber" runat="server">DWEPG0025110</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">MSL</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblMsl" runat="server">19201</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">MSID</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblMsid" runat="server">00000-123456-1234</asp:label></SPAN></div>
<div class="spacer">&nbsp;</div>
<div class="report_row"><span class="report_title">Payor Information</span></div>
<div class="report_row"><SPAN class="report_label">Payor Name</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblPayorName" runat="server">DRAFORD SMITH</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">Street Address</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblStreetAddress" runat="server">244 E RED BAY RD</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">City State Zip</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblCityStateZip" runat="server">SUMTER SC 29150</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">E-Mail</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblEmail" runat="server">sergei.kalashnikov@gmail.com</asp:label></SPAN></div>
<div class="spacer">&nbsp;</div>
<div class="report_row"><span class="report_title">Payment Information</span></div>
<div class="report_row"><SPAN class="report_label">Approval/Confirmation Number</SPAN>
	<SPAN class="report_value">
		<asp:label id="m_lblConfirmationNumber" runat="server">2960956</asp:label></SPAN></div>
<div class="report_row"><span class="report_label">Processed</span> <SPAN class="report_value">
		<asp:label id="m_lblProcessed" runat="server">Yes</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">Payment Type</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblPaymentType" runat="server">Credit Card - VISA</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label"><asp:label id="m_lblCreditCardNumberCaption" runat="server">Credit Card Number</asp:label></SPAN><SPAN class="report_value"><asp:label id="m_lblCreditCardNumber" runat="server">*************1234</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">Sub Total</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblSubTotal" runat="server">$50.00</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">Taxes, Fees and Surcharges</SPAN>
	<SPAN class="report_value">
		<asp:label id="m_lblTaxes" runat="server">$12</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label"> Payment Amount</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblTotalAmountDue" runat="server">$50.00</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">Payment Date</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblPaymentDate" runat="server">02/22/2007 07:31</asp:label></SPAN></div>
<div class="spacer">&nbsp;</div>
<div class="report_row"><span class="report_title">Rate Schedule</span></div>
<div class="report_row"><SPAN class="report_label">Directory Assistance</SPAN> <SPAN class="report_value">
		10 Minutes for every 1 real time minute or $1.99 if anytime minutes are 
		unavailable.</SPAN>
</div>
<div class="report_row"><SPAN class="report_label">3G Web Services</SPAN> <SPAN class="report_value">
		1 Minute for every 22 kilobytes or 35 cents per kilobyte.</SPAN>
</div>
<div class="report_row"><SPAN class="report_label">Ringtones</SPAN> <SPAN class="report_value">
		Cost will vary from $1.99 to $3.00 depending on Ringtone.</SPAN>
</div>
<div class="report_row"><SPAN class="report_label">Text Messages</SPAN> <SPAN class="report_value">
		1 Minute for every message or 10 cents if anytime minutes are not available.</SPAN>
</div>
<div class="report_row"><SPAN class="report_label">Push to Talk</SPAN> <SPAN class="report_value">
		Unlimited Usage (Not applicable for Cash Card and International products.)</SPAN>
</div>
<div class="spacer">&nbsp;</div>
<div class="report_row"><span class="report_title">Notes</span></div>
<div class="report_row"><SPAN class="report_text">Billing cycles start on the day of 
		activation and run for 30 days for Monthly plans and 7 days for Weekly plans. 
		More than one Plan may be purchased within the billing cycle. Unlimited Nights 
		for Monthly and Weekly Plans are from 9 PM to 7 AM Monday – Thursday. Unlimited 
		Weekends for Monthly and Weekly Plans are from 9 PM Friday to 7 AM Monday. 
		Monthly and Weekly Plans provide Unlimited nights and weekends that will stay 
		active with at least 1 anytime minute remaining. Cash Cards do not prevent the 
		account from terminating according to the Monthly or Weekly Plan Terms. 
		Remaining Cash Card balances carry over with the replenishment of a New Monthly 
		or Weekly Plan.</SPAN>
</div>
<div class="spacer">&nbsp;</div>
<div class="report_row"><span class="report_title">Hours of Operation</span></div>
<div class="report_row"><SPAN class="report_label">dPi Wireless Automated Line:
		<asp:label id="m_lblRechargePhoneNumber" runat="server">1-800-314-1630</asp:label>
		24 Hours 7 Days a Week.<br>
		dPi Wireless Customer Care: 1-800-350-4009<br>
		Monday – Friday 8:00 am to 4:00 pm CST. </SPAN>
</div>
<div class="spacer">&nbsp;</div>
<div class="report_row"><SPAN class="report_foot">To contact dPi Wireless Customer Care 
		Please call 1-800-350-4009.<br>
		customerservice@dpiteleconnect.com<br>
		<br>
		Retain this copy for your records<br>
		Thank you for choosing dPi Wireless.</SPAN>
</div>
