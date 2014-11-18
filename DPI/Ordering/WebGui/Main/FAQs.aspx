<%@ Page language="c#" Codebehind="FAQs.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.FAQs" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NOGetZip</title>
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
		<TABLE height="803" cellSpacing="0" cellPadding="0" width="770" border="0" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD width="770" height="803">
					<form id="Form1" onsubmit="return check();" method="post" runat="server">
						<TABLE height="768" cellSpacing="0" cellPadding="0" width="801" border="0" ms_2d_layout="TRUE">
							<TR vAlign="top">
								<TD width="801" height="768">
									<table height="767" cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
										<tr>
											<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
										</tr>
										<tr>
											<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
												height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
											<td vAlign="top">
												<table cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
													<tr>
														<td colSpan="2">
															<table cellSpacing="0" cellPadding="0" width="651" border="0">
																<tr>
																	<td colSpan="4"><IMG height="9" alt="" src="images/subtable_r1_c1.jpg" width="660" border="0"></td>
																</tr>
																<tr>
																	<td><IMG height="33" alt="" src="images/subtable_r2_c1.jpg" width="33" border="0"></td>
																	<td vAlign="bottom" align="left" width="270" background="images/subtable_location.jpg"
																		height="33" border="0">&nbsp;
																		<asp:label id="lblLocation" runat="server" Font-Size="Smaller" Width="200px"></asp:label></td>
																	<td vAlign="top" align="right" width="357" background="images/subtable_r2_c2.jpg" height="33"
																		border="0"><asp:label id="lblFeature" runat="server" Width="235px" CssClass="subheader_feature" Height="27px">Frequently Asked Questions</asp:label>&nbsp;
																		<asp:image id="imgDivide" runat="server" ImageUrl="images/subtable_divide.jpg" Height="100%"></asp:image>&nbsp;
																		<asp:imagebutton id="btnLogout" runat="server" ImageUrl="images/subtable_logout.gif" Height="100%"
																			CausesValidation="False"></asp:imagebutton></td>
																	<td><IMG height="33" alt="" src="images/subtable_r2_c3.jpg" width="25" border="0"></td>
																</tr>
																<tr>
																	<td colSpan="4"><IMG height="11" alt="" src="images/subtable_r3_c1.jpg" width="660" border="0"></td>
																</tr>
															</table>
														</td>
													</tr>
													<!----------->
													<tr>
														<td colSpan="2">
															<table cellSpacing="0" cellPadding="20" border="0">
																<tr>
																	<td width="100%" colSpan="2" class="faqs">
																		<P><STRONG> Do I need to provide my own telephone?<br>
																			</STRONG>Yes, dPi does not provide any telephone equipment required for 
																			service.
																			<br>
																			<br>
																			<STRONG>What if my house/apartment does not have phone jacks?<br>
																			</STRONG>dPi does not provide telephones or telephone jacks. It is the 
																			customers responsibility to provide all equipment inside the house/apartment.
																			<br>
																			<br>
																			<STRONG>Can I get this service as an additional line?<br>
																			</STRONG>Yes. It will require an additional activation for that line and dPi 
																			must be aware that you are requesting an additional line. Also, the customer's 
																			home must already be wired to have 2 lines. dPi does not provide in-home 
																			installations.
																			<br>
																			<br>
																			<STRONG>What is considered my local calling area?<br>
																			</STRONG>It is the same area that you had with your previous carrier. This 
																			includes immediate local calling area only. No area plus, or extended local 
																			area.
																			<br>
																			<br>
																			<STRONG>How will I know my phone number?</STRONG>
																			<br>
																			Call <STRONG>1-800-350-4009</STRONG> to receive an activation date and 
																			telephone number. Typically, this information is available within <STRONG>2-3</STRONG>
																			business days after placing the order for new telephone service.
																			<br>
																			<br>
																			<STRONG>Will my telephone number be in the phone book?<br>
																			</STRONG>Yes. It will also be available through 411 (directory assistance)
																			<br>
																			<br>
																			<STRONG>What if I do not want my number published?</STRONG>
																			<br>
																			When activating your new service, you can purchase non-published service as a 
																			call feature. This feature can only be purchased at this time, and it may not 
																			be dropped in the future.
																			<br>
																			<br>
																			<STRONG>Where do I make my monthly payment?<br>
																			</STRONG>Your monthly payment should be made at the original location where you 
																			initially set up your service, or any other authorized payment location. dPi 
																			does not accept payment by mail.
																			<br>
																			<br>
																			<STRONG>Can I receive collect calls?<br>
																			</STRONG>No. dPi's service does not allow for you to make or receive collect 
																			calls.
																			<br>
																			<br>
																			<STRONG>Can I make long distance calls?<br>
																			</STRONG>Yes. Only if you purchase pre-paid long distance phone service or with 
																			the use of a calling card. dPi customers can add dPi long distance minutes when 
																			activating new dPi phone service. dPi has 2 types of Long Distance products:
																		</P>
																		<P>The Domestic Only Anytime Minutes plans can be ordered separately or as part of 
																			a package. To add the Long Distance Anytime Minutes plans after the initial 
																			order, customers must call dPi at <STRONG>1-800-350-4009. </STRONG>
																		</P>
																		<P>
																			The LD Calling Card product allows the customer to make Domestic and 
																			International calls. The LD Calling Card product can be ordered at any time at 
																			the Agent location.
																			<br>
																			<br>
																			<STRONG>Can I use this service for the Internet?<br>
																			</STRONG>Yes. As long as your Internet dial-up is a local call.
																			<br>
																			<br>
																			<STRONG>How should I handle a Customer complaining that their dPi service has been 
																				disconnected?<br>
																			</STRONG>Empathize with the Customer. Explain that dPi makes several attempts 
																			to contact them about their bill. Give them dPi's Customer Service number <STRONG>(1-800-350-4009)</STRONG>
																			and suggest they call dPi with any questions. Make sure to mention that they 
																			are welcome at anytime to signup again. If the customer is still not satisfied, 
																			the employee may also opt to contact the dPi Agent Hotline to find out more 
																			information.
																			<br>
																			<br>
																			<STRONG>How can a customer add dPi Call Features or Packaged Services?</STRONG> 
																			When activating new service, dPi customers can add Call Features and Packaged 
																			Services. After the initial order Call Features and Packaged Services can be 
																			purchased by contacting the dPi Customer Service number at <STRONG>1-800-350-4009.</STRONG>
																			<br>
																			<br>
																			<STRONG>Can I receive calls from anywhere?<br>
																			</STRONG>Yes.
																		</P>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
													<!----------->
													<tr>
														<td>&nbsp;&nbsp;&nbsp;&nbsp;
														</td>
														<td align="right">
															<asp:ImageButton id="ImageButton1" runat="server" ImageUrl="images/btn_gotomain.jpg"></asp:ImageButton>&nbsp;&nbsp;&nbsp;&nbsp;
														</td>
													</tr>
													<tr>
														<td align="center" colSpan="2">
															<P>&nbsp;</P>
															<P>&nbsp;</P>
															<P>&nbsp;</P>
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
