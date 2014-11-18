<%@ Page language="c#" Codebehind="CustRecPymtsSet.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.CustRecPymtsSet" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>NOGetZip</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript" type="text/javascript">
		function check() 
		{
			if (clickedButton)
			{
				clickedButton = false;
				return true;
			}
			else
				return false;
		}
		function SetControls()
		{
			if (document.getElementById("ddlAcctType").disabled)
				return;
				
			document.getElementById("txtBankRouteNum").disabled = false;
			document.getElementById("txtCVV2").disabled = false;
			document.getElementById("ddlExpMonth").disabled = false;
			document.getElementById("ddlExpYear").disabled = false;
			document.getElementById("ddlExpYear").disabled = false;
			document.getElementById("ddlDLState").disabled = false;
			document.getElementById("txtDLNum").disabled = false;
			
			if (document.getElementById("ddlAcctType").value == 1) //Credit or debit
			{
				document.getElementById("txtBankRouteNum").value ="";
				document.getElementById("ddlDLState").value = "";
				document.getElementById("txtDLNum").value = "";
				
				document.getElementById("txtBankRouteNum").disabled = true;
				document.getElementById("ddlDLState").disabled = true;
				document.getElementById("txtDLNum").disabled = true;		
			}	
			if (document.getElementById("ddlAcctType").value == 3) //Check
			{
				document.getElementById("txtCVV2").value = "";
				document.getElementById("ddlExpMonth").value = "";
				document.getElementById("ddlExpYear").value = "";
				
				document.getElementById("txtCVV2").disabled = true;
				document.getElementById("ddlExpMonth").disabled = true;
				document.getElementById("ddlExpYear").disabled = true;			
				
			}
		}
		/*
		function ValidateIt()
		{
			if (document.getElementById("txtCVV2").disabled)
				return;
				
			if (document.getElementById("txtCVV2").value.length < 3)
			{
				
				clickedButton = !window.confirm("Please select OK if CVV2 is available else click on Cancel");
				//clickedButton = false;
			}
		}
		*/
		</script>
	</HEAD>
	<body text="#000000" onload="SetControls();" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<form id="Form1" onsubmit="return check();" action="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="800" align="left" border="0">
				<tr>
					<td colSpan="2"><dpiuser:siteheader id="Siteheader1" runat="server"></dpiuser:siteheader></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="124" background="images/sidenav_bgd.gif" bgColor="white"
						height="100%"><dpiuser:sidecontrol id="Sidecontrol2" runat="server"></dpiuser:sidecontrol></td>
					<td vAlign="top" width="660">
						<table cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
							<TR>
								<TD align="center" colSpan="5"></TD>
							</TR>
							<TR>
								<TD align="center" colSpan="5">&nbsp;&nbsp;&nbsp;&nbsp; 
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
								</TD>
							</TR>
							<tr>
								<td style="HEIGHT: 543px" vAlign="top" align="center" colSpan="5">
									<table style="WIDTH: 661px; HEIGHT: 160px" cellSpacing="0" cellPadding="0" width="661"
										border="0">
										<TBODY>
											<TR>
												<TD style="WIDTH: 28px; HEIGHT: 19px"></TD>
												<TD style="WIDTH: 189px; HEIGHT: 19px">Account Number:</TD>
												<TD style="WIDTH: 456px; HEIGHT: 19px"><asp:label id="lblAccNumber" runat="server" Width="192px"></asp:label>&nbsp;</TD>
												<TD style="HEIGHT: 19px">&nbsp;&nbsp;&nbsp;</TD>
											</TR>
											<TR>
												<TD style="WIDTH: 28px"></TD>
												<TD style="WIDTH: 189px; HEIGHT: 19px">Billing First Name:</TD>
												<TD style="WIDTH: 456px; HEIGHT: 19px"><asp:textbox id="txtBFirstName" runat="server" Width="192px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtBFirstName" ErrorMessage="Enter Billing First Name">*</asp:requiredfieldvalidator></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 28px; HEIGHT: 19px"></TD>
												<TD style="WIDTH: 189px; HEIGHT: 19px">Billing Last Name:</TD>
												<TD style="WIDTH: 456px; HEIGHT: 19px"><asp:textbox id="txtBLastName" runat="server" Width="192px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtBLastName" ErrorMessage="Enter Billing Last Name">*</asp:requiredfieldvalidator></TD>
												<TD style="HEIGHT: 19px"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 28px; HEIGHT: 19px"></TD>
												<TD style="WIDTH: 189px; HEIGHT: 19px">Billing Address:</TD>
												<TD style="WIDTH: 456px; HEIGHT: 19px"><asp:textbox id="txtBAddress1" runat="server" Width="192px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ControlToValidate="txtBAddress1" ErrorMessage="Enter Billing Address">*</asp:requiredfieldvalidator></TD>
												<TD style="HEIGHT: 19px"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 28px; HEIGHT: 19px"></TD>
												<TD style="WIDTH: 189px; HEIGHT: 19px"></TD>
												<TD style="WIDTH: 456px; HEIGHT: 19px"><asp:textbox id="txtBAddress2" runat="server" Width="192px"></asp:textbox></TD>
												<TD style="HEIGHT: 19px"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 28px; HEIGHT: 18px"></TD>
												<TD style="WIDTH: 189px; HEIGHT: 18px">Billing City:</TD>
												<TD style="WIDTH: 456px; HEIGHT: 18px"><asp:textbox id="txtBCIty" runat="server" Width="192px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" ControlToValidate="txtBCIty" ErrorMessage="Enter Billing City">*</asp:requiredfieldvalidator></TD>
												<TD style="HEIGHT: 18px"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 28px"></TD>
												<TD style="WIDTH: 189px; HEIGHT: 6px">Billing State:</TD>
												<TD style="WIDTH: 456px; HEIGHT: 6px"><asp:dropdownlist id="ddlBState" runat="server" Width="192px">
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
													</asp:dropdownlist><asp:requiredfieldvalidator id="RequiredFieldValidator5" runat="server" ControlToValidate="ddlBState" ErrorMessage="Select Billing State">*</asp:requiredfieldvalidator></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 28px"></TD>
												<TD style="WIDTH: 189px; HEIGHT: 6px">Billing Zip:</TD>
												<TD style="WIDTH: 456px; HEIGHT: 6px"><asp:textbox id="txtBZip" runat="server" Width="88px" MaxLength="5"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator6" runat="server" ControlToValidate="txtBZip" ErrorMessage="Enter Billing Zip">*</asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator1" runat="server" ControlToValidate="txtBZip" ErrorMessage="Enter 5 digit zip code"
														ValidationExpression="\d{5}">*</asp:regularexpressionvalidator></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 28px"></TD>
												<TD style="WIDTH: 189px; HEIGHT: 6px">Phone Number:</TD>
												<TD style="WIDTH: 456px; HEIGHT: 6px"><asp:textbox id="txtNPA" runat="server" Width="40px" MaxLength="3"></asp:textbox>-
													<asp:textbox id="txtNxx" runat="server" Width="40px" MaxLength="3"></asp:textbox>-
													<asp:textbox id="txtLastFour" runat="server" Width="48px" MaxLength="4"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator7" runat="server" ControlToValidate="txtNPA" ErrorMessage="Enter Phone Area code">*</asp:requiredfieldvalidator><asp:requiredfieldvalidator id="RequiredFieldValidator8" runat="server" ControlToValidate="txtNxx" ErrorMessage="Enter Phone Number">*</asp:requiredfieldvalidator><asp:requiredfieldvalidator id="RequiredFieldValidator9" runat="server" ControlToValidate="txtLastFour" ErrorMessage="Enter Phone Number">*</asp:requiredfieldvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator2" runat="server" ControlToValidate="txtNxx" ErrorMessage="Enter only numbers on phone"
														ValidationExpression="\d{3}">*</asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator3" runat="server" ControlToValidate="txtNxx" ErrorMessage="Enter only numbers on phone"
														ValidationExpression="\d{3}">*</asp:regularexpressionvalidator><asp:regularexpressionvalidator id="RegularExpressionValidator4" runat="server" ControlToValidate="txtLastFour"
														ErrorMessage="Enter only numbers on phone" ValidationExpression="\d{4}">*</asp:regularexpressionvalidator></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 28px"></TD>
												<TD style="WIDTH: 189px; HEIGHT: 6px">E-Mail Address:</TD>
												<TD style="WIDTH: 456px; HEIGHT: 6px"><asp:textbox id="txtEmailAddress" runat="server" Width="192px"></asp:textbox><asp:regularexpressionvalidator id="Regularexpressionvalidator6" runat="server" ControlToValidate="txtEmailAddress"
														ErrorMessage="Enter valid email address" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:regularexpressionvalidator></TD>
												<TD></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 28px; HEIGHT: 6px"></TD>
												<TD style="WIDTH: 189px; HEIGHT: 6px">Status:</TD>
												<TD style="WIDTH: 456px; HEIGHT: 6px"><asp:dropdownlist id="ddlStatus" runat="server" Width="128px">
														<asp:ListItem Value="1" Selected="True">Active</asp:ListItem>
														<asp:ListItem Value="0">Inactive</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD style="HEIGHT: 6px"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 28px; HEIGHT: 1px"></TD>
												<TD style="WIDTH: 189px; HEIGHT: 1px">Account Type:</TD>
												<TD style="WIDTH: 456px; HEIGHT: 1px"><asp:dropdownlist id="ddlAcctType" runat="server" Width="128px">
														<asp:ListItem Value="1" Selected="True">Credit/Debit Card</asp:ListItem>
														<asp:ListItem Value="3">Checking</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD style="HEIGHT: 1px"></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 28px" rowSpan="6">&nbsp;</TD>
												<TD style="WIDTH: 189px; HEIGHT: 35px">Bank Account/Card Number:</TD>
												<TD style="WIDTH: 456px; HEIGHT: 35px"><asp:textbox id="txtCardNumber" runat="server" Width="192px"></asp:textbox><asp:requiredfieldvalidator id="RequiredFieldValidator10" runat="server" ControlToValidate="txtCardNumber" ErrorMessage="Enter Bank Account/Card Number">*</asp:requiredfieldvalidator></TD>
												<TD rowSpan="5">&nbsp;</TD>
											</TR>
											<TR>
												<TD style="WIDTH: 189px; HEIGHT: 19px">Bank Route Number:</TD>
												<TD style="WIDTH: 456px; HEIGHT: 19px"><asp:textbox id="txtBankRouteNum" runat="server" Width="192px" Enabled="False"></asp:textbox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 189px; HEIGHT: 23px">DL State &amp; Number:</TD>
												<TD style="WIDTH: 456px; HEIGHT: 23px"><asp:dropdownlist id="ddlDLState" runat="server" Width="192px" Enabled="False">
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
													</asp:dropdownlist><asp:textbox id="txtDLNum" runat="server" Width="144px" Enabled="False"></asp:textbox></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 189px; HEIGHT: 2px">Expiration Date:</TD>
												<TD style="WIDTH: 456px; HEIGHT: 2px"><asp:dropdownlist id="ddlExpMonth" runat="server"></asp:dropdownlist>/
													<asp:dropdownlist id="ddlExpYear" runat="server"></asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 189px; HEIGHT: 19px">CVV2</TD>
												<TD style="WIDTH: 456px; HEIGHT: 19px"><asp:textbox id="txtCVV2" runat="server" Width="72px" MaxLength="4"></asp:textbox><asp:regularexpressionvalidator id="Regularexpressionvalidator5" runat="server" ControlToValidate="txtCVV2" ErrorMessage="Enter correct number of digits on CVV2"
														ValidationExpression="\d{3,4}">*</asp:regularexpressionvalidator></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 189px; HEIGHT: 19px">Priority</TD>
												<TD style="WIDTH: 456px; HEIGHT: 19px"><asp:dropdownlist id="ddlPriority" runat="server" Width="72px">
														<asp:ListItem Value="1" Selected="True">1</asp:ListItem>
													</asp:dropdownlist></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 28px" rowSpan="5"></TD>
												<TD class="05_con_bold" style="WIDTH: 189px; HEIGHT: 65px"><asp:imagebutton id="btnSubmit" runat="server" ImageUrl="images/submit.jpg"></asp:imagebutton></TD>
												<td class="05_con_bold" style="WIDTH: 456px; HEIGHT: 65px"><asp:validationsummary id="ValidationSummary1" runat="server" Width="448px" Height="28px"></asp:validationsummary></td>
												<td rowSpan="5"></td>
											</TR>
										</TBODY>
									</table>
									<asp:label id="lblErrMsg" runat="server" Width="577px" ForeColor="Red"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
							</tr>
							<tr>
								<td vAlign="top" height="100%">
									<!------------------------------------------------------------------------>
									<TABLE id="Table1" style="WIDTH: 655px; HEIGHT: 34px" cellSpacing="1" cellPadding="1" width="655"
										border="0">
										<TR>
											<TD align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="images/btn_previous.jpg" CausesValidation="False"></asp:imagebutton>
											</TD>
											<TD align="right"><asp:imagebutton id="btnGotoMain" runat="server" ImageUrl="images/btn_gotomain.jpg"></asp:imagebutton></TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="2"><dpiuser:sitefooter id="Sitefooter1" runat="server"></dpiuser:sitefooter></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
