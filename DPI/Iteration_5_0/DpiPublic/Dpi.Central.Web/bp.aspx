<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Page CodeBehind="bp.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Dpi.Central.Web.BpPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC</title>
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="bpForm" method="post" runat="server">
			<table border="0" width="792" cellspacing="0" cellpadding="0" id="table1">
				<tr>
					<td rowspan="3" align="left" valign="top">
						<img border="0" src="images/ind_products_side.jpg" width="124"></td>
					<td valign="top">
						<table border="0" cellpadding="0" cellspacing="0" id="table2">
							<tr>
								<td>
									<img border="0" src="images/bp_top_l.jpg" width="326" height="222"></td>
								<td>
									<img border="0" src="images/bp_top_r.jpg" width="342" height="222"></td>
							</tr>
							<tr>
								<td>
									<img border="0" src="images/bp_mid_l.jpg" width="326" height="99"></td>
								<td>
									<img border="0" src="images/bp_mid_r.jpg" width="342" height="99"></td>
							</tr>
							<tr>
								<td colspan="2" height="210">
									<p align="center">
										<img border="0" src="images/p_coming_soon.jpg" width="187" height="52"></p>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td class="page_footer">
						<dwc:Footer id="_footer" runat="server"></dwc:Footer>
					</td>
				</tr>
			</table>
		</form>
		<span style="VISIBILITY: hidden">
			<SCRIPT LANGUAGE="Javascript" SRC="http://www.dpiteleconnect.com/counter/counter.php?page=bp"><!--
			//--></SCRIPT>
		</span>
	</body>
</HTML>
