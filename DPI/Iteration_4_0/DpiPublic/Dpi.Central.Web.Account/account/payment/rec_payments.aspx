<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Register TagPrefix="tb" TagName="Tabs" Src="~/account/tabs.ascx" %>
<%@ Page language="c#" Codebehind="rec_payments.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Payment.RecurringPaymentsPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="recurringPaymentsForm" method="post" runat="server">
			<table class="layout_table">
				<tr>
					<td colspan="2"><tb:Tabs id="m_tabs" runat="server"></tb:Tabs></td>
				</tr>
				<TR>
					<TD class="left_padding"></TD>
					<TD></TD>
				</TR>
				<tr class="separator_row">
					<td colSpan="2">
						<div class="row"><span class="statement"> Enrolling in Recurring Payments enables dPi 
								Teleconnect to process a payment on a monthly basis by using the Credit Card or 
								Check information provided. A payment will automatically be processed every 
								month on the Due Date indicated on your monthly statement. </span>
						</div>
					</td>
				</tr>
				<tr>
					<td align="right" colSpan="2">
						<table class="linkTable" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td noWrap><asp:linkbutton id="lbtnAddCCPayment" runat="server">
													<img src="../../images/icon_credit_card.gif" alt="">Add Credit Card Payment</asp:linkbutton></td>
								<td noWrap><asp:linkbutton id="lbtnAddCheckPayment" runat="server">
													<img src="../../images/icon_cheque.gif" alt="">Add Bank Payment</asp:linkbutton></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR class="separator_row">
					<TD colSpan="2"></TD>
				</TR>
				<tr>
					<td align="left" colspan="2"><asp:datagrid id="dgPayments" runat="server" DataKeyField="Id" AutoGenerateColumns="False" Width="100%">
							<AlternatingItemStyle CssClass="Grid_AlternatingItem"></AlternatingItemStyle>
							<HeaderStyle Font-Italic="True" BackColor="Chocolate"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="Type">
									<ItemTemplate>
										<asp:Label runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Status">
									<ItemTemplate>
										<asp:Label runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Last 4 Digits">
									<ItemTemplate>
										<asp:Label runat="server"></asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox runat="server"></asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" HeaderText="Modify" CancelText="Cancel"
									EditText="Modify"></asp:EditCommandColumn>
								<asp:ButtonColumn Text="Deactivate" HeaderText="Change Status" CommandName="ChangeStatus"></asp:ButtonColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
