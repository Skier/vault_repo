<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Register TagPrefix="duc" TagName="CreditCardInfo" Src="~/account/payment/cc_info.ascx" %>
<%@ Register TagPrefix="duc" TagName="CheckInfo" Src="~/account/payment/check_info.ascx" %>
<%@ Register TagPrefix="duc" TagName="AccountInfo" Src="~/account/payment/account_info.ascx" %>
<%@ Page language="c#" Codebehind="customer_info.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Wireless.Processes.Rwa.CustomerInfoPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Customer Information</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
		<script language="javascript" type="text/javascript">
			function SetInitialFocus() 
			{
				var field = document.getElementById('phnPhoneNumber_area_code');
				if (field != null || field != 'undefined') {
					field.focus();
				}
			}
		</script>
	</HEAD>
	<body id="_body" runat="server">
		<form id="customerInfoForm" method="post" runat="server">
			<div class="process_form">
				<div class="step_caption">Enter the phone number you want to refill
				</div>
				<div class="process_form_section">
					<div class="row"><span class="label">Phone Number<IMG alt="" src="../images/asterisk.gif"></span>
						<span class="value">
							<dwc:phonenumberbox id="phnPhoneNumber" runat="server"></dwc:phonenumberbox><asp:customvalidator id="vldCstPhoneNumber" runat="server" Display="Dynamic" ErrorMessage="<br>The Phone Number provided is invalid"></asp:customvalidator><asp:requiredfieldvalidator id="vldRfPhoneNumber" runat="server" Display="Dynamic" ErrorMessage="<br>Required field cannot be left blank"
								ControlToValidate="phnPhoneNumber"></asp:requiredfieldvalidator></span></div>
				</div>
				<div class="button_row"><span class="next_button"><asp:imagebutton id="btnProceed" runat="server" EnableViewState="False" ImageUrl="~/images/btn_proceed.gif"></asp:imagebutton></span></div>
			</div>
		</form>
	</body>
</HTML>
