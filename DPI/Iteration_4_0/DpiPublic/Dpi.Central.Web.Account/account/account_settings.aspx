<%@ Page language="c#" Codebehind="account_settings.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.AccountSettingsPage" %>
<%@ Register TagPrefix="tb" TagName="Tabs" Src="~/account/tabs.ascx" %>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
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
	<body runat="server" id="_body">
		<form id="settingsForm" method="post" runat="server">
			<div class="form">
				<div class="row">
					<tb:Tabs id="m_tabs" runat="server"></tb:Tabs>
				</div>
				<div class="section_row">
					<span class="label">Change E-Mail Address<br>
						<span class="label_note">(Change your email address below. When you are finished, 
							press <b>Submit</b>.<br>
							Communications on Account Activity will use this email address.)</span></span>
				</div>
				<div class="section_row">
					<span class="label">Email<IMG alt="" src="../images/asterisk.gif"></span> <span class="value">
						<asp:TextBox id="txtEMail" runat="server" CssClass="wide_field" MaxLength="50"></asp:TextBox>
						<asp:RequiredFieldValidator id="vldRfPassword" runat="server" Display="Dynamic" ErrorMessage="Required field cannot be left blank"
							ControlToValidate="txtEMail"></asp:RequiredFieldValidator>
						<asp:RegularExpressionValidator ID="vldReEmail" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
							ControlToValidate="txtEMail" ErrorMessage="The Email provided is invalid" Display="Dynamic" EnableViewState="False"
							EnableClientScript="False"></asp:RegularExpressionValidator></span>
				</div>
				<div class="section_row">
					<span class="label">Change Web Access Password<br>
						<span class="label_note">(Fill out the below Password fields only if you are 
							wanting to change your current password for login.)</span></span>
				</div>
				<div class="section_row">
					<span class="label">New Password</span> <span class="value">
						<asp:TextBox id="txtNewPassword" runat="server" CssClass="wide_field" TextMode="Password"></asp:TextBox>
						<asp:RegularExpressionValidator id="vldRePassword" runat="server" Display="Dynamic" ErrorMessage="The Password must have at least 6 and not be longer then 25 characters"
							ControlToValidate="txtNewPassword" ValidationExpression="^.{6,25}$"></asp:RegularExpressionValidator></span>
				</div>
				<div class="row">
					<span class="label">Confirm Password</span> <span class="value">
						<asp:TextBox id="txtPasswordConfirm" runat="server" CssClass="wide_field" TextMode="Password"></asp:TextBox>
						<asp:CompareValidator id="vldCmpConfirmPassword" runat="server" Display="Dynamic" ErrorMessage="The Password and Confirm Password must match"
							ControlToValidate="txtPasswordConfirm" ControlToCompare="txtNewPassword"></asp:CompareValidator>
					</span>
				</div>
				<div class="button_row">
					<span class="button">
						<asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="../images/submit.jpg"></asp:ImageButton></span>
				</div>
			</div>
		</form>
	</body>
</HTML>
