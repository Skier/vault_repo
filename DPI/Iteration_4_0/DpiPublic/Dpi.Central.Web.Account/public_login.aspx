<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Page CodeBehind="public_login.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Dpi.Central.Web.PublicLogin" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<meta content="dPi Teleconnect LLC, Pre-Paid Home Phone Service, Pre-Paid Long Distance, Pre-Paid Cell Phones, Pre-Paid MasterCards and Pre-Paid Internet Service."
			name="description">
		<meta content="PrePaid Home Phone, PrePaid Long Distance, PrePaid Cell Phones, PrePaid MasterCards, PrePaid Internet, PrePaid Cable, PrePaid Electric, cellular phone, mobile phone, wireless phone, internet service provider, isp, long distance calling cards, prepaid credit card, prepaid debit card"
			name="keywords">
		<META content="General" name="rating">
		<META content="14 days" name="revisit-after">
		<META content="ALL" name="ROBOTS">
                <script src="script/mainpageflashloader.js" type="text/javascript"></script>
		<script type="text/javascript">
		<!--// first contact number check //-->
					function countMePhone1(string)
					{ 
						if (window.event.keyCode == 9 || window.event.keyCode == 16) 
							return; 
						if (string.length > 2) 
							document.all["txtNxx"].focus();
					} 
					
					function countMePhone2(string)
					{ 
						if (window.event.keyCode == 9 || window.event.keyCode == 16) 
							return; 
						if (string.length > 2) 
							document.all["txtNumber"].focus();
					}		
		</script>
        <style type="text/css">

.package_box_tl { BACKGROUND-IMAGE: url(images/package_bi.gif); MARGIN: 0px; WIDTH: 11px; BACKGROUND-REPEAT: no-repeat; HEIGHT: 14px }

.package_box_top { BACKGROUND-IMAGE: url(images/package_bj.gif); MARGIN: 0px; BACKGROUND-REPEAT: repeat-x; HEIGHT: 14px }

.package_box_tr { BACKGROUND-IMAGE: url(images/package_bk.gif); MARGIN: 0px; WIDTH: 11px; BACKGROUND-REPEAT: no-repeat; HEIGHT: 14px }

