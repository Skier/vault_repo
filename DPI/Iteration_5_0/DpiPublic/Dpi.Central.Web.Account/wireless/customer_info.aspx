<%@ Page language="c#" Codebehind="customer_info.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Wireless.CustomerInfoPage" %>
<%@ Register TagPrefix="tb" TagName="Tabs" Src="~/wireless/tabs.ascx" %>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Register TagPrefix="duc" TagName="CustomerInfoEditor" Src="~/wireless/customer_info_editor.ascx"%>
<%@ Register TagPrefix="duc" TagName="CustomerInfoViewer" Src="~/wireless/customer_info_viewer.ascx"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Customer Information</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="customerInfoForm" method="post" runat="server">
			<table cellSpacing="8" cellPadding="0" width="788" border="0">
				<tr>
					<td>
						<tb:tabs id="tbsTabs" runat="server"></tb:tabs>
					</td>
				</tr>
				<tr>
					<td><dwc:panel id="customerInfo" runat="server">
							<DUC:CUSTOMERINFOVIEWER id="ctrlCustomerInfoViewer" runat="server"></DUC:CUSTOMERINFOVIEWER>
							<DUC:CUSTOMERINFOEDITOR id="ctrlCustomerInfoEditor" runat="server"></DUC:CUSTOMERINFOEDITOR>
						</dwc:panel>
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="772" border="0" style="MARGIN-TOP: 2px; MARGIN-LEFT: 8px">
				<tr>
					<td class="buttons" style="MARGIN-TOP: 17px; PADDING-BOTTOM: 2px; PADDING-TOP: 5px">
						<asp:ImageButton id="btnChange" runat="server" ImageUrl="~/images/btn_change.gif"></asp:ImageButton>
						<asp:ImageButton id="btnUpdate" runat="server" ImageUrl="~/images/btn_update.gif"></asp:ImageButton>
						<asp:ImageButton id="btnCancel" runat="server" ImageUrl="~/images/btn_cancel.gif" CausesValidation="False"></asp:ImageButton>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
