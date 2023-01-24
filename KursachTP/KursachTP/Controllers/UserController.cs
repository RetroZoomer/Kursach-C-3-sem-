using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KursachTP.Controllers;
using KursachTP.DAO;
using KursachTP.Models;

namespace KursachTP.Controllers
{
    public class UserController : Controller
    {
        WorkDAO dataDao2 = new WorkDAO();
        // GET: UserController
        public ActionResult PostViewU()
        { // Посты для пользователя
            return View("PostViewU", dataDao2.ListPost(false));
        }
        
        public IActionResult ProfileU()
        {
            //Страница профиля
            //static ClaimsPrincipal.FindFirst(string);
            string namesuser = HttpContext.User.Identity.Name;
            return View("ProfileU", dataDao2.RecordOprName(namesuser));
        }
        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult WarningU(Warning warning, int id_post)
        {
            /* // Добавление в БД*/
            string nameAuthor = HttpContext.User.Identity.Name;
            int id_user = Convert.ToInt32(dataDao2.GetID(nameAuthor));
            dataDao2.GetWarning(warning,id_post,id_user);
            return View("WarningU");
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
