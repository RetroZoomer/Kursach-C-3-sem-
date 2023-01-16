using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using KursachTP.Models;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;
using KursachTP.DAO;

//namespace ValidationApp.Controllers
namespace KursachTP.Controllers
{
    public class HomeController : Controller
    {
        WorkDAO dataDao = new WorkDAO();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Zapas()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            //Пока что Стартовая страница
            return View(dataDao.Record(1,0,0,null));
        }
        public IActionResult LogIn()
        {
            //Пока что Стартовая страница
            return View("LogIn");
        }

        public IActionResult NewPerson(User user)
        {
            //Создание нового пользователя
            dataDao.GetPerson(user);
            return View("Index", dataDao.Record(1, 0, 0, null));
        }

        public IActionResult CreatePerson(User user)
        {
            return View("Reg", user);
            //Ссылка на создание нового знатока
        }

        public IActionResult DeletePerson(int id)
        {
            dataDao.DeleteById(id);
            return View("Index", dataDao.Record(1, 0, 0, null));
            //Удаление пользователя
        }

        public IActionResult EditPerson(int id)
        {
            return View("UpdateUser", dataDao.UserInfo(id));
            // Ссылка на страницу редактуры
        }

        public IActionResult UpdatePerson(User user)
        // Редактура
        {
            dataDao.UpZn(user);
            return View("Index", dataDao.Record(1, 0, 0, null));
        }

        public IActionResult InfoPerson(int id)

        {
            // Подробный Вывод
            return View("InfoUserView", dataDao.UserInfo(id));
        }
        public IActionResult IndexID(int usersid, int usersid2)
        {
            //Вывод пользователей от определенного id
            return View("Index", dataDao.Record(2, usersid, usersid2, null));
        }
        public IActionResult IndexName(string namesuser)
        {
            //Вывод пользователей по имени
            return View("Index" , dataDao.Record(3,0,0,namesuser));
        }

        public IActionResult PostView()
        {
            //Страница постов
            return View("PostView", dataDao.ListPost());
        }

        public IActionResult NewPost(Post post)
        {
            //Создание нового поста
            dataDao.GetPost(post);
            return View("PostView", dataDao.ListPost());
        }

        public IActionResult CreatePost(Post post)
        {
            return View("InsertPost", post);
            //Ссылка на создание нового поста
        }
        public IActionResult DeletePost(int id)
        {
            dataDao.DeleteByIdPost(id);
            return View("PostView", dataDao.ListPost());
            //Удаление поста
        }
        public IActionResult EditPost(int id)
        {
            return View("UpdatePost", dataDao.PostInfo(id));
            // Ссылка на страницу редактуры
        }

        public IActionResult UpdatePost(Post post)
        // Редактура
        {
            dataDao.UpPost(post);
            return View("PostView", dataDao.ListPost());
        }
        public IActionResult InfoPost(int id)

        {
            // Подробный Вывод return View("InfoUserView", dataDao.UserInfo(id));
            return View("InfoPostView", dataDao.PostInfo(id));
        }

        /*public IActionResult Create()
        {
            return View();
        }*/
        /*
        [HttpPost]
        public IActionResult Str(User user)
        {/*

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
        }*/


    }
}
