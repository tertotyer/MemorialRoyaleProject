using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemorialRoyaleProject.Data;
using MemorialRoyaleProject.Models;
using MemorialRoyaleProject.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MemorialRoyaleProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MemorialsController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;

        public MemorialsController(IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // GET: Memorials
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Memorial.ToListAsync());
        }

        // GET: Memorials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memorial = await _context.Memorial
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memorial == null)
            {
                return NotFound();
            }

            return View(memorial);
        }

        // GET: Memorials/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Memorials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemorialCreateViewModel memorial)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (memorial.Image != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/memorials");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + memorial.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    memorial.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Memorial newMemorial = new Memorial
                {
                    Model = memorial.Model,
                    Proportion = memorial.Proportion,
                    Granite = memorial.Granite,
                    Nabor = memorial.Nabor,
                    ImagePath = uniqueFileName
                };

                _context.Add(newMemorial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Memorials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memorial = await _context.Memorial.FindAsync(id);
            if (memorial == null)
            {
                return NotFound();
            }
            return View(memorial);
        }

        // POST: Memorials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,Proportion,Granite,Nabor,ImagePath")] Memorial memorial)
        {
            if (id != memorial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memorial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemorialExists(memorial.Id))
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
            return View(memorial);
        }

        // GET: Memorials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memorial = await _context.Memorial
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memorial == null)
            {
                return NotFound();
            }
            _context.Memorial.Remove(memorial);
            await _context.SaveChangesAsync();

            var imagePath = memorial.ImagePath;
            if (System.IO.File.Exists("wwwroot/images/memorials/" + imagePath))
            {
                System.IO.File.Delete("wwwroot/images/memorials/" + imagePath);
            }

            

            return RedirectToAction(nameof(Index));
        }

        private bool MemorialExists(int id)
        {
            return _context.Memorial.Any(e => e.Id == id);
        }
    }
}
