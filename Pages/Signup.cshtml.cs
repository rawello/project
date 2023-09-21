using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace project.Pages
{
    public class SignupModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost(string email, string password)
        {
            if(Routes.Signup(email, password) == "1")
            {
                Response.Redirect("/Login");
            }
            else { }
        }
    }
}
