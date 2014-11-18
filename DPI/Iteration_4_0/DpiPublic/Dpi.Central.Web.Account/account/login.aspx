<%@ Page language="c#" Codebehind="login.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.LoginPage" %>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
		<script language="javascript" type="text/javascript">
		function SetInitialFocus() 
		{
			var field = document.getElementById('txtAccountNumber');
			if (field != null || field != 'undefined') {
				field.focus();
			}
		}
		</script>
	</HEAD>
	<body onload="SetInitialFocus();">
		<form id="loginForm" method="post" runat="server">
			<div class="form">
				<div class="row"><span class="label">Phone Number</span> <span class="value">
						<dwc:phonenumberbox id="phnPhoneNumber" runat="server" tabIndex="1"></dwc:phonenumberbox><asp:customvalidator id="vldCstPhoneNumber" runat="server" ControlToValidate="phnPhoneNumber" ErrorMessage="<br>The Phone Number provided is invalid"
							Display="Dynamic" ClientValidationFunction="ValidatePhoneNumber"></asp:customvalidator></span></div>
				<div class="row"><span class="label">Account Number</span> <span class="value">
						<asp:textbox id="txtAccountNumber" runat="server" MaxLength="8" CssClass="wide_field" tabIndex="4"></asp:textbox>
						<asp:RegularExpressionValidator id="vldReAccountNumber" runat="server" ControlToValidate="txtAccountNumber" ErrorMessage="The Account Number provided is invalid"
							Display="Dynamic" ValidationExpression="^\d{8}$"></asp:RegularExpressionValidator></span></div>
				<div class="row"><span class="label_note">(Enter either your Phone Number or Account 
						Number)<IMG alt="" src="../images/asterisk.gif"></span> <span class="value">
						<asp:CustomValidator id="vldCstIdentity" runat="server" ErrorMessage="Both Phone Number and Account Number cannot be left blank"
							Display="Dynamic"></asp:CustomValidator></span>
				</div>
				<div class="row"><span class="label">Password<IMG alt="" src="../images/asterisk.gif"></span>
					<span class="value">
						<asp:textbox id="txtPassword" runat="server" MaxLength="25" CssClass="wide_field" TextMode="Password"
							tabIndex="5"></asp:textbox><asp:requiredfieldvalidator id="vldRfPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Required field cannot be left blank"
							Display="Dynamic"></asp:requiredfieldvalidator>
						<asp:regularexpressionvalidator id="vldRePassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="The Password must have at least 6 and not be longer then 25 characters"
							Display="Dynamic" ValidationExpression="^.{6,25}$"></asp:regularexpressionvalidator></span></div>
				<div class="button_row"><span class="button"><asp:imagebutton id="btnSubmit" runat="server" ImageUrl="../images/submit.jpg" tabIndex="6"></asp:imagebutton></span></div>
				<div class="row"><A class="link" href="../password_reminder.aspx" tabIndex="7">Forgot 
						My Password</A></div>
				<div class="row"><A class="link" href="../signup.aspx" tabIndex="8">Web Access Sign Up</A></div>
			</div>
		</form>
	</body>
</HTML>
