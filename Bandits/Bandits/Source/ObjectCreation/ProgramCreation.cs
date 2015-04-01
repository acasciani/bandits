using Bandits.Utils;
using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanditsModel
{
    public static class ProgramCreation
    {
        public static Program Create()
        {
            return new Program() {};
        }


        public static Program WithBasicInfo(this Program refr, string programName)
        {
            refr.Name = programName;
            return refr;
        }
    }
}