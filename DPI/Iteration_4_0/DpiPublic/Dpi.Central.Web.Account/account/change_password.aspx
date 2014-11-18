<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
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
			<div class="form">
				<div class="row">
					<span class="label">New Password<IMG alt="" src="../images/asterisk.gif"></span>
					<span class="value">
						<P>
							<asp:TextBox id="txtNewPwd" runat="server" CssClass="wide_field" TextMode="Password" MaxLength="25"></asp:TextBox>
							<asp:RequiredFieldValidator id="vldRfPassword" runat="server" Display="Dynamic" ErrorMessage="Required field cannot be left blank"
								ControlToValidate="txtNewPwd"></asp:RequiredFieldValidator>
							<asp:RegularExpressionValidator id="vldRePassword" runat="server" Display="Dynamic" ErrorMessage="The New Password must have at least 6 and not be longer then 25 characters"
								ControlToValidate="txtNewPwd" ValidationExpression="^.{6,25}$"></asp:RegularExpressionValidator></P>
					</span>
				</div>
				<div class="row">
					<span class="label">Confirm Password<IMG alt="" src="../images/asterisk.gif"></span>
					<span class="value">
						<P>
							<asp:TextBox id="txtConfirmPwd" runat="server" CssClass="wide_field" TextMode="Password" MaxLength="25"></asp:TextBox>
							<asp:RequiredFieldValidator id="vldRfConfirmPassword" runat="server" Display="Dynamic" ErrorMessage="Required field cannot be left blank"
								ControlToValidate="txtConfirmPwd"></asp:RequiredFieldValidator>
							<asp:CompareValidator id="vldCmpConfirmPassword" runat="server" Display="Dynamic" ErrorMessage="The New Password and Confirm Password must match"
								ControlToValidate="txtConfirmPwd" ControlToCompare="txtNewPwd"></asp:CompareValidator></P>
					</span>
				</div>
				<div class="button_row">
					<span class="button">
						<asp:imagebutton id="btnSubmit" runat="server" ImageUrl="../images/submit.jpg"></asp:imagebutton></span>
				</div>
			</div>
		</FORM>
	</body>
</HTML>
