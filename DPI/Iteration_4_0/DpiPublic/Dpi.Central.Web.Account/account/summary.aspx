<%@ Page language="c#" Codebehind="summary.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.SummaryPage" %>
<%@ Register TagPrefix="tb" TagName="Tabs" Src="~/account/tabs.ascx" %>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="accountSummaryForm" method="post" runat="server">
			<div class="wide_form">
				<div class="row">
					<tb:tabs id="m_tabs" runat="server"></tb:tabs>
				</div>
				<div class="row">
					<table width="100%" cellpadding="1" cellspacing="2" border="0">
						<tr>
							<td class="header" colSpan="2">Customer Information</td>
						</tr>
						<tr>
							<td width="40%">Account Number:</td>
							<td width="60%"><asp:label id="lblAccNumber" runat="server" Width="100%"></asp:label></td>
						</tr>
						<tr>
							<td>Phone Number:</td>
							<td><asp:label id="lblPhoneNumber" runat="server" Width="100%"></asp:label></td>
						</tr>
						<tr>
							<td>Customer Name:</td>
							<td><asp:label id="lblCustomerName" runat="server" Width="100%"></asp:label></td>
						</tr>
						<tr>
							<td>Service Address:</td>
							<td><asp:label id="lblAddress" runat="server" Width="100%"></asp:label></td>
						</tr>
						<tr>
							<td></td>
							<td><asp:label id="lblCityStateZip" runat="server" Width="100%" Height="8px"></asp:label></td>
						</tr>
						<tr id="rowActivationDate" runat="server">
							<td><asp:label id="lblActivDateCap" runat="server" Width="112px">Activation Date</asp:label></td>
							<td><asp:label id="lblActivDate" runat="server" Width="100%"></asp:label></td>
						</tr>
						<tr>
							<td>Status:</td>
							<td><asp:label id="lblStatus" runat="server" Width="100%" Height="8px"></asp:label></td>
						</tr>
						<tr>
							<td>Due Date:</td>
							<td><asp:label id="lblDueDate" runat="server" Width="100%"></asp:label></td>
						</tr>
						<tr>
							<td class="05_con_bold"><font color="red"></font></td>
							<td class="05_con_bold"><font color="red"><asp:label id="lblLastDay" runat="server" Width="100%" Visible="False"></asp:label></font></td>
						</tr>
						<tr>
							<td class="header" colSpan="2">Reminder Notice</td>
						</tr>
						<tr>
							<td vAlign="middle" colSpan="2"><asp:imagebutton id="btnPaymentForecast" runat="server" CausesValidation="False" ImageUrl="../images/btn_view_payment_forecast.gif"></asp:imagebutton><img src="../images/blank.gif"><asp:imagebutton id="btnCustomerBill" runat="server" ImageUrl="../images/btn_view_customer_bill.gif"
									CausesValidation="False"></asp:imagebutton></td>
						</tr>
						<tr>
							<td class="header" colSpan="2">Account Summary</td>
						</tr>
						<tr>
							<td align="right">Balance Forward (from a prior bill period)</td>
							<td align="right"><asp:label id="lblBalForward" runat="server" Width="106px"></asp:label></td>
						</tr>
						<tr>
							<td align="right" bgColor="whitesmoke">Current Charges</td>
							<td align="right" bgColor="whitesmoke"><asp:label id="lblCurrCharges" runat="server" Width="106px"></asp:label></td>
						</tr>
						<tr>
							<td align="right"><strong>Total&nbsp;Amount Due</strong></td>
							<td align="right"><asp:label id="lblAmountDue" runat="server" Width="106px"></asp:label></td>
						</tr>
					</table>
				</div>
			</div>
		</form>
	</body>
</HTML>
