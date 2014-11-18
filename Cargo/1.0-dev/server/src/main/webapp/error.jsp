<html>
<head>
<title>Login</title>

<link rel="stylesheet" href="/cargo/com.affilia.cargo.Cargo/cargo.css" type="text/css" media="screen, print"/>

</head>
<body>
<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" class="bodyTable">
<tr>
  <td align="center" valign="middle"><br />
    <h3><img src="/cargo/com.affilia.cargo.Cargo/css/error.png" width="32" height="32" align="absmiddle"> Login Error</h3>
    <form name="loginForm" method="POST" action='<%= response.encodeURL("j_security_check")%>'>
      <table width="220 px" cellspacing="0" cellpadding="4" class="formaw">
        <tr>
          <td colspan="3" class="tableHeading">Login to Cargo System </td>
        </tr>
        <tr>
          <td colspan="3" height="3px;"> </td>
        </tr>

        <tr>
          <td align="right">User:</td>
          <td><input type="text" size="15" name="j_username" class="inputShadow"></td>
          <td><div align="left"><img src="/cargo/com.affilia.cargo.Cargo/css/user.png" width="24" height="24"></div></td>
        </tr>
        <tr>
          <td align="right">Password:</td>
          <td><input type="password" size="15" name="j_password" class="inputShadow"></td>
          <td><div align="left"><img src="/cargo/com.affilia.cargo.Cargo/css/pas.png" width="24" height="24"></div></td>
        </tr>
        <tr>
          <td colspan="3" align="center"><table cellpadding="0" cellspacing="0" align="center">
              <tr>
                <td><img src="/cargo/com.affilia.cargo.Cargo/css/go.png" width="24" height="24"></td>
                <td><input type="submit" value="Login">
                </td>
              </tr>
            </table></td>
        </tr>
      </table>
    </form>
    <h5><a href='<%= response.encodeURL("index.jsp")%>'>Home</a></h5>
    <script language="JavaScript" type="text/javascript">
         document.forms["loginForm"].elements["j_username"].focus()
    </script>
</body>
</html>