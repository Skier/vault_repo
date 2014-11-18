<%@ Page language="c#" Codebehind="DebCardWireless.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.DebCardWireless" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NOGetZip</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
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
								<td width="100%" colSpan="5"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
							</tr>
							<tr>
								<td class="05_con_label" width="334" height="112"><IMG src="images/subtable_header_internetL.jpg" width="100%" border="0">
								</td>
								<td class="05_con_label" width="326" height="112"><IMG src="images/subtable_header_internetr.jpg" width="100%" border="0">
								</td>
							</tr>
							<tr>
								<td colSpan="2">&nbsp;</td>
							</tr>
							<tr>
								<td colspan="2" align="center">
									<table border="0" cellpadding="0" cellspacing="0" width="600">
										<tr>
											<td style="HEIGHT: 36px" bgColor="#ffffff" colSpan="2" height="36"><br>
												<div align="center">Select a Vendor<br>
													<asp:dropdownlist id="ddlVendor" runat="server" Width="225px" AutoPostBack="True"></asp:dropdownlist></div>
											</td>
										</tr>
										<tr>
											<td style="HEIGHT: 36px" bgColor="#ffffff" colSpan="2" height="36"><br>
												<div align="center">Select a Product<br>
													<asp:dropdownlist id="ddlProduct" runat="server" Width="225px" AutoPostBack="True"></asp:dropdownlist></div>
											</td>
										</tr>
										<tr>
											<td height="30">&nbsp;</td>
										</tr>
										<tr>
											<td colSpan="2" align="right">&nbsp;&nbsp;<IMG height="29" src="images/product_header.jpg" width="629" border="0"></td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td align="center" bgColor="#ffffff" colSpan="2" height="50">
												<div class="05_con_normal" align="center">&nbsp;</div>
												<DIV class="05_con_normal" align="center"><asp:label id="lblProductSummary" runat="server" Width="635px" Font-Italic="True"> $150 - Verizon 60 days</asp:label></DIV>
											</td>
										</tr>
										<tr>
											<td colSpan="2" align="right"><IMG height="1" src="images/pixel_gray.jpg" width="629" border="0"></td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 288px; HEIGHT: 18px" align="right">Payment Method</td>
											<td style="HEIGHT: 18px" align="right"><asp:dropdownlist id="ddlPayMethod" runat="server" AutoPostBack="True">
													<asp:ListItem Value="0" Selected="True">Cash</asp:ListItem>
													<asp:ListItem Value="1">Credit Card</asp:ListItem>
													<asp:ListItem Value="2">Debit Card</asp:ListItem>
													<asp:ListItem Value="3">Check</asp:ListItem>
													<asp:ListItem Value="4">Money Order</asp:ListItem>
												</asp:dropdownlist>
												&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
											<td style="HEIGHT: 18px">&nbsp;</td>
										</tr>
										<tr>
											<td colSpan="2" align="right"><IMG height="1" src="images/pixel_gray.jpg" width="629" border="0"></td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 288px; HEIGHT: 15px" align="right"><asp:label id="Label2" runat="server" Width="160px" ForeColor="#C04000" Font-Bold="True" Font-Size="Medium"
													Font-Names="Arial">Total Amount Due</asp:label></td>
											<td style="HEIGHT: 15px" align="right"><asp:label id="lblAmountDue" runat="server" Width="82px" Font-Size="Medium"></asp:label>
												&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										<tr>
											<td style="WIDTH: 288px; HEIGHT: 23px" align="right">Total Amount Collected</td>
											<td style="HEIGHT: 23px" align="right"><asp:textbox id="txtAmountTendered" runat="server" Width="89px"></asp:textbox>
												&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										<tr>
											<td colSpan="2" align="right"><IMG height="1" src="images/pixel_gray.jpg" width="629" border="0"></td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 288px" rowSpan="1">&nbsp;</td>
											<td vAlign="middle" align="right"><asp:label id="txtNonRefund" runat="server" Width="177px" Font-Italic="True" CssClass="05_con_small">Payment is non-refundable.</asp:label><asp:imagebutton id="btnChangeDue" runat="server" ImageUrl="images/btn_changedue.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 288px" align="right" height="40"></td>
											<td vAlign="middle" align="right">&nbsp;&nbsp;
												<asp:label id="lblChangeDue" runat="server" Width="111px" ForeColor="Red" Font-Bold="True"
													Font-Size="Medium" Font-Names="Arial"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
												&nbsp;&nbsp;&nbsp;
											</td>
											<td>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td bgColor="#ffffff" colSpan="2" height="10">
									<div align="center"></div>
								</td>
							</tr>
							<tr>
								<td class="05_con_small" align="center" colSpan="2"><asp:label id="lblErrMsg" runat="server" Width="643px" ForeColor="Red" Font-Bold="True" Font-Size="Medium"
										Font-Names="Arial" Height="10px"></asp:label><BR>
								</td>
							</tr>
							<tr>
								<td vAlign="top" colSpan="2" height="100%">
									<TABLE id="Table1" style="WIDTH: 656px; HEIGHT: 34px" cellSpacing="1" cellPadding="1" width="656"
										border="0">
										<TR>
											<TD align="left"><asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg"></asp:imagebutton></TD>
											<TD align="right"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton></TD>
										</TR>
									</TABLE>
								</td>
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
