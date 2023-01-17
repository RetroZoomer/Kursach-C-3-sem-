using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using KursachTP.Models;
using Microsoft.Extensions.Logging;
using KursachTP.DAO;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Policy = "OnlyForMicrosoft")]
        public IActionResult About()
        {
            return Content("Only for Microsoft employees");
        }
        [Authorize(Policy = "OnlyForLondon")]
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
        [Authorize(Policy = "OnlyForAdmin")]
        public IActionResult DeletePerson(int id)
        {
            dataDao.DeleteById(id);
            return View("Index", dataDao.Record(1, 0, 0, null));
            //Удаление пользователя
        }

        [Authorize]
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
        /*
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return View("Index");
        }


        public IActionResult Login(string returnURL)
        {
            ViewBag.ReturnURL = returnURL;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (dataDao.YesNoData(user))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Login), // Возможное место ошибки
                    new Claim(ClaimTypes.Locality, dataDao.GetRole(user))
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Login");


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                return Redirect("/Home");
            }
            else
            {
                return View();
            }
        }*/

        /*public IActionResult Create()
        {
            return View();
        }*/
        
    }
}
