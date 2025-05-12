using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTPTPMQL.Data;
using BTPTPMQL.Models;
using BTPTPMQL.Models.Process;
using System.Data;
using OfficeOpenXml;
using X.PagedList.Extensions;
using System.Diagnostics;


using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace BTPTPMQL.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        private ExcelProcess _excelProcess = new ExcelProcess();

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Person
        // public async Task<IActionResult> Index()
        // {
        //     return View(await _context.Person.ToListAsync());
        // }
        public ActionResult Index(int? page)
        {
            int pageSize = 3; // số mục mỗi trang
            int pageNumber = page ?? 1; // trang hiện tại

            var users = _context.Person.OrderBy(p => p.PersonId); // truy vấn danh sách

            return View(users.ToPagedList(pageNumber, pageSize));
        }

        // GET: Person/Details/5
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(p => p.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }
        public async Task<IActionResult> Download()
        {
            var fileName = Guid.NewGuid().ToString() + ".xlsx";
            using ExcelPackage excelPackage = new();
            ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");
            excelWorksheet.Cells["A1"].Value = "PersonID";
            excelWorksheet.Cells["B1"].Value = "FullName";
            excelWorksheet.Cells["C1"].Value = "Address";
            //get all person from database
            var personList = await _context.Person.ToListAsync();
            //fill data to worksheet
            excelWorksheet.Cells["A2"].LoadFromCollection(personList, true);

            var stream = new MemoryStream();
            excelPackage.SaveAs(stream);
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Address")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }
            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Person' is null.");
            }
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Upload()
        {
            var ps = await _context.Person.ToListAsync();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Please choose an excel file to upload!");
                }
                else
                {
                    //remame file when upload to server
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Upload/Excel", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to server
                        await file.CopyToAsync(stream);
                        // read data from excel file fill DataTable
                        var excelProcess = new ExcelProcess();
                        var dt = _excelProcess.ExcelToDataTable(filePath);
                        //using for loop to read data from dt
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var person = new Person()
                            {
                                PersonId = dt.Rows[i][0].ToString(),
                                FullName = dt.Rows[i][1].ToString(),
                                Address = dt.Rows[i][2].ToString()
                            };
                            _context.Add(person);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }
        private bool PersonExists(string id)
        {
            return (_context.Person?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    
}
    