<?

foreach (glob("q-agent_bin/Q-Agent.ppc2003_v*.exe") as $filename){
	$filename2003 = $filename;
}

$version2003 = str_replace("q-agent_bin/Q-Agent.ppc2003_v", "", $filename2003);
$version2003 = str_replace(".exe", "", $version2003);


$errorMessages = array();

if (isset($_POST['submit'])){

	if (@$_POST["name"] == "")
		array_push($errorMessages, "Please enter your name");

	if (@$_POST["email"] == "")
		array_push($errorMessages, "Please enter email address");
	else if (!eregi("^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$", @$_POST["email"]))
		array_push($errorMessages, "Please enter valid email address");


	if (count($errorMessages) == 0) {//Send email

		$url       = "q-agent.download.php";

		$recipient = "info@affilia.com";

    		$name      = (@$_POST['name']);

    		$email     = (@$_POST['email']);

    		$phone     = (@$_POST['phone']);

    		$company   = (@$_POST['company']);

    		$message   = (@$_POST['message']);


    		$full_message   = "This person has been requested Q-Agent trial download.\r\n Personal information:\r\n Name - " . $name . "\r\n E-mail - " . $email . "\r\n Phone - " . $phone . "\r\n Company - " . $company . "\r\n Notes:\r\n----------------------------------------\r\n" . $message ."\r\n----------------------------------------\r\n";

    		$sendform  = mail("$recipient", "Q-Agent trial download", "$full_message", "From: $recipient (Q-Agent trial download)");

		if ($sendform)
			header("Location: $url");								
		else
    			echo "<li><font color='Red'>We are sorry. Cannot process your request right now. Try again later</font></li>";
	}


}

?>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
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





td.field-name{

  text-align: left;

  font-family: Arial, sans-serif;

  font-size: 11px;

  font-weight: normal;

  color: #999999;

}



input {

  border: 1px solid;

  border-color: #d7d7d7;

  font-family: Arial, sans-serif;

  font-size: 11px;

  font-weight: normal;

  color: #616161;

}



input.button {

  border: 0px solid;

  background-color: ff4200;

  font-family: Arial, sans-serif;

  font-size: 13px;

  font-weight: bold;

  color: #ffffff;

}



</style>



</head>



<body>



<!-- header begin -->

