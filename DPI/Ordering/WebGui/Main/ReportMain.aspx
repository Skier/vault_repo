<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=9.1.5000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<%@ Page language="c#" Codebehind="ReportMain.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.ReportMain" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="DPI.Services" %>
<%@ Import Namespace="DPI.Interfaces" %>
<%@ Import Namespace="DPI.Ordering" %>
<%@ Import Namespace="DPI.ClientComp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ReportMain</title>
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
</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0">
		<form id="ReportformZ" runat="server">
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
								<td width="330" background="images/subtable_header_reports.jpg" height="24"><IMG src="images/subtable_header_reports.jpg" border="0"></td>
								<td width="326" background="images/subtable_header_reports2.jpg" height="24" border="0"><IMG src="images/subtable_header_reports2.jpg" border="0"></td>
							</tr>
				</tr>
				<tr>
					<td align="center" colSpan="2" height="90">
					<% if (StoreSvc.ShowEndOfDayRpt((IUser)Session["User"])) { %>
						<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="449" border="2" height="88">
							<TR>
								<TD colSpan="2" align="center"><FONT face="Tahoma"><STRONG>
											<asp:Label id="Label6" runat="server" Width="153px" Font-Names="Tahoma">End of Day Report</asp:Label></STRONG></FONT></TD>
							</TR>
							<TR>
								<TD width="102">
									<asp:imagebutton id="btnEndOfDayRpt" runat="server" Width="112px" ImageUrl="images/btnRetrieve.jpg"
										ImageAlign="AbsMiddle"></asp:imagebutton></TD>
								<TD width="289">&nbsp;&nbsp;&nbsp; 
									<asp:dropdownlist id="ddlPayDateMonth" runat="server" Width="60px"></asp:dropdownlist><asp:dropdownlist id="ddlPayDateDay" runat="server" Width="40px"></asp:dropdownlist><asp:dropdownlist id="ddlPayDateYear" runat="server" Width="55px"></asp:dropdownlist>&nbsp;
									<asp:imagebutton id="imgCalendar" runat="server" ImageUrl="images/calendar2.gif" ImageAlign="AbsMiddle"></asp:imagebutton></TD>
								
							</TR>
						</TABLE>
						<% } %>
						<br>
						<asp:label id="lblErrMsg" runat="server" Width="480px" Font-Bold="True" Visible="False" ForeColor="Red"
							EnableViewState="False"></asp:label>
					</td>
				</tr>
				<tr>
					<td align="center" colSpan="2" height="100%">
						<table id="Table1" width="652">
							
							
							<tr>
								<td align="center" colSpan="3"><asp:label id="Label1" Font-Bold="True" ForeColor="#C04000" Font-Name="tahoma" Runat="server"
										Font-Names="Arial">Report Menu</asp:label></td>
							</tr>
							<tr>
								<td width="97">&nbsp;</td>
								<td width="123"><asp:label id="Label3" runat="server" Width="112px" Font-Bold="True" ForeColor="Black" Font-Names="Tahoma"
										Font-Size="X-Small">Daily Reports</asp:label></td>
								<td vAlign="middle" align="left" height="12">&nbsp;&nbsp;&nbsp;
									<asp:dropdownlist id="DropDownList1" runat="server" Width="240px">
										<asp:ListItem Value="--">--</asp:ListItem>
										<asp:ListItem Value="Daily Totals">Daily Totals</asp:ListItem>
										<asp:ListItem Value="Daily Detail">Daily Detail</asp:ListItem>
									</asp:dropdownlist><asp:imagebutton id="btnDailyReport" runat="server" ImageAlign="AbsMiddle" ImageUrl="images/btnRetrieve.jpg"></asp:imagebutton></td>
							</tr>
							<tr>
								<td width="97">&nbsp;</td>
								<td align="left" colSpan="2"><IMG height="1" src="../main/images/pixel_gray.jpg" width="85%" border="0"></td>
							</tr>
							<tr>
								<td width="97">&nbsp;</td>
								<td width="123"><asp:label id="Label2" runat="server" Width="112px" Font-Bold="True" ForeColor="Black" Font-Names="Tahoma"
										Font-Size="X-Small">Customer Lists</asp:label></td>
								<td vAlign="baseline" align="left">&nbsp;&nbsp;&nbsp;
									<asp:dropdownlist id="Dropdownlist2" runat="server" Width="240px">
										<asp:ListItem Value="--">--</asp:ListItem>
										<asp:ListItem Value="Active Customers by Due Date7">Active Customers by Due Date</asp:ListItem>
										<asp:ListItem Value="Active Customer List4">Active Customer List</asp:ListItem>
										<asp:ListItem Value="Active Customers by Order Date2">Active Customers by Order Date</asp:ListItem>
										<asp:ListItem Value="Customers by Account Status5">Customers by Account Status</asp:ListItem>
										<asp:ListItem Value="Customers by Order Date1">Customers by Order Date</asp:ListItem>
										<asp:ListItem Value="Disconnected Customers by Order Date6">Disconnected Customers by Order Date</asp:ListItem>
										<asp:ListItem Value="Disconnected Customer List8">Disconnected Customer List</asp:ListItem>
										<asp:ListItem Value="Pending Customers by Order Date3">Pending Customers by Order Date</asp:ListItem>
										<asp:ListItem Value="Pending Order Payment Info9">Pending Order Payment Info</asp:ListItem>
									</asp:dropdownlist><asp:imagebutton id="btnCustList" runat="server" ImageAlign="AbsMiddle" ImageUrl="images/btnRetrieve.jpg"></asp:imagebutton></td>
							</tr>
							<tr>
								<td width="97">&nbsp;</td>
								<td align="left" colSpan="2"><IMG height="1" src="../main/images/pixel_gray.jpg" width="85%" border="0"></td>
							</tr>
							<tr>
								<td width="97">&nbsp;</td>
								<td width="123"><asp:label id="Label4" runat="server" Width="112px" Font-Bold="True" ForeColor="Black" Font-Names="Tahoma"
										Font-Size="X-Small"> Commissions</asp:label></td>
								<td vAlign="baseline" align="left">&nbsp;&nbsp;&nbsp;
									<asp:dropdownlist id="Dropdownlist3" runat="server" Width="240px">
										<asp:ListItem Value="--">--</asp:ListItem>
										<asp:ListItem Value="Local Phone Commission Earned">Local Phone Commission Earned</asp:ListItem>
										<asp:ListItem Value="Cellular Phone Commission Earned">Cellular Phone Commission Earned</asp:ListItem>
									</asp:dropdownlist><asp:imagebutton id="btnCommissions" runat="server" ImageAlign="AbsMiddle" ImageUrl="images/btnRetrieve.jpg"></asp:imagebutton></td>
							</tr>
							<tr>
								<td width="97">&nbsp;</td>
								<td align="left" colSpan="2"><IMG height="1" src="../main/images/pixel_gray.jpg" width="85%" border="0"></td>
							</tr>
							<tr>
								<td width="97">&nbsp;</td>
								<td width="123"><asp:label id="Label5" runat="server" Width="112px" Font-Bold="True" ForeColor="Black" Font-Names="Tahoma"
										Font-Size="X-Small"> Certification</asp:label></td>
								<td vAlign="baseline" align="left">&nbsp; &nbsp;
									<asp:dropdownlist id="Dropdownlist4" runat="server" Width="240px">
										<asp:ListItem Value="--">--</asp:ListItem>
										<asp:ListItem Value="Certification Result">Certification Result</asp:ListItem>
									</asp:dropdownlist><asp:imagebutton id="btnCertification" runat="server" ImageAlign="AbsMiddle" ImageUrl="images/btnRetrieve.jpg"></asp:imagebutton></td>
							</tr>
						</table>
						<br>
					</td>
				</tr>
				<tr>
					<td align="center" colSpan="2"></td>
				</tr>
			</table>
			</td></tr>
			<tr>
				<td colSpan="2"><uc1:sitefooter id="SiteFooter1" runat="server"></uc1:sitefooter></td>
			</tr>
			</table></form>
	</body>
</HTML>
