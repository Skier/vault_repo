<%@ Control Language="c#" AutoEventWireup="false" Codebehind="subheader.ascx.cs" Inherits="DPI.Ordering.control.subheader" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table id="Table1" cellSpacing="0" cellPadding="0" width="655" border="0">
	<tr>
		<td colSpan="5"><IMG height="17" src="images/subheader_top.jpg" width="655" border="0"></td>
	</tr>
	<tr bgColor="#f3f3f3">
		<td width="9" height="6"><IMG height="100%" src="images/subheader_left.jpg" width="25" border="0"></td>
		<td width="618">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td colSpan="4" style="WIDTH: 362px; HEIGHT: 23px">
						<asp:image id="Image1" runat="server" ImageUrl="../images/quickstats.gif"></asp:image>
					</td>
					<td align="right" style="HEIGHT: 23px" vAlign="bottom">
						<asp:label id="lblStoreNum" Width="111px" runat="server" Height="20px" Font-Names="Arial"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="5"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
				</tr>
				<tr bgcolor="aliceblue">
					<td style="WIDTH: 360px" align="left" colSpan="2">
						<asp:label id="lblLocationLabel" Width="56px" runat="server">Location</asp:label>
						<asp:label id="lblLocation" Width="232px" runat="server" Font-Bold="True" Font-Size="Smaller"></asp:label></td>
					<td align="right" colspan="3"><asp:label id="lblFeature" runat="server" Font-Bold="True" Font-Size="X-Small" CssClass="subheader_feature"
							Height="20px"></asp:label><asp:label id="lblSteps" Width="84px" runat="server" Font-Bold="True" Font-Size="X-Small" CssClass="subheader_step"
							Height="20px"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" colSpan="5"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
				</tr>
				<TR bgColor="white">
					<TD style="WIDTH: 135px">
						<asp:label id="lblActiveCust" Width="144px" runat="server" Font-Size="Smaller"></asp:label>
					</TD>
					<TD style="WIDTH: 214px">
						<asp:imagebutton id="btnActCustRank" runat="server" ImageUrl="../images/ddown.gif" CausesValidation="False"></asp:imagebutton>
						<asp:label id="lblActCustRank" Width="160px" runat="server" Font-Size="Smaller"></asp:label>
					</TD>
					<TD align="left" colspan="3">
						<asp:label id="lblMTDRevenue" Width="168px" runat="server" Font-Size="Smaller"></asp:label>
						<asp:label id="lblMTDRev" runat="server" Width="80px" Font-Size="Smaller"></asp:label>&nbsp;</TD>
				</TR>
				<tr>
					<td vAlign="top" colSpan="5"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
				</tr>
				<TR bgColor="floralwhite">
					<TD style="WIDTH: 135px">
						<asp:label id="lblMTD" Width="144px" runat="server" Font-Size="Smaller"></asp:label>
					</TD>
					<TD style="WIDTH: 214px">
						<asp:imagebutton id="btnMtdRank" runat="server" ImageUrl="../images/ddown.gif" CausesValidation="False"></asp:imagebutton>
						<asp:label id="lblMTDRank" Width="152px" runat="server" Font-Size="Smaller"></asp:label>
					</TD>
					<TD style="WIDTH: 259px" colspan="3">
						<asp:imagebutton id="btnMTDRevRank" ImageUrl="../images/ddown.gif" runat="server" CausesValidation="False"></asp:imagebutton>
						<asp:label id="lblMTDRevRank" runat="server" Width="171px" Font-Size="Smaller"></asp:label>
					</TD>
				</TR>
			</table>
		</td>
		<TD align="right" width="12" height="6"><IMG height="100%" src="images/subheader_right.jpg" width="12" border="0"></TD>
	</tr>
	<TR>
		<TD colSpan="5"><IMG height="15" src="images/subheader_bottom.jpg" width="655" border="0"></TD>
	</TR>
</table>
<SCRIPT language="JavaScript" src="../Core/wz_tooltip.js" type="text/javascript"></SCRIPT>

