<%@ Control Language="c#" AutoEventWireup="false" Codebehind="subscriber_info_viewer.ascx.cs" Inherits="Dpi.Central.Web.Account.Wireless.SubscriberInfoViewer" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE cellSpacing="1" cellPadding="0" border="0" width="100%" class="table_form">
	<TR>
		<TD colspan="2" class="title">
			Subscriber Information
		</TD>
	</TR>
	<TR>
		<TD width="50%">MDN</TD>
		<TD width="50%">
			<asp:Label id="lblMdn" runat="server" CssClass="value">Label</asp:Label></TD>
	</TR>
	<TR>
		<TD>ESN</TD>
		<TD>
			<asp:Label id="lblEsn" runat="server" CssClass="value">Label</asp:Label></TD>
	</TR>
	<TR>
		<TD>Customer Since</TD>
		<TD>
			<asp:Label id="lblCustomerSince" runat="server" CssClass="value">Label</asp:Label></TD>
	</TR>
</TABLE>
