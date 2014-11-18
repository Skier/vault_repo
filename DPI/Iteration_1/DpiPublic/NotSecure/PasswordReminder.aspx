<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Page language="c#" Codebehind="PasswordReminder.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.PasswordReminder" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
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
										<asp:ValidationSummary id="vldSummary" runat="server" DisplayMode="List" CssClass="Error"></asp:ValidationSummary>
								</TD>
							</TR>
							<TR class="separator_row">
								<TD colSpan="3"></TD>
							</TR>
							<tr>
								<td class="property_name">
									<asp:Label id="lblAccountNumber" runat="server">Account Number</asp:Label>
								</td>
								<TD class="property_value">
									<asp:TextBox id="txtAccountNumber" runat="server" Width="100%"></asp:TextBox></TD>
								<TD class="property_validator">
									<asp:RequiredFieldValidator id="anReqFldValidator" runat="server" ControlToValidate="txtAccountNumber" ErrorMessage="Account Number can not be empty"
										Display="None"></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="anRegExpValidator" runat="server" ErrorMessage="Account Number is invalid" ControlToValidate="txtAccountNumber"
										ValidationExpression="\d{8}" Display="None"></asp:RegularExpressionValidator></TD>
							</tr>
							<TR>
								<TD></TD>
								<TD align="right">
									<asp:imagebutton id="btnSubmit" runat="server" ImageUrl="images/submit.jpg"></asp:imagebutton></TD>
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
