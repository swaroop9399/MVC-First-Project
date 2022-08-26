using Microsoft.AspNetCore.Mvc;
using MyProject1.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Numerics;

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
            return View (users);
        }

        [HttpGet]
        public IActionResult Data()
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
            return new JsonResult(users);
            
        }
        [HttpPost]
        public IActionResult Insert(User u)
        {
            //var user = JsonConvert.DeserializeObject<User>(requestData);
            //Console.WriteLine(user.Username);
            
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
            
            return Redirect("/Home/AllData");

        }

        public RedirectResult DeleteProcess(int id)
        {
           
            //int a = int.Parse(JsonConvert.DeserializeObject<int>(id).ToString());

            string Delete = "delete from Users where uid = @id";
            SqlConnection connection = new SqlConnection(conString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(Delete, connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            connection.Close();
            return Redirect("/Home/AllData");

        }

        public IActionResult EditProcess(int id)
        {
            Console.WriteLine(id);
            User u = null;
            string update = "select * from Users where uid=@id";
            SqlConnection connection = new SqlConnection(conString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(update, connection);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                u = new User((int)reader["uid"], reader["name"].ToString(), reader["email"].ToString(), reader["password"].ToString(), (DateTime)reader["date1"]);
            }
            connection.Close();
            
            return View(u);
        }

        [HttpPost]
        public RedirectResult Update(User u)
        {

            string update = "update Users set email=@email, name=@name, password=@password where uid = @id";
            SqlConnection connection = new SqlConnection(conString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(update, connection);
            cmd.Parameters.AddWithValue("@email", u.Email);
            cmd.Parameters.AddWithValue("@name", u.Username);
            cmd.Parameters.AddWithValue("@password", u.Password);
            cmd.Parameters.AddWithValue("@id", u.U_id);
            int i = cmd.ExecuteNonQuery();
            Console.WriteLine("Row affected " + u.Username);
            connection.Close();
            return Redirect("/Home/AllData");
        }
    }
}