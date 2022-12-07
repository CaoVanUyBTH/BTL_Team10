using Nhom10.Data;
using Nhom10.Models;
using Nhom10.Models.Process;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Nhom10.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();
        public CustomerController (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }
      
        /*public async Task<IActionResult> Index()
        {
            var epl = await _context.Customers.ToListAsync();
            return View(epl);
        }*/
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Customer KH)
        {
            if(ModelState.IsValid)
            {
                _context.Add(KH);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(KH);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null)
            {
               return NotFound();
            }
            var cusloyee = await _context.Customers.FindAsync(id);
            if (cusloyee == null)
            {
                return NotFound();
            }
            return View(cusloyee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CusID,CusName")] Customer KH)
        {
            if (id != KH.CusID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(KH);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CusExists(KH.CusID))
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
            return View(KH);
        }

        private bool CusExists(object cusID)
        {
            throw new NotImplementedException();
        }

        private bool CusExists(string cusID)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var KH = await _context.Customers
                .FirstOrDefaultAsync(m => m.CusID == id);
            if (KH == null)
            {
                return NotFound();
            }
            return View(KH);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var KH = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(KH);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Upload(IFormFile file)
        {
            if (file!=null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension !=".xlsx")
                {
                    ModelState.AddModelError("", "Please choose excel file to upload!");
                }
                else
                {
                    var FileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", FileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream (filePath, FileMode.Create))
                    {
                        //save file to server
                        await file.CopyToAsync(stream);
                        //read dât from file and write to database
                        var dt= _excelProcess.ExcelToDataTable(fileLocation);
                        //using for loop...
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //create a new Emp
                            var cus = new Customer();
                            // set values for...
                            cus.CusID = dt.Rows[i][0].ToString();
                            cus.CusName = dt.Rows[i][1].ToString();
                            // add object to Context
                            _context.Customers.Add(cus);
                        }
                        //save to database 
                        await _context.SaveChangesAsync();
                        return RedirectToAction (nameof(Index));
                    }
                }
            }
            return View();
        }

        public bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.CusID == id);
        }

    }
}