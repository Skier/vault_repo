<%@ Page language="c#" Codebehind="tpv_agreement.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.AccountSetup.TpvAgreementPage" %>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • TPV Agreement</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="tpvAgreementForm" method="post" runat="server">
			<div class="process_form">
				<div class="step_caption">
					Conversion Verification
				</div>
				<div class="row">
					<span class="label">Letter of Agency to Switch Telephone Companies</span>
				</div>
				<div class="spacer">&nbsp;</div>
				<div class="row">
					<span class="statement">By agreeing below, I am authorizing dPi Teleconnect to 
						become my new telephone service provider in place of my current provider for 
						local exchange telecommunications services. I authorize dPi Teleconnect to act 
						as my agent to make this change happen, and direct my current telephone company 
						to work with dPi Teleconnect to effect this change. I further certify that I am 
						at least eighteen years of age, and that I am authorized to change telephone 
						companies for services to the telephone number listed below. I understand and 
						agree that my telephone service will be converted to dPi Teleconnect however, 
						certain features or functionalities of my current service may change </span>
				</div>
				<div class="spacer">&nbsp;</div>
				<div class="process_form_section">
					<div class="row"><span class="label">Birthday (month/day/year)</span> <span class="value">
							<dwc:datebox id="txtBirthday" runat="server"></dwc:datebox><asp:customvalidator id="vldCstBirthday" runat="server" ControlToValidate="txtBirthday" Display="Dynamic"
								ErrorMessage="The Birthday provided is invalid"></asp:customvalidator></span></div>
				</div>
				<div class="button_row">
					<span class="back_button">
						<asp:imagebutton id="btnPrevious" runat="server" CausesValidation="False" ImageUrl="../images/btn_back.gif"></asp:imagebutton></span>
					<span class="next_button">
						<asp:imagebutton id="btnAgree" runat="server" ImageUrl="../images/btn_agree.gif"></asp:imagebutton><img src="../images/blank.gif">
						<asp:imagebutton id="btnDisagree" runat="server" ImageUrl="../images/btn_disagree.gif" CausesValidation="False"></asp:imagebutton>
					</span>
				</div>
			</div>
		</form>
	</body>
</HTML>
