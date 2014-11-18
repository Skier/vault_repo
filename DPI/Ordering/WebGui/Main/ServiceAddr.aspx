<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Page language="c#" Codebehind="ServiceAddr.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.NO_ServiceAddr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title 
id=pagetitle>dPi Teleconnect</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="JavaScript">
		<!---
			var clickedButton = false;
			function check() {
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
		//--->
		</script>
		<script>
			<!--// first contact number check //-->
			function countMeHomePhone1(string)
			{ 
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 2) 
					document.YourAccount.txtCnNum2.focus();
			} 
			
			function countMeHomePhone2(string)
			{ 
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 2) 
					document.YourAccount.txtCnNum3.focus();
			}
			
			<!--// second contact number check //-->
			function countMe2ndPhone1(string)
			{ 
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 2) 
					document.YourAccount.txtCnNumSnd2.focus();
			} 
			function countMe2ndPhone2(string)
			{ 
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 2) 
					document.YourAccount.txtCnNumSnd3.focus();
			}
		
			<!--// previous contact number check //-->
			function countMePrevPhone1(string)
			{ 
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 2) 
					document.YourAccount.txtPrevPhon2.focus();
			} 
			function countMePrevPhone2(string)
			{ 
				if (window.event.keyCode == 9 || window.event.keyCode == 16) 
					return; 
				if (string.length > 2) 
					document.YourAccount.txtPrevPhon3.focus();
			}							
		</script>
