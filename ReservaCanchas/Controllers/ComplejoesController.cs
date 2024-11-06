using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservaCanchas.Data;
using ReservaCanchas.Models;

namespace ReservaCanchas.Controllers
{
    public class ComplejoesController : Controller
    {
        private readonly ReservaCanchasContext _context;

        public ComplejoesController(ReservaCanchasContext context)
        {
            _context = context;
        }

        // GET: Complejoes
        public async Task<IActionResult> Index()
        {
            var reservaCanchasContext = _context.Complejo.Include(c => c.Usuario);
            return View(await reservaCanchasContext.ToListAsync());
        }

        // GET: Complejoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complejo = await _context.Complejo
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complejo == null)
            {
                return NotFound();
            }

            return View(complejo);
        }

        // GET: Complejoes/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id");
            return View();
        }

        // POST: Complejoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Ubicacion,Foto,IdUsuario")] Complejo complejo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(complejo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id", complejo.IdUsuario);
            return View(complejo);
        }

        // GET: Complejoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complejo = await _context.Complejo.FindAsync(id);
            if (complejo == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id", complejo.IdUsuario);
            return View(complejo);
        }

        // POST: Complejoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Ubicacion,Foto,IdUsuario")] Complejo complejo)
        {
            if (id != complejo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(complejo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComplejoExists(complejo.Id))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id", complejo.IdUsuario);
            return View(complejo);
        }

        // GET: Complejoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var complejo = await _context.Complejo
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (complejo == null)
            {
                return NotFound();
            }

            return View(complejo);
        }

        // POST: Complejoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var complejo = await _context.Complejo.FindAsync(id);
            if (complejo != null)
            {
                _context.Complejo.Remove(complejo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComplejoExists(int id)
        {
            return _context.Complejo.Any(e => e.Id == id);
        }
    }
}
