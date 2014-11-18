<%@ Page language="c#" Codebehind="reciept.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Payment.RecieptPage" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<%@ Register TagPrefix="cr" Namespace="CrystalDecisions.Web" Assembly="CrystalDecisions.Web, Version=9.1.5000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Payment Accepted</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table1" cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td colSpan="2"><dns:header id="ctrlHeader" runat="server"></dns:header></td>
					<td vAlign="top" align="left"></td>
				</tr>
				<tr>
					<td vAlign="top" rowSpan="2"><IMG alt="" src="../../images/about_side.jpg">
					</td>
					<td vAlign="top" align="center"><IMG src="../../images/ppc_top.jpg" border="0">
						<table class="layout_table" style="WIDTH: 490px" cellPadding="2" align="center">
							<tr class="separator_row">
								<td></td>
							</tr>
							<tr>
								<td>
									<table class="layout_table">
										<tr>
											<th align="center" colSpan="2">
												<big>Payment&nbsp;Receipt</big></th></tr>
										<tr>
											<td colSpan="2">
												<h4>Account Information</h4>
											</td>
										</tr>
										<tr>
											<td>Account Number</td>
											<td><asp:label id="lblAccountNumber" Runat="server"></asp:label></td>
										</tr>
										<tr>
											<td>Phone Number</td>
											<td><asp:label id="lblPhoneNumber" Runat="server"></asp:label></td>
										</tr>
										<tr>
											<td>Account Name</td>
											<td><asp:label id="lblFirstName" Runat="server"></asp:label>&nbsp;<asp:label id="lblLastName" Runat="server"></asp:label></td>
										</tr>
										<TR class="separator_row">
											<TD colSpan="2"></TD>
										</TR>
										<TR>
											<TD colSpan="2">
												<h4>Payor Information</h4>
											</TD>
										</TR>
										<tr>
											<td>Payor Name</td>
											<td><asp:label id="lblPayorFirstName" Runat="server"></asp:label>&nbsp;<asp:label id="lblPayorLastName" Runat="server"></asp:label></td>
										</tr>
										<tr>
											<td>Street Address</td>
											<td><asp:label id="lblStreetAddress" Runat="server"></asp:label></td>
										</tr>
										<tr>
											<td>
												<P>City State Zip</P>
											</td>
											<td><asp:label id="lblCity" Runat="server"></asp:label>&nbsp;<asp:label id="lblState" Runat="server"></asp:label>&nbsp;<asp:label id="lblZip" Runat="server"></asp:label></td>
										</tr>
										<tr>
											<td>E-Mail</td>
											<td><asp:label id="lblEmail" Runat="server"></asp:label></td>
										</tr>
										<tr class="separator_row">
											<td colSpan="2"></td>
										</tr>
										<tr>
											<td colSpan="2">
												<h4>Payment Information</h4>
											</td>
										</tr>
										<tr>
											<td>Payment Type</td>
											<td><asp:label id="lblPaymentType" Runat="server"></asp:label></td>
										</tr>
										<tr id="trCreditCardInfo" style="DISPLAY: none" runat="server">
											<td>Credit Card Number</td>
											<td><asp:label id="lblCreditCard" Runat="server"></asp:label></td>
										</tr>
										<tr id="trCheckInfo" style="DISPLAY: none" runat="server">
											<td>Check Number</td>
											<td><asp:label id="lblCheckNumber" Runat="server"></asp:label></td>
										</tr>
										<tr>
											<td>Payment Amount</td>
											<td><asp:label id="lblPaymentAmount" Runat="server"></asp:label></td>
										</tr>
										<tr>
											<td>Payment Date</td>
											<td><asp:label id="lblPaymentDate" Runat="server"></asp:label></td>
										</tr>
										<tr>
											<td>Approval/Confirmation Number
											</td>
											<td><asp:label id="lblConfirmationNumber" Runat="server"></asp:label></td>
										</tr>
										<tr class="separator_row">
											<td colSpan="2"></td>
										</tr>
										<tr class="separator_row">
											<td colSpan="2"></td>
										</tr>
										<tr>
											<td align="center" colSpan="2">Thank you for choosing dPi Teleconnect.
											</td>
										</tr>
										<tr class="separator_row">
											<td colSpan="2"></td>
										</tr>
										<tr class="separator_row">
											<td colSpan="2"></td>
										</tr>
										<tr>
											<td colSpan="2"><asp:hyperlink id="lnkReturn" runat="server" NavigateUrl="~/account/summary.aspx">Return to Account Summary</asp:hyperlink></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td vAlign="bottom" align="center"><dns:footer id="ctrlFooter" runat="server"></dns:footer></td>
					<td vAlign="top" align="left"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
