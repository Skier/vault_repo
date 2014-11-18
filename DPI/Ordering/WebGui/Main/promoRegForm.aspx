<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Page language="c#" Codebehind="promoRegForm.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.PromoRegForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Incentives</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="main/Styles/Navigator.css" rel="stylesheet">
		<LINK href="main/Styles/DPI.css" rel="stylesheet">
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" onsubmit="return check();" method="post" runat="server">
			<table style="LEFT: 1px; POSITION: absolute; TOP: 0px" height="741" cellSpacing="0" cellPadding="0"
				width="800" align="left" border="0">
				<tr>
					<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
						height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
					<td vAlign="top">
						<table cellSpacing="10" cellPadding="0" width="800" align="right" border="0">
							<tr>
								<td width="681" colSpan="2"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
							</tr>
							<tr vAlign="middle">
								<td width="630" colSpan="2" height="936">
									<p><IMG style="WIDTH: 664px; HEIGHT: 209px" height="209" src="images/promopagegraphic2.jpg"
											width="664" border="0"></p>
									<p align="center">
										<TABLE id="Table2" style="WIDTH: 647px; HEIGHT: 318px" cellSpacing="0" cellPadding="0"
											width="647" border="0">
											<TR>
												<TD style="HEIGHT: 77px" align="center" colSpan="2"><SPAN style="FONT-WEIGHT: bold; FONT-SIZE: 28pt; COLOR: #3c8823; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">WIN 
														ONE OF 10 FRANKLINS!</SPAN></TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 88px" colSpan="2"><SPAN style="FONT-WEIGHT: bold; FONT-SIZE: 11pt; COLOR: #3c8823; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">GREED 
														IS GOOD!<br>
														<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; FONT-FAMILY: Tahoma">
															Feed your greed. dPi is hosting a <span style="FONT-WEIGHT: bold; FONT-SIZE: 13px; COLOR: #3c8823; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
																“Greed Is Good”</span> Incentive to all registered Store Managers or Owners 
															regardless of the Company you work for. </span></SPAN>
												</TD>
											</TR>
											<TR>
												<TD style="WIDTH: 330px; HEIGHT: 268px">
													<P><span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; FONT-FAMILY: Tahoma">That’s 
															right! This is a completely voluntary program that pays out directly to the 
															person In-Charge of the Store. Store Managers across the country, from the 
															Rent-to-Own sector, Check Cashing, Pay-Day Advance, Grocery or any others may 
															participate. You compete against only yourselves.
															<br>
															<BR>
															<span style="FONT-WEIGHT: bold; FONT-SIZE: 13px; COLOR: #3c8823; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
																Incentive is from&nbsp;November 1, 2006 through&nbsp;November 30, 2006.</span>
														</span>
													</P>
													<P><SPAN style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; FONT-FAMILY: Tahoma"><SPAN style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; FONT-FAMILY: Tahoma">Don’t 
																worry if you are not a Store Manager or Owner. dPi has something for you as 
																well. Every Registered
																<BR>
																person has a chance to win in this fantastic opportunity. 10 lucky people will 
																win the $100.00 "Cash is King" prizes. So hurry, Register yourself and you 
																could receive the "Cash is King" prizes.</SPAN></SPAN></P>
												</TD>
												<TD style="WIDTH: 300px; HEIGHT: 268px" background="images/PromoBody2.jpg"></TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 64px" colSpan="2"><span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; FONT-FAMILY: Tahoma"><SPAN style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; FONT-FAMILY: Tahoma">You 
															will get $200 for signing up just 10 new dPi customers. Plus get $15 for every 
															new dPi customer after the first 10 customers. Here is an example pay-out:</SPAN></span></TD>
											</TR>
										</TABLE>
									</p>
									<P><span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; FONT-FAMILY: Tahoma">
											<IMG height="158" src="images/examplepayout.jpg" width="312" border="0">
											<br>
											<br>
											This is a completely voluntary Incentive. Your Incentive check will be mailed 
											to the address that you provide
											<BR>
											within the Registration Form below.<BR>
											<BR>
											So, you have two options...<BR>
										</span><IMG height="204" src="images/option1.jpg" width="266"><IMG height="204" src="images/option2.jpg" width="266"><br>
										&nbsp;&nbsp;&nbsp;</P>
									<DIV style="WIDTH: 634px; POSITION: relative; HEIGHT: 364px" align="center" ms_positioning="GridLayout">
										<P><asp:dropdownlist id="ddlState" style="Z-INDEX: 122; LEFT: 344px; POSITION: absolute; TOP: 248px"
												tabIndex="9" runat="server" Width="64px" Height="20px"></asp:dropdownlist></P>
										<asp:label id="Label1" style="Z-INDEX: 100; LEFT: 88px; POSITION: absolute; TOP: 16px" runat="server"
											Width="11px">Title</asp:label><asp:dropdownlist id="ddlTitle" style="Z-INDEX: 101; LEFT: 128px; POSITION: absolute; TOP: 16px" tabIndex="1"
											runat="server" Width="125px" Height="20px">
											<asp:ListItem Value="Clerk">Sales Associate</asp:ListItem>
											<asp:ListItem Value="Manager">Store Manager</asp:ListItem>
											<asp:ListItem Value="Owner">Owner</asp:ListItem>
											<asp:ListItem Value="Other">Other</asp:ListItem>
										</asp:dropdownlist><asp:label id="Label2" style="Z-INDEX: 103; LEFT: 80px; POSITION: absolute; TOP: 48px" runat="server">Name</asp:label><asp:textbox id="txtFirstName" style="Z-INDEX: 104; LEFT: 128px; POSITION: absolute; TOP: 48px"
											tabIndex="2" runat="server" Width="174px" Height="20px" BorderStyle="Groove" BorderColor="White"></asp:textbox><asp:textbox id="txtLastName" style="Z-INDEX: 105; LEFT: 336px; POSITION: absolute; TOP: 48px"
											tabIndex="3" runat="server" Width="176px" Height="20px" BorderStyle="Groove" BorderColor="White"></asp:textbox><asp:label id="Label4" style="Z-INDEX: 106; LEFT: 128px; POSITION: absolute; TOP: 72px" runat="server"
											Font-Size="XX-Small">First</asp:label><asp:label id="Label3" style="Z-INDEX: 107; LEFT: 336px; POSITION: absolute; TOP: 72px" runat="server"
											Font-Size="XX-Small">Last</asp:label><asp:textbox id="txtContact" style="Z-INDEX: 108; LEFT: 128px; POSITION: absolute; TOP: 88px"
											tabIndex="4" runat="server" Width="384px" Height="20px" BorderStyle="Groove" BorderColor="White"></asp:textbox><asp:label id="Label5" style="Z-INDEX: 109; LEFT: 56px; POSITION: absolute; TOP: 96px" runat="server">Contact #</asp:label><asp:label id="Label6" style="Z-INDEX: 110; LEFT: 128px; POSITION: absolute; TOP: 112px" runat="server"
											Font-Size="XX-Small">Home or Mobile</asp:label><asp:label id="Label8" style="Z-INDEX: 111; LEFT: 80px; POSITION: absolute; TOP: 128px" runat="server">Email</asp:label><asp:textbox id="txtEmail" style="Z-INDEX: 112; LEFT: 128px; POSITION: absolute; TOP: 128px"
											tabIndex="5" runat="server" Width="384px" Height="20px" BorderStyle="Groove" BorderColor="White"></asp:textbox><asp:label id="Label9" style="Z-INDEX: 113; LEFT: 64px; POSITION: absolute; TOP: 160px" runat="server"
											Height="23px">Personal</asp:label><asp:label id="Label10" style="Z-INDEX: 114; LEFT: 24px; POSITION: absolute; TOP: 176px" runat="server">Mailing Address</asp:label><asp:textbox id="txtAddr1" style="Z-INDEX: 115; LEFT: 128px; POSITION: absolute; TOP: 168px"
											tabIndex="6" runat="server" Width="384px" Height="20px" BorderStyle="Groove" BorderColor="White"></asp:textbox><asp:textbox id="txtAddr2" style="Z-INDEX: 116; LEFT: 128px; POSITION: absolute; TOP: 208px"
											tabIndex="7" runat="server" Width="384px" Height="20px" BorderStyle="Groove" BorderColor="White"></asp:textbox><asp:textbox id="txtCity" style="Z-INDEX: 117; LEFT: 128px; POSITION: absolute; TOP: 248px" tabIndex="8"
											runat="server" Width="176px" Height="20px" BorderStyle="Groove" BorderColor="White"></asp:textbox><asp:textbox id="TxtZipCode" style="Z-INDEX: 118; LEFT: 432px; POSITION: absolute; TOP: 248px"
											tabIndex="10" runat="server" Width="81px" Height="20px" BorderStyle="Groove" BorderColor="White"></asp:textbox><asp:label id="Label11" style="Z-INDEX: 119; LEFT: 128px; POSITION: absolute; TOP: 272px" runat="server"
											Font-Size="XX-Small">City,</asp:label><asp:label id="Label12" style="Z-INDEX: 120; LEFT: 336px; POSITION: absolute; TOP: 272px" runat="server"
											Width="31px" Font-Size="XX-Small">State</asp:label><asp:label id="Label13" style="Z-INDEX: 121; LEFT: 432px; POSITION: absolute; TOP: 272px" runat="server"
											Font-Size="XX-Small">Zipcode</asp:label>&nbsp;
										<asp:label id="lblErrMsg" style="Z-INDEX: 102; LEFT: 32px; POSITION: absolute; TOP: 320px"
											runat="server" Width="521px" ForeColor="Red"></asp:label><asp:label id="lblFNameErr" style="Z-INDEX: 123; LEFT: 304px; POSITION: absolute; TOP: 48px"
											runat="server" Width="9px" Font-Size="Medium" ForeColor="Red" Visible="False" Font-Bold="True">*</asp:label><asp:label id="lblLNameErr" style="Z-INDEX: 124; LEFT: 520px; POSITION: absolute; TOP: 48px"
											runat="server" Width="9px" Font-Size="Medium" ForeColor="Red" Visible="False" Font-Bold="True">*</asp:label><asp:label id="lblContactErr" style="Z-INDEX: 126; LEFT: 520px; POSITION: absolute; TOP: 88px"
											runat="server" Width="9px" Font-Size="Medium" ForeColor="Red" Visible="False" Font-Bold="True">*</asp:label><asp:label id="lblAddressErr" style="Z-INDEX: 127; LEFT: 520px; POSITION: absolute; TOP: 168px"
											runat="server" Width="9px" Font-Size="Medium" ForeColor="Red" Visible="False" Font-Bold="True">*</asp:label><asp:label id="lblCityErr" style="Z-INDEX: 128; LEFT: 312px; POSITION: absolute; TOP: 248px"
											runat="server" Width="9px" Font-Size="Medium" ForeColor="Red" Visible="False" Font-Bold="True">*</asp:label><asp:label id="lblStateErr" style="Z-INDEX: 129; LEFT: 408px; POSITION: absolute; TOP: 248px"
											runat="server" Width="9px" Font-Size="Medium" ForeColor="Red" Visible="False" Font-Bold="True">*</asp:label><asp:label id="lblZipcodeErr" style="Z-INDEX: 130; LEFT: 520px; POSITION: absolute; TOP: 248px"
											runat="server" Width="9px" Font-Size="Medium" ForeColor="Red" Visible="False" Font-Bold="True">*</asp:label></DIV>
									<P>
									<P>
									<P>
									<P>
									<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<textarea style="BORDER-RIGHT: #fff 1px solid; BORDER-TOP: #fff 1px solid; FONT: 8pt Arial; BORDER-LEFT: #fff 1px solid; BORDER-BOTTOM: #fff 1px solid"
											name="terms" rows="10" readOnly cols="80">TERMS &amp; CONDITIONS

