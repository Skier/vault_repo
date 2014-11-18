<%@ Import Namespace="DPI.Services" %>
<%@ Import Namespace="DPI.Interfaces" %>
<%@ Import Namespace="DPI.Ordering" %>
<%@ Import Namespace="DPI.ClientComp" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Page language="c#" Codebehind="ReprintReceiptsDate.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Main.ReprintReceiptsDate" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>Reprint Receipts - dPi Teleconnect</title>
		<meta content="False" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			function confirmButton()
			{ 
				var agree=confirm('You will be voiding real transactions. \n Pressing the \"OK\" button \n will reverse the transaction and create a credit on your invoice.');
				if (agree)
					return true;
				else
					return false;
			}
		</script>
</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0"
		ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0" height="100%">
				<tr>
					<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
						height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
					<td vAlign="top" width="660">
						<table height="100%" cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
							<tr>
								<td colSpan="2"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
							</tr>
							<tr>
								<% 
									IUser user = (IUser)Session["User"];
									if(StoreSvc.GetCorporation(user.LoginStoreCode).RAC_WF){
								%>
								<td class="05_con_label" style="HEIGHT: 113px" align="right" width="660" background="images/subtable_header_ReprintConf.jpg"
									colSpan="2" height="113">
								<%	
									} else {
								%>
								<td class="05_con_label" style="HEIGHT: 113px" align="right" width="660" background="images/subtable_header_ReprintReceipt.jpg"
									colSpan="2" height="113">
								<% } %>	
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label id="Label2" runat="server">Enter Receipt Date</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
									&nbsp;
									<asp:dropdownlist id="ddlPayDateYear" runat="server" Width="55px" AutoPostBack="True"></asp:dropdownlist>&nbsp;<asp:dropdownlist id="ddlPayDateDay" runat="server" Width="40px" AutoPostBack="True"></asp:dropdownlist>&nbsp;<asp:dropdownlist id="ddlPayDateMonth" runat="server" Width="60px" AutoPostBack="True"></asp:dropdownlist>&nbsp;<asp:imagebutton id="imgCalendarStartDate" runat="server" ImageUrl="images/calendar2.gif"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<P></P>
								</td>
							</tr>
							<tr>
								<td style="HEIGHT: 67px" colSpan="2">&nbsp;
            <DIV align=center><asp:label id="lblErrMsg" runat="server" Width="588px" ForeColor="Red"></asp:label></DIV></td>
							</tr>
							<tr>
								<td align="left">&nbsp;&nbsp;&nbsp;
									<asp:imagebutton id="ImageButton1" runat="server" ImageUrl="images/btn_gotomain.jpg"></asp:imagebutton></td>
								<td align="right"><asp:imagebutton id="btnNext" tabIndex="1" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td class="05_con_bold" align="center" colSpan="2" height="100%"><br>
									<div align="center">&nbsp;</div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="2"><dpiuser:sitefooter id="SiteFooter" runat="server"></dpiuser:sitefooter></td>
				</tr>
			</table>
			<asp:calendar id="calPayDate" style="Z-INDEX: 102; LEFT: 550px; POSITION: absolute; TOP: 308px"
				runat="server" Width="192px" ForeColor="Black" Font-Size="9pt" Font-Names="Verdana" Height="139px"
				BorderColor="Black" CellSpacing="1" BackColor="White" NextPrevFormat="ShortMonth" BorderStyle="Solid"
				Visible="False">
				<TodayDayStyle ForeColor="White" BackColor="#999999"></TodayDayStyle>
				<DayStyle BackColor="#E0E0E0"></DayStyle>
				<NextPrevStyle Font-Size="8pt" Font-Bold="True" ForeColor="White"></NextPrevStyle>
				<DayHeaderStyle Font-Size="8pt" Font-Bold="True" Height="8pt" ForeColor="#333333"></DayHeaderStyle>
				<SelectedDayStyle ForeColor="White" BackColor="#333399"></SelectedDayStyle>
				<TitleStyle Font-Size="12pt" Font-Bold="True" Height="12pt" ForeColor="White" BackColor="Chocolate"></TitleStyle>
				<OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>
			</asp:calendar></form>
	</body>
</HTML>
