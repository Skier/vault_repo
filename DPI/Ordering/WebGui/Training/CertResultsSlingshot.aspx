<%@ Page language="c#" Codebehind="CertResultsSlingshot.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Training.CertResultsSlingshot" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Slingshot Certification Results</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="511" align="left" border="0">
				<tr>
					<td style="WIDTH: 511px">
						<asp:Image id="Image1" runat="server" ImageUrl="../Main/images/printheader.jpg"></asp:Image></td>
				</tr>
				<tr>
					<td align="center">
						<table cellSpacing="0" cellPadding="0" width="640" align="middle" border="0">
							<tr>
								<td align="center" style="HEIGHT: 55px">
									<asp:Label id="Label1" runat="server" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Large"
										Width="229px"> Congratulations!</asp:Label></td>
							</tr>
							<tr>
								<td style=" HEIGHT: 198px" align="center">
									<P>
										<asp:Label id="Label5" runat="server" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small"
											Width="488px"></asp:Label></P>
									<P>
										<asp:Label id="Label2" runat="server" Width="448px" Font-Size="X-Small" Font-Bold="True" Font-Names="Tahoma"></asp:Label></P>
								</td>
							</tr>
							<tr>
								<td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:ImageButton id="btnGetCert" runat="server" ImageUrl="../Main/images/btnGetCertified.jpg" Visible="False"></asp:ImageButton>
									<asp:ImageButton id="btnMenu" runat="server" ImageUrl="../Main/images/btn_tutorialmenu.jpg" Visible="False"></asp:ImageButton></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 511px"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
