<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Register TagPrefix="ctl" Namespace="Dpi.Central.Web.Controls" Assembly="Dpi.Central.Web" %>
<%@ Page language="c#" Codebehind="account_settings.aspx.cs" AutoEventWireup="false"
Inherits="Dpi.Central.Web.Account.AccountSettings" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
<title>dPi Teleconnect LLC</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="../DPI.css" type=text/css rel=stylesheet >
<script type=text/javascript>
<!--
var pwd, cnf, pwdTxt = null, cnfFld, chkShowPwd, lblChangedNotify;

function checkPassword(source, arguments){
	var newPwd;
	var pwdCnf;

	arguments.IsValid = true;

	if (chkShowPwd.checked){
		newPwd = pwdCnf = pwdTxt.value;
	} else {
		newPwd = pwd.value;
		pwdCnf = cnf.value;
	}

	if (!newPwd){
		arguments.IsValid = true;
		return;
	}
	
	var val = el("pwdConfirmValidator");
	
	if (!chkShowPwd.checked && newPwd != pwdCnf){
		arguments.IsValid = false;
		val.errormessage = "Password does not match";
		return;
	}

	if (newPwd != newPwd.trim()){
		arguments.IsValid = false;
		val.errormessage = "Password cannot start and/or end with spaces. Please change it";
		return;
	}
}

function setPwdVisible(value){
	if (value){
		if (!pwdTxt){
			pwdTxt = ce("input", pwd.parentNode);
			pwdTxt.type = "text";
			pwdTxt.className = pwd.className;
			pwdTxt.style.width = "100%";

			cnfFld = document.createElement('input');
			cnfFld.type = 'hidden';
			cnf.form.appendChild(cnfFld);
			pushHandler(pwdTxt, "change", function (e){cnfFld.value = pwdTxt.value});
		}
		pwd.style.display = "none";
		pwdTxt.style.display = "block";
		cnfFld.value = pwdTxt.value = pwd.value;
		cnfFld.name = cnf.name;
		cnf.name = "";
		cnf.style.backgroundColor = "ButtonFace";
		pwdTxt.select();
	} else {
		pwd.style.display = "block";
		pwdTxt.style.display = "none";
		pwd.value = pwdTxt.value;
		cnf.style.backgroundColor = "";
		cnf.name = cnfFld.name;
		cnfFld.name = "";
	}
	cnf.readOnly = cnf.disabled = value;
}
//-->
</script>
</HEAD>
<body>
<form id=loginForm method=post runat="server">
<TABLE cellSpacing=0 cellPadding=0 border=0>
  <TBODY>
  <TR>
    <TD colSpan=2><dns:header id=ctrlHeader runat="server"></dns:header></TD>
    <TD vAlign=top align=left></TD></TR>
  <TR>
    <TD rowSpan=2><IMG alt="" src="../images/about_side.jpg" > </TD>
    <TD vAlign=top align=left><IMG src="../images/ppc_top.jpg" border=0 > 
      <TABLE class=layout_table>
        <TBODY>
        <TR class=separator_row>
          <TD colSpan=3>&nbsp;</TD></TR>
        <TR>
          <TD colSpan=3><asp:customvalidator id=vldCustErrorMsg runat="server" ErrorMessage="Initialize me" Display="None" EnableClientScript="False" Width="100%"></asp:customvalidator><asp:validationsummary id=vldSummary runat="server" DisplayMode="List" CssClass="Error"></asp:validationsummary><asp:label id=lblChangedNotify runat="server" ENABLEVIEWSTATE="False" forecolor="red" visible="False">Your account settings are saved</asp:label></TD></TR>
        <TR>
          <TD colSpan=3 height=20>&nbsp;</TD></TR>
        <TR>
          <TD noWrap width="20%"><ctl:ControlLabel target="txtEMail" id=lblEMail runat="server" ENABLEVIEWSTATE="False">E-Mail</ctl:ControlLabel></TD>
          <TD width="30%"><ctl:textbox id=txtEMail runat="server" Width="100%" MaxLength="50"></ctl:textbox></TD>
          <TD width="30%"><ASP:REGULAREXPRESSIONVALIDATOR id=emailValidator runat="server" validationexpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" controltovalidate="txtEMail" errormessage="E-Mail address is invalid" display="None" ENABLEVIEWSTATE="False"></ASP:REGULAREXPRESSIONVALIDATOR></TD></TR>
        <TR>
          <TD style="PADDING-TOP: 12px" colSpan=3>Change Web Access Password<br><span 
            style="FONT-WEIGHT: normal">Fill the following fields only if you want to change your password</span></TD></TR>
        <TR>
          <TD noWrap><ctl:ControlLabel Target="txtNewPassword" id=lblNewPassword runat="server" ENABLEVIEWSTATE="False">New Password</ctl:ControlLabel></TD>
          <TD><ctl:textbox id=txtNewPassword runat="server" Width="100%" TextMode="Password"></ctl:textbox></TD>
          <TD></TD></TR>
        <TR>
          <TD noWrap><ctl:ControlLabel Target="txtPasswordConfirm" id=lblPasswordConfirm runat="server" ENABLEVIEWSTATE="False">Confirm Password</ctl:ControlLabel></TD>
          <TD><ctl:textbox id=txtPasswordConfirm runat="server" Width="100%" TextMode="Password"></ctl:textbox></TD>
          <TD><asp:customvalidator id=pwdConfirmValidator runat="server" ErrorMessage="Password does not match" controltovalidate="txtNewPassword" display="None" CLIENTVALIDATIONFUNCTION="checkPassword" ENABLEVIEWSTATE="False"></asp:customvalidator></TD></TR>
        <TR style="VISIBILITY:hidden" id="chkShowPwdTR">
          <TD>&nbsp;</TD>
          <TD colSpan=2><ASP:CHECKBOX id=chkShowPwd runat="server" text="Show password" EnableViewState="False"></ASP:CHECKBOX></TD></TR>
        <TR>
          <TD>&nbsp;</TD>
          <TD align=right><asp:imagebutton id=btnSubmit runat="server" ImageUrl="../images/submit.jpg"></asp:imagebutton></TD>
          <TD>&nbsp;</TD></TR></TBODY></TABLE></TD></TR>
  <TR>
    <TD vAlign=bottom align=center><dns:footer id=ctrlFooter runat="server"></dns:footer></TD>
    <TD vAlign=top align=left></TD></TR></TBODY></TABLE></form></TR></TBODY></TABLE></TR></TBODY></TABLE></FORM>
<script type="text/javascript">
<!--
chkShowPwd = el("chkShowPwd");
pushHandler(chkShowPwd, "click", function (e){setPwdVisible(chkShowPwd.checked)});
pwd = el("txtNewPassword");
cnf = el("txtPasswordConfirm");
el("chkShowPwdTR").style.visibility = "visible";
lblChangedNotify = el("lblChangedNotify");
if (lblChangedNotify) pushHandler(el("btnSubmit"), "click", function (e){lblChangedNotify.style.visibility = "hidden"});
//-->
</script>    
</body>
</HTML>
