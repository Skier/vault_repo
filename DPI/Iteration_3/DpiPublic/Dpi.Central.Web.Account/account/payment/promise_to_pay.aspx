<%@ Page language="c#" Codebehind="promise_to_pay.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Payment.PromiseToPayPage" %>
<%@ Register TagPrefix="dns" TagName="Footer" Src="~/footer.ascx" %>
<%@ Register TagPrefix="dns" TagName="Header" Src="~/header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>dPi Teleconnect LLC</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../../DPI.css" type="text/css" rel="stylesheet">
		<script src="../../script/dhtmlutils.js" type="text/javascript"></script>
		<script src="../../script/DatePicker.js" type="text/javascript"></script>
		<script type="text/javascript">
			function OnEnterPress(buttonId) 
			{
				button = document.getElementById(buttonId);
				
				// Enter key has been press.
				if (window.event.keyCode == 13) {
					button.click();
				}
			}
		</script>
</HEAD>
	<body onkeypress="OnEnterPress('btnSubmit');">
		<form id="promiseToPayForm" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<td colSpan="2"><dns:header id="ctrlHeader" runat="server"></dns:header></td>
				</tr>
				<tr>
					<td rowSpan="2">
						<IMG alt="" src="../../images/about_side.jpg">
					</td>
					<td vAlign="top" align="left">
						<IMG src="../../images/ppc_top.jpg" border="0">
						<table class="layout_table">
							<TR>
								<TD colspan="3">
									<asp:CustomValidator id="vldCustErrorMsg" runat="server" ErrorMessage="Initialize me" EnableClientScript="False"></asp:CustomValidator>
								</TD>
							</TR>
							<tr class="separator_row">
								<td colspan="3"></td>
							</tr>
							<TR>
								<TD class="property_name">
									<asp:imagebutton id="btnSubmit" runat="server" ImageUrl="../../images/submit.jpg"></asp:imagebutton>
								</TD>
							</TR>
							<TR>
								<TD>
<asp:HyperLink id=lnkSummary runat="server" NavigateUrl="~/account/summary.aspx">Return to Account Summary</asp:HyperLink>

								</TD>
							</TR>
						</table>
					</td>
				</tr>
				<TR>
					<TD vAlign="bottom" align="center" colSpan="2">
						<dns:footer id="ctrlFooter" runat="server"></dns:footer></TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
