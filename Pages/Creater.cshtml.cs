using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace project.Pages
{
    public class CreaterModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnGetOnCrt(string name, string description, string cshtml, string title)
        {
            if(Routes.AddPost(name, description, cshtml, title) == "1")
            {
                Response.Redirect("/News");
            }
            else
            {

            }
        }
    }
}
