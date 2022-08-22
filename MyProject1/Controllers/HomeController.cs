using Microsoft.AspNetCore.Mvc;
using MyProject1.Models;
using System;
using System.Data.SqlClient;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace MyProject1.Controllers
{
    public class HomeController : Controller
    {
        public string conString = "Data Source = LTP_RD_411; Initial Catalog = test; Integrated Security = True;";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllData()
        {
            List<User> users = new List<User>();
            string select = "select * from Users";
            SqlConnection connection = new SqlConnection(conString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(select, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User((int)reader["uid"], reader["name"].ToString(), reader["email"].ToString(), reader["password"].ToString(), (DateTime)reader["date1"]));

            }
            cmd.Dispose();
            connection.Close();
            return View(users);
        }
        [HttpPost]
        public RedirectResult Insert(User u)
        {
            Console.WriteLine(u);
            DateTime localDate = DateTime.Now;
            string Insert = "Insert into Users(name, email, password, date1) values(@name, @email, @password, @date1)";
            SqlConnection connection = new SqlConnection(conString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(Insert, connection);
            cmd.Parameters.AddWithValue("@name", u.Username);
            cmd.Parameters.AddWithValue("@email", u.Email);
            cmd.Parameters.AddWithValue("@password", u.Password);
            cmd.Parameters.AddWithValue("@date1", localDate);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();

            return Redirect("/");

        }

    }
}