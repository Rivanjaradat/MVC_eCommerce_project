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
    public class SliderImagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SliderImagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/SliderImages
        public async Task<IActionResult> Index()
        {
            return View(await _context.SliderImages.ToListAsync());
        }

        // GET: Dashboard/SliderImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderImage = await _context.SliderImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sliderImage == null)
            {
                return NotFound();
            }

            return View(sliderImage);
        }

        // GET: Dashboard/SliderImages/Create
        public IActionResult Create()
        {
            return View();
        }

        // Fix for the error CS1061: 'SliderImage' does not contain a definition for 'CopyToAsync'  
        // The issue is that the `CopyToAsync` method is being called on the `sliderImage` object,  
        // which is of type `SliderImage`. However, `CopyToAsync` is a method of `IFormFile`,  
        // and the correct object to call it on is the `Image` parameter.  

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderImage sliderImage, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image == null)
                {
                    ModelState.AddModelError(nameof(sliderImage.Image), "Image is required");
                    return View(sliderImage);
                }

                var ImageName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/SliderImages")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/SliderImages"));
                }

                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/SliderImages", ImageName);
                await using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream); 
                }

                sliderImage.Image = $"/img/SliderImages/{ImageName}";
                _context.Add(sliderImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(sliderImage);
        }

        // GET: Dashboard/SliderImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderImage = await _context.SliderImages.FindAsync(id);
            if (sliderImage == null)
            {
                return NotFound();
            }
            return View(sliderImage);
        }

        // POST: Dashboard/SliderImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Image")] SliderImage sliderImage)
        {
            if (id != sliderImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sliderImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderImageExists(sliderImage.Id))
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
            return View(sliderImage);
        }

        // GET: Dashboard/SliderImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderImage = await _context.SliderImages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sliderImage == null)
            {
                return NotFound();
            }

            return View(sliderImage);
        }

        // POST: Dashboard/SliderImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sliderImage = await _context.SliderImages.FindAsync(id);
            if (sliderImage != null)
            {
                _context.SliderImages.Remove(sliderImage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderImageExists(int id)
        {
            return _context.SliderImages.Any(e => e.Id == id);
        }
    }
}
