using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTPTPMQL.Models;

namespace BTPTPMQL.Controllers
{
    public class HeThongPhanPhoiController : Controller
    {
        private readonly ILogger<HeThongPhanPhoiController> _logger;

        public HeThongPhanPhoiController(ILogger<HeThongPhanPhoiController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(HeThongPhanPhoi htp)
        {
            string strOutput = "Mã hệ thống phân phối: " + htp.MaHTPP + " Tên hệ thống phân phối: " + htp.TenHTPP ;
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