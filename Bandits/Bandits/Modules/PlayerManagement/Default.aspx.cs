using Bandits.Usability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bandits.ObjectCreation;
using BanditsModel;
using Telerik.OpenAccess.FetchOptimization;
using System.Linq.Expressions;
using Bandits.Utils;

namespace Bandits.Modules.PlayerManagement
{
    public partial class Default : Page
    {
        public const string PageKeyConst = "PlayerManagement.CurrentPlayers";
        public const string PageDisplayConst = "Current Players";
        public const string PageLinkConst = "/Modules/PlayerManagement/Default.aspx";
        public override string PageKey { get { return PageKeyConst; } }
        public override string PageDisplay { get { return PageDisplayConst; } }


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

        protected void Page_Init(object sender, EventArgs e)
        {
            AllowRole("Admin");
            base.Page_Init(sender, e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }

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
    }
}