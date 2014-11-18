<%@ Page CodeBehind="index.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Dpi.Central.Web.IndexPage" %>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta content="dPi Teleconnect LLC, Pre-Paid Home Phone Service, Pre-Paid Long Distance, Pre-Paid Cell Phones, Pre-Paid MasterCards and Pre-Paid Internet Service."
			name="description">
		<meta content="PrePaid Home Phone, PrePaid Long Distance, PrePaid Cell Phones, PrePaid MasterCards, PrePaid Internet, PrePaid Cable, PrePaid Electric, cellular phone, mobile phone, wireless phone, internet service provider, isp, long distance calling cards, prepaid credit card, prepaid debit card"
			name="keywords">
		<META content="General" name="rating">
		<META content="14 days" name="revisit-after">
		<META content="ALL" name="ROBOTS">
		<LINK href="DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="indexForm" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="775" border="0" style="MARGIN-LEFT: 8px">
				<tr>
					<!-- Account Login Panal -->
					<td colspan="2" style="PADDING-BOTTOM: 10px; WIDTH: 50%; PADDING-TOP: 10px">
						<dwc:panel id="pnlPublicAccountLogin" runat="server">
							<DIV style="FONT-WEIGHT: bold; FONT-SIZE: 11pt; MARGIN-BOTTOM: 5px; COLOR: chocolate; PADDING-TOP: 5px; HEIGHT: 25px; BACKGROUND-COLOR: whitesmoke; TEXT-ALIGN: center">View 
								Your Account, Pay Bills Online</DIV>
							<DIV class="row"><SPAN class="label">Phone Number</SPAN> <SPAN class="value">
									<dwc:phonenumberbox id="phnPhoneNumber" tabIndex="1" runat="server"></dwc:phonenumberbox>
									<asp:customvalidator id="vldCstPhoneNumber" runat="server" ControlToValidate="phnPhoneNumber" ErrorMessage="<br>The Phone Number provided is invalid"
										Display="Dynamic" ClientValidationFunction="ValidatePhoneNumber"></asp:customvalidator></SPAN></DIV>
							<DIV class="row"><SPAN class="label">Account Number</SPAN> <SPAN class="value">
									<asp:textbox id="txtAccountNumber" tabIndex="4" runat="server" MaxLength="8" CssClass="wide_field"></asp:textbox>
									<asp:RegularExpressionValidator id="vldReAccountNumber" runat="server" ControlToValidate="txtAccountNumber" ErrorMessage="The Account Number provided is invalid"
										Display="Dynamic" ValidationExpression="^\d{8}$"></asp:RegularExpressionValidator></SPAN></DIV>
							<DIV class="row"><SPAN class="label_note">(Enter either your Phone Number or Account 
									Number)<IMG alt="" src="images/asterisk.gif"></SPAN> <SPAN class="value">
									<asp:CustomValidator id="vldCstIdentity" runat="server" ErrorMessage="Both Phone Number and Account Number cannot be left blank"
										Display="Dynamic"></asp:CustomValidator></SPAN></DIV>
							<DIV class="row"><SPAN class="label">Password<IMG alt="" src="images/asterisk.gif"></SPAN>
								<SPAN class="value">
									<asp:textbox id="txtPassword" tabIndex="5" runat="server" MaxLength="25" CssClass="wide_field"
										TextMode="Password"></asp:textbox>
									<asp:requiredfieldvalidator id="vldRfPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Required field cannot be left blank"
										Display="Dynamic"></asp:requiredfieldvalidator>
									<asp:regularexpressionvalidator id="vldRePassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="The Password must have at least 6 and not be longer then 25 characters"
										Display="Dynamic" ValidationExpression="^.{6,25}$"></asp:regularexpressionvalidator></SPAN></DIV>
							<DIV class="spacer">&nbsp;</DIV>
							<DIV class="row"><SPAN class="label"><A class="link" id="lnkPasswordReminder" style="FLOAT: left; WIDTH: 200px" tabIndex="7"
										href="../password_reminder.aspx" runat="server">Forgot My Password</A><BR>
									<A class="link" id="lnkWebAccessSignUp" style="FLOAT: left; WIDTH: 200px" tabIndex="8"
										href="../signup.aspx" runat="server">Web Access Sign Up</A></SPAN> <SPAN class="value" style="WIDTH: 100px; TEXT-ALIGN: right">
									<asp:imagebutton id="btnSubmit" tabIndex="6" runat="server" ImageUrl="images/submit.jpg"></asp:imagebutton></SPAN></DIV>
						</dwc:panel>
					</td>
					<td colspan="2" align="center" valign="middle" style="PADDING-LEFT: 5px; PADDING-BOTTOM: 0px; WIDTH: 50%; PADDING-TOP: 0px;">
						<img src="images/stub_energy.jpg">
					</td>
				</tr>
				<tr valign="top">
					<td>
						<asp:HyperLink id="lnkNewAccount1" runat="server">
							<IMG src="images/hphone_circle.jpg" border="0"></asp:HyperLink>
					</td>
					<td style="PADDING-LEFT: 5px; FONT-SIZE: 12px; PADDING-TOP: 5px">
						<div style="MARGIN-BOTTOM: 20px; COLOR: chocolate">
							Get Reliable Home Phone Service.
						</div>
						<div style="FONT-SIZE: 11px; COLOR: #6b6b6b; HEIGHT: 60px">
							Get reliable home phone service, low rates on long distance service, and low 
							cost internet access.
						</div>
						<asp:HyperLink id="lnkNewAccount2" runat="server" Font-Size="11px">Click Here&gt;</asp:HyperLink>
					</td>
					<td>
						<A href="ppc.aspx" target="_self"><IMG src="images/cphone_circle.jpg" border="0"></A>
					</td>
					<td style="PADDING-LEFT: 5px; FONT-SIZE: 12px; PADDING-TOP: 5px">
						<div style="MARGIN-BOTTOM: 20px; COLOR: chocolate">Get a No-Hassle Cell Phone 
							Today.</div>
						<div style="FONT-SIZE: 11px; COLOR: #6b6b6b; HEIGHT: 60px">
							Choose from packages to fit any budget.
						</div>
						<div style="MARGIN-BOTTOM: 10px">
							<A href="ppc.aspx" target="_self" style="FONT-SIZE: 11px">Click Here&gt;</A>
						</div>
						<asp:HyperLink id="lnkWirelessReplenish" runat="server" Font-Size="11px">Refill Wireless Account&gt;</asp:HyperLink>
					</td>
				</tr>
			</table>
			<IMG src="images/footer.jpg" border="0" style="MARGIN-TOP: 20px">
			<dwc:Footer id="ctrlFooter" runat="server" class="page_footer_stand_along"></dwc:Footer>
		</form>
	</body>
</HTML>
