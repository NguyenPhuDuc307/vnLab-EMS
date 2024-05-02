using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vnLab.Data;
using vnLab.Data.Entities;

namespace vnLab.Controllers
{
    public class SalarySheetsController : Controller
    {
        private readonly vnLabDbContext _context;

        public SalarySheetsController(vnLabDbContext context)
        {
            _context = context;
        }

        // GET: SalarySheets
        public async Task<IActionResult> Index()
        {
            var vnLabDbContext = _context.SalarySheets.Include(s => s.TimeSheet).ThenInclude(s => s!.User);
            return View(await vnLabDbContext.ToListAsync());
        }

        // GET: SalarySheets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salarySheet = await _context.SalarySheets
                .Include(s => s.TimeSheet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salarySheet == null)
            {
                return NotFound();
            }

            return View(salarySheet);
        }

        // GET: SalarySheets/Create
        public async Task<IActionResult> Create(int id)
        {
            SalarySheet salarySheet = new SalarySheet();
            var timeSheet = await _context.TimeSheets.Include(x => x.User).FirstOrDefaultAsync(m => m.Id == id);
            var contract = await _context.Contracts.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == timeSheet!.UserId);
            ViewData["TimeSheetId"] = new SelectList(_context.TimeSheets, "Id", "Id");
            salarySheet.TimeSheetId = timeSheet!.Id;
            salarySheet.LuongChinh = contract!.HeSoLuong * 4960000;
            double BHYT = contract!.User!.BHYT * contract!.HeSoLuong * 4960000;
            double BHXH = contract!.User!.BHXH * contract!.HeSoLuong * 4960000;
            salarySheet.DateCreated = DateTime.Now;
            salarySheet.NetSalary = salarySheet.LuongChinh + salarySheet.PhuCap + salarySheet.TienThuong - BHYT - BHXH;
            return View(salarySheet);
        }

        // POST: SalarySheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalarySheet salarySheet)
        {
            if (ModelState.IsValid)
            {
                salarySheet.Id = 0;
                var timeSheet = await _context.TimeSheets.Include(x => x.User).FirstOrDefaultAsync(m => m.Id == salarySheet.TimeSheetId);
                var contract = await _context.Contracts.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == timeSheet!.UserId);
                double BHYT = contract!.User!.BHYT * contract!.HeSoLuong * 4960000;
                double BHXH = contract!.User!.BHXH * contract!.HeSoLuong * 4960000;
                salarySheet.DateCreated = DateTime.Now;
                salarySheet.NetSalary = salarySheet.LuongChinh + salarySheet.PhuCap + salarySheet.TienThuong - BHYT - BHXH;

                _context.Add(salarySheet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TimeSheetId"] = new SelectList(_context.TimeSheets, "Id", "Id", salarySheet.TimeSheetId);
            return View(salarySheet);
        }

        // GET: SalarySheets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salarySheet = await _context.SalarySheets.FindAsync(id);
            if (salarySheet == null)
            {
                return NotFound();
            }
            ViewData["TimeSheetId"] = new SelectList(_context.TimeSheets, "Id", "Id", salarySheet.TimeSheetId);
            return View(salarySheet);
        }

        // POST: SalarySheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SalarySheet salarySheet)
        {
            if (id != salarySheet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salarySheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalarySheetExists(salarySheet.Id))
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
            ViewData["TimeSheetId"] = new SelectList(_context.TimeSheets, "Id", "Id", salarySheet.TimeSheetId);
            return View(salarySheet);
        }

        // GET: SalarySheets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salarySheet = await _context.SalarySheets
                .Include(s => s.TimeSheet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salarySheet == null)
            {
                return NotFound();
            }

            return View(salarySheet);
        }

        // POST: SalarySheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salarySheet = await _context.SalarySheets.FindAsync(id);
            if (salarySheet != null)
            {
                _context.SalarySheets.Remove(salarySheet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalarySheetExists(int id)
        {
            return _context.SalarySheets.Any(e => e.Id == id);
        }
    }
}
