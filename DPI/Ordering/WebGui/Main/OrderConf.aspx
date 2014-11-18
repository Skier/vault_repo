<%@ Page language="c#" Codebehind="OrderConf.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.OrderConf" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>DPI Ordering - Order Confirmation</title>
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
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<TABLE height="663" cellSpacing="0" cellPadding="0" width="540" border="0" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD width="540" height="663">
					<form id="Form1" action="post" runat="server" onSubmit="return check(); ">
						<TABLE height="538" cellSpacing="0" cellPadding="0" width="661" border="0" ms_2d_layout="TRUE">
							<TR vAlign="top">
								<TD width="661" height="538">
									<table height="537" cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
										<tr>
											<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
										</tr>
										<tr>
											<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
												height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
											<td vAlign="top" width="488">
												<table height="100%" cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
													<tr>
														<td width="100%" colSpan="5"></td>
													</tr>
													<tr>
														<td class="05_con_sublabel_zip" vAlign="middle" align="left" background="images/subtable_header_blank.jpg"
															bgColor="white" colSpan="5" height="61">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="lblSubheader" runat="server" Width="152px" ForeColor="Chocolate" Font-Size="Small"
																Font-Names="Arial" Font-Bold="True" Height="22px"></asp:Label>
															&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
															ZipCode:
															<asp:label id="lblZipCode" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp;
															<asp:label id="lblIlec" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp; 
															&nbsp;&nbsp;&nbsp;&nbsp;
														</td>
													</tr>
													<tr>
														<td vAlign="top" align="center">
															<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																<tr>
																	<td width="3" bgColor="#ffffff" colSpan="1" rowSpan="10" height="174">&nbsp;&nbsp;&nbsp;</td>
																	<td bgColor="#ffffff" height="19" width="123">&nbsp;&nbsp; Customer Name</td>
																	<td bgColor="#ffffff" height="19"><asp:label id="lblCustName" runat="server" Width="232px"></asp:label></td>
																	<td width="5" bgColor="#ffffff" rowSpan="10" height="174">&nbsp;</td>
																</tr>
																<tr>
																	<td bgColor="#ffffff" height="17" width="123">&nbsp;&nbsp; Merchant</td>
																	<td bgColor="#ffffff" height="17"><asp:label id="lblMerch" runat="server" Width="224px"></asp:label></td>
																</tr>
																<tr>
																	<td bgColor="#ffffff" width="123">&nbsp;&nbsp; Date</td>
																	<td width="50" bgColor="#ffffff">
																		<asp:label id="lblDate" runat="server" Width="216px"></asp:label></td>
																</tr>
																<tr>
																	<td bgColor="#ffffff" height="17" width="123">&nbsp;&nbsp;
																		<asp:Label id="lblPayType" runat="server">Payment Type</asp:Label></td>
																	<td bgColor="#ffffff" height="17"><asp:label id="lblPType" runat="server" Width="224px"></asp:label></td>
																</tr>
																<tr>
																	<td colspan="2">&nbsp;</td>
																</tr>
																<tr>
																	<td colspan="2" height="98">
																		<table width="100%" border="0" class="grey_table" cellpadding="0" cellspacing="0">
																			<tr>
																				<td colspan="2"><asp:placeholder id="placehldrOrderedProducts" runat="server"></asp:placeholder></td>
																			</tr>
																			<tr>
																				<td colspan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																			</tr>
																			<tr>
																				<td align="left" bgColor="gainsboro">&nbsp; PRODUCT TOTAL</td>
																				<td align="right" bgColor="gainsboro"><asp:Label id="lblProdSubTotal" runat="server"></asp:Label>&nbsp;&nbsp;</td>
																			</tr>
																			<tr>
																				<td align="left">&nbsp; LD Amount:</td>
																				<td align="right"><asp:label id="lblLDAmount" runat="server"></asp:label>&nbsp;&nbsp;</td>
																			</tr>
																			<tr>
																				<td align="left">&nbsp; Taxes, fees and surcharges:</td>
																				<td align="right"><asp:label id="lblTaxes" runat="server"></asp:label>&nbsp;&nbsp;</td>
																			</tr>
																		</table>
																	</td>
																</tr>
															</table>
															<asp:label id="lblError" runat="server" Font-Names="Arial" Font-Bold="True" ForeColor="Red"></asp:label></td>
													</tr>
													<tr>
														<td bgColor="white" align="right" height="92"><FONT face="Arial" color="chocolate" size="4"><STRONG>Total 
																	Amount Due:</STRONG></FONT>&nbsp;&nbsp;
															<asp:label id="lblOrderTotal" runat="server" Font-Names="Arial" Font-Bold="True" ForeColor="DarkGray"
																Font-Size="Medium"></asp:label>&nbsp;<br>
															<FONT face="Arial" color="chocolate" size="4"><STRONG>
																	<asp:Label id="lblTotAmntPaid" runat="server" Width="213px">Total Amount Paid:</asp:Label>
																	&nbsp; </STRONG></FONT>&nbsp;
															<asp:label id="lblPaid" runat="server" Font-Names="Arial" Font-Bold="True" ForeColor="DarkGray"
																Font-Size="Medium"></asp:label>&nbsp;
														</td>
													</tr>
													<tr>
														<td>
															<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																<tr>
																	<td vAlign="top" align="left">&nbsp;&nbsp;&nbsp;
																		<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg"></asp:imagebutton></td>
																	<td vAlign="top" align="right"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																	</td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td vAlign="top" height="100%">
															<!------------------------------------------------------------------------>
															<!------------------------------------------------------------------------>
															<!----------------------------- FILLER ----------------------------------->
															<!------------------------------------------------------------------------>
															<!------------------------------------------------------------------------></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td colSpan="2"><dpiuser:sitefooter id="Sitefooter1" runat="server"></dpiuser:sitefooter></td>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
					</form>
				</TD>
			</TR>
		</TABLE>
	</body>
</HTML>
