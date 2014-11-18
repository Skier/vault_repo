<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="account_info.ascx.cs" Inherits="Dpi.Central.Web.Account.Payment.AccountInfoControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="row" runat="server" id="rowAccountNumber"><span class="label">Account 
		Number</span> <span class="value">
		<asp:textbox id="txtAcctNumber" runat="server" Enabled="False" CssClass="account_number">123456789</asp:textbox></span></div>
<div class="row" runat="server" id="rowPhoneNumber"><span class="label">Phone Number</span>
	<span class="value">
		<dwc:phonenumberbox id="phnPhoneNumber" runat="server"></dwc:phonenumberbox><asp:customvalidator id="vldCstPhoneNumber" runat="server" ClientValidationFunction="ValidatePhoneNumber"
			ErrorMessage="<br>The Phone Number provided is invalid" ControlToValidate="phnPhoneNumber" Display="Dynamic"></asp:customvalidator></span></div>
<div class="row"><span class="label">First Name<IMG id="Img2" alt="" src="~/images/asterisk.gif" runat="server"></span>
	<span class="value">
		<asp:textbox id="txtFirstName" runat="server" CssClass="wide_field"></asp:textbox><asp:requiredfieldvalidator id="vldRfFirstName" runat="server" ErrorMessage="Required field cannot be left blank"
			ControlToValidate="txtFirstName" Display="Dynamic" EnableViewState="False"></asp:requiredfieldvalidator></span></div>
<div class="row"><span class="label">Last Name<IMG id="Img3" alt="" src="~/images/asterisk.gif" runat="server"></span>
	<span class="value">
		<asp:textbox id="txtLastName" runat="server" CssClass="wide_field"></asp:textbox><asp:requiredfieldvalidator id="vldRfLastName" runat="server" ErrorMessage="Required field cannot be left blank"
			ControlToValidate="txtLastName" Display="Dynamic" EnableViewState="False"></asp:requiredfieldvalidator></span></div>
<div class="row"><span class="label">Street Address<IMG id="Img4" alt="" src="~/images/asterisk.gif" runat="server"></span>
	<span class="value">
		<asp:textbox id="txtAddr" runat="server" CssClass="wide_field"></asp:textbox><asp:requiredfieldvalidator id="vldRfAddr" runat="server" ErrorMessage="Required field cannot be left blank"
			ControlToValidate="txtAddr" Display="Dynamic" EnableViewState="False"></asp:requiredfieldvalidator></span></div>
