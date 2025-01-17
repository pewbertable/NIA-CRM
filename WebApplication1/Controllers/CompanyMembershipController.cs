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
    public class CompanyMembershipController : Controller
    {
        private readonly NewDbContext _context;

        public CompanyMembershipController(NewDbContext context)
        {
            _context = context;
        }

        // GET: CompanyMembership
        public async Task<IActionResult> Index()
        {
            var newDbContext = _context.CompanyMemberships.Include(c => c.Company);
            return View(await newDbContext.ToListAsync());
        }

        // GET: CompanyMembership/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyMembership = await _context.CompanyMemberships
                .Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyMembership == null)
            {
                return NotFound();
            }

            return View(companyMembership);
        }

        // GET: CompanyMembership/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name");
            return View();
        }

        // POST: CompanyMembership/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyId,MembershipType")] CompanyMembership companyMembership)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyMembership);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", companyMembership.CompanyId);
            return View(companyMembership);
        }

        // GET: CompanyMembership/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyMembership = await _context.CompanyMemberships.FindAsync(id);
            if (companyMembership == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", companyMembership.CompanyId);
            return View(companyMembership);
        }

        // POST: CompanyMembership/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyId,MembershipType")] CompanyMembership companyMembership)
        {
            if (id != companyMembership.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyMembership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyMembershipExists(companyMembership.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", companyMembership.CompanyId);
            return View(companyMembership);
        }

        // GET: CompanyMembership/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyMembership = await _context.CompanyMemberships
                .Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (companyMembership == null)
            {
                return NotFound();
            }

            return View(companyMembership);
        }

        // POST: CompanyMembership/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyMembership = await _context.CompanyMemberships.FindAsync(id);
            if (companyMembership != null)
            {
                _context.CompanyMemberships.Remove(companyMembership);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyMembershipExists(int id)
        {
            return _context.CompanyMemberships.Any(e => e.Id == id);
        }
    }
}
