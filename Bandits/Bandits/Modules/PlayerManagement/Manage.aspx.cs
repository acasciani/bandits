using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bandits.Modules.PlayerManagement
{
    public partial class Manage : Page
    {
        #region page definition
        public override string PageKey { get { return "PlayerManagement.Manage"; } }
        #endregion


        private Player Player { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Player = GetPlayer(Request.QueryString["pid"] ?? "n/a");

            if (Player == null)
            {
                // could find player, redirect to find a player to manage
                Response.Redirect("/Modules/PlayerManagement/ManageFind.aspx");
            }

            PlayerWizard.Player = Player;
        }




        private Player GetPlayer(string playerIdStr)
        {
            int playerId;
            if (!int.TryParse(playerIdStr, out playerId)) return null;

            if (IsPostBack) return Session["PlayerManagement_Player"] as Player;

            using (PlayersController c = new PlayersController())
            {
                Session["PlayerManagement_Player"] = c.Get(playerId);
                return Session["PlayerManagement_Player"] as Player;
            }
        }
    }
}