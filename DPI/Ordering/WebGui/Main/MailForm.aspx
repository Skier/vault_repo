<%@ Page language="c#" Codebehind="MailForm.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Main.MailForm" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect</title>
		<meta name="vs_snapToGrid" content="False">
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
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout"
		onKeyPress="IEKeyCap()">
		<form method="post" name="MainForm" runat="server" ID="Form2">
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
								<td colspan="2" align="center">
									<table>
										<tr>
											<td class="main" align="right">Email:</td>
											<td class="main"><input type="text" class="main" name="Email"></td>
										</tr>
										<tr>
											<td class="main" align="right">
												Subject:</td>
											<td class="main"><input type="text" class="main" name="Subject"></td>
										</tr>
										<tr>
											<td class="main" align="right" valign="top">Message:</td>
											<td class="main"><textarea name="Message" cols="50" rows="8"></textarea></td>
										</tr>
										<tr>
											<td class="main">&nbsp;</td>
											<td class="main" align="center">
												<asp:ImageButton id="ImageButton1" runat="server" ImageUrl="images/submit.jpg"></asp:ImageButton></td>
										</tr>
										<tr>
											<td colspan="2" align="center">
												<asp:Label id="lblResponse" runat="server" Font-Names="Arial" ForeColor="Maroon" Font-Bold="True"></asp:Label></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg"></asp:imagebutton>
								</td>
								<td align="right"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td class="05_con_bold" colSpan="2" height="24" align="center"><br>
									<br>
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
								<td align="center" colSpan="2"></td>
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
