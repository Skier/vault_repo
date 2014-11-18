<%@ Page language="c#" Codebehind="AgentHotline.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.AgentHotline" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML xmlns:o xmlns:st1>
	<HEAD>
		<title>Agent Hotline - dPi Teleconnect</title>
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
																		border="0"><asp:label id="lblFeature" runat="server" Width="235px" CssClass="subheader_feature" Height="27px"> Agent Hotline</asp:label>&nbsp;
																		<asp:image id="imgDivide" runat="server" ImageUrl="images/subtable_divide.jpg" Height="100%"></asp:image>&nbsp;</td>
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
															<table cellSpacing="0" cellPadding="20" border="0" width="100%">
																<tr>
																	<td colspan="2" class="faqs">
																		<P><B><I><SPAN><FONT size="5">Call the dPi Agent Hotline at<SPAN>&nbsp;&nbsp; 
                              </SPAN>1-800-383-9956</FONT>
																					</SPAN></I></B></P>
																		<P>
																			<br>
																			Hours of Operations: M – F: 8:00 am – 7:00 pm CST
																			<br>
																			Sat: 8:00 am – 3:00 pm CST
																		</P>
																		<P>
																			<br>
																			<I><U>
																					<SPAN>Field Sales and Support Team</SPAN></U></I>
																		</P>
																	</td>
																</tr>
																<tr>
																	<td width="100%" colSpan="2" class="faqs"><STRONG> Stephen Alexander - </STRONG>Agent 
																		Relations Hotline Supervisor<br>
																		<A href="mailto:salexander@dpiteleconnect.com">salexander@dpiteleconnect.com<br>
																		</A>Office 972-488-5500 ext. 4039<br>
																		Fax 972-243-6122<br>
																	</td>
																</tr>
																<tr>
																	<td colspan="2" class="faqs"><STRONG> LaTasha Myrick - </STRONG>Agent Relations 
																		Team Member<br>
																		<A href="mailto:lmyrick@dpiteleconnect.com">lmyrick@dpiteleconnect.com<br>
																		</A>Office 972-488-5500 ext. 4038<br>
																		Fax 972-243-6122<br>
																	</td>
																</tr>
																<TR>
																	<TD class="faqs" colSpan="2"><STRONG>Rafael Tamayo - </STRONG>Agent Relations Team 
																		Member<BR>
																		<A href="mailto:tamayora@dpiteleconnect.com">tamayora@dpiteleconnect.com<BR>
																		</A>Office 972-488-5500 ext. 5823<BR>
																		Fax 972-243-6122</TD>
																</TR>
																<TR>
																	<TD class="faqs" colSpan="2"><STRONG>Jason Thompson - </STRONG>Agent Relations Team 
																		Member<BR>
																		<A href="mailto:thompsonja@dpiteleconnect.com">thompsonja@dpiteleconnect.com<BR>
																		</A>Office 972-488-5500 ext. 4029<BR>
																		Fax 972-243-6122</TD>
																</TR>
																<tr>
																	<td colspan="2"><STRONG>Call your Representative to satisfy your store training needs 
																			and to order Point Of Sale Materials.</STRONG>
																	</td>
																</tr>
															</table>
															<table cellSpacing="0" cellPadding="20" border="0">
																<tr>
																	<td width="100%" colSpan="2" class="faqs">
																		<STRONG>What is the dPi TeleConnect program?</STRONG><br>
																		The dPi TeleConnect program is designed to meet the needs of a variety of 
																		people, including college students, those in temporary residences, or those who 
																		need a second chance to have phone service.
																		<br>
																		<br>
																		What does dPi TeleConnect provide to the customer?
																		<br>
																		<br>
																		Unlimited "local" phone service
																		<ul>
																			<li>
																			911 service
																			<li>
																			Convenient residential service (No business lines)
																			<li>
																			Service regardless of payment history
																			<li>
																			Fixed monthly pricing
																			<li>
																			No credit check
																			<li>
																			No deposit
																			<li>
																				No personal ID required</li></ul>
																	</td>
																</tr>
																<tr>
																	<td colspan="2" class="faqs"><STRONG>What to tell your Customer</STRONG>
																		<br>
																		dPi TeleConnect provides only residential dial tone service.<br>
																		<br>
																		The customer must reside in an established dPi Service area. Currently, dPi can 
																		provide service to customers in areas that are supported by:</td>
																</tr>
																<tr>
																	<td class="faqs">
																		<ul>
																			<li>
																			Bell South
																			<li>
																			Valor
																			<li>
																			Sprint
																			<li>
																			Century Tel
																			<li>
																			SBC - SNET
																			<li>
																				ALLTEL
																				<ul>
																				</ul>
																			</li>
																		</ul>
																	</td>
																	<td class="faqs">
																		<ul>
																			<li>
																			SBC - Ameritech
																			<li>
																			SBC - Pacific Bell
																			<li>
																			Verizon - GTE
																			<li>
																			Verizon - Bell Atlantic
																			<li>
																			Qwest - US West
																			<li>
																				SBC - Southwestern Bell</li>
																		</ul>
																	</td>
																</tr>
																<tr>
																	<td colspan="2" class="faqs">
																		<P>It will take&nbsp;<STRONG>about a week</STRONG> to activate dPi TeleConnect 
																			service, with exception of Sprint and Verizon (GTE), which may take <STRONG>5-14</STRONG>
																			days. Coverage varies by state. It is recommended to contact the <STRONG>dPi Agent 
																				Hotline 1-800-383-9956</STRONG> to find out which carriers dPi has 
																			agreements with in your state.
																			<br>
																			<br>
																			<b>What to ask your Customer</b><br>
																			It is important to ask the customer if they reside in an area where one of the 
																			previously listed phone companies is the primary carrier. Also ask the customer <STRONG>
																				if they have had service at this address before, and if that location is still 
																				wired for phone service</STRONG> . If the residence is not wired for phone 
																			service the customer will have to contract an outside vendor, at their own 
																			charge, to have the home wired.
																			<br>
																			<br>
																			<STRONG>Overview of dPi TeleConnect Sign-up Process<br>
																			</STRONG>The steps for setting up a customer with new dPi TeleConnect Phone 
																			Service are as follows:
																		</P>
																		<P>
																			<br>
																			Employee properly explains dPi TeleConnect program to inquiring customer and 
																			closes sale.
																			<br>
																			<br>
																			Employee enters the customer’s zip code and selects products and services that 
																			the customer is ordering. Payment data is entered into the dPi online Web 
																			ordering payment function.
																		</P>
																		<P>Customer pays employee for initial month's service.
																		</P>
																		<P>Employee gives customer a dPi New order or New Payment Receipt.
																		</P>
																		<P>For New Payment transactions the Customer calls dPi TeleConnect at <STRONG>1-800-646-2111</STRONG>
																			to activate account. The dPi Customer Service Representative then collects the 
																			following information from the customer:
																		</P>
																		<ul>
																			<li>
																				<STRONG>The name the customer would like on the account </STRONG>
																			<li>
																				<STRONG>The address to have service connected </STRONG>
																			<li>
																				<STRONG>Requested Call Features and/or Packaged Services</STRONG></li>
																		</ul>
																		<P>For New order transactions all the account information and product information 
																			is entered directly into the dPi Web ordering system and does not require the 
																			customer to contact dPi Teleconnect.
																		</P>
																		<P>Customer is then instructed by the dPi Representative to call <STRONG>1-800-350-4009</STRONG>
																			within <STRONG>2-3</STRONG> business days to find out the status of their 
																			order. This number is an <STRONG>"Interactive Voice Response - (IVR)"</STRONG> system 
																			that allows the customer to get updates on their account.
																		</P>
																		<P><STRONG> When calling the IVR line within the initial 2-3 business days, the 
																				customer must have their account and confirmation numbers available from the 
																				New order or New Payment receipt.
																				<br>
																				<br>
																				When contacting the IVR line anytime thereafter, the customer must enter their 
																				phone number to access the system for updates on their dPi account.
																				<br>
																			</STRONG>
																			<br>
																			Customer's phone service will be activated by dPi TeleConnect Pre-paid Phone 
																			Service within <STRONG>5-7</STRONG> days, with exception of Sprint and Verizon 
																			(GTE), which may take <STRONG>5-14</STRONG> days.
																			<br>
																			<br>
																			Beginning in month <STRONG>2</STRONG> , the customer must come in each month to 
																			the location where they made their original payment to make their regular 
																			monthly payment for dPi service. Their new phone number will then be used as 
																			their account number for each recurring payment.
																			<br>
																			<br>
																			<STRONG>Pricing: dPi Rate Information</STRONG>
																			<br>
																			It's important that each store employee knows their store's dPi rate 
																			information. Rates may vary from store to store depending on taxes, fees, 
																			surcharges, and the phone service company in that area. Listed below are the 
																			ways in which you can find out your store's rates for dPi Teleconnect service:
																		</P>
																		<ul>
																			<li>
																				<STRONG>Go to the Product/Pricing Lookup tab and enter the customer’s zip code, and 
																					previous phone company, if required and view all available products and prices </STRONG>
																			<li>
																				<STRONG>Contacting the dPi Agent Hotline at 1-800-383-9956</STRONG></li>
																		</ul>
																		<STRONG>Pricing: Initial Month Payment (a.k.a. "Start-up Charge")</STRONG>
																		<br>
																		In most store locations a customer's initial month payment is only <STRONG>$79.95</STRONG>
																		including taxes, fees and surcharges. The entire initial payment must be paid 
																		"up-front" to get started with dPi "basic" local service. If customer choices 
																		other dPi products the initial rate will vary based on the customers choices.
																		<br>
																		<br>
																		<STRONG>Pricing: Regular Monthly Payments ("Prompt Pay Discount")</STRONG>
																		<br>
																		Regular monthly service rates begin in month <STRONG>2</STRONG> and may differ 
																		from the advertised rates and may also vary by state and phone company. 
																		Customers must pay on or before their due date to receive a <STRONG>Prompt Pay 
																			Discount of $10.00</STRONG>. If the customer's payment is not made on 
																		time,&nbsp;in full, the Prompt Pay Discount will not be credited. dPi sends 
																		each dPi TeleConnect customer a <STRONG>Customer Reminder Notice</STRONG> approximately
																		<STRONG>14</STRONG> days prior to their scheduled monthly payment with a 
																		reminder of this benefit.
																		<br>
																		<br>
																		<STRONG>dPi Payment Reversals/Voids</STRONG>
																		<br>
																		The reversal process for dPi transactions is as follows:
																		<ul>
																			<li>
																				If a dPi transaction must be reversed for any reason the same day it is 
																				received, the store employee must contact either their internal contact or the 
																				dPi's Agent Hotline <STRONG>1-800-383-9956</STRONG>
																			to conduct the reversal/void.
																			<li>
																				If a dPi transaction must be reversed after the day of transaction, it must 
																				only be for a customer refund. Direct the customer to call dPi's Customer 
																				Service telephone number: <STRONG>1-800-350-4009</STRONG> for a refund.</li>
																		</ul>
																		<STRONG>Past Due Payments</STRONG>
																		<br>
																		If a dPi TeleConnect Customer fails to make an on time payment, dPi will notify 
																		them by phone. These calls and notices will be directed to the customer's home, 
																		and will disclose the "Amount Past Due" and "Due Date" necessary to prevent 
																		possible "disconnection" of service. Customers must be aware, that by missing 
																		their scheduled due date, their Prompt Pay Discount for that payment will not 
																		be credited. If a customer inquires about their "past due account", the amount 
																		can be found under the Customer Inquiry tab or the employee can direct the 
																		customer to contact the 800 number that is listed on their <STRONG>Customer 
																			Reminder Notice (1-800-350-4009).</STRONG>
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
