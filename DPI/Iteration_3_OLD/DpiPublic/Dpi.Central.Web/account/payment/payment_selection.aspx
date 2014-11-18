<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<%@ Page language="c#" Codebehind="payment_selection.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Payment.PaymentSelectionPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC Payment Selection</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="../../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="PaymentSelectionForm" method="post" runat="server">
			<table cellspacing="0" cellpadding="0" border="0">
				<tr>
					<td colspan="2">
						<dns:header id="ctrlHeader" runat="server"></dns:header></td>
					<td valign="top" align="left"></td>
				</tr>
				<tr>
					<td rowspan="2" valign="top"><img alt="" src="../../images/about_side.jpg">
					</td>
					<td valign="top" align="center"><img src="../../images/ppc_top.jpg" border="0">
						<table align="center" cellpadding="2" class="layout_table" style="WIDTH: 500px">
							<tr class="separator_row">
								<td colspan="2"></td>
							</tr>
							<tr>
								<td colspan="2">
									<asp:CustomValidator ID="vldCustErrorMsg" runat="server" ErrorMessage="" Display="None" EnableClientScript="False"
										Width="100%"></asp:CustomValidator>
									<asp:ValidationSummary ID="vldSummary" runat="server" CssClass="Error"></asp:ValidationSummary>
									<asp:RequiredFieldValidator id="vldRfAmt" runat="server" Display="None" ErrorMessage="Payment Amount is required"
										ControlToValidate="txtAmt"></asp:RequiredFieldValidator>
									<asp:CustomValidator id="vldCstAmt" runat="server" EnableClientScript="False" Display="None" ErrorMessage="CustomValidator"
										ControlToValidate="txtAmt"></asp:CustomValidator></td>
							</tr>
							<tr class="separator_row">
								<td colspan="2"></td>
							</tr>
							<tr>
								<td>Account Number</td>
								<td align="right">
									<asp:Label id="lblAcctNumber" runat="server">123456789</asp:Label></td>
							</tr>
							<tr>
								<td>Phone Number</td>
								<td align="right">
									<asp:Label id="lblPhoneNumber" runat="server">123-456-7890</asp:Label></td>
							</tr>
							<tr>
								<td>Name</td>
								<td align="right">
									<asp:Label id="lblAcctName" runat="server">Samuel Sopilka</asp:Label></td>
							</tr>
							<tr class="separator_row">
								<td colspan="2"></td>
							</tr>
							<tr runat="server" id="pastDueRow">
								<td><p>Balance Forward<br>
										<span style="FONT-WEIGHT: normal">Must be paid to avoid disconnect of service</span></p>
								</td>
								<td align="right">
									<asp:Label id="lblBalForward" runat="server">$64.20</asp:Label></td>
							</tr>
							<tr>
								<td>Current Charges Due on
									<asp:Label id="lblDueDate" runat="server">Nov, 10 2006</asp:Label></td>
								<td align="right">
									<asp:Label id="lblCurrentChargesAmt" runat="server">$80.02</asp:Label></td>
							</tr>
							<tr class="separator_row">
								<td colspan="2"></td>
							</tr>
							<tr>
								<td style="HEIGHT: 34px">
									<asp:Label id="lblAmt" runat="server">Payment Amount, $</asp:Label></td>
								<td align="right" style="HEIGHT: 34px">
									<asp:textbox id="txtAmt" runat="server" Width="100%">64.20</asp:textbox></td>
							</tr>
							<tr class="separator_row">
								<td colspan="2"></td>
							</tr>
							<tr>
								<td align="left"><asp:ImageButton ID="btnCreditCardPay" runat="server" ImageUrl="../../images/paybyccbtn.gif" AlternateText="Pay By Credit Card"></asp:ImageButton></td>
								<td align="right"><asp:ImageButton ID="btnCheckPay" runat="server" ImageUrl="../../images/paybycheckbtn.gif" AlternateText="Pay By Check"></asp:ImageButton>
								</td>
							</tr>
							<tr>
								<td colspan="2" nowrap>
									<asp:HyperLink id="lnkReturn" runat="server" NavigateUrl="~/account/summary.aspx">Return to Account Summary</asp:HyperLink></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="bottom" align="center">
						<dns:footer id="ctrlFooter" runat="server"></dns:footer></td>
					<td></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
