<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteHeader" Src="control/ReceiptHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=9.1.5000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Page language="c#" Codebehind="PendOrderPymtInfoRpt.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Main.PendOrderPymtInfoRpt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Store_OrderStatus</title>
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0">
		<form id="ReportformZ" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="800" border="0" ms_2d_layout="TRUE">
				<TR vAlign="top">
					<TD width="1" height="507"></TD>
					<TD width="800">
						<table cellSpacing="0" cellPadding="0" width="660" border="0">
							<tr>
								<td colSpan="2"><uc1:siteheader id="SiteHeader1" runat="server"></uc1:siteheader></td>
							</tr>
							<tr>
								<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
									height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
								<td vAlign="top" width="100%">
									<table height="100%" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
										<tr>
											<td width="100%" colSpan="5"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
										</tr>
										<tr>
											<td align="center" colSpan="2" height="28"><asp:label id="lblRptTitle" runat="server" Width="640px" Height="31px" Font-Size="Large" Font-Names="Arial">Certification Report</asp:label><br>
											</td>
										</tr>
										<tr>
											<td colSpan="2" height="2"><asp:button id="cmdGO" runat="server" Width="48px" Text="Go"></asp:button><asp:button id="cmdPrint" runat="server" Text="Print" Visible="False"></asp:button><asp:button id="cmdDone" runat="server" Text="Done" Visible="False"></asp:button></td>
										</tr>
										<TR>
											<td align="left" colSpan="2" height="219"><cr:crystalreportviewer id="ReportViewer" runat="server" Width="350px" Height="50px" Visible="False" DisplayGroupTree="False"></cr:crystalreportviewer><BR>
												<TABLE id="tblParameters" width="660" runat="server">
													<TR>
														<TD vAlign="top" align="center" width="46" height="166"></TD>
														<TD vAlign="top" align="center" width="56" height="166"></TD>
														<TD vAlign="top" align="center" width="407" height="166">
															<asp:RadioButtonList id="rbtnStore" runat="server" Height="104px" Width="273px" Visible="False">
																<asp:ListItem Value="Store">Store Training Report</asp:ListItem>
																<asp:ListItem Value="Corporation">Corporation Training Report</asp:ListItem>
															</asp:RadioButtonList></TD>
														<TD vAlign="top" align="center" width="34" height="166"></TD>
														<td height="166"></td>
													</TR>
													<TR>
														<TD vAlign="top" align="center" width="46" height="145"></TD>
														<TD vAlign="top" align="center" width="56" height="166"></TD>
														<TD vAlign="top" align="center" width="407" height="145"></TD>
														<TD vAlign="top" align="center" width="34" height="145"></TD>
														<td></td>
													</TR>
												</TABLE>
											</td>
										</TR>
										<TR>
											<TD align="center" colSpan="2" height="2"></TD>
										</TR>
										<TR>
											<TD align="center" colSpan="2" height="2"><asp:label id="lblErrorMsg" runat="server" Font-Names="Arial" ForeColor="Red" Font-Bold="True"></asp:label></TD>
										</TR>
									</table>
								</td>
							</tr>
							<TR>
								<TD colSpan="2"><uc1:sitefooter id="SiteFooter1" runat="server"></uc1:sitefooter></TD>
							</TR>
						</table>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
