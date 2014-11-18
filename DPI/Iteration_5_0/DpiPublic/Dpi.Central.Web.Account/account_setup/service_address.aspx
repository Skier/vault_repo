<%@ Register TagPrefix="duc" TagName="AddressInfo" Src="~/account_setup/address_info.ascx" %>
<%@ Page language="c#" Codebehind="service_address.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.AccountSetup.ServiceAddressPage" %>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Service Address</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
		<script language="JavaScript">
			function SynchronizeItemsWithIndicator(srcId, dstId, chkId)
			{
				if (document.getElementById(chkId).checked) {
					if (document.getElementById(dstId).selectedIndex && document.getElementById(srcId).selectedIndex) {
						document.getElementById(dstId).selectedIndex = document.getElementById(srcId).selectedIndex;
					} else {
						document.getElementById(dstId).value = document.getElementById(srcId).value;
					}
				}
			}	
			
			function SynchronizeItemsWithTrigger(srcId, dstId, chkId)
			{
				if (document.getElementById(chkId).checked) {
					SynchronizeItemsWithIndicator(srcId, dstId, chkId);
					if (document.getElementById(dstId).selectedIndex && document.getElementById(srcId).selectedIndex) {
						document.getElementById(dstId).selectedIndex = document.getElementById(srcId).selectedIndex;
					} else {
						document.getElementById(dstId).value = document.getElementById(srcId).value;
					}
				} else {
					if (document.getElementById(dstId).selectedIndex) {
						document.getElementById(dstId).selectedIndex = 0;
					} else {
						document.getElementById(dstId).value = '';
					}
				}
			}	
		</script>
	</HEAD>
	<body runat="server" id="_body">
		<form id="serviceAddressForm" method="post" runat="server">
			<div class="process_form">
				<div class="step_caption" style="WHITE-SPACE: nowrap">
					<span style="WIDTH: 50%">Please provide the following account information</span>
					<span style="WIDTH: 50%; MARGIN-RIGHT: 9px; TEXT-ALIGN: right" class="zip_code_header">
						ZipCode:<asp:label id="lblZipCode" runat="server"></asp:label>
					</span>
				</div>
				<div class="process_form_section">
					<div class="section_title">Customer Information</div>
					<div class="row"><span class="label">First Name<IMG alt="" src="../images/asterisk.gif"></span>
						<span class="value">
							<asp:textbox id="txtFirstName" runat="server" CssClass="wide_field" MaxLength="25"></asp:textbox><asp:requiredfieldvalidator id="vldRfFirstName" runat="server" ControlToValidate="txtFirstName" Display="Dynamic"
								ErrorMessage="<br>Required field cannot be left blank"></asp:requiredfieldvalidator></span></div>
					<div class="row"><span class="label">Last Name<IMG alt="" src="../images/asterisk.gif"> </span>
						<span class="value">
							<asp:textbox id="txtLastName" runat="server" CssClass="wide_field" MaxLength="30"></asp:textbox><asp:requiredfieldvalidator id="vldRfLastName" runat="server" ControlToValidate="txtLastName" Display="Dynamic"
								ErrorMessage="<br>Required field cannot be left blank"></asp:requiredfieldvalidator></span></div>
					<DIV class="row"><SPAN class="label">Email<IMG alt="" src="../images/asterisk.gif"></SPAN>
						<span class="value">
							<asp:textbox id="txtEmail" runat="server" CssClass="wide_field" MaxLength="50"></asp:textbox><asp:regularexpressionvalidator id="vldReEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="<br>The Email provided is invalid"
								ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:regularexpressionvalidator><asp:requiredfieldvalidator id="vldRfEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="<br>Required field cannot be left blank"></asp:requiredfieldvalidator></span></DIV>
					<div class="row"><span class="label">Birthday (mm/dd/yyyy)</span> <span class="value">
							<dwc:datebox id="dateBirthday" runat="server"></dwc:datebox>
							<asp:CustomValidator id="vldCstDateBirthday" runat="server" ErrorMessage="<br>The Birthday is invalid"
								Display="Dynamic" ControlToValidate="dateBirthday"></asp:CustomValidator>
						</span>
					</div>
					<div class="row"><span class="label">1st Contact #<IMG alt="" src="../images/asterisk.gif"></span>
						<span class="value">
							<dwc:phonenumberbox id="phnFirstContact" runat="server"></dwc:phonenumberbox><asp:customvalidator id="vldCstFirstContact" runat="server" ControlToValidate="phnFirstContact" Display="Dynamic"
								ErrorMessage="<br>The 1st Contact # provided is invalid" ClientValidationFunction="ValidatePhoneNumber"></asp:customvalidator><asp:requiredfieldvalidator id="vldRfFirstContact" runat="server" ControlToValidate="phnFirstContact" Display="Dynamic"
								ErrorMessage="<br>Required field cannot be left blank"></asp:requiredfieldvalidator></span></div>
					<div class="row"><span class="label">2nd Contact #</span> <span class="value">
							<dwc:phonenumberbox id="phnSecondContact" runat="server"></dwc:phonenumberbox><asp:customvalidator id="vldCstSecondContact" runat="server" ControlToValidate="phnSecondContact" Display="Dynamic"
								ErrorMessage="<br>The 2nd Contact # provided is invalid" ClientValidationFunction="ValidatePhoneNumber"></asp:customvalidator></span></div>
					<div class="row"><span class="label">Previous Phone #</span> <span class="value">
							<dwc:phonenumberbox id="phnPrevPhone" runat="server"></dwc:phonenumberbox><asp:customvalidator id="vldCstPrevPhone" runat="server" ControlToValidate="phnPrevPhone" Display="Dynamic"
								ErrorMessage="<br>The Previous Phone # provided is invalid" ClientValidationFunction="ValidatePhoneNumber"></asp:customvalidator></span></div>
					<div class="row"><span class="label">Previous Phone Co.</span> <span class="value">
							<asp:dropdownlist id="ddlPhoneComp" runat="server" CssClass="state"></asp:dropdownlist></span></div>
				</div>
				<div id="serviceAddressDiv">
					<div class="process_form_section">
						<div class="section_title">Service Address</div>
						<div class="row"><asp:checkbox id="chkMailingAddress" runat="server" CssClass="midgray_small" Text="Uncheck the box if your mailing address will be different than your service address."
								Checked="True"></asp:checkbox></div>
						<div class="spacer">&nbsp;</div>
						<duc:addressinfo id="ctrlServiceAddress" runat="server"></duc:addressinfo>
					</div>
				</div>
				<div id="mailAddressDiv" runat="server">
					<div class="process_form_section">
						<div class="section_title">Mail Address</div>
						<duc:addressinfo id="ctrlMailAddress" runat="server"></duc:addressinfo>
					</div>
				</div>
				<div class="process_form_section">
					<div class="section_title">Web Access</div>
					<div class="row">
						<span class="statement">dPi Teleconnect provides you with the ability to access 
							your Account Online. You have the ability to view the progress of your new 
							order, view your most recent billing statement, process online payments and 
							much more!</span></div>
					<div class="spacer">&nbsp;</div>
					<div class="row">
						<span class="statement">Please provide a password to access your Online Account: </span>
					</div>
					<div class="row"><span class="label">Password<IMG alt="" src="../images/asterisk.gif"> </span>
						<span class="value">
							<asp:textbox id="txtPassword" runat="server" CssClass="password" MaxLength="25" TextMode="Password"></asp:textbox><asp:requiredfieldvalidator id="vldRfPassword" runat="server" ControlToValidate="txtPassword" Display="Dynamic"
								ErrorMessage="<br>Required field cannot be left blank"></asp:requiredfieldvalidator><asp:regularexpressionvalidator id="vldRePassword" runat="server" ControlToValidate="txtPassword" Display="Dynamic"
								ErrorMessage="<br>The Password must have at least 6 and not be longer then 25 characters" ValidationExpression="^.{6,25}$"></asp:regularexpressionvalidator></span></div>
					<div class="row"><span class="label">Confirm Password<IMG alt="" src="../images/asterisk.gif"></span>
						<span class="value">
							<asp:textbox id="txtConfirmPassword" runat="server" CssClass="password" MaxLength="25" TextMode="Password"></asp:textbox><asp:comparevalidator id="vldCmpPassword" runat="server" ControlToValidate="txtConfirmPassword" Display="Dynamic"
								ErrorMessage="<br>The Password and Confirm Password must match" ControlToCompare="txtPassword"></asp:comparevalidator><asp:requiredfieldvalidator id="vldRfConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
								Display="Dynamic" ErrorMessage="<br>Required field cannot be left blank"></asp:requiredfieldvalidator></span></div>
				</div>
				<div class="process_form_section">
					<div class="section_title">Refund Policy</div>
					<div class="row">
						<span class="statement">Refunds are made only if DPI cannot establish the service 
							you are requesting. Refunds are mailed directly from the Dallas, TX office and 
							typically arrive in 7-10 business days.</span>
					</div>
					<div class="spacer">&nbsp;</div>
				</div>
				<div class="button_row">
					<span class="back_button">
						<asp:imagebutton id="m_btnOnEnterStub" runat="server" Width="0px" Height="0px"></asp:imagebutton>
						<asp:ImageButton id="btnBack" runat="server" ImageUrl="../images/btn_back.gif" CausesValidation="False"></asp:ImageButton></span><span class="next_button">
						<asp:imagebutton id="btnPayCreditCard" runat="server" ImageUrl="../images/btn_pay_by_credit_card.gif"></asp:imagebutton><img src="../images/blank.gif">
						<asp:imagebutton id="btnPayCheck" runat="server" ImageUrl="../images/btn_pay_by_check.gif"></asp:imagebutton></span>
				</div>
			</div>
		</form>
	</body>
</HTML>
