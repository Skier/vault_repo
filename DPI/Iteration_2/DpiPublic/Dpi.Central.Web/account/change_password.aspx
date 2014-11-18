<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Page language="c#" Codebehind="change_password.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.ChangePasswordPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>change_password</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<FORM id="loginForm" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD colSpan="2">
						<dns:header id="ctrlHeader" runat="server"></dns:header></TD>
					<TD vAlign="top" align="left"></TD>
				</TR>
				<TR>
					<TD rowSpan="2"><IMG alt="" src="../images/about_side.jpg">
					</TD>
					<TD vAlign="top" align="left"><IMG src="../images/ppc_top.jpg" border="0">
						<TABLE class="layout_table">
							<TR class="separator_row">
								<TD colSpan="3">&nbsp;</TD>
							</TR>
							<TR>
								<TD colSpan="3">
									<asp:customvalidator id="vldCustErrorMsg" runat="server" ErrorMessage="" Display="None" EnableClientScript="False"
										Width="100%"></asp:customvalidator>
									<asp:validationsummary id="vldSummary" runat="server" DisplayMode="List" CssClass="Error"></asp:validationsummary></TD>
							</TR>
							<TR class="separator_row">
								<TD colSpan="3">&nbsp;</TD>
							</TR>
							<TR>
								<TD class="property_name">
									<asp:Label id="lblNewPwd" runat="server">New Password</asp:Label></TD>
								<TD class="property_value">
									<asp:TextBox id="txtNewPwd" runat="server" Width="100%" TextMode="Password"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD class="property_name">
									<asp:Label id="lblConfirmPwd" runat="server">Confirm Password</asp:Label></TD>
								<TD class="property_value">
									<asp:TextBox id="txtConfirmPwd" runat="server" Width="100%" TextMode="Password"></asp:TextBox></TD>
							</TR>
							<TR>
								<TD noWrap></TD>
								<TD align="right">
									<asp:imagebutton id="btnSubmit" runat="server" ImageUrl="../images/submit.jpg"></asp:imagebutton></TD>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="bottom" align="center">
						<dns:footer id="ctrlFooter" runat="server"></dns:footer></TD>
					<TD vAlign="top" align="left"></TD>
				</TR>
			</TABLE>
		</FORM>
	</body>
</HTML>
