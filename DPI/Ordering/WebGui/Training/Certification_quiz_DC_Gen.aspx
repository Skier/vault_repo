<%@ Page language="c#" Codebehind="Certification_quiz_DC_Gen.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Training.Certification_quiz_DC_Gen" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Certification Quiz</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body text="#000000" bgColor="#ffffff" leftMargin="0" topMargin="0" ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="511" align="left" border="0">
				<tr>
					<td style="WIDTH: 511px">
						<asp:Image id="Image1" runat="server" ImageUrl="../Main/images/printheader.jpg"></asp:Image></td>
				</tr>
				<tr>
					<td align="center">
						<table cellSpacing="0" cellPadding="0" width="640" align="middle" border="0">
							<tr>
								<td align="center">
									<asp:Label id="Label1" runat="server" Font-Names="Tahoma" Font-Bold="True" Font-Size="Large"
										Width="322px">DebitCard Certification</asp:Label></td>
							</tr>
							<tr>
								<td colspan="2" align="left" style="WIDTH: 529px">
									<asp:Label id="Label2" runat="server" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">Please enter your name:</asp:Label></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label3" runat="server" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">Co-Worker ID:</asp:Label>
									<asp:TextBox id="txtCoWorkerID" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Please provide a coworker ID."
										ControlToValidate="txtCoWorkerID">*</asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:Label id="Label4" runat="server" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">Name: </asp:Label>
									<asp:TextBox id="txtName" runat="server"></asp:TextBox>
									<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Please provide a name."
										ControlToValidate="txtName">*</asp:RequiredFieldValidator></td>
							</tr>
							<tr>
								<td style=" HEIGHT: 89px" align="center">
									<asp:Label id="Label5" runat="server" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">Below are twelve questions designed to confirm you knowledge of dPi's new DebitCard program. Please choose one answer for each question. To select an answer, simply click on the circle next to the answer you believe is correct. When complete, please click the submit button at the bottom of this page.</asp:Label></td>
							</tr>
							<tr>
								<td style=" HEIGHT: 38px" align="center">
									<asp:Label id="Label6" runat="server" Font-Names="Tahoma" Font-Bold="True" Font-Size="Medium">Questions:</asp:Label></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label16" runat="server" Width="536px" Font-Size="X-Small" Font-Bold="True" Font-Names="Tahoma">1.	How old do you have to be to Enroll and receive a Purpose Prepaid MasterCard?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Please provide an answer for question number 1."
										ControlToValidate="RadioButtonList1">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist1" runat="server" Width="592px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="18">a. 18</asp:ListItem>
										<asp:ListItem Value="19">b. 19</asp:ListItem>
										<asp:ListItem Value="21">c. 21</asp:ListItem>
										<asp:ListItem Value="18or19">d. 18 or 19 depending upon which state you live in.</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label7" runat="server" Width="384px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small"> 2. Where can a cardholder use their Instant Issue Card?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ErrorMessage="Please provide an answer for question number 2."
										ControlToValidate="Radiobuttonlist2">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist2" runat="server" Width="576px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="NYCE">a.	Where Cirrus, NYCE or Star acceptance marks are displayed</asp:ListItem>
										<asp:ListItem Value="Mastercard">b.	Where MasterCard is accepted</asp:ListItem>
										<asp:ListItem Value="Purpose">c.	Where the Purpose acceptance mark is displayed</asp:ListItem>
										<asp:ListItem Value="Anywhere">d.	Anywhere debit cards are accepted</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label8" runat="server" Width="512px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">3. Why are there 2 cards?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" ErrorMessage="Please provide an answer for question number 3."
										ControlToValidate="Radiobuttonlist3">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist3" runat="server" Width="616px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="Backup">a. So that if a cardholder loses one, they have one as a backup.</asp:ListItem>
										<asp:ListItem Value="Friend">b. A cardholder has one to keep and one to give to a friend or family member.</asp:ListItem>
										<asp:ListItem Value="InstantIssue">c. The cardholder received an Instant Issue card when they enroll, and then their personalized Purpose Card arrives in the mail 7-10 days later.</asp:ListItem>
										<asp:ListItem Value="2and1">d. Because 2 cards are better than 1.</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label9" runat="server" Width="632px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">4. When a cardholder withdraws money from an ATM using an Instant Issue Card or a personalized Purpose Prepaid MasterCard, what do they choose on the ATM screen?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" ErrorMessage="Please provide an answer for question number 4."
										ControlToValidate="Radiobuttonlist4">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist4" runat="server" Width="264px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="Credit">a. Credit</asp:ListItem>
										<asp:ListItem Value="Debit">b. Debit</asp:ListItem>
										<asp:ListItem Value="Checking">c. Checking</asp:ListItem>
										<asp:ListItem Value="Savings">d. Savings</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label10" runat="server" Width="616px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small"> 5. The customer <u>
											must</u> provide what items in order to purchase a Purpose Prepaid MasterCard?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" ErrorMessage="Please provide an answer for question number 5."
										ControlToValidate="Radiobuttonlist5">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist5" runat="server" Width="624px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="SSN">a. Social Security Number or Federal Tax ID Number</asp:ListItem>
										<asp:ListItem Value="CurrentAddress">b. Current address and phone number</asp:ListItem>
										<asp:ListItem Value="ID">c. An acceptable form of identification such as: Driver’s License, State ID, US Passport, US Military ID, or Metricula Consular </asp:ListItem>
										<asp:ListItem Value="Funds">d. Funds between $10 and $2,500 to load on the card.</asp:ListItem>
										<asp:ListItem Value="All of the above">e. All of the above</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label11" runat="server" Width="520px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">6. True or False : <br> &nbsp;&nbsp;&nbsp;&nbsp;The Purpose Prepaid MasterCard cannot be used to pay for gas at the pump.</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator8" runat="server" ErrorMessage="Please provide an answer for question number 6."
										ControlToValidate="Radiobuttonlist6">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist6" runat="server" Width="264px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="True">a. True</asp:ListItem>
										<asp:ListItem Value="False">b. False</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0">
								</td>
							</tr>
							<TR>
								<TD style="WIDTH: 529px">
									<asp:Label id="Label12" runat="server" Width="568px" Font-Size="X-Small" Font-Bold="True" Font-Names="Tahoma">7. True or False : <br>&nbsp;&nbsp;&nbsp;&nbsp;The Purpose Prepaid MasterCard should not be used for reservations of rental cars.</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator9" runat="server" ControlToValidate="Radiobuttonlist6"
										ErrorMessage="Please provide an answer for question number 6.">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="RadioButtonList7" runat="server" Width="264px" Font-Size="X-Small" Font-Names="Tahoma">
										<asp:ListItem Value="True">a. True</asp:ListItem>
										<asp:ListItem Value="False">b. False</asp:ListItem>
									</asp:RadioButtonList></TD>
							</TR>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label13" runat="server" Width="592px" Font-Size="X-Small" Font-Bold="True" Font-Names="Tahoma">8. True or False : <br>&nbsp;&nbsp;&nbsp;&nbsp;The Instant Issue Card can be used to make debit purchases online and over the phone.</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator10" runat="server" ControlToValidate="Radiobuttonlist6"
										ErrorMessage="Please provide an answer for question number 6.">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="RadioButtonList8" runat="server" Width="264px" Font-Size="X-Small" Font-Names="Tahoma">
										<asp:ListItem Value="True">a. True</asp:ListItem>
										<asp:ListItem Value="False">b. False</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label14" runat="server" Width="512px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">9. Where can a cardholder Reload their Instant Issue Card or personalized Purpose Prepaid MasterCard?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator11" runat="server" ErrorMessage="Please provide an answer for question number 9."
										ControlToValidate="Radiobuttonlist9">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist9" runat="server" Width="624px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value=" At the nearest bank">a. At any bank near their house.</asp:ListItem>
										<asp:ListItem Value="At any Purpose Network location">b. At any Purpose Network location, including the location where they signed up for the Purpose Prepaid MasterCard.</asp:ListItem>
										<asp:ListItem Value="It is not reloadable">c. The Purpose Prepaid MasterCard is not reloadable.</asp:ListItem>
										<asp:ListItem Value="At any location with the MasterCard acceptance mark.">d. At any MasterCard Reload Center.</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label15" runat="server" Width="512px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">10. Where is the personalized Purpose Prepaid MasterCard accepted?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator12" runat="server" ErrorMessage="Please provide an answer for question number 10."
										ControlToValidate="Radiobuttonlist10">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist10" runat="server" Width="576px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="anywhere">a. Anywhere Visa and Discover acceptance marks are displayed.</asp:ListItem>
										<asp:ListItem Value="Displayed">b. Anywhere the MasterCard acceptance mark is displayed.</asp:ListItem>
										<asp:ListItem Value="Only in the US">c. Only in the US.</asp:ListItem>
										<asp:ListItem Value="Only where debit is accepted">d. Only where debit is accepted</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label17" runat="server" Width="192px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">11. Which statement is true?</asp:Label>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator13" runat="server" ErrorMessage="Please provide an answer for question number 10."
										ControlToValidate="Radiobuttonlist10">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist11" runat="server" Width="632px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="Reloadable">a. The Purpose Prepaid MasterCard is reloadable.</asp:ListItem>
										<asp:ListItem Value="NewHolder">b. The personalized Purpose Prepaid MasterCard is sent to the new cardholder 7-10 days after enrollment.</asp:ListItem>
										<asp:ListItem Value="The Instant Issue Card is automatically de-activated when the cardholder calls to activate their new personalized Purpose Prepaid MasterCard">c. The Instant Issue Card is automatically de-activated when the cardholder calls to activate their new personalized Purpose Prepaid MasterCard.</asp:ListItem>
										<asp:ListItem Value="All of the above are true.">d. All of the above are true.</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label18" runat="server" Width="440px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">12. The cardholder can go to which website to check their balance?</asp:Label>
									<asp:RequiredFieldValidator id="Requiredfieldvalidator14" runat="server" ErrorMessage="Please provide an answer for question number 10."
										ControlToValidate="Radiobuttonlist10">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist12" runat="server" Width="344px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="www.balance.com">a. www.balance.com</asp:ListItem>
										<asp:ListItem Value="www.purposecard.com">b. www.purposecard.com</asp:ListItem>
										<asp:ListItem Value="www.mycardbalance.com">c. www.mycardbalance.com</asp:ListItem>
										<asp:ListItem Value="None of the above">d. None of the above</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td align="center">
									<asp:ValidationSummary id="ValidationSummary1" runat="server" Width="528px"></asp:ValidationSummary>
									<asp:Label id="lblError" runat="server" Width="608px" Font-Names="Arial" ForeColor="Red" Visible="False"></asp:Label></td>
							</tr>
							<tr>
								<td align="center">
									<asp:ImageButton id="btnSubmit" runat="server" ImageUrl="../Main/images/btn_Submit.jpg"></asp:ImageButton></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td style="WIDTH: 511px"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
