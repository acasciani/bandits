using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Bandits.Utils
{
    public static class SortHelper<T>
    {
        public static IOrderedEnumerable<T> Sort(IEnumerable<T> data, bool isAscending, string sortExpression, ref GridView gridView){
            PropertyDescriptor prop = TypeDescriptor.GetProperties(typeof(T)).Find(sortExpression, false);
            
            IOrderedEnumerable<T> result;

            if (isAscending)
            {
                result = data.OrderBy(t => prop.GetValue(t));
            }
            else
            {
                result = data.OrderByDescending(t => prop.GetValue(t));
            }

            gridView.DataSource = result;
            gridView.DataBind();

            return result;
        }
    }
}