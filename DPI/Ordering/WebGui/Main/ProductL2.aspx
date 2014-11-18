<%@ OutputCache Duration="1200" VaryByParam="*" Location="Client" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Page buffer="true" language="c#" Codebehind="ProductL2.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.ProductL2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>dPi Web Order</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript">
			window.history.forward(1);
		</script>
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
	<body text="#000000" bottomMargin="0" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server" onSubmit="return check(); ">
			<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
				<TBODY>
					<tr>
						<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
					</tr>
					<tr>
						<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
							height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
						<td vAlign="top" align="center" width="660">
							<table height="100%" cellSpacing="0" cellPadding="0" width="660" align="center" border="0">
								<tr>
									<td colSpan="2"></td>
								</tr>
								<tr>
									<td class="05_con_sublabel_zip" vAlign="middle" align="right" background="images/subtable_header_lvl2.jpg"
										bgColor="white" colSpan="2" height="61">ZipCode:
										<asp:label id="lblZipCode" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp;
										<asp:label id="lblIlec" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp; 
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									</td>
								</tr>
								<tr>
									<td vAlign="top" align="left" width="17">&nbsp;</td>
									<td vAlign="top" align="left">
										<asp:placeholder id="Level2Prods" runat="server"></asp:placeholder>
									</td>
								</tr>
								<tr>
									<td class="05_con_bold" vAlign="middle" align="center" bgColor="white" colSpan="2" height="40">&nbsp;<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:label id="Label1" runat="server" Font-Names="Arial" Font-Bold="True" ForeColor="Chocolate"
											Font-Size="Medium" Width="140px">Products Total</asp:label>&nbsp;
										<asp:label id="lblOrdTotal" runat="server" Font-Names="Arial" Font-Bold="True" ForeColor="Gray"
											Font-Size="Medium"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton>&nbsp;
									</td>
								</tr>
								<tr>
									<td vAlign="top" colSpan="2"><asp:label id="lblErrorMsg" runat="server" Font-Bold="True" Visible="False" ForeColor="Red"></asp:label></td>
								</tr>
								<tr>
									<td class="05_con_small" vAlign="top" align="center" bgColor="white" colSpan="2" height="50%"><FONT class="05_con_small">(Before 
											taxes &amp; fees)</FONT></td>
								</tr>
								<tr>
									<TD vAlign="top" align="left">&nbsp;&nbsp;&nbsp;&nbsp;
									</TD>
									<TD vAlign="top" align="right">&nbsp;&nbsp;&nbsp;&nbsp;
									</TD>
								</tr>
							</table>
						</td>
					</tr>
					<TR>
						<td colSpan="2"><dpiuser:sitefooter id="Sitefooter1" runat="server"></dpiuser:sitefooter></td>
					</TR>
				</TBODY>
			</table>
		</form></TR></TBODY></TABLE></FORM></TR></TBODY></TABLE>
		<script language="JavaScript" src="../Core/wz_tooltip.js" type="text/javascript"></script>
	</body>
</HTML>
