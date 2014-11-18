<%@ Page language="c#" Codebehind="check_payment.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Payment.CheckPaymentPage" %>
<%@ Register TagPrefix="duc" TagName="CheckInfo" Src="~/account/payment/check_info.ascx" %>
<%@ Register TagPrefix="duc" TagName="AccountInfo" Src="~/account/payment/account_info.ascx" %>
<%@ Register TagPrefix="tb" TagName="Tabs" Src="~/account/tabs.ascx" %>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Check Payment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="checkPaymentForm" method="post" runat="server">
			<div class="form">
				<div class="tab_row">
					<tb:Tabs id="m_tabs" runat="server"></tb:Tabs>
				</div>
				<duc:accountinfo id="ctrlAccountInfo" runat="server"></duc:accountinfo>
				<duc:checkinfo id="ctrlCheckInfo" runat="server"></duc:checkinfo>
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
						<span class="label_note">By clicking Process Transaction, you authorize an 
							electronic debit from your checking account that will be processed through the 
							regular banking system. If your full order is not available at the same time, 
							you authorize partial debits to your account, not to exceed the total 
							authorized amount. The partial debits will take place upon each shipment of 
							partial goods. If any of your payments are returned unpaid, you will be charged 
							a returned item fee up to the maximum allowed by law. </span>
					</div>
					<div class="row">
						<span class="label_note">Check Example:</span><br>
						<IMG alt="Check description" src="../../images/checkdesc.gif">
					</div>
				</div>
			</div>
		</form>
	</body>
</HTML>
