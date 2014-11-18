<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteMapPage.aspx.cs" Inherits="Dalworth.Server.Web.Restoration.SiteMapPage" %>

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
      <div id="content_head">
      <div id="breadcrumb">
        <asp:Literal id="m_breadCrum" runat="server" />
      </div>
      <div id="bookmark">
        <a class="addthis_button" href="http://www.addthis.com/bookmark.php?v=250&amp;pub=xa-4ac490e71dad3c02"><img src="http://s7.addthis.com/static/btn/v2/lg-bookmark-en.gif" width="125" height="16" alt="Bookmark and Share" style="border:0"/></a>
        <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js?pub=xa-4ac490e71dad3c02"></script>
        <!-- AddThis Button END -->
      </div>
      </div>
      <div class="colum-big left">
      
          <div class="article map-page">
            
            <h1> Dalworth Restoration Sitemap</h1>
            
            <asp:Literal ID="m_txtSiteMap" runat="server" />
            
            </div>
           
      </div>
      
      <!--colum_big-->
      <div class="colum-small right">
        <div class="forma-small formadiv">
          <div class="forma-top">
            <h1>Call 1 800 326 7193</h1>
          </div>
          <div class="forma-content">
          <div class="formslogans">
            <h4 class="center">Submit Your information <br/>
              for immediate Response</h4>
          </div>
            <form id="m_submitForm" runat="server">
            
               <asp:Label id="m_lblErrorMessage" runat="server"  style="display:block;" Visible="false" />
                   
               <div class="row">
                <label>First Name:</label>
                <input id="m_txtFirstName"  runat="server" name="txtFirstName" type="text"  maxlength="40" tabindex="1" size="20" value="" />
                <asp:Label ID="m_lblErrorFirstName" runat="server" Visible = "false"/>
              </div>
              
              <div class="row">
                <label>Last Name:</label>
                <input id="m_txtLastName" runat="server" name="txtLastName" type="text" tabindex="2" size="20"  value="" />
                <asp:Label ID="m_lblErrorLastName" runat="server" style="color:Red;" Visible = "false"></asp:Label>
              </div>
              
              <div class="row">
                <label>Phone:</label>
                <input id="m_txtPhone1" runat="server" name="txtPhone1" type="text" maxlength="14"   tabindex="3" size="20" value="" />
                <asp:Label ID="m_lblErrorPhone1" runat="server" style="color:Red;" Visible = "false"></asp:Label>
              </div>
              
             <div class="row">
                <label>E-mail:</label>
                <input  id="m_txtEmail" runat="server" name="txtEmail" type="text" maxlength="50" tabindex="4" size="20" value="" />
                <asp:Label ID="m_lblErrorEmail" runat="server" style="color:Red;" Visible="false"></asp:Label>
              </div>
              
               <div class="row">
                <label class="optional">Comment:</label>
                <textarea  id="m_txtCustomerNotes" runat="server" name="CustomerNotes"  cols="15" rows="3" tabindex="5"></textarea>
              </div>
             
              <div class="row submit">
                <input id="m_btn_Submit" type="submit" runat="server" tabindex="6" name="" value="Submit" />
                <h5><b>Free</b> Inspection for all services!</h5>
              </div>
            </form>
            
          </div>
        </div>
        <!--forma-big-->
        <div class="support_image"><img id="m_img_support" runat="server" /></div>
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
