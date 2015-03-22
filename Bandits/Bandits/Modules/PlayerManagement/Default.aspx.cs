using Bandits.Usability;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bandits.ObjectCreation;
using BanditsModel;

namespace Bandits.Modules.PlayerManagement
{
    public partial class Default : Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            AllowRole("Admin");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Person test = new Person().WithName("Alex", 'C', "Casciani").WithGender(Gender.Male).WithDOB(new DateTime(1991, 11, 14));
            //AbstractFactoryCreation.GetFactory<Person>().AddNewObject(test);
        }
    }
}