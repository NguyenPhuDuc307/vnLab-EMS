using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vnLab.Data;
using vnLab.Data.Entities;

namespace vnLab.Controllers
{
    public class TimeSheetDetailsController : Controller
    {
        private readonly vnLabDbContext _context;

        public TimeSheetDetailsController(vnLabDbContext context)
        {
            _context = context;
        }

        // GET: TimeSheetDetails
        public async Task<IActionResult> Index()
        {
            var vnLabDbContext = _context.TimeSheetDetails.Include(t => t.TimeSheet).ThenInclude(t => t!.User);
            return View(await vnLabDbContext.ToListAsync());
        }

        // GET: TimeSheetDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSheetDetail = await _context.TimeSheetDetails
                .Include(t => t.TimeSheet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeSheetDetail == null)
            {
                return NotFound();
            }

            return View(timeSheetDetail);
        }

        // GET: TimeSheetDetails/Create
        public IActionResult Create(int Id)
        {
            ViewData["TimeSheetId"] = Id;
            return View();
        }

        // POST: TimeSheetDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TimeSheetDetail timeSheetDetail)
        {
            if (timeSheetDetail.TimeCheckOut.HasValue && timeSheetDetail.TimeCheckIn.Date != timeSheetDetail.TimeCheckOut.Value.Date)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                bool timeCheckInExists = await _context.TimeSheetDetails.AnyAsync(t => t.TimeCheckIn.Date == timeSheetDetail.TimeCheckIn.Date &&
                                                                         t.TimeCheckIn.Month == timeSheetDetail.TimeCheckIn.Month &&
                                                                         t.TimeCheckIn.Year == timeSheetDetail.TimeCheckIn.Year &&
                                                                         t.TimeSheetId == timeSheetDetail.TimeSheetId);
                if (timeCheckInExists)
                {
                    return RedirectToAction("Index", "TimeSheets");
                }
                timeSheetDetail.Id = 0;
                _context.Add(timeSheetDetail);
                if (timeSheetDetail.TimeCheckOut != null)
                {
                    var timeSheet = await _context.TimeSheets.FindAsync(timeSheetDetail.TimeSheetId);
                    timeSheet!.TongNgayCong += 1;
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "TimeSheets");
            }
            return View(timeSheetDetail);
        }

        // GET: TimeSheetDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSheetDetail = await _context.TimeSheetDetails.FindAsync(id);
            if (timeSheetDetail == null)
            {
                return NotFound();
            }
            ViewData["TimeSheetId"] = new SelectList(_context.TimeSheets, "Id", "Id", timeSheetDetail.TimeSheetId);
            return View(timeSheetDetail);
        }

        // POST: TimeSheetDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TimeSheetId,TimeCheckIn,TimeCheckOut")] TimeSheetDetail timeSheetDetail)
        {
            if (id != timeSheetDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeSheetDetail);
                    if (timeSheetDetail.TimeCheckOut != null)
                    {
                        var timeSheet = await _context.TimeSheets.FindAsync(timeSheetDetail.TimeSheetId);
                        timeSheet!.TongNgayCong += 1;
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeSheetDetailExists(timeSheetDetail.Id))
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
            ViewData["TimeSheetId"] = new SelectList(_context.TimeSheets, "Id", "Id", timeSheetDetail.TimeSheetId);
            return View(timeSheetDetail);
        }

        // GET: TimeSheetDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSheetDetail = await _context.TimeSheetDetails
                .Include(t => t.TimeSheet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeSheetDetail == null)
            {
                return NotFound();
            }

            return View(timeSheetDetail);
        }

        // POST: TimeSheetDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeSheetDetail = await _context.TimeSheetDetails.FindAsync(id);
            if (timeSheetDetail != null)
            {
                _context.TimeSheetDetails.Remove(timeSheetDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeSheetDetailExists(int id)
        {
            return _context.TimeSheetDetails.Any(e => e.Id == id);
        }
    }
}
