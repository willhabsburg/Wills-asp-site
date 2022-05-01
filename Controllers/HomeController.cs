using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WillCal.Models;

namespace WillCal.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(ThisMonth.getMonthText());
    }

    public IActionResult CalByDate(string? newDate) {
        ThisMonth.CalByMonth(newDate);
        return View(ThisMonth.getMonthText());
    }

    [HttpGet]
    public IActionResult AddEvent() {
        return View();
    }

    [HttpPost]
    public void AddEvent(CalData data) {
        DataAccess.AddEvent(data);
        Response.Redirect("/");
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
}
