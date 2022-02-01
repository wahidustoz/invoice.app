using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using invoice.app.Models;
using Microsoft.AspNetCore.Authorization;
using invoice.app.Services;

namespace invoice.app.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly QueueClientService _que;

    public HomeController(
        ILogger<HomeController> logger,
        QueueClientService queue)
    {
        _logger = logger;
        _que = queue;
    }

    [Authorize]
    public IActionResult Index()
    {
        _que.SendMessage("Hammsi ok");
        return View();
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
