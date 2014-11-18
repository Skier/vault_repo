<%@ Page language="c#" Codebehind="Receipt.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Receipt" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=9.1.5000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/ReceiptHeader.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Import Namespace="DPI.ClientComp" %>
<%@ Import Namespace="DPI.Ordering" %>
<%@ Import Namespace="DPI.Interfaces" %>
<%@ Import Namespace="DPI.Services" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NOGetZip</title>
		<script language="JavaScript">
		<!---
			var clickedButton = false;
			function check() {
				if (clickedButton)
					{
						clickedButton = false;
						return true;
					}
				else
					{
						return false;
					}
			}
		//--->
		</script>
		<script language="javascript">
		<!---
			function confirmButton()
			{ 
				if (document.all.hdnIsRAC.value == "no")
					return;
					
				var agree=confirm('Please print 2 copies of the confirmation document \n - provide one to the customer and keep one for the store records.');
				if (agree)
					return true;
				else
					return false;
			}
		//--->
		</script>
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" onload="confirmButton();"
		ms_positioning="GridLayout">
		<TABLE height="803" cellSpacing="0" cellPadding="0" width="227" border="0" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD width="227" height="803">
					<form id="Form1" onsubmit="return check(); " action="post" runat="server">
						<TABLE height="225" cellSpacing="0" cellPadding="0" width="801" border="0" ms_2d_layout="TRUE">
							<TR vAlign="top">
								<TD width="801" height="225">
									<table height="224" cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
										<tr>
											<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
										</tr>
										<tr>
											<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
												height="176"><dpiuser:sidecontrol id="Sidecontrol1" runat="server"></dpiuser:sidecontrol></td>
											<td vAlign="top" width="660" height="176">
												<table height="133" cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
													<tr>
														<td width="100%" colSpan="5"></td>
													</tr>
													<tr>
														<td vAlign="top" align="left" colSpan="2"><font class="05_con_bold_big">&nbsp;&nbsp;&nbsp;<asp:imagebutton id="imgPrint" runat="server" ImageUrl="images/btn_print2.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</font>
															<asp:imagebutton id="btnSvcPwrSchedule" runat="server" ImageUrl="images/btnSatelliteScheduler.jpg"
																Visible="False"></asp:imagebutton></td>
													</tr>
													<tr>
														<td align="left" colSpan="4"><CR:CRYSTALREPORTVIEWER id="ReportViewer" runat="server" DisplayGroupTree="False" Width="350px" Height="50px"></CR:CRYSTALREPORTVIEWER></td>
													</tr>
													<tr>
														<td vAlign="top" align="left" colSpan="2"><font class="05_con_bold_big">&nbsp;&nbsp;&nbsp;&nbsp;<asp:imagebutton id="Imagebutton1" runat="server" ImageUrl="images/btn_print2.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:imagebutton id="btnGotoMain" runat="server" ImageUrl="images/btn_gotomain.jpg"></asp:imagebutton>
														</td>
													</tr>
													<tr>
														<td align="right" height="100%"><INPUT id="hdnIsRAC" type="hidden" size="2" name="hdnIsRAC" runat="server"></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td colSpan="2"><dpiuser:sitefooter id="Sitefooter1" runat="server"></dpiuser:sitefooter></td>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
					</form>
				</TD>
			</TR>
		</TABLE>
	</body>
</HTML>
