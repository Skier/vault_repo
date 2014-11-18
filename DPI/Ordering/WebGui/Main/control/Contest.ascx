<%@ Control Language="c#" AutoEventWireup="false" Codebehind="Contest.ascx.cs" Inherits="DPI.Ordering.Main.control.Contest" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table height="237" cellSpacing="0" cellPadding="0" width="244" background="images/menu_contest_body.jpg"
	border="0">
	<tr> <!----  Top of table  ----->
		<td><asp:image id="Image1" ImageUrl="../images/menu_contest_top.jpg" runat="server"></asp:image></td>
	</tr>
	<tr> <!----  Body of table  ----->
		<td vAlign="top" width="100%" height="100%">
			<!----------- Dynamic table data -------------->
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr align="top">
					<td width="30" style="HEIGHT: 63px">&nbsp;</td>
					<td vAlign="top" align="left">
						<table border="0" cellpadding="0" cellspacing="0">
							<tr>
								<td rowspan="2" width="67" height="85">
									<asp:Image id="Image5" runat="server" ImageUrl="../images/prizes.gif" ImageAlign="TextTop"></asp:Image>
								</td>
								<td style="HEIGHT: 1px" vAlign="top"><asp:label id="Label1" runat="server" Font-Names="Arial" Font-Size="X-Small" ForeColor="Chocolate"
										Font-Bold="True">Contest Results:</asp:label></td>
							</tr>
							<tr>
								<td vAlign="top">
									<asp:Label id="Label2" runat="server">Click start for the latest contest results.</asp:Label><br>
									<asp:LinkButton id="Linkbutton2" runat="server" ForeColor="Gray" Font-Bold="True" Font-Size="Smaller"
										Font-Names="Tahoma">> Start</asp:LinkButton>
								</td>
							</tr>
						</table>
					</td>
					<td width="20" style="HEIGHT: 63px">&nbsp;</td>
				</tr>
				<tr align="top">
					<td width="30">&nbsp;</td>
					<td align="left">
						<asp:Image id="Image4" runat="server" ImageUrl="../images/callfeatures.gif"></asp:Image>
					</td>
					<td width="20">&nbsp;</td>
				</tr>
			</table>
			<!----------- End of dynamic table data -------------->
		</td>
	</tr>
	<tr> <!----  Bottom of table  ----->
		<td vAlign="bottom"><asp:image id="Image7" ImageUrl="../images/menu_contest_bottom.jpg" runat="server" BorderWidth="0"></asp:image></td>
	</tr>
</table>
