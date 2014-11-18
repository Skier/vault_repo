<%@ Page language="c#" Codebehind="password_reminder.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.PasswordReminder" %>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Password Reminder</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="DPI.css" type="text/css" rel="stylesheet">
		<script language="javascript" type="text/javascript">
			var effectDuration = 0.3;
			
			function ShowPhoneNumber()
			{
				if (!isVisible('divPhoneNumberRow'))
					new Effect.Appear('divPhoneNumberRow', {duration: effectDuration});
				
				if (isVisible('divAccountNumberRow'))
					new Effect.Fade('divAccountNumberRow', {duration: effectDuration});
					
				if (!isVisible('divButtonsRow'))
					new Effect.Appear('divButtonsRow', {duration: effectDuration});
			}
			
			function ShowAccountNumber() 
			{
				if (isVisible('divPhoneNumberRow'))
					new Effect.Fade('divPhoneNumberRow', {duration: effectDuration});
					
				if (!isVisible('divAccountNumberRow'))
					new Effect.Appear('divAccountNumberRow', {duration: effectDuration});
					
				if (!isVisible('divButtonsRow'))
					new Effect.Appear('divButtonsRow', {duration: effectDuration});
			}
		</script>
	</HEAD>
	<body>
		<form id="passwordReminderForm" method="post" runat="server">
			<div class="form" style="WIDTH: 530px"><asp:customvalidator id="vldCstIdentity" runat="server" Display="None" ErrorMessage="Both Phone Number and Account Number cannot be left blank"></asp:customvalidator>
				<div class="row"><span class="label">Please Select Your Type Of Service?<IMG alt="" src="images/asterisk.gif"></span>
					<span class="value">
						<asp:radiobutton id="rbtnWireless" runat="server" Text="Wireless" GroupName="ServiceType"></asp:radiobutton><asp:radiobutton id="rbtnOrdinary" runat="server" Text="Home Phone" GroupName="ServiceType"></asp:radiobutton></span></div>
				<div class="section_row" id="divPhoneNumberRow" style="DISPLAY: none" runat="server"><span class="label">Enter 
						Your Phone Number<IMG alt="" src="images/asterisk.gif"></span> <span class="value">
						<dwc:phonenumberbox id="phnPhoneNumber" tabIndex="1" runat="server"></dwc:phonenumberbox><asp:customvalidator id="vldCstPhoneNumber" runat="server" Display="Dynamic" ErrorMessage="<br>The Phone Number provided is invalid"
							ControlToValidate="phnPhoneNumber"></asp:customvalidator>
						<asp:RequiredFieldValidator id="vldRfPhoneNumber" runat="server" ErrorMessage="<br>Required field cannot be left blank"
							Display="Dynamic" ControlToValidate="phnPhoneNumber"></asp:RequiredFieldValidator></span></div>
				<div class="section_row" id="divAccountNumberRow" style="DISPLAY: none" runat="server"><span class="label">Enter 
						Your Account Number<IMG alt="" src="images/asterisk.gif"></span> <span class="value">
						<asp:textbox id="txtAccountNumber" runat="server" MaxLength="8" CssClass="wide_field"></asp:textbox><asp:regularexpressionvalidator id="vldReAccountNumber" runat="server" Display="Dynamic" ErrorMessage="The Account Number provided is invalid"
							ValidationExpression="^\d{8}$" ControlToValidate="txtAccountNumber"></asp:regularexpressionvalidator>
						<asp:RequiredFieldValidator id="vldRfAccountNumber" runat="server" Display="Dynamic" ErrorMessage="Required field cannot be left blank"
							ControlToValidate="txtAccountNumber"></asp:RequiredFieldValidator></span></div>
				<div id="divButtonsRow" style="DISPLAY: none" runat="server">
					<div class="button_row"><span class="button"><asp:imagebutton id="btnSubmit" runat="server" ImageUrl="images/submit.jpg"></asp:imagebutton></span></div>
					<div class="row">
						<A class="link" id="lnkOrdinarySignUp" href="signup.aspx" runat="server">Web Access 
							Ordinary Sign Up<br><br></A> <A class="link" id="lnkWirelessSignUp" href="wireless_signup.aspx" runat="server">
							Web Access Wireless Sign Up<br><br></A> <A class="link" href="account/login.aspx">Return 
							to Account Login</A>
					</div>
				</div>
			</div>
		</form>
	</body>
</HTML>
