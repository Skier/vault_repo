<%@ Page language="c#" Codebehind="PendingConf.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Main.PendingConf" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
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
								<td colspan="2" class="05_con_label" width="660" height="24"><asp:image id="imgWorkflow" runat="server"></asp:image></td>
							</tr>
							<tr>
								<td align="center" colSpan="2">
									<P>
										<asp:PlaceHolder id="phVoidedTran" runat="server"></asp:PlaceHolder></P>
            <P>&nbsp;</P>
            <P>
															<asp:ImageButton id="btnPrint" runat="server" ImageUrl="images/btn_print2.jpg"></asp:ImageButton></P>
									<P>&nbsp;</P>
								</td>
							</tr>
							<tr>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:imagebutton id="btn_GotoMain" runat="server" ImageUrl="images/btn_gotomain.jpg"></asp:imagebutton></td>
								<td align="right"><asp:imagebutton id="ImageButton1" runat="server" ImageUrl="images/btn_voidAnother.gif"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td class="05_con_bold" align="center" colSpan="2" height="24"><br>
									<asp:validationsummary id="ValidationSummary1" runat="server" Width="341px"></asp:validationsummary><br>
									<div align="center">&nbsp;</div>
								</td>
							</tr>
							<tr>
								<td align="center" colSpan="2">
									<div class="05_con_normal" align="center"></div>
									<br>
								</td>
							</tr>
							<tr>
								<td align="center" colSpan="2"><asp:label id="lblErrMsg" runat="server" Width="472px" ForeColor="Red"></asp:label></td>
							</tr>
							<tr>
								<td class="05_con_bold" colSpan="2" height="24"><br>
									<br>
									<div align="center">&nbsp;</div>
								</td>
							</tr>
							<tr>
								<td align="center" colSpan="2">
									<div class="05_con_normal" align="center"></div>
									<br>
									<br>
								</td>
							</tr>
							<tr>
								<td colSpan="2">
									<div align="center"></div>
								</td>
							</tr>
							<tr>
								<td colSpan="2" height="100%">&nbsp;</td>
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
