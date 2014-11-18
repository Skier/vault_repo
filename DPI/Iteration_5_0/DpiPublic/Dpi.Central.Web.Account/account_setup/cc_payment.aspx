<%@ Page language="c#" Codebehind="cc_payment.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.AccountSetup.CreditCardPaymentPage" %>
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
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body runat="server" id="_body">
		<form id="creditCardPaymentForm" method="post" runat="server">
			<a href="http://www.verisign.com" target="_blank"><IMG title="Open in new window" alt="" src="../images/verisign_logo.png" class="verisign_logo">
			</a>
			<div class="process_form">
				<div class="step_caption">
					Please make credit card payment
				</div>
				<div class="process_form_section">
					<duc:accountinfo id="ctrlAccountInfo" runat="server"></duc:accountinfo>
					<duc:creditcardinfo id="ctrlCreditCardInfo" runat="server"></duc:creditcardinfo>
					<div class="row">
						<span class="label">Payment Due<IMG alt="" src="../images/asterisk.gif"></span> <span class="value">
							<asp:textbox id="txtPaymentDue" CssClass="wide_field" runat="server" Enabled="False"></asp:textbox>
						</span>
					</div>
					<br>
					<div class="row">
						<span class="label"><p>Recurring Payment</p></span> 
						<span class="value">
							<asp:CheckBox id="chkRecurringPayment" runat="server" Text="Use this credit card to process monthly recurring payments"></asp:CheckBox>
						</span>
					</div>
					<br>
					<div class="row"><SPAN class="statement">By selecting the check box above, you are 
							agreeing to allow dPi Teleconnect to process a payment on a monthly basis by 
							using the Credit Card information you provided today. We will automatically 
							process the payment every month on the Due Date indicated on your monthly 
							statement. A confirmation will be sent to the email address provided verifying 
							if the payment is accepted or denied.</SPAN><SPAN class="statement">
							<br>
							If you decide to stop enrollment in the Recurring Payment process, you may 
							disable this online by logging into your account and selecting to deactivate 
							the Credit Card entry currently in an Active Status. Or you may contact our 
							Customer Service team to process this request. You are able to manage your 
							recurring payment method from your Online Account at any time. </SPAN>
					</div>
				</div>
				<div class="button_row">
					<span class="back_button">
						<asp:imagebutton id="m_btnBack" runat="server" ImageUrl="../images/btn_back.gif" EnableViewState="False"
							CausesValidation="False"></asp:imagebutton>
					</span><span class="next_button">
						<asp:imagebutton id="btnPay" runat="server" EnableViewState="False" ImageUrl="../images/btn_process_transaction.gif"></asp:imagebutton>
					</span>
				</div>
				<div runat="server" id="detailsDiv" style="DISPLAY:none">
					<div class="row">
						<span class="label_note">* CVV2 Security Code 
							Example:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><br>
						<IMG alt="CVV2 Security Code Example" src="../images/cvv2a.gif">
					</div>
				</div>
			</div>
		</form>
	</body>
</HTML>
