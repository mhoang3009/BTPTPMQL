using Microsoft.AspNetCore.Mvc;
using BTPTPMQL.Models;
using System.Diagnostics;

namespace BTPTPMQL.Controllers
{
    
    public class TinhDiemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(TinhDiem TD)
        {
            float DiemA = TD.DiemA;
            float DiemB = TD.DiemB;
            float DiemC = TD.DiemC;
            float TDValue = (DiemA*0.6f + DiemB*0.3f + DiemC*0.1f);
            string strOutput = "Điểm môn A: " + DiemA + ", Điểm môn B: " + DiemB + ", Điểm môn C: " + DiemC + ", Điểm tổng: " + TDValue;
            if (TDValue < 5.4)
            {
                strOutput +="D-Học lực trung bình yếu";
            }
            else if (TDValue < 6.9)
            {
                strOutput +="C-Học lực trung bình";
            }
            else if (TDValue < 8.4)
            {
                strOutput +="B-Học lực khá";
            }
            else 
            {
                strOutput +="A-Học lực giỏi";
            }
            ViewBag.Point = strOutput;
            return View();
        }

    }

}