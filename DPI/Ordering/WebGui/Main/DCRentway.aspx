<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Page language="c#" Codebehind="DCRentway.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Main.DCRentway" smartNavigation="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect - Debit Card Summary</title>
		<meta content="False" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">		
		<script language="JavaScript">
		<!---
			var clickedButton = false;
			function check() 
			{
				//--->alert("DCSummary.Check");
				return clickedButton;
			}
		//--->
		</script>
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0"
		ms_positioning="GridLayout">
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
							<tr>
								<td class="05_con_label" width="660" colSpan="2" height="24"><asp:image id="imgWorkflow" runat="server" ImageUrl="images/subtable_header_purpose.jpg"></asp:image></td>
							</tr>
							<tr>
								<td class="05_con_sublabel_zip" vAlign="middle" align="left" background="images/subtable_header_blank.jpg"
									bgColor="white" colSpan="5" height="50" style="HEIGHT: 50px">&nbsp;&nbsp; 
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="Label2" runat="server" ForeColor="Chocolate" Font-Names="Arial" Width="176px"
										Font-Size="Small" Font-Bold="True" Height="22px">Purchase Summary</asp:label>
								</td>
							</tr>
							<tr>
								<td align="center" colSpan="2">
									<table cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td align="center">
												<table cellSpacing="0" cellPadding="0" width="642" border="0">
													<tr>
														<td colSpan="3"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
													<tr>
														<td align="center" bgColor="aliceblue" colSpan="3" rowSpan="1" style="HEIGHT: 22px"></td>
													</tr>
												</table>
												<table cellSpacing="0" cellPadding="0" width="97%" border="0">
													<tr>
														<td style="WIDTH: 501px" align="left" bgColor="chocolate" colSpan="1" rowSpan="1">&nbsp;
															<asp:label id="Label1" runat="server" ForeColor="White" Font-Names="Arial">Payment Information</asp:label></td>
														<td bgColor="chocolate">&nbsp;</td>
													</tr>
													<tr>
														<td style="WIDTH: 501px" bgColor="#f0f8ff">&nbsp;&nbsp;
															<asp:label id="Label6" runat="server" ForeColor="Black" Font-Names="Arial" Width="317px">Enrollment Fee</asp:label></td>
														<td align="right" bgColor="#f0f8ff"><asp:label id="lblEnrollFee" runat="server" ForeColor="Black" Font-Names="Arial"></asp:label>&nbsp;&nbsp;&nbsp;</td>
													</tr>
													<tr>
														<td colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
													<tr>
														<td style="WIDTH: 501px; HEIGHT: 26px" bgColor="floralwhite">&nbsp;&nbsp;
															<asp:label id="Label7" runat="server" ForeColor="Black" Font-Names="Arial" Width="160px">Initial Load Amount</asp:label></td>
														<td style="HEIGHT: 26px" align="right" bgColor="floralwhite">
															<asp:Label id="lblLoadAmt" runat="server" Width="74px"></asp:Label>&nbsp;&nbsp;</td>
													</tr>
													<tr>
														<td colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
													<tr>
														<td style="WIDTH: 501px" align="left" bgColor="#f0f8ff">&nbsp;&nbsp;<asp:label id="Label9" runat="server" ForeColor="Chocolate" Font-Names="Arial" Width="153px"
																Font-Size="Small" Font-Bold="True">Total Amount Due:</asp:label>&nbsp;</td>
														<td align="right" bgColor="#f0f8ff"><asp:label id="txtOrderTotal" runat="server" Font-Names="Arial" Width="47px" Font-Bold="True"
																Font-Size="Medium" ForeColor="Red"></asp:label>&nbsp;&nbsp;&nbsp;</td>
													</tr>
													<tr>
														<td colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
													<tr>
														<td bgColor="floralwhite" style="HEIGHT: 17px">
														</td>
														<td align="right" bgColor="floralwhite" style="HEIGHT: 17px">
														</td>
													</tr>
													<tr>
														<td colSpan="2" style="HEIGHT: 1px"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
													<tr>
														<td colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
													<tr>
														<td colSpan="2"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
									<P align="right">&nbsp;&nbsp;</P>
									<P align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</P>
								</td>
							</tr>
							<tr>
								<td align="left">&nbsp;&nbsp;&nbsp;
									<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg" CausesValidation="False"></asp:imagebutton></td>
								<td align="right"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton>&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td class="05_con_bold" align="center" colSpan="2" height="24">
									<P>&nbsp;</P>
									<P><asp:validationsummary id="ValidationSummary2" runat="server" Width="490px" Height="37px"></asp:validationsummary></P>
									<asp:placeholder id="phError" runat="server"></asp:placeholder></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="2"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
