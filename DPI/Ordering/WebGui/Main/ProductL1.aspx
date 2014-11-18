<%@ Page language="c#" Codebehind="ProductL1.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.ProductL1" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<script language="JavaScript">
		
		var clickedButton = false;
		
		function check() 
		{
			if (clickedButton)
				{
					clickedButton = false;
					return true;
				}
			return false;
		}
		function showlifeline(product)
		{
			if (product != "664")
				return;
				
			alert("To qualify for the Lifeline Product customers must meet Required Legal Qualifications.\n\nPRINT LIFELINE APPLICATION IN THE FORMS SECTION OF WEB CENTRAL.\n\nComplete application to validate eligibility.");
		}
		
		</script>
</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
<TABLE height=803 cellSpacing=0 cellPadding=0 width=456 border=0 
ms_2d_layout="TRUE">
  <TR vAlign=top>
    <TD width=456 height=803>
					<form id="form_ProductL1" onsubmit="return check(); " method="post" runat="server">
      <TABLE height=454 cellSpacing=0 cellPadding=0 width=801 border=0 
      ms_2d_layout="TRUE">
        <TR vAlign=top>
          <TD width=801 height=454>
									<table id="Table1" height="453" cellSpacing="0" cellPadding="0" width="800" align="left"
										border="0">
										<TBODY>
											<tr>
												<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
											</tr>
											<tr>
												<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
													height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
												<td vAlign="top" width="660" height="100%">
													<table height="100%" cellSpacing="0" cellPadding="0" width="660" border="0">
														<TBODY>
															<tr>
																<td colSpan="2"></td>
															</tr>
															<tr>
																<td class="05_con_sublabel_zip" vAlign="middle" align="right" background="images/subtable_header_lvl1.jpg"
																	bgColor="white" colSpan="2" height="61">ZipCode:
																	<asp:label id="lblZipCode" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp;
																	<asp:label id="lblIlec" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp; 
																	&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																</td>
															</tr>
															<tr>
																<td vAlign="top" align="center" colSpan="2"><asp:placeholder id="FeaturesGrid" runat="server" EnableViewState="False"></asp:placeholder></td>
															</tr>
															<tr>
																<td class="05_con_medium" align="center" vAlign="top" colSpan="2"><asp:label id="lblErrMsg" runat="server" EnableViewState="False" ForeColor="Red" Width="632px"
																		Visible="False" Font-Bold="True" Font-Names="Arial" Font-Size="Medium" Height="33px"></asp:label><asp:label id="Label1" runat="server" Width="660px">
																		<STRONG><FONT face="Arial" color="chocolate" size="2"><FONT size="3">&nbsp;&nbsp;&nbsp; *</FONT></FONT><FONT face="Arial" color="dimgray" size="2">Prompt 
																				Pay Discount applies when payment in full is made on or before the customer's 
																				due date.</FONT> </STRONG>
																	</asp:label></td>
															</tr>
															<tr>
																<td align="left" width="607" height="37">&nbsp;
																</td>
																<td align="left" height="37"><asp:imagebutton id="btnPrint" runat="server" ImageUrl="images/buttons_printversion.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;</td>
															</tr>
															<tr>
																<td align="left" width="607">&nbsp;&nbsp;&nbsp;
																	<asp:imagebutton id="btnPrevious" runat="server" EnableViewState="False" ImageUrl="images/btn_previous.jpg"></asp:imagebutton></td>
																<td align="left"><asp:imagebutton id="btnNext" runat="server" EnableViewState="False" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;
																</td>
															</tr>
														</TBODY></table>
												</td>
											</tr>
										</TBODY></table></TD></TR></TABLE>
					</form></TD></TR></TABLE>
	</body>
</HTML>
