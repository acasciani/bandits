<%@ Page Title="Manage Player" Language="C#" MasterPageFile="~/Modules/PlayerManagement/PlayerManagement.master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="Bandits.Modules.PlayerManagement.Manage" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ModuleContent" runat="server">
    <ui:PlayerWizard runat="server" ID="PlayerWizard" />
</asp:Content>
