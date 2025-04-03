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
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dashboard/Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: Dashboard/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Dashboard/Products/Create
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Product product,IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image == null)
                {
                    ModelState.AddModelError(nameof(Product.Image),"Image is required");
                    return View(product);
                }
                var ImageName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/products")))
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/products"));
                }
                var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/products", ImageName);
               await using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
                product.Image = $"/img/products/{ImageName}";
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Dashboard/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Dashboard/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Product product, IFormFile Image)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
                    if (Image != null)
                    {
                        var ImageName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
                        if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/products")))
                        {
                            Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/products"));
                        }
                        var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/products", ImageName);
                        await using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await Image.CopyToAsync(stream);
                        }
                        oldProduct.Image = $"/img/products/{ImageName}";
                    }
                    oldProduct.Name = product.Name;
                    oldProduct.Description = product.Description;
                    oldProduct.Price = product.Price;

                    _context.Update(oldProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Dashboard/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Dashboard/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
