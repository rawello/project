using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace project.Pages
{
    public class EditorModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnGetOnUpdt(int id,string name, string description, string cshtmlCode, string title)
        {
            if (Routes.UpdatePost(id, name, description, cshtmlCode, title) == "1")
            {
                Response.Redirect("/News");
            }
            else
            {
                Response.Redirect("/News");
            }
        }
    }
}
