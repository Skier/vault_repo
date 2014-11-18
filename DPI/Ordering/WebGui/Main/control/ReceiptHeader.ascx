<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ReceiptHeader.ascx.cs" Inherits="DPI.Ordering.Control.PrintHeader" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<LINK href="Styles/Navigator.css" rel="stylesheet">
<LINK href="Styles/DPI.css" rel="stylesheet">
<table cellSpacing="0" cellPadding="0" width="905" border="0">
	<tr>
		<td background="images/receiptheader.jpg" border="0" height="96" width="897" vAlign="bottom"
			align="right">&nbsp;
			<asp:Label id="lblVersion" runat="server" Height="48px" Width="167px" ForeColor="Black" Font-Names="Arial"
				Font-Size="Smaller" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
			&nbsp;
			<asp:Button id="btnLogoutKiller" ForeColor="Transparent" runat="server" CausesValidation="False"
				BackColor="Transparent" BorderStyle="None"></asp:Button>
			<asp:ImageButton id="btnImgLogout" Width="69px" Height="27px" runat="server" ImageUrl="../images/Logout.gif"
				CausesValidation="False"></asp:ImageButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
	</tr>
	<tr>
		<td vAlign="middle" background="images/mnu_bg.jpg" height="28">
			<table height="28" cellSpacing="0" cellPadding="0" width="897" background="images/header_r1_c2.jpg"
				border="0">
				<% if(Context.User.Identity.IsAuthenticated){ %>
				<tr vAlign="middle" align="center">
					<td width="12"><IMG height="100%" alt="" src="images/header_r1_c1.jpg" width="12" border="0"></td>
					<td align="left" width="926" height="28">&nbsp;
						<asp:imagebutton id="btnHome" Height="100%" ImageUrl="../images/navi_home.gif" runat="server" CausesValidation="False"></asp:imagebutton>
						<asp:image id="Image1" Height="100%" ImageUrl="../images/navi_shim.gif" runat="server"></asp:image>
						<asp:imagebutton id="btnReporting" Height="100%" ImageUrl="../images/navi_reports.gif" runat="server"
							CausesValidation="False"></asp:imagebutton>
						<asp:image id="Image2" Height="100%" ImageUrl="../images/navi_shim.gif" runat="server"></asp:image>
						<asp:imagebutton id="btnQandA" Height="100%" ImageUrl="../images/navi_q&amp;a.gif" runat="server"
							CausesValidation="False"></asp:imagebutton>
						<asp:image id="Image3" Height="100%" ImageUrl="../images/navi_shim.gif" runat="server"></asp:image>
						<asp:imagebutton id="btnAgentHotline" Height="100%" ImageUrl="../images/navi_agenthotline.gif" runat="server"
							CausesValidation="False"></asp:imagebutton>
						<asp:image id="Image4" Height="100%" ImageUrl="../images/navi_shim.gif" runat="server"></asp:image>
						<asp:imagebutton id="btnForms" Height="100%" ImageUrl="../images/navi_forms.gif" runat="server" CausesValidation="False"></asp:imagebutton>
						<asp:image id="Image5" Height="100%" ImageUrl="../images/navi_shim.gif" runat="server"></asp:image>
						<asp:label id="Greeting" Height="27px" Runat="server" ForeColor="Gray" CssClass="05_con_normal"
							Width="160px"></asp:label>
					</td>
					<td width="14" align="right"><IMG height="100%" alt="" src="images/header_r1_c3.jpg" width="14" border="0">
					</td>
				</tr>
				<% } %>
				<tr>
					<td colSpan="3"><IMG height="5" alt="" src="images/printheader_r2_c1.jpg" width="921" border="0"></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
