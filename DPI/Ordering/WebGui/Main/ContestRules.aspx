<%@ Page language="c#" Codebehind="ContestRules.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Main.ContestRules" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="DPI.Services" %>
<%@ Import Namespace="DPI.Interfaces" %>
<%@ Import Namespace="DPI.Ordering" %>
<%@ Import Namespace="DPI.ClientComp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Logon</title>
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
						<table cellSpacing="10" cellPadding="0" width="660" align="left" border="0">
							<tr>
								<td width="681" colSpan="2"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
							</tr>
							<tr>
								<td width="681" colSpan="2" height="636">
									<DIV style="WIDTH: 691px; POSITION: relative; HEIGHT: 262px" ms_positioning="GridLayout"><asp:image id="Image1" style="Z-INDEX: 101; LEFT: 528px; POSITION: absolute; TOP: 8px" runat="server"
											ImageUrl="images/rulespagegraphic.jpg"></asp:image><span style="FONT-WEIGHT: bold; FONT-SIZE: 11pt; COLOR: #666; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">Terms 
											&amp; Conditions</span><br>
										<br>
										<strong>Prizes:</strong>
										<ul style="PADDING-RIGHT: 0px; PADDING-LEFT: 24px; PADDING-BOTTOM: 0px; MARGIN: 0px; PADDING-TOP: 4px">
											<li>
												32-Inch Flat-Screen television conducted by a random drawing of all Registered 
												Co-<BR>
											Workers on or about November 15.
											<li>
												$200 for each Store Manager/Owner that signs-up 10 new dPi Teleconnect home
												<br>
											telephone service customers during the month of October.
											<li>
												$15.00 for each new customer after selling the initial 10.
											</li>
										</ul>
										<asp:imagebutton id="ImageButton1" style="Z-INDEX: 102; LEFT: 568px; POSITION: absolute; TOP: 192px"
											runat="server" ImageUrl="images/button_promopage.jpg"></asp:imagebutton><br>
										<strong>Eligibility:</strong>
										<ul style="PADDING-RIGHT: 0px; PADDING-LEFT: 24px; PADDING-BOTTOM: 0px; MARGIN: 0px; PADDING-TOP: 4px">
											<li>
												Only dPi Teleconnect Agent Co-Workers and their employees using WebCentral are
												<br>
											eligible.
											<li>
												All Store Co-Workers that register are eligible for the 32-Inch Flat-Screen 
												television.
												<asp:imagebutton id="ImageButton2" style="Z-INDEX: 103; LEFT: 568px; POSITION: absolute; TOP: 240px"
													runat="server" ImageUrl="images/button_printpage.jpg"></asp:imagebutton>
											<li>
												Only Store Managers OR Store Owners are eligible for cash payouts. In the event 
												that
												<BR>
												a Store Manager AND a Store Owner both work in the same registered location, 
												ONLY<br> the Store Owner will be paid.
											</li>
										</ul>
										<br>
										<strong>Validation:</strong><sub>&nbsp;</sub><br>
										Prior to any cash payout, dPi Teleconnect will confirm that the winning 
										individual is the
										<BR>
										Store Manager or Owner, and will also confirm that the address of the 
										individual is correct.<br>
										<br>
										<strong>Miscellaneous:</strong>
										<ol style="PADDING-RIGHT: 0px; PADDING-LEFT: 32px; PADDING-BOTTOM: 0px; MARGIN: 0px; PADDING-TOP: 4px">
											<li>
											This Incentive runs from October 1, 2005 through October 31, 2005.
											<li>
												Cash payouts will be determined by the number of new dPi Teleconnect new sales 
												as
												<BR>
												of midnight on October 31, 2005 minus any and all refunds and voided payments.
												<BR>
											New Sales will be tracked on Web-Central in the “Quick-Stats” section.
											<li>
											Cash Incentives will not have any taxes taken out.
											<li>
											Cash payouts shall be paid in the form of a check mailed by November 15, 2005.
											<li>
												There will be only 1 cash payout for each qualifying Registered Store Manager 
												or
												<BR>
											Owner.
											<li>
											The televisions will be mailed to the winner by December 1, 2005.
											<li>
												Store Co-Workers may register for the television any time during the month of
												<BR>
											October.
											<li>
											Store Co-Workers may only register for the television one time.
											<li>
												By accepting these terms and conditions, the individual releases dPi 
												Teleconnect from
												<BR>
												any and all liabilities concerning this contest.
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
									</DIV>
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
