<%@ Page language="c#" Codebehind="Operations.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Main.Operations" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Operations</title>
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<TABLE height="803" cellSpacing="0" cellPadding="0" width="558" border="0" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD width="558" height="803">
					<form id="Form1" method="post" runat="server">
						<TABLE height="556" cellSpacing="0" cellPadding="0" width="801" border="0" ms_2d_layout="TRUE">
							<TR vAlign="top">
								<TD width="801" height="556">
									<table align="left" width="800" border="0" cellpadding="0" cellspacing="0" height="555">
										<tr>
											<td background="images/sidenav_bgd.gif" vAlign="top" height="100%" align="center" width="124"
												bgColor="white">&nbsp;
											</td>
											<td valign="top">
												<table width="660" align="left" border="0" cellpadding="0" cellspacing="0">
													<!----------->
													<tr>
														<td colspan="2" vAlign="middle" align="center" height="250">
															<P>
																<asp:Label id="Label1" runat="server" Width="435px" Font-Size="Large" Height="75px"> Operations User Interface</asp:Label></P>
															<P>
																<asp:Button id="btnRefreshData" runat="server" Width="128px" Text="Refresh Data"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:Button id="btnRestart" runat="server" Width="128px" Text="Restart WebQueue"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:Button id="btnStop" runat="server" Width="128px" Text="Stop WebQueue"></asp:Button></P>
															<P>
																<asp:Label id="lblMsg" runat="server" Width="643px"></asp:Label></P>
															<P>
																<asp:Button id="btnMenu" runat="server" Text="Back to Main Menu"></asp:Button></P>
															<P>&nbsp;</P>
														</td>
													</tr>
													<!----------->
												</table>
											</td>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
					</form>
				</TD>
			</TR>
		</TABLE>
	</body>
</HTML>
