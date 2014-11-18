<%@ Page language="c#" Codebehind="WirelessActivation.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.WirelessActivation" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="DPI.Services" %>
<%@ Import Namespace="DPI.Interfaces" %>
<%@ Import Namespace="DPI.Ordering" %>
<%@ Import Namespace="DPI.ClientComp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NOGetZip</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
								<td width="100%" colSpan="5"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
							</tr>
							<TR>
								<TD width="100%" colSpan="5" style="HEIGHT: 14px"></TD>
							</TR>
							<tr>
								<td class="05_con_label" width="334" height="112"><IMG src="images/Infinity_New_Phone.jpg" width="100%" border="0">
								</td>
								<td class="05_con_label" width="326" height="112"><IMG src="images/subtable_header_blank2.jpg" width="100%" border="0">
								</td>
							</tr>
							<TR>
								<TD class="05_con_label" style="HEIGHT: 13px" align="center" width="334" height="13"></TD>
								<TD class="05_con_label" style="HEIGHT: 13px" width="326" height="13"></TD>
							</TR>
							<tr>
							<tr>
								<td align="center" colSpan="2" style="HEIGHT: 138px">
									<table cellSpacing="0" cellPadding="0" width="600" border="0">
										<tr>
											<td style="HEIGHT: 54px" bgColor="#ffffff" colSpan="2" height="54">
												<div align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
													ESN:
													<asp:textbox id="txtESN" runat="server" Width="176px" MaxLength="15"></asp:textbox>&nbsp;
													<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter ESN" ControlToValidate="txtESN">*</asp:RequiredFieldValidator>
													<asp:LinkButton id="lbESN" runat="server">Help</asp:LinkButton></div>
											</td>
										</tr>
										<tr>
											<td style="HEIGHT: 54px" align="center" bgColor="#ffffff" colSpan="2" height="9">
												<div align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
													Zip Code:
													<asp:textbox id="txtZip" runat="server" Width="81px" MaxLength="5"></asp:textbox>
													<asp:RegularExpressionValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="Please only enter numerics in Zip"
														ControlToValidate="txtZip" ValidationExpression="\d{5}">*</asp:RegularExpressionValidator>
													<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Zip Code"
														ControlToValidate="txtZip">*</asp:RequiredFieldValidator></div>
											</td>
										</tr>
										<tr>
											<td align="right" colSpan="2">&nbsp;&nbsp;</td>
											<td>&nbsp;</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td bgColor="#ffffff" colSpan="2" height="73" style="HEIGHT: 73px" align="center">&nbsp;
									<asp:ValidationSummary id="ValidationSummary1" runat="server" Width="619px" Font-Size="Small" Font-Names="Arial"
										Height="32px"></asp:ValidationSummary></td>
							</tr>
							<tr>
								<td class="05_con_small" align="center" colSpan="2"><asp:label id="lblErrMsg" runat="server" Width="643px" Height="10px" Font-Names="Arial" Font-Size="Medium"
										Font-Bold="True" ForeColor="Red"></asp:label><BR>
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
