<%@ Page Language="C#" MasterPageFile="~/RugCleaningMasterPage.master" AutoEventWireup="true" CodeBehind="FeedbackReceipt.aspx.cs" Inherits="Dalworth.Server.Web.RugCleaning.FeedbackReceipt" %>
<%@ MasterType VirtualPath="~/RugCleaningMasterPage.master" %>
<%@ Import Namespace="Dalworth.Server.Web.RugCleaning" %>

<asp:Content ID="title" ContentPlaceHolderID="titlePlaceHolder" Runat="Server"><title>Thank you for your feedback</title>
</asp:Content>

<asp:Content ID="description" ContentPlaceHolderID="descriptionPlaceHolder" Runat="Server">
    <meta name="description" content=""/>
</asp:Content>

<asp:Content ID="keywords" ContentPlaceHolderID="keywordsPlaceHolder" Runat="Server">
    <meta name="keywords" content=""/>
</asp:Content>

<asp:Content ID="article" ContentPlaceHolderID="articlePlaceholder" Runat="Server">
    <div class="form_s formemul">
        <h3>Dear <strong><em><%Response.Write(FirstName);%></em></strong>, Thank you for your feedback</h3>
        
         <div id="m_divShowReviews" runat="server" visible="false">
            <h4 class="padbefore">Please tell your friends about us <br> by posting your comments to the following sites:</h4>
         
            <div class="center marklinks padbefore"> 
            <a class="facebooka" href="http://www.facebook.com/pages/Euless-TX/Dalworth-Rug-Cleaning/86919179537?v=wall">Facebook</a> 
            <a class="yelpa" href="http://www.yelp.com/biz/dalworth-rug-cleaning-euless">Yelp</a> 
            <a class="googla" href="http://maps.google.com/places/us/euless/s-pipeline-rd/12750/-oriental-rug-cleaning">Maps.google.com</a> 
            <!--<a class="yahooa" href="#">Local.yahoo.com</a>  -->
            </div>
         </div>
         
         <div id="m_divSaySorry" runat="server" visible="false"  class="standart">
         
            <p>
                Thank you so much for taking the time to complete our survey. 
                We really appreciate your honesty and while it is not always easy to give constructive criticism, 
                it can be the most helpful kind of feedback we receive.
            </p>
            <p>
                Your satisfaction with our services is very important to us as we strive to provide the most thorough cleaning 
                and repair process along with the smoothest and most courteous customer care assistance.
            </p>
            <p>
                We recognize that you rated our service as <strong>“<%Response.Write(Rating);%>”</strong> and we will contact you as soon as possible to 
                determine what we can do to increase your satisfaction with our service.  
            </p>
            <p>
                You are welcome to call us at <strong><%Response.Write(Master.PhoneNumber);%></strong>
            </p>
            
            <p>
                Sincerely,
            </p>
            <p>
                Courtney Hobbs <br />
                Manager, Customer Solutions 
            </p>
             
         </div>
        
        <h4 class="separator">Details of feedback:</h4>
         
         <div class="row result half">
            <label style="text-align:left;">Rate our services:</label>
            <span><%Response.Write(Rating);%></span> 
         </div>
            
         <div class="row result half">
            <label style="text-align:left;">When remind about next cleaning?</label>
            <span><%Response.Write(RemiderPeriod); %></span> 
         </div>
              
        <div class="row result half">
            <label style="text-align:left">Comment:</label>
            <span class="commentar" style="clear:both; text-align: justify; text-justify: newspaper">
            <%Response.Write(CustomerNotes);%>
            </span>
        </div> 
    </div>
</asp:Content>
