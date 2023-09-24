using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace project.Pages
{
    public class ReloaderModel : PageModel
    {
        public void OnGet()
        {
            Program.IsAdmin = false;
            Program.IsLogged = false;
            Program.SessionKeyName = "Гость";
            Response.Redirect("/");
        }
    }
}
