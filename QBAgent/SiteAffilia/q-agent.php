<?phpforeach (glob("q-agent_bin/Q-Agent.ppc2003_v*.exe") as $filename){	$filename2003 = $filename;}$version2003 = str_replace("q-agent_bin/Q-Agent.ppc2003_v", "", $filename2003);$version2003 = str_replace(".exe", "", $version2003);?><!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>

<head>
<title>Products [Affilia Inc.]</title>

<style type="text/css">

body {
  margin: 0;
  padding: 0;
  font-family: Verdana, sans-serif;
}

p,ul,li {
  text-align: left;
  font-family: Arial, sans-serif;
  font-size: 12px;
  color: #616161;
}

table, td {
  border: 0px solid;
  border-color: #ff0000;
  margin: 0px,0px,0px,0px;
  padding: 0px,0px,0px,0px;
}

h3 {
  text-align: left;
  font-family: Arial, sans-serif;
  font-size: 20px;
  font-weight: normal;
  color: #ff4200;
  line-height: 0.4cm;
}

h4 {
  text-align: left;
  font-family: Arial, sans-serif;
  font-size: 17px;
  font-weight: bold;
  color: #6fa5d7;
  line-height: 0.5cm;
}

table.selected-cell {
  border: 1px solid;
  border-color:#ffffff;
  margin: 0px,0px,0px,0px;
  padding: 0px,0px,0px,0px;
  background-color: #ff4200;
  width: 100%;
  height: 100%;
}

table.logo-panel {
  background-image: url('images/logo_small_back.gif');
  height: 92px;
}

table.blue-main {
  background-image: url('images/blue_small_back.png');
  height: 76px;
}

table.blue-left {
  background-image: url('images/blue_small_left.png');
  background-repeat: no-repeat;
  height: 100%;
}

table.blue-right {
  background-image: url('images/blue_small_right.png');
  background-repeat: no-repeat;
  background-position: right;
  height: 100%;
}

tr.bottom-line {
  background-image: url('images/bottom_gray_back.gif');
  height: 9px;
  width: 100%;
}

tr.3px-height {
  height: 3px;
}

.top-menu-panel {
  text-align: center;
  font-size: 10px;
  font-weight: bold;
  color: #ababab;
}

.invisible-menu {
  font-size: 11px;
  font-weight: bold;
  color: #ffffff;
  background-color: #ffffff;
}

a:link {
  font-size: 11px;
  color: #3a96ff;
  text-decoration: underline;
}

a:visited {
  font-size: 11px;
  color: #3a96ff;
  text-decoration: underline;
}

a:hover {
  font-size: 11px;
  color: #3a96ff;
  text-decoration: underline;
}

a.main-menu:link {
  font-size: 11px;
  font-weight: bold;
  color: #ffffff;
  text-decoration: none;
}

a.main-menu:visited {
  font-weight: bold;
  color: #ffffff;
  text-decoration: none;
}

a.main-menu:hover {
  color: #dd3333;
}

.main-menu-selected {
  text-align: center;
  font-size: 11px;
  font-weight: bold;
  color: #0d4d9d;
}

a.top-menu-panel:link {
  text-align: center;
  font-size: 10px;
  font-weight: bold;
  color: #ababab;
  text-decoration: none;
}

a.top-menu-panel:visited {
  text-align: center;
  font-size: 10px;
  font-weight: bold;
  color: #ababab;
  text-decoration: none;
}

a.top-menu-panel:hover {
  text-align: center;
  font-size: 10px;
  font-weight: bold;
  color: #ff4200;
  text-decoration: none;
}

.top-menu-panel-selected {
  text-align: center;
  font-size: 10px;
  font-weight: bold;
  color: #ababab;
  text-decoration: none;
}

td.header-top-panel {
  background-image: url('images/top_gray-white_back.gif');
  height: 25px;
  width: 100%;
}

div.panel {
  padding : 0,0,0,0; 
  width : 100%;
  background-color: #ffffff;
}


</style>

</head>

<body>

