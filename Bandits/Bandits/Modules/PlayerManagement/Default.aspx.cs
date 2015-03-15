using Bandits.Usability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bandits.Modules.PlayerManagement
{
    public partial class Default : Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            AllowRole("Admin");
        }
    }
}