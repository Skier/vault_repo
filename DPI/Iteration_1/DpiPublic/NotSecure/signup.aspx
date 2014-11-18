<%@ Page language="c#" Codebehind="signup.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.SignupPage" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="signUpForm" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD colSpan="2"><dns:header id="ctrlHeader" runat="server"></dns:header></TD>
					<TD vAlign="top" align="left"></TD>
				</TR>
				<TR>
					<TD rowSpan="2">
						<IMG alt="" src="images/about_side.jpg">
					</TD>
					<TD vAlign="top" align="left">
						<IMG src="images/ppc_top.jpg" border="0">
						<TABLE class="layout_table">
							<TR class="separator_row">
								<TD colSpan="3"></TD>
							</TR>
							<TR>
								<TD colSpan="3">
									<asp:CustomValidator id="vldCustErrorMsg" runat="server" Display="None" ErrorMessage="Initialize me"
										EnableClientScript="False"></asp:CustomValidator>
									<asp:ValidationSummary id="vldSummary" runat="server" DisplayMode="List" CssClass="Error"></asp:ValidationSummary></TD>
							</TR>
							<TR class="separator_row">
								<TD colSpan="3"></TD>
							</TR>
							<TR>
								<TD class="property_name">
									<asp:label id="lblAccountNumber" runat="server">Account Number</asp:label></TD>
								<TD class="property_value">
									<asp:textbox id="txtAccountNumber" runat="server" Width="100%"></asp:textbox></TD>
								<TD class="property_validator">
									<asp:RequiredFieldValidator id="vldRfAccountNumber" runat="server" ErrorMessage="Account Number can not be empty"
										ControlToValidate="txtAccountNumber" Display="None"></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="vldReAccountNumber" runat="server" ErrorMessage="Account Number is invalid"
										ValidationExpression="\d{8}" ControlToValidate="txtAccountNumber" Display="None"></asp:RegularExpressionValidator></TD>
							</TR>
							<TR>
								<TD class="property_name">
									<asp:label id="lblAccountLastName" runat="server">Account Last Name</asp:label></TD>
								<TD class="property_value">
									<asp:textbox id="txtAccountLastName" runat="server" Width="100%"></asp:textbox></TD>
								<TD class="property_validator">
									<asp:RequiredFieldValidator id="vldRfAccountLastName" runat="server" ErrorMessage="Account Last Name can not be empty"
										Display="None" ControlToValidate="txtAccountLastName"></asp:RequiredFieldValidator></TD>
							</TR>
							<TR>
								<TD class="property_name">
									<asp:label id="lblEmail" runat="server">Email</asp:label></TD>
								<TD class="property_value">
									<asp:textbox id="txtEmail" runat="server" Width="100%"></asp:textbox></TD>
								<TD class="property_validator">
									<asp:RequiredFieldValidator id="vldRfEmail" runat="server" ErrorMessage="Email can not be empty" DESIGNTIMEDRAGDROP="109"
										ControlToValidate="txtEmail" Display="None"></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="vldReEmail" runat="server" ErrorMessage="Email is invalid" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
										ControlToValidate="txtEmail" Display="None"></asp:RegularExpressionValidator></TD>
							</TR>
							<TR>
								<TD class="property_name">
									<asp:label id="lblPassword" runat="server">Password</asp:label></TD>
								<TD class="property_value">
									<asp:textbox id="txtPassword" runat="server" Width="100%" TextMode="Password"></asp:textbox></TD>
								<TD class="property_validator">
									<asp:RequiredFieldValidator id="vldRfPassword" runat="server" ErrorMessage="Password can not be empty" DESIGNTIMEDRAGDROP="111"
										ControlToValidate="txtPassword" Display="None"></asp:RequiredFieldValidator></TD>
							</TR>
							<TR>
								<TD class="property_name">
									<asp:label id="lblConfirmPassword" runat="server">Confirm Password</asp:label></TD>
								<TD class="property_value">
									<asp:textbox id="txtConfirmPassword" runat="server" Width="100%" TextMode="Password"></asp:textbox></TD>
								<TD class="property_validator">
									<asp:CompareValidator id="vldCmpConfirmPassword" runat="server" ErrorMessage="Confirmation failed" ControlToValidate="txtConfirmPassword"
										ControlToCompare="txtPassword" Display="None"></asp:CompareValidator></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD align="right"><asp:imagebutton id="btnSubmit" runat="server" ImageUrl="images/submit.jpg"></asp:imagebutton></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="bottom" align="center" colspan="2">
						<dns:footer id="ctrlFooter" runat="server"></dns:footer>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
