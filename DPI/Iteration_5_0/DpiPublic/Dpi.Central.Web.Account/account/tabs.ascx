<%@ Control Language="c#" AutoEventWireup="false" Codebehind="tabs.ascx.cs" Inherits="Dpi.Central.Web.Account.Tabs" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="dwc" Namespace="Dpi.Central.Web.Controls" Assembly="Dpi.Central.Web.Common" %>
<dwc:tabcontrol id="_tabControl" runat="server" CssClass="tab" Width="773px" style="MARGIN-LEFT: 8px">
	<Tabs>
		<dwc:Tab Title="Summary" Tag="~/account/summary.aspx"></dwc:Tab>
		<dwc:Tab Title="Settings" Tag="~/account/account_settings.aspx"></dwc:Tab>
		<dwc:Tab Title="Order Status" Tag="~/account/order_status.aspx"></dwc:Tab>
		<dwc:Tab Title="Make A Payment" Tag="~/account/payment/payment_selection.aspx"></dwc:Tab>
		<dwc:Tab Title="Promise To Pay" Tag="~/account/payment/promise_to_pay.aspx"></dwc:Tab>
		<dwc:Tab Title="Recurring Payments" Tag="~/account/payment/rec_payments.aspx"></dwc:Tab>
	</Tabs>
</dwc:tabcontrol>
