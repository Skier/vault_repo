<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Page language="c#" Codebehind="AccountSummary.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.AccountSummary" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NOGetZip</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
		<form id="Form1" onsubmit="return check(); " action="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
				<tr>
					<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
						height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
					<td vAlign="top" width="660">
						<table cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
							<tr>
								<td colSpan="5"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
							</tr>
							<tr>
								<td class="05_con_sublabel_zip" vAlign="middle" align="left" background="images/subtable_header_blank.jpg"
									bgColor="white" colSpan="5" height="61">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label id="Label5" runat="server" Height="22px" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
										ForeColor="Chocolate" Width="336px"></asp:label></td>
							</tr>
							<tr>
								<td vAlign="top" colSpan="5">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TBODY>
											<tr>
												<td style="WIDTH: 28px; HEIGHT: 96px" rowSpan="6">&nbsp;</td>
												<td style="WIDTH: 228px; HEIGHT: 19px">Account Number</td>
												<td style="HEIGHT: 19px"><asp:label id="lblAccNumber" runat="server" Width="152px"></asp:label></td>
												<td style="HEIGHT: 96px" rowSpan="5">&nbsp;</td>
											</tr>
											<tr>
												<td style="WIDTH: 228px">Phone Number:</td>
												<td><asp:label id="lblPhoneNumber" runat="server" Width="200px"></asp:label></td>
											</tr>
											<tr>
												<td style="WIDTH: 228px; HEIGHT: 19px">Customer Name:</td>
												<td style="HEIGHT: 19px"><asp:label id="lblCustomerName" runat="server" Width="392px"></asp:label></td>
											</tr>
											<tr>
												<td style="WIDTH: 228px">Service Address:</td>
												<td><asp:label id="lblAddress" runat="server" Width="392px"></asp:label></td>
											</tr>
											<tr>
												<td style="WIDTH: 228px; HEIGHT: 20px"></td>
												<td style="HEIGHT: 19px"><asp:label id="lblCityStateZip" runat="server" Width="392px"></asp:label>&nbsp;</td>
											</tr>
											<tr>
												<td style="WIDTH: 228px">Status:</td>
												<td><asp:label id="lblStatus" runat="server" Width="392px"></asp:label></td>
											</tr>
											<tr>
												<td style="WIDTH: 28px" rowSpan="5"></td>
												<td class="05_con_bold" style="WIDTH: 228px">Due Date:</td>
												<td class="05_con_bold"><asp:label id="lblDueDate" runat="server" Width="152px"></asp:label></td>
												<td rowSpan="5"></td>
											</tr>
											<tr>
												<td class="05_con_bold" style="WIDTH: 228px"><font color="red">Last day to make payment 
														before disconnect:</font></td>
												<td class="05_con_bold"><font color="red"><asp:label id="lblLastDay" runat="server" Width="144px"></asp:label></font></td>
											</tr>
										</TBODY>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td class="05_con_medium" colSpan="4">&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 23px">&nbsp;</td>
											<td class="header" style="WIDTH: 620px" bgColor="chocolate" colSpan="2">&nbsp;&nbsp;&nbsp; 
												Reminder Notice</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 23px">&nbsp;</td>
											<td style="WIDTH: 621px" align="center" colSpan="2"><asp:imagebutton id="imgPastReminderNotice" runat="server" Height="32px" Width="136px" ImageUrl="images/btn_viewbill.jpg"
													CausesValidation="False"></asp:imagebutton></td>
											<td>&nbsp;</td>
										</tr>
									</table>
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td style="WIDTH: 22px">&nbsp;</td>
											<td class="header" bgColor="chocolate" colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp;Payment 
												Options</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 22px">&nbsp;</td>
											<td style="WIDTH: 288px" align="right" bgColor="whitesmoke"></td>
											<td align="right" bgColor="whitesmoke">&nbsp;&nbsp;&nbsp;&nbsp;</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 22px">&nbsp;</td>
											<td style="WIDTH: 288px" align="right">Balance Forward (from a prior bill period)</td>
											<td align="right"><asp:label id="lblBalForward" runat="server" Width="109px"></asp:label>&nbsp;&nbsp; 
												&nbsp;
											</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 22px">&nbsp;</td>
											<td style="WIDTH: 288px" align="right" bgColor="whitesmoke">Current Charges</td>
											<td align="right" bgColor="whitesmoke"><asp:label id="lblCurrCharges" runat="server" Width="116px"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 22px">&nbsp;</td>
											<td style="WIDTH: 288px" align="right">Amount&nbsp;Due</td>
											<td align="right"><asp:label id="lblLocalAmountDue" runat="server" Width="116px"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 22px; HEIGHT: 20px">&nbsp;</td>
											<td style="WIDTH: 288px; HEIGHT: 20px" align="right">Long Distance Calling Card</td>
											<td style="HEIGHT: 20px" align="right" bgColor="whitesmoke"><asp:dropdownlist id="ddlLdAmount" runat="server" Width="74px" AutoPostBack="True">
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
												</asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
											<td style="HEIGHT: 20px">&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 22px">&nbsp;</td>
											<td style="WIDTH: 288px; HEIGHT: 19px" align="right" bgColor="whitesmoke"><STRONG><asp:label id="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium"
														ForeColor="#C04000" Width="160px">Total Amount Due</asp:label></STRONG></td>
											<td style="HEIGHT: 19px" align="right"><asp:label id="lblTotalAmountDue" runat="server" Width="106px"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 22px; HEIGHT: 22px">&nbsp;</td>
											<td style="WIDTH: 288px; HEIGHT: 22px" align="right"><asp:label id="lblPaymethod" runat="server" Visible="False">Payment Method</asp:label></td>
											<td style="HEIGHT: 22px" align="right"><asp:dropdownlist id="ddlPayMethod" runat="server" AutoPostBack="True">
													<asp:ListItem Value="0" Selected="True">Cash</asp:ListItem>
													<asp:ListItem Value="1">Credit Card</asp:ListItem>
													<asp:ListItem Value="2">Debit Card</asp:ListItem>
													<asp:ListItem Value="3">Check</asp:ListItem>
													<asp:ListItem Value="4">Money Order</asp:ListItem>
												</asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
											<td style="HEIGHT: 22px">&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 22px">&nbsp;</td>
											<td style="WIDTH: 288px" align="left" bgColor="whitesmoke"><asp:label id="lblAmountPaid" runat="server"></asp:label></td>
											<td align="right" vAlign="top"><asp:checkbox id="chkLocalInFull" runat="server" AutoPostBack="True" Text="Pay in full"></asp:checkbox>&nbsp;
												<asp:label id="lblPaidError" runat="server" ForeColor="Red">*</asp:label>&nbsp;
												<asp:textbox id="txtAmountPaid" runat="server" Width="74px" AutoPostBack="False"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 22px">&nbsp;</td>
											<td style="WIDTH: 288px" align="left"><asp:label id="lblTotAmntColl" runat="server"></asp:label></td>
											<td align="right" bgColor="whitesmoke"><asp:image id="imgSignal" runat="server" ImageUrl="images/signal.gif" Visible="False"></asp:image>&nbsp;&nbsp;
												<asp:label id="lblAmtColError" runat="server" ForeColor="Red">*</asp:label>&nbsp;&nbsp;&nbsp;<asp:textbox id="txtAmountTendered" runat="server" Width="74px" AutoPostBack="True"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 22px">&nbsp;</td>
											<td style="WIDTH: 288px" rowSpan="1">&nbsp;</td>
											<td align="right"><asp:label id="txtNonRefund" runat="server" Width="233px" CssClass="05_con_small" Font-Italic="True">Payment is non-refundable.</asp:label><asp:imagebutton id="btnChangeDue" runat="server" ImageUrl="images/btn_changedue.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 22px">&nbsp;</td>
											<td style="WIDTH: 288px" align="right" height="40"></td>
											<td align="right"><asp:label id="lblChangeDueTxt" runat="server" Width="81px">Change due</asp:label>&nbsp;&nbsp;
												<asp:label id="lblChangeDue" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium"
													ForeColor="Red" Width="96px"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
											<td>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td class="05_con_small" align="center"><asp:label id="lblErrMsg" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium"
										ForeColor="Red" Width="643px"></asp:label><BR>
								</td>
							</tr>
							<tr>
								<td vAlign="top" height="100%">
									<!------------------------------------------------------------------------>
									<TABLE id="Table1" style="WIDTH: 655px; HEIGHT: 34px" cellSpacing="1" cellPadding="1" width="655"
										border="0">
										<TR>
											<TD align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg"></asp:imagebutton></TD>
											<TD align="right"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton></TD>
										</TR>
									</TABLE>
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
	</body>
</HTML>
