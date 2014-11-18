<%@ Control Language="c#" AutoEventWireup="false" Codebehind="billing_account_info_editor.ascx.cs" Inherits="Dpi.Central.Web.Account.Payment.BillingAccountInfoEditor" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE cellpadding="0" border="0" width="100%">
	<!-- Billing First Name -->
	<TR>
		<TD class="property_name">Billing First Name</TD>
		<TD class="property_value"><asp:textbox id="txtBillingFirstName" Width="100%" runat="server"></asp:textbox></TD>
		<TD class="property_validator"><asp:requiredfieldvalidator id="vldRfBillingFirstName" runat="server" Display="None" ControlToValidate="txtBillingFirstName"
				ErrorMessage="Billing First Name can not be empty"></asp:requiredfieldvalidator></TD>
	</TR>
	<!-- Billing Last Name -->
	<TR>
		<TD class="property_name">Billing Last Name</TD>
		<TD class="property_value"><asp:textbox id="txtBillingLastName" Width="100%" runat="server"></asp:textbox></TD>
		<TD class="property_validator"><asp:requiredfieldvalidator id="vldRfBillingLastName" runat="server" Display="None" ControlToValidate="txtBillingLastName"
				ErrorMessage="Billing Last Name can not be empty"></asp:requiredfieldvalidator></TD>
	</TR>
	<!-- Billing Address -->
	<TR>
		<TD class="property_name">Billing Address</TD>
		<TD class="property_value"><asp:textbox id="txtBillingAddress" Width="100%" runat="server"></asp:textbox></TD>
		<TD class="property_validator"><asp:requiredfieldvalidator id="vldRfBillingAddress" runat="server" Display="None" ControlToValidate="txtBillingAddress"
				ErrorMessage="Billing Address can not be empty"></asp:requiredfieldvalidator></TD>
	</TR>
	<!-- Billing City -->
	<TR>
		<TD class="property_name">Billing City</TD>
		<TD class="property_value"><asp:textbox id="txtBillingCity" Width="100%" runat="server"></asp:textbox></TD>
		<TD class="property_validator"><asp:requiredfieldvalidator id="vldRfBillingCity" runat="server" Display="None" ControlToValidate="txtBillingCity"
				ErrorMessage="Billing City can not be empty"></asp:requiredfieldvalidator></TD>
	</TR>
	<!-- Billing State -->
	<TR>
		<TD class="property_name">Billing State</TD>
		<TD class="property_value"><asp:dropdownlist id="ddlBillingState" Width="100%" runat="server"></asp:dropdownlist></TD>
		<TD class="property_validator"><asp:requiredfieldvalidator id="vldRfBillingState" runat="server" Display="None" ControlToValidate="ddlBillingState"
				ErrorMessage="Billing State can not be empty"></asp:requiredfieldvalidator></TD>
	</TR>
	<!-- Billing Zip -->
	<TR>
		<TD class="property_name">Billing Zip</TD>
		<TD class="property_value"><asp:textbox id="txtBillingZip" Width="100%" runat="server"></asp:textbox></TD>
		<TD class="property_validator"><asp:requiredfieldvalidator id="vldRfBillingZip" runat="server" Display="None" ControlToValidate="txtBillingZip"
				ErrorMessage="Billing Zip can not be empty"></asp:requiredfieldvalidator>
			<asp:RegularExpressionValidator id="vldReBillingZip" runat="server" ErrorMessage="Billing Zip is invalid" ControlToValidate="txtBillingZip"
				Display="None" ValidationExpression="\d{5}(-\d{4})?"></asp:RegularExpressionValidator></TD>
	</TR>
	<!-- Phone Number -->
	<TR>
		<TD class="property_name">Phone Number</TD>
		<TD class="property_value"><asp:textbox id="txtNpa" Width="25%" runat="server" MaxLength="3"></asp:textbox><asp:label id="lblDefis2" runat="server">&nbsp;-&nbsp; </asp:label><asp:textbox id="txtNxx" Width="25%" runat="server" MaxLength="3"></asp:textbox><asp:label id="lblDefis1" runat="server">&nbsp;-&nbsp; </asp:label><asp:textbox id="txtNumber" Width="39%" runat="server" MaxLength="4"></asp:textbox></TD>
		<TD class="property_validator">
			<asp:RegularExpressionValidator id="vldReNpa" runat="server" ErrorMessage="Phone Number is invalid" ControlToValidate="txtNpa"
				Display="None" ValidationExpression="\d{3}"></asp:RegularExpressionValidator>
			<asp:RegularExpressionValidator id="vldReNxx" runat="server" ErrorMessage="Phone Number is invalid" ControlToValidate="txtNxx"
				Display="None" ValidationExpression="\d{3}"></asp:RegularExpressionValidator>
			<asp:RegularExpressionValidator id="vldReNumber" runat="server" ErrorMessage="Phone Number is invalid" ControlToValidate="txtNumber"
				Display="None" ValidationExpression="\d{4}"></asp:RegularExpressionValidator></TD>
	</TR>
	<!-- Email Address -->
	<TR>
		<TD class="property_name">Email Address</TD>
		<TD class="property_value">
			<asp:TextBox id="txtEmailAddress" runat="server" Width="100%"></asp:TextBox></TD>
		<TD class="property_validator">
			<asp:RequiredFieldValidator id="vldRfEmailAddress" runat="server" ErrorMessage="Email Address can not be empty"
				ControlToValidate="txtEmailAddress" Display="None"></asp:RequiredFieldValidator>
			<asp:RegularExpressionValidator id="vldReBillingEmail" runat="server" ErrorMessage="Email Address is invalid" ControlToValidate="txtEmailAddress"
				Display="None" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
		</TD>
	</TR>
</TABLE>
