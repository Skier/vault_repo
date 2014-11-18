<%@ Page language="c#" Codebehind="password_reminder.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.PasswordReminder" %>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
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
			<div class="form">
				<div class="row">
					<span class="label">Account Number<IMG alt="" src="images/asterisk.gif"></span> <span class="value">
						<asp:textbox id="txtAccountNumber" runat="server" CssClass="wide_field" MaxLength="8"></asp:textbox>
						<asp:RequiredFieldValidator id="vldRfAccountNumber" runat="server" Display="Dynamic" ErrorMessage="Required field cannot be left blank"
							ControlToValidate="txtAccountNumber"></asp:RequiredFieldValidator>
						<asp:RegularExpressionValidator id="vldReAccountNumber" runat="server" Display="Dynamic" ErrorMessage="The Account Number provided is invalid"
							ControlToValidate="txtAccountNumber" ValidationExpression="^\d{8}$" DESIGNTIMEDRAGDROP="885"></asp:RegularExpressionValidator></span>
				</div>
				<div class="button_row">
					<span class="button">
						<asp:imagebutton id="btnSubmit" runat="server" ImageUrl="images/submit.jpg"></asp:imagebutton></span>
				</div>
				<div class="row"><a href="signup.aspx" class="link" runat="server" id="lnkSignUp">Web 
						Access Sign Up</a><a href="account/login.aspx" class="link">Return to Account 
						Login</a></div>
			</div>
		</form>
	</body>
</HTML>