</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0"
		ms_positioning="GridLayout">
		<form id="YourAccount" name="YourAccount" onsubmit="return check(); " method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
				<tr>
					<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
						height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
					<td vAlign="top" width="660">
						<table height="490" cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
							<tr>
								<td colSpan="5"></td>
							</tr>
							<tr>
								<td class="05_con_sublabel_zip" vAlign="middle" align="left" background="images/subtable_header_blank.jpg"
									bgColor="white" colSpan="5" height="61">&nbsp;&nbsp; 
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:label id="Label5" runat="server" Height="22px" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
										ForeColor="Chocolate" Width="176px">Customer Information</asp:label>
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ZipCode:
									<asp:label id="lblZipCode" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp;
									<asp:label id="lblIlec" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp; 
									&nbsp;&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td colSpan="5">
									<table cellSpacing="0" cellPadding="0" width="660" border="0">
										<tr class="input_titles">
											<td style="WIDTH: 131px">&nbsp;</td>
											<td colSpan="4">&nbsp;
												<asp:image id="Image9" runat="server" ImageUrl="images/asterisk.gif"></asp:image><asp:label id="Label3" runat="server" Width="176px">Required Fields</asp:label></td>
											<td>&nbsp;</td>
										</tr>
										<tr class="input_titles">
											<td style="WIDTH: 131px" width="131" rowSpan="12">&nbsp;</td>
											<td style="WIDTH: 238px; HEIGHT: 63px"><asp:label id="lblFirstName" runat="server" Width="68px">First 
														Name</asp:label><asp:image id="Image1" runat="server" ImageUrl="images/asterisk.gif"></asp:image><br>
												<asp:textbox id="txtFirstName" tabIndex="43" runat="server"></asp:textbox><asp:requiredfieldvalidator id="errFirstName" runat="server" Font-Bold="True" Font-Names="Arial" ControlToValidate="txtFirstName"
													ErrorMessage="Please enter a first name.">X</asp:requiredfieldvalidator></td>
											<td style="WIDTH: 235px; HEIGHT: 63px"><asp:label id="lblLastname" runat="server" Width="68px">Last Name</asp:label><asp:image id="Image2" runat="server" ImageUrl="images/asterisk.gif"></asp:image><br>
												<asp:textbox id="txtLastName" tabIndex="44" runat="server" Width="144px"></asp:textbox><asp:requiredfieldvalidator id="errLastName" runat="server" Font-Bold="True" Font-Names="Arial" ControlToValidate="txtLastName"
													ErrorMessage="Please enter a last name.">X</asp:requiredfieldvalidator></td>
											<td style="HEIGHT: 63px" colSpan="2"><asp:label id="Label2" runat="server" Width="104px">Email (optional)</asp:label><br>
												<asp:textbox id="txtEmail" tabIndex="45" runat="server"></asp:textbox><asp:regularexpressionvalidator id="vldEmail" runat="server" Font-Bold="True" Font-Names="Arial" ControlToValidate="txtEmail"
													ErrorMessage="Please put a valid email." ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">X</asp:regularexpressionvalidator></td>
											<td width="12" rowSpan="12">&nbsp;</td>
										</tr>
										<tr class="input_titles">
											<td style="WIDTH: 238px; HEIGHT: 53px" vAlign="top"><asp:label id="lblBirthday" runat="server" Width="199px"> Birthday(mm/dd/yyyy)(optional)</asp:label><asp:textbox id="txtBirthday" tabIndex="46" runat="server"></asp:textbox></td>
											<td style="WIDTH: 235px; HEIGHT: 53px" vAlign="top">Contact #
												<asp:image id="Image3" runat="server" ImageUrl="images/asterisk.gif"></asp:image><BR>
												<asp:textbox id="txtCnNum1" onkeyup="countMeHomePhone1(this.value);" tabIndex="47" runat="server"
													Width="35px" size="3" maxLength="3" name="txtCnNum1"></asp:textbox>-
												<asp:textbox id="txtCnNum2" onkeyup="countMeHomePhone2(this.value);" tabIndex="48" runat="server"
													Width="35px" size="3" maxLength="3" name="txtCnNum2"></asp:textbox>-
												<asp:textbox id="txtCnNum3" tabIndex="49" runat="server" Width="45px" size="4" maxLength="4"
													name="txtCnNum3"></asp:textbox><asp:requiredfieldvalidator id="errNum" runat="server" Font-Bold="True" Font-Names="Arial" ControlToValidate="txtCnNum1"
													ErrorMessage="Please enter a contact number.">X</asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" Font-Bold="True" Font-Names="Arial"
													Font-Size="Smaller" ControlToValidate="txtCnNum1" ErrorMessage="Please enter numeric value" ValidationExpression="[0-9]*">x</asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" Font-Bold="True" Font-Names="Arial"
													Font-Size="Smaller" ControlToValidate="txtCnNum2" ErrorMessage="Please enter numeric value" ValidationExpression="[0-9]*">x</asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator3" runat="server" Font-Bold="True" Font-Names="Arial"
													Font-Size="Smaller" ControlToValidate="txtCnNum3" ErrorMessage="Please enter numeric value" ValidationExpression="[0-9]*">x</asp:regularexpressionvalidator></td>
											<td style="HEIGHT: 53px" vAlign="top" colSpan="2">2nd Contact # (optional)<BR>
												<asp:textbox id="txtCnNumSnd1" onkeyup="countMe2ndPhone1(this.value);" tabIndex="50" runat="server"
													Width="35px" size="3" maxLength="3" name="txtCnNumSnd1"></asp:textbox>-
												<asp:textbox id="txtCnNumSnd2" onkeyup="countMe2ndPhone2(this.value);" tabIndex="51" runat="server"
													Width="35px" size="3" maxLength="3" name="txtCnNumSnd2"></asp:textbox>-
												<asp:textbox id="txtCnNumSnd3" tabIndex="52" runat="server" Width="45px" size="4" maxLength="4"
													name="txtCnNumSnd3"></asp:textbox><asp:regularexpressionvalidator id="RegularExpressionValidator6" runat="server" Font-Bold="True" Font-Names="Arial"
													ControlToValidate="txtCnNumSnd1" ErrorMessage="RegularExpressionValidator" ValidationExpression="[0-9]*">x</asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator5" runat="server" Font-Bold="True" Font-Names="Arial"
													ControlToValidate="txtCnNumSnd2" ErrorMessage="RegularExpressionValidator" ValidationExpression="[0-9]*">x</asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator4" runat="server" Font-Bold="True" Font-Names="Arial"
													ControlToValidate="txtCnNumSnd3" ErrorMessage="RegularExpressionValidator" ValidationExpression="[0-9]*">x</asp:regularexpressionvalidator></td>
										</tr>
										<tr class="input_titles">
											<td style="WIDTH: 238px; HEIGHT: 37px" vAlign="top">Previous Phone # (optional)<BR>
												<asp:textbox id="txtPrevPhon1" onkeyup="countMePrevPhone1(this.value);" tabIndex="53" runat="server"
													Width="35px" size="3" maxLength="3" name="txtPrevPhon1"></asp:textbox>-
												<asp:textbox id="txtPrevPhon2" onkeyup="countMePrevPhone2(this.value);" tabIndex="54" runat="server"
													Width="35px" size="3" maxLength="3" name="txtPrevPhon2"></asp:textbox>-
												<asp:textbox id="txtPrevPhon3" tabIndex="55" runat="server" Width="45px" size="4" maxLength="4"
													name="txtPrevPhon3"></asp:textbox><asp:regularexpressionvalidator id="RegularExpressionValidator9" runat="server" Font-Bold="True" Font-Names="Arial"
													ControlToValidate="txtPrevPhon1" ErrorMessage="RegularExpressionValidator" ValidationExpression="[0-9]*">x</asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator8" runat="server" Font-Bold="True" Font-Names="Arial"
													ControlToValidate="txtPrevPhon2" ErrorMessage="RegularExpressionValidator" ValidationExpression="[0-9]*">x</asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator7" runat="server" Font-Bold="True" Font-Names="Arial"
													ControlToValidate="txtPrevPhon3" ErrorMessage="RegularExpressionValidator" ValidationExpression="[0-9]*">x</asp:regularexpressionvalidator></td>
											<td style="WIDTH: 235px; HEIGHT: 37px" vAlign="top">Previous Phone Co. (optional)<BR>
												<asp:dropdownlist id="ddlPhoneComp" tabIndex="56" runat="server">
													<asp:ListItem Selected="True"></asp:ListItem>
													<asp:ListItem Value="GTE">Verizon - GTE</asp:ListItem>
													<asp:ListItem Value="BAT">Verizon - Bell Atlantic</asp:ListItem>
													<asp:ListItem Value="PAC">SBC - Pac Bell</asp:ListItem>
													<asp:ListItem Value="INT">Sprint</asp:ListItem>
													<asp:ListItem Value="ALT">Alltel</asp:ListItem>
													<asp:ListItem Value="CEN">Century Tel</asp:ListItem>
													<asp:ListItem Value="VAL">Valor</asp:ListItem>
													<asp:ListItem Value="USW">Qwest - US West</asp:ListItem>
													<asp:ListItem Value="AMT">SBC - Ameritech</asp:ListItem>
													<asp:ListItem Value="SNT">SBC - SNET</asp:ListItem>
													<asp:ListItem Value="BSO">Bell South</asp:ListItem>
													<asp:ListItem Value="SWB">SBC - Southwestern Bell</asp:ListItem>
												</asp:dropdownlist></td>
											<td style="HEIGHT: 37px" colSpan="2">&nbsp;</td>
										</tr>
										<tr class="input_titles">
											<td style="WIDTH: 238px">&nbsp;</td>
										</tr>
										<tr>
											<td colSpan="4"><IMG src="images/serviceaddy_header.jpg" border="0"></td>
										</tr>
										<TR class="input_titles">
											<td colspan="4" vAlign="middle" align="center" height="40"><asp:checkbox id="Checkbox1" tabIndex="66" runat="server" BorderStyle="Inset" BorderWidth="1px"
													BorderColor="Gray" BackColor="Gainsboro"></asp:checkbox><asp:label id="Label1" runat="server" Width="496px" ForeColor="Red" Font-Bold="True" Font-Size="Small" BorderColor="Transparent">Check box if mailing address is the same as the service address.</asp:label></td>
										</TR>
										<tr class="input_titles">
											<td style="HEIGHT: 41px" align="left" colSpan="4"><asp:label id="lblAddyWarn" runat="server" Width="585px"><font color="chocolate">
														Note:</font> Enter the customer's service address - no P.O. Box</asp:label></td>
										</tr>
										<tr class="input_titles">
											<td style="WIDTH: 238px; HEIGHT: 4px"><A onmouseover='return escape("Enter a street number if appropriate.")' href="javascript:void(0);">Street 
													Number</A>
												<asp:image id="Image4" runat="server" ImageUrl="images/asterisk.gif"></asp:image><BR>
												<asp:textbox id="txtStreetNum" tabIndex="57" runat="server" Width="88px" MaxLength="8"></asp:textbox><asp:requiredfieldvalidator id="vldStreet" runat="server" Font-Bold="True" Font-Names="Arial" ControlToValidate="txtStreetNum"
													ErrorMessage="Please enter a street number.">X</asp:requiredfieldvalidator></td>
											<td style="WIDTH: 235px; HEIGHT: 4px"><A onmouseover='return escape("Enter a direction if included in your address.")' href="javascript:void(0);">Direction</A><BR>
												<asp:dropdownlist id="txtStreetPfx" tabIndex="58" runat="server">
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
											<td style="WIDTH: 171px; HEIGHT: 4px"><A onmouseover='return escape("Enter the name of your street.")' href="javascript:void(0);">Street 
													Name</A>
												<asp:image id="Image5" runat="server" ImageUrl="images/asterisk.gif"></asp:image><BR>
												<asp:textbox id="txtStreetName" tabIndex="59" runat="server" Width="153px"></asp:textbox><asp:requiredfieldvalidator id="vldStrtName" runat="server" Font-Bold="True" Font-Names="Arial" ControlToValidate="txtStreetName"
													ErrorMessage="Please enter a street name.">X</asp:requiredfieldvalidator></td>
											<td>&nbsp;<A onmouseover='return escape("Choose the street type/suffix following your street name, i.e. Avenue, Street, etc.")'
													href="javascript:void(0);">Street Type</A><BR>
												<asp:dropdownlist id="ddlStreetType" tabIndex="60" runat="server"></asp:dropdownlist></td>
										</tr>
										<tr class="input_titles">
											<td style="WIDTH: 238px; HEIGHT: 12px"><asp:label id="Label4" runat="server" Width="96px">
													<A onmouseover='return escape("Enter a direction/suffix if declared after your street name.")'
														href="javascript:void(0);">Post Directional</A></asp:label><BR>
												<asp:dropdownlist id="ddlSfx" tabIndex="61" runat="server">
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
											<td style="WIDTH: 235px; HEIGHT: 12px"><A onmouseover='return escape("What type of unit do you live in?")' href="javascript:void(0);">Unit 
													Type</A><BR>
												<asp:dropdownlist id="ddlUnitType" tabIndex="62" runat="server">
													<asp:ListItem></asp:ListItem>
													<asp:ListItem Value="Apartment">Apartment</asp:ListItem>
													<asp:ListItem Value="BSMT">Basement</asp:ListItem>
													<asp:ListItem Value="BLDG">Building</asp:ListItem>
													<asp:ListItem Value="DEPT">Department</asp:ListItem>
													<asp:ListItem Value="FL">Floor</asp:ListItem>
													<asp:ListItem Value="FRNT">Front</asp:ListItem>
													<asp:ListItem Value="LOT">Lot</asp:ListItem>
													<asp:ListItem Value="LOWR">Lower</asp:ListItem>
													<asp:ListItem Value="PIER">Pier</asp:ListItem>
													<asp:ListItem Value="REAR">Rear</asp:ListItem>
													<asp:ListItem Value="SIDE">Side</asp:ListItem>
													<asp:ListItem Value="PAD">Pad</asp:ListItem>
													<asp:ListItem Value="STE">Suite</asp:ListItem>
													<asp:ListItem Value="RM">Room</asp:ListItem>
													<asp:ListItem Value="TRLR">Trailer</asp:ListItem>
													<asp:ListItem Value="SPC">Space</asp:ListItem>
													<asp:ListItem Value="UNIT">Unit</asp:ListItem>
													<asp:ListItem Value="SLIP">Slip</asp:ListItem>
													<asp:ListItem Value="UPPR">Upper</asp:ListItem>
												</asp:dropdownlist></td>
											<td style="HEIGHT: 12px" colSpan="2"><A onmouseover='return escape("The number of your unit.")' href="javascript:void(0);">Unit 
													Number</A><BR>
												<asp:textbox id="TxtUnit" tabIndex="63" runat="server" Width="80px" MaxLength="15"></asp:textbox></td>
										</tr>
										<tr>
											<td colSpan="4"></td>
										</tr>
										<TR class="input_titles">
											<TD style="WIDTH: 238px; HEIGHT: 38px"><A onmouseover='return escape("Enter your city.")' href="javascript:void(0);">City</A>
												<asp:image id="Image6" runat="server" ImageUrl="images/asterisk.gif"></asp:image><BR>
												<asp:textbox id="TxtCity" tabIndex="64" runat="server" MaxLength="20"></asp:textbox><asp:requiredfieldvalidator id="vldCity" runat="server" Font-Bold="True" Font-Names="Arial" ControlToValidate="TxtCity"
													ErrorMessage="Please enter a city.">X</asp:requiredfieldvalidator><asp:regularexpressionvalidator id="vldCityAlpha" runat="server" Font-Bold="True" Font-Names="Arial" ControlToValidate="TxtCity"
													ErrorMessage="Please enter letters in the city field." ValidationExpression="[a-zA-Z \.  \- ]{1,20}">X</asp:regularexpressionvalidator></TD>
											<TD style="WIDTH: 235px; HEIGHT: 38px"><A onmouseover='return escape("Enter your state.")' href="javascript:void(0);">State</A><BR>
												<asp:dropdownlist id="txtState" tabIndex="65" runat="server" Enabled="False">
													<asp:ListItem></asp:ListItem>
													<asp:ListItem Value="AL">AL - Alabama</asp:ListItem>
													<asp:ListItem Value="AK">AK - Alaska</asp:ListItem>
													<asp:ListItem Value="AR">AR - Arkansas</asp:ListItem>
													<asp:ListItem Value="AZ">AZ - Arizona</asp:ListItem>
													<asp:ListItem Value="CA">CA - California</asp:ListItem>
													<asp:ListItem Value="CO">CO - Colorado</asp:ListItem>
													<asp:ListItem Value="CT">CT - Connecticut</asp:ListItem>
													<asp:ListItem Value="DE">DE - Delaware</asp:ListItem>
													<asp:ListItem Value="DC">DC - District of Columbia</asp:ListItem>
													<asp:ListItem Value="FL">FL - Florida</asp:ListItem>
													<asp:ListItem Value="GA">GA - Georgia</asp:ListItem>
													<asp:ListItem Value="HI">HI - Hawaii</asp:ListItem>
													<asp:ListItem Value="ID">ID - Idaho</asp:ListItem>
													<asp:ListItem Value="IL">IL - Illinois</asp:ListItem>
													<asp:ListItem Value="IN">IN - Indiana</asp:ListItem>
													<asp:ListItem Value="IA">IA - Iowa</asp:ListItem>
													<asp:ListItem Value="KS">KS - Kansas</asp:ListItem>
													<asp:ListItem Value="KY">KY - Kentucky</asp:ListItem>
													<asp:ListItem Value="LA">LA - Louisiana</asp:ListItem>
													<asp:ListItem Value="ME">ME - Maine</asp:ListItem>
													<asp:ListItem Value="MD">MD - Maryland</asp:ListItem>
													<asp:ListItem Value="MA">MA - Massachusetts</asp:ListItem>
													<asp:ListItem Value="MI">MI - Michigan</asp:ListItem>
													<asp:ListItem Value="MN">MN - Minnesota</asp:ListItem>
													<asp:ListItem Value="MS">MS - Mississippi</asp:ListItem>
													<asp:ListItem Value="MO">MO - Missouri</asp:ListItem>
													<asp:ListItem Value="MT">MT - Montana</asp:ListItem>
													<asp:ListItem Value="NE">NE - Nebraska</asp:ListItem>
													<asp:ListItem Value="NV">NV - Nevada</asp:ListItem>
													<asp:ListItem Value="NH">NH - New Hampshire</asp:ListItem>
													<asp:ListItem Value="NJ">NJ - New Jersey</asp:ListItem>
													<asp:ListItem Value="NM">NM - New Mexico</asp:ListItem>
													<asp:ListItem Value="NY">NY - New York</asp:ListItem>
													<asp:ListItem Value="NC">NC - North Carolina</asp:ListItem>
													<asp:ListItem Value="ND">ND - North Dakota</asp:ListItem>
													<asp:ListItem Value="OH">OH - Ohio</asp:ListItem>
													<asp:ListItem Value="OK">OK - Oklahoma</asp:ListItem>
													<asp:ListItem Value="OR">OR - Oregon</asp:ListItem>
													<asp:ListItem Value="PA">PA - Pennsylvania</asp:ListItem>
													<asp:ListItem Value="PR">PR - Puerto Rico</asp:ListItem>
													<asp:ListItem Value="RI">RI - Rhode Island</asp:ListItem>
													<asp:ListItem Value="SC">SC - South Carolina</asp:ListItem>
													<asp:ListItem Value="SD">SD - South Dakota</asp:ListItem>
													<asp:ListItem Value="TN">TN - Tennessee</asp:ListItem>
													<asp:ListItem Value="TX">TX - Texas</asp:ListItem>
													<asp:ListItem Value="UT">UT - Utah</asp:ListItem>
													<asp:ListItem Value="VT">VT - Vermont</asp:ListItem>
													<asp:ListItem Value="VI">VI - Virgin Islands</asp:ListItem>
													<asp:ListItem Value="VA">VA - Virginia</asp:ListItem>
													<asp:ListItem Value="WA">WA - Washington</asp:ListItem>
													<asp:ListItem Value="WV">WV - West Virginia</asp:ListItem>
													<asp:ListItem Value="WI">WI - Wisconsin</asp:ListItem>
													<asp:ListItem Value="WY">WY - Wyoming</asp:ListItem>
												</asp:dropdownlist></TD>
											<TD style="HEIGHT: 38px" colSpan="2"><A onmouseover='return escape("Selected zip.")' href="javascript:void(0);">Zip</A><BR>
												<asp:label id="TxtZip" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
													ForeColor="Black" Width="144px"></asp:label></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 238px"><asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg" CausesValidation="False"></asp:imagebutton></TD>
											<TD style="WIDTH: 235px">&nbsp;</TD>
											<TD align="right" colSpan="2"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton>&nbsp;&nbsp;
											</TD>
										</TR>
									</table>
								</td>
							</tr>
							<TR>
								<TD class="05_con_medium" align="center" width="100%" colSpan="5">&nbsp;
									<asp:label id="lblError" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="Red"></asp:label><asp:validationsummary id="ValidationSummary1" runat="server"></asp:validationsummary></TD>
							</TR>
						</table>
					</td>
				</tr>
				<TR>
					<TD colSpan="2"><dpiuser:sitefooter id="Sitefooter1" runat="server"></dpiuser:sitefooter></TD>
				</TR>
			</table>
		</form>
		<SCRIPT language="JavaScript" src="../Core/wz_tooltip.js" type="text/javascript"></SCRIPT>
		<SCRIPT language="JavaScript">
			window.history.forward(1);
		</SCRIPT>
	</body>
</HTML>