PRIZES: 10 – $100.00 "Cash is King" dollar bills will be given out by random drawing of all registered agent employees on or about December 15, 2006.
$200 for each store location that signs-up 10 new dPi Teleconnect home telephone service customers during the month of November.
$15.00 for each new customer after selling the initial 10.

ELIGIBILITY: Only dPi Teleconnect agent locations and their employees using WebCentral are eligible.  All agent employees that register are eligible to win the "Cash is King" prizes.  Only store managers OR store owners are eligible for cash payouts.  In the event that a store manager AND a store owner both work in a location, ONLY the store owner will be paid.

VALIDATION: Prior to any cash payout, dPi Teleconnect will confirm that the winner individual is the store manager or owner as well as the correct address.

MISCELLANEUS:
1. The incentive runs from November 1, 2006 through November 30, 2006.
2. Cash payouts will be determined by the number of new dPi Teleconnect new sales as of midnight on November 30, 2006 minus any and all refunds and voided payments.
3. All winners shall be responsible for any and all applicable taxes on winnings.
4. Cash payouts shall be paid in the form of a check mailed by 12/15/06. 
5. There will be only 1 cash payout for each qualifying store location.
6. The "Cash is King" prizes will be mailed/delivered to the winners by 12/15/06.
7. Registration for the "Cash is King" drawing is from 11/1/06 – 11/30/06.
8. Agent employees may only register for the "Cash is King" drawing one time.
9. Any and all winners must be employed in the same agent until at least December 15, 2006.
10. By accepting the terms and conditions, the individual releases dPi Teleconnect from any and all liability concerning this incentive.</textarea></P>
									<DIV style="WIDTH: 584px; POSITION: relative; HEIGHT: 150px" ms_positioning="GridLayout">
										<P><A href="regFormResponse.html"></A>&nbsp;</P>
										<A href="regFormResponse.html"></A>
										<asp:checkbox id="chkAgree" style="Z-INDEX: 100; LEFT: 88px; POSITION: absolute; TOP: 16px" tabIndex="11"
											runat="server" Text="I have read AND agree to the contest's terms and conditions."></asp:checkbox><asp:imagebutton id="btnSubmit" style="Z-INDEX: 101; LEFT: 160px; POSITION: absolute; TOP: 48px"
											tabIndex="12" runat="server" ImageUrl="images/button_submit.jpg"></asp:imagebutton><asp:imagebutton id="btnCancel" style="Z-INDEX: 102; LEFT: 368px; POSITION: absolute; TOP: 48px"
											runat="server" ImageUrl="images/button_cancel.jpg"></asp:imagebutton><asp:label id="lblCheckErr" style="Z-INDEX: 130; LEFT: 64px; POSITION: absolute; TOP: 16px"
											runat="server" Width="9px" Font-Size="Medium" ForeColor="Red" Visible="False" Font-Bold="True">*</asp:label></DIV>
									<FONT face="ARIAL" color="black" size="1">
										<p align="center"><B>Copyright © 2005 dPi TeleConnect, LLC.&nbsp; All Rights Reserved.<BR>
												Please read our&nbsp; <A href="http://www.dpiteleconnect.com/misc/legalinformation.htm">
													Terms and Conditions.<BR>
												</A>| <A href="http://www.dpiteleconnect.com/about_us/contact.htm">Contact Us</A>
												| <A href="mailto:webmaster@dpiteleconnect.com">Webmaster</A> |</B>
										</p>
									</FONT>
								</td>
							</tr>
						</table>
						<table id="Table1" cellSpacing="0" cellPadding="0" width="655" border="0">
							<TR>
								<TD colSpan="5"></TD>
							</TR>
						</table>
						<!------------------ !SALES ID CELL! ----------------------></td>
				</tr>
				<!----------->
				<tr>
					<td class="05_con_label" width="334" height="56"></td>
				</tr>
				<!----------->
				<tr>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
					<td align="right">&nbsp;&nbsp;&nbsp;&nbsp;
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
		</form>
		</TABLE>
	</body>
</HTML>
