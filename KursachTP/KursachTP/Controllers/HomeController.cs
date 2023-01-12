using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using KursachTP.Models;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;

//namespace ValidationApp.Controllers
namespace KursachTP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Str(User user)
        {

            string conect = "server=localhost;user=root;database=dbkp2;password=root";
            MySqlConnection connection = new MySqlConnection(conect);
            connection.Open();

            string sql = "INSERT INTO USER(name,lastname,userdescription,birthday,pol,login," +
                "password,phone) VALUES (@user.name, @user.lastname,  @user.userdescription, @user.birthday, " +
                "@user.pol, @user.login, @user.password, @user.phone)";

            MySqlCommand comanda = new MySqlCommand(sql, connection);

            comanda.Parameters.AddWithValue("user.name", user.Name);
            comanda.Parameters.AddWithValue("user.lastname", user.LastName);
            comanda.Parameters.AddWithValue("user.userdescription", user.UserDescription);
            comanda.Parameters.AddWithValue("user.birthday", user.Birthday);
            comanda.Parameters.AddWithValue("user.pol", user.Pol);
            comanda.Parameters.AddWithValue("user.login", user.Login);
            comanda.Parameters.AddWithValue("user.password", user.Password);
            comanda.Parameters.AddWithValue("user.phone", user.Phone);

            comanda.ExecuteNonQuery();
            
            Users.users.Clear();

            sql = "SELECT name,lastname,userdescription,birthday,pol,login," +
                "password,phone FROM user";

            comanda = new MySqlCommand(sql, connection);

            MySqlDataReader reader = comanda.ExecuteReader();

            while (reader.Read())
            {
                Users.users.Add(new User(reader.GetString(0), reader.GetString(1),
                    reader.GetString(2), reader.GetString(3), reader.GetString(4),
                    reader.GetString(5), reader.GetString(6), reader.GetString(7)));
            }
            return View(Users.users);
            
        }
    }
}
