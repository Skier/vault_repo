<%@ Page language="c#" Codebehind="DCMain.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.DCMain" %>
<%@ Import Namespace="DPI.Services" %>
<%@ Import Namespace="DPI.Interfaces" %>
<%@ Import Namespace="DPI.Ordering" %>
<%@ Import Namespace="DPI.ClientComp" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>dPi Teleconnect - Debit Card Main</title>
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
</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0"
		ms_positioning="GridLayout">
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
								<td colSpan="2"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
							</tr>
							<tr>
								<td class="05_con_label" width="660" colSpan="2" height="24"><asp:image id="imgWorkflow" runat="server" ImageUrl="images/subtable_header_purpose.jpg"></asp:image></td>
							</tr>
							<% if (Sales.SalesIdReq((IUser)Session["User"])){ %>
							<tr>
								<td colSpan="2">
									<!------------------ SALES ID CELL ---------------------->
									<table id="Table1" cellSpacing="0" cellPadding="0" width="655" border="0">
										<tr>
											<td colSpan="5"><IMG height="17" src="images/subheader_top.jpg" width="655" border="0"></td>
										</tr>
										<tr bgColor="#f3f3f3">
											<td width="9" height="6"><IMG height="100%" src="images/subheader_left.jpg" width="25" border="0"></td>
											<td align="center" width="618">&nbsp;
												<asp:label id="Label2" runat="server">Co-Worker ID</asp:label>&nbsp;
												<asp:textbox id="txtSalesId" runat="server"></asp:textbox>&nbsp;&nbsp;&nbsp;
											</td>
											<TD align="right" width="12" height="6"><IMG height="100%" src="images/subheader_right.jpg" width="12" border="0"></TD>
										</tr>
										<TR>
											<TD colSpan="5"><IMG height="15" src="images/subheader_bottom.jpg" width="655" border="0"></TD>
										</TR>
									</table>
									<!------------------ !SALES ID CELL! ----------------------></td>
							</tr>
							<% } %>
							<tr>
								<td align="center" colSpan="2">
									<table cellSpacing="0" cellPadding="5" width="90%" border="0">
										<tr>
											<td colspan="2"><asp:label id="Label1" runat="server" Font-Bold="True" Font-Names="Arial">Please select one of the following options</asp:label></td>
										</tr>
										<tr>
											<td rowspan="2" style="WIDTH: 9px">
												<asp:radiobuttonlist id="rblCardOption" runat="server" CellPadding="0" CellSpacing="0" ForeColor="White" Height="69px" Width="582px">
<asp:ListItem Value="New" Selected="True"><font size=3 color="green">New</font> &nbsp;&nbsp;&nbsp;Purpose Instant Issue Card.<font class="05_con_small">
														<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(Purpose Prepaid MasterCard will be sent to Customer within 7-10 days)</font></asp:ListItem>
<asp:ListItem Value="Reload"><font  size=3 color="chocolate">Reload</font> &nbsp;&nbsp;&nbsp;Existing Purpose Instant Issue Card or Purpose Prepaid MasterCard.</asp:ListItem>
												</asp:radiobuttonlist>
											</td>
										</tr>
									</table>
									<P>&nbsp;</P>
									<P>&nbsp;</P>
								</td>
							</tr>
							<tr>
								<td align="left">&nbsp;&nbsp;&nbsp;
									<asp:imagebutton id="btnMain" runat="server" ImageUrl="images/btn_gotomain.jpg"></asp:imagebutton></td>
								<td align="right"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td class="05_con_bold" align="center" colSpan="2" height="24"><br>
									<div align="center"><asp:label id="lblErrMsg" runat="server" Width="588px" ForeColor="Red"></asp:label></div>
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
