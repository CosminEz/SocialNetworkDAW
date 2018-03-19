<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Albums.aspx.cs" Inherits="DAWprojectSocialNetwork.Account.Albums" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="AlbumsDiv" runat="server" style="margin-top:80px;padding:10px">
    <asp:Label  id="NoAlbumsLabel" runat="server" Text="You have no albums yet."></asp:Label> 
    </div>

    <div id="addAlbum" style="position:absolute;right:5px;padding-right:10px;margin-top:100px" runat="server">
    <asp:Button ID="newAlbum" runat="server" Text=" Album Nou" OnClick="addNewAlbum" />
    </div>

    <div id="addingNewAlbum" runat="server" style="position:absolute;margin-top:100px;right:5px;padding-right:10px">
        <asp:Label  runat="server" Text="Nume Album"></asp:Label> &nbsp;
        <asp:TextBox ID="albumName" runat="server" Width="100px"></asp:TextBox> <br /> 
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Album Name must be completed." ControlToValidate="albumName" ForeColor="#CC0000"></asp:RequiredFieldValidator>
        <br />
       
        
        <asp:Button ID="AddAlbumBtn" runat="server" Text="Adaugare Album" OnClick="addAlbumInDB"/>
        <br />

    </div>

</asp:Content>
