<%@ Import Namespace="DPI.ClientComp" %>
<%@ Import Namespace="DPI.Ordering" %>
<%@ Import Namespace="DPI.Interfaces" %>
<%@ Import Namespace="DPI.Services" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Page language="c#" Codebehind="RegFormResponse.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.RegFormResponse" %>
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
								<td width="711" colSpan="2" height="936">
									<DIV style="WIDTH: 691px; POSITION: relative; HEIGHT: 262px" ms_positioning="GridLayout"><asp:image id="Image1" style="Z-INDEX: 100; LEFT: 528px; POSITION: absolute; TOP: 8px" runat="server"
											ImageUrl="images/rulespagegraphic.jpg"></asp:image><span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: #db671c; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">Thank 
											You <span style="COLOR: #000">
												<asp:label id="lblName" style="Z-INDEX: 101; LEFT: 88px; POSITION: absolute; TOP: 0px" runat="server"
													ForeColor="Black" Width="129px"></asp:label></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
											&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
											for Registering for our <span style="COLOR: #3c8823">GREED IS GOOD</span>
											<br>
											promotion!</span><br>
										<br>
										<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
											Please print out this page as proof of registration. Also, refer to this page 
											for details on
											<br>
											how this program works, as well as rules and regulations. The details of your 
											registration
											<br>
											are:<br>
										</span>
										<asp:label id="lblNameTitle" style="Z-INDEX: 102; LEFT: 64px; POSITION: absolute; TOP: 112px"
											runat="server" ForeColor="Gray" Width="360px" Font-Size="Small" Font-Names="Times New Roman"
											Height="24px"></asp:label><asp:label id="lblAddr1" style="Z-INDEX: 103; LEFT: 64px; POSITION: absolute; TOP: 136px" runat="server"
											ForeColor="Gray" Width="360px" Font-Size="Small" Font-Names="Times New Roman" Height="24px"></asp:label><asp:imagebutton id="btnPromo" style="Z-INDEX: 105; LEFT: 520px; POSITION: absolute; TOP: 176px"
											runat="server" ImageUrl="images/button_promopage.jpg" Visible="False"></asp:imagebutton><asp:label id="lblCityStZip" style="Z-INDEX: 106; LEFT: 64px; POSITION: absolute; TOP: 160px"
											runat="server" ForeColor="Gray" Width="360px" Font-Size="Small" Font-Names="Times New Roman" Height="24px"></asp:label><asp:label id="lblPhone" style="Z-INDEX: 109; LEFT: 112px; POSITION: absolute; TOP: 184px"
											runat="server" ForeColor="Gray" Font-Size="Small" Font-Names="Times New Roman"></asp:label><asp:label id="Label1" style="Z-INDEX: 110; LEFT: 64px; POSITION: absolute; TOP: 184px" runat="server"
											ForeColor="Gray" Font-Size="Small" Font-Names="Times New Roman">Phone:</asp:label><asp:label id="lblEmail" style="Z-INDEX: 111; LEFT: 112px; POSITION: absolute; TOP: 200px"
											runat="server" ForeColor="Gray" Font-Size="Small" Font-Names="Times New Roman"></asp:label><asp:label id="Label3" style="Z-INDEX: 112; LEFT: 64px; POSITION: absolute; TOP: 200px" runat="server"
											ForeColor="Gray" Font-Size="Small" Font-Names="Times New Roman">Email:</asp:label><asp:imagebutton id="btnPrint" style="Z-INDEX: 113; LEFT: 520px; POSITION: absolute; TOP: 216px"
											runat="server" ImageUrl="images/button_printpage.jpg"></asp:imagebutton></DIV>
									<span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: #000000; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
										Good Luck and <span style="COLOR: #3c8823">Get Greedy!</span></span><br>
									<hr style="BORDER-RIGHT: #db671c 1px solid; BORDER-TOP: #db671c 1px solid; MARGIN: 6px 0px; BORDER-LEFT: #db671c 1px solid; BORDER-BOTTOM: #db671c 1px solid">
									<span style="FONT-WEIGHT: bold; FONT-SIZE: 12pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
										<strong>Terms &amp; Conditions</strong></span>
									<P><span style="FONT-WEIGHT: lighter; FONT-SIZE: 11pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal"><strong>Prizes:</strong></span>
									</P>
									<ul>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												PRIZES: 10 – $100.00 "Cash is King" dollar bills will be given out by random 
												drawing of all registered agent employees on or about&nbsp;December 15, 2006.</span>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												$200 for each store location that signs-up 10 new dPi Teleconnect home 
												telephone service customers during the month of November.</span>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												$15.00 for each new customer after selling the initial 10. </span>
										</li>
									</ul>
									<span style="FONT-WEIGHT: lighter; FONT-SIZE: 11pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
										<strong>Eligibility:</strong> </span>
									<ul>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												Only dPi Teleconnect agent locations and their employees using WebCentral are 
												eligible. </span>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												All agent employees that register are eligible to win the "Cash is King" 
												prizes.</span>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												Only Store Managers OR Store Owners are eligible for cash payouts. In the event 
												that a Store Manager AND a Store Owner both work in the same registered 
												location, ONLY the Store Owner will be paid. </span>
										</li>
									</ul>
									<span style="FONT-WEIGHT: lighter; FONT-SIZE: 11pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
										<strong>Validation:</strong></span><sub>&nbsp;</sub><br>
									<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
										Prior to any cash payout, dPi Teleconnect will confirm that the winning 
										individual is the Store Manager or Owner, and will also confirm that the 
										address of the individual is correct. </span>
									<br>
									<br>
									<span style="FONT-WEIGHT: lighter; FONT-SIZE: 11pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
										<strong>Miscellaneous:</strong></span>
									<ol>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												The incentive runs from&nbsp;November 1, 2006 through&nbsp;November 30, 2006.</span>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												Cash payouts will be determined by the number of new dPi Teleconnect new sales 
												as of midnight on&nbsp;November 30, 2006 minus any and all refunds and voided 
												payments. New Sales will be tracked on Web-Central in the “Quick-Stats” 
												section. </span>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												All winners shall be responsible for any and all applicable taxes on winnings. </span>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												Cash payouts shall be paid in the form of a check mailed by 12/15/06. </span>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												There will be only 1 cash payout for each qualifying store location. </span>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												The "Cash is King" prizes will be mailed/delivered to the winners by 12/15/06.</span>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												Registration for the "Cash is King" drawing is from 11/1/06 – 11/30/06.</span>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												Agent employees may only register for the "Cash is King" drawing one time.</span>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												Any and all winners must be employed in the same agent until at 
												least&nbsp;December 15, 2006.</span>
										<li>
											<span style="FONT-WEIGHT: lighter; FONT-SIZE: 10pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
												By accepting the terms and conditions, the individual releases dPi Teleconnect 
												from any and all liability concerning this incentive. </span>
										</li>
									</ol>
									<FONT face="ARIAL" color="black" size="1">
										<br>
										<br>
										<p align="center"><B>Copyright © 2004 dPi TeleConnect, LLC.&nbsp; All Rights Reserved.<BR>
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
