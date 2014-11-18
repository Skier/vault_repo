<%@ Page language="c#" Codebehind="bank_rec_payment_editor.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Payment.BankReccuringPaymentEditorPage" %>
<%@ Register TagPrefix="dns" TagName="BillingAccountInfoEditor" Src="~/account/payment/billing_account_info_editor.ascx" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../DPI.css" type="text/css" rel="stylesheet">
		<script type="text/Jscript">
			function UpdateControls()
			{	
				pnpaCtrl = document.all["txtNpa"];	
				pnxxCtrl = document.all["txtNxx"];	
				pnCtrl = document.all["txtNumber"];	
				anCtrl = document.all["txtAccountNumber"];	
				
				document.all["lblPhoneNumber"].disabled = pnpaCtrl.disabled = pnxxCtrl.disabled = pnCtrl.disabled = anCtrl.value.length > 0;
				document.all["lblAccountNumber"].disabled = anCtrl.disabled = 
					(pnpaCtrl.value.length > 0 || pnxxCtrl.value.length > 0 || pnCtrl.value.length > 0);
			}
		</script>
	</HEAD>
	<body>
		<form id="loginForm" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD colSpan="2">
						<dns:header id="ctrlHeader" runat="server"></dns:header>
					</TD>
				</TR>
				<TR>
					<TD rowSpan="2">
						<IMG alt="" src="../../images/about_side.jpg">
					</TD>
					<TD vAlign="top" align="left">
						<IMG src="../../images/ppc_top.jpg" border="0">
						<TABLE class="layout_table">
							<TR class="separator_row">
								<TD colSpan="4"></TD>
							</TR>
							<TR>
								<TD class="left_padding"></TD>
								<TD colSpan="3">
									<asp:CustomValidator id="vldCustErrorMsg" runat="server" Display="None" ErrorMessage="Initialize me"
										EnableClientScript="False"></asp:CustomValidator>
									<asp:ValidationSummary id="vldSummary" runat="server" CssClass="Error" DisplayMode="List"></asp:ValidationSummary></TD>
							</TR>
							<TR class="separator_row">
								<TD colSpan="4"></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD class="header" colSpan="3" bgColor="#d2691e">Bank Draft&nbsp;Info</TD>
							</TR>
							<TR class="separator_row">
								<TD colSpan="4"></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD class="property_name">Bank Account&nbsp;Number</TD>
								<TD class="property_value">
									<asp:TextBox id="txtBankAccountNumber" runat="server" Width="100%" MaxLength="16"></asp:TextBox></TD>
								<TD class="property_validator">
									<asp:RequiredFieldValidator id="vldRfBankAccountNumber" runat="server" ErrorMessage="Bank Account Number can not be empty"
										ControlToValidate="txtBankAccountNumber" Display="None"></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="vldReBankAccountNumber" runat="server" Display="None" ControlToValidate="txtBankAccountNumber"
										ErrorMessage="Bank Account Number is invalid" ValidationExpression="\d{16}"></asp:RegularExpressionValidator></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD class="property_name">Bank&nbsp;Route&nbsp;Number</TD>
								<TD class="property_value">
									<asp:TextBox id="txtBankRouteNumber" runat="server" Width="100%" MaxLength="9"></asp:TextBox></TD>
								<TD class="property_validator">
									<asp:RequiredFieldValidator id="vldRfBankRouteNumber" runat="server" Display="None" ControlToValidate="txtBankRouteNumber"
										ErrorMessage="Bank Route Number can not be empty"></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="vldReBankRouteNumber" runat="server" Display="None" ControlToValidate="txtBankRouteNumber"
										ErrorMessage="Bank Route Number is invalid" ValidationExpression="\d{9}"></asp:RegularExpressionValidator></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD class="property_name">
									<asp:Label id="lblDrvLicState" runat="server">Driver License State</asp:Label></TD>
								<TD class="property_value">
									<asp:DropDownList id="ddlDrvLicState" runat="server" Width="100%"></asp:DropDownList></TD>
								<TD class="property_validator">
									<asp:RequiredFieldValidator id="vldRfDrvLicState" runat="server" Display="None" ControlToValidate="ddlDrvLicState"
										ErrorMessage="Driver License State can not be empty"></asp:RequiredFieldValidator></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD class="property_name">
									<asp:Label id="lblDrvLicNumber" runat="server">Driver License Number</asp:Label></TD>
								<TD class="property_value">
									<asp:TextBox id="txtDrvLicNumber" runat="server" Width="100%"></asp:TextBox></TD>
								<TD class="property_validator">
									<asp:RequiredFieldValidator id="vldRfDrvLicNumber" runat="server" ErrorMessage="Driver License Number can not be empty"
										Display="None" ControlToValidate="txtDrvLicNumber"></asp:RequiredFieldValidator></TD>
							</TR>
							<TR class="separator_row">
								<TD colSpan="4"></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD class="header" colSpan="3" bgColor="#d2691e">Billing Account Info</TD>
							</TR>
							<TR class="separator_row">
								<TD colSpan="4"></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD colSpan="3">
									<dns:BillingAccountInfoEditor id="ctrlBillingAccountInfo" runat="server"></dns:BillingAccountInfoEditor>
								</TD>
							</TR>
							<TR class="separator_row">
								<TD colSpan="4"></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD class="property_name"></TD>
								<TD align="right"><asp:imagebutton id="btnSubmit" runat="server" ImageUrl="../../images/submit.jpg"></asp:imagebutton>
									<asp:imagebutton id="btnCancel" runat="server" ImageUrl="../../images/cancel.jpg" CausesValidation="False"></asp:imagebutton>&nbsp;</TD>
								<TD class="property_validator"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD vAlign="bottom" align="center" colspan="2">
						<dns:footer id="ctrlFooter" runat="server"></dns:footer></TD>					
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
