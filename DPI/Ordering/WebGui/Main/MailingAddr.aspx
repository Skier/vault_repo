<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Page language="c#" Codebehind="MailingAddr.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.MailingAddr" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>MailingAddress</title>
		<script language="JavaScript">
		<!---
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
		//--->
		</script>
</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
<TABLE height=803 cellSpacing=0 cellPadding=0 width=541 border=0 
ms_2d_layout="TRUE">
  <TR vAlign=top>
    <TD width=541 height=803>
					<form id="Form1" onsubmit="return check(); " action="post" runat="server">
      <TABLE height=539 cellSpacing=0 cellPadding=0 width=801 border=0 
      ms_2d_layout="TRUE">
        <TR vAlign=top>
          <TD width=801 height=539>
									<table height="538" cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
										<tr>
											<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
										</tr>
										<tr>
											<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
												height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
											<td vAlign="top" width="660">
												<table height="490" cellSpacing="0" cellPadding="0" width="660" align="left" border="0">
													<tr>
														<td width="100%" colSpan="5"></td>
													</tr>
													<tr>
														<td class="05_con_sublabel_zip" vAlign="middle" align="left" background="images/subtable_header_blank.jpg"
															bgColor="white" colSpan="5" height="61">&nbsp;&nbsp; 
															&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label id="Label5" runat="server" Width="176px" ForeColor="Chocolate" Font-Size="Small"
																Font-Names="Arial" Font-Bold="True" Height="22px">Mailing Address</asp:Label>
															&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ZipCode:
															<asp:label id="lblZipCode" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp;
															<asp:label id="lblIlec" runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp; 
															&nbsp;&nbsp;&nbsp;&nbsp;
														</td>
													</tr>
													<tr>
														<td class="05_con_medium" align="center" width="100%" colSpan="6"><asp:label id="lblError" runat="server" Font-Names="Arial" ForeColor="Red" Font-Bold="True"></asp:label>&nbsp;
														</td>
													</tr>
													<tr>
														<td rowSpan="8">&nbsp;&nbsp;&nbsp;</td>
														<td class="faqs" colSpan="4">
															<table border="0" width="100%">
																<tr>
																	<td class="faqs" width="96">
																		<b>First Name:</b>
																	</td>
																	<td>
																		<asp:label id="lblFirstName" runat="server" Font-Names="Arial" ForeColor="Black" Font-Bold="True"
																			Width="128px"></asp:label>
																	</td>
																	<td class="faqs" width="107">
																		<b>Last name:</b>
																	</td>
																	<td>
																		<asp:label id="lblLastName" runat="server" Font-Names="Arial" ForeColor="Black" Font-Bold="True"
																			Width="160px"></asp:label>
																	</td>
																</tr>
																<tr>
																	<td class="faqs" width="96">
																		<b>Birthday:</b>
																	</td>
																	<td>
																		<asp:label id="lblBDay" runat="server" Font-Names="Arial" ForeColor="Black" Font-Bold="True"
																			Width="144px"></asp:label>
																	</td>
																	<td class="faqs" width="107">
																		<b>Email:</b>
																	</td>
																	<td>
																		<asp:label id="lblEmail" runat="server" Font-Names="Arial" ForeColor="Black" Font-Bold="True"
																			Width="184px"></asp:label>
																	</td>
																</tr>
																<tr>
																	<td class="faqs" width="96">
																		<b>Contact #:</b>
																	</td>
																	<td>
																		<asp:label id="lblContact" runat="server" Font-Names="Arial" ForeColor="Black" Font-Bold="True"
																			Width="136px"></asp:label>
																	</td>
																	<td class="faqs" width="107">
																		<b>2nd Contact #:</b>
																	</td>
																	<td>
																		<asp:label id="lblContact2" runat="server" Font-Names="Arial" ForeColor="Black" Font-Bold="True"
																			Width="128px"></asp:label>
																	</td>
																</tr>
															</table>
														</td>
														<td rowSpan="8">&nbsp;&nbsp;</td>
													</tr>
													<tr>
														<td vAlign="top" colSpan="4">&nbsp;
														</td>
													</tr>
													<tr>
														<td vAlign="top" colSpan="4" height="18"><asp:label id="Label3" runat="server" Width="176px">Denotes Mandatory Fields</asp:label><asp:image id="Image9" runat="server" ImageUrl="images/asterisk.gif"></asp:image></td>
													</tr>
													<tr>
														<td class="05_con_label" width="182"><A onmouseover='return escape("Enter a street number if appropriate.")' href="javascript:void(0);">Street 
																Number</A>&nbsp;
															<asp:image id="Image1" runat="server" ImageUrl="images/asterisk.gif"></asp:image><br>
															<asp:textbox id="txtStreetNum" tabIndex="1" runat="server" Width="88px" MaxLength="10"></asp:textbox></td>
														<td class="05_con_label" width="184" height="55"><A onmouseover='return escape("Enter a direction if included in your address.")' href="javascript:void(0);">Direction</A><BR>
															<asp:dropdownlist id="txtStreetPfx" tabIndex="2" runat="server">
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
														<td class="05_con_label" width="139"><A onmouseover='return escape("Enter the name of your street.")' href="javascript:void(0);">Street 
																Name</A>
															<asp:image id="Image2" runat="server" ImageUrl="images/asterisk.gif"></asp:image><BR>
															<asp:textbox id="txtStreetName" tabIndex="3" runat="server" Width="120px"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" Font-Names="Arial" Font-Bold="True"
																Font-Size="Smaller" ControlToValidate="txtStreetName" ErrorMessage="Please enter a street name.">X</asp:requiredfieldvalidator></td>
														<td>&nbsp;<A onmouseover='return escape("Choose the street type/suffix following your street name, i.e. Avenue, Street, etc.")'
																href="javascript:void(0);">St. Type</A><BR>
															<asp:dropdownlist id="ddlStreetType" tabIndex="4" runat="server" Width="88px"></asp:dropdownlist></td>
													</tr>
													<tr>
														<td class="05_con_label" width="182"><asp:label id="Label1" runat="server" Width="96px">
																<A onmouseover='return escape("Enter a direction/suffix if declared after your street name.")'
																	href="javascript:void(0);">Post Directional</A></asp:label><BR>
															<asp:dropdownlist id="ddlSfx" tabIndex="5" runat="server">
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
														<td class="05_con_label" width="184"><A onmouseover='return escape("What type of unit do you live in?")' href="javascript:void(0);">Unit 
																Type</A><BR>
															<asp:dropdownlist id="ddlUnitType" tabIndex="6" runat="server">
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
														<td class="05_con_label" width="139">&nbsp;<A onmouseover='return escape("The number of your unit.")' href="javascript:void(0);">Unit 
																Number</A><BR>
															<asp:textbox id="TxtUnit" tabIndex="7" runat="server" MaxLength="15"></asp:textbox></td>
														<td>&nbsp;</td>
													</tr>
													<tr>
														<td class="05_con_label" width="182" height="53"><A onmouseover='return escape("Enter your city.")' href="javascript:void(0);">City</A><br>
															<asp:image id="Image3" runat="server" ImageUrl="images/asterisk.gif"></asp:image><br>
															<asp:textbox id="TxtCity" tabIndex="8" runat="server" MaxLength="20"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" Font-Names="Arial" Font-Bold="True"
																ControlToValidate="TxtCity" ErrorMessage="Please enter a city.">X</asp:requiredfieldvalidator><asp:regularexpressionvalidator id="vldCityAlpha" runat="server" Font-Names="Arial" Font-Bold="True" ControlToValidate="TxtCity"
																ErrorMessage="Please enter letters in the city field." ValidationExpression="[a-zA-Z \.  \- ]{1,20}">X</asp:regularexpressionvalidator></td>
														<td width="184" height="53"><A onmouseover='return escape("Enter your state.")' href="javascript:void(0);">State</A>
															<asp:image id="Image4" runat="server" ImageUrl="images/asterisk.gif"></asp:image><br>
															<asp:dropdownlist id="txtState" tabIndex="9" runat="server">
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
															</asp:dropdownlist><asp:requiredfieldvalidator id="vldState" runat="server" Font-Names="Arial" Font-Bold="True" ControlToValidate="txtState"
																ErrorMessage="Please enter a state." InitialValue=" ">X</asp:requiredfieldvalidator></td>
														<td width="139" height="53"><A onmouseover='return escape("Selected zip.")' href="javascript:void(0);">Zip</A><br>
															<asp:image id="Image5" runat="server" ImageUrl="images/asterisk.gif"></asp:image><BR>
															<asp:textbox id="TxtZip" tabIndex="10" runat="server" Width="120px" MaxLength="10"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" Font-Names="Arial" Font-Bold="True"
																ControlToValidate="TxtZip" ErrorMessage="Please enter a zipcode.">X</asp:requiredfieldvalidator></td>
														<td height="53">&nbsp;
															<asp:Label id="Label2" runat="server" Visible="False"></asp:Label></td>
													</tr>
													<tr>
														<td colSpan="5" height="100%">&nbsp;</td>
													</tr>
													<tr>
														<td align="left" width="379" colSpan="2" height="100%">&nbsp;&nbsp;&nbsp;<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg" CausesValidation="False"></asp:imagebutton></td>
														<td align="right" colSpan="3" height="100%"><asp:imagebutton id="btnNext" runat="server" ImageUrl="images/btn_proceed.jpg"></asp:imagebutton>&nbsp;&nbsp;&nbsp;</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td colSpan="2"><dpiuser:sitefooter id="Sitefooter1" runat="server"></dpiuser:sitefooter></td>
										</tr>
									</table></TD></TR></TABLE>
					</form></TD></TR></TABLE>
	</body>
</HTML>
