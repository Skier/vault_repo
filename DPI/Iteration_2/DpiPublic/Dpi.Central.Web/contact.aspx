<%@ Page CodeBehind="contact.aspx.cs" Language="c#" AutoEventWireup="false" Inherits="Dpi.Central.Web.ContactPage" %>
<%@ Register TagPrefix="dns" TagName="Header" Src="header.ascx" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="footer.ascx" %>
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC</title>
		<meta http-equiv="Content-Language" content="en-us">
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
	</HEAD>
	<body>
		<table id="table1" cellSpacing="0" cellPadding="0" width="792" border="0">
			<tr>
				<td><dns:header id="_header" runat="server"></dns:header></td>
			</tr>
		</table>
		<table id="table1" cellSpacing="0" cellPadding="0" width="792" border="0">
			<tr>
				<td>
					<table id="table1" cellSpacing="0" cellPadding="0" width="792" border="0">
						<tr>
							<td vAlign="top" align="left"><IMG src="images/contact_side.jpg" width="133" border="0"></td>
							<td vAlign="top" align="left">
								<form id="Form1" name="frmContact" method="post" runat="server">
									<table id="table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
										<tr>
											<td><IMG height="276" src="images/contact_top_l.jpg" width="316" border="0"></td>
											<td><IMG height="276" src="images/contact_top_r.jpg" width="343" border="0"></td>
										</tr>
										<tr>
											<td colSpan="2">
												<script language="JavaScript">
  function checkValues() {
    if (document.frmContact.txtName.value == "") {
      alert("Please enter your full name.");
      document.frmContact.txtName.focus();
      return; }
    if (document.frmContact.txtEMail.value == "") {
      alert("Please enter your e-mail address.");
      document.frmContact.txtEMail.focus();
      return; }
	if (document.frmContact.city.value == "") {
      alert("Please enter your city.");
      document.frmContact.city.focus();
      return; }
	if (document.frmContact.address.value == "") {
      alert("Please enter your address.");
      document.frmContact.address.focus();
      return; }
	if (document.frmContact.state.value == "Select") {
      alert("Please select your home state from the drop-down list.");
      document.frmContact.state.focus();
      return; }
    if (document.frmContact.customer.value == "Select") {
      alert("Please tell us whether or not you are an existing customer.");
      document.frmContact.customer.focus();
      return; }
    if (document.frmContact.txtMessage.value == "") {
      alert("Please enter your comment.");
      document.frmContact.txtMessage.focus();
      return; }
    document.frmContact.submit(); }
												</script>
												<font face="Tahoma,Verdana,Arial" size="-1">
													<table id="table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
														<tr>
															<td align="left" width="53%"><font face="Tahoma,Verdana,Arial" size="-1">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 15px"><b><font color="#6b6b6b" size="2">Full 
																				Name:</font></b>
																</font></P></td>
															<td align="left" width="47%" colSpan="2"><font face="Tahoma,Verdana,Arial" size="-1">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 25px"><b><font color="#6b6b6b" size="2">Email 
																				Address:</font></b>
																</font></P></td>
														</tr>
														<tr>
															<td align="left" width="53%">
																<p style="MARGIN-LEFT: 15px"><font face="Tahoma,Verdana,Arial" size="-1"><input id="_nameTextBox" tabIndex="1" type="text" maxLength="64" size="35" name="txtName"
																			runat="server"><font color="#db6c1d">*</font></font></p>
															</td>
															<td align="left" width="47%" colSpan="2"><p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 5px; MARGIN-LEFT: 25px"><font face="Tahoma,Verdana,Arial" size="-1"><input id="_emailTextBox" tabIndex="2" type="text" maxLength="128" size="35" name="txtEMail"
																			runat="server"><font color="#db6c1d">*</font></font></p>
															</td>
														</tr>
														<tr>
															<td align="left" width="53%"><font face="Tahoma,Verdana,Arial" size="-1">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 15px"><b><font color="#6b6b6b" size="2">Home 
																				Phone Number:</font></b>
																</font></P></td>
															<td align="left" width="47%" colSpan="2"><font face="Tahoma,Verdana,Arial" size="-1">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 25px"><b><font color="#6b6b6b" size="2">&nbsp;Alternate 
																				Phone Number:</font></b>
																</font></P></td>
														</tr>
														<tr>
															<td align="left" width="53%">
																<p style="MARGIN-LEFT: 15px"><font face="Tahoma,Verdana,Arial" size="-1"><input id="_phoneNumberTextBox" tabIndex="3" type="text" maxLength="12" size="12" name="txtPhoneNumber"
																			runat="server"><font color="#db6c1d">*</font></font></p>
															</td>
															<td align="left" width="47%" colSpan="2"><p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 5px; MARGIN-LEFT: 25px"><font face="Tahoma,Verdana,Arial" size="-1"><input id="_altPhoneNumberTextBox" tabIndex="4" type="text" maxLength="12" size="12" name="altPhoneNumber"
																			runat="server"></font></p>
															</td>
														</tr>
														<tr>
															<td align="left" width="53%"><font face="Tahoma,Verdana,Arial" color="#6b6b6b" size="-1">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 15px"><b>&nbsp;Address:</b>
																</font></P></td>
															<td align="left" width="47%" colSpan="2"><p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 25px"></p>
															</td>
														</tr>
														<tr>
															<td align="left" width="99%" colSpan="3"><p style="MARGIN-LEFT: 15px"><font face="Tahoma,Verdana,Arial" size="-1"><input id="_addressTextBox" tabIndex="5" type="text" maxLength="50" size="50" name="address"
																			runat="server"><font color="#db6c1d">*</font></font></p>
															</td>
														</tr>
														<tr>
															<td align="left" width="53%"><font face="Tahoma,Verdana,Arial" color="#6b6b6b" size="-1">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 15px"><b>&nbsp;City:</b>
																</font></P></td>
															<td align="left" width="22%"><font face="Tahoma,Verdana,Arial" color="#6b6b6b" size="-1">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 25px"><b>State:</b>
																</font></P></td>
															<td align="left" width="25%"><font face="Tahoma,Verdana,Arial" color="#6b6b6b" size="-1">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 27px"><b>Zip Code:</b>
																</font></P></td>
														</tr>
														<tr>
															<td align="left" width="53%">
																<p style="MARGIN-LEFT: 15px"><font face="Tahoma,Verdana,Arial" size="-1"><input id="_cityTextBox" tabIndex="6" type="text" maxLength="50" size="35" name="city"
																			runat="server"><font color="#db6c1d">*</font></font></p>
															</td>
															<td align="left" width="22%">
																<p style="MARGIN-LEFT: 25px"><font face="Tahoma,Verdana,Arial" size="-1"><select id="_stateSelect" tabIndex="7" size="1" name="state" runat="server">
																			<option value="Select" selected>Select</option>
																			<option value="Alabama">Alabama</option>
																			<option value="Alaska">Alaska</option>
																			<option value="Arizona">Arizona</option>
																			<option value="Arkansas">Arkansas</option>
																			<option value="California">California</option>
																			<option value="Colorado">Colorado</option>
																			<option value="Connecticut">Connecticut</option>
																			<option value="Delaware">Delaware</option>
																			<option value="Florida">Florida</option>
																			<option value="Georgia">Georgia</option>
																			<option value="Hawaii">Hawaii</option>
																			<option value="Iowa">Iowa</option>
																			<option value="Idaho">Idaho</option>
																			<option value="Illinois">Illinois</option>
																			<option value="Indiana">Indiana</option>
																			<option value="Kansas">Kansas</option>
																			<option value="Kentucky">Kentucky</option>
																			<option value="Louisiana">Louisiana</option>
																			<option value="Maine">Maine</option>
																			<option value="Maryland">Maryland</option>
																			<option value="Massachusetts">Massachusetts</option>
																			<option value="Michigan">Michigan</option>
																			<option value="Minnesota">Minnesota</option>
																			<option value="Missouri">Missouri</option>
																			<option value="Mississippi">Mississippi</option>
																			<option value="Montana">Montana</option>
																			<option value="Nebraska">Nebraska</option>
																			<option value="Nevada">Nevada</option>
																			<option value="New Hampshire">New Hampshire</option>
																			<option value="New Jersey">New Jersey</option>
																			<option value="New Mexico">New Mexico</option>
																			<option value="New York">New York</option>
																			<option value="North Carolina">North Carolina</option>
																			<option value="North Dakota">North Dakota</option>
																			<option value="Ohio">Ohio</option>
																			<option value="Oklahoma">Oklahoma</option>
																			<option value="Oregon">Oregon</option>
																			<option value="Pennsylvania">Pennsylvania</option>
																			<option value="Rhode Island">Rhode Island</option>
																			<option value="South Carolina">South Carolina</option>
																			<option value="South Dakota">South Dakota</option>
																			<option value="Tennessee">Tennessee</option>
																			<option value="Texas">Texas</option>
																			<option value="Utah">Utah</option>
																			<option value="Virginia">Virginia</option>
																			<option value="Vermont">Vermont</option>
																			<option value="Washington">Washington</option>
																			<option value="West Virginia">West Virginia</option>
																			<option value="Wisconsin">Wisconsin</option>
																			<option value="Wyoming">Wyoming</option>
																		</select><font color="#db6c1d">*</font></font></p>
															</td>
															<td align="left" width="25%">
																<p style="MARGIN-LEFT: 27px"><font face="Tahoma,Verdana,Arial" size="-1"><input id="_zipTextBox" tabIndex="8" type="text" maxLength="12" size="12" name="zipcode"
																			runat="server"><font color="#db6c1d">*</font></font></p>
															</td>
														</tr>
														<tr>
															<td align="center" width="99%" colSpan="3">
																<p style="MARGIN-TOP: 10px; MARGIN-BOTTOM: 7px; MARGIN-RIGHT: 20px"><IMG height="2" src="images/seperator.jpg" width="605" border="0"></p>
															</td>
														</tr>
														<tr>
															<td align="left" width="53%"><font face="Tahoma,Verdana,Arial" color="#6b6b6b" size="-1">
																	<p style="MARGIN-TOP: 2px; MARGIN-BOTTOM: 7px; MARGIN-LEFT: 15px"><b>Are you an 
																			existing customer?</b>
																</font></P></td>
															<td align="left" width="47%" colSpan="2">&nbsp;
															</td>
														</tr>
														<tr>
															<td align="left" width="53%">
																<p style="MARGIN-LEFT: 15px"><font face="Tahoma,Verdana,Arial" size="-1"><select id="_existingCustomerSelect" tabIndex="9" size="1" name="customer" runat="server">
																			<option value="Select" selected>Select</option>
																			<option value="Yes">Yes</option>
																			<option value="No">No</option>
																		</select><font color="#db6c1d">*</font></font></p>
															</td>
															<td align="left" width="47%" colSpan="2">&nbsp;
															</td>
														</tr>
														<tr>
															<td align="right" width="53%"><font face="Tahoma,Verdana,Arial" color="#6b6b6b" size="-1">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 5px; MARGIN-LEFT: 15px" align="left"><b>I 
																			would like more information about?</b>
																</font></P></td>
															<td width="47%" colSpan="2">
																<p style="MARGIN-BOTTOM: 0px; MARGIN-LEFT: 15px"></p>
															</td>
														</tr>
														<tr>
															<td align="left" width="53%">
																<p style="MARGIN-BOTTOM: 0px; MARGIN-LEFT: 75px"><font face="Tahoma,Verdana,Arial" color="#6b6b6b" size="-1"><input id="_pphpCheckBox" tabIndex="10" type="checkbox" value="ON" name="pphp" runat="server">Pre-Paid 
																		Home Phone Service</font></p>
															</td>
															<td align="left" width="47%" colSpan="2"><p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 75px"><font face="Tahoma,Verdana,Arial" color="#6b6b6b" size="-1"><input id="_ppmcCheckBox" tabIndex="13" type="checkbox" value="ON" name="ppmc" runat="server">Pre-Paid 
																		MasterCard</font></p>
															</td>
														</tr>
														<tr>
															<td align="left" width="53%">
																<p style="MARGIN-BOTTOM: 0px; MARGIN-LEFT: 75px"><font face="Tahoma,Verdana,Arial" color="#6b6b6b" size="-1"><input id="_ppldCheckBox" tabIndex="11" type="checkbox" value="ON" name="ppld" runat="server">Long 
																		Distance</font></p>
															</td>
															<td align="left" width="47%" colSpan="2"><p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 75px"><font face="Tahoma,Verdana,Arial" color="#6b6b6b" size="-1"><input id="_ppiCheckBox" tabIndex="14" type="checkbox" value="ON" name="ppi" runat="server">Pre-Paid 
																		Internet</font></p>
															</td>
														</tr>
														<tr>
															<td align="left" width="53%">
																<p style="MARGIN-BOTTOM: 0px; MARGIN-LEFT: 75px"><font face="Tahoma,Verdana,Arial" color="#6b6b6b" size="-1"><input id="_ppcCheckBox" tabIndex="12" type="checkbox" value="ON" name="ppc" runat="server">Pre-Paid 
																		Cellular</font></p>
															</td>
															<td align="left" width="47%" colSpan="2"><p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 75px"><font face="Tahoma,Verdana,Arial" color="#6b6b6b" size="-1"><input id="_bpCheckBox" tabIndex="15" type="checkbox" value="ON" name="bps" runat="server">Bill 
																		Pay Services</font></p>
															</td>
														</tr>
														<tr>
															<td align="center" width="99%" colSpan="3">
																<p style="MARGIN-TOP: 10px; MARGIN-BOTTOM: 7px; MARGIN-RIGHT: 20px"><IMG height="2" src="images/seperator.jpg" width="605" border="0"></p>
															</td>
														</tr>
														<tr>
															<td align="left" width="53%"><font face="Tahoma,Verdana,Arial" size="-1">
																	<p style="MARGIN-TOP: 2px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 15px"><b><font color="#6b6b6b" size="2">Comment 
																				or Question:</font></b>
																</font></P></td>
															<td align="left" width="47%" colSpan="2"><p style="MARGIN-TOP: 2px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 15px"></p>
															</td>
														</tr>
														<tr>
															<td vAlign="top" align="left" width="99%" colSpan="3" height="75">
																<p style="MARGIN-LEFT: 15px"><font face="Tahoma,Verdana,Arial" size="-1"><textarea id="_messageTextArea" tabIndex="16" name="txtMessage" rows="4" cols="73" runat="server"
																			input></textarea></font></p>
															</td>
														</tr>
														<tr>
															<td align="right" width="99%" colSpan="3"><font face="Tahoma,Verdana,Arial" size="-1">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 5px; MARGIN-LEFT: 15px" align="left"><asp:button id="_submitButton" runat="server" Width="70px" Text="Submit"></asp:button>
																</font></P></td>
														</tr>
													</table>
												</font>
											</td>
										</tr>
										<tr>
											<td colSpan="2" height="52">
												<p align="center"><font face="Arial" color="#6b6b6b"><span style="FONT-WEIGHT: 700"><font style="FONT-SIZE: 11px">dPi 
																Teleconnect</font></span><span style="FONT-WEIGHT: 700; FONT-SIZE: 11px"><br>
														</span><span style="FONT-WEIGHT: 700"><font size="1">2997 LBJ Freeway, Suite 225<br>
																Dallas, Texas 75234<br>
																Customer Service: 1-877 JOIN dPi (564-6374)</font></span></font></p>
											</td>
										</tr>
										<tr>
											<td colSpan="2">
												<p align="center"><dns:footer id="_footer" runat="server"></dns:footer></p>
											</td>
										</tr>
									</table>
								</form>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<p><span style="VISIBILITY: hidden">
				<SCRIPT language="Javascript" src="http://www.dpiteleconnect.com/counter/counter.php?page=contact"><!--
//--></SCRIPT>
		</p>
		</SPAN>
	</body>
</HTML>
