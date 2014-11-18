<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN"
"http://www.w3.org/TR/html4/strict.dtd">

        <!--- #include virtual="/security/session.asp" --->
        <!--- #include virtual="/security/dbconn.asp" --->
        <!--- include virtual="/security/cryptoapi.asp" --->
        <!--- include virtual="/security/security.asp" --->
        <!--- #include virtual="/inc/GlobalVariables.asp" --->
<html>

    <head>
        <title>Titus - The Leader in Air Management</title>
        <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
        <link href="/inc/CssMenu2.css" rel="stylesheet" type="text/css"> 
        <script type="text/JavaScript" src="/inc/preload.js"></script>
        <script type="text/javascript" src="/inc/writeFlash.js"></script>

<style type="text/css">
/* GENERAL STYLES */
html,body {
    min-height: 100%;
    margin: 0;
    padding: 0;
/*  overflow:hidden;*/
/*min-height: 100%;*/
}
* html html,body {
    height: 100%;
}
body {
    color: #666666;
    background-color: #ffffff;
    font-family: Tahoma, Arial, Helvetica, sans-serif;
    font-size: 8pt;
}
a:link {
    font-family: Tahoma, Arial, Helvetica, sans-serif;
    font-size: 8pt;
    text-decoration: none;
    font-weight: bold;
    color: #6699cc;
    /*color: #eb7700;*/
}
a:visited {
    color: #6699cc;
    text-decoration: none;
    font-weight: bold;
}
a:hover {
    color: #eb7700;
    text-decoration: underline;
    font-weight: bold;
}
a:active {
    color: #6699cc;
    text-decoration: none;
    font-weight: bold;
}
h1, h2, h3, h4 {
    font-family: Tahoma, Geneva, sans-serif;
    margin-bottom: 10px;
    line-height: 1;
    /*margin-bottom: -10px;*/
}
h1.rule, h2.rule, h3.rule, h4.rule {
    width: 450px;
    border-bottom: 2px dotted #264b92;
}
h1 {
    font-size: 24pt;
}
h2 {
    font-size: 14pt;
}
h3 {
    font-size: 11pt;
    color: #93A5C9;
}
h4 {
    font-size: 11pt;
}
h5 {
    font-size: 10pt;
    margin-bottom: 5px;
}
h6 {
    font-size: 9pt;
}
h4.section {
    width: 300px;
    margin-bottom: 10px;
    border-bottom: 2px dotted #264b92;
}
h6.accent {
    color: #000000;
    font-weight: bold;
    background: url(/images/ch_more.gif) no-repeat 0 2px;
    padding-left: 15px;
    margin-top: 5px;
    margin-bottom: 0;
}
.txtAlert {
    color: #ee0000;
}
.download {
    background: url(/images/ch_more.gif) no-repeat 0px 2px;
    padding-left: 13px;
}




/* LAYOUT STYLES background-color: #dd0000;*/
.main {
    width: 980px;/*800*/
    margin-top: 0;
    margin-left: auto;
    margin-right: auto;
    min-height: 100%;
    background: #E6E6E6 url(/images/img_background2.jpg) repeat-y ;
}
* html .main {
    height: 100%;
}
.topBanner {
    float: left;
    clear: both;
    width: 980px;/*800*/
    height: 90px;
    
}
.topNavigation {
    float:left;
    clear:both;
    width: 980px;/*800*/
    height: 87px;
}
* html .topNavigation {
    margin-top: -2px;
}
.topNavMenu {
    width: 960px;/*780*/
    height: 62px;
    background-image: url(/images/topNav_background.jpg);
    margin-left: 10px;
}
.topNavMenuReps {
    width: 960px;/*780*/
    height: 62px;
    background-image: url(/images/topNav_background_mytitus.jpg);
    margin-left: 10px;
}
.topNavMenuHolder {
    padding-left: 211px; 
    padding-top: 40px;
    height: 17px;/*15*/
}
.topNavMenuHolder2 {
    padding-left: 9px; 
    padding-top: 40px;
    height: 17px;/*15*/
}
.topSubNavMenu {
    width: 960px;/*780*/
    height: 25px;
    padding-left: 10px;
}
/* topNav Menu */
.topNavItem {
    float: left;
    height: 17px;/*15*/
    background-image: url(/images/navMenuDisc.png);
    background-position: 0 1px;
    background-repeat: no-repeat;
    margin-right: 10px;
}
* html .topNavItem {
    background-image: none;
    filter: progid:DXImageTransform.Microsoft.AlphaImageLoader(src="/images/navMenuDisc.png", sizingMethod="crop");
    /*filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(src='/images/navMenuDisc.png');
    background-image: url(/images/navMenuDisc.gif);*/
}

