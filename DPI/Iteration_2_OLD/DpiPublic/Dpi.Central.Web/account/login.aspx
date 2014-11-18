<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Register TagPrefix="ctl" Namespace="Dpi.Central.Web.Controls" Assembly="Dpi.Central.Web" %>
<%@ Page language="c#" Codebehind="login.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.LoginPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
<title>dPi Teleconnect LLC</title>
<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
<meta content="C#" name="CODE_LANGUAGE">
<meta content="JavaScript" name="vs_defaultClientScript">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<link href="../DPI.css" type="text/css" rel="stylesheet">
<script type="text/javascript">
<!--
var txtNpa = null;
var txtNxx;
var txtNumber;
var txtAccountNumber;
var txtPassword;
var lblPhoneNumber;
var lblAccountNumber;

function UpdateControls(e){
	checkBoxes();

	/*if (e.propertyName == "value"){
		var el = getEventElement(e);

		if (el && typeof el.value != "undefined" && el.value && !el.getAttribute("trimming")){
			el.setAttribute("trimming", true);
			el.value = el.value.trim();
			el.setAttribute("trimming", false);
		}
	}*/

	disableBoxes([lblPhoneNumber, txtNpa, txtNxx, txtNumber], txtAccountNumber.value.length > 0);
	disableBoxes([lblAccountNumber, txtAccountNumber], (txtNpa.value.length > 0 || txtNxx.value.length > 0 || txtNumber.value.length > 0));
}

function checkBoxes(){
	if (!txtNpa){
		txtNpa = el("txtNpa");
		txtNxx = el("txtNxx");
		txtNumber = el("txtNumber");
		txtAccountNumber = el("txtAccountNumber");
		txtPassword = el("txtPassword");
		lblPhoneNumber = el("lblPhoneNumber");
		lblAccountNumber = el("lblAccountNumber");

		/*var boxes = [txtNpa, txtNxx, txtNumber, txtAccountNumber, txtPassword];
		for (var i = 0; i < boxes.length; i++){
			pushHandler(boxes[i], "change", function(e){
					var o = getEventElement(e);
					if (!o || typeof o.value == "undefined" || !o.value) return;
					o.value = o.value.trim();
				})
		}*/
	}
}

function disableBoxes(boxes, value){
	for (var i = 0; i < boxes.length; i++){
		var box = boxes[i];
		box.disabled = value;
		if (box.tagName.toLowerCase() == "input"){
			box.style.backgroundColor = value ? "ButtonFace" : "";
		}
	}
}
//-->
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
						<IMG alt="" src="../images/about_side.jpg">
					</TD>
					<TD vAlign="top" align="left">
						<IMG src="../images/ppc_top.jpg" border="0">
						<TABLE class="layout_table">
							<TR class="separator_row">
								<TD colSpan="3"></TD>
							</TR>
							<TR>
								<TD colSpan="3">
									<asp:CustomValidator id="vldCustErrorMsg" runat="server" ErrorMessage="Initialize me" Display="None"
										EnableClientScript="False" Width="100%"></asp:CustomValidator>
									<asp:ValidationSummary id="vldSummary" runat="server" DisplayMode="List" CssClass="Error"></asp:ValidationSummary></TD>
							</TR>
							<TR>
								<TD colSpan="3" height="20"></TD>
							</TR>
							<TR>
								<TD width="20%" noWrap><asp:label id="lblPhoneNumber" runat="server">Phone Number</asp:label></TD>
								<TD width="30%">
									<TABLE cellpadding=0 cellspacing=0 border=0 width="100%">
										<TR>
										<TD width=57><CTL:TEXTBOX id="txtNpa" runat="server" width="100%" maxlength="3"></CTL:TEXTBOX></TD>
										<TD width=12>&nbsp;-&nbsp;</TD>
										<TD width=57><CTL:TEXTBOX id="txtNxx" runat="server" width="100%" maxlength="3"></CTL:TEXTBOX></TD>
										<TD width=12>&nbsp;-&nbsp;</TD>
										<TD><CTL:TEXTBOX id="txtNumber" runat="server" width="100%" maxlength="4"></CTL:TEXTBOX></TD>
										</TR>
									</TABLE></TD>
								<TD width="30%"></TD>
							</TR>
							<TR>
								<TD noWrap></TD>
								<TD align="center">
									<asp:Label id="lblOr" runat="server">-- OR --</asp:Label></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD noWrap><asp:label id="lblAccountNumber" runat="server">Account Number</asp:label></TD>
								<TD><ctl:textbox id="txtAccountNumber" runat="server" Width="100%"></ctl:textbox></TD>
								<TD><asp:regularexpressionvalidator id="anRegExpValidator" runat="server" ValidationExpression="\d{8}" ControlToValidate="txtAccountNumber"
										ErrorMessage="Account Number is invalid" Display="None" ENABLED="False" ENABLEVIEWSTATE="False"></asp:regularexpressionvalidator></TD>
							</TR>
							<TR>
								<TD noWrap><asp:label id="lblPassword" runat="server">Password</asp:label></TD>
								<TD><ctl:textbox id="txtPassword" runat="server" Width="100%" TextMode="Password"></ctl:textbox></TD>
								<TD><asp:requiredfieldvalidator id="pwdReqFldValidator" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password can not be empty"
										Display="None"></asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD align="right">
									<asp:imagebutton id="btnSubmit" runat="server" ImageUrl="../images/submit.jpg"></asp:imagebutton></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD><asp:linkbutton id="lbtnForgotMyPassword" runat="server" CausesValidation="False">Forgot My Password</asp:linkbutton></TD>
								<TD></TD>
							</TR>
							<TR>
								<TD></TD>
								<TD>
									<asp:LinkButton id="lbtnSignUp" runat="server" CausesValidation="False">Web Access Sign Up</asp:LinkButton></TD>
								<TD></TD>
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
<script type="text/javascript">
<!--
checkBoxes();
//-->
</script>
</body>
</HTML>
