using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTPTPMQL.Models;
using BTPTPMQL.Data;
using Microsoft.EntityFrameworkCore;

namespace BTPTPMQL.Controllers
{
    public class HeThongPhanPhoiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HeThongPhanPhoiController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.HeThongPhanPhoi.ToListAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHTPP,TenHTPP")]HeThongPhanPhoi htp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(htp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(htp);
        
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(string id, [Bind("MaHTPP,TenHTPP")] HeThongPhanPhoi htp)
        {
            if (id !=htp.MaHTPP)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(htp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeThongPhanPhoiExists(htp.MaHTPP))
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
            return View(htp);
        }

        public async Task<IActionResult> Edit(string id)

        {
            if (id == null || _context.HeThongPhanPhoi == null)
            {
                return NotFound();
            }
            var htp = await _context.HeThongPhanPhoi.FindAsync(id);
            if (htp == null)
            {
                return NotFound();
            }
            return View(htp);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.HeThongPhanPhoi == null)
            {
                return NotFound();
            }
            var htp = await _context.HeThongPhanPhoi
                .FirstOrDefaultAsync(m => m.MaHTPP == id);
            if (htp == null)
            {
                return NotFound();
            }
            return View(htp);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.HeThongPhanPhoi == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HeThongPhanPhoi' is null.");
            }
            var htp = await _context.HeThongPhanPhoi.FindAsync(id);
            if (htp != null)
            {
                _context.HeThongPhanPhoi.Remove(htp);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Privacy()
        {
            return View();
        }

        private bool HeThongPhanPhoiExists(string id)
        {
            return _context.HeThongPhanPhoi.Any(e => e.MaHTPP == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}