using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTPTPMQL.Models;

namespace BTPTPMQL.Controllers
{
    public class DaiLyController : Controller
    {
        private readonly ILogger<DaiLyController> _logger;

        public DaiLyController(ILogger<DaiLyController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(DaiLy dl)
        {
            string strOutput = "Mã đại lý " + dl.MaDaiLy + " Tên Đại Lý: " + dl.TenDaiLy + " Địa chỉ: " + dl.DiaChi + " Điện thoại: " + dl.DienThoai + " Người đại diện: " + dl.NguoiDaiDien + " Mã hệ thống phân phối: " + dl.MaHTPP;
            ViewBag.message = strOutput;
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