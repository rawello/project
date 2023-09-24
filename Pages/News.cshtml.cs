using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;

namespace project.Pages
{
    public class NewsModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnGetOnClick(int id)
        {
            Program.tempPage = id;
            Response.Redirect("/NavRoute");
        }
        public void OnGetOnAdd()
        {
            Response.Redirect("/Creater");
        }
        public void OnGetOnDelete(int id)
        {
            Routes.RemovePost(id);
        }
        public void OnGetOnEdit(int id)
        {
            Program.tempPage = id;
            Response.Redirect("/Editor");
        }
    }
 }
