<%@ Page language="c#" Codebehind="Logon.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.dPiTeleconnect" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="main/control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="main/control/SiteFooter.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Logon</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="main/Styles/Navigator.css" rel="stylesheet">
		<LINK href="main/Styles/DPI.css" rel="stylesheet">
	</HEAD>
	<BODY text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0">
		<TABLE cellSpacing="0" cellPadding="0" width="800" border="0">
			<tr>
				<td vAlign="bottom" align="right" background="main/images/header.jpg" colSpan="1" height="101"
					rowSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
			</tr>
			<tr>
				<td><IMG src="main/images/logon_banner.jpg" border="0"></td>
			</tr>
			<TR>
				<TD>
					<FORM id="dPiTeleconnect" method="post" runat="server">
						<TABLE cellSpacing="0" cellPadding="0" align="center" border="0">
							<TBODY>
								<tr>
									<td rowSpan="4"><IMG height="233" src="main/images/logon_logo.jpg" width="186" border="0"></td>
									<td><IMG height="39" src="main/images/logon_top.jpg" border="0"></td>
								</tr>
								<TR>
									<TD align="center" background="main/images/logon_back.jpg" height="142"><INPUT id="txtUserName" type="text" name="txtUserName" runat="server" tabindex="1"><ASP:REQUIREDFIELDVALIDATOR id="Requiredfieldvalidator1" runat="server" ControlToValidate="txtUserName" Display="Static"
											ErrorMessage="*"></ASP:REQUIREDFIELDVALIDATOR><br>
										User ID
										<br>
										<INPUT id="txtUserPass" type="password" name="txtUserPass" runat="server" tabindex="2"><ASP:REQUIREDFIELDVALIDATOR id="Requiredfieldvalidator2" runat="server" ControlToValidate="txtUserPass" Display="Static"
											ErrorMessage="*"></ASP:REQUIREDFIELDVALIDATOR><br>
										Password
									</TD>
								</TR>
								<tr>
									<td align="center" background="main/images/logon_backbtn.jpg" height="35">&nbsp;
										<asp:imagebutton id="cmdLogin" runat="server" ImageUrl="main/images/submit.jpg" tabindex="3"></asp:imagebutton></td>
								</tr>
								<tr>
									<td background="main/images/logon_bottom.jpg" height="17">&nbsp;</td>
								</tr>
							</TBODY>
						</TABLE>
					</FORM>
				</TD>
			</TR>
			<tr>
				<td align="center" colspan="2" height="30"><asp:label id="lblMsg" runat="server" ForeColor="red" Font-Name="Verdana" Font-Size="10"></asp:label></td>
			</tr>
			<tr>
				<td align="center" colspan="2">This 
						website is best viewed by using Internet Explorer Version 5 or higher.<br>
						<br>
						This site also requires the use of Adobe Acrobat Reader. If you do not have the 
						Adobe Acrobat Reader,<br>
						you can use the button below to go to the Adobe Acrobat website to download a 
						copy and install<br>
						<a href="http://www.adobe.com/products/acrobat/readstep2.html">
						<IMG alt="Adobe Reader" src="Main/images/get_adobe_reader.gif" border="0"></a></td>
			</tr>
		</TABLE>
		<script>
		window.dPiTeleconnect.txtUserName.focus();
		</script>
	</BODY>
</HTML>
