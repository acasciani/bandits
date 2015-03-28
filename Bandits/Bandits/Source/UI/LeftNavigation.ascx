<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftNavigation.ascx.cs" Inherits="Bandits.UI.LeftNavigation" %>

<asp:Repeater runat="server" ID="Groups">
    <ItemTemplate>
        <ul class="nav nav-sidebar">
            <asp:Repeater runat="server" ID="GroupItems" DataSource='<%# Eval("Items") %>'>
                <ItemTemplate>
                    <li class="<%# IsActive(Eval("Key").ToString()) ? "active" : "" %>"><a href="<%# Eval("Link") %>"><%# Eval("Display") %> <%# IsActive(Eval("Key").ToString()) ? "<span class=\"sr-only\">(current)</span>" : "" %></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </ItemTemplate>
    <SeparatorTemplate>
        <hr class="left-navigation" />
    </SeparatorTemplate>
</asp:Repeater>