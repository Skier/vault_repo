<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Page language="c#" Codebehind="rec_payment_manager.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.account.RecurringPaymentManagerPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>recurring_payments_manager</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td colSpan="2"><dns:header id="ctrlHeader" runat="server"></dns:header></td>
				</tr>
				<tr>
					<td rowSpan="2">
						<IMG alt="" src="../../images/about_side.jpg">
					</td>
					<td vAlign="top" align="left">
						<IMG src="../../images/ppc_top.jpg" border="0">
						<table class="layout_table">
							<TR>
								<TD class="left_padding"></TD>
								<TD><asp:label id="lblErrMsg" runat="server" ForeColor="Red" Visible="False"></asp:label></TD>
							</TR>
							<tr class="separator_row">
								<td colSpan="2"></td>
							</tr>
							<TR>
								<TD align="right" colSpan="2"><asp:linkbutton id="lbtnAddCCPayment" runat="server">Add New Credit Card Recurring Payment</asp:linkbutton></TD>
							</TR>
							<TR>
								<TD align="right" colSpan="2"><asp:linkbutton id="lbtnAddBankPayment" runat="server">Add New Bank Recurring Payment</asp:linkbutton></TD>
							</TR>
							<TR class="separator_row">
								<TD colSpan="2"></TD>
							</TR>
							<tr>
								<td class="left_padding"></td>
								<td><asp:datagrid id="dgPayments" runat="server" DataKeyField="Id" AutoGenerateColumns="False" Width="100%">
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
											<asp:ButtonColumn Text="Deactivate" HeaderText="Deactivate" CommandName="Deactivate"></asp:ButtonColumn>
										</Columns>
									</asp:datagrid></td>
							</tr>
						</table>
					</td>
				</tr>
				<TR>
					<TD vAlign="bottom" align="center" colSpan="2"><dns:footer id="ctrlFooter" runat="server"></dns:footer></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
