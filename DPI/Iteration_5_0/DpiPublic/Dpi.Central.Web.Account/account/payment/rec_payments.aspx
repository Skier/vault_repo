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
			<tb:Tabs id="m_tabs" runat="server"></tb:Tabs>
			<div class="wide_form">
				<div class="row"><span class="statement"> Enrolling in Recurring Payments enables dPi 
						Teleconnect to process a payment on a monthly basis by using the Credit Card or 
						Check information provided. A payment will automatically be processed every 
						month on the Due Date indicated on your monthly statement. </span>
				</div>
				<div class="row" style="MARGIN-TOP: 10px; FLOAT: right">
					<asp:ImageButton id="btnAddCCPayment" runat="server" ImageUrl="../../images/btn_add_credit_card_payment.gif"></asp:ImageButton>
					<img src="../../images/blank.gif">
					<asp:ImageButton id="btnAddCheckPayment" runat="server" ImageUrl="../../images/btn_add_check_payment.gif"></asp:ImageButton>
				</div>
				<asp:datagrid id="dgPayments" runat="server" DataKeyField="Id" AutoGenerateColumns="False" Width="100%"
					style="MARGIN-TOP: 10px" CellPadding="3">
					<AlternatingItemStyle CssClass="Grid_AlternatingItem"></AlternatingItemStyle>
					<HeaderStyle BackColor="Chocolate"></HeaderStyle>
					<Columns>
						<asp:TemplateColumn HeaderText="Type">
							<HeaderStyle ForeColor="WhiteSmoke"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Status">
							<HeaderStyle ForeColor="WhiteSmoke"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server"></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Last 4 Digits">
							<HeaderStyle ForeColor="WhiteSmoke"></HeaderStyle>
							<ItemTemplate>
								<asp:Label runat="server"></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox runat="server"></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Update" HeaderText="Modify" CancelText="Cancel"
							EditText="Modify">
							<HeaderStyle ForeColor="WhiteSmoke"></HeaderStyle>
						</asp:EditCommandColumn>
						<asp:ButtonColumn Text="Deactivate" HeaderText="Change Status" CommandName="ChangeStatus">
							<HeaderStyle ForeColor="WhiteSmoke"></HeaderStyle>
						</asp:ButtonColumn>
					</Columns>
				</asp:datagrid>
			</div>
		</form>
	</body>
</HTML>
