<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpsertUser.ascx.cs" Inherits="Bandits.Modules.ClubManagement.UpsertUser" %>

<div class="col-sm-12">
    <div class="form-group">
        <asp:TextBox runat="server" ID="UserName" MaxLength="100" CssClass="form-control" placeholder="Email" />
    </div>

    <div class="form-group">
        <asp:TextBox runat="server" ID="Password" MaxLength="100" CssClass="form-control" placeholder="Password" TextMode="Password" />
    </div>

    <div class="form-group">
        <asp:Button runat="server" CssClass="btn btn-danger pull-left" Text="Delete" ID="DeleteBtn" Visible="false" />

        <asp:Button runat="server" CssClass="btn btn-primary pull-right" Text="Save" ID="SaveBtn" OnClick="SaveBtn_Click" Visible="false" />
    </div>
</div>