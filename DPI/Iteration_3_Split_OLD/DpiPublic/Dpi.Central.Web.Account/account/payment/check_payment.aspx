<%@ Page language="c#" Codebehind="check_payment.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Payment.CheckPaymentPage" %>
<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Register TagPrefix="ctl" Namespace="Dpi.Central.Web.Account.Controls" Assembly="Dpi.Central.Web.Account" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Check Payment</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellspacing="0" cellpadding="0" border="0">
				<tbody>
					<tr>
						<td colspan="2"><dns:header id="ctrlHeader" runat="server"></dns:header></td>
						<td valign="top" align="left"></td>
					</tr>
					<tr>
						<td valign="top" rowspan="2"><img alt="" src="../../images/about_side.jpg">
						</td>
						<td valign="top" align="center"><img src="../../images/ppc_top.jpg" border="0">
							<table class="layout_table" style="WIDTH: 490px" cellpadding="2" align="center">
								<tbody>
									<tr class="separator_row">
										<td colspan="2"></td>
									</tr>
									<tr>
										<td colspan="2"><asp:CustomValidator ID="vldCustErrorMsg" runat="server" ErrorMessage="" Display="None" EnableClientScript="False"
												Width="100%"></asp:CustomValidator><asp:ValidationSummary ID="vldSummary" runat="server" DisplayMode="List" CssClass="Error"></asp:ValidationSummary>
											<asp:RequiredFieldValidator id="vldRfState" runat="server" ErrorMessage="State is required" Display="None" ControlToValidate="lstState"></asp:RequiredFieldValidator>
											<asp:RequiredFieldValidator id="vldRfDrvState" runat="server" ErrorMessage="Driver License State is required"
												Display="None" ControlToValidate="lstDrvState"></asp:RequiredFieldValidator>
										</td>
									</tr>
									<tr class="separator_row">
										<td colspan="2"></td>
									</tr>
									<tr>
										<td width="50%">Account Number</td>
										<td align="left" width="50%"><ctl:controllabel id="lblAcctN" runat="server">123456789</ctl:controllabel></td>
									</tr>
									<tr>
										<td>Phone Number</td>
										<td align="left"><ctl:controllabel id="lblPhoneN" runat="server">123-456-7890</ctl:controllabel></td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblFName" runat="server" EnableViewState="False" AssociatedControlID="txtFName">First Name</ctl:controllabel></td>
										<td><ctl:textbox id="txtFName" runat="server" Width="100%" PerformValidation="True" IsRequired="True"
												ErrorMsgEmpty="First Name is required" TabIndex="1">Samuel</ctl:textbox></td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblLName" runat="server" EnableViewState="False" AssociatedControlID="txtLName">Last Name</ctl:controllabel></td>
										<td><ctl:textbox id="txtLName" runat="server" Width="100%" PerformValidation="True" IsRequired="True"
												ErrorMsgEmpty="Last Name is required" TabIndex="2">Sopilka</ctl:textbox></td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblAddr" runat="server" EnableViewState="False" AssociatedControlID="txtAddr"
												FieldName="Street Address">Street Address</ctl:controllabel></td>
										<td><ctl:textbox id="txtAddr" runat="server" Width="100%" PerformValidation="True" IsRequired="True"
												ErrorMsgEmpty="Street Address is required" TabIndex="3">112 Pobedy ave.</ctl:textbox></td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblCity" runat="server" EnableViewState="False" AssociatedControlID="txtCity"
												FieldName="City, State">City, State, ZIP</ctl:controllabel></td>
										<td>
											<table cellspacing="0" cellpadding="0" width="100%" border="0">
												<tr>
													<td width="45%"><ctl:textbox id="txtCity" runat="server" PerformValidation="True" IsRequired="True" ErrorMsgEmpty="City name is required"
															width="100%" ShowRequiredIndicator="False" TabIndex="4">Dallas</ctl:textbox></td>
													<td width="1%"><img height="1" alt="" src="..\..\blank.gif" width="3"></td>
													<td width="25%"><asp:DropDownList ID="lstState" runat="server" Width="100%" TabIndex="5">
															<asp:ListItem Selected="True"></asp:ListItem>
															<asp:ListItem Value="AL">AL</asp:ListItem>
															<asp:ListItem Value="AK">AK</asp:ListItem>
															<asp:ListItem Value="AZ">AZ</asp:ListItem>
															<asp:ListItem Value="AR">AR</asp:ListItem>
															<asp:ListItem Value="CA">CA</asp:ListItem>
															<asp:ListItem Value="CO">CO</asp:ListItem>
															<asp:ListItem Value="CT">CT</asp:ListItem>
															<asp:ListItem Value="DC">DC</asp:ListItem>
															<asp:ListItem Value="DE">DE</asp:ListItem>
															<asp:ListItem Value="FL">FL</asp:ListItem>
															<asp:ListItem Value="GA">GA</asp:ListItem>
															<asp:ListItem Value="HI">HI</asp:ListItem>
															<asp:ListItem Value="ID">ID</asp:ListItem>
															<asp:ListItem Value="IL">IL</asp:ListItem>
															<asp:ListItem Value="IN">IN</asp:ListItem>
															<asp:ListItem Value="IA">IA</asp:ListItem>
															<asp:ListItem Value="KS">KS</asp:ListItem>
															<asp:ListItem Value="KY">KY</asp:ListItem>
															<asp:ListItem Value="LA">LA</asp:ListItem>
															<asp:ListItem Value="ME">ME</asp:ListItem>
															<asp:ListItem Value="MD">MD</asp:ListItem>
															<asp:ListItem Value="MA">MA</asp:ListItem>
															<asp:ListItem Value="MI">MI</asp:ListItem>
															<asp:ListItem Value="MN">MN</asp:ListItem>
															<asp:ListItem Value="MS">MS</asp:ListItem>
															<asp:ListItem Value="MO">MO</asp:ListItem>
															<asp:ListItem Value="MT">MT</asp:ListItem>
															<asp:ListItem Value="NE">NE</asp:ListItem>
															<asp:ListItem Value="NV">NV</asp:ListItem>
															<asp:ListItem Value="NH">NH</asp:ListItem>
															<asp:ListItem Value="NJ">NJ</asp:ListItem>
															<asp:ListItem Value="NM">NM</asp:ListItem>
															<asp:ListItem Value="NY">NY</asp:ListItem>
															<asp:ListItem Value="NC">NC</asp:ListItem>
															<asp:ListItem Value="ND">ND</asp:ListItem>
															<asp:ListItem Value="OH">OH</asp:ListItem>
															<asp:ListItem Value="OK">OK</asp:ListItem>
															<asp:ListItem Value="OR">OR</asp:ListItem>
															<asp:ListItem Value="PA">PA</asp:ListItem>
															<asp:ListItem Value="RI">RI</asp:ListItem>
															<asp:ListItem Value="SC">SC</asp:ListItem>
															<asp:ListItem Value="SD">SD</asp:ListItem>
															<asp:ListItem Value="TN">TN</asp:ListItem>
															<asp:ListItem Value="TX">TX</asp:ListItem>
															<asp:ListItem Value="UT">UT</asp:ListItem>
															<asp:ListItem Value="VT">VT</asp:ListItem>
															<asp:ListItem Value="VA">VA</asp:ListItem>
															<asp:ListItem Value="WA">WA</asp:ListItem>
															<asp:ListItem Value="WV">WV</asp:ListItem>
															<asp:ListItem Value="WI">WI</asp:ListItem>
															<asp:ListItem Value="WY">WY</asp:ListItem>
														</asp:DropDownList></td>
													<td width="1%"><img height="1" alt="" src="..\..\blank.gif" width="3"></td>
													<td width="28%"><ctl:textbox id="txtZIP" runat="server" PerformValidation="True" IsRequired="True" ErrorMsgEmpty="ZIP is required"
															width="100%" ShowRequiredIndicator="False" ErrorMsgRegExp="ZIP code is required" ValidationRegExp="\d{5}(-\d{4})?"
															ValueType="RegExp" TabIndex="6">75025</ctl:textbox></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblDrvNum" runat="server" EnableViewState="False" AssociatedControlID="txtDrvNum">Driver License Number</ctl:controllabel></td>
										<td><ctl:textbox id="txtDrvNum" runat="server" Width="100%" PerformValidation="True" IsRequired="True"
												ErrorMsgEmpty="{0} is required" ErrorMsgRegExp="Please enter valid {0}" ValidationRegExp="\d+"
												ValueType="RegExp" EnableViewState="False" TabIndex="7"></ctl:textbox></td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblDrvState" runat="server" EnableViewState="False" AssociatedControlID="lstDrvState"
												ShowRequiredMark="True">Driver License State</ctl:controllabel></td>
										<td><asp:DropDownList ID="lstDrvState" runat="server" Width="100%" EnableViewState="False" TabIndex="8">
												<asp:ListItem Selected="True"></asp:ListItem>
												<asp:ListItem Value="AL">Alabama</asp:ListItem>
												<asp:ListItem Value="AK">Alaska</asp:ListItem>
												<asp:ListItem Value="AZ">Arizona</asp:ListItem>
												<asp:ListItem Value="AR">Arkansas</asp:ListItem>
												<asp:ListItem Value="CA">California</asp:ListItem>
												<asp:ListItem Value="CO">Colorado</asp:ListItem>
												<asp:ListItem Value="CT">Connecticut</asp:ListItem>
												<asp:ListItem Value="DC">D.C.</asp:ListItem>
												<asp:ListItem Value="DE">Delaware</asp:ListItem>
												<asp:ListItem Value="FL">Florida</asp:ListItem>
												<asp:ListItem Value="GA">Georgia</asp:ListItem>
												<asp:ListItem Value="HI">Hawaii</asp:ListItem>
												<asp:ListItem Value="ID">Idaho</asp:ListItem>
												<asp:ListItem Value="IL">Illinois</asp:ListItem>
												<asp:ListItem Value="IN">Indiana</asp:ListItem>
												<asp:ListItem Value="IA">Iowa</asp:ListItem>
												<asp:ListItem Value="KS">Kansas</asp:ListItem>
												<asp:ListItem Value="KY">Kentucky</asp:ListItem>
												<asp:ListItem Value="LA">Louisiana</asp:ListItem>
												<asp:ListItem Value="ME">Maine</asp:ListItem>
												<asp:ListItem Value="MD">Maryland</asp:ListItem>
												<asp:ListItem Value="MA">Massachusetts</asp:ListItem>
												<asp:ListItem Value="MI">Michigan</asp:ListItem>
												<asp:ListItem Value="MN">Minnesota</asp:ListItem>
												<asp:ListItem Value="MS">Mississippi</asp:ListItem>
												<asp:ListItem Value="MO">Missouri</asp:ListItem>
												<asp:ListItem Value="MT">Montana</asp:ListItem>
												<asp:ListItem Value="NE">Nebraska</asp:ListItem>
												<asp:ListItem Value="NV">Nevada</asp:ListItem>
												<asp:ListItem Value="NH">New Hampshire</asp:ListItem>
												<asp:ListItem Value="NJ">New Jersey</asp:ListItem>
												<asp:ListItem Value="NM">New Mexico</asp:ListItem>
												<asp:ListItem Value="NY">New York</asp:ListItem>
												<asp:ListItem Value="NC">North Carolina</asp:ListItem>
												<asp:ListItem Value="ND">North Dakota</asp:ListItem>
												<asp:ListItem Value="OH">Ohio</asp:ListItem>
												<asp:ListItem Value="OK">Oklahoma</asp:ListItem>
												<asp:ListItem Value="OR">Oregon</asp:ListItem>
												<asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
												<asp:ListItem Value="RI">Rhode Island</asp:ListItem>
												<asp:ListItem Value="SC">South Carolina</asp:ListItem>
												<asp:ListItem Value="SD">South Dakota</asp:ListItem>
												<asp:ListItem Value="TN">Tennessee</asp:ListItem>
												<asp:ListItem Value="TX">Texas</asp:ListItem>
												<asp:ListItem Value="UT">Utah</asp:ListItem>
												<asp:ListItem Value="VT">Vermont</asp:ListItem>
												<asp:ListItem Value="VA">Virginia</asp:ListItem>
												<asp:ListItem Value="WA">Washington</asp:ListItem>
												<asp:ListItem Value="WV">West Virginia</asp:ListItem>
												<asp:ListItem Value="WI">Wisconsin</asp:ListItem>
												<asp:ListItem Value="WY">Wyoming</asp:ListItem>
											</asp:DropDownList></td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblEmail" runat="server" EnableViewState="False" AssociatedControlID="txtEmail">E-Mail</ctl:controllabel></td>
										<td><ctl:textbox id="txtEmail" runat="server" Width="100%" PerformValidation="True" ErrorMsgEmpty="{0} is required"
												ErrorMsgRegExp="{0} is invalid" ValidationRegExp="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
												ValueType="RegExp" EnableViewState="False" IsRequired="True" TabIndex="9">samuel@sopilka.gov</ctl:textbox></td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblRNum" runat="server" EnableViewState="False" AssociatedControlID="txtRNum">Bank Routing Number</ctl:controllabel>&nbsp;<br>
											<span class="midgray_normal">(see example below)</span></td>
										<td><ctl:textbox id="txtRNum" runat="server" Width="100%" PerformValidation="True" IsRequired="True"
												ErrorMsgEmpty="{0} is required" ErrorMsgRegExp="Please enter valid{0}" ValidationRegExp="[\d-]{9}"
												ValueType="Digits" EnableViewState="False" TabIndex="10"></ctl:textbox></td>
									</tr>
									<tr>
										<td><ctl:controllabel id="lblBANum" runat="server" EnableViewState="False" AssociatedControlID="txtBANum">Bank Account Number</ctl:controllabel></td>
										<td><ctl:textbox id="txtBANum" runat="server" Width="100%" PerformValidation="True" IsRequired="True"
												ErrorMsgEmpty="{0} is required" ErrorMsgRegExp="Invalid {0}. It should contain 3 or 4 digit."
												ValidationRegExp="\d{3-4}" ValueType="Digits" EnableViewState="False" TabIndex="11"></ctl:textbox></td>
									</tr>
									<tr class="separator_row">
										<td colspan="2"></td>
									</tr>
									<tr>
										<td>Payment Amount, $</td>
										<td><ctl:controllabel id="lblAmt" runat="server">$66.59</ctl:controllabel></td>
									</tr>
									<tr class="separator_row">
										<td colspan="2"></td>
									</tr>
									<tr>
										<td align="left">
											<asp:HyperLink id="lnkReturn" runat="server" NavigateUrl="~/account/payment/payment_selection.aspx"
												TabIndex="13">Return to Payment Selection</asp:HyperLink></td>
										<td align="right"><asp:ImageButton ID="btnPay" runat="server" EnableViewState="False" ImageUrl="../../images/proc_tran_btn.gif"
												AlternateText="Process Transaction" TabIndex="12"></asp:ImageButton></td>
									</tr>
									<tr class="separator_row">
										<td colspan="2"></td>
									</tr>
									<tr>
										<td style="FONT-WEIGHT: normal; FONT-SIZE: 11px" colspan="2">By clicking Process 
											Transaction, you authorize an electronic debit from your checking account that 
											will be processed through the regular banking system. If your full order is not 
											available at the same time, you authorize partial debits to your account, not 
											to exceed the total authorized amount. The partial debits will take place upon 
											each shipment of partial goods. If any of your payments are returned unpaid, 
											you will be charged a returned item fee up to the maximum allowed by law. To 
											exit without authorizing, click <a href="../summary.aspx">here</a>.</td>
									</tr>
									<tr class="separator_row">
										<td colspan="2"></td>
									</tr>
									<tr>
										<td colspan="2">Check Example:<br>
											<img height="475" alt="Check description" src="../../images/checkdesc.gif" width="500"></td>
									</tr>
								</tbody>
							</table>
						</td>
					</tr>
					<tr>
						<td valign="bottom" align="center"><dns:footer id="ctrlFooter" runat="server"></dns:footer></td>
						<td valign="top" align="left"></td>
					</tr>
				</tbody>
			</table>
		</form>
		</TR></TBODY></TABLE></TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
	</body>
</HTML>
