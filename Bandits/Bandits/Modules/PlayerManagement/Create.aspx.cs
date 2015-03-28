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
        public const string PageKeyConst = "PlayerManagement.Create";
        public const string PageDisplayConst = "Add Player";
        public const string PageLinkConst = "/Modules/PlayerManagement/Create.aspx";
        public override string PageKey { get { return PageKeyConst; } }
        public override string PageDisplay { get { return PageDisplayConst; } }


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