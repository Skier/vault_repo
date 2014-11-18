<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<%@ Register TagPrefix="ctl" Namespace="Dpi.Central.Web.Controls" Assembly="Dpi.Central.Web" %>
<%@ Page language="c#" Codebehind="summary.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.SummaryPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LL</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="accountSummaryForm" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD vAlign="top" align="left" colSpan="3"><dns:header id="ctrlHeader" runat="server"></dns:header></TD>
				</TR>
				<TR>
					<TD rowSpan="2">
						<IMG alt="" src="../images/about_side.jpg">
					</TD>
					<TD vAlign="top" align="left">
						<IMG src="../images/ppc_top.jpg" border="0">
						<TABLE class="layout_table">
							<tr>
								<td class="05_con_sublabel_zip" vAlign="middle" align="right" background="../images/subtable_header_custaddress.jpg"
									bgColor="white" colSpan="3" height="61">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
							<TR>
								<TD width="11" class="left_padding"></TD>
								<TD style="HEIGHT: 11px" colSpan="2"><asp:label id="lblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD align="right" colSpan="2"><asp:HyperLink id="lnkPayment" runat="server" NavigateUrl="~/account/payment/payment_selection.aspx"
										VISIBLE="False"><IMG SRC="../images/icon_cash.gif" BORDER="0" HEIGHT="16" WIDTH="16" ALT="" align="absmiddle"
											hspace="3">Make a Payment</asp:HyperLink>&nbsp;&nbsp;
									<asp:HyperLink id="lnkPromiseToPay" runat="server" NavigateUrl="~/account/payment/promise_to_pay.aspx">Promise To Pay</asp:HyperLink></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD align="right" colSpan="2"><asp:HyperLink id="lnkAccSettings" runat="server" NavigateUrl="~/account/account_settings.aspx">Change Account Settings</asp:HyperLink>&nbsp;
									<asp:HyperLink id="lnkRecurringSetup" runat="server" NavigateUrl="~/account/payment/rec_payment_manager.aspx"
										VISIBLE="False">Recurring Payments</asp:HyperLink></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD align="right" colspan="2"><asp:HyperLink id="lnkOrders" runat="server" NavigateUrl="~/account/order_status.aspx">Order Status</asp:HyperLink>&nbsp;</TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD width="430">Account Number:</TD>
								<TD width="212"><ctl:ControlLabel id="lblAccNumber" runat="server" Width="100%" SIMPLETEXT="True"></ctl:ControlLabel></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD>Phone Number:</TD>
								<TD><ctl:ControlLabel id="lblPhoneNumber" runat="server" Width="100%" SIMPLETEXT="True"></ctl:ControlLabel></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD>Customer Name:</TD>
								<TD><ctl:ControlLabel id="lblCustomerName" runat="server" Width="100%" SIMPLETEXT="True"></ctl:ControlLabel></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD>Service Address:</TD>
								<TD><ctl:ControlLabel id="lblAddress" runat="server" Width="100%" SIMPLETEXT="True"></ctl:ControlLabel></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD></TD>
								<TD><ctl:ControlLabel id="lblCityStateZip" runat="server" Width="100%" Height="8px" SIMPLETEXT="True"></ctl:ControlLabel></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD>Status:</TD>
								<TD><ctl:ControlLabel id="lblStatus" runat="server" Width="100%" Height="8px" SIMPLETEXT="True"></ctl:ControlLabel></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD class="05_con_bold">Due Date:</TD>
								<TD class="05_con_bold"><ctl:ControlLabel id="lblDueDate" runat="server" Width="100%" SIMPLETEXT="True"></ctl:ControlLabel></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD nowrap class="05_con_bold"><FONT color="red">Last day to 
										receive&nbsp;a&nbsp;payment</FONT><br>
									<FONT color="red">before service interruption:</FONT>
								</TD>
								<TD class="05_con_bold"><FONT color="red"><ctl:ControlLabel id="lblLastDay" runat="server" Width="100%" SIMPLETEXT="True"></ctl:ControlLabel></FONT></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD class="05_con_bold" style="WIDTH: 175px"><ctl:ControlLabel id="lblActivDateCap" runat="server" Width="112px">Activation Date</ctl:ControlLabel></TD>
								<TD class="05_con_bold"><ctl:ControlLabel id="lblActivDate" runat="server" Width="100%" SIMPLETEXT="True"></ctl:ControlLabel></TD>
							</TR>
							<tr class="separator_row">
								<td colSpan="3"></td>
							</tr>
							<TR>
								<TD class="left_padding"></TD>
								<TD class="header" style="WIDTH: 620px" bgColor="chocolate" colSpan="2">
									Reminder Notice</TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD style="WIDTH: 621px" align="center" colSpan="2">
									<asp:imagebutton id="imgPastReminderNotice" runat="server" ImageUrl="../images/btn_viewbill.jpg"></asp:imagebutton>
								</TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD class="header" bgColor="chocolate" colSpan="2">Account Summary</TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD style="WIDTH: 288px; HEIGHT: 13px" align="right">Balance Forward (from a prior 
									bill period)</TD>
								<TD style="HEIGHT: 13px" vAlign="top" align="right">
									<ctl:ControlLabel id="lblBalForward" runat="server" Width="106px" SIMPLETEXT="True"></ctl:ControlLabel>
								</TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD style="WIDTH: 288px; HEIGHT: 19px" align="right" bgColor="whitesmoke">Current 
									Charges</TD>
								<TD style="HEIGHT: 19px" align="right" bgColor="whitesmoke">
									<ctl:ControlLabel id="lblCurrCharges" runat="server" Width="106px" SIMPLETEXT="True"></ctl:ControlLabel></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD style="WIDTH: 288px; HEIGHT: 19px" align="right"><STRONG>Total&nbsp;Amount Due</STRONG></TD>
								<TD style="HEIGHT: 19px" align="right">
									<ctl:ControlLabel id="lblAmountDue" runat="server" Width="106px" SIMPLETEXT="True"></ctl:ControlLabel>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="bottom" align="center"><dns:footer id="ctrlFooter" runat="server"></dns:footer></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
