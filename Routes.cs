using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using project;
using project.Models;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace project
{
    public class Routes : PageModel
    {
        public const string Db = "project.db";

        public static string AddPost(string name, string description, string cshtml, string title)
        {
            //добавляем данные
            using (var connection = new SqliteConnection($"Data Source={Db}"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO Posts (Name, Description, Cshtml, Title) VALUES ('{name}', '{description}', '{cshtml}', '{title}')";
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
                        connection.Close();

                        return "1";
                    }
                    if(reader.HasRows == false) 
                    {
                        connection.Close();

                    return "0";
                    }
                }
            }
            return "0";
        }



           
        public static string UpdatePost(int id, string name, string description, string cshtml, string title)
        {
            using (var connection = new SqliteConnection($"Data Source={Db}"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"UPDATE Posts SET Name=\"{name}\", " +
                    $" Description ='{description}', " +
                    $" Cshtml = '{cshtml}', " +
                    $" Title = '{title}' " +
                    $"WHERE _id = {id} ";
                command.ExecuteNonQuery();

                connection.Close();
                //return "1";
            }

            string sqlExp = $"SELECT * FROM Posts " +
                            $"WHERE _id = '{id}'" +
                            $"AND Name = '{name}'" +
                            $"AND Description = '{description}'" +
                            $"AND Cshtml = '{cshtml}'";

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
                    if(reader.HasRows == false)
                    {
                        return "0";
                    }
                }
            }
            return "0";
        }


        public static string RemovePost(int id)
        {
            //удаляем
            using (var connection = new SqliteConnection($"Data Source={Db}"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"DELETE FROM Posts" +
                                      $" WHERE _id = {id}";
                command.ExecuteNonQuery();

                connection.Close();
            }

            //удалилось или нет
            //string sqlExp = $"SELECT * FROM Posts " +
            //                $"WHERE Name = \"{name}\"" +
            //                $"AND Description = \"{description}\"" +
            //                $"AND Cshtml = \"{cshtml}\"";
            string sqlExp = "SELECT * FROM Posts WHERE _id = '{id}'";

            using (var connection = new SqliteConnection($"Data Source={Db}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExp, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        connection.Close();

                        return "0";
                    }
                    if(reader.HasRows == false)
                    {
                        connection.Close();
                        return "1";
                    }
                }
            }
            return "0";
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

        //0- операция не успешна\забыл почту и пароль, 
        //1-вошел 2- забыл пароль
        public static string Login(string email, string password)
        {
            //проверяем есть ли в бд почта и пароль
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
                        connection.Close();
                        return "1";
                    }
                    if(reader.HasRows == false)//если введенная почта верна, а пароль нет
                    {
                        sqlExp = $"SELECT * FROM Users " +
                                 $"WHERE Email = \"{email}\"";
                        SqliteCommand commandEmail = new SqliteCommand(sqlExp, connection);
                        using (SqliteDataReader readerEmail = commandEmail.ExecuteReader())
                        {
                            if (readerEmail.HasRows)
                            {
                                connection.Close();
                                return "2";
                            }
                        }
                    }
                }
                connection.Close();//если нет почты и пароля или ошибка
                return "0";
            }
        }


        //0- операция не успешна или косяк, 1-зарегистрирован
        //2- уже зарегистрирован  3- почта используется - отправляем на вход\восстановление
        public static string Signup(string email, string password)
        {
            //проверка есть ли такие данные в таблице
            string sqlExp = $"SELECT * FROM Users " +
                            $"WHERE Email = \"{email}\"" +
                            $"AND Password = \"{password}\"";
            using (var connection = new SqliteConnection($"Data Source={Db}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(sqlExp, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    //проверяем есть ли почта и пароль
                    if (reader.HasRows)
                    {
                        connection.Close();
                        return "2";
                    }
                    //если нет - проверяем есть ли почта
                    if(reader.HasRows == false)
                    {
                        sqlExp = $"SELECT * FROM Users " +
                                 $"WHERE Email = \"{email}\"";
                        SqliteCommand commandEmail = new SqliteCommand(sqlExp, connection);
                        using (SqliteDataReader readerEmail = commandEmail.ExecuteReader())
                        {
                            if (readerEmail.HasRows)
                            {
                                return "3";
                            }
                            if(readerEmail.HasRows == false)// если нет создаем пользователя
                            {
                                sqlExp = $"INSERT INTO Users (Email, Password)" +
                                                      $" VALUES ('{email}', '{password}')";
                                SqliteCommand commandInsert = new SqliteCommand(sqlExp, connection);
                                commandInsert.ExecuteNonQuery();
                                
                                return "1";
                            }
                        }
                        connection.Close();
                    }
                    return "0";
                }
            }
        }
        public static RedirectResult ChangeSize(int size)
        {
            Program.IsBig = size;
            return new RedirectResult("/News");
        }
    }
}
