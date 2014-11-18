<%@ Page language="c#" Codebehind="DCSummary.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Main.DCSummary" smartNavigation="False"%>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect - Debit Card Summary</title>
		<meta content="False" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">		
		<script language="JavaScript">
		<!---
			var clickedButton = false;
			function check() 
			{
				//--->alert("DCSummary.Check");
				return clickedButton;
			}
		//--->
		</script>
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0"
		ms_positioning="GridLayout">
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
								<td colSpan="2"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
							</tr>
							<tr>
								<td class="05_con_label" width="660" colSpan="2" height="24"><asp:image id="imgWorkflow" runat="server" ImageUrl="images/subtable_header_purpose.jpg"></asp:image></td>
							</tr>
							<tr>
								<td class="05_con_sublabel_zip" style="HEIGHT: 50px" vAlign="middle" align="left" background="images/subtable_header_blank.jpg"
									bgColor="white" colSpan="5" height="50">&nbsp;&nbsp; 
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="Label2" runat="server" ForeColor="Chocolate" Font-Names="Arial" Width="176px"
										Font-Size="Small" Font-Bold="True" Height="22px">Purchase Summary</asp:label>
								</td>
							</tr>
							<tr>
								<td align="center" colSpan="2">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td align="center">
												<table cellSpacing="0" cellPadding="0" width="642" border="0">
													<tr>
														<td style="WIDTH: 72px; HEIGHT: 42px" align="right" bgColor="whitesmoke" colSpan="1"
															rowSpan="1"><asp:checkbox id="ckValidID" runat="server" Width="53px"></asp:checkbox></td>
														<td style="HEIGHT: 80px" vAlign="middle" bgColor="whitesmoke"><asp:label id="Label14" runat="server" ForeColor="Black" Width="518px">The customer has presented a Drivers License, State ID, Military ID or U.S. Passport. The Document has not expired, the picture matches the person and the person is 18 years or older. (19 years or older in Alabama and Nebraska)</asp:label></td>
														<TD style="WIDTH: 40px; HEIGHT: 38px" bgColor="whitesmoke"></TD>
													</tr>
													<tr>
														<td colSpan="3"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
													<tr>
														<td style="HEIGHT: 14px" align="center" bgColor="aliceblue" colSpan="3" rowSpan="1"><asp:label id="Label17" runat="server" ForeColor="Brown" Font-Names="Arial" Width="579px" Font-Size="X-Small"
																Font-Bold="True" Visible="False">Is the customer converting from an existing prepaid debit card to the Purpose Prepaid MasterCard?</asp:label></td>
													</tr>
													<tr>
														<td style="WIDTH: 72px; HEIGHT: 38px" align="right" bgColor="#f0f8ff" colSpan="1" rowSpan="1"><asp:regularexpressionvalidator id="RegularExpressionValidator3" runat="server" Font-Size="Small" Font-Bold="True"
																ValidationExpression="\d{4}" ErrorMessage="Please enter four digits for your card number." ControlToValidate="txtCardNum">*</asp:regularexpressionvalidator><asp:radiobutton id="rbYes" runat="server" Visible="False" AutoPostBack="True" GroupName="rbYesNo"></asp:radiobutton></td>
														<TD style="WIDTH: 520px; HEIGHT: 38px" bgColor="#f0f8ff"><asp:label id="Label15" runat="server" ForeColor="Black" Width="516px" Visible="False"> <font color="red">YES</font>, please enter the first 4 numbers from the customer's existing prepaid debit card.</asp:label></TD>
														<TD style="WIDTH: 50px; HEIGHT: 38px" bgColor="#f0f8ff"><asp:textbox id="txtCardNum" runat="server" Width="42px" Font-Size="X-Small" Visible="False"
																MaxLength="4"></asp:textbox></TD>
													</tr>
													<TR>
														<td style="WIDTH: 72px; HEIGHT: 35px" align="right" bgColor="#f0f8ff" colSpan="1" rowSpan="1"><asp:radiobutton id="rbNo" runat="server" Visible="False" AutoPostBack="True" GroupName="rbYesNo"
																Checked="True"></asp:radiobutton></td>
														<td style="WIDTH: 499px; HEIGHT: 35px" bgColor="#f0f8ff"><asp:label id="Label16" runat="server" ForeColor="Black" Width="59px" Visible="False">
																<font color="red">NO</font></asp:label></td>
														<TD style="WIDTH: 40px; HEIGHT: 38px" bgColor="#f0f8ff"></TD>
													</TR>
												</table>
												<table cellSpacing="0" cellPadding="0" width="97%" border="0">
													<tr>
														<td style="WIDTH: 501px" align="left" bgColor="chocolate" colSpan="1" rowSpan="1">&nbsp;
															<asp:label id="Label1" runat="server" ForeColor="White" Font-Names="Arial">Payment Information</asp:label></td>
														<td bgColor="chocolate">&nbsp;</td>
													</tr>
													<tr>
														<td style="WIDTH: 501px" bgColor="#f0f8ff">&nbsp;&nbsp;
															<asp:label id="Label6" runat="server" ForeColor="Black" Font-Names="Arial" Width="317px">Enrollment Fee</asp:label></td>
														<td align="right" bgColor="#f0f8ff"><asp:label id="lblEnrollFee" runat="server" ForeColor="Black" Font-Names="Arial"></asp:label>&nbsp;&nbsp;&nbsp;</td>
													</tr>
													<tr>
														<td colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
													<tr>
														<td style="WIDTH: 501px; HEIGHT: 27px" bgColor="floralwhite">&nbsp;&nbsp;
															<asp:label id="Label7" runat="server" ForeColor="Black" Font-Names="Arial" Width="160px">Initial Load Amount</asp:label></td>
														<td style="HEIGHT: 27px" align="right" bgColor="floralwhite"><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" Width="12px" ErrorMessage="Please enter Load Amount"
																ControlToValidate="txtInitLoadAmnt">*</asp:requiredfieldvalidator><asp:textbox id="txtInitLoadAmnt" runat="server" Width="61px" AutoPostBack="True"></asp:textbox>&nbsp;&nbsp;</td>
													</tr>
													<tr>
														<td colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
													<tr>
														<td style="WIDTH: 501px" align="left" bgColor="#f0f8ff">&nbsp;&nbsp;<asp:label id="Label9" runat="server" ForeColor="Chocolate" Font-Names="Arial" Width="153px"
																Font-Size="Small" Font-Bold="True">Total Amount Due:</asp:label>&nbsp;</td>
														<td align="right" bgColor="#f0f8ff"><asp:label id="txtOrderTotal" runat="server" ForeColor="Red" Font-Names="Arial" Width="47px"
																Font-Size="Medium" Font-Bold="True"></asp:label>&nbsp;&nbsp;&nbsp;</td>
													</tr>
													<tr>
														<td colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
													<tr>
														<td style="HEIGHT: 17px" bgColor="floralwhite">&nbsp;&nbsp;
															<asp:label id="Label12" runat="server" Font-Names="Arial" Width="117px">Payment Method  </asp:label></td>
														<td style="HEIGHT: 17px" align="right" bgColor="floralwhite"><asp:dropdownlist id="ddlPayType" runat="server" Width="105px">
																<asp:ListItem Value="0">Cash</asp:ListItem>
																<asp:ListItem Value="1">Credit Card</asp:ListItem>
																<asp:ListItem Value="2">Debit Card</asp:ListItem>
																<asp:ListItem Value="3">Check</asp:ListItem>
																<asp:ListItem Value="4">Money Order</asp:ListItem>
															</asp:dropdownlist>&nbsp;&nbsp;
														</td>
													</tr>
													<tr>
														<td colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
													<tr>
														<td bgColor="#f0f8ff">&nbsp;&nbsp;<asp:label id="Label11" runat="server" Font-Names="Arial" Width="124px"> Amount Collected</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="Label13" runat="server" Font-Names="Arial" Font-Size="Smaller">*Payment is non refundable</asp:label>
														</td>
														<td align="right" bgColor="#f0f8ff"><asp:regularexpressionvalidator id="RegularExpressionValidator5" runat="server" ValidationExpression="^\$?[\d,]*\.?\d{0,2}$"
																ErrorMessage="Invalid Total Amount Paid" ControlToValidate="txtAmountCollected">*</asp:regularexpressionvalidator><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" Width="13px" ErrorMessage="Please enter Total Amount Paid"
																ControlToValidate="txtAmountCollected">*</asp:requiredfieldvalidator><asp:textbox id="txtAmountCollected" runat="server" Width="105px"></asp:textbox>&nbsp;&nbsp;
														</td>
													</tr>
													<tr>
														<td colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
													<tr>
														<td></td>
														<td align="right"><asp:imagebutton id="btnChangeDue" runat="server" ImageUrl="images/btn_changedue.jpg"></asp:imagebutton>&nbsp;
														</td>
													</tr>
													<tr>
														<td></td>
														<td align="right"><asp:label id="lblChange" runat="server" ForeColor="DarkGreen" Font-Names="Arial" Font-Size="Medium"
																Font-Bold="True"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;
														</td>
													</tr>
													<tr>
														<td colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td></td>
										</tr>
									</table>
									<P align="right">&nbsp;&nbsp;</P>
									<P align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</P>
								</td>
							</tr>
							<tr>
								<td align="left">&nbsp;&nbsp;&nbsp;
									<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg" CausesValidation="False"></asp:imagebutton></td>
								<td align="right"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton>&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td class="05_con_bold" align="center" colSpan="2" height="24">
									<P>&nbsp;</P>
									<P><asp:validationsummary id="ValidationSummary2" runat="server" Width="490px" Height="37px"></asp:validationsummary></P>
									<asp:placeholder id="phError" runat="server"></asp:placeholder></td>
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
