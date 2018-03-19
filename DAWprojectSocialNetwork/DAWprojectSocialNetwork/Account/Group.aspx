<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Group.aspx.cs" Inherits="DAWprojectSocialNetwork.Account.Group" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top:100px">
    <asp:Button ID="joinGroup" runat="server" Text="Join Group!" OnClick ="joinGroupEvent" />
    </div> <br /> <br />
    <div runat="server" id="chat" style="position:absolute;width:500px">

    <div runat="server" id="ChatGroup" style="height :400px ; padding:2px ;border:1px solid black"></div> <br />
    <asp:TextBox ID="TextChat" runat="server" Height="71px" Width="362px" >
        </asp:TextBox>  &nbsp;  &nbsp;
       
        
        <asp:Button ID="SendMessage" runat="server" Text="Send" OnClick="sendMessageOnGroup" /> 

         </div>

    <div style="position:absolute;right:5px;margin-top:60px;padding:5px">
        <asp:Label ID="Label1" runat="server" Text="Members in this groups"></asp:Label>
    </div>
    <div runat="server" id="divMembers" style="position:absolute ;border:1px solid black; padding-top:5px ; height :400px ; padding:2px ;right:5px; margin-top:100px ; overflow:auto;"></div>
    
</asp:Content>
