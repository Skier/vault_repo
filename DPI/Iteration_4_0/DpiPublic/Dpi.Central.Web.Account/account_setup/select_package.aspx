<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Register TagPrefix="uc2" TagName="package_details" Src="package_details.ascx" %>
<%@ Page language="c#" Codebehind="select_package.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.AccountSetup.SelectPackages" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC - Select Package</title>
		<meta name="vs_showGrid" content="False">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
		<script type="text/javascript" src="../script/p7_eqCols2_10.js"></script>
		<style type="text/css">

.package_box_tl { BACKGROUND-IMAGE: url(../images/package_bi.gif); MARGIN: 0px; WIDTH: 11px; BACKGROUND-REPEAT: no-repeat; HEIGHT: 14px }

.package_box_top { BACKGROUND-IMAGE: url(../images/package_bj.gif); MARGIN: 0px; BACKGROUND-REPEAT: repeat-x; HEIGHT: 14px }

.package_box_tr { BACKGROUND-IMAGE: url(../images/package_bk.gif); MARGIN: 0px; WIDTH: 11px; BACKGROUND-REPEAT: no-repeat; HEIGHT: 14px }

.package_box { BORDER-RIGHT: #b5b5b5 1px solid; PADDING-RIGHT: 10px; MARGIN-TOP: 0px; PADDING-LEFT: 15px; PADDING-BOTTOM: 5px; BORDER-LEFT: #b5b5b5 1px solid; PADDING-TOP: 5px }

.defaultPackageText { MARGIN-TOP: 5px; FONT-SIZE: 11px; COLOR: #4d4d4d; LINE-HEIGHT: 15px; FONT-FAMILY: Verdana }

.package_box_bl { BACKGROUND-IMAGE: url(../images/package_bm.gif); MARGIN: 0px; WIDTH: 11px; LINE-HEIGHT: 5px; BACKGROUND-REPEAT: no-repeat; HEIGHT: 6px }

.package_box_bottom { BACKGROUND-IMAGE: url(../images/package_bn.gif); MARGIN: 0px; WIDTH: 300px; LINE-HEIGHT: 5px; BACKGROUND-REPEAT: repeat-x; HEIGHT: 6px }

.package_box_br { BACKGROUND-IMAGE: url(../images/package_bu.gif); MARGIN: 0px; WIDTH: 11px; LINE-HEIGHT: 5px; BACKGROUND-REPEAT: no-repeat; HEIGHT: 6px }

.package_text { FONT-SIZE: 12px; COLOR: #4d4d4d; FONT-FAMILY: Arial, Helvetica, sans-serif }

.package_name { FONT-SIZE: 14pt; COLOR: #ff6600 }

.package_price { FONT-SIZE: 14pt; COLOR: #ff6600 }

.package_price_note { FONT-WEIGHT: normal; FONT-SIZE: 8pt; COLOR: #999999; FONT-STYLE: normal; FONT-FAMILY: Arial, Helvetica, sans-serif; TEXT-DECORATION: none }

		</style>
		<style type="text/css" media="screen">

#m_packageDetails0_m_featuresWrapper { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 0.75em; FLOAT: left; PADDING-BOTTOM: 0px; WIDTH: 275px; MARGIN-RIGHT: 0px; PADDING-TOP: 0px }

#m_packageDetails1_m_featuresWrapper { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 0.75em; FLOAT: left; PADDING-BOTTOM: 0px; WIDTH: 275px; MARGIN-RIGHT: 0px; PADDING-TOP: 0px }

#m_packageDetails2_m_featuresWrapper { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 0.75em; FLOAT: left; PADDING-BOTTOM: 0px; WIDTH: 275px; MARGIN-RIGHT: 0px; PADDING-TOP: 0px }

#m_packageDetails3_m_featuresWrapper { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 0.75em; FLOAT: left; PADDING-BOTTOM: 0px; WIDTH: 275px; MARGIN-RIGHT: 0px; PADDING-TOP: 0px }

#m_packageDetails4_m_featuresWrapper { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 0.75em; FLOAT: left; PADDING-BOTTOM: 0px; WIDTH: 275px; MARGIN-RIGHT: 0px; PADDING-TOP: 0px }

#m_packageDetails5_m_featuresWrapper { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 0.75em; FLOAT: left; PADDING-BOTTOM: 0px; WIDTH: 275px; MARGIN-RIGHT: 0px; PADDING-TOP: 0px }

#m_packageDetails6_m_featuresWrapper { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 0.75em; FLOAT: left; PADDING-BOTTOM: 0px; WIDTH: 275px; MARGIN-RIGHT: 0px; PADDING-TOP: 0px }

#m_packageDetails7_m_featuresWrapper { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 0.75em; FLOAT: left; PADDING-BOTTOM: 0px; WIDTH: 275px; MARGIN-RIGHT: 0px; PADDING-TOP: 0px }

#m_packageDetails8_m_featuresWrapper { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 0.75em; FLOAT: left; PADDING-BOTTOM: 0px; WIDTH: 275px; MARGIN-RIGHT: 0px; PADDING-TOP: 0px }

#m_packageDetails9_m_featuresWrapper { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; FONT-SIZE: 0.75em; FLOAT: left; PADDING-BOTTOM: 0px; WIDTH: 275px; MARGIN-RIGHT: 0px; PADDING-TOP: 0px }

		</style>
	</HEAD>
	<body>
		<form id="selectpackage" method="post" runat="server">
			<div class="process_form" style="PADDING-TOP: 10px">
				<table class="process_table">
					<tr valign="top">
						<td height="100%"><uc2:package_details id="m_packageDetails0" runat="server"></uc2:package_details></td>
						<td width="100%"><uc2:package_details id="m_packageDetails1" runat="server"></uc2:package_details></td>
					</tr>
					<tr valign="top">
						<TD height="100%"><uc2:package_details id="m_packageDetails2" runat="server"></uc2:package_details></TD>
						<td><uc2:package_details id="m_packageDetails3" runat="server"></uc2:package_details></td>
					</tr>
					<tr valign="top">
						<TD height="100%"><uc2:package_details id="m_packageDetails4" runat="server"></uc2:package_details></TD>
						<td><uc2:package_details id="m_packageDetails5" runat="server"></uc2:package_details></td>
					</tr>
					<tr valign="top">
						<TD height="100%"><uc2:package_details id="m_packageDetails6" runat="server"></uc2:package_details></TD>
						<td><uc2:package_details id="m_packageDetails7" runat="server"></uc2:package_details></td>
					</tr>
					<tr valign="top">
						<TD height="100%"><uc2:package_details id="m_packageDetails8" runat="server"></uc2:package_details></TD>
						<td><uc2:package_details id="m_packageDetails9" runat="server"></uc2:package_details></td>
					</tr>
					<tr>
						<TD><asp:ImageButton id="btnPrevious" runat="server" ImageUrl="../images/btn_back.gif"></asp:ImageButton></TD>
						<td style="COLOR: chocolate; TEXT-ALIGN: right">Please select a package to proceed</td>
					</tr>
				</table>
			</div>
		</form>
	</body>
</HTML>
