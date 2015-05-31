<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpsertUser.ascx.cs" Inherits="Bandits.Modules.ClubManagement.UpsertUser" %>

<asp:HiddenField runat="server" ID="ActiveTab" />

<script type="text/javascript">
    function pageLoad(){
        var tabName = $("[id*=ActiveTab]").val() != "" ? $("[id*=ActiveTab]").val() : "personalinformation";
        $('.nav.nav-tabs a[href="#' + tabName + '"]').tab('show');
        $(".nav.nav-tabs a").click(function () {
            $("[id*=ActiveTab]").val($(this).attr("href").replace("#", ""));
        });
    }
</script>

<div>
    <div role="tabpanel">
        <ul class="nav nav-tabs" role="tablist">
            <li role="presentation" class="active"><a href="#personalinformation" aria-controls="personal" role="tab" data-toggle="tab">Personal</a></li>
            <li role="presentation"><a href="#logininformation" aria-controls="login" role="tab" data-toggle="tab">Login</a></li>
            <li role="presentation"><a href="#roleinformation" aria-controls="roles" role="tab" data-toggle="tab">Authorization</a></li>
            <li role="presentation"><a href="#scopinginformation" aria-controls="scoping" role="tab" data-toggle="tab">Scoping</a></li>
        </ul>
    
        <!-- Tab panes -->
        <div class="tab-content">
            <div role="tabpanel" class="tab-pane active" id="personalinformation">
                <p>Enter personal information about the user below.</p>
                <div class="row">
                    <div class="col-sm-5"><div class="form-group"><asp:TextBox runat="server" ID="FirstName" MaxLength="100" CssClass="form-control" placeholder="First name" /></div></div>
                    <div class="col-sm-2"><div class="form-group"><asp:TextBox runat="server" ID="MiddleInitial" MaxLength="1" CssClass="form-control" placeholder="M.I." /></div></div>
                    <div class="col-sm-5"><div class="form-group"><asp:TextBox runat="server" ID="LastName" MaxLength="100" CssClass="form-control" placeholder="Last name" /></div></div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <asp:DropDownList runat="server" ID="Gender" CssClass="form-control">
                                <asp:ListItem Value="">-- Gender --</asp:ListItem>
                                <asp:ListItem Value="M">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="form-group"><asp:TextBox runat="server" ID="DateOfBirth" MaxLength="100" CssClass="form-control" placeholder="Date of birth (mm/dd/yyyy)"/></div>
                    </div>
                </div>
            </div>


            <div role="tabpanel" class="tab-pane" id="logininformation">
                <div class="form-group"><asp:TextBox runat="server" ID="UserName" MaxLength="100" CssClass="form-control" placeholder="Email" /></div>
                <div class="form-group"><asp:TextBox runat="server" ID="Password" MaxLength="100" CssClass="form-control" placeholder="Password" TextMode="Password" /></div>
            </div>


            <div role="tabpanel" class="tab-pane" id="roleinformation">
                <h4>Role Assignment</h4>
                <div class="col-sm-6">
                    <strong>Available Roles</strong>
                    <div class="well well-sm">
                        <asp:Repeater runat="server" ID="rptrAvailableRoles">
                            <ItemTemplate>
                                <span style="margin:10px"><asp:LinkButton runat="server" CssClass="label label-primary" CommandName="Role" CommandArgument='<%# Eval("RoleId") %>' OnClick="Assign_Click">
                                    <%# Eval("RoleName") %> <span class="glyphicon glyphicon-plus"></span>
                                </asp:LinkButton></span>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <div class="col-sm-6">
                    <strong>Assigned Roles</strong>
                    <div class="well well-sm">
                        <asp:Repeater runat="server" ID="rptrAssignedRoles">
                            <ItemTemplate>
                                <span style="margin:10px"><asp:LinkButton runat="server" CssClass="label label-danger" CommandName="Role" CommandArgument='<%# Eval("RoleId") %>' OnClick="Unassign_Click">
                                    <%# Eval("RoleName") %> <span class="glyphicon glyphicon-minus"></span>
                                </asp:LinkButton></span>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <div class="clearfix"></div>

                <h4>Permission Assignment</h4>
                <div class="col-sm-6">
                    <strong>Available Permissions</strong>
                    <div class="well well-sm">
                        <asp:Repeater runat="server" ID="rptrAvailablePermissions">
                            <ItemTemplate>
                                <span style="margin:10px"><asp:LinkButton runat="server" CssClass="label label-primary" CommandName="Permission" CommandArgument='<%# Eval("PermissionId") %>' OnClick="Assign_Click">
                                    <%# Eval("PermissionName") %> <span class="glyphicon glyphicon-plus"></span>
                                </asp:LinkButton></span>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <div class="col-sm-6">
                    <strong>Assigned Permissions</strong>
                    <div class="well well-sm">
                        <asp:Repeater runat="server" ID="rptrAssignedPermissions">
                            <ItemTemplate>
                                <span style="margin:10px"><asp:LinkButton runat="server" CssClass="label label-danger" CommandName="Permission" CommandArgument='<%# Eval("PermissionId") %>' OnClick="Unassign_Click">
                                    <%# Eval("PermissionName") %> <span class="glyphicon glyphicon-minus"></span>
                                </asp:LinkButton></span>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <div class="clearfix"></div>
            </div>


            <div role="tabpanel" class="tab-pane" id="scopinginformation">
                <h4>Scope Levels</h4>
                <div class="col-sm-6">
                    <div class="form-group">
                        <asp:DropDownList runat="server" ID="ddlScopeLevel" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlScopeLevel_SelectedIndexChanged">
                            <asp:ListItem Value="">-- Select a Scope Level --</asp:ListItem>
                            <asp:ListItem Value="0" Text="Client" />
                            <asp:ListItem Value="1" Text="Program" />
                            <asp:ListItem Value="2" Text="Team" />
                            <asp:ListItem Value="3" Text="ClubDepartment" />
                            <asp:ListItem Value="4" Text="Player" />
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-sm-6">
                    <strong>Available Roles</strong>
                    <div class="well well-sm">
                        <asp:Repeater runat="server" ID="Repeater1">
                            <ItemTemplate>
                                <span style="margin:10px"><asp:LinkButton runat="server" CssClass="label label-primary" CommandName="Role" CommandArgument='<%# Eval("RoleId") %>' OnClick="Assign_Click">
                                    <%# Eval("RoleName") %> <span class="glyphicon glyphicon-plus"></span>
                                </asp:LinkButton></span>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <div class="col-sm-6">
                    <strong>Assigned Roles</strong>
                    <div class="well well-sm">
                        <asp:Repeater runat="server" ID="Repeater2">
                            <ItemTemplate>
                                <span style="margin:10px"><asp:LinkButton runat="server" CssClass="label label-danger" CommandName="Role" CommandArgument='<%# Eval("RoleId") %>' OnClick="Unassign_Click">
                                    <%# Eval("RoleName") %> <span class="glyphicon glyphicon-minus"></span>
                                </asp:LinkButton></span>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

                <div class="clearfix"></div>
            </div>
        </div>
    </div>


    <div class="margin-top-15 upsert-navigation">
        <asp:Button runat="server" CssClass="btn btn-primary pull-right" Text="Continue" ID="SaveBtn" OnClick="SaveBtn_Click" Visible="true" />
        <asp:Button runat="server" CssClass="btn btn-danger pull-right" Text="Delete" ID="DeleteBtn" Visible="true" />
    </div>
</div>