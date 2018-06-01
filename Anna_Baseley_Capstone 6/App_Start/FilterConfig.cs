using System.Web;
using System.Web.Mvc;

namespace Anna_Baseley_Capstone_6
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
