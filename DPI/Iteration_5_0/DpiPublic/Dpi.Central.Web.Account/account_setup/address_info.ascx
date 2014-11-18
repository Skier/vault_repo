<%@ Control Language="c#" AutoEventWireup="false" Codebehind="address_info.ascx.cs" Inherits="Dpi.Central.Web.Account.AccountSetup.AddressInfoControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="row"><span class="label">Address 1<IMG alt="" src="../images/asterisk.gif"></span>
	<span class="value">
		<asp:textbox id="txtAddress1" CssClass="wide_field" runat="server"></asp:textbox><asp:requiredfieldvalidator id="vldStreet" runat="server" Display="Dynamic" ErrorMessage="<br>Required field cannot be left blank"
			ControlToValidate="txtAddress1"></asp:requiredfieldvalidator></span></div>
<div class="row"><span class="label">Address 2</span> <span class="value">
		<asp:textbox id="txtAddress2" CssClass="address2" runat="server"></asp:textbox></span></div>
<div class="row"><span class="label">City<IMG alt="" src="../images/asterisk.gif"></span>
	<span class="value">
		<asp:textbox id="txtCity" CssClass="city" runat="server" MaxLength="28"></asp:textbox><asp:requiredfieldvalidator id="vldRfCity" runat="server" Display="Dynamic" ErrorMessage="<br>Required field cannot be left blank"
			ControlToValidate="txtCity"></asp:requiredfieldvalidator></span></div>
<div class="row"><span class="label">State<IMG alt="" src="../images/asterisk.gif"></span>
	<span class="value">
		<asp:dropdownlist id="ddlState" CssClass="state" runat="server">
			<asp:ListItem></asp:ListItem>
			<asp:ListItem Value="AL">AL - Alabama</asp:ListItem>
			<asp:ListItem Value="AK">AK - Alaska</asp:ListItem>
			<asp:ListItem Value="AR">AR - Arkansas</asp:ListItem>
			<asp:ListItem Value="AZ">AZ - Arizona</asp:ListItem>
			<asp:ListItem Value="CA">CA - California</asp:ListItem>
			<asp:ListItem Value="CO">CO - Colorado</asp:ListItem>
			<asp:ListItem Value="CT">CT - Connecticut</asp:ListItem>
			<asp:ListItem Value="DE">DE - Delaware</asp:ListItem>
			<asp:ListItem Value="DC">DC - District of Columbia</asp:ListItem>
			<asp:ListItem Value="FL">FL - Florida</asp:ListItem>
			<asp:ListItem Value="GA">GA - Georgia</asp:ListItem>
			<asp:ListItem Value="HI">HI - Hawaii</asp:ListItem>
			<asp:ListItem Value="ID">ID - Idaho</asp:ListItem>
			<asp:ListItem Value="IL">IL - Illinois</asp:ListItem>
			<asp:ListItem Value="IN">IN - Indiana</asp:ListItem>
			<asp:ListItem Value="IA">IA - Iowa</asp:ListItem>
			<asp:ListItem Value="KS">KS - Kansas</asp:ListItem>
			<asp:ListItem Value="KY">KY - Kentucky</asp:ListItem>
			<asp:ListItem Value="LA">LA - Louisiana</asp:ListItem>
			<asp:ListItem Value="ME">ME - Maine</asp:ListItem>
			<asp:ListItem Value="MD">MD - Maryland</asp:ListItem>
			<asp:ListItem Value="MA">MA - Massachusetts</asp:ListItem>
			<asp:ListItem Value="MI">MI - Michigan</asp:ListItem>
			<asp:ListItem Value="MN">MN - Minnesota</asp:ListItem>
			<asp:ListItem Value="MS">MS - Mississippi</asp:ListItem>
			<asp:ListItem Value="MO">MO - Missouri</asp:ListItem>
			<asp:ListItem Value="MT">MT - Montana</asp:ListItem>
			<asp:ListItem Value="NE">NE - Nebraska</asp:ListItem>
			<asp:ListItem Value="NV">NV - Nevada</asp:ListItem>
			<asp:ListItem Value="NH">NH - New Hampshire</asp:ListItem>
			<asp:ListItem Value="NJ">NJ - New Jersey</asp:ListItem>
			<asp:ListItem Value="NM">NM - New Mexico</asp:ListItem>
			<asp:ListItem Value="NY">NY - New York</asp:ListItem>
			<asp:ListItem Value="NC">NC - North Carolina</asp:ListItem>
			<asp:ListItem Value="ND">ND - North Dakota</asp:ListItem>
			<asp:ListItem Value="OH">OH - Ohio</asp:ListItem>
			<asp:ListItem Value="OK">OK - Oklahoma</asp:ListItem>
			<asp:ListItem Value="OR">OR - Oregon</asp:ListItem>
			<asp:ListItem Value="PA">PA - Pennsylvania</asp:ListItem>
			<asp:ListItem Value="PR">PR - Puerto Rico</asp:ListItem>
			<asp:ListItem Value="RI">RI - Rhode Island</asp:ListItem>
			<asp:ListItem Value="SC">SC - South Carolina</asp:ListItem>
			<asp:ListItem Value="SD">SD - South Dakota</asp:ListItem>
			<asp:ListItem Value="TN">TN - Tennessee</asp:ListItem>
			<asp:ListItem Value="TX">TX - Texas</asp:ListItem>
			<asp:ListItem Value="UT">UT - Utah</asp:ListItem>
			<asp:ListItem Value="VT">VT - Vermont</asp:ListItem>
			<asp:ListItem Value="VI">VI - Virgin Islands</asp:ListItem>
			<asp:ListItem Value="VA">VA - Virginia</asp:ListItem>
			<asp:ListItem Value="WA">WA - Washington</asp:ListItem>
			<asp:ListItem Value="WV">WV - West Virginia</asp:ListItem>
			<asp:ListItem Value="WI">WI - Wisconsin</asp:ListItem>
			<asp:ListItem Value="WY">WY - Wyoming</asp:ListItem>
		</asp:dropdownlist><asp:requiredfieldvalidator id="vldRfState" runat="server" Display="Dynamic" ErrorMessage="<br>Required field cannot be left blank"
			ControlToValidate="ddlState"></asp:requiredfieldvalidator></span></div>
<div class="row"><span class="label">Zip<IMG alt="" src="../images/asterisk.gif"></span>
	<span class="value">
		<asp:textbox id="txtZip" CssClass="zip" runat="server" MaxLength="5"></asp:textbox><asp:requiredfieldvalidator id="vldRfZip" runat="server" Display="Dynamic" ErrorMessage="<br>Required field cannot be left blank"
			ControlToValidate="txtZip"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="vldReZip" runat="server" Display="Dynamic" ErrorMessage="<br>The Zip provided is invalid"
			ControlToValidate="txtZip" ValidationExpression="\d{5}(-\d{4})?"></asp:regularexpressionvalidator></span></div>
