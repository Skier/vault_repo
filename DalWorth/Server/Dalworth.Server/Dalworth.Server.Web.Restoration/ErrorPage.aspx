<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="Dalworth.Server.Web.Restoration.ErrorPage" %>

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
      
         <div class="article">
         <h1>Page is NOT FOUND !</h1>
         
         <p>
         Opps!. We've got an error! Please let us know that you see this page.
         </p>
          
         
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
