<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Photo.aspx.cs" Inherits="DAWprojectSocialNetwork.Account.Photo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top:100px;padding-top:20px;width:500px;padding-left:20px">
    <asp:Label ID="LabelUserName" runat="server" Text="Label" ></asp:Label> <br />
        </div>
    <div runat="server" id="photoDiv"  >
        <div runat="server" id="comments" style="position:absolute;border:1px solid black;right:100px;width:430px;height:400px;padding:5px;overflow:auto"></div>
        <div style="position:absolute;right:280px;margin-top:430px;">
          <asp:TextBox ID="TextBoxComment"  runat="server"></asp:TextBox> &nbsp; &nbsp; &nbsp;
            
            <asp:Button ID="sendComment" runat="server" Text="Send" OnClick="insertComment" /><br />
            <asp:RequiredFieldValidator  runat="server" ErrorMessage="You must insert something!" ControlToValidate="TextBoxComment"></asp:RequiredFieldValidator>

        </div>
    </div>
    
</asp:Content>
