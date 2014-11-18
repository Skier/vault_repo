<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Register TagPrefix="tb" TagName="Tabs" Src="~/account/tabs.ascx" %>
<%@ Page language="c#" Codebehind="summary.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.SummaryPage" %>
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
			<tb:tabs id="m_tabs" runat="server"></tb:tabs>
			<div class="wide_form">
				<dwc:panel id="pnlCustomerInfo" runat="server">
					<TABLE class="table_form" cellSpacing="1" cellPadding="0" width="100%" border="0">
						<TR>
							<TD class="title" colSpan="2">Customer Information</TD>
						</TR>
						<TR>
							<TD width="40%">Account Number</TD>
							<TD width="60%">
								<asp:label id="lblAccNumber" runat="server" Width="100%" CssClass="value"></asp:label></TD>
						</TR>
						<TR>
							<TD>Phone Number</TD>
							<TD>
								<asp:label id="lblPhoneNumber" runat="server" Width="100%" CssClass="value"></asp:label></TD>
						</TR>
						<TR>
							<TD>Customer Name</TD>
							<TD>
								<asp:label id="lblCustomerName" runat="server" Width="100%" CssClass="value"></asp:label></TD>
						</TR>
						<TR>
							<TD>Service Address</TD>
							<TD>
								<asp:label id="lblAddress" runat="server" Width="100%" CssClass="value"></asp:label></TD>
						</TR>
						<TR>
							<TD></TD>
							<TD>
								<asp:label id="lblCityStateZip" runat="server" Width="100%" Height="8px" CssClass="value"></asp:label></TD>
						</TR>
						<TR id="rowActivationDate" runat="server">
							<TD>Activation Date</TD>
							<TD>
								<asp:label id="lblActivDate" runat="server" Width="100%" CssClass="value"></asp:label></TD>
						</TR>
						<TR>
							<TD>Status</TD>
							<TD>
								<asp:label id="lblStatus" runat="server" Width="100%" CssClass="value"></asp:label></TD>
						</TR>
						<TR>
							<TD>Due Date</TD>
							<TD>
								<asp:label id="lblDueDate" runat="server" Width="100%" CssClass="value"></asp:label></TD>
						</TR>
					</TABLE>
				</dwc:panel>
				<dwc:panel id="pnlAccountSummary" runat="server" style="MARGIN-TOP: 10px">
					<TABLE class="table_form" cellSpacing="1" cellPadding="0" width="100%" border="0">
						<TR>
							<TD class="title" colSpan="2">Account Summary</TD>
						</TR>
						<TR>
							<TD width="40%">Balance Forward <span class="label_note">(from a prior bill period)</span></TD>
							<TD width="60%">
								<asp:label id="lblBalForward" runat="server" Width="100%" CssClass="value"></asp:label></TD>
						</TR>
						<TR>
							<TD>Current Charges</TD>
							<TD>
								<asp:label id="lblCurrCharges" runat="server" Width="100%" CssClass="value"></asp:label></TD>
						</TR>
						<TR>
							<TD>Total Amount Due</TD>
							<TD>
								<asp:label id="lblAmountDue" runat="server" Width="100%" CssClass="value"></asp:label></TD>
						</TR>
					</TABLE>
				</dwc:panel>
				<table cellSpacing="0" cellPadding="0" width="100%" border="0" style="MARGIN-TOP: 10px;">
					<tr>
						<td class="buttons" style="MARGIN-TOP: 17px; PADDING-BOTTOM: 2px; PADDING-TOP: 5px">
							<asp:imagebutton id="btnPaymentForecast" runat="server" ImageUrl="../images/btn_view_payment_forecast.gif"
								CausesValidation="False"></asp:imagebutton><IMG src="../images/blank.gif">
							<asp:imagebutton id="btnCustomerBill" runat="server" ImageUrl="../images/btn_view_customer_bill.gif"
								CausesValidation="False"></asp:imagebutton></td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</HTML>
