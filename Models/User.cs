using Microsoft.AspNetCore.Mvc;

namespace project.Models
{
    public class User
    {
        [BindProperty]
        public int      ID          { get; set; }
        [BindProperty]
        public string   Email       { get; set; }
        [BindProperty]
        public string   Password    { get; set; }
        [BindProperty]
        public string   IsAdmin     { get; set; }
    }
}
