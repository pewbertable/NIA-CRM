using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Data; 

namespace WebApplication1.Controllers
{
    public class CompanyIndustryController : Controller
    {
        private readonly NewDbContext _context;

        public CompanyIndustryController(NewDbContext context)
        {
            _context = context;
        }

        // GET: CompanyIndustry
        public async Task<IActionResult> Index()
        {
            var newDbContext = _context.CompanyIndustries.Include(c => c.Company);
            return View(await newDbContext.ToListAsync());
        }

        // GET: CompanyIndustry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyIndustry = await _context.CompanyIndustries
                .Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyIndustry == null)
            {
                return NotFound();
            }

            return View(companyIndustry);
        }

        // GET: CompanyIndustry/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name");
            return View();
        }

        // POST: CompanyIndustry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyId,Industry")] CompanyIndustry companyIndustry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyIndustry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", companyIndustry.CompanyId);
            return View(companyIndustry);
        }

        // GET: CompanyIndustry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyIndustry = await _context.CompanyIndustries.FindAsync(id);
            if (companyIndustry == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", companyIndustry.CompanyId);
            return View(companyIndustry);
        }

        // POST: CompanyIndustry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyId,Industry")] CompanyIndustry companyIndustry)
        {
            if (id != companyIndustry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyIndustry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyIndustryExists(companyIndustry.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", companyIndustry.CompanyId);
            return View(companyIndustry);
        }

        // GET: CompanyIndustry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyIndustry = await _context.CompanyIndustries
                .Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyIndustry == null)
            {
                return NotFound();
            }

            return View(companyIndustry);
        }

        // POST: CompanyIndustry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyIndustry = await _context.CompanyIndustries.FindAsync(id);
            if (companyIndustry != null)
            {
                _context.CompanyIndustries.Remove(companyIndustry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyIndustryExists(int id)
        {
            return _context.CompanyIndustries.Any(e => e.Id == id);
        }
    }
}
