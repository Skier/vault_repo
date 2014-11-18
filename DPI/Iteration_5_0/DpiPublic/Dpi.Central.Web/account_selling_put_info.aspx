<%@ Page language="c#" Codebehind="account_selling_put_info.aspx.cs" AutoEventWireup="false" Inherits="Dpi.Central.Web.AccountSellingPutInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>dPi Teleconnect LLC • Please fill your personal information</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="DPI.css" type="text/css" rel="stylesheet">
		<script language="JavaScript">
  function checkValues() {
  
    if (document.getElementById("m_txtName").value == "") {
        alert("Please enter your Name.");
        document.getElementById("m_txtName").focus();
        return false;    
      }
    if (document.getElementById("m_txtCompany").value == "") {
        alert("Please enter your Company name.");
        document.getElementById("m_txtCompany").focus();
        return false;    
      }
    if (document.getElementById("m_txtAddress").value == "") {
        alert("Please enter your Address.");
        document.getElementById("m_txtAddress").focus();
        return false;    
      }
    if (document.getElementById("m_txtMobilePhone").value == "") {
        alert("Please enter your Mobile Phone.");
        document.getElementById("m_txtMobilePhone").focus();
        return false;    
      }
    if (document.getElementById("m_txtOfficePhone").value == "") {
        alert("Please enter your Office Phone.");
        document.getElementById("m_txtOfficePhone").focus();
        return false;    
      }
    if (document.getElementById("m_txtEmail").value == "") {
        alert("Please enter your Email.");
        document.getElementById("m_txtEmail").focus();
        return false;    
      }
          
    document.accountSellingForm.submit(); 
    
    }
		</script>
	</HEAD>
	<body>
		<form id="accountSellingForm" method="post" runat="server">
			<TABLE class="layout_table">
				<TR class="separator_row">
					<TD colSpan="3">
						<asp:label id="m_lblCaption" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Small"
							ForeColor="Chocolate">Please fill the fields below</asp:label></TD>
				</TR>
				<TR class="separator_row">
					<TD colSpan="3">
					</TD>
				</TR>
				<TR>
					<TD class="property_name">
						<asp:label id="m_lblName" runat="server">Name</asp:label><FONT color="#db6c1d">*</FONT></TD>
					<TD class="property_value" style="WIDTH: 252px">
						<asp:textbox id="m_txtName" runat="server" Width="100%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="property_name">
						<asp:label id="m_lblCompany" runat="server"> Company</asp:label><FONT color="#db6c1d">*</FONT></TD>
					<TD class="property_value" style="WIDTH: 252px">
						<asp:textbox id="m_txtCompany" runat="server" Width="100%"></asp:textbox><FONT color="#db6c1d"></FONT></TD>
				</TR>
				<TR>
					<TD class="property_name">
						<asp:label id="m_lblAddress" runat="server">Address</asp:label><FONT color="#db6c1d">*</FONT></TD>
					<TD class="property_value" style="WIDTH: 252px">
						<asp:textbox id="m_txtAddress" runat="server" Width="100%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="property_name">
						<asp:label id="m_lblMobilePhone" runat="server">Mobile phone</asp:label><FONT color="#db6c1d">*</FONT></TD>
					<TD class="property_value" style="WIDTH: 252px">
						<asp:textbox id="m_txtMobilePhone" runat="server" Width="100%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD class="property_name">
						<asp:label id="m_lblOfficePhone" runat="server">Office phone</asp:label><FONT color="#db6c1d">*</FONT></TD>
					<TD class="property_value" style="WIDTH: 252px">
						<asp:textbox id="m_txtOfficePhone" runat="server" Width="100%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD>
						<asp:label id="m_lblEmail" runat="server"> Email</asp:label><FONT color="#db6c1d">*</FONT></TD>
					<TD class="property_value" style="WIDTH: 252px">
						<asp:textbox id="m_txtEmail" runat="server" Width="100%"></asp:textbox></TD>
				</TR>
				<TR>
					<TD></TD>
					<TD align="right" style="WIDTH: 252px"><asp:imagebutton id="m_btnSubmit" runat="server" ImageUrl="images/submit.jpg"></asp:imagebutton></TD>
					<TD></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
