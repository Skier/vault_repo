<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Page language="c#" Codebehind="MonthChart.aspx.cs" AutoEventWireup="false"  Inherits="DPI.Ordering.MonthChart" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>dPi Web Ordering - Month Chart</title>
		<LINK href="Styles/Navigator.css" rel="stylesheet">
			<LINK href="Styles/DPI.css" rel="stylesheet">
  </HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
<TABLE height=668 cellSpacing=0 cellPadding=0 width=403 border=0 
ms_2d_layout="TRUE">
  <TR vAlign=top>
    <TD width=403 height=668>
					<form id="Form1" action="post" runat="server">
      <TABLE height=401 cellSpacing=0 cellPadding=0 width=666 border=0 
      ms_2d_layout="TRUE">
        <TR vAlign=top>
          <TD width=666 height=401>
									<table cellSpacing="0" cellPadding="0" width="665" align="left" border="0" height="400">
										<tr>
											<td vAlign="top" bgColor="white" colSpan="2" align="center" height="443">
												<!------------------------------------------------->
												<table cellSpacing="0" cellPadding="0" width="510" border="0" height="100%">
													<tr>
														<td align="center" colspan="6" height="109"><asp:image id="Image1" runat="server" ImageUrl="images/printheader.jpg" ImageAlign="Middle"></asp:image></td>
													</tr>
													<tr>
														<td rowspan="9" width="5">&nbsp;</td>
														<td class="05_con_sublabel_zip" vAlign="middle" align="right" bgColor="white" colSpan="4"
															height="31" width="636">
															ZipCode:
															<asp:label id="lblZipCode" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp;
															<asp:label id="lblIlec" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
														</td>
														<td rowspan="9">&nbsp;</td>
													</tr>
													<tr>
														<td colspan="6" align="center" height="31"><asp:label id="lblDate" runat="server" Width="205px"></asp:label></td>
													</tr>
													<tr>
														<td align="center" colSpan="4" width="636" height="148"><asp:placeholder id="phldrMonthChart" runat="server"></asp:placeholder></td>
													</tr>
													<tr>
														<td align="left" width="121" bgColor="#ffffff" colSpan="3" height="7"><STRONG><FONT color="dimgray">
																	&nbsp;&nbsp;</FONT></STRONG></td>
														<td align="right" bgColor="#ffffff" height="7" width="624">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
													</tr>
													<tr>
														<td align="center" colSpan="6">
															<br>
															<asp:imagebutton id="btnPrint" runat="server" ImageUrl="images/btn_print2.jpg" CausesValidation="False"></asp:imagebutton>
															<br>
															<asp:Label id="lblErrMsg" runat="server" Width="374px" ForeColor="Red" Font-Names="Arial"></asp:Label>
															<br>
															<asp:Button id="btnClose" runat="server" Text="Close"></asp:Button>
														</td>
													</tr>
												</table>
												<A onmouseover='this.T_BORDERCOLOR="#ffffff"; this.T_SHADOWCOLOR="#ffffff"; this.T_BGCOLOR="#ffffff"; return escape("")'
													href="javascript:void(0);"></A>
											</td>
										</tr>
									</table></TD></TR></TABLE>
					</form></TD></TR></TABLE>
	</body>
</HTML>