<div class="row"><span class="label">City, State, ZIP<IMG id="Img5" alt="" src="~/images/asterisk.gif" runat="server"></span>
	<span class="value">
		<asp:textbox id="txtCity" runat="server" width="61%"></asp:textbox><IMG height="1" alt="" src="..\..\blank.gif" width="3">
		<asp:dropdownlist id="lstState" runat="server" Width="19%">
			<asp:ListItem Selected="True"></asp:ListItem>
			<asp:ListItem Value="AL">AL</asp:ListItem>
			<asp:ListItem Value="AK">AK</asp:ListItem>
			<asp:ListItem Value="AZ">AZ</asp:ListItem>
			<asp:ListItem Value="AR">AR</asp:ListItem>
			<asp:ListItem Value="CA">CA</asp:ListItem>
			<asp:ListItem Value="CO">CO</asp:ListItem>
			<asp:ListItem Value="CT">CT</asp:ListItem>
			<asp:ListItem Value="DC">DC</asp:ListItem>
			<asp:ListItem Value="DE">DE</asp:ListItem>
			<asp:ListItem Value="FL">FL</asp:ListItem>
			<asp:ListItem Value="GA">GA</asp:ListItem>
			<asp:ListItem Value="HI">HI</asp:ListItem>
			<asp:ListItem Value="ID">ID</asp:ListItem>
			<asp:ListItem Value="IL">IL</asp:ListItem>
			<asp:ListItem Value="IN">IN</asp:ListItem>
			<asp:ListItem Value="IA">IA</asp:ListItem>
			<asp:ListItem Value="KS">KS</asp:ListItem>
			<asp:ListItem Value="KY">KY</asp:ListItem>
			<asp:ListItem Value="LA">LA</asp:ListItem>
			<asp:ListItem Value="ME">ME</asp:ListItem>
			<asp:ListItem Value="MD">MD</asp:ListItem>
			<asp:ListItem Value="MA">MA</asp:ListItem>
			<asp:ListItem Value="MI">MI</asp:ListItem>
			<asp:ListItem Value="MN">MN</asp:ListItem>
			<asp:ListItem Value="MS">MS</asp:ListItem>
			<asp:ListItem Value="MO">MO</asp:ListItem>
			<asp:ListItem Value="MT">MT</asp:ListItem>
			<asp:ListItem Value="NE">NE</asp:ListItem>
			<asp:ListItem Value="NV">NV</asp:ListItem>
			<asp:ListItem Value="NH">NH</asp:ListItem>
			<asp:ListItem Value="NJ">NJ</asp:ListItem>
			<asp:ListItem Value="NM">NM</asp:ListItem>
			<asp:ListItem Value="NY">NY</asp:ListItem>
			<asp:ListItem Value="NC">NC</asp:ListItem>
			<asp:ListItem Value="ND">ND</asp:ListItem>
			<asp:ListItem Value="OH">OH</asp:ListItem>
			<asp:ListItem Value="OK">OK</asp:ListItem>
			<asp:ListItem Value="OR">OR</asp:ListItem>
			<asp:ListItem Value="PA">PA</asp:ListItem>
			<asp:ListItem Value="RI">RI</asp:ListItem>
			<asp:ListItem Value="SC">SC</asp:ListItem>
			<asp:ListItem Value="SD">SD</asp:ListItem>
			<asp:ListItem Value="TN">TN</asp:ListItem>
			<asp:ListItem Value="TX">TX</asp:ListItem>
			<asp:ListItem Value="UT">UT</asp:ListItem>
			<asp:ListItem Value="VT">VT</asp:ListItem>
			<asp:ListItem Value="VA">VA</asp:ListItem>
			<asp:ListItem Value="WA">WA</asp:ListItem>
			<asp:ListItem Value="WV">WV</asp:ListItem>
			<asp:ListItem Value="WI">WI</asp:ListItem>
			<asp:ListItem Value="WY">WY</asp:ListItem>
		</asp:dropdownlist><IMG height="1" alt="" src="..\..\blank.gif" width="3">
		<asp:textbox id="txtZip" runat="server" width="16%" MaxLength="5"></asp:textbox><asp:requiredfieldvalidator id="vldRfCity" runat="server" ErrorMessage="<br>City is required field and cannot be left blank"
			ControlToValidate="txtCity" Display="Dynamic" EnableViewState="False" EnableClientScript="False"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="vldRfState" runat="server" ErrorMessage="<br>State is required field and cannot be left blank"
			ControlToValidate="lstState" Display="Dynamic" EnableViewState="False" EnableClientScript="False"></asp:requiredfieldvalidator><asp:requiredfieldvalidator id="vldRfZip" runat="server" ErrorMessage="<br>Zip is required field and cannot be left blank"
			ControlToValidate="txtZip" Display="Dynamic" EnableViewState="False" EnableClientScript="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="vldReZip" runat="server" ErrorMessage="<br>The Zip provided is invalid" ControlToValidate="txtZip"
			Display="Dynamic" EnableViewState="False" EnableClientScript="False" ValidationExpression="\d{5}"></asp:regularexpressionvalidator></span></div>
<div class="row"><span class="label">E-Mail<IMG id="Img6" alt="" src="~/images/asterisk.gif" runat="server"></span>
	<span class="value">
		<asp:textbox id="txtEmail" runat="server" EnableViewState="False" CssClass="wide_field"></asp:textbox><asp:requiredfieldvalidator id="vldRfEmail" runat="server" ErrorMessage="Required field cannot be left blank"
			ControlToValidate="txtEmail" Display="Dynamic" EnableViewState="False"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="vldReEmail" runat="server" ErrorMessage="The E-Mail provided is invalid" ControlToValidate="txtEmail"
			Display="Dynamic" EnableViewState="False" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:regularexpressionvalidator></span></div>
