<%@ Page language="c#" Codebehind="account_settings.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.AccountSettingsPage" %>
<%@ Register TagPrefix="ctl" Namespace="Dpi.Central.Web.Controls" Assembly="Dpi.Central.Web" %>
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
		<link href="../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="loginForm" method="post" runat="server">
			<table cellspacing="0" cellpadding="0" border="0">
				<tbody>
					<tr>
						<td colspan="2"><dns:header id="ctrlHeader" runat="server"></dns:header></td>
						<td valign="top" align="left"></td>
					</tr>
					<tr>
						<td rowspan="2"><img alt="" src="../images/about_side.jpg">
						</td>
						<td valign="top" align="left"><img src="../images/ppc_top.jpg" border="0">
							<table class="layout_table">
								<tbody>
									<tr class="separator_row">
										<td colspan="3">&nbsp;</td>
									</tr>
									<tr>
										<td colspan="3"><asp:CustomValidator ID="vldCustErrorMsg" runat="server" ErrorMessage="Initialize me" Display="None"
												EnableClientScript="False" Width="100%"></asp:CustomValidator><asp:ValidationSummary ID="vldSummary" runat="server" DisplayMode="List" CssClass="Error"></asp:ValidationSummary><asp:Label ID="lblChangedNotify" runat="server" EnableViewState="False" ForeColor="red" Visible="False">Your account settings have been saved</asp:Label></td>
									</tr>
									<tr>
										<td colspan="3" height="20">&nbsp;</td>
									</tr>
									<tr>
										<td nowrap class="property_name"><ctl:ControlLabel id="lblEMail" runat="server" ENABLEVIEWSTATE="False">E-Mail</ctl:ControlLabel></td>
										<td class="property_value"><ctl:textbox id="txtEMail" runat="server" Width="100%" MaxLength="50"></ctl:textbox></td>
										<td><asp:RegularExpressionValidator ID="emailValidator" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
												ControlToValidate="txtEMail" ErrorMessage="E-Mail address is invalid" Display="None" EnableViewState="False"
												EnableClientScript="False"></asp:RegularExpressionValidator></td>
									</tr>
									<tr>
										<td style="PADDING-TOP: 12px" colspan="3">Change Web Access Password<br>
											<span style="FONT-WEIGHT: normal">(Fill out the below 
            Password fields only if you are wanting to change your current 
												password for login)</span></td>
									</tr>
									<tr>
										<td nowrap class="property_name"><ctl:ControlLabel AssociatedControlID="txtNewPassword" id="lblNewPassword" runat="server" ENABLEVIEWSTATE="False">New Password</ctl:ControlLabel></td>
										<td class="property_value"><ctl:textbox id="txtNewPassword" runat="server" Width="100%" TextMode="Password"></ctl:textbox></td>
										<td>
											<asp:CustomValidator ID="pwdValidator" runat="server" ErrorMessage="Password must have at least {0} characters"
												EnableViewState="False" EnableClientScript="False" Display="None" ControlToValidate="txtNewPassword"
												ClientValidationFunction="checkPassword"></asp:CustomValidator></td>
									</tr>
									<tr>
										<td nowrap class="property_name"><ctl:ControlLabel AssociatedControlID="txtPasswordConfirm" id="lblPasswordConfirm" runat="server" ENABLEVIEWSTATE="False">Confirm Password</ctl:ControlLabel></td>
										<td class="property_value"><ctl:textbox id="txtPasswordConfirm" runat="server" Width="100%" TextMode="Password"></ctl:textbox></td>
										<td><asp:CustomValidator ID="pwdConfirmValidator" runat="server" ErrorMessage="Password does not match" ControlToValidate="txtNewPassword"
												Display="None" ClientValidationFunction="checkPassword" EnableViewState="False" EnableClientScript="False"></asp:CustomValidator></td>
									</tr>
									<tr>
										<td>&nbsp;</td>
										<td align="right"><asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="../images/submit.jpg"></asp:ImageButton></td>
										<td>&nbsp;</td>
									</tr>
									<tr>
										<td>&nbsp;</td>
										<td><asp:HyperLink id=lnkSummary runat="server" NavigateUrl="~/account/summary.aspx">Return to Account Summary</asp:HyperLink></td>
										<td>&nbsp;</td>
									</tr>
								</tbody>
							</table>
						</td>
					</tr>
					<tr>
						<td valign="bottom" align="center"><dns:footer id="ctrlFooter" runat="server"></dns:footer></td>
						<td valign="top" align="left"></td>
					</tr>
				</tbody>
			</table>
		</form></TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
