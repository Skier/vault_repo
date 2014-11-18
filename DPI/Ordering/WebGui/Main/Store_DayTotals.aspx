<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=9.1.5000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Page language="c#" Codebehind="Store_DayTotals.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Store_DayTotals" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteHeader" Src="control/ReceiptHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Store_DayTotalsDetail</title>
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<TABLE height="645" cellSpacing="0" cellPadding="0" width="801" border="0" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD width="801" height="645">
					<TABLE height="644" cellSpacing="0" cellPadding="0" width="800" border="0">
						<TR vAlign="top">
							<TD width="800">
								<form id="ReportformZ" runat="server">
									<table cellSpacing="0" cellPadding="0" width="800" border="0">
										<tr>
											<td colSpan="2"><uc1:siteheader id="SiteHeader1" runat="server"></uc1:siteheader></td>
										</tr>
										<tr>
											<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
												height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol>
											</td>
											<td vAlign="top" width="100%">
												<table height="100%" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
													<tr>
														<td colSpan="5" width="100%"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
													</tr>
													<tr>
														<td align="center" colSpan="2" height="28"><asp:label id="lblRptTitle" runat="server" Width="224px" Height="31px" Font-Size="Large" Font-Names="Arial">Daily Totals</asp:label><br>
														</td>
													</tr>
													<tr>
														<td colSpan="2" height="2"><asp:button id="cmdGO" runat="server" Width="48px" Text="Go"></asp:button><asp:button id="cmdPrint" runat="server" Text="Print" Visible="False"></asp:button><asp:button id="cmdDone" runat="server" Text="Done" Visible="False"></asp:button></td>
													</tr>
													<TR>
														<td align="left" colSpan="2" height="219"><cr:crystalreportviewer id="ReportViewer" runat="server" Width="350px" Height="50px" Visible="False" DisplayGroupTree="False"></cr:crystalreportviewer><BR>
															<TABLE id="tblParameters" height="203" width="496" runat="server">
																<TR>
																	<TD vAlign="top" align="center" width="48" height="145"><asp:label id="lblPayDate" runat="server" Width="72px">Pay Date</asp:label></TD>
																	<TD vAlign="top" align="center" width="182" height="145">
																		<asp:DropDownList id="ddlPayDateMonth" runat="server" Width="60px" AutoPostBack="True"></asp:DropDownList>
																		<asp:DropDownList id="ddlPayDateDay" runat="server" Width="40px" AutoPostBack="True"></asp:DropDownList>
																		<asp:DropDownList id="ddlPayDateYear" runat="server" Width="55px" AutoPostBack="True"></asp:DropDownList></TD>
																	<TD vAlign="top" align="center" width="34" height="145"><asp:imagebutton id="imgCalendar" runat="server" ImageUrl="images/calendar2.gif"></asp:imagebutton></TD>
																	<TD vAlign="top" align="center" height="145"><asp:calendar id="calPayDate" runat="server" Width="192px" Height="139px" Font-Size="9pt" BorderColor="Black"
																			Font-Names="Verdana" CellSpacing="1" ForeColor="Black" BackColor="White" NextPrevFormat="ShortMonth" BorderStyle="Solid" Visible="False">
																			<TodayDayStyle ForeColor="White" BackColor="#999999"></TodayDayStyle>
																			<DayStyle BackColor="#E0E0E0"></DayStyle>
																			<NextPrevStyle Font-Size="8pt" Font-Bold="True" ForeColor="White"></NextPrevStyle>
																			<DayHeaderStyle Font-Size="8pt" Font-Bold="True" Height="8pt" ForeColor="#333333" BackColor="White"></DayHeaderStyle>
																			<SelectedDayStyle ForeColor="White" BackColor="#333399"></SelectedDayStyle>
																			<TitleStyle Font-Size="12pt" Font-Bold="True" Height="12pt" ForeColor="White" BackColor="Chocolate"></TitleStyle>
																			<OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>
																		</asp:calendar></TD>
																</TR>
															</TABLE>
														</td>
													</TR>
													<tr>
														<td colSpan="2" height="2" align="center"></td>
													</tr>
													<tr>
														<td colSpan="2" height="2" align="center">
															<asp:label id="lblErrorMsg" runat="server" Font-Names="Arial" ForeColor="Red" Font-Bold="True"></asp:label></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td colSpan="2"><uc1:sitefooter id="SiteFooter1" runat="server"></uc1:sitefooter></td>
										</tr>
									</table>
								</form>
							</TD>
						</TR>
					</TABLE>
				</TD>
			</TR>
		</TABLE>
	</body>
</HTML>
