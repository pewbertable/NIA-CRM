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
    public class MembershipCancellationController : Controller
    {
        private readonly NewDbContext _context;

        public MembershipCancellationController(NewDbContext context)
        {
            _context = context;
        }

        // GET: MembershipCancellation
        public async Task<IActionResult> Index()
        {
            var newDbContext = _context.MembershipCancellations.Include(m => m.Company);
            return View(await newDbContext.ToListAsync());
        }

        // GET: MembershipCancellation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCancellation = await _context.MembershipCancellations
                .Include(m => m.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipCancellation == null)
            {
                return NotFound();
            }

            return View(membershipCancellation);
        }

        // GET: MembershipCancellation/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name");
            return View();
        }

        // POST: MembershipCancellation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CompanyId,Reason,CancellationDate")] MembershipCancellation membershipCancellation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membershipCancellation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", membershipCancellation.CompanyId);
            return View(membershipCancellation);
        }

        // GET: MembershipCancellation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCancellation = await _context.MembershipCancellations.FindAsync(id);
            if (membershipCancellation == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", membershipCancellation.CompanyId);
            return View(membershipCancellation);
        }

        // POST: MembershipCancellation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyId,Reason,CancellationDate")] MembershipCancellation membershipCancellation)
        {
            if (id != membershipCancellation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membershipCancellation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembershipCancellationExists(membershipCancellation.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Name", membershipCancellation.CompanyId);
            return View(membershipCancellation);
        }

        // GET: MembershipCancellation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipCancellation = await _context.MembershipCancellations
                .Include(m => m.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipCancellation == null)
            {
                return NotFound();
            }

            return View(membershipCancellation);
        }

        // POST: MembershipCancellation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var membershipCancellation = await _context.MembershipCancellations.FindAsync(id);
            if (membershipCancellation != null)
            {
                _context.MembershipCancellations.Remove(membershipCancellation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembershipCancellationExists(int id)
        {
            return _context.MembershipCancellations.Any(e => e.Id == id);
        }
    }
}
