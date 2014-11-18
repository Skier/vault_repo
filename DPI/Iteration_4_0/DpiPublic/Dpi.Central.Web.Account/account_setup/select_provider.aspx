<%@ Page language="c#" Codebehind="select_provider.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.AccountSetup.SelectProvider" %>
<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Select Provider</title>
		<meta name="vs_showGrid" content="False">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script src="AjaxZip.js"></script>		
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="selectprovider" method="post" runat="server">
			<div class="process_form">
				<table class="process_table">
					<tbody>
						<TR>
							<TD class="question">I would like to switch my existing home telephone number</TD>
							<TD class="answer"><asp:radiobutton id="m_rbnMovePhoneYes" runat="server" GroupName="MoveExistingPhoneGroup" Text="Yes"></asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:radiobutton id="m_rbnMovePhoneNo" runat="server" GroupName="MoveExistingPhoneGroup" Text="No"></asp:radiobutton></TD>
						</TR>
						<asp:TableRow id="m_rowPhoneFields" runat="server">
							<asp:TableCell class="question">
								Please enter the existing phone number you would like to keep
							</asp:TableCell>
							<asp:TableCell class="answer">
								<dwc:phonenumberbox id="phnPhoneNumber" runat="server"></dwc:phonenumberbox>
								<asp:customvalidator id="vldCstPhoneNumber" runat="server" Display="Dynamic" ErrorMessage="<br>The Phone Number provided is invalid"
									ClientValidationFunction="ValidatePhoneNumber"></asp:customvalidator>
							</asp:TableCell>
						</asp:TableRow>
						<TR>
							<TD class="question">Please provide your zip code</TD>
							<TD class="answer">
								<asp:textbox id="m_txtZip" runat="server" Width="72px" MaxLength="5" onkeyup="return OnZipChanged()"
									autocomplete="off"></asp:textbox>
								<asp:customvalidator id="m_vldZip" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="Zip Validator"></asp:customvalidator>
								<asp:Label id="m_lblZipError" runat="server" ForeColor="Red"></asp:Label>
								<asp:Label id="m_lblWirelessProducts" runat="server">Check out our <a href="localhost">wireless 
										products</a></asp:Label>
							</TD>
						</TR>
						<asp:TableRow id="m_rowProviderField" runat="server">
							<asp:TableCell class="question">
								Please select the local telephone company in your area
							</asp:TableCell>
							<asp:TableCell class="answer">
								<asp:DropDownList id="m_cmbProviders" runat="server" Width="240px" onchange="return OnProviderChanged()"></asp:DropDownList>
								<asp:customvalidator id="m_vldProvider" runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="Provider validator"></asp:customvalidator>
							</asp:TableCell>
						</asp:TableRow>
						<asp:TableRow id="m_rowLowIncome" runat="server">
							<asp:TableCell class="question">
								<asp:label id="m_lblDoYouQualify" runat="server" ENABLEVIEWSTATE="False">Do you qualify for low income assistance?</asp:label>
							</asp:TableCell>
							<asp:TableCell class="answer">
								<asp:radiobutton id="m_rbnLowIncomeYes" runat="server" GroupName="LowIncomeGroup" Text="Yes"></asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:radiobutton id="m_rbnLowIncomeNo" runat="server" GroupName="LowIncomeGroup" Text="No"></asp:radiobutton>
                             </asp:TableCell>
						</asp:TableRow>
						<asp:TableRow id="m_rowLowIncomeLink" runat="server">
							<asp:TableCell class="question">
								<asp:label id="m_lblLowIncomeLink" runat="server" ENABLEVIEWSTATE="False">For Lifeline/Link-Up qualification and required documentation please <a href="javascript:OpenLifeLineApplication()">Click Here: LifeLine Application</a></asp:label>
							</asp:TableCell>
							<asp:TableCell class="answer">								
							</asp:TableCell>
						</asp:TableRow>
						<TR class="separator_row">
							<TD colspan="2"></TD>
						</TR>
						<tr>
							<td colspan="2" align="right">
								<asp:imagebutton id="m_btnNext" runat="server" ImageUrl="../images/btn_proceed.gif"></asp:imagebutton>
							</td>
						</tr>
					</tbody>
					<INPUT id="m_hdnProviderValues" type="hidden" runat="server"> <INPUT id="m_hdnProviderTexts" type="hidden" runat="server">
					<INPUT id="m_hdnError" type="hidden" name="Hidden2" runat="server"> <INPUT id="m_hdnProviderSelectedIndex" type="hidden" name="Hidden1" runat="server">
					<INPUT id="m_hdnIsShowWirelessString" type="hidden" runat="server"> <INPUT id="m_hdnIsLowIncomeRowVisible" type="hidden" runat="server">
				</table>
			</div>
		</form>
	</body>
</HTML>
