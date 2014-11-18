<%@ Page language="c#" Codebehind="receipt.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Wireless.Processes.Rsp.AccountSummary" %>
<%@ Register TagPrefix="dwc" TagName="WirelessReceipt" Src="~/wireless/rsp/receipt.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Receipt</title>
		<meta content="False" name="vs_showGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="wirelessReceiptForm" method="post" runat="server">
			<div class="process_form">
				<div class="step_caption">
					Congratulations! You have recharged the plan successfully!
				</div>
				<dwc:WirelessReceipt id="ctrlWirelessReceipt" runat="server"></dwc:WirelessReceipt>
				<div class="spacer">&nbsp;</div>
				<div class="button_row">
					<span class="back_button"><a href="receipt_print_version.aspx" target="_blank"><IMG border="0" alt="" src="../../images/btn_print_version.gif"></a></span>				
				</div>
			</div>
		</form>
	</body>
</HTML>
