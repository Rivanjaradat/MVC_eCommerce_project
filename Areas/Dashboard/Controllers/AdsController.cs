using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_eCommerce_project.Data;
using MVC_eCommerce_project.Models;

namespace MVC_eCommerce_project.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class AdsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/Ads
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ads.ToListAsync());
        }

        // GET: Dashboard/Ads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ads = await _context.Ads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ads == null)
            {
                return NotFound();
            }

            return View(ads);
        }

        // GET: Dashboard/Ads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Ads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,IsActive")] Ads ads)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ads);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ads);
        }

        // GET: Dashboard/Ads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ads = await _context.Ads.FindAsync(id);
            if (ads == null)
            {
                return NotFound();
            }
            return View(ads);
        }

        // POST: Dashboard/Ads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,IsActive")] Ads ads)
        {
            if (id != ads.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ads);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdsExists(ads.Id))
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
            return View(ads);
        }

        // GET: Dashboard/Ads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ads = await _context.Ads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ads == null)
            {
                return NotFound();
            }

            return View(ads);
        }

        // POST: Dashboard/Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ads = await _context.Ads.FindAsync(id);
            if (ads != null)
            {
                _context.Ads.Remove(ads);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdsExists(int id)
        {
            return _context.Ads.Any(e => e.Id == id);
        }
    }
}
