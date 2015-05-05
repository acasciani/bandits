<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpsertProgram.ascx.cs" Inherits="Bandits.Modules.ProgramManagement.UpsertProgram" %>

<div class="col-sm-12">
    <div class="form-group">
        <asp:TextBox runat="server" ID="ProgramName" MaxLength="100" CssClass="form-control" placeholder="Program name" />
    </div>

    <div class="form-group">
        <asp:Button runat="server" CssClass="btn btn-danger pull-left" Text="Delete" ID="DeleteBtn" OnClick="DeleteBtn_Click" Visible="false" />

        <asp:Button runat="server" CssClass="btn btn-primary pull-right" Text="Save" ID="SaveBtn" OnClick="SaveBtn_Click" Visible="false" />
    </div>
</div>