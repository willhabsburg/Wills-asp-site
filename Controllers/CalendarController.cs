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

    public IActionResult AddTask() {
        return View(TaskAccess.getCurrentTask());
    }
    
    [HttpPost]
    public void AddTask(TaskData data) {
        Console.WriteLine("sdsdfafasdf");
        TaskAccess.AddEvent(data);
        Response.Redirect("/Calendar/CalByDate");
    }

    public IActionResult CalByDate(string? newDate) {
        ThisMonth.CalByMonth(newDate);
        return View();
    }

    public IActionResult CalWeek(string? newDate) {
        ThisWeek.CalByWeek(newDate);
        return View();
    }

    [HttpGet]
    public IActionResult AddEvent(string? selDate) {
        string[] tmp;
        if(selDate == null)
            tmp = new string[] {DateTime.Today.ToString("d")};
        else {
            tmp = new string[] {selDate.Substring(0,4) + "-" + selDate.Substring(4,2) + "-" + selDate.Substring(6,2), ""};
        }
        return View(tmp);
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
