using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4.Data;
using Lab4.Models;


/*  
 *  Student Name : Param Savalia (040963842), Kunjesh Aghera (040980391)
 *  Reference: : https://github.com/aarad-ac/EFCore
 *               https://github.com/ParamSavalia/Lab4 
 *  Version : 1.0
*/


namespace Lab4.Controllers
{
    public class CommunityMembershipsController : Controller
    {
        private readonly SchoolCommunityContext _context;

        public CommunityMembershipsController(SchoolCommunityContext context)
        {
            _context = context;
        }

        // GET: CommunityMemberships
        public async Task<IActionResult> Index()
        {
            return View(await _context.CommunityMemberships.ToListAsync());
        }

        // GET: CommunityMemberships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communityMembership = await _context.CommunityMemberships
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (communityMembership == null)
            {
                return NotFound();
            }

            return View(communityMembership);
        }

        // GET: CommunityMemberships/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CommunityMemberships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,CommunityId")] CommunityMembership communityMembership)
        {
            if (ModelState.IsValid)
            {
                _context.Add(communityMembership);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(communityMembership);
        }

        // GET: CommunityMemberships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communityMembership = await _context.CommunityMemberships.FindAsync(id);
            if (communityMembership == null)
            {
                return NotFound();
            }
            return View(communityMembership);
        }

        // POST: CommunityMemberships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,CommunityId")] CommunityMembership communityMembership)
        {
            if (id != communityMembership.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(communityMembership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommunityMembershipExists(communityMembership.StudentId))
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
            return View(communityMembership);
        }

        // GET: CommunityMemberships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communityMembership = await _context.CommunityMemberships
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (communityMembership == null)
            {
                return NotFound();
            }

            return View(communityMembership);
        }

        // POST: CommunityMemberships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var communityMembership = await _context.CommunityMemberships.FindAsync(id);
            _context.CommunityMemberships.Remove(communityMembership);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommunityMembershipExists(int id)
        {
            return _context.CommunityMemberships.Any(e => e.StudentId == id);
        }
    }
}
