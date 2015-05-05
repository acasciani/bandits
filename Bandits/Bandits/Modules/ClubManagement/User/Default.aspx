<%@ Page Title="User Management" Language="C#" MasterPageFile="~/Modules/ClubManagement/ClubManagement.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bandits.Modules.ClubManagement.Default" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ModuleContent" runat="server">
    <asp:GridView runat="server" ID="Results" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField ShowHeader="true" HeaderText="Email" DataField="Email" />
        </Columns>
    </asp:GridView>



</asp:Content>
