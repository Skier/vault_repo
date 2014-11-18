<%@ Import Namespace="DPI.ClientComp" %>
<%@ Import Namespace="DPI.Ordering" %>
<%@ Import Namespace="DPI.Interfaces" %>
<%@ Import Namespace="DPI.Services" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Page language="c#" Codebehind="Satellite.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Satellite" enableViewState="False"%>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>NOGetZip</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript" type="text/JavaScript">
		<!--		
		
		var preSelVendor = "136";
		var preSelProduct = "2164";
		var clickedButton = false;
		function check() 
		{
			document.all.btnNext.focus();
			if (clickedButton)
			{
				clickedButton = false;
				return true;
			}
			else
				return false;
		}
		function GetProducts(vendor)
		{
			var prods = new Array();
			var allProds = document.all.hdnProducts.value.split(";");
			
			var j = 0;
			
			for (var i = 0; i < allProds.length; i++)
			{
				if (allProds[i].substr(0, allProds[i].indexOf(",")) == vendor)
				{
					prods[j] = allProds[i];
					j++;
				}							
			}						
			return prods;						
		}
		function GetProduct(prodId)
		{
			var prods = GetProducts(preSelVendor);
			
			for (var i = 0; i < prods.length; i++)
				if (prods[i].split(",")[1] == prodId)
					return prods[i];				
		}		
		function FillProduct()
		{
			var prods = GetProducts(preSelVendor);
			
			document.all.ddlProduct.options.length = 0;
			
			for (var i = 0; i < prods.length; i++)
				document.all.ddlProduct.options[i + 1] = new Option(prods[i].split(",")[2], prods[i].split(",")[1]);
		}
		function SetData()
		{
			var product = GetProduct(GetSelProdId());
			
			document.all.lblProductSummary.innerText = product.split(",")[2] + " - " + product.split(",")[4]			
			document.all.lblAmountDue.innerText = "$" + product.split(",")[3]
		}
		function ValidateAmt()
		{
			if (isNaN(parseFloat(document.all.txtAmountTendered.value.replace("$", ""))) ||
				parseFloat(document.all.txtAmountTendered.value.replace("$", "")) <
				parseFloat(document.all.lblAmountDue.innerText.replace("$", "")))
			{
				alert("Please enter a Total Amount Collected greater than or equal to the Total Amount Due.");
				document.all.txtAmountTendered.focus();
				return false;
			}
			
			return true;
		}
		function ValSelProd()
		{
			var prod = GetSelProdId();
			if (isNaN(parseFloat(prod)) || parseFloat(prod) < 1)
			{
				alert("Please select a Program.");
				return false;
			}		
			
			return true;
		}
		function Validate()
		{
			if (!ValSelProd())
				return false;
			
			if (!ValidateAmt())
				return false;				
			
			return true;
		}
		function Calculate()
		{
			if (!Validate())
				return false;
			
			var due = parseFloat(document.all.txtAmountTendered.value.replace("$", "")) - parseFloat(document.all.lblAmountDue.innerText.replace("$", ""));
			document.all.lblChangeDue.innerText = "$" + due.toFixed(2).toString();
			document.all.hdnAmtDue.value = document.all.lblAmountDue.innerText;
			document.all.hdnSelectedProd.value = GetSelProdId();
			
			return true;
		}
		
		function GetSelProdId()
		{
			var prodId = 0;
			for (var i = 0; i < document.all.ddlProduct.length; i++)
			{
				if (document.all.ddlProduct[i].checked)
					prodId = document.all.ddlProduct[i].value;
			}
			
			return prodId;
		}
		function Submit()
		{
			if (Calculate())
				clickedButton = true;
			
		}
		
		// -->       
		</script>
