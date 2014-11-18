<%@ Page language="c#" Codebehind="summary.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.SummaryPage" %>
<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
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
								<TD class="left_padding"></TD>
								<TD style="HEIGHT: 11px" colSpan="2"><asp:label id="lblErrMsg" runat="server" ForeColor="Red" Width="100%"></asp:label></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD style="HEIGHT: 11px" align="right" colSpan="2">
									<asp:linkbutton id="lbPromiseToPay" runat="server">(Promise To Pay)</asp:linkbutton></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD style="HEIGHT: 11px" align="right" colSpan="2">
									<asp:linkbutton id="lbtnChangeAccountSettings" runat="server" CausesValidation="False">Change Account Settings</asp:linkbutton><asp:label id="lblRecurringPymts" runat="server" Width="276px" Visible="False">Recurring Payments:</asp:label>&nbsp;
									<asp:linkbutton id="lbRecurringSetup" runat="server" Visible="False">(View)</asp:linkbutton></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD style="HEIGHT: 11px" align="right" colspan="2"><ASP:LABEL id="Label1" runat="server" width="276px">Order Status for Add/Change:</ASP:LABEL>&nbsp;
									<ASP:LINKBUTTON id="lbOrderStatus" runat="server">(View)</ASP:LINKBUTTON></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD style="WIDTH: 175px; HEIGHT: 19px">Account Number:</TD>
								<TD style="HEIGHT: 19px"><asp:label id="lblAccNumber" runat="server" Width="280px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD style="WIDTH: 175px; HEIGHT: 19px">Phone Number:</TD>
								<TD style="HEIGHT: 19px"><asp:label id="lblPhoneNumber" runat="server" Width="280px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD style="WIDTH: 175px; HEIGHT: 19px">Customer Name:</TD>
								<TD style="HEIGHT: 19px"><asp:label id="lblCustomerName" runat="server" Width="280px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD style="WIDTH: 175px; HEIGHT: 19px">Service Address:</TD>
								<TD style="HEIGHT: 19px"><asp:label id="lblAddress" runat="server" Width="280px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD style="WIDTH: 175px; HEIGHT: 19px"></TD>
								<TD style="HEIGHT: 19px"><asp:label id="lblCityStateZip" runat="server" Width="280px" Height="8px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD style="WIDTH: 175px; HEIGHT: 19px">Status:</TD>
								<TD style="HEIGHT: 19px"><asp:label id="lblStatus" runat="server" Width="280px" Height="8px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD class="05_con_bold" style="WIDTH: 175px">Due Date:</TD>
								<TD class="05_con_bold"><asp:label id="lblDueDate" runat="server" Width="100%"></asp:label></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD class="05_con_bold" style="WIDTH: 175px"><FONT color="red">Last day to make payment 
										before disconnect:</FONT></TD>
								<TD class="05_con_bold"><FONT color="red"><asp:label id="lblLastDay" runat="server" Width="100%"></asp:label></FONT></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD class="05_con_bold" style="WIDTH: 175px"><asp:label id="lblActivDateCap" runat="server" Width="112px">Activation Date</asp:label></TD>
								<TD class="05_con_bold"><asp:label id="lblActivDate" runat="server" Width="100%"></asp:label></TD>
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
									<asp:label id="lblBalForward" runat="server" Width="106px"></asp:label>
								</TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD style="WIDTH: 288px; HEIGHT: 19px" align="right" bgColor="whitesmoke">Current 
									Charges</TD>
								<TD style="HEIGHT: 19px" align="right" bgColor="whitesmoke">
									<asp:label id="lblCurrCharges" runat="server" Width="106px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD style="WIDTH: 288px; HEIGHT: 19px" align="right"><STRONG>Total&nbsp;Amount Due</STRONG></TD>
								<TD style="HEIGHT: 19px" align="right">
									<asp:label id="lblAmountDue" runat="server" Width="106px"></asp:label>
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
