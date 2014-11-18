<%@ Register TagPrefix="ctl" Namespace="Dpi.Central.Web.Account.Controls" Assembly="Dpi.Central.Web.Account" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<%@ Page language="c#" Codebehind="cc_payment.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Payment.CreditCardPaymentPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC Credit Card Payment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../DPI.css" type="text/css" rel="stylesheet">
		<script type="text/javascript" language="javascript">
			
			function selectCardType(radioButtonId) 
			{
				document.getElementById('rbVisa').checked = radioButtonId == 'rbVisa';
				document.getElementById('rbMasterCard').checked = radioButtonId == 'rbMasterCard';
				document.getElementById('rbAmericanExpress').checked = radioButtonId == 'rbAmericanExpress';
				document.getElementById('rbDiscover').checked = radioButtonId == 'rbDiscover';
				
				objItem = document.getElementById('txtCvNumber');
				
				if(objItem == null)
				{
					return;
				}
				
				objItem.disabled = radioButtonId == 'rbDiscover';
			}
				
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellSpacing="0" cellPadding="0" border="0">
				<tbody>
					<tr>
						<td colSpan="2"><dns:header id="ctrlHeader" runat="server"></dns:header></td>
						<td vAlign="top" align="left"></td>
					</tr>
					<tr>
						<td vAlign="top" rowSpan="2"><IMG alt="" src="../../images/about_side.jpg">
						</td>
						<td vAlign="top" align="center"><IMG src="../../images/ppc_top.jpg" border="0">
							<table class="layout_table" style="WIDTH: 490px" cellPadding="2" align="center">
								<tbody>
									<tr class="separator_row">
										<td colSpan="2"></td>
									</tr>
									<tr>
										<td colSpan="2"><asp:customvalidator id="vldCustErrorMsg" runat="server" ErrorMessage="" Display="None" EnableClientScript="False"
												Width="100%"></asp:customvalidator><asp:validationsummary id="vldSummary" runat="server" DisplayMode="List" CssClass="Error"></asp:validationsummary><asp:customvalidator id="vldCstExpDate" runat="server" ErrorMessage="Please select a valid Expiration Date"
												Display="None" EnableClientScript="False" EnableViewState="False"></asp:customvalidator>
											<asp:RequiredFieldValidator id="vldRfState" runat="server" ErrorMessage="State is required" Display="None" ControlToValidate="lstState"
												EnableClientScript="False"></asp:RequiredFieldValidator>
											<asp:CustomValidator id="vldCstCreditCardType" runat="server" ErrorMessage="Please choose a Credit Card type"
												Display="None"></asp:CustomValidator>
										</td>
									</tr>
									<tr class="separator_row">
										<td colSpan="2"></td>
									</tr>
									<tr>
										<td width="50%">Account Number</td>
										<td align="left" width="50%"><asp:label id="lblAcctNumber" runat="server">123456789</asp:label></td>
									</tr>
									<tr>
										<td>Phone Number</td>
										<td align="left"><asp:label id="lblPhoneNumber" runat="server">123-456-7890</asp:label></td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblFirstName" runat="server" EnableViewState="False" AssociatedControlID="txtFirstName">First Name</ctl:controllabel></td>
										<td><ctl:textbox id="txtFirstName" runat="server" Width="100%" PerformValidation="True" IsRequired="True"
												ErrorMsgEmpty="First Name is required" TabIndex="1">Samuel</ctl:textbox></td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblLastName" runat="server" EnableViewState="False" AssociatedControlID="txtLastName">Last Name</ctl:controllabel></td>
										<td><ctl:textbox id="txtLastName" runat="server" Width="100%" PerformValidation="True" IsRequired="True"
												ErrorMsgEmpty="Last Name is required" TabIndex="2">Sopilka</ctl:textbox></td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblAddr" runat="server" EnableViewState="False" AssociatedControlID="txtAddr">Street Address</ctl:controllabel></td>
										<td><ctl:textbox id="txtAddr" runat="server" Width="100%" PerformValidation="True" IsRequired="True"
												ErrorMsgEmpty="Street Address is required" FieldName="Street Address" TabIndex="3">112 Pobedy ave.</ctl:textbox></td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblCity" runat="server" EnableViewState="False" AssociatedControlID="txtCity"
												FieldName="City, State, ZIP">City, State, ZIP</ctl:controllabel></td>
										<td>
											<table cellSpacing="0" cellPadding="0" width="100%" border="0">
												<tr>
													<td width="45%"><ctl:textbox id="txtCity" runat="server" width="100%" PerformValidation="True" IsRequired="True"
															ErrorMsgEmpty="City name is required" ShowRequiredIndicator="False" TabIndex="4">Dallas</ctl:textbox></td>
													<td width="1%"><IMG height="1" alt="" src="..\..\blank.gif" width="3"></td>
													<td width="25%"><asp:dropdownlist id="lstState" runat="server" Width="100%" TabIndex="5">
															<asp:ListItem Selected="True"></asp:ListItem>
															<asp:ListItem>AL</asp:ListItem>
															<asp:ListItem>AK</asp:ListItem>
															<asp:ListItem>AZ</asp:ListItem>
															<asp:ListItem>AR</asp:ListItem>
															<asp:ListItem>CA</asp:ListItem>
															<asp:ListItem>CO</asp:ListItem>
															<asp:ListItem>CT</asp:ListItem>
															<asp:ListItem>DC</asp:ListItem>
															<asp:ListItem>DE</asp:ListItem>
															<asp:ListItem>FL</asp:ListItem>
															<asp:ListItem>GA</asp:ListItem>
															<asp:ListItem>HI</asp:ListItem>
															<asp:ListItem>ID</asp:ListItem>
															<asp:ListItem>IL</asp:ListItem>
															<asp:ListItem>IN</asp:ListItem>
															<asp:ListItem>IA</asp:ListItem>
															<asp:ListItem>KS</asp:ListItem>
															<asp:ListItem>KY</asp:ListItem>
															<asp:ListItem>LA</asp:ListItem>
															<asp:ListItem>ME</asp:ListItem>
															<asp:ListItem>MD</asp:ListItem>
															<asp:ListItem>MA</asp:ListItem>
															<asp:ListItem>MI</asp:ListItem>
															<asp:ListItem>MN</asp:ListItem>
															<asp:ListItem>MS</asp:ListItem>
															<asp:ListItem>MO</asp:ListItem>
															<asp:ListItem>MT</asp:ListItem>
															<asp:ListItem>NE</asp:ListItem>
															<asp:ListItem>NV</asp:ListItem>
															<asp:ListItem>NH</asp:ListItem>
															<asp:ListItem>NJ</asp:ListItem>
															<asp:ListItem>NM</asp:ListItem>
															<asp:ListItem>NY</asp:ListItem>
															<asp:ListItem>NC</asp:ListItem>
															<asp:ListItem>ND</asp:ListItem>
															<asp:ListItem>OH</asp:ListItem>
															<asp:ListItem>OK</asp:ListItem>
															<asp:ListItem>OR</asp:ListItem>
															<asp:ListItem>PA</asp:ListItem>
															<asp:ListItem>RI</asp:ListItem>
															<asp:ListItem>SC</asp:ListItem>
															<asp:ListItem>SD</asp:ListItem>
															<asp:ListItem>TN</asp:ListItem>
															<asp:ListItem>TX</asp:ListItem>
															<asp:ListItem>UT</asp:ListItem>
															<asp:ListItem>VT</asp:ListItem>
															<asp:ListItem>VA</asp:ListItem>
															<asp:ListItem>WA</asp:ListItem>
															<asp:ListItem>WV</asp:ListItem>
															<asp:ListItem>WI</asp:ListItem>
															<asp:ListItem>WY</asp:ListItem>
														</asp:dropdownlist></td>
													<td width="1%"><IMG height="1" alt="" src="..\..\blank.gif" width="3"></td>
													<td width="28%"><ctl:textbox id="txtZip" runat="server" PerformValidation="True" IsRequired="True" ErrorMsgEmpty="ZIP is required"
															width="100%" ShowRequiredIndicator="False" TabIndex="6" MaxLength="5">75025</ctl:textbox></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblEmail" runat="server" EnableViewState="False" AssociatedControlID="txtEmail">E-Mail</ctl:controllabel></td>
										<td><ctl:textbox id="txtEmail" runat="server" Width="100%" PerformValidation="True" ErrorMsgEmpty="E-Mail is required"
												ErrorMsgRegExp="E-Mail is invalid" ValidationRegExp="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
												ValueType="RegExp" EnableViewState="False" IsRequired="True" TabIndex="7">samuel@sopilka.gov</ctl:textbox></td>
									</tr>
									<tr>
										<td><asp:label id="lblCardType" runat="server" EnableViewState="False">Card Type</asp:label></td>
										<td>
											<TABLE cellSpacing="0" cellPadding="0" border="0">
												<TR class="radiogroup">
													<TD nowrap>
														<asp:RadioButton id="rbVisa" runat="server" Text=" " tabIndex="8" onclick="selectCardType('rbVisa')"></asp:RadioButton>
														<IMG src="../../images/cc_visa.gif" alt="Visa" height="16" width="25" border="0" onclick="selectCardType('rbVisa')">
													</TD>
													<TD nowrap>
														<asp:RadioButton id="rbMasterCard" runat="server" DESIGNTIMEDRAGDROP="166" Text=" " tabIndex="9"
															onclick="selectCardType('rbMasterCard')"></asp:RadioButton>
														<IMG src="../../images/cc_mast.gif" alt="Visa" height="16" width="25" border="0" onclick="selectCardType('rbMasterCard')">
													</TD>
													<TD nowrap>
														<asp:RadioButton id="rbAmericanExpress" runat="server" DESIGNTIMEDRAGDROP="170" Text=" " tabIndex="10"
															onclick="selectCardType('rbAmericanExpress')"></asp:RadioButton>
														<IMG src="../../images/cc_amex.gif" alt="Visa" height="16" width="25" border="0" onclick="selectCardType('rbAmericanExpress')">
													</TD>
													<TD nowrap>
														<asp:RadioButton id="rbDiscover" runat="server" Text=" " tabIndex="11" onclick="selectCardType('rbDiscover')"></asp:RadioButton>
														<IMG src="../../images/cc_disc.gif" alt="Visa" height="16" width="25" border="0" onclick="selectCardType('rbDiscover')">
													</TD>
												</TR>
											</TABLE>
										</td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblCNum" runat="server" EnableViewState="False" AssociatedControlID="txtCcNumber">Card Number</ctl:controllabel></td>
										<td><ctl:textbox id="txtCcNumber" runat="server" Width="100%" PerformValidation="True" ErrorMsgEmpty="Card Number is required"
												ErrorMsgRegExp="Card Number is invalid" ValidationRegExp="\d+" ValueType="RegExp" EnableViewState="False"
												IsRequired="True" TabIndex="12" MaxLength="16"></ctl:textbox></td>
									</tr>
									<tr>
										<td><asp:label id="lblExp" runat="server" EnableViewState="False">Expiration Date</asp:label></td>
										<td>
											<table cellSpacing="0" cellPadding="0" width="100%" border="0">
												<tbody>
													<tr>
														<td noWrap width="49%"><asp:dropdownlist id="lstExpMonth" runat="server" Width="100%" EnableViewState="False" TabIndex="13"></asp:dropdownlist></td>
														<td width="1%"><IMG height="1" alt="" src="..\..\blank.gif" width="3"></td>
														<td noWrap align="right" width="49%"><asp:dropdownlist id="lstExpYear" runat="server" Width="100%" EnableViewState="False" TabIndex="14"></asp:dropdownlist></td>
													</tr>
												</tbody>
											</table>
										</td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblCVNum" runat="server" EnableViewState="False" AssociatedControlID="txtCvNumber">CVV2 Security Code</ctl:controllabel>&nbsp;<span class="midgray_normal">(see 
												example below)</span></td>
										<td><ctl:textbox id="txtCvNumber" runat="server" Width="100%" PerformValidation="True" ErrorMsgEmpty="CVV2 Security Code is required"
												IsRequired="True" TabIndex="15"></ctl:textbox></td>
									</tr>
									<tr class="separator_row">
										<td colSpan="2"></td>
									</tr>
									<tr>
										<td>Payment Amount, $</td>
										<td><asp:label id="lblAmt" runat="server">$66.59</asp:label></td>
									</tr>
									<tr class="separator_row">
										<td colSpan="2"></td>
									</tr>
									<tr>
										<td align="left"><asp:hyperlink id="lnkReturn" runat="server" NavigateUrl="~/account/payment/payment_selection.aspx"
												TabIndex="16">Return to Payment Selection</asp:hyperlink></td>
										<td align="right"><asp:imagebutton id="btnPay" runat="server" EnableViewState="False" AlternateText="Process Transaction"
												ImageUrl="../../images/proc_tran_btn.gif" TabIndex="17"></asp:imagebutton></td>
									</tr>
									<tr class="separator_row">
										<td colSpan="2"></td>
									</tr>
									<tr>
										<td align="left" colSpan="2">
											<p>* CVV2 Security Code Example:<br>
												<IMG height="79" alt="CVV2 Security Code Example" src="../../images/cvv2a.gif" width="253"></p>
										</td>
									</tr>
									<tr>
										<td noWrap colSpan="2"></td>
									</tr>
								</tbody>
							</table>
						</td>
					</tr>
					<tr>
						<td vAlign="bottom" align="center"><dns:footer id="ctrlFooter" runat="server"></dns:footer></td>
						<td vAlign="top" align="left"></td>
					</tr>
				</tbody>
			</table>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
