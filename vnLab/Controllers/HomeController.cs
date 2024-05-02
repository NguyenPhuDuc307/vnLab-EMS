using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vnLab.Data;
using vnLab.Data.Entities;
using vnLab.Models;

namespace vnLab.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly vnLabDbContext _context;


    public HomeController(ILogger<HomeController> logger, vnLabDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index(string currentFilter, string searchString, int? pageNumber)
    {
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