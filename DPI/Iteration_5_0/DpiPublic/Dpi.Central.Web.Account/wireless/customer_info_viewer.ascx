<%@ Control Language="c#" AutoEventWireup="false" Codebehind="customer_info_viewer.ascx.cs" Inherits="Dpi.Central.Web.Account.Wireless.CustomerInfoViewer" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE class="table_form" cellSpacing="1" cellPadding="0" width="100%" border="0">
	<TR>
		<TD class="title" colSpan="4">Customer Information
		</TD>
	</TR>
	<TR>
		<TD width="24%">First Name</TD>
		<TD width="28%"><asp:label id="lblFirstName" CssClass="value" runat="server">Label</asp:label></TD>
		<TD width="19%">Address 1</TD>
		<TD width="29%"><asp:label id="lblAddress1" CssClass="value" runat="server">Label</asp:label></TD>
	</TR>
	<TR>
		<TD>Last Name</TD>
		<TD><asp:label id="lblLastName" CssClass="value" runat="server">Label</asp:label></TD>
		<TD>Address 2</TD>
		<TD><asp:label id="lblAddress2" CssClass="value" runat="server">Label</asp:label></TD>
	</TR>
	<TR>
		<TD>Contact Number</TD>
		<TD><asp:label id="lblContactNumber" CssClass="value" runat="server">Label</asp:label></TD>
		<TD>City</TD>
		<TD><asp:label id="lblCity" CssClass="value" runat="server">Label</asp:label></TD>
	</TR>
	<TR>
		<TD>Email</TD>
		<TD><asp:label id="lblEmail" CssClass="value" runat="server">Label</asp:label></TD>
		<TD>State</TD>
		<TD><asp:label id="lblState" CssClass="value" runat="server">Label</asp:label></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD></TD>
		<TD>Zip Code</TD>
		<TD><asp:label id="lblZipCode" CssClass="value" runat="server">Label</asp:label></TD>
	</TR>
</TABLE>
