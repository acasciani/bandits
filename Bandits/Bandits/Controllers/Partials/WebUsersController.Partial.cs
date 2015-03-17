using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanditsModel;
using System.Web.Security;
using Bandits.Providers.Security;
using System.Web.Script.Serialization;

namespace Bandits
{
    public partial class WebUsersController : OpenAccessBaseApiController<BanditsModel.WebUser, BanditsModel.BanditsModel>
    {

    }
}