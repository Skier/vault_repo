<%@ Page language="c#" Codebehind="TrainingMenuRW.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Training.TrainingMenuRW" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>RentWay Training Menu</title> 
		<!---
		<script language="JavaScript">
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
		<LINK href="../Main/Styles/Navigator.css" rel="stylesheet">
			<LINK href="../Main/Styles/DPI.css" rel="stylesheet">
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0">
		<form id="ReportformZ" onsubmit="return check(); " runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
				<tr>
					<td colSpan="2"><uc1:siteheader id="SiteHeader1" runat="server"></uc1:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
						height="100%">
						<dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol>
					</td>
					<td vAlign="top" width="660">
						<subhdr:subheader id="subheader" runat="server"></subhdr:subheader>
						<table border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr>
								<td vAlign="middle" align="right" colspan="4" background="images/subheader_agenttraining.jpg"
									height="68" width="655">
									<asp:Label id="Label1" runat="server" ForeColor="Black" Font-Bold="True" Font-Size="Medium"
										Font-Names="Tahoma" Width="376px">RentWay Agent Training Program</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
							</tr>
							<tr>
								<td vAlign="bottom" align="center" colspan="4">&nbsp;</td>
							</tr>
							<tr>
								<td width="64">&nbsp;</td>
								<td vAlign="top" align="center">
									<asp:ImageButton id="btnManual" runat="server" ImageUrl="images/subheader_radiobutton.jpg"></asp:ImageButton></td>
								<td valign="top">
									<P>&nbsp;&nbsp;
										<asp:LinkButton id="lnkManual" runat="server" Font-Names="Tahoma" Font-Size="Large" Font-Bold="True"
											ForeColor="#804000">Step 1 The Tutorial</asp:LinkButton><br>
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:Label id="Label3" runat="server" Width="312px" ForeColor="SteelBlue">Click this button to learn about WebCentral. </asp:Label></P>
								</td>
								<td rowspan="3">&nbsp;
									<asp:Image id="Image2" runat="server" ImageUrl="images/website_graphics.jpg"></asp:Image></td>
							</tr>
							<tr>
								<td height="5" width="64">&nbsp;</td>
								<td vAlign="top" align="center" height="5">
									<asp:ImageButton id="btnTest" runat="server" ImageUrl="images/subheader_radiobutton.jpg"></asp:ImageButton></td>
								<td height="5" valign="top">
									<P align="left">&nbsp;&nbsp;
										<asp:LinkButton id="lnkTest" runat="server" Font-Names="Tahoma" Font-Size="Large" Font-Bold="True"
											ForeColor="#804000">Step 2 Certification</asp:LinkButton><BR>
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:Label id="Label2" runat="server" Width="233px" ForeColor="SteelBlue">After you have completed the tutorial, Click this button to get Certified.</asp:Label></P>
								</td>
							</tr>
							<tr>
								<td width="64">&nbsp;</td>
								<td vAlign="top" align="center"></td>
								<td>
								</td>
							</tr>
						</table>
						<br>
					</td>
				</tr>
				<tr>
					<td colSpan="2"><uc1:sitefooter id="SiteFooter1" runat="server"></uc1:sitefooter></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
