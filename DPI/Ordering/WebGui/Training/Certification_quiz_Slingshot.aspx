<%@ Page language="c#" Codebehind="Certification_quiz_Slingshot.aspx.cs" AutoEventWireup="false" Inherits="DPI.Ordering.Training.Certification_quiz_Slingshot" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Slingshot Certification Quiz</title>
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
										Width="322px">Slingshot Certification</asp:Label></td>
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
									<asp:Label id="Label5" runat="server" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small"> Below are ten questions designed to confirm you knowledge of dPi's Slingshot product. Please choose one answer for each question. To select an answer, simply click on the circle next to the answer you believe is correct. When complete, please click the submit button at the bottom of this page.</asp:Label></td>
							</tr>
							<tr>
								<td style=" HEIGHT: 38px" align="center">
									<asp:Label id="Label6" runat="server" Font-Names="Tahoma" Font-Bold="True" Font-Size="Medium">Questions:</asp:Label></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label16" runat="server" Width="536px" Font-Size="X-Small" Font-Bold="True" Font-Names="Tahoma">1.	Slingshot Communication Inc. provides what services and features?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Please provide an answer for question number 1."
										ControlToValidate="RadioButtonList1">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist1" runat="server" Width="592px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="18">a. Prepaid Internet Access.</asp:ListItem>
										<asp:ListItem Value="19">b. No contract, commitment or credit card requirements.</asp:ListItem>
										<asp:ListItem Value="21">c. Local Internet access numbers with no Toll charges.</asp:ListItem>
										<asp:ListItem Value="18or19">d. All of the above.</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label7" runat="server" Width="632px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small"> 2. My Slingshot Primary Access Code (Pin) can be used on only one computer for security and to protect my minutes from being used by someone else?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ErrorMessage="Please provide an answer for question number 2."
										ControlToValidate="Radiobuttonlist2">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist2" runat="server" Width="576px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="NYCE">a.	True</asp:ListItem>
										<asp:ListItem Value="Mastercard">b.	False</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label8" runat="server" Width="576px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">3. The Slingshot Starter CD provides our Internet users the following features?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator5" runat="server" ErrorMessage="Please provide an answer for question number 3."
										ControlToValidate="Radiobuttonlist3">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist3" runat="server" Width="616px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="Backup">a. A 4-step install and registration wizard.</asp:ListItem>
										<asp:ListItem Value="Friend">b. About a 2-minute installation and registration period.</asp:ListItem>
										<asp:ListItem Value="InstantIssue">c. One time installation.</asp:ListItem>
										<asp:ListItem Value="2and1">d. All of the above.</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label9" runat="server" Width="632px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">4. The Slingshot Starter CD is free and available from your local Retail location?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator6" runat="server" ErrorMessage="Please provide an answer for question number 4."
										ControlToValidate="Radiobuttonlist4">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist4" runat="server" Width="264px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="Credit">a. True</asp:ListItem>
										<asp:ListItem Value="Debit">b. False</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label10" runat="server" Width="616px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small"> 5. The primary access code (pin) is available as a prepaid product listed on Web Central.?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator7" runat="server" ErrorMessage="Please provide an answer for question number 5."
										ControlToValidate="Radiobuttonlist5">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist5" runat="server" Width="624px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="SSN">a. True</asp:ListItem>
										<asp:ListItem Value="CurrentAddress">b. False</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label11" runat="server" Width="520px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">6. Slingshot Internet Access can only be used on?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator8" runat="server" ErrorMessage="Please provide an answer for question number 6."
										ControlToValidate="Radiobuttonlist6">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist6" runat="server" Width="536px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="True">a. On Apple computers.</asp:ListItem>
										<asp:ListItem Value="False">b. Windows operating system based computers.</asp:ListItem>
										<asp:ListItem Value="False">c. Cell phones.</asp:ListItem>
										<asp:ListItem Value="False">d. PDAs.</asp:ListItem>
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
									<asp:Label id="Label12" runat="server" Width="568px" Font-Size="X-Small" Font-Bold="True" Font-Names="Tahoma">7. Slingshot users are allowed?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator9" runat="server" ControlToValidate="Radiobuttonlist6"
										ErrorMessage="Please provide an answer for question number 6.">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="RadioButtonList7" runat="server" Width="560px" Font-Size="X-Small" Font-Names="Tahoma">
										<asp:ListItem Value="True">a. Free, unlimited email for life.</asp:ListItem>
										<asp:ListItem Value="False">b. A free email address while the primary access code has value.</asp:ListItem>
										<asp:ListItem Value="False">c. A free email address for 6 months.</asp:ListItem>
										<asp:ListItem Value="False">d. No free email.</asp:ListItem>
									</asp:RadioButtonList></TD>
							</TR>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label13" runat="server" Width="592px" Font-Size="X-Small" Font-Bold="True" Font-Names="Tahoma">8. Slingshot Email:</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator10" runat="server" ControlToValidate="Radiobuttonlist6"
										ErrorMessage="Please provide an answer for question number 6.">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="RadioButtonList8" runat="server" Width="536px" Font-Size="X-Small" Font-Names="Tahoma">
										<asp:ListItem Value="False">c. Can be used with your email client.</asp:ListItem>
										<asp:ListItem Value="False">a. Sign up for Slingshot email at  http://dialup.slingshot.com</asp:ListItem>
										<asp:ListItem Value="False">b. Can be checked from any computer connected to the Internet.</asp:ListItem>
										<asp:ListItem Value="True">d. All of the above.</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label14" runat="server" Width="512px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">9. Slingshot Customer Service?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator11" runat="server" ErrorMessage="Please provide an answer for question number 9."
										ControlToValidate="Radiobuttonlist9">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist9" runat="server" Width="624px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value=" At the nearest bank">a. Is available online under support at www.slingshot.com</asp:ListItem>
										<asp:ListItem Value="At any Purpose Network location">b. Is available in person at 866-506-9600.</asp:ListItem>
										<asp:ListItem Value="It is not reloadable">c. By email at support@corp.slingshot.com</asp:ListItem>
										<asp:ListItem Value="At any location with the MasterCard acceptance mark.">d. All of the above.</asp:ListItem>
									</asp:RadioButtonList>
								</td>
							</tr>
							<tr>
								<td style=" HEIGHT: 25px"><IMG height="1" src="../main/images/pixel_gray.jpg" width="100%" border="0"></td>
							</tr>
							<tr>
								<td style="WIDTH: 529px">
									<asp:Label id="Label15" runat="server" Width="632px" Font-Names="Tahoma" Font-Bold="True" Font-Size="X-Small">10. Slingshot Customer Support is friendly, professional, knowledgeable and convenient with no cost to our customers?</asp:Label>
									<asp:RequiredFieldValidator id="RequiredFieldValidator12" runat="server" ErrorMessage="Please provide an answer for question number 10."
										ControlToValidate="Radiobuttonlist10">*</asp:RequiredFieldValidator>
									<asp:RadioButtonList id="Radiobuttonlist10" runat="server" Width="576px" Font-Names="Tahoma" Font-Size="X-Small">
										<asp:ListItem Value="anywhere">a. True</asp:ListItem>
										<asp:ListItem Value="Displayed">b. False</asp:ListItem>
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