<!-- header begin -->
<table width="100%" cellpadding="0" cellspacing="0">
  <tr>
    <td class="header-top-panel">&nbsp;</td>
  </tr>
  <tr>
    <td>
      <table cellpadding="0" cellspacing="0" class="logo-panel">
        <tr valign="bottom">
          <td><img src="images/bg.gif" width="100" height="92"></td>
          <td><img src="images/logo_small.gif" width="162" height="92"></td>
          <td valign="bottom"><img src="images/slogan.gif" width="280" height="92"></td>
          <td width="100%">&nbsp;</td>
          <td>
            <table cellpadding="0" cellspacing="0">
              <tr>
                <td></td>
                <td class="top-menu-panel"><a href="about.html"><img src="images/about_color.gif" width="24" height="16" border="0"></a></td>
                <td></td>
                <td class="top-menu-panel"><a href="contact.html"><img src="images/contact_color.gif" width="25" height="16" border="0"></a></td>
                <td></td>
              </tr>
              <tr>
                <td colspan="5"><img src="images/bg.gif" width="10" height="4"></td>
              </tr>
              <tr>
                <td class="top-menu-panel">|</td>
                <td><a href="about.html" class="top-menu-panel">&nbsp;&nbsp;&nbsp;About&nbsp;&nbsp;&nbsp;</a></td>
                <td class="top-menu-panel">|</td>
                <td nowrap><a href="contact.html" class="top-menu-panel">&nbsp;&nbsp;&nbsp;Contact Us&nbsp;&nbsp;&nbsp;</a></td>
                <td class="top-menu-panel">|</td>
              </tr>
              <tr>
                <td colspan="5"><img src="images/bg.gif" width="10" height="4"></td>
              </tr>
              <tr class="3px-height">
                <td colspan="5"><img src="images/bg.gif" width="10" height="4"></td>
              </tr>
            </table>
          </td>
          <td><img src="images/bg.gif" width="143" height="16"></td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td>
      <table class="blue-main" width="100%" cellpadding="0" cellspacing="0">
        <tr>
          <td>
            <table class="blue-left" width="100%" cellpadding="0" cellspacing="0">
              <tr>
                <td>
                  <table class="blue-right" width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                      <td valign="bottom">
                        <table cellpadding="0" cellspacing="0" width="100%">
                          <tr>
                            <td nowrap><img src="images/bg.gif" width="117" height="2"></td>
                            <td nowrap><a href="index.html" class="main-menu">[ Home ]</a></td>
                            
                            <td nowrap><img height="2" src="images/bg.gif" width="15"></td>
                              
                            <td nowrap><a href="services.html" class="main-menu">[ Services ]</a></td>
                            <td nowrap><img src="images/bg.gif" width="15" height="2"></td>
                            <td nowrap class="main-menu-selected">[ Products ]</td>
                            <td nowrap><img src="images/bg.gif" width="15" height="2"></td>
                            <td nowrap><a href="approach.html" class="main-menu">[ Approach ]</a></td>
                            <td nowrap><img src="images/bg.gif" width="15" height="2"></td>
                            <td nowrap><a href="capabilities.html" class="main-menu">[ Capabilities ]</a></td>
                            <td nowrap><img src="images/bg.gif" width="15" height="2"></td>
                            <td nowrap><a href="technologies.html" class="main-menu">[ Technologies ]</a></td>
                            <td nowrap><img src="images/bg.gif" width="15" height="2"></td>
                            <td nowrap><a href="clients.html" class="main-menu">[ Clients ]</a></td>
                            <td nowrap width="100%"><img src="images/bg.gif" width="15" height="2"></td>
                          </tr>
                          <tr>
                            <td colspan="15"><img src="images/bg.gif" width="10" height="18"></td>
                          </tr>
                          <tr>
                            <td><img src="images/bg.gif" width="117" height="2"></td>
                            <td colspan="4"><img src="images/bg.gif" width="10" height="2"></td>
                            <td><table cellpadding="0" cellspacing="0" class="selected-cell"><tr><td><img src="images/bg.gif" width="10" height="8"></td></tr></table></td>
                            <td colspan="9" width="100%"><img src="images/bg.gif" width="10" height="2"></td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </td>
  </tr>
</table>

<!-- header end -->

