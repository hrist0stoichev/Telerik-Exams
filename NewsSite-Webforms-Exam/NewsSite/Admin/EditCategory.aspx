<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditCategory.aspx.cs" Inherits="NewsSite.Admin.EditCategory" %>

<asp:Content ID="ContentBody" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView runat="server" ID="ListViewCategories" ItemType="NewsSite.Models.Category" SelectMethod="ListViewCategories_GetData" DeleteMethod="ListViewCategories_DeleteItem"
        UpdateMethod="ListViewCategories_UpdateItem" DataKeyNames="ID" InsertMethod="ListViewCategories_InsertItem" InsertItemPosition="LastItem">
        <LayoutTemplate>
            <div class="row">
                <asp:LinkButton runat="server" ID="LinkButtonSortByCategory" Text="Category Name" CommandName="Sort" CommandArgument="Name" CssClass="btn btn-default"></asp:LinkButton>
            </div>
            <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
            <div class="row">
                <asp:DataPager runat="server" ID="DataPagerCategories" PagedControlID="ListViewCategories" PageSize="5">
                    <Fields>
                        <asp:NextPreviousPagerField ShowNextPageButton="false" />
                        <asp:NumericPagerField />
                        <asp:NextPreviousPagerField ShowPreviousPageButton="false"></asp:NextPreviousPagerField>
                    </Fields>
                </asp:DataPager>
            </div>
        </LayoutTemplate>
        <ItemTemplate>
            <div class="row">
                <div class="col-md-3"><%#: Item.Name %></div>
                <asp:LinkButton runat="server" ID="LinkButtonEdit" Text="Edit" CommandName="Edit" CssClass="btn btn-info"></asp:LinkButton>
                <asp:LinkButton runat="server" ID="LinkButtonDelete" Text="Delete" CommandName="Delete" CssClass="btn btn-danger"></asp:LinkButton>
            </div>
        </ItemTemplate>
        <EditItemTemplate>
            <div class="row">
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="TextBoxName" Text="<%#: BindItem.Name %>"></asp:TextBox>
                    <div>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorUpdateCategory" ControlToValidate="TextBoxName" EnableClientScript="true"
                     ErrorMessage="Name is required!" Text="Name is required!" Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="Update"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <asp:LinkButton runat="server" ID="LinkButtonSave" Text="Save" CommandName="Update" CssClass="btn btn-success" ValidationGroup="Update"></asp:LinkButton>
                <asp:LinkButton runat="server" ID="LinkButtonCancel" Text="Cancel" CommandName="Cancel" CssClass="btn btn-danger"></asp:LinkButton>
            </div>
        </EditItemTemplate>
        <InsertItemTemplate>
            <div class="row">
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="TextBoxInsert" Text="<%#: BindItem.Name %>"></asp:TextBox>
                    <div>
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidatorInsertCategory" ControlToValidate="TextBoxInsert" EnableClientScript="true"
                     ErrorMessage="Name is required!" Text="Name is required!" Display="Dynamic" ForeColor="Red" Font-Bold="true" ValidationGroup="Insert"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <asp:LinkButton runat="server" ID="LinkButtonInsert" Text="Save" CommandName="Insert" OnClick="LinkButtonInsert_Click" CssClass="btn btn-info" ValidationGroup="Insert"></asp:LinkButton>
                <asp:LinkButton runat="server" ID="LinkButtonCancel" Text="Cancel" CommandName="Cancel" CssClass="btn btn-danger"></asp:LinkButton>
            </div>
        </InsertItemTemplate>
    </asp:ListView>
</asp:Content>
