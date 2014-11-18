<%@ Page language="c#" Codebehind="PrintPromoReg.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.PrintPromoReg" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<LINK href="Styles/Navigator.css" rel="stylesheet">
			<LINK href="Styles/DPI.css" rel="stylesheet">
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0">
		<form id="form_ProductL1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="510" border="0">
				<tr>
					<td align="center"><asp:image id="Image1" runat="server" ImageUrl="images/printheader.jpg" ImageAlign="Middle"></asp:image></td>
				</tr>
				<tr>
					<td align="center" height="34"><asp:label id="lblDate" runat="server" Width="373px"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center" colSpan="2"><asp:placeholder id="phPrintPromoReg" runat="server" EnableViewState="False"></asp:placeholder></td>
				</tr>
				<tr>
					<td class="05_con_medium" vAlign="top" colSpan="2" height="27">
						<P align="center"><asp:label id="lblErrMsg" runat="server" EnableViewState="False" ForeColor="Red" Visible="False"
								Font-Bold="True" Height="40px"></asp:label></P>
					</td>
				</tr>
				<tr>
					<td>&nbsp;
					</td>
				</tr>
				<tr>
					<td align="center" colSpan="2" height="37"><asp:imagebutton id="btnPrint" runat="server" ImageUrl="images/btn_print.jpg"></asp:imagebutton></td>
				</tr>
				<tr>
					<td align="center" colSpan="2" height="37">
						<asp:Button id="btnClose" runat="server" Text="Close"></asp:Button></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
