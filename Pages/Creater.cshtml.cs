using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace project.Pages
{
    public class CreaterModel : PageModel
    {
        public void OnGet()
        {
            if (Program.IsAdmin == false)
            {
                Response.Redirect("/News");
            }
        }
        public void OnPostOnCrt(string name, string description, string cshtml, string title)
        {
            if(Routes.AddPost(name, description, cshtml, title) == "1")
            {
                Response.Redirect("/News");
            }
            else
            {
                Response.Redirect("/Index");
            }
        }
        public void OnGetBack()
        {
            Response.Redirect("/News");
        }
    }
}
