<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="DAWprojectSocialNetwork.Account.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div style="margin-top:100px">
        <asp:Label ID="Label1" runat="server" Text="Users:"></asp:Label>  <br />   
        <asp:BulletedList ID="UsersList" runat="server" DisplayMode="HyperLink"></asp:BulletedList>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Groups:"></asp:Label>  <br />
        <asp:BulletedList ID="GroupsList" runat="server" DisplayMode="HyperLink"></asp:BulletedList>

        </div>
</asp:Content>
