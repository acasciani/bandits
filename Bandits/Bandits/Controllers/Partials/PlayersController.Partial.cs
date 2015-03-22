using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits
{
    public partial class PlayersController : OpenAccessBaseApiController<BanditsModel.Player, BanditsModel.BanditsModel>
    {
        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}