a.topNav{
    padding-left: 21px;
    color: #999999;
    font-weight: bold;
    font-family: Trebuchet MS, Helvetica, sans-serif;
    font-size: 10pt;
    text-decoration: none;
}
a.topNav:visited {
    color: #999999;
    text-decoration: none;
}
a.topNav:hover {
    color: #ffffff; 
    text-decoration: none;
}
a.topNav:active {
    color: #999999;
    text-decoration: none;
}
.topSubNav a:link{
    color: #ffffff;
    font-weight: normal;
    font-family: Trebuchet MS, Helvetica, sans-serif;
    font-size: 10pt;
    text-decoration: none;
}
.topSubNav a:visited {
    color: #ffffff;
    font-weight: normal;
    font-size: 10pt;
    text-decoration: none;
}
.topSubNav a:hover {
    color: #eb7700; 
    font-weight: normal;
    font-size: 10pt;
    text-decoration: none;
}
.topSubNav a:active {
    color: #ffffff;
    font-weight: normal;
    font-size: 10pt;
    text-decoration: none;
}
.topSubNavItem {
    float: left;
    height: 15px;
    padding-left: 20px;
    padding-right: 20px;
    margin-top: 5px;
    border-right: 1px solid #ffffff;
}
.contentHolder {
    float: left;
    clear: both;
    width: 980px;/*800*/
    background: url(/images/contentHolderBG.gif) 10px repeat-y;
min-height: 100%;
}
* html .contentHolder {
    margin-top: -2px;
}
.contentNav {
    float:left;
    width:219px;
    margin-left: 10px;
}
* html .contentNav {
    margin-left: 5px;
}
.ecatNavSearch {
    height: 17px;
    margin-top: 5px;
}
* html .ecatNavSearch {
    margin-bottom: -13px;
}
.content {
    float: left;
    width: 726px;/*546*/
    margin-left: 0px;
/*min-height: 100%;*/
}
* html .content {
}
.padContent {
    padding:10px 20px 10px 20px;
}
.content li {
    list-style-image: url(/images/ch_more.gif);
}
.contentMainFlash {
    width:741px;/*561*/ 
    background:#000b1e;
}
.footer {
    width: 960px;/*780*/
    padding-left: 10px;
}
.hidden {
    display: none;
}
.sectionLink {
    display: block;
    font-family: Tahoma, Arial, Helvetica, sans-serif;
    font-size: 8pt;
    font-weight: bold;
    color: #5C86D8;
    margin-top: 0px;
    line-height: normal;
}
a.sectionLink:link {
    background: url(/images/arrowMenu_blue.gif) no-repeat 0px 6px;
    line-height: 1.9;
    font-size: 8pt;
    padding-left: 15px;
    margin-left: 0px;
    text-decoration: none;
    color: #5C86D8;
}
a.sectionLink:visited {
    background: url(/images/arrowMenu_blue.gif) no-repeat 0px 6px;
    line-height: 1.9;
    font-size: 8pt;
    padding-left: 15px;
    margin-left: 0px;
    text-decoration: none;
    color: #5C86D8;
}
a.sectionLink:hover {
    text-decoration: underline;
    color: #fe9400;
}
a.sectionLink:active {
    background: url(/images/arrowMenu_blue.gif) no-repeat 0px 6px;
    line-height: 1.9;
    font-size: 8pt;
    padding-left: 15px;
    margin-left: 0px;
    text-decoration: none;
    color: #5C86D8;
}
a.question{
    color: #000000;
    font-weight: bold;
    font-family: Trebuchet MS, Helvetica, sans-serif;
    font-size: 9pt;
    text-decoration: none;
}
a.question:visited {
    color: #000000;
    text-decoration: none;
}
a.question:hover {
    color: #000000; 
    text-decoration: underline;
}
a.question:active {
    color: #eb7700;
    text-decoration: none;
}
a.itemLink {
    display: block;
    padding-left: 15px;
    color: #6699cc;
    font-weight: bold;
    font-size: 10pt;
    text-decoration: none;
    background: url(/images/arrowMenu_blue.gif) 0 4px no-repeat;
}
a.itemLink:link {
    
}
a.itemLink:visited {
    color: #6699cc;
    text-decoration: none;
}
a.itemLink:hover {
    color: #eb7700; 
    text-decoration: underline;
}
a.itemLink:active {
    color: #6699cc;
    text-decoration: none;
}
.sideBarSectionLink {
    width: 190px;
    height: 24px;
    background: url(/images/arrowMenu.gif) no-repeat 5px 8px;
    font-family: Tahoma, Arial, Helvetica, sans-serif;
    font-size: 11px;
    font-weight: bold;
    color: #FFFFFF;
    margin-top: 1px;
    line-height: normal;
    padding-top: 5px;
    padding-left: 20px;
}
.leftCol340 {
    float: left;
    width: 500px;
}
.hand {
    cursor: pointer;
}
a.plain {
    color: #ffffff;
    text-decoration: none;
    font-weight: normal;
}
a.plain:visited {
    color: #ffffff;
    text-decoration: none;
    font-weight: normal;
}
a.plain:hover {
    color: #ffffff;
    text-decoration: none;
    font-weight: normal;
}
a.plain:active {
    color: #ffffff;
    text-decoration: none;
    font-weight: normal;        
}
a.plainBold {
    color: #ffffff;
    text-decoration: none;
    font-weight: bold;
}
a.plainBold:visited {
    color: #ffffff;
    text-decoration: none;
    font-weight: bold;
}
a.plainBold:hover {
    color: #ffffff;
    text-decoration: none;
    font-weight: bold;
}
a.plainBold:active {
    color: #ffffff;
    text-decoration: none;
    font-weight: bold;      
}   
.sideBarSectionDocument {
    line-height: 24px;
    width: 166px;
    margin-top: 1px;
    margin-left: 10px;
    padding-left: 10px;
    background-color: #294680;
}


