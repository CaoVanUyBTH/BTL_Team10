using Nhom10.Data;
using Nhom10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Nhom10.Controllers
{
    public class QLDHController : Controller
    {
        //Khai bao DBContext de lam viec voi Database
        private readonly ApplicationDbContext _context;
        public QLDHController (ApplicationDbContext context)
        {
            _context = context;
        }
        //Action tra ve view hien thi danh sach QLDH
        public async Task<IActionResult> Index()
        {
            var model = await _context.QLDH.ToListAsync();
            return View(model);
        }
        //Action tra ve view them moi sinh vien
        public IActionResult Create()
        {
            return View();
        }
        
        //Action xu ly du lieu sinh vien gui len tu view va luu vao csdl
        [HttpPost]
        public async Task<IActionResult> Create(QLDH fac)
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
            var student = await _context.QLDH.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("QLDHID,QLDHName")] QLDH fac)
        {
            if (id != fac.QLDHID)
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
                    if (!QLDHExists(fac.QLDHID))
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
            var fac = await _context.QLDH
                .FirstOrDefaultAsync(m => m.QLDHID == id);
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
            var fac = await _context.QLDH.FindAsync(id);
            _context.QLDH.Remove(fac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool QLDHExists(string id)
        {
            return _context.QLDH.Any(e => e.QLDHID == id);
        }
    }
}