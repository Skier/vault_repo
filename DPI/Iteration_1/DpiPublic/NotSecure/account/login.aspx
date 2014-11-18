<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Page language="c#" Codebehind="login.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.LoginPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
		<script type="text/Jscript">
			function UpdateControls()
			{	
				pnpaCtrl = document.all["txtNpa"];	
				pnxxCtrl = document.all["txtNxx"];	
				pnCtrl = document.all["txtNumber"];	
				anCtrl = document.all["txtAccountNumber"];	
				
				document.all["lblPhoneNumber"].disabled = pnpaCtrl.disabled = pnxxCtrl.disabled = pnCtrl.disabled = anCtrl.value.length > 0;
				document.all["lblAccountNumber"].disabled = anCtrl.disabled = 
					(pnpaCtrl.value.length > 0 || pnxxCtrl.value.length > 0 || pnCtrl.value.length > 0);
			}
		</script>
	</HEAD>
	<body>
		<form id="loginForm" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD colSpan="2"><dns:header id="ctrlHeader" runat="server"></dns:header></TD>
					<TD vAlign="top" align="left"></TD>
				</TR>
				<TR>
					<TD rowSpan="2">
						<IMG alt="" src="../images/about_side.jpg">
					</TD>
					<TD vAlign="top" align="left">
						<IMG src="../images/ppc_top.jpg" border="0">
						<TABLE class="layout_table">
							<TR class="separator_row">
								<TD colSpan="3"></TD>
							</TR>
							<TR>
								<TD colSpan="3">
									<asp:CustomValidator id="vldCustErrorMsg" runat="server" ErrorMessage="Initialize me" Display="None"
										EnableClientScript="False" Width="100%"></asp:CustomValidator>
									<asp:ValidationSummary id="vldSummary" runat="server" DisplayMode="List" CssClass="Error"></asp:ValidationSummary></TD>
							</TR>
							<TR>
								<TD colSpan="3" height="20"></TD>
							</TR>
							<TR>
								<TD width="20%" noWrap><asp:label id="lblPhoneNumber" runat="server">Phone Number</asp:label></TD>
								<TD width="30%"><asp:textbox id="txtNpa" runat="server" Width="57px" MaxLength="3"></asp:textbox>
									<asp:Label id="lblDefis2" runat="server" DESIGNTIMEDRAGDROP="99" Width="10px">&nbsp;-&nbsp; </asp:Label>
									<asp:textbox id="txtNxx" runat="server" Width="57px" MaxLength="3"></asp:textbox>
									<asp:Label id="lblDefis1" runat="server" Width="10px">&nbsp;-&nbsp; </asp:Label>
									<asp:textbox id="txtNumber" runat="server" Width="107px" MaxLength="4"></asp:textbox></TD>
								<TD width="30%"></TD>
							</TR>
							<TR>
								<TD noWrap width="20%"></TD>
								<TD align="center" width="30%">
									<asp:Label id="lblOr" runat="server">-- OR --</asp:Label></TD>
								<TD width="30%"></TD>
							</TR>
							<TR>
								<TD width="20%" noWrap><asp:label id="lblAccountNumber" runat="server">Account Number</asp:label></TD>
								<TD width="30%"><asp:textbox id="txtAccountNumber" runat="server" Width="100%"></asp:textbox></TD>
								<TD width="30%"><asp:regularexpressionvalidator id="anRegExpValidator" runat="server" ValidationExpression="\d{8}" ControlToValidate="txtAccountNumber"
										ErrorMessage="Account Number is invalid" Display="None"></asp:regularexpressionvalidator></TD>
							</TR>
							<TR>
								<TD width="20%" noWrap><asp:label id="lblPassword" runat="server">Password</asp:label></TD>
								<TD width="30%"><asp:textbox id="txtPassword" runat="server" Width="100%" TextMode="Password"></asp:textbox></TD>
								<TD width="30%"><asp:requiredfieldvalidator id="pwdReqFldValidator" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password can not be empty"
										Display="None"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD align="right">
									<asp:imagebutton id="btnSubmit" runat="server" ImageUrl="../images/submit.jpg"></asp:imagebutton></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD><asp:linkbutton id="lbtnForgotMyPassword" runat="server" CausesValidation="False">Forgot My Password</asp:linkbutton></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD>
									<asp:LinkButton id="lbtnSignUp" runat="server" CausesValidation="False">Web Access Sign Up</asp:LinkButton></TD>
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
		</form>
	</body>
</HTML>
