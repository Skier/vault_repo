<%@ Page language="c#" Codebehind="LDCard.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.LDCard" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>NOGetZip</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
  </HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="left" width="800" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
						height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
					<td width="660" valign="top">
						<table height="100%" width="660" align="left" border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td width="100%" colSpan="5"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
							</tr>
							<tr>
								<td class="05_con_label" height="112" width="334">
									<img src="images/subtable_header_ppld1.jpg" border='0' width="100%">
								</td>
								<td class="05_con_label" height="112" width="326">
									<img src="images/subtable_header_LDCardR.jpg" border='0' width="100%">
								</td>
							</tr>
							<tr>
								<td colspan="2" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:Label id="Label1" runat="server" Font-Names="Arial Black" Font-Size="Small" ForeColor="Gray">Please choose one of the products listed below:</asp:Label></td>
							</tr>
							<tr>
								<td class="05_con_label" colspan="2" align="center" bgColor="#ffffff">
									<asp:CheckBoxList id="CheckBoxList1" runat="server" RepeatDirection="Horizontal" Width="647px" TextAlign="Left"
										Font-Size="Smaller" Font-Names="Arial">
										<asp:ListItem Value="1"><img src="images/callcard_5.jpg" border="0"><br>$5 Domestic Pre-Paid LD</asp:ListItem>
										<asp:ListItem Value="2"><img src="images/callcard_15.jpg" border="0"><br>$10 Domestic Pre-Paid LD</asp:ListItem>
										<asp:ListItem Value="3"><img src="images/callcard_20.jpg" border="0"><br>$20 Domestic Pre-Paid LD</asp:ListItem>
										<asp:ListItem Value="4"><img src="images/callcard_spanish.jpg" border="0"><br>International Pre-Paid LD</asp:ListItem>
									</asp:CheckBoxList>&nbsp;
								</td>
							</tr>
							<tr>
								<td colspan="2" class="05_con_bold" height="24" bgColor="#ffffff" align="center">&nbsp;
									<asp:Label id="lblErrMsg" runat="server" Font-Names="Arial" Font-Size="Smaller" ForeColor="Red"></asp:Label></td>
							</tr>
							<tr>
								<td align="left" width="607">&nbsp;&nbsp;&nbsp;</td>
								<td align="right"><asp:imagebutton id="btnNext" runat="server" EnableViewState="False" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<dpiuser:SiteFooter id="SiteFooter" runat="server"></dpiuser:SiteFooter>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
