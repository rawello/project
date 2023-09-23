using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace project.Pages
{
    [IgnoreAntiforgeryToken]
    public class LoginModel : PageModel
    {
        public string mess {  get; set; }
        public void OnGet(string email, string password)
        {

            mess = Routes.Login(email, password);
            if (Routes.Login(email, password) == "1")
            {
                if(email == "rawello" & password == "rawello")
                {
                    Program.IsAdmin = true;
                }
                Program.SessionKeyName = email;
                Response.Redirect("/News");
            }
            else
            {

            }

        }

    }
}
