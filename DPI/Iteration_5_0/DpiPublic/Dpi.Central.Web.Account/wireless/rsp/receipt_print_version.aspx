<%@ Register TagPrefix="dwc" TagName="WirelessReceipt" Src="~/wireless/rsp/receipt.ascx" %>
<%@ Page language="c#" Codebehind="receipt_print_version.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Wireless.Processes.Rsp.AccountSummaryPrintVersionPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Receipt</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="AccountSummaryPrintVersionForm" method="post" runat="server">
			<div class="report">
				<div class="report_row">
					<IMG alt="Report Header" src="../../images/report_header.jpg">
				</div>
				<div class="spacer">&nbsp;</div>
				<dwc:WirelessReceipt id="ctrlWirelessReceipt" runat="server"></dwc:WirelessReceipt>
				<div class="spacer">&nbsp;</div>
			</div>
		</form>
	</body>
</HTML>
