using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using project;
using System.Xml.Linq;

namespace project
{
    public class Routes : PageModel
    {
        public const string Db = "project.db";

        public static string AddPost(string name, string description, string cshtml)
        {
            //добавляем данные
            using (var connection = new SqliteConnection($"Data Source={Db}"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO Posts (Name, Description, Cshtml) VALUES ({name}, {description}, {cshtml})";
                command.ExecuteNonQuery();

                connection.Close();
            }


            string sqlExp = $"SELECT * FROM Posts " +
                            $"WHERE Name = \"{name}\"" +
                            $"AND Description = \"{description}\"" +
                            $"AND Cshtml = \"{cshtml}\"";

            //проверка добавилось или нет
            using (var connection = new SqliteConnection($"Data Source={Db}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExp, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
            }
        }




        public static int UpdatePost(int id, string name, string description, string cshtml)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={Db}"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"UPDATE Posts SET Name='{name}' WHERE ID={id} ";
                    command.ExecuteNonQuery();
                    command.CommandText = $"UPDATE Posts SET Description='{description}' WHERE ID={id} ";
                    command.ExecuteNonQuery();
                    command.CommandText = $"UPDATE Posts SET Cshtml='{cshtml}' WHERE ID={id} ";
                    command.ExecuteNonQuery();

                    connection.Close();
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }


        public static string RemovePost(string name, string description, string cshtml)
        {
            //удаляем
            using (var connection = new SqliteConnection($"Data Source={Db}"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"DELETE FROM Posts" +
                                      $"WHERE Name = \"{name}\"" +
                                      $"AND Description = \"{description}\"" +
                                      $"AND Cshtml = \"{cshtml}\"";
                command.ExecuteNonQuery();

                connection.Close();
            }

            //удалилось или нет
            string sqlExp = $"SELECT * FROM Posts " +
                            $"WHERE Name = \"{name}\"" +
                            $"AND Description = \"{description}\"" +
                            $"AND Cshtml = \"{cshtml}\"";

            using (var connection = new SqliteConnection($"Data Source={Db}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExp, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
            }
        }


        //--------------------------------------------------------------------------------------


        /* public static int ChangeUser(int id, string email, string password, bool isadmin)
         {
             try
             {
                 using (var connection = new SqliteConnection($"Data Source={Db}"))
                 {
                     connection.Open();

                     SqliteCommand command = new SqliteCommand();
                     command.Connection = connection;
                     command.CommandText = $"UPDATE Users SET Email='{email}' WHERE ID={id} ";
                     command.ExecuteNonQuery();
                     command.CommandText = $"UPDATE Users SET Password='{password}' WHERE ID={id} ";
                     command.ExecuteNonQuery();
                     command.CommandText = $"UPDATE Users SET IsAdmin={isadmin} WHERE ID={id} ";
                     command.ExecuteNonQuery();

                     connection.Close();
                 }
                 return 1;
             }
             catch (Exception e)
             {
                 return 0;
             }

         }


                                      временно не нуждаюсь в данных функциях

         public static int RemoveUser(int id)
         {
             try
             {
                 using (var connection = new SqliteConnection($"Data Source={Db}"))
                 {
                     connection.Open();

                     SqliteCommand command = new SqliteCommand();
                     command.Connection = connection;
                     command.CommandText = $"DELETE FROM Users WHERE ID={id}";
                     command.ExecuteNonQuery();

                     connection.Close();
                 }
                 return 1;
             }
             catch (Exception e)
             {
                 return 0;
             }

         }*/


        public static string Login(string email, string password)
        {
            //проверяем есть ли в бд
            string sqlExp = $"SELECT * FROM Users " +
                            $"WHERE Email = \"{email}\"" +
                            $"AND Password = \"{password}\"";
            using (var connection = new SqliteConnection($"Data Source={Db}"))
            {
                connection.Open();


                SqliteCommand command = new SqliteCommand(sqlExp, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
            }
        }


        public static string Signup(string email, string password)
        {
            //добавляем данные
            using (var connection = new SqliteConnection($"Data Source={Db}"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO Users (Email, Password)" +
                                      $" VALUES ('{email}', '{password}')";
                command.ExecuteNonQuery();

                connection.Close();
            }


            //проверка добавилось или нет
            string sqlExp = $"SELECT * FROM Users " +
                            $"WHERE Email = \"{email}\"" +
                            $"AND Password = \"{password}\"";
            using (var connection = new SqliteConnection($"Data Source={Db}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExp, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return "1";
                    }
                    else
                    {
                        return "0";
                    }
                }
            }
            catch (Exception e)
            {
                return 0;
            }

        }


        //------------------------------------------------

        public static int Login(string email, string password)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={Db}"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"SELECT {email} FROM Users WHERE {email} = @Email" +
                        $"AND {password} = @password";
                    command.ExecuteNonQuery();

                    connection.Close();
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }


        public static int Signup(string email, string password)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={Db}"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO Users (Email, Password) VALUES ('{email}', '{password}')";
                    command.ExecuteNonQuery();

                    connection.Close();
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }

        }
    }
}
