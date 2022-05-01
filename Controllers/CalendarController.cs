using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WillCal.Models;

namespace WillCal.Controllers;

public class CalendarController : Controller
{
    private readonly ILogger<CalendarController> _logger;

    public CalendarController(ILogger<CalendarController> logger)
    {
        _logger = logger;
    }

    public void Index()
    {
        Response.Redirect("/Calendar/CalByDate");
    }

    public IActionResult CalByDate(string? newDate) {
        ThisMonth.CalByMonth(newDate);
        return View(ThisMonth.getMonthText());
    }

    [HttpGet]
    public IActionResult AddEvent(string? selDate) {
        if(selDate == null)
            return View();
        else {
            string[] tmp = new string[] {selDate.Substring(0,4) + "-" + selDate.Substring(4,2) + "-" + selDate.Substring(6,2), ""};
            //Console.WriteLine(tmp);
            return View(tmp);
        }
    }

    [HttpPost]
    public void AddEvent(CalData data) {
        DataAccess.AddEvent(data);
        Response.Redirect("/Calendar/CalByDate");
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
