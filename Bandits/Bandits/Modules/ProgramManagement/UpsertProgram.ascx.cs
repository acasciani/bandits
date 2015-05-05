using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bandits.Usability;

namespace Bandits.Modules.ProgramManagement
{
    public partial class UpsertProgram : UpsertBase<ProgramsControllerScoped, Program, Int32>
    {
        protected override Func<Program, Int32> IdentityCheck { get { return i => i.ProgramId; } }
        protected override string CreatePermission { get { return Permissions.AddNewProgram; } }
        protected override string EditPermission { get { return Permissions.EditExistingProgram; } }
        protected override string ViewPermission { get { return Permissions.ViewAllPrograms; } }
        protected override bool TryParse(string raw, out int value) { return int.TryParse(raw, out value); }

        protected override void Delete()
        {
            // check if they have the scoped user, then if they do delete
        }

        protected override bool IsSaveValid()
        {
            throw new NotImplementedException();
        }

        protected override Program GetEntity(int id)
        {
            using (ProgramsController c = new ProgramsController())
            {
                return c.GetWhere(i => i.ProgramId == id).FirstOrDefault();
            }
        }

        protected override Program SaveEntity()
        {
            using (ProgramsController c = new ProgramsController())
            {
                return c.Update(Entity);
            }
        }
    }
}