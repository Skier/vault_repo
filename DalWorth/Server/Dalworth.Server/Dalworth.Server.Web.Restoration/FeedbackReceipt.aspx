<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FeedbackReceipt.aspx.cs" Inherits="Dalworth.Server.Web.Restoration.FeedbackReceipt" %><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
      </div>
      <div class="colum-big left">
        <div class="article feedback_response" id="m_divPositiveFeedback" runat="server">
          <h2 class="center">Thank you for your feedback!</h2>
          <div class="block" id="m_divReferalSites" runat="server">
            <h3 class="center">Please tell your friends about us<br>by posting your comments to the following sites:</h3>
            <div class="services">
              <ul class="center">
                <li><a href="http://www.insiderpages.com/b/15250707459/dalworth-restoration-euless">InsiderPages.com</a></li>
                <li><a href="http://www.yelp.com/biz/dalworth-restoration-euless">Yelp.com</a></li>
                <li><a href="http://maps.google.com/maps/place?hl=en&georestrict=input_srcid:4deb42fdc2cfddf0">Maps.google.com</a></li>
                <li><a href="http://national.citysearch.com/profile/603783572/euless_tx/dalworth_restoration_water_damage_dallas.html#done"> citisearch.com</a></li>
                <li><a href="http://www.facebook.com/pages/Dalworth-Restoration/116336731729069?v=app_6261817190#!/pages/Dalworth-Restoration/116336731729069?v=app_6261817190">Facebook.com</a></li>
              </ul>
            </div>
          </div>
          <div class="block">
            <h3 class="center">Details of feedback:</h3>
            <div class="information">
              <div class="row"><label>Name:</label> <b><asp:Literal ID="m_txtName" runat="server" /></b></div>
              <div class="row"><label>Rate our services:</label> <b><asp:Literal ID="m_txtServiceRate" runat="server" /></asp:Literal></b></div>
              <div class="row"><label>Comment:</label><br><b><asp:Literal ID="m_txtComment" runat="server" /></b></div>
            </div>
          </div>

        </div>
        
         <div class="article feedback_response" id="m_divNegativeFeedback" runat="server">
          <h2 class="center">Thank you for your feedback!</h2>
          <div class="block center">
            <p>Thank you so much for taking the time to complete our survey. We really appreciate your honesty and while it is not always easy to give constructive criticism, it can be the most helpful kind of feedback we receive.</p>
            <p>Your satisfaction with our services is very important to us as we strive to provide the most thorough cleaning and repair process along with the smoothest and most courteous customer care assistance.</p>
            <p>We recognize that you rated our service as <b>“<asp:Literal ID="m_txtServiceRate1" runat="server" />"</b> and we will contact you as soon as possible to determine what we can do to increase your satisfaction with our service.</p>
            <p>You are welcome to call us at <b>1 800 459 2563</b></p>
            <p>Sincerely,</p>
            <p>Courtney Hobbs<br>Manager, Customer Solutions</p>
          </div>
           <div class="block">
            <h3 class="center">Details of feedback:</h3>
            <div class="information">
              <div class="row"><label>Name:</label> <b><asp:Literal ID="m_txtName1" runat="server" /></b></div>
              <div class="row"><label>Rate our services:</label> <b><asp:Literal ID="m_txtServiceRate2" runat="server" /></asp:Literal></b></div>
              <div class="row"><label>Comment:</label><br><b><asp:Literal ID="m_txtComment1" runat="server" /></b></div>
            </div>
          </div>
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

<dalworth:Footer ID="m_footer" runat="server" />  
</div> <!-- wrapper -->

</body>

</html>
