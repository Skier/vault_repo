<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Register TagPrefix="dns" TagName="BillingAccountInfoEditor" Src="~/account/payment/billing_account_info_editor.ascx" %>
<%@ Page language="c#" Codebehind="cc_rec_payment_editor.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Payment.CreditCardReccuringPaymentEditorPage" %>
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
			
			function ValidateExpirationDate(source, arguments) 
			{
				var y = document.all["ddlExpYear"].value * 1;
				var m = document.all["ddlExpMonth"].value * 1;
				var d = new Date();
				
				arguments.IsValid=!(y <= d.getFullYear() && m < d.getMonth() + 1);
			}
		</script>
	</HEAD>
	<body>
		<form id="loginForm" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD colSpan="2"><dns:header id="ctrlHeader" runat="server"></dns:header></TD>
					<TD vAlign="top" align="left"></TD>
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
								<TD class="header" colSpan="3" bgColor="#d2691e">Credit Card Info</TD>
							</TR>
							<TR class="separator_row">
								<TD colSpan="4"></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD class="property_name">
									<asp:Label id="lblCcType" runat="server">Credit Card Type</asp:Label></TD>
								<TD class="property_value">
									<asp:DropDownList id="ddlCcType" runat="server" Width="100%" AutoPostBack="True"></asp:DropDownList></TD>
								<TD class="property_validator">
									<asp:RequiredFieldValidator id="vldRfCcType" runat="server" ErrorMessage="Credit Card Type can not be empty"
										Display="None" ControlToValidate="ddlCcType"></asp:RequiredFieldValidator></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD class="property_name">Credit Card Number</TD>
								<TD class="property_value">
									<asp:TextBox id="txtCcNumber" runat="server" Width="100%" MaxLength="16"></asp:TextBox></TD>
								<TD class="property_validator">
									<asp:RequiredFieldValidator id="vldRfCcNumber" runat="server" ErrorMessage="Credit Card Number can not be empty"
										ControlToValidate="txtCcNumber" Display="None"></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="vldReCcNumber" runat="server" Display="None" ControlToValidate="txtCcNumber"
										ErrorMessage="Credit Card Number is invalid" ValidationExpression="\d{16}"></asp:RegularExpressionValidator></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD class="property_name">
									<asp:Label id="lblExpDate" runat="server">Expiration Date</asp:Label></TD>
								<TD class="property_value">
									<asp:dropdownlist id="ddlExpMonth" runat="server"></asp:dropdownlist>
									<asp:Label id="lblExpDateSeparator" runat="server">&nbsp;&nbsp;/&nbsp;&nbsp;</asp:Label>
									<asp:dropdownlist id="ddlExpYear" runat="server"></asp:dropdownlist></TD>
								<TD class="property_validator">
									<asp:RequiredFieldValidator id="vldRfExpMonth" runat="server" ControlToValidate="ddlExpMonth" ErrorMessage="Month can not be empty"
										Display="None"></asp:RequiredFieldValidator>
									<asp:RequiredFieldValidator id="vldRfExpYear" runat="server" ControlToValidate="ddlExpYear" ErrorMessage="Year can not be empty"
										Display="None"></asp:RequiredFieldValidator>
									<asp:CustomValidator id="vldCstExpDate" runat="server" ErrorMessage="Invalid Expiration Date" Display="None"
										ClientValidationFunction="ValidateExpirationDate"></asp:CustomValidator></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD class="property_name">
									<asp:Label id="lblSecurityCode" runat="server">Security Code</asp:Label></TD>
								<TD class="property_value">
									<asp:TextBox id="txtSecurityCode" runat="server" Width="100%" MaxLength="4"></asp:TextBox></TD>
								<TD class="property_validator">
									<asp:RequiredFieldValidator id="vldRfSecurityCode" runat="server" ErrorMessage="Security Code can not be empty"
										ControlToValidate="txtSecurityCode" Display="None"></asp:RequiredFieldValidator>
									<asp:RegularExpressionValidator id="vldReSecurityCode" runat="server" Display="None" ControlToValidate="txtSecurityCode"
										ErrorMessage="Security Code is invalid" ValidationExpression="\d{4}"></asp:RegularExpressionValidator></TD>
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
					<TD vAlign="bottom" align="center">
						<dns:footer id="ctrlFooter" runat="server"></dns:footer></TD>
					<TD vAlign="top" align="left"></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
