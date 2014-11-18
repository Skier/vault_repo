<%@ Import Namespace="DPI.Ordering" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Page language="c#" Codebehind="TrainingMenuGen.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Training.TrainingMenuGen" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Training Menu</title>
		<script language="JavaScript">
		<!--
			var clickedButton = false;
			function check() 
			{
				if (clickedButton)
					{
						clickedButton = false;
						return true;
					}
				else
					{
						return false;
					}
			}
			function OpenWindow(provider)
			{
				var doc = "OnlineTraining_gen.pdf";;
				var docPath = document.all.hdnDocPath.value;
				var features = "height= 690, width=682 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=yes,resizable=yes";
				var name = "_blank";
				
				if (provider == "prepaid")
					doc = "DebitCard_Training.pdf";
					
				if (provider == "slingshot")
					doc = "Slingshot_Training.pdf";
					
				if (provider == "infinity")
					doc = "Infinity_Training.pdf";
				
				if (provider == "lifeline")
					doc = "LifelineTutorialAndApplication.pdf";	
				
				window.open(docPath + '/' + doc, name, features);				
			}
			//-->
		</script>
		<LINK href="../Main/Styles/Navigator.css" rel="stylesheet">
			<LINK href="../Main/Styles/DPI.css" rel="stylesheet">
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0">
		<form id="ReportformZ" onsubmit="return check(); " runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
				<tr>
					<td colSpan="2"><uc1:siteheader id="SiteHeader1" runat="server"></uc1:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
						height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
					<td vAlign="top" width="660"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader>
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td vAlign="middle" align="right" width="655" background="images/subheader_agenttraining.jpg"
									colSpan="4" height="68"><asp:label id="Label1" runat="server" ForeColor="Black" Font-Bold="True" Font-Size="Medium"
										Font-Names="Tahoma" Width="376px"> Agent Training Program</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
							</tr>
							<tr>
								<td vAlign="bottom" align="center" colSpan="4">&nbsp;</td>
							</tr>
							<tr>
								<td width="64">&nbsp;</td>
								<td vAlign="top" align="center"><asp:image id="Image3" runat="server" ImageUrl="images/subheader_radiobutton.jpg"></asp:image></td>
								<td vAlign="top">
									<P>&nbsp;&nbsp;
										<asp:label id="Label4" runat="server" ForeColor="#804000" Font-Size="Large" Font-Names="Tahoma">Step 1 The Tutorial</asp:label><br>
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:label id="Label3" runat="server" ForeColor="SteelBlue" Width="312px">Click this button to learn about WebCentral. </asp:label>
										<ul>
											<li>
												<A onclick="OpenWindow('basicweb');" href="TrainingMenuGen.aspx"><font color="red">Basic 
														Web</font></A></FONT>
											<li>
												<A onclick="OpenWindow('prepaid');" href="TrainingMenuGen.aspx"><font color="red">Prepaid 
														MasterCard</font></A></FONT>
											<li>
												<A onclick="OpenWindow('slingshot');" href="TrainingMenuGen.aspx"><font color="red">Slingshot</font></A></FONT>
											<LI>
												<A onclick="OpenWindow('infinity');" href="TrainingMenuGen.aspx"><font color="red">Infinity 
														Mobile</font></A></FONT>
											<LI>
												<A onclick="OpenWindow('lifeline');" href="TrainingMenuGen.aspx"><font color="red">Lifeline</font></A></FONT>
											</LI>
										</ul>
									<P>&nbsp;</P>
								</td>
								<td rowSpan="5">&nbsp;
									<asp:image id="Image2" runat="server" ImageUrl="images/website_graphics.jpg"></asp:image></td>
							</tr>
							<tr>
								<td width="64" height="5">&nbsp;</td>
								<td vAlign="top" align="center" height="5"><asp:image id="Image4" runat="server" ImageUrl="images/subheader_radiobutton.jpg"></asp:image></td>
								<td vAlign="top" height="5">
									<P align="left">&nbsp;&nbsp;&nbsp;
										<asp:label id="Label5" runat="server" ForeColor="#804000" Font-Size="Large" Font-Names="Tahoma">Step 2 Certification</asp:label><BR>
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:label id="Label2" runat="server" ForeColor="SteelBlue" Width="233px">After you have completed the tutorial, Click this button to get Certified.</asp:label>
										<ul>
											<li>
												<A onclick="window.open('../training/certification_quiz_Gen.aspx', '_blank' ,'height= 690, width=682 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=yes,resizable=yes')"
													href="TrainingMenuGen.aspx"><font color="red">Basic Web</font></A>
											<li>
												<font color="coral"><A onclick="window.open('../training/certification_quiz_DC_Gen.aspx', '_blank' ,'height= 690, width=682 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=yes,resizable=yes')"
														href="TrainingMenuGen.aspx"><font color="red">Prepaid MasterCard</font></A></font>
											<li>
												<font color="coral"><A onclick="window.open('../training/certification_quiz_Slingshot.aspx', '_blank' ,'height= 690, width=682 ,toolbar=no, location=no,directories=no,status=no,menubar=no, scrollbars=yes,resizable=yes')"
														href="TrainingMenuGen.aspx"><font color="red">Slingshot</font></A></font>
											</li>
										</ul>
									<P></P>
								</td>
							</tr>
						</table>
						<br>
					</td>
				</tr>
				<tr>
					<td colSpan="2"><uc1:sitefooter id="SiteFooter1" runat="server"></uc1:sitefooter><INPUT id="hdnDocPath" runat="server" type="hidden" size="5"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
