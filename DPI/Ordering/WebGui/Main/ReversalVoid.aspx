<%@ Page language="c#" Codebehind="ReversalVoid.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Main.ReversalVoid" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Import Namespace="DPI.Interfaces" %>
<%@ Import Namespace="DPI.Services" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect</title>
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
								<td class="05_con_label" width="660" colSpan="2" height="24"><asp:image id="imgWorkflow" runat="server"></asp:image></td>
							</tr>
							<% if(LoginSvc.GetIfClerkIDRequested((IUser)Session["User"])){ %>
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
												<asp:label id="Label1" runat="server">Co-Worker ID</asp:label>&nbsp;
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
								<td class="05_con_label" width="660" colSpan="2" height="24">
									<P>&nbsp;
										<asp:RadioButtonList id="rListTrans" runat="server" Width="225px" AutoPostBack="True">
											<asp:ListItem Value="Wireless" Selected="True">Slingshot Internet</asp:ListItem>
										</asp:RadioButtonList></P>
								</td>
							</tr>
							<tr>
								<td colSpan="2">
									<P align="center"><asp:placeholder id="PlaceHolder1" runat="server"></asp:placeholder></P>
									<P align="center">&nbsp;</P>
									<P align="center"><asp:label id="lblNoVoids" runat="server" Visible="False"></asp:label></P>
								</td>
							</tr>
							<tr>
								<td align="left">&nbsp;&nbsp;&nbsp;
									<asp:ImageButton id="ImageButton1" runat="server" ImageUrl="images/btn_gotomain.jpg"></asp:ImageButton>
								</td>
								<td align="right">
									<asp:imagebutton id="btnNext" tabIndex="1" runat="server" ImageUrl="images/btn_void.gif"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td class="05_con_bold" align="center" colSpan="2" height="24"><br>
									<div align="center">
										<asp:Label id="lblErrMsg" runat="server" Width="588px" ForeColor="Red"></asp:Label></div>
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
