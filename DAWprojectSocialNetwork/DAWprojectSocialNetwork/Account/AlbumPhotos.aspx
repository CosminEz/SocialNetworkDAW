<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlbumPhotos.aspx.cs" Inherits="DAWprojectSocialNetwork.Account.AlbumPhotos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div id="addNewPhoto" runat="server" style="position:absolute;margin-top:100px;right:5px;padding-right:5px">
            <asp:Label runat="server"  CssClass="col-md-2 control-label">Add New Photo</asp:Label> <br />
            
                <asp:FileUpload ID="UploadPhotoToAlbum" runat="server" /> <br />
          <asp:Button ID="AddPhoto" runat="server" Text="Add Photo!" OnClick="addPhoto" />

            
        </div>

    <div id="AlbumPhotosDiv" runat="server" style="margin-top:80px;padding:10px">
    <asp:Label  id="NoPhotosLabel" runat="server" Text="You have no photos in this album yet."></asp:Label> 
    </div>


    
    
</asp:Content>
