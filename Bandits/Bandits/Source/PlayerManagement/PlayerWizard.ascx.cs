using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bandits;
using BanditsModel;
using Bandits.Utils;

namespace Bandits.PlayerManagement
{
    public partial class PlayerWizard : System.Web.UI.UserControl
    {
        public bool IsNewPlayer { get; set; }

        private Player _player;
        public Player Player
        {
            get { return _player; }
            set
            {
                if (_player == null)
                {
                    // if already set we don't want to reset
                    _player = value;
                }
                else
                {
                    throw new ApplicationException("Player was already set on wizard, cannot set again.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
        }

        protected void sideBarList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            WizardStep dataitem = (WizardStep)e.Item.DataItem;
            LinkButton linkButton = e.Item.FindControl("SideBarButton") as LinkButton;
            if (linkButton != null)
            {
                linkButton.Enabled = (e.Item.DataItemIndex <= ThePlayerWizard.ActiveStepIndex);
                if (!linkButton.Enabled)
                {
                    ((System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("SideBarListItem")).Attributes["class"] = "disabled";
                }
            }
        }

        protected void ThePlayerWizard_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (ValidateForm())
            {
                MapFormToModel();

                using (PlayersController pc = new PlayersController())
                {
                    if (IsNewPlayer)
                    {
                        pc.AddNew(_player);
                    }
                    else
                    {
                        pc.Update(_player);
                    }
                }
            }
        }

        private void MapModelToForm()
        {
            playersFirstName.Text = Player.Person.FName;
            playersMiddleInitial.Text = Player.Person.MInitial.ToString();
            playersLastName.Text = Player.Person.LName;

            if (Player.Person.Gender.HasValue)
            {
                char gender = Player.Person.Gender.Value;
                playersGender.SelectedValue = gender == 'M' ? "Male" : "Female";
            }

            if (Player.Person.DOB.HasValue)
            {
                playersDateOfBirth.Text = Player.Person.DOB.Value.ToString("mm/dd/yyyy");
            }
        }

        private bool ValidateForm()
        {
            DateTime tryParseDTCatchAll;
            bool fnameValid = playersFirstName.Text.Length > 0 && playersFirstName.Text.Length <= 75;
            bool lnameValid = playersLastName.Text.Length > 0 && playersFirstName.Text.Length <= 75;
            bool minitValid = playersMiddleInitial.Text.Length <= 1;
            bool genderValid = playersGender.SelectedValue.In("M", "F");
            bool dobValid = DateTime.TryParse(playersDateOfBirth.Text, out tryParseDTCatchAll);

            return fnameValid && lnameValid && minitValid && genderValid && dobValid;
        }

        private void MapFormToModel()
        {
            _player.Person.FName = playersFirstName.Text.Trim();
            _player.Person.LName = playersLastName.Text.Trim();
            if (playersMiddleInitial.Text.Trim().Length > 0) _player.Person.MInitial = playersMiddleInitial.Text.Trim().ToCharArray()[0];
            _player.Person.Gender = playersGender.SelectedValue.Trim().ToCharArray()[0];
            _player.Person.DOB = DateTime.Parse(playersDateOfBirth.Text);
        }
    }
}