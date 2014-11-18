<%@ Import Namespace="DPI.ClientComp" %>
<%@ Import Namespace="DPI.Ordering" %>
<%@ Import Namespace="DPI.Interfaces" %>
<%@ Import Namespace="DPI.Services" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Page language="c#" Codebehind="Wireless.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Wireless" enableViewState="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NOGetZip</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script>
			
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
				{
					return false;
				}
			}
			function countMePhone1(string)
			{ 
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 2) 
					document.Form1.txtNxx.focus();
			} 
			
			function countMePhone2(string)
			{ 
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 2) 
					document.Form1.txtNumber.focus();
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
				var prods = GetProducts(document.all.ddlVendor.value);
				
				for (var i = 0; i < prods.length; i++)
					if (prods[i].split(",")[1] == prodId)
						return prods[i];				
			}		
			
			function FillProduct(vendor)
			{
				EnablePhone(false);
				
				if (vendor.value == -1)
					return;
				
				var prods = GetProducts(vendor.value);
				
				document.all.cboProduct.options.length = 0;
				
				for (var i = 0; i < prods.length; i++)
					document.all.cboProduct.options[i + 1] = new Option(prods[i].split(",")[2], prods[i].split(",")[1]);
			}
			
			function SetData(prodId)
			{
				if (document.all.ddlVendor.value == -1)
					return;
				
				var product = GetProduct(prodId);
				
				document.all.lblProductSummary.innerText = product.split(",")[2] + " - " + product.split(",")[4]
				
				document.all.lblAmountDue.innerText = "$" + product.split(",")[3]
					
				if (document.all.ddlVendor.value == "134" || document.all.ddlVendor.value == "135" || document.all.ddlVendor.value == "138" || document.all.ddlVendor.value == "142")
				{
					EnablePhone(true);
					document.all.txtNpa.focus();
				}
				else
				{	
					EnablePhone(false);
					document.all.txtAmountTendered.focus();
				}
			}
			function CreatePhone()
			{
				if (document.all.ddlVendor.value == "129")
				{					
					document.writeln('<TR>');
					document.writeln('<TD style="HEIGHT: 61px" align="center" bgColor="#ffffff" colSpan="2" height="61"><br>');
					document.writeln('<div align="center">Enter Phone Number<br>');
					document.writeln('<INPUT id="txtNpa" style="WIDTH: 37px; HEIGHT: 22px" type="text" size="1">&nbsp;-');
					document.writeln('<INPUT id="txtNxx" style="WIDTH: 37px; HEIGHT: 22px" type="text" size="1">&nbsp;-');
					document.writeln('<INPUT id="txtNumber" style="WIDTH: 45px; HEIGHT: 22px" type="text" size="2">');
					document.writeln('</div>');
					document.writeln('</TD>');
					document.writeln('</TR>');				
				}
			}
			function EnablePhone(isEnable)
			{
				document.all.txtNpa.disabled = !isEnable;
				document.all.txtNxx.disabled = !isEnable;
				document.all.txtNumber.disabled = !isEnable;
			}
			function ValidatePhone()
			{
				if (document.all.txtNpa.disabled)
					return true;
				
				if (document.all.txtNpa.value.match(/\d{3}/) == null)
				{
					alert("Please enter area code");
					document.all.txtNpa.focus();
					return false;
				}	
				if (document.all.txtNxx.value.match(/\d{3}/) == null)
				{
					alert("Please enter prefix");
					document.all.txtNxx.focus();
					return false;
				}	
				if (document.all.txtNumber.value.match(/\d{4}/) == null)
				{
					alert("Please enter Phone Number");
					document.all.txtNumber.focus();
					return false;
				}
				
				return true;
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
				if (isNaN(parseFloat(document.all.cboProduct.value)) || parseFloat(document.all.cboProduct.value) < 1)
				{
					alert("Please select a Wireless Product.");
					return false;
				}		
				
				return true;
			}
			function Validate()
			{
				if (!ValSelProd())
					return false;
				
				if (!ValidatePhone())
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
				document.all.hdnSelectedProd.value = document.all.cboProduct.value;
				
				return true;
			}
			function Submit()
			{
				if (Calculate())
					clickedButton = true;
				
			}			
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
								<td width="100%" colSpan="5"></td>
							</tr>
							<tr>
								<td class="05_con_label" width="334" height="112"><IMG src="images/subtable_header_cellular1.jpg" width="100%" border="0">
								</td>
								<td class="05_con_label" width="326" height="112"><IMG src="images/subtable_header_cellular2.jpg" width="100%" border="0">
								</td>
							</tr>
							<tr>
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
											<td align="center" width="618">&nbsp;
												<asp:label id="Label1" runat="server">Co-Worker ID</asp:label>&nbsp;
												<asp:textbox id="txtSalesId" runat="server"></asp:textbox>&nbsp;&nbsp;&nbsp;
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
											<td style="HEIGHT: 54px" bgColor="#ffffff" colSpan="2" height="54"><INPUT id="hdnProducts" type="hidden" runat="server"><INPUT id="hdnSelectedProd" style="WIDTH: 48px; HEIGHT: 22px" type="hidden" size="2" name="hdnSelectedProd"
													runat="server"><INPUT id="hdnAmtDue" style="WIDTH: 48px; HEIGHT: 22px" type="hidden" size="2" name="hdnAmtDue"
													runat="server"><br>
												<div align="center">Select a Vendor<br>
													<asp:dropdownlist id="ddlVendor" runat="server" Width="279px"></asp:dropdownlist></div>
											</td>
										</tr>
										<tr>
											<td style="HEIGHT: 10px" bgColor="#ffffff" colSpan="2" height="9"><br>
												<div align="center">Select a Product<br>
													<SELECT id="cboProduct" style="WIDTH: 280px" onchange="SetData(this.value);" runat="server">
														<OPTION selected></OPTION>
													</SELECT></div>
											</td>
										</tr>
										<TR>
											<TD style="HEIGHT: 61px" align="center" bgColor="#ffffff" colSpan="2" height="61"><br>
												<div align="center">Enter Phone Number<br>
													<INPUT id="txtNpa" onkeyup="countMePhone1(this.value);" style="WIDTH: 37px; HEIGHT: 22px"
														disabled type="text" maxLength="3" size="1" runat="server">&nbsp;- <INPUT id="txtNxx" onkeyup="countMePhone2(this.value);" style="WIDTH: 37px; HEIGHT: 22px"
														disabled type="text" maxLength="3" size="1" runat="server">&nbsp;- <INPUT id="txtNumber" style="WIDTH: 45px; HEIGHT: 22px" disabled type="text" maxLength="4"
														size="2" runat="server">
												</div>
											</TD>
										</TR>
										<tr>
											<td align="center" bgColor="#ffffff" colSpan="2" height="50">
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
											<td style="HEIGHT: 18px" align="right"><asp:dropdownlist id="ddlPayMethod" runat="server">
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
											<td style="WIDTH: 288px; HEIGHT: 15px" align="right"><asp:label id="Label2" runat="server" Width="160px" ForeColor="#C04000" Font-Bold="True" Font-Size="Medium"
													Font-Names="Arial">Total Amount Due</asp:label></td>
											<td style="HEIGHT: 15px" align="right"><asp:label id="lblAmountDue" runat="server" Width="82px" Font-Size="Medium"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										<tr>
											<td style="WIDTH: 288px; HEIGHT: 15px" align="right">Total Amount Collected</td>
											<td style="HEIGHT: 15px" align="right"><asp:textbox id="txtAmountTendered" runat="server" Width="89px"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											</td>
										</tr>
										<tr>
											<td align="right" colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="629" border="0"></td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 288px" rowSpan="1">&nbsp;</td>
											<td vAlign="middle" align="right"><asp:label id="txtNonRefund" runat="server" Width="177px" Font-Italic="True" CssClass="05_con_small">Payment is non-refundable.</asp:label><asp:imagebutton id="btnChangeDue" runat="server" ImageUrl="images/btn_changedue.jpg" EnableViewState="False"
													CausesValidation="False"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td style="WIDTH: 288px" align="right" height="40"></td>
											<td vAlign="middle" align="right">&nbsp;&nbsp;
												<asp:label id="lblChangeDue" runat="server" Width="111px" ForeColor="Red" Font-Bold="True"
													Font-Size="Medium" Font-Names="Arial"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
												&nbsp;&nbsp;&nbsp;
											</td>
											<td>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td bgColor="#ffffff" colSpan="2" height="10">&nbsp;</td>
							</tr>
							<tr>
								<td class="05_con_small" align="center" colSpan="2"><asp:label id="lblErrMsg" runat="server" Width="643px" ForeColor="Red" Font-Bold="True" Font-Size="Small"
										Font-Names="Arial" Height="10px"></asp:label><BR>
								</td>
							</tr>
							<tr>
								<td vAlign="top" colSpan="2" height="100%">
									<TABLE id="Table1" style="WIDTH: 656px; HEIGHT: 34px" cellSpacing="1" cellPadding="1" width="656"
										border="0">
										<TR>
											<TD align="left"><asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg"></asp:imagebutton></TD>
											<TD align="right"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton></TD>
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
