<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatestArticlesControl.ascx.cs" Inherits="Dalworth.Server.Web.Restoration.LatestArticlesControl" %>
<div class="links">
            <h3><asp:Literal ID="m_txtArticleName" runat="server" /> </h3>
            <ul> 
                <asp:Repeater ID="m_repeater" runat ="server">
                    <ItemTemplate>
                    <li><a href="<%#DataBinder.Eval(Container.DataItem, "Url")%>"><%#DataBinder.Eval(Container.DataItem, "Title")%></a> </li> 
                    </ItemTemplate>
                </asp:Repeater> 
                
            </ul>
</div>

