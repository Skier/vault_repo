<%@ Page language="c#" Codebehind="ReloadSummary.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.ReloadSummary" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Web Order</title>
		<script language="JavaScript" type="">	
	var clickedButton = false;
	function check() 
	{
	   return clickedButton;
	}
		// autofocus for cc number
			function countMeCard1(string)
			{ 
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 3) 
					document.Form1.txtAccNo2.focus();
			}
			
			function countMeCard2(string)
			{ 
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 3) 
					document.Form1.txtAccNo3.focus();
			}
			function countMeCard3(string)
			{ 
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 3) 
					document.Form1.txtAccNo4.focus();
			}
			

/*
			function checkMonth(source, arguments)
			{
				var d = new Date();

				arguments.IsValid = false;
				if(document.Form1.ddlYY.value != "-")
				{
					if(document.Form1.ddlYY.value > d.getFullYear())
					{
						if(document.Form1.ddlMM.value != "-")
							arguments.IsValid = true;
					}
					if(document.Form1.ddlMM.value >= (d.getMonth()+1))
						arguments.IsValid = true;
				}			
			}
			
			
			function FocusCursor()
			{
				document.Form1.txtAccNo1.focus();
			}
			*/
		</script>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<form id="Form1" action="post" runat="server">
			<table style="Z-INDEX: 100; LEFT: 1px; POSITION: absolute; TOP: 0px" height="490" cellSpacing="0"
				cellPadding="0" width="800" align="left" border="0">
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
													<asp:label id="Label5" runat="server" Width="184px" ForeColor="Chocolate" Font-Size="Small"
														Font-Names="Arial" Font-Bold="True" Height="22px">Purchase Summary</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												</td>
											</tr>
											<tr>
												<td colSpan="4">
													<table height="5" width="100%" border="0">
														<TR>
															<td width="20">&nbsp;&nbsp;&nbsp;</td>
															<td width="100%" bgColor="chocolate" colSpan="2">&nbsp;<asp:label id="Label4" runat="server" Width="200px" ForeColor="White" Font-Size="Small">Payment Information</asp:label></td>
															<td width="5">&nbsp;&nbsp;&nbsp;</td>
														</TR>
													</table>
												</td>
											</tr>
											<tr>
												<td align="center" colSpan="4">
													<table height="100%" width="100%" border="0">
														<tr>
															<td width="10">&nbsp;</td>
															<td colSpan="2">
																<!-----------------------Order total table ------------------------------------------>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<td vAlign="top" colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																	</TR>
																	<TR bgColor="floralwhite">
																		<TD style="WIDTH: 375px" align="left" height="2">&nbsp;&nbsp;
																			<asp:label id="lblCard" runat="server" Width="360px">Purpose Prepaid MasterCard Number</asp:label></TD>
																		<TD align="right" width="261" height="2"><asp:textbox id="txtAccNo1" onkeyup="countMeCard1(this.value);" runat="server" Width="40px" MaxLength="4"
																				Wrap="False"></asp:textbox>-&nbsp;
																			<asp:textbox id="txtAccNo2" onkeyup="countMeCard2(this.value);" runat="server" Width="40px" MaxLength="4"
																				Wrap="False"></asp:textbox>&nbsp;-&nbsp;
																			<asp:textbox id="txtAccNo3" onkeyup="countMeCard3(this.value);" runat="server" Width="40px" MaxLength="4"
																				Wrap="False"></asp:textbox>&nbsp;-
																			<asp:textbox id="txtAccNo4" runat="server" Width="40px" MaxLength="4" Wrap="False"></asp:textbox>&nbsp;&nbsp;
																		</TD>
																	</TR>
																	<tr>
																		<td vAlign="top" colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																	</tr>
																	<TR bgColor="aliceblue">
																		<TD style="WIDTH: 375px; HEIGHT: 16px" align="left" height="16">&nbsp;&nbsp;
																			<asp:label id="Label8" runat="server"> Purpose Card Expiration Date</asp:label></TD>
																		<TD style="WIDTH: 261px; HEIGHT: 16px" align="right" width="261" height="16"><asp:dropdownlist id="ddlMM" runat="server"></asp:dropdownlist>&nbsp;/
																			<asp:dropdownlist id="ddlYY" runat="server"></asp:dropdownlist>&nbsp;&nbsp;
																		</TD>
																	</TR>
																	<tr>
																		<td style="HEIGHT: 1px" vAlign="top" colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																	</tr>
																	<TR bgColor="floralwhite">
																		<TD style="WIDTH: 375px; HEIGHT: 18px" align="left" height="18">&nbsp;&nbsp;
																			<asp:label id="Label7" runat="server">Reload Fee</asp:label></TD>
																		<TD style="WIDTH: 261px; HEIGHT: 18px" align="right" width="261" height="18"><asp:label id="lblReloadFee" runat="server"></asp:label>&nbsp;&nbsp; 
																			&nbsp;</TD>
																	</TR>
																	<tr>
																		<td vAlign="top" colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																	</tr>
																	<tr>
																	<TR bgColor="aliceblue">
																		<TD style="WIDTH: 325px" align="left" height="24">&nbsp;&nbsp;
																			<asp:label id="Label1" runat="server">Load Amount</asp:label></TD>
																		<TD style="WIDTH: 261px" align="right" width="261" height="24"><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Load Amount is required"
																				ControlToValidate="txtLoadAmount">*</asp:requiredfieldvalidator><asp:textbox id="txtLoadAmount" runat="server" Width="160px" AutoPostBack="True"></asp:textbox>&nbsp;&nbsp;
																		</TD>
																	</TR>
																	<tr>
																		<td vAlign="top" colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																	</tr>
																	<TR bgColor="floralwhite">
																		<TD style="WIDTH: 325px; HEIGHT: 2px" align="left">&nbsp;&nbsp;
																			<asp:label id="Label2" runat="server" Width="160px" ForeColor="#C04000" Font-Size="Medium"
																				Font-Names="Arial" Font-Bold="True">Total Amount Due</asp:label></TD>
																		<TD style="WIDTH: 261px; HEIGHT: 2px" align="right" width="261"><asp:label id="lblAmountDue" runat="server" Width="90px" ForeColor="Red" Font-Size="Medium"
																				Font-Names="Arial" Font-Bold="True"></asp:label>&nbsp;&nbsp;</TD>
																	</TR>
																	<tr>
																		<td vAlign="top" colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																	</tr>
																	<TR bgColor="aliceblue">
																		<TD style="WIDTH: 325px; HEIGHT: 25px" align="left" height="25">&nbsp;&nbsp;
																			<asp:label id="lblPayMethod" runat="server" Width="112px">Payment Method</asp:label></TD>
																		<TD style="WIDTH: 261px; HEIGHT: 25px" align="right" width="261" height="25">
																			<asp:dropdownlist id="ddlPayMethod" runat="server" Width="160px" Visible="True">
																				<asp:ListItem Value="0" Selected="True">Cash</asp:ListItem>
																				<asp:ListItem Value="1">Credit Card</asp:ListItem>
																				<asp:ListItem Value="2">Debit Card</asp:ListItem>
																				<asp:ListItem Value="3">Check</asp:ListItem>
																				<asp:ListItem Value="4">Money Order</asp:ListItem>
																			</asp:dropdownlist>&nbsp;&nbsp;
																		</TD>
																	</TR>
																	<tr>
																		<td vAlign="top" colSpan="3"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																	</tr>
																	<TR bgColor="floralwhite">
																		<TD style="WIDTH: 325px" align="left">&nbsp;&nbsp;
																			<asp:label id="lblTotAmtColl" runat="server" Width="128px"> Amount Collected</asp:label></TD>
																		<TD style="WIDTH: 261px" align="right" width="261"><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Amount Collected is required"
																				ControlToValidate="txtAmountTendered">*</asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" ErrorMessage="Amount Collected is not in correct format"
																				ControlToValidate="txtAmountTendered" ValidationExpression="^\$?[+-]?[\d,]*\.?\d{0,2}$">*</asp:regularexpressionvalidator><asp:textbox id="txtAmountTendered" runat="server" Width="160px"></asp:textbox>&nbsp;&nbsp;
																		</TD>
																	</TR>
																	<tr>
																		<td vAlign="top" colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
																	</tr>
																	<tr>
																		<td style="WIDTH: 325px" align="left">&nbsp;&nbsp;
																			<asp:label id="txtNonRefund" runat="server" Width="179px" CssClass="05_con_small" Font-Italic="True">Payment is non-refundable.</asp:label></td>
																		<td style="WIDTH: 261px" align="right" width="261"><asp:imagebutton id="btnChangeDue" runat="server" ImageUrl="images/btn_changedue.jpg"></asp:imagebutton>&nbsp;
																		</td>
																	</tr>
																	<TR>
																		<td style="WIDTH: 325px">&nbsp;</td>
																		<td style="WIDTH: 261px" align="right" width="261"><asp:label id="lblChangeDue" runat="server" Width="111px" ForeColor="DarkGreen" Font-Size="Medium"
																				Font-Names="Arial" Font-Bold="True"></asp:label>&nbsp;&nbsp;</td>
																	</TR>
																</TABLE>
																<!--------------------------------------------------------------------------></td>
															<td width="5">&nbsp;&nbsp;&nbsp;</td>
														</tr>
													</table>
													<asp:validationsummary id="ValidationSummary1" runat="server" Width="642px" DisplayMode="List"></asp:validationsummary></td>
											</tr>
											<TR>
												<TD align="center" colSpan="4"><asp:placeholder id="phError" runat="server"></asp:placeholder></TD>
											</TR>
										</table>
									</td>
								</tr>
					</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="4"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0">
					</td>
				</tr>
				<tr>
				<TR>
					<TD align="left" width="607">&nbsp;&nbsp;&nbsp;
						<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg" EnableViewState="False"
							CausesValidation="False"></asp:imagebutton></TD>
					<TD align="right"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton>&nbsp;&nbsp; 
						&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" bgColor="white" colSpan="2">&nbsp;
					</TD>
				</TR>
			</table>
			</TD></TR>
			<TR>
				<TD colSpan="2"><dpiuser:sitefooter id="Sitefooter1" runat="server"></dpiuser:sitefooter></TD>
			</TR>
			<tr>
				<td colSpan="2">&nbsp;
				</td>
			</tr>
			</TBODY></TABLE></form>
		<script language="JavaScript" src="../Core/wz_tooltip.js" type="text/javascript"></script>
	</body>
</HTML>
