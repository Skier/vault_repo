<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Page language="c#" Codebehind="DCApp.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Main.DCApp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>dPi Teleconnect - Debit Card Application</title>
		<meta content="False" name="vs_snapToGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript">
		<!---
			var clickedButton = false;
			function check() 
			{
				//--->alert("DCApp.Check");
				return clickedButton;
			}
		//--->
		</script>		
		<script language="javascript">
			// autofocus for cc number
			function countMeCard1(string)
			{ 
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 3) 
					document.Form1.txtAccNo2.focus();
			
			}
			
			function countMeCard2(string)
			{
			
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 3) 
					document.Form1.txtAccNo3.focus();
			
			}
			function countMeCard3(string)
			{
			
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 3) 
					document.Form1.txtAccNo4.focus();
			
			}				
		</script>
		<script>
			<!--// first contact number check //-->
			function countMeHomePhone1(string)
			{ 
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 2) 
					document.Form1.txtCnNum2.focus();
			} 
			
			function countMeHomePhone2(string)
			{
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 2) 
					document.Form1.txtCnNum3.focus();
			}
			
			<!--// ssn number check //-->
			function countMeSSN1(string)
			{
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
			
				if (string.length > 2) 
					document.Form1.txtSSN2.focus();
			} 
			
			function countMeSSN2(string)
			{ 
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 1) 
					document.Form1.txtSSN3.focus();
			}
			
			//function checkDl()
			//{
			//	if(document.Form1.ddlIDType.selectedindex = 1)
			//		document.Form1.RequiredFieldValidator7. = true ;
			//}
			
			//function checkAlarm(source, arguments)
			//{
			//	if(document.Form1.ddlIDType.value == "Driver’s License")
			//		alert("You have chosen a Drivers' License as the form of ID. /n Please use the correct format when entering in their Driver's License number and choose a state.")
			//}
		</script>
