using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTPTPMQL.Models;

namespace BTPTPMQL.Controllers
{
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index ( Person ps)
        {
            string strOutput = "Xin chào: " + ps.PersonID + "-" + ps.FullName + "-" + ps.Address;
            ViewBag.infoPerson = strOutput;
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
}