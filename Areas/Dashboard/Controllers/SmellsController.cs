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
    public class SmellsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SmellsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/Smells
        public async Task<IActionResult> Index()
        {
            return View(await _context.Smells.ToListAsync());
        }

        // GET: Dashboard/Smells/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smell = await _context.Smells
                .FirstOrDefaultAsync(m => m.Id == id);
            if (smell == null)
            {
                return NotFound();
            }

            return View(smell);
        }

        // GET: Dashboard/Smells/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Smells/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Smell smell)
        {
            if (ModelState.IsValid)
            {
                _context.Add(smell);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(smell);
        }

        // GET: Dashboard/Smells/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smell = await _context.Smells.FindAsync(id);
            if (smell == null)
            {
                return NotFound();
            }
            return View(smell);
        }

        // POST: Dashboard/Smells/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Smell smell)
        {
            if (id != smell.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(smell);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SmellExists(smell.Id))
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
            return View(smell);
        }

        // GET: Dashboard/Smells/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var smell = await _context.Smells
                .FirstOrDefaultAsync(m => m.Id == id);
            if (smell == null)
            {
                return NotFound();
            }

            return View(smell);
        }

        // POST: Dashboard/Smells/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var smell = await _context.Smells.FindAsync(id);
            if (smell != null)
            {
                _context.Smells.Remove(smell);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SmellExists(int id)
        {
            return _context.Smells.Any(e => e.Id == id);
        }
    }
}