</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<form id="Form1" onsubmit="return check();" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
				<tr>
					<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
						height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
					<td vAlign="top" width="660">
						<table height="100%" cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
							<tr>
								<td width="100%" colSpan="5" style="HEIGHT: 6px"></td>
							</tr>
							<tr>
								<td class="05_con_label" style="HEIGHT: 22px" width="334" height="22">&nbsp;
								</td>
								<td class="05_con_label" style="HEIGHT: 22px" width="326" height="22">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
							</tr>
							<% if (Sales.SalesIdReq((IUser)Session["User"], wipper.Wip)){ %>
							<tr>
								<td colSpan="2">
									<!------------------ SALES ID CELL ---------------------->
									<table id="Table1" cellSpacing="0" cellPadding="0" width="655" border="0">
										<tr>
											<td colSpan="5"><IMG height="17" src="images/subheader_top.jpg" width="655" border="0"></td>
										</tr>
										<tr bgColor="#f3f3f3">
											<td width="9" height="6"><IMG height="100%" src="images/subheader_left.jpg" width="25" border="0"></td>
											<td align="center" width="618"><asp:label id="Label1" runat="server">Co-Worker ID</asp:label>&nbsp;<asp:textbox id="txtSalesId" tabIndex="1" runat="server" Width="127px"></asp:textbox>&nbsp;&nbsp;&nbsp;
											</td>
											<TD align="right" width="12" height="6"><IMG height="100%" src="images/subheader_right.jpg" width="12" border="0"></TD>
										</tr>
										<TR>
											<TD colSpan="5"><IMG height="15" src="images/subheader_bottom.jpg" width="655" border="0"></TD>
										</TR>
									</table>
									<!------------------ !SALES ID CELL! ----------------------></td>
							</tr>
							<% } %>
							<tr>
								<td align="center" colSpan="2">
									<table cellSpacing="0" cellPadding="0" width="600" border="0">
										<tr>
											<td style="HEIGHT: 60px" align="center" bgColor="#ffffff" colSpan="2" height="60">
												<div align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<INPUT id="hdnSelVendor" style="WIDTH: 48px; HEIGHT: 22px" type="hidden" size="2" value="136"
														name="hdnSelVendor" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<IMG alt="" src="images/dish.jpg">
													&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
													<asp:imagebutton id="btnSvcPwrSchedule" runat="server" ImageUrl="images/btnSatelliteScheduler.jpg"></asp:imagebutton></div>
											</td>
										</tr>
										<!--- Sling Shot Zipcode --->
										<!--- Sling Shot Zipcode--->
										<tr>
											<td style="HEIGHT: 49px" align="center" bgColor="#ffffff" colSpan="2" height="49">
												<asp:RadioButtonList id="ddlProduct" runat="server" EnableViewState="False"></asp:RadioButtonList>
												<div align="center"><INPUT id="hdnSelectedProd" style="WIDTH: 48px; HEIGHT: 22px" type="hidden" size="2" name="hdnSelectedProd"
														runat="server"><INPUT id="hdnAmtDue" style="WIDTH: 48px; HEIGHT: 22px" type="hidden" size="2" name="hdnAmtDue"
														runat="server"><INPUT id="hdnProducts" type="hidden" name="hdnProducts" runat="server">
												</div>
											</td>
										</tr>
										<tr>
											<td style="HEIGHT: 38px" align="center" bgColor="#ffffff" colSpan="2" height="38">
												<div class="05_con_normal" align="center">&nbsp;</div>
												<DIV class="05_con_normal" align="center"><asp:label id="lblProductSummary" runat="server" Width="635px" Font-Italic="True"></asp:label></DIV>
											</td>
										</tr>
										<tr>
											<td align="right" colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="629" border="0"></td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 288px; HEIGHT: 18px" align="right">Payment Method</td>
											<td style="HEIGHT: 18px" align="right"><asp:dropdownlist id="ddlPayMethod" tabIndex="7" runat="server">
													<asp:ListItem Value="0" Selected="True">Cash</asp:ListItem>
													<asp:ListItem Value="1">Credit Card</asp:ListItem>
													<asp:ListItem Value="2">Debit Card</asp:ListItem>
													<asp:ListItem Value="3">Check</asp:ListItem>
													<asp:ListItem Value="4">Money Order</asp:ListItem>
												</asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
											<td style="HEIGHT: 18px">&nbsp;</td>
										</tr>
										<tr>
											<td align="right" colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="629" border="0"></td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 288px; HEIGHT: 15px" align="right"><asp:label id="Label2" runat="server" Width="160px" Font-Names="Arial" Font-Size="Medium" Font-Bold="True"
													ForeColor="#C04000">Total Amount Due</asp:label></td>
											<td style="HEIGHT: 15px" align="right"><asp:label id="lblAmountDue" runat="server" Width="82px" Font-Size="Medium"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										<tr>
											<td style="WIDTH: 288px; HEIGHT: 23px" align="right">Total Amount Collected</td>
											<td style="HEIGHT: 23px" align="right"><asp:textbox id="txtAmountTendered" tabIndex="8" runat="server" Width="89px"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										<tr>
											<td align="right" colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="629" border="0"></td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 288px" rowSpan="1">&nbsp;</td>
											<td vAlign="middle" align="right"><asp:label id="txtNonRefund" runat="server" Width="177px" Font-Italic="True" CssClass="05_con_small">Payment is non-refundable.</asp:label><asp:imagebutton id="btnChangeDue" tabIndex="9" runat="server" ImageUrl="images/btn_changedue.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 288px" align="right" height="40"></td>
											<td vAlign="middle" align="right">&nbsp;&nbsp;
												<asp:label id="lblChangeDue" runat="server" Width="111px" Font-Names="Arial" Font-Size="Medium"
													Font-Bold="True" ForeColor="Red"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
												&nbsp;&nbsp;&nbsp;
											</td>
											<td>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td style="HEIGHT: 34px" bgColor="#ffffff" colSpan="2" height="34">
									<div align="center"><asp:label id="lblErrMsg" runat="server" Width="628px" Font-Names="Arial" Font-Size="Small"
											Font-Bold="True" ForeColor="Red" Height="10px"></asp:label></div>
								</td>
							</tr>
							<tr>
								<td vAlign="top" colSpan="2" height="100%">
									<TABLE id="Table2" style="WIDTH: 656px; HEIGHT: 34px" cellSpacing="1" cellPadding="1" width="656"
										border="0">
										<TR>
											<TD align="left"><asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg"></asp:imagebutton></TD>
											<TD align="right"><asp:imagebutton id="btnNext" tabIndex="10" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="2"><dpiuser:sitefooter id="SiteFooter" runat="server"></dpiuser:sitefooter></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
