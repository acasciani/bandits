using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bandits.Modules.ProgramManagement
{
    public class Permissions
    {
        public const string ViewAllPrograms = "ProgramManagement.ViewProgram";
        public const string AddNewProgram = "ProgramManagement.UpsertProgram";
        public const string EditExistingProgram = "ProgramManagement.DeleteProgram";
    }
}