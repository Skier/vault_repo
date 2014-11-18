<%@ Page language="c#" Codebehind="FindCustomer.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.FindCustomer" %>
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
		<title>dPi Teleconnect</title>
		<meta name="vs_snapToGrid" content="False">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
		<script>
					<!--// first contact number check //-->
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
		</script>		
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
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
								<td colSpan="2"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
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
											<td align="center" width="618">&nbsp;
												<asp:label id="Label2" runat="server">Co-Worker ID</asp:label>&nbsp;
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
								<td class="05_con_label" width="334" height="24">
									<asp:Image id="imgWorkflow" runat="server"></asp:Image></td>
								<td class="05_con_label" width="326" background="images/subtable_Neworder2.jpg" height="24"
									border="0">
									<table border="0">
										<tr>
											<td width="105">
												<font class="title_normal">Customer's<br>
													Phone Number</font>
											</td>
											<td>
												<asp:textbox id="txtNpa" onkeyup="countMePhone1(this.value);" runat="server" MaxLength="3" Width="35px"
													tabIndex="4"></asp:textbox>-&nbsp;
												<asp:textbox id="txtNxx" onkeyup="countMePhone2(this.value);" runat="server" MaxLength="3" Width="35px"
													tabIndex="5"></asp:textbox>-
												<asp:textbox id="txtNumber" onkeyup="countMePhone3(this.value);" runat="server" MaxLength="4"
													Width="45px" tabIndex="6"></asp:textbox>
												<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="Please use numbers in phone number field."
													ControlToValidate="txtNpa" ValidationExpression="\d{3}">*</asp:RegularExpressionValidator>
												<asp:RegularExpressionValidator id="RegularExpressionValidator2" runat="server" ErrorMessage="Please use numbers in phone number field."
													ControlToValidate="txtNxx" ValidationExpression="\d{3}">*</asp:RegularExpressionValidator>
												<asp:RegularExpressionValidator id="RegularExpressionValidator3" runat="server" ErrorMessage="Please use numbers in phone number field."
													ControlToValidate="txtNumber" ValidationExpression="\d{4}">*</asp:RegularExpressionValidator>
											</td>
										</tr>
										<tr>
											<td>&nbsp;</td>
											<td align="center">-- OR 
												--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
										</tr>
										<tr>
											<td>
												<font class="title_normal">Customer's<br>
													Account Number</font>
											</td>
											<td>
												<asp:textbox id="txtAccNumber" runat="server" type="text" Width="132px" tabIndex="3"></asp:textbox>
												<asp:RegularExpressionValidator id="RegularExpressionValidator4" runat="server" ValidationExpression="^\d+$" ErrorMessage="Please enter numbers for the account number."
													ControlToValidate="txtAccNumber">*</asp:RegularExpressionValidator>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg" tabIndex="2"></asp:imagebutton>
								</td>
								<td align="right"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg" tabIndex="1"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td class="05_con_bold" colSpan="2" height="24" align="center"><br>
									<asp:ValidationSummary id="ValidationSummary1" runat="server" Width="341px"></asp:ValidationSummary>
									<br>
									<div align="center">&nbsp;</div>
								</td>
							</tr>
							<tr>
								<td align="center" colSpan="2">
									<div class="05_con_normal" align="center"></div>
									<br>
								</td>
							</tr>
							<tr>
								<td align="center" colSpan="2"><asp:label id="lblErrMsg" runat="server" Width="472px" ForeColor="Red"></asp:label></td>
							</tr>
							<tr>
								<td class="05_con_bold" colSpan="2" height="24"><br>
									<br>
									<div align="center">&nbsp;</div>
								</td>
							</tr>
							<tr>
								<td align="center" colSpan="2">
									<div class="05_con_normal" align="center"></div>
									<br>
									<br>
								</td>
							</tr>
							<tr>
								<td colSpan="2">
									<div align="center"></div>
								</td>
							</tr>
							<tr>
								<td colSpan="2" height="100%">&nbsp;</td>
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
