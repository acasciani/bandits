using Bandits.ObjectCreation;
using BanditsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bandits.ProgramManagement
{
    public partial class ProgramWizard : System.Web.UI.UserControl
    {
        public bool IsNewProgram
        {
            get
            {
                if (Program != null)
                {
                    return Program.ProgramId == 0;
                }
                else
                {
                    throw new ApplicationException("Program was null, can't tell if it is new or not.");
                }
            }
        }

        public bool HasProgramAttached
        {
            get { return Program != null; }
        }

        public Program Program
        {
            get { return HttpContext.Current.Session["ProgramWizard_Program"] as Program; }
            set { HttpContext.Current.Session["ProgramWizard_Program"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HasProgramAttached) throw new ApplicationException("No program was attached on the program wizard.");

            if (IsPostBack)
            {
                MapFormToModel();
                return;
            }

            MapModelToForm();
        }

        protected void sideBarList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            WizardStep dataitem = (WizardStep)e.Item.DataItem;
            LinkButton linkButton = e.Item.FindControl("SideBarButton") as LinkButton;
            if (linkButton != null)
            {
                linkButton.Enabled = (e.Item.DataItemIndex <= TheProgramWizard.ActiveStepIndex);
                if (!linkButton.Enabled)
                {
                    ((System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("SideBarListItem")).Attributes["class"] = "disabled";
                }
            }
        }

        protected void TheProgramWizard_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            if (ValidateForm())
            {
                if (IsNewProgram)
                {
                    AbstractFactoryCreation.GetFactory<Program>().AddNewObject(Program);
                }
                else
                {
                    AbstractFactoryCreation.GetFactory<Program>().UpdateObject(Program);
                }

                Response.Redirect("~/Modules/ProgramManagement/");
            }
        }

        private void MapModelToForm()
        {
            programName.Text = Program.Name;
        }

        private bool ValidateForm()
        {
            bool isProgramValid;

            // validate program
            using (ProgramsController c = new ProgramsController())
            {
                isProgramValid = c.IsValid(Program);
            }

            return isProgramValid;
        }

        private void MapFormToModel()
        {
            Program.WithBasicInfo(programName.Text.Trim());
        }
    }
}