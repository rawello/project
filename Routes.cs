using Microsoft.Data.Sqlite;
using project;

namespace project
{
    public class Routes
    {
        public const string Db = "project.db";

        public static int AddPost(string name, string cshtml)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={Db}"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO Posts (Name, Cshtml) VALUES ({name}, {cshtml})";
                    command.ExecuteNonQuery();

                    connection.Close();
                }
                return 1;
            }
            catch(Exception e)
            {
                return 0;
            }
        }


        public static int UpdatePost(int id, string name, string cshtml)
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


        public static int RemovePost(int id)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={Db}"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"DELETE FROM Posts WHERE ID={id}";
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

        //--------------------------------------------------------------------------------------

        public static int AddUser(string email, string password, bool isadmin)
        {
            try
            {
                using (var connection = new SqliteConnection($"Data Source={Db}"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"INSERT INTO Users (Email, Password, IsAdmin) VALUES ('{email}', '{password}', {isadmin})";
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


        public static int ChangeUser(int id, string email, string password, bool isadmin)
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
