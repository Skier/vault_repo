<%@ Page Language="C#" MasterPageFile="~/RugCleaningMasterPage.master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Dalworth.Server.Web.RugCleaning.Error" %>

<%@ MasterType VirtualPath="~/RugCleaningMasterPage.master" %>
<%@ Import Namespace="Dalworth.Server.Web.RugCleaning" %>

<asp:Content ID="title" ContentPlaceHolderID="titlePlaceHolder" Runat="Server"><title>Error Page Dalworth Rug Cleaning </title>
</asp:Content>

<asp:Content ID="description" ContentPlaceHolderID="descriptionPlaceHolder" Runat="Server">
    <meta name="description" content=""/>
</asp:Content>

<asp:Content ID="keywords" ContentPlaceHolderID="keywordsPlaceHolder" Runat="Server">
    <meta name="keywords" content=""/>
</asp:Content>

<asp:Content ID="article" ContentPlaceHolderID="articlePlaceholder" Runat="Server">
<div class="article">
        <h1>Ooops!  Something wrong with our web site.  </h1>
        <p>
            Dear customer 
        </p>
        <p>
        We are very sorry that you reached this error page. Please call and let us know.
        </p>  
</div>
</asp:Content>
