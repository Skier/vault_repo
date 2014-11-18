<%@ Page language="c#" Codebehind="LearnMore.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.LearnMore" %>
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
	<body text="#000000" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
<TABLE height=803 cellSpacing=0 cellPadding=0 width=770 border=0 
ms_2d_layout="TRUE">
  <TR vAlign=top>
    <TD width=770 height=803>
					<form id="Form1" onsubmit="return check();" method="post" runat="server">
      <TABLE height=768 cellSpacing=0 cellPadding=0 width=801 border=0 
      ms_2d_layout="TRUE">
        <TR vAlign=top>
          <TD width=801 height=768>
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
																		border="0"><asp:label id="lblFeature" runat="server" Width="235px" CssClass="subheader_feature" Height="27px"> Learn More</asp:label>&nbsp;
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
																	<TABLE id=Table1 cellSpacing=0 cellPadding=0 border=0 class="learnmore_Table">
																		<TR>
																			<TD vAlign=top width=590 bgcolor="#eda270">
																				<P class=faqs><B>BASIC SERVICE AND BASIC 
																				SERVICE PACKAGES</B></P>
																			</TD>
																		</TR>
																		<TR>
																			<TD vAlign=top width=590 bgcolor="#dfdfdf">
																				<P class=faqs><b>Basic Service</b></P>
																			</TD>
																		</TR>
																		<TR>
																			<TD vAlign=top width=590>
																				<P class=faqs><B>Includes:</B></P>
																				<UL type=disc>
																				<LI class=faqs>Local Dial 
                                Tone 
                                
																				<LI class=faqs>Unlimited Local 
																				Calling</LI>
																				</UL>
																			</TD>
																		</TR>
																		<TR>
																		<TD vAlign=top width=590 bgcolor="#dfdfdf">
																		<P class=faqs><b>Great American Hook-Up 
																		Advantage (Availability based on customer 
																		location)</b></P></TD></TR>
																		<TR>
																		<TD vAlign=top width=590>
																		<P class=faqs><B>Includes:</B> </P>
																		<UL type=disc>
																		<LI class=faqs>Local Dial 
                                Tone 
                                
																		<LI class=faqs>Unlimited 
                                Local Calling 
                                
																		<LI class=faqs>Call Feature 
                                Package: 
                                
																		<UL type=circle>
																		<LI class=faqs>Call Return 
                                
																		<LI class=faqs>Call 
                                Forwarding 
                                
																		<LI class=faqs>Call Waiting 

                                
																		<LI class=faqs>Caller ID 
                                
																		<LI class=faqs>3-Way Calling</LI></UL>
																		<LI class=faqs>100 Minutes Long Distance 
																		for 5 months*</LI></UL>
																		<P class=faqs>* Select Advantage 100 
																		minutes product that comes with the package at 
																		no cost.<SPAN>&nbsp; </SPAN>If the customer 
																		needs more minutes select any of the Anytime LD 
																		or Unlimited LD products for an additional 
																		charge.</P></TD></TR>
																		<TR>
																		<TD vAlign=top width=590 bgcolor="#dfdfdf">
																		<P class=faqs><b>Great American Hook-Up 
																		Advantage (Availability based on customer 
																		location)</b></P></TD></TR>
																		<TR>
																		<TD vAlign=top width=590>
																		<P class=faqs><B>Includes:</B> </P>
																		<UL type=disc>
																		<LI class=faqs>Local Dial 
                                Tone 
                                
																		<LI class=faqs>Unlimited 
                                Local Calling 
                                
																		<LI class=faqs>Call Feature 
                                Package: 
                                
																		<UL type=circle>
																		<LI class=faqs>Call Waiting 

                                
																		<LI class=faqs>Caller ID</LI></UL>
																		<LI class=faqs>100 Minutes Long Distance 
																		for 5 months*</LI></UL>
																		<P class=faqs><SPAN>&nbsp;</SPAN>* Select 
																		Advantage 100 minutes product that comes with 
																		the package at no cost.<SPAN>&nbsp; 
																		</SPAN><SPAN>&nbsp;</SPAN>If the customer needs 
																		more minutes select any of the Anytime LD or 
																		Unlimited LD products for an additional 
																		charge.</P></TD></TR>
																		<TR>
																		<TD vAlign=top width=590 bgcolor="#dfdfdf">
																		<P class=faqs><b>Great American Hook-Up 
																		Complete<SPAN>&nbsp; </SPAN>(Availability based 
																		on customer location)</b></P></TD></TR>
																		<TR>
																		<TD vAlign=top width=590>
																		<P class=faqs><B>Includes:</B> </P>
																		<UL type=disc>
																		<LI class=faqs>Local Dial 
                                Tone 
                                
																		<LI class=faqs>Unlimited 
                                Local Calling 
                                
																		<LI class=faqs>500 Minutes 
                                Long Distance each month 
                                
																		<LI class=faqs>Call Feature 
                                Package: 
                                
																		<UL type=circle>
																		<LI class=faqs>Call Return 
                                
																		<LI class=faqs>Call 
                                Forwarding 
                                
																		<LI class=faqs>Call Waiting 

                                
																		<LI class=faqs>Caller ID 
                                
																		<LI class=faqs>3-Way 
																		Calling</LI></UL></LI></UL></TD></TR>
																		<TR>
																		<TD vAlign=top width=590 bgcolor="#dfdfdf">
																		<P class=faqs><b>Great American Hook-Up 
																		Premier<SPAN>&nbsp; </SPAN>(Availability based 
																		on customer location)</b></P></TD></TR>
																		<TR>
																		<TD vAlign=top width=590>
																		<P class=faqs><B>Includes:</B> </P>
																		<UL type=disc>
																		<LI class=faqs>Local Dial 
                                Tone 
                                
																		<LI class=faqs>Unlimited 
                                Local Calling 
                                
																		<LI class=faqs>100 Minutes 
                                Long Distance for 5 months 
                                
																		<LI class=faqs>Call Feature 
                                Package: 
                                
																		<UL type=circle>
																		<LI class=faqs>Call Return 
                                
																		<LI class=faqs>Call 
                                Forwarding 
                                
																		<LI class=faqs>Call Waiting 

                                
																		<LI class=faqs>Caller ID 
                                
																		<LI class=faqs>3-Way 
                                Calling 
                                
																		<LI class=faqs>Also Busy Redial in all but 
																		BellSouth areas.</LI></UL></LI></UL></TD></TR>
																		<TR>
																		<TD vAlign=top width=590>
																		<P class=faqs>Pricing and taxes vary based 
																		on exact service area.</P>
																		<P class=faqs>&nbsp;
										<o:p></o:p></P>
																		<P class=faqs>Customer is also entitled to 
																		a $10 Prompt Pay discount each month if your 
																		bill is paid in full on or before the due 
																		date.</P></TD></TR></TABLE>
																	<P class=faqs>&nbsp;
										<o:p></o:p></P>
																	<TABLE id=Table2 cellSpacing=0 cellPadding=0 
																	border=1 class="learnmore_table">
																		<TR>
																		<TD vAlign=top width=590 bgcolor="#eda270">
																		<P class=faqs><B>CALL FEATURES</B></P>
																		</TD>
																		</TR>
																		<TR>
																		<TD vAlign=top width=590>
																		<P class=faqs>Features: </P>
																		<UL type=disc>
																		<LI class=faqs>Busy Redial 
                                
																		<LI class=faqs>Call Return 
                                
																		<LI class=faqs>Caller ID 
                                
																		<LI class=faqs>3 Way 
                                Calling 
                                
																		<LI class=faqs>Call Waiting 

                                
																		<LI class=faqs>Call 
                                Forwarding 
                                
																		<LI class=faqs>Non-Published 
																		Listing</LI></UL>
																		<P class=faqs>Call Features sold separately 
																		or part of a 
																		package<BR><SPAN>&nbsp;</SPAN></P>
																		</TD>
																		</TR>
																	</TABLE>
																	<br><br>
																	<TABLE id=Table3 cellSpacing=0 cellPadding=0 
																	border=0 class="learnmore_table">
																		<TR>
																		<TD vAlign=top width=590 colSpan=2 bgcolor="#eda270">
																		<P class=faqs><B>PACKAGED SERVICES</B></P>
																		</TD>
																		</TR>
																		<TR>
																		<TD vAlign=top width=295>
																		<P class=faqs><b>Call Feature Saver 
																		Package</b></P></TD>
																		<TD vAlign=top width=295>
																		<P class=faqs><B>Features:</B> </P>
																		<UL type=disc>
																		<LI class=faqs>Call Waiting 

                                
																		<LI class=faqs>Caller ID</LI>
																		</UL>
																		</TD>
																		</TR>
																		<tr>
																			<td colspan="2"><img src="images/pixel_gray.jpg" border="0" width="100%" height="1"></td>
																		</tr>
																		<TR>
																		<TD vAlign=top width=295>
																		<P class=faqs><b>Call Feature Super Value</b><BR><BR></P></TD>
																		<TD vAlign=top width=295>
																		<P class=faqs><B>Features:</B> </P>
																		<UL type=disc>
																		<LI class=faqs>Call 
                                Forwarding 
                                
																		<LI class=faqs>Caller ID 
                                
																		<LI class=faqs>3-Way 
                                Calling 
                                
																		<LI class=faqs>Call 
																		Waiting</LI></UL>
																		</TD>
																		</TR>
																		<tr>
																			<td colspan="2"><img src="images/pixel_gray.jpg" border="0" width="100%" height="1"></td>
																		</tr>
																		<TR>
																		<TD vAlign=top width=295>
																		<P class=faqs><b>Call Feature Bonus Package</b><BR><BR></P>
																		</TD>
																		<TD vAlign=top width=295>
																		<P class=faqs>&nbsp;&nbsp;&nbsp;<B>Features:</B> 
																		</P>
																		<UL type=disc>
																		<LI class=faqs>Call 
                                Forwarding 
                                
																		<LI class=faqs>Caller ID 
                                
																		<LI class=faqs>3-Way 
                                Calling 
                                
																		<LI class=faqs>Call Return 
                                
																		<LI class=faqs>Call Waiting 

                                
																		<LI class=faqs>Unlimited Local 
																		Calling</LI></UL>
																		</TD>
																		</TR>
																		<tr>
																			<td colspan="2"><img src="images/pixel_gray.jpg" border="0" width="100%" height="1"></td>
																		</tr>
																		<TR>
																		<TD vAlign=top width=295>
																		<P class=faqs><b>Long Distance<BR>(Domestic Only)</b></P></TD>
																		<TD vAlign=top width=295>
																		<P class=faqs><B>Features:</B> </P>
																		<UL type=disc>
																		<LI class=faqs>100 Anytime 
                                Minutes 
                                
																		<LI class=faqs>200 Anytime 
                                Minutes 
                                
																		<LI class=faqs>500 Anytime 
                                Minutes 
                                
																		<LI class=faqs>Unlimited (Domestic 
																		Only)</LI></UL>
																		</TD>
																		</TR>
																		<tr>
																			<td colspan="2"><img src="images/pixel_gray.jpg" border="0" width="100%" height="1"></td>
																		</tr>
																		<TR>
																		<TD vAlign=top width=295>
																		<P class=faqs><b>Long Distance<BR>(International &amp; Domestic)</b></P></TD>
																		<TD vAlign=top width=295>
																		<P class=faqs><B>Features:</B> </P>
																		<UL type=disc>
																		<LI class=faqs>Purchase 
                                Directly From dPi Agent 
                                
																		<LI class=faqs>Purchase $5, 
                                $10, $20 or more 
                                
																		<LI class=faqs>NO CONNECTION FEES</LI>
																		</UL>
																		</TD>
																		</TR>
																		<tr>
																			<td colspan="2"><img src="images/pixel_gray.jpg" border="0" width="100%" height="1"></td>
																		</tr>
																		<TR>
																		<TD vAlign=top width=295>
																		<P class=faqs><b>dPi Club Program</b></P>
																		</TD>
																		<TD vAlign=top width=295>
																		<P class=faqs><B>Features:</B> </P>
																		<UL type=disc>
																		<LI class=faqs>Debit &amp; 
                                Credit Counseling 
                                
																		<LI class=faqs>Grocery 
                                Coupon Savings Book 
                                
																		<LI class=faqs>Involuntary Unemployment Insurance</LI></UL>
																		</TD>
																		</TR>
																		<tr>
																			<td colspan="2"><img src="images/pixel_gray.jpg" border="0" width="100%" height="1"></td>
																		</tr>
																		<TR>
																		<TD vAlign=top width=295>
																		<P class=faqs><b>dPi Club Program - Gold</b></P></TD>
																		<TD vAlign=top width=295>
																		<P class=faqs><B>Features:</B> </P>
																		<UL type=disc>
																		<LI class=faqs>Debit &amp; 
                                Credit Counseling 
                                
																		<LI class=faqs>Grocery 
                                Coupon Savings Book 
                                
																		<LI class=faqs>Involuntary 
                                Unemployment Insurance 
                                
																		<LI class=faqs>5 Grace Days 

                                
																		<LI class=faqs>Inside Wire 
																		Maintenance</LI></UL>
																		</TD>
																		</TR>
																		<tr>
																			<td colspan="2"><img src="images/pixel_gray.jpg" border="0" width="100%" height="1"></td>
																		</tr>
																		<TR>
																		<TD vAlign=top width=295>
																		<P class=faqs><b>Club Program sold Individually</b></P></TD>
																		<TD vAlign=top width=295>
																		<P 
																		class=faqs>&nbsp;&nbsp;&nbsp;<B>Features:</B> 
																		</P>
																		<UL type=disc>
																		<LI class=faqs>5 Grace Days 

                                
																		<LI class=faqs>Inside Wire 
																		Maintenance</LI></UL>
																		</TD>
																		</TR>
																	</TABLE>
										                               <br><br> 
																	<TABLE id=Table4 cellSpacing=0 cellPadding=0 
																	border=0 class="descriptions_table">
																		<TR>
																			<TD vAlign=top width=590 colSpan=2 bgcolor="#eda270">
																			<P class=faqs><B><SPAN>&nbsp;</SPAN>PRODUCT DESCRIPTIONS</B></P>
																			</TD>
																		</TR>
																		<TR>
																			<TD vAlign=top width=126 bgcolor="#dfdfdf">
																			<P class=faqs><b>Busy Redial</b></P></TD>
																			<TD vAlign=top width=439 bgcolor="#dfdfdf">
																			<P class=faqs>Busy redial automatically 
																			redials the lasts number you dialed. To Activate 
																			– Lift the headset and listen for dial tone and 
																			press *66.<SPAN>&nbsp; </SPAN>To cancel busy 
																			redial press *86.</P>
																			</TD>
																		</TR>
																		<TR>
																			<TD vAlign=top width=126>
																				<P class=faqs><b>Call Forwarding</b></P>
																			</TD>
																			<TD vAlign=top width=439>
																				<P class=faqs><SPAN>Call Forwarding allows 
																				you to transfer all of your calls to another 
																				telephone number.<SPAN>&nbsp; </SPAN>Call 
																				Forwarding must be activated from your home 
																				phone. Lift the handset and listen for dial 
																				tone.<SPAN>&nbsp; </SPAN>Press *72<SPAN>&nbsp; 
																				</SPAN>in some areas you must dial 72# with 
																				touch tone.<SPAN>&nbsp; </SPAN>At the tone, dial 
																				the # calls are to be forwarded to. To 
																				deactivate Call Forwarding Press *73 or 
																				73#<SPAN>&nbsp; </SPAN>and hang 
																				up.</SPAN></P>
																			</TD>
																		</TR>
																		<TR>
																			<TD vAlign=top width=126 bgcolor="#dfdfdf">
																				<P class=faqs><b>Caller ID</b></P>
																			</TD>
																			<TD vAlign=top width=439 bgcolor="#dfdfdf">
																				<P class=faqs>Caller ID allows you to see 
																				the name and number of the person calling 
																				you.<SPAN>&nbsp; </SPAN>dPi does not supply the 
																				Caller ID box.</P>
																			</TD>
																		</TR>
																		<TR>
																		<TD vAlign=top width=126>
																		<P class=faqs><b>Call Return</b></P></TD>
																		<TD vAlign=top width=439>
																		<P class=faqs>Call Return automatically 
																		returns the most recent incoming call, whether 
																		answered or not.<SPAN>&nbsp;&nbsp; </SPAN>Listen 
																		for the dial tone and press *69 to return the 
																		last call received.<SPAN>&nbsp; 
																		</SPAN></P></TD></TR>
																		<TR>
																		<TD vAlign=top width=126 bgcolor="#dfdfdf">
																		<P class=faqs><b>Call Waiting</b></P></TD>
																		<TD vAlign=top width=439 bgcolor="#dfdfdf">
																		<P class=faqs>To use call waiting you will 
																		hear a tone during the call.<SPAN>&nbsp;&nbsp; 
																		</SPAN>Press<SPAN>&nbsp; </SPAN>flash key to 
																		answer the other line and then press flash again 
																		to return to caller.</P></TD></TR>
																		<TR>
																			<TD vAlign=top width=126>
																			<P class=faqs><b>Non-Published Listing</b></P></TD>
																			<TD vAlign=top width=439>
																			<P class=faqs>This feature allows for a 
																			phone number to be withheld from both the 
																			printed phone book as well as being accessible 
																			on 411.</P>
																			</TD>
																		</TR>
																		<TR>
																			<TD vAlign=top width=126 bgcolor="#dfdfdf">
																			<P class=faqs><b>Three Way Calling</b></P></TD>
																			<TD vAlign=top width=439 bgcolor="#dfdfdf">
																			<P class=faqs>Three-way Calling allows you 
																			to add a third person to your 
																			conversation.<SPAN>&nbsp; </SPAN>To activate 
																			press and release the flash key (#)<SPAN>&nbsp; 
																			</SPAN>to place the first caller on hold, listen 
																			for three quick tones, followed by a dial 
																			tone.<SPAN>&nbsp; </SPAN>Dial the number you 
																			want to add to the conversation, when that 
																			person answers press the flash (#) key once and 
																			all three parties will be 
																			connected.</P>
																			</TD>
																		</TR>
																		</TABLE>
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
									</table></TD></TR></TABLE>
					</form></TD></TR></TABLE>
	</body>
</HTML>
