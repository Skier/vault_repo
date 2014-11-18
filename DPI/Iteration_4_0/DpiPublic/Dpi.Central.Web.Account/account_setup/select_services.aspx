<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Page language="c#" Codebehind="select_services.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.AccountSetup.SelectServices" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC - Select Services</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
		<script src="select_services.js"></script>
	</HEAD>
	<body>
		<form id="selectServicesForm" method="post" runat="server">
			<div class="process_form">
				<div class="step_caption">
					Please select additional features/services for your monthly plan.<br>
					<span class="midgray_small"><img src="../images/asterisk.gif">Click on the feature/service name to view a description</span>
				</div>
				<TABLE class="process_table">
					<TR vAlign="top">
						<td width="50%">
							<table id="quoteTable" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr bgColor="chocolate">
									<td style="COLOR: white" align="center" colSpan="2">Quote
									</td>
								</tr>
								<tr>
									<td colSpan="2"><asp:table id="m_tblPackage" Width="100%" Runat="server">
											<asp:TableRow>
												<asp:TableCell HorizontalAlign="Left" Width="100%">
													<asp:Label id="m_lblPackageName" runat="server" Font-Bold="True">Service Name</asp:Label>
												</asp:TableCell>
												<asp:TableCell HorizontalAlign="Right" Wrap="False">
													<asp:label id="m_lblPackageTotal" runat="server" CssClass="subitems" Font-Bold="True">Total: $50</asp:label>
												</asp:TableCell>
											</asp:TableRow>
										</asp:table></td>
								</tr>
								<tr>
									<td colSpan="2" style="PADDING-LEFT: 3px; FONT-WEIGHT: bold; PADDING-BOTTOM: 3px; PADDING-TOP: 3px">Upgrades</td>
								</tr>
								<tr>
									<td colSpan="2">
										<asp:PlaceHolder id="phldFeaturesTable" runat="server"></asp:PlaceHolder></td>
								</tr>
								<tr>
									<td colSpan="2" style="PADDING-RIGHT: 3px; PADDING-TOP: 10px; TEXT-ALIGN: right"><asp:label id="m_lblTotalUpgrages" runat="server" CssClass="subitems">Total Upgrades: $12</asp:label></td>
								</tr>
								<tr>
									<td colSpan="2" style="PADDING-RIGHT: 3px; COLOR: red; PADDING-TOP: 10px; TEXT-ALIGN: right">
										<asp:label id="m_lblGrandTotal" runat="server">Grand Total: $15</asp:label><br>
										<span class="midgray_small">(Excluding Taxes and Fees)</span>
									</td>
								</tr>
							</table>
						</td>
						<TD width="50%">
							<asp:PlaceHolder id="phldProductsTable" runat="server"></asp:PlaceHolder></TD>
					</TR>
					<TR>
						<TD><asp:imagebutton id="m_btnOnEnterStub" runat="server" Width="0px" Height="0px"></asp:imagebutton><asp:imagebutton id="m_btnPrevious" tabIndex="3" runat="server" ImageUrl="../images/btn_back.gif"></asp:imagebutton></TD>
						<TD align="right"><asp:imagebutton id="m_btnNext" tabIndex="2" runat="server" ImageUrl="../images/btn_proceed.gif"></asp:imagebutton></TD>
					</TR>					
				</TABLE>
			</div>
		</form>
	</body>
</HTML>
