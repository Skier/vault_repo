<%@ Register TagPrefix="duc" TagName="SubscriberInfoViewer" Src="~/wireless/subscriber_info_viewer.ascx"%>
<%@ Register TagPrefix="duc" TagName="ServicePlanDescriptionViewer" Src="~/wireless/service_plan_description_viewer.ascx"%>
<%@ Register TagPrefix="duc" TagName="FeatureUsageViewer" Src="~/wireless/feature_usage_viewer.ascx"%>
<%@ Register TagPrefix="duc" TagName="PinDescriptionViewer" Src="~/wireless/pin_description_viewer.ascx"%>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Register TagPrefix="tb" TagName="Tabs" Src="~/wireless/tabs.ascx" %>
<%@ Page language="c#" Codebehind="service_info.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Wireless.ServiceInfoPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Service Information</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="serviceInfoForm" method="post" runat="server">
			<table cellSpacing="8" cellPadding="0" width="788" border="0">
				<tr>
					<td colspan="2">
						<tb:tabs id="tbsTabs" runat="server"></tb:tabs>
					</td>
				</tr>
				<tr>
					<td width="50%" height="100%">
						<dwc:panel id="pnlPinDescriptionViewer" runat="server">
							<DUC:PINDESCRIPTIONVIEWER id="ctrlPinDescriptionViewer" runat="server"></DUC:PINDESCRIPTIONVIEWER>
						</dwc:panel>
					</td>
					<td>
						<dwc:panel id="pnlServicePlanDescriptionViewer" runat="server">
							<DUC:SERVICEPLANDESCRIPTIONVIEWER id="ctrlServicePlanDescription" runat="server"></DUC:SERVICEPLANDESCRIPTIONVIEWER>
						</dwc:panel>
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="772" border="0" style="MARGIN-TOP: 2px; MARGIN-LEFT: 8px">
				<tr>
					<td class="buttons" style="MARGIN-TOP: 17px; PADDING-BOTTOM: 2px; PADDING-TOP: 5px">
						<asp:ImageButton id="btnRechargeSamePlan" runat="server" ImageUrl="~/images/btn_recharge_same_plan.gif"></asp:ImageButton>
						<asp:ImageButton id="btnRechargeDifferentPlan" runat="server" ImageUrl="~/images/btn_recharge_different_plan.gif"></asp:ImageButton>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