<table width="100%" cellpadding="0" cellspacing="0">

  <tr>

    <td class="header-top-panel">&nbsp;<a name="PageTop"></a></td>

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

    <td><img src="images/bg.gif" width="50" height="50"></td>
    <td colspan="2"><a style="font-size:11px;font-family:Arial,sans-serif;" href="./products.html">Products</a>
    <span style="font-size:11px;color:#616161;font-family:Arial,sans-serif;">/</span>
    <a style="font-size:11px;font-family:Arial,sans-serif;" href="./q-agent.php">Q-Agent</a>
    <span style="font-size:11px;color:#616161;font-family:Arial,sans-serif;">/&nbsp;Download</span>
    </td>
  </tr>

  <tr>

    <td><img src="images/bg.gif" width="117" height="2"></td>

    <td width="100%">

      <h3>Download (version <?echo $version2003?> for Pocket PC)</h3>      

      <div class="panel">

        <table cellpadding="0" cellspacing="0">

          <tr>

            <td background="images/panel_top_back.gif" width="100%"></td>

            <td><img src="images/panel_top_right_corner.gif" width="12" height="24"></td>

          </tr>

          <tr>

            <td width="100%">

              <table cellpadding="0" cellspacing="0" width="100%">

                <tr valign="top">

                  <td colspan="3">
                  
                    <table cellpadding="0" cellspacing="10" width="755">
                    
                        <tr>
                    
                            <td>

                                <h4>Please fill the fields below</h4>

                               
                                <ul>
					<?
					if (count($errorMessages)!== 0)
						foreach ($errorMessages as $message) {
						   echo "<li><font color='Red'>$message</font></li>";
						}

					?>
                                </ul>
                                
                            </td>
                            
                            <td>&nbsp;</td>
                                
                        </tr>
                        <tr>
                    
                            <td>

                    <form method="post" action="q-agent.download.register.php">

                      <table cellpadding="0" cellspacing="3" style="border: 0px solid #ff0000; margin: 0px; padding: 0px" id="table1">

                        <tr valign="top">

                          <td class="field-name">Name&nbsp;<font color="Red">*<font></td>

                          <td style="border: 0px solid #ff0000; margin: 0px; padding: 0px">
							<input name="name" style="border:1px solid #d7d7d7; width:115px; font-family:Arial, sans-serif; font-size:11px; font-weight:normal; color:#616161" maxlength="50" value="<?php echo @htmlentities($_POST['name'])?>"></td>

                          <td style="border: 0px solid #ff0000; margin: 0px; padding: 0px"><img src="images/bg.gif" width="40" height="5"></td>

                          <td class="field-name">Company &nbsp;</td>

                          <td style="border: 0px solid #ff0000; margin: 0px; padding: 0px">
							<input name="company" style="border:1px solid #d7d7d7; width:115px; font-family:Arial, sans-serif; font-size:11px; font-weight:normal; color:#616161" maxlength="50" value="<?php echo @htmlentities($_POST['company'])?>"></td>

                        </tr>

                        <tr valign="top">

                          <td colspan="5" style="border: 0px solid #ff0000; margin: 0px; padding: 0px"><img src="images/bg.gif" width="5" height="5"></td>

                        </tr>

                        <tr valign="top">

                          <td class="field-name">Email&nbsp;<font color="Red">*<font></td>

                          <td style="border: 0px solid #ff0000; margin: 0px; padding: 0px">
							<input name="email" style="border:1px solid #d7d7d7; width:115px; font-family:Arial, sans-serif; font-size:11px; font-weight:normal; color:#616161" maxlength="150" value="<?php echo @htmlentities($_POST['email'])?>"></td>

                          <td style="border: 0px solid #ff0000; margin: 0px; padding: 0px"><img src="images/bg.gif" width="40" height="5"></td>

                          <td class="field-name">Phone</td>

                          <td style="border: 0px solid #ff0000; margin: 0px; padding: 0px">
							<input name="phone" style="border:1px solid #d7d7d7; width:115px; font-family:Arial, sans-serif; font-size:11px; font-weight:normal; color:#616161" maxlength="50" value="<?php echo @htmlentities($_POST['phone'])?>"></td>

                        </tr>

                        <tr valign="top">

                          <td colspan="5" style="border: 0px solid #ff0000; margin: 0px; padding: 0px"><img src="images/bg.gif" width="5" height="5"></td>

                        </tr>

                        <tr valign="top">

                          <td class="field-name">Note</td>

                          <td colspan="4" style="border: 0px solid #ff0000; margin: 0px; padding: 0px">
							<textarea name="message" style="border:1px solid #d7d7d7; width:330px;height:105px; font-family:Arial, sans-serif; font-size:11px; font-weight:normal; color:#616161" rows="1" cols="20"><?php echo @htmlentities($_POST['message'])?></textarea></td>

                        </tr>

                        <tr valign="top">

                          <td align="right" colspan="5" style="border: 0px solid #ff0000; margin: 0px; padding: 0px">

                            <input type="submit" name="submit" style="width:80px;" value="SEND" class="button"/>

                            <input type="hidden" name="do" value="send" style="font-family: Arial, sans-serif; font-size: 11px; font-weight: normal; color: #616161; border: 1px solid #d7d7d7"/></td>

                        </tr>

                      </table>

                    </form>

                  				<p>&nbsp;</td>
                            
                            <td>&nbsp;</td>
                                
                        </tr>                        
                        
                        </table>

                  </td>

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

          <td nowrap><img src="images/bg.gif" width="15" height="2"></td>
          
          <td nowrap class="invisible-menu">[ Products ]</td>

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

