﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GaiboSoibifaSail.Models;

namespace GaiboSoibifaSail.Controllers
{
    public class GsMembersController : Controller
    {
        private readonly SailContext _context;

        public GsMembersController(SailContext context)
        {
            _context = context;
        }

        // GET: GsMembers
        public async Task<IActionResult> Index()
        {
            var sailContext = _context.Member.Include(m => m.ProvinceCodeNavigation);
            return View(await sailContext.ToListAsync());
        }

        // GET: GsMembers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .Include(m => m.ProvinceCodeNavigation)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: GsMembers/Create
        public IActionResult Create()
        {
            ViewData["ProvinceCode"] = new SelectList(_context.Province, "ProvinceCode", "ProvinceCode");
            return View();
        }

        // POST: GsMembers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,FullName,FirstName,LastName,SpouseFirstName,SpouseLastName,Street,City,ProvinceCode,PostalCode,HomePhone,Email,YearJoined,Comment,TaskExempt,UseCanadaPost")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProvinceCode"] = new SelectList(_context.Province, "ProvinceCode", "ProvinceCode", member.ProvinceCode);
            return View(member);
        }

        // GET: GsMembers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            ViewData["ProvinceCode"] = new SelectList(_context.Province, "ProvinceCode", "ProvinceCode", member.ProvinceCode);
            return View(member);
        }

        // POST: GsMembers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,FullName,FirstName,LastName,SpouseFirstName,SpouseLastName,Street,City,ProvinceCode,PostalCode,HomePhone,Email,YearJoined,Comment,TaskExempt,UseCanadaPost")] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId))
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
            ViewData["ProvinceCode"] = new SelectList(_context.Province, "ProvinceCode", "ProvinceCode", member.ProvinceCode);
            return View(member);
        }

        // GET: GsMembers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .Include(m => m.ProvinceCodeNavigation)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: GsMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Member.FindAsync(id);
            _context.Member.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.MemberId == id);
        }
    }
}
