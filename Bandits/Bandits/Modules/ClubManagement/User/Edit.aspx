<%@ Page Title="Edit User" Language="C#" MasterPageFile="~/Modules/ClubManagement/User/User.master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="Bandits.Modules.ClubManagement.User.Edit" %>

<%@ Register Src="~/Modules/ClubManagement/User/UpsertUser.ascx" TagName="UpsertUser" TagPrefix="ui" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ModuleContent" runat="server">
    <ui:UpsertUser runat="server" ID="UpsertWizard" />
</asp:Content>
