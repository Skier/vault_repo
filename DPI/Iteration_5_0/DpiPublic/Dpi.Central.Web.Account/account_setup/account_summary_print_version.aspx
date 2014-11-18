<%@ Page language="c#" Codebehind="account_summary_print_version.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.AccountSetup.AccountSummaryPrintVersionPage" %>
<%@ Register TagPrefix="dwc" TagName="AccountSummary" Src="~/account_setup/account_summary.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC - Account Summary</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="AccountSummaryPrintVersionForm" method="post" runat="server">
			<div class="report">
				<div class="report_row">
					<IMG alt="Report Header" src="../images/report_header.jpg">
				</div>
				<div class="report_row">
					<span class="report_header">Customer Service Toll Free 1-800-350-4009<br>
						customerservice@dpiteleconnect.com</span>
				</div>
				<div class="spacer">&nbsp;</div>
				<dwc:accountsummary id="m_ctrlAccountSummary" runat="server"></dwc:accountsummary>
				<div class="spacer">&nbsp;</div>
			</div>
		</form>
	</body>
</HTML>
