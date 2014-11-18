<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Page language="c#" Codebehind="OrderSumPend.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.OrderSumPend" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Web Order</title>
		<meta content="JavaScript" name="vs_defaultClientScript">
		<script language="JavaScript">
	<!---
		var clickedButton = false;
		function check() 
		{
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
		function Submit(product)
		{
			showlifeline(product);			
			clickedButton = true;
			
		}
		function showlifeline(product)
		{
			if (product != "664")
				return;
				
			alert("To qualify for the Lifeline Product customers must meet Required Legal Qualifications.\n\nPRINT LIFELINE APPLICATION IN THE FORMS SECTION OF WEB CENTRAL.\n\nComplete application to validate eligibility.");
		}
	//--->
		</script>
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<TABLE height="812" cellSpacing="0" cellPadding="0" width="493" border="0" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD width="493" height="812">
					<form id="Form1" onsubmit="return check(); " action="post" runat="server">
						<TABLE height="491" cellSpacing="0" cellPadding="0" width="802" border="0" ms_2d_layout="TRUE">
							<TR vAlign="top">
								<TD width="1" height="491"></TD>
								<TD width="801">
									<table height="490" cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
										<TBODY>
											<tr>
												<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
											</tr>
											<tr>
												<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
													height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
												<td vAlign="top" width="490">
													<table height="100%" cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
														<TBODY>
															<tr>
																<td colSpan="2"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
															</tr>
															<tr>
																<td vAlign="top" bgColor="white" colSpan="2">
																	<table cellSpacing="0" cellPadding="0" width="669" border="0">
																		<tr>
																			<td class="05_con_sublabel_zip" vAlign="middle" align="left" width="669" background="images/subtable_header_blank.jpg"
																				bgColor="white" colSpan="4" height="61">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																				<asp:label id="Label5" runat="server" Width="126px" ForeColor="Chocolate" Font-Size="Small"
																					Font-Names="Arial" Font-Bold="True" Height="22px">Order Summary</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
																				ZipCode:
																				<asp:label id="lblZipCode" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp;
																				<asp:label id="lblIlec" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp; 
																				&nbsp;&nbsp;&nbsp;&nbsp;
																			</td>
																		</tr>
																		<tr>
																			<td align="center" colSpan="4"><asp:placeholder id="phldrOrdrDetails" runat="server"></asp:placeholder></td>
																		</tr>
																		<tr>
																			<td colSpan="4">
																				<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																					<TR>
																						<td width="10">&nbsp;</td>
																						<td align="left" width="304" bgColor="gainsboro" height="19"><STRONG><FONT color="dimgray">&nbsp;&nbsp;&nbsp;Product 
																									Total&nbsp;</FONT></STRONG></td>
																						<TD align="right" bgColor="gainsboro" height="19"><asp:label id="lblOrderTotal" runat="server" Width="72px" ForeColor="Red"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																							<asp:label id="lblOrderTotalM2" runat="server" Width="72px"></asp:label>&nbsp;&nbsp;</TD>
																						<td width="10">&nbsp;</td>
																					</TR>
																				</table>
																			</td>
																		</tr>
																		<tr>
																			<td vAlign="middle" align="left" width="783" bgColor="white" colSpan="3" height="19">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Taxes, 
																				Fees and Surcharges&nbsp;
																				<asp:imagebutton id="btnMonthChart" runat="server" ImageUrl="images/btn_monthcharge.jpg"></asp:imagebutton></td>
																			<td align="right" bgColor="white" height="19">&nbsp;&nbsp;<asp:label id="lblFees" runat="server" Width="72px" ForeColor="Red"></asp:label>
																				&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
																				<asp:label id="lblFeesM2" runat="server" Width="72px"></asp:label>&nbsp;&nbsp;
																				<asp:label id="Label6" runat="server" Width="2px">.</asp:label>&nbsp;&nbsp;&nbsp;</td>
																		</tr>
																		<tr>
																			<td colSpan="4">
																				<table height="5" width="100%" border="0">
																					<TR>
																						<td width="5">&nbsp;</td>
																						<td width="100%" bgColor="chocolate" colSpan="2">&nbsp;<asp:label id="Label4" runat="server" Width="151px" ForeColor="White">Payment Details</asp:label></td>
																						<td width="5">&nbsp;</td>
																					</TR>
																				</table>
																			</td>
																		</tr>
																		<tr>
																			<td colSpan="4">
																				<table width="100%" border="0">
																					<TBODY>
																						<tr>
																							<td width="5">&nbsp;</td>
																							<td colSpan="2">
																								<!-----------------------Order total table ------------------------------------------>
																								<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																									<tr>
																										<td vAlign="top" colSpan="3"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																									</tr>
																									<TR bgColor="aliceblue">
																										<TD align="left" colSpan="1" rowSpan="1"><asp:label id="Label3" runat="server" Font-Size="Medium" Font-Names="Arial" Font-Bold="True">Order Total</asp:label></TD>
																										<TD align="right" height="9"><asp:label id="lblLocalAmountDue" runat="server" ForeColor="Red" Font-Size="Medium" Font-Bold="True"></asp:label></TD>
																										<td align="right" width="125" height="9"><asp:label id="lblLocalAmountDue2" runat="server" Font-Size="Medium" Font-Bold="True"></asp:label>&nbsp;
																										</td>
																									</TR>
																									<tr>
																										<td vAlign="top" colSpan="3"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																									</tr>
																									<TR bgColor="floralwhite">
																										<TD align="left" height="2">Long Distance Calling Card</TD>
																										<TD align="right" height="2"><asp:dropdownlist id="ddlLdAmount" runat="server" Width="74px" Height="10px" AutoPostBack="True">
																												<asp:ListItem Value="0">---</asp:ListItem>
																												<asp:ListItem Value="5.00">$5.00</asp:ListItem>
																												<asp:ListItem Value="10.00">$10.00</asp:ListItem>
																												<asp:ListItem Value="15.00">$15.00</asp:ListItem>
																												<asp:ListItem Value="20.00">$20.00</asp:ListItem>
																												<asp:ListItem Value="25.00">$25.00</asp:ListItem>
																												<asp:ListItem Value="30.00">$30.00</asp:ListItem>
																												<asp:ListItem Value="35.00">$35.00</asp:ListItem>
																												<asp:ListItem Value="40.00">$40.00</asp:ListItem>
																												<asp:ListItem Value="45.00">$45.00</asp:ListItem>
																												<asp:ListItem Value="50.00">$50.00</asp:ListItem>
																												<asp:ListItem Value="55.00">$55.00</asp:ListItem>
																												<asp:ListItem Value="60.00">$60.00</asp:ListItem>
																												<asp:ListItem Value="65.00">$65.00</asp:ListItem>
																												<asp:ListItem Value="70.00">$70.00</asp:ListItem>
																												<asp:ListItem Value="75.00">$75.00</asp:ListItem>
																												<asp:ListItem Value="80.00">$80.00</asp:ListItem>
																												<asp:ListItem Value="85.00">$85.00</asp:ListItem>
																												<asp:ListItem Value="90.00">$90.00</asp:ListItem>
																												<asp:ListItem Value="95.00">$95.00</asp:ListItem>
																												<asp:ListItem Value="100.00">$100.00</asp:ListItem>
																											</asp:dropdownlist></TD>
																										<td align="right" colSpan="1" height="2" rowSpan="1">&nbsp;----&nbsp;&nbsp; 
																											&nbsp;&nbsp;</td>
																									</TR>
																									<tr>
																										<td vAlign="top" colSpan="3"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																									</tr>
																									<TR bgColor="aliceblue">
																										<TD align="left"><asp:label id="Label2" runat="server" Width="160px" ForeColor="#C04000" Font-Size="Medium"
																												Font-Names="Arial" Font-Bold="True">Total Amount Due</asp:label></TD>
																										<TD align="right"><asp:label id="lblAmountDue" runat="server" Width="90px" ForeColor="Red" Font-Size="Medium"
																												Font-Names="Arial" Font-Bold="True"></asp:label></TD>
																										<td align="right">&nbsp;----&nbsp;&nbsp;&nbsp; &nbsp;</td>
																									</TR>
																									<tr>
																										<td vAlign="top" colSpan="3"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																									</tr>
																									<tr>
																										<td vAlign="top" colSpan="3"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																									</tr>
																									<tr>
																										<td vAlign="top" colSpan="3"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																									</tr>
																									<tr>
																										<td vAlign="top" colSpan="3"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																									</tr>
																								</TABLE>
																								<!--------------------------------------------------------------------------></td>
																							<td width="5">&nbsp;</td>
																						</tr>
																					</TBODY></table>
																			</td>
																		</tr>
																		<TR>
																			<TD align="center" bgColor="white" colSpan="4" rowSpan="1"><asp:label id="lblReminder" runat="server" Width="348px" ForeColor="#C00000" Visible="False">
																					<table border="0">
																						<tr>
																							<td><img src="images/bang.gif" border="0"></td>
																							<td><font color="red">Remind the customer to contact dPi to order service. The 800 
																									number is on the receipt.</font></td>
																						</tr>
																					</table>
																				</asp:label></TD>
																		</TR>
																		<TR>
																			<TD colSpan="4">
																				<TABLE height="5" width="100%" border="0">
																					<TR>
																						<TD width="5">&nbsp;</TD>
																						<TD bgColor="chocolate" colSpan="2">&nbsp;<asp:label id="Label1" runat="server" Width="151px" ForeColor="White">Order Notes (optional)</asp:label></TD>
																						<TD width="5">&nbsp;</TD>
																					</TR>
																				</TABLE>
																			</TD>
																		</TR>
																		<tr>
																			<td colSpan="4">
																				<table height="5" width="100%" border="0">
																					<TR>
																						<td width="5">&nbsp;</td>
																						<td width="100%" colSpan="2"><asp:textbox id="txtOrderNotes" runat="server" Width="100%" Height="96px" TextMode="MultiLine"
																								Rows="2"></asp:textbox></td>
																						<td width="5">&nbsp;</td>
																					</TR>
																				</table>
																			</td>
																		</tr>
																		<TR>
																			<TD align="center" colSpan="4"><asp:label id="lblErrMsg" runat="server" Width="625px" ForeColor="Red" Font-Names="Arial" Font-Bold="True"></asp:label></TD>
																		</TR>
																	</table>
																</td>
															</tr>
												</td>
											</tr>
											<TR>
												<TD align="left" width="607" height="37">&nbsp;&nbsp;&nbsp;</TD>
												<TD align="right" height="37"><asp:imagebutton id="btnPrint" runat="server" ImageUrl="images/buttons_printversion.jpg"></asp:imagebutton>&nbsp;&nbsp;
												</TD>
											</TR>
											<TR>
												<TD colSpan="2">
													<TABLE width="100%" border="0">
														<TR>
															<TD width="5">&nbsp;</TD>
															<TD bgColor="#cccccc" height="5">&nbsp;</TD>
															<TD width="13">&nbsp;</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD align="left" width="607">&nbsp;&nbsp;&nbsp;
													<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg" EnableViewState="False"></asp:imagebutton></TD>
												<TD align="right"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg" EnableViewState="False"></asp:imagebutton>&nbsp;&nbsp; 
													&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												</TD>
											</TR>
											<TR>
												<TD vAlign="top" align="center" bgColor="white" colSpan="2">&nbsp;
												</TD>
											</TR>
										</TBODY></table>
								</TD>
							</TR>
							<TR>
								<TD colSpan="2"><dpiuser:sitefooter id="Sitefooter1" runat="server"></dpiuser:sitefooter></TD>
							</TR>
						</TABLE>
				</TD>
			</TR>
		</TABLE>
		</FORM></TD></TR></TBODY>
		<script language="JavaScript" src="../Core/wz_tooltip.js" type="text/javascript"></script>
		</TABLE>
	</body>
</HTML>
