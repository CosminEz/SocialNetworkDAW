<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="DAWprojectSocialNetwork.Account.Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="position:absolute ; right:5px; margin-top:50px " >
        
        <asp:Label ID="NameProfileText" runat="server" Text="Label"></asp:Label>


        <br /> <br />

        <p>My Profile Picture</p>

        <br /> 

        <asp:Image ID="ProfileImage" runat="server" Height="200px" Width="200px" />

    </div>

    <div runat="server" id="divFriends" style="position:absolute ;border:1px solid black; padding-top:5px ; height :400px ; padding-left:2px ;right:5px; margin-top:370px ; overflow:auto;"></div>

    <div  style="margin-top:100px">
    <asp:Button ID="FriendRequest" runat="server" Text="Friend Request" OnClick="FriendRequestClick"  />
        <br /> 
        <asp:Label ID="FriendRequestLabel" runat="server" Text=""></asp:Label>
        </div>

    <div id="div1" runat="server"></div>

    

    <div>
        <asp:Button ID="FriendRequestsButton" OnClick = "FriendRequestsSearch" runat="server" Text="FriendRequests" /> &nbsp; &nbsp;
        <asp:Button ID="Albums" OnClick ="goToAlbums" runat="server" Text="Albums" />
    </div>
</asp:Content>
