<%@ Page CodeBehind="ppc.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Dpi.Central.Web.PpcPage" %>
<%@ Register TagPrefix="dns" TagName="Header" Src="header.ascx" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="footer.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<title>dPi Teleconnect LLC</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<base target="Body">
<script language="JavaScript" type="text/JavaScript">
<!--
function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}
//-->
</script>
<style type="text/css">
.contentTd { PADDING-LEFT: 20px }
.hdr { FONT-WEIGHT: bold; FONT-SIZE: 17px; MARGIN: 0px; COLOR: #db6c1d; FONT-FAMILY: Arial, Helvetica, sans-serif }
.hdrs { FONT-WEIGHT: bold; FONT-SIZE: 11px; MARGIN: 0px; COLOR: #db6c1d; PADDING-TOP: 12px; FONT-FAMILY: Arial, Helvetica, sans-serif }
.txt { FONT-WEIGHT: bold; FONT-SIZE: 11px; MARGIN: 0px; COLOR: #6b6b6b; FONT-FAMILY: Arial, Helvetica, sans-serif }
.priceTD { FONT-WEIGHT: bold; FONT-SIZE: 11px; MARGIN: 0px; COLOR: #6b6b6b; FONT-FAMILY: Arial, Helvetica, sans-serif }
.planNameTD { FONT-WEIGHT: bold; FONT-SIZE: 11px; MARGIN: 0px; COLOR: #6b6b6b; FONT-FAMILY: Arial, Helvetica, sans-serif }
.priceTD { PADDING-RIGHT: 6px; WIDTH: 36px; TEXT-ALIGN: right }
.planNameTD { WIDTH: 125px }
#planTable TD { PADDING-LEFT: 4px }
</style>
</head>
<body onLoad="MM_preloadImages('images/ready_to_b1_on.jpg','images/ready_to_b2_on.jpg','images/ready_to_b3_on.jpg')">
	<form id="ppcForm" method="post" runat="server">
		<table border="0" cellpadding="0" cellspacing="0" width="792">
			<tr>
				<td><dns:Header id="_header" runat="server" /></td>
			</tr>
		</table>
		<table border="0" width="792" cellspacing="0" cellpadding="0">
			<tr>
				<td rowspan="3" align="left" valign="top" background="images/ppc_side_bg.gif">
					<img src="images/ppc_side.jpg" width="124" height="206" border="0"></td>
				<td valign="top">
					<table border="0" cellpadding="0" cellspacing="0" id="table2">
						<tr>
							<td colspan="2">
							<table width="637" height="201" border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td rowspan="2">
										<img src="images/ppc_top_01.gif" width="453" height="201" alt=""></td>
									<td>
										<img src="images/ppc_top_02.jpg" width="120" height="153" alt=""></td>
									<td rowspan="2">
										<img src="images/ppc_top_03.gif" width="64" height="201" alt=""></td>
								</tr>
								<tr>
									<td>
										<img src="images/ppc_top_04.gif" width="120" height="48" alt=""></td>
								</tr>
								<tr><td style="background-image:url(images/ppc_top2.gif)" height="19" colspan="3"></td></tr>
							</table>							</td>
						</tr>
						<tr>
							<td colspan="2">
							<table width="100%" height="95" border="0" cellpadding="0" cellspacing="0">
								<tr>
									<td><img src="images/ppc_mid_02.gif" style="margin-left:20px" width="152" height="95" alt=""></td>
									<td><img src="images/blank.gif" width="102" height="1" alt=""></td>
									<td><img src="images/ppc_mid_04.gif" width="333" height="94" alt=""></td>
								</tr>
							</table>							</td>
						</tr>
						<tr>
							<td colspan="2" align="center">
								<img border="0" src="images/ppc_mid2.gif" width="638" height="50"></td>
						</tr>
						<tr>
							<td colspan="2" class="contentTd">
								<p class="hdr">Plans</p>
									<p class="txt">Our plans are designed 
											to grow with you as your needs change.&nbsp; So once you choose dPi Pre-Paid 
											cellular, you&rsquo;ll never have to choose another cellular provider again.</p>							</td>
						</tr>
						<tr>
						  <td colspan="2" class="contentTd"><table width="636" border="0" cellpadding="0" cellspacing="0">
                              <tr>
                                <td width="198" valign="top"><table border="0" cellspacing="0" cellpadding="2">
                              <tr>
                                <td colspan="2" class="hdrs">Weekly</td>
                              </tr>
                              <tr>
                                <td class="planNameTD"><p>25 Anytime Minutes </p></td>
                                <td class="priceTD">$9.99</td>
                              </tr>
                              <tr>
                                <td class="planNameTD">50 Anytime Minutes</td>
                                <td class="priceTD">$16.99</td>
                              </tr>
                            </table><table border="0" cellspacing="0" cellpadding="2">
                              <tr>
                                <td colspan="2" class="hdrs">Monthly</td>
                              </tr>
                              <tr>
                                <td class="planNameTD">100 Anytime Minutes</td>
                                <td class="priceTD">$29.99</td>
                              </tr>
                              <tr>
                                <td class="planNameTD">200 Anytime Minutes</td>
                                <td class="priceTD">$39.99</td>
                              </tr>
                              <tr>
                                <td class="planNameTD">300 Anytime Minutes</td>
                                <td class="priceTD">$49.99</td>
                              </tr>
                              <tr>
                                <td class="planNameTD">500 Anytime Minutes</td>
                                <td class="priceTD">$69.99</td>
                              </tr>
                              <tr>
                                <td class="planNameTD">1000 Anytime Minutes</td>
                                <td class="priceTD">$119.99</td>
                              </tr>
                            </table></td>
                                <td width="233" valign="top"><table border="0" cellspacing="0" cellpadding="2">
                              <tr>
                                <td class="hdrs">Optional Features </td>
                                <td class="hdrs"><span class="hdrs">Week</span></td>
                                <td class="hdrs"><span class="hdrs">Month</span></td>
                              </tr>
                              <tr>
                                <td class="planNameTD">3G Web Services </td>
                                <td class="priceTD">$1.50</td>
                                <td class="priceTD">$5.00</td>
                              </tr>
                              <tr>
                                <td class="planNameTD">Picture Mail </td>
                                <td class="priceTD">$1.50</td>
                                <td class="priceTD">$5.00</td>
                              </tr>
                              <tr>
                                <td class="planNameTD">Push To Talk </td>
                                <td class="priceTD">$3.75</td>
                                <td class="priceTD">$15.00</td>
                              </tr>
                            </table><table border="0" cellspacing="0" cellpadding="2">
                              <tr>
                                <td colspan="2" class="hdrs">Text Messages </td>
                              </tr>
                              <tr>
                                <td class="planNameTD">25 Text Messages</td>
                                <td class="priceTD">$2.50</td>
                              </tr>
                              <tr>
                                <td class="planNameTD">100 Text Messages</td>
                                <td class="priceTD">$10.00</td>
                              </tr>
                              <tr>
                                <td class="planNameTD">250 Text Messages</td>
                                <td class="priceTD">$25.00</td>
                              </tr>
                            </table></td>
                                <td width="205" rowspan="2" valign="top"><p class="hdrs">Control Your Wireless! </p>
                                <p class="txt">When your minutes get low, you will&nbsp;recieve a&nbsp;text message on&nbsp;your phone and you decide weather or&nbsp;not you to add more minutes by&nbsp;using a cash card, loading a&nbsp;new service plan or just wait until your next monthly plan starts. </p>
								<table border="0" cellspacing="0" cellpadding="2">
                              <tr>
                                <td colspan="2" class="hdrs">Text Messages </td>
                              </tr>
                              <tr>
                                <td width="153" valign="top" class="planNameTD">15 Anytime Minutes</td>
                                <td width="40" class="priceTD">$5.00</td>
                              </tr>
                              <tr>
                                <td valign="top" class="planNameTD">25 Anytime Minutes</td>
                                <td class="priceTD">$10.00</td>
                              </tr>
                              <tr>
                                <td valign="top" class="planNameTD">45 Anytime Minutes</td>
                                <td class="priceTD">$15.00</td>
                              </tr>
                              <tr>
                                <td valign="top" class="planNameTD">60 Anytime Minutes</td>
                                <td class="priceTD">$20.00</td>
                              </tr>
                            </table></td>
                              </tr>
                              <tr>
                                <td colspan="2" valign="bottom"><a href="contact.aspx"><img src="images/ppc_readytosign.gif" alt="Ready to sign?" width="425" height="63" border="0" style="margin-top:6px"></a></td>
                            </tr>
                            </table>					        
				          </td>
						</tr>
						<tr>
							<td colspan="2" class="contentTd"><br>
								<p class="hdr">Phones</p>							</td>
						</tr>
						<tr>
							<td colspan="2">
								<p style="MARGIN-BOTTOM: 5px; MARGIN-LEFT: 20px; MARGIN-RIGHT: 8px">
									<font face="Arial" style="FONT-SIZE: 11px" color="#6b6b6b">
										<span style="FONT-WEIGHT: 700">
			Choose 
			from our great selection of cool phones or use your existing Sprint 
			PCS pre-paid cellular phone.<br>
			</span></font> <font face="Arial" color="#6b6b6b" size="1">* Call '1-877 Join dPi' or visit an authorized 
										dPi reseller for phone pricing and availability</font></p>							</td>
						</tr>
						<tr>
							<td colspan="2">
							<table align="center" width="652" height="450" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td>
			<img src="images/ppc_sanyo2400.gif" width="336" height="224" alt=""></td>
		<td>
			<img src="images/ppc_sanyo_katana.gif" width="316" height="224" alt=""></td>
	</tr>
	<tr>
		<td>
			<img src="images/ppc_lg125.gif" width="336" height="226" alt=""></td>
		<td>
			<img src="images/ppc_lg225.gif" width="316" height="226" alt=""></td>
	</tr>
</table></td>
</tr>
</table></td></tr>
<tr><td><p align="center"><dns:Footer id="_footer" runat="server" /></p>
</td></tr>
</table>
<script type="text/javascript" src="http://www.dpiteleconnect.com/counter/counter.php?page=ppc"></script>
</form>
</body>
</html>
