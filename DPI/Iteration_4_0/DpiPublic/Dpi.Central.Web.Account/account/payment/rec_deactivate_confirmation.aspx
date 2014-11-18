<%@ Page language="c#" Codebehind="rec_deactivate_confirmation.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Payment.DeactivateConfirmationPage" %>
<%@ Register TagPrefix="tb" TagName="Tabs" Src="~/account/tabs.ascx" %>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
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
				<tr class="separator_row">
					<td colSpan="2"></td>
				</tr>
				<TR>
					<TD colSpan="2">
						<asp:Label id="lblPaymentQuestion" runat="server">An Active recurring payment option already exist with {0} Number ending in {1}.<br>Do you want to deactivate the existing recurring payment option and make this one active?</asp:Label></TD>
				</TR>
				<TR>
					<TD align="right" width="88%">
						<asp:ImageButton id="btnSubmit" runat="server" ImageUrl="../../images/btn_yes.jpg"></asp:ImageButton></TD>
					<TD align="right">
						<asp:ImageButton id="btnCancel" runat="server" ImageUrl="../../images/btn_no.jpg"></asp:ImageButton></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
