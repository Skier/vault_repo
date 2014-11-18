<%@ Page language="c#" Codebehind="summary.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.SummaryPage" %>
<%@ Register TagPrefix="ctl" Namespace="Dpi.Central.Web.Controls" Assembly="Dpi.Central.Web" %>
<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>dPi Teleconnect LL</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../DPI.css" type="text/css" rel="stylesheet">
	</head>
	<body>
		<form id="accountSummaryForm" method="post" runat="server">
			<table cellspacing="0" cellpadding="0" border="0">
				<tr>
					<td valign="top" align="left" colspan="3"><dns:header id="ctrlHeader" runat="server"></dns:header></td>
				</tr>
				<tr>
					<td rowspan="2">
						<img alt="" src="../images/about_side.jpg">
					</td>
					<td valign="top" align="left">
						<img src="../images/ppc_top.jpg" border="0">
						<table class="layout_table">
							<tr>
								<td class="05_con_sublabel_zip" valign="middle" align="right" background="../images/subtable_header_custaddress.jpg"
									bgcolor="white" colspan="3" height="61">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td width="11" class="left_padding"></td>
								<td style="HEIGHT: 11px" colspan="2"><asp:Label ID="lblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:Label></td>
							</tr>
							<tr>
								<td></td>
								<td colspan="2">
									<table border="0" cellspacing="0" cellpadding="0" class="linkTable">
										<tr>
											<td nowrap><asp:HyperLink ID="lnkAccSettings" runat="server" NavigateUrl="~/account/account_settings.aspx" onclick="this.blur()"><img src="../images/contact.gif" alt="">Account Settings</asp:HyperLink></td>
											<td nowrap><asp:HyperLink ID="lnkOrders" runat="server" NavigateUrl="~/account/order_status.aspx" onclick="this.blur()"><img src="../images/orders.gif" alt="">Order Status</asp:HyperLink></td>
											<td nowrap><asp:HyperLink ID="lnkPayment" runat="server" NavigateUrl="~/account/payment/payment_selection.aspx" onclick="this.blur()"><img src="../images/icon_cash.gif" alt="">Make a Payment</asp:HyperLink></td>
											<td nowrap><asp:HyperLink ID="lnkPromiseToPay" runat="server" NavigateUrl="~/account/payment/promise_to_pay.aspx" onclick="this.blur()"><img src="../images/promisetopay.gif" alt="">Promise To Pay</asp:HyperLink></td>
											<td nowrap><asp:HyperLink ID="lnkRecurringSetup" runat="server" NavigateUrl="~/account/payment/rec_payment_manager.aspx" Visible="False">Recurring Payments</asp:HyperLink></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr class="separator_row"><td colspan="3"></td></tr>
							<tr>
								<td class="left_padding"></td>
								<td width="430">Account Number:</td>
								<td width="212"><ctl:ControlLabel id="lblAccNumber" runat="server" Width="100%" SIMPLETEXT="True"></ctl:ControlLabel></td>
							</tr>
							<tr>
								<td class="left_padding"></td>
								<td>Phone Number:</td>
								<td><ctl:ControlLabel id="lblPhoneNumber" runat="server" Width="100%" SIMPLETEXT="True"></ctl:ControlLabel></td>
							</tr>
							<tr>
								<td class="left_padding"></td>
								<td>Customer Name:</td>
								<td><ctl:ControlLabel id="lblCustomerName" runat="server" Width="100%" SIMPLETEXT="True"></ctl:ControlLabel></td>
							</tr>
							<tr>
								<td class="left_padding"></td>
								<td>Service Address:</td>
								<td><ctl:ControlLabel id="lblAddress" runat="server" Width="100%" SIMPLETEXT="True"></ctl:ControlLabel></td>
							</tr>
							<tr>
								<td class="left_padding"></td>
								<td></td>
								<td><ctl:ControlLabel id="lblCityStateZip" runat="server" Width="100%" Height="8px" SIMPLETEXT="True"></ctl:ControlLabel></td>
							</tr>
							<tr>
								<td class="left_padding"></td>
								<td>Status:</td>
								<td><ctl:ControlLabel id="lblStatus" runat="server" Width="100%" Height="8px" SIMPLETEXT="True"></ctl:ControlLabel></td>
							</tr>
							<tr>
								<td class="left_padding"></td>
								<td class="05_con_bold">Due Date:</td>
								<td class="05_con_bold"><ctl:ControlLabel id="lblDueDate" runat="server" Width="100%" SIMPLETEXT="True"></ctl:ControlLabel></td>
							</tr>
							<tr>
								<td class="left_padding"></td>
								<td nowrap class="05_con_bold"><font color="red">Last day to 
										receive&nbsp;a&nbsp;payment before service interruption:</font></td>
								<td class="05_con_bold"><font color="red"><ctl:ControlLabel id="lblLastDay" runat="server" Width="100%" SIMPLETEXT="True"></ctl:ControlLabel></font></td>
							</tr>
							<tr>
								<td class="left_padding"></td>
								<td class="05_con_bold" style="WIDTH: 175px"><ctl:ControlLabel id="lblActivDateCap" runat="server" Width="112px">Activation Date</ctl:ControlLabel></td>
								<td class="05_con_bold"><ctl:ControlLabel id="lblActivDate" runat="server" Width="100%" SIMPLETEXT="True"></ctl:ControlLabel></td>
							</tr>
							<tr class="separator_row">
								<td colspan="3"></td>
							</tr>
							<tr>
								<td class="left_padding"></td>
								<td class="header" style="WIDTH: 620px" bgcolor="chocolate" colspan="2">
									Reminder Notice</td>
							</tr>
							<tr>
								<td class="left_padding"></td>
								<td style="WIDTH: 621px" align="center" colspan="2">
									<asp:ImageButton ID="imgPastReminderNotice" runat="server" ImageUrl="../images/btn_viewbill.jpg"></asp:ImageButton>
								</td>
							</tr>
							<tr>
								<td class="left_padding"></td>
								<td class="header" bgcolor="chocolate" colspan="2">Account Summary</td>
							</tr>
							<tr>
								<td class="left_padding"></td>
								<td style="WIDTH: 288px; HEIGHT: 13px" align="right">Balance Forward (from a prior 
									bill period)</td>
								<td style="HEIGHT: 13px" valign="top" align="right">
									<ctl:ControlLabel id="lblBalForward" runat="server" Width="106px" SIMPLETEXT="True"></ctl:ControlLabel>
								</td>
							</tr>
							<tr>
								<td class="left_padding"></td>
								<td style="WIDTH: 288px; HEIGHT: 19px" align="right" bgcolor="whitesmoke">Current 
									Charges</td>
								<td style="HEIGHT: 19px" align="right" bgcolor="whitesmoke">
									<ctl:ControlLabel id="lblCurrCharges" runat="server" Width="106px" SIMPLETEXT="True"></ctl:ControlLabel></td>
							</tr>
							<tr>
								<td class="left_padding"></td>
								<td style="WIDTH: 288px; HEIGHT: 19px" align="right"><strong>Total&nbsp;Amount Due</strong></td>
								<td style="HEIGHT: 19px" align="right">
									<ctl:ControlLabel id="lblAmountDue" runat="server" Width="106px" SIMPLETEXT="True"></ctl:ControlLabel>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td valign="bottom" align="center"><dns:footer id="ctrlFooter" runat="server"></dns:footer></td>
				</tr>
			</table>
		</form>
	</body>
</html>
