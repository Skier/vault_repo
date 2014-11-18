<%@ Page language="c#" Codebehind="PL_OrdSummary.aspx.cs" AutoEventWireup="false"  Inherits="DPI.Ordering.PL_OrdSummary" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>dPi Web Ordering - Product Price Look-up : Order Summary</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript">
		<!--
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
		//-->
		</script>
</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<form id="Form1" onsubmit="return check()" action="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
				<tr>
					<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
						height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
					<td vAlign="top" width="490">
						<table height="100%" cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
							<tr>
								<td colSpan="2"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
							</tr>
							<tr>
								<td vAlign="top" bgColor="white" colSpan="2">
									<table cellSpacing="0" cellPadding="0" width="660" border="0">
										<tr>
											<td class="05_con_sublabel_zip" vAlign="middle" align="right" background="images/subtable_header_ordsum.jpg"
												bgColor="white" colSpan="4" height="61">ZipCode:
												<asp:label id="lblZipCode" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp;
												<asp:label id="lblIlec" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp; 
												&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										<tr>
											<td align="center" colSpan="4"><asp:placeholder id="phldrOrdrDetails" runat="server"></asp:placeholder></td>
										</tr>
										<tr>
											<td colspan="4">
												<table border="0" cellspacing="0" cellpadding="0" width="100%">
													<TR>
														<td width="10">&nbsp;</td>
														<td style="WIDTH: 304px; HEIGHT: 19px" align="left" width="304" bgColor="gainsboro"><STRONG><FONT color="dimgray">
																	&nbsp;&nbsp;&nbsp;Product Total</FONT></STRONG></td>
														<td style="HEIGHT: 19px" align="right" bgColor="gainsboro">&nbsp;<asp:label id="lblOrderTotal" runat="server" Width="72px" ForeColor="Red"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
															&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
															<asp:Label id="lbl2month" runat="server"></asp:Label>
															<asp:Label id="Label6" runat="server" Width="2px">.</asp:Label>
														</td>
														<td width="10">
															&nbsp;
														</td>
													</TR>
												</table>
											</td>
										</tr>
										<tr>
											<td style="WIDTH: 768px; HEIGHT: 20px" align="left" width="768" bgColor="white" colSpan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
												Taxes, Fees and Surcharges
												<asp:ImageButton id="btnMonthChart" runat="server" ImageUrl="images/btn_monthcharge.jpg"></asp:ImageButton></td>
											<td style="HEIGHT: 20px" align="right" bgColor="white"><asp:label id="lblFees" runat="server" Width="72px" ForeColor="Red"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:Label id="lbl2monthtotal" runat="server"></asp:Label>
												&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td>&nbsp;</td>
											<td align="right" bgColor="white" colSpan="2" rowSpan="1">&nbsp;</td>
										</tr>
										<tr>
											<td colSpan="4" align="right" bgColor="white"><asp:label id="Label2" runat="server" Width="160px" Font-Bold="True" Font-Names="Arial" ForeColor="#C04000"
													Font-Size="Medium">Total Amount Due</asp:label>&nbsp;<asp:label id="lblAmountDue" runat="server" Width="90px" Font-Bold="True" Font-Names="Arial"
													ForeColor="Red" Font-Size="Medium"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:label id="lblAmountDue2" runat="server" Width="90px" Font-Size="Medium" ForeColor="Gray"
													Font-Names="Arial" Font-Bold="True"></asp:label>&nbsp;&nbsp; &nbsp;&nbsp;
											</td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td>&nbsp;</td>
											<td align="right" bgColor="white" colSpan="2" rowSpan="1">
												<P>&nbsp;</P>
												<P>&nbsp;
													<asp:imagebutton id="btnPrint" runat="server" ImageUrl="images/btn_print2.jpg"></asp:imagebutton>&nbsp;&nbsp;
												</P>
											</td>
										</tr>
										<tr>
											<td align="center" colSpan="4"><asp:validationsummary id="validSummary" runat="server" DisplayMode="List"></asp:validationsummary><asp:label id="lblError" runat="server" Width="308px" Font-Bold="True" Font-Names="Arial" ForeColor="Red"></asp:label></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td vAlign="top" align="left" bgColor="white">&nbsp;&nbsp;&nbsp;&nbsp;<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg" CausesValidation="False"></asp:imagebutton>
								</td>
								<td vAlign="top" align="right" bgColor="white"><asp:imagebutton id="btnGotoMain" runat="server" ImageUrl="images/btn_gotomain.jpg" CausesValidation="False"></asp:imagebutton>
									&nbsp;
								</td>
							</tr>
							<tr>
								<td vAlign="top" align="center" bgColor="white" colSpan="2">&nbsp;
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="2"><dpiuser:sitefooter id="Sitefooter1" runat="server"></dpiuser:sitefooter></td>
				</tr>
			</table>
		</form>
		<script language="JavaScript" src="../Core/wz_tooltip.js" type="text/javascript"></script>
	</body>
</HTML>
