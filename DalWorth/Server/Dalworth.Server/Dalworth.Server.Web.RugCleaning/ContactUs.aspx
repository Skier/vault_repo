<%@ Page Language="C#" MasterPageFile="~/RugCleaningMasterPage.master" AutoEventWireup="true" CodeBehind="ContactUs.aspx.cs" Inherits="Dalworth.Server.Web.RugCleaning.ContactUs" %>
<%@ MasterType VirtualPath="~/RugCleaningMasterPage.master" %>
<%@ Import Namespace="Dalworth.Server.Web.RugCleaning" %>

<asp:Content ID="title" ContentPlaceHolderID="titlePlaceHolder" Runat="Server"><title>Contact Information, Dalworth Area Rug Cleaning, Dallas Texas</title>
</asp:Content>

<asp:Content ID="description" ContentPlaceHolderID="descriptionPlaceHolder" Runat="Server">
    <meta name="description" content="Contact Dalworth for your Oriental Rug Cleaning and get Free Quote Today!. Call: <%Response.Write(Master.PhoneNumber);%>"/>
</asp:Content>

<asp:Content ID="keywords" ContentPlaceHolderID="keywordsPlaceHolder" Runat="Server">
    <meta name="keywords" content="dalworth rug cleaning, driving directions, phone number"/>
</asp:Content>

<asp:Content ID="article" ContentPlaceHolderID="articlePlaceholder" Runat="Server">
 <div class="content_col">
     <h1>Contact Dalworth Rug Cleaning</h1>   
      <h4 class="center">Call:&nbsp;<span style="font-size:1.8em"><%Response.Write(Master.PhoneNumber);%></span></h4>
     <div class="article">
        <p> Drop your rug off and pick it up, receive a
        <strong>10% discount.</strong> </p>
        
        <h4 class="addres center">Dalworth Rug Cleaning<br />
          12750 South Pipeline Road, <br />
          Euless, Texas 76040</h4>
     </div> 
     <div class="border" style="margin:2em 0;">
        <iframe width="598" height="333" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="http://maps.google.com/maps?f=q&amp;source=s_q&amp;hl=en&amp;q=12750+S+Pipeline+Rd,+Euless,+Tarrant,+Texas+76040&amp;ie=UTF8&amp;cd=1&amp;geocode=FaXR9AEdv402-g&amp;split=0&amp;sll=37.0625,-95.677068&amp;sspn=23.875,57.630033&amp;hq=&amp;hnear=12750+S+Pipeline+Rd,+Euless,+Tarrant,+Texas+76040&amp;ll=32.825221,-97.087941&amp;spn=0.012009,0.025449&amp;z=15&amp;iwloc=A&amp;output=embed"></iframe>
    </div>
 </div>
</asp:Content>