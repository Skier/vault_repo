<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Page language="c#" Codebehind="SatelliteSchedular.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.SatelliteSchedular" enableViewState="False"%>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="DPI.Services" %>
<%@ Import Namespace="DPI.Interfaces" %>
<%@ Import Namespace="DPI.Ordering" %>
<%@ Import Namespace="DPI.ClientComp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NOGetZip</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript" type="text/JavaScript">
		<!--
		function GetUrl()
		{
			return "test";
		}
		
		//-->
		</script>
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<form id="Form1" onsubmit="return check();" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
				<TBODY>
					<tr>
						<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
					</tr>
					<tr>
						<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
							height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
						<td vAlign="top" width="660">
							<asp:PlaceHolder id="phFrame" runat="server"></asp:PlaceHolder>
						</td>
					</tr>
					<tr>
						<td colSpan="2"><dpiuser:sitefooter id="SiteFooter" runat="server"></dpiuser:sitefooter><INPUT id="hdnSelVendor" style="WIDTH: 48px; HEIGHT: 22px" type="hidden" size="2" value="DirectNetwork"
								name="hdnSelVendor" runat="server"></td>
					</tr>
				</TBODY>
			</table>
		</form>
		</IFRAME></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
