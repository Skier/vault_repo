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
		<table border="0" cellpadding="0" cellspacing="0" width="792" id="table1">
			<tr>
				<td><dns:Header id="_header" runat="server" /></td>
			</tr>
		</table>
		<table border="0" width="792" cellspacing="0" cellpadding="0" id="table1">
			<tr>
				<td><table border="0" cellpadding="0" cellspacing="0" width="792" id="table1">
						<tr>
							<td align="left" valign="top">
								<img border="0" src="images/contact_side.jpg" width="133"></td>
							<td align="left" valign="top">
								<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table2">
									<tr>
										<td>
											<img border="0" src="images/contact_top_l.jpg" width="316" height="276"></td>
										<td>
											<img border="0" src="images/contact_top_r.jpg" width="343" height="276"></td>
									</tr>
									<tr>
										<td colspan="2">
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
												<form name="frmContact" method="post" runat="server">
													<table border="0" cellpadding="0" cellspacing="0" width="100%" id="table3">
														<tr>
															<td width="53%" align="left">
																<font face="Tahoma,Verdana,Arial" size="-1">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 15px">
																		<b><font size="2" color="#6b6b6b">Full Name:</font></b>
																</font></P>
															</td>
															<td width="47%" align="left" colspan="2">
																<font face="Tahoma,Verdana,Arial" size="-1">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 25px">
																		<b><font size="2" color="#6b6b6b">Email Address:</font></b>
																</font></P>
															</td>
														</tr>
														<tr>
															<td width="53%" align="left">
																<p style="MARGIN-LEFT: 15px">
																	<font face="Tahoma,Verdana,Arial" size="-1"><input name="txtName" type="text" maxlength="64" size="35" tabindex="1" id="_nameTextBox"
																			runat="server"><font color="#db6c1d">*</font></font></p>
															</td>
															<td width="47%" align="left" colspan="2">
																<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 5px; MARGIN-LEFT: 25px">
																	<font face="Tahoma,Verdana,Arial" size="-1"><input name="txtEMail" type="text" maxlength="128" size="35" tabindex="2" id="_emailTextBox"
																			runat="server"><font color="#db6c1d">*</font></font></p>
															</td>
														</tr>
														<tr>
															<td width="53%" align="left">
																<font face="Tahoma,Verdana,Arial" size="-1">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 15px">
																		<b><font size="2" color="#6b6b6b">Home Phone Number:</font></b>
																</font></P>
															</td>
															<td width="47%" align="left" colspan="2">
																<font face="Tahoma,Verdana,Arial" size="-1">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 25px">
																		<b><font size="2" color="#6b6b6b">&nbsp;Alternate Phone Number:</font></b>
																</font></P>
															</td>
														</tr>
														<tr>
															<td width="53%" align="left">
																<p style="MARGIN-LEFT: 15px">
																	<font face="Tahoma,Verdana,Arial" size="-1"><input name="txtPhoneNumber" type="text" maxlength="12" size="12" tabindex="3" id="_phoneNumberTextBox"
																			runat="server"><font color="#db6c1d">*</font></font></p>
															</td>
															<td width="47%" align="left" colspan="2">
																<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 5px; MARGIN-LEFT: 25px">
																	<font face="Tahoma,Verdana,Arial" size="-1"><input name="altPhoneNumber" type="text" maxlength="12" size="12" tabindex="4" id="_altPhoneNumberTextBox"
																			runat="server"></font></p>
															</td>
														</tr>
														<tr>
															<td width="53%" align="left">
																<font face="Tahoma,Verdana,Arial" size="-1" color="#6b6b6b">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 15px"><b>&nbsp;Address:</b>
																</font></P>
															</td>
															<td width="47%" align="left" colspan="2">
																<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 25px"></p>
															</td>
														</tr>
														<tr>
															<td width="99%" align="left" colspan="3">
																<p style="MARGIN-LEFT: 15px">
																	<font face="Tahoma,Verdana,Arial" size="-1"><input name="address" type="text" maxlength="50" size="50" tabindex="5" id="_addressTextBox"
																			runat="server"><font color="#db6c1d">*</font></font></p>
															</td>
														</tr>
														<tr>
															<td width="53%" align="left">
																<font face="Tahoma,Verdana,Arial" size="-1" color="#6b6b6b">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 15px"><b>&nbsp;City:</b>
																</font></P>
															</td>
															<td width="22%" align="left">
																<font face="Tahoma,Verdana,Arial" size="-1" color="#6b6b6b">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 25px"><b>State:</b>
																</font></P>
															</td>
															<td width="25%" align="left">
																<font face="Tahoma,Verdana,Arial" size="-1" color="#6b6b6b">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 27px"><b>Zip Code:</b>
																</font></P>
															</td>
														</tr>
														<tr>
															<td width="53%" align="left">
																<p style="MARGIN-LEFT: 15px">
																	<font face="Tahoma,Verdana,Arial" size="-1"><input name="city" type="text" maxlength="50" size="35" tabindex="6" id="_cityTextBox"
																			runat="server"><font color="#db6c1d">*</font></font></p>
															</td>
															<td width="22%" align="left">
																<p style="MARGIN-LEFT: 25px">
																	<font face="Tahoma,Verdana,Arial" size="-1">
																		<select name="state" size="1" tabindex="7" id="_stateSelect" runat="server">
																			<option value="Select" selected>
																				Select</option>
																			<option value="Alabama">
																				Alabama</option>
																			<option value="Alaska">
																				Alaska</option>
																			<option value="Arizona">
																				Arizona</option>
																			<option value="Arkansas">
																				Arkansas</option>
																			<option value="California">
																				California</option>
																			<option value="Colorado">
																				Colorado</option>
																			<option value="Connecticut">
																				Connecticut</option>
																			<option value="Delaware">
																				Delaware</option>
																			<option value="Florida">
																				Florida</option>
																			<option value="Georgia">
																				Georgia</option>
																			<option value="Hawaii">
																				Hawaii</option>
																			<option value="Iowa">
																				Iowa</option>
																			<option value="Idaho">
																				Idaho</option>
																			<option value="Illinois">
																				Illinois</option>
																			<option value="Indiana">
																				Indiana</option>
																			<option value="Kansas">
																				Kansas</option>
																			<option value="Kentucky">
																				Kentucky</option>
																			<option value="Louisiana">
																				Louisiana</option>
																			<option value="Maine">
																				Maine</option>
																			<option value="Maryland">
																				Maryland</option>
																			<option value="Massachusetts">
																				Massachusetts</option>
																			<option value="Michigan">
																				Michigan</option>
																			<option value="Minnesota">
																				Minnesota</option>
																			<option value="Missouri">
																				Missouri</option>
																			<option value="Mississippi">
																				Mississippi</option>
																			<option value="Montana">
																				Montana</option>
																			<option value="Nebraska">
																				Nebraska</option>
																			<option value="Nevada">
																				Nevada</option>
																			<option value="New Hampshire">
																				New Hampshire</option>
																			<option value="New Jersey">
																				New Jersey</option>
																			<option value="New Mexico">
																				New Mexico</option>
																			<option value="New York">
																				New York</option>
																			<option value="North Carolina">
																				North Carolina</option>
																			<option value="North Dakota">
																				North Dakota</option>
																			<option value="Ohio">
																				Ohio</option>
																			<option value="Oklahoma">
																				Oklahoma</option>
																			<option value="Oregon">
																				Oregon</option>
																			<option value="Pennsylvania">
																				Pennsylvania</option>
																			<option value="Rhode Island">
																				Rhode Island</option>
																			<option value="South Carolina">
																				South Carolina</option>
																			<option value="South Dakota">
																				South Dakota</option>
																			<option value="Tennessee">
																				Tennessee</option>
																			<option value="Texas">
																				Texas</option>
																			<option value="Utah">
																				Utah</option>
																			<option value="Virginia">
																				Virginia</option>
																			<option value="Vermont">
																				Vermont</option>
																			<option value="Washington">
																				Washington</option>
																			<option value="West Virginia">
																				West Virginia</option>
																			<option value="Wisconsin">
																				Wisconsin</option>
																			<option value="Wyoming">
																				Wyoming</option>
																		</select><font color="#db6c1d">*</font></font></p>
															</td>
															<td width="25%" align="left">
																<p style="MARGIN-LEFT: 27px">
																	<font face="Tahoma,Verdana,Arial" size="-1"><input name="zipcode" type="text" maxlength="12" size="12" tabindex="8" id="_zipTextBox"
																			runat="server"><font color="#db6c1d">*</font></font></p>
															</td>
														</tr>
														<tr>
															<td width="99%" align="center" colspan="3">
																<p style="MARGIN-TOP: 10px; MARGIN-BOTTOM: 7px; MARGIN-RIGHT: 20px">
																	<img border="0" src="images/seperator.jpg" width="605" height="2"></p>
															</td>
														</tr>
														<tr>
															<td width="53%" align="left">
																<font face="Tahoma,Verdana,Arial" size="-1" color="#6b6b6b">
																	<p style="MARGIN-TOP: 2px; MARGIN-BOTTOM: 7px; MARGIN-LEFT: 15px"><b>Are you an 
																			existing customer?</b>
																</font></P>
															</td>
															<td width="47%" colspan="2" align="left">&nbsp;
															</td>
														</tr>
														<tr>
															<td width="53%" align="left">
																<p style="MARGIN-LEFT: 15px">
																	<font face="Tahoma,Verdana,Arial" size="-1">
																		<select name="customer" size="1" tabindex="9" id="_existingCustomerSelect" runat="server">
																			<option value="Select" selected>
																				Select</option>
																			<option value="Yes">
																				Yes</option>
																			<option value="No">
																				No</option>
																		</select><font color="#db6c1d">*</font></font></p>
															</td>
															<td width="47%" colspan="2" align="left">&nbsp;
															</td>
														</tr>
														<tr>
															<td width="53%" align="right">
																<font face="Tahoma,Verdana,Arial" size="-1" color="#6b6b6b">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 5px; MARGIN-LEFT: 15px" align="left">
																		<b>I would like more information about?</b>
																</font></P>
															</td>
															<td width="47%" colspan="2">
																<p style="MARGIN-BOTTOM: 0px; MARGIN-LEFT: 15px"></p>
															</td>
														</tr>
														<tr>
															<td width="53%" align="left">
																<p style="MARGIN-BOTTOM: 0px; MARGIN-LEFT: 75px">
																	<font color="#6b6b6b" face="Tahoma,Verdana,Arial" size="-1"><input type="checkbox" name="pphp" value="ON" tabindex="10" id="_pphpCheckBox" runat="server">Pre-Paid 
																		Home Phone Service</font></p>
															</td>
															<td width="47%" colspan="2" align="left">
																<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 75px">
																	<font color="#6b6b6b" face="Tahoma,Verdana,Arial" size="-1"><input type="checkbox" name="ppmc" value="ON" tabindex="13" id="_ppmcCheckBox" runat="server">Pre-Paid 
																		MasterCard</font></p>
															</td>
														</tr>
														<tr>
															<td width="53%" align="left">
																<p style="MARGIN-BOTTOM: 0px; MARGIN-LEFT: 75px">
																	<font color="#6b6b6b" face="Tahoma,Verdana,Arial" size="-1"><input type="checkbox" name="ppld" value="ON" tabindex="11" id="_ppldCheckBox" runat="server">Long 
																		Distance</font></p>
															</td>
															<td width="47%" colspan="2" align="left">
																<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 75px">
																	<font color="#6b6b6b" face="Tahoma,Verdana,Arial" size="-1"><input type="checkbox" name="ppi" value="ON" tabindex="14" id="_ppiCheckBox" runat="server">Pre-Paid 
																		Internet</font></p>
															</td>
														</tr>
														<tr>
															<td width="53%" align="left">
																<p style="MARGIN-BOTTOM: 0px; MARGIN-LEFT: 75px">
																	<font color="#6b6b6b" face="Tahoma,Verdana,Arial" size="-1"><input type="checkbox" name="ppc" value="ON" tabindex="12" id="_ppcCheckBox" runat="server">Pre-Paid 
																		Cellular</font></p>
															</td>
															<td width="47%" colspan="2" align="left">
																<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 75px">
																	<font color="#6b6b6b" face="Tahoma,Verdana,Arial" size="-1"><input type="checkbox" name="bps" value="ON" tabindex="15" id="_bpCheckBox" runat="server">Bill 
																		Pay Services</font></p>
															</td>
														</tr>
														<tr>
															<td width="99%" align="center" colspan="3">
																<p style="MARGIN-TOP: 10px; MARGIN-BOTTOM: 7px; MARGIN-RIGHT: 20px">
																	<img border="0" src="images/seperator.jpg" width="605" height="2"></p>
															</td>
														</tr>
														<tr>
															<td width="53%" align="left">
																<font face="Tahoma,Verdana,Arial" size="-1">
																	<p style="MARGIN-TOP: 2px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 15px">
																		<b><font size="2" color="#6b6b6b">Comment or Question:</font></b>
																</font></P>
															</td>
															<td width="47%" colspan="2" align="left">
																<p style="MARGIN-TOP: 2px; MARGIN-BOTTOM: 0px; MARGIN-LEFT: 15px"></p>
															</td>
														</tr>
														<tr>
															<td width="99%" align="left" height="75" valign="top" colspan="3">
																<p style="MARGIN-LEFT: 15px">
																	<font face="Tahoma,Verdana,Arial" size="-1"><textarea rows="4" input name="txtMessage" cols="73" tabindex="16" id="_messageTextArea" runat="server"></textarea></font></p>
															</td>
														</tr>
														<tr>
															<td width="99%" align="right" colspan="3">
																<font face="Tahoma,Verdana,Arial" size="-1">
																	<p style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 5px; MARGIN-LEFT: 15px" align="left">
																		<asp:Button id="_submitButton" runat="server" Text="Submit" Width="70px"></asp:Button>
																</font></P>
															</td>
														</tr>
													</table>
												</form>
											</font>
										</td>
									</tr>
									<tr>
										<td colspan="2" height="52">
											<p align="center"><font face="Arial" color="#6b6b6b">
													<span style="FONT-WEIGHT: 700">
														<font style="FONT-SIZE: 11px">dPi Teleconnect</font></span><span style="FONT-WEIGHT: 700; FONT-SIZE: 11px"><br>
													</span><span style="FONT-WEIGHT: 700"><font size="1">2997 LBJ Freeway, Suite 225<br>
															Dallas, Texas 75234<br>
															Customer Service: 1-877 JOIN dPi (564-6374)</font></span></font></p>
										</td>
									</tr>
									<tr>
										<td colspan="2">
											<p align="center"><dns:Footer id="_footer" runat="server" /></p>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<p><span style="VISIBILITY: hidden">
				<SCRIPT LANGUAGE="Javascript" SRC="http://www.dpiteleconnect.com/counter/counter.php?page=contact"><!--
//--></SCRIPT>
		</p>
		</SPAN>
	</body>
</HTML>