</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0"
		ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
				<tr>
					<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
						height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
					<td vAlign="top" width="660">
						<table height="100%" cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
							<tr>
								<td colSpan="2"><subhdr:subheader id="subheader" runat="server"></subhdr:subheader></td>
							</tr>
							<tr>
								<td class="05_con_label" width="660" colSpan="2" height="24"><asp:image id="imgWorkflow" runat="server"></asp:image></td>
							</tr>
							<tr>
								<td align="center" colSpan="2">
									<table cellSpacing="0" cellPadding="0" width="98%" border="0">
										<tr>
											<td class="05_con_sublabel_zip" vAlign="middle" align="left" background="images/subtable_header_blank.jpg"
												bgColor="white" colSpan="5" height="61">&nbsp;&nbsp; 
												&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="Label1" runat="server" Font-Names="Arial" Font-Bold="True" Width="176px" ForeColor="Chocolate"
													Font-Size="Small" Height="22px">Customer Information</asp:label>
											</td>
										</tr>
										<tr>
											<td width="17">&nbsp;</td>
											<td align="center">
												<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
													<tr>
														<td colSpan="3"><asp:image id="Image16" runat="server" ImageUrl="images/asterisk.gif"></asp:image><asp:label id="Label19" runat="server" Width="176px" ForeColor="#C04000">Required Fields</asp:label></td>
													</tr>
													<tr>
														<td colSpan="3"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
													<tr>
													<tr>
														<td style="WIDTH: 584px" background="images/hashing.gif" colSpan="3">&nbsp;</td>
													</tr>
													<tr>
														<td style="WIDTH: 584px" align="center" background="images/hashing.gif" colSpan="3"><asp:label id="Label2" runat="server" Width="203px"> Instant Issue Card Number</asp:label><asp:image id="Image8" runat="server" ImageUrl="images/asterisk.gif"></asp:image>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br>
															<asp:textbox id="txtAccNo1" onkeyup="countMeCard1(this.value);" tabIndex="1" runat="server" Width="40px"
																MaxLength="4" Wrap="False"></asp:textbox>&nbsp;-&nbsp;
															<asp:textbox id="txtAccNo2" onkeyup="countMeCard2(this.value);" tabIndex="2" runat="server" Width="40px"
																MaxLength="4" Wrap="False"></asp:textbox>&nbsp;-&nbsp;
															<asp:textbox id="txtAccNo3" onkeyup="countMeCard3(this.value);" tabIndex="3" runat="server" Width="40px"
																MaxLength="4" Wrap="False"></asp:textbox>&nbsp;-&nbsp;
															<asp:textbox id="txtAccNo4" onkeyup="countMeCard4(this.value);" tabIndex="4" runat="server" Width="40px"
																MaxLength="4" Wrap="False"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtAccNo1" ErrorMessage="Please enter digits for the Card Account Number.">*</asp:requiredfieldvalidator><asp:requiredfieldvalidator id="RequiredFieldValidator8" runat="server" ControlToValidate="txtAccNo2" ErrorMessage="Please enter digits for the Card Account Number.">*</asp:requiredfieldvalidator><asp:requiredfieldvalidator id="RequiredFieldValidator11" runat="server" ControlToValidate="txtAccNo3" ErrorMessage="Please enter digits for the Card Account Number.">*</asp:requiredfieldvalidator><asp:requiredfieldvalidator id="RequiredFieldValidator15" runat="server" ControlToValidate="txtAccNo4" ErrorMessage="Please enter digits for the Card Account Number.">*</asp:requiredfieldvalidator></td>
													</tr>
													<tr>
														<td style="WIDTH: 584px" background="images/hashing.gif" colSpan="3">&nbsp;</td>
													</tr>
													<tr>
														<td colSpan="3"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
													<tr>
														<td style="WIDTH: 584px" colSpan="3">&nbsp;</td>
													</tr>
													<tr>
														<td style="WIDTH: 285px"><asp:label id="Label10" runat="server">First Name</asp:label><asp:image id="Image1" runat="server" ImageUrl="images/asterisk.gif"></asp:image><br>
															<asp:textbox id="TxtFirstName" tabIndex="5" runat="server" Width="242px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator9" runat="server" ControlToValidate="TxtFirstName" ErrorMessage="Please enter a Firstname.">*</asp:requiredfieldvalidator></td>
														<td style="WIDTH: 340px" align="left" colSpan="2"><asp:label id="Label12" runat="server">Last Name</asp:label><asp:image id="Image2" runat="server" ImageUrl="images/asterisk.gif"></asp:image><br>
															<asp:textbox id="TxtLastName" tabIndex="6" runat="server" Width="249px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator10" runat="server" ControlToValidate="TxtLastName" ErrorMessage="Please Enter  a Lastname.">*</asp:requiredfieldvalidator></td>
													</tr>
													<tr>
														<td style="WIDTH: 585px" colSpan="3">
															<table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
																<tr>
																	<td style="WIDTH: 104px"><asp:label id="Label11" runat="server">Street Number</asp:label><asp:image id="Image3" runat="server" ImageUrl="images/asterisk.gif"></asp:image><br>
																		<asp:textbox id="TxtStreetNumber" tabIndex="7" runat="server" Width="80px" MaxLength="200"></asp:textbox><asp:requiredfieldvalidator id="vldStreet" runat="server" Font-Names="Arial" Font-Bold="True" ControlToValidate="TxtStreetNumber"
																			ErrorMessage="Please enter a street number.">*</asp:requiredfieldvalidator></td>
																	<td style="WIDTH: 174px"><asp:label id="Label13" runat="server">Direction</asp:label><br>
																		<asp:dropdownlist id="txtStreetPfx" tabIndex="8" runat="server" Width="69px">
																			<asp:ListItem></asp:ListItem>
																			<asp:ListItem Value="N">N</asp:ListItem>
																			<asp:ListItem Value="S">S</asp:ListItem>
																			<asp:ListItem Value="E">E</asp:ListItem>
																			<asp:ListItem Value="W">W</asp:ListItem>
																			<asp:ListItem Value="NE">NE</asp:ListItem>
																			<asp:ListItem Value="NW">NW</asp:ListItem>
																			<asp:ListItem Value="SE">SE</asp:ListItem>
																			<asp:ListItem Value="SW">SW</asp:ListItem>
																		</asp:dropdownlist></td>
																	<td style="WIDTH: 189px"><asp:label id="Label17" runat="server">Street Name</asp:label><asp:image id="Image4" runat="server" ImageUrl="images/asterisk.gif"></asp:image><br>
																		<asp:textbox id="TxtStreetName" tabIndex="9" runat="server" Width="176px" MaxLength="200"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator16" runat="server" ControlToValidate="TxtStreetName" ErrorMessage="Please enter a Street Name.">*</asp:requiredfieldvalidator></td>
																	<td><asp:label id="Label18" runat="server">St. Type</asp:label><br>
																		<asp:dropdownlist id="ddlStreetType" tabIndex="10" runat="server" Width="88px"></asp:dropdownlist></td>
																</tr>
															</table>
														</td>
													</tr>
													<tr>
														<td style="WIDTH: 285px" align="left"><asp:label id="Label14" runat="server"> City</asp:label><asp:image id="Image5" runat="server" ImageUrl="images/asterisk.gif"></asp:image><br>
															<asp:textbox id="TxtCity" tabIndex="11" runat="server" Width="168px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator12" runat="server" ControlToValidate="TxtCity" ErrorMessage="Please enter a city">*</asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator3" runat="server" ControlToValidate="TxtCity" ErrorMessage="Invalid City"
																ValidationExpression="[a-zA-Z \.  \- ]{1,20}">*</asp:regularexpressionvalidator></td>
														<td style="WIDTH: 164px" align="left"><asp:label id="Label15" runat="server"> State</asp:label><asp:image id="Image6" runat="server" ImageUrl="images/asterisk.gif"></asp:image><br>
															<asp:dropdownlist id="ddlState" tabIndex="12" runat="server" Width="140px"></asp:dropdownlist><asp:requiredfieldvalidator id="RequiredFieldValidator13" runat="server" ControlToValidate="ddlState" ErrorMessage="Please select a valid State">*</asp:requiredfieldvalidator></td>
														<td style="WIDTH: 198px"><asp:label id="Label16" runat="server">ZIP Code</asp:label><asp:image id="Image7" runat="server" ImageUrl="images/asterisk.gif"></asp:image><br>
															<asp:textbox id="TxtZipCode" tabIndex="13" runat="server" Width="80px" MaxLength="10"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator14" runat="server" ControlToValidate="TxtZipCode" ErrorMessage="Please enter a ZIP code.">*</asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ControlToValidate="TxtZipCode" ErrorMessage="Invalid Zip Code"
																ValidationExpression="\d{5}(-\d{4})?">*</asp:regularexpressionvalidator></td>
													</tr>
													<tr>
														<td colSpan="3" height="20"><IMG height="1" src="images/pixel_gray.jpg" width="100%" border="0"></td>
													</tr>
													<tr>
														<td style="WIDTH: 285px"><asp:label id="Label3" runat="server" Width="114px"> Telephone Number</asp:label><asp:image id="Image9" runat="server" ImageUrl="images/asterisk.gif"></asp:image><br>
															<asp:textbox id="txtCnNum1" onkeyup="countMeHomePhone1(this.value);" tabIndex="14" runat="server"
																Width="40px" name="txtCnNum1" maxLength="3" size="3"></asp:textbox>-
															<asp:textbox id="txtCnNum2" onkeyup="countMeHomePhone2(this.value);" tabIndex="15" runat="server"
																Width="40px" name="txtCnNum2" maxLength="3" size="3"></asp:textbox>-
															<asp:textbox id="txtCnNum3" tabIndex="16" runat="server" Width="45px" name="txtCnNum3" maxLength="4"
																size="4"></asp:textbox><asp:requiredfieldvalidator id="errNum" runat="server" Font-Names="Arial" Font-Bold="True" ControlToValidate="txtCnNum1"
																ErrorMessage="Please enter a Telephone Number.">X</asp:requiredfieldvalidator><asp:regularexpressionvalidator id="Regularexpressionvalidator5" runat="server" Font-Names="Arial" Font-Bold="True"
																Font-Size="Smaller" ControlToValidate="txtCnNum1" ErrorMessage="Please enter only numbers for your Telephone Number." ValidationExpression="[0-9]*">x</asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" Font-Names="Arial" Font-Bold="True"
																Font-Size="Smaller" ControlToValidate="txtCnNum2" ErrorMessage="Please enter only numbers for your Telephone Number." ValidationExpression="[0-9]*">x</asp:regularexpressionvalidator><asp:regularexpressionvalidator id="Regularexpressionvalidator4" runat="server" Font-Names="Arial" Font-Bold="True"
																Font-Size="Smaller" ControlToValidate="txtCnNum3" ErrorMessage="Please enter only numbers for your Telephone Number." ValidationExpression="[0-9]*">x</asp:regularexpressionvalidator></td>
														<td style="WIDTH: 340px" colSpan="2"><asp:label id="Label5" runat="server" Width="74px">SSN  Number</asp:label><asp:image id="Image10" runat="server" ImageUrl="images/asterisk.gif"></asp:image><BR>
															<asp:textbox id="txtSSN1" onkeyup="countMeSSN1(this.value);" tabIndex="17" runat="server" Width="39px"
																MaxLength="3"></asp:textbox>&nbsp;-
															<asp:textbox id="txtSSN2" onkeyup="countMeSSN2(this.value);" tabIndex="18" runat="server" Width="33px"
																MaxLength="2"></asp:textbox>&nbsp;-
															<asp:textbox id="txtSSN3" tabIndex="19" runat="server" Width="45px" MaxLength="4"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtSSN1" ErrorMessage="Please enter a complete SSN Number.">*</asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator7" runat="server" ControlToValidate="txtSSN1" ErrorMessage="Please enter only numbers in the SSN fields."
																ValidationExpression="\d{3}">*</asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator8" runat="server" ControlToValidate="txtSSN2" ErrorMessage="Please enter only numbers in the SSN fields."
																ValidationExpression="\d{2}">*</asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator9" runat="server" ControlToValidate="txtSSN3" ErrorMessage="Please enter only numbers in the SSN fields."
																ValidationExpression="\d{4}">*</asp:regularexpressionvalidator></td>
													</tr>
													<tr>
														<td style="WIDTH: 285px"><asp:label id="Label4" runat="server" Width="74px">Date Of Birth</asp:label><asp:image id="Image11" runat="server" ImageUrl="images/asterisk.gif"></asp:image><BR>
															<asp:dropdownlist id="ddlDOBMM" tabIndex="20" runat="server" Width="107px"></asp:dropdownlist>&nbsp;/
															<asp:dropdownlist id="ddlDOBDD" tabIndex="21" runat="server" Width="41px"></asp:dropdownlist>&nbsp;/
															<asp:dropdownlist id="ddlDOBYY" tabIndex="22" runat="server" Width="60px"></asp:dropdownlist><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" ControlToValidate="ddlDOBMM" ErrorMessage="Please select a month.">*</asp:requiredfieldvalidator><asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" ControlToValidate="ddlDOBYY" ErrorMessage="Please select a year.">*</asp:requiredfieldvalidator><asp:requiredfieldvalidator id="Requiredfieldvalidator20" runat="server" ControlToValidate="ddlDOBDD" ErrorMessage="Please select a day.">*</asp:requiredfieldvalidator></td>
														<td colSpan="2"><asp:label id="Label20" runat="server" Width="91px">Email Address</asp:label><BR>
															<asp:textbox id="txtEmail" tabIndex="23" runat="server" Width="228px"></asp:textbox><asp:regularexpressionvalidator id="RegularExpressionValidator6" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please enter a proper email address."
																ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:regularexpressionvalidator></td>
													</tr>
													<tr>
														<td colSpan="2"></td>
														<td></td>
													</tr>
													<!--
                    <tr>
                      <td style="WIDTH: 253px; HEIGHT: 54px" 
                      ><asp:label id=Label7 runat="server">Identification Number</asp:label><asp:image id=Image13 runat="server" ImageUrl="images/asterisk.gif"></asp:image><br 
                        ><asp:textbox id=TxtIdentificationNumber tabIndex=100 runat="server" Width="204px"></asp:textbox></TD>
                      <td style="WIDTH: 165px; HEIGHT: 54px" 
                      ><asp:label id=Label6 runat="server" Width="108px"> Identification Type</asp:label><asp:image id=Image12 runat="server" ImageUrl="images/asterisk.gif"></asp:image><br 
                        ><asp:dropdownlist id=ddlIDType tabIndex=102 runat="server" Width="144px">
																<asp:ListItem></asp:ListItem>
																<asp:ListItem Value="Driver’s License">Driver’s License</asp:ListItem>
																<asp:ListItem Value="State ID">State ID</asp:ListItem>
																<asp:ListItem Value="Military ID">Military ID</asp:ListItem>
																<asp:ListItem Value="U.S. Passport">U.S. Passport</asp:ListItem>
															</asp:dropdownlist></TD>
                      <td style="HEIGHT: 54px"><asp:label id=Label8 runat="server" Width="42px">State</asp:label><asp:image id=Image14 runat="server" ImageUrl="images/asterisk.gif" Visible="False"></asp:image><br 
                        ><asp:dropdownlist id=ddlIDState tabIndex=103 runat="server" Width="136px" BackColor="Transparent"></asp:dropdownlist></TD></TR>
                    <tr>
                      <td style="WIDTH: 253px"><asp:label id=Label9 runat="server" Width="166px">ID Document Expiration Date</asp:label><asp:image id=Image15 runat="server" ImageUrl="images/asterisk.gif"></asp:image><br 
                        ><asp:dropdownlist id=ddlDocMm tabIndex=104 runat="server" Width="107px"></asp:dropdownlist>&nbsp;/ 
