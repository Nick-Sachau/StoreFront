using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;

namespace StoreFront.UI.MVC.Controllers
{
    public class TrainerDetailsController : Controller
    {
        private readonly StoreFrontContext _context;

        public TrainerDetailsController(StoreFrontContext context)
        {
            _context = context;
        }

        // GET: TrainerDetails
        public async Task<IActionResult> Index()
        {
              return View(await _context.TrainerDetails.ToListAsync());
        }

        // GET: TrainerDetails/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TrainerDetails == null)
            {
                return NotFound();
            }

            var trainerDetail = await _context.TrainerDetails
                .FirstOrDefaultAsync(m => m.TrainerId == id);
            if (trainerDetail == null)
            {
                return NotFound();
            }

            return View(trainerDetail);
        }

        // GET: TrainerDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainerDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainerId,FirstName,LastName,Address,City,State,Zip,Phone")] TrainerDetail trainerDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainerDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainerDetail);
        }

        // GET: TrainerDetails/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TrainerDetails == null)
            {
                return NotFound();
            }

            var trainerDetail = await _context.TrainerDetails.FindAsync(id);
            if (trainerDetail == null)
            {
                return NotFound();
            }
            return View(trainerDetail);
        }

        // POST: TrainerDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TrainerId,FirstName,LastName,Address,City,State,Zip,Phone")] TrainerDetail trainerDetail)
        {
            if (id != trainerDetail.TrainerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainerDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerDetailExists(trainerDetail.TrainerId))
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
            return View(trainerDetail);
        }

        // GET: TrainerDetails/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TrainerDetails == null)
            {
                return NotFound();
            }

            var trainerDetail = await _context.TrainerDetails
                .FirstOrDefaultAsync(m => m.TrainerId == id);
            if (trainerDetail == null)
            {
                return NotFound();
            }

            return View(trainerDetail);
        }

        // POST: TrainerDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TrainerDetails == null)
            {
                return Problem("Entity set 'StoreFrontContext.TrainerDetails'  is null.");
            }
            var trainerDetail = await _context.TrainerDetails.FindAsync(id);
            if (trainerDetail != null)
            {
                _context.TrainerDetails.Remove(trainerDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainerDetailExists(string id)
        {
          return _context.TrainerDetails.Any(e => e.TrainerId == id);
        }
    }
}
