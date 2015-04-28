using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits
{
    public partial class ProgramsController : OpenAccessBaseApiController<BanditsModel.Program, BanditsModel.BanditsModel>
    {
        #region validation checks
        public bool IsValid(Program program)
        {
            bool isNameValid = IsNameValid(program.ProgramName);

            return isNameValid;
        }

        public bool IsNameValid(string programName)
        {
            return (programName.Length > 0 && programName.Length <= 75);
        }
        #endregion
    }
}