using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bandits.Modules.PlayerManagement
{
    public partial class Create : Page
    {
        public override string PageKey { get { return "PlayerManagement.Create"; } }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

            PlayerWizard.Player = PlayerCreation.Create();
        }



        
    }
}