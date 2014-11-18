<%@ Page language="c#" Codebehind="cc_payment.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Payment.CreditCardPaymentPage" %>
<%@ Register TagPrefix="tb" TagName="Tabs" Src="~/account/tabs.ascx" %>
<%@ Register TagPrefix="duc" TagName="AccountInfo" Src="~/account/payment/account_info.ascx" %>
<%@ Register TagPrefix="duc" TagName="CreditCardInfo" Src="~/account/payment/cc_info.ascx" %>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC Credit Card Payment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="creditCardPaymentForm" method="post" runat="server">
			<tb:Tabs id="m_tabs" runat="server"></tb:Tabs>
			<div class="form">				
				<duc:accountinfo id="ctrlAccountInfo" runat="server"></duc:accountinfo>
				<duc:creditcardinfo id="ctrlCreditCardInfo" runat="server"></duc:creditcardinfo>
				<div class="row">
					<span class="label">Payment Due<IMG alt="" src="../../images/asterisk.gif"></span>
					<span class="value">
						<asp:textbox id="txtPaymentAmount" CssClass="wide_field" runat="server" Enabled="False"></asp:textbox>
					</span>
				</div>
				<div class="button_row">
					<span class="back_button">
						<asp:imagebutton id="btnBack" runat="server" ImageUrl="../../images/btn_back.gif" EnableViewState="False"
							CausesValidation="False"></asp:imagebutton>
					</span><span class="next_button">
						<asp:imagebutton id="btnPay" runat="server" EnableViewState="False" ImageUrl="../../images/btn_process_transaction.gif"
							AlternateText="Process Transaction"></asp:imagebutton>
					</span>
				</div>
				<div runat="server" id="detailsDiv" style="DISPLAY:none">
					<div class="row">
						<span class="label_note">* CVV2 Security Code 
							Example:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><br>
						<IMG alt="CVV2 Security Code Example" src="../../images/cvv2a.gif">
					</div>
				</div>				
			</div>
		</form>
	</body>
</HTML>
