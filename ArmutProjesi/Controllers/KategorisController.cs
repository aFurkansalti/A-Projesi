﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArmutProjesi.Data;
using EntityLayer;

namespace ArmutProjesi.Controllers
{
    public class KategorisController : Controller
    {
        private readonly ArmutProjesiContext _context;

        public KategorisController(ArmutProjesiContext context)
        {
            _context = context;
        }

        // GET: Kategoris
        public async Task<IActionResult> Index()
        {
              return _context.Kategori != null ? 
                          View(await _context.Kategori.ToListAsync()) :
                          Problem("Entity set 'ArmutProjesiContext.Kategori'  is null.");
        }

        // GET: Kategoris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kategori == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategori
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // GET: Kategoris/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kategoris/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KategoriAdi")] Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kategori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategori);
        }

        // GET: Kategoris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kategori == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategori.FindAsync(id);
            if (kategori == null)
            {
                return NotFound();
            }
            return View(kategori);
        }

        // POST: Kategoris/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KategoriAdi")] Kategori kategori)
        {
            if (id != kategori.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriExists(kategori.Id))
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
            return View(kategori);
        }

        // GET: Kategoris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kategori == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategori
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // POST: Kategoris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kategori == null)
            {
                return Problem("Entity set 'ArmutProjesiContext.Kategori'  is null.");
            }
            var kategori = await _context.Kategori.FindAsync(id);
            if (kategori != null)
            {
                _context.Kategori.Remove(kategori);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriExists(int id)
        {
          return (_context.Kategori?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
