<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Page language="c#" Codebehind="Forms.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Forms" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=9.1.5000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ReportMain</title> 
		<!---
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
		</script> -->
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0">
		<form id="ReportformZ" onsubmit="return check(); " runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
				<tr>
					<td colSpan="2"><uc1:siteheader id="SiteHeader1" runat="server"></uc1:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
						height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
					<td vAlign="top" width="660">
						<table height="100%" cellSpacing="0" cellPadding="0" align="left" border="0">
							<tr>
								<td width="100%" colSpan="5"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
							</tr>
							<tr>
							<tr>
								<td width="330" background="images/subtable_header_reports.jpg" height="24"><IMG src="images/subtable_header_forms.jpg" border="0"></td>
								<td width="326" background="images/subtable_header_reports2.jpg" height="24" border="0"><IMG src="images/subtable_Neworder2.jpg" border="0"></td>
							</tr>
				</tr>
				<tr>
					<td align="center" colSpan="2" height="22"><br>
					</td>
				</tr>
				<tr>
					<td align="center" colSpan="2" height="100%">
						<table id="ReportMenu" height="151" width="652" runat="server">
							<tr>
								<td align="center" width="612" colSpan="4"><asp:label id="lblReportMenu" Font-Bold="True" ForeColor="#C04000" Font-Name="tahoma" Runat="server"
										Font-Names="Arial">Forms Menu</asp:label></td>
							</tr>
							<tr>
								<td class="05_con_normal" align="center" height="30"><asp:imagebutton id="imgAppLocPhone" runat="server" Width="25px" Height="25px" ImageUrl="images/reporting.jpg"></asp:imagebutton></td>
								<td class="05_con_normal" width="567" height="30"><asp:linkbutton id="lnkAppLocPhone" runat="server" Font-Bold="True" ForeColor="Black" Font-Name="tahoma"
										Font-Names="tahoma" Font-Size="Smaller"> Application for Local Telephone Service</asp:linkbutton></td>
								<td class="05_con_normal" align="center" height="30" width="283"></td>
								<td class="05_con_normal" height="30"><br>
								</td>
							</tr>
							<tr>
								<td class="05_con_normal" align="center" height="30"><asp:imagebutton id="imgNCServiceAg" runat="server" Width="25px" Height="25px" ImageUrl="images/reporting.jpg"></asp:imagebutton></td>
								<td class="05_con_normal" width="567" height="30"><asp:linkbutton id="lnkNCServiceAg" runat="server" Font-Bold="True" ForeColor="Black" Font-Name="tahoma"
										Font-Names="tahoma" Font-Size="Smaller">North Carolina Service Agreement</asp:linkbutton></td>
								<td class="05_con_normal" align="center" height="30" width="283"></td>
								<td class="05_con_normal" height="30"><br>
								</td>
							</tr>
							<tr>
								<td class="05_con_normal" align="center" height="30"><asp:imagebutton id="imgWebLoa" runat="server" Width="25px" Height="25px" ImageUrl="images/reporting.jpg"></asp:imagebutton></td>
								<td class="05_con_normal" width="567" height="30"><asp:linkbutton id="lnkWebLoa" runat="server" Font-Bold="True" ForeColor="Black" Font-Name="tahoma"
										Font-Names="tahoma" Font-Size="Smaller"> Letter of Agency</asp:linkbutton></td>
								<td class="05_con_normal" align="center" height="30" width="283"></td>
								<td class="05_con_normal" height="30"><br>
								</td>
							</tr>
							<tr>
								<td class="05_con_normal" align="center" height="30">
									<asp:imagebutton id="imgDebCard" runat="server" ImageUrl="images/reporting.jpg" Height="25px" Width="25px"></asp:imagebutton></td>
								<td class="05_con_normal" width="567" height="30">
									<asp:linkbutton id="lnkDebCard" runat="server" Font-Names="tahoma" Font-Name="tahoma" ForeColor="Black"
										Font-Bold="True" Font-Size="Smaller">Purpose Prepaid MasterCard Enrollment Form</asp:linkbutton>
								</td>
								<td class="05_con_normal" align="center" width="283" height="30"></td>
								<td class="05_con_normal" height="30"><br>
								</td>
							</tr>
							<tr>
								<td class="05_con_normal" align="center" height="30">
									<asp:imagebutton id="Imagebutton1" runat="server" ImageUrl="images/reporting.jpg" Height="25px" Width="25px"></asp:imagebutton></td>
								<td class="05_con_normal" width="567" height="30">
									<asp:linkbutton id="lbFees" runat="server" Font-Names="tahoma" Font-Name="tahoma" ForeColor="Black"
										Font-Bold="True" Font-Size="Smaller">Schedule of Cardholder Fees</asp:linkbutton>
								</td>
								<td class="05_con_normal" align="center" width="283" height="30"></td>
								<td class="05_con_normal" width="281" height="30"><br>
								</td>
							</tr>
							<TR>
								<TD class="05_con_normal" align="center" height="30">
									<asp:imagebutton id="imgIntRates" runat="server" ImageUrl="images/reporting.jpg" Height="25px" Width="25px"></asp:imagebutton></TD>
								<TD class="05_con_normal" width="567" height="30">
									<asp:linkbutton id="lbIntRates" runat="server" Font-Names="tahoma" Font-Name="tahoma" ForeColor="Black"
										Font-Bold="True" Font-Size="Smaller">International Termination Rates</asp:linkbutton>
								</TD>
								<TD class="05_con_normal" align="center" width="283" height="30"></TD>
								<TD class="05_con_normal" width="281" height="30"></TD>
							</TR>
							<tr>
								<td class="05_con_normal" align="center" height="30">
									<asp:imagebutton id="imgLifeline" runat="server" ImageUrl="images/reporting.jpg" Height="25px" Width="25px"></asp:imagebutton></td>
								<td class="05_con_normal" width="567" height="30">
									<asp:linkbutton id="lbLifeline" runat="server" Font-Names="tahoma" Font-Name="tahoma" ForeColor="Black"
										Font-Bold="True" Font-Size="Smaller">Lifeline Application</asp:linkbutton>
								</td>
								<td class="05_con_normal" align="center" width="283" height="30"></td>
								<td class="05_con_normal" width="281" height="30"><br>
								</td>
							</tr>
							<tr>
								<td class="05_con_normal" align="center" height="30">
									<asp:imagebutton id="imgSatellitePerm" runat="server" ImageUrl="images/reporting.jpg" Height="25px"
										Width="25px"></asp:imagebutton></td>
								<td class="05_con_normal" width="567" height="30">
									<asp:linkbutton id="lbSatellitePerm" runat="server" Font-Names="tahoma" Font-Name="tahoma" ForeColor="Black"
										Font-Bold="True" Font-Size="Smaller">Landlord Permission Form</asp:linkbutton>
								</td>
								<td class="05_con_normal" align="center" width="283" height="30"></td>
								<td class="05_con_normal" width="281" height="30"><br>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td align="center" colSpan="2">
						<asp:imagebutton id="btn_GotoMain" runat="server" ImageUrl="images/btn_gotomain.jpg"></asp:imagebutton></td>
				</tr>
			</table>
			</td></tr>
			<tr>
				<td colSpan="2"><uc1:sitefooter id="SiteFooter1" runat="server"></uc1:sitefooter></td>
			</tr>
			</table></form>
	</body>
</HTML>
