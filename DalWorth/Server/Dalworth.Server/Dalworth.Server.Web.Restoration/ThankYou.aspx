<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThankYou.aspx.cs" Inherits="Dalworth.Server.Web.Restoration.ThankYou" %>
<%@ Register TagPrefix="dalworth" TagName="Head" Src="~/Head.ascx" %>
<%@ Register TagPrefix="dalworth" TagName="Header" Src="~/HeaderControl.ascx" %>
<%@ Register TagPrefix="dalworth" TagName="Footer" Src="~/FooterControl.ascx" %>
<%@ Register TagPrefix="dalworth" TagName="LatestArticles" Src="~/LatestArticlesControl.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<dalworth:Head ID="m_head" runat="server"/>

<body>
<div class="wrapper">
  
  <dalworth:Header ID ="m_header" runat="server" />
  
  <div id="content_wrapper">
    <div id="content">
    
      <div class="colum-big left">
      
         <div class="article thankyou">
         <h1>Thank You!</h1>
         
         <p>You request <strong><asp:Literal ID="m_txtLeadId" runat="server" /></strong> was received. 
          <br />
          A copy was sent to <strong><asp:Literal ID="m_txtEmail" runat="server" /></strong><br />
          We will contact you as soon as possible.</p>
          
         <div class="row result">
          <label>First Name:</label>
          <asp:Label ID="m_txtFirstName" runat="server" />
         </div>
         <div class="row result">
          <label>Last Name:</label>
          <asp:Label ID="m_txtLastName" runat="server" />
         </div>
         <div class="row result">
          <label>Phone:</label>
          <asp:Label ID="m_txtPhone" runat="server" />
         </div>
         <div class="row result">
          <label>E-mail:</label>
          <asp:Label ID="m_txtEmailAddress" runat="server" />
         </div>
         <div class="row result">
          <label>Comment:</label>
          <textarea id = "m_txtMessageText" runat="server" disabled="disabled"/></textarea>
         </div>
        </div>
      </div>
      
      <!--colum_big-->
      <div class="colum-small right">
        <!--forma-big-->
       
        <dalworth:LatestArticles ID="m_latestArticles" runat="server" />
      </div>
      <!--colum_small-->
      <div class="clear">&nbsp;</div>
    </div>
    <!--content-->
  </div>
  <div class="clear">&nbsp;</div>
  
  <dalworth:Footer ID="m_footer" runat="server" />
</div>
</body>
</html>
