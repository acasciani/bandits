using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bandits.Usability;
using BanditsModel;

namespace Bandits.Modules.ClubManagement
{
    public partial class UpsertUser : UpsertBase<WebUsersControllerSoped, WebUser, Int32>
    {
        protected override bool TryParse(string raw, out int value) { return int.TryParse(raw, out value); }
        protected override Func<WebUser, int> IdentityCheck { get { return i => i.WebUserId; } }
        protected override string CreatePermission { get { return Permissions.UpsertUser; } }
        protected override string EditPermission { get { return Permissions.UpsertUser; } }
        protected override string ViewPermission { get { return Permissions.ViewUser; } }

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
    }
}