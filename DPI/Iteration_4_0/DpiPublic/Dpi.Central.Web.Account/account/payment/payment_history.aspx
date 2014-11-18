<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Register TagPrefix="tb" TagName="Tabs" Src="~/account/tabs.ascx" %>
<%@ Page language="c#" Codebehind="payment_history.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Account.Payment.PaymentHistoryPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Payment History</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="paymentHistoryForm" method="post" runat="server">
			<div class="form">
				<div class="tab_row">
					<tb:Tabs id="m_tabs" runat="server"></tb:Tabs>
				</div>
				<table>
					<TR>
						<TD width="150"><span class="label">Payment Type</span></TD>
						<TD width="200"><span class="label">Payments From&nbsp;<span class="label_note">(mm/dd/yyyy)</span></span></TD>
						<TD width="200"><span class="label">Payments To&nbsp;<span class="label_note">(mm/dd/yyyy)</span></span></TD>
					</TR>
					<TR>
						<TD><span class="value">
								<asp:dropdownlist id="ddlPaymentType" runat="server">
									<asp:ListItem Selected="True">All</asp:ListItem>
									<asp:ListItem Value="Credit">Credit Card</asp:ListItem>
									<asp:ListItem Value="Check">Check</asp:ListItem>
								</asp:dropdownlist></span>
						</TD>
						<TD>
							<span class="value">
								<dwc:datebox id="dtFrom" runat="server"></dwc:datebox>
							</span>
						</TD>
						<TD>
							<span class="value">
								<dwc:datebox id="dtTo" runat="server"></dwc:datebox><asp:customvalidator id="vldCstPaymentDateRange" runat="server" ErrorMessage="<br>The Payment Date Range provided is invalid"
									Display="Dynamic"></asp:customvalidator>
							</span>
						</TD>
					</TR>
					<TR>
						<TD colspan="3" align="left">
							<asp:imagebutton id="btnSubmit" runat="server" ImageUrl="../../images/submit.jpg"></asp:imagebutton>
						</TD>
					</TR>
				</table>
			</div>
			<div class="wide_form">
				<asp:datagrid id="dgPayments" runat="server" AutoGenerateColumns="False" EnableViewState="False"
					PageSize="20" Width="100%">
					<AlternatingItemStyle CssClass="Grid_AlternatingItem"></AlternatingItemStyle>
					<HeaderStyle BackColor="Chocolate"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="Amount" HeaderText="Payment Amount" DataFormatString="{0:C}"></asp:BoundColumn>
						<asp:BoundColumn HeaderText="Payment Type"></asp:BoundColumn>
						<asp:BoundColumn DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:d}"></asp:BoundColumn>
						<asp:BoundColumn HeaderText="Payment Status"></asp:BoundColumn>
					</Columns>
				</asp:datagrid>				
			</div>
		</form>
	</body>
</HTML>
