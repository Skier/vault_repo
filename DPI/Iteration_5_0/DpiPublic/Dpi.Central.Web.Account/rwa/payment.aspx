<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Register TagPrefix="duc" TagName="CreditCardInfo" Src="~/account/payment/cc_info.ascx" %>
<%@ Register TagPrefix="duc" TagName="CheckInfo" Src="~/account/payment/check_info.ascx" %>
<%@ Register TagPrefix="duc" TagName="AccountInfo" Src="~/account/payment/account_info.ascx" %>
<%@ Page language="c#" Codebehind="payment.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Wireless.Processes.Rwa.PaymentPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Make Payment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
		<script language="javascript" type="text/javascript">
			var effectDuration = 0.3;
			
			function ShowCreditCard()
			{
				if (!isVisible('divCreditCard'))
					new Effect.Appear('divCreditCard', {duration: effectDuration});
				
				if (isVisible('divCheck'))
					new Effect.Fade('divCheck', {duration: effectDuration});
					
				AdjustAppearance();
			}
			
			function ShowCheck() 
			{
				if (isVisible('divCreditCard'))
					new Effect.Fade('divCreditCard', {duration: effectDuration});
					
				if (!isVisible('divCheck'))
					new Effect.Appear('divCheck', {duration: effectDuration});
					
				AdjustAppearance();
			}
			
			function AdjustAppearance() 
			{
				if (!isVisible('divButtonsRow'))
					new Effect.Appear('divButtonsRow', {duration: effectDuration});
					
				if (isVisible('divCreditCardDetails'))
					new Effect.Fade('divCreditCardDetails', {duration: effectDuration});
					
				if (isVisible('divCheckDetails'))
					new Effect.Fade('divCheckDetails', {duration: effectDuration});
			}		
		</script>
	</HEAD>
	<body id="_body" runat="server">
		<form id="creditCardPaymentForm" method="post" runat="server">
			<a href="http://www.verisign.com" target="_blank"><IMG class="verisign_logo" title="Open in new window" alt="" src="../images/verisign_logo.png">
			</a>
			<div class="process_form">
				<div class="step_caption">Please enter the following payment information
				</div>
				<div class="process_form_section">
					<duc:accountinfo id="ctrlAccountInfo" runat="server"></duc:accountinfo>
					<div class="row"><span class="label">Payment Type<IMG alt="" src="../images/asterisk.gif"></span>
						<span class="value">
							<TABLE cellSpacing="0" cellPadding="0" border="0">
								<TR class="radiogroup">
									<TD noWrap><asp:radiobutton id="rbCreditCard" runat="server" Text="Credit Card" GroupName="PaymentType"></asp:radiobutton></TD>
									<TD noWrap><asp:radiobutton id="rbCheck" runat="server" Text="Check" GroupName="PaymentType"></asp:radiobutton></TD>
								</TR>
							</TABLE>
						</span>
					</div>
					<div class="row"><span class="label_note">(Choose the payment type by which you want to 
							replenish the phone number)</span>
					</div>
					<div id="divCreditCard" style="DISPLAY: none" runat="server"><duc:creditcardinfo id="ctrlCreditCardInfo" runat="server"></duc:creditcardinfo></div>
					<div id="divCheck" style="DISPLAY: none" runat="server"><duc:checkinfo id="ctrlCheckInfo" runat="server"></duc:checkinfo></div>
					<div class="row"><span class="label">Payment Amount<IMG alt="" src="../images/asterisk.gif"></span>
						<span class="value">
							<asp:textbox id="txtPaymentAmount" runat="server" CssClass="wide_field"></asp:textbox><asp:requiredfieldvalidator id="vldRfAmt" runat="server" Display="Dynamic" ErrorMessage="<br>Required field cannot be left blank"
								ControlToValidate="txtPaymentAmount"></asp:requiredfieldvalidator><asp:customvalidator id="vldCstPaymentAmount" runat="server" Display="Dynamic" ErrorMessage="CustomValidator"
								ControlToValidate="txtPaymentAmount"></asp:customvalidator></span></div>
				</div>
				<div class="button_row" id="divButtonsRow" style="DISPLAY: none" runat="server">
					<span class="back_button"><asp:imagebutton id="btnPrevious" runat="server" ImageUrl="~/images/btn_back.gif"></asp:imagebutton></span>
					<span class="next_button"><asp:imagebutton id="btnPay" runat="server" EnableViewState="False" ImageUrl="~/images/btn_process_transaction.gif"></asp:imagebutton></span></div>
				<div id="divCreditCardDetails" style="MARGIN-TOP: 10px; DISPLAY: none" runat="server">
					<div class="row"><span class="label_note">* CVV2 Security Code 
							Example:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span><br>
						<IMG alt="CVV2 Security Code Example" src="../images/cvv2a.gif">
					</div>
				</div>
				<div id="divCheckDetails" style="MARGIN-TOP: 10px; DISPLAY: none" runat="server">
					<div class="row"><span class="label_note">By clicking Process Transaction, you 
							authorize an electronic debit from your checking account that will be processed 
							through the regular banking system. If your full order is not available at the 
							same time, you authorize partial debits to your account, not to exceed the 
							total authorized amount. The partial debits will take place upon each shipment 
							of partial goods. If any of your payments are returned unpaid, you will be 
							charged a returned item fee up to the maximum allowed by law. </span>
					</div>
					<div class="row"><span class="label_note">Check Example:</span><br>
						<IMG alt="Check description" src="../images/checkdesc.gif">
					</div>
				</div>
			</div>
		</form>
	</body>
</HTML>
