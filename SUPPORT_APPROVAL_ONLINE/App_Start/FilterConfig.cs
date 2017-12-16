using System.Web;
using System.Web.Mvc;

namespace SUPPORT_APPROVAL_ONLINE
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
