<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Page language="c#" Codebehind="signup.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.SignupPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Sign Up</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="signUpForm" method="post" runat="server">
			<div class="form">
				<div class="row">
					<span class="label">Account Number<IMG alt="" src="images/asterisk.gif"></span> <span class="value">
						<asp:textbox id="txtAccountNumber" runat="server" MaxLength="8" CssClass="account_number"></asp:textbox>
						<asp:RequiredFieldValidator id="vldRfAccountNumber" runat="server" ErrorMessage="<br>Required field cannot be left blank"
							ControlToValidate="txtAccountNumber" Display="Dynamic"></asp:RequiredFieldValidator>
						<asp:RegularExpressionValidator id="vldReAccountNumber" runat="server" ErrorMessage="<br>The Account Number provided is invalid"
							Display="Dynamic" ControlToValidate="txtAccountNumber" ValidationExpression="^\d{8}$" DESIGNTIMEDRAGDROP="885"></asp:RegularExpressionValidator>
					</span>
				</div>
				<DIV class="row">
					<span class="label">Account Last Name<IMG alt="" src="images/asterisk.gif"></span>
					<span class="value">
						<asp:textbox id="txtAccountLastName" runat="server" CssClass="wide_field"></asp:textbox>
						<asp:RequiredFieldValidator id="vldRfAccountLastName" runat="server" ErrorMessage="Required field cannot be left blank"
							ControlToValidate="txtAccountLastName" Display="Dynamic"></asp:RequiredFieldValidator>
					</span>
				</DIV>
				<div class="row">
					<span class="label">Email<IMG alt="" src="images/asterisk.gif"></span> <span class="value">
						<asp:textbox id="txtEmail" runat="server" CssClass="wide_field"></asp:textbox>
						<asp:RequiredFieldValidator id="vldRfEmail" runat="server" ErrorMessage="Required field cannot be left blank"
							ControlToValidate="txtEmail" Display="Dynamic"></asp:RequiredFieldValidator>
						<asp:RegularExpressionValidator id="vldReEmail" runat="server" ErrorMessage="The Email provided is invalid" Display="Dynamic"
							ControlToValidate="txtEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" DESIGNTIMEDRAGDROP="924"></asp:RegularExpressionValidator>
					</span>
				</div>
				<div class="row">
					<span class="label">Password<IMG alt="" src="images/asterisk.gif"></span> <span class="value">
						<asp:textbox id="txtPassword" runat="server" TextMode="Password" MaxLength="25" CssClass="password"></asp:textbox>
						<asp:RequiredFieldValidator id="vldRfPassword" runat="server" ErrorMessage="<br>Required field cannot be left blank"
							ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator>
						<asp:RegularExpressionValidator id="vldRePassword" runat="server" ErrorMessage="<br>The Password must have at least 6 and not be longer then 25 characters"
							Display="Dynamic" ControlToValidate="txtPassword" ValidationExpression="^.{6,25}$"></asp:RegularExpressionValidator>
					</span>
				</div>
				<div class="row">
					<span class="label">Confirm Password<IMG alt="" src="images/asterisk.gif"></span>
					<span class="value">
						<asp:textbox id="txtConfirmPassword" runat="server" TextMode="Password" MaxLength="25" CssClass="password"></asp:textbox>
						<asp:RequiredFieldValidator id="vldRfConfirmPassword" runat="server" ErrorMessage="<br>Required field cannot be left blank"
							ControlToValidate="txtConfirmPassword" Display="Dynamic"></asp:RequiredFieldValidator>
						<asp:CompareValidator id="vldCmpConfirmPassword" runat="server" ErrorMessage="<br>The Password and Confirm Password must match"
							Display="Dynamic" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword"></asp:CompareValidator>
					</span>
				</div>
				<div class="button_row">
					<span class="button">
						<asp:imagebutton id="btnSubmit" runat="server" ImageUrl="images/submit.jpg"></asp:imagebutton></span>
				</div>
				<div class="row"><a href="account/login.aspx" class="link">Account Login</a></div>
			</div>
		</form>
	</body>
</HTML>
