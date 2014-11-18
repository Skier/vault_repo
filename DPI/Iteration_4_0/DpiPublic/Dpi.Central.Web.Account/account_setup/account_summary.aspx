<%@ Page language="c#" Codebehind="account_summary.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.AccountSetup.AccountSummary" %>
<%@ Register TagPrefix="dwc" TagName="AccountSummary" Src="~/account_setup/account_summary.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC - Account Summary</title>
		<meta content="False" name="vs_showGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="AccountSummaryForm" method="post" runat="server">
			<div class="process_form">
				<div class="step_caption">
					Congratulations! Your account has been setup successfully!
				</div>
				<dwc:accountsummary id="m_ctrlAccountSummary" runat="server"></dwc:accountsummary>
				<div class="spacer">&nbsp;</div>
				<div class="button_row">
					<span class="back_button"><a href="account_summary_print_version.aspx" target="_blank"><IMG border="0" alt="" src="../images/btn_print_version.gif"></a></span>
					<span class="next_button">
						<asp:imagebutton id="m_btnLogin" runat="server" ImageUrl="../images/btn_proceed_to_login.gif"></asp:imagebutton></span>
				</div>
			</div>
		</form>
	</body>
</HTML>