.spotlight {
margin:7px 7px 17px 2px;
padding:3px;
cursor:pointer;
}
.spotlight:hover {
color:black;
background:#f8f8f8;
}
.spotimg {
float:left;
height:100%;
width:74px;
margin-right:10px;
}
.ota {
margin-left:20px;
}
.otc {
margin-left:30px;
}
.sectionLink:hover {
color:#EB7700;
}
.sectionH {
padding-bottom:3px;
margin-bottom:0;
border-bottom-color:#b2a6b3;
border-bottom-style:dotted;
border-bottom-width:2px;
}

.sideBarSection2 {
   background-color:#264A92;
   width:166px;
    height: 24px;
    cursor:pointer;
    background: url(/images/plusMenu.gif) #264A92 no-repeat 5px 8px;
    font-family: Tahoma, Arial, Helvetica, sans-serif;
    font-size: 11px;
    font-weight: bold;
    color: #FFFFFF;
    margin-top: 1px;
    line-height: normal;
    padding-top: 5px;
    padding-left: 20px;
}
.sideBarSection2open {
   background-color:red;
   width:166px;
    height: 24px;
    cursor:pointer;
    background: url(/images/arrowdownMenu.gif) #5cb2f2 no-repeat 5px 8px;
    font-family: Tahoma, Arial, Helvetica, sans-serif;
    font-size: 11px;
    font-weight: bold;
    color: #FFFFFF;
    margin-top: 1px;
    line-height: normal;
    padding-top: 5px;
    padding-left: 20px;
}

#topBanner {
position:relative;
}
#location {
color:#fff;
position:absolute;
top:59px;
left:247px;
width:500px;
border-top:1px solid #5e7fa0;
    font-family: "Trebuchet MS", Tahoma, Arial, Helvetica, sans-serif;
}
#crumbsCont {
display:none;
}
</style>

<script src="AC_OETags.js" language="javascript"></script>

<script language="JavaScript" type="text/javascript">
<!--
// -----------------------------------------------------------------------------
// Globals
// Major version of Flash required
var requiredMajorVersion = 9;
// Minor version of Flash required
var requiredMinorVersion = 0;
// Minor version of Flash required
var requiredRevision = 0;
// -----------------------------------------------------------------------------
// -->
</script>

