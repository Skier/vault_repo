<%@ Import Namespace="DPI.ClientComp" %>
<%@ Import Namespace="DPI.Ordering" %>
<%@ Import Namespace="DPI.Interfaces" %>
<%@ Import Namespace="DPI.Services" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SideControl2" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="Contest" TagName="ContestTable" Src="control/Contest.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Page language="c#" Codebehind="MenuScreen.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.MenuScreen" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NOGetZip</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
			window.name = "main";
<!--
function emailpop()
{
	win = window.open("MailForm.aspx",'_blank','toolbar=no,location=1,directories=1,status=1, menubar=1, scrollbars=1,resizable=1');
	return false
}

function MM_openBrWindow(theURL,winName,features) 
{ 
	var showgogreed = "<%= Session["showgogreed"] %>";
	
	//alert(showgogreed);
	
	if (showgogreed == 'no')
		return;		
	
	showgogreed	= "<%= Session["showgogreed"] = "no" %>";
		
	//alert(showgogreed);
	
	window.open(theURL,winName,features);  
}
function CloseByName(name)
{ 
	window.open("javascript:close()",name);
}
function OpenGreedIsGoodWindow()
{
	var doc = "Greed is Good.pdf";
	var docPath = document.all.hdnDocPath.value;
				   
	var features = "height= 690, width=682 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=yes,resizable=yes";
	var name = "_blank";
	
	window.open(docPath + '/' + doc, name, features);				
}

//-->

		</script>
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" onload="MM_openBrWindow('promoPopUp.aspx','dpipromoPopUp','width=340,height=420')"
		ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="640" align="left" border="0">
				<tr>
					<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"><uc1:sidecontrol2 id="SideControl21" runat="server"></uc1:sidecontrol2></td>
					<td vAlign="top" align="left"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader>
						<!----  menu table ---->
						<!----  menu table ---->
						<table cellSpacing="0" cellPadding="0" width="660" border="0">
							<tr>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<IMG alt="" src="images/hot1.gif" border="0"></td>
								<td vAlign="bottom" width="109" background="images/products_table_r1_c2.jpg" height="112"
									border="0">&nbsp;
									<asp:linkbutton id="lnkNewOrder" runat="server" Font-Bold="True" Font-Name="tahoma" ForeColor="Gray"
										Font-Size="Smaller" Font-Names="tahoma" Height="28px"></asp:linkbutton></td>
								<td width="99" background="images/products_table_r1_c3.gif" height="112" border="0">&nbsp;</td>
								<td vAlign="bottom" align="left" width="116" background="images/subtable_header_tutorial.jpg"
									height="112" border="0">&nbsp;&nbsp;
									<asp:linkbutton id="btnTutorial" runat="server" Font-Bold="True" Font-Name="tahoma" ForeColor="Gray"
										Font-Size="Smaller" Font-Names="tahoma" Height="28px" Width="71px">> Start</asp:linkbutton></td>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<IMG alt="" src="images/hot1.gif" border="0"></td>
								<td vAlign="bottom" width="109" background="images/products_table_r1_c6.jpg" height="112"
									border="0">&nbsp;<asp:linkbutton id="lnkMonthlyPayment" runat="server" Font-Bold="True" Font-Name="tahoma" ForeColor="Gray"
										Font-Size="Smaller" Font-Names="tahoma" Height="28px"></asp:linkbutton>
								</td>
							</tr>
						</table>
						<!----  end menu table ---->
						<!----  end menu table ----><br>
						<table cellSpacing="0" cellPadding="0" width="660" border="0">
							<tr>
								<%			
string url = null;
if (ContestResults.AllowIncentives((IUser)Session["User"])) 
{
%>
								<td vAlign="bottom" align="right" width="244" background="images/get200.jpg" height="237"><asp:linkbutton id="lnkGoGreed" runat="server" Font-Bold="True" ForeColor="MediumBlue" Font-Size="X-Small"
										Font-Names="Tahoma" Height="38px">Click Here Now! >></asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;&nbsp; &nbsp;
								</td>
								<%
} else { 
%>
								<td vAlign="bottom" align="right" width="244" background="images/menu_adspace_r1_c1.jpg"
									height="237"><asp:linkbutton id="lnkProductsOffered" runat="server" Font-Bold="True" ForeColor="Gray" Font-Size="Smaller"
										Font-Names="Tahoma" Height="38px">> learn more</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
								<%
} 
%>
								<td width="178" background="images/menu_adspac_blank.jpg" height="237">
									<p>&nbsp;&nbsp;&nbsp;&nbsp;<A onclick="OpenGreedIsGoodWindow();" href="MenuScreen.aspx"><font color="mediumblue">Click 
												Here</font></A> to see results of</p>
									<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <FONT style="FONT-WEIGHT: bold; FONT-SIZE: 13px; COLOR: #3c8823; LINE-HEIGHT: normal; FONT-STYLE: normal; FONT-VARIANT: normal">
											Greed Is Good</FONT></P>
								</td>
								<td vAlign="bottom" align="left" width="238" background="images/menu_adspace_r1_c3.jpg"
									height="237">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:linkbutton id="lnkAgentHotline" runat="server" Font-Bold="True" ForeColor="Gray" Font-Size="Smaller"
										Font-Names="Tahoma" Height="38px" Width="112px">> learn more</asp:linkbutton></td>
							</tr>
						</table>
						<br>
					</td>
				</tr>
				<TR>
					<td colSpan="2"><dpiuser:sitefooter id="Sitefooter1" runat="server"></dpiuser:sitefooter><INPUT id="hdnDocPath" runat="server" style="WIDTH: 56px; HEIGHT: 22px" type="hidden" size="4"></td>
				</TR>
			</table>
			<asp:linkbutton id="LinkButton1" style="Z-INDEX: 101; LEFT: 730px; POSITION: absolute; TOP: 545px"
				runat="server" Font-Bold="True" ForeColor="Chocolate" Font-Size="X-Small" Font-Names="Arial">email</asp:linkbutton><asp:label id="lblErrMsg" style="Z-INDEX: 102; LEFT: 264px; POSITION: absolute; TOP: 520px"
				runat="server" ForeColor="Red" Width="192px"></asp:label></form>
		<script language="JavaScript">
		window.history.forward(1);
		</script>
	</body>
</HTML>
