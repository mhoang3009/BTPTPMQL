using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTPTPMQL.Models.Process;
using BTPTPMQL.Models;
using BTPTPMQL.Data;
using Microsoft.EntityFrameworkCore;

using System.IO;

namespace BTPTPMQL.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        private ExcelProcess _excelProcess = new ExcelProcess();

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _context.Person.ToListAsync();
            return View(model);
        }
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Address")] Person person)
        {
            if (id !=person.PersonId)
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
            if (file!= null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("","Please choose an excel file to upload!");
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
    // Tạo đối tượng mới từ Excel
    var ps = new Person
    {
        PersonId = dt.Rows[i][0].ToString(),
        FullName = dt.Rows[i][1].ToString(),
        Address = dt.Rows[i][2].ToString()
    };

    // Kiểm tra xem PersonId đã tồn tại trong database chưa
    var existingPerson = await _context.Person.FindAsync(ps.PersonId);
    if (existingPerson == null) 
    {
        _context.Add(ps); // Thêm mới nếu chưa tồn tại
    }
    else
    {
        // Cập nhật thông tin nếu đã tồn tại
        existingPerson.FullName = ps.FullName;
        existingPerson.Address = ps.Address;
        _context.Update(existingPerson);
    }
}

await _context.SaveChangesAsync();
return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
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
    