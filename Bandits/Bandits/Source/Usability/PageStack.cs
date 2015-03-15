using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bandits.Usability
{
    public class PageStack
    {
        private Stack<PageRef> stack;

        public PageRef Push(PageRef page)
        {
            // deactivate the previous one
            stack.Peek().ActiveBreadcrumb = false;
            page.ActiveBreadcrumb = true;
            stack.Push(page);
            return stack.Peek();
        }

        public PageRef Pop()
        {
            PageRef popped = stack.Pop();
            popped.ActiveBreadcrumb = false;

            // active the next one
            stack.Peek().ActiveBreadcrumb = true;
            return popped;
        }

        public IEnumerable<object> GetAsBreadcrumbs()
        {
            return stack.Select(r => new
            {
                Active = r.ActiveBreadcrumb,
                Href = r.Location,
                Title = r.PageTitle
            });
        }
    }
}