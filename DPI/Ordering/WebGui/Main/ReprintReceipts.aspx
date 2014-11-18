<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Page language="c#" Codebehind="ReprintReceipts.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Main.ReprintReceipts" %>
<%@ Import Namespace="DPI.ClientComp" %>
<%@ Import Namespace="DPI.Ordering" %>
<%@ Import Namespace="DPI.Interfaces" %>
<%@ Import Namespace="DPI.Services" %>
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
		
		<!---
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
		--->
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
							</tr>
							<tr>
								<td colSpan="2">
									<P align="center"><asp:placeholder id="PlaceHolder1" runat="server"></asp:placeholder></P>
									<P align="center">&nbsp;</P>
									<P align="center"><asp:label id="lblNoReceipts" runat="server" Visible="False"></asp:label></P>
								</td>
							</tr>
							<tr>
								<td align="left">&nbsp;&nbsp;&nbsp;
									<asp:imagebutton id="ImageButton1" runat="server" ImageUrl="images/btn_gotomain.jpg"></asp:imagebutton></td>
								<td align="right"><asp:imagebutton id="btnNext" tabIndex="1" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;
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
