using Nhom10.Data;
using Nhom10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Nhom10.Controllers
{
    public class QLSPController : Controller
    {
        //Khai bao DBContext de lam viec voi Database
        private readonly ApplicationDbContext _context;
        public QLSPController (ApplicationDbContext context)
        {
            _context = context;
        }
        //Action tra ve view hien thi danh sach QLSP
        public async Task<IActionResult> Index()
        {
            var model = await _context.QLSP.ToListAsync();
            return View(model);
        }
        //Action tra ve view them moi sinh vien
        public IActionResult Create()
        {
            return View();
        }
        
        //Action xu ly du lieu sinh vien gui len tu view va luu vao csdl
        [HttpPost]
        public async Task<IActionResult> Create(QLSP fac)
        {
            if(ModelState.IsValid)
            {
                _context.Add(fac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fac);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null)
            {
               return NotFound();
            }
            var student = await _context.QLSP.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("QLSPID,QLSPName")] QLSP fac)
        {
            if (id != fac.QLSPID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QLSPExists(fac.QLSPID))
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
            return View(fac);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var fac = await _context.QLSP
                .FirstOrDefaultAsync(m => m.QLSPID == id);
            if (fac == null)
            {
                return NotFound();
            }
            return View(fac);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var fac = await _context.QLSP.FindAsync(id);
            _context.QLSP.Remove(fac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool QLSPExists(string id)
        {
            return _context.QLSP.Any(e => e.QLSLID == id);
        }
    }
}