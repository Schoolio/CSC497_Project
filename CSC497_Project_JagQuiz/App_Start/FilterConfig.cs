using System.Web;
using System.Web.Mvc;

namespace CSC497_Project_JagQuiz
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
