<%@ Page language="c#" Codebehind="PasswordReminder.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.PasswordReminder" %>
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
		<form id="passwordReminderForm" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD colSpan="2"><dns:header id="ctrlHeader" runat="server"></dns:header></TD>
					<TD vAlign="top" align="left"></TD>
				</TR>
				<TR>
					<TD rowSpan="2"><IMG alt="" src="images/about_side.jpg">
					</TD>
					<TD vAlign="top" align="left"><IMG src="images/ppc_top.jpg" border="0">
						<TABLE class="layout_table">
							<TR class="separator_row">
								<TD colSpan="3"></TD>
							</TR>
							<TR>
								<TD colSpan="3"><asp:customvalidator id="vldCustErrorMsg" runat="server" EnableClientScript="False" ErrorMessage="Initialize me"
										Display="None"></asp:customvalidator><asp:validationsummary id="vldSummary" runat="server" CssClass="Error" DisplayMode="List"></asp:validationsummary></TD>
							</TR>
							<TR class="separator_row">
								<TD colSpan="3"></TD>
							</TR>
							<tr>
								<td class="property_name"><asp:label id="lblAccountNumber" runat="server">Account Number</asp:label></td>
								<td class="property_value"><asp:textbox id="txtAccountNumber" runat="server" Width="100%"></asp:textbox></td>
								<td></td>
							</tr>
							<TR>
								<TD></TD>
								<TD align="right"><asp:imagebutton id="btnSubmit" runat="server" ImageUrl="images/submit.jpg"></asp:imagebutton></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD colspan=2><asp:HyperLink id=lnkSignUp navigateurl="~/account/signup.aspx" runat="server" FONT-UNDERLINE="True" VISIBLE="False">Web Access Sign Up</asp:HyperLink>
									<br/><asp:HyperLink id=lnkLogin runat="server" FONT-UNDERLINE="True" navigateurl="~/account/login.aspx">Return to Account Login</asp:HyperLink></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="bottom" align="center" colSpan="2"><dns:footer id="ctrlFooter" runat="server"></dns:footer></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
