<%@ Control Language="c#" AutoEventWireup="false" Codebehind="account_summary.ascx.cs" Inherits="Dpi.Central.Web.Account.AccountSetup.AccountSummaryControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div class="report_row"><span class="report_title">Account Information</span></div>
<div class="report_row"><SPAN class="report_label">Account Number</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblAcctNumber" runat="server">56423344</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">Account Name</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblAcctName" runat="server">Nick Casey</asp:label></SPAN></div>
<div class="report_row" id="m_rowLowIncomeLink" runat="server"><SPAN class="report_label">For 
		Lifeline/Link-Up qualification and required documentation please click here </SPAN>
	<SPAN class="report_value"><A href="javascript:OpenLifeLineApplication()">Lifeline 
			Application</A> </SPAN>
</div>
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
<div class="report_row"><span class="report_title">Product Information</span></div>
<asp:Repeater id="productRepeater" runat="server">
	<ItemTemplate>
		<div class="report_row">
			<SPAN class="report_label">
				<%# DataBinder.Eval(Container.DataItem, "Key") %>
			</SPAN><SPAN class="report_value">
				<%# DataBinder.Eval(Container.DataItem, "Value", "{0:c}") %>
			</SPAN>
		</div>
	</ItemTemplate>
</asp:Repeater>
<div class="report_row" runat="server" id="termsAndConditionsDiv">
	<div class="spacer">&nbsp;</div>
	<SPAN class="report_terms_and_conditions">Your use of iZoomOnline's services 
		through the software is also subject to iZoomOnline’s Terms of Use<br>
		as amended from time to time by iZoomOnline and located at 
		www.izoomonline.com/company.<br>
		Technical Support contact information:
		<br>
		Customer Service: 877-4-ZOOMERS (877-496-6637)
		<br>
		Web Site: www.izoomonline.com
		<br>
		Email: support@izoomonline.com<br>
	</SPAN>
</div>
<div class="spacer">&nbsp;</div>
<div class="report_row"><span class="report_title">Payment Information</span></div>
<div class="report_row"><SPAN class="report_label">Payment Type</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblPaymentType" runat="server">Credit Card - VISA</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label"><asp:label id="m_lblCreditCardNumberCaption" runat="server">Credit Card Number</asp:label></SPAN><SPAN class="report_value"><asp:label id="m_lblCreditCardNumber" runat="server">*************1234</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">Payment Amount</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblPaymentAmount" runat="server">$50.00</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">Payment Date</SPAN> <SPAN class="report_value">
		<asp:label id="m_lblPaymentDate" runat="server">02/22/2007 07:31</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">Approval/Confirmation Number</SPAN>
	<SPAN class="report_value">
		<asp:label id="m_lblConfirmationNumber" runat="server">2960956</asp:label></SPAN></div>
<div class="report_row"><SPAN class="report_label">Taxes, Fees and Surcharges</SPAN>
	<SPAN class="report_value">
		<asp:label id="m_lblTaxes" runat="server">$12</asp:label></SPAN></div>
<div class="spacer">&nbsp;</div>
<div class="report_row"><SPAN class="report_foot">Thank You for joining dPi 
		Teleconnect. Your account has been successfully created.<br>
		Please make note of your dPi Teleconnect Account Number for your future 
		reference.<br>
		If you have any questions please feel free to contact our Customer Service team 
		by calling 1-800-350-4009<br>
		or send us an email at customerservice@dpiteleconnect.com<br>
		<br>
		Within 3 to 5 business days you can access your account to obtain your 
		Activation date and Telephone Number.<br>Please allow up to 7 business days for 
		service connection.<br><br>
		<span runat="server" id="starterKitSpan">A Starter Kit containing an Installation 
			CD will be mailed to you upon Activation of your telephone service with dPi 
			Teleconnect.<br>
			Please allow 3-5 business days from the date of Activation to receive the 
			Starter Kit.</span> </SPAN>
</div>
