using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Http;

namespace project.Pages
{
    [IgnoreAntiforgeryToken]
    public class LoginModel : PageModel
    {
        public void OnGet() { Program.LoginState = "0"; }
        public void OnPostOnLogin(string email, string password)
        {
            switch (Routes.Login(email, password))
            {
                case "0":
                    break;
                case "1":
                    checkIsAdmin(email, password);
                    Program.ModalState = "1";
                    Program.IsLogged = true;
                    Program.SessionKeyName = email;
                    Response.Redirect("/News");
                    break;
                case "2":
                    Program.LoginState = "2";
                    break;
            }
        }
        
        public bool checkIsAdmin(string email, string password)
        {
            using (var connection = new SqliteConnection("Data Source=project.db"))
            {
                connection.Open();
                
                string sqlExpression =   $"SELECT * FROM Users" +
                                         $" WHERE Email = \"{email}\"" +
                                         $" and Password = \"{password}\"";
                
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            string isAdmn = (string)reader.GetValue(3);
                            if (isAdmn != "0")
                            {
                                Program.IsAdmin = true;
                                return true;
                            }
                            else
                            {
                                Program.IsAdmin = false;
                                return false;
                            }
                        }
                    }
                }
                return false;
            }
        }
    }
}
