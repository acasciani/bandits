using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Bandits.Utils
{
    public static class BindUtils
    {
        public static void BindItemsToDDL(this DropDownList refr, IEnumerable<DropdownListStruct<object>> structObj, bool hasTopLevelValue = false)
        {
            ListItem option1 = null;
            if (hasTopLevelValue && refr.Items.Count > 0)
            {
                option1 = refr.Items[0];
            }

            refr.DataValueField = "Value";
            refr.DataTextField = "Label";
            refr.DataSource = structObj;
            refr.DataBind();
            
            if(option1 != null) refr.Items.Insert(0, option1);
        }
    }
}