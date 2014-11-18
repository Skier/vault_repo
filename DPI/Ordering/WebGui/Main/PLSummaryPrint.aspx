<%@ Page language="c#" Codebehind="PLSummaryPrint.aspx.cs" AutoEventWireup="false"  Inherits="DPI.Ordering.PLSummaryPrint" %>
<%@ Register TagPrefix="dPiUser" TagName="SideControl" Src="control/SideControl2.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteFooter" Src="control/SiteFooter.ascx" %>
<%@ Register TagPrefix="dPiUser" TagName="SiteHeader" Src="control/SiteHeader.ascx" %>
<%@ Register TagPrefix="subhdr" TagName="subheader" Src="control/subheader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>dPi Web Ordering - Order Summary Print</title>
<LINK href="Styles/Navigator.css" rel=stylesheet ><LINK href="Styles/DPI.css" rel=stylesheet >
  </HEAD>
<body text=#000000 bgColor=#ffffff leftMargin=0 topMargin=0 
ms_positioning="GridLayout">
<TABLE height=513 cellSpacing=0 cellPadding=0 width=236 border=0 
ms_2d_layout="TRUE">
  <TR vAlign=top>
    <TD width=236 height=513>
      <form id=Form1 action=post runat="server">
      <TABLE height=234 cellSpacing=0 cellPadding=0 width=511 border=0 
      ms_2d_layout="TRUE">
        <TR vAlign=top>
          <TD width=511 height=234>
            <table height=233 cellSpacing=0 cellPadding=0 width=510 align=left 
            border=0>
              <tr>
                <td vAlign=top align=center bgColor=white colSpan=2 
                >
                  <table cellSpacing=0 cellPadding=0 width=510 border=0 
                  >
                    <tr>
                      <td align=center colSpan=6><asp:image id=Image1 runat="server" ImageAlign="Middle" ImageUrl="images/printheader.jpg"></asp:image></TD></TR>
                    <tr>
                      <td rowSpan=9>&nbsp;</TD>
                      <td class=05_con_sublabel_zip vAlign=middle align=right 
                      bgColor=white colSpan=4 height=61 
                        >ZipCode: <asp:label id=lblZipCode runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp; 
<asp:label id=lblIlec runat="server" BackColor="White"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                      </TD>
                      <td rowSpan=10>&nbsp;</TD></TR>
                    <tr>
                      <td align=center colSpan=6><asp:label id=lblDate runat="server" Width="205px"></asp:label></TD></TR>
                    <tr>
                      <td align=center colSpan=4 
                        ><asp:placeholder 
                        id=phldrOrdrDetails 
                        runat="server"></asp:placeholder></TD></TR>
                    <tr>
                      <td align=left width=121 bgColor=#ffffff colSpan=3 
                      height=1>&nbsp;</TD>
                      <td align=right bgColor=#ffffff height=1 
                      >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                      </TD></TR>
                    <TR>
                      <td align=center width=637 colSpan=4>
															<!------------------------------------------------->
                        <TABLE cellSpacing=0 cellPadding=0 width="97%" border=0 
                        >
                          <TR bgColor=aliceblue>
                            <TD align=left width=273 colSpan=1 rowSpan=1 
                            >Order Total</TD>
                            <TD align=right width=127 height=9 
                            ><asp:label id=lblOrderTotal runat="server" Width="72px"></asp:label>&nbsp;</TD>
                            <td align=right width=125 height=9 
                            >----&nbsp;&nbsp;&nbsp; &nbsp; 
                            </TD></TR>
                          <tr>
                            <td vAlign=top colSpan=3><IMG height=1 src="images/pixel_gray.jpg" width="100%" border=0 ></TD></TR>
                          <TR bgColor=floralwhite>
                            <TD align=left width=273 height=2 
                            >Taxes, Fees and Surcharges</TD>
                            <TD align=right width=127 height=2 
                            ><asp:label id=lblFees runat="server" Width="72px"></asp:label>&nbsp;</TD>
                            <td align=right colSpan=1 height=2 rowSpan=1 
                            >&nbsp;----&nbsp;&nbsp; 
                            &nbsp;&nbsp;</TD></TR>
                          <tr>
                            <td vAlign=top colSpan=3><IMG height=1 src="images/pixel_gray.jpg" width="100%" border=0 ></TD></TR>
                          <TR bgColor=aliceblue>
                            <TD align=left width=273><asp:label id=Label2 runat="server" Width="216px" Font-Bold="True" Font-Names="Arial" ForeColor="#C04000" Font-Size="Medium">Total Amount Due</asp:label></TD>
                            <TD align=right width=127><asp:label id=lblAmountDue runat="server" Width="112px" Font-Bold="True" Font-Names="Arial" ForeColor="Gray" Font-Size="Medium"></asp:label>&nbsp;</TD>
                            <td align=right 
                              >&nbsp;----&nbsp;&nbsp;&nbsp; 
                            &nbsp;</TD></TR>
                          <tr>
                            <td vAlign=top colSpan=3><IMG height=1 src="images/pixel_gray.jpg" width="100%" border=0 ></TD></TR></TABLE>
															<!------------------------------------------------->
						</TD>
					</TR>
                    <tr>
                      <td align=center colSpan=6><asp:imagebutton id=btnPrint runat="server" ImageUrl="images/btn_print2.jpg" CausesValidation="False"></asp:imagebutton></TD></TR></TABLE>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:button id=btnClose runat="server" Text="Close"></asp:button></TD></TR></TABLE></TD></TR></TABLE></FORM></TD></TR></TABLE>
	</body>
</HTML>
