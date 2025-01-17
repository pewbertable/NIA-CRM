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
    public class ContactCompanyController : Controller
    {
        private readonly NewDbContext _context;

        public ContactCompanyController(NewDbContext context)
        {
            _context = context;
        }

        // GET: ContactCompany
        public async Task<IActionResult> Index()
        {
            var newDbContext = _context.ContactCompanies.Include(c => c.Company).Include(c => c.Contact);
            return View(await newDbContext.ToListAsync());
        }

        // GET: ContactCompany/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactCompany = await _context.ContactCompanies
                .Include(c => c.Company)
                .Include(c => c.Contact)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactCompany == null)
            {
                return NotFound();
            }

            return View(contactCompany);
        }

        // GET: ContactCompany/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name");
            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Email");
            return View();
        }

        // POST: ContactCompany/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContactId,CompanyId")] ContactCompany contactCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", contactCompany.CompanyId);
            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Email", contactCompany.ContactId);
            return View(contactCompany);
        }

        // GET: ContactCompany/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactCompany = await _context.ContactCompanies.FindAsync(id);
            if (contactCompany == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", contactCompany.CompanyId);
            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Email", contactCompany.ContactId);
            return View(contactCompany);
        }

        // POST: ContactCompany/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContactId,CompanyId")] ContactCompany contactCompany)
        {
            if (id != contactCompany.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactCompanyExists(contactCompany.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", contactCompany.CompanyId);
            ViewData["ContactId"] = new SelectList(_context.Contacts, "Id", "Email", contactCompany.ContactId);
            return View(contactCompany);
        }

        // GET: ContactCompany/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactCompany = await _context.ContactCompanies
                .Include(c => c.Company)
                .Include(c => c.Contact)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactCompany == null)
            {
                return NotFound();
            }

            return View(contactCompany);
        }

        // POST: ContactCompany/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactCompany = await _context.ContactCompanies.FindAsync(id);
            if (contactCompany != null)
            {
                _context.ContactCompanies.Remove(contactCompany);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactCompanyExists(int id)
        {
            return _context.ContactCompanies.Any(e => e.Id == id);
        }
    }
}
