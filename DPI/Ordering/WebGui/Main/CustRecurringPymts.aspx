<%@ Page language="c#" Codebehind="CustRecurringPymts.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.CustRecurringPymts" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
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
		<form id="Form1" action="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
				<tr>
					<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
						height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
					<td vAlign="top" width="660">
						<table cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
							<TR>
								<TD align="center" colSpan="5"></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="5">Account Number:&nbsp;<asp:label id="lblAccNumber" runat="server" Width="192px"></asp:label></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="5"></TD>
							</TR>
							<tr>
								<td align="center" colSpan="5"><asp:placeholder id="phCustAcctInfo" runat="server"></asp:placeholder></td>
							</tr>
							<tr>
								<td style="HEIGHT: 56px" vAlign="top" colSpan="5">
									<table style="WIDTH: 661px; HEIGHT: 24px" cellSpacing="0" cellPadding="0" width="661" border="0">
										<TBODY>
											<TR>
												<TD style="WIDTH: 30px; HEIGHT: 7px"></TD>
												<TD style="WIDTH: 171px; HEIGHT: 7px"></TD>
												<TD style="WIDTH: 456px; HEIGHT: 7px"></TD>
												<TD style="HEIGHT: 7px"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 30px; HEIGHT: 22px"></TD>
												<TD style="WIDTH: 171px; HEIGHT: 22px">
													<asp:linkbutton id="lbAdd" runat="server" CausesValidation="False">Add</asp:linkbutton>&nbsp;&nbsp;&nbsp;
												</TD>
												<TD style="WIDTH: 456px; HEIGHT: 22px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												</TD>
												<TD style="HEIGHT: 22px"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 30px; HEIGHT: 21px"></TD>
												<TD style="WIDTH: 171px; HEIGHT: 21px">
													<asp:linkbutton id="lbEdit" runat="server" CausesValidation="False">Edit</asp:linkbutton></TD>
												<TD style="WIDTH: 456px; HEIGHT: 21px"></TD>
												<TD style="HEIGHT: 21px"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 30px; HEIGHT: 23px"></TD>
												<TD style="WIDTH: 171px; HEIGHT: 23px">
													<asp:linkbutton id="lbDisable" runat="server">Disable All</asp:linkbutton></TD>
												<TD style="WIDTH: 456px; HEIGHT: 23px"></TD>
												<TD style="HEIGHT: 23px"></TD>
											</TR>
										</TBODY>
									</table>
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
							</tr>
							<TR>
								<TD class="05_con_small" style="HEIGHT: 27px" align="center">
									<asp:label id="lblErrMsg" runat="server" Width="580px" ForeColor="Red" Font-Size="Medium"></asp:label><BR>
								</TD>
							</TR>
							<TR>
								<TD vAlign="top" height="100%"><!------------------------------------------------------------------------>
									<TABLE id="Table1" style="WIDTH: 655px; HEIGHT: 34px" cellSpacing="1" cellPadding="1" width="655"
										border="0">
										<TR>
											<TD align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
												<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg"></asp:imagebutton>
											</TD>
											<TD align="right">
												<asp:imagebutton id="btnGotoMain" runat="server" ImageUrl="images/btn_gotomain.jpg"></asp:imagebutton></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</table>
					</td>
				</tr>
				<TR>
					<TD colSpan="2">
						<dpiuser:sitefooter id="Sitefooter1" runat="server"></dpiuser:sitefooter></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
