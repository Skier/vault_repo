<%@ Register TagPrefix="dwc" Namespace="Dpi.Central.Web.Controls" Assembly="Dpi.Central.Web.Common" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="customer_info_editor.ascx.cs" Inherits="Dpi.Central.Web.Account.Wireless.CustomerInfoEditor" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE class="table_form" cellSpacing="4" cellPadding="0" width="100%" border="0">
	<TR>
		<TD class="title" colSpan="4">Customer Information
		</TD>
	</TR>
	<TR>
		<TD width="15%">First Name<IMG alt="" src="../images/asterisk.gif"></TD>
		<TD width="37%" style="PADDING-RIGHT: 13px"><asp:TextBox id="txtFirstName" runat="server" CssClass="wide_field"></asp:TextBox>
			<asp:RequiredFieldValidator id="vldRfFirstName" runat="server" ErrorMessage="Required field cannot be left blank"
				ControlToValidate="txtFirstName" Display="Dynamic"></asp:RequiredFieldValidator></TD>
		<TD width="12%">Address 1<IMG alt="" src="../images/asterisk.gif"></TD>
		<TD width="36%"><asp:TextBox id="txtAddress1" runat="server" CssClass="wide_field"></asp:TextBox>
			<asp:RequiredFieldValidator id="vldRfAddress1" runat="server" ErrorMessage="Required field cannot be left blank"
				ControlToValidate="txtAddress1" Display="Dynamic"></asp:RequiredFieldValidator></TD>
	</TR>
	<TR>
		<TD>Last Name<IMG alt="" src="../images/asterisk.gif"></TD>
		<TD style="PADDING-RIGHT: 13px"><asp:TextBox id="txtLastName" runat="server" CssClass="wide_field"></asp:TextBox>
			<asp:RequiredFieldValidator id="vldRfLastName" runat="server" ErrorMessage="Required field cannot be left blank"
				ControlToValidate="txtLastName" Display="Dynamic"></asp:RequiredFieldValidator></TD>
		<TD>Address 2</TD>
		<TD><asp:TextBox id="txtAddress2" runat="server" CssClass="wide_field"></asp:TextBox></TD>
	</TR>
	<TR>
		<TD>Contact Number<IMG alt="" src="../images/asterisk.gif"></TD>
		<TD style="PADDING-RIGHT: 13px"><dwc:phonenumberbox id="phnContactNumber" runat="server"></dwc:phonenumberbox>
			<asp:CustomValidator id="vldCstContactNumber" runat="server" ErrorMessage="<br>The Contact Number provided is invalid"
				Display="Dynamic"></asp:CustomValidator></TD>
		<TD>City<IMG alt="" src="../images/asterisk.gif"></TD>
		<TD><asp:TextBox id="txtCity" runat="server" CssClass="wide_field"></asp:TextBox>
			<asp:RequiredFieldValidator id="vldRfCity" runat="server" ErrorMessage="Required field cannot be left blank"
				ControlToValidate="txtCity" Display="Dynamic"></asp:RequiredFieldValidator></TD>
	</TR>
	<TR>
		<TD>Email<IMG alt="" src="../images/asterisk.gif"></TD>
		<TD style="PADDING-RIGHT: 13px"><asp:TextBox id="txtEmail" runat="server" CssClass="wide_field"></asp:TextBox>
			<asp:RequiredFieldValidator id="vldRfEmail" runat="server" ErrorMessage="Required field cannot be left blank"
				ControlToValidate="txtEmail" Display="Dynamic"></asp:RequiredFieldValidator>
			<asp:RegularExpressionValidator id="vldReEmail" runat="server" ErrorMessage="The Email provided is invalid" ControlToValidate="txtEmail"
				Display="Dynamic" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></TD>
		<TD>State<IMG alt="" src="../images/asterisk.gif"></TD>
		<TD><asp:dropdownlist id="ddlState" runat="server" CssClass="state">
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
			</asp:dropdownlist>
			<asp:RequiredFieldValidator id="vldRfState" runat="server" ErrorMessage="<br>Required field cannot be left blank"
				ControlToValidate="ddlState" Display="Dynamic"></asp:RequiredFieldValidator></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD></TD>
		<TD>Zip Code<IMG alt="" src="../images/asterisk.gif"></TD>
		<TD><asp:TextBox id="txtZipCode" runat="server" MaxLength="5" CssClass="zip"></asp:TextBox>
			<asp:RequiredFieldValidator id="vldRfZipCode" runat="server" ErrorMessage="<br>Required field cannot be left blank"
				ControlToValidate="txtZipCode" Display="Dynamic"></asp:RequiredFieldValidator>
			<asp:RegularExpressionValidator id="vldReZipCode" runat="server" ErrorMessage="<br>The Zip Code provided is invalid"
				ControlToValidate="txtZipCode" Display="Dynamic" ValidationExpression="\d{5}(-\d{4})?"></asp:RegularExpressionValidator></TD>
	</TR>
</TABLE>
