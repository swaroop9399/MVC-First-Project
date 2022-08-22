namespace MyProject1.Models
{
    public class User
    {
        public int U_id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime RegisterDate { get; set; }

        public User(int u_id, string username, string email, string password, DateTime registerDate)
        {
            U_id = u_id;
            Username = username;
            Email = email;
            Password = password;
            RegisterDate = registerDate;
        }
        public User()
        {

        }
    }
}
