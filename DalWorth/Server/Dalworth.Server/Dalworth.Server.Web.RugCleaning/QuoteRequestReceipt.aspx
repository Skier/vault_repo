<%@ Page Language="C#" MasterPageFile="~/RugCleaningMasterPage.master"  AutoEventWireup="true" CodeBehind="QuoteRequestReceipt.aspx.cs" Inherits="Dalworth.Server.Web.RugCleaning.QuoteRequestReceipt" %>
<%@ MasterType VirtualPath="~/RugCleaningMasterPage.master" %>
<%@ Import Namespace="Dalworth.Server.Web.RugCleaning" %>

<asp:Content ID="title" ContentPlaceHolderID="titlePlaceHolder" Runat="Server">
<title>Dalworth Rug Cleaning Quote Receipt</title>
</asp:Content>

<asp:Content ID="description" ContentPlaceHolderID="descriptionPlaceHolder" Runat="Server">
<meta name="description" content="">
</asp:Content>

<asp:Content ID="keywords" ContentPlaceHolderID="keywordsPlaceHolder" Runat="Server">
<meta name="keywords" content=""/>
</asp:Content>

<asp:Content ID="article" ContentPlaceHolderID="articlePlaceholder" Runat="Server">
 <div class="form_s">
          <h3>Thank You!</h3>
          <p>You request <strong><asp:Label ID="m_txtLeadId" runat="server" Text="#105" /></strong> was received. A copy was sent
            to <strong><asp:Label ID="m_txtEmailAddress" runat="server" /></strong><br />
            We will contact you as soon as possible to schedule pickup time</p>
          <div class="row result">
            <label>First Name:</label>
            <asp:Label id="m_txtFirstName" runat="server"/>
          </div>
          <div class="row result">
            <label>Last Name:</label>
            <asp:Label id="m_txtLastName" runat="server"/>
          </div>
          <div class="row result">
            <label>Phone:</label>
            <asp:Label id="m_txtPhone" runat="server"/>
          </div>
          <div class="row result">
            <label>E-mail:</label>
            <asp:Label id="m_txtEmail" runat="server"/>
          </div>
          <div class="row result">
            <label class="optional">Promo Code: </label>
            <asp:Label id="m_txtPromoCode" runat="server"/>
          </div>
          <div class="row result">
            <label class="optional">Comment:</label>
            <asp:Label id="m_txtMessage" runat="server"/>
          </div>
    </div>
</asp:Content>
