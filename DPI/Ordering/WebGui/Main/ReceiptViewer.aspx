<%@ Page language="c#" Codebehind="ReceiptViewer.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.ReceiptViewer" %>
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
		<script language='javascript'>
		<!---
			function confirmButton()
			{ 
				var agree=confirm('Please print 2 copies of the confirmation document \n - provide one to the customer and keep one for the store records.');
				if (agree)
					return true;
				else
					return false;
			}
		//--->
		</script>
	</HEAD>
	<% if (StoreSvc.GetCorporation(DPI.ClientComp.User.GetUser(this).LoginStoreCode).RAC_WF){ %>
	<body onload="confirmButton();" text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0"
		ms_positioning="GridLayout">
		<% } else { %>
		<!--	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout"> -->
		<% } %>
		<TABLE height="803" cellSpacing="0" cellPadding="0" width="227" border="0" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD width="227" height="803">
					<form id="Form1" action="post" runat="server">
						<TABLE height="225" cellSpacing="0" cellPadding="0" width="801" border="0" ms_2d_layout="TRUE">
							<TR vAlign="top">
								<TD width="801" height="225">
									<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0" height="224">
										<tr>
											<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
										</tr>
										<tr>
											<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
												height="175"><dpiuser:sidecontrol id="Sidecontrol1" runat="server"></dpiuser:sidecontrol></td>
											<td vAlign="top" width="660" height="175">
												<table height="133" cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
													<tr>
														<td width="100%" colSpan="5"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
													</tr>
													<tr>
														<td vAlign="top" align="left" colSpan="2"><font class="05_con_bold_big">&nbsp;&nbsp;&nbsp;<asp:imagebutton id="imgPrint" runat="server" ImageUrl="images/btn_print2.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;</font>
														</td>
													</tr>
													<tr>
														<td align="left" colSpan="4"><CR:CRYSTALREPORTVIEWER id="ReportViewer" runat="server" Height="50px" Width="350px" DisplayGroupTree="False"></CR:CRYSTALREPORTVIEWER></td>
													</tr>
													<tr>
														<td vAlign="top" align="left" colSpan="2"><font class="05_con_bold_big">&nbsp;&nbsp;&nbsp;&nbsp;<asp:imagebutton id="Imagebutton1" runat="server" ImageUrl="images/btn_print2.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</font>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:ImageButton id="btnMain" runat="server" ImageUrl="images/btn_gotomain.jpg"></asp:ImageButton>
														</td>
													</tr>
													<tr>
														<td align="center" height="23">
															<asp:label id="lblErrMsg" runat="server" Width="612px" ForeColor="Red" Font-Names="Arial" Font-Bold="True"></asp:label></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td colSpan="2" height="25"><dpiuser:sitefooter id="Sitefooter1" runat="server"></dpiuser:sitefooter></td>
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
