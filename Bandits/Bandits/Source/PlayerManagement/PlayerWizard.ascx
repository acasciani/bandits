<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlayerWizard.ascx.cs" Inherits="Bandits.PlayerManagement.PlayerWizard" %>
    
<asp:Wizard ID="ThePlayerWizard" runat="server" ActiveStepIndex="0" OnFinishButtonClick="ThePlayerWizard_FinishButtonClick">
        <LayoutTemplate>
            <div><asp:PlaceHolder ID="headerPlaceHolder" runat="server" /></div>
            <div class="row">
                <div class="col-md-3 col-sm-4"><asp:PlaceHolder ID="sideBarPlaceHolder" runat="server" /></div>
                <div class="col-md-9 col-sm-8 form-horizontal"><asp:PlaceHolder ID="WizardStepPlaceHolder" runat="server" /></div>
            </div>
            <div class="row">
                <div class="col-sm-12"><asp:PlaceHolder ID="navigationPlaceHolder" runat="server" /></div></div>
        </LayoutTemplate>
        
        <StartNavigationTemplate>
            <div class="pull-left"><asp:Button ID="resetCreatePlayerAll" runat="server" CssClass="btn btn-danger" Text="Reset" Enabled="false"></asp:Button></div>
            <div class="pull-right"><asp:Button ID="nextCreatePlayerStep" runat="server" CssClass="btn btn-default" Text="Continue" CommandName="MoveNext"></asp:Button></div>
        </StartNavigationTemplate>
        <StepNavigationTemplate>
            <div class="pull-left"><asp:Button ID="resetCreatePlayerAll" runat="server" CssClass="btn btn-danger" Text="Reset" Enabled="false"></asp:Button></div>
            <div class="pull-right">
                <asp:Button ID="prevCreatePlayerStep" runat="server" CssClass="btn btn-default" Text="Previous" CommandName="MovePrevious"></asp:Button>
                <asp:Button ID="nextCreatePlayerStep" runat="server" CssClass="btn btn-default" Text="Continue" CommandName="MoveNext"></asp:Button>
            </div>
        </StepNavigationTemplate>
        <FinishNavigationTemplate>
            <div class="pull-left"><asp:Button ID="resetCreatePlayerAll" runat="server" CssClass="btn btn-danger" Text="Reset" Enabled="false"></asp:Button></div>
            <div class="pull-right">
                <asp:Button ID="prevCreatePlayerStep" runat="server" CssClass="btn btn-default" Text="Previous" CommandName="MovePrevious"></asp:Button>
                <asp:Button ID="submitCreatePlayer" runat="server" CssClass="btn btn-default" Text="Save Player" CommandName="MoveComplete"></asp:Button>
            </div>
        </FinishNavigationTemplate>

        <HeaderTemplate>
            <h2>Add New Player</h2>
        </HeaderTemplate>

        <SideBarTemplate>
            <asp:ListView runat="server" ID="sideBarList" OnItemDataBound="sideBarList_ItemDataBound">               
                <LayoutTemplate><ul class="nav nav-pills nav-stacked"><div id="itemPlaceholder" runat="server"/></ul></LayoutTemplate>

                <SelectedItemTemplate><li class="active" runat="server" id="sideBarListItem"><asp:LinkButton ID="sideBarButton" runat="server" Text="Button" /></li></SelectedItemTemplate>

                <ItemTemplate><li runat="server" id="sideBarListItem"><asp:LinkButton ID="sideBarButton" runat="server" Text="Button" /></li></ItemTemplate>
            </asp:ListView>
        </SideBarTemplate>

        <WizardSteps>
            <asp:WizardStep runat="server" title="Basic Information">
                <div class="form-group">
                    <div class="col-sm-12"><label for="<%=playersFirstName.ClientID %>">Player's Name</label></div>
                    <div class="col-sm-5"><asp:TextBox ID="playersFirstName" runat="server" MaxLength="75" placeholder="First Name" CssClass="form-control" /></div>
                    <div class="col-sm-2"><asp:TextBox ID="playersMiddleInitial" runat="server" MaxLength="1" placeholder="M.I." CssClass="form-control" /></div>
                    <div class="col-sm-5"><asp:TextBox ID="playersLastName" runat="server" MaxLength="75" placeholder="Last Name" CssClass="form-control" /></div>
                </div>

                <div class="form-group">
                    <div class="col-sm-12"><label for="<%=playersDateOfBirth.ClientID %>">Player's Date of Birth</label></div>
                    <div class="col-sm-12"><asp:TextBox ID="playersDateOfBirth" runat="server" MaxLength="50" placeholder="mm/dd/yyyy" CssClass="form-control" TextMode="Date" /></div>
                </div>

                <div class="form-group">
                    <div class="col-sm-12"><label for="<%=playersGender.ClientID %>">Player's Gender</label></div>
                    <div class="col-sm-12">
                        <asp:DropDownList runat="server" ID="playersGender" CssClass="form-control">
                            <asp:ListItem Enabled="true" Text="Select Gender" Value="" />
                            <asp:ListItem Enabled="true" Text="Male" Value="M" />
                            <asp:ListItem Enabled="true" Text="Female" Value="F" />
                        </asp:DropDownList>
                    </div>
                </div>


            </asp:WizardStep>

            <asp:WizardStep runat="server" title="Guardians">
                <asp:UpdatePanel ID="GuardiansUpdatePanel" runat="server" UpdateMode="Conditional"><ContentTemplate>
                    <asp:Repeater runat="server" ID="GuardiansRepeater" EnableViewState="true" OnItemCommand="GuardiansRepeater_ItemCommand">
                        <ItemTemplate>
                            <div class="well">
                                <div class="form-group">
                                    <div class="col-sm-12"><label for="MainContent_ModuleContent_PlayerWizard_ThePlayerWizard_GuardiansRepeater_guardianFirstName_<%#DataBinder.Eval(Container, "ItemIndex", "")%>">Guardian's Name</label></div>
                                    <div class="col-sm-5"><asp:TextBox ID="guardianFirstName" runat="server" MaxLength="75" placeholder="First Name" CssClass="form-control" EnableViewState="true" Text="<%# ((BanditsModel.Guardian)Container.DataItem).Person.FName %>" /></div>
                                    <div class="col-sm-2"><asp:TextBox ID="guardianMInitial" runat="server" MaxLength="1" placeholder="M.I." CssClass="form-control" /></div>
                                    <div class="col-sm-5"><asp:TextBox ID="guardianLastName" runat="server" MaxLength="75" placeholder="Last Name" CssClass="form-control" /></div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-12"><label for="MainContent_ModuleContent_PlayerWizard_ThePlayerWizard_GuardiansRepeater_guardianRelation_<%#DataBinder.Eval(Container, "ItemIndex", "")%>">Relation to Player</label></div>
                                    <div class="col-sm-12">
                                        <asp:DropDownList runat="server" ID="guardianRelation" DataSourceID="GuardianTypesDataSource" DataTextField="Label" DataValueField="Value" CssClass="form-control">
                                            <asp:ListItem Enabled="true" Text="Select Relation to Player" Value="" />
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="row" runat="server" visible="<%# GuardiansRepeater.Items.Count > 0 %>">
                                    <div class="col-sm-12">
                                        <i class="glyphicon glyphicon-minus-sign"></i>&nbsp;
                                        <asp:LinkButton runat="server" Text="Remove Guardian" CommandArgument='<%# Container.ItemIndex%>' CommandName="RemoveGuardian" />
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                
                    <div class="row">
                        <div class="col-sm-12">
                            <i class="glyphicon glyphicon-plus-sign"></i>&nbsp;
                            <asp:LinkButton runat="server" Text="Add Another Guardian" ID="AddGuardian" OnClick="AddGuardian_Click" />
                        </div>
                    </div>

                    <asp:ObjectDataSource ID="GuardianTypesDataSource" runat="server" SelectMethod="SelectGuardianTypes"></asp:ObjectDataSource>
                </ContentTemplate></asp:UpdatePanel>
            </asp:WizardStep>
        </WizardSteps>
    </asp:Wizard>