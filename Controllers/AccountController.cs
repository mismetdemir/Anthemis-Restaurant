using Anthemis.Data;
using Anthemis.Models;
using Microsoft.AspNetCore.Mvc;

namespace Anthemis.Controllers{
    public class AccountController : Controller{
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context){
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(){
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Username"))){
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model){
            if (!ModelState.IsValid){
                return View(model);
            }

            var user = _context.Users.FirstOrDefault(u =>
                u.Username == model.Username &&
                u.Password == model.Password
            );

            if (user == null){
                ViewBag.ErrorMessage = "Invalid username or password.";
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register(){
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Username"))){
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model){
            if (!ModelState.IsValid){
                return View(model);
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Username == model.Username || u.Email == model.Email);

            if (existingUser != null){
                ViewBag.ErrorMessage = "Username or email already exists.";
                return View(model);
            }

            bool isFirstUser = !_context.Users.Any();

            var user = new User{
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                Role = isFirstUser ? "Admin" : "User"
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Login", "Account");
        }

        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}