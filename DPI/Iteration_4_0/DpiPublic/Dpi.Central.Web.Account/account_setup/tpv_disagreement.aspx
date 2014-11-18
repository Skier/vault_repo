<%@ Page language="c#" Codebehind="tpv_disagreement.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.AccountSetup.TpvDisagreement" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • TPV Disagreement</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="tpvDisagreementForm" method="post" runat="server">
			<div class="process_form">
				<div class="step_caption">
					Conversion Verification – Disagree statement
				</div>
				<div class="row">
					<span class="statement">To keep your existing telephone number with dPi Teleconnect 
						service, we are required to receive your permission to switch your home 
						telephone service from your current Telephone Company. By disagreeing with this 
						verification we are not able to switch your telephone number. If you wish to 
						receive a new telephone number and not maintain your current number then please 
						select the entry to “Return to Select Provider”. If you elect to return to 
						the product selection your home telephone number must be completely 
						disconnected, through your current company, for dPi Teleconnect to establish 
						new service.
						<br><br>
						If you wish to continue with your current product selection and keep your 
						existing telephone number, please select Return to Agreement and proceed with 
						selecting the “I Agree” button. </span>
				</div>
				<div class="spacer">&nbsp;</div>
				<div class="button_row">
					<span class="back_button">
						<asp:imagebutton id="btnPrevious" runat="server" ImageUrl="../images/btn_return_to_agreement.gif"
							CausesValidation="False"></asp:imagebutton>
					</span><span class="next_button">
						<asp:imagebutton id="btnBackToPackageSelection" runat="server" ImageUrl="../images/btn_return_to_provider_selection.gif"></asp:imagebutton>
					</span>
				</div>
			</div>
		</form>
	</body>
</HTML>
