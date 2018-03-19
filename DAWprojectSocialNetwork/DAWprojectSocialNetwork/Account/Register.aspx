<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="DAWprojectSocialNetwork.Account.Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %>.</h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new account</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">

            <asp:Label ID="Label1" runat="server" AssociatedControlID="Username" CssClass="col-md-2 control-label">Username</asp:Label>
             <div class="col-md-10">
                 <asp:TextBox ID="Username" runat="server" CssClass="form-control" ></asp:TextBox>
                 <asp:RequiredFieldValidator  runat="server" CssClass="text-danger" 
                     ErrorMessage="The username filed is required." ControlToValidate="Username"></asp:RequiredFieldValidator>
            </div>

            <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Email</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Email" CssClass="form-control" TextMode="Email" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                    CssClass="text-danger" ErrorMessage="The email field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="The password field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-md-2 control-label">Confirm password</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" Width="129px" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="BirthDate" CssClass="col-md-2 control-label">BirthDate</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="BirthDate" TextMode="Date" CssClass="form-control" Width="184px" />
                <asp:RequiredFieldValidator runat="server"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The birth date field is required." ControlToValidate="BirthDate" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server"  CssClass="col-md-2 control-label">ProfilePhoto</asp:Label>
            <div class="col-md-10">
                <asp:FileUpload ID="ProfilePhotoUpload" runat="server" />

            </div>
        </div>

        <div class="form-group">
            <br />
            <asp:Label ID="Label2"  CssClass="col-md-2 control-label" runat="server" Text="Private Profile?"></asp:Label>
            <asp:RadioButtonList ID="PrivateProfileRadioList" CssClass="col-md-2 control-label" runat="server" ControlToValidate="PrivateProfileRadioList">

    <asp:ListItem  Text ="Yes" Value="1" />
    <asp:ListItem Text ="No" Value="2" />
    

    </asp:RadioButtonList>
            <asp:RequiredFieldValidator CssClass="col-md-2 control-label" ID="RequiredFieldValidator1" runat="server" ErrorMessage="You must select your profile type." ControlToValidate="PrivateProfileRadioList"></asp:RequiredFieldValidator>
        </div>



        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
