<%@ Page Title="News" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="NewsSite.News" %>
<asp:Content ID="ContentBody" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%: this.Title %></h1>
    <h2>Most popular articles</h2>
    <asp:Repeater runat="server" ID="RepeaterMostPopularArticles" ItemType="NewsSite.Models.Article" SelectMethod="RepeaterMostPopularArticles_GetData">
        <ItemTemplate>
            <div class="row">
                <h3>
                    <asp:HyperLink runat="server" ID="HyperLinkArticle" NavigateUrl='<%#: string.Format("~/ArticleDetails?id={0}", Item.ID) %>'><%#: Item.Title %></asp:HyperLink>
                     <small><%#: Item.Category.Name %></small>
                </h3>
                <p><%#: Item.Content.Length >= 300 ? Item.Content.Substring(0, 300) + "..." : Item.Content %></p>
                <p>Likes: <%#: Item.Likes.Count %></p>
                <div>
                    <i>by <%#: Item.Author.UserName %></i>
                    <i>created on: <%#: Item.DateCreated %></i>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <h2>All categories</h2>
    <asp:ListView runat="server" ID="ListViewCategories" ItemType="NewsSite.Models.Category" SelectMethod="ListViewCategories_GetData" GroupItemCount="2">
        <GroupTemplate>
            <div class="row">
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
            </div>
        </GroupTemplate>
        <ItemTemplate>
            <div class="col-md-6">
                <h3><%#: Item.Name %></h3>
                <asp:ListView runat="server" ID="ListViewArticles" ItemType="NewsSite.Models.Article" DataSource="<%# Item.Articles.OrderByDescending(a => a.DateCreated).Take(3) %>">
                    <LayoutTemplate>
                        <ul>
                            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/ArticleDetails?id={0}", Item.ID) %>' Text='<%# string.Format("<strong>{0}</strong> <i>by {1}</i>", Item.Title, Item.Author.UserName) %>'></asp:HyperLink>
                        </li>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        No articles.
                    </EmptyDataTemplate>
                </asp:ListView>
            </div>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
