<%@ Control Language="c#" AutoEventWireup="false" Codebehind="check_info.ascx.cs" Inherits="Dpi.Central.Web.Account.Payment.CheckInfoControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="row">
	<span class="label">Bank Account Number<IMG alt="" src="~/images/asterisk.gif" runat="server"></span>
	<span class="value">
		<asp:TextBox id="txtBankAccountNumber" runat="server" CssClass="wide_field" MaxLength="16"></asp:TextBox>
		<asp:RequiredFieldValidator id="vldRfBankAccountNumber" runat="server" ErrorMessage="Required field cannot be left blank"
			ControlToValidate="txtBankAccountNumber" Display="Dynamic"></asp:RequiredFieldValidator>
		<asp:RegularExpressionValidator id="vldReBankAccountNumber" runat="server" Display="Dynamic" ControlToValidate="txtBankAccountNumber"
			ErrorMessage="The Bank Account Number provided is invalid" ValidationExpression="\d+"></asp:RegularExpressionValidator>
	</span>
</div>
<div class="row">
	<span class="label">Bank Route Number<IMG alt="" src="~/images/asterisk.gif" runat="server"><br>
		<span class="label_note"><a href="javascript:void(0);" runat="server" id="lnkToggleDetails">
				(show/hide example)</a> </span></span><span class="value">
		<asp:TextBox id="txtBankRouteNumber" runat="server" CssClass="wide_field" MaxLength="9"></asp:TextBox>
		<asp:RequiredFieldValidator id="vldRfBankRouteNumber" runat="server" Display="Dynamic" ControlToValidate="txtBankRouteNumber"
			ErrorMessage="Required field cannot be left blank"></asp:RequiredFieldValidator>
		<asp:RegularExpressionValidator id="vldReBankRouteNumber" runat="server" Display="Dynamic" ControlToValidate="txtBankRouteNumber"
			ErrorMessage="The Bank Route Number provided is invalid" ValidationExpression="\d{9}"></asp:RegularExpressionValidator>
		<asp:CustomValidator id="vldCstBankRouteNumber" Display="Dynamic" ControlToValidate="txtBankRouteNumber"
			ErrorMessage="The Bank Route Number provided is invalid" runat="server"></asp:CustomValidator></span>
</div>
<div class="row">
	<span class="label">Driver License State<IMG alt="" src="~/images/asterisk.gif" runat="server"></span>
	<span class="value">
		<asp:DropDownList id="lstDrvLicState" runat="server" CssClass="wide_field">
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
		</asp:DropDownList>
		<asp:RequiredFieldValidator id="vldRfDrvLicState" runat="server" Display="Dynamic" ControlToValidate="lstDrvLicState"
			ErrorMessage="Required field cannot be left blank"></asp:RequiredFieldValidator>
	</span>
</div>
<div class="row">
	<span class="label">Driver License Number<IMG alt="" src="~/images/asterisk.gif" runat="server"></span>
	<span class="value">
		<asp:TextBox id="txtDrvLicNumber" runat="server" CssClass="wide_field"></asp:TextBox>
		<asp:RequiredFieldValidator id="vldRfDrvLicNumber" runat="server" ErrorMessage="Required field cannot be left blank"
			Display="Dynamic" ControlToValidate="txtDrvLicNumber"></asp:RequiredFieldValidator>
	</span>
</div>
