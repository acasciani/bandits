<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/Module.master" AutoEventWireup="True" CodeBehind="Create.aspx.cs" Inherits="Bandits.Modules.PlayerManagement.Create" %>
<%@ Register Src="~/Source/PlayerManagement/PlayerWizard.ascx" TagPrefix="ui" TagName="PlayerWizard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ModuleContent" runat="server">
    <ui:PlayerWizard runat="server" ID="PlayerWizard" />

</asp:Content>
