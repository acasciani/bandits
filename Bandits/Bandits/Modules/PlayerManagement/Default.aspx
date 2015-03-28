<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/PlayerManagement/PlayerManagement.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bandits.Modules.PlayerManagement.Default" %>
<%@ MasterType virtualPath="~/Modules/PlayerManagement/PlayerManagement.master" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="ModuleContent" runat="server">
    <h1>Player Management</h1>

    <div class="row">
        <div class="col-sm-12">
            <asp:GridView runat="server" ID="PlayersResults" UseAccessibleHeader="true" CssClass="table table-responsive table-striped table-hover table-condensed" GridLines="None" AutoGenerateColumns="false" OnSorting="PlayersResults_Sorting" AllowSorting="true">
                <Columns>
                    <asp:BoundField DataField="LName" SortExpression="LName" HeaderText="Last Name" />
                    <asp:BoundField DataField="FName" SortExpression="FName" HeaderText="First Name" />
                    
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
