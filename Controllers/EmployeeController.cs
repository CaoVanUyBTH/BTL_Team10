using Nhom10.Data;
using Nhom10.Models;
using Nhom10.Models.Process;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Nhom10.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess = new ExcelProcess();
        public EmployeeController (ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }
      
        /*public async Task<IActionResult> Index()
        {
            var epl = await _context.Employees.ToListAsync();
            return View(epl);
        }*/
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee nv)
        {
            if(ModelState.IsValid)
            {
                _context.Add(nv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nv);
        }
        public async Task<IActionResult> Edit(string id)
        {
            if(id == null)
            {
               return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("EmpID,EmpName")] Employee nv)
        {
            if (id != nv.EmpID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nv);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpExists(nv.EmpID))
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
            return View(nv);
        }

        private bool EmpExists(string empID)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var nv = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmpID == id);
            if (nv == null)
            {
                return NotFound();
            }
            return View(nv);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nv = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(nv);
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
                            var emp = new Employee();
                            // set values for...
                            emp.EmpID = dt.Rows[i][0].ToString();
                            emp.EmpName = dt.Rows[i][1].ToString();
                            emp.Address = dt.Rows[i][2].ToString();
                            // add object to Context
                            _context.Employees.Add(emp);
                        }
                        //save to database 
                        await _context.SaveChangesAsync();
                        return RedirectToAction (nameof(Index));
                    }
                }
            }
            return View();
        }

        public bool EmployeeExists(string id)
        {
            return _context.Employees.Any(e => e.EmpID == id);
        }

    }
}