<%@ Register TagPrefix="tb" TagName="Tabs" Src="~/account/tabs.ascx" %>
<%@ Page language="c#" Codebehind="promise_to_pay.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Payment.PromiseToPayPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Promise To Pay</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body runat="server" id="_body">
		<form id="promiseToPayForm" method="post" runat="server">
			<tb:tabs id="tabs" runat="server"></tb:tabs>
			<div class="wide_form">
				<div class="row">
					<asp:label id="lblMessage" runat="server" CssClass="statement">Message text goes here</asp:label>
				</div>
				<div class="row" style="FLOAT: right">
					<asp:imagebutton id="btnSubmit" runat="server" ImageUrl="../../images/btn_submit.gif"></asp:imagebutton>
				</div>
			</div>
		</form>
	</body>
</HTML>
