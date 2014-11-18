<%@ Page language="c#" Codebehind="payment_selection.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Payment.PaymentSelectionPage" %>
<%@ Register TagPrefix="tb" TagName="Tabs" Src="~/account/tabs.ascx" %>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC Payment Selection</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="paymentSelectionForm" method="post" runat="server">
			<tb:Tabs id="m_tabs" runat="server"></tb:Tabs>
			<div class="form">
				<div class="row">
					<span class="label">Account Number</span> <span class="value">
						<asp:Label id="lblAcctNumber" runat="server">123456789</asp:Label>
					</span>
				</div>
				<div class="row">
					<span class="label">Phone Number</span> <span class="value">
						<asp:Label id="lblPhoneNumber" runat="server">123-456-7890</asp:Label>
					</span>
				</div>
				<div class="row">
					<span class="label">Name</span> <span class="value">
						<asp:Label id="lblAcctName" runat="server">Samuel Sopilka</asp:Label>
					</span>
				</div>
				<div class="row">
					<span class="label">Balance Forward</span><span class="value">
						<asp:Label id="lblBalForward" runat="server">$64.20</asp:Label>
					</span>
				</div>
				<div class="row">
					<span class="label_note">(Should be paid to avoid interruption of service)</span>
				</div>
				<div class="row">
					<span class="label">Charges Due on
						<asp:Label id="lblDueDate" runat="server">Nov, 10 2006</asp:Label></span> <span class="value">
						<asp:Label id="lblCurrentChargesAmt" runat="server">$80.02</asp:Label></span>
				</div>
				<div class="row">
					<span class="label">Payment Amount</span> <span class="value">
						<asp:textbox id="txtAmt" runat="server" Width="50">64.20</asp:textbox>
						<asp:RequiredFieldValidator id="vldRfAmt" runat="server" Display="Dynamic" ErrorMessage="<br>Required field cannot be left blank"
							ControlToValidate="txtAmt"></asp:RequiredFieldValidator>
						<asp:CustomValidator id="vldCstAmt" runat="server" Display="Dynamic" ErrorMessage="CustomValidator" ControlToValidate="txtAmt"></asp:CustomValidator>
					</span>
				</div>
			</div>
			<div class="wide_form">
				<div class="row" style="FLOAT: right">
					<asp:ImageButton ID="btnCheckPay" runat="server" ImageUrl="../../images/btn_pay_by_check.gif" AlternateText="Pay By Check"></asp:ImageButton>
					<img src="../../images/blank.gif">
					<asp:ImageButton ID="btnCreditCardPay" runat="server" ImageUrl="../../images/btn_pay_by_credit_card.gif"
						AlternateText="Pay By Credit Card"></asp:ImageButton>
				</div>
			</div>
		</form>
	</body>
</HTML>
