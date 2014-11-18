<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Register TagPrefix="tb" TagName="Tabs" Src="~/account/tabs.ascx" %>
<%@ Page language="c#" Codebehind="order_status.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.OrderStatus" %>
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
		<form id="ordersForm" method="post" runat="server">
			<tb:Tabs id="m_tabs" runat="server"></tb:Tabs>
			<div class="wide_form">
				<ASP:DATAGRID id="grdOrderStatuses" runat="server" CellPadding="3" ENABLEVIEWSTATE="False" pagesize="20"
					autogeneratecolumns="False" allowsorting="True" Width="100%">
					<AlternatingItemStyle CssClass="Grid_AlternatingItem"></AlternatingItemStyle>
					<HeaderStyle BackColor="Chocolate"></HeaderStyle>
					<Columns>
						<asp:BoundColumn DataField="Type" HeaderText="Activity Type">
							<HeaderStyle ForeColor="WhiteSmoke" Width="80px"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="Description" HeaderText="Description">
							<HeaderStyle ForeColor="WhiteSmoke" Width="160px"></HeaderStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="WorkStart" HeaderText="Begin" DataFormatString="{0:d}">
							<HeaderStyle ForeColor="WhiteSmoke"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="WorkFinish" HeaderText="End" DataFormatString="{0:d}">
							<HeaderStyle ForeColor="WhiteSmoke"></HeaderStyle>
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
					</Columns>
					<PagerStyle Mode="NumericPages"></PagerStyle>
				</ASP:DATAGRID>
			</div>
		</form>
	</body>
</HTML>
