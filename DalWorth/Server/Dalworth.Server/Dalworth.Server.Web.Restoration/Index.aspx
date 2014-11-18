<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Index.aspx.cs" Inherits="Dalworth.Server.Web.Restoration.Index" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="dalworth" TagName="Head" Src="~/Head.ascx" %>
<%@ Register TagPrefix="dalworth" TagName="Header" Src="~/HeaderControl.ascx" %>
<%@ Register TagPrefix="dalworth" TagName="Footer" Src="~/FooterControl.ascx" %>
<%@ Register TagPrefix="dalworth" TagName="LatestArticles" Src="~/LatestArticlesControl.ascx" %>
<%@ Register TagPrefix="dalworth" TagName="NewsFlash" Src="~/NewsFlashControl.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">

<dalworth:Head ID="m_head" runat="server"/>

<body>
<div class="wrapper">
 
 <dalworth:Header ID ="m_header" runat="server" />
 
  <div id="content_wrapper">
    <div id="content" class="homepage">
      <div class="colum-big left">
        <dalworth:NewsFlash ID="m_newsFlash" runat="server" />
        <div class="forma-big formadiv">
          <div class="forma-top">
            <h1>24 HR EMERGENCY SERVICE<br/>1 800 326 7913</h1>
          </div>
          <div class="forma-content clear">
            <div class="right formslogans">
              <h3>BONDED <br /> LICENCED <br /> INSURED</h3>
              <div class="center">
                <a href="http://www.fortworth.bbb.org/codbrep.html?wlcl=y&id=76110001"><img src="img/bbb_logo.png" alt="bbb" /></a>
              </div>
              <h2>FREE QUOTE!</h2>
              <a id="m_lnkInsurance" runat="server" class="some">Direct Insurance Billing</a> 
            </div>
            <form  runat="server">
              <asp:Label id="m_lblErrorMessage" runat="server"  style="display:block;" Visible="false"/>
              <div class="row">
                <label><b>First Name:</b></label>
                <input id="m_txtFirstName"  runat="server" name="txtFirstName" type="text"  maxlength="40" tabindex="1" size="20" value="" />
                <asp:Label ID="m_lblErrorFirstName" runat="server" Visible = "false"/>
              </div>
              <div class="row">
                <label><b>Last Name:</b></label>
                <input id="m_txtLastName" runat="server" name="txtLastName" type="text" tabindex="2" size="20"  value="" />
                <asp:Label ID="m_lblErrorLastName" runat="server" style="color:Red;" Visible = "false"></asp:Label>
              </div>
              <div class="row">
                <label><b>Phone:</b></label>
                <input id="m_txtPhone1" runat="server" name="txtPhone1" type="text" maxlength="14"   tabindex="3" size="20" value="" />
                <asp:Label ID="m_lblErrorPhone1" runat="server" style="color:Red;" Visible = "false"></asp:Label>
              </div>
              <div class="row">
                <label><b>E-mail:</b></label>
                <input  id="m_txtEmail" runat="server" name="txtEmail" type="text" maxlength="50" tabindex="4" size="20" value="" />
                <asp:Label ID="m_lblErrorEmail" runat="server" style="color:Red;" Visible="false"></asp:Label>
              </div>
              <div class="row">
                <label class="optional">Comment:</label>
                <textarea  id="m_txtCustomerNotes" runat="server" name="CustomerNotes" rows="3" tabindex="5"></textarea>
              </div>
              <div class="row">
                <label class="optional">&nbsp; </label>
                <input id="m_btn_Submit" type="submit" runat="server" tabindex="6" name="" value="Submit" /><br />
                <p>
                    We will call you within <b>10</b> minutes <br />
                    <a id="m_lnkPrivacyPolicy" runat="server">Our commitment to your privacy</a>
                </p>
              </div>
            </form>
          </div>
        </div>
        <!--forma-big-->
        <div class="article">
            <asp:Literal ID="m_txtArticle" runat="server" />
        </div>
      </div>
      <!--colum_big-->
      <div class="colum-small right">

        <div class="article"><a class="addthis_button" href="http://www.addthis.com/bookmark.php?v=250&amp;pub=xa-4ac490e71dad3c02"><img src="http://s7.addthis.com/static/btn/v2/lg-bookmark-en.gif" width="125" height="16" alt="Bookmark and Share" style="border:0"/></a>
        <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js?pub=xa-4ac490e71dad3c02"></script>
        <!-- AddThis Button END -->
          <h2>Our Services</h2>
          <asp:Literal ID="m_txtServices" runat="server" />
        </div>
        <dalworth:LatestArticles ID="m_latestNews" runat="server" />
        <dalworth:LatestArticles ID="m_latestArticles" runat="server" />
      </div>
      <!--colum_small-->
      <div class="clear">&nbsp;</div>
    </div>
    <!--content-->
  </div>
  
  <dalworth:Footer ID="m_footer" runat="server" />
  
</div>
</body>
</html>