<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/ProgramManagement/ProgramManagement.master" AutoEventWireup="true" CodeBehind="ManageProgram.aspx.cs" Inherits="Bandits.Modules.ProgramManagement.ManageProgram" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ModuleContent" runat="server">
    <ui:ProgramWizard runat="server" ID="ProgramWizard" />
</asp:Content>
