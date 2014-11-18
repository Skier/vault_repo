<%@ Control Language="c#" AutoEventWireup="false" Codebehind="service_plan_description_viewer.ascx.cs" Inherits="Dpi.Central.Web.Account.Wireless.ServicePlanDescriptionViewer" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE cellSpacing="1" cellPadding="0" border="0" width="100%" class="table_form">
    <TR>
        <TD colspan="2" class="title">
            Account Balance
        </TD>
    </TR>
    <TR>
        <TD width="20%">Status</TD>
        <TD width="30%">
            <asp:Label id="lblPlanStatus" runat="server" CssClass="value">Label</asp:Label></TD>
    </TR>
    <TR>
        <TD width="20%">Anytime Used Mins</TD>
        <TD width="30%">
            <asp:Label id="lblAnytimeUsedMins" runat="server" CssClass="value">Label</asp:Label></TD>
    </TR>
    <TR>
        <TD>Start Date</TD>
        <TD>
            <asp:Label id="lblStartDate" runat="server" CssClass="value">Label</asp:Label></TD>
    </TR>
    <TR>
        <TD>Expiration Date</TD>
        <TD>
            <asp:Label id="lblExpirationDate" runat="server" CssClass="value">Label</asp:Label></TD>
    </TR>
    <TR>
        <TD>Cash Balance</TD>
        <TD>
            <asp:Label id="lblCashBalance" runat="server" CssClass="value">Label</asp:Label></TD>
    </TR>
</TABLE>
