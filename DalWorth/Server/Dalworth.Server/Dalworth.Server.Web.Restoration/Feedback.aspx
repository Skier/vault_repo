<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="Dalworth.Server.Web.Restoration.Feedback" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="dalworth" TagName="Head" Src="~/Head.ascx" %>
<%@ Register TagPrefix="dalworth" TagName="Header" Src="~/HeaderControl.ascx" %>
<%@ Register TagPrefix="dalworth" TagName="Footer" Src="~/FooterControl.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">

<dalworth:Head ID="m_head" runat="server"/>

<body>
<div class="wrapper">
 
    <dalworth:Header ID ="m_header" runat="server" />
    
      <div id="content_wrapper">
    <div id="content">
      <div id="content_head">
      <div id="bookmark">
        <a class="addthis_button" href="http://www.addthis.com/bookmark.php?v=250&amp;pub=xa-4ac490e71dad3c02"><img src="http://s7.addthis.com/static/btn/v2/lg-bookmark-en.gif" width="125" height="16" alt="Bookmark and Share" style="border:0"/></a>
        <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js?pub=xa-4ac490e71dad3c02"></script>
        <!-- AddThis Button END -->
      </div>
      </div>
      <div class="colum-big left">
        <div class="article feedback">
        <form id="m_formFeedback" runat="server">
          <h2 class="center">Thank you for helping us improve our service<br>Your feedback is critical to us</h2>
          <div class="block">
            <h3 class="center">Your information:</h3>
            <div class="information">
              <div class="row"><label>First&nbsp;Name:</label> <b><asp:Literal ID="m_txtFirstName" runat="server"/></b></div>
              <div class="row"><label>Last&nbsp;Name:</label> <b><asp:Literal ID="m_txtLastName" runat="server" /></b></div>
              <div class="row"><label>Date&nbsp;of&nbsp;service:</label> <b><asp:Literal ID="m_txtCompletionDate" runat="server" /></b></div>
            </div>
          </div>
          <div class="block">
            <h3 class="center">Please rate our services:</h3>
            <div class="center">
               <select id="m_selServiceRate" runat="server" name="serviceRate">
                <option value="0" class="center" disabled="disabled" selected="selected"> Please select -</option>
                <option value="1">Excellent</option>
                <option value="2">Good</option>
                <option value="3">Acceptable</option>
                <option value="4">Needs improvement</option>
                <option value="5">Not satisfied</option>
              </select>
            </div>
          </div>
          
          <div class="block">
            <h3 class="center">Comment:</h3>
            <div class="center">
                <textarea id ="m_txtComment"  runat="server" name="comments" rows="6" cols="30"   style="width:310px;margin-right:20px;"></textarea>
            </div>
            <div class="center">
                <input type="submit" id ="m_btnSubmit" runat="server" name="btnSubmit" value="Submit"/>
            </div>
          </div>
        </form>
        </div>
      </div>
      <!--colum_big-->
      <div class="colum-small right">
     
        <div class="article">
          <h2>Our ServiceS</h2>
          <asp:Literal ID="m_txtServices" runat="server" />
        </div>
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
 