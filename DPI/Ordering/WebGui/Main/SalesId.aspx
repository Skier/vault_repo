<%@ Page language="c#" Codebehind="SalesId.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.SalesId" %>
<%@ Register TagPrefix="dPiUser" TagName="SalesIdCell" Src="control/SalesIdCell.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ReportMain</title>
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0"
		ms_positioning="GridLayout">
		<TABLE height="803" cellSpacing="0" cellPadding="0" width="619" border="0" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD width="619" height="803">
					<form id="Form1" onsubmit="return check();" method="post" runat="server">
						<TABLE height="617" cellSpacing="0" cellPadding="0" width="801" border="0" ms_2d_layout="TRUE">
							<TR vAlign="top">
								<TD width="801" height="617">
									<table height="616" cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
										<tr>
											<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
										</tr>
										<tr>
											<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
												height="587"><dpiuser:sidecontrol id="Sidecontrol1" runat="server"></dpiuser:sidecontrol></td>
											<td vAlign="top" height="587">
												<table cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
													<tr>
														<td colSpan="2"><subhdr:subheader id="subheader1" runat="server"></subhdr:subheader></td>
													</tr>
													<tr>
														<td height="20">&nbsp;</td>
													</tr>
													<tr>
														<td>&nbsp;</td>
													</tr>
													<tr>
														<td>&nbsp;</td>
													</tr>
													<tr>
														<td colSpan="2" height="27">
															<!--<dPiUser:SalesIdCell id="SalesIdCell" runat="server"></dPiUser:SalesIdCell>-->
														</td>
													</tr>
													<tr>
														<td colSpan="2" height="46">
															<p>
																<center><font size="4"></font>&nbsp;</center>
																<CENTER><FONT size="4">Sales Id </FONT>
																</CENTER>
														</td>
													</tr>
													<tr>
														<td colSpan="2">
															<p>
																<center><asp:textbox id="txtSalesId" runat="server"></asp:textbox></center>
															<P></P>
														</td>
													</tr>
													<tr>
														<td>
															<center>&nbsp;</center>
														</td>
														<td>
															<center>
																<CENTER><asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg"></asp:imagebutton><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton></CENTER>
															</center>
														</td>
													</tr>
													<tr>
														<td align="center" colSpan="2">
															<P><asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" Font-Size="10pt" Font-Name="verdana"
																	Display="Static" ValidationExpression="^\d{5}$" ControlToValidate="txtSalesId"></asp:regularexpressionvalidator></P>
															<p>&nbsp;</p>
															<P><asp:label id="Label1" runat="server" Font-Size="12pt" Width="446px" Visible="False" ForeColor="#cc3333"
																	Font-Names="Arial" Font-Bold="True">Please enter your Sales Id</asp:label></P>
														</td>
													</tr>
												</table>
											</td>
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
