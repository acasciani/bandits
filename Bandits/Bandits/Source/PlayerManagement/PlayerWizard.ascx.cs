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

        public IEnumerable<GuardianType> GuardianTypes
        {
            get
            {
                IEnumerable<GuardianType> types = HttpContext.Current.Session["PlayerWizard_GuardianTypes"] as IEnumerable<GuardianType>;
                if (types == null)
                {
                    using (GuardianTypesController c = new GuardianTypesController())
                    {
                        types = c.Get();
                        HttpContext.Current.Session["PlayerWizard_GuardianTypes"] = types;
                    }
                }
                return types;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HasPlayerAttached) throw new ApplicationException("No player was attached on the player wizard.");

            GuardianTypesDataSource.TypeName = typeof(PlayerWizard).AssemblyQualifiedName;

            if (IsPostBack)
            {
                MapFormToModel();
                return;
            }

            MapModelToForm();

            BindGuardians();
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
                if (IsNewPlayer)
                {
                    AbstractFactoryCreation.GetFactory<Player>().AddNewObject(Player);
                    Response.Redirect("~/Modules/PlayerManagement/");
                }
                else
                {
                    AbstractFactoryCreation.GetFactory<Player>().UpdateObject(Player);
                    Response.Redirect("~/Modules/PlayerManagement/");
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
                Gender gender = Player.Person.Gender.GetGender();
                playersGender.SelectedValue = gender.GetLabel();
            }

            if (Player.Person.DOB.HasValue)
            {
                playersDateOfBirth.Text = Player.Person.DOB.Value.ToString("yyyy-MM-dd");
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
            DateTime dob;

            Player.Person.WithName(playersFirstName.Text.Trim(), playersMiddleInitial.Text.Trim(), playersLastName.Text.Trim())
                .WithGender(playersGender.SelectedValue.Trim().ToCharArray()[0].ToGender())
                .WithDOB(DateTime.TryParse(playersDateOfBirth.Text, out dob) ? dob : (DateTime?)null);

            // map guardians
            foreach (RepeaterItem item in GuardiansRepeater.Items)
            {
                TextBox fname = (TextBox)item.FindControl("guardianFirstName");
                TextBox mInit = (TextBox)item.FindControl("guardianMInitial");
                TextBox lname = (TextBox)item.FindControl("guardianLastName");
                DropDownList relation = (DropDownList)item.FindControl("guardianRelation");

                Guardian guardian = Player.Guardians.ElementAt(item.ItemIndex);
                guardian.Person.WithName(fname.Text.Trim(), mInit.Text.Trim(), lname.Text.Trim());
                
                int guardianTypeId;
                if (int.TryParse(relation.SelectedValue, out guardianTypeId)) guardian.GuardianType = GuardianTypes.Where(gt => gt.GuardianTypeId == guardianTypeId).First();
            }
        }

        public IEnumerable<DropdownListStruct<int>> SelectGuardianTypes()
        {
            return GuardianTypes.Select(t => new DropdownListStruct<int>() { Label = t.Name, Value = t.GuardianTypeId.ToString() }).OrderBy(t => t.Label);
        }

        private void BindGuardians()
        {
            GuardiansRepeater.DataSource = Player.Guardians;
            GuardiansRepeater.DataBind();

            foreach (RepeaterItem item in GuardiansRepeater.Items)
            {
                DropDownList relation = (DropDownList)item.FindControl("guardianRelation");
                Guardian guardian = Player.Guardians.ElementAt(item.ItemIndex);

                if (guardian != null && guardian.GuardianType != null)
                {
                    relation.SelectedValue = Player.Guardians.ElementAt(item.ItemIndex).GuardianType.GuardianTypeId.ToString();
                }
            }
        }

        protected void GuardiansRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "RemoveGuardian":
                    int guardianId;
                    if (int.TryParse(e.CommandArgument.ToString(), out guardianId))
                    {
                        Guardian guardian = Player.Guardians.Where(g => g.GuardianId == guardianId).First();
                        Player.Guardians.Remove(guardian);
                        BindGuardians();
                    }
                    break;
                
                default: break;
            }
        }

        protected void AddGuardian_Click(object sender, EventArgs e)
        {
            Player.HasGuardian(GuardianCreation.Create());
            BindGuardians();
        }
    }
}