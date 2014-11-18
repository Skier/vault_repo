<%@ Import Namespace="DPI.ClientComp" %>
<%@ Import Namespace="DPI.Ordering" %>
<%@ Import Namespace="DPI.Interfaces" %>
<%@ Import Namespace="DPI.Services" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Page language="c#" Codebehind="GetZip.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.NOGetZip" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>GetZip</title>
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
<TABLE height=804 cellSpacing=0 cellPadding=0 width=744 border=0 
ms_2d_layout="TRUE">
  <TR vAlign=top>
    <TD width=744 height=804>
					<form id="Form1" onsubmit="return check();" method="post" runat="server">
      <TABLE height=742 cellSpacing=0 cellPadding=0 width=802 border=0 
      ms_2d_layout="TRUE">
        <TR vAlign=top>
          <TD width=1 height=742></TD>
          <TD width=801>
									<table height="741" cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
										<tr>
											<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
										</tr>
										<tr>
											<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
												height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
											<td vAlign="top">
												<table cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
													<TR>
														<td colSpan="2"></td>
													</TR>
													<% if (Sales.SalesIdReq((IUser)Session["User"], wipper.Wip)){ %>
													<tr>
														<td colSpan="2">
															<!------------------ SALES ID CELL ---------------------->
															<table id="Table1" cellSpacing="0" cellPadding="0" width="655" border="0">
																<tr>
																	<td colSpan="5"><IMG height="17" src="images/subheader_top.jpg" width="655" border="0"></td>
																</tr>
																<tr bgColor="#f3f3f3">
																	<td width="27" height="6"><IMG height="100%" src="images/subheader_left.jpg" width="25" border="0"></td>
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
													<!------------------ Conversion CELL ---------------------->
													<%if ((bool)wipper.Wip["AllowLocalConv"]){ %>
													<tr>
														<td colSpan="2" height="103">
															<table id="Table2" cellSpacing="0" cellPadding="0" width="655" border="0">
																<tr>
																	<td colSpan="5"><IMG height="17" src="images/subheader_top.jpg" width="655" border="0"></td>
																</tr>
																<tr bgColor="#f3f3f3">
																	<td width="9" height="40"><IMG height="100%" src="images/subheader_left.jpg" width="25" border="0"></td>
																	<td align="center" width="618" height="40">
																		<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																			<asp:checkbox id="chkConversion" tabIndex="1" runat="server" AutoPostBack="True" Text="Convert Current Service"></asp:checkbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
																			&nbsp;
																		</P>
																		<P><asp:label id="lblCurSvcPh" runat="server" Visible="False">Current Service Phone #: </asp:label><asp:textbox id="txtNpa" onkeyup="countMePhone1(this.value);" tabIndex="4" runat="server" Visible="False"
																				Width="35px" MaxLength="3"></asp:textbox>&nbsp;
																			<asp:textbox id="txtNxx" onkeyup="countMePhone2(this.value);" tabIndex="5" runat="server" Visible="False"
																				Width="35px" MaxLength="3"></asp:textbox>&nbsp;
																			<asp:textbox id="txtNumber" onkeyup="countMePhone3(this.value);" tabIndex="6" runat="server"
																				Visible="False" Width="45px" MaxLength="4"></asp:textbox><asp:regularexpressionvalidator id="Regularexpressionvalidator2" runat="server" ControlToValidate="txtNpa" ValidationExpression="^\d{3}$"
																				ErrorMessage="Please use numbers in phone number field.">*</asp:regularexpressionvalidator><asp:regularexpressionvalidator id="Regularexpressionvalidator3" runat="server" ControlToValidate="txtNxx" ValidationExpression="^\d{3}$"
																				ErrorMessage="Please use numbers in phone number field.">*</asp:regularexpressionvalidator><asp:regularexpressionvalidator id="Regularexpressionvalidator4" runat="server" ControlToValidate="txtNumber" ValidationExpression="^\d{4}$"
																				ErrorMessage="Please use numbers in phone number field.">*</asp:regularexpressionvalidator></P>
																	</td>
																	<TD align="right" width="12" height="40"><IMG height="100%" src="images/subheader_right.jpg" width="12" border="0"></TD>
																</tr>
																<TR>
																	<TD colSpan="5"><IMG height="15" src="images/subheader_bottom.jpg" width="655" border="0"></TD>
																</TR>
															</table>
														</td>
													</tr>
													<% } %>
													<!------------------ End of Conversion CELL ---------------------->
													<!------------------ Source ------------------- -->
													<%if ((bool)wipper.Wip["ShowSource"] && StoreSvc.ShowSource(wipper.IMap, ((IUser)Session["User"]).LoginStoreCode)){ %>
													<TR>
														<TD class="05_con_label" width="334" height="39"></TD>
														<TD class="05_con_label" align="right" width="326" height="39">Select Customer 
															Source&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
															<asp:dropdownlist id="ddlSource" runat="server" Width="155px"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
														</TD>
													</TR>
													<% } %>
													<!------------------- End Of Source ---------- -->
													<tr>
														<td class="05_con_label" width="334" height="24"><asp:image id="imgWorkflow" runat="server"></asp:image></td>
														<td class="05_con_label" align="right" width="326" background="images/subtable_Neworder2.jpg"
															height="24" border="0">Enter Customer's Zip Code&nbsp;&nbsp; 
															&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
															<asp:textbox id="txtZipcode" tabIndex="7" runat="server"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
														</td>
													</tr>
													<!----------->
													<tr>
														<td>&nbsp;&nbsp;&nbsp;&nbsp;<asp:imagebutton id="btnPrevious" runat="server" CausesValidation="False" ImageUrl="images/btn_previous.jpg"></asp:imagebutton>
														</td>
														<td align="right"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;
														</td>
													</tr>
													<tr>
														<td align="center" colSpan="2">
															<P><asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ValidationExpression="^\d{5}$" ControlToValidate="txtZipcode"
																	Font-Size="10pt" Font-Name="verdana" Display="Static">
										Zip code must be 5 numeric digits
									</asp:regularexpressionvalidator></P>
															<P><asp:label id="lblErrMsg" runat="server" Width="408px" Font-Bold="True" ForeColor="Red" Visible="False"></asp:label></P>
															<P><asp:label id="Label1" runat="server" Width="446px" Font-Bold="True" ForeColor="Gray" Visible="False"
																	Font-Names="Arial">We are sorry. dPi does not sell  phone services at your location at this time. Please check back soon to determine if there has been a service added to your area.</asp:label></P>
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table></TD></TR></TABLE>
					</form></TD></TR></TABLE>
	</body>
</HTML>
