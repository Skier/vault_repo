<%@ Control Language="c#" AutoEventWireup="false" Codebehind="feature_usage_viewer.ascx.cs" Inherits="Dpi.Central.Web.Account.Wireless.FeatureUsageViewer" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE cellSpacing="1" cellPadding="0" border="0" width="100%" class="table_form">
	<TR>
		<TD colspan="2" class="title">
			Feature Usage
		</TD>
	</TR>
	<TR>
		<TD width="50%">Anytime Used Mins</TD>
		<TD width="50%">
			<asp:Label id="lblMuAnyTime" runat="server" CssClass="value">Label</asp:Label></TD>
	</TR>
	<TR>
		<TD>Night/Weekend Used Mins</TD>
		<TD>
			<asp:Label id="lblMuNw" runat="server" CssClass="value">Label</asp:Label></TD>
	</TR>
	<TR>
		<TD>Web Used Mins</TD>
		<TD>
			<asp:Label id="lblMuWeb" runat="server" CssClass="value">Label</asp:Label></TD>
	</TR>
	<TR>
		<TD>Text Used Mins</TD>
		<TD>
			<asp:Label id="lblMuText" runat="server" CssClass="value">Label</asp:Label></TD>
	</TR>
	<TR>
		<TD>3G Web Used Mins</TD>
		<TD>
			<asp:Label id="lblMu3gWeb" runat="server" CssClass="value">Label</asp:Label></TD>
	</TR>
	<TR>
		<TD>3G Pic Used Mins</TD>
		<TD>
			<asp:Label id="lblMu3gPic" runat="server" CssClass="value">Label</asp:Label></TD>
	</TR>
	<TR>
		<TD>3G PTT Used Mins</TD>
		<TD>
			<asp:Label id="lblMu3gTalk" runat="server" CssClass="value">Label</asp:Label></TD>
	</TR>
</TABLE>
