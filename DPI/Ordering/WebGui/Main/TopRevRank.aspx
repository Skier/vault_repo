<%@ Page language="c#" Codebehind="TopRevRank.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Main.TopRevRank" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>TopRevRank</title>
		<LINK href="Styles/Navigator.css" rel="stylesheet">
			<LINK href="Styles/DPI.css" rel="stylesheet">
	</HEAD>
	<body topmargin="0" leftmargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE height="500" cellSpacing="0" cellPadding="0" width="208" border="0" ms_2d_layout="TRUE">
				<TR vAlign="top">
					<TD width="208" height="500">
						<table border="0" width="400" height="500">
							<tr>
								<td align="center">
									<asp:Label id="lblRank" runat="server" Font-Names="Arial" Font-Bold="True" ForeColor="#C00000"
										Font-Size="Small">Revenue Ranking</asp:Label></td>
							</tr>
							<tr>
								<td>
									<asp:PlaceHolder id="tblTopRevRank" runat="server"></asp:PlaceHolder>
								</td>
							</tr>
							<tr>
								<td align="center">
									<asp:Label id="lblVerbs" runat="server" Width="100%"></asp:Label>
									<asp:ImageButton id="btnPrint" runat="server" ImageUrl="images/btn_print2.jpg"></asp:ImageButton>
									<asp:ImageButton id="btnDone" runat="server" ImageUrl="images/btn_done.jpg"></asp:ImageButton>
								</td>
							</tr>
						</table>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
