<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Page language="c#" Codebehind="IlecInfo.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.IlecInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>ilec</title>
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
</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
<TABLE height=803 cellSpacing=0 cellPadding=0 width=744 border=0 
ms_2d_layout="TRUE">
  <TR vAlign=top>
    <TD width=744 height=803>
					<form id="Form1" method="post" runat="server" onSubmit="return check();">
      <TABLE height=742 cellSpacing=0 cellPadding=0 width=801 border=0 
      ms_2d_layout="TRUE">
        <TR vAlign=top>
          <TD width=801 height=742>
									<table cellSpacing="0" cellPadding="0" height="741" width="800" align="left" border="0">
										<tr>
											<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
										</tr>
										<tr>
											<td background="images/sidenav_bgd.gif" vAlign="top" height="100%" align="center" width="124"
												bgColor="white"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
											<td vAlign="top" width="660">
												<table height="100%" cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
													<TBODY>
														<tr>
															<td colSpan="2"></td>
														</tr>
														<tr>
															<td vAlign="middle" align="left" width="660" background="images/subtable_header_blank.jpg"
																colSpan="2" height="61" border="0">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:Label id="Label1" runat="server" Width="360px" Height="16px" ForeColor="#404040" Font-Names="Tahoma"
																	Font-Size="XX-Small" Font-Bold="True">Please have the customer identify service provider in their zip code or simply proceed with the default provider selected.</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
																&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; 
																&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<FONT face="Arial"><STRONG>Zip 
																		Code:</STRONG></FONT>
																<asp:label id="lblZipcode" runat="server" Width="64px" Font-Names="Arial" ForeColor="Black"
																	CssClass="05_con_sublabel_zip"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp; 
																&nbsp;&nbsp;&nbsp;&nbsp;
															</td>
														</tr>
														<tr>
															<td vAlign="top" align="center" colSpan="2"><asp:radiobuttonlist id="RadioButtonList1" runat="server" DataTextField="" DataValueField="" BorderColor="White"
																	BackColor="Transparent" Width="488px" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy" BorderStyle="Solid" BorderWidth="1px" ToolTip="List of Available Providers"></asp:radiobuttonlist></td>
														</tr>
														<tr>
															<td class="05_con_normal" align="center" colSpan="2"><br>
																<asp:label id="lblErrMsg" runat="server" Width="480px" ForeColor="Red" Font-Bold="True" Visible="False"></asp:label></td>
											</td>
										</tr>
										<tr>
											<td vAlign="top" align="left" height="13">&nbsp;&nbsp;&nbsp;&nbsp;<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg" CausesValidation="False"></asp:imagebutton></td>
											<td vAlign="top" align="right" height="13"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;</td>
										</tr>
										<tr>
											<td vAlign="top" colSpan="2" height="100%">&nbsp;</td>
										</tr>
									</table></TD></TR></TABLE></TD></TR></TABLE></FORM></TD></TR></TBODY>
		<script language="JavaScript">
		window.history.forward(1);
		</script>
</TABLE>
	</body>
</HTML>
