<%@ Page language="c#" Codebehind="WLActivationInfo.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.WLActivationInfo" %>
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
								<td class="05_con_label" width="334" height="112"><IMG src="images/InfinityActProcess.jpg" width="100%" border="0">
								</td>
								<td class="05_con_label" width="326" height="112"><IMG src="images/subtable_header_blank2.jpg" width="100%" border="0">
								</td>
							</tr>
							<TR>
								<TD class="05_con_label" style="HEIGHT: 10px" align="center" width="334" height="10"></TD>
								<TD class="05_con_label" style="HEIGHT: 10px" width="326" height="10">&nbsp;&nbsp;
								</TD>
							</TR>
							<tr>
							<tr>
								<td align="center" colSpan="2">
									<table cellSpacing="0" cellPadding="0" width="618" border="0" style="WIDTH: 618px; HEIGHT: 213px">
										<tr>
											<td style="WIDTH: 533px; HEIGHT: 163px" bgColor="#ffffff" colSpan="2" height="163">
												<div align="center">
													<DIV align="center"><asp:label id="Label1" runat="server" Font-Size="Medium" Width="607px">The Activation process may take a few seconds</asp:label></DIV>
													<DIV align="left">&nbsp;</DIV>
                  <DIV align=center><asp:label id="Label2" runat="server" Width="603px" Font-Size="Medium">...the system is locating a Phone 
																Number</b> for this handset</asp:label>&nbsp;</DIV>
                  <DIV align=center>
														<asp:label id="Label3" runat="server" Width="604px" Font-Size="Medium">...and, setting up the connection to the 
																Nationwide PCS Network</b></asp:label></DIV>
													<DIV align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                  &nbsp;&nbsp;
														<asp:label id="Label4" runat="server" Font-Size="Medium" Width="582px">
															Press the Proceed button to continue</asp:label></DIV>
													<DIV align="left">&nbsp;</DIV>
													<DIV align="left">&nbsp;</DIV>
												</div>
											</td>
										</tr>
										<tr>
											<td style="WIDTH: 533px" align="right" colSpan="2">&nbsp;&nbsp;</td>
											<td>&nbsp;</td>
										</tr>
									</table>
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td bgColor="#ffffff" colSpan="2" height="10">&nbsp;</td>
							</tr>
							<tr>
								<td class="05_con_small" align="center" colSpan="2"><asp:label id="lblErrMsg" runat="server" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
										Font-Names="Arial" Height="10px" Width="643px"></asp:label><BR>
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
