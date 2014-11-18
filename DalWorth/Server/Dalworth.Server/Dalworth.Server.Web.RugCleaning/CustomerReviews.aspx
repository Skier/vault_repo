<%@ Page Language="C#" MasterPageFile="~/RugCleaningMasterPage.master" AutoEventWireup="true" CodeBehind="CustomerReviews.aspx.cs" Inherits="Dalworth.Server.Web.RugCleaning.CustomerReviews" %>
<%@ MasterType VirtualPath="~/RugCleaningMasterPage.master" %>
<%@ Import Namespace="Dalworth.Server.Web.RugCleaning" %>

<asp:Content ID="title" ContentPlaceHolderID="titlePlaceHolder" Runat="Server"><title>Dalworth Oriental Rug cleaning customer reviews | Dallas, Fort Worth, Plano, Southlake</title>
</asp:Content>

<asp:Content ID="description" ContentPlaceHolderID="descriptionPlaceHolder" Runat="Server">
    <meta name="description" content="Dalworth Rug Cleaning Customer testimonials. Call <%Response.Write(Master.PhoneNumber);%>, free quote."/>
</asp:Content>

<asp:Content ID="keywords" ContentPlaceHolderID="keywordsPlaceHolder" Runat="Server">
    <meta name="keywords" content="rug cleaning reviews, rug repair review, dalworth, dallas, fort worth, plano, southlake, frisco"/>
</asp:Content>

<asp:Content ID="article" ContentPlaceHolderID="articlePlaceholder" Runat="Server">
 <div class="article faq">
 <h1>Testimonials</h1>
 
         <asp:Repeater ID="m_repeater" runat ="server">
            <ItemTemplate>
                <div class="testimon">
                    <div class="name"><%#DataBinder.Eval(Container.DataItem, "DatePosted")%> &nbsp &nbsp <%#DataBinder.Eval(Container.DataItem, "FirstLastName")%><span class="city">&nbsp, &nbsp<%#DataBinder.Eval(Container.DataItem, "City")%>,<%#DataBinder.Eval(Container.DataItem, "State")%>,<%#DataBinder.Eval(Container.DataItem, "Zip")%></span></div>
                    <div class="comment">
                        <%#DataBinder.Eval(Container.DataItem, "CustomerNoteEdited")%>
                    </div>
                </div>        
            </ItemTemplate>
        </asp:Repeater>                 
</div>
</asp:Content>