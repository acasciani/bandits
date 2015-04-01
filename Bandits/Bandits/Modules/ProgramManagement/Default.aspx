<%@ Page Title="" Language="C#" MasterPageFile="~/Modules/ProgramManagement/ProgramManagement.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Bandits.Modules.ProgramManagement.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ModuleContent" runat="server">
    <div class="row">
        <div class="col-sm-12">
            <asp:GridView runat="server" ID="ProgramsResults" UseAccessibleHeader="true" CssClass="table table-responsive table-striped table-hover table-condensed" GridLines="None" AutoGenerateColumns="false" OnSorting="ProgramsResults_Sorting" 
                AllowSorting="true" OnRowCommand="ProgramsResults_RowCommand">
                <Columns>
                    <asp:BoundField DataField="ProgramName" SortExpression="ProgramName" HeaderText="Program Name" />                    
                
                    <asp:TemplateField ItemStyle-CssClass="text-right">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CssClass="glyphicon glyphicon-trash" ToolTip="Remove program" CommandName="Delete" Enabled="false" CommandArgument='<%# Eval("ProgramObject.ProgramId") %>' />&nbsp;&nbsp;
                            <asp:LinkButton runat="server" CssClass="glyphicon glyphicon-edit" ToolTip="Edit program information" CommandName="Edit" CommandArgument='<%# Eval("ProgramObject.ProgramId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
