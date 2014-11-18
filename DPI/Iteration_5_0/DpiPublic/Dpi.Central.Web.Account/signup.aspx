<%@ Page language="c#" Codebehind="signup.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.SignupPage" %>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Sign Up</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="DPI.css" type="text/css" rel="stylesheet">
		<script language="javascript" type="text/javascript">
			var effectDuration = 0.3;
			
			function ShowWirelessSignUp()
			{
				if (!isVisible('divWirelessSignUpRow'))
					new Effect.Appear('divWirelessSignUpRow', {duration: effectDuration});
				
				if (isVisible('divOrdinarySignUpRow'))
					new Effect.Fade('divOrdinarySignUpRow', {duration: effectDuration});
					
				if (!isVisible('divButtonsRow'))
					new Effect.Appear('divButtonsRow', {duration: effectDuration});
			}
			
			function ShowOrdinarySignUp() 
			{
				if (isVisible('divWirelessSignUpRow'))
					new Effect.Fade('divWirelessSignUpRow', {duration: effectDuration});
					
				if (!isVisible('divOrdinarySignUpRow'))
					new Effect.Appear('divOrdinarySignUpRow', {duration: effectDuration});
					
				if (!isVisible('divButtonsRow'))
					new Effect.Appear('divButtonsRow', {duration: effectDuration});
			}
		</script>
	</HEAD>
	<body>
		<form id="signUpForm" method="post" runat="server">
			<div class="form" style="WIDTH: 530px"><asp:customvalidator id="vldCstIdentity" runat="server" ErrorMessage="Both Phone Number and Account Number cannot be left blank"
					Display="None"></asp:customvalidator>
				<div class="row"><span class="label">Please Select Your Type Of Service?<IMG alt="" src="images/asterisk.gif">
					</span><span class="value">
						<asp:radiobutton id="rbtnWireless" runat="server" GroupName="ServiceType" Text="Wireless"></asp:radiobutton><asp:radiobutton id="rbtnOrdinary" runat="server" GroupName="ServiceType" Text="Home Phone"></asp:radiobutton></span></div>
				<div class="spacer">&nbsp;</div>
				<div id="divOrdinarySignUpRow" style="DISPLAY: none" runat="server">
					<div class="row"><span class="label">Account Number<IMG alt="" src="images/asterisk.gif"></span>
						<span class="value">
							<asp:textbox id="txtAccountNumber" runat="server" CssClass="account_number" MaxLength="8"></asp:textbox><asp:requiredfieldvalidator id="vldRfAccountNumber" runat="server" ErrorMessage="<br>Required field cannot be left blank"
								Display="Dynamic" ControlToValidate="txtAccountNumber"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="vldReAccountNumber" runat="server" ErrorMessage="<br>The Account Number provided is invalid"
								Display="Dynamic" ControlToValidate="txtAccountNumber" DESIGNTIMEDRAGDROP="885" ValidationExpression="^\d{8}$"></asp:regularexpressionvalidator></span></div>
					<DIV class="row"><span class="label">Account Last Name<IMG alt="" src="images/asterisk.gif"></span>
						<span class="value">
							<asp:textbox id="txtAccountLastName" runat="server" CssClass="wide_field"></asp:textbox><asp:requiredfieldvalidator id="vldRfAccountLastName" runat="server" ErrorMessage="Required field cannot be left blank"
								Display="Dynamic" ControlToValidate="txtAccountLastName"></asp:requiredfieldvalidator></span></DIV>
					<div class="row"><span class="label">Email<IMG alt="" src="images/asterisk.gif"></span>
						<span class="value">
							<asp:textbox id="txtEmail" runat="server" CssClass="wide_field"></asp:textbox><asp:requiredfieldvalidator id="vldRfEmail" runat="server" ErrorMessage="Required field cannot be left blank"
								Display="Dynamic" ControlToValidate="txtEmail"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="vldReEmail" runat="server" ErrorMessage="The Email provided is invalid" Display="Dynamic"
								ControlToValidate="txtEmail" DESIGNTIMEDRAGDROP="924" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:regularexpressionvalidator></span></div>
					<div class="row"><span class="label">Password<IMG alt="" src="images/asterisk.gif"></span>
						<span class="value">
							<asp:textbox id="txtPassword" runat="server" CssClass="password" MaxLength="25" TextMode="Password"></asp:textbox><asp:requiredfieldvalidator id="vldRfPassword" runat="server" ErrorMessage="<br>Required field cannot be left blank"
								Display="Dynamic" ControlToValidate="txtPassword"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="vldRePassword" runat="server" ErrorMessage="<br>The Password must have at least 6 and not be longer then 25 characters"
								Display="Dynamic" ControlToValidate="txtPassword" ValidationExpression="^.{6,25}$"></asp:regularexpressionvalidator></span></div>
					<div class="row"><span class="label">Confirm Password<IMG alt="" src="images/asterisk.gif"></span>
						<span class="value">
							<asp:textbox id="txtConfirmPassword" runat="server" CssClass="password" MaxLength="25" TextMode="Password"></asp:textbox><asp:requiredfieldvalidator id="vldRfConfirmPassword" runat="server" ErrorMessage="<br>Required field cannot be left blank"
								Display="Dynamic" ControlToValidate="txtConfirmPassword"></asp:requiredfieldvalidator><asp:comparevalidator id="vldCmpConfirmPassword" runat="server" ErrorMessage="<br>The Password and Confirm Password must match"
								Display="Dynamic" ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword"></asp:comparevalidator></span></div>
				</div>
				<div id="divWirelessSignUpRow" style="DISPLAY: none" runat="server">
					<div class="row"><span class="label">Phone Number<IMG alt="" src="images/asterisk.gif"></span>
						<span class="value">
							<dwc:phonenumberbox id="phnPhoneNumber" tabIndex="1" runat="server"></dwc:phonenumberbox><asp:customvalidator id="vldCstPhoneNumber" runat="server" ErrorMessage="<br>The Phone Number provided is invalid"
								Display="Dynamic"></asp:customvalidator></span></div>
				</div>
				<div id="divButtonsRow" style="DISPLAY: none" runat="server">
					<div class="button_row"><span class="button"><asp:imagebutton id="btnSubmit" runat="server" ImageUrl="images/submit.jpg"></asp:imagebutton></span></div>
					<div class="row"><a href="account/login.aspx" class="link">Account Login</a></div>
				</div>
			</div>
		</form>
	</body>
</HTML>
