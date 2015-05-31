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
        private List<Auth_ScopeAssignment> AssignedScopeAssignments{
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
                return c.GetWhere(i => i.WebUserId == id).FirstOrDefault();
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

        private void BindRoles(bool init = false)
        {
            using (Auth_AssignmentsController aac = new Auth_AssignmentsController())
            {
                int userid = Entity.WebUserId;
                List<int> unavailableIDs;
                if (init)
                {
                    unavailableIDs = aac.GetWhere(i => i.UserId == userid).Select(i => i.Role).Select(i => i.RoleId).ToList();
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
                    unavailableIDs = apc.GetWhere(i => i.UserId == userid).Select(i => i.Permission).Select(i => i.PermissionId).ToList();
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

        private void BindScopeAssignments(ScopeType type, bool init = false)
        {
            using (Auth_ScopeAssignmentsController asac = new Auth_ScopeAssignmentsController())
            {
                int userid = Entity.WebUserId;
                List<int> unavailableIDs;
                
                asac.GetWhere(i=>i.ScopeId == (int)type)

                IEnumerable<Auth_Permission> allPermissions = AuthorizationCache.Permissions.Values.AsEnumerable();

                rptrAvailablePermissions.DataSource = allPermissions.Where(i => !unavailableIDs.Contains(i.PermissionId)).ToList().OrderBy(i => i.PermissionName);
                rptrAvailablePermissions.DataBind();
                rptrAssignedPermissions.DataSource = allPermissions.Where(i => unavailableIDs.Contains(i.PermissionId)).ToList().OrderBy(i => i.PermissionName);
                rptrAssignedPermissions.DataBind();
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

            }
        }
    }
}