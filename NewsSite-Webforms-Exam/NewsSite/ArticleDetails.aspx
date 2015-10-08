<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArticleDetails.aspx.cs" Inherits="NewsSite.ArticleDetails" %>
<%@ Register Src="~/Controls/LikeControl.ascx" TagPrefix="uc" TagName="LikeControl" %>
<asp:Content ID="ContentBody" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView runat="server" ID="FormViewDetails" ItemType="NewsSite.Models.Article" SelectMethod="FormViewDetails_GetItem">
        <ItemTemplate>
            <uc:LikeControl runat="server" ID="LikeControlArticleDetails" LikesValue="<%# GetLikesValue(Item) %>" OnLike="LikeControl_Like"
                DataID="<%# Item.ID %>" UserVote="<%# GetUserVote(Item) %>" />
            <h3>
                <%#: Item.Title %>
                <small> Category: <%#: Item.Category.Name %></small>
            </h3>
            <p><%#: Item.Content %></p>
            <p>
                Author:<%#: Item.Author.UserName %>
                <span class="pull-right"><%#: Item.DateCreated %></span>
            </p>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
