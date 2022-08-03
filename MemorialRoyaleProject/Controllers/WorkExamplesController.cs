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
    public class WorkExamplesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public WorkExamplesController(IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // GET: WorkExamples
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkExample.ToListAsync());
        }

        // GET: WorkExamples/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workExample = await _context.WorkExample
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workExample == null)
            {
                return NotFound();
            }

            return View(workExample);
        }

        // GET: WorkExamples/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkExamples/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkExampleCreateViewModel workExample)
        {
            string uniqueFileName = null;
            if (workExample.Image != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images/WorkExamples");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + workExample.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                workExample.Image.CopyTo(new FileStream(filePath, FileMode.Create));
            }

            WorkExample newWorkExample = new WorkExample
            {
                ImagePath = uniqueFileName
            };

            if (ModelState.IsValid)
            {
                _context.Add(newWorkExample);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(workExample);
        }

        // GET: WorkExamples/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workExample = await _context.WorkExample.FindAsync(id);
            if (workExample == null)
            {
                return NotFound();
            }
            return View(workExample);
        }

        // POST: WorkExamples/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImagePath")] WorkExample workExample)
        {
            if (id != workExample.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workExample);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkExampleExists(workExample.Id))
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
            return View(workExample);
        }

        // GET: WorkExamples/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workExample = await _context.WorkExample
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workExample == null)
            {
                return NotFound();
            }

            _context.WorkExample.Remove(workExample);
            await _context.SaveChangesAsync();

            var imagePath = workExample.ImagePath;
            if (System.IO.File.Exists("wwwroot/images/WorkExamples/" + imagePath))
            {
                System.IO.File.Delete("wwwroot/images/WorkExamples/" + imagePath);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: WorkExamples/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workExample = await _context.WorkExample.FindAsync(id);
            _context.WorkExample.Remove(workExample);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkExampleExists(int id)
        {
            return _context.WorkExample.Any(e => e.Id == id);
        }
    }
}
