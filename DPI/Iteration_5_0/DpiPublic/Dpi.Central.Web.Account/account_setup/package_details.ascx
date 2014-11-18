<%@ Control Language="c#" AutoEventWireup="false" Codebehind="package_details.ascx.cs" Inherits="Dpi.Central.Web.Account.AccountSetup.PackageDetails" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="m_tblMain" cellSpacing="0" cellPadding="0" width="384" height="100%" border="0"
	runat="server">
	<TR>
		<TD class="package_box_tl"><IMG height="1" src="../images/spacer01.gif" width="11" border="0"></TD>
		<TD class="package_box_top"><IMG height="1" src="../images/spacer01.gif" width="360" border="0"></TD>
		<TD class="package_box_tr"><IMG height="1" src="../images/spacer01.gif" width="11" border="0"></TD>
	</TR>
	<TR vAlign="top">
		<TD class="package_box" colSpan="3" height="100%">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD nowrap>
						<asp:label id="m_lblPackageName" runat="server" CssClass="package_name">Package Name</asp:label>
					</TD>
					<td align="right">
						<asp:imagebutton id="m_btnSelect" runat="server" ImageUrl="../images/btn_order_now.gif"></asp:imagebutton>
					</td>
				</TR>
			</TABLE>
			<p class="package_text">
				<asp:label id="m_lblPrice" runat="server" CssClass="package_price">$39.50</asp:label>
				<span class="package_price">*</span>&nbsp;per month<br>
				<span class="package_price_note" runat="server" id="spnPpdNote">(Monthly price 
					includes prompt pay discount. <a href="javascript:void(0);" onclick="javascript:window.open('ppd_explanation.aspx',null,'height= 400, width=500, toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no,resizable=yes');return false;"
						target="_blank">Learn More</a><img src="../images/link_onw.gif">) </span>
			</p>
			<p class="package_text">Package Includes:</p>
			<asp:table id="m_tblFeatures" runat="server" CELLSPACING="0" CELLPADDING="0"></asp:table>
		</TD>
	</TR>
	<TR>
		<TD class="package_box_bl"><IMG height="1" src="../images/spacer01.gif" width="11" border="0"></TD>
		<TD class="package_box_bottom"><IMG height="1" src="../images/spacer01.gif" width="360" border="0"></TD>
		<TD class="package_box_br"><IMG height="1" src="../images/spacer01.gif" width="11" border="0"></TD>
	</TR>
</TABLE>
