using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bandits.Usability;
using BanditsModel;
using System.Collections;
using Bandits.CacheManagement;
using Telerik.OpenAccess.FetchOptimization;

namespace Bandits.Modules.ClubManagement
{
    public partial class UpsertUser : UpsertBase<WebUsersControllerSoped, WebUser, Int32>
    {
        protected override bool TryParse(string raw, out int value) { return int.TryParse(raw, out value); }
        protected override Func<WebUser, int> IdentityCheck { get { return i => i.WebUserId; } }
        protected override string CreatePermission { get { return Permissions.UpsertUser; } }
        protected override string EditPermission { get { return Permissions.UpsertUser; } }
        protected override string ViewPermission { get { return Permissions.ViewUser; } }

        #region viewstates/sessions
        private List<int> AssignedRoleIds
        {
            get { return ViewState["AssignedRoleIds"] as List<int>; }
            set { ViewState["AssignedRoleIds"] = value; }
        }
        private List<int> AssignedPermissionIds
        {
            get { return ViewState["AssignedPermissionIds"] as List<int>; }
            set { ViewState["AssignedPermissionIds"] = value; }
        }
        private List<Auth_ScopeAssignment> AssignedScopeAssignments
        {
            get { return ViewState["AssignedScopeAssignments"] as List<Auth_ScopeAssignment>; }
            set { ViewState["AssignedScopeAssignments"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCurrentUserScopedToEntity()) { Response.Redirect("Default.aspx", true); }

            if (!IsPostBack)
            {
                BindToModel();
                BindRoles(init: true);
                BindPermissions(init: true);
                BindScopeLevels();
                BindPersonalTab();
            }

            if (IsPostBack)
            {
                ActiveTab.Value = Request.Form[ActiveTab.UniqueID];
            }
        }

        protected override WebUser GetEntity(int id)
        {
            using (WebUsersController c = new WebUsersController())
            {
                return c.GetWhere(i => i.WebUserId == id, i => i.Person).FirstOrDefault();
            }
        }

        protected override WebUser SaveEntity()
        {
            using (WebUsersController c = new WebUsersController())
            {
                return c.Update(Entity);
            }
        }

        protected override void Delete()
        {
            throw new NotImplementedException();
        }

        protected override bool IsSaveValid()
        {
            string email = UserName.Text;
            string password = Password.Text;

            if (string.IsNullOrWhiteSpace(email)) { return false; }
            if (password.Length < 6) { return false; }

            return true;
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsSaveValid())
                {
                    WebUser savedUser = SaveEntity();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void BindToModel()
        {
            if (Entity == null) { return; }

            Entity.Email = UserName.Text;
            Entity.Password = Password.Text;
        }

        private void MakeReadonly()
        {
            UserName.ReadOnly = true;
            Password.ReadOnly = true;
        }

        private void BindPersonalTab()
        {
            WebUser user = Entity;
            FirstName.Text = user.Person.FName;
        }

        private void BindRoles(bool init = false)
        {
            using (Auth_AssignmentsController aac = new Auth_AssignmentsController())
            {
                int userid = Entity.WebUserId;
                List<int> unavailableIDs;
                if (init)
                {
                    unavailableIDs = aac.GetWhere(i => i.UserId == userid && i.RoleId.HasValue).Select(i => i.RoleId.Value).ToList();
                    AssignedRoleIds = unavailableIDs;
                }
                else
                {
                    unavailableIDs = AssignedRoleIds;
                }

                IEnumerable<Auth_Role> allRoles = AuthorizationCache.Roles.Values.AsEnumerable();

                rptrAvailableRoles.DataSource = allRoles.Where(i => !unavailableIDs.Contains(i.RoleId)).ToList().OrderBy(i => i.RoleName);
                rptrAvailableRoles.DataBind();
                rptrAssignedRoles.DataSource = allRoles.Where(i => unavailableIDs.Contains(i.RoleId)).ToList().OrderBy(i => i.RoleName);
                rptrAssignedRoles.DataBind();
            }
        }

        private void BindPermissions(bool init = false)
        {
            using (Auth_AssignmentsController apc = new Auth_AssignmentsController())
            {
                int userid = Entity.WebUserId;
                List<int> unavailableIDs;
                if (init)
                {
                    unavailableIDs = apc.GetWhere(i => i.UserId == userid && i.PermissionId.HasValue).Select(i => i.PermissionId.Value).ToList();
                    AssignedPermissionIds = unavailableIDs;
                }
                else
                {
                    unavailableIDs = AssignedPermissionIds;
                }

                IEnumerable<Auth_Permission> allPermissions = AuthorizationCache.Permissions.Values.AsEnumerable();

                rptrAvailablePermissions.DataSource = allPermissions.Where(i => !unavailableIDs.Contains(i.PermissionId)).ToList().OrderBy(i => i.PermissionName);
                rptrAvailablePermissions.DataBind();
                rptrAssignedPermissions.DataSource = allPermissions.Where(i => unavailableIDs.Contains(i.PermissionId)).ToList().OrderBy(i => i.PermissionName);
                rptrAssignedPermissions.DataBind();
            }
        }

        private void BindScopeLevels()
        {
            using (Auth_ScopesController asc = new Auth_ScopesController())
            {
                var levels = asc.Get().OrderBy(i => i.Scope.ToString());
                ddlScopeLevel.DataSource = levels.Select(i => new { Label = i.Scope.ToString(), Value = i.Scope });
                ddlScopeLevel.DataTextField = "Label";
                ddlScopeLevel.DataValueField = "Value";
                ddlScopeLevel.DataBind();
            }
        }

        private void BindScopeAssignments(ScopeType type, bool init = false)
        {
            using (Auth_ScopeAssignmentsController asac = new Auth_ScopeAssignmentsController())
            using (ProgramsController pc = new ProgramsController())
            {
                int userid = Entity.WebUserId;
                List<long> unavailableIDs = asac.GetWhere(i => (i.Auth_Scope.Scope == type && i.Auth_Assignment.UserId == userid), i => i.Auth_Assignment, i=>i.Auth_Scope)
                    .Select(i => i.ResourceId).ToList();

                // We now have the unavailable resource IDs, but we need to see which scope type this is for an understandable translation
                switch (type)
                {
                    case ScopeType.Client: break; // Not supported at this time.

                    case ScopeType.ClubDepartment: break; // Not supported at this time.

                    case ScopeType.Program: // Need to represent results as programs.
                        List<int> programIDs = unavailableIDs.Select(i => Convert.ToInt32(i)).ToList();
                        ddlAssignedScopes.DataSource = pc.GetWhere(i => programIDs.Contains(i.ProgramId)).Select(i => new { Label = string.Format("{0}", i.ProgramName), Value = i.ProgramId });
                        ddlAssignedScopes.DataValueField = "Value";
                        ddlAssignedScopes.DataTextField = "Label";
                        ddlAssignedScopes.DataBind();

                        // in the available scopes dropdown list, add those that aren't already assigned and if there is a filter, then apply the filter
                        ddlAvailableScopes.DataSource = pc.GetWhere(i => !programIDs.Contains(i.ProgramId) && !string.IsNullOrWhiteSpace(txtFilterAvailableScopeResults.Text) ? (
                            i.ProgramName.Contains(txtFilterAvailableScopeResults.Text.Trim()) // the program name contains the filter
                            ) : true).OrderBy(i=>i.ProgramName).Take(10) // otherwise if filter is empty just return true. only display first 10 
                            .Select(i=> new {Label=i.ProgramName, Value=i.ProgramId});
                        ddlAvailableScopes.DataTextField = "Label";
                        ddlAvailableScopes.DataValueField = "Value";
                        ddlAvailableScopes.DataBind();

                        break;

                    default: break;

                }
            }
        }

        protected void Unassign_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            int id;
            if (!int.TryParse(btn.CommandArgument.ToString(), out id)) { return; }

            switch (btn.CommandName)
            {
                case "Role":
                    if (!AssignedRoleIds.Contains(id)) { return; }
                    AssignedRoleIds.Remove(id);

                    // Remove it from repeater list.
                    BindRoles(init: false);
                    break;

                case "Permission":
                    if (!AssignedPermissionIds.Contains(id)) { return; }
                    AssignedPermissionIds.Remove(id);

                    // Remove it from repeater list.
                    BindPermissions(init: false);
                    break;

                default: break;
            }
        }

        protected void Assign_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;

            int id;
            if (!int.TryParse(btn.CommandArgument.ToString(), out id)) { return; }

            switch (btn.CommandName)
            {
                case "Role":
                    if (AssignedRoleIds.Contains(id)) { return; }
                    AssignedRoleIds.Add(id);

                    // Add it to the repeater list.
                    BindRoles(init: false);
                    break;

                case "Permission":
                    if (AssignedPermissionIds.Contains(id)) { return; }
                    AssignedPermissionIds.Add(id);

                    // Add it to the repeater list.
                    BindPermissions(init: false);
                    break;

                default: break;
            }
        }

        protected void ddlScopeLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScopeType scopeType;
            if (Enum.TryParse<ScopeType>(ddlScopeLevel.SelectedValue, out scopeType))
            {
                BindScopeAssignments(scopeType, init: true);
            }
        }
    }
}