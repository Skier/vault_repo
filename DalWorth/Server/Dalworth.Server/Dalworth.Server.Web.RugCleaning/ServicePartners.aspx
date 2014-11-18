<%@ Page Language="C#" MasterPageFile="~/RugCleaningMasterPage.master" AutoEventWireup="true" CodeBehind="ServicePartners.aspx.cs" Inherits="Dalworth.Server.Web.RugCleaning.ServicePartners" %>
<%@ MasterType VirtualPath="~/RugCleaningMasterPage.master" %>
<%@ Import Namespace="Dalworth.Server.Web.RugCleaning" %>

<asp:Content ID="title" ContentPlaceHolderID="titlePlaceHolder" Runat="Server"><title>Dalworth referral program.  Add area rug cleaning to your list of services.  | Dallas Fort Worth</title>
</asp:Content>

<asp:Content ID="description" ContentPlaceHolderID="descriptionPlaceHolder" Runat="Server">
    <meta name="description" content="Dalworth referral program. Refer a customer and get paid for the job order once it is complete. Dalworth <%Response.Write(Master.PhoneNumber);%>"/>
</asp:Content>

<asp:Content ID="keywords" ContentPlaceHolderID="keywordsPlaceHolder" Runat="Server">
    <meta name="keywords" content="service referrals , commission, referral fee,  maid service, house cleaning service, 
    oriental rug retailer, oriental rug wholesaler, veterinarian, plumber, electrician, carpenter"/>
</asp:Content>

<asp:Content ID="article" ContentPlaceHolderID="articlePlaceholder" Runat="Server">
<div class="article">
        <h1>Dalworth Service Referral Program</h1>
        
        <p> 
            If you are a maid service, house cleaning service, Oriental rug retailer / wholesaler, 
            veterinarian, plumber, electrician, carpenter or any other service provider 
            and you know your clients or customers would benefit from our professional rug cleaning services, 
            please give us a call or email us. 
            We'll be happy to discuss with you details about our referral program.   
        </p>
        <p>
           We have set up a detailed database application system that tracks orders we receive 
           from each of our service partners. At the end of the month, we send you a check and 
           a full report with the list of referrals you have provided, the actual resulting job orders, 
           the dollar amount of each job and your commission.
        </p>
        
        <h2>Call our Service Partner line at <%Response.Write(Master.PhoneNumber);%> </h2>
        
</div>
</asp:Content>