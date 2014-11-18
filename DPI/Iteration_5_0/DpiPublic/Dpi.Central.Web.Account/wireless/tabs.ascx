<%@ Control Language="c#" AutoEventWireup="false" Codebehind="tabs.ascx.cs" Inherits="Dpi.Central.Web.Account.Wireless.Tabs" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="dwc" Namespace="Dpi.Central.Web.Controls" Assembly="Dpi.Central.Web.Common" %>
<dwc:tabcontrol id="_tabControl" runat="server" CssClass="tab">
	<Tabs>
		<dwc:Tab Title="Service Information" Tag="service_info.aspx"></dwc:Tab>
		<dwc:Tab Title="Customer Information" Tag="customer_info.aspx"></dwc:Tab>
	</Tabs>
</dwc:tabcontrol>
