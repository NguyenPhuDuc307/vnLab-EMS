using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using vnLab.Data;
using vnLab.Data.Entities;
using vnLab.Models;

namespace vnLab.Controllers
{
    public class TimeSheetsController : Controller
    {
        private readonly vnLabDbContext _context;

        public TimeSheetsController(vnLabDbContext context)
        {
            _context = context;
        }

        // GET: TimeSheets
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["LastDayOfMothSoftParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("ldom") ? "ldom_desc" : "";
            ViewData["UserSortParm"] = String.IsNullOrEmpty(sortOrder) || sortOrder.Equals("user") ? "user_desc" : "user";

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var courses = from m in _context.TimeSheets.Include(t => t.User) select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(s => s.User!.Email!.Contains(searchString)
                || s.User!.FullName!.Contains(searchString)
                || s.User!.PhoneNumber!.Contains(searchString)
                || s.LastDayOfMoth.ToString()!.Contains(searchString));
            }

            courses = sortOrder switch
            {
                "ldom_desc" => courses.OrderBy(s => s.LastDayOfMoth),
                "user" => courses.OrderBy(s => s.User!.Email),
                "user_desc" => courses.OrderByDescending(s => s.User!.Email),
                _ => courses.OrderByDescending(s => s.LastDayOfMoth),
            };

            return View(PaginatedList<TimeSheet>.Create(await courses.ToListAsync(), pageNumber ?? 1, 20));
        }

        // GET: TimeSheets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSheet = await _context.TimeSheets
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeSheet == null)
            {
                return NotFound();
            }

            var timeSheetDetails = await _context.TimeSheetDetails.Include(t => t.TimeSheet).Where(t => t.TimeSheetId == id).OrderByDescending(x => x.TimeCheckIn).ToListAsync();
            ViewData["TimeSheetDetails"] = timeSheetDetails;
            return View(timeSheet);
        }

        // GET: TimeSheets/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: TimeSheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TimeSheet timeSheet)
        {
            if (ModelState.IsValid)
            {
                DateTime lastDayOfMonthCheck = new DateTime(timeSheet.LastDayOfMoth!.Value.Year, timeSheet.LastDayOfMoth!.Value.Month, DateTime.DaysInMonth(timeSheet.LastDayOfMoth!.Value.Year, timeSheet.LastDayOfMoth!.Value.Month));
                bool exists = await _context.TimeSheets
                .AnyAsync(t => t.LastDayOfMoth!.Value.Date == lastDayOfMonthCheck.Date);

                if (!exists)
                {
                    var users = await _context.Users.ToListAsync();
                    DateTime lastDayOfMonth = new DateTime(timeSheet.LastDayOfMoth!.Value.Year, timeSheet.LastDayOfMoth!.Value.Month, DateTime.DaysInMonth(timeSheet.LastDayOfMoth!.Value.Year, timeSheet.LastDayOfMoth!.Value.Month));
                    timeSheet.LastDayOfMoth = lastDayOfMonth;

                    foreach (var user in users)
                    {
                        TimeSheet t = new TimeSheet
                        {
                            LastDayOfMoth = lastDayOfMonth,
                            TongNgayCong = 0,
                            NghiCoPhep = 0,
                            NghiKhongPhep = 0,
                            UserId = user.Id
                        };
                        _context.Add(t);
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", timeSheet.UserId);
            return View(timeSheet);
        }

        // GET: TimeSheets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSheet = await _context.TimeSheets.Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeSheet == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", timeSheet.UserId);
            return View(timeSheet);
        }

        // POST: TimeSheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TimeSheet timeSheet)
        {
            if (id != timeSheet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeSheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeSheetExists(timeSheet.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", timeSheet.UserId);
            return View(timeSheet);
        }

        // GET: TimeSheets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSheet = await _context.TimeSheets
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeSheet == null)
            {
                return NotFound();
            }

            return View(timeSheet);
        }

        // POST: TimeSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeSheet = await _context.TimeSheets.FindAsync(id);
            if (timeSheet != null)
            {
                _context.TimeSheets.Remove(timeSheet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeSheetExists(int id)
        {
            return _context.TimeSheets.Any(e => e.Id == id);
        }
    }
}
