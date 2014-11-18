<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Register TagPrefix="ctl" Namespace="Dpi.Central.Web.Account.Controls" Assembly="Dpi.Central.Web.Account" %>
<%@ Page language="c#" Codebehind="order_status.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.OrderStatus" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="loginForm" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TBODY>
					<TR>
						<TD colSpan="2"><dns:header id="ctrlHeader" runat="server"></dns:header></TD>
						<TD vAlign="top" align="left"></TD>
					</TR>
					<TR>
						<TD vAlign="top" rowSpan="2"><IMG alt="" src="../images/about_side.jpg"></TD>
						<TD vAlign="top" align="left"><IMG src="../images/ppc_top.jpg" border="0">
							<TABLE class="layout_table">
								<TR>
									<TD>
										<h2>Orders</h2>
									</TD>
								</TR>
								<TR>
									<TD align="center"><ASP:DATAGRID id="grdOrderStatuses" runat="server" CellPadding="3" ENABLEVIEWSTATE="False" pagesize="20"
											autogeneratecolumns="False" allowsorting="True">
											<HEADERSTYLE BACKCOLOR="LightGray"></HEADERSTYLE>
											<COLUMNS>
												<asp:BoundColumn DataField="Type" HeaderText="Activity Type">
													<HEADERSTYLE WIDTH="80px"></HEADERSTYLE>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Description" HeaderText="Description">
													<HEADERSTYLE WIDTH="160px"></HEADERSTYLE>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="WorkStart" HeaderText="Begin" DataFormatString="{0:d}">
													<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="WorkFinish" HeaderText="End" DataFormatString="{0:d}">
													<ITEMSTYLE HORIZONTALALIGN="Right"></ITEMSTYLE>
												</asp:BoundColumn>
											</COLUMNS>
											<PAGERSTYLE MODE="NumericPages"></PAGERSTYLE>
										</ASP:DATAGRID></TD>
								</TR>
								<TR>
									<TD>
										<asp:linkbutton id="lbtnGotoLogin" runat="server" CausesValidation="False">Return to Account Summary</asp:linkbutton></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD vAlign="bottom" align="center"><dns:footer id="ctrlFooter" runat="server"></dns:footer></TD>
						<TD vAlign="top" align="left"></TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
