using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.AspNetCore.Http;

namespace project
{
    public class Program
    {
        public static   string  SessionKeyName  = "�����";
        public static   bool    IsLogged        = false;
        public static   bool    IsAdmin         = false;
        public static   int     tempPage        = -1;
        public static   string  temp            = "";
        public static   string  openedPage      = "�������";
        public const    string  Db              = "project.db";


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

                //command.CommandText = "Drop table Users";
                //command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE IF NOT EXISTS Users(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
                    " Email TEXT NOT NULL," +
                    " Password TEXT NOT NULL," +
                    " IsAdmin TEXT DEFAULT 0 NOT NULL )";

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
                //string cshtml = "<!DOCTYPE html><html><head></head><body><h1>������������ �������</h1><p>������� 1, ����� ��������� ��� �1, �������� ������ ������� ��������� �� ���������� ����������� �������.</p> <img src=https://imgtest.mir24.tv/uploaded/images/crops/2023/July/870x489_0x259_detail_crop_20230720121303_bb7da4e9_e4eb3f5ef3303ee23c6c7827fec55723597f0c7a88cfd13cf9bd380568ccfda7.jpg/><p>������� 1 ������������ ����� ������� ���������� ���������� � ����� ��������, ��� ����������, �������� � ������������������. � ��� ��������� ������������������ �������� ������� � ������� ��������, ���������� ������������ ��������� � ������ ������������ ���������. ������� 1 ���������� �������������� ����������� �� ����� ���� ��������� ����� ��������, ���������� � ���������� ���������.</p></body></html>";
                //command.CommandText = $"INSERT INTO Posts (Name, Description, Cshtml, Title) VALUES (\"{name}\", \"{desc}\", \"{cshtml}\", \"{title}\")";
                //command.ExecuteNonQuery();


                //string name = "����� ����� �������� �������";
                //string desc = "����� ����� ������� �������� ������� �� ������� ���� ����";
                //string title = "�������";
                //string cshtml = "<img src=https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTEPbLj6f4c4xCjA6Yj80539-zmbqPpd57YXw&usqp=CAU />";
                //command.CommandText = $"INSERT INTO Posts (Name, Description, Cshtml, Title) VALUES (\"{name}\", \"{desc}\", \"{cshtml}\", \"{title}\")";
                //command.ExecuteNonQuery();

                connection.Close();

            }

            app.UseSession();
            app.Run();
        }
    }
}