<%@ Page language="c#" Codebehind="GenericError.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.GenericError" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>GenericError</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Main/Styles/Navigator.css" rel="stylesheet">
		<LINK href="Main/Styles/DPI.css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td><asp:Image id="Image1" runat="server" ImageUrl="Main/images/header.jpg"></asp:Image></td>
				</tr>
				<tr>
					<td align="center"><font class="05_con_bold_big">
							<P>&nbsp;</P>
							<P>
								<asp:Label id="Label2" runat="server">We're sorry. There has been an error processing the page.<br> Please click the button below to return to the main menu.</asp:Label>
								<HR width="90%" SIZE="1">
							</P>
							<P>&nbsp;<asp:ImageButton id="ImageButton1" runat="server" ImageUrl="Main/images/btn_gotomain.jpg"></asp:ImageButton><br>
							</P>
							<P>&nbsp;</P>
						</font>
					</td>
				</tr>
			</table>
		</form>
		</FONT>
	</body>
</HTML>
