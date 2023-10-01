using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics.Metrics;

namespace project.Pages
{
    public class NewsModel : PageModel
    {

        public void OnGet()
        {
        }
        public void OnGetOnClick(int id, string name)
        {
            Program.tempPage = id;
            Program.Name = name;
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
        public void OnGetSize(string state)
        {
            if(state == "big")
            {
                Program.IsBig = 100;
                Response.Redirect("/News");
            }
            else
            {
                Program.IsBig = 23;
                Response.Redirect("/News");
            }
        }  
    }
 }
