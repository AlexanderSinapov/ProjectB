using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectB.Data;
using ProjectB.Models;

namespace ProjectB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        private IActionResult CheckAuthorization()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("SignIn", "Home");
            }
            return null;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }

        [HttpGet]
        public IActionResult Credits()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Lessons()
        {
            var lessons = await _context.Lessons
                .Include(l => l.User)
                .ToListAsync();

            var viewModel = new LessonsViewModel
            {
                LessonsList = lessons,
                NewLesson = new Lessons()
            };

            return View(viewModel);
        }

        //[HttpGet]
        //public async Task<IActionResult> LessonDetails(int? id)
        //{
        //    var authCheck = CheckAuthorization();
        //    if (authCheck != null) return authCheck;

        //    if (id == null)
        //        return NotFound();

        //    var lesson = await _context.Lessons
        //        .Include(l => l.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);

        //    if (lesson == null)
        //        return NotFound();

        //    ViewBag.CanEdit = lesson.UserId == HttpContext.Session.GetInt32("UserId").Value;
        //    return View(lesson);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUp model)
        {
            _logger.LogInformation("SignUp method triggered.");
            _logger.LogInformation("Model Data: {@Model}", model);

            if (ModelState.IsValid)
            {
                _logger.LogInformation("Model State is valid.");
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Confirmation password and password do not match.");
                    return View(model);
                }

                var passwordHasher = new PasswordHasher<SignUp>();
                var hashedPassword = passwordHasher.HashPassword(model, model.Password);

                var user = new SignUp
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Username = model.Username,
                    Email = model.Email,
                    Password = hashedPassword,
                    ConfirmPassword = hashedPassword,
                    AccountType = model.AccountType,
                };

                _context.SignUps.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("SignIn");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignIn model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.SignUps.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                    return View(model);
                }

                var passwordHasher = new PasswordHasher<SignUp>();
                var result = passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

                if (result == PasswordVerificationResult.Failed)
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                    return View(model);
                }

                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("AccountType", user.AccountType.ToString());

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetCookieAcknowledged([FromBody] CookieChoiceModel choice)
        {
            HttpContext.Session.SetString("CookieAcknowledged", "true");
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
