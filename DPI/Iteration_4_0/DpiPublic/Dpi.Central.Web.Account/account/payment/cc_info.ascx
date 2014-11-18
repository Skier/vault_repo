<%@ Control Language="c#" AutoEventWireup="false" Codebehind="cc_info.ascx.cs" Inherits="Dpi.Central.Web.Account.Payment.CreditCardInfoControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="row"><span class="label">Card Type<IMG alt="" src="~/images/asterisk.gif" runat="server"></span>
	<span class="value">
		<TABLE cellSpacing="0" cellPadding="0" border="0">
			<TR class="radiogroup">
				<TD noWrap><asp:radiobutton id="rbVisa" runat="server" Text=" "></asp:radiobutton><IMG id="imgVisa" height="16" alt="Visa" src="../../images/cc_visa.gif" width="25" border="0"
						runat="server">
				</TD>
				<TD noWrap><asp:radiobutton id="rbMasterCard" runat="server" Text=" "></asp:radiobutton><IMG id="imgMasterCard" height="16" alt="MasterCard" src="../../images/cc_mast.gif" width="25"
						border="0" runat="server">
				</TD>
				<TD noWrap><asp:radiobutton id="rbAmericanExpress" runat="server" Text=" "></asp:radiobutton><IMG id="imgAmericanExpress" height="16" alt="AmericanExpress" src="../../images/cc_amex.gif"
						width="25" border="0" runat="server">
				</TD>
				<TD noWrap><asp:radiobutton id="rbDiscover" runat="server" Text=" "></asp:radiobutton><IMG id="imgDiscover" height="16" alt="Discover" src="../../images/cc_disc.gif" width="25"
						border="0" runat="server">
				</TD>
			</TR>
		</TABLE>
		<asp:customvalidator id="vldCstCreditCardType" runat="server" ErrorMessage="Required field cannot be left blank"
			Display="Dynamic"></asp:customvalidator></span></div>
<div class="row"><span class="label">Card Number<IMG alt="" src="~/images/asterisk.gif" runat="server"></span>
	<span class="value">
		<asp:textbox id="txtCcNumber" runat="server" MaxLength="16" CssClass="wide_field"></asp:textbox><asp:requiredfieldvalidator id="vldRfCcNumber" runat="server" ErrorMessage="Required field cannot be left blank"
			Display="Dynamic" ControlToValidate="txtCcNumber"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="vldReCcNumber" runat="server" ErrorMessage="The Card Number provided is invalid"
			Display="Dynamic" ControlToValidate="txtCcNumber" ValidationExpression="\d+"></asp:regularexpressionvalidator><asp:customvalidator id="vldCstCcNumber" runat="server" ErrorMessage="The Card Number provided is invalid"
			Display="Dynamic" ControlToValidate="txtCcNumber"></asp:customvalidator></span></div>
<div class="row"><span class="label">Expiration Date<IMG alt="" src="~/images/asterisk.gif" runat="server"></span>
	<span class="value">
		<asp:dropdownlist id="lstExpMonth" runat="server" Width="49%"></asp:dropdownlist><IMG height="1" alt="" src="..\..\blank.gif" width="3">
		<asp:dropdownlist id="lstExpYear" runat="server" Width="49%"></asp:dropdownlist><asp:customvalidator id="vldCstExpDate" runat="server" ErrorMessage="<br>The Expiration Date provided is invalid"
			Display="Dynamic"></asp:customvalidator><asp:requiredfieldvalidator id="vldRfExpMonth" EnableClientScript="False" runat="server" ErrorMessage="<br>Month is required field and cannot be left blank"
			Display="Dynamic" ControlToValidate="lstExpMonth"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="vldRfExpYear" EnableClientScript="False" runat="server" ErrorMessage="<br>Year is required field and cannot be left blank"
			Display="Dynamic" ControlToValidate="lstExpYear"></asp:requiredfieldvalidator></span></div>
<div class="row"><span class="label">CVV2 Security Code<IMG alt="" src="~/images/asterisk.gif" runat="server"><br>
		<span class="label_note"><a href="javascript:void(0);" runat="server" id="lnkToggleDetails">
				(show/hide example)</a> </span></span><span class="value">
		<asp:textbox id="txtCvNumber" runat="server" CssClass="wide_field"></asp:textbox><asp:requiredfieldvalidator id="vldRfCvNumber" runat="server" ErrorMessage="Required field cannot be left blank"
			Display="Dynamic" ControlToValidate="txtCvNumber"></asp:requiredfieldvalidator></span></div>
