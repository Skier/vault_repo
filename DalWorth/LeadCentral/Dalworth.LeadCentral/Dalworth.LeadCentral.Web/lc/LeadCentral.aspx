<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeadCentral.aspx.cs" Inherits="Dalworth.LeadCentral.Web.lc.LeadCentral" %>
<!-- saved from url=(0014)about:internet -->
<html lang="en">

<!-- 
Smart developers always View Source. 

This application was built using Adobe Flex, an open source framework
for building rich Internet applications that get delivered via the
Flash Player or to desktops via Adobe AIR. 

Learn more about Flex at http://flex.org 
// -->

<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

<!--  BEGIN Browser History required section -->
<link rel="stylesheet" type="text/css" href="history/history.css" />
<!--  END Browser History required section -->

<title></title>
<script src="AC_OETags.js" language="javascript"></script>

<!--  BEGIN Browser History required section -->
<script src="history/history.js" language="javascript"></script>
<!--  END Browser History required section -->

<style>
body { margin: 0px; overflow:hidden }
</style>
<script language="JavaScript" type="text/javascript">
<!--
//Initialize flash variables for use with QuickBase
var mydbid = "";
var myqburl = "";
var urlrest = "";

var querystring = new String(document.location).split('?')[1];
var dbIdString = document.location.href;
var dbIndex = dbIdString.indexOf("db");

if (dbIndex > 0) {
        mydbid = dbIdString.substring(dbIndex + 3,dbIndex+12);
        myqburl = dbIdString.substring(0,dbIndex-1);
}
if (document.domain != "") {
        document.domain=document.domain;
}
// -----------------------------------------------------------------------------
// Globals
// Major version of Flash required
var requiredMajorVersion = 9;
// Minor version of Flash required
var requiredMinorVersion = 0;
// Minor version of Flash required
var requiredRevision = 28;
// -----------------------------------------------------------------------------
// ExternalInterface functions
function doUserMgmt()
{
        LeadCentral.doUserMgmt();
}
function canDoUserMgmt()
{
        //change to false if your app does NOT support user management functions.
        return true;
}
function doAddUser()
{
        LeadCentral.doAddUser();
}
function useNativeUserMgmt()
{
    //change to false if you don't want to control user management entirely in your app
        return true;
}
function updateUserData()
{
        LeadCentral.updateUserData()
}

function setDirty(isDirty)
{ 
        var dirty = "false";
        if (isDirty) {
            dirty = "true";
        }
        
    //The name of the frame
    var id = "proxyframe";
 
    //Look for existing frame with name "proxyframe"
    var proxy = frames[id];
 
    //Set URL and querystring
    var url = myqburl + "/iFrameProxy.html?a=setDirty&dirty=" + dirty;
 
    //If the proxy iframe has already been created
    if(proxy){
        //Redirect to the new URL
        proxy.location.href = url;
    } else {
        //Create the proxy iframe element.
        var iframe = document.createElement("iframe");
        iframe.id = id;
        iframe.name = id;
        iframe.src = url;
        iframe.style.display = "none";
        document.body.appendChild(iframe);
    }
}

// -->
</script>
<script src="/js/QBmenu.js" language="javascript"></script>
</head>

<body scroll="no">
<div id="QBheader"></div>
<div id="QBcontent">
<script language="JavaScript" type="text/javascript">
<!--


// Version check for the Flash Player that has the ability to start Player Product Install (6.0r65)
var hasProductInstall = DetectFlashVer(6, 0, 65);

// Version check based upon the values defined in globals
var hasRequestedVersion = DetectFlashVer(requiredMajorVersion, requiredMinorVersion, requiredRevision);

if ( hasProductInstall && !hasRequestedVersion ) {
        // DO NOT MODIFY THE FOLLOWING FOUR LINES
        // Location visited after installation is complete if installation is required
        var MMPlayerType = (isIE == true) ? "ActiveX" : "PlugIn";
        var MMredirectURL = window.location;
    document.title = document.title.slice(0, 47) + " - Flash Player Installation";
    var MMdoctitle = document.title;

        AC_FL_RunContent(
                "src", "playerProductInstall",
                "FlashVars", 't=<%=Ticket%>&r=<%=RealmId%>&d=<%=DbId%>&dbidRoot='+mydbid+'&qbUrl='+myqburl+'&' + querystring + '&MMredirectURL='+MMredirectURL+'&MMplayerType='+MMPlayerType+'&MMdoctitle='+MMdoctitle+"",
                "width", "100%",
                "height", "100%",
                "align", "middle",
                "id", "LeadCentral",
                "quality", "high",
                "bgcolor", "#869ca7",
                "name", "LeadCentral",
                "allowScriptAccess","sameDomain",
                "type", "application/x-shockwave-flash",
                "pluginspage", "http://www.adobe.com/go/getflashplayer",
                "wmode", "transparent"  
        );
} else if (hasRequestedVersion) {
        // if we've detected an acceptable version
        // embed the Flash Content SWF when all tests are passed
        AC_FL_RunContent(
                        "src", "LeadCentral",
                        "FlashVars", 't=<%=Ticket%>&r=<%=RealmId%>&d=<%=DbId%>&dbidRoot='+mydbid+'&qbUrl='+myqburl+"&"+querystring,
                        "width", "100%",
                        "height", "100%",
                        "align", "middle",
                        "id", "LeadCentral",
                        "quality", "high",
                        "bgcolor", "#869ca7",
                        "name", "LeadCentral",
                        "allowScriptAccess","sameDomain",
                        "type", "application/x-shockwave-flash",
                        "pluginspage", "http://www.adobe.com/go/getflashplayer",
                        "wmode", "transparent"
        );
  } else {  // flash is too old or we can't detect the plugin
    var alternateContent = 'Alternate HTML content should be placed here. '
        + 'This content requires the Adobe Flash Player. '
        + '<a href=http://www.adobe.com/go/getflash/>Get Flash</a>';
    document.write(alternateContent);  // insert non-flash content
  }
// -->
</script>
<noscript>
        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"
                        id="LeadCentral" width="100%" height="100%"
                        codebase="http://fpdownload.macromedia.com/get/flashplayer/current/swflash.cab">
                        <param name="movie" value="LeadCentral.swf" />
                        <param name="quality" value="high" />
                        <param name="bgcolor" value="#869ca7" />
                        <param name="allowScriptAccess" value="sameDomain" />
                        <embed src="LeadCentral.swf" quality="high" bgcolor="#869ca7"
                                width="100%" height="100%" name="LeadCentral" align="middle"
                                play="true"
                                loop="false"
                                quality="high"
                                allowScriptAccess="sameDomain"
                                type="application/x-shockwave-flash"
                                pluginspage="http://www.adobe.com/go/getflashplayer"
                                wmode="transparent">
                        </embed>
        </object>
</noscript>
</div>
</body>
</html>
