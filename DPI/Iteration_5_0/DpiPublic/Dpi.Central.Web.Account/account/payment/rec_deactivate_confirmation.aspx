<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Register TagPrefix="tb" TagName="Tabs" Src="~/account/tabs.ascx" %>
<%@ Page language="c#" Codebehind="rec_deactivate_confirmation.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Payment.DeactivateConfirmationPage" %>
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
			<tb:Tabs id="Tabs" runat="server"></tb:Tabs>
			<div class="wide_form">
				<div class="row">
					<asp:Label id="lblPaymentQuestion" runat="server" CssClass="statement"></asp:Label>
				</div>
				<div class="row" style="FLOAT: right">
					<asp:ImageButton id="btnSubmit" runat="server" ImageUrl="../../images/btn_yes.gif"></asp:ImageButton>
					<img src="../../images/blank.gif">
					<asp:ImageButton id="btnCancel" runat="server" ImageUrl="../../images/btn_no.gif"></asp:ImageButton></BIV>
				</div>
			</div>
		</form>
	</body>
</HTML>
