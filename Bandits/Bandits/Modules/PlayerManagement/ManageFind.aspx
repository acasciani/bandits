<%@ Page Title="Manage Player" Language="C#" MasterPageFile="~/Modules/PlayerManagement/PlayerManagement.master" AutoEventWireup="true" CodeBehind="ManageFind.aspx.cs" Inherits="Bandits.Modules.PlayerManagement.ManageFind" %>


<asp:Content ID="MainContent" ContentPlaceHolderID="ModuleContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <asp:GridView runat="server" ID="PlayersResults" UseAccessibleHeader="true" CssClass="table table-responsive table-striped table-hover table-condensed" GridLines="None" AutoGenerateColumns="false" 
                OnSorting="PlayersResults_Sorting" AllowSorting="true" OnRowCommand="PlayersResults_RowCommand">
                <Columns>
                    <asp:BoundField DataField="LName" SortExpression="LName" HeaderText="Last Name" />
                    <asp:BoundField DataField="FName" SortExpression="FName" HeaderText="First Name" />
                    <asp:TemplateField ItemStyle-CssClass="text-right">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CssClass="glyphicon glyphicon-trash" ToolTip="Remove player" CommandName="Delete" Enabled="false" CommandArgument='<%# Eval("PlayerObject.PlayerId") %>' />&nbsp;&nbsp;
                            <asp:LinkButton runat="server" CssClass="glyphicon glyphicon-edit" ToolTip="Edit player information" CommandName="Edit" CommandArgument='<%# Eval("PlayerObject.PlayerId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
