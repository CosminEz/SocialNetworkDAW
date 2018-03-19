<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="DAWprojectSocialNetwork.Account.Message" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br /> <br />

    <div runat="server" id="Chat" style="padding:20px; width: 329px; ;height:400px;overflow:auto; position:fixed"></div>

    <div runat="server" id="ChatBox" style="height: 53px ; padding-top:430px">

        <asp:TextBox ID="TextChat" runat="server" Height="71px" Width="362px" >
        </asp:TextBox>  &nbsp;  &nbsp;
        
        <asp:Button ID="SendMessage" runat="server" Text="Send" OnClick="sendMessage" /> 

   </div>

</asp:Content>
