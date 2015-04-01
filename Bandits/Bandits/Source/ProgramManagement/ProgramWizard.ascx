<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProgramWizard.ascx.cs" Inherits="Bandits.ProgramManagement.ProgramWizard" %>
    
<asp:Wizard ID="TheProgramWizard" runat="server" ActiveStepIndex="0" OnFinishButtonClick="TheProgramWizard_FinishButtonClick">
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
            <div class="pull-left"><asp:Button ID="resetCreateProgramAll" runat="server" CssClass="btn btn-danger" Text="Reset" Enabled="false"></asp:Button></div>
            <div class="pull-right"><asp:Button ID="nextCreateProgramStep" runat="server" CssClass="btn btn-default" Text="Continue" CommandName="MoveNext"></asp:Button></div>
        </StartNavigationTemplate>
        <StepNavigationTemplate>
            <div class="pull-left"><asp:Button ID="resetCreateProgramAll" runat="server" CssClass="btn btn-danger" Text="Reset" Enabled="false"></asp:Button></div>
            <div class="pull-right">
                <asp:Button ID="prevCreateProgramStep" runat="server" CssClass="btn btn-default" Text="Previous" CommandName="MovePrevious"></asp:Button>
                <asp:Button ID="nextCreateProgramStep" runat="server" CssClass="btn btn-default" Text="Continue" CommandName="MoveNext"></asp:Button>
            </div>
        </StepNavigationTemplate>
        <FinishNavigationTemplate>
            <div class="pull-left"><asp:Button ID="resetCreateProgramAll" runat="server" CssClass="btn btn-danger" Text="Reset" Enabled="false"></asp:Button></div>
            <div class="pull-right">
                <asp:Button ID="prevCreateProgramStep" runat="server" CssClass="btn btn-default" Text="Previous" CommandName="MovePrevious"></asp:Button>
                <asp:Button ID="submitCreateProgram" runat="server" CssClass="btn btn-default" Text="Save Program" CommandName="MoveComplete"></asp:Button>
            </div>
        </FinishNavigationTemplate>

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
                    <div class="col-sm-12"><label for="<%=programName.ClientID %>">Program Name</label></div>
                    <div class="col-sm-12"><asp:TextBox ID="programName" runat="server" MaxLength="75" placeholder="Program Name" CssClass="form-control" /></div>
                </div>

            </asp:WizardStep>

        </WizardSteps>
    </asp:Wizard>