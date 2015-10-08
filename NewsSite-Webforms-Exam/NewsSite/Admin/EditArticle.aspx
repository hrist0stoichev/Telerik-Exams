<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditArticle.aspx.cs" Inherits="NewsSite.Admin.EditArticle" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView runat="server" ID="ListViewArticles" ItemType="NewsSite.Models.Article" SelectMethod="ListViewArticles_GetData" DeleteMethod="ListViewArticles_DeleteItem"
        UpdateMethod="ListViewArticles_UpdateItem" InsertMethod="ListViewArticles_InsertItem" DataKeyNames="ID" InsertItemPosition="LastItem">
        <LayoutTemplate>
            <div class="row">
                <asp:LinkButton runat="server" ID="LinkButtonSortByTitle" Text="Sort by Title" CommandName="Sort" CommandArgument="Title" CssClass="col-md-2 btn btn-default"></asp:LinkButton>
                <asp:LinkButton runat="server" ID="LinkButtonSortByDate" Text="Sort by Date" CommandName="Sort" CommandArgument="DateCreated" CssClass="col-md-2 btn btn-default"></asp:LinkButton>
                <asp:LinkButton runat="server" ID="LinkButtonSortByCategory" Text="Sort by Category" CommandName="Sort" CommandArgument="Category.Name" CssClass="col-md-2 btn btn-default"></asp:LinkButton>
                <asp:LinkButton runat="server" ID="LinkButtonSortByLikes" Text="Sort by Likes" CommandName="Sort" CommandArgument="Likes.Value" CssClass="col-md-2 btn btn-default"></asp:LinkButton>
            </div>
            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
            <div class="row">
                <asp:DataPager runat="server" ID="DataPagerCategories" PagedControlID="ListViewArticles" PageSize="5">
                    <Fields>
                        <asp:NextPreviousPagerField ShowNextPageButton="false" />
                        <asp:NumericPagerField />
                        <asp:NextPreviousPagerField ShowPreviousPageButton="false"></asp:NextPreviousPagerField>
                    </Fields>
                </asp:DataPager>
                <div class="btn btn-info pull-right" id="buttonShowInsertPanel" onclick="showNewArticle()">Insert new Article</div>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="row">
                <h3>
                    <%#: Item.Title %>
                    <asp:LinkButton runat="server" ID="LinkButtonEdit" Text="Edit" CommandName="Edit" CssClass="btn btn-info"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="LinkButtonDelete" Text="Delete" CommandName="Delete" CssClass="btn btn-danger"></asp:LinkButton>
                </h3>
                <p>Category: <%#: Item.Category.Name %></p>
                <p><%#: Item.Content.Length >= 300 ? Item.Content.Substring(0, 300) + "..." : Item.Content %></p>
                <p>Likes count: <%#: Item.Likes.Count %></p>
                <div>
                    <i>by <%#: Item.Author.UserName %></i>
                    <i>created on: <%#: Item.DateCreated %></i>
                </div>
            </div>
        </ItemTemplate>
        <EditItemTemplate>
            <div class="row">
                <h3>
                    <asp:TextBox runat="server" ID="TextBoxTitle" Text="<%#: BindItem.Title %>"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorUpdateTitle" ControlToValidate="TextBoxTitle" EnableClientScript="true"
                     ErrorMessage="Title is required!" Text="Title is required!" Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="Update"></asp:RequiredFieldValidator>
                    <asp:LinkButton runat="server" ID="LinkButtonSave" Text="Save" CommandName="Update" CssClass="btn btn-success" ValidationGroup="Update"></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="LinkButtonCancel" Text="Cancel" CommandName="Cancel" CssClass="btn btn-danger"></asp:LinkButton>
                </h3>
                <p>
                    <asp:DropDownList runat="server" ID="DropDownListCategories" DataTextField="Name" DataValueField="ID"
                        SelectedValue="<%#: BindItem.CategoryID %>" ItemType="NewsSite.Models.Category" SelectMethod="DropDownListCategories_GetData">
                    </asp:DropDownList>
                </p>
                <p>
                    <asp:TextBox runat="server" ID="TextBoxContent" TextMode="MultiLine" Width="100%" Height="120px" Text="<%#: BindItem.Content %>"></asp:TextBox>
                    <div>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorUpdateContent" ControlToValidate="TextBoxContent" EnableClientScript="true"
                     ErrorMessage="Content is required!" Text="Content is required!" Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="Update"></asp:RequiredFieldValidator>
                    </div>
                </p>
                <p>Likes count: <%#: Item.Likes.Count %></p>
                <div>
                    <i>by <%#: Item.Author.UserName %></i>
                    <i>created on: <%#: Item.DateCreated %></i>
                </div>
            </div>
        </EditItemTemplate>
        <InsertItemTemplate>
            <div id="insertNewArticle" style="display:none">
                <div class="row">
                    <h3>Title:
                        <asp:TextBox runat="server" ID="TextBoxInsert" Text="<%#: BindItem.Title %>"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorInsertTitle" ControlToValidate="TextBoxInsert" EnableClientScript="true"
                     ErrorMessage="Title is required!" Text="Title is required!" Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                    </h3>
                    <p>
                        Category:
                    <asp:DropDownList runat="server" ID="DropDownListCategories" DataTextField="Name" DataValueField="ID"
                        SelectedValue="<%#: BindItem.CategoryID %>" ItemType="NewsSite.Models.Category" SelectMethod="DropDownListCategories_GetData">
                    </asp:DropDownList>
                    </p>
                    <p>
                        Content:
                    <asp:TextBox runat="server" ID="TextBoxContent" TextMode="MultiLine" Width="307px" Height="128px" Text="<%#: BindItem.Content %>"></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorInsertContent" ControlToValidate="TextBoxContent" EnableClientScript="true"
                     ErrorMessage="Content is required!" Text="Content is required!" Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                    </p>
                    <div>
                        <asp:LinkButton runat="server" ID="LinkButtonInsert" Text="Insert" CommandName="Insert" CssClass="btn btn-success" ValidationGroup="Insert"></asp:LinkButton>
                        <asp:LinkButton runat="server" ID="LinkButtonCancel" Text="Cancel" CommandName="Cancel" CssClass="btn btn-danger"></asp:LinkButton>
                    </div>
                </div>
            </div>

        </InsertItemTemplate>
    </asp:ListView>
    <script>
        function showNewArticle() {
            $('#insertNewArticle').show();
            $('#buttonShowInsertPanel').hide();
        }
    </script>
</asp:Content>
