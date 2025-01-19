using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectB.Controllers;
using ProjectB.Data;
using ProjectB.Models;
using System.Text;

public class LessonsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public LessonsController(ApplicationDbContext context, ILogger<HomeController> logger)
    {

        Console.OutputEncoding = Encoding.UTF8;
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

    //public async Task<IActionResult> Index()
    //{
    //    var authCheck = CheckAuthorization();
    //    if (authCheck != null) return authCheck;

    //    var lessons = await _context.Lessons
    //        .Include(l => l.User)
    //        .ToListAsync();

    //    var viewModel = new LessonsViewModel
    //    {
    //        LessonsList = lessons
    //    };

    //    return View(viewModel);
    //}

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LessonsViewModel model)
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (!userId.HasValue)
        {
            return RedirectToAction("SignIn", "Home");
        }

        if (!ModelState.IsValid)
        {
            // Return to the view with validation errors
            var lessons = await _context.Lessons
                .Include(l => l.User)
                .ToListAsync();

            model.LessonsList = lessons;
            TempData["ErrorMessage"] = "Please correct the highlighted errors.";
            return View("~/Views/Home/Lessons.cshtml", model);
        }

        try
        {
            var lesson = model.NewLesson;
            lesson.UserId = userId.Value;
            lesson.CreatedAt = DateTime.UtcNow;

            _context.Add(lesson);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Lesson created successfully!";
            return RedirectToAction("Lessons", "Home");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a lesson");
            TempData["ErrorMessage"] = "An unexpected error occurred. Please try again.";
            return RedirectToAction("Lessons", "Home");
        }
    }

    public async Task<IActionResult> LessonDetails(int? id)
    {
        var authCheck = CheckAuthorization();
        if (authCheck != null) return authCheck;

        if (id == null)
            return NotFound();

        var lesson = await _context.Lessons
            .Include(l => l.User)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (lesson == null)
            return NotFound();

        ViewBag.CanEdit = lesson.UserId == HttpContext.Session.GetInt32("UserId").Value;
        return View(lesson);
    }

    public IActionResult ForLove()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Lessons lesson)
    {
        var authCheck = CheckAuthorization();
        if (authCheck != null) return authCheck;

        if (id != lesson.Id)
            return NotFound();

        var existingLesson = await _context.Lessons.FindAsync(id);
        if (existingLesson == null || existingLesson.UserId != HttpContext.Session.GetInt32("UserId").Value)
        {
            return Forbid();
        }

        if (ModelState.IsValid)
        {
            try
            {
                existingLesson.Title = lesson.Title;
                existingLesson.Description = lesson.Description;
                existingLesson.VideoUrl = lesson.VideoUrl;
                existingLesson.DocumentUrl = lesson.DocumentUrl;
                existingLesson.TextContent = lesson.TextContent;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LessonExists(lesson.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction("Lessons", "Home");
        }
        return View(lesson);
    }

    private bool LessonExists(int id)
    {
        return _context.Lessons.Any(e => e.Id == id);
    }
}