using Microsoft.AspNetCore.Mvc;
using BTPTPMQL.Models;
using System.Diagnostics;

namespace BTPTPMQL.Controllers
{
    public class BMIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(BMI bmi)
        {
            float height = bmi.Height;
            float weight = bmi.Weight;
            float bmiValue = weight / ((height * height)/10000);
            string strOutput = "Chiều cao: " + height + " cm, Cân nặng: " + weight + " kg, Chỉ số BMI: " + bmiValue;
            if (bmiValue < 18.5)
            {
                strOutput += " (Thiếu cân)";
            }
            else if (bmiValue < 24.9)
            {
                strOutput += " (Bình thường)";
            }
            else if (bmiValue < 29.9)
            {
                strOutput += " (Thừa cân)";
            }
            else if (bmiValue <34.9)
            {
                strOutput += " (Béo phì 1)";
            }
            else 
            {
                strOutput += " (Béo phì 2)";
            }
            ViewBag.infoBMI = strOutput;
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

