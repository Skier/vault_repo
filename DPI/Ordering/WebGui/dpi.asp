<%@ Language=VBScript%>
<%@ Language=VBScript%>
<%
'	option explicit
'	Response.Buffer = true
'	Response.Expires = 10
'	Dim ipAddress
'	Dim locLoop
'	Dim DB
'	Dim rs
'	Dim strsql
'
'	ipAddress = Request.ServerVariables("REMOTE_ADDR")
'	locLoop = 1
'	if (mid(ipAddress, 1, 5) <> "10.1.") Then
'		if (mid(right(ipAddress, 3), 1, 1) = ".") Then
'			While (mid(ipAddress, locLoop, 1) <> ".")
'				locLoop = locLoop + 1
'			Wend
'			locLoop = locLoop + 1
'			While (mid(ipAddress, locLoop, 1) <> ".")
'				locLoop = locLoop + 1
'			Wend
'			locLoop = locLoop + 1
'			While (mid(ipAddress, locLoop, 1) <> ".")
'				locLoop = locLoop + 1
'			Wend
'			ipAddress = mid(ipAddress, 1, locLoop)
'		end if		
'	else
'		ipAddress = ipAddress
'	end if
'
'	Set DB = Server.CreateObject("ADODB.Connection")
'	DB.ConnectionString = Application("SQLProd")
'	DB.Open
'	strsql = "select netip from ipaddress where netipnew like '" & ipAddress & "%'"
'	Set rs = DB.Execute(strsql)
'	if (Not rs.eof) Then
'		ipAddress = rs("netip")
'	end if
%>
<HTML>
<HEAD>
</HEAD>
<BODY onload="document.forms['dpi'].submit();">
<!---<form method="POST" id="Form1" name="dpi" action="http://auth-dev.dpiteleconnect.com/wotest/autologon.aspx">--->
<form method="POST" id="dpi" name="dpi" action="autologon.aspx">
<input type="hidden" name="IP"	id="IP"	value='<%= 'ipAddress %>'>
</form>
</BODY>
</HTML>