.package_box { BORDER-RIGHT: #b5b5b5 1px solid; PADDING-RIGHT: 10px; MARGIN-TOP: 0px; PADDING-LEFT: 15px; PADDING-BOTTOM: 5px; BORDER-LEFT: #b5b5b5 1px solid; PADDING-TOP: 5px }

.defaultPackageText { MARGIN-TOP: 5px; FONT-SIZE: 11px; COLOR: #4d4d4d; LINE-HEIGHT: 15px; FONT-FAMILY: Verdana }

.package_box_bl { BACKGROUND-IMAGE: url(images/package_bm.gif); MARGIN: 0px; WIDTH: 11px; LINE-HEIGHT: 5px; BACKGROUND-REPEAT: no-repeat; HEIGHT: 6px }

.package_box_bottom { BACKGROUND-IMAGE: url(images/package_bn.gif); MARGIN: 0px; WIDTH: 300px; LINE-HEIGHT: 5px; BACKGROUND-REPEAT: repeat-x; HEIGHT: 6px }

.package_box_br { BACKGROUND-IMAGE: url(images/package_bu.gif); MARGIN: 0px; WIDTH: 11px; LINE-HEIGHT: 5px; BACKGROUND-REPEAT: no-repeat; HEIGHT: 6px }

        </style>
		<LINK href="DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>



		<form id="indexForm" method="post" runat="server">
	<div style="position: absolute; width: 600px; height: 100px; z-index: 0; left: 20px; top: 140px" id="layer1">

    <TABLE BORDER="0" CELLSPACING="0" CELLPADDING="0" width="390" align="left">
        <TR>
            <TD CLASS="package_box_tl" WIDTH="11"><IMG SRC="images/spacer01.gif" HEIGHT="1" BORDER="0" WIDTH="11"></TD>
            <TD CLASS="package_box_top" WIDTH="291"><IMG SRC="images/spacer01.gif" HEIGHT="1" BORDER="0" WIDTH="291"></TD>
            <TD CLASS="package_box_tr" WIDTH="11"><IMG SRC="images/spacer01.gif" HEIGHT="1" BORDER="0" WIDTH="11"></TD>
        </TR>
        <TR valign="top">
            <TD COLSPAN="3" CLASS="package_box">
			<TABLE width="300px">
				<TR>
					<TD align="center" colspan="3">
						<SPAN style="FONT-WEIGHT: small">View Your Account, Pay Bills Online</SPAN></TD>
				</TR>
				<TR>
					<TD align="left" colspan="3" height="35"><asp:label runat="server" id="m_lblErrorMessage" ForeColor="Red" style="FONT-WEIGHT: normal"></asp:label></TD>
				</TR>
				<TR>
					<TD width="20%" noWrap><SPAN style="FONT-WEIGHT: normal;font-size: 8pt;">Phone Number</SPAN></TD>
					<TD width="30%" colspan="2">
						<TABLE cellpadding="0" cellspacing="0" border="0" width="100%">
							<TR>
								<TD width="31"><asp:TEXTBOX id="txtNpa" onkeyup="countMePhone1(this.value);" runat="server" width="100%" maxlength="3" CssClass="flattextbox"></asp:TEXTBOX></TD>
								<TD width="12">&nbsp;-&nbsp;</TD>
								<TD width="31"><asp:TEXTBOX id="txtNxx" onkeyup="countMePhone2(this.value);" runat="server" width="100%" maxlength="3" CssClass="flattextbox"></asp:TEXTBOX></TD>
								<TD width="12">&nbsp;-&nbsp;</TD>
								<TD><asp:TEXTBOX id="txtNumber" runat="server" width="40px" maxlength="4" CssClass="flattextbox"></asp:TEXTBOX></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD noWrap><SPAN style="FONT-WEIGHT: normal;font-size: 8pt;">Account Number</SPAN></TD>
					<TD colspan="2"><asp:textbox id="txtAccountNumber" runat="server" Width="126px" CssClass="flattextbox"></asp:textbox></TD>
				</TR>
				<TR>
					<TD noWrap><SPAN style="FONT-WEIGHT: normal;font-size: 8pt;">Password</SPAN></TD>
					<TD colspan="2"><asp:textbox id="txtPassword" runat="server" Width="126px" TextMode="Password" MAXLENGTH="25" CssClass="flattextbox"></asp:textbox></TD>
				</TR>
				<TR>				
				<table width="300">
					<tr>
					<TD nowrap align="left"><asp:HyperLink id="lnkForgotPwd" runat="server" EnableViewState="False" CssClass="small">Forgot My Password</asp:HyperLink><br><asp:HyperLink id="lnkSignUp" runat="server" EnableViewState="False" CssClass="small">Web Access Sign Up</asp:HyperLink></TD>
					<TD align="right">
						<asp:imagebutton id="btnSubmit" runat="server" ImageUrl="images/login_button.png"></asp:imagebutton></TD>						
					</tr>
				<table>				
				</TR>
			</TABLE>

            </TD>
        </TR>
        <TR>
            <TD CLASS="package_box_bl" WIDTH="11"><IMG SRC="images/spacer01.gif" HEIGHT="1" BORDER="0" WIDTH="11"></TD>
            <TD CLASS="package_box_bottom" WIDTH="291"><IMG SRC="images/spacer01.gif" HEIGHT="1" BORDER="0" WIDTH="291"></TD>
            <TD CLASS="package_box_br" WIDTH="11"><IMG SRC="images/spacer01.gif" HEIGHT="1" BORDER="0" WIDTH="11"></TD>
        </TR>
    </TABLE>

	</div>
                                                                  		
			<table cellSpacing="0" cellPadding="0" width="792" border="0">
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td align="left" valign="top" width="403px">
								</td>
								<td align="left" valign="top">
									<A href="ppc.aspx" target="_self">
                                                                           <script type="text/javascript">LoadRightFlash();</script>
									</A>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
                        <table id="table4" cellSpacing="0" cellPadding="0" width="98.5%" border="0">
                            <tr>
                                <td align="left">
                                    <div align="left">
                                        <table id="table5" cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
                                            <tr>
                                                <td vAlign="top" align="left" width="70"><asp:HyperLink id="m_lnkNewAccount1" runat="server"><IMG height="69" src="images/hphone_circle.jpg" border="0"></asp:HyperLink></td>
                                                <td vAlign="top" align="left">
                                                    <p style="MARGIN-TOP: 5px; MARGIN-LEFT: 5px"><b><font face="Arial" color="#e0631b" size="2">Get 
                                                                Reliable Home Phone Service.</font></b><br>
                                                        <b><font style="FONT-SIZE: 11px" face="Arial" color="#6b6b6b"><br>Get reliable home phone service, low rates on long distance service, and low cost internet access.<br><asp:HyperLink id="m_lnkNewAccount3" runat="server"><font style="FONT-SIZE: 14px" face="Arial" color="#e0631b">Get your quote NOW!</font></asp:HyperLink><asp:HyperLink id="m_lnkNewAccount2" runat="server"><font color="#4375a3"><br><br>Click Here&gt;</font></asp:HyperLink></font></b></p>
                                                </td>
                                                <td vAlign="top" align="left" width="68">
                                                    <p style="MARGIN-LEFT: 5px"><A href="ppc.aspx" target="_self"><IMG height="69" src="images/cphone_circle.jpg" border="0"></A></p>
                                                </td>
                                                <td vAlign="top" align="left" width="212">
                                                    <b><font style="FONT-SIZE: 11px" face="Arial" color="#6b6b6b">
                                                            <P style="MARGIN-TOP: 5px; MARGIN-LEFT: 5px"><B><FONT face="Arial" color="#e0631b" size="2">Get 
                                                                        a No-Hassle Cell Phone Today.</FONT></B><BR>
                                                                <B><FONT style="FONT-SIZE: 11px" face="Arial" color="#6b6b6b"><br>Choose from packages to 
                                                                        fit any budget. <A href="ppc.aspx" target="_self"><FONT color="#4375a3"><br><br>Click Here&gt;</FONT></A></FONT></B></P>
                                                            <P style="MARGIN-TOP: 30px; MARGIN-LEFT: 5px">
                                                                <A href="ppld.aspx" target="_self"><font color="#4375a3"></font></A>
                                                        </font></b>&nbsp;</P>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td vAlign="top" align="left" width="199"><A href="account_selling_put_info.aspx" target="_self"><IMG height="150" src="images/10_things_box.jpg" width="199" border="0"></A></td>
                            </tr>
                        </table>
					</td>
				</tr>
				<tr>
					<td><IMG height="52" src="images/footer.jpg" width="792" border="0"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>