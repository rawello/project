using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace project.Pages
{
    public class NavRouteModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnGetBack()
        {
            Response.Redirect("/News");
        }
    }
}
