<%@ Page Title="User Management" Language="C#" MasterPageFile="~/Modules/ClubManagement/User/User.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bandits.Modules.ClubManagement.Default" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ModuleContent" runat="server">
    <asp:GridView runat="server" ID="Results" AutoGenerateColumns="false" CssClass="table table-striped table-condensed table-hover" BorderWidth="0">
        <Columns>
            <asp:BoundField ShowHeader="true" HeaderText="Email" DataField="Email" />
        </Columns>
    </asp:GridView>



</asp:Content>
