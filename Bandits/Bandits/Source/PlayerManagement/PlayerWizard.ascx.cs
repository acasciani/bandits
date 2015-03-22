using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bandits;
using BanditsModel;
using Bandits.Utils;
using Bandits.ObjectCreation;

namespace Bandits.PlayerManagement
{
    public partial class PlayerWizard : System.Web.UI.UserControl
    {
        public bool IsNewPlayer
        {
            get
            {
                if (Player != null)
                {
                    return Player.PlayerId == 0;
                }
                else
                {
                    throw new ApplicationException("Player was null, can't tell if it is new or not.");
                }
            }
        }

        public bool HasPlayerAttached
        {
            get { return Player != null; }
        }

        public Player Player
        {
            get { return HttpContext.Current.Session["PlayerWizard_Player"] as Player; }
            set { HttpContext.Current.Session["PlayerWizard_Player"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HasPlayerAttached) throw new ApplicationException("No player was attached on the player wizard.");

            GuardianTypesDataSource.TypeName = typeof(PlayerWizard).AssemblyQualifiedName;

            BindGuardians();

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
                AbstractFactoryCreation.GetFactory<Player>().AddNewObject(Player);
            }
        }

        private void MapModelToForm()
        {
            playersFirstName.Text = Player.Person.FName;
            playersMiddleInitial.Text = Player.Person.MInitial.ToString();
            playersLastName.Text = Player.Person.LName;

            if (Player.Person.Gender.HasValue)
            {
                Gender gender = Player.Person.Gender.GetGender();
                playersGender.SelectedValue = gender.GetLabel();
            }

            if (Player.Person.DOB.HasValue)
            {
                playersDateOfBirth.Text = Player.Person.DOB.Value.ToString("mm/dd/yyyy");
            }
        }

        private bool ValidateForm()
        {
            bool isPersonValid;

            // validate person
            using (PeopleController c = new PeopleController())
            {
                isPersonValid = c.IsValid(Player.Person);
            }

            // validate player

            // validate guardian

            return isPersonValid;
        }

        private void MapFormToModel()
        {
            Player.Person.WithName(playersFirstName.Text.Trim(), playersMiddleInitial.Text.Trim(), playersLastName.Text.Trim())
                .WithGender(playersGender.SelectedValue.Trim().ToCharArray()[0].ToGender())
                .WithDOB(DateTime.Parse(playersDateOfBirth.Text));

            // map guardians
            foreach (RepeaterItem item in GuardiansRepeater.Items)
            {
                TextBox fname = (TextBox)item.FindControl("guardianFirstName");
                TextBox mInit = (TextBox)item.FindControl("guardianMInitial");
                TextBox lname = (TextBox)item.FindControl("guardianLastName");
                DropDownList relation = (DropDownList)item.FindControl("guardianRelation");

                Guardian thisGuardian = GuardianCreation.Create().WithGuardianType(relation.SelectedValue);
                thisGuardian.Person.WithName(fname.Text.Trim(), mInit.Text.Trim(), lname.Text.Trim());

                // add guardian to player
                Player.HasGuardian(thisGuardian);
            }
        }

        public IEnumerable<DropdownListStruct<int>> SelectGuardianTypes()
        {
            using (GuardianTypesController c = new GuardianTypesController())
            {
                
                return c.Get().Select(t => new DropdownListStruct<int>() { Label = t.Name, Value = t.GuardianTypeId.ToString() }).OrderBy(t => t.Label);
            }
        }

        private void BindGuardians()
        {
            GuardiansRepeater.DataSource = Player.Guardians;
            GuardiansRepeater.DataBind();
        }

        protected void GuardiansRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "RemoveGuardian":
                    int index;
                    if (int.TryParse(e.CommandArgument.ToString(), out index))
                    {
                        IList<Guardian> guardians = Player.Guardians;
                        guardians.RemoveAt(index);
                        Player.HasGuardian(guardians.ToArray());
                        BindGuardians();
                    }
                    break;
                
                default: break;
            }
        }

        protected void AddGuardian_Click(object sender, EventArgs e)
        {
            IList<Guardian> guardians = Player.Guardians;
            guardians.Add(GuardianCreation.Create());
            Player.HasGuardian(guardians.ToArray());
            BindGuardians();
        }

        protected void guardianFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void guardianMInitial_TextChanged(object sender, EventArgs e)
        {

        }

        protected void guardianLastName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}