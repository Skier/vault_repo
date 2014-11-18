<%@ Page language="c#" Codebehind="CI_AccountSummary.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.CI_AccountSumm" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NOGetZip</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<form id="Form1" action="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
				<tr>
					<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
						height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
					<td vAlign="top" width="660">
						<table cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
							<tr>
								<td colSpan="5"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
							</tr>
							<tr>
								<td class="05_con_sublabel_zip" vAlign="middle" align="right" background="images/subtable_header_custaddress.jpg"
									bgColor="white" colSpan="5" height="61">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td style="HEIGHT: 320px" vAlign="top" colSpan="5">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TBODY>
											<TR>
												<TD style="WIDTH: 28px; HEIGHT: 11px"></TD>
												<TD style="WIDTH: 175px; HEIGHT: 11px"></TD>
												<TD style="HEIGHT: 11px" align="right">
													<asp:label id="lblRecurringPymts" runat="server" Width="276px" Visible="False"></asp:label>&nbsp;
													<asp:LinkButton id="lbRecurringSetup" runat="server" Visible="False">setup</asp:LinkButton></TD>
												<TD style="HEIGHT: 11px"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 28px" rowSpan="6">&nbsp;</TD>
												<TD style="WIDTH: 175px; HEIGHT: 19px">Account Number:</TD>
												<TD style="HEIGHT: 19px">
													<asp:label id="lblAccNumber" runat="server" Width="280px"></asp:label></TD>
												<TD rowSpan="5">&nbsp;</TD>
											</TR>
											<TR>
												<TD style="WIDTH: 175px; HEIGHT: 19px">Phone Number:</TD>
												<TD style="HEIGHT: 19px">
													<asp:label id="lblPhoneNumber" runat="server" Width="280px"></asp:label></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 175px; HEIGHT: 19px">Customer Name:</TD>
												<TD style="HEIGHT: 19px">
													<asp:label id="lblCustomerName" runat="server" Width="280px"></asp:label></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 175px; HEIGHT: 19px">Service Address:</TD>
												<TD style="HEIGHT: 19px">
													<asp:label id="lblAddress" runat="server" Width="280px"></asp:label></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 175px; HEIGHT: 19px"></TD>
												<TD style="HEIGHT: 19px">
													<asp:label id="lblCityStateZip" runat="server" Width="280px" Height="8px"></asp:label></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 175px; HEIGHT: 19px">Status:</TD>
												<TD style="HEIGHT: 19px">
													<asp:label id="lblStatus" runat="server" Width="280px" Height="8px"></asp:label></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 28px" rowSpan="5"></TD>
												<TD class="05_con_bold" style="WIDTH: 175px">Due Date:</TD>
												<td class="05_con_bold"><asp:label id="lblDueDate" runat="server" Width="280px"></asp:label></td>
												<td rowSpan="5"></td>
											</TR>
											<tr>
												<td class="05_con_bold" style="WIDTH: 175px"><font color="red">Last day to make payment 
														before disconnect:</font></td>
												<td class="05_con_bold"><font color="red"><asp:label id="lblLastDay" runat="server" Width="144px"></asp:label></font></td>
											</tr>
										</TBODY>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="05_con_medium" colSpan="4">&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 23px">&nbsp;</td>
											<td class="header" style="WIDTH: 620px" bgColor="chocolate" colSpan="2">&nbsp;&nbsp;&nbsp; 
												Reminder Notice</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 23px">&nbsp;</td>
											<td style="WIDTH: 621px" align="center" colSpan="2"><asp:imagebutton id="imgPastReminderNotice" runat="server" Width="136px" Height="32px" ImageUrl="images/btn_viewbill.jpg"></asp:imagebutton></td>
											<td>&nbsp;</td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td style="WIDTH: 22px">&nbsp;</td>
											<td class="header" bgColor="chocolate" colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp;Account 
												Summary</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 22px; HEIGHT: 13px">&nbsp;</td>
											<td style="WIDTH: 288px; HEIGHT: 13px" align="right">Balance Forward (from a prior 
												bill period)</td>
											<td style="HEIGHT: 13px" align="right"><asp:label id="lblBalForward" runat="server" Width="106px"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
											<td style="HEIGHT: 13px">&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 22px; HEIGHT: 19px">&nbsp;</td>
											<td style="WIDTH: 288px; HEIGHT: 19px" align="right" bgColor="whitesmoke">Current 
												Charges</td>
											<td style="HEIGHT: 19px" align="right" bgColor="whitesmoke"><asp:label id="lblCurrCharges" runat="server" Width="106px"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
											<td style="HEIGHT: 19px">&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 22px">&nbsp;</td>
											<td style="WIDTH: 288px; HEIGHT: 19px" align="right"><STRONG>Total&nbsp;Amount Due</STRONG></td>
											<td style="HEIGHT: 19px" align="right"><asp:label id="lblAmountDue" runat="server" Width="106px"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
											<td>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="05_con_small" align="center" style="HEIGHT: 27px"><asp:label id="lblErrMsg" runat="server" Width="226px" ForeColor="Red"></asp:label><BR>
								</td>
							</tr>
							<tr>
								<td vAlign="top" height="100%">
									<!------------------------------------------------------------------------>
									<TABLE id="Table1" style="WIDTH: 655px; HEIGHT: 34px" cellSpacing="1" cellPadding="1" width="655"
										border="0">
										<TR>
											<TD align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg"></asp:imagebutton>
											</TD>
											<TD align="right">
												<asp:imagebutton id="btnGotoMain" runat="server" ImageUrl="images/btn_gotomain.jpg"></asp:imagebutton>
											</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="2"><dpiuser:sitefooter id="Sitefooter1" runat="server"></dpiuser:sitefooter></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