<!-- content begin -->
<table cellpadding="0" cellspacing="0">
  <tr>
    <td><img src="images/bg.gif" width="50" height="50"></td>    <td colspan="2"><a style="font-size:11px;font-family:Arial,sans-serif;" href="./products.html">Products</a>
    <span style="font-size:11px;color:#616161;font-family:Arial,sans-serif;">/&nbsp;Q-Agent</span>
    </td>
  </tr>
  <tr>
    <td><img src="images/bg.gif" width="117" height="2"></td>
    <td width="100%">
      <h3>Q-Agent</h3>
      <div class="panel">
        <table cellpadding="0" cellspacing="0">
          <tr>
            <td background="images/panel_top_back.gif" width="100%"></td>
            <td><img src="images/panel_top_right_corner.gif" width="12" height="24"></td>
          </tr>
          <tr>
            <td width="100%">
              <table cellpadding="0" cellspacing="0">
                <tr valign="top">
                  <td>
                    <p>If you are					<a style="font-size:12px" href="http://quickbooks.intuit.com/">					Quick Books</a><font style="font-size: 9pt"> </font>customer 					and using					<a style="font-size:12px" href="http://oe.quickbooks.com/">					Quick Books Online</a> this solution intended for you. It 					allows you to keep and manage your accounting data from the 					Pocket PC device wherever you go. You can manage time 					sheets, create invoices, make banking operations and even 					more. At any time you can synchronize handheld data with 					your Quick Books Online account straightly from the Pocket 					PC without using desktop computer via WiFi or GPRS 					connection.                                        <ul>
                      <li><a style="font-size:12px" href="./q-agent.details.html">Details</a></li>
                      <li><a style="font-size:12px" href="./q-agent.install.html">Installation manual</a></li>
                      <li><a style="font-size:12px" href="./q-agent.download.register.php">Download (version <?echo $version2003?> for Pocket PC)</a></li>

                    </ul>                  </td>
                </tr>
              </table>
            </td>
            <td background="images/panel_right_back.gif"></td>
          </tr>
          <tr>
            <td width="100%" background="images/panel_bottom_back.gif"></td>
            <td><img src="images/panel_bottom_right_corner.gif" width="12" height="24"></td>
          </tr>
        </table>
      </div>
    </td>
    <td><img src="images/bg.gif" width="50" height="2"></td>
  </tr>
</table>
<!-- content end -->

<!-- footer begin -->
<table cellpadding="0" cellspacing="0" width="100%">
  <tr>
    <td><img src="images/bg.gif" width="5" height="60"></td>
  </tr>
  <tr>
    <td>
      <table cellpadding="0" cellspacing="0">
        <tr>
          <td nowrap><img src="images/bg.gif" width="117" height="2"></td>
          <td nowrap class="invisible-menu">[ Home ]</td>
          <td nowrap><img src="images/bg.gif" width="15" height="2"></td>
          <td nowrap class="invisible-menu">[ Services ]</td>
          <td nowrap><img src="images/bg.gif" width="15" height="2"></td>                    <td nowrap class="invisible-menu">[ Products ]</td>
          <td nowrap><img src="images/bg.gif" width="15" height="2"></td>
          <td nowrap class="invisible-menu">[ Approach ]</td>
          <td nowrap><img src="images/bg.gif" width="15" height="2"></td>
          <td nowrap class="invisible-menu">[ Capabilities ]</td>
          <td nowrap><img src="images/bg.gif" width="15" height="2"></td>
          <td nowrap class="invisible-menu">[ Technologies ]</td>
          <td nowrap><img src="images/bg.gif" width="15" height="2"></td>
          <td nowrap class="invisible-menu">[ Clients ]</td>
          <td nowrap width="100%"><img src="images/bg.gif" width="15" height="2"></td>
        </tr>
        <tr class="bottom-line">
          <td colspan="5"><img src="images/bg.gif" width="9" height="9"></td>
          <td><table cellpadding="0" cellspacing="0" class="selected-cell"><tr><td><img src="images/bg.gif" width="7" height="7"></td></tr></table></td>
          <td colspan="9"><img src="images/bg.gif" width="9" height="9"></td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td><img src="images/bg.gif" width="8" height="8"></td>
  </tr>
  <tr>
    <td nowrap><img src="images/bg.gif" width="117" height="9"><span 
      style="font-size:11px;color:#a8a8a8;font-family:Arial,sans-serif;font-weight:bold;"
      >&copy; 2006, Affilia Inc. </span><span 
      style="font-size:10px;color:#a8a8a8;font-family:Arial,sans-serif;"
      >All Rights Reserved.</span></td>
  </tr>
</table>
<!-- footer end -->
<br>

</body>

</html>