<asp:dropdownlist id=ddlDocDd tabIndex=105 runat="server" Width="42px"></asp:dropdownlist>&nbsp;/ 
<asp:dropdownlist id=ddlDocYy tabIndex=106 runat="server" Width="56px"></asp:dropdownlist></TD>
                      <td colSpan=2></TD></TR> 
                      --->
													<tr>
														<td style="WIDTH: 285px"></td>
														<td style="WIDTH: 340px" colSpan="2"></td>
													</tr>
												</table>
											</td>
											<td width="10">&nbsp;</td>
										</tr>
									</table>
									<P><asp:validationsummary id="ValidationSummary2" runat="server" Width="588px" DisplayMode="List" BorderWidth="0px"
											BorderStyle="None"></asp:validationsummary></P>
									<DIV align="center"><asp:placeholder id="phError" runat="server"></asp:placeholder></DIV>
								</td>
							</tr>
							<tr>
								<td align="left">&nbsp;&nbsp;&nbsp;
									<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg" CausesValidation="False"></asp:imagebutton></td>
								<td align="right"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btnCardApproval.jpg" EnableViewState="False"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td class="05_con_bold" align="center" colSpan="2" height="24"><br>
									<div align="center">&nbsp;</div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="2"><dpiuser:sitefooter id="SiteFooter" runat="server"></dpiuser:sitefooter></td>
				</tr>
			</table>
		</form>
		<!--	<SCRIPT language="JavaScript" src="../Core/wz_tooltip.js" type="text/javascript"></SCRIPT> --></TABLE></TR></TABLE></TR></TABLE></TR></TABLE></FORM>
	</body>
</HTML>
