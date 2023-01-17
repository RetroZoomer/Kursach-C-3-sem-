using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KursachTP.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace KursachTP.Controllers
{
    public class AccountController : Controller
    {
        /*private ApplicationContext _context;
        public AccountController(ApplicationContex context)
        {
            _context = context;
        }*/

       /* [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    user = new User
                    {
                        Email = model.Email,
                        Password = model.Password,
                        Year = model.Year,
                        City = model.City,
                        Company = model.Company
                    };
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();*/

        /*
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        */
        
    }
}
