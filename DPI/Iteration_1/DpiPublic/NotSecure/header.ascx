<%@ Control CodeBehind="header.ascx.cs" Language="c#" AutoEventWireup="false" Inherits="Dpi.Central.Web.HeaderUserControl" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC</title>
		<meta http-equiv="Content-Language" content="en-us" />
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
		<script language="JavaScript" src="<%= Request.ApplicationPath + "/script/mm_menu.js" %>"></script>
		<script language="JavaScript">
		<!--
			function mmLoadMenus() 
			{
				if (window.mm_menu_become_an_agent) 
					return;
								
				// Products
				window.mm_menu_products = new Menu("root",192,16,"Arial, Helvetica, sans-serif",10,"#333333","#FFFFFF","#DBDBDB","#DB6C1D","left","middle",1,0,500,-5,7,true,true,true,0,false,false);
				mm_menu_products.addMenuItem("Overview","window.open('<%= Request.ApplicationPath + "/products.aspx" %>', '_self');");
				mm_menu_products.addMenuItem("Pre-Paid&nbsp;Home&nbsp;Phone&nbsp;Service","window.open('<%= Request.ApplicationPath + "/pphp.aspx" %>', '_self');");
				mm_menu_products.addMenuItem("Pre-Paid&nbsp;Long&nbsp;Distance","window.open('<%= Request.ApplicationPath + "/ppld.aspx" %>', '_self');");
				mm_menu_products.addMenuItem("Pre-Paid&nbsp;Cellular","window.open('<%= Request.ApplicationPath + "/ppc.aspx" %>', '_self');");
				mm_menu_products.addMenuItem("Pre-Paid&nbsp;MasterCard","window.open('<%= Request.ApplicationPath + "/ppmc.aspx" %>', '_self');");
				mm_menu_products.addMenuItem("Pre-Paid&nbsp;Internet","window.open('<%= Request.ApplicationPath + "/ppi.aspx" %>', '_self');");
				mm_menu_products.addMenuItem("Bill&nbsp;Pay","window.open('<%= Request.ApplicationPath + "/bp.aspx" %>', '_self');");
				mm_menu_products.fontWeight="bold";
				mm_menu_products.hideOnMouseOut=true;
				mm_menu_products.bgColor='#9A9A9A';
				mm_menu_products.menuBorder=1;
				mm_menu_products.menuLiteBgColor='#DADADA';
				mm_menu_products.menuBorderBgColor='#FBF9F8';
				
				// Become Agent
				window.mm_menu_become_an_agent = new Menu("root",130,16,"Arial, Helvetica, sans-serif",10,"#333333","#FFFFFF","#DBDBDB","#DA6518","left","middle",1,0,500,-5,7,true,true,true,0,false,false);
				mm_menu_become_an_agent.addMenuItem("Overview","window.open('<%= Request.ApplicationPath + "/reseller.aspx" %>', '_self');");
				mm_menu_become_an_agent.addMenuItem("Benefits","window.open('<%= Request.ApplicationPath + "/reseller2.aspx" %>', '_self');");
				mm_menu_become_an_agent.addMenuItem("Sign&nbsp;Up&nbsp;Now","window.open('<%= Request.ApplicationPath + "/agent_contact.aspx" %>', '_self');");
				// mm_menu_become_an_agent.addMenuItem("Agent&nbsp;Login","window.open('https://secure.dpiteleconnect.com/agents/', '_blank');");
				mm_menu_become_an_agent.fontWeight="bold";
				mm_menu_become_an_agent.hideOnMouseOut=true;
				mm_menu_become_an_agent.bgColor='#9A9A9A';
				mm_menu_become_an_agent.menuBorder=1;
				mm_menu_become_an_agent.menuLiteBgColor='#DADADA';
				mm_menu_become_an_agent.menuBorderBgColor='#FBF9F8';
				
				// Account
				window.mm_menu_account = new Menu("root",130,16,"Arial, Helvetica, sans-serif",10,"#333333","#FFFFFF","#DBDBDB","#DA6518","left","middle",1,0,500,-5,7,true,true,true,0,false,false);
				mm_menu_account.addMenuItem("Sign&nbsp;Up&nbsp;Now","window.open('<%= Request.ApplicationPath + "/signup.aspx" %>', '_self');");
				mm_menu_account.addMenuItem("Account&nbsp;Login","window.open('<%= Request.ApplicationPath + "/account/login.aspx" %>', '_self');");
				mm_menu_account.addMenuItem("Account&nbsp;Summary","window.open('<%= Request.ApplicationPath + "/account/summary.aspx" %>', '_self');");
				mm_menu_account.fontWeight="bold";
				mm_menu_account.hideOnMouseOut=true;
				mm_menu_account.bgColor='#9A9A9A';
				mm_menu_account.menuBorder=1;
				mm_menu_account.menuLiteBgColor='#DADADA';
				mm_menu_account.menuBorderBgColor='#FBF9F8';

				// About Us
				window.mm_menu_about_us = new Menu("root",95,16,"Arial, Helvetica, sans-serif",10,"#333333","#FFFFFF","#DBDBDB","#DB6C1D","left","middle",3,0,500,-5,7,true,true,true,0,true,true);
				mm_menu_about_us.addMenuItem("About&nbsp;Us","window.open('<%= Request.ApplicationPath + "/about.aspx" %>', '_self');");
				mm_menu_about_us.addMenuItem("Employment","window.open('<%= Request.ApplicationPath + "/jobs.aspx" %>', '_self');");
				mm_menu_about_us.fontWeight="bold";
				mm_menu_about_us.hideOnMouseOut=true;
				mm_menu_about_us.bgColor='#9A9A9A';
				mm_menu_about_us.menuBorder=1;
				mm_menu_about_us.menuLiteBgColor='#DADADA';
				mm_menu_about_us.menuBorderBgColor='#FBF9F8';
				
				// Contact Us
				window.mm_menu_contact_us = new Menu("root",161,16,"Arial, Helvetica, sans-serif",10,"#333333","#FFFFFF","#DBDBDB","#DB6C1D","left","middle",3,0,500,-5,7,true,true,true,0,true,true);
				mm_menu_contact_us.addMenuItem("Contact&nbsp;dPi","window.open('<%= Request.ApplicationPath + "/contact.aspx" %>', '_self');");
				mm_menu_contact_us.addMenuItem("Find&nbsp;A&nbsp;Reseller&nbsp;Near&nbsp;You","window.open('<%= Request.ApplicationPath + "/locations.aspx" %>', '_self');");
				mm_menu_contact_us.fontWeight="bold";
				mm_menu_contact_us.hideOnMouseOut=true;
				mm_menu_contact_us.bgColor='#9A9A9A';
				mm_menu_contact_us.menuBorder=1;
				mm_menu_contact_us.menuLiteBgColor='#DADADA';
				mm_menu_contact_us.menuBorderBgColor='#FBF9F8';

				mm_menu_about_us.writeMenus();
			}
		//-->
		</script>
		<script language="JavaScript">
		<!--
			function MM_reloadPage(init) {  //reloads the window if Nav4 resized
			if (init==true) with (navigator) {if ((appName=="Netscape")&&(parseInt(appVersion)==4)) {
				document.MM_pgW=innerWidth; document.MM_pgH=innerHeight; onresize=MM_reloadPage; }}
			else if (innerWidth!=document.MM_pgW || innerHeight!=document.MM_pgH) location.reload();
			}
			MM_reloadPage(true);

			function MM_preloadImages() { //v3.0
			var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
				var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
				if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
			}

			function MM_swapImgRestore() { //v3.0
			var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
			}

			function MM_findObj(n, d) { //v4.01
			var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
				d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
			if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
			for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
			if(!x && d.getElementById) x=d.getElementById(n); return x;
			}

			function MM_swapImage() { //v3.0
			var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
			if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
			}
		//-->
		</script>
	</HEAD>
	<body onLoad="MM_preloadImages( '<%= Request.ApplicationPath + "/images/home_button_on.gif" %>', 
									'<%= Request.ApplicationPath + "/images/products_button_on.gif" %>', 
									'<%= Request.ApplicationPath + "/images/reseller_button_on.gif" %>', 
									'<%= Request.ApplicationPath + "/images/ainfo_button_on.gif" %>', 
									'<%= Request.ApplicationPath + "/images/about_button_on.gif" %>', 
									'<%= Request.ApplicationPath + "/images/contact_button_on.gif" %>')">
		<script language="JavaScript">mmLoadMenus();</script>
		<table id="_contentTable" border="0" cellpadding="0" cellspacing="0" width="792">
			<tr>
				<td background="<%= Request.ApplicationPath + "/images/header.jpg" %>" border="0" width="792" height="94" vAlign="bottom" align="right">
					<asp:ImageButton id="btnImgLogout" runat="server" Height="27px" Width="69px" 
						ImageUrl="~/images/Logout.jpg" CausesValidation="False" Visible="False">
					</asp:ImageButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
			&nbsp;&nbsp;
				</td>
			</tr>
			<tr>
				<td>
					<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table2">
						<tr>
							<td><a href="<%= Request.ApplicationPath + "/index.aspx" %>" target="_self" onMouseOver="MM_swapImage('Image61','','<%= Request.ApplicationPath + "/images/home_button_on.gif" %>',1)"
									onMouseOut="MM_swapImgRestore()"> <img src="<%= Request.ApplicationPath + "/images/home_button.gif" %>" name="Image61" height="29" border="0" id="Image61"></a></td>
							<td><a href="<%= Request.ApplicationPath + "/products.aspx" %>" target="_self" onMouseOver="MM_showMenu(window.mm_menu_products,0,30,null,'image2');MM_swapImage('image2','','<%= Request.ApplicationPath + "/images/products_button_on.gif" %>',1)"
									onMouseOut="MM_startTimeout();MM_swapImgRestore()"> <img src="<%= Request.ApplicationPath + "/images/products_button.gif" %>" name="image2" height="29" border="0" id="image2"></a></td>
							<td><a href="<%= Request.ApplicationPath + "/reseller.aspx" %>" target="_self" onMouseOver="MM_showMenu(window.mm_menu_become_an_agent,0,30,null,'image1');MM_swapImage('image1','','<%= Request.ApplicationPath + "/images/reseller_button_on.gif" %>',1)"
									onMouseOut="MM_startTimeout();MM_swapImgRestore()"> <img src="<%= Request.ApplicationPath + "/images/reseller_button.gif" %>" name="image1" height="29" border="0" id="image1"></a></td>
							<td><a href="https://secure.dpiteleconnect.com/agents/" target="_blank" onMouseOver="MM_swapImage('Image62','','<%= Request.ApplicationPath + "/images/ainfo_button_on.gif" %>',1)" 
									onMouseOut="MM_swapImgRestore()"> <img src="<%= Request.ApplicationPath + "/images/ainfo_button.gif" %>" name="Image62" height="29" border="0" id="Image62"></a></td>
							<td><a href="<%= Request.ApplicationPath + "/account/login.aspx" %>" target="_self" onMouseOver="MM_showMenu(window.mm_menu_account,0,30,null,'image20');MM_swapImage('image20','','<%= Request.ApplicationPath + "/images/account_button_on.gif" %>',1)"
									onMouseOut="MM_startTimeout();MM_swapImgRestore()"> <img src="<%= Request.ApplicationPath + "/images/account_button.gif" %>" name="image20" height="29" border="0" id="image20"></a></td>
							<td><a href="<%= Request.ApplicationPath + "/about.aspx" %>" target="_self" onMouseOver="MM_swapImage('Image3','','<%= Request.ApplicationPath + "/images/about_button_on.gif" %>',1);MM_showMenu(window.mm_menu_about_us,0,30,null,'Image3')"
									onMouseOut="MM_swapImgRestore();MM_startTimeout();"> <img src="<%= Request.ApplicationPath + "/images/about_button.gif" %>" name="Image3" border="0" id="Image3"></a></td>
							<td><a href="<%= Request.ApplicationPath + "/contact.aspx" %>" target="_self" onMouseOver="MM_swapImage('image3','','<%= Request.ApplicationPath + "/images/contact_button_on.gif" %>',1);MM_showMenu(window.mm_menu_contact_us,0,30,null,'image3')"
									onMouseOut="MM_swapImgRestore();MM_startTimeout();"> <img src="<%= Request.ApplicationPath + "/images/contact_button.gif" %>" name="image3" height="29" border="0" id="image3"></a></td>
							<td><img border="0" src="<%= Request.ApplicationPath + "/images/call_us.gif" %>" height="29"></td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</body>
</HTML>
