using Bandits.Utils;
using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bandits.Modules.PlayerManagement
{
    public partial class ManageFind : Page
    {
        #region page definition
        public override string PageKey { get { return "PlayerManagement.Manage"; } }
        #endregion


        protected class PlayerSortable
        {
            public string FName { get; set; }
            public string LName { get; set; }
            public Player PlayerObject { get; set; }
        }

        private IEnumerable<Player> Players
        {
            get
            {
                using (PlayersController c = new PlayersController())
                {
                    Expression<Func<Player, object>> loadWithGuardians = t => t.Guardians;
                    Expression<Func<Player, object>> loadWithPerson = t => t.Person;
                    return c.GetWhere((t => true), loadWithGuardians, loadWithPerson);
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            BindPlayersResults();
        }

        private IEnumerable<PlayerSortable> PlayersSortable(IEnumerable<Player> data)
        {
            return data.Select(p => new PlayerSortable() { FName = p.Person.FName, LName = p.Person.LName, PlayerObject = p });
        }

        private void BindPlayersResults()
        {
            PlayersResults.DataSource = PlayersSortable(Players);
            PlayersResults.DataBind();
        }

        protected void PlayersResults_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortHelper<PlayerSortable>.Sort(PlayersSortable(Players), GetSortDirection(e.SortExpression.ToString()) == "ASC", e.SortExpression.ToString(), ref PlayersResults);
        }

        protected void PlayersResults_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int playerId;
            if (!int.TryParse(e.CommandArgument.ToString(), out playerId))
            {
                return;
            }

            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect("~/Modules/PlayerManagement/Manage.aspx?pid=" + playerId);
                    break;
                default: break;
            }
        }
    }
}