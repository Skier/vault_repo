<%@ Register Namespace="Dpi.Central.Web.Controls" TagPrefix="dwc" Assembly="Dpi.Central.Web.Common" %>
<%@ Page language="c#" Codebehind="order_summary.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.Account.Wireless.Processes.Rwa.OrderSummaryPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Order Summary</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../DPI.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="orderSummaryForm" method="post" runat="server">
			<div class="process_form">
				<div class="step_caption" style="WHITE-SPACE: nowrap">
					<span style="WIDTH: 50%">Order Summary</span> <span style="WIDTH: 50%; MARGIN-RIGHT: 9px; TEXT-ALIGN: right">
						&nbsp; </span>
				</div>
				<TABLE class="process_table">
					<TR>
						<TD colSpan="2"><asp:placeholder id="phldrOrdrDetails" runat="server"></asp:placeholder></TD>
					</TR>
					<TR>
						<TD colSpan="2">
							<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD align="left" width="304" bgColor="gainsboro" height="19"><STRONG><FONT color="dimgray">&nbsp;&nbsp;&nbsp;Product 
												Total</FONT></STRONG></TD>
									<TD align="right" bgColor="gainsboro" height="19">
										<asp:label id="lblOrderTotal" runat="server" Width="72px">$185.00</asp:label>&nbsp;</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD colSpan="2">
							<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD align="left" width="304" bgColor="gainsboro" height="19"><STRONG><FONT color="dimgray">&nbsp;&nbsp;&nbsp;Taxes, 
												Fees and Surcharges</FONT></STRONG></TD>
									<TD align="right" bgColor="gainsboro" height="19">
										<asp:label id="lblFees" runat="server" Width="72px">$15.00</asp:label>&nbsp;</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD colSpan="2">
							<TABLE id="Table3" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD align="left" width="304" bgColor="gainsboro" height="19"><STRONG><FONT color="dimgray">&nbsp;&nbsp;&nbsp;Total 
												Amount Due</FONT></STRONG></TD>
									<TD align="right" bgColor="gainsboro" height="19">
										<asp:label id="lblAmountDue" runat="server" ForeColor="Red" Width="72px">$200.00</asp:label>&nbsp;</TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
					<TR>
						<TD colSpan="2">
						</TD>
					</TR>
					<TR>
						<TD><asp:imagebutton id="btnPrevious" runat="server" ImageUrl="~/images/btn_back.gif"></asp:imagebutton></TD>
						<TD align="right"><asp:imagebutton id="btnProceed" runat="server" ImageUrl="~/images/btn_proceed.gif"></asp:imagebutton></TD>
					</TR>
				</TABLE>
			</div>
		</form>
	</body>
</HTML>
