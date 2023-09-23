using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Http;

namespace project
{
    public class Program
    {
        public static string SessionKeyName = "�����";
        public static bool IsAdmin = false;
        public static int tempPage = -1;
        public static string openedPage = "�������";
        public const string Db = "project.db";


        [AllowAnonymous]
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
            });
            builder.Services.AddSession();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            using (var connection = new SqliteConnection($"Data Source={Db}"))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand();
                command.Connection = connection;
                

                command.CommandText = "CREATE TABLE IF NOT EXISTS Users(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
                    " Email TEXT NOT NULL," +
                    " Password TEXT NOT NULL," +
                    " IsAdmin BOOLEAN DEFAULT false NOT NULL )";

                command.ExecuteNonQuery();

                //command.CommandText = "DROP TABLE Posts";
                //command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE IF NOT EXISTS Posts(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
                    " Name TEXT NOT NULL," +
                    " Description TEXT NOT NULL," +
                    " Cshtml TEXT NOT NULL, " +
                    " Title TEXT NOT NULL )";

                command.ExecuteNonQuery();

                //string name = "����� ����� �������� �������";
                //string desc = "����� ����� ������� �������� ������� �� ������� ���� ����";
                //string title = "�������";
                //string cshtml = "<!DOCTYPE html><html><head></head><body><h1>������������ �������</h1><p>������� 1, ����� ��������� ��� �1, �������� ������ ������� ��������� �� ���������� ����������� �������. ������� 1 ������������ ����� ������� ���������� ���������� � ����� ��������, ��� ����������, �������� � ������������������. � ��� ��������� ������������������ �������� ������� � ������� ��������, ���������� ������������ ��������� � ������ ������������ ���������. ������� 1 ���������� �������������� ����������� �� ����� ���� ��������� ����� ��������, ���������� � ���������� ���������.</p></body></html>";
                //command.CommandText = $"INSERT INTO Posts (Name, Description, Cshtml, Title) VALUES (\"{name}\", \"{desc}\", \"{cshtml}\", \"{title}\")";
                //command.ExecuteNonQuery();

                connection.Close();

            }

            app.UseSession();
            app.Run();
        }
    }
}