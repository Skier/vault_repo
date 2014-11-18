<%@ Page Language="C#" MasterPageFile="~/RugCleaningMasterPage.master" AutoEventWireup="true" CodeBehind="InterestingLinks.aspx.cs" Inherits="Dalworth.Server.Web.RugCleaning.InterestingLinks" %>
<%@ MasterType VirtualPath="~/RugCleaningMasterPage.master" %>
<%@ Import Namespace="Dalworth.Server.Web.RugCleaning" %>

<asp:Content ID="title" ContentPlaceHolderID="titlePlaceHolder" Runat="Server"><title>Oriental Rug Cleaning Links</title>
</asp:Content>

<asp:Content ID="description" ContentPlaceHolderID="descriptionPlaceHolder" Runat="Server">
    <meta name="description" content="Interesting oriental rug cleaning links."/>
</asp:Content>

<asp:Content ID="keywords" ContentPlaceHolderID="keywordsPlaceHolder" Runat="Server">
    <meta name="keywords" content="oriental rug cleaning"/>
</asp:Content>

<asp:Content ID="article" ContentPlaceHolderID="articlePlaceholder" Runat="Server">
<div class="article">
        <h1>Oriental rug cleaning links recommended by Dalworth </h1>
        <ul>
         <li>Wikipedia.org: <a href="http://en.wikipedia.org/wiki/Oriental_rug">History of oriental rugs </a></li>
         <li>Rugnology.com: <a href="http://www.rugnology.com/rugcare.html">Area rug care</a></li>
         <li>Jacobsenrugs.com: <a href="http://www.jacobsenrugs.com/care.htm">Taking care of your rugs at home</a></li>
         <li>Karastan.com: <a href="http://www.karastan.com/CarpetCare.aspx">How to care for Karastan rugs</a></li>
         <li>Flokati-rugs.com: <a href="http://www.flokati-rugs.com/flokati-care.cfm">How to care for Flokati rugs</a></li>
        </ul>
      </div>
</asp:Content>