<script>
<!--

var lastX = 0;
var lastY = 0;
var lastW = 0;
var lastH = 0;

function init() {
    window.onresize = onResize;
    setAppSize();
}

function onResize(e) {
    moveIFrame(lastX,lastY,lastW,lastH);
    setAppSize();
}


function getWindowHeight() {
var windowHeight=0;
if (typeof(window.innerHeight)=='number') {
windowHeight=window.innerHeight;
}
else {
if (document.documentElement&&
document.documentElement.clientHeight) {
windowHeight=
document.documentElement.clientHeight;
}
else {
if (document.body&&document.body.clientHeight) {
windowHeight=document.body.clientHeight;
}
}
}
return windowHeight;
}


function setAppSize() 
{
/*    var appBox = document.getElementById("content");
   var newHeight = document.body.offsetHeight - 225; 
    if (newHeight < 400) 
        appBox.style.height = String(400) + "px";
    else 
        appBox.style.height = String(newHeight) + "px";*/

var newHeight = getWindowHeight() - 105;
//alert(newHeight);
if (newHeight < 300) { newHeight = 300};

document.getElementById("contentHolder").style.height = newHeight + 'px';
document.getElementById("content").style.height = newHeight + 'px';
document.getElementById("WarehouseApp").style.height = newHeight + 1 + 'px';
}

function moveIFrame(x,y,w,h) {
    lastX = x - 0;
    lastY = y - 0;
    lastW = w;
    lastH = h;

    var frameRef = document.getElementById("brandHomeFrame");


    var offsetTrail = document.getElementById("WarehouseApp");
    var offsetLeft = 0;
    var offsetTop = 0;
    while (offsetTrail){
        offsetLeft += offsetTrail.offsetLeft;
        offsetTop += offsetTrail.offsetTop;
        offsetTrail = offsetTrail.offsetParent;
    }
    
    var left = String(offsetLeft + lastX);
    var top = String(offsetTop + lastY);
    frameRef.style.left = left + "px";
    frameRef.style.top = 90 + lastY + "px";
    frameRef.style.width = w + "px";
    frameRef.style.height = h + "px";
    frameRef.style.zIndex = 999;
}

function hideIFrame(){
    document.getElementById("brandHomeFrame").style.visibility="hidden";
}

function showIFrame(){
    document.getElementById("brandHomeFrame").style.visibility="visible";
}

function loadIFrame(url){
    brandHomeFrame.location = url;
}

function isMSIE(){
    agent = navigator.userAgent;
    opera = (agent.indexOf('Opera')!=-1);
    return navigator.userAgent;
//    return ((agent.indexOf('MSIE')!=-1) && !opera);
}
-->
</script>

    </head>

    <body onLoad="MM_preloadImages('/images/btn_guidelines-over.jpg','/images/btn_grilles-over.jpg','/images/btn_eCatalog-over.jpg','/images/btn_diffusers-over.jpg','/images/btn_terminalUnits-over.jpg','/images/btn_fanCoils-over.jpg','/images/btn_airHandlers-over.jpg'); init();">

        <div id="main" class="main">

            <div id="topBanner" class="topBanner">
                <div style="float:left;width:208px;height:90px;padding-left: 10px;">
                    <script type="text/javascript">writeFlash("/images/logoMain_turn",208,90);</script>
                </div>
                <div style="float:left;width:752px;height:90px;">
                    <img src="/repsonly/inc/images/logoHeader_MyTitus.jpg" width="752" height="90" border="0" usemap="#myTitus">
                    <map name="myTitus">
                        <area shape="rect" coords="552,15,617,35" href="/logout.asp">
                        <area shape="rect" coords="552,35,707,52" href="/repsonly/">
                        <area shape="rect" coords="557,57,720,80" href="/repsonly/myprofile/index.asp">
                    </map>
                </div>
                <div id="location"><a href="/">Home</a> | MyTitus</div>
            </div>

        <div id="contentHolder" class="contentHolder">

        <div id="content" name="content" class="content"
        style="padding-left:25px; height:500px; width:980px;">
            
<!-- PAGE CONTENT STARTS HERE -->

