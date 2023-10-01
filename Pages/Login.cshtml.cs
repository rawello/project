using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Http;

namespace project.Pages
{
    [IgnoreAntiforgeryToken]
    public class LoginModel : PageModel
    {
        public void OnGet() { }
        public void OnPostOnLogin(string email, string password)
        {
            if (Routes.Login(email, password) == "1")
            {
                string sqlExpression =   $"SELECT * FROM Users" +
                                         $" WHERE Email = \"{email}\"" +
                                         $" and Password = \"{password}\"";
                using (var connection = new SqliteConnection("Data Source=project.db"))
                {
                    connection.Open();

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
                                }
                                else
                                {
                                    Program.IsAdmin = false;
                                }
                            }
                        }
                    }
                    Program.IsLogged = true;
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
