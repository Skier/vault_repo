<%@ Page CodeBehind="locations.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Dpi.Central.Web.LocationsPage" %>
<%@ Register TagPrefix="dns" TagName="Header" Src="header.ascx" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="footer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="storeLocationForm" method="post" runat="server">
			<TABLE height="1" cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD vAlign="top" align="left" colSpan="3"><dns:header id="ctrlHeader" runat="server"></dns:header></TD>
				</TR>
				<TR>
					<TD rowSpan="2"><IMG alt="" src="images/locations_side.jpg">
					</TD>
					<TD vAlign="top" align="left">
						<TABLE cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td><IMG src="images/locations_top_l.jpg" border="0"></td>
								<td><IMG src="images/locations_top_r.jpg" border="0"></td>
							</tr>
						</TABLE>
						<TABLE class="layout_table">
							<tr>
								<td class="page_header" colSpan="3">Please select a city and state to locate a dPi 
									reseller near you.
								</td>
							</tr>
							<TR class="separator_row">
								<TD colSpan="3"></TD>
							</TR>
							<TR>
								<TD class="property_name"><asp:label id="lblState" runat="server">Choose a State:</asp:label></TD>
								<TD class="property_name"><asp:dropdownlist id="ddlState" runat="server" AutoPostBack="True" Width="150px"></asp:dropdownlist></TD>
								<TD class="property_validator"></TD>
							</TR>
							<TR>
								<TD class="property_value"><asp:label id="lblCity" runat="server">Choose a City:</asp:label></TD>
								<TD class="property_value"><asp:dropdownlist id="ddlCity" runat="server" AutoPostBack="True" Width="150px"></asp:dropdownlist></TD>
								<TD class="property_validator"></TD>
							</TR>
							<TR class="separator_row">
								<TD colSpan="3"></TD>
							</TR>
							<TR>
								<TD colSpan="3"><asp:label id="lblHeader" runat="server" Width="100%" CssClass="page_sub_header">Title</asp:label></TD>
							</TR>
							<TR>
								<TD vAlign="top" colSpan="3">
									<div style="OVERFLOW: auto; HEIGHT: 278px"><asp:datalist id="dlStoreLocations" runat="server" Width="100%" RepeatColumns="2" CellSpacing="10">
											<AlternatingItemStyle HorizontalAlign="Center"></AlternatingItemStyle>
											<ItemStyle HorizontalAlign="Center"></ItemStyle>
											<ItemTemplate>
												<DIV class="property_sub_value">
													<asp:Label id="lblName" runat="server" CssClass="05_con_medium">Name</asp:Label><BR>
													<asp:Label id="lblAddress" runat="server" CssClass="05_con_medium">Address</asp:Label><BR>
													<asp:Label id="lblCSZ" runat="server" CssClass="05_con_medium">CSZ</asp:Label><BR>
													<asp:Label id="lblPhone" runat="server" CssClass="05_con_medium">Phone</asp:Label><BR>
												</DIV>
											</ItemTemplate>
										</asp:datalist></div>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="bottom" align="center"><dns:footer id="ctrlFooter" runat="server"></dns:footer></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
