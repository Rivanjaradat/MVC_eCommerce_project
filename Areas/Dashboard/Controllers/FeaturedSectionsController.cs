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
    public class FeaturedSectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FeaturedSectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/FeaturedSections
        public async Task<IActionResult> Index()
        {
            return View(await _context.FeaturedSections.ToListAsync());
        }

        // GET: Dashboard/FeaturedSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featuredSection = await _context.FeaturedSections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (featuredSection == null)
            {
                return NotFound();
            }

            return View(featuredSection);
        }

        // GET: Dashboard/FeaturedSections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/FeaturedSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeaturedSection featuredSection, IFormFile Image)
        {
            if (ModelState.IsValid)
 {
     if (Image == null)
     {
         ModelState.AddModelError(nameof(Product.Image),"Image is required");
         return View(featuredSection);
     }
     var ImageName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
     if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/featuredSection")))
     {
         Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/featuredSection"));
     }
     var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/featuredSection", ImageName);
    await using (var stream = new FileStream(savePath, FileMode.Create))
     {
         await Image.CopyToAsync(stream);
     }
                featuredSection.Image = $"/img/featuredSection/{ImageName}";
     _context.Add(featuredSection);
     await _context.SaveChangesAsync();
     return RedirectToAction(nameof(Index));
 }
            return View(featuredSection);
        }

        // GET: Dashboard/FeaturedSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featuredSection = await _context.FeaturedSections.FindAsync(id);
            if (featuredSection == null)
            {
                return NotFound();
            }
            return View(featuredSection);
        }

        // POST: Dashboard/FeaturedSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Image,ButtonText,ButtonLink,IsActive")] FeaturedSection featuredSection)
        {
            if (id != featuredSection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(featuredSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeaturedSectionExists(featuredSection.Id))
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
            return View(featuredSection);
        }

        // GET: Dashboard/FeaturedSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featuredSection = await _context.FeaturedSections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (featuredSection == null)
            {
                return NotFound();
            }

            return View(featuredSection);
        }

        // POST: Dashboard/FeaturedSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var featuredSection = await _context.FeaturedSections.FindAsync(id);
            if (featuredSection != null)
            {
                _context.FeaturedSections.Remove(featuredSection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeaturedSectionExists(int id)
        {
            return _context.FeaturedSections.Any(e => e.Id == id);
        }
    }
}
