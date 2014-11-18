<%@ Page language="c#" Codebehind="WLCheckActivation.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.WLCheckActivation" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="DPI.Services" %>
<%@ Import Namespace="DPI.Interfaces" %>
<%@ Import Namespace="DPI.Ordering" %>
<%@ Import Namespace="DPI.ClientComp" %>
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
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
				<tr>
					<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
						height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
					<td vAlign="top" width="660">
						<table height="100%" cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
							<tr>
								<td width="100%" colSpan="5"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
							</tr>
							<TR>
								<TD width="100%" colSpan="5"></TD>
							</TR>
							<tr>
								<td class="05_con_label" width="334" height="112"><IMG src="images/Infinity Activation.jpg" width="100%" border="0">
								</td>
								<td class="05_con_label" width="326" height="112"><IMG src="images/subtable_header_blank2.jpg" width="100%" border="0">
								</td>
							</tr>
							<TR>
								<TD class="05_con_label" style="HEIGHT: 8px" width="334" height="8" align="center"></TD>
								<TD class="05_con_label" style="HEIGHT: 8px" width="326" height="8">&nbsp;&nbsp;</TD>
							</TR>
							<tr>
							<tr>
								<td colspan="2" align="center">
									<table border="0" cellpadding="0" cellspacing="0" width="635" style="WIDTH: 635px; HEIGHT: 190px">
										<tr>
											<td style="HEIGHT: 158px" bgColor="#ffffff" colSpan="2" height="158">
												<div align="center">
													<asp:Label id="Label1" runat="server" Font-Size="Medium" Width="637px">...the System is still searching for a Phone Number</asp:Label></div>
												<DIV align="left">&nbsp;</DIV>
												<DIV align="center">
													<asp:Label id="Label2" runat="server" Font-Size="Medium" Width="633px">...in some cases, internet traffic may slow this process down</asp:Label></DIV>
												<DIV align="center">
													<asp:Label id="Label3" runat="server" Font-Size="Medium" Width="634px">requiring up to 60 seconds to complete</asp:Label></DIV>
												<DIV align="left">&nbsp;</DIV>
												<DIV align="center">
													<asp:Label id="Label4" runat="server" Font-Size="Medium" Width="636px">
														...to update the Activation Status - Press the Activation Update button</asp:Label></DIV>
												<DIV align="left"><STRONG></STRONG>&nbsp;</DIV>
												<DIV align="center">
													<DIV align="center">
														<asp:Label id="lblSearch" runat="server" Font-Size="Medium" Width="635px">...still searching</asp:Label>
													</DIV>
												</DIV>
												<DIV align="left">&nbsp;</DIV>
											</td>
										</tr>
										<tr>
											<td colSpan="2" align="right">&nbsp;&nbsp;</td>
											<td>&nbsp;</td>
										</tr>
									</table>
									<asp:Button id="btnCheckAct" runat="server" Width="123px" Text="Activation Update" Height="24px"></asp:Button>
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:Button id="btnManActivation" runat="server" Width="123px" Text="Activate Manually" Height="24px"></asp:Button>
								</td>
							</tr>
							<tr>
								<td bgColor="#ffffff" colSpan="2" height="10">&nbsp;</td>
							</tr>
							<tr>
								<td class="05_con_small" align="center" colSpan="2"><asp:label id="lblErrMsg" runat="server" Width="643px" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
										Font-Names="Arial" Height="10px"></asp:label><BR>
								</td>
							</tr>
							<tr>
								<td vAlign="top" colSpan="2" height="100%">
									<TABLE id="Table1" style="WIDTH: 656px; HEIGHT: 34px" cellSpacing="1" cellPadding="1" width="656"
										border="0">
										<TR>
											<TD align="left"><asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg" Visible="False"></asp:imagebutton></TD>
											<TD align="right"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg" Visible="False"></asp:imagebutton></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="2"><dpiuser:sitefooter id="SiteFooter" runat="server"></dpiuser:sitefooter></